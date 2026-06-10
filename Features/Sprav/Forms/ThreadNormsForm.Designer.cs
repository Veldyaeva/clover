using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Class;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SewingProduction.Features.Sprav.Forms
{
    internal partial class ThreadNormsForm
    {
        private IContainer components = null;
        private CustomGridControl gridControl;
        private GridView gridView;
        private CustomSimpleButton addButton;
        private CustomSimpleButton copyButton;
        private CustomSimpleButton deleteButton;
        private RepositoryItemLookUpEdit assortLookup;
        private RepositoryItemCheckEdit approvedCheck;
        private RepositoryItemSpinEdit normEditor;
        private GridColumn colClassName;
        private GridColumn colGroupName;
        private GridColumn colCategory;
        private GridColumn colAssort;
        private GridColumn colThreadCode;
        private GridColumn colNorm;
        private GridColumn colApproved;
        private GridColumn colDateChange;
        private GridColumn colId;
        private GridColumn colTcId;
        private GridColumn colTgId;
        private GridColumn colCategoryName;
        private GridColumn colTatName;
        private GridColumn colThreadDisplay;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions3 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions4 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions5 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ThreadNormsForm));
            gridControl = new CustomGridControl();
            bindingSource = new BindingSource(components);
            gridView = new GridView();
            colCategory = new GridColumn();
            colCategoryName = new GridColumn();
            colGroupName = new GridColumn();
            colClassName = new GridColumn();
            colTatName = new GridColumn();
            colAssort = new GridColumn();
            assortLookup = new RepositoryItemLookUpEdit();
            colThreadCode = new GridColumn();
            gridColumn1 = new GridColumn();
            colNorm = new GridColumn();
            colApproved = new GridColumn();
            approvedCheck = new RepositoryItemCheckEdit();
            colDateChange = new GridColumn();
            colId = new GridColumn();
            colTcId = new GridColumn();
            colTgId = new GridColumn();
            colThreadDisplay = new GridColumn();
            normEditor = new RepositoryItemSpinEdit();
            addButton = new CustomSimpleButton();
            ThreadNormsFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            copyButton = new CustomSimpleButton();
            deleteButton = new CustomSimpleButton();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            gridControlitem = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)gridControl).BeginInit();
            ((ISupportInitialize)bindingSource).BeginInit();
            ((ISupportInitialize)gridView).BeginInit();
            ((ISupportInitialize)assortLookup).BeginInit();
            ((ISupportInitialize)approvedCheck).BeginInit();
            ((ISupportInitialize)normEditor).BeginInit();
            ((ISupportInitialize)ThreadNormsFormlayoutControl1ConvertedLayout).BeginInit();
            ThreadNormsFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((ISupportInitialize)layoutControlItem6).BeginInit();
            ((ISupportInitialize)layoutControlGroup1).BeginInit();
            ((ISupportInitialize)emptySpaceItem2).BeginInit();
            ((ISupportInitialize)layoutControlItem4).BeginInit();
            ((ISupportInitialize)layoutControlItem5).BeginInit();
            ((ISupportInitialize)layoutControlGroup2).BeginInit();
            ((ISupportInitialize)gridControlitem).BeginInit();
            SuspendLayout();
            // 
            // gridControl
            // 
            gridControl.DataSource = bindingSource;
            gridControl.Font = new Font("Arial", 10F);
            gridControl.Location = new Point(5, 26);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl";
            gridControl.RepositoryItems.AddRange(new RepositoryItem[] { assortLookup, approvedCheck, normEditor });
            gridControl.Size = new Size(1254, 704);
            gridControl.TabIndex = 3;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            // 
            // gridView
            // 
            gridView.Appearance.FocusedCell.ForeColor = Color.Black;
            gridView.Appearance.FocusedCell.Options.UseForeColor = true;
            gridView.Appearance.FocusedRow.ForeColor = Color.Black;
            gridView.Appearance.FocusedRow.Options.UseForeColor = true;
            gridView.Columns.AddRange(new GridColumn[] { colCategory, colCategoryName, colGroupName, colClassName, colTatName, colAssort, colThreadCode, gridColumn1, colNorm, colApproved, colDateChange, colId, colTcId, colTgId, colThreadDisplay });
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            gridView.OptionsNavigation.AutoFocusNewRow = true;
            gridView.OptionsView.ShowAutoFilterRow = true;
            gridView.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.RowCellStyle += GridView_RowCellStyle;
            gridView.ShowingEditor += GridView_ShowingEditor;
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
            gridView.CellValueChanged += GridView_CellValueChanged;
            gridView.ValidateRow += GridView_ValidateRow;
            gridView.ValidatingEditor += GridView_ValidatingEditor;
            // 
            // colCategory
            // 
            colCategory.Caption = "Категория";
            colCategory.FieldName = "tg_id_n";
            colCategory.Name = "colCategory";
            colCategory.OptionsColumn.AllowEdit = false;
            colCategory.OptionsColumn.ReadOnly = true;
            colCategory.Width = 208;
            // 
            // colCategoryName
            // 
            colCategoryName.Caption = "Категория";
            colCategoryName.FieldName = "TCAT_CategoryName";
            colCategoryName.Name = "colCategoryName";
            colCategoryName.OptionsColumn.AllowEdit = false;
            colCategoryName.OptionsColumn.ReadOnly = true;
            colCategoryName.Visible = true;
            colCategoryName.VisibleIndex = 0;
            colCategoryName.Width = 208;
            // 
            // colGroupName
            // 
            colGroupName.Caption = "Группа";
            colGroupName.FieldName = "TG_GroupName";
            colGroupName.Name = "colGroupName";
            colGroupName.OptionsColumn.AllowEdit = false;
            colGroupName.OptionsColumn.ReadOnly = true;
            colGroupName.Visible = true;
            colGroupName.VisibleIndex = 1;
            colGroupName.Width = 170;
            // 
            // colClassName
            // 
            colClassName.Caption = "Класс";
            colClassName.FieldName = "TC_ClassName";
            colClassName.Name = "colClassName";
            colClassName.OptionsColumn.AllowEdit = false;
            colClassName.OptionsColumn.ReadOnly = true;
            colClassName.Visible = true;
            colClassName.VisibleIndex = 2;
            colClassName.Width = 142;
            // 
            // colTatName
            // 
            colTatName.FieldName = "TAT_Name";
            colTatName.Name = "colTatName";
            // 
            // colAssort
            // 
            colAssort.Caption = "Ассортимент";
            colAssort.ColumnEdit = assortLookup;
            colAssort.FieldName = "ta_id";
            colAssort.Name = "colAssort";
            colAssort.Visible = true;
            colAssort.VisibleIndex = 3;
            colAssort.Width = 124;
            // 
            // assortLookup
            // 
            assortLookup.AutoHeight = false;
            assortLookup.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            assortLookup.DisplayMember = "TAT_Name";
            assortLookup.Name = "assortLookup";
            assortLookup.NullText = "[Выберите ассортимент]";
            assortLookup.PopupFilterMode = PopupFilterMode.Contains;
            assortLookup.SearchMode = SearchMode.AutoSearch;
            assortLookup.ShowFooter = false;
            assortLookup.ShowHeader = false;
            assortLookup.ValueMember = "TAT_ID";
            // 
            // colThreadCode
            // 
            colThreadCode.AppearanceCell.ForeColor = Color.Black;
            colThreadCode.AppearanceCell.Options.UseForeColor = true;
            colThreadCode.Caption = "Код ниток";
            colThreadCode.FieldName = "kod_dr";
            colThreadCode.Name = "colThreadCode";
            colThreadCode.OptionsColumn.AllowEdit = false;
            colThreadCode.OptionsColumn.ReadOnly = true;
            colThreadCode.Width = 196;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Артикул";
            gridColumn1.FieldName = "ThreadDisplay";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.OptionsColumn.AllowEdit = false;
            gridColumn1.OptionsColumn.ReadOnly = true;
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 4;
            gridColumn1.Width = 220;
            // 
            // colNorm
            // 
            colNorm.Caption = "Норма";
            colNorm.FieldName = "norm";
            colNorm.Name = "colNorm";
            colNorm.Visible = true;
            colNorm.VisibleIndex = 5;
            colNorm.Width = 80;
            // 
            // colApproved
            // 
            colApproved.Caption = "Утверждено";
            colApproved.ColumnEdit = approvedCheck;
            colApproved.FieldName = "approved";
            colApproved.Name = "colApproved";
            colApproved.Visible = true;
            colApproved.VisibleIndex = 6;
            colApproved.Width = 80;
            // 
            // approvedCheck
            // 
            approvedCheck.AutoHeight = false;
            approvedCheck.Name = "approvedCheck";
            // 
            // colDateChange
            // 
            colDateChange.Caption = "Дата";
            colDateChange.DisplayFormat.FormatString = "dd.MM.yyyy";
            colDateChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colDateChange.FieldName = "date_change";
            colDateChange.Name = "colDateChange";
            colDateChange.Visible = true;
            colDateChange.VisibleIndex = 7;
            colDateChange.Width = 101;
            // 
            // colId
            // 
            colId.FieldName = "id";
            colId.Name = "colId";
            // 
            // colTcId
            // 
            colTcId.FieldName = "TC_ID";
            colTcId.Name = "colTcId";
            // 
            // colTgId
            // 
            colTgId.FieldName = "TG_ID";
            colTgId.Name = "colTgId";
            // 
            // colThreadDisplay
            // 
            colThreadDisplay.Caption = "Нитки";
            colThreadDisplay.FieldName = "ThreadDisplay";
            colThreadDisplay.Name = "colThreadDisplay";
            colThreadDisplay.OptionsColumn.AllowEdit = false;
            colThreadDisplay.OptionsColumn.ReadOnly = true;
            colThreadDisplay.Width = 220;
            // 
            // normEditor
            // 
            normEditor.AutoHeight = false;
            normEditor.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            normEditor.Mask.EditMask = "0.###";
            normEditor.MaxValue = new decimal(new int[] { 999, 0, 0, 0 });
            normEditor.Name = "normEditor";
            // 
            // addButton
            // 
            addButton.Location = new Point(12, 727);
            addButton.Margin = new Padding(8, 6, 0, 6);
            addButton.Name = "addButton";
            addButton.Size = new Size(113, 22);
            addButton.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            addButton.TabIndex = 1;
            addButton.Text = "Добавить";
            addButton.Visible = false;
            addButton.Click += AddButton_Click;
            // 
            // ThreadNormsFormlayoutControl1ConvertedLayout
            // 
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(addButton);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(copyButton);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(deleteButton);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(gridControl);
            ThreadNormsFormlayoutControl1ConvertedLayout.Dock = DockStyle.Fill;
            ThreadNormsFormlayoutControl1ConvertedLayout.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem6 });
            ThreadNormsFormlayoutControl1ConvertedLayout.Location = new Point(0, 0);
            ThreadNormsFormlayoutControl1ConvertedLayout.Name = "ThreadNormsFormlayoutControl1ConvertedLayout";
            ThreadNormsFormlayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(1270, 474, 650, 400);
            ThreadNormsFormlayoutControl1ConvertedLayout.Root = layoutControlGroup1;
            ThreadNormsFormlayoutControl1ConvertedLayout.Size = new Size(1264, 761);
            ThreadNormsFormlayoutControl1ConvertedLayout.TabIndex = 3;
            // 
            // copyButton
            // 
            copyButton.Location = new Point(2, 737);
            copyButton.Margin = new Padding(8, 6, 0, 6);
            copyButton.Name = "copyButton";
            copyButton.Size = new Size(144, 22);
            copyButton.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            copyButton.TabIndex = 4;
            copyButton.Text = "Добавить копию";
            copyButton.Click += CopyButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(150, 737);
            deleteButton.Margin = new Padding(8, 6, 0, 6);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(146, 22);
            deleteButton.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            deleteButton.TabIndex = 5;
            deleteButton.Text = "Удалить";
            deleteButton.Visible = false;
            deleteButton.Click += DeleteButton_Click;
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = addButton;
            layoutControlItem6.Location = new Point(0, 715);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new Size(117, 26);
            layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { emptySpaceItem2, layoutControlItem4, layoutControlItem5, layoutControlGroup2 });
            layoutControlGroup1.Name = "Root";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup1.Size = new Size(1264, 761);
            layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.Location = new Point(298, 735);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new Size(966, 26);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = deleteButton;
            layoutControlItem4.Location = new Point(148, 735);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(150, 26);
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = copyButton;
            layoutControlItem5.Location = new Point(0, 735);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(148, 26);
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            buttonImageOptions5.Image = (Image)resources.GetObject("buttonImageOptions5.Image");
            layoutControlGroup2.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("в работе", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, true, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("   |   ", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Все", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, true, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("   |   ", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Обновить", true, buttonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { gridControlitem });
            layoutControlGroup2.Location = new Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup2.Size = new Size(1264, 735);
            layoutControlGroup2.Text = " ";
            layoutControlGroup2.CustomButtonClick += LayoutControlGroup2_CustomButtonClick;
            layoutControlGroup2.CustomButtonUnchecked += layoutControlGroup2_CustomButtonUnchecked;
            layoutControlGroup2.CustomButtonChecked += layoutControlGroup2_CustomButtonChecked;
            // 
            // gridControlitem
            // 
            gridControlitem.Control = gridControl;
            gridControlitem.Location = new Point(0, 0);
            gridControlitem.Name = "gridControlitem";
            gridControlitem.Size = new Size(1258, 708);
            gridControlitem.TextVisible = false;
            // 
            // ThreadNormsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 761);
            Controls.Add(ThreadNormsFormlayoutControl1ConvertedLayout);
            MinimumSize = new Size(1100, 700);
            Name = "ThreadNormsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Справочник норм ниток";
            FormClosing += ThreadNormsForm_FormClosing;
            Load += ThreadNormsForm_Load;
            ((ISupportInitialize)gridControl).EndInit();
            ((ISupportInitialize)bindingSource).EndInit();
            ((ISupportInitialize)gridView).EndInit();
            ((ISupportInitialize)assortLookup).EndInit();
            ((ISupportInitialize)approvedCheck).EndInit();
            ((ISupportInitialize)normEditor).EndInit();
            ((ISupportInitialize)ThreadNormsFormlayoutControl1ConvertedLayout).EndInit();
            ThreadNormsFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((ISupportInitialize)layoutControlItem6).EndInit();
            ((ISupportInitialize)layoutControlGroup1).EndInit();
            ((ISupportInitialize)emptySpaceItem2).EndInit();
            ((ISupportInitialize)layoutControlItem4).EndInit();
            ((ISupportInitialize)layoutControlItem5).EndInit();
            ((ISupportInitialize)layoutControlGroup2).EndInit();
            ((ISupportInitialize)gridControlitem).EndInit();
            ResumeLayout(false);
        }
        private DevExpress.XtraLayout.LayoutControl ThreadNormsFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem gridControlitem;
        private GridColumn gridColumn1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private BindingSource bindingSource;
    }
}
