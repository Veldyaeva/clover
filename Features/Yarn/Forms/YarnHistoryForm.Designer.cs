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

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup rootGroup;
        private DevExpress.XtraLayout.LayoutControlGroup grpCostHistory;
        private DevExpress.XtraLayout.LayoutControlGroup grpArticulHistory;

        private CustomGridControl gridCostHistory;
        private GridView gridViewCostHistory;
        private BindingSource bsCostHistory;

        private CustomGridControl gridArticulHistory;
        private GridView gridViewArticulHistory;
        private BindingSource bsArticulHistory;

        private CustomSimpleButton btnClose;

        private DevExpress.XtraLayout.LayoutControlItem lciGridCost;
        private DevExpress.XtraLayout.LayoutControlItem lciGridArticul;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpace1;
        private DevExpress.XtraLayout.SplitterItem splitter1;

        private GridColumn colDataIzm, colSsDo, colSsPosle, colNorma, colSeb;
        private GridColumn colTknOboz, colTknNaim, colTknKod, colTknNorma, colTknCena;
        private GridColumn colKolSek, colZatratyZp, colDopRash, colKoefPrZatrat, colKoef, colKompUser;

        private GridColumn colAhKompUser, colAhDataSmena, colAhKod;
        private GridColumn colAhFieldName, colAhFieldNK, colAhOldValue, colAhNewValue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            gridCostHistory = new CustomGridControl();
            bsCostHistory = new BindingSource(components);
            gridViewCostHistory = new GridView();
            gridArticulHistory = new CustomGridControl();
            bsArticulHistory = new BindingSource(components);
            gridViewArticulHistory = new GridView();
            btnClose = new CustomSimpleButton();
            layoutControl = new DevExpress.XtraLayout.LayoutControl();
            rootGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            grpCostHistory = new DevExpress.XtraLayout.LayoutControlGroup();
            grpArticulHistory = new DevExpress.XtraLayout.LayoutControlGroup();
            lciGridCost = new DevExpress.XtraLayout.LayoutControlItem();
            lciGridArticul = new DevExpress.XtraLayout.LayoutControlItem();
            lciBtnClose = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpace1 = new DevExpress.XtraLayout.EmptySpaceItem();
            splitter1 = new DevExpress.XtraLayout.SplitterItem();

            colDataIzm = new GridColumn();
            colSsDo = new GridColumn();
            colSsPosle = new GridColumn();
            colNorma = new GridColumn();
            colSeb = new GridColumn();
            colTknOboz = new GridColumn();
            colTknNaim = new GridColumn();
            colTknKod = new GridColumn();
            colTknNorma = new GridColumn();
            colTknCena = new GridColumn();
            colKolSek = new GridColumn();
            colZatratyZp = new GridColumn();
            colDopRash = new GridColumn();
            colKoefPrZatrat = new GridColumn();
            colKoef = new GridColumn();
            colKompUser = new GridColumn();
            colAhKompUser = new GridColumn();
            colAhDataSmena = new GridColumn();
            colAhKod = new GridColumn();
            colAhFieldName = new GridColumn();
            colAhFieldNK = new GridColumn();
            colAhOldValue = new GridColumn();
            colAhNewValue = new GridColumn();

            ((ISupportInitialize)gridCostHistory).BeginInit();
            ((ISupportInitialize)bsCostHistory).BeginInit();
            ((ISupportInitialize)gridViewCostHistory).BeginInit();
            ((ISupportInitialize)gridArticulHistory).BeginInit();
            ((ISupportInitialize)bsArticulHistory).BeginInit();
            ((ISupportInitialize)gridViewArticulHistory).BeginInit();
            ((ISupportInitialize)layoutControl).BeginInit();
            layoutControl.SuspendLayout();
            ((ISupportInitialize)rootGroup).BeginInit();
            ((ISupportInitialize)grpCostHistory).BeginInit();
            ((ISupportInitialize)grpArticulHistory).BeginInit();
            ((ISupportInitialize)lciGridCost).BeginInit();
            ((ISupportInitialize)lciGridArticul).BeginInit();
            ((ISupportInitialize)lciBtnClose).BeginInit();
            ((ISupportInitialize)emptySpace1).BeginInit();
            ((ISupportInitialize)splitter1).BeginInit();
            SuspendLayout();
            //
            // gridCostHistory
            //
            gridCostHistory.DataSource = bsCostHistory;
            gridCostHistory.Font = new Font("Arial", 9F);
            gridCostHistory.MainView = gridViewCostHistory;
            gridCostHistory.Name = "gridCostHistory";
            gridCostHistory.Size = new Size(1160, 300);
            gridCostHistory.TabIndex = 0;
            gridCostHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewCostHistory });
            //
            // gridViewCostHistory
            //
            gridViewCostHistory.Columns.AddRange(new GridColumn[] {
                colDataIzm, colSsDo, colSsPosle, colNorma, colSeb,
                colTknOboz, colTknNaim, colTknKod, colTknNorma, colTknCena,
                colKolSek, colZatratyZp, colDopRash, colKoefPrZatrat, colKoef, colKompUser });
            gridViewCostHistory.GridControl = gridCostHistory;
            gridViewCostHistory.Name = "gridViewCostHistory";
            gridViewCostHistory.OptionsBehavior.Editable = false;
            gridViewCostHistory.OptionsBehavior.ReadOnly = true;
            gridViewCostHistory.OptionsFind.AlwaysVisible = true;
            gridViewCostHistory.OptionsView.ShowAutoFilterRow = true;
            gridViewCostHistory.OptionsView.ShowGroupPanel = false;
            gridViewCostHistory.OptionsView.ColumnAutoWidth = false;
            gridViewCostHistory.OptionsView.ShowIndicator = false;
            gridViewCostHistory.OptionsView.EnableAppearanceEvenRow = true;
            gridViewCostHistory.Appearance.EvenRow.BackColor = Color.FromArgb(245, 248, 252);
            gridViewCostHistory.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewCostHistory.Appearance.FocusedRow.BackColor = Color.FromArgb(210, 228, 248);
            gridViewCostHistory.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewCostHistory.RowHeight = 22;
            //
            // Cost history columns
            //
            SetupColumn(colDataIzm, "Дата изм.", "data_izm", 0, 85, DevExpress.Utils.FormatType.DateTime, "dd.MM.yyyy");
            SetupColumn(colSsDo, "С/С расч. до", "ss_do", 1, 85, DevExpress.Utils.FormatType.Numeric, "N2");
            SetupColumn(colSsPosle, "С/С расч. после", "ss_posle", 2, 85, DevExpress.Utils.FormatType.Numeric, "N2");
            SetupColumn(colNorma, "Норма ткани/пряжи", "norma", 3, 85);
            SetupColumn(colSeb, "Себ-ть ткани/пряжи", "seb", 4, 85);
            SetupColumn(colTknOboz, "Ткань (обозн.)", "tkn_oboz", 5, 110);
            SetupColumn(colTknNaim, "наименование", "tkn_naim", 6, 120);
            SetupColumn(colTknKod, "код", "tkn_kod", 7, 65);
            SetupColumn(colTknNorma, "норма", "tkn_norma", 8, 65);
            SetupColumn(colTknCena, "цена", "tkn_cena", 9, 65);
            SetupColumn(colKolSek, "Кол-во сек.", "kol_sek", 10, 65);
            SetupColumn(colZatratyZp, "Затраты ЗП", "zatraty_zp", 11, 70);
            SetupColumn(colDopRash, "Доп. расх.", "dop_rash", 12, 65);
            SetupColumn(colKoefPrZatrat, "Коэф. пр. затрат", "koef_pr_zatrat", 13, 75);
            SetupColumn(colKoef, "Коэф.", "koef", 14, 60);
            SetupColumn(colKompUser, "Компьютер", "komp_user", 15, 110);
            //
            // gridArticulHistory
            //
            gridArticulHistory.DataSource = bsArticulHistory;
            gridArticulHistory.Font = new Font("Arial", 9F);
            gridArticulHistory.MainView = gridViewArticulHistory;
            gridArticulHistory.Name = "gridArticulHistory";
            gridArticulHistory.Size = new Size(1160, 250);
            gridArticulHistory.TabIndex = 1;
            gridArticulHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewArticulHistory });
            //
            // gridViewArticulHistory
            //
            gridViewArticulHistory.Columns.AddRange(new GridColumn[] {
                colAhKompUser, colAhDataSmena, colAhKod, colAhFieldName, colAhFieldNK, colAhOldValue, colAhNewValue });
            gridViewArticulHistory.GridControl = gridArticulHistory;
            gridViewArticulHistory.Name = "gridViewArticulHistory";
            gridViewArticulHistory.OptionsBehavior.Editable = false;
            gridViewArticulHistory.OptionsBehavior.ReadOnly = true;
            gridViewArticulHistory.OptionsFind.AlwaysVisible = true;
            gridViewArticulHistory.OptionsView.ShowAutoFilterRow = true;
            gridViewArticulHistory.OptionsView.ShowGroupPanel = false;
            gridViewArticulHistory.OptionsView.ColumnAutoWidth = true;
            gridViewArticulHistory.OptionsView.ShowIndicator = false;
            gridViewArticulHistory.OptionsView.EnableAppearanceEvenRow = true;
            gridViewArticulHistory.Appearance.EvenRow.BackColor = Color.FromArgb(245, 248, 252);
            gridViewArticulHistory.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewArticulHistory.Appearance.FocusedRow.BackColor = Color.FromArgb(210, 228, 248);
            gridViewArticulHistory.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewArticulHistory.RowHeight = 22;
            //
            // Articul history columns
            //
            SetupColumn(colAhKompUser, "Компьютер", "komp_user", 0, 180);
            SetupColumn(colAhDataSmena, "Дата изменения", "data_smena", 1, 120, DevExpress.Utils.FormatType.DateTime, "dd.MM.yyyy HH:mm");
            SetupColumn(colAhKod, "Код", "kod", 2, 80);
            SetupColumn(colAhFieldName, "Поле", "field_name", 3, 80);
            SetupColumn(colAhFieldNK, "Поле комплекта", "field_n_k", 4, 130);
            SetupColumn(colAhOldValue, "Значение до", "old_value", 5, 190);
            SetupColumn(colAhNewValue, "Значение после", "new_value", 6, 190);
            //
            // btnClose
            //
            btnClose.Appearance.Font = new Font("Arial", 10F);
            btnClose.Appearance.Options.UseFont = true;
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(130, 34);
            btnClose.TabIndex = 2;
            btnClose.Text = "Закрыть";
            btnClose.Click += btnClose_Click;
            //
            // layoutControl
            //
            layoutControl.Controls.Add(gridCostHistory);
            layoutControl.Controls.Add(gridArticulHistory);
            layoutControl.Controls.Add(btnClose);
            layoutControl.Dock = DockStyle.Fill;
            layoutControl.Name = "layoutControl";
            layoutControl.Root = rootGroup;
            layoutControl.Size = new Size(1200, 750);
            layoutControl.TabIndex = 0;
            //
            // rootGroup
            //
            rootGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            rootGroup.GroupBordersVisible = false;
            rootGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                grpCostHistory, splitter1, grpArticulHistory, emptySpace1, lciBtnClose });
            rootGroup.Name = "rootGroup";
            rootGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            rootGroup.Size = new Size(1200, 750);
            //
            // grpCostHistory
            //
            grpCostHistory.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lciGridCost });
            grpCostHistory.Location = new Point(0, 0);
            grpCostHistory.Name = "grpCostHistory";
            grpCostHistory.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            grpCostHistory.Size = new Size(1192, 340);
            grpCostHistory.Text = "История смены себестоимости";
            grpCostHistory.AppearanceGroup.Font = new Font("Arial", 10F, FontStyle.Bold);
            grpCostHistory.AppearanceGroup.Options.UseFont = true;
            //
            // splitter1
            //
            splitter1.AllowHotTrack = true;
            splitter1.Location = new Point(0, 340);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(1192, 10);
            //
            // grpArticulHistory
            //
            grpArticulHistory.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lciGridArticul });
            grpArticulHistory.Location = new Point(0, 350);
            grpArticulHistory.Name = "grpArticulHistory";
            grpArticulHistory.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            grpArticulHistory.Size = new Size(1192, 300);
            grpArticulHistory.Text = "История изменений по артикулу";
            grpArticulHistory.AppearanceGroup.Font = new Font("Arial", 10F, FontStyle.Bold);
            grpArticulHistory.AppearanceGroup.Options.UseFont = true;
            //
            // lciGridCost
            //
            lciGridCost.Control = gridCostHistory;
            lciGridCost.Name = "lciGridCost";
            lciGridCost.TextVisible = false;
            //
            // lciGridArticul
            //
            lciGridArticul.Control = gridArticulHistory;
            lciGridArticul.Name = "lciGridArticul";
            lciGridArticul.TextVisible = false;
            //
            // emptySpace1
            //
            emptySpace1.AllowHotTrack = false;
            emptySpace1.Location = new Point(0, 650);
            emptySpace1.Name = "emptySpace1";
            emptySpace1.Size = new Size(1052, 42);
            //
            // lciBtnClose
            //
            lciBtnClose.Control = btnClose;
            lciBtnClose.Location = new Point(1052, 650);
            lciBtnClose.Name = "lciBtnClose";
            lciBtnClose.Size = new Size(140, 42);
            lciBtnClose.TextVisible = false;
            lciBtnClose.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 2);
            //
            // YarnHistoryForm
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 750);
            Controls.Add(layoutControl);
            MinimumSize = new Size(900, 550);
            Name = "YarnHistoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "История изменений";
            Load += YarnHistoryForm_Load;
            ((ISupportInitialize)gridCostHistory).EndInit();
            ((ISupportInitialize)bsCostHistory).EndInit();
            ((ISupportInitialize)gridViewCostHistory).EndInit();
            ((ISupportInitialize)gridArticulHistory).EndInit();
            ((ISupportInitialize)bsArticulHistory).EndInit();
            ((ISupportInitialize)gridViewArticulHistory).EndInit();
            ((ISupportInitialize)layoutControl).EndInit();
            layoutControl.ResumeLayout(false);
            ((ISupportInitialize)rootGroup).EndInit();
            ((ISupportInitialize)grpCostHistory).EndInit();
            ((ISupportInitialize)grpArticulHistory).EndInit();
            ((ISupportInitialize)lciGridCost).EndInit();
            ((ISupportInitialize)lciGridArticul).EndInit();
            ((ISupportInitialize)lciBtnClose).EndInit();
            ((ISupportInitialize)emptySpace1).EndInit();
            ((ISupportInitialize)splitter1).EndInit();
            ResumeLayout(false);
        }

        private static void SetupColumn(GridColumn col, string caption, string fieldName, int visibleIndex, int width,
            DevExpress.Utils.FormatType formatType = DevExpress.Utils.FormatType.None, string formatString = null)
        {
            col.Caption = caption;
            col.FieldName = fieldName;
            col.Name = "col_" + fieldName;
            col.Visible = true;
            col.VisibleIndex = visibleIndex;
            col.Width = width;
            if (formatType != DevExpress.Utils.FormatType.None && formatString != null)
            {
                col.DisplayFormat.FormatType = formatType;
                col.DisplayFormat.FormatString = formatString;
            }
        }
    }
}
