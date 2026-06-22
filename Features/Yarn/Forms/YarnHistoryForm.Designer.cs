using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Class;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SewingProduction.Features.Yarn.Forms
{
    internal partial class YarnHistoryForm
    {
        private IContainer components = null;
        private SplitContainer splitGrids;
        private CustomGridControl gridCostHistory;
        private GridView gvCostHistory;
        private BindingSource bsCostHistory;
        private CustomGridControl gridArticulHistory;
        private GridView gvArticulHistory;
        private BindingSource bsArticulHistory;
        private DevExpress.XtraEditors.GroupControl grpCost;
        private DevExpress.XtraEditors.GroupControl grpArticul;
        private CustomSimpleButton btnClose;
        private Panel panelBottom;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();

            splitGrids = new SplitContainer();
            grpCost = new DevExpress.XtraEditors.GroupControl();
            grpArticul = new DevExpress.XtraEditors.GroupControl();
            gridCostHistory = new CustomGridControl();
            bsCostHistory = new BindingSource(components);
            gvCostHistory = new GridView();
            gridArticulHistory = new CustomGridControl();
            bsArticulHistory = new BindingSource(components);
            gvArticulHistory = new GridView();
            btnClose = new CustomSimpleButton();
            panelBottom = new Panel();

            ((ISupportInitialize)splitGrids).BeginInit();
            splitGrids.SuspendLayout();
            ((ISupportInitialize)grpCost).BeginInit();
            grpCost.SuspendLayout();
            ((ISupportInitialize)grpArticul).BeginInit();
            grpArticul.SuspendLayout();
            ((ISupportInitialize)gridCostHistory).BeginInit();
            ((ISupportInitialize)bsCostHistory).BeginInit();
            ((ISupportInitialize)gvCostHistory).BeginInit();
            ((ISupportInitialize)gridArticulHistory).BeginInit();
            ((ISupportInitialize)bsArticulHistory).BeginInit();
            ((ISupportInitialize)gvArticulHistory).BeginInit();
            SuspendLayout();

            // Cost history grid
            gridCostHistory.DataSource = bsCostHistory;
            gridCostHistory.Font = new Font("Arial", 9F);
            gridCostHistory.MainView = gvCostHistory;
            gridCostHistory.Dock = DockStyle.Fill;
            gridCostHistory.Name = "gridCostHistory";
            gridCostHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvCostHistory });

            var chCols = new[]
            {
                C("Дата изм.", "data_izm", 80, "dd.MM.yyyy", DevExpress.Utils.FormatType.DateTime),
                C("С/С до", "ss_do", 75, "N2"), C("С/С после", "ss_posle", 75, "N2"),
                C("Норма", "norma", 70), C("Себ-ть", "seb", 70),
                C("Ткань обозн.", "tkn_oboz", 100), C("наименование", "tkn_naim", 110),
                C("код", "tkn_kod", 55), C("норма", "tkn_norma", 55), C("цена", "tkn_cena", 55),
                C("Кол сек.", "kol_sek", 55), C("Затр.ЗП", "zatraty_zp", 60),
                C("Доп.расх", "dop_rash", 60), C("Кпр.затр", "koef_pr_zatrat", 65),
                C("Коэф.", "koef", 55), C("Компьютер", "komp_user", 100)
            };
            gvCostHistory.Columns.AddRange(chCols);
            gvCostHistory.GridControl = gridCostHistory;
            gvCostHistory.Name = "gvCostHistory";
            StyleGrid(gvCostHistory, false);

            // Articul history grid
            gridArticulHistory.DataSource = bsArticulHistory;
            gridArticulHistory.Font = new Font("Arial", 9F);
            gridArticulHistory.MainView = gvArticulHistory;
            gridArticulHistory.Dock = DockStyle.Fill;
            gridArticulHistory.Name = "gridArticulHistory";
            gridArticulHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvArticulHistory });

            var ahCols = new[]
            {
                C("Компьютер", "komp_user", 170), C("Дата изменения", "data_smena", 115, "dd.MM.yyyy HH:mm", DevExpress.Utils.FormatType.DateTime),
                C("Код", "kod", 75), C("Поле", "field_name", 80),
                C("Поле комплекта", "field_n_k", 120), C("Значение до", "old_value", 180), C("Значение после", "new_value", 180)
            };
            gvArticulHistory.Columns.AddRange(ahCols);
            gvArticulHistory.GridControl = gridArticulHistory;
            gvArticulHistory.Name = "gvArticulHistory";
            StyleGrid(gvArticulHistory, true);

            // Groups
            grpCost.Text = "История смены себестоимости";
            grpCost.AppearanceCaption.Font = new Font("Arial", 10F, FontStyle.Bold);
            grpCost.AppearanceCaption.Options.UseFont = true;
            grpCost.Dock = DockStyle.Fill;
            grpCost.Controls.Add(gridCostHistory);
            grpCost.Padding = new Padding(2);

            grpArticul.Text = "История изменений по артикулу";
            grpArticul.AppearanceCaption.Font = new Font("Arial", 10F, FontStyle.Bold);
            grpArticul.AppearanceCaption.Options.UseFont = true;
            grpArticul.Dock = DockStyle.Fill;
            grpArticul.Controls.Add(gridArticulHistory);
            grpArticul.Padding = new Padding(2);

            // SplitContainer
            splitGrids.Dock = DockStyle.Fill;
            splitGrids.Orientation = Orientation.Horizontal;
            splitGrids.SplitterWidth = 6;
            splitGrids.Panel1.Controls.Add(grpCost);
            splitGrids.Panel2.Controls.Add(grpArticul);
            splitGrids.Panel1MinSize = 150;
            splitGrids.Panel2MinSize = 150;

            // Close button
            btnClose.Appearance.Font = new Font("Arial", 10F);
            btnClose.Appearance.Options.UseFont = true;
            btnClose.Name = "btnClose";
            btnClose.Text = "Закрыть";
            btnClose.Size = new Size(120, 34);
            btnClose.Click += btnClose_Click;

            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 44;
            panelBottom.Padding = new Padding(0, 4, 8, 4);
            btnClose.Dock = DockStyle.Right;
            panelBottom.Controls.Add(btnClose);

            // Form
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            MinimumSize = new Size(800, 450);
            StartPosition = FormStartPosition.CenterParent;
            Controls.Add(splitGrids);
            Controls.Add(panelBottom);
            Name = "YarnHistoryForm";
            Text = "История изменений";
            Load += YarnHistoryForm_Load;

            ((ISupportInitialize)gridCostHistory).EndInit();
            ((ISupportInitialize)bsCostHistory).EndInit();
            ((ISupportInitialize)gvCostHistory).EndInit();
            ((ISupportInitialize)gridArticulHistory).EndInit();
            ((ISupportInitialize)bsArticulHistory).EndInit();
            ((ISupportInitialize)gvArticulHistory).EndInit();
            ((ISupportInitialize)grpCost).EndInit();
            grpCost.ResumeLayout(false);
            ((ISupportInitialize)grpArticul).EndInit();
            grpArticul.ResumeLayout(false);
            ((ISupportInitialize)splitGrids).EndInit();
            splitGrids.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static GridColumn C(string caption, string field, int width, string fmt = null, DevExpress.Utils.FormatType ft = DevExpress.Utils.FormatType.Numeric)
        {
            var c = new GridColumn { Caption = caption, FieldName = field, Width = width, Visible = true, VisibleIndex = -1 };
            c.Name = "col_" + field;
            if (fmt != null) { c.DisplayFormat.FormatType = ft; c.DisplayFormat.FormatString = fmt; }
            return c;
        }

        private static void StyleGrid(GridView gv, bool autoWidth)
        {
            gv.OptionsBehavior.Editable = false;
            gv.OptionsBehavior.ReadOnly = true;
            gv.OptionsFind.AlwaysVisible = true;
            gv.OptionsView.ShowAutoFilterRow = true;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ColumnAutoWidth = autoWidth;
            gv.OptionsView.ShowIndicator = false;
            gv.OptionsView.EnableAppearanceEvenRow = true;
            gv.Appearance.EvenRow.BackColor = Color.FromArgb(245, 248, 252);
            gv.Appearance.EvenRow.Options.UseBackColor = true;
            gv.Appearance.FocusedRow.BackColor = Color.FromArgb(210, 228, 248);
            gv.Appearance.FocusedRow.Options.UseBackColor = true;
            gv.RowHeight = 22;
        }
    }
}
