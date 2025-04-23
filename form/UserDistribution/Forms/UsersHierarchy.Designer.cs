namespace SewingProduction.form.UserDistribution
{
    partial class UsersHierarchy
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
            this.customGridControlAllProfile = new SewingProduction.CustomGridControl();
            this.bindingSourceUsers = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.UserId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FIO1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BrigID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customButtonAddProfile = new SewingProduction.CustomButton();
            this.customButtonDeleteProfile = new SewingProduction.CustomButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Fio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Password = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlAllProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customGridControlAllProfile
            // 
            this.customGridControlAllProfile.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridControlAllProfile.DataSource = this.bindingSourceUsers;
            this.customGridControlAllProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControlAllProfile.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControlAllProfile.Location = new System.Drawing.Point(3, 3);
            this.customGridControlAllProfile.MainView = this.gridViewUsers;
            this.customGridControlAllProfile.Name = "customGridControlAllProfile";
            this.customGridControlAllProfile.ObjectName = null;
            this.tableLayoutPanel1.SetRowSpan(this.customGridControlAllProfile, 10);
            this.customGridControlAllProfile.Size = new System.Drawing.Size(1142, 641);
            this.customGridControlAllProfile.TabIndex = 0;
            this.customGridControlAllProfile.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers});
            this.customGridControlAllProfile.Load += new System.EventHandler(this.customGridControlUsers_Load);
            // 
            // gridViewUsers
            // 
            this.gridViewUsers.Appearance.DetailTip.BackColor = System.Drawing.Color.Transparent;
            this.gridViewUsers.Appearance.DetailTip.Options.UseBackColor = true;
            this.gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.UserId,
            this.UserName,
            this.FIO1,
            this.BrigID,
            this.Password});
            this.gridViewUsers.GridControl = this.customGridControlAllProfile;
            this.gridViewUsers.Name = "gridViewUsers";
            this.gridViewUsers.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewUsers.OptionsDetail.SmartDetailHeight = true;
            this.gridViewUsers.OptionsEditForm.EditFormColumnCount = 1;
            this.gridViewUsers.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewUsers.OptionsView.RowAutoHeight = true;
            this.gridViewUsers.OptionsView.ShowChildrenInGroupPanel = true;
            this.gridViewUsers.OptionsView.ShowGroupPanel = false;
            this.gridViewUsers.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridViewUsers_InitNewRow);
            this.gridViewUsers.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewUsers_RowUpdated);
            // 
            // UserId
            // 
            this.UserId.Caption = "ИД";
            this.UserId.FieldName = "UserId";
            this.UserId.Name = "UserId";
            this.UserId.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.UserId.Visible = true;
            this.UserId.VisibleIndex = 0;
            this.UserId.Width = 80;
            // 
            // UserName
            // 
            this.UserName.Caption = "Имя пользователя";
            this.UserName.FieldName = "UserName";
            this.UserName.Name = "UserName";
            this.UserName.Visible = true;
            this.UserName.VisibleIndex = 1;
            this.UserName.Width = 272;
            // 
            // FIO1
            // 
            this.FIO1.Caption = "ФИО";
            this.FIO1.FieldName = "Fio";
            this.FIO1.Name = "FIO1";
            this.FIO1.OptionsEditForm.Caption = "ФИО ID:";
            this.FIO1.Visible = true;
            this.FIO1.VisibleIndex = 2;
            this.FIO1.Width = 437;
            // 
            // BrigID
            // 
            this.BrigID.Caption = "Бригада";
            this.BrigID.FieldName = "BrigID";
            this.BrigID.Name = "BrigID";
            this.BrigID.OptionsEditForm.Caption = "Бригада ID:";
            this.BrigID.Visible = true;
            this.BrigID.VisibleIndex = 3;
            this.BrigID.Width = 318;
            // 
            // customButtonAddProfile
            // 
            this.customButtonAddProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonAddProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonAddProfile.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonAddProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonAddProfile.Location = new System.Drawing.Point(1151, 3);
            this.customButtonAddProfile.Name = "customButtonAddProfile";
            this.customButtonAddProfile.ObjectName = null;
            this.customButtonAddProfile.Size = new System.Drawing.Size(122, 58);
            this.customButtonAddProfile.TabIndex = 1;
            this.customButtonAddProfile.Text = "Добавить профиль";
            this.customButtonAddProfile.UseVisualStyleBackColor = false;
            this.customButtonAddProfile.Click += new System.EventHandler(this.customButtonAddProfile_Click);
            // 
            // customButtonDeleteProfile
            // 
            this.customButtonDeleteProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonDeleteProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonDeleteProfile.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonDeleteProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonDeleteProfile.Location = new System.Drawing.Point(1151, 579);
            this.customButtonDeleteProfile.Name = "customButtonDeleteProfile";
            this.customButtonDeleteProfile.ObjectName = null;
            this.customButtonDeleteProfile.Size = new System.Drawing.Size(122, 65);
            this.customButtonDeleteProfile.TabIndex = 2;
            this.customButtonDeleteProfile.Text = "Удалить профиль";
            this.customButtonDeleteProfile.UseVisualStyleBackColor = false;
            this.customButtonDeleteProfile.Click += new System.EventHandler(this.customButtonDeleteProfile_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.customGridControlAllProfile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customButtonAddProfile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customButtonDeleteProfile, 1, 9);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1276, 647);
            this.tableLayoutPanel1.TabIndex = 2;
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
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Название";
            this.gridColumn1.FieldName = "RoleName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 255;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Creator";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Creator";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Creator";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // Fio
            // 
            this.Fio.Caption = "Fio";
            this.Fio.Name = "Fio";
            this.Fio.Visible = true;
            this.Fio.VisibleIndex = 2;
            // 
            // Password
            // 
            this.Password.Caption = "Password";
            this.Password.FieldName = "Password";
            this.Password.MaxWidth = 1;
            this.Password.MinWidth = 10;
            this.Password.Name = "Password";
            this.Password.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            this.Password.Width = 10;
            // 
            // UsersHierarchy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 647);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UsersHierarchy";
            this.Text = "Иерархия пользователей";
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlAllProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSourceUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private CustomGridControl customGridControlAllProfile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButtonAddProfile;
        private CustomButton customButtonDeleteProfile;
        private DevExpress.XtraGrid.Columns.GridColumn UserId;
        private DevExpress.XtraGrid.Columns.GridColumn UserName;
        private DevExpress.XtraGrid.Columns.GridColumn BrigID;
        private DevExpress.XtraGrid.Columns.GridColumn RoleName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn Fio;
        private DevExpress.XtraGrid.Columns.GridColumn FIO1;
        private DevExpress.XtraGrid.Columns.GridColumn Password;
    }
}