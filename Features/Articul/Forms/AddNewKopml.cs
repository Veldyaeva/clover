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

namespace SewingProduction.Features.Articul.Forms
{
    public partial class AddNewKopml : CustomForm // FoxPro: kompl_new_2016
    {
        ArticulDataService _articulDataService = new ArticulDataService();
        GrupMenDataService _grupMenDataService = new GrupMenDataService();
        private BindingList<SpArticulGrupMenViewModel> _selectedRazmItems = new();
        string xkod;

        public AddNewKopml(UserClass user, string kod) : base(user)
        {
            InitializeComponent();
            xkod = kod;
        }
        public AddNewKopml()
        {
            InitializeComponent();
        }
        //загрузка данных
        private async void customGridControlKomplArt_Load(object sender, EventArgs e)
        {
            var articuls = await _articulDataService.GetAllAsync();
            var grups = await _grupMenDataService.GetAllAsync();

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

            customGridControlKomplArt.DataSource = result.ToList();

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
            customGridControlKomplRazm.DataSource = resulRazm.ToList();

            var firstRazm = resulRazm.FirstOrDefault();
            if (firstRazm != null)
            {
                customLabelGrup.Text = firstRazm.grup;
                customLabelArtText.Text = firstRazm.articul;
                customLabelModText.Text = firstRazm.mod;
            }
        }

        #region TabPage
        private void customNumericUpDownValueTab_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)customNumericUpDownValueTab.Value;
            if (count < 2 || count > 10)
                return;

            // Удаляем все вкладки, кроме первой
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
        private void CreateKomplRazmTabPage(int index)
        {
            // Создаём вкладку
            var tabPage = new XtraTabPage
            {
                Name = $"xtraTabPageKomplRazm{index}",
                Text = $"{index}"
            };

            // Таблица внутри вкладки
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

            // Колонка выбора (pr_po)
            var checkColumn = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Caption = "Выбор",
                FieldName = "pr_po",
                Visible = true
            };

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

        private void AddToKomplSelected(SpArticulGrupMenViewModel selected)
        {
            // 1. Удаляем старую выбранную строку с той же вкладки (по TabIndex)
            var existing = _selectedKomplItems
                .FirstOrDefault(x => x.TabIndex == selected.TabIndex);

            if (existing != null)
            {
                _selectedKomplItems.Remove(existing);
            }

            // 2. Добавляем новую строку
            _selectedKomplItems.Add(selected);
            customGridControlKomplSelected.DataSource = _selectedKomplItems;
        }

        // Расстановка галочек
        void CheckEditValueChanged(Object s, CustomGridControl grid)
        {
            var editor = s as DevExpress.XtraEditors.CheckEdit;
            var gridView = grid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView == null || editor == null) return;

            var selected = gridView.GetFocusedRow() as SpArticulGrupMenViewModel;
            if (selected == null) return;

            var data = grid.DataSource as BindingList<SpArticulGrupMenViewModel>;
            if (data == null) return;

            // Получаем текущий TabIndex
            var parentTab = customTabControlKomplRazm.TabPages
                .FirstOrDefault(tab => tab.Controls[0].Controls.Contains(grid));

            if (parentTab == null) return;

            int tabIndex = customTabControlKomplRazm.TabPages.IndexOf(parentTab);

            // Если галочка уже стояла — считаем это снятием
            if (selected.pr_po)
            {
                selected.pr_po = false;

                // Удаляем из выбранных только по TabIndex
                var toRemove = _selectedKomplItems
                    .FirstOrDefault(x => x.TabIndex == tabIndex);

                if (toRemove != null)
                    _selectedKomplItems.Remove(toRemove);

                grid.MainView.RefreshData();
                customGridControlKomplSelected.DataSource = _selectedKomplItems;
                customGridControlKomplSelected.RefreshDataSource();

                return;
            }

            // Иначе — это установка новой галочки

            // Снимаем все галочки в текущем гриде
            foreach (var item in data)
                item.pr_po = false;

            selected.pr_po = true;
            selected.TabIndex = tabIndex;

            gridView.RefreshData();

            // Удаляем старую запись с этой вкладки (если была)
            var existing = _selectedKomplItems
                .FirstOrDefault(x => x.TabIndex == tabIndex);

            if (existing != null)
                _selectedKomplItems.Remove(existing);

            _selectedKomplItems.Add(selected);
            customGridControlKomplSelected.DataSource = _selectedKomplItems;
            customGridControlKomplSelected.RefreshDataSource();

            // Переход на следующую незаполненную вкладку
            for (int i = 0; i < customTabControlKomplRazm.TabPages.Count; i++)
            {
                var tab = customTabControlKomplRazm.TabPages[i];
                if (tab.Controls[0] is not TableLayoutPanel layout) continue;

                var nextGrid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                if (nextGrid?.DataSource is not BindingList<SpArticulGrupMenViewModel> list) continue;

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

            // Переключаемся
            customTabControlKomplRazm.SelectedTabPageIndex = targetIndex;

            // Добавляем строки
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
                        sost = row.sost,
                        kod_v = row.kod_v,
                        kle = row.kle,
                        po = row.po,
                        pr_po = row.pr_po,
                        GrupMen = row.GrupMen,
                        TabIndex = targetIndex
                    });
                    kodRazmSet.Add(key); // обновим множество, если вдруг в будущем ещё нужно
                }
            }
        }
        void Proverka()
        { 
        
        }
        #region button for delete
        //Button on TabPage
        void RemoveButtonClick(Object s, CustomGridControl grid)
        {
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

            // Сбрасываем pr_po
            foreach (var item in list)
                item.pr_po = false;

            // Очищаем грид этой вкладки
            list.Clear();
        }

        //Button on customGridControlKomplSelected
        private void repositoryItemButtonEditDelRazm_Click(object sender, EventArgs e)
        {
            var view = gridViewKomplSelected;
            if (view == null) return;

            var selected = view.GetFocusedRow() as SpArticulGrupMenViewModel;
            if (selected == null) return;

            _selectedKomplItems.Remove(selected);
            customGridControlKomplSelected.DataSource = _selectedKomplItems;

            // Используем TabIndex для точного удаления из вкладки
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
            customGridControlKompl.DataSource = null;
            customGridControlKompl.RefreshDataSource();
        }
        private void customButtonForm_Click(object sender, EventArgs e)
        {
            customNumericUpDownValueTab.Value = 0;
            customButtonDelKomplSelected_Click(sender, e);
        }
        #endregion
        #region kompl
        private void customCheckBoxVerified_CheckedChanged(object sender, EventArgs e)
        {
            int requiredCount = (int)customNumericUpDownValueTab.Value;
            int selectedCount = _selectedKomplItems.Count;

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

            var mainItem = razmList.First();

            if (mainItem.kod == null || mainItem.kod == 0)
            {
                MessageBox.Show("Код комплекта не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Debug.WriteLine($"kod_k: {mainItem.kod}, artikul: {mainItem.articul}, poz: {_selectedKomplItems.Count}");

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

            for (int i = 0; i < _selectedKomplItems.Count; i++)
            {
                var item = _selectedKomplItems[i];
                switch (i)
                {
                    case 0: kompl.kod1 = item.kod; break;
                    case 1: kompl.kod2 = item.kod; break;
                    case 2: kompl.kod3 = item.kod; break;
                    case 3: kompl.kod4 = item.kod; break;
                    case 4: kompl.kod5 = item.kod; break;
                    case 5: kompl.kod6 = item.kod; break;
                    case 6: kompl.kod7 = item.kod; break;
                    case 7: kompl.kod8 = item.kod; break;
                    case 8: kompl.kod9 = item.kod; break;
                    case 9: kompl.kod10 = item.kod; break;
                }
            }

            var komplService = new KomplDataService();
            if (await komplService.ExistsAsync(kompl))
            {
                var result = MessageBox.Show("Комплект уже существует. Перезаписать?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                await komplService.DeleteAsync(kompl);
            }

            await komplService.SaveAsync(kompl);
            MessageBox.Show("Комплект успешно сохранён.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _selectedKomplItems.Clear();
            customGridControlKomplSelected.DataSource = _selectedKomplItems;
            customCheckBoxVerified.Checked = false;
            
            foreach (XtraTabPage tab in customTabControlKomplRazm.TabPages)
            {
                if (tab.Controls[0] is not TableLayoutPanel layout) continue;

                var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                if (grid?.DataSource is BindingList<SpArticulGrupMenViewModel> list)
                {
                    foreach (var row in list)
                        row.pr_po = false;

                    list.Clear();
                    grid.RefreshDataSource();
                }
            }

            customTabControlKomplRazm.SelectedTabPageIndex = 0;
            var newKompl = await komplService.GetByKodAsync(mainItem.kod);
            if (newKompl != null)
            {
                customGridControlKompl.DataSource = new List<KomplModel> { newKompl };
                customGridControlKompl.RefreshDataSource();
                loadKodForKompl();
            }
        }
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

            gridViewKompl.MasterRowGetChildList += (s, e) =>
            {
                var view = s as GridView;
                var row = view.GetRow(e.RowHandle) as KomplModel;
                if (row == null) return;

                var kodList = new List<dynamic>();

                if (row.kod1.HasValue) kodList.Add(new { Kod = row.kod1.Value.ToString() });
                if (row.kod2.HasValue) kodList.Add(new { Kod = row.kod2.Value.ToString() });
                if (row.kod3.HasValue) kodList.Add(new { Kod = row.kod3.Value.ToString() });
                if (row.kod4.HasValue) kodList.Add(new { Kod = row.kod4.Value.ToString() });
                if (row.kod5.HasValue) kodList.Add(new { Kod = row.kod5.Value.ToString() });
                if (row.kod6.HasValue) kodList.Add(new { Kod = row.kod6.Value.ToString() });
                if (row.kod7.HasValue) kodList.Add(new { Kod = row.kod7.Value.ToString() });
                if (row.kod8.HasValue) kodList.Add(new { Kod = row.kod8.Value.ToString() });
                if (row.kod9.HasValue) kodList.Add(new { Kod = row.kod9.Value.ToString() });
                if (row.kod10.HasValue) kodList.Add(new { Kod = row.kod10.Value.ToString() });

                e.ChildList = kodList;
            };

            gridViewKompl.MasterRowGetRelationCount += (s, e) => e.RelationCount = 1;
            gridViewKompl.MasterRowGetRelationName += (s, e) => e.RelationName = "Коды";
            gridViewKompl.SetMasterRowExpanded(0, true);
        }
        #endregion
    }
}
