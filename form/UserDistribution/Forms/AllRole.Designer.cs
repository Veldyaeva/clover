namespace SewingProduction.form.UserDistribution
{
    partial class AllRole
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridViewForms = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ProjectFormsID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NameForm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HasAccess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBoxForms = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.customGridControlRoles = new SewingProduction.CustomGridControl();
            this.bindingSourceRoles = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.UserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserNameUsers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HasRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridViewRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.RoleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.repositoryItemComboBoxObject = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridViewObject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ObjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ObjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HasAccessObject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditForms = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEditObject = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.customButtonAddRole = new SewingProduction.CustomButton();
            this.customButtonDeleteRole = new SewingProduction.CustomButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customButtonCopyRole = new SewingProduction.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditObject)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridViewForms
            // 
            this.gridViewForms.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridViewForms.Appearance.Row.Options.UseBackColor = true;
            this.gridViewForms.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridViewForms.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridViewForms.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ProjectFormsID,
            this.NameForm,
            this.HasAccess});
            this.gridViewForms.DetailHeight = 3500;
            this.gridViewForms.GridControl = this.customGridControlRoles;
            this.gridViewForms.Name = "gridViewForms";
            this.gridViewForms.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridViewForms.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridViewForms.OptionsPrint.ExpandAllDetails = true;
            this.gridViewForms.OptionsPrint.MaxMergedCellHeight = 1080;
            this.gridViewForms.OptionsPrint.PrintDetails = true;
            this.gridViewForms.OptionsView.ShowGroupPanel = false;
            this.gridViewForms.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewForms_FocusedRowChanged);
            // 
            // ProjectFormsID
            // 
            this.ProjectFormsID.Caption = "Ид";
            this.ProjectFormsID.FieldName = "ProjectFormsID";
            this.ProjectFormsID.Name = "ProjectFormsID";
            this.ProjectFormsID.OptionsColumn.ReadOnly = true;
            this.ProjectFormsID.Width = 139;
            // 
            // NameForm
            // 
            this.NameForm.Caption = "Имя формы";
            this.NameForm.FieldName = "NameForm";
            this.NameForm.Name = "NameForm";
            this.NameForm.OptionsColumn.ReadOnly = true;
            this.NameForm.Visible = true;
            this.NameForm.VisibleIndex = 0;
            this.NameForm.Width = 566;
            // 
            // HasAccess
            // 
            this.HasAccess.Caption = "Доступ";
            this.HasAccess.ColumnEdit = this.repositoryItemComboBoxForms;
            this.HasAccess.FieldName = "HasAccess";
            this.HasAccess.Name = "HasAccess";
            this.HasAccess.Visible = true;
            this.HasAccess.VisibleIndex = 1;
            this.HasAccess.Width = 406;
            // 
            // repositoryItemComboBoxForms
            // 
            this.repositoryItemComboBoxForms.AutoHeight = false;
            this.repositoryItemComboBoxForms.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxForms.Items.AddRange(new object[] {
            "Нет доступа",
            "Просмотр",
            "Редактор"});
            this.repositoryItemComboBoxForms.Name = "repositoryItemComboBoxForms";
            this.repositoryItemComboBoxForms.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // customGridControlRoles
            // 
            this.customGridControlRoles.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridControlRoles.DataSource = this.bindingSourceRoles;
            this.customGridControlRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControlRoles.Font = new System.Drawing.Font("Arial", 10F);
            gridLevelNode1.LevelTemplate = this.gridViewForms;
            gridLevelNode2.LevelTemplate = this.gridViewObject;
            gridLevelNode2.RelationName = "Объекты";
            gridLevelNode1.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            gridLevelNode1.RelationName = "Формы";
            gridLevelNode3.LevelTemplate = this.gridViewUsers;
            gridLevelNode3.RelationName = "Пользователи";
            this.customGridControlRoles.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode3});
            this.customGridControlRoles.Location = new System.Drawing.Point(3, 3);
            this.customGridControlRoles.MainView = this.gridViewRoles;
            this.customGridControlRoles.Name = "customGridControlRoles";
            this.customGridControlRoles.ObjectName = null;
            this.customGridControlRoles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckedComboBoxEdit1,
            this.repositoryItemComboBoxForms,
            this.repositoryItemComboBoxObject});
            this.tableLayoutPanel1.SetRowSpan(this.customGridControlRoles, 10);
            this.customGridControlRoles.Size = new System.Drawing.Size(1136, 667);
            this.customGridControlRoles.TabIndex = 0;
            this.customGridControlRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers,
            this.gridViewRoles,
            this.gridViewObject,
            this.gridViewForms});
            this.customGridControlRoles.Load += new System.EventHandler(this.customGridControlRoles_Load);
            // 
            // gridViewUsers
            // 
            this.gridViewUsers.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(200)))));
            this.gridViewUsers.Appearance.Row.Options.UseBackColor = true;
            this.gridViewUsers.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridViewUsers.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.UserID,
            this.UserNameUsers,
            this.HasRole});
            this.gridViewUsers.DetailHeight = 3500;
            this.gridViewUsers.GridControl = this.customGridControlRoles;
            this.gridViewUsers.Name = "gridViewUsers";
            this.gridViewUsers.OptionsDetail.ShowDetailTabs = false;
            this.gridViewUsers.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridViewUsers.OptionsView.ShowGroupPanel = false;
            this.gridViewUsers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewUsers_FocusedRowChanged);
            // 
            // UserID
            // 
            this.UserID.Caption = "Ид";
            this.UserID.FieldName = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.OptionsColumn.ReadOnly = true;
            this.UserID.Width = 144;
            // 
            // UserNameUsers
            // 
            this.UserNameUsers.Caption = "Пользователь";
            this.UserNameUsers.FieldName = "UserName";
            this.UserNameUsers.Name = "UserNameUsers";
            this.UserNameUsers.OptionsColumn.ReadOnly = true;
            this.UserNameUsers.Visible = true;
            this.UserNameUsers.VisibleIndex = 0;
            this.UserNameUsers.Width = 596;
            // 
            // HasRole
            // 
            this.HasRole.Caption = "Доступ";
            this.HasRole.FieldName = "HasRole";
            this.HasRole.Name = "HasRole";
            this.HasRole.Visible = true;
            this.HasRole.VisibleIndex = 1;
            this.HasRole.Width = 371;
            // 
            // gridViewRoles
            // 
            this.gridViewRoles.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridViewRoles.Appearance.Row.Options.UseBackColor = true;
            this.gridViewRoles.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.White;
            this.gridViewRoles.AppearancePrint.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gridViewRoles.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridViewRoles.AppearancePrint.EvenRow.Options.UseBorderColor = true;
            this.gridViewRoles.AppearancePrint.GroupRow.BackColor = System.Drawing.Color.White;
            this.gridViewRoles.AppearancePrint.GroupRow.Options.UseBackColor = true;
            this.gridViewRoles.AppearancePrint.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridViewRoles.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.gridViewRoles.AppearancePrint.Row.BackColor = System.Drawing.Color.White;
            this.gridViewRoles.AppearancePrint.Row.Options.UseBackColor = true;
            this.gridViewRoles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.RoleID,
            this.RoleName,
            this.Description,
            this.UserName});
            this.gridViewRoles.DetailHeight = 3500;
            this.gridViewRoles.GridControl = this.customGridControlRoles;
            this.gridViewRoles.Name = "gridViewRoles";
            this.gridViewRoles.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewRoles.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewRoles.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridViewRoles.OptionsEditForm.EditFormColumnCount = 1;
            this.gridViewRoles.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridViewRoles.OptionsPrint.EnableAppearanceOddRow = true;
            this.gridViewRoles.OptionsPrint.MaxMergedCellHeight = 1080;
            this.gridViewRoles.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridViewRoles_InitNewRow);
            this.gridViewRoles.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewRoles_FocusedRowChanged);
            this.gridViewRoles.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewRoles_RowUpdated);
            // 
            // RoleID
            // 
            this.RoleID.Caption = "ИД роли";
            this.RoleID.FieldName = "RoleID";
            this.RoleID.Name = "RoleID";
            this.RoleID.OptionsColumn.ReadOnly = true;
            // 
            // RoleName
            // 
            this.RoleName.Caption = "Название";
            this.RoleName.FieldName = "RoleName";
            this.RoleName.Name = "RoleName";
            this.RoleName.Visible = true;
            this.RoleName.VisibleIndex = 0;
            this.RoleName.Width = 255;
            // 
            // Description
            // 
            this.Description.Caption = "Описание";
            this.Description.FieldName = "Description";
            this.Description.Name = "Description";
            this.Description.Visible = true;
            this.Description.VisibleIndex = 1;
            this.Description.Width = 592;
            // 
            // UserName
            // 
            this.UserName.Caption = "Создатель";
            this.UserName.FieldName = "UserName";
            this.UserName.Name = "UserName";
            this.UserName.OptionsColumn.ReadOnly = true;
            this.UserName.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.UserName.Visible = true;
            this.UserName.VisibleIndex = 2;
            this.UserName.Width = 155;
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // repositoryItemComboBoxObject
            // 
            this.repositoryItemComboBoxObject.AutoHeight = false;
            this.repositoryItemComboBoxObject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxObject.Name = "repositoryItemComboBoxObject";
            this.repositoryItemComboBoxObject.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // gridViewObject
            // 
            this.gridViewObject.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.gridViewObject.Appearance.Row.Options.UseBackColor = true;
            this.gridViewObject.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridViewObject.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridViewObject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ObjectID,
            this.ObjectName,
            this.HasAccessObject});
            this.gridViewObject.DetailHeight = 3500;
            this.gridViewObject.GridControl = this.customGridControlRoles;
            this.gridViewObject.Name = "gridViewObject";
            this.gridViewObject.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridViewObject.OptionsPrint.EnableAppearanceOddRow = true;
            this.gridViewObject.OptionsPrint.MaxMergedCellHeight = 1080;
            this.gridViewObject.OptionsView.ShowGroupPanel = false;
            this.gridViewObject.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewObject_CustomRowCellEdit);
            this.gridViewObject.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewObject_FocusedRowChanged);
            // 
            // ObjectID
            // 
            this.ObjectID.Caption = "Ид";
            this.ObjectID.FieldName = "ObjectID";
            this.ObjectID.Name = "ObjectID";
            this.ObjectID.OptionsColumn.ReadOnly = true;
            this.ObjectID.Width = 158;
            // 
            // ObjectName
            // 
            this.ObjectName.Caption = "Имя объекта";
            this.ObjectName.FieldName = "ObjectName";
            this.ObjectName.Name = "ObjectName";
            this.ObjectName.OptionsColumn.ReadOnly = true;
            this.ObjectName.Visible = true;
            this.ObjectName.VisibleIndex = 0;
            this.ObjectName.Width = 543;
            // 
            // HasAccessObject
            // 
            this.HasAccessObject.Caption = "Доступ";
            this.HasAccessObject.ColumnEdit = this.repositoryItemComboBoxObject;
            this.HasAccessObject.FieldName = "HasAccessObject";
            this.HasAccessObject.Name = "HasAccessObject";
            this.HasAccessObject.Visible = true;
            this.HasAccessObject.VisibleIndex = 1;
            this.HasAccessObject.Width = 410;
            // 
            // repositoryItemCheckEditForms
            // 
            this.repositoryItemCheckEditForms.AutoHeight = false;
            this.repositoryItemCheckEditForms.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.repositoryItemCheckEditForms.Name = "repositoryItemCheckEditForms";
            this.repositoryItemCheckEditForms.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // repositoryItemCheckEditObject
            // 
            this.repositoryItemCheckEditObject.AutoHeight = false;
            this.repositoryItemCheckEditObject.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.repositoryItemCheckEditObject.Name = "repositoryItemCheckEditObject";
            this.repositoryItemCheckEditObject.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // customButtonAddRole
            // 
            this.customButtonAddRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonAddRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonAddRole.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonAddRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonAddRole.Location = new System.Drawing.Point(1145, 3);
            this.customButtonAddRole.Name = "customButtonAddRole";
            this.customButtonAddRole.ObjectName = null;
            this.customButtonAddRole.Size = new System.Drawing.Size(121, 61);
            this.customButtonAddRole.TabIndex = 1;
            this.customButtonAddRole.Text = "Добавить роль";
            this.customButtonAddRole.UseVisualStyleBackColor = false;
            this.customButtonAddRole.Click += new System.EventHandler(this.customButtonAddRole_Click);
            // 
            // customButtonDeleteRole
            // 
            this.customButtonDeleteRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonDeleteRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonDeleteRole.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonDeleteRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonDeleteRole.Location = new System.Drawing.Point(1145, 606);
            this.customButtonDeleteRole.Name = "customButtonDeleteRole";
            this.customButtonDeleteRole.ObjectName = null;
            this.customButtonDeleteRole.Size = new System.Drawing.Size(121, 64);
            this.customButtonDeleteRole.TabIndex = 2;
            this.customButtonDeleteRole.Text = "Удалить роль";
            this.customButtonDeleteRole.UseVisualStyleBackColor = false;
            this.customButtonDeleteRole.Click += new System.EventHandler(this.customButtonDeleteRole_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.customButtonCopyRole, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customGridControlRoles, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customButtonAddRole, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customButtonDeleteRole, 1, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1269, 673);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // customButtonCopyRole
            // 
            this.customButtonCopyRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonCopyRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonCopyRole.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonCopyRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonCopyRole.Location = new System.Drawing.Point(1145, 70);
            this.customButtonCopyRole.Name = "customButtonCopyRole";
            this.customButtonCopyRole.ObjectName = null;
            this.customButtonCopyRole.Size = new System.Drawing.Size(121, 61);
            this.customButtonCopyRole.TabIndex = 3;
            this.customButtonCopyRole.Text = "Копировать роль";
            this.customButtonCopyRole.UseVisualStyleBackColor = false;
            this.customButtonCopyRole.Click += new System.EventHandler(this.customButtonCopyRole_Click);
            // 
            // AllRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AllRole";
            this.Text = "Роли";
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditObject)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomGridControl customGridControlRoles;
        private System.Windows.Forms.BindingSource bindingSourceRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRoles;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButtonAddRole;
        private CustomButton customButtonDeleteRole;
        private DevExpress.XtraGrid.Columns.GridColumn RoleID;
        private DevExpress.XtraGrid.Columns.GridColumn RoleName;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private DevExpress.XtraGrid.Columns.GridColumn UserName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewForms;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private DevExpress.XtraGrid.Columns.GridColumn NameForm;
        private DevExpress.XtraGrid.Columns.GridColumn HasAccess;
        private DevExpress.XtraGrid.Columns.GridColumn UserID;
        private DevExpress.XtraGrid.Columns.GridColumn ProjectFormsID;
        private DevExpress.XtraGrid.Columns.GridColumn UserNameUsers;
        private DevExpress.XtraGrid.Columns.GridColumn HasRole;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewObject;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectID;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectName;
        private DevExpress.XtraGrid.Columns.GridColumn HasAccessObject;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditForms;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditObject;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxForms;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxObject;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private CustomButton customButtonCopyRole;
    }
}