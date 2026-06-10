namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class UserPodr
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPodr));
			layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			customGridControlPodr = new SewingProduction.Core.Class.CustomGridControl();
			gridViewPodr = new DevExpress.XtraGrid.Views.Grid.GridView();
			IsSelectedPodr = new DevExpress.XtraGrid.Columns.GridColumn();
			repositoryItemCheckEditPodr = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			NamePodr = new DevExpress.XtraGrid.Columns.GridColumn();
			PodrTableName = new DevExpress.XtraGrid.Columns.GridColumn();
			PodrID = new DevExpress.XtraGrid.Columns.GridColumn();
			PodrTableID = new DevExpress.XtraGrid.Columns.GridColumn();
			UserPodrID = new DevExpress.XtraGrid.Columns.GridColumn();
			customGridControlUser = new SewingProduction.Core.Class.CustomGridControl();
			gridViewUser = new DevExpress.XtraGrid.Views.Grid.GridView();
			UserID = new DevExpress.XtraGrid.Columns.GridColumn();
			UserName = new DevExpress.XtraGrid.Columns.GridColumn();
			FioID = new DevExpress.XtraGrid.Columns.GridColumn();
			Fio = new DevExpress.XtraGrid.Columns.GridColumn();
			IsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			repositoryItemCheckEditUser = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			Root = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlGroupUser = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroupPodr = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)customGridControlPodr).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewPodr).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditPodr).BeginInit();
			((System.ComponentModel.ISupportInitialize)customGridControlUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroupUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroupPodr).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
			SuspendLayout();
			// 
			// layoutControl1
			// 
			layoutControl1.Controls.Add(customGridControlPodr);
			layoutControl1.Controls.Add(customGridControlUser);
			layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			layoutControl1.Location = new System.Drawing.Point(0, 0);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(580, 335, 650, 400);
			layoutControl1.Root = Root;
			layoutControl1.Size = new System.Drawing.Size(1203, 629);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			// 
			// customGridControlPodr
			// 
			customGridControlPodr.Font = new System.Drawing.Font("Arial", 10F);
			customGridControlPodr.Location = new System.Drawing.Point(574, 45);
			customGridControlPodr.MainView = gridViewPodr;
			customGridControlPodr.Name = "customGridControlPodr";
			customGridControlPodr.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEditPodr });
			customGridControlPodr.Size = new System.Drawing.Size(605, 560);
			customGridControlPodr.TabIndex = 5;
			customGridControlPodr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewPodr });
			// 
			// gridViewPodr
			// 
			gridViewPodr.Appearance.EvenRow.Options.UseBackColor = true;
			gridViewPodr.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			gridViewPodr.Appearance.FocusedRow.Options.UseBackColor = true;
			gridViewPodr.Appearance.FocusedRow.Options.UseFont = true;
			gridViewPodr.Appearance.GroupFooter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewPodr.Appearance.GroupFooter.Options.UseFont = true;
			gridViewPodr.Appearance.GroupRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewPodr.Appearance.GroupRow.Options.UseFont = true;
			gridViewPodr.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			gridViewPodr.Appearance.HeaderPanel.Options.UseFont = true;
			gridViewPodr.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewPodr.Appearance.Row.Options.UseFont = true;
			gridViewPodr.Appearance.TopNewRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewPodr.Appearance.TopNewRow.Options.UseFont = true;
			gridViewPodr.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { IsSelectedPodr, NamePodr, PodrTableName, PodrID, PodrTableID, UserPodrID });
			gridViewPodr.GridControl = customGridControlPodr;
			gridViewPodr.Name = "gridViewPodr";
			gridViewPodr.OptionsView.EnableAppearanceEvenRow = true;
			gridViewPodr.OptionsView.ShowGroupedColumns = true;
			gridViewPodr.RowClick += gridViewPodr_RowClick;
			gridViewPodr.CustomDrawGroupRow += gridViewPodr_CustomDrawGroupRow;
			gridViewPodr.CellValueChanged += gridViewPodr_CellValueChanged;
			gridViewPodr.CellValueChanging += gridViewPodr_CellValueChanging;
			gridViewPodr.KeyDown += gridViewPodr_KeyDown;
			gridViewPodr.DoubleClick += gridViewPodr_DoubleClick;
			// 
			// IsSelectedPodr
			// 
			IsSelectedPodr.Caption = "Выбор";
			IsSelectedPodr.ColumnEdit = repositoryItemCheckEditPodr;
			IsSelectedPodr.FieldName = "IsSelected";
			IsSelectedPodr.Name = "IsSelectedPodr";
			IsSelectedPodr.Visible = true;
			IsSelectedPodr.VisibleIndex = 0;
			IsSelectedPodr.Width = 102;
			// 
			// repositoryItemCheckEditPodr
			// 
			repositoryItemCheckEditPodr.AutoHeight = false;
			repositoryItemCheckEditPodr.Name = "repositoryItemCheckEditPodr";
			repositoryItemCheckEditPodr.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			repositoryItemCheckEditPodr.CheckedChanged += repositoryItemCheckEditPodr_CheckedChanged;
			// 
			// NamePodr
			// 
			NamePodr.Caption = "Название подразделения";
			NamePodr.FieldName = "Name";
			NamePodr.Name = "NamePodr";
			NamePodr.OptionsColumn.AllowEdit = false;
			NamePodr.OptionsColumn.ReadOnly = true;
			NamePodr.Visible = true;
			NamePodr.VisibleIndex = 1;
			NamePodr.Width = 216;
			// 
			// PodrTableName
			// 
			PodrTableName.Caption = "Организация";
			PodrTableName.FieldName = "PodrTableName";
			PodrTableName.Name = "PodrTableName";
			PodrTableName.OptionsColumn.AllowEdit = false;
			PodrTableName.OptionsColumn.ReadOnly = true;
			PodrTableName.Visible = true;
			PodrTableName.VisibleIndex = 2;
			PodrTableName.Width = 183;
			// 
			// PodrID
			// 
			PodrID.Caption = "PodrID";
			PodrID.FieldName = "PodrID";
			PodrID.Name = "PodrID";
			PodrID.OptionsColumn.AllowEdit = false;
			PodrID.OptionsColumn.ReadOnly = true;
			PodrID.Width = 31;
			// 
			// PodrTableID
			// 
			PodrTableID.Caption = "PodrTableID";
			PodrTableID.FieldName = "PodrTableID";
			PodrTableID.Name = "PodrTableID";
			PodrTableID.OptionsColumn.AllowEdit = false;
			PodrTableID.OptionsColumn.ReadOnly = true;
			PodrTableID.Width = 24;
			// 
			// UserPodrID
			// 
			UserPodrID.Caption = "UserPodrID";
			UserPodrID.FieldName = "UserPodrID";
			UserPodrID.Name = "UserPodrID";
			UserPodrID.OptionsColumn.AllowEdit = false;
			UserPodrID.OptionsColumn.ReadOnly = true;
			UserPodrID.Width = 24;
			// 
			// customGridControlUser
			// 
			customGridControlUser.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			customGridControlUser.Location = new System.Drawing.Point(24, 45);
			customGridControlUser.MainView = gridViewUser;
			customGridControlUser.Name = "customGridControlUser";
			customGridControlUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEditUser });
			customGridControlUser.Size = new System.Drawing.Size(522, 560);
			customGridControlUser.TabIndex = 4;
			customGridControlUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUser });
			customGridControlUser.Click += customGridControlUser_Click;
			// 
			// gridViewUser
			// 
			gridViewUser.Appearance.EvenRow.Options.UseBackColor = true;
			gridViewUser.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			gridViewUser.Appearance.FocusedRow.Options.UseBackColor = true;
			gridViewUser.Appearance.FocusedRow.Options.UseFont = true;
			gridViewUser.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			gridViewUser.Appearance.HeaderPanel.Options.UseFont = true;
			gridViewUser.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewUser.Appearance.Row.Options.UseFont = true;
			gridViewUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { UserID, UserName, FioID, Fio, IsSelected });
			gridViewUser.GridControl = customGridControlUser;
			gridViewUser.Name = "gridViewUser";
			gridViewUser.OptionsView.EnableAppearanceEvenRow = true;
			gridViewUser.RowClick += gridViewUser_RowClick;
			gridViewUser.CellValueChanging += gridViewUser_CellValueChanging;
			gridViewUser.KeyDown += gridViewUser_KeyDown;
			// 
			// UserID
			// 
			UserID.Caption = "UserID";
			UserID.FieldName = "UserID";
			UserID.Name = "UserID";
			UserID.OptionsColumn.AllowEdit = false;
			UserID.OptionsColumn.ReadOnly = true;
			UserID.Width = 27;
			// 
			// UserName
			// 
			UserName.Caption = "Логин";
			UserName.FieldName = "UserName";
			UserName.Name = "UserName";
			UserName.OptionsColumn.AllowEdit = false;
			UserName.OptionsColumn.ReadOnly = true;
			UserName.Visible = true;
			UserName.VisibleIndex = 0;
			UserName.Width = 119;
			// 
			// FioID
			// 
			FioID.Caption = "FioID";
			FioID.FieldName = "FioID";
			FioID.Name = "FioID";
			FioID.OptionsColumn.AllowEdit = false;
			FioID.OptionsColumn.ReadOnly = true;
			FioID.Width = 26;
			// 
			// Fio
			// 
			Fio.Caption = "ФИО";
			Fio.FieldName = "Fio";
			Fio.Name = "Fio";
			Fio.OptionsColumn.AllowEdit = false;
			Fio.OptionsColumn.ReadOnly = true;
			Fio.Visible = true;
			Fio.VisibleIndex = 1;
			Fio.Width = 200;
			// 
			// IsSelected
			// 
			IsSelected.Caption = "Выбор";
			IsSelected.ColumnEdit = repositoryItemCheckEditUser;
			IsSelected.FieldName = "IsSelected";
			IsSelected.Name = "IsSelected";
			IsSelected.Visible = true;
			IsSelected.VisibleIndex = 2;
			IsSelected.Width = 125;
			// 
			// repositoryItemCheckEditUser
			// 
			repositoryItemCheckEditUser.AutoHeight = false;
			repositoryItemCheckEditUser.Name = "repositoryItemCheckEditUser";
			repositoryItemCheckEditUser.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			repositoryItemCheckEditUser.ValueGrayed = false;
			repositoryItemCheckEditUser.CheckedChanged += repositoryItemCheckEditUser_CheckedChanged;
			// 
			// Root
			// 
			Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			Root.GroupBordersVisible = false;
			Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroupUser, layoutControlGroupPodr });
			Root.Name = "Root";
			Root.Size = new System.Drawing.Size(1203, 629);
			Root.TextVisible = false;
			// 
			// layoutControlGroupUser
			// 
			layoutControlGroupUser.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
			layoutControlGroupUser.Location = new System.Drawing.Point(0, 0);
			layoutControlGroupUser.Name = "layoutControlGroupUser";
			layoutControlGroupUser.Size = new System.Drawing.Size(550, 609);
			layoutControlGroupUser.Text = "Пользователи";
			// 
			// layoutControlItem1
			// 
			layoutControlItem1.Control = customGridControlUser;
			layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new System.Drawing.Size(526, 564);
			layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroupPodr
			// 
			layoutControlGroupPodr.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
			layoutControlGroupPodr.Location = new System.Drawing.Point(550, 0);
			layoutControlGroupPodr.Name = "layoutControlGroupPodr";
			layoutControlGroupPodr.Size = new System.Drawing.Size(633, 609);
			layoutControlGroupPodr.Text = "Подразделения";
			// 
			// layoutControlItem2
			// 
			layoutControlItem2.Control = customGridControlPodr;
			layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Size = new System.Drawing.Size(609, 564);
			layoutControlItem2.TextVisible = false;
			// 
			// UserPodr
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1203, 629);
			Controls.Add(layoutControl1);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			Name = "UserPodr";
			Text = "Администрирование табеля";
			((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)customGridControlPodr).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewPodr).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditPodr).EndInit();
			((System.ComponentModel.ISupportInitialize)customGridControlUser).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewUser).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditUser).EndInit();
			((System.ComponentModel.ISupportInitialize)Root).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroupUser).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroupPodr).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Core.Class.CustomGridControl customGridControlUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUser;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupUser;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupPodr;
        private Core.Class.CustomGridControl customGridControlPodr;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPodr;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn PodrID;
        private DevExpress.XtraGrid.Columns.GridColumn PodrTableID;
        private DevExpress.XtraGrid.Columns.GridColumn PodrTableName;
        private DevExpress.XtraGrid.Columns.GridColumn NamePodr;
        private DevExpress.XtraGrid.Columns.GridColumn IsSelectedPodr;
        private DevExpress.XtraGrid.Columns.GridColumn UserID;
        private DevExpress.XtraGrid.Columns.GridColumn UserName;
        private DevExpress.XtraGrid.Columns.GridColumn FioID;
        private DevExpress.XtraGrid.Columns.GridColumn Fio;
        private DevExpress.XtraGrid.Columns.GridColumn IsSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditPodr;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditUser;
        private DevExpress.XtraGrid.Columns.GridColumn UserPodrID;
    }
}