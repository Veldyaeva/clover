using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Class;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SewingProduction.Features.Yarn.Forms
{
    internal partial class YarnForm
    {
        private IContainer components = null;

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup rootGroup;
        private DevExpress.XtraLayout.LayoutControlGroup grpInputs;
        private DevExpress.XtraLayout.LayoutControlGroup grpGrid;
        private DevExpress.XtraLayout.LayoutControlGroup grpColorSearch;

        private CustomTextBoxEx txtNakl;
        private CustomTextBoxEx txtArticul;
        private CustomTextBoxEx txtSebUpr;
        private CustomTextBoxEx txtSebDok;
        private CustomTextBoxEx txtColorSearch;
        private CustomSimpleButton btnObnovit;
        private CustomSimpleButton btnCalc;
        private CustomSimpleButton btnHistory;

        private CustomGridControl gridControl;
        private GridView gridView;
        private BindingSource bindingSource;
        private GridColumn colNakl;
        private GridColumn colArticul;
        private GridColumn colZvet;
        private GridColumn colSebTM;

        private CustomGridControl gridColorResult;
        private GridView gridViewColor;
        private BindingSource bsColorResult;
        private GridColumn colCrNakl;
        private GridColumn colCrArticul;

        private DevExpress.XtraLayout.LayoutControlItem lciNakl;
        private DevExpress.XtraLayout.LayoutControlItem lciArticul;
        private DevExpress.XtraLayout.LayoutControlItem lciSebUpr;
        private DevExpress.XtraLayout.LayoutControlItem lciSebDok;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnCalc;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnObnovit;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnHistory;
        private DevExpress.XtraLayout.LayoutControlItem lciGrid;
        private DevExpress.XtraLayout.LayoutControlItem lciColorSearch;
        private DevExpress.XtraLayout.LayoutControlItem lciGridColor;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpace1;
        private DevExpress.XtraLayout.SplitterItem splitterH;
        private DevExpress.XtraLayout.SplitterItem splitterV;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            txtNakl = new CustomTextBoxEx();
            txtArticul = new CustomTextBoxEx();
            txtSebUpr = new CustomTextBoxEx();
            txtSebDok = new CustomTextBoxEx();
            txtColorSearch = new CustomTextBoxEx();
            btnCalc = new CustomSimpleButton();
            btnObnovit = new CustomSimpleButton();
            btnHistory = new CustomSimpleButton();

            gridControl = new CustomGridControl();
            bindingSource = new BindingSource(components);
            gridView = new GridView();
            colNakl = new GridColumn();
            colArticul = new GridColumn();
            colZvet = new GridColumn();
            colSebTM = new GridColumn();

            gridColorResult = new CustomGridControl();
            bsColorResult = new BindingSource(components);
            gridViewColor = new GridView();
            colCrNakl = new GridColumn();
            colCrArticul = new GridColumn();

            layoutControl = new DevExpress.XtraLayout.LayoutControl();
            rootGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            grpInputs = new DevExpress.XtraLayout.LayoutControlGroup();
            grpGrid = new DevExpress.XtraLayout.LayoutControlGroup();
            grpColorSearch = new DevExpress.XtraLayout.LayoutControlGroup();
            lciNakl = new DevExpress.XtraLayout.LayoutControlItem();
            lciArticul = new DevExpress.XtraLayout.LayoutControlItem();
            lciSebUpr = new DevExpress.XtraLayout.LayoutControlItem();
            lciSebDok = new DevExpress.XtraLayout.LayoutControlItem();
            lciBtnCalc = new DevExpress.XtraLayout.LayoutControlItem();
            lciBtnObnovit = new DevExpress.XtraLayout.LayoutControlItem();
            lciBtnHistory = new DevExpress.XtraLayout.LayoutControlItem();
            lciGrid = new DevExpress.XtraLayout.LayoutControlItem();
            lciColorSearch = new DevExpress.XtraLayout.LayoutControlItem();
            lciGridColor = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpace1 = new DevExpress.XtraLayout.EmptySpaceItem();
            splitterH = new DevExpress.XtraLayout.SplitterItem();
            splitterV = new DevExpress.XtraLayout.SplitterItem();

            ((ISupportInitialize)txtNakl.Properties).BeginInit();
            ((ISupportInitialize)txtArticul.Properties).BeginInit();
            ((ISupportInitialize)txtSebUpr.Properties).BeginInit();
            ((ISupportInitialize)txtSebDok.Properties).BeginInit();
            ((ISupportInitialize)txtColorSearch.Properties).BeginInit();
            ((ISupportInitialize)gridControl).BeginInit();
            ((ISupportInitialize)bindingSource).BeginInit();
            ((ISupportInitialize)gridView).BeginInit();
            ((ISupportInitialize)gridColorResult).BeginInit();
            ((ISupportInitialize)bsColorResult).BeginInit();
            ((ISupportInitialize)gridViewColor).BeginInit();
            ((ISupportInitialize)layoutControl).BeginInit();
            layoutControl.SuspendLayout();
            ((ISupportInitialize)rootGroup).BeginInit();
            ((ISupportInitialize)grpInputs).BeginInit();
            ((ISupportInitialize)grpGrid).BeginInit();
            ((ISupportInitialize)grpColorSearch).BeginInit();
            ((ISupportInitialize)lciNakl).BeginInit();
            ((ISupportInitialize)lciArticul).BeginInit();
            ((ISupportInitialize)lciSebUpr).BeginInit();
            ((ISupportInitialize)lciSebDok).BeginInit();
            ((ISupportInitialize)lciBtnCalc).BeginInit();
            ((ISupportInitialize)lciBtnObnovit).BeginInit();
            ((ISupportInitialize)lciBtnHistory).BeginInit();
            ((ISupportInitialize)lciGrid).BeginInit();
            ((ISupportInitialize)lciColorSearch).BeginInit();
            ((ISupportInitialize)lciGridColor).BeginInit();
            ((ISupportInitialize)emptySpace1).BeginInit();
            ((ISupportInitialize)splitterH).BeginInit();
            ((ISupportInitialize)splitterV).BeginInit();
            SuspendLayout();
            //
            // txtNakl
            //
            txtNakl.Name = "txtNakl";
            txtNakl.ObjectName = null;
            txtNakl.Properties.Appearance.Font = new Font("Arial", 10F);
            txtNakl.Properties.Appearance.Options.UseFont = true;
            txtNakl.Size = new Size(250, 22);
            txtNakl.TabIndex = 0;
            txtNakl.KeyDown += txtNakl_KeyDown;
            //
            // txtArticul
            //
            txtArticul.Name = "txtArticul";
            txtArticul.ObjectName = null;
            txtArticul.Properties.Appearance.Font = new Font("Arial", 10F);
            txtArticul.Properties.Appearance.Options.UseFont = true;
            txtArticul.Properties.ReadOnly = true;
            txtArticul.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 245);
            txtArticul.Properties.Appearance.Options.UseBackColor = true;
            txtArticul.Size = new Size(250, 22);
            txtArticul.TabIndex = 1;
            //
            // txtSebUpr
            //
            txtSebUpr.Name = "txtSebUpr";
            txtSebUpr.ObjectName = null;
            txtSebUpr.Properties.Appearance.Font = new Font("Arial", 10F, FontStyle.Bold);
            txtSebUpr.Properties.Appearance.Options.UseFont = true;
            txtSebUpr.Properties.ReadOnly = true;
            txtSebUpr.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 245);
            txtSebUpr.Properties.Appearance.Options.UseBackColor = true;
            txtSebUpr.Size = new Size(250, 22);
            txtSebUpr.TabIndex = 2;
            //
            // txtSebDok
            //
            txtSebDok.Name = "txtSebDok";
            txtSebDok.ObjectName = null;
            txtSebDok.Properties.Appearance.Font = new Font("Arial", 10F);
            txtSebDok.Properties.Appearance.Options.UseFont = true;
            txtSebDok.Properties.ReadOnly = true;
            txtSebDok.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 245);
            txtSebDok.Properties.Appearance.Options.UseBackColor = true;
            txtSebDok.Size = new Size(250, 22);
            txtSebDok.TabIndex = 3;
            //
            // txtColorSearch
            //
            txtColorSearch.Name = "txtColorSearch";
            txtColorSearch.ObjectName = null;
            txtColorSearch.Properties.Appearance.Font = new Font("Arial", 10F);
            txtColorSearch.Properties.Appearance.Options.UseFont = true;
            txtColorSearch.Properties.NullValuePrompt = "введите цвет и нажмите Enter";
            txtColorSearch.Properties.NullValuePromptShowForEmptyValue = true;
            txtColorSearch.Size = new Size(200, 22);
            txtColorSearch.TabIndex = 8;
            txtColorSearch.KeyDown += txtColorSearch_KeyDown;
            //
            // btnCalc
            //
            btnCalc.Appearance.Font = new Font("Arial", 9F);
            btnCalc.Appearance.Options.UseFont = true;
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(120, 26);
            btnCalc.TabIndex = 4;
            btnCalc.Text = "рассчитать";
            btnCalc.Click += btnCalc_Click;
            //
            // btnObnovit
            //
            btnObnovit.Appearance.Font = new Font("Arial", 10F);
            btnObnovit.Appearance.Options.UseFont = true;
            btnObnovit.Name = "btnObnovit";
            btnObnovit.Size = new Size(160, 34);
            btnObnovit.TabIndex = 5;
            btnObnovit.Text = "Обновить";
            btnObnovit.Click += btnObnovit_Click;
            //
            // btnHistory
            //
            btnHistory.Appearance.Font = new Font("Arial", 10F);
            btnHistory.Appearance.Options.UseFont = true;
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(180, 34);
            btnHistory.TabIndex = 6;
            btnHistory.Text = "История изменений";
            btnHistory.Click += btnHistory_Click;
            //
            // gridControl (main)
            //
            gridControl.DataSource = bindingSource;
            gridControl.Font = new Font("Arial", 10F);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl";
            gridControl.Size = new Size(650, 400);
            gridControl.TabIndex = 7;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            //
            // gridView
            //
            gridView.Columns.AddRange(new GridColumn[] { colNakl, colArticul, colZvet, colSebTM });
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.ReadOnly = true;
            gridView.OptionsFind.AlwaysVisible = true;
            gridView.OptionsView.ShowAutoFilterRow = true;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.OptionsView.ShowIndicator = false;
            gridView.OptionsView.EnableAppearanceEvenRow = true;
            gridView.Appearance.EvenRow.BackColor = Color.FromArgb(245, 248, 252);
            gridView.Appearance.EvenRow.Options.UseBackColor = true;
            gridView.Appearance.FocusedRow.BackColor = Color.FromArgb(210, 228, 248);
            gridView.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView.RowHeight = 24;
            //
            // main grid columns
            //
            colNakl.Caption = "Карта";
            colNakl.FieldName = "nakl";
            colNakl.Name = "colNakl";
            colNakl.Visible = true;
            colNakl.VisibleIndex = 0;
            colNakl.Width = 120;

            colArticul.Caption = "Артикул";
            colArticul.FieldName = "t_articul";
            colArticul.Name = "colArticul";
            colArticul.Visible = true;
            colArticul.VisibleIndex = 1;
            colArticul.Width = 250;

            colZvet.Caption = "Цвет";
            colZvet.FieldName = "zvet";
            colZvet.Name = "colZvet";
            colZvet.Visible = true;
            colZvet.VisibleIndex = 2;
            colZvet.Width = 180;

            colSebTM.Caption = "Себестоимость";
            colSebTM.FieldName = "seb_t_m";
            colSebTM.Name = "colSebTM";
            colSebTM.Visible = true;
            colSebTM.VisibleIndex = 3;
            colSebTM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSebTM.DisplayFormat.FormatString = "N3";
            colSebTM.Width = 150;
            //
            // gridColorResult (color search)
            //
            gridColorResult.DataSource = bsColorResult;
            gridColorResult.Font = new Font("Arial", 10F);
            gridColorResult.MainView = gridViewColor;
            gridColorResult.Name = "gridColorResult";
            gridColorResult.Size = new Size(280, 400);
            gridColorResult.TabIndex = 9;
            gridColorResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewColor });
            //
            // gridViewColor
            //
            gridViewColor.Columns.AddRange(new GridColumn[] { colCrNakl, colCrArticul });
            gridViewColor.GridControl = gridColorResult;
            gridViewColor.Name = "gridViewColor";
            gridViewColor.OptionsBehavior.Editable = false;
            gridViewColor.OptionsBehavior.ReadOnly = true;
            gridViewColor.OptionsView.ShowAutoFilterRow = true;
            gridViewColor.OptionsView.ShowGroupPanel = false;
            gridViewColor.OptionsView.ColumnAutoWidth = true;
            gridViewColor.OptionsView.ShowIndicator = false;
            gridViewColor.OptionsView.EnableAppearanceEvenRow = true;
            gridViewColor.Appearance.EvenRow.BackColor = Color.FromArgb(245, 248, 252);
            gridViewColor.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewColor.Appearance.FocusedRow.BackColor = Color.FromArgb(210, 228, 248);
            gridViewColor.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewColor.RowHeight = 24;
            gridViewColor.DoubleClick += gridViewColor_DoubleClick;
            //
            // color result columns
            //
            colCrNakl.Caption = "Карта";
            colCrNakl.FieldName = "nakl";
            colCrNakl.Name = "colCrNakl";
            colCrNakl.Visible = true;
            colCrNakl.VisibleIndex = 0;
            colCrNakl.Width = 100;

            colCrArticul.Caption = "Артикул";
            colCrArticul.FieldName = "t_articul";
            colCrArticul.Name = "colCrArticul";
            colCrArticul.Visible = true;
            colCrArticul.VisibleIndex = 1;
            colCrArticul.Width = 150;
            //
            // layoutControl
            //
            layoutControl.Controls.Add(txtNakl);
            layoutControl.Controls.Add(txtArticul);
            layoutControl.Controls.Add(txtSebUpr);
            layoutControl.Controls.Add(txtSebDok);
            layoutControl.Controls.Add(txtColorSearch);
            layoutControl.Controls.Add(btnCalc);
            layoutControl.Controls.Add(btnObnovit);
            layoutControl.Controls.Add(btnHistory);
            layoutControl.Controls.Add(gridControl);
            layoutControl.Controls.Add(gridColorResult);
            layoutControl.Dock = DockStyle.Fill;
            layoutControl.Name = "layoutControl";
            layoutControl.Root = rootGroup;
            layoutControl.Size = new Size(1100, 700);
            layoutControl.TabIndex = 0;
            //
            // rootGroup
            //
            rootGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            rootGroup.GroupBordersVisible = false;
            rootGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                grpInputs, splitterV, grpColorSearch, splitterH, grpGrid });
            rootGroup.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            rootGroup.Name = "rootGroup";
            rootGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            rootGroup.Size = new Size(1100, 700);

            var columnDef1 = new DevExpress.XtraLayout.ColumnDefinition();
            columnDef1.SizeType = SizeType.Percent;
            columnDef1.Width = 65;
            var columnDef2 = new DevExpress.XtraLayout.ColumnDefinition();
            columnDef2.SizeType = SizeType.AutoSize;
            var columnDef3 = new DevExpress.XtraLayout.ColumnDefinition();
            columnDef3.SizeType = SizeType.Percent;
            columnDef3.Width = 35;
            rootGroup.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] { columnDef1, columnDef2, columnDef3 });

            var rowDef1 = new DevExpress.XtraLayout.RowDefinition();
            rowDef1.SizeType = SizeType.Absolute;
            rowDef1.Height = 210;
            var rowDef2 = new DevExpress.XtraLayout.RowDefinition();
            rowDef2.SizeType = SizeType.AutoSize;
            var rowDef3 = new DevExpress.XtraLayout.RowDefinition();
            rowDef3.SizeType = SizeType.Percent;
            rowDef3.Height = 100;
            rootGroup.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] { rowDef1, rowDef2, rowDef3 });
            //
            // grpInputs (row 0, col 0)
            //
            grpInputs.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                lciNakl, lciArticul, lciSebUpr, lciBtnCalc, lciSebDok,
                lciBtnObnovit, lciBtnHistory, emptySpace1 });
            grpInputs.Name = "grpInputs";
            grpInputs.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            grpInputs.Text = "Пряжа";
            grpInputs.OptionsTableLayoutItem.RowIndex = 0;
            grpInputs.OptionsTableLayoutItem.ColumnIndex = 0;
            //
            // splitterV (row 0, col 1) — vertical splitter between inputs and color search
            //
            splitterV.AllowHotTrack = true;
            splitterV.Name = "splitterV";
            splitterV.OptionsTableLayoutItem.RowIndex = 0;
            splitterV.OptionsTableLayoutItem.ColumnIndex = 1;
            splitterV.OptionsTableLayoutItem.RowSpan = 3;
            //
            // grpColorSearch (row 0, col 2) — right panel
            //
            grpColorSearch.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lciColorSearch, lciGridColor });
            grpColorSearch.Name = "grpColorSearch";
            grpColorSearch.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            grpColorSearch.Text = "Поиск по цвету";
            grpColorSearch.AppearanceGroup.Font = new Font("Arial", 10F, FontStyle.Bold);
            grpColorSearch.AppearanceGroup.Options.UseFont = true;
            grpColorSearch.OptionsTableLayoutItem.RowIndex = 0;
            grpColorSearch.OptionsTableLayoutItem.ColumnIndex = 2;
            grpColorSearch.OptionsTableLayoutItem.RowSpan = 3;
            //
            // splitterH (row 1, col 0) — horizontal splitter between inputs and grid
            //
            splitterH.AllowHotTrack = true;
            splitterH.Name = "splitterH";
            splitterH.OptionsTableLayoutItem.RowIndex = 1;
            splitterH.OptionsTableLayoutItem.ColumnIndex = 0;
            //
            // grpGrid (row 2, col 0)
            //
            grpGrid.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lciGrid });
            grpGrid.Name = "grpGrid";
            grpGrid.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            grpGrid.Text = "Данные карты";
            grpGrid.OptionsTableLayoutItem.RowIndex = 2;
            grpGrid.OptionsTableLayoutItem.ColumnIndex = 0;
            //
            // lciNakl
            //
            lciNakl.Control = txtNakl;
            lciNakl.Location = new Point(0, 0);
            lciNakl.MaxSize = new Size(450, 30);
            lciNakl.MinSize = new Size(300, 30);
            lciNakl.Name = "lciNakl";
            lciNakl.Size = new Size(450, 30);
            lciNakl.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            lciNakl.Text = "Введите № карты пряжи";
            lciNakl.TextSize = new Size(150, 16);
            lciNakl.AppearanceItemCaption.Font = new Font("Arial", 10F);
            lciNakl.AppearanceItemCaption.Options.UseFont = true;
            //
            // lciArticul
            //
            lciArticul.Control = txtArticul;
            lciArticul.Location = new Point(0, 30);
            lciArticul.MaxSize = new Size(450, 30);
            lciArticul.MinSize = new Size(300, 30);
            lciArticul.Name = "lciArticul";
            lciArticul.Size = new Size(450, 30);
            lciArticul.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            lciArticul.Text = "Артикул";
            lciArticul.TextSize = new Size(150, 16);
            lciArticul.AppearanceItemCaption.Font = new Font("Arial", 10F);
            lciArticul.AppearanceItemCaption.Options.UseFont = true;
            //
            // lciSebUpr
            //
            lciSebUpr.Control = txtSebUpr;
            lciSebUpr.Location = new Point(0, 60);
            lciSebUpr.MaxSize = new Size(350, 30);
            lciSebUpr.MinSize = new Size(250, 30);
            lciSebUpr.Name = "lciSebUpr";
            lciSebUpr.Size = new Size(350, 30);
            lciSebUpr.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            lciSebUpr.Text = "Себ-ть упр.";
            lciSebUpr.TextSize = new Size(150, 16);
            lciSebUpr.AppearanceItemCaption.Font = new Font("Arial", 10F);
            lciSebUpr.AppearanceItemCaption.Options.UseFont = true;
            //
            // lciBtnCalc
            //
            lciBtnCalc.Control = btnCalc;
            lciBtnCalc.Location = new Point(350, 60);
            lciBtnCalc.Name = "lciBtnCalc";
            lciBtnCalc.Size = new Size(130, 30);
            lciBtnCalc.TextVisible = false;
            //
            // lciSebDok
            //
            lciSebDok.Control = txtSebDok;
            lciSebDok.Location = new Point(0, 90);
            lciSebDok.MaxSize = new Size(450, 30);
            lciSebDok.MinSize = new Size(300, 30);
            lciSebDok.Name = "lciSebDok";
            lciSebDok.Size = new Size(450, 30);
            lciSebDok.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            lciSebDok.Text = "Себ-ть по документам";
            lciSebDok.TextSize = new Size(150, 16);
            lciSebDok.AppearanceItemCaption.Font = new Font("Arial", 10F);
            lciSebDok.AppearanceItemCaption.Options.UseFont = true;
            //
            // lciBtnObnovit
            //
            lciBtnObnovit.Control = btnObnovit;
            lciBtnObnovit.Location = new Point(0, 126);
            lciBtnObnovit.Name = "lciBtnObnovit";
            lciBtnObnovit.Size = new Size(170, 44);
            lciBtnObnovit.TextVisible = false;
            lciBtnObnovit.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 8, 6, 2);
            //
            // lciBtnHistory
            //
            lciBtnHistory.Control = btnHistory;
            lciBtnHistory.Location = new Point(170, 126);
            lciBtnHistory.Name = "lciBtnHistory";
            lciBtnHistory.Size = new Size(190, 44);
            lciBtnHistory.TextVisible = false;
            lciBtnHistory.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 6, 2);
            //
            // emptySpace1
            //
            emptySpace1.AllowHotTrack = false;
            emptySpace1.Location = new Point(360, 126);
            emptySpace1.Name = "emptySpace1";
            emptySpace1.Size = new Size(300, 44);
            //
            // lciColorSearch
            //
            lciColorSearch.Control = txtColorSearch;
            lciColorSearch.Location = new Point(0, 0);
            lciColorSearch.Name = "lciColorSearch";
            lciColorSearch.Size = new Size(300, 30);
            lciColorSearch.TextVisible = false;
            //
            // lciGridColor
            //
            lciGridColor.Control = gridColorResult;
            lciGridColor.Location = new Point(0, 30);
            lciGridColor.Name = "lciGridColor";
            lciGridColor.TextVisible = false;
            //
            // lciGrid
            //
            lciGrid.Control = gridControl;
            lciGrid.Name = "lciGrid";
            lciGrid.TextVisible = false;
            //
            // YarnForm
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 700);
            Controls.Add(layoutControl);
            MinimumSize = new Size(800, 500);
            Name = "YarnForm";
            Text = "Пряжа";
            Load += YarnForm_Load;
            ((ISupportInitialize)txtNakl.Properties).EndInit();
            ((ISupportInitialize)txtArticul.Properties).EndInit();
            ((ISupportInitialize)txtSebUpr.Properties).EndInit();
            ((ISupportInitialize)txtSebDok.Properties).EndInit();
            ((ISupportInitialize)txtColorSearch.Properties).EndInit();
            ((ISupportInitialize)gridControl).EndInit();
            ((ISupportInitialize)bindingSource).EndInit();
            ((ISupportInitialize)gridView).EndInit();
            ((ISupportInitialize)gridColorResult).EndInit();
            ((ISupportInitialize)bsColorResult).EndInit();
            ((ISupportInitialize)gridViewColor).EndInit();
            ((ISupportInitialize)layoutControl).EndInit();
            layoutControl.ResumeLayout(false);
            ((ISupportInitialize)rootGroup).EndInit();
            ((ISupportInitialize)grpInputs).EndInit();
            ((ISupportInitialize)grpGrid).EndInit();
            ((ISupportInitialize)grpColorSearch).EndInit();
            ((ISupportInitialize)lciNakl).EndInit();
            ((ISupportInitialize)lciArticul).EndInit();
            ((ISupportInitialize)lciSebUpr).EndInit();
            ((ISupportInitialize)lciSebDok).EndInit();
            ((ISupportInitialize)lciBtnCalc).EndInit();
            ((ISupportInitialize)lciBtnObnovit).EndInit();
            ((ISupportInitialize)lciBtnHistory).EndInit();
            ((ISupportInitialize)lciGrid).EndInit();
            ((ISupportInitialize)lciColorSearch).EndInit();
            ((ISupportInitialize)lciGridColor).EndInit();
            ((ISupportInitialize)emptySpace1).EndInit();
            ((ISupportInitialize)splitterH).EndInit();
            ((ISupportInitialize)splitterV).EndInit();
            ResumeLayout(false);
        }
    }
}
