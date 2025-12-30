using SewingProduction.Core.Class;
namespace SewingProduction.Features.UserDistribution.Forms
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
            components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllUser));
            gridViewRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            RoleID = new DevExpress.XtraGrid.Columns.GridColumn();
            RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            Description = new DevExpress.XtraGrid.Columns.GridColumn();
            HasRole1 = new DevExpress.XtraGrid.Columns.GridColumn();
            customGridControlUser = new CustomGridControl();
            bindingSourceUsers = new System.Windows.Forms.BindingSource(components);
            gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            UserID = new DevExpress.XtraGrid.Columns.GridColumn();
            UserNameUsers = new DevExpress.XtraGrid.Columns.GridColumn();
            BrigID = new DevExpress.XtraGrid.Columns.GridColumn();
            Brig = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemLookUpEditBrig = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            FioID = new DevExpress.XtraGrid.Columns.GridColumn();
            Fio = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemLookUpEditFio = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            Password = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            customButtonUserPodr = new CustomButton();
            customButtonShareUser = new CustomButton();
            customButtonCopyUser = new CustomButton();
            customButtonAddUser = new CustomButton();
            customButtonDeleteUser = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)gridViewRoles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditBrig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditFio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // gridViewRoles
            // 
            gridViewRoles.Appearance.Row.BackColor = System.Drawing.Color.White;
            gridViewRoles.Appearance.Row.Options.UseBackColor = true;
            gridViewRoles.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.White;
            gridViewRoles.AppearancePrint.EvenRow.BorderColor = System.Drawing.Color.White;
            gridViewRoles.AppearancePrint.EvenRow.Options.UseBackColor = true;
            gridViewRoles.AppearancePrint.EvenRow.Options.UseBorderColor = true;
            gridViewRoles.AppearancePrint.GroupRow.BackColor = System.Drawing.Color.White;
            gridViewRoles.AppearancePrint.GroupRow.Options.UseBackColor = true;
            gridViewRoles.AppearancePrint.OddRow.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
            gridViewRoles.AppearancePrint.OddRow.Options.UseBackColor = true;
            gridViewRoles.AppearancePrint.Row.BackColor = System.Drawing.Color.White;
            gridViewRoles.AppearancePrint.Row.Options.UseBackColor = true;
            gridViewRoles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { RoleID, RoleName, Description, HasRole1 });
            gridViewRoles.DetailHeight = 4038;
            gridViewRoles.GridControl = customGridControlUser;
            gridViewRoles.Name = "gridViewRoles";
            gridViewRoles.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            gridViewRoles.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            gridViewRoles.OptionsDetail.AllowExpandEmptyDetails = true;
            gridViewRoles.OptionsDetail.ShowDetailTabs = false;
            gridViewRoles.OptionsEditForm.EditFormColumnCount = 1;
            gridViewRoles.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewRoles.OptionsPrint.EnableAppearanceEvenRow = true;
            gridViewRoles.OptionsPrint.EnableAppearanceOddRow = true;
            gridViewRoles.OptionsPrint.MaxMergedCellHeight = 1080;
            gridViewRoles.OptionsView.ShowGroupPanel = false;
            gridViewRoles.SelectionChanged += gridViewRoles_SelectionChanged;
            gridViewRoles.FocusedRowChanged += gridViewRoles_FocusedRowChanged;
            // 
            // RoleID
            // 
            RoleID.Caption = "ИД роли";
            RoleID.FieldName = "RoleID";
            RoleID.MinWidth = 23;
            RoleID.Name = "RoleID";
            RoleID.OptionsColumn.ReadOnly = true;
            RoleID.Width = 87;
            // 
            // RoleName
            // 
            RoleName.Caption = "Название";
            RoleName.FieldName = "RoleName";
            RoleName.MinWidth = 23;
            RoleName.Name = "RoleName";
            RoleName.OptionsColumn.ReadOnly = true;
            RoleName.Visible = true;
            RoleName.VisibleIndex = 0;
            RoleName.Width = 307;
            // 
            // Description
            // 
            Description.Caption = "Описание";
            Description.FieldName = "Description";
            Description.MinWidth = 23;
            Description.Name = "Description";
            Description.OptionsColumn.ReadOnly = true;
            Description.Visible = true;
            Description.VisibleIndex = 1;
            Description.Width = 527;
            // 
            // HasRole1
            // 
            HasRole1.Caption = "Доступ";
            HasRole1.FieldName = "HasRole";
            HasRole1.MinWidth = 23;
            HasRole1.Name = "HasRole1";
            HasRole1.Visible = true;
            HasRole1.VisibleIndex = 2;
            HasRole1.Width = 238;
            // 
            // customGridControlUser
            // 
            customGridControlUser.DataSource = bindingSourceUsers;
            customGridControlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlUser.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlUser.Font = new System.Drawing.Font("Arial", 10F);
            gridLevelNode2.LevelTemplate = gridViewRoles;
            gridLevelNode2.RelationName = "Роли";
            customGridControlUser.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode2 });
            customGridControlUser.Location = new System.Drawing.Point(4, 3);
            customGridControlUser.MainView = gridViewUsers;
            customGridControlUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlUser.Name = "customGridControlUser";
            customGridControlUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEdit1, repositoryItemLookUpEditBrig, repositoryItemLookUpEditFio });
            tableLayoutPanel1.SetRowSpan(customGridControlUser, 10);
            customGridControlUser.Size = new System.Drawing.Size(1124, 646);
            customGridControlUser.TabIndex = 0;
            customGridControlUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUsers, gridViewRoles });
            customGridControlUser.Load += customGridControlUsers_Load;
            // 
            // gridViewUsers
            // 
            gridViewUsers.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            gridViewUsers.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewUsers.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(250, 250, 200);
            gridViewUsers.Appearance.Row.Options.UseBackColor = true;
            gridViewUsers.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(255, 192, 255);
            gridViewUsers.AppearancePrint.EvenRow.Options.UseBackColor = true;
            gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { UserID, UserNameUsers, BrigID, Brig, FioID, Fio, Password });
            gridViewUsers.DetailHeight = 4038;
            gridViewUsers.GridControl = customGridControlUser;
            gridViewUsers.Name = "gridViewUsers";
            gridViewUsers.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridViewUsers.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridViewUsers.OptionsDetail.AllowExpandEmptyDetails = true;
            gridViewUsers.OptionsEditForm.EditFormColumnCount = 1;
            gridViewUsers.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewUsers.OptionsEditForm.ShowOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            gridViewUsers.OptionsEditForm.ShowOnEnterKey = DevExpress.Utils.DefaultBoolean.True;
            gridViewUsers.OptionsEditForm.ShowOnF2Key = DevExpress.Utils.DefaultBoolean.True;
            gridViewUsers.OptionsPrint.EnableAppearanceEvenRow = true;
            gridViewUsers.OptionsView.EnableAppearanceEvenRow = true;
            gridViewUsers.OptionsView.ShowGroupPanel = false;
            gridViewUsers.InitNewRow += gridViewUsers_InitNewRow;
            gridViewUsers.FocusedRowChanged += gridViewUsers_FocusedRowChanged;
            gridViewUsers.RowUpdated += gridViewUsers_RowUpdated;
            // 
            // UserID
            // 
            UserID.Caption = "Ид";
            UserID.FieldName = "UserID";
            UserID.MinWidth = 23;
            UserID.Name = "UserID";
            UserID.OptionsColumn.ReadOnly = true;
            UserID.Width = 168;
            // 
            // UserNameUsers
            // 
            UserNameUsers.Caption = "Пользователь";
            UserNameUsers.FieldName = "UserName";
            UserNameUsers.MinWidth = 23;
            UserNameUsers.Name = "UserNameUsers";
            UserNameUsers.Visible = true;
            UserNameUsers.VisibleIndex = 0;
            // 
            // BrigID
            // 
            BrigID.Caption = "BrigID";
            BrigID.FieldName = "BrigID";
            BrigID.MinWidth = 23;
            BrigID.Name = "BrigID";
            BrigID.OptionsColumn.AllowEdit = false;
            BrigID.Width = 87;
            // 
            // Brig
            // 
            Brig.Caption = "Бригада";
            Brig.ColumnEdit = repositoryItemLookUpEditBrig;
            Brig.FieldName = "BrigID";
            Brig.MinWidth = 23;
            Brig.Name = "Brig";
            Brig.Visible = true;
            Brig.VisibleIndex = 1;
            Brig.Width = 266;
            // 
            // repositoryItemLookUpEditBrig
            // 
            repositoryItemLookUpEditBrig.AutoHeight = false;
            repositoryItemLookUpEditBrig.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            repositoryItemLookUpEditBrig.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemLookUpEditBrig.DisplayMember = "Brig";
            repositoryItemLookUpEditBrig.Name = "repositoryItemLookUpEditBrig";
            repositoryItemLookUpEditBrig.NullText = "-";
            repositoryItemLookUpEditBrig.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            repositoryItemLookUpEditBrig.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditBrig.ValueMember = "BrigID";
            // 
            // FioID
            // 
            FioID.Caption = "FioID";
            FioID.FieldName = "FioID";
            FioID.MinWidth = 23;
            FioID.Name = "FioID";
            FioID.Width = 87;
            // 
            // Fio
            // 
            Fio.Caption = "ФИО";
            Fio.ColumnEdit = repositoryItemLookUpEditFio;
            Fio.FieldName = "FioID";
            Fio.MinWidth = 23;
            Fio.Name = "Fio";
            Fio.Visible = true;
            Fio.VisibleIndex = 2;
            Fio.Width = 329;
            // 
            // repositoryItemLookUpEditFio
            // 
            repositoryItemLookUpEditFio.AutoHeight = false;
            repositoryItemLookUpEditFio.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemLookUpEditFio.DisplayMember = "Fio";
            repositoryItemLookUpEditFio.Name = "repositoryItemLookUpEditFio";
            repositoryItemLookUpEditFio.NullText = "-";
            repositoryItemLookUpEditFio.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            repositoryItemLookUpEditFio.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditFio.ValueMember = "FioID";
            // 
            // Password
            // 
            Password.Caption = "Пароль";
            Password.FieldName = "Password";
            Password.MinWidth = 23;
            Password.Name = "Password";
            Password.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            Password.Width = 87;
            // 
            // repositoryItemCheckEdit1
            // 
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            repositoryItemCheckEdit1.ValueGrayed = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.Controls.Add(customButtonUserPodr, 1, 3);
            tableLayoutPanel1.Controls.Add(customButtonShareUser, 1, 2);
            tableLayoutPanel1.Controls.Add(customButtonCopyUser, 1, 1);
            tableLayoutPanel1.Controls.Add(customGridControlUser, 0, 0);
            tableLayoutPanel1.Controls.Add(customButtonAddUser, 1, 0);
            tableLayoutPanel1.Controls.Add(customButtonDeleteUser, 1, 9);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 10;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1258, 652);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // customButtonUserPodr
            // 
            customButtonUserPodr.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonUserPodr.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonUserPodr.Font = new System.Drawing.Font("Arial", 10F);
            customButtonUserPodr.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonUserPodr.Location = new System.Drawing.Point(1136, 198);
            customButtonUserPodr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonUserPodr.MinimumSize = new System.Drawing.Size(160, 20);
            customButtonUserPodr.Name = "customButtonUserPodr";
            customButtonUserPodr.Size = new System.Drawing.Size(160, 59);
            customButtonUserPodr.TabIndex = 14;
            customButtonUserPodr.Text = "Администрирование табеля";
            customButtonUserPodr.UseVisualStyleBackColor = false;
            customButtonUserPodr.Click += customButtonUserPodr_Click;
            // 
            // customButtonShareUser
            // 
            customButtonShareUser.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonShareUser.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonShareUser.Font = new System.Drawing.Font("Arial", 10F);
            customButtonShareUser.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonShareUser.Location = new System.Drawing.Point(1136, 133);
            customButtonShareUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonShareUser.Name = "customButtonShareUser";
            customButtonShareUser.Size = new System.Drawing.Size(118, 59);
            customButtonShareUser.TabIndex = 4;
            customButtonShareUser.Text = "Поделиться  пользователем";
            customButtonShareUser.UseVisualStyleBackColor = false;
            customButtonShareUser.Click += customButtonShareUser_Click;
            // 
            // customButtonCopyUser
            // 
            customButtonCopyUser.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonCopyUser.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonCopyUser.Font = new System.Drawing.Font("Arial", 10F);
            customButtonCopyUser.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonCopyUser.Location = new System.Drawing.Point(1136, 68);
            customButtonCopyUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonCopyUser.Name = "customButtonCopyUser";
            customButtonCopyUser.Size = new System.Drawing.Size(118, 59);
            customButtonCopyUser.TabIndex = 3;
            customButtonCopyUser.Text = "Копировать  пользователя";
            customButtonCopyUser.UseVisualStyleBackColor = false;
            customButtonCopyUser.Click += customButtonCopyUser_Click;
            // 
            // customButtonAddUser
            // 
            customButtonAddUser.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonAddUser.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonAddUser.Font = new System.Drawing.Font("Arial", 10F);
            customButtonAddUser.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonAddUser.Location = new System.Drawing.Point(1136, 3);
            customButtonAddUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonAddUser.Name = "customButtonAddUser";
            customButtonAddUser.Size = new System.Drawing.Size(118, 59);
            customButtonAddUser.TabIndex = 1;
            customButtonAddUser.Text = "Добавить пользователя";
            customButtonAddUser.UseVisualStyleBackColor = false;
            customButtonAddUser.Click += customButtonAddUser_Click;
            // 
            // customButtonDeleteUser
            // 
            customButtonDeleteUser.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonDeleteUser.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonDeleteUser.Font = new System.Drawing.Font("Arial", 10F);
            customButtonDeleteUser.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonDeleteUser.Location = new System.Drawing.Point(1136, 588);
            customButtonDeleteUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonDeleteUser.Name = "customButtonDeleteUser";
            customButtonDeleteUser.Size = new System.Drawing.Size(118, 61);
            customButtonDeleteUser.TabIndex = 2;
            customButtonDeleteUser.Text = "Удалить пользователя";
            customButtonDeleteUser.UseVisualStyleBackColor = false;
            customButtonDeleteUser.Click += customButtonDeleteUser_Click;
            // 
            // AllUser
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1258, 652);
            Controls.Add(tableLayoutPanel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "AllUser";
            Text = "Пользователи";
            Load += AllUser_Load;
            ((System.ComponentModel.ISupportInitialize)gridViewRoles).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditBrig).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditFio).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButtonCopyUser;
        private CustomGridControl customGridControlUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private DevExpress.XtraGrid.Columns.GridColumn UserID;
        private DevExpress.XtraGrid.Columns.GridColumn UserNameUsers;
        private DevExpress.XtraGrid.Columns.GridColumn Brig;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRoles;
        private DevExpress.XtraGrid.Columns.GridColumn RoleID;
        private DevExpress.XtraGrid.Columns.GridColumn RoleName;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private CustomButton customButtonAddUser;
        private CustomButton customButtonDeleteUser;
        private CustomButton customButtonShareUser;
        private System.Windows.Forms.BindingSource bindingSourceUsers;
        private DevExpress.XtraGrid.Columns.GridColumn FioID;
        private DevExpress.XtraGrid.Columns.GridColumn Fio;
        private DevExpress.XtraGrid.Columns.GridColumn HasRole1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn BrigID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditBrig;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditFio;
        private DevExpress.XtraGrid.Columns.GridColumn Password;
        private CustomButton customButtonUserPodr;
    }
}