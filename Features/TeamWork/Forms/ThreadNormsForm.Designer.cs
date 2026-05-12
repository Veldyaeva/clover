using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
        private GridControl gridControl;
        private GridView gridView;
        private RadioGroup filterGroup;
        private SimpleButton addButton;
        private SimpleButton copyButton;
        private SimpleButton deleteButton;
        private SimpleButton saveButton;
        private SimpleButton closeButton;
        private PanelControl topPanel;
        private PanelControl bottomPanel;
        private FlowLayoutPanel buttonsPanel;
        private RepositoryItemLookUpEdit managerLookup;
        private RepositoryItemLookUpEdit categoryLookup;
        private RepositoryItemLookUpEdit assortLookup;
        private RepositoryItemLookUpEdit threadLookup;
        private RepositoryItemCheckEdit approvedCheck;
        private RepositoryItemSpinEdit normEditor;
        private GridColumn colMen;
        private GridColumn colClassName;
        private GridColumn colGroupName;
        private GridColumn colCategory;
        private GridColumn colAssort;
        private GridColumn colThreadCode;
        private GridColumn colNorm;
        private GridColumn colApproved;
        private GridColumn colDateChange;
        private GridColumn colId;
        private GridColumn colKod3;
        private GridColumn colKodArt;
        private GridColumn colMenName;
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
            gridControl = new GridControl();
            gridView = new GridView();
            colMen = new GridColumn();
            colClassName = new GridColumn();
            colGroupName = new GridColumn();
            colCategory = new GridColumn();
            colAssort = new GridColumn();
            colThreadCode = new GridColumn();
            colNorm = new GridColumn();
            colApproved = new GridColumn();
            colDateChange = new GridColumn();
            colId = new GridColumn();
            colKod3 = new GridColumn();
            colKodArt = new GridColumn();
            colMenName = new GridColumn();
            colTcId = new GridColumn();
            colTgId = new GridColumn();
            colCategoryName = new GridColumn();
            colTatName = new GridColumn();
            colThreadDisplay = new GridColumn();
            managerLookup = new RepositoryItemLookUpEdit();
            categoryLookup = new RepositoryItemLookUpEdit();
            assortLookup = new RepositoryItemLookUpEdit();
            threadLookup = new RepositoryItemLookUpEdit();
            approvedCheck = new RepositoryItemCheckEdit();
            normEditor = new RepositoryItemSpinEdit();
            filterGroup = new RadioGroup();
            addButton = new SimpleButton();
            copyButton = new SimpleButton();
            deleteButton = new SimpleButton();
            saveButton = new SimpleButton();
            closeButton = new SimpleButton();
            topPanel = new PanelControl();
            bottomPanel = new PanelControl();
            buttonsPanel = new FlowLayoutPanel();
            ((ISupportInitialize)bindingSource).BeginInit();
            ((ISupportInitialize)gridControl).BeginInit();
            ((ISupportInitialize)gridView).BeginInit();
            ((ISupportInitialize)managerLookup).BeginInit();
            ((ISupportInitialize)categoryLookup).BeginInit();
            ((ISupportInitialize)assortLookup).BeginInit();
            ((ISupportInitialize)threadLookup).BeginInit();
            ((ISupportInitialize)approvedCheck).BeginInit();
            ((ISupportInitialize)normEditor).BeginInit();
            ((ISupportInitialize)filterGroup.Properties).BeginInit();
            ((ISupportInitialize)topPanel).BeginInit();
            topPanel.SuspendLayout();
            ((ISupportInitialize)bottomPanel).BeginInit();
            bottomPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // gridControl
            // 
            gridControl.DataSource = bindingSource;
            gridControl.Dock = DockStyle.Fill;
            gridControl.Location = new Point(0, 54);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl";
            gridControl.RepositoryItems.AddRange(new RepositoryItem[] { managerLookup, categoryLookup, assortLookup, threadLookup, approvedCheck, normEditor });
            gridControl.Size = new Size(1264, 649);
            gridControl.TabIndex = 0;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            // 
            // gridView
            // 
            gridView.Columns.AddRange(new GridColumn[] { colMen, colClassName, colGroupName, colCategory, colAssort, colThreadCode, colNorm, colApproved, colDateChange, colId, colKod3, colKodArt, colMenName, colTcId, colTgId, colCategoryName, colTatName, colThreadDisplay });
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsBehavior.Editable = true;
            gridView.OptionsNavigation.AutoFocusNewRow = true;
            gridView.OptionsView.ShowAutoFilterRow = true;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.RowCellStyle += GridView_RowCellStyle;
            gridView.CellValueChanged += GridView_CellValueChanged;
            gridView.ValidateRow += GridView_ValidateRow;
            // 
            // colMen
            // 
            colMen.Caption = "Менеджер";
            colMen.ColumnEdit = managerLookup;
            colMen.FieldName = "men";
            colMen.Name = "colMen";
            colMen.Visible = true;
            colMen.VisibleIndex = 0;
            colMen.Width = 90;
            // 
            // colClassName
            // 
            colClassName.Caption = "Класс";
            colClassName.FieldName = "TC_ClassName";
            colClassName.Name = "colClassName";
            colClassName.OptionsColumn.AllowEdit = false;
            colClassName.OptionsColumn.ReadOnly = true;
            colClassName.Visible = true;
            colClassName.VisibleIndex = 1;
            colClassName.Width = 160;
            // 
            // colGroupName
            // 
            colGroupName.Caption = "Группа";
            colGroupName.FieldName = "TG_GroupName";
            colGroupName.Name = "colGroupName";
            colGroupName.OptionsColumn.AllowEdit = false;
            colGroupName.OptionsColumn.ReadOnly = true;
            colGroupName.Visible = true;
            colGroupName.VisibleIndex = 2;
            colGroupName.Width = 180;
            // 
            // colCategory
            // 
            colCategory.Caption = "Категория";
            colCategory.ColumnEdit = categoryLookup;
            colCategory.FieldName = "tg_id_n";
            colCategory.Name = "colCategory";
            colCategory.Visible = true;
            colCategory.VisibleIndex = 3;
            colCategory.Width = 220;
            // 
            // colAssort
            // 
            colAssort.Caption = "Ассортимент";
            colAssort.ColumnEdit = assortLookup;
            colAssort.FieldName = "ta_id";
            colAssort.Name = "colAssort";
            colAssort.Visible = true;
            colAssort.VisibleIndex = 4;
            colAssort.Width = 140;
            // 
            // colThreadCode
            // 
            colThreadCode.Caption = "Код ниток";
            colThreadCode.ColumnEdit = threadLookup;
            colThreadCode.FieldName = "kod_dr";
            colThreadCode.Name = "colThreadCode";
            colThreadCode.Visible = true;
            colThreadCode.VisibleIndex = 5;
            colThreadCode.Width = 220;
            // 
            // colNorm
            // 
            colNorm.Caption = "Норма";
            colNorm.ColumnEdit = normEditor;
            colNorm.FieldName = "norm";
            colNorm.Name = "colNorm";
            colNorm.Visible = true;
            colNorm.VisibleIndex = 6;
            colNorm.Width = 90;
            // 
            // colApproved
            // 
            colApproved.Caption = "Утверждено";
            colApproved.ColumnEdit = approvedCheck;
            colApproved.FieldName = "approved";
            colApproved.Name = "colApproved";
            colApproved.Visible = true;
            colApproved.VisibleIndex = 7;
            colApproved.Width = 90;
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
            colDateChange.VisibleIndex = 8;
            colDateChange.Width = 110;
            // 
            // hidden columns
            // 
            colId.FieldName = "id";
            colId.Name = "colId";
            colKod3.FieldName = "kod3";
            colKod3.Name = "colKod3";
            colKodArt.FieldName = "kod_art";
            colKodArt.Name = "colKodArt";
            colMenName.FieldName = "men_name";
            colMenName.Name = "colMenName";
            colTcId.FieldName = "TC_ID";
            colTcId.Name = "colTcId";
            colTgId.FieldName = "TG_ID";
            colTgId.Name = "colTgId";
            colCategoryName.FieldName = "TCAT_CategoryName";
            colCategoryName.Name = "colCategoryName";
            colTatName.FieldName = "TAT_Name";
            colTatName.Name = "colTatName";
            colThreadDisplay.FieldName = "ThreadDisplay";
            colThreadDisplay.Name = "colThreadDisplay";
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
            managerLookup.TextEditStyle = TextEditStyles.Standard;
            managerLookup.ValueMember = "Men";
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
            categoryLookup.ShowFooter = false;
            categoryLookup.ShowHeader = false;
            categoryLookup.TextEditStyle = TextEditStyles.Standard;
            categoryLookup.ValueMember = "TCAT_ID";
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
            assortLookup.TextEditStyle = TextEditStyles.Standard;
            assortLookup.ValueMember = "TAT_ID";
            // 
            // threadLookup
            // 
            threadLookup.AutoHeight = false;
            threadLookup.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            threadLookup.DisplayMember = "displayText";
            threadLookup.Name = "threadLookup";
            threadLookup.NullText = "[Выберите нитки]";
            threadLookup.PopupFilterMode = PopupFilterMode.Contains;
            threadLookup.SearchMode = SearchMode.AutoSearch;
            threadLookup.ShowFooter = false;
            threadLookup.ShowHeader = false;
            threadLookup.TextEditStyle = TextEditStyles.Standard;
            threadLookup.ValueMember = "kod_dr";
            // 
            // approvedCheck
            // 
            approvedCheck.AutoHeight = false;
            approvedCheck.Name = "approvedCheck";
            approvedCheck.ValueChecked = true;
            approvedCheck.ValueUnchecked = false;
            // 
            // normEditor
            // 
            normEditor.AutoHeight = false;
            normEditor.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            normEditor.IsFloatValue = true;
            normEditor.Mask.EditMask = "0.###";
            normEditor.MaxValue = new decimal(new int[] { 999, 0, 0, 0 });
            normEditor.Name = "normEditor";
            // 
            // filterGroup
            // 
            filterGroup.Dock = DockStyle.Left;
            filterGroup.Location = new Point(2, 2);
            filterGroup.Name = "filterGroup";
            filterGroup.Properties.Items.AddRange(new RadioGroupItem[] {
                new RadioGroupItem(0, "0 норма"),
                new RadioGroupItem(1, "Все")
            });
            filterGroup.SelectedIndex = 0;
            filterGroup.Size = new Size(180, 50);
            filterGroup.TabIndex = 0;
            filterGroup.SelectedIndexChanged += FilterGroup_SelectedIndexChanged;
            // 
            // addButton
            // 
            addButton.AutoSize = true;
            addButton.Margin = new Padding(8, 6, 0, 6);
            addButton.Name = "addButton";
            addButton.Size = new Size(81, 27);
            addButton.TabIndex = 0;
            addButton.Text = "Добавить";
            addButton.Click += AddButton_Click;
            // 
            // copyButton
            // 
            copyButton.AutoSize = true;
            copyButton.Margin = new Padding(8, 6, 0, 6);
            copyButton.Name = "copyButton";
            copyButton.Size = new Size(117, 27);
            copyButton.TabIndex = 1;
            copyButton.Text = "Добавить копию";
            copyButton.Click += CopyButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.AutoSize = true;
            deleteButton.Margin = new Padding(8, 6, 0, 6);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(72, 27);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Удалить";
            deleteButton.Click += DeleteButton_Click;
            // 
            // saveButton
            // 
            saveButton.AutoSize = true;
            saveButton.Margin = new Padding(8, 6, 0, 6);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(89, 27);
            saveButton.TabIndex = 3;
            saveButton.Text = "Сохранить";
            saveButton.Click += SaveButton_Click;
            // 
            // closeButton
            // 
            closeButton.AutoSize = true;
            closeButton.Margin = new Padding(8, 6, 0, 6);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 27);
            closeButton.TabIndex = 4;
            closeButton.Text = "Закрыть";
            closeButton.Click += CloseButton_Click;
            // 
            // topPanel
            // 
            topPanel.Controls.Add(filterGroup);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1264, 54);
            topPanel.TabIndex = 1;
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(buttonsPanel);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 703);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(1264, 58);
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
            buttonsPanel.Size = new Size(1260, 54);
            buttonsPanel.TabIndex = 0;
            buttonsPanel.WrapContents = false;
            // 
            // ThreadNormsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 761);
            Controls.Add(gridControl);
            Controls.Add(bottomPanel);
            Controls.Add(topPanel);
            MinimumSize = new Size(1100, 700);
            Name = "ThreadNormsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Справочник норм ниток";
            FormClosing += ThreadNormsForm_FormClosing;
            Load += ThreadNormsForm_Load;
            ((ISupportInitialize)bindingSource).EndInit();
            ((ISupportInitialize)gridControl).EndInit();
            ((ISupportInitialize)gridView).EndInit();
            ((ISupportInitialize)managerLookup).EndInit();
            ((ISupportInitialize)categoryLookup).EndInit();
            ((ISupportInitialize)assortLookup).EndInit();
            ((ISupportInitialize)threadLookup).EndInit();
            ((ISupportInitialize)approvedCheck).EndInit();
            ((ISupportInitialize)normEditor).EndInit();
            ((ISupportInitialize)filterGroup.Properties).EndInit();
            ((ISupportInitialize)topPanel).EndInit();
            topPanel.ResumeLayout(false);
            ((ISupportInitialize)bottomPanel).EndInit();
            bottomPanel.ResumeLayout(false);
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}
