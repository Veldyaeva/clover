namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class ShareUser
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
			layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			customButtonOk = new SewingProduction.Core.Class.CustomButton();
			customGridControlChild = new SewingProduction.Core.Class.CustomGridControl();
			gridViewChild = new DevExpress.XtraGrid.Views.Grid.GridView();
			UserID1 = new DevExpress.XtraGrid.Columns.GridColumn();
			UserName1 = new DevExpress.XtraGrid.Columns.GridColumn();
			FioID1 = new DevExpress.XtraGrid.Columns.GridColumn();
			Fio1 = new DevExpress.XtraGrid.Columns.GridColumn();
			IsSelected1 = new DevExpress.XtraGrid.Columns.GridColumn();
			repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			customGridControlUser = new SewingProduction.Core.Class.CustomGridControl();
			gridViewUser = new DevExpress.XtraGrid.Views.Grid.GridView();
			UserID = new DevExpress.XtraGrid.Columns.GridColumn();
			UserName = new DevExpress.XtraGrid.Columns.GridColumn();
			FioID = new DevExpress.XtraGrid.Columns.GridColumn();
			Fio = new DevExpress.XtraGrid.Columns.GridColumn();
			IsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			repositoryItemCheckEditUser = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			Root = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)customGridControlChild).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewChild).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)customGridControlUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
			SuspendLayout();
			// 
			// layoutControl1
			// 
			layoutControl1.Controls.Add(customButtonOk);
			layoutControl1.Controls.Add(customGridControlChild);
			layoutControl1.Controls.Add(customGridControlUser);
			layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			layoutControl1.Location = new System.Drawing.Point(0, 0);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = Root;
			layoutControl1.Size = new System.Drawing.Size(749, 641);
			layoutControl1.TabIndex = 1;
			layoutControl1.Text = "layoutControl1";
			// 
			// customButtonOk
			// 
			customButtonOk.Font = new System.Drawing.Font("Arial", 10F);
			customButtonOk.Location = new System.Drawing.Point(520, 586);
			customButtonOk.Name = "customButtonOk";
			customButtonOk.Size = new System.Drawing.Size(205, 31);
			customButtonOk.TabIndex = 3;
			customButtonOk.Text = "Сохранить и выйти";
			customButtonOk.UseVisualStyleBackColor = false;
			customButtonOk.Click += customButtonOk_Click;
			// 
			// customGridControlChild
			// 
			customGridControlChild.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			customGridControlChild.Location = new System.Drawing.Point(24, 317);
			customGridControlChild.MainView = gridViewChild;
			customGridControlChild.Name = "customGridControlChild";
			customGridControlChild.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEdit1 });
			customGridControlChild.Size = new System.Drawing.Size(701, 265);
			customGridControlChild.TabIndex = 2;
			customGridControlChild.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewChild });
			// 
			// gridViewChild
			// 
			gridViewChild.Appearance.EvenRow.Options.UseBackColor = true;
			gridViewChild.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			gridViewChild.Appearance.FocusedRow.Options.UseBackColor = true;
			gridViewChild.Appearance.FocusedRow.Options.UseFont = true;
			gridViewChild.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			gridViewChild.Appearance.HeaderPanel.Options.UseFont = true;
			gridViewChild.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewChild.Appearance.Row.Options.UseFont = true;
			gridViewChild.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { UserID1, UserName1, FioID1, Fio1, IsSelected1 });
			gridViewChild.GridControl = customGridControlChild;
			gridViewChild.Name = "gridViewChild";
			gridViewChild.OptionsView.EnableAppearanceEvenRow = true;
			gridViewChild.CellValueChanging += gridViewChild_CellValueChanging;
			// 
			// UserID1
			// 
			UserID1.Caption = "UserID";
			UserID1.FieldName = "UserID";
			UserID1.Name = "UserID1";
			UserID1.OptionsColumn.AllowEdit = false;
			UserID1.OptionsColumn.ReadOnly = true;
			UserID1.Width = 27;
			// 
			// UserName1
			// 
			UserName1.Caption = "Логин";
			UserName1.FieldName = "UserName";
			UserName1.Name = "UserName1";
			UserName1.OptionsColumn.AllowEdit = false;
			UserName1.OptionsColumn.ReadOnly = true;
			UserName1.Visible = true;
			UserName1.VisibleIndex = 0;
			UserName1.Width = 473;
			// 
			// FioID1
			// 
			FioID1.Caption = "FioID";
			FioID1.FieldName = "FioID";
			FioID1.Name = "FioID1";
			FioID1.OptionsColumn.AllowEdit = false;
			FioID1.OptionsColumn.ReadOnly = true;
			FioID1.Width = 26;
			// 
			// Fio1
			// 
			Fio1.Caption = "ФИО";
			Fio1.FieldName = "Fio";
			Fio1.Name = "Fio1";
			Fio1.OptionsColumn.AllowEdit = false;
			Fio1.OptionsColumn.ReadOnly = true;
			Fio1.Width = 200;
			// 
			// IsSelected1
			// 
			IsSelected1.Caption = "Выбор";
			IsSelected1.ColumnEdit = repositoryItemCheckEdit1;
			IsSelected1.FieldName = "IsSelected";
			IsSelected1.Name = "IsSelected1";
			IsSelected1.Visible = true;
			IsSelected1.VisibleIndex = 1;
			IsSelected1.Width = 203;
			// 
			// repositoryItemCheckEdit1
			// 
			repositoryItemCheckEdit1.AutoHeight = false;
			repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			repositoryItemCheckEdit1.ValueGrayed = false;
			// 
			// customGridControlUser
			// 
			customGridControlUser.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			customGridControlUser.Location = new System.Drawing.Point(24, 45);
			customGridControlUser.MainView = gridViewUser;
			customGridControlUser.Name = "customGridControlUser";
			customGridControlUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEditUser });
			customGridControlUser.Size = new System.Drawing.Size(701, 223);
			customGridControlUser.TabIndex = 0;
			customGridControlUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUser });
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
			UserName.Width = 470;
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
			Fio.Width = 200;
			// 
			// IsSelected
			// 
			IsSelected.Caption = "Выбор";
			IsSelected.ColumnEdit = repositoryItemCheckEditUser;
			IsSelected.FieldName = "IsSelected";
			IsSelected.Name = "IsSelected";
			IsSelected.Visible = true;
			IsSelected.VisibleIndex = 1;
			IsSelected.Width = 206;
			// 
			// repositoryItemCheckEditUser
			// 
			repositoryItemCheckEditUser.AutoHeight = false;
			repositoryItemCheckEditUser.Name = "repositoryItemCheckEditUser";
			repositoryItemCheckEditUser.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			repositoryItemCheckEditUser.ValueGrayed = false;
			// 
			// Root
			// 
			Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			Root.GroupBordersVisible = false;
			Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlGroup2 });
			Root.Name = "Root";
			Root.Size = new System.Drawing.Size(749, 641);
			Root.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
			layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Size = new System.Drawing.Size(729, 272);
			layoutControlGroup1.Text = "Выберите пользователя с которым хотите поделиться";
			// 
			// layoutControlItem1
			// 
			layoutControlItem1.Control = customGridControlUser;
			layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new System.Drawing.Size(705, 227);
			layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3, emptySpaceItem1 });
			layoutControlGroup2.Location = new System.Drawing.Point(0, 272);
			layoutControlGroup2.Name = "layoutControlGroup2";
			layoutControlGroup2.Size = new System.Drawing.Size(729, 349);
			layoutControlGroup2.Text = "Выберите пользователей которых хотите передать";
			// 
			// layoutControlItem2
			// 
			layoutControlItem2.Control = customGridControlChild;
			layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Size = new System.Drawing.Size(705, 269);
			layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			layoutControlItem3.Control = customButtonOk;
			layoutControlItem3.Location = new System.Drawing.Point(496, 269);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new System.Drawing.Size(209, 35);
			layoutControlItem3.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			emptySpaceItem1.Location = new System.Drawing.Point(0, 269);
			emptySpaceItem1.Name = "emptySpaceItem1";
			emptySpaceItem1.Size = new System.Drawing.Size(496, 35);
			// 
			// ShareUser
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(749, 641);
			Controls.Add(layoutControl1);
			IsMdiContainer = true;
			Name = "ShareUser";
			Text = "Поделиться пользователем";
			((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)customGridControlChild).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewChild).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)customGridControlUser).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewUser).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditUser).EndInit();
			((System.ComponentModel.ISupportInitialize)Root).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Core.Class.CustomGridControl customGridControlChild;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChild;
        private DevExpress.XtraGrid.Columns.GridColumn UserID1;
        private DevExpress.XtraGrid.Columns.GridColumn UserName1;
        private DevExpress.XtraGrid.Columns.GridColumn FioID1;
        private DevExpress.XtraGrid.Columns.GridColumn Fio1;
        private DevExpress.XtraGrid.Columns.GridColumn IsSelected1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private Core.Class.CustomGridControl customGridControlUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUser;
        private DevExpress.XtraGrid.Columns.GridColumn UserID;
        private DevExpress.XtraGrid.Columns.GridColumn UserName;
        private DevExpress.XtraGrid.Columns.GridColumn FioID;
        private DevExpress.XtraGrid.Columns.GridColumn Fio;
        private DevExpress.XtraGrid.Columns.GridColumn IsSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditUser;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Core.Class.CustomButton customButtonOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}