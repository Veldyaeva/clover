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

namespace SewingProduction.Features.Articul.Forms
{
    public partial class AddNewKopml : CustomForm // FoxPro: kompl_new_2016
    {
        ArticulDataService _articulDataService = new ArticulDataService();
        GrupMenDataService _grupMenDataService = new GrupMenDataService();
        private BindingList<SpArticulGrupMenViewModel> _selectedRazmItems = new();
        public BindingList<KomplModel> _previewKompls = new();
        List<ArticulModel> articuls;
        string xkod; //код комплекта
        bool xFlagKod = false;
        bool xAutoRazm = false; //галочка авторазмер
        bool xAllZap = false; //галочка все записи одинаковые

        public AddNewKopml(UserClass user, List<ArticulModel> articuls, string kod) : base(user)
        {
            InitializeComponent();
            xkod = kod;
            this.articuls = articuls;
        }
        public AddNewKopml()
        {
            InitializeComponent();
        }
        //загрузка данных
        private async void customGridControlKomplArt_Load(object sender, EventArgs e)
        {
            //var articuls = await _articulDataService.GetAllAsync();
            var grups = await _grupMenDataService.GetAllAsync();
            var komplService = new KomplDataService();

            var result = from a in articuls
                         where !a.grup.ToLower().Contains("комплект") &&
                               a.kod.ToString().Length >= 7 &&
                               !a.kod.ToString().Substring(0, 7).Equals(xkod.Substring(0, 7))
                         join g in grups on a.grup equals g.men into gj
                         from g in gj.DefaultIfEmpty()
                         select new SpArticulGrupMenViewModel
                         {
                             kod = a.kod,
                             grup = a.grup,
                             articul = a.articul,
                             mod = a.mod,
                             razm = a.razm,
                             sost = a.sost,
                             kod_v = a.kod_v,
                             kle = a.kle,
                             GrupMen = g ?? new GrupMenModel(),
                             po = " ",
                             pr_po = false
                         };

            var resultList = result.ToList();
            await FillRazmAllAsync(resultList);
            customGridControlKomplArt.DataSource = resultList;


            var resulRazm = from a in articuls
                            where a.kod.ToString().PadLeft(7, '0').Substring(0, 7) == xkod.Substring(0, 7)
                            join g in grups on a.grup equals g.men into gj
                            from g in gj.DefaultIfEmpty()
                            select new SpArticulGrupMenViewModel
                            {
                                kod = a.kod,
                                grup = a.grup,
                                articul = a.articul,
                                mod = a.mod,
                                razm = a.razm,
                                sost = a.sost,
                                kod_v = a.kod_v,
                                kle = a.kle,
                                GrupMen = g ?? new GrupMenModel(),
                                po = " ",
                                pr_po = false
                            };
            var resulRazmList = resulRazm.ToList();
            await FillRazmAllAsync(resulRazmList); // 🔹 заполняем razm_all
            customGridControlKomplRazm.DataSource = resulRazmList;

            var firstRazm = resulRazm.FirstOrDefault();
            if (firstRazm != null)
            {
                customLabelGrup.Text = firstRazm.grup;
                customLabelArtText.Text = firstRazm.articul;
                customLabelModText.Text = firstRazm.mod;
            }

            await loadKomplByArticul(komplService, firstRazm.articul);
            int handle = gridViewKomplRazm.LocateByValue("kod", xkod.Trim());
            gridViewKomplRazm.MakeRowVisible(handle); BeginInvoke(new Action(() =>
            {
                if (int.TryParse(xkod, out var val))
                {
                    int h = gridViewKomplRazm.LocateByValue("kod", val);
                    if (h >= 0)
                    {
                        gridViewKomplRazm.FocusedRowHandle = h;
                        gridViewKomplRazm.MakeRowVisible(h);
                        xFlagKod = true;
                    }
                }
            }));
        }
        // галочка Авторазмер
        private void customCheckBoxAutoRazm_CheckedChanged(object sender, EventArgs e)
        {
            xAutoRazm = customCheckBoxAutoRazm.Checked;

            // Сброс значений
            customNumericUpDownValueTab.Value = 0;
            customButtonKompl.Enabled = false;
            customCheckBoxVerified.Checked = false;

            if (gridViewKomplRazm?.DataSource is not List<SpArticulGrupMenViewModel> data)
                return;

            foreach (var item in data)
                item.pr_po = false;

            gridViewKomplRazm.RefreshData();

            // В ЛЮБОМ режиме колонка pr_po остаётся редактируемой
            var col = gridViewKomplRazm.Columns["pr_po"];
            if (col != null)
                col.OptionsColumn.AllowEdit = true;

            ApplyGroupingByRazmAll(xAutoRazm ? true : false);
        }

        //галочка все записи одинковые 
        private void customCheckBoxOdinak_CheckedChanged(object sender, EventArgs e)
        {
            xAllZap = customCheckBoxOdinak.Checked;
            customNumericUpDownValueTab.Value = 0;
            customButtonKompl.Enabled = false;
            customCheckBoxVerified.Checked = false;
        }

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

            // Создаём GridView
            var view = new DevExpress.XtraGrid.Views.Grid.GridView(grid);
            grid.MainView = view;
            grid.ViewCollection.Add(view);

            // Привязываем пустой список данных
            grid.DataSource = new BindingList<SpArticulGrupMenViewModel>();

            // Добавляем стандартные колонки
            view.Columns.AddVisible("articul", "Артикул");
            view.Columns.AddVisible("mod", "Модель");
            view.Columns.AddVisible("kod", "Код");
            view.Columns.AddVisible("razm", "Размер");
            view.Columns.AddVisible("kle", "ТМ");
            view.Columns.AddVisible("razm_all", "Основной Размер");

            //view.OptionsBehavior.Editable = false; 
            foreach (var col in view.Columns.Cast<DevExpress.XtraGrid.Columns.GridColumn>())
                col.OptionsColumn.AllowEdit = false;
            //col.OptionsColumn.AllowEdit = col.FieldName == "pr_po" ? true : false;

            // Колонка выбора (pr_po)
            var checkColumn = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Caption = "Выбор",
                FieldName = "pr_po",
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

            bool wasChecked = selected.pr_po;
            bool newChecked = editor?.Checked ?? !wasChecked; // если нет editor — считаем, что клик инвертирует

            // ===== 1) АВТО + ОДИНАКОВО =====
            if (xAutoRazm && xAllZap)
            {
                // Обычно ручной клик в табах при авторазмере блокируется.
                // Если всё же пришёл — трактуем как выбор образца: одна строка внизу, количество = вкладки * галочки слева.
                int tabsCount = (int)customNumericUpDownValueTab.Value;
                int checkedCount = 0;
                for (int i = 0; i < gridViewKomplRazm.RowCount; i++)
                    if (gridViewKomplRazm.GetRow(i) is SpArticulGrupMenViewModel r && r.pr_po)
                        checkedCount++;
                if (checkedCount <= 0) checkedCount = 1;

                _selectedKomplItems.Clear();
                _selectedKomplItems.Add(new SpArticulGrupMenViewModel
                {
                    kod = selected.kod,
                    grup = selected.grup,
                    articul = selected.articul,
                    mod = selected.mod,
                    razm = selected.razm,
                    razm_all = selected.razm_all,
                    sost = selected.sost,
                    kod_v = selected.kod_v,
                    kle = selected.kle,
                    po = selected.po,
                    pr_po = true,
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
                selected.pr_po = newChecked;
                selected.TabIndex = tabIndex;

                var agg = _selectedKomplItems.FirstOrDefault(x => x.kod == selected.kod && x.razm == selected.razm);
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
                    selected.pr_po = false;
                    view.RefreshData();

                    _selectedKomplItems.Clear();
                    customGridControlKomplSelected.RefreshDataSource();

                    _tabSelection.Remove(tabIndex);
                    return;
                }

                // Установка: на странице ровно одна галочка, одна строка с количеством = числу вкладок
                foreach (var item in data) item.pr_po = false;
                selected.pr_po = true;
                selected.TabIndex = tabIndex;
                view.RefreshData();

                int tabsCount = (int)customNumericUpDownValueTab.Value;

                _selectedKomplItems.Clear();
                _selectedKomplItems.Add(new SpArticulGrupMenViewModel
                {
                    kod = selected.kod,
                    grup = selected.grup,
                    articul = selected.articul,
                    mod = selected.mod,
                    razm = selected.razm,
                    razm_all = selected.razm_all,
                    sost = selected.sost,
                    kod_v = selected.kod_v,
                    kle = selected.kle,
                    po = selected.po,
                    pr_po = true,
                    GrupMen = selected.GrupMen,
                    TabIndex = 0,
                    countStr = tabsCount
                });

                _tabSelection[tabIndex] = (selected.kod, selected.razm);
                customGridControlKomplSelected.RefreshDataSource();
                return;
            }

            // ===== 4) НЕ АВТО + РАЗНЫЕ =====
            // На странице можно выбрать максимум одну строку.
            if (wasChecked && !newChecked)
            {
                // Снятие текущего выбора этой вкладки
                selected.pr_po = false;
                view.RefreshData();

                // Уменьшаем агрегат у пары, снятой на этой вкладке
                var agg = _selectedKomplItems.FirstOrDefault(x => x.kod == selected.kod && x.razm == selected.razm);
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
            foreach (var item in data) item.pr_po = false;
            selected.pr_po = true;
            selected.TabIndex = tabIndex;
            view.RefreshData();

            // Если на вкладке раньше была другая пара — её нужно уменьшить
            if (_tabSelection.TryGetValue(tabIndex, out var prev))
            {
                if (prev.kod != selected.kod || prev.razm != selected.razm)
                {
                    var prevAgg = _selectedKomplItems.FirstOrDefault(x => x.kod == prev.kod && x.razm == prev.razm);
                    if (prevAgg != null)
                    {
                        if (prevAgg.countStr > 1) prevAgg.countStr -= 1;
                        else _selectedKomplItems.Remove(prevAgg);
                    }
                }
            }

            // Учесть новую пару
            var curAgg = _selectedKomplItems.FirstOrDefault(x => x.kod == selected.kod && x.razm == selected.razm);
            if (curAgg != null) curAgg.countStr += 1;
            else { selected.countStr = 1; _selectedKomplItems.Add(selected); }

            _tabSelection[tabIndex] = (selected.kod, selected.razm);
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
                    if (!list.Any(x => x.pr_po))
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
        //
        private void repositoryItemCheckEditViborRazm_CheckedChanged(object sender, EventArgs e)
        {
            var view = gridViewKomplRazm;
            if (view?.GetFocusedRow() is not SpArticulGrupMenViewModel selected) return;
            var data = view.DataSource as List<SpArticulGrupMenViewModel>;
            if (data == null) return;

            if (xAutoRazm)
            {
                // переключаем состояние конкретной записи
                selected.pr_po = !selected.pr_po;
                view.RefreshData();
                AutoRazmForForming();
            }
            else
            {
                // только одна галочка
                foreach (var item in data)
                    item.pr_po = false;

                selected.pr_po = true;
                view.RefreshData();
            }

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

            if (mykompl_razm.kle != kompl_art.kle)
            {
                MessageBox.Show("Комплектовать модели нельзя — разные торговые марки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mykompl_razm.kod_v == 2 && kompl_art.kod_v == 2 && mykompl_razm.GrupMen.frm_s != kompl_art.GrupMen.frm_s)
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
                    .Where(x => x.articul == selectedRow.articul)
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
            var kodRazmSet = new HashSet<(int, string)>(targetList.Select(x => (x.kod, x.razm)));

            foreach (var row in sameArticulRows)
            {
                var key = (row.kod, row.razm);
                if (!kodRazmSet.Contains(key))
                {
                    targetList.Add(new SpArticulGrupMenViewModel
                    {
                        kod = row.kod,
                        grup = row.grup,
                        articul = row.articul,
                        mod = row.mod,
                        razm = row.razm,
                        razm_all = row.razm_all, // 🔹 переносим общий размер
                        sost = row.sost,
                        kod_v = row.kod_v,
                        kle = row.kle,
                        po = row.po,
                        pr_po = row.pr_po,
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

        #region button for delete
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
                            item.pr_po = false;

                        pageList.Clear();
                        pageGrid.RefreshDataSource();
                    }
                }

                // 4) дополнительно (если нужно) снять галочки слева в общем списке размеров
                //    чтобы «авторазмер» тоже сбросился визуально
                var razmList = customGridControlKomplRazm.DataSource as List<SpArticulGrupMenViewModel>;
                if (razmList != null)
                {
                    foreach (var r in razmList) r.pr_po = false;
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
                item.pr_po = false;

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
                        string.Equals(r.razm_all?.Trim(), selected.razm_all?.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (leftRow != null)
                    {
                        leftRow.pr_po = false;
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
                        var matchingRow = list.FirstOrDefault(x => x.kod == selected.kod && x.razm == selected.razm);
                        if (matchingRow != null)
                        {
                            matchingRow.pr_po = false;
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
                        row.pr_po = false;

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
        #region Filter
        private bool _grupLoaded = false;
        private bool _articulLoaded = false;
        private bool _modLoaded = false;
        private bool _razmLoaded = false;

        private async Task LoadFilterValuesAsync(CustomComboBox comboBox, IEnumerable<string> values)
        {
            await Task.Run(() =>
            {
                var set = new HashSet<string>();
                var list = new List<string>();

                foreach (var val in values)
                {
                    if (!string.IsNullOrWhiteSpace(val) && set.Add(val))
                        list.Add(val);
                }

                Invoke(new Action(() =>
                {
                    comboBox.BeginUpdate();
                    comboBox.Items.Clear();
                    comboBox.Items.Add(""); // Пустой фильтр
                    comboBox.Items.AddRange(list.ToArray());
                    comboBox.SelectedIndex = 0;
                    comboBox.EndUpdate();
                }));
            });
        }

        private void ApplyGridFilters(object sender, EventArgs e)
        {
            var view = gridViewKomplArt;
            if (view == null) return;

            var filters = new List<string>();

            // Важно: Экранируем одинарные кавычки
            string Esc(string s) => s?.Replace("'", "''");

            if (!string.IsNullOrWhiteSpace(customComboBoxGrup.Text))
                filters.Add($"[grup] = '{Esc(customComboBoxGrup.Text.Trim())}'");

            if (!string.IsNullOrWhiteSpace(customComboBoxArt.Text))
                filters.Add($"[articul] = '{Esc(customComboBoxArt.Text.Trim())}'");

            if (!string.IsNullOrWhiteSpace(customComboBoxMod.Text))
                filters.Add($"[mod] = '{Esc(customComboBoxMod.Text.Trim())}'");

            if (!string.IsNullOrWhiteSpace(customComboBoxRazm.Text))
                filters.Add($"[razm] = '{Esc(customComboBoxRazm.Text).Trim()}'");

            // применяем
            view.ActiveFilter.Clear();
            if (filters.Count > 0)
                view.ActiveFilterString = string.Join(" AND ", filters);
        }

        private async void customComboBoxGrup_DropDown(object sender, EventArgs e)
        {
            if (_grupLoaded) return;

            var data = customGridControlKomplArt.DataSource as List<SpArticulGrupMenViewModel>;
            if (data != null)
            {
                await LoadFilterValuesAsync(customComboBoxGrup, data.Select(x => x.grup.Trim()));
                _grupLoaded = true;
            }
        }

        private async void customComboBoxArt_DropDown(object sender, EventArgs e)
        {
            if (_articulLoaded) return;

            var data = customGridControlKomplArt.DataSource as List<SpArticulGrupMenViewModel>;
            if (data != null)
            {
                await LoadFilterValuesAsync(customComboBoxArt, data.Select(x => x.articul.Trim()));
                _articulLoaded = true;
            }
        }

        private async void customComboBoxMod_DropDown(object sender, EventArgs e)
        {
            if (_modLoaded) return;

            var data = customGridControlKomplArt.DataSource as List<SpArticulGrupMenViewModel>;
            if (data != null)
            {
                await LoadFilterValuesAsync(customComboBoxMod, data.Select(x => x.mod.Trim()));
                _modLoaded = true;
            }
        }

        private async void customComboBoxRazm_DropDown(object sender, EventArgs e)
        {
            if (_razmLoaded) return;

            var data = customGridControlKomplArt.DataSource as List<SpArticulGrupMenViewModel>;
            if (data != null)
            {
                await LoadFilterValuesAsync(customComboBoxRazm, data.Select(x => x.razm.Trim()));
                _razmLoaded = true;
            }
        }

        private void customButtonClearFilter_Click(object sender, EventArgs e)
        {
            // Сбрасываем тексты
            if (_grupLoaded) customComboBoxGrup.SelectedIndex = 0;
            if (_articulLoaded) customComboBoxArt.SelectedIndex = 0;
            if (_modLoaded) customComboBoxMod.SelectedIndex = 0;
            if (_razmLoaded) customComboBoxRazm.SelectedIndex = 0;

            // Сбрасываем фильтр
            gridViewKomplArt.ActiveFilter.Clear();
        }
        #endregion


        // Авторасстановка галочек + автоформирование списка выбранных по razm_all с накоплением countStr
        private void AutoRazmForForming()
        {
            var gridData = customGridControlKomplRazm.DataSource as List<SpArticulGrupMenViewModel>;
            if (gridData == null) return;

            // 1) какие общие размеры выбраны слева
            var selectedRazmAll = gridData
                .Where(x => x.pr_po)
                .Select(x => x.razm_all?.Trim())
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
                    row.pr_po = selectedRazmAll.Contains(row.razm_all?.Trim());

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

                foreach (var row in list.Where(r => r.pr_po))
                {
                    var key = (row.kod, row.razm);
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

            // 4) группировка по razm_all (если она у тебя включается при авторазмере)
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
            var colRazmAll = view.Columns.ColumnByFieldName("razm_all");
            if (colRazmAll == null)
            {
                colRazmAll = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                {
                    FieldName = "razm_all",
                    Caption = "Размер",
                    Visible = true
                };
                view.Columns.Add(colRazmAll);
                band.Columns.Add(colRazmAll);
            }
            else
            {
                if (colRazmAll.OwnerBand != band)
                    band.Columns.Add(colRazmAll);
                colRazmAll.Visible = true;
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
                .Where(x => !string.IsNullOrWhiteSpace(x.razm))
                .GroupBy(x => x.razm.Trim())
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var key in byRazm.Keys.ToList())
            {
                // Берём общий размер из таблицы Razm (ArticulDataService.GetAllRazmByRazmAsync)
                var razmAll = await _articulDataService.GetAllRazmByRazmAsync(key);
                if (!string.IsNullOrWhiteSpace(razmAll))
                {
                    foreach (var row in byRazm[key])
                        row.razm_all = razmAll.Trim();
                }
            }
        }
        // Включает/выключает группировку по общему размеру (razm_all) в customGridControlKomplSelected
        private void ApplyGroupingByRazmAll(bool enabled)
        {
            var view = bandedGridViewSelected;                  // MainView у customGridControlKomplSelected
            if (view == null) return;

            var colRazmAll = view.Columns.ColumnByFieldName("razm_all");
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

        #region kompl
        private void customCheckBoxVerified_CheckedChanged(object sender, EventArgs e)
        {
            int requiredCount = (int)customNumericUpDownValueTab.Value;
            int selectedCount = _selectedKomplItems.Sum(x => x.countStr);
            // Считаем галочки в gridViewKomplRazm
            int checkedCount = 0;
            for (int i = 0; i < gridViewKomplRazm.RowCount; i++)
            {
                var row = gridViewKomplRazm.GetRow(i) as SpArticulGrupMenViewModel;
                if (row != null && row.pr_po)
                    checkedCount++;
            }

            // Умножаем требуемое количество на число галочек
            requiredCount *= checkedCount;

            if (requiredCount == 0)
            {
                customCheckBoxVerified.CheckedChanged -= customCheckBoxVerified_CheckedChanged;
                customCheckBoxVerified.Checked = false;
                customCheckBoxVerified.CheckedChanged += customCheckBoxVerified_CheckedChanged;

                MessageBox.Show(
                    $"Необходимо заполнить поле количество и выбрать артикулы для комплекта.",
                    "Нет данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            if (selectedCount != requiredCount && customCheckBoxVerified.Checked)
            {
                customCheckBoxVerified.CheckedChanged -= customCheckBoxVerified_CheckedChanged;
                customCheckBoxVerified.Checked = false;
                customCheckBoxVerified.CheckedChanged += customCheckBoxVerified_CheckedChanged;

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

        }
        private static void ClearKodSlots(KomplModel k)
        {
            k.kod1 = k.kod2 = k.kod3 = k.kod4 = k.kod5 =
            k.kod6 = k.kod7 = k.kod8 = k.kod9 = k.kod10 = null;
        }

        private static void SetKodByIndex(KomplModel k, int idx, int kod)
        {
            switch (idx)
            {
                case 0: k.kod1 = kod; break;
                case 1: k.kod2 = kod; break;
                case 2: k.kod3 = kod; break;
                case 3: k.kod4 = kod; break;
                case 4: k.kod5 = kod; break;
                case 5: k.kod6 = kod; break;
                case 6: k.kod7 = kod; break;
                case 7: k.kod8 = kod; break;
                case 8: k.kod9 = kod; break;
                case 9: k.kod10 = kod; break;
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
                    if (it.kod > 0) flat.Add(it.kod);
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
            if (razmList == null || razmList.Count == 0)
            {
                MessageBox.Show("Нет данных артикула для комплектования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var komplService = new KomplDataService();

            // =========================
            // НЕ АВТОРАЗМЕР — одна галочка слева, один комплект
            // =========================
            if (!customCheckBoxAutoRazm.Checked)
            {
                var selectedLeft = razmList.Where(x => x.pr_po).ToList();
                if (selectedLeft.Count != 1)
                {
                    MessageBox.Show("Выберите слева ровно один размер (одна галочка).", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var mainItem = selectedLeft[0];
                if (mainItem.kod == null || mainItem.kod == 0)
                {
                    MessageBox.Show("Код комплекта не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Берём внизу в том порядке, как они отображаются, и разворачиваем по countStr
                var flatCodes = BuildFlatCodesByCountStr(_selectedKomplItems);
                if (flatCodes.Count == 0)
                {
                    MessageBox.Show("Нет выбранных позиций для комплекта.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var kompl = new KomplModel
                {
                    kod_k = mainItem.kod,
                    grup_k = mainItem.grup,
                    articul_k = mainItem.articul,
                    mod_k = mainItem.mod,
                    razm_k = mainItem.razm,
                    sost_k = mainItem.sost,
                    compName = Environment.MachineName
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
                        foreach (var row in list) row.pr_po = false;
                        list.Clear();
                        grid.RefreshDataSource();
                    }
                }
                foreach (var r in razmList) r.pr_po = false;
                gridViewKomplRazm.RefreshData();

                await loadKomplByArticul(komplService, kompl.articul_k);
                customNumericUpDownValueTab.Value = 0;
                return;
            }

            // =========================
            // АВТОРАЗМЕР — несколько галочек слева → по записи на каждую галочку
            // =========================
            var selectedAuto = razmList.Where(x => x.pr_po).ToList();
            if (selectedAuto.Count == 0)
            {
                MessageBox.Show("Не выбраны общие размеры (галочки слева).", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int saved = 0;

            foreach (var leftRow in selectedAuto)
            {
                var keyRazmAll = (leftRow.razm_all ?? "").Trim();
                if (string.IsNullOrEmpty(keyRazmAll)) continue;

                // Фильтруем нижние выбранные по razm_all, разворачиваем по countStr в плоский список кодов
                var flatCodes = BuildFlatCodesByCountStr(
                    _selectedKomplItems.Where(x =>
                        string.Equals((x.razm_all ?? "").Trim(), keyRazmAll, StringComparison.OrdinalIgnoreCase) &&
                        x.pr_po && x.kod > 0));

                if (flatCodes.Count == 0)
                    continue;

                var kompl = new KomplModel
                {
                    kod_k = leftRow.kod,
                    grup_k = leftRow.grup,
                    articul_k = leftRow.articul,
                    mod_k = leftRow.mod,
                    razm_k = leftRow.razm,       // раздельный размер из левого списка
                    sost_k = leftRow.sost,
                    compName = Environment.MachineName
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

            MessageBox.Show(saved > 0
                ? $"Сохранено комплектов: {saved}."
                : "Нет данных для сохранения по выбранным размерам.",
                "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Сброс после авто режима
            _selectedKomplItems.Clear();
            customGridControlKomplSelected.DataSource = _selectedKomplItems;
            customGridControlKomplSelected.RefreshDataSource();
            customCheckBoxVerified.Checked = false;

            foreach (var r in razmList) r.pr_po = false;
            gridViewKomplRazm.RefreshData();
        }

        async Task loadKomplByArticul(KomplDataService komplService, string articul_k)
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
                _artByKod = (articuls ?? new List<ArticulModel>()).GroupBy(a => a.kod)
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
                        var found = list.FirstOrDefault(x => x.kod == kod);
                        if (found != null)
                        {
                            var a = new ArticulModel
                            {
                                kod = found.kod,
                                grup = found.grup,
                                articul = found.articul,
                                mod = found.mod,
                                razm = found.razm,
                                sost = found.sost
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
                        Grup = art?.grup ?? "",
                        Articul = art?.articul ?? "",
                        Mod = art?.mod ?? "",
                        Razm = art?.razm ?? ""
                    });
                }

                // твоя логика заполнения — не трогаю
                if (row.kod1.HasValue) addKod(row.kod1);
                if (row.kod2.HasValue) addKod(row.kod2);
                if (row.kod3.HasValue) addKod(row.kod3);
                if (row.kod4.HasValue) addKod(row.kod4);
                if (row.kod5.HasValue) addKod(row.kod5);
                if (row.kod6.HasValue) addKod(row.kod6);
                if (row.kod7.HasValue) addKod(row.kod7);
                if (row.kod8.HasValue) addKod(row.kod8);
                if (row.kod9.HasValue) addKod(row.kod9);
                if (row.kod10.HasValue) addKod(row.kod10);

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
    }
}
