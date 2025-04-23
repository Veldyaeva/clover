namespace SewingProduction.form.UserDistribution
{
    partial class AllUser
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
            this.gridViewRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.RoleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HasRole1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customGridControlUser = new SewingProduction.CustomGridControl();
            this.bindingSourceUsers = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.UserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserNameUsers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Brig = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FioID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Fio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.repositoryItemComboBoxForms = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxObject = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customButtonShareUser = new SewingProduction.CustomButton();
            this.customButtonCopyUser = new SewingProduction.CustomButton();
            this.customButtonAddUser = new SewingProduction.CustomButton();
            this.customButtonDeleteUser = new SewingProduction.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxObject)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.HasRole1});
            this.gridViewRoles.DetailHeight = 3500;
            this.gridViewRoles.GridControl = this.customGridControlUser;
            this.gridViewRoles.Name = "gridViewRoles";
            this.gridViewRoles.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewRoles.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewRoles.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridViewRoles.OptionsEditForm.EditFormColumnCount = 1;
            this.gridViewRoles.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridViewRoles.OptionsPrint.EnableAppearanceOddRow = true;
            this.gridViewRoles.OptionsPrint.MaxMergedCellHeight = 1080;
            this.gridViewRoles.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewRoles_FocusedRowChanged);
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
            this.RoleName.Width = 263;
            // 
            // Description
            // 
            this.Description.Caption = "Описание";
            this.Description.FieldName = "Description";
            this.Description.Name = "Description";
            this.Description.Visible = true;
            this.Description.VisibleIndex = 1;
            this.Description.Width = 452;
            // 
            // HasRole1
            // 
            this.HasRole1.Caption = "Доступ";
            this.HasRole1.FieldName = "HasRole";
            this.HasRole1.Name = "HasRole1";
            this.HasRole1.Visible = true;
            this.HasRole1.VisibleIndex = 2;
            this.HasRole1.Width = 204;
            // 
            // customGridControlUser
            // 
            this.customGridControlUser.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridControlUser.DataSource = this.bindingSourceUsers;
            this.customGridControlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControlUser.Font = new System.Drawing.Font("Arial", 10F);
            gridLevelNode1.LevelTemplate = this.gridViewRoles;
            gridLevelNode1.RelationName = "Роли";
            this.customGridControlUser.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.customGridControlUser.Location = new System.Drawing.Point(3, 3);
            this.customGridControlUser.MainView = this.gridViewUsers;
            this.customGridControlUser.Name = "customGridControlUser";
            this.customGridControlUser.ObjectName = null;
            this.customGridControlUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckedComboBoxEdit1,
            this.repositoryItemComboBoxForms,
            this.repositoryItemComboBoxObject});
            this.tableLayoutPanel1.SetRowSpan(this.customGridControlUser, 10);
            this.customGridControlUser.Size = new System.Drawing.Size(1136, 667);
            this.customGridControlUser.TabIndex = 0;
            this.customGridControlUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers,
            this.gridViewRoles});
            this.customGridControlUser.Load += new System.EventHandler(this.customGridControlUsers_Load);
            // 
            // gridViewUsers
            // 
            this.gridViewUsers.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.gridViewUsers.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridViewUsers.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(200)))));
            this.gridViewUsers.Appearance.Row.Options.UseBackColor = true;
            this.gridViewUsers.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridViewUsers.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.UserID,
            this.UserNameUsers,
            this.Brig,
            this.FioID,
            this.Fio});
            this.gridViewUsers.DetailHeight = 3500;
            this.gridViewUsers.GridControl = this.customGridControlUser;
            this.gridViewUsers.Name = "gridViewUsers";
            this.gridViewUsers.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewUsers.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewUsers.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridViewUsers.OptionsEditForm.EditFormColumnCount = 1;
            this.gridViewUsers.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridViewUsers.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewUsers.OptionsView.ShowGroupPanel = false;
            this.gridViewUsers.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridViewUsers_InitNewRow);
            this.gridViewUsers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewUsers_FocusedRowChanged);
            this.gridViewUsers.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewUsers_RowUpdated);
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
            this.UserNameUsers.Visible = true;
            this.UserNameUsers.VisibleIndex = 0;
            this.UserNameUsers.Width = 601;
            // 
            // Brig
            // 
            this.Brig.Caption = "Бригада";
            this.Brig.FieldName = "Brig";
            this.Brig.Name = "Brig";
            this.Brig.Visible = true;
            this.Brig.VisibleIndex = 1;
            this.Brig.Width = 228;
            // 
            // FioID
            // 
            this.FioID.Caption = "FioID";
            this.FioID.FieldName = "FioID";
            this.FioID.Name = "FioID";
            // 
            // Fio
            // 
            this.Fio.Caption = "ФИО";
            this.Fio.FieldName = "Fio";
            this.Fio.Name = "Fio";
            this.Fio.Visible = true;
            this.Fio.VisibleIndex = 2;
            this.Fio.Width = 282;
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
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
            // repositoryItemComboBoxObject
            // 
            this.repositoryItemComboBoxObject.AutoHeight = false;
            this.repositoryItemComboBoxObject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxObject.Name = "repositoryItemComboBoxObject";
            this.repositoryItemComboBoxObject.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.customButtonShareUser, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.customButtonCopyUser, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customGridControlUser, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customButtonAddUser, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customButtonDeleteUser, 1, 9);
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
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // customButtonShareUser
            // 
            this.customButtonShareUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonShareUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonShareUser.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonShareUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonShareUser.Location = new System.Drawing.Point(1145, 137);
            this.customButtonShareUser.Name = "customButtonShareUser";
            this.customButtonShareUser.ObjectName = null;
            this.customButtonShareUser.Size = new System.Drawing.Size(121, 61);
            this.customButtonShareUser.TabIndex = 4;
            this.customButtonShareUser.Text = "Поделиться  пользователем";
            this.customButtonShareUser.UseVisualStyleBackColor = false;
            // 
            // customButtonCopyUser
            // 
            this.customButtonCopyUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonCopyUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonCopyUser.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonCopyUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonCopyUser.Location = new System.Drawing.Point(1145, 70);
            this.customButtonCopyUser.Name = "customButtonCopyUser";
            this.customButtonCopyUser.ObjectName = null;
            this.customButtonCopyUser.Size = new System.Drawing.Size(121, 61);
            this.customButtonCopyUser.TabIndex = 3;
            this.customButtonCopyUser.Text = "Копировать  пользователя";
            this.customButtonCopyUser.UseVisualStyleBackColor = false;
            // 
            // customButtonAddUser
            // 
            this.customButtonAddUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonAddUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonAddUser.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonAddUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonAddUser.Location = new System.Drawing.Point(1145, 3);
            this.customButtonAddUser.Name = "customButtonAddUser";
            this.customButtonAddUser.ObjectName = null;
            this.customButtonAddUser.Size = new System.Drawing.Size(121, 61);
            this.customButtonAddUser.TabIndex = 1;
            this.customButtonAddUser.Text = "Добавить пользователя";
            this.customButtonAddUser.UseVisualStyleBackColor = false;
            this.customButtonAddUser.Click += new System.EventHandler(this.customButtonAddUser_Click);
            // 
            // customButtonDeleteUser
            // 
            this.customButtonDeleteUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonDeleteUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonDeleteUser.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonDeleteUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonDeleteUser.Location = new System.Drawing.Point(1145, 606);
            this.customButtonDeleteUser.Name = "customButtonDeleteUser";
            this.customButtonDeleteUser.ObjectName = null;
            this.customButtonDeleteUser.Size = new System.Drawing.Size(121, 64);
            this.customButtonDeleteUser.TabIndex = 2;
            this.customButtonDeleteUser.Text = "Удалить пользователя";
            this.customButtonDeleteUser.UseVisualStyleBackColor = false;
            // 
            // AllUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AllUser";
            this.Text = "Пользователи";
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxObject)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButtonCopyUser;
        private CustomGridControl customGridControlUser;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxForms;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxObject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private DevExpress.XtraGrid.Columns.GridColumn UserID;
        private DevExpress.XtraGrid.Columns.GridColumn UserNameUsers;
        private DevExpress.XtraGrid.Columns.GridColumn Brig;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRoles;
        private DevExpress.XtraGrid.Columns.GridColumn RoleID;
        private DevExpress.XtraGrid.Columns.GridColumn RoleName;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private CustomButton customButtonAddUser;
        private CustomButton customButtonDeleteUser;
        private CustomButton customButtonShareUser;
        private System.Windows.Forms.BindingSource bindingSourceUsers;
        private DevExpress.XtraGrid.Columns.GridColumn FioID;
        private DevExpress.XtraGrid.Columns.GridColumn Fio;
        private DevExpress.XtraGrid.Columns.GridColumn HasRole1;
    }
}