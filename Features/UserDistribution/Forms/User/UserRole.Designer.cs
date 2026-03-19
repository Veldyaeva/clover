using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Crmf;

namespace SewingProduction.Features.UserDistribution.Forms.User
{
    partial class UserRole
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
            customGridControlRole = new SewingProduction.Core.Class.CustomGridControl();
            gridViewRole = new DevExpress.XtraGrid.Views.Grid.GridView();
            IsSelectedRole = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEditPodr = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            NameRole = new DevExpress.XtraGrid.Columns.GridColumn();
            DescriptionRole = new DevExpress.XtraGrid.Columns.GridColumn();
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
            layoutControlGroupRole = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControlRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditPodr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(customGridControlRole);
            layoutControl1.Controls.Add(customGridControlUser);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(580, 335, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1203, 629);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // customGridControlRole
            // 
            customGridControlRole.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlRole.Location = new System.Drawing.Point(574, 45);
            customGridControlRole.MainView = gridViewRole;
            customGridControlRole.Name = "customGridControlRole";
            customGridControlRole.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEditPodr });
            customGridControlRole.Size = new System.Drawing.Size(605, 560);
            customGridControlRole.TabIndex = 5;
            customGridControlRole.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRole });
            // 
            // gridViewRole
            // 
            gridViewRole.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewRole.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewRole.Appearance.FocusedRow.Options.UseFont = true;
            gridViewRole.Appearance.GroupFooter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.GroupFooter.Options.UseFont = true;
            gridViewRole.Appearance.GroupRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.GroupRow.Options.UseFont = true;
            gridViewRole.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewRole.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.Row.Options.UseFont = true;
            gridViewRole.Appearance.TopNewRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.TopNewRow.Options.UseFont = true;
            gridViewRole.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { IsSelectedRole, NameRole, DescriptionRole, PodrID, PodrTableID, UserPodrID });
            gridViewRole.GridControl = customGridControlRole;
            gridViewRole.Name = "gridViewRole";
            gridViewRole.OptionsView.EnableAppearanceEvenRow = true;
            gridViewRole.OptionsView.ShowGroupedColumns = true;
            gridViewRole.RowClick += gridViewPodr_RowClick;
            gridViewRole.CustomDrawGroupRow += gridViewPodr_CustomDrawGroupRow;
            gridViewRole.CellValueChanged += gridViewPodr_CellValueChanged;
            gridViewRole.KeyDown += gridViewPodr_KeyDown;
            // 
            // IsSelectedRole
            // 
            IsSelectedRole.Caption = "Выбор";
            IsSelectedRole.ColumnEdit = repositoryItemCheckEditPodr;
            IsSelectedRole.FieldName = "IsSelected";
            IsSelectedRole.Name = "IsSelectedRole";
            IsSelectedRole.Visible = true;
            IsSelectedRole.VisibleIndex = 0;
            IsSelectedRole.Width = 102;
            // 
            // repositoryItemCheckEditPodr
            // 
            repositoryItemCheckEditPodr.AutoHeight = false;
            repositoryItemCheckEditPodr.Name = "repositoryItemCheckEditPodr";
            repositoryItemCheckEditPodr.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEditPodr.CheckedChanged += repositoryItemCheckEditPodr_CheckedChanged;
            // 
            // NameRole
            // 
            NameRole.Caption = "Роль";
            NameRole.FieldName = "Name";
            NameRole.Name = "NameRole";
            NameRole.OptionsColumn.AllowEdit = false;
            NameRole.OptionsColumn.ReadOnly = true;
            NameRole.Visible = true;
            NameRole.VisibleIndex = 1;
            NameRole.Width = 216;
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
            DescriptionRole.Width = 183;
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
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroupUser, layoutControlGroupRole });
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
            // layoutControlGroupRole
            // 
            layoutControlGroupRole.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
            layoutControlGroupRole.Location = new System.Drawing.Point(550, 0);
            layoutControlGroupRole.Name = "layoutControlGroupRole";
            layoutControlGroupRole.Size = new System.Drawing.Size(633, 609);
            layoutControlGroupRole.Text = "Роли";
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = customGridControlRole;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(609, 564);
            layoutControlItem2.TextVisible = false;
            // 
            // UserRole
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1203, 629);
            Controls.Add(layoutControl1);
            Name = "UserRole";
            Text = "Администрирование табеля";
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customGridControlRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditPodr).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupRole).EndInit();
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
            private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRole;
            private Core.Class.CustomGridControl customGridControlRole;
            private DevExpress.XtraGrid.Views.Grid.GridView gridViewRole;
            private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
            private DevExpress.XtraGrid.Columns.GridColumn PodrID;
            private DevExpress.XtraGrid.Columns.GridColumn PodrTableID;
            private DevExpress.XtraGrid.Columns.GridColumn DescriptionRole;
            private DevExpress.XtraGrid.Columns.GridColumn NameRole;
            private DevExpress.XtraGrid.Columns.GridColumn IsSelectedRole;
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