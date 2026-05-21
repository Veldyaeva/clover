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

namespace SewingProduction.Features.TeamWork.Forms
{
    internal partial class ThreadNormsForm
    {
        private IContainer components = null;

        private BindingSource bindingSource;
        private CustomGridControl gridControl;
        private GridView gridView;
        private CustomRadioGroup filterGroup;
        private CustomSimpleButton addButton;
        private CustomSimpleButton copyButton;
        private CustomSimpleButton deleteButton;
        private CustomSimpleButton saveButton;
        private CustomSimpleButton closeButton;
        private PanelControl topPanel;
        private PanelControl bottomPanel;
        private FlowLayoutPanel buttonsPanel;
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
            bindingSource = new BindingSource(components);
            gridControl = new CustomGridControl();
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
            normEditor = new RepositoryItemSpinEdit();
            colDateChange = new GridColumn();
            colApproved = new GridColumn();
            approvedCheck = new RepositoryItemCheckEdit();
            colId = new GridColumn();
            colTcId = new GridColumn();
            colTgId = new GridColumn();
            colThreadDisplay = new GridColumn();
            filterGroup = new CustomRadioGroup();
            addButton = new CustomSimpleButton();
            ThreadNormsFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            copyButton = new CustomSimpleButton();
            deleteButton = new CustomSimpleButton();
            saveButton = new CustomSimpleButton();
            closeButton = new CustomSimpleButton();
            customSimpleButton1 = new CustomSimpleButton();
            bottomPanel = new PanelControl();
            buttonsPanel = new FlowLayoutPanel();
            topPanel = new PanelControl();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            gridControlitem = new DevExpress.XtraLayout.LayoutControlItem();
            bottomPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            topPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(components);
            ((ISupportInitialize)bindingSource).BeginInit();
            ((ISupportInitialize)gridControl).BeginInit();
            ((ISupportInitialize)gridView).BeginInit();
            ((ISupportInitialize)assortLookup).BeginInit();
            ((ISupportInitialize)normEditor).BeginInit();
            ((ISupportInitialize)approvedCheck).BeginInit();
            ((ISupportInitialize)filterGroup.Properties).BeginInit();
            ((ISupportInitialize)ThreadNormsFormlayoutControl1ConvertedLayout).BeginInit();
            ThreadNormsFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((ISupportInitialize)bottomPanel).BeginInit();
            bottomPanel.SuspendLayout();
            ((ISupportInitialize)topPanel).BeginInit();
            topPanel.SuspendLayout();
            ((ISupportInitialize)layoutControlGroup1).BeginInit();
            ((ISupportInitialize)gridControlitem).BeginInit();
            ((ISupportInitialize)bottomPanelitem).BeginInit();
            ((ISupportInitialize)topPanelitem).BeginInit();
            ((ISupportInitialize)layoutControlItem1).BeginInit();
            ((ISupportInitialize)emptySpaceItem1).BeginInit();
            ((ISupportInitialize)emptySpaceItem2).BeginInit();
            ((ISupportInitialize)layoutControlItem2).BeginInit();
            ((ISupportInitialize)layoutControlItem3).BeginInit();
            ((ISupportInitialize)layoutControlItem4).BeginInit();
            ((ISupportInitialize)layoutControlItem5).BeginInit();
            ((ISupportInitialize)layoutControlItem6).BeginInit();
            SuspendLayout();
            // 
            // gridControl
            // 
            gridControl.DataSource = bindingSource;
            gridControl.Font = new Font("Arial", 10F);
            gridControl.Location = new Point(12, 90);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl";
            gridControl.RepositoryItems.AddRange(new RepositoryItem[] { assortLookup, approvedCheck, normEditor });
            gridControl.Size = new Size(1240, 633);
            gridControl.TabIndex = 3;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            // 
            // gridView
            // 
            gridView.Appearance.FocusedCell.ForeColor = Color.Black;
            gridView.Appearance.FocusedCell.Options.UseForeColor = true;
            gridView.Appearance.FocusedRow.ForeColor = Color.Black;
            gridView.Appearance.FocusedRow.Options.UseForeColor = true;
            gridView.Columns.AddRange(new GridColumn[] { colCategory, colCategoryName, colGroupName, colClassName, colTatName, colAssort, colThreadCode, gridColumn1, colNorm, colDateChange, colApproved, colId, colTcId, colTgId, colThreadDisplay });
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridView.OptionsNavigation.AutoFocusNewRow = true;
            gridView.OptionsView.ShowAutoFilterRow = true;
            gridView.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.RowCellStyle += GridView_RowCellStyle;
            gridView.ShowingEditor += GridView_ShowingEditor;
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
            gridView.CellValueChanged += GridView_CellValueChanged;
            gridView.ValidateRow += GridView_ValidateRow;
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
            colNorm.ColumnEdit = normEditor;
            colNorm.FieldName = "norm";
            colNorm.Name = "colNorm";
            colNorm.Visible = true;
            colNorm.VisibleIndex = 5;
            colNorm.Width = 80;
            // 
            // normEditor
            // 
            normEditor.AutoHeight = false;
            normEditor.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            normEditor.Mask.EditMask = "0.###";
            normEditor.MaxValue = new decimal(new int[] { 999, 0, 0, 0 });
            normEditor.Name = "normEditor";
            // 
            // colDateChange
            // 
            colDateChange.Caption = "Дата";
            colDateChange.DisplayFormat.FormatString = "dd.MM.yyyy";
            colDateChange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colDateChange.FieldName = "date_change";
            colDateChange.Name = "colDateChange";
            colDateChange.Visible = true;
            colDateChange.VisibleIndex = 6;
            colDateChange.Width = 101;
            // 
            // colApproved
            // 
            colApproved.Caption = "Утверждено";
            colApproved.ColumnEdit = approvedCheck;
            colApproved.FieldName = "approved";
            colApproved.Name = "colApproved";
            colApproved.Visible = true;
            colApproved.VisibleIndex = 7;
            colApproved.Width = 80;
            // 
            // approvedCheck
            // 
            approvedCheck.AutoHeight = false;
            approvedCheck.Name = "approvedCheck";
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
            // filterGroup
            // 
            filterGroup.Dock = DockStyle.Left;
            filterGroup.EditValue = 0;
            filterGroup.Location = new Point(2, 2);
            filterGroup.Name = "filterGroup";
            filterGroup.ObjectName = null;
            filterGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(0, "0 норма"), new RadioGroupItem(1, "Все") });
            filterGroup.Size = new Size(180, 44);
            filterGroup.TabIndex = 0;
            filterGroup.SelectedIndexChanged += FilterGroup_SelectedIndexChanged;
            // 
            // addButton
            // 
            addButton.Location = new Point(633, 727);
            addButton.Margin = new Padding(8, 6, 0, 6);
            addButton.Name = "addButton";
            addButton.Size = new Size(57, 22);
            addButton.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            addButton.TabIndex = 0;
            addButton.Text = "Добавить";
            addButton.Visible = false;
            addButton.Click += AddButton_Click;
            // 
            // ThreadNormsFormlayoutControl1ConvertedLayout
            // 
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(addButton);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(copyButton);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(deleteButton);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(saveButton);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(closeButton);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(customSimpleButton1);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(gridControl);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(bottomPanel);
            ThreadNormsFormlayoutControl1ConvertedLayout.Controls.Add(topPanel);
            ThreadNormsFormlayoutControl1ConvertedLayout.Dock = DockStyle.Fill;
            ThreadNormsFormlayoutControl1ConvertedLayout.Location = new Point(0, 0);
            ThreadNormsFormlayoutControl1ConvertedLayout.Name = "ThreadNormsFormlayoutControl1ConvertedLayout";
            ThreadNormsFormlayoutControl1ConvertedLayout.Root = layoutControlGroup1;
            ThreadNormsFormlayoutControl1ConvertedLayout.Size = new Size(1264, 761);
            ThreadNormsFormlayoutControl1ConvertedLayout.TabIndex = 3;
            // 
            // copyButton
            // 
            copyButton.Location = new Point(694, 727);
            copyButton.Margin = new Padding(8, 6, 0, 6);
            copyButton.Name = "copyButton";
            copyButton.Size = new Size(93, 22);
            copyButton.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            copyButton.TabIndex = 4;
            copyButton.Text = "Добавить копию";
            copyButton.Click += CopyButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(791, 727);
            deleteButton.Margin = new Padding(8, 6, 0, 6);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(51, 22);
            deleteButton.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            deleteButton.TabIndex = 5;
            deleteButton.Text = "Удалить";
            deleteButton.Visible = false;
            deleteButton.Click += DeleteButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(846, 727);
            saveButton.Margin = new Padding(8, 6, 0, 6);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(62, 22);
            saveButton.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            saveButton.TabIndex = 6;
            saveButton.Text = "Сохранить";
            saveButton.Click += SaveButton_Click;
            // 
            // closeButton
            // 
            closeButton.Location = new Point(912, 727);
            closeButton.Margin = new Padding(8, 6, 0, 6);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(51, 22);
            closeButton.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            closeButton.TabIndex = 7;
            closeButton.Text = "Закрыть";
            closeButton.Click += CloseButton_Click;
            // 
            // customSimpleButton1
            // 
            customSimpleButton1.Appearance.Font = new Font("Arial", 10F);
            customSimpleButton1.Appearance.Options.UseFont = true;
            customSimpleButton1.Location = new Point(12, 64);
            customSimpleButton1.Name = "customSimpleButton1";
            customSimpleButton1.Size = new Size(181, 22);
            customSimpleButton1.StyleController = ThreadNormsFormlayoutControl1ConvertedLayout;
            customSimpleButton1.TabIndex = 2;
            customSimpleButton1.Text = "Обновить";
            customSimpleButton1.Click += customSimpleButton1_Click;
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(buttonsPanel);
            bottomPanel.Location = new Point(12, 727);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(617, 22);
            bottomPanel.TabIndex = 1;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Dock = DockStyle.Fill;
            buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonsPanel.Location = new Point(2, 2);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new Padding(8);
            buttonsPanel.Size = new Size(613, 18);
            buttonsPanel.TabIndex = 0;
            buttonsPanel.WrapContents = false;
            // 
            // topPanel
            // 
            topPanel.Controls.Add(filterGroup);
            topPanel.Location = new Point(12, 12);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(181, 48);
            topPanel.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { gridControlitem, bottomPanelitem, topPanelitem, layoutControlItem1, emptySpaceItem1, emptySpaceItem2, layoutControlItem2, layoutControlItem3, layoutControlItem4, layoutControlItem5, layoutControlItem6 });
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new Size(1264, 761);
            layoutControlGroup1.TextVisible = false;
            // 
            // gridControlitem
            // 
            gridControlitem.Control = gridControl;
            gridControlitem.Location = new Point(0, 78);
            gridControlitem.Name = "gridControlitem";
            gridControlitem.Size = new Size(1244, 637);
            gridControlitem.TextVisible = false;
            // 
            // bottomPanelitem
            // 
            bottomPanelitem.Control = bottomPanel;
            bottomPanelitem.Location = new Point(0, 715);
            bottomPanelitem.Name = "bottomPanelitem";
            bottomPanelitem.Size = new Size(621, 26);
            bottomPanelitem.TextVisible = false;
            // 
            // topPanelitem
            // 
            topPanelitem.Control = topPanel;
            topPanelitem.Location = new Point(0, 0);
            topPanelitem.Name = "topPanelitem";
            topPanelitem.Size = new Size(185, 52);
            topPanelitem.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customSimpleButton1;
            layoutControlItem1.Location = new Point(0, 52);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(185, 26);
            layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new Point(185, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(1059, 78);
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.Location = new Point(955, 715);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new Size(289, 26);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = closeButton;
            layoutControlItem2.Location = new Point(900, 715);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(55, 26);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = saveButton;
            layoutControlItem3.Location = new Point(834, 715);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(66, 26);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = deleteButton;
            layoutControlItem4.Location = new Point(779, 715);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(55, 26);
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = copyButton;
            layoutControlItem5.Location = new Point(682, 715);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(97, 26);
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = addButton;
            layoutControlItem6.Location = new Point(621, 715);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new Size(61, 26);
            layoutControlItem6.TextVisible = false;
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
            ((ISupportInitialize)bindingSource).EndInit();
            ((ISupportInitialize)gridControl).EndInit();
            ((ISupportInitialize)gridView).EndInit();
            ((ISupportInitialize)assortLookup).EndInit();
            ((ISupportInitialize)normEditor).EndInit();
            ((ISupportInitialize)approvedCheck).EndInit();
            ((ISupportInitialize)filterGroup.Properties).EndInit();
            ((ISupportInitialize)ThreadNormsFormlayoutControl1ConvertedLayout).EndInit();
            ThreadNormsFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((ISupportInitialize)bottomPanel).EndInit();
            bottomPanel.ResumeLayout(false);
            ((ISupportInitialize)topPanel).EndInit();
            topPanel.ResumeLayout(false);
            ((ISupportInitialize)layoutControlGroup1).EndInit();
            ((ISupportInitialize)gridControlitem).EndInit();
            ((ISupportInitialize)bottomPanelitem).EndInit();
            ((ISupportInitialize)topPanelitem).EndInit();
            ((ISupportInitialize)layoutControlItem1).EndInit();
            ((ISupportInitialize)emptySpaceItem1).EndInit();
            ((ISupportInitialize)emptySpaceItem2).EndInit();
            ((ISupportInitialize)layoutControlItem2).EndInit();
            ((ISupportInitialize)layoutControlItem3).EndInit();
            ((ISupportInitialize)layoutControlItem4).EndInit();
            ((ISupportInitialize)layoutControlItem5).EndInit();
            ((ISupportInitialize)layoutControlItem6).EndInit();
            ResumeLayout(false);
        }
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraLayout.LayoutControl ThreadNormsFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem gridControlitem;
        private DevExpress.XtraLayout.LayoutControlItem bottomPanelitem;
        private DevExpress.XtraLayout.LayoutControlItem topPanelitem;
        private GridColumn gridColumn1;
        private CustomSimpleButton customSimpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
