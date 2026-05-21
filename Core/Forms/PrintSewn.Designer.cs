namespace SewingProduction.Core.Forms
{
    partial class PrintSewn
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintSewn));
			customHeaderLabel1 = new SewingProduction.Core.Class.CustomHeaderLabel();
			customLayoutControl1 = new SewingProduction.Core.Class.CustomLayoutControl();
			customGridControlR = new SewingProduction.Core.Class.CustomGridControl();
			gridViewR = new DevExpress.XtraGrid.Views.Grid.GridView();
			IsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			repositoryItemCheckEditPodr = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			KodIzd = new DevExpress.XtraGrid.Columns.GridColumn();
			Articul = new DevExpress.XtraGrid.Columns.GridColumn();
			Mod = new DevExpress.XtraGrid.Columns.GridColumn();
			NomZad = new DevExpress.XtraGrid.Columns.GridColumn();
			NomPach = new DevExpress.XtraGrid.Columns.GridColumn();
			NPach = new DevExpress.XtraGrid.Columns.GridColumn();
			customSimpleButtonNabor = new SewingProduction.Core.Class.CustomSimpleButton();
			customSimpleButtonKompl = new SewingProduction.Core.Class.CustomSimpleButton();
			customSimpleButtonACE = new SewingProduction.Core.Class.CustomSimpleButton();
			Root = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
			RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
			DescriptionRole = new DevExpress.XtraGrid.Columns.GridColumn();
			RoleID = new DevExpress.XtraGrid.Columns.GridColumn();
			RoleTableID = new DevExpress.XtraGrid.Columns.GridColumn();
			UserRoleID = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)customLayoutControl1).BeginInit();
			customLayoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)customGridControlR).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewR).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditPodr).BeginInit();
			((System.ComponentModel.ISupportInitialize)Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
			SuspendLayout();
			// 
			// customHeaderLabel1
			// 
			customHeaderLabel1.Appearance.Font = new System.Drawing.Font("Arial", 17F, System.Drawing.FontStyle.Bold);
			customHeaderLabel1.Appearance.Options.UseFont = true;
			customHeaderLabel1.Appearance.Options.UseTextOptions = true;
			customHeaderLabel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			customHeaderLabel1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			customHeaderLabel1.Location = new System.Drawing.Point(12, 12);
			customHeaderLabel1.Name = "customHeaderLabel1";
			customHeaderLabel1.Size = new System.Drawing.Size(174, 27);
			customHeaderLabel1.StyleController = customLayoutControl1;
			customHeaderLabel1.TabIndex = 1;
			customHeaderLabel1.Text = "Печать вшивок";
			customHeaderLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// customLayoutControl1
			// 
			customLayoutControl1.Controls.Add(customGridControlR);
			customLayoutControl1.Controls.Add(customSimpleButtonNabor);
			customLayoutControl1.Controls.Add(customSimpleButtonKompl);
			customLayoutControl1.Controls.Add(customHeaderLabel1);
			customLayoutControl1.Controls.Add(customSimpleButtonACE);
			customLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			customLayoutControl1.Font = new System.Drawing.Font("Arial", 10F);
			customLayoutControl1.Location = new System.Drawing.Point(0, 0);
			customLayoutControl1.Name = "customLayoutControl1";
			customLayoutControl1.Root = Root;
			customLayoutControl1.Size = new System.Drawing.Size(795, 497);
			customLayoutControl1.TabIndex = 2;
			customLayoutControl1.Text = "customLayoutControl1";
			// 
			// customGridControlR
			// 
			customGridControlR.Font = new System.Drawing.Font("Arial", 10F);
			customGridControlR.Location = new System.Drawing.Point(12, 185);
			customGridControlR.MainView = gridViewR;
			customGridControlR.Name = "customGridControlR";
			customGridControlR.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEditPodr });
			customGridControlR.Size = new System.Drawing.Size(771, 290);
			customGridControlR.TabIndex = 3;
			customGridControlR.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewR });
			// 
			// gridViewR
			// 
			gridViewR.Appearance.EvenRow.Options.UseBackColor = true;
			gridViewR.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			gridViewR.Appearance.FocusedRow.Options.UseBackColor = true;
			gridViewR.Appearance.FocusedRow.Options.UseFont = true;
			gridViewR.Appearance.GroupFooter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewR.Appearance.GroupFooter.Options.UseFont = true;
			gridViewR.Appearance.GroupRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewR.Appearance.GroupRow.Options.UseFont = true;
			gridViewR.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			gridViewR.Appearance.HeaderPanel.Options.UseFont = true;
			gridViewR.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewR.Appearance.Row.Options.UseFont = true;
			gridViewR.Appearance.TopNewRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			gridViewR.Appearance.TopNewRow.Options.UseFont = true;
			gridViewR.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { IsSelected, KodIzd, Articul, Mod, NomZad, NomPach, NPach });
			gridViewR.GridControl = customGridControlR;
			gridViewR.Name = "gridViewR";
			gridViewR.OptionsView.EnableAppearanceEvenRow = true;
			gridViewR.OptionsView.ShowGroupedColumns = true;
			gridViewR.RowCellClick += gridViewR_RowCellClick;
			// 
			// IsSelected
			// 
			IsSelected.Caption = "Выбор";
			IsSelected.ColumnEdit = repositoryItemCheckEditPodr;
			IsSelected.FieldName = "IsSelected";
			IsSelected.Name = "IsSelected";
			IsSelected.Visible = true;
			IsSelected.VisibleIndex = 0;
			// 
			// repositoryItemCheckEditPodr
			// 
			repositoryItemCheckEditPodr.AutoHeight = false;
			repositoryItemCheckEditPodr.Name = "repositoryItemCheckEditPodr";
			repositoryItemCheckEditPodr.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			// 
			// KodIzd
			// 
			KodIzd.Caption = "Код";
			KodIzd.FieldName = "kod_izd";
			KodIzd.Name = "KodIzd";
			KodIzd.OptionsColumn.AllowEdit = false;
			KodIzd.Visible = true;
			KodIzd.VisibleIndex = 1;
			KodIzd.Width = 77;
			// 
			// Articul
			// 
			Articul.Caption = "Артикул";
			Articul.FieldName = "articul";
			Articul.Name = "Articul";
			Articul.OptionsColumn.AllowEdit = false;
			Articul.Visible = true;
			Articul.VisibleIndex = 2;
			Articul.Width = 121;
			// 
			// Mod
			// 
			Mod.Caption = "Модель";
			Mod.FieldName = "mod";
			Mod.Name = "Mod";
			Mod.OptionsColumn.AllowEdit = false;
			Mod.Visible = true;
			Mod.VisibleIndex = 3;
			Mod.Width = 121;
			// 
			// NomZad
			// 
			NomZad.Caption = "Ном. Зад.";
			NomZad.FieldName = "nom_zad";
			NomZad.Name = "NomZad";
			NomZad.OptionsColumn.AllowEdit = false;
			NomZad.Visible = true;
			NomZad.VisibleIndex = 4;
			NomZad.Width = 121;
			// 
			// NomPach
			// 
			NomPach.Caption = "Ном. Пач.";
			NomPach.FieldName = "nom_pach";
			NomPach.Name = "NomPach";
			NomPach.OptionsColumn.AllowEdit = false;
			NomPach.Visible = true;
			NomPach.VisibleIndex = 5;
			NomPach.Width = 128;
			// 
			// NPach
			// 
			NPach.Caption = "Пачка";
			NPach.FieldName = "n_pach_nz";
			NPach.Name = "NPach";
			NPach.Visible = true;
			NPach.VisibleIndex = 6;
			// 
			// customSimpleButtonNabor
			// 
			customSimpleButtonNabor.Appearance.Font = new System.Drawing.Font("Arial", 10F);
			customSimpleButtonNabor.Appearance.Options.UseFont = true;
			customSimpleButtonNabor.Location = new System.Drawing.Point(12, 144);
			customSimpleButtonNabor.Name = "customSimpleButtonNabor";
			customSimpleButtonNabor.Size = new System.Drawing.Size(296, 37);
			customSimpleButtonNabor.StyleController = customLayoutControl1;
			customSimpleButtonNabor.TabIndex = 3;
			customSimpleButtonNabor.Text = "Набор одежды";
			customSimpleButtonNabor.Click += customSimpleButtonNabor_Click;
			// 
			// customSimpleButtonKompl
			// 
			customSimpleButtonKompl.Appearance.Font = new System.Drawing.Font("Arial", 10F);
			customSimpleButtonKompl.Appearance.Options.UseFont = true;
			customSimpleButtonKompl.Location = new System.Drawing.Point(12, 104);
			customSimpleButtonKompl.Name = "customSimpleButtonKompl";
			customSimpleButtonKompl.Size = new System.Drawing.Size(296, 36);
			customSimpleButtonKompl.StyleController = customLayoutControl1;
			customSimpleButtonKompl.TabIndex = 2;
			customSimpleButtonKompl.Text = "Комплекты";
			customSimpleButtonKompl.Click += customSimpleButtonKompl_Click;
			// 
			// customSimpleButtonACE
			// 
			customSimpleButtonACE.Appearance.Font = new System.Drawing.Font("Arial", 10F);
			customSimpleButtonACE.Appearance.Options.UseFont = true;
			customSimpleButtonACE.Location = new System.Drawing.Point(12, 64);
			customSimpleButtonACE.Name = "customSimpleButtonACE";
			customSimpleButtonACE.Size = new System.Drawing.Size(296, 36);
			customSimpleButtonACE.StyleController = customLayoutControl1;
			customSimpleButtonACE.TabIndex = 0;
			customSimpleButtonACE.Text = "широкие ЭЙС, Клевер (новый)";
			customSimpleButtonACE.Click += customSimpleButtonACE_Click;
			// 
			// Root
			// 
			Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			Root.GroupBordersVisible = false;
			Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, layoutControlItem3, layoutControlItem4, emptySpaceItem2, emptySpaceItem6, emptySpaceItem7, emptySpaceItem8, layoutControlItem6, emptySpaceItem1 });
			Root.Name = "Root";
			Root.Size = new System.Drawing.Size(795, 497);
			Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			layoutControlItem1.Control = customSimpleButtonACE;
			layoutControlItem1.Location = new System.Drawing.Point(0, 52);
			layoutControlItem1.MaxSize = new System.Drawing.Size(300, 40);
			layoutControlItem1.MinSize = new System.Drawing.Size(300, 40);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new System.Drawing.Size(300, 40);
			layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			layoutControlItem2.Control = customHeaderLabel1;
			layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Size = new System.Drawing.Size(775, 31);
			layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			layoutControlItem3.Control = customSimpleButtonKompl;
			layoutControlItem3.Location = new System.Drawing.Point(0, 92);
			layoutControlItem3.MaxSize = new System.Drawing.Size(300, 40);
			layoutControlItem3.MinSize = new System.Drawing.Size(300, 40);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new System.Drawing.Size(300, 40);
			layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			layoutControlItem4.Control = customSimpleButtonNabor;
			layoutControlItem4.Location = new System.Drawing.Point(0, 132);
			layoutControlItem4.MaxSize = new System.Drawing.Size(300, 41);
			layoutControlItem4.MinSize = new System.Drawing.Size(300, 41);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new System.Drawing.Size(300, 41);
			layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem4.TextVisible = false;
			// 
			// emptySpaceItem2
			// 
			emptySpaceItem2.Location = new System.Drawing.Point(300, 92);
			emptySpaceItem2.Name = "emptySpaceItem2";
			emptySpaceItem2.Size = new System.Drawing.Size(475, 40);
			// 
			// emptySpaceItem6
			// 
			emptySpaceItem6.Location = new System.Drawing.Point(300, 132);
			emptySpaceItem6.Name = "emptySpaceItem6";
			emptySpaceItem6.Size = new System.Drawing.Size(475, 41);
			// 
			// emptySpaceItem7
			// 
			emptySpaceItem7.Location = new System.Drawing.Point(300, 52);
			emptySpaceItem7.Name = "emptySpaceItem7";
			emptySpaceItem7.Size = new System.Drawing.Size(475, 40);
			// 
			// emptySpaceItem8
			// 
			emptySpaceItem8.Location = new System.Drawing.Point(0, 31);
			emptySpaceItem8.Name = "emptySpaceItem8";
			emptySpaceItem8.Size = new System.Drawing.Size(775, 21);
			// 
			// layoutControlItem6
			// 
			layoutControlItem6.Control = customGridControlR;
			layoutControlItem6.Location = new System.Drawing.Point(0, 173);
			layoutControlItem6.Name = "layoutControlItem6";
			layoutControlItem6.Size = new System.Drawing.Size(775, 294);
			layoutControlItem6.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			emptySpaceItem1.Location = new System.Drawing.Point(0, 467);
			emptySpaceItem1.Name = "emptySpaceItem1";
			emptySpaceItem1.Size = new System.Drawing.Size(775, 10);
			// 
			// gridColumn1
			// 
			gridColumn1.Name = "gridColumn1";
			// 
			// emptySpaceItem3
			// 
			emptySpaceItem3.Location = new System.Drawing.Point(298, 31);
			emptySpaceItem3.Name = "emptySpaceItem2";
			emptySpaceItem3.Size = new System.Drawing.Size(299, 40);
			// 
			// emptySpaceItem4
			// 
			emptySpaceItem4.Location = new System.Drawing.Point(298, 71);
			emptySpaceItem4.Name = "emptySpaceItem2";
			emptySpaceItem4.Size = new System.Drawing.Size(299, 26);
			// 
			// emptySpaceItem5
			// 
			emptySpaceItem5.Location = new System.Drawing.Point(0, 453);
			emptySpaceItem5.Name = "emptySpaceItem2";
			emptySpaceItem5.Size = new System.Drawing.Size(597, 24);
			// 
			// RoleName
			// 
			RoleName.Caption = "Роль";
			RoleName.FieldName = "RoleName";
			RoleName.Name = "RoleName";
			RoleName.OptionsColumn.AllowEdit = false;
			RoleName.OptionsColumn.ReadOnly = true;
			RoleName.Visible = true;
			RoleName.VisibleIndex = 1;
			RoleName.Width = 127;
			// 
			// DescriptionRole
			// 
			DescriptionRole.Caption = "Описание";
			DescriptionRole.FieldName = "Description";
			DescriptionRole.Name = "DescriptionRole";
			DescriptionRole.OptionsColumn.AllowEdit = false;
			DescriptionRole.OptionsColumn.ReadOnly = true;
			DescriptionRole.Visible = true;
			DescriptionRole.VisibleIndex = 2;
			DescriptionRole.Width = 112;
			// 
			// RoleID
			// 
			RoleID.Caption = "RoleID";
			RoleID.FieldName = "RoleID";
			RoleID.Name = "RoleID";
			RoleID.OptionsColumn.AllowEdit = false;
			RoleID.OptionsColumn.ReadOnly = true;
			RoleID.Width = 31;
			// 
			// RoleTableID
			// 
			RoleTableID.Caption = "RoleTableID";
			RoleTableID.FieldName = "RoleTableID";
			RoleTableID.Name = "RoleTableID";
			RoleTableID.OptionsColumn.AllowEdit = false;
			RoleTableID.OptionsColumn.ReadOnly = true;
			RoleTableID.Width = 24;
			// 
			// UserRoleID
			// 
			UserRoleID.Caption = "UserRoleID";
			UserRoleID.FieldName = "UserRoleID";
			UserRoleID.Name = "UserRoleID";
			UserRoleID.OptionsColumn.AllowEdit = false;
			UserRoleID.OptionsColumn.ReadOnly = true;
			UserRoleID.Width = 24;
			// 
			// PrintSewn
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(795, 497);
			Controls.Add(customLayoutControl1);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			Name = "PrintSewn";
			Text = "Печать вшивок";
			Load += PrintSewn_Load;
			((System.ComponentModel.ISupportInitialize)customLayoutControl1).EndInit();
			customLayoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)customGridControlR).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewR).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditPodr).EndInit();
			((System.ComponentModel.ISupportInitialize)Root).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Class.CustomHeaderLabel customHeaderLabel1;
        private Class.CustomLayoutControl customLayoutControl1;
        private Class.CustomSimpleButton customSimpleButtonACE;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Class.CustomSimpleButton customSimpleButtonNabor;
        private Class.CustomSimpleButton customSimpleButtonKompl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private Class.CustomGridControl customGridControlR;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewR;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditPodr;
		private DevExpress.XtraGrid.Columns.GridColumn RoleName;
		private DevExpress.XtraGrid.Columns.GridColumn DescriptionRole;
		private DevExpress.XtraGrid.Columns.GridColumn RoleID;
		private DevExpress.XtraGrid.Columns.GridColumn RoleTableID;
		private DevExpress.XtraGrid.Columns.GridColumn UserRoleID;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraGrid.Columns.GridColumn KodIzd;
		private DevExpress.XtraGrid.Columns.GridColumn Articul;
		private DevExpress.XtraGrid.Columns.GridColumn Mod;
		private DevExpress.XtraGrid.Columns.GridColumn NomZad;
		private DevExpress.XtraGrid.Columns.GridColumn NomPach;
		private DevExpress.XtraGrid.Columns.GridColumn IsSelected;
		private DevExpress.XtraGrid.Columns.GridColumn NPach;
	}
}