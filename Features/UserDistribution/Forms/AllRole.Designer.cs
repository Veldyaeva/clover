using SewingProduction.Core.Class;
namespace SewingProduction.Features.UserDistribution.Forms
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
            components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllRole));
            gridViewForms = new DevExpress.XtraGrid.Views.Grid.GridView();
            ProjectFormsID = new DevExpress.XtraGrid.Columns.GridColumn();
            NameForm = new DevExpress.XtraGrid.Columns.GridColumn();
            HasAccess = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemComboBoxForms = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            NameFormRus = new DevExpress.XtraGrid.Columns.GridColumn();
            customGridControlRoles = new CustomGridControl();
            bindingSourceRoles = new System.Windows.Forms.BindingSource(components);
            gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            UserID = new DevExpress.XtraGrid.Columns.GridColumn();
            UserNameUsers = new DevExpress.XtraGrid.Columns.GridColumn();
            HasRole = new DevExpress.XtraGrid.Columns.GridColumn();
            gridViewRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            RoleID = new DevExpress.XtraGrid.Columns.GridColumn();
            RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            Description = new DevExpress.XtraGrid.Columns.GridColumn();
            UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            repositoryItemComboBoxObject = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            gridViewObject = new DevExpress.XtraGrid.Views.Grid.GridView();
            ObjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            ObjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            HasAccessObject = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEditForms = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            repositoryItemCheckEditObject = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            customButtonAddRole = new CustomButton();
            customButtonDeleteRole = new CustomButton();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            customButtonCopyRole = new CustomButton();
            ObjectNameRus = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)gridViewForms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxForms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlRoles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceRoles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRoles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckedComboBoxEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditForms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditObject).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // gridViewForms
            // 
            gridViewForms.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
            gridViewForms.Appearance.Row.Options.UseBackColor = true;
            gridViewForms.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
            gridViewForms.AppearancePrint.EvenRow.Options.UseBackColor = true;
            gridViewForms.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ProjectFormsID, NameForm, HasAccess, NameFormRus });
            gridViewForms.DetailHeight = 4038;
            gridViewForms.GridControl = customGridControlRoles;
            gridViewForms.Name = "gridViewForms";
            gridViewForms.OptionsDetail.AllowExpandEmptyDetails = true;
            gridViewForms.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewForms.OptionsPrint.EnableAppearanceEvenRow = true;
            gridViewForms.OptionsPrint.ExpandAllDetails = true;
            gridViewForms.OptionsPrint.MaxMergedCellHeight = 1080;
            gridViewForms.OptionsPrint.PrintDetails = true;
            gridViewForms.OptionsView.ShowGroupPanel = false;
            gridViewForms.FocusedRowChanged += gridViewForms_FocusedRowChanged;
            // 
            // ProjectFormsID
            // 
            ProjectFormsID.Caption = "Ид";
            ProjectFormsID.FieldName = "ProjectFormsID";
            ProjectFormsID.MinWidth = 23;
            ProjectFormsID.Name = "ProjectFormsID";
            ProjectFormsID.OptionsColumn.ReadOnly = true;
            ProjectFormsID.Width = 162;
            // 
            // NameForm
            // 
            NameForm.Caption = "Имя формы";
            NameForm.FieldName = "NameForm";
            NameForm.MinWidth = 23;
            NameForm.Name = "NameForm";
            NameForm.OptionsColumn.ReadOnly = true;
            NameForm.Visible = true;
            NameForm.VisibleIndex = 0;
            NameForm.Width = 388;
            // 
            // HasAccess
            // 
            HasAccess.Caption = "Доступ";
            HasAccess.ColumnEdit = repositoryItemComboBoxForms;
            HasAccess.FieldName = "HasAccess";
            HasAccess.MinWidth = 23;
            HasAccess.Name = "HasAccess";
            HasAccess.Visible = true;
            HasAccess.VisibleIndex = 2;
            HasAccess.Width = 317;
            // 
            // repositoryItemComboBoxForms
            // 
            repositoryItemComboBoxForms.AutoHeight = false;
            repositoryItemComboBoxForms.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBoxForms.Items.AddRange(new object[] { "Нет доступа", "Просмотр", "Редактор" });
            repositoryItemComboBoxForms.Name = "repositoryItemComboBoxForms";
            repositoryItemComboBoxForms.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // NameFormRus
            // 
            NameFormRus.Caption = "Название формы (рус)";
            NameFormRus.FieldName = "NameFormRus";
            NameFormRus.Name = "NameFormRus";
            NameFormRus.OptionsColumn.ReadOnly = true;
            NameFormRus.Visible = true;
            NameFormRus.VisibleIndex = 1;
            NameFormRus.Width = 383;
            // 
            // customGridControlRoles
            // 
            customGridControlRoles.DataSource = bindingSourceRoles;
            customGridControlRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlRoles.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlRoles.Font = new System.Drawing.Font("Arial", 10F);
            gridLevelNode1.LevelTemplate = gridViewForms;
            gridLevelNode2.LevelTemplate = gridViewObject;
            gridLevelNode2.RelationName = "Объекты";
            gridLevelNode1.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode2 });
            gridLevelNode1.RelationName = "Формы";
            gridLevelNode3.LevelTemplate = gridViewUsers;
            gridLevelNode3.RelationName = "Пользователи";
            customGridControlRoles.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode1, gridLevelNode3 });
            customGridControlRoles.Location = new System.Drawing.Point(4, 3);
            customGridControlRoles.MainView = gridViewRoles;
            customGridControlRoles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlRoles.Name = "customGridControlRoles";
            customGridControlRoles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckedComboBoxEdit1, repositoryItemComboBoxForms, repositoryItemComboBoxObject });
            tableLayoutPanel1.SetRowSpan(customGridControlRoles, 10);
            customGridControlRoles.Size = new System.Drawing.Size(1113, 663);
            customGridControlRoles.TabIndex = 0;
            customGridControlRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUsers, gridViewRoles, gridViewObject, gridViewForms });
            customGridControlRoles.Load += customGridControlRoles_Load;
            // 
            // gridViewUsers
            // 
            gridViewUsers.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(250, 250, 200);
            gridViewUsers.Appearance.Row.Options.UseBackColor = true;
            gridViewUsers.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(255, 192, 255);
            gridViewUsers.AppearancePrint.EvenRow.Options.UseBackColor = true;
            gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { UserID, UserNameUsers, HasRole });
            gridViewUsers.DetailHeight = 4038;
            gridViewUsers.GridControl = customGridControlRoles;
            gridViewUsers.Name = "gridViewUsers";
            gridViewUsers.OptionsDetail.ShowDetailTabs = false;
            gridViewUsers.OptionsEditForm.EditFormColumnCount = 1;
            gridViewUsers.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewUsers.OptionsPrint.EnableAppearanceEvenRow = true;
            gridViewUsers.OptionsView.ShowGroupPanel = false;
            gridViewUsers.FocusedRowChanged += gridViewUsers_FocusedRowChanged;
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
            UserNameUsers.OptionsColumn.ReadOnly = true;
            UserNameUsers.Visible = true;
            UserNameUsers.VisibleIndex = 0;
            UserNameUsers.Width = 695;
            // 
            // HasRole
            // 
            HasRole.Caption = "Доступ";
            HasRole.FieldName = "HasRole";
            HasRole.MinWidth = 23;
            HasRole.Name = "HasRole";
            HasRole.Visible = true;
            HasRole.VisibleIndex = 1;
            HasRole.Width = 433;
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
            gridViewRoles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { RoleID, RoleName, Description, UserName });
            gridViewRoles.DetailHeight = 4038;
            gridViewRoles.GridControl = customGridControlRoles;
            gridViewRoles.Name = "gridViewRoles";
            gridViewRoles.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridViewRoles.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridViewRoles.OptionsDetail.AllowExpandEmptyDetails = true;
            gridViewRoles.OptionsEditForm.EditFormColumnCount = 1;
            gridViewRoles.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewRoles.OptionsPrint.EnableAppearanceEvenRow = true;
            gridViewRoles.OptionsPrint.EnableAppearanceOddRow = true;
            gridViewRoles.OptionsPrint.MaxMergedCellHeight = 1080;
            gridViewRoles.InitNewRow += gridViewRoles_InitNewRow;
            gridViewRoles.FocusedRowChanged += gridViewRoles_FocusedRowChanged;
            gridViewRoles.RowUpdated += gridViewRoles_RowUpdated;
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
            RoleName.Visible = true;
            RoleName.VisibleIndex = 0;
            RoleName.Width = 297;
            // 
            // Description
            // 
            Description.Caption = "Описание";
            Description.FieldName = "Description";
            Description.MinWidth = 23;
            Description.Name = "Description";
            Description.Visible = true;
            Description.VisibleIndex = 1;
            Description.Width = 691;
            // 
            // UserName
            // 
            UserName.Caption = "Создатель";
            UserName.FieldName = "UserName";
            UserName.MinWidth = 23;
            UserName.Name = "UserName";
            UserName.OptionsColumn.ReadOnly = true;
            UserName.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            UserName.Visible = true;
            UserName.VisibleIndex = 2;
            UserName.Width = 181;
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // repositoryItemComboBoxObject
            // 
            repositoryItemComboBoxObject.AutoHeight = false;
            repositoryItemComboBoxObject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBoxObject.Name = "repositoryItemComboBoxObject";
            repositoryItemComboBoxObject.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // gridViewObject
            // 
            gridViewObject.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            gridViewObject.Appearance.Row.Options.UseBackColor = true;
            gridViewObject.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
            gridViewObject.AppearancePrint.EvenRow.Options.UseBackColor = true;
            gridViewObject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ObjectID, ObjectName, HasAccessObject, ObjectNameRus });
            gridViewObject.DetailHeight = 4038;
            gridViewObject.GridControl = customGridControlRoles;
            gridViewObject.Name = "gridViewObject";
            gridViewObject.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewObject.OptionsPrint.EnableAppearanceEvenRow = true;
            gridViewObject.OptionsPrint.EnableAppearanceOddRow = true;
            gridViewObject.OptionsPrint.MaxMergedCellHeight = 1080;
            gridViewObject.OptionsView.ShowGroupPanel = false;
            gridViewObject.CustomRowCellEdit += gridViewObject_CustomRowCellEdit;
            gridViewObject.FocusedRowChanged += gridViewObject_FocusedRowChanged;
            // 
            // ObjectID
            // 
            ObjectID.Caption = "Ид";
            ObjectID.FieldName = "ObjectID";
            ObjectID.MinWidth = 23;
            ObjectID.Name = "ObjectID";
            ObjectID.OptionsColumn.ReadOnly = true;
            ObjectID.Width = 184;
            // 
            // ObjectName
            // 
            ObjectName.Caption = "Имя объекта";
            ObjectName.FieldName = "ObjectName";
            ObjectName.MinWidth = 23;
            ObjectName.Name = "ObjectName";
            ObjectName.OptionsColumn.ReadOnly = true;
            ObjectName.Visible = true;
            ObjectName.VisibleIndex = 0;
            ObjectName.Width = 399;
            // 
            // HasAccessObject
            // 
            HasAccessObject.Caption = "Доступ";
            HasAccessObject.ColumnEdit = repositoryItemComboBoxObject;
            HasAccessObject.FieldName = "HasAccessObject";
            HasAccessObject.MinWidth = 23;
            HasAccessObject.Name = "HasAccessObject";
            HasAccessObject.Visible = true;
            HasAccessObject.VisibleIndex = 2;
            HasAccessObject.Width = 328;
            // 
            // repositoryItemCheckEditForms
            // 
            repositoryItemCheckEditForms.AutoHeight = false;
            repositoryItemCheckEditForms.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            repositoryItemCheckEditForms.Name = "repositoryItemCheckEditForms";
            repositoryItemCheckEditForms.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // repositoryItemCheckEditObject
            // 
            repositoryItemCheckEditObject.AutoHeight = false;
            repositoryItemCheckEditObject.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            repositoryItemCheckEditObject.Name = "repositoryItemCheckEditObject";
            repositoryItemCheckEditObject.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // customButtonAddRole
            // 
            customButtonAddRole.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonAddRole.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonAddRole.Font = new System.Drawing.Font("Arial", 10F);
            customButtonAddRole.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonAddRole.Location = new System.Drawing.Point(1125, 3);
            customButtonAddRole.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonAddRole.Name = "customButtonAddRole";
            customButtonAddRole.Size = new System.Drawing.Size(117, 60);
            customButtonAddRole.TabIndex = 1;
            customButtonAddRole.Text = "Добавить роль";
            customButtonAddRole.UseVisualStyleBackColor = false;
            customButtonAddRole.Click += customButtonAddRole_Click;
            // 
            // customButtonDeleteRole
            // 
            customButtonDeleteRole.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonDeleteRole.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonDeleteRole.Font = new System.Drawing.Font("Arial", 10F);
            customButtonDeleteRole.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonDeleteRole.Location = new System.Drawing.Point(1125, 597);
            customButtonDeleteRole.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonDeleteRole.Name = "customButtonDeleteRole";
            customButtonDeleteRole.Size = new System.Drawing.Size(117, 69);
            customButtonDeleteRole.TabIndex = 2;
            customButtonDeleteRole.Text = "Удалить роль";
            customButtonDeleteRole.UseVisualStyleBackColor = false;
            customButtonDeleteRole.Click += customButtonDeleteRole_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.Controls.Add(customButtonCopyRole, 1, 1);
            tableLayoutPanel1.Controls.Add(customGridControlRoles, 0, 0);
            tableLayoutPanel1.Controls.Add(customButtonAddRole, 1, 0);
            tableLayoutPanel1.Controls.Add(customButtonDeleteRole, 1, 9);
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
            tableLayoutPanel1.Size = new System.Drawing.Size(1246, 669);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // customButtonCopyRole
            // 
            customButtonCopyRole.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonCopyRole.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonCopyRole.Font = new System.Drawing.Font("Arial", 10F);
            customButtonCopyRole.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonCopyRole.Location = new System.Drawing.Point(1125, 69);
            customButtonCopyRole.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonCopyRole.Name = "customButtonCopyRole";
            customButtonCopyRole.Size = new System.Drawing.Size(117, 60);
            customButtonCopyRole.TabIndex = 3;
            customButtonCopyRole.Text = "Копировать роль";
            customButtonCopyRole.UseVisualStyleBackColor = false;
            customButtonCopyRole.Click += customButtonCopyRole_Click;
            // 
            // ObjectNameRus
            // 
            ObjectNameRus.Caption = "Имя объекта (рус)";
            ObjectNameRus.FieldName = "ObjectNameRus";
            ObjectNameRus.Name = "ObjectNameRus";
            ObjectNameRus.Visible = true;
            ObjectNameRus.VisibleIndex = 1;
            ObjectNameRus.Width = 361;
            // 
            // AllRole
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1246, 669);
            Controls.Add(tableLayoutPanel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "AllRole";
            Text = "Роли";
            ((System.ComponentModel.ISupportInitialize)gridViewForms).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxForms).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlRoles).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceRoles).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRoles).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckedComboBoxEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditForms).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEditObject).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
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
        private DevExpress.XtraGrid.Columns.GridColumn NameFormRus;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectNameRus;
    }
}