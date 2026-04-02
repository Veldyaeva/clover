using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Crmf;

namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class RoleFormObject
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
            customGridControlObject = new SewingProduction.Core.Class.CustomGridControl();
            gridViewObject = new DevExpress.XtraGrid.Views.Grid.GridView();
            ObjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            ObjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            ObjectNameRus = new DevExpress.XtraGrid.Columns.GridColumn();
            HasAccessObject = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemComboBoxObject = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            customGridControlRole = new SewingProduction.Core.Class.CustomGridControl();
            gridViewRole = new DevExpress.XtraGrid.Views.Grid.GridView();
            IsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEditPodr = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            DescriptionRole = new DevExpress.XtraGrid.Columns.GridColumn();
            RoleID = new DevExpress.XtraGrid.Columns.GridColumn();
            RoleTableID = new DevExpress.XtraGrid.Columns.GridColumn();
            UserRoleID = new DevExpress.XtraGrid.Columns.GridColumn();
            customGridControlForm = new SewingProduction.Core.Class.CustomGridControl();
            gridViewForm = new DevExpress.XtraGrid.Views.Grid.GridView();
            ProjectFormsID = new DevExpress.XtraGrid.Columns.GridColumn();
            NameForm = new DevExpress.XtraGrid.Columns.GridColumn();
            NameFormRus = new DevExpress.XtraGrid.Columns.GridColumn();
            HasAccess = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemComboBoxForm = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroupObject = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroupRole = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroupForm = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControlObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditPodr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlForm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewForm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxForm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupForm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(customGridControlObject);
            layoutControl1.Controls.Add(customGridControlRole);
            layoutControl1.Controls.Add(customGridControlForm);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(580, 335, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1098, 629);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // customGridControlObject
            // 
            customGridControlObject.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            customGridControlObject.Location = new System.Drawing.Point(726, 45);
            customGridControlObject.MainView = gridViewObject;
            customGridControlObject.Name = "customGridControlObject";
            customGridControlObject.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemComboBoxObject });
            customGridControlObject.Size = new System.Drawing.Size(348, 560);
            customGridControlObject.TabIndex = 3;
            customGridControlObject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewObject });
            // 
            // gridViewObject
            // 
            gridViewObject.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewObject.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            gridViewObject.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewObject.Appearance.FocusedRow.Options.UseFont = true;
            gridViewObject.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            gridViewObject.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewObject.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewObject.Appearance.Row.Options.UseFont = true;
            gridViewObject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ObjectID, ObjectName, ObjectNameRus, HasAccessObject });
            gridViewObject.GridControl = customGridControlObject;
            gridViewObject.Name = "gridViewObject";
            gridViewObject.OptionsView.EnableAppearanceEvenRow = true;
            gridViewObject.CellValueChanged += gridViewObject_CellValueChanged;
            gridViewObject.KeyDown += gridViewObject_KeyDown;
            gridViewObject.CustomRowCellEdit += gridViewObject_CustomRowCellEdit;
            // 
            // ObjectID
            // 
            ObjectID.Caption = "ObjectID";
            ObjectID.FieldName = "ObjectID";
            ObjectID.Name = "ObjectID";
            ObjectID.OptionsColumn.AllowEdit = false;
            ObjectID.OptionsColumn.ReadOnly = true;
            ObjectID.Width = 99;
            // 
            // ObjectName
            // 
            ObjectName.Caption = "Имя";
            ObjectName.FieldName = "ObjectName";
            ObjectName.Name = "ObjectName";
            ObjectName.OptionsColumn.AllowEdit = false;
            ObjectName.OptionsColumn.ReadOnly = true;
            ObjectName.Visible = true;
            ObjectName.VisibleIndex = 0;
            ObjectName.Width = 116;
            // 
            // ObjectNameRus
            // 
            ObjectNameRus.Caption = "Название";
            ObjectNameRus.FieldName = "ObjectNameRus";
            ObjectNameRus.Name = "ObjectNameRus";
            ObjectNameRus.OptionsColumn.AllowEdit = false;
            ObjectNameRus.OptionsColumn.ReadOnly = true;
            ObjectNameRus.Visible = true;
            ObjectNameRus.VisibleIndex = 1;
            ObjectNameRus.Width = 119;
            // 
            // HasAccessObject
            // 
            HasAccessObject.Caption = "Доступ";
            HasAccessObject.ColumnEdit = repositoryItemComboBoxObject;
            HasAccessObject.FieldName = "HasAccessObject";
            HasAccessObject.Name = "HasAccessObject";
            HasAccessObject.Visible = true;
            HasAccessObject.VisibleIndex = 2;
            HasAccessObject.Width = 92;
            // 
            // repositoryItemComboBoxObject
            // 
            repositoryItemComboBoxObject.AutoHeight = false;
            repositoryItemComboBoxObject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBoxObject.Items.AddRange(new object[] { "Нет доступа", "Просмотр", "Редактор" });
            repositoryItemComboBoxObject.Name = "repositoryItemComboBoxObject";
            // 
            // customGridControlRole
            // 
            customGridControlRole.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlRole.Location = new System.Drawing.Point(24, 45);
            customGridControlRole.MainView = gridViewRole;
            customGridControlRole.Name = "customGridControlRole";
            customGridControlRole.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEditPodr });
            customGridControlRole.Size = new System.Drawing.Size(294, 560);
            customGridControlRole.TabIndex = 0;
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
            gridViewRole.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { IsSelected, RoleName, DescriptionRole, RoleID, RoleTableID, UserRoleID });
            gridViewRole.GridControl = customGridControlRole;
            gridViewRole.Name = "gridViewRole";
            gridViewRole.OptionsView.EnableAppearanceEvenRow = true;
            gridViewRole.OptionsView.ShowGroupedColumns = true;
            gridViewRole.RowClick += gridViewRole_RowClick;
            gridViewRole.CellValueChanging += gridViewRole_CellValueChanging;
            gridViewRole.KeyDown += gridViewRole_KeyDown;
            // 
            // IsSelected
            // 
            IsSelected.Caption = "В";
            IsSelected.ColumnEdit = repositoryItemCheckEditPodr;
            IsSelected.FieldName = "IsSelected";
            IsSelected.Name = "IsSelected";
            IsSelected.Visible = true;
            IsSelected.VisibleIndex = 0;
            IsSelected.Width = 30;
            // 
            // repositoryItemCheckEditPodr
            // 
            repositoryItemCheckEditPodr.AutoHeight = false;
            repositoryItemCheckEditPodr.Name = "repositoryItemCheckEditPodr";
            repositoryItemCheckEditPodr.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
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
            // customGridControlForm
            // 
            customGridControlForm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            customGridControlForm.Location = new System.Drawing.Point(346, 45);
            customGridControlForm.MainView = gridViewForm;
            customGridControlForm.Name = "customGridControlForm";
            customGridControlForm.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemComboBoxForm });
            customGridControlForm.Size = new System.Drawing.Size(352, 560);
            customGridControlForm.TabIndex = 2;
            customGridControlForm.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewForm });
            // 
            // gridViewForm
            // 
            gridViewForm.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewForm.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            gridViewForm.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewForm.Appearance.FocusedRow.Options.UseFont = true;
            gridViewForm.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            gridViewForm.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewForm.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewForm.Appearance.Row.Options.UseFont = true;
            gridViewForm.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ProjectFormsID, NameForm, NameFormRus, HasAccess });
            gridViewForm.GridControl = customGridControlForm;
            gridViewForm.Name = "gridViewForm";
            gridViewForm.OptionsView.EnableAppearanceEvenRow = true;
            gridViewForm.RowClick += gridViewForm_RowClick;
            gridViewForm.FocusedRowChanged += gridViewForm_FocusedRowChanged;
            gridViewForm.CellValueChanged += gridViewForm_CellValueChanged;
            gridViewForm.KeyDown += gridViewForm_KeyDown;
            // 
            // ProjectFormsID
            // 
            ProjectFormsID.Caption = "FormID";
            ProjectFormsID.FieldName = "ProjectFormsID";
            ProjectFormsID.Name = "ProjectFormsID";
            ProjectFormsID.OptionsColumn.AllowEdit = false;
            ProjectFormsID.OptionsColumn.ReadOnly = true;
            ProjectFormsID.Width = 27;
            // 
            // NameForm
            // 
            NameForm.Caption = "Имя";
            NameForm.FieldName = "NameForm";
            NameForm.Name = "NameForm";
            NameForm.OptionsColumn.AllowEdit = false;
            NameForm.OptionsColumn.ReadOnly = true;
            NameForm.Visible = true;
            NameForm.VisibleIndex = 0;
            NameForm.Width = 116;
            // 
            // NameFormRus
            // 
            NameFormRus.Caption = "Название";
            NameFormRus.FieldName = "NameFormRus";
            NameFormRus.Name = "NameFormRus";
            NameFormRus.OptionsColumn.AllowEdit = false;
            NameFormRus.OptionsColumn.ReadOnly = true;
            NameFormRus.Visible = true;
            NameFormRus.VisibleIndex = 1;
            NameFormRus.Width = 119;
            // 
            // HasAccess
            // 
            HasAccess.Caption = "Доступ";
            HasAccess.ColumnEdit = repositoryItemComboBoxForm;
            HasAccess.FieldName = "HasAccess";
            HasAccess.Name = "HasAccess";
            HasAccess.Visible = true;
            HasAccess.VisibleIndex = 2;
            HasAccess.Width = 92;
            // 
            // repositoryItemComboBoxForm
            // 
            repositoryItemComboBoxForm.AutoHeight = false;
            repositoryItemComboBoxForm.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBoxForm.Items.AddRange(new object[] { "Нет доступа", "Просмотр", "Редактор" });
            repositoryItemComboBoxForm.Name = "repositoryItemComboBoxForm";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroupObject, layoutControlGroupRole, layoutControlGroupForm });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1098, 629);
            Root.TextVisible = false;
            // 
            // layoutControlGroupObject
            // 
            layoutControlGroupObject.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3 });
            layoutControlGroupObject.Location = new System.Drawing.Point(702, 0);
            layoutControlGroupObject.Name = "layoutControlGroupObject";
            layoutControlGroupObject.Size = new System.Drawing.Size(376, 609);
            layoutControlGroupObject.Text = "Объекты";
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = customGridControlObject;
            layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(352, 564);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroupRole
            // 
            layoutControlGroupRole.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
            layoutControlGroupRole.Location = new System.Drawing.Point(0, 0);
            layoutControlGroupRole.Name = "layoutControlGroupRole";
            layoutControlGroupRole.Size = new System.Drawing.Size(322, 609);
            layoutControlGroupRole.Text = "Роли";
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = customGridControlRole;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(298, 564);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroupForm
            // 
            layoutControlGroupForm.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroupForm.Location = new System.Drawing.Point(322, 0);
            layoutControlGroupForm.Name = "layoutControlGroupForm";
            layoutControlGroupForm.Size = new System.Drawing.Size(380, 609);
            layoutControlGroupForm.Text = "Формы";
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customGridControlForm;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(356, 564);
            layoutControlItem1.TextVisible = false;
            // 
            // RoleFormObject
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1098, 629);
            Controls.Add(layoutControl1);
            Name = "RoleFormObject";
            Text = "Добавление объектов в роль";
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customGridControlObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditPodr).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlForm).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewForm).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxForm).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupForm).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Core.Class.CustomGridControl customGridControlForm;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewForm;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRole;
        private Core.Class.CustomGridControl customGridControlRole;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRole;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn RoleID;
        private DevExpress.XtraGrid.Columns.GridColumn RoleTableID;
        private DevExpress.XtraGrid.Columns.GridColumn DescriptionRole;
        private DevExpress.XtraGrid.Columns.GridColumn RoleName;
        private DevExpress.XtraGrid.Columns.GridColumn IsSelected;
        private DevExpress.XtraGrid.Columns.GridColumn ProjectFormsID;
        private DevExpress.XtraGrid.Columns.GridColumn NameForm;
        private DevExpress.XtraGrid.Columns.GridColumn NameFormRus;
        private DevExpress.XtraGrid.Columns.GridColumn HasAccess;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditPodr;
        private DevExpress.XtraGrid.Columns.GridColumn UserRoleID;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupObject;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupForm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxForm;
        private Core.Class.CustomGridControl customGridControlObject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewObject;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectID;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectName;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectNameRus;
        private DevExpress.XtraGrid.Columns.GridColumn HasAccessObject;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}