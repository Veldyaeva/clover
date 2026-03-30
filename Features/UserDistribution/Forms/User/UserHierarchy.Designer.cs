using SewingProduction.Core.Class;
namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class UserHierarchy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserHierarchy));
            customGridControlAllProfile = new CustomGridControl();
            bindingSourceUsers = new System.Windows.Forms.BindingSource(components);
            gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            UserId = new DevExpress.XtraGrid.Columns.GridColumn();
            UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            Fio1 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemLookUpEditFio = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            Brig = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemLookUpEditBrig = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            Password = new DevExpress.XtraGrid.Columns.GridColumn();
            FioID = new DevExpress.XtraGrid.Columns.GridColumn();
            BrigID = new DevExpress.XtraGrid.Columns.GridColumn();
            customButtonAddProfile = new CustomButton();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            Fio = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)customGridControlAllProfile).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditFio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditBrig).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // customGridControlAllProfile
            // 
            customGridControlAllProfile.DataSource = bindingSourceUsers;
            customGridControlAllProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlAllProfile.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlAllProfile.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlAllProfile.Location = new System.Drawing.Point(4, 3);
            customGridControlAllProfile.MainView = gridViewUsers;
            customGridControlAllProfile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlAllProfile.Name = "customGridControlAllProfile";
            customGridControlAllProfile.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemLookUpEditBrig, repositoryItemLookUpEditFio });
            tableLayoutPanel1.SetRowSpan(customGridControlAllProfile, 10);
            customGridControlAllProfile.Size = new System.Drawing.Size(1150, 618);
            customGridControlAllProfile.TabIndex = 0;
            customGridControlAllProfile.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUsers });
            customGridControlAllProfile.Load += customGridControlUsers_Load;
            // 
            // gridViewUsers
            // 
            gridViewUsers.Appearance.DetailTip.BackColor = System.Drawing.Color.Transparent;
            gridViewUsers.Appearance.DetailTip.Options.UseBackColor = true;
            gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { UserId, UserName, Fio1, Brig, Password, FioID, BrigID });
            gridViewUsers.DetailHeight = 900;
            gridViewUsers.GridControl = customGridControlAllProfile;
            gridViewUsers.Name = "gridViewUsers";
            gridViewUsers.OptionsBehavior.Editable = false;
            gridViewUsers.OptionsDetail.SmartDetailHeight = true;
            gridViewUsers.OptionsEditForm.EditFormColumnCount = 1;
            gridViewUsers.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewUsers.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewUsers.OptionsView.RowAutoHeight = true;
            gridViewUsers.OptionsView.ShowChildrenInGroupPanel = true;
            gridViewUsers.OptionsView.ShowGroupPanel = false;
            gridViewUsers.InitNewRow += gridViewUsers_InitNewRow;
            gridViewUsers.RowUpdated += gridViewUsers_RowUpdated;
            // 
            // UserId
            // 
            UserId.Caption = "ИД";
            UserId.FieldName = "UserId";
            UserId.MinWidth = 23;
            UserId.Name = "UserId";
            UserId.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            UserId.Visible = true;
            UserId.VisibleIndex = 0;
            UserId.Width = 93;
            // 
            // UserName
            // 
            UserName.Caption = "Имя пользователя";
            UserName.FieldName = "UserName";
            UserName.MinWidth = 23;
            UserName.Name = "UserName";
            UserName.Visible = true;
            UserName.VisibleIndex = 1;
            UserName.Width = 317;
            // 
            // Fio1
            // 
            Fio1.Caption = "ФИО";
            Fio1.ColumnEdit = repositoryItemLookUpEditFio;
            Fio1.FieldName = "FioID";
            Fio1.MinWidth = 23;
            Fio1.Name = "Fio1";
            Fio1.Visible = true;
            Fio1.VisibleIndex = 2;
            Fio1.Width = 510;
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
            // Brig
            // 
            Brig.Caption = "Бригада";
            Brig.ColumnEdit = repositoryItemLookUpEditBrig;
            Brig.FieldName = "BrigID";
            Brig.MinWidth = 23;
            Brig.Name = "Brig";
            Brig.Visible = true;
            Brig.VisibleIndex = 3;
            Brig.Width = 371;
            // 
            // repositoryItemLookUpEditBrig
            // 
            repositoryItemLookUpEditBrig.AutoHeight = false;
            repositoryItemLookUpEditBrig.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemLookUpEditBrig.DisplayMember = "Brig";
            repositoryItemLookUpEditBrig.Name = "repositoryItemLookUpEditBrig";
            repositoryItemLookUpEditBrig.NullText = "-";
            repositoryItemLookUpEditBrig.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            repositoryItemLookUpEditBrig.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditBrig.ValueMember = "BrigID";
            // 
            // Password
            // 
            Password.Caption = "Password";
            Password.FieldName = "Password";
            Password.MaxWidth = 1;
            Password.MinWidth = 12;
            Password.Name = "Password";
            Password.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            Password.Width = 12;
            // 
            // FioID
            // 
            FioID.Caption = "FioID";
            FioID.FieldName = "FioID";
            FioID.MinWidth = 23;
            FioID.Name = "FioID";
            FioID.Visible = true;
            FioID.VisibleIndex = 4;
            FioID.Width = 87;
            // 
            // BrigID
            // 
            BrigID.Caption = "BrigID";
            BrigID.FieldName = "BrigID";
            BrigID.MinWidth = 23;
            BrigID.Name = "BrigID";
            BrigID.Visible = true;
            BrigID.VisibleIndex = 5;
            BrigID.Width = 87;
            // 
            // customButtonAddProfile
            // 
            // customButtonAddProfile.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonAddProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonAddProfile.Font = new System.Drawing.Font("Arial", 10F);
            // customButtonAddProfile.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonAddProfile.Location = new System.Drawing.Point(1162, 3);
            customButtonAddProfile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonAddProfile.Name = "customButtonAddProfile";
            customButtonAddProfile.Size = new System.Drawing.Size(121, 56);
            customButtonAddProfile.TabIndex = 1;
            customButtonAddProfile.Text = "Добавить профиль";
            customButtonAddProfile.UseVisualStyleBackColor = false;
            customButtonAddProfile.Click += customButtonAddProfile_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.Controls.Add(customGridControlAllProfile, 0, 0);
            tableLayoutPanel1.Controls.Add(customButtonAddProfile, 1, 0);
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
            tableLayoutPanel1.Size = new System.Drawing.Size(1287, 624);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // RoleName
            // 
            RoleName.Caption = "Название";
            RoleName.FieldName = "RoleName";
            RoleName.Name = "RoleName";
            RoleName.Visible = true;
            RoleName.VisibleIndex = 0;
            RoleName.Width = 255;
            // 
            // gcCertGrupmen_name1
            // 
            gridColumn1.Name = "gridColumn1";
            // 
            // gcCertTsn_name1
            // 
            gridColumn2.Name = "gridColumn2";
            // 
            // gcCertTb_id1
            // 
            gridColumn3.Name = "gridColumn3";
            // 
            // gcCertArticul1
            // 
            gridColumn4.Name = "gridColumn4";
            // 
            // Fio
            // 
            Fio.Name = "Fio";
            // 
            // UserHierarchy
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1287, 624);
            Controls.Add(tableLayoutPanel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "UserHierarchy";
            Text = "Иерархия пользователей";
            Load += UsersHierarchy_Load;
            ((System.ComponentModel.ISupportInitialize)customGridControlAllProfile).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditFio).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditBrig).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSourceUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private CustomGridControl customGridControlAllProfile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButtonAddProfile;
        private DevExpress.XtraGrid.Columns.GridColumn UserId;
        private DevExpress.XtraGrid.Columns.GridColumn UserName;
        private DevExpress.XtraGrid.Columns.GridColumn Brig;
        private DevExpress.XtraGrid.Columns.GridColumn RoleName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn Fio;
        private DevExpress.XtraGrid.Columns.GridColumn FIO1;
        private DevExpress.XtraGrid.Columns.GridColumn Password;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditFio;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditBrig;
        private DevExpress.XtraGrid.Columns.GridColumn FioID;
        private DevExpress.XtraGrid.Columns.GridColumn BrigID;
        private DevExpress.XtraGrid.Columns.GridColumn Fio1;
    }
}