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
        private RepositoryItemLookUpEdit managerLookup;
        private RepositoryItemLookUpEdit categoryLookup;
        private RepositoryItemLookUpEdit assortLookup;
        private RepositoryItemLookUpEdit threadLookup;
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
            categoryLookup = new RepositoryItemLookUpEdit();
            colGroupName = new GridColumn();
            colClassName = new GridColumn();
            colAssort = new GridColumn();
            assortLookup = new RepositoryItemLookUpEdit();
            colThreadCode = new GridColumn();
            colNorm = new GridColumn();
            normEditor = new RepositoryItemSpinEdit();
            colApproved = new GridColumn();
            approvedCheck = new RepositoryItemCheckEdit();
            colDateChange = new GridColumn();
            colId = new GridColumn();
            colTcId = new GridColumn();
            colTgId = new GridColumn();
            colCategoryName = new GridColumn();
            colTatName = new GridColumn();
            colThreadDisplay = new GridColumn();
            managerLookup = new RepositoryItemLookUpEdit();
            threadLookup = new RepositoryItemLookUpEdit();
            managerSearchLookUp = new RepositoryItemSearchLookUpEdit();
            repositoryItemSearchLookUpEdit1View = new GridView();
            threadSearchLookUp = new RepositoryItemSearchLookUpEdit();
            gridView1 = new GridView();
            filterGroup = new CustomRadioGroup();
            addButton = new CustomSimpleButton();
            copyButton = new CustomSimpleButton();
            deleteButton = new CustomSimpleButton();
            saveButton = new CustomSimpleButton();
            closeButton = new CustomSimpleButton();
            topPanel = new PanelControl();
            bottomPanel = new PanelControl();
            buttonsPanel = new FlowLayoutPanel();
            layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(components);
            ThreadNormsFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            gridControlitem = new DevExpress.XtraLayout.LayoutControlItem();
            bottomPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            topPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)bindingSource).BeginInit();
            ((ISupportInitialize)gridControl).BeginInit();
            ((ISupportInitialize)gridView).BeginInit();
            ((ISupportInitialize)categoryLookup).BeginInit();
            ((ISupportInitialize)assortLookup).BeginInit();
            ((ISupportInitialize)normEditor).BeginInit();
            ((ISupportInitialize)approvedCheck).BeginInit();
            ((ISupportInitialize)managerLookup).BeginInit();
            ((ISupportInitialize)threadLookup).BeginInit();
            ((ISupportInitialize)managerSearchLookUp).BeginInit();
            ((ISupportInitialize)repositoryItemSearchLookUpEdit1View).BeginInit();
            ((ISupportInitialize)threadSearchLookUp).BeginInit();
            ((ISupportInitialize)gridView1).BeginInit();
            ((ISupportInitialize)filterGroup.Properties).BeginInit();
            ((ISupportInitialize)topPanel).BeginInit();
            topPanel.SuspendLayout();
            ((ISupportInitialize)bottomPanel).BeginInit();
            bottomPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            ((ISupportInitialize)ThreadNormsFormlayoutControl1ConvertedLayout).BeginInit();
            ThreadNormsFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((ISupportInitialize)layoutControlGroup1).BeginInit();
            ((ISupportInitialize)gridControlitem).BeginInit();
            ((ISupportInitialize)bottomPanelitem).BeginInit();
            ((ISupportInitialize)topPanelitem).BeginInit();
            SuspendLayout();
            // 
            // gridControl
            // 
            gridControl.DataSource = bindingSource;
            gridControl.Font = new Font("Arial", 10F);
            gridControl.Location = new Point(12, 65);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl";
            gridControl.RepositoryItems.AddRange(new RepositoryItem[] { managerLookup, categoryLookup, assortLookup, threadLookup, approvedCheck, normEditor, managerSearchLookUp, threadSearchLookUp });
            gridControl.Size = new Size(1240, 628);
            gridControl.TabIndex = 0;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            // 
            // gridView
            // 
            gridView.Appearance.FocusedCell.ForeColor = Color.Black;
            gridView.Appearance.FocusedCell.Options.UseForeColor = true;
            gridView.Appearance.FocusedRow.ForeColor = Color.Black;
            gridView.Appearance.FocusedRow.Options.UseForeColor = true;
            gridView.Columns.AddRange(new GridColumn[] { colCategory, colGroupName, colClassName, colAssort, colThreadCode, colNorm, colApproved, colDateChange, colId, colTcId, colTgId, colCategoryName, colTatName, colThreadDisplay });
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsNavigation.AutoFocusNewRow = true;
            gridView.OptionsView.ShowAutoFilterRow = true;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.RowCellStyle += GridView_RowCellStyle;
            gridView.CellValueChanged += GridView_CellValueChanged;
            gridView.ValidateRow += GridView_ValidateRow;
            // 
            // colCategory
            // 
            colCategory.Caption = "Категория";
            colCategory.ColumnEdit = categoryLookup;
            colCategory.FieldName = "tg_id_n";
            colCategory.Name = "colCategory";
            colCategory.OptionsColumn.AllowEdit = false;
            colCategory.OptionsColumn.ReadOnly = true;
            colCategory.Visible = true;
            colCategory.VisibleIndex = 0;
            colCategory.Width = 208;
            // 
            // categoryLookup
            // 
            categoryLookup.AutoHeight = false;
            categoryLookup.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            categoryLookup.DisplayMember = "TCAT_CategoryName";
            categoryLookup.Name = "categoryLookup";
            categoryLookup.NullText = "[Выберите категорию]";
            categoryLookup.PopupFilterMode = PopupFilterMode.Contains;
            categoryLookup.SearchMode = SearchMode.AutoSearch;
            categoryLookup.ShowDropDown = ShowDropDown.Never;
            categoryLookup.ShowFooter = false;
            categoryLookup.ShowHeader = false;
            categoryLookup.ValueMember = "TCAT_ID";
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
            colThreadCode.Visible = true;
            colThreadCode.VisibleIndex = 4;
            colThreadCode.Width = 196;
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
            colDateChange.OptionsColumn.AllowEdit = false;
            colDateChange.OptionsColumn.ReadOnly = true;
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
            // colCategoryName
            // 
            colCategoryName.FieldName = "TCAT_CategoryName";
            colCategoryName.Name = "colCategoryName";
            // 
            // colTatName
            // 
            colTatName.FieldName = "TAT_Name";
            colTatName.Name = "colTatName";
            // 
            // colThreadDisplay
            // 
            colThreadDisplay.FieldName = "ThreadDisplay";
            colThreadDisplay.Name = "colThreadDisplay";
            colThreadDisplay.Width = 114;
            // 
            // managerLookup
            // 
            managerLookup.AutoHeight = false;
            managerLookup.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            managerLookup.DisplayMember = "Name";
            managerLookup.Name = "managerLookup";
            managerLookup.NullText = "[Выберите менеджера]";
            managerLookup.PopupFilterMode = PopupFilterMode.Contains;
            managerLookup.SearchMode = SearchMode.AutoSearch;
            managerLookup.ShowFooter = false;
            managerLookup.ShowHeader = false;
            managerLookup.ValueMember = "Men";
            // 
            // threadLookup
            // 
            threadLookup.AutoHeight = false;
            threadLookup.BestFitMode = BestFitMode.BestFitResizePopup;
            threadLookup.BestFitRowCount = 8;
            threadLookup.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            threadLookup.DisplayMember = "displayText";
            threadLookup.Name = "threadLookup";
            threadLookup.NullText = "[Выберите нитки]";
            threadLookup.PopupFilterMode = PopupFilterMode.Contains;
            threadLookup.SearchMode = SearchMode.AutoSearch;
            threadLookup.ValueMember = "kod_dr";
            // 
            // managerSearchLookUp
            // 
            managerSearchLookUp.AutoHeight = false;
            managerSearchLookUp.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            managerSearchLookUp.DisplayMember = "Name";
            managerSearchLookUp.Name = "managerSearchLookUp";
            managerSearchLookUp.NullText = "[Выберите менеджера]";
            managerSearchLookUp.PopupView = repositoryItemSearchLookUpEdit1View;
            managerSearchLookUp.ValueMember = "Men";
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            repositoryItemSearchLookUpEdit1View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // threadSearchLookUp
            // 
            threadSearchLookUp.AppearanceFocused.ForeColor = Color.Black;
            threadSearchLookUp.AppearanceFocused.Options.UseForeColor = true;
            threadSearchLookUp.AutoHeight = false;
            threadSearchLookUp.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            threadSearchLookUp.DisplayMember = "displayText";
            threadSearchLookUp.Name = "threadSearchLookUp";
            threadSearchLookUp.NullText = "[Выберите нитки]";
            threadSearchLookUp.PopupView = gridView1;
            threadSearchLookUp.ShowDropDown = ShowDropDown.DoubleClick;
            threadSearchLookUp.ValueMember = "kod_dr";
            // 
            // gridView1
            // 
            gridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            gridView1.Name = "gridView1";
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // filterGroup
            // 
            filterGroup.Dock = DockStyle.Left;
            filterGroup.EditValue = 0;
            filterGroup.Location = new Point(2, 2);
            filterGroup.Name = "filterGroup";
            filterGroup.ObjectName = null;
            filterGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(0, "0 норма"), new RadioGroupItem(1, "Все") });
            filterGroup.Size = new Size(180, 45);
            filterGroup.TabIndex = 0;
            filterGroup.SelectedIndexChanged += FilterGroup_SelectedIndexChanged;
            // 
            // addButton
            // 
            addButton.AutoSize = true;
            addButton.Location = new Point(874, 14);
            addButton.Margin = new Padding(8, 6, 0, 6);
            addButton.Name = "addButton";
            addButton.Size = new Size(57, 22);
            addButton.TabIndex = 0;
            addButton.Text = "Добавить";
            addButton.Click += AddButton_Click;
            // 
            // copyButton
            // 
            copyButton.AutoSize = true;
            copyButton.Location = new Point(939, 14);
            copyButton.Margin = new Padding(8, 6, 0, 6);
            copyButton.Name = "copyButton";
            copyButton.Size = new Size(93, 22);
            copyButton.TabIndex = 1;
            copyButton.Text = "Добавить копию";
            copyButton.Click += CopyButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.AutoSize = true;
            deleteButton.Location = new Point(1040, 14);
            deleteButton.Margin = new Padding(8, 6, 0, 6);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(51, 22);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Удалить";
            deleteButton.Click += DeleteButton_Click;
            // 
            // saveButton
            // 
            saveButton.AutoSize = true;
            saveButton.Location = new Point(1099, 14);
            saveButton.Margin = new Padding(8, 6, 0, 6);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(62, 22);
            saveButton.TabIndex = 3;
            saveButton.Text = "Сохранить";
            saveButton.Click += SaveButton_Click;
            // 
            // closeButton
            // 
            closeButton.AutoSize = true;
            closeButton.Location = new Point(1169, 14);
            closeButton.Margin = new Padding(8, 6, 0, 6);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(51, 22);
            closeButton.TabIndex = 4;
            closeButton.Text = "Закрыть";
            closeButton.Click += CloseButton_Click;
            // 
            // topPanel
            // 
            topPanel.Controls.Add(filterGroup);
            topPanel.Location = new Point(12, 12);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1240, 49);
            topPanel.TabIndex = 1;
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(buttonsPanel);
            bottomPanel.Location = new Point(12, 697);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(1240, 52);
            bottomPanel.TabIndex = 2;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Controls.Add(closeButton);
            buttonsPanel.Controls.Add(saveButton);
            buttonsPanel.Controls.Add(deleteButton);
            buttonsPanel.Controls.Add(copyButton);
            buttonsPanel.Controls.Add(addButton);
            buttonsPanel.Dock = DockStyle.Fill;
            buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonsPanel.Location = new Point(2, 2);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new Padding(8);
            buttonsPanel.Size = new Size(1236, 48);
            buttonsPanel.TabIndex = 0;
            buttonsPanel.WrapContents = false;
            // 
            // ThreadNormsFormlayoutControl1ConvertedLayout
            // 
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
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { gridControlitem, bottomPanelitem, topPanelitem });
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new Size(1264, 761);
            layoutControlGroup1.TextVisible = false;
            // 
            // gridControlitem
            // 
            gridControlitem.Control = gridControl;
            gridControlitem.Location = new Point(0, 53);
            gridControlitem.Name = "gridControlitem";
            gridControlitem.Size = new Size(1244, 632);
            gridControlitem.TextVisible = false;
            // 
            // bottomPanelitem
            // 
            bottomPanelitem.Control = bottomPanel;
            bottomPanelitem.Location = new Point(0, 685);
            bottomPanelitem.Name = "bottomPanelitem";
            bottomPanelitem.Size = new Size(1244, 56);
            bottomPanelitem.TextVisible = false;
            // 
            // topPanelitem
            // 
            topPanelitem.Control = topPanel;
            topPanelitem.Location = new Point(0, 0);
            topPanelitem.Name = "topPanelitem";
            topPanelitem.Size = new Size(1244, 53);
            topPanelitem.TextVisible = false;
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
            ((ISupportInitialize)categoryLookup).EndInit();
            ((ISupportInitialize)assortLookup).EndInit();
            ((ISupportInitialize)normEditor).EndInit();
            ((ISupportInitialize)approvedCheck).EndInit();
            ((ISupportInitialize)managerLookup).EndInit();
            ((ISupportInitialize)threadLookup).EndInit();
            ((ISupportInitialize)managerSearchLookUp).EndInit();
            ((ISupportInitialize)repositoryItemSearchLookUpEdit1View).EndInit();
            ((ISupportInitialize)threadSearchLookUp).EndInit();
            ((ISupportInitialize)gridView1).EndInit();
            ((ISupportInitialize)filterGroup.Properties).EndInit();
            ((ISupportInitialize)topPanel).EndInit();
            topPanel.ResumeLayout(false);
            ((ISupportInitialize)bottomPanel).EndInit();
            bottomPanel.ResumeLayout(false);
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ((ISupportInitialize)ThreadNormsFormlayoutControl1ConvertedLayout).EndInit();
            ThreadNormsFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((ISupportInitialize)layoutControlGroup1).EndInit();
            ((ISupportInitialize)gridControlitem).EndInit();
            ((ISupportInitialize)bottomPanelitem).EndInit();
            ((ISupportInitialize)topPanelitem).EndInit();
            ResumeLayout(false);
        }
        private RepositoryItemSearchLookUpEdit managerSearchLookUp;
        private GridView repositoryItemSearchLookUpEdit1View;
        private RepositoryItemSearchLookUpEdit threadSearchLookUp;
        private GridView gridView1;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraLayout.LayoutControl ThreadNormsFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem gridControlitem;
        private DevExpress.XtraLayout.LayoutControlItem bottomPanelitem;
        private DevExpress.XtraLayout.LayoutControlItem topPanelitem;
    }
}
