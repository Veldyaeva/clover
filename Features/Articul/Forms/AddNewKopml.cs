using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.DataAccess.DataFederation;
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

        private void customNumericUpDownValueTab_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)customNumericUpDownValueTab.Value;
            if (count < 2 || count > 10)
                return;

            // Удаляем все вкладки, кроме первой
            while (customTabControlKomplRazm.TabPages.Count > 1)
                customTabControlKomplRazm.TabPages.RemoveAt(customTabControlKomplRazm.TabPages.Count - 1);

            for (int i = 2; i <= count; i++)
            {
                var newTab = CreateKomplRazmTabPage(i);
                customTabControlKomplRazm.TabPages.Add(newTab);
            }
        }
        private XtraTabPage CreateKomplRazmTabPage(int index)
        {
            var newTabPage = new XtraTabPage
            {
                Name = $"xtraTabPageKomplRazm{index}",
                Text = index.ToString()
            };

            var layout = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
                Name = $"tableLayoutPanelKomplRazm{index}"
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 91F));

            // Создаем новый gridControl
            var grid = new CustomGridControl
            {
                Name = $"customGridControlKomplRazm{index}",
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10F)
            };

            var view = new DevExpress.XtraGrid.Views.Grid.GridView
            {
                Name = $"gridViewKomplRazm{index}"
            };

            view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Арт.", FieldName = "articul", Visible = true });
            view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Мод.", FieldName = "mod", Visible = true });
            view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Код", FieldName = "kod", Visible = true });
            view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Размер", FieldName = "razm", Visible = true });
            view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Выбор", FieldName = "check", Visible = true });

            grid.MainView = view;
            grid.ViewCollection.Add(view);

            // Кнопка "X"
            var closeButton = new CustomButton
            {
                Name = $"customButtonCloseRazm{index}",
                Text = "X",
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(230, 230, 250),
                Font = new Font("Arial", 10F),
                ForeColor = Color.FromArgb(72, 61, 139),
                MaximumSize = new Size(25, 25),
                MinimumSize = new Size(25, 25)
            };

            layout.Controls.Add(grid, 0, 0);
            layout.SetRowSpan(grid, 2);
            layout.Controls.Add(closeButton, 1, 0);

            newTabPage.Controls.Add(layout);
            return newTabPage;
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
                             kodV = a.kodV,
                             kle = a.kle,
                             GrupMen = g ?? new GrupMenModel(),
                             po = " ",
                             prPo = 0
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
                                kodV = a.kodV,
                                kle = a.kle,
                                GrupMen = g ?? new GrupMenModel(),
                                po = " ",
                                prPo = 0
                            };
            customGridControlKomplRazm.DataSource = resulRazm.ToList();
        }
        private void repositoryItemButtonEditAddRazm_Click(object sender, EventArgs e)
        {
            var view = gridViewKomplArt;
            if (view == null) return;

            var selectedRow = view.GetFocusedRow() as SpArticulGrupMenViewModel;
            if (selectedRow == null) return;

            // Получаем все строки из основного грида
            var allRows = customGridControlKomplArt.DataSource as List<SpArticulGrupMenViewModel>;
            if (allRows == null) return;

            // Находим все строки с тем же артикулом
            var sameArticulRows = allRows
                .Where(x => x.articul == selectedRow.articul)
                .ToList();

            if (!sameArticulRows.Any()) return;

            // Проверка: артикул уже добавлен в какой-то грид?
            foreach (XtraTabPage tab in customTabControlKomplRazm.TabPages)
            {
                if (tab.Controls[0] is TableLayoutPanel layout)
                {
                    var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                    if (grid?.DataSource is BindingList<SpArticulGrupMenViewModel> list)
                    {
                        if (list.Any(x => x.articul == selectedRow.articul))
                        {
                            MessageBox.Show("Запись с таким артикулом уже добавлена в другую вкладку", "Дубликат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }

            // Поиск первой подходящей (пустой и прошедшей проверки) вкладки
            for (int i = 0; i < customTabControlKomplRazm.TabPages.Count; i++)
            {
                var tab = customTabControlKomplRazm.TabPages[i];
                if (tab.Controls[0] is not TableLayoutPanel layout) continue;

                var grid = layout.Controls.OfType<CustomGridControl>().FirstOrDefault();
                if (grid == null) continue;

                var list = grid.DataSource as BindingList<SpArticulGrupMenViewModel>;
                if (list == null)
                {
                    list = new BindingList<SpArticulGrupMenViewModel>();
                    grid.DataSource = list;
                }

                // Проверки:

                // 1. ТМ (kle)
                if (list.Any(x => x.kle?.Trim() != selectedRow.kle?.Trim()))
                {
                    MessageBox.Show("Комплектовать модели нельзя — разные торговые марки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                // 2. Производитель
                if (selectedRow.kodV == "2" && selectedRow.GrupMen.frm_s != null)
                {
                    if (list.Any(x => x.kodV == "2" && x.GrupMen.frm_s != selectedRow.GrupMen.frm_s))
                    {
                        MessageBox.Show("Комплектовать модели нельзя — разные производители", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                }

                // 3. Дубликаты по kod+razm
                bool hasDup = list.Any(existing =>
                    sameArticulRows.Any(x =>
                        x.kod == existing.kod && x.razm == existing.razm));

                if (hasDup)
                {
                    MessageBox.Show("Запись с таким кодом и размером уже выбрана", "Дубликат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue;
                }

                // Если грид пустой — это наш кандидат
                if (list.Count == 0)
                {
                    // Переключаемся на вкладку до вставки
                    customTabControlKomplRazm.SelectedTabPageIndex = i;

                    // Добавляем все размеры данного артикула
                    foreach (var row in sameArticulRows)
                    {
                        if (!list.Any(x => x.kod == row.kod && x.razm == row.razm))
                        {
                            list.Add(new SpArticulGrupMenViewModel
                            {
                                kod = row.kod,
                                grup = row.grup,
                                articul = row.articul,
                                mod = row.mod,
                                razm = row.razm,
                                sost = row.sost,
                                kodV = row.kodV,
                                kle = row.kle,
                                po = row.po,
                                prPo = row.prPo,
                                GrupMen = row.GrupMen
                            });
                        }
                    }

                    return;
                }
            }

            MessageBox.Show("Нет свободной вкладки для вставки артикула, либо не прошли проверки", "Не удалось вставить", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
