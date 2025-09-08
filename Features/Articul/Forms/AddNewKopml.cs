using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.DataAccess.DataFederation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SewingProduction.Core.Class;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.form;
using SewingProduction.Features.Articul;
using DevExpress.XtraGrid;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraGrid.Columns;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ToolTip = System.Windows.Forms.ToolTip;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class AddNewKopml : CustomForm // FoxPro: kompl_new_2016
    {
        ArticulDataService _articulDataService = new ArticulDataService();
        GrupMenDataService _grupMenDataService = new GrupMenDataService();
        KomplDataService komplService = new KomplDataService();
        //private BindingList<SpArticulGrupMenViewModel> _selectedRazmItems = new();
        List<ArticulModel> articuls;
        string xkod; //код комплекта
        bool xFlagKod = false;
        bool xAutoRazm = false; //галочка авторазмер
        bool xAllZap = false; //галочка все записи одинаковые
        private ToolTip toolTip = new ToolTip();

        public AddNewKopml(UserClass user, List<ArticulModel> articuls, string kod) : base(user)
        {
            InitializeComponent();
            xkod = kod;
            this.articuls = articuls;
            toolTipButton();
        }
        public AddNewKopml()
        {
            InitializeComponent();
        }
        #region Ctor & Load
        //загрузка данных
        private async void customGridControlKomplArt_Load(object sender, EventArgs e)
        {
            //var articuls = await _articulDataService.GetAllAsync();
            var grups = await _grupMenDataService.GetAllAsync();

            var result = from a in articuls
                         where !a.Grup.ToLower().Contains("комплект") &&
                               a.Kod.ToString().Length >= 7 &&
                               !a.Kod.ToString().Substring(0, 7).Equals(xkod.Substring(0, 7))
                         join g in grups on a.Grup equals g.Men into gj
                         from g in gj.DefaultIfEmpty()
                         select new SpArticulGrupMenViewModel
                         {
                             Kod = a.Kod,
                             Grup = a.Grup,
                             Articul = a.Articul,
                             Mod = a.Mod,
                             Razm = a.Razm,
                             Sost = a.Sost,
                             Kod_v = a.Kod_v,
                             Kle = a.Kle,
                             GrupMen = g ?? new GrupMenModel(),
                             Po = " ",
                             Pr_po = false
                         };

            var resultList = result.ToList();
            await FillRazmAllAsync(resultList);
            customGridControlKomplArt.DataSource = resultList;


            var resulRazm = from a in articuls
                            where a.Kod.ToString().PadLeft(7, '0').Substring(0, 7) == xkod.Substring(0, 7)
                            join g in grups on a.Grup equals g.Men into gj
                            from g in gj.DefaultIfEmpty()
                            select new SpArticulGrupMenViewModel
                            {
                                Kod = a.Kod,
                                Grup = a.Grup,
                                Articul = a.Articul,
                                Mod = a.Mod,
                                Razm = a.Razm,
                                Sost = a.Sost,
                                Kod_v = a.Kod_v,
                                Kle = a.Kle,
                                GrupMen = g ?? new GrupMenModel(),
                                Po = " ",
                                Pr_po = false
                            };
            var resulRazmList = resulRazm.ToList();
            await FillRazmAllAsync(resulRazmList); // 🔹 заполняем razm_all
            customGridControlKomplRazm.DataSource = resulRazmList;

            var firstRazm = resulRazm.FirstOrDefault();
            if (firstRazm != null)
            {
                customLabelGrup.Text = firstRazm.Grup;
                customLabelArtText.Text = firstRazm.Articul;
                customLabelModText.Text = firstRazm.Mod;
            }

            await loadKomplByArticul(firstRazm.Articul);
            int handle = gridViewKomplRazm.LocateByValue("Kod", xkod.Trim());
            gridViewKomplRazm.MakeRowVisible(handle); BeginInvoke(new Action(() =>
            {
                if (int.TryParse(xkod, out var val))
                {
                    int h = gridViewKomplRazm.LocateByValue("Kod", val);
                    if (h >= 0)
                    {
                        gridViewKomplRazm.FocusedRowHandle = h;
                        gridViewKomplRazm.MakeRowVisible(h);
                        xFlagKod = true;
                    }
                }
            }));
        }
        // галочка Автоподбор
        private void customCheckBoxAutoRazm_CheckedChanged(object sender, EventArgs e)
        {
            xAutoRazm = customCheckBoxAutoRazm.Checked;

            if (xAutoRazm && !ValidateRazmAll())
            {
                xAutoRazm = false;
                customCheckBoxAutoRazm.Checked = false;
                return; 
            }

            // Сброс значений
            customNumericUpDownValueTab.Value = 0;
            customButtonKompl.Enabled = false;
            customCheckBoxVerified.Checked = false;

            if (gridViewKomplRazm?.DataSource is not List<SpArticulGrupMenViewModel> data)
                return;

            foreach (var item in data)
                item.Pr_po = false;

            gridViewKomplRazm.RefreshData();

            // В ЛЮБОМ режиме колонка pr_po остаётся редактируемой
            var col = gridViewKomplRazm.Columns["Pr_po"];
            if (col != null)
                col.OptionsColumn.AllowEdit = true;

            ApplyGroupingByRazmAll(xAutoRazm ? true : false);
        }
        //проверка на дубли в авторазмере
        private bool ValidateRazmAll()
        {
            for (int i = 0; i < gridViewKomplRazm.RowCount; i++)
            {
                var razmAll = gridViewKomplRazm.GetRowCellValue(i, "Razm_all")?.ToString();
                Debug.WriteLine(razmAll);
                if (string.IsNullOrWhiteSpace(razmAll)) continue;
                int count = komplService.GetCountByRazmAll(razmAll);
                if (count > 1)
                {
                    MessageBox.Show($"Ошибка размерного ряда. Общий размер не уникален. Автоподбор невозможен",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        //галочка все записи одинковые 
        private void customCheckBoxOdinak_CheckedChanged(object sender, EventArgs e)
        {
            xAllZap = customCheckBoxOdinak.Checked;
            customNumericUpDownValueTab.Value = 0;
            customButtonKompl.Enabled = false;
            customCheckBoxVerified.Checked = false;
        }
        #endregion
        #region TabPage
        //выбор количества страниц
        private void customNumericUpDownValueTab_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)customNumericUpDownValueTab.Value;
            if (count < 0 || count > 10)
                return;

            while (customTabControlKomplRazm.TabPages.Count > 0)
                customTabControlKomplRazm.TabPages.RemoveAt(customTabControlKomplRazm.TabPages.Count - 1);

            for (int i = 1; i <= count; i++)
            {
                CreateKomplRazmTabPage(i);
            }

            customButtonDelKomplSelected_Click(sender, e);
            // блочим кнопки комплектовки:
            customCheckBoxVerified.Checked = false;
            customButtonKompl.Enabled = false;
        }
        //создание вкладок
        private void CreateKomplRazmTabPage(int index)
        {
            // создаем страницу
            var tabPage = new XtraTabPage
            {
                Name = $"xtraTabPageKomplRazm{index}",
                Text = $"{index}",
            };
            if (xAllZap)
                tabPage.PageVisible = index > 1 ? false : true;
            // Таблица внутри страницы
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92)); // грид
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8)); // кнопка

            // Создаём грид
            var grid = new CustomGridControl
            {
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(grid, 0, 0);

            // Создаём кнопку Х
            var removeButton = new CustomButton
            {
                Text = "✖",
                Dock = DockStyle.Fill,
                MaximumSize = new System.Drawing.Size(25, 25),
                MinimumSize = new System.Drawing.Size(25, 25)
            };
            layout.Controls.Add(removeButton, 1, 0);
            removeButton.Click += (s, e) => RemoveButtonClick(s, grid);
            toolTip.SetToolTip(removeButton, "Очистить страницу");

            // Создаём GridView
            var view = new DevExpress.XtraGrid.Views.Grid.GridView(grid);
            grid.MainView = view;
            grid.ViewCollection.Add(view);

            // Привязываем пустой список данных
            grid.DataSource = new BindingList<SpArticulGrupMenViewModel>();

            // Добавляем стандартные колонки
            view.Columns.AddVisible("Articul", "Артикул");
            view.Columns.AddVisible("Mod", "Модель");
            view.Columns.AddVisible("Kod", "Код");
            view.Columns.AddVisible("Razm", "Размер");
            view.Columns.AddVisible("Kle", "ТМ");
            view.Columns.AddVisible("Razm_all", "Основной Размер");

            //view.OptionsBehavior.Editable = false; 
            foreach (var col in view.Columns.Cast<DevExpress.XtraGrid.Columns.GridColumn>())
                col.OptionsColumn.AllowEdit = false;
            //col.OptionsColumn.AllowEdit = col.FieldName == "pr_po" ? true : false;

            // Колонка выбора (pr_po)
            var checkColumn = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Caption = "Выбор",
                FieldName = "Pr_po",
                Visible = true
            };
            checkColumn.OptionsColumn.AllowEdit = !xAutoRazm;

            var checkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
            {
                ValueChecked = true,
                ValueUnchecked = false,
                ValueGrayed = false,
                NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
            };

            checkEdit.EditValueChanged += (s, e) => CheckEditValueChanged(s, grid);

            grid.RepositoryItems.Add(checkEdit);
            checkColumn.ColumnEdit = checkEdit;
            view.Columns.Add(checkColumn);

            // Добавляем layout в вкладку
            tabPage.Controls.Add(layout);

            // Добавляем вкладку в контрол
            customTabControlKomplRazm.TabPages.Add(tabPage);
        }

        private BindingList<SpArticulGrupMenViewModel> _selectedKomplItems = new();

        // карта выбранного на вкладках: tabIndex -> (kod, razm)
        private readonly Dictionary<int, (int kod, string razm)> _tabSelection = new();

        private void CheckEditValueChanged(object sender, CustomGridControl grid)
        {
            customCheckBoxVerified.Checked = false;

            var editor = sender as DevExpress.XtraEditors.CheckEdit; // нужен, чтобы понять — сняли или поставили
            var view = grid?.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            var selected = view.GetRow(view.FocusedRowHandle) as SpArticulGrupMenViewModel;
            if (selected == null) return;

            if (grid.DataSource is not BindingList<SpArticulGrupMenViewModel> data) return;

            int tabIndex = customTabControlKomplRazm.SelectedTabPageIndex;

            bool wasChecked = selected.Pr_po;
            bool newChecked = editor?.Checked ?? !wasChecked; // если нет editor — считаем, что клик инвертирует

            // ===== 1) АВТО + ОДИНАКОВО =====
            if (xAutoRazm && xAllZap)
            {
                // Обычно ручной клик в табах при авторазмере блокируется.
                // Если всё же пришёл — трактуем как выбор образца: одна строка внизу, количество = вкладки * галочки слева.
                int tabsCount = (int)customNumericUpDownValueTab.Value;
                int checkedCount = 0;
                for (int i = 0; i < gridViewKomplRazm.RowCount; i++)
                    if (gridViewKomplRazm.GetRow(i) is SpArticulGrupMenViewModel r && r.Pr_po)
                        checkedCount++;
                if (checkedCount <= 0) checkedCount = 1;

                _selectedKomplItems.Clear();
                _selectedKomplItems.Add(new SpArticulGrupMenViewModel
                {
                    Kod = selected.Kod,
                    Grup = selected.Grup,
                    Articul = selected.Articul,
                    Mod = selected.Mod,
                    Razm = selected.Razm,
                    Razm_all = selected.Razm_all,
                    Sost = selected.Sost,
                    Kod_v = selected.Kod_v,
                    Kle = selected.Kle,
                    Po = selected.Po,
                    Pr_po = true,
                    GrupMen = selected.GrupMen,
                    TabIndex = 0,
                    countStr = tabsCount * checkedCount
                });

                customGridControlKomplSelected.RefreshDataSource();
                return;
            }

            // ===== 2) АВТО + РАЗНЫЕ =====
            if (xAutoRazm && !xAllZap)
            {
                // В авторазмере обычно табы нередактируемые; но если клик прошёл — агрегируем по (kod, razm)
                selected.Pr_po = newChecked;
                selected.TabIndex = tabIndex;

                var agg = _selectedKomplItems.FirstOrDefault(x => x.Kod == selected.Kod && x.Razm == selected.Razm);
                if (newChecked)
                {
                    if (agg != null) agg.countStr += 1;
                    else { selected.countStr = 1; _selectedKomplItems.Add(selected); }
                }
                else
                {
                    if (agg != null)
                    {
                        if (agg.countStr > 1) agg.countStr -= 1;
                        else _selectedKomplItems.Remove(agg);
                    }
                }

                view.RefreshData();
                customGridControlKomplSelected.RefreshDataSource();
                return;
            }

            // ===== 3) НЕ АВТО + ОДИНАКОВО =====
            if (!xAutoRazm && xAllZap)
            {
                // Здесь как раз твой кейс: при СНЯТИИ галочки строка внизу должна пропасть.
                if (wasChecked && !newChecked)
                {
                    // Снятие: просто убираем выбор и чистим низ
                    selected.Pr_po = false;
                    view.RefreshData();

                    _selectedKomplItems.Clear();
                    customGridControlKomplSelected.RefreshDataSource();

                    _tabSelection.Remove(tabIndex);
                    return;
                }

                // Установка: на странице ровно одна галочка, одна строка с количеством = числу вкладок
                foreach (var item in data) item.Pr_po = false;
                selected.Pr_po = true;
                selected.TabIndex = tabIndex;
                view.RefreshData();

                int tabsCount = (int)customNumericUpDownValueTab.Value;

                _selectedKomplItems.Clear();
                _selectedKomplItems.Add(new SpArticulGrupMenViewModel
                {
                    Kod = selected.Kod,
                    Grup = selected.Grup,
                    Articul = selected.Articul,
                    Mod = selected.Mod,
                    Razm = selected.Razm,
                    Razm_all = selected.Razm_all,
                    Sost = selected.Sost,
                    Kod_v = selected.Kod_v,
                    Kle = selected.Kle,
                    Po = selected.Po,
                    Pr_po = true,
                    GrupMen = selected.GrupMen,
                    TabIndex = 0,
                    countStr = tabsCount
                });

                _tabSelection[tabIndex] = (selected.Kod, selected.Razm);
                customGridControlKomplSelected.RefreshDataSource();
                return;
            }

            // ===== 4) НЕ АВТО + РАЗНЫЕ =====
            // На странице можно выбрать максимум одну строку.
            if (wasChecked && !newChecked)
            {
                // Снятие текущего выбора этой вкладки
                selected.Pr_po = false;
                view.RefreshData();

                // Уменьшаем агрегат у пары, снятой на этой вкладке
                var agg = _selectedKomplItems.FirstOrDefault(x => x.Kod == selected.Kod && x.Razm == selected.Razm);
                if (agg != null)
                {
                    if (agg.countStr > 1) agg.countStr -= 1;
                    else _selectedKomplItems.Remove(agg);
                }


                _tabSelection.Remove(tabIndex);
                customGridControlKomplSelected.RefreshDataSource();
                return;
            }


            // Установка новой
            foreach (var item in data) item.Pr_po = false;
            selected.Pr_po = true;
            selected.TabIndex = tabIndex;
            view.RefreshData();

            // Если на вкладке раньше была другая пара — её нужно уменьшить
            if (_tabSelection.TryGetValue(tabIndex, out var prev))
            {
                if (prev.kod != selected.Kod || prev.razm != selected.Razm)
                {
                    var prevAgg = _selectedKomplItems.FirstOrDefault(x => x.Kod == prev.kod && x.Razm == prev.razm);
                    if (prevAgg != null)
                    {
                        if (prevAgg.countStr > 1) prevAgg.countStr -= 1;
                        else _selectedKomplItems.Remove(prevAgg);
                    }
                }
            }

            // Учесть новую пару
            var curAgg = _selectedKomplItems.FirstOrDefault(x => x.Kod == selected.Kod && x.Razm == selected.Razm);
            if (curAgg != null) curAgg.countStr += 1;
            else { selected.countStr = 1; _selectedKomplItems.Add(selected); }

            _tabSelection[tabIndex] = (selected.Kod, selected.Razm);
            customGridControlKomplSelected.RefreshDataSource();

            if (!xAutoRazm && !xAllZap)
                // Переход на следующую незаполненную вкладку
                for (int i = 0; i < customTabControlKomplRazm.TabPages.Count; i++)
                {
                    var tab = customTabControlKomplRazm.TabPages[i];
                    if (tab.Controls[0] is not TableLayoutPanel layout)
                        continue;
                    var nextGrid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                    if (nextGrid?.DataSource is not BindingList<SpArticulGrupMenViewModel> list)
                        continue;
                    if (!list.Any(x => x.Pr_po))
                    {
                        customTabControlKomplRazm.SelectedTabPageIndex = i;
                        break;
                    }
                }
        }

        private void customButtonNext_Click(object sender, EventArgs e)
        {
            if (customTabControlKomplRazm.SelectedTabPageIndex < customTabControlKomplRazm.TabPages.Count - 1)
            {
                customTabControlKomplRazm.SelectedTabPageIndex++;
            }
        }

        private void customButtonBack_Click(object sender, EventArgs e)
        {
            if (customTabControlKomplRazm.SelectedTabPageIndex > 0)
            {
                customTabControlKomplRazm.SelectedTabPageIndex--;
            }
        }
        #endregion
        #region SelectZapisi
        private void repositoryItemCheckEditViborRazm_CheckedChanged(object sender, EventArgs e)
        {
            var view = gridViewKomplRazm;
            if (view?.GetFocusedRow() is not SpArticulGrupMenViewModel selected) return;

            // Фактическое новое состояние чекбокса (а не текущее значение в модели)
            bool desired = (sender as DevExpress.XtraEditors.CheckEdit)?.Checked ?? selected.Pr_po;

            // Быстрая валидация — если хотим поставить галочку, но позиция заблокирована
            if (desired)
            {
                if (komplService.CheckInProizv(selected.Kod.ToString()))
                {
                    // Снимаем галочку и выходим
                    selected.Pr_po = false;
                    view.RefreshRow(view.FocusedRowHandle);
                    customCheckBoxVerified.Checked = false;
                    MessageBox.Show("Невозможно комплектовать — запущено в производство", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (komplService.CheckNaklRas(selected.Kod.ToString()))
                {
                    selected.Pr_po = false;
                    view.RefreshRow(view.FocusedRowHandle);
                    customCheckBoxVerified.Checked = false;
                    MessageBox.Show("Невозможно комплектовать — созданы накладные", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            view.BeginDataUpdate();
            try
            {
                if (xAutoRazm)
                {
                    // В авто-режиме не инвертируем, а ставим то, что запросил пользователь
                    selected.Pr_po = desired;
                    AutoRazmForForming();
                }
                else
                {
                    // Режим «только одна галочка»
                    var data = view.DataSource as List<SpArticulGrupMenViewModel>;
                    if (data != null)
                    {
                        if (desired)
                        {
                            // Снимаем у всех и ставим у выбранной
                            foreach (var item in data)
                                item.Pr_po = false;

                            selected.Pr_po = true;
                        }
                        else
                        {
                            // Просто сняли галочку на выбранной
                            selected.Pr_po = false;
                        }
                    }
                }
            }
            finally
            {
                view.EndDataUpdate();
            }

            // Минимальная перерисовка
            view.RefreshRow(view.FocusedRowHandle);
            customCheckBoxVerified.Checked = false;
        }


        // кнопки "+" для добавления записей на таблицу
        private async void repositoryItemButtonEditAddRazm_Click(object sender, EventArgs e)
        {
            var view = gridViewKomplArt;
            if (view?.GetFocusedRow() is not SpArticulGrupMenViewModel selectedRow)
                return;

            var kompl_art = gridViewKomplArt.GetFocusedRow() as SpArticulGrupMenViewModel;
            var mykompl_razm = gridViewKomplRazm.GetFocusedRow() as SpArticulGrupMenViewModel;

            if (mykompl_razm.Kle != kompl_art.Kle)
            {
                MessageBox.Show("Комплектовать модели нельзя — разные торговые марки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mykompl_razm.Kod_v == 2 && kompl_art.Kod_v == 2 && mykompl_razm.GrupMen.Frm_s != kompl_art.GrupMen.Frm_s)
            {
                MessageBox.Show("Комлектовать модели нельзя - разные производители", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Готовим артикулы
            var allRows = customGridControlKomplArt.DataSource as List<SpArticulGrupMenViewModel>;
            if (allRows == null) return;

            var sameArticulRows = await Task.Run(() =>
            {
                return allRows
                    .Where(x => x.Articul == selectedRow.Articul)
                    .ToList();
            });

            if (sameArticulRows.Count == 0) return;

            // заполняем razm_all для добавляемых строк
            await FillRazmAllAsync(sameArticulRows);

            // Ищем первую свободную вкладку
            BindingList<SpArticulGrupMenViewModel> targetList = null;
            int targetIndex = -1;

            for (int i = 0; i < customTabControlKomplRazm.TabPages.Count; i++)
            {
                var tab = customTabControlKomplRazm.TabPages[i];
                if (tab.Controls[0] is not TableLayoutPanel layout) continue;

                var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                if (grid == null) continue;

                if (grid.DataSource is not BindingList<SpArticulGrupMenViewModel> list)
                {
                    list = new BindingList<SpArticulGrupMenViewModel>();
                    grid.DataSource = list;
                }

                if (list.Count == 0)
                {
                    targetList = list;
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex == -1 || targetList == null)
            {
                MessageBox.Show("Нет свободных вкладок для добавления артикула", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Переключаемся на нужную вкладку
            customTabControlKomplRazm.SelectedTabPageIndex = targetIndex;

            // Добавляем строки без дублей по (kod, razm)
            var kodRazmSet = new HashSet<(int, string)>(targetList.Select(x => (x.Kod, x.Razm)));

            foreach (var row in sameArticulRows)
            {
                var key = (row.Kod, row.Razm);
                if (!kodRazmSet.Contains(key))
                {
                    targetList.Add(new SpArticulGrupMenViewModel
                    {
                        Kod = row.Kod,
                        Grup = row.Grup,
                        Articul = row.Articul,
                        Mod = row.Mod,
                        Razm = row.Razm,
                        Razm_all = row.Razm_all, // 🔹 переносим общий размер
                        Sost = row.Sost,
                        Kod_v = row.Kod_v,
                        Kle = row.Kle,
                        Po = row.Po,
                        Pr_po = row.Pr_po,
                        GrupMen = row.GrupMen,
                        TabIndex = targetIndex
                    });
                    kodRazmSet.Add(key);
                }
            }

            // Если авторазмер включён — сразу авторасставим галки в вкладках
            if (xAutoRazm)
                AutoRazmForForming();
            // если все записи одинаковые создаем дубликаты
            if (xAllZap && targetIndex < customNumericUpDownValueTab.Value - 1)
                repositoryItemButtonEditAddRazm_Click(sender, e);
        }
        #endregion
        #region Button for delete
        //Button on TabPage
        void RemoveButtonClick(Object s, CustomGridControl grid)
        {
            if (xAllZap)
            {
                // 1) очищаем выбранные элементы (нижний грид)
                _selectedKomplItems.Clear();
                customGridControlKomplSelected.DataSource = _selectedKomplItems;
                customGridControlKomplSelected.RefreshDataSource();

                // 2) сбрасываем чек "проверено"
                customCheckBoxVerified.Checked = false;

                // 3) Сбрасываем pr_po и очищаем списки на КАЖДОЙ вкладке
                foreach (XtraTabPage tab in customTabControlKomplRazm.TabPages)
                {
                    if (tab.Controls.Count == 0) continue;
                    if (tab.Controls[0] is not TableLayoutPanel layout) continue;

                    var pageGrid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                    if (pageGrid?.DataSource is BindingList<SpArticulGrupMenViewModel> pageList)
                    {
                        foreach (var item in pageList)
                            item.Pr_po = false;

                        pageList.Clear();
                        pageGrid.RefreshDataSource();
                    }
                }

                // 4) дополнительно (если нужно) снять галочки слева в общем списке размеров
                //    чтобы «авторазмер» тоже сбросился визуально
                var razmList = customGridControlKomplRazm.DataSource as List<SpArticulGrupMenViewModel>;
                if (razmList != null)
                {
                    foreach (var r in razmList) r.Pr_po = false;
                    gridViewKomplRazm?.RefreshData();
                }

                return;
            }

            var list = grid.DataSource as BindingList<SpArticulGrupMenViewModel>;
            if (list == null || list.Count == 0) return;

            // Получаем номер вкладки, к которой относится этот грид
            var parentTab = customTabControlKomplRazm.TabPages
                .FirstOrDefault(tab => tab.Controls[0].Controls.Contains(grid));

            if (parentTab == null) return;

            int tabIndex = customTabControlKomplRazm.TabPages.IndexOf(parentTab);

            // Удаляем из _selectedKomplItems все строки, добавленные с этой вкладки
            var itemsToRemove = _selectedKomplItems
                .Where(x => x.TabIndex == tabIndex)
                .ToList();

            foreach (var item in itemsToRemove)
                _selectedKomplItems.Remove(item);

            // Обновляем таблицу выбранных
            customGridControlKomplSelected.DataSource = _selectedKomplItems;
            customGridControlKomplSelected.RefreshDataSource();

            customCheckBoxVerified.Checked = false;

            // Сбрасываем pr_po
            foreach (var item in list)
                item.Pr_po = false;

            // Очищаем грид этой вкладки
            list.Clear();

        }

        // Button on customGridControlKomplSelected — удаление выбранной строки
        private void repositoryItemButtonEditDelRazm_Click(object sender, EventArgs e)
        {
            var viewSel = bandedGridViewSelected;
            if (viewSel == null) return;

            var selected = viewSel.GetFocusedRow() as SpArticulGrupMenViewModel;
            if (selected == null) return;

            // РЕЖИМ АВТОРАЗМЕР
            if (xAutoRazm)
            {
                // найти в левом гриде запись с тем же razm_all и снять pr_po
                var leftData = gridViewKomplRazm?.DataSource as List<SpArticulGrupMenViewModel>;
                if (leftData != null)
                {
                    var leftRow = leftData.FirstOrDefault(r =>
                        string.Equals(r.Razm_all?.Trim(), selected.Razm_all?.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (leftRow != null)
                    {
                        leftRow.Pr_po = false;
                        gridViewKomplRazm.RefreshData(); // визуально обновим левый список
                    }
                }

                // перестроить галочки на вкладках и нижний список
                AutoRazmForForming(); // учтёт новые pr_po в левом гриде и обновит _selectedKomplItems

                customCheckBoxVerified.Checked = false;
                return;
            }

            _selectedKomplItems.Remove(selected);
            customGridControlKomplSelected.DataSource = _selectedKomplItems;

            if (selected.TabIndex >= 0 && selected.TabIndex < customTabControlKomplRazm.TabPages.Count)
            {
                var tab = customTabControlKomplRazm.TabPages[selected.TabIndex];
                if (tab.Controls[0] is TableLayoutPanel layout)
                {
                    var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                    if (grid?.DataSource is BindingList<SpArticulGrupMenViewModel> list)
                    {
                        var matchingRow = list.FirstOrDefault(x => x.Kod == selected.Kod && x.Razm == selected.Razm);
                        if (matchingRow != null)
                        {
                            matchingRow.Pr_po = false;
                            grid.MainView.RefreshData();
                        }
                    }
                }
            }

            customCheckBoxVerified.Checked = false;
        }


        private void customButtonDelKomplSelected_Click(object sender, EventArgs e)
        {
            // 1. Сбросить pr_po = false во всех вкладках
            foreach (XtraTabPage tab in customTabControlKomplRazm.TabPages)
            {
                if (tab.Controls[0] is not TableLayoutPanel layout) continue;

                var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                if (grid?.DataSource is BindingList<SpArticulGrupMenViewModel> list)
                {
                    foreach (var row in list)
                        row.Pr_po = false;

                    grid.MainView.RefreshData();
                }
            }

            // 2. Очистить список выбранных строк
            _selectedKomplItems.Clear();

            // 3. Обновить грид с выбранными
            customGridControlKomplSelected.DataSource = _selectedKomplItems;
            customGridControlKomplSelected.RefreshDataSource();

            customCheckBoxVerified.Checked = false;
        }
        private void customButtonDelKompl_Click(object sender, EventArgs e)
        {
            var list = customGridControlKompl.DataSource as List<object>;
            if (list == null) return;

            list.Clear();
            customCheckBoxVerified.Checked = false;
            customGridControlKompl.DataSource = null;
            customGridControlKompl.RefreshDataSource();
        }
        #endregion
        #region LogicForAutomize

        // Авторасстановка галочек + автоформирование списка выбранных по razm_all с накоплением countStr
        private void AutoRazmForForming()
        {
            var gridData = customGridControlKomplRazm.DataSource as List<SpArticulGrupMenViewModel>;
            if (gridData == null) return;

            // 1) какие общие размеры выбраны слева
            var selectedRazmAll = gridData
                .Where(x => x.Pr_po)
                .Select(x => x.Razm_all?.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .ToList();

            // если ничего не выбрано — очистить
            if (selectedRazmAll.Count == 0)
            {
                _selectedKomplItems.Clear();
                customGridControlKomplSelected.DataSource = _selectedKomplItems;
                customGridControlKomplSelected.RefreshDataSource();
                SetupSelectedGroupingByRazmAll();
                customCheckBoxVerified.Checked = false;
                return;
            }

            // 2) проставляем галочки во всех вкладках по razm_all
            foreach (XtraTabPage tab in customTabControlKomplRazm.TabPages)
            {
                if (tab.Controls[0] is not TableLayoutPanel layout)
                    continue;

                var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                if (grid?.DataSource is not BindingList<SpArticulGrupMenViewModel> list)
                    continue;

                foreach (var row in list)
                    row.Pr_po = selectedRazmAll.Contains(row.Razm_all?.Trim());

                grid.RefreshDataSource();
            }

            // 3) Сформировать выбранные с НАКОПЛЕНИЕМ countStr по (kod, razm)
            _selectedKomplItems.Clear();
            var agg = new Dictionary<(int kod, string razm), SpArticulGrupMenViewModel>();

            for (int i = 0; i < customTabControlKomplRazm.TabPages.Count; i++)
            {
                var tab = customTabControlKomplRazm.TabPages[i];
                if (tab.Controls[0] is not TableLayoutPanel layout) continue;

                var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                if (grid?.DataSource is not BindingList<SpArticulGrupMenViewModel> list) continue;

                foreach (var row in list.Where(r => r.Pr_po))
                {
                    var key = (row.Kod, row.Razm);
                    if (!agg.TryGetValue(key, out var acc))
                    {
                        // копируем ссылку/объект и обнуляем счётчик перед накоплением
                        row.TabIndex = i;
                        row.countStr = 1;
                        agg[key] = row;
                    }
                    else
                    {
                        acc.countStr += 1;
                    }
                }
            }

            foreach (var item in agg.Values)
                _selectedKomplItems.Add(item);

            customGridControlKomplSelected.DataSource = _selectedKomplItems;
            customGridControlKomplSelected.RefreshDataSource();

            // 4) группировка по razm_all
            SetupSelectedGroupingByRazmAll();

            // просим перепроверить
            customCheckBoxVerified.Checked = false;
        }

        /// <summary>
        /// Группируем customGridControlKomplSelected по общему размеру razm_all.
        /// Создаём колонку/бэнд при необходимости.
        /// Вызывать сразу после установки DataSource у customGridControlKomplSelected.
        /// </summary>
        // группировка выбранных по razm_all (общему размеру)
        private void SetupSelectedGroupingByRazmAll()
        {
            var view = bandedGridViewSelected;
            if (view == null) return;

            // Бэнд "Размер" — создаём если нет
            var band = view.Bands.FirstOrDefault(b => b.Name == "bandRazmAll");
            if (band == null)
            {
                band = view.Bands.AddBand("Размер");
                band.Name = "bandRazmAll";
            }

            // Колонка razm_all — создаём если нет
            var colRazmAll = view.Columns.ColumnByFieldName("Razm_all");
            if (colRazmAll == null)
            {
                colRazmAll = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                {
                    FieldName = "Razm_all",
                    Caption = "Размер",
                    Visible = false
                };
                view.Columns.Add(colRazmAll);
                band.Columns.Add(colRazmAll);
            }
            else
            {
                if (colRazmAll.OwnerBand != band)
                    band.Columns.Add(colRazmAll);
                colRazmAll.Visible = false;
                colRazmAll.Caption = "Размер";
            }

            view.BeginUpdate();
            try
            {
                view.ClearGrouping();
                view.SortInfo.Clear();
                colRazmAll.GroupIndex = 0;
                view.SortInfo.Add(colRazmAll, DevExpress.Data.ColumnSortOrder.Ascending);
                view.OptionsBehavior.AutoExpandAllGroups = true;
                view.ExpandAllGroups();
                view.OptionsView.ShowGroupPanel = false;
            }
            finally
            {
                view.EndUpdate();
            }
        }
        private async Task FillRazmAllAsync(IEnumerable<SpArticulGrupMenViewModel> items)
        {
            if (items == null) return;

            // Группируем уникальные значения раздельных размеров
            var byRazm = items
                .Where(x => !string.IsNullOrWhiteSpace(x.Razm))
                .GroupBy(x => x.Razm.Trim())
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var key in byRazm.Keys.ToList())
            {
                // Берём общий размер из таблицы Razm (ArticulDataService.GetAllRazmByRazmAsync)
                var razmAll = await _articulDataService.GetAllRazmByRazmAsync(key);
                if (!string.IsNullOrWhiteSpace(razmAll))
                {
                    foreach (var row in byRazm[key])
                        row.Razm_all = razmAll.Trim();
                }
            }
        }
        // Включает/выключает группировку по общему размеру (razm_all) в customGridControlKomplSelected
        private void ApplyGroupingByRazmAll(bool enabled)
        {
            var view = bandedGridViewSelected;                  // MainView у customGridControlKomplSelected
            if (view == null) return;

            var colRazmAll = view.Columns.ColumnByFieldName("Razm_all");
            if (colRazmAll == null) return;                     // колонка должна быть добавлена в дизайнере

            view.BeginUpdate();
            try
            {
                // Полный сброс прошлого состояния
                view.ClearGrouping();
                view.GroupCount = 0;
                view.SortInfo.Clear();
                colRazmAll.GroupIndex = -1;
                colRazmAll.SortMode = DevExpress.XtraGrid.ColumnSortMode.Default; // на всякий случай
                colRazmAll.SortOrder = DevExpress.Data.ColumnSortOrder.None;

                if (enabled)
                {
                    // Вариант без дублирования SortInfo при повторных вызовах
                    view.GroupCount = 1;
                    view.SortInfo.Add(new DevExpress.XtraGrid.Columns.GridColumnSortInfo(
                        colRazmAll, DevExpress.Data.ColumnSortOrder.Ascending));

                    view.OptionsBehavior.AutoExpandAllGroups = true;
                    view.OptionsView.ShowGroupPanel = false;
                    view.ExpandAllGroups();
                }
                else
                {
                    view.OptionsBehavior.AutoExpandAllGroups = false;
                    view.OptionsView.ShowGroupPanel = false;
                }
            }
            finally
            {
                view.EndUpdate();
            }
        }
        #endregion
        #region kompl
        private void customCheckBoxVerified_CheckedChanged(object sender, EventArgs e)
        {
            int requiredCount = (int)customNumericUpDownValueTab.Value;
            if (requiredCount < 2 || requiredCount > 10)
            {
                VerifiedCheckedFalse();
                MessageBox.Show("Количество артикулов должно быть от 2 до 10", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int selectedCount = _selectedKomplItems.Sum(x => x.countStr);
            // Считаем галочки в gridViewKomplRazm
            int checkedCount = 0;
            for (int i = 0; i < gridViewKomplRazm.RowCount; i++)
            {
                var row = gridViewKomplRazm.GetRow(i) as SpArticulGrupMenViewModel;
                if (row != null && row.Pr_po)
                    checkedCount++;
            }

            // Умножаем требуемое количество на число галочек
            requiredCount *= checkedCount;

            if (requiredCount == 0)
            {
                VerifiedCheckedFalse();
                MessageBox.Show(
                    $"Необходимо заполнить поле количество и выбрать артикулы для комплекта.",
                    "Нет данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            if (selectedCount != requiredCount && customCheckBoxVerified.Checked)
            {
                VerifiedCheckedFalse();
                MessageBox.Show(
                    $"Необходимо выбрать ровно {requiredCount} позиций для подтверждения комплекта.",
                    "Недостаточно позиций",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            if (customCheckBoxVerified.Checked)
                customButtonKompl.Enabled = true;
            else
                customButtonKompl.Enabled = false;

            var razmList = customGridControlKomplRazm.DataSource as List<SpArticulGrupMenViewModel>;
            if (razmList == null || razmList.Count == 0)
            {
                VerifiedCheckedFalse();
                MessageBox.Show("Нет данных артикула для комплектования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // НЕ АВТОРАЗМЕР
            if (!xAutoRazm)
            {
                var selectedLeft = razmList.Where(x => x.Pr_po).ToList();
                if (selectedLeft.Count != 1)
                {
                    VerifiedCheckedFalse();
                    MessageBox.Show("Выберите слева ровно один размер (одна галочка).", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var mainItem = selectedLeft[0];
                if (mainItem.Kod == 0)
                {
                    VerifiedCheckedFalse();
                    MessageBox.Show("Код комплекта не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var flatCodes = BuildFlatCodesByCountStr(_selectedKomplItems);
                if (flatCodes.Count == 0)
                {
                    VerifiedCheckedFalse();
                    MessageBox.Show("Нет выбранных позиций для комплекта.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else 
            {
                var selectedAuto = razmList.Where(x => x.Pr_po).ToList();
                if (selectedAuto.Count == 0)
                {
                    VerifiedCheckedFalse();
                    MessageBox.Show("Не выбраны общие размеры (галочки слева).", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        private void VerifiedCheckedFalse()
        {
            customCheckBoxVerified.CheckedChanged -= customCheckBoxVerified_CheckedChanged;
            customCheckBoxVerified.Checked = false;
            customCheckBoxVerified.CheckedChanged += customCheckBoxVerified_CheckedChanged;
        }
        private static void ClearKodSlots(KomplModel k)
        {
            k.Kod1 = k.Kod2 = k.Kod3 = k.Kod4 = k.Kod5 =
            k.Kod6 = k.Kod7 = k.Kod8 = k.Kod9 = k.Kod10 = null;
        }

        private static void SetKodByIndex(KomplModel k, int idx, int kod)
        {
            switch (idx)
            {
                case 0: k.Kod1 = kod; break;
                case 1: k.Kod2 = kod; break;
                case 2: k.Kod3 = kod; break;
                case 3: k.Kod4 = kod; break;
                case 4: k.Kod5 = kod; break;
                case 5: k.Kod6 = kod; break;
                case 6: k.Kod7 = kod; break;
                case 7: k.Kod8 = kod; break;
                case 8: k.Kod9 = kod; break;
                case 9: k.Kod10 = kod; break;
            }
        }

        /// Разворачиваем список выбранных по countStr в «плоский» список кодов (макс. 10)
        private static List<int> BuildFlatCodesByCountStr(IEnumerable<SpArticulGrupMenViewModel> items)
        {
            var flat = new List<int>(10);
            foreach (var it in items)
            {
                int times = it.countStr > 0 ? it.countStr : 1;
                for (int r = 0; r < times && flat.Count < 10; r++)
                    if (it.Kod > 0) flat.Add(it.Kod);
                if (flat.Count >= 10) break;
            }
            return flat;
        }

        /// Заполняем kod1..kod10 из списка кодов
        private static void FillKodSlots(KomplModel k, IList<int> codes)
        {
            ClearKodSlots(k);
            int n = Math.Min(10, codes.Count);
            for (int i = 0; i < n; i++)
                SetKodByIndex(k, i, codes[i]);
        }

        private async void customButtonKompl_Click(object sender, EventArgs e)
        {
            if (!customCheckBoxVerified.Checked)
                return;

            var razmList = customGridControlKomplRazm.DataSource as List<SpArticulGrupMenViewModel>;

            // =========================
            // НЕ АВТОРАЗМЕР — одна галочка слева, один комплект
            // =========================
            if (!xAutoRazm)
            {
                var selectedLeft = razmList.Where(x => x.Pr_po).ToList();
                var mainItem = selectedLeft[0];
                var flatCodes = BuildFlatCodesByCountStr(_selectedKomplItems);

                var kompl = new KomplModel
                {
                    Kod_k = mainItem.Kod,
                    Grup_k = mainItem.Grup,
                    Articul_k = mainItem.Articul,
                    Mod_k = mainItem.Mod,
                    Razm_k = mainItem.Razm,
                    Sost_k = mainItem.Sost,
                    CompName = Environment.MachineName
                };
                FillKodSlots(kompl, flatCodes);

                if (await komplService.ExistsExactAsync(kompl))
                {
                    var result = MessageBox.Show("Комплект уже существует. Перезаписать?",
                        "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes) return;

                    await komplService.DeleteAsync(kompl);
                }

                await komplService.SaveAsync(kompl);
                MessageBox.Show("Комплект успешно сохранён.", "Готово",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Очистка
                _selectedKomplItems.Clear();
                customGridControlKomplSelected.DataSource = _selectedKomplItems;
                customGridControlKomplSelected.RefreshDataSource();
                customCheckBoxVerified.Checked = false;

                // Снимаем галочки и чистим вкладки
                foreach (XtraTabPage tab in customTabControlKomplRazm.TabPages)
                {
                    if (tab.Controls.Count == 0 || tab.Controls[0] is not TableLayoutPanel layout) continue;
                    var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                    if (grid?.DataSource is BindingList<SpArticulGrupMenViewModel> list)
                    {
                        foreach (var row in list) row.Pr_po = false;
                        list.Clear();
                        grid.RefreshDataSource();
                    }
                }
                foreach (var r in razmList) r.Pr_po = false;
                gridViewKomplRazm.RefreshData();

                await loadKomplByArticul(kompl.Articul_k);
                customNumericUpDownValueTab.Value = 0;
                return;
            }

            // =========================
            // АВТОРАЗМЕР — несколько галочек слева → по записи на каждую галочку
            // =========================
            var selectedAuto = razmList.Where(x => x.Pr_po).ToList();

            int saved = 0;

            foreach (var leftRow in selectedAuto)
            {
                var keyRazmAll = (leftRow.Razm_all ?? "").Trim();
                if (string.IsNullOrEmpty(keyRazmAll)) continue;

                // Фильтруем нижние выбранные по razm_all, разворачиваем по countStr в плоский список кодов
                var flatCodes = BuildFlatCodesByCountStr(
                    _selectedKomplItems.Where(x =>
                        string.Equals((x.Razm_all ?? "").Trim(), keyRazmAll, StringComparison.OrdinalIgnoreCase) &&
                        x.Pr_po && x.Kod > 0));

                if (flatCodes.Count == 0)
                    continue;

                var kompl = new KomplModel
                {
                    Kod_k = leftRow.Kod,
                    Grup_k = leftRow.Grup,
                    Articul_k = leftRow.Articul,
                    Mod_k = leftRow.Mod,
                    Razm_k = leftRow.Razm,       // раздельный размер из левого списка
                    Sost_k = leftRow.Sost,
                    CompName = Environment.MachineName
                };
                FillKodSlots(kompl, flatCodes);

                if (await komplService.ExistsExactAsync(kompl))
                {
                    var result = MessageBox.Show(
                        $"Комплект по razm_all='{keyRazmAll}' уже существует. Перезаписать?",
                        "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes) continue;

                    await komplService.DeleteAsync(kompl);
                }

                await komplService.SaveAsync(kompl);
                saved++;
            }

            await loadKomplByArticul(xkod);

            MessageBox.Show(saved > 0
                ? $"Сохранено комплектов: {saved}."
                : "Нет данных для сохранения по выбранным размерам.",
                "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Сброс после авто режима
            _selectedKomplItems.Clear();
            customGridControlKomplSelected.DataSource = _selectedKomplItems;
            customGridControlKomplSelected.RefreshDataSource();
            customCheckBoxVerified.Checked = false;


            foreach (var r in razmList) r.Pr_po = false;
            gridViewKomplRazm.RefreshData();
        }

        async Task loadKomplByArticul(string articul_k)
        {
            customGridControlKompl.DataSource = await komplService.GetByArticulAsync(articul_k);
            customGridControlKompl.RefreshDataSource();
            loadKodForKompl();
        }
        private Dictionary<int, ArticulModel> _artByKod;
        void loadKodForKompl()
        {
            customGridControlKompl.LevelTree.Nodes[0].RelationName = "Коды";
            customGridControlKompl.LevelTree.Nodes[0].LevelTemplate = gridViewKomplKod;

            gridViewKomplKod.Columns.Clear();
            //gridViewKomplKod.OptionsView.ShowViewCaption = false;
            gridViewKomplKod.OptionsView.ShowColumnHeaders = false;

            var col = gridViewKomplKod.Columns.AddField("Kod");
            col.Caption = "Код";
            col.Visible = true;
            col.UnboundType = DevExpress.Data.UnboundColumnType.String;

            // ===== ДОБАВЛЕНО: дополнительные колонки для информации из артикула =====
            gridViewKomplKod.OptionsBehavior.AutoPopulateColumns = false;
            gridViewKomplKod.OptionsView.ShowColumnHeaders = true; // показать заголовки

            var colGrup = gridViewKomplKod.Columns.AddField("Grup");
            colGrup.Caption = "Группа";
            colGrup.Visible = true;
            colGrup.UnboundType = DevExpress.Data.UnboundColumnType.String;

            var colArticul = gridViewKomplKod.Columns.AddField("Articul");
            colArticul.Caption = "Артикул";
            colArticul.Visible = true;
            colArticul.UnboundType = DevExpress.Data.UnboundColumnType.String;

            var colMod = gridViewKomplKod.Columns.AddField("Mod");
            colMod.Caption = "Модель";
            colMod.Visible = true;
            colMod.UnboundType = DevExpress.Data.UnboundColumnType.String;

            var colRazm = gridViewKomplKod.Columns.AddField("Razm");
            colRazm.Caption = "Размер";
            colRazm.Visible = true;
            colRazm.UnboundType = DevExpress.Data.UnboundColumnType.String;
            // =======================================================================

            // ===== ДОБАВЛЕНО: кэш по коду, чтобы быстро получать ArticulModel =====
            if (_artByKod == null)
            {
                // если у тебя есть список articuls — используем его
                _artByKod = (articuls ?? new List<ArticulModel>()).GroupBy(a => a.Kod)
                             .ToDictionary(g => g.Key, g => g.First());
            }

            // fallback-поиск по вкладкам, если кода нет в кэше articuls
            ArticulModel LookupArt(int kod)
            {
                if (_artByKod.TryGetValue(kod, out var hit))
                    return hit;

                foreach (XtraTabPage tab in customTabControlKomplRazm.TabPages)
                {
                    if (tab.Controls.Count == 0) continue;
                    if (tab.Controls[0] is not TableLayoutPanel layout) continue;
                    var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                    if (grid?.DataSource is BindingList<SpArticulGrupMenViewModel> list)
                    {
                        var found = list.FirstOrDefault(x => x.Kod == kod);
                        if (found != null)
                        {
                            var a = new ArticulModel
                            {
                                Kod = found.Kod,
                                Grup = found.Grup,
                                Articul = found.Articul,
                                Mod = found.Mod,
                                Razm = found.Razm,
                                Sost = found.Sost
                            };
                            _artByKod[kod] = a;
                            return a;
                        }
                    }
                }
                return null;
            }
            // =======================================================================

            gridViewKompl.MasterRowGetChildList += (s, e) =>
            {
                var view = s as GridView;
                var row = view.GetRow(e.RowHandle) as KomplModel;
                if (row == null) return;

                var kodList = new List<object>(); // ← оставляем как у тебя, но теперь с доп. полями

                // Локальная функция — добавляет строку с данными артикула по коду
                void addKod(int? kodNullable)
                {
                    if (!kodNullable.HasValue) return;
                    int k = kodNullable.Value;

                    // ДОБАВЛЕНО: тянем информацию артикула
                    var art = LookupArt(k);

                    kodList.Add(new
                    {
                        Kod = k.ToString(),
                        Grup = art?.Grup ?? "",
                        Articul = art?.Articul ?? "",
                        Mod = art?.Mod ?? "",
                        Razm = art?.Razm ?? ""
                    });
                }

                // твоя логика заполнения — не трогаю
                if (row.Kod1.HasValue) addKod(row.Kod1);
                if (row.Kod2.HasValue) addKod(row.Kod2);
                if (row.Kod3.HasValue) addKod(row.Kod3);
                if (row.Kod4.HasValue) addKod(row.Kod4);
                if (row.Kod5.HasValue) addKod(row.Kod5);
                if (row.Kod6.HasValue) addKod(row.Kod6);
                if (row.Kod7.HasValue) addKod(row.Kod7);
                if (row.Kod8.HasValue) addKod(row.Kod8);
                if (row.Kod9.HasValue) addKod(row.Kod9);
                if (row.Kod10.HasValue) addKod(row.Kod10);

                e.ChildList = kodList;
            };

            gridViewKompl.MasterRowGetRelationCount += (s, e) => e.RelationCount = 1;
            gridViewKompl.MasterRowGetRelationName += (s, e) => e.RelationName = "Коды";
            int handle = gridViewKompl.LocateByValue("kod_k", Convert.ToInt32(xkod));
            if (handle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                gridViewKompl.ExpandMasterRow(handle);
            }
        }

        #endregion
        #region image
        private async void gridViewKomplArt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
                // 1) Получаем kod

                var kodObj = view.GetRowCellValue(e.FocusedRowHandle, "Kod");
                var kod = kodObj?.ToString();

                if (string.IsNullOrWhiteSpace(kod))
                {
                    SetPicture(null);
                    return;
                }

                // 2) Путь к файлу из БД (скалярно)
                var path = await _articulDataService.GetFileEskizForKod(kod);
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    SetPicture(null);
                    return;
                }

                // 3) Безопасно загружаем и показываем
                var img = LoadImageNoLock(path);
                SetPicture(img); // SetPicture сам освободит старое изображение
            }
            catch
            {
                // В простом варианте — просто очищаем превью
                SetPicture(null);
            }

        }
        private Image _currentImg;

        private void SetPicture(Image img)
        {
            // Освобождаем предыдущую картинку, чтобы не было утечек
            var old = _currentImg;
            _currentImg = img;
            pictureBoxArticul.Image = img;
            old?.Dispose();
        }

        private static Image LoadImageNoLock(string path)
        {
            // Чтение без блокировки файла: из потока + Clone()
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var tmp = Image.FromStream(fs))
            {
                return (Image)tmp.Clone();
            }
        }
        #endregion

        /// <summary>
        /// Подсказки при наведении на кнопки
        /// </summary>
        private void toolTipButton()
        {
            toolTip.AutoPopDelay = 5000;     // Подсказка исчезнет через 5 секунд.
            toolTip.InitialDelay = 500;      // Подсказка появится через 0.5 секунды.
            toolTip.ReshowDelay = 100;       // Подсказка появится повторно при движении мыши через 0.1 секунду.
            //toolTip.IsBalloon = true;      // Показывать подсказку в виде воздушного шара.
            //toolTip.ToolTipIcon = ToolTipIcon.Info; // Показывать иконку информации.
            //toolTip.ToolTipTitle = "Подсказка";  // Заголовок подсказки.

            toolTip.SetToolTip(customButtonDelKomplSelected, "Очистить список предварительной комплектовки");
            toolTip.SetToolTip(customButtonDelKompl, "Удалить комплект");
        }
    }
}
