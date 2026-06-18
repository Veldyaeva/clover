using SewingProduction.Core.Class;

namespace SewingProduction.Features.UserDistribution.Forms
{
	partial class FormAccessViewer
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			tableLayoutPanelRoot = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			splitContainerLeft = new System.Windows.Forms.SplitContainer();
			groupControlUsers = new DevExpress.XtraEditors.GroupControl();
			customGridControlUsers = new CustomGridControl();
			bindingSourceUsers = new System.Windows.Forms.BindingSource(components);
			gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
			colUserID = new DevExpress.XtraGrid.Columns.GridColumn();
			colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
			colHasAccessForm = new DevExpress.XtraGrid.Columns.GridColumn();
			colUserRoleNames = new DevExpress.XtraGrid.Columns.GridColumn();
			groupControlObjects = new DevExpress.XtraEditors.GroupControl();
			customGridControlObjects = new CustomGridControl();
			bindingSourceObjects = new System.Windows.Forms.BindingSource(components);
			gridViewObjects = new DevExpress.XtraGrid.Views.Grid.GridView();
			colObjectID = new DevExpress.XtraGrid.Columns.GridColumn();
			colObjectNameRus = new DevExpress.XtraGrid.Columns.GridColumn();
			colObjectName = new DevExpress.XtraGrid.Columns.GridColumn();
			colObjectType = new DevExpress.XtraGrid.Columns.GridColumn();
			colHasAccessObject = new DevExpress.XtraGrid.Columns.GridColumn();
			colObjectRoleNames = new DevExpress.XtraGrid.Columns.GridColumn();
			groupControlForms = new DevExpress.XtraEditors.GroupControl();
			customGridControlForms = new CustomGridControl();
			bindingSourceForms = new System.Windows.Forms.BindingSource(components);
			gridViewForms = new DevExpress.XtraGrid.Views.Grid.GridView();
			colProjectFormsID = new DevExpress.XtraGrid.Columns.GridColumn();
			colNameFormRus = new DevExpress.XtraGrid.Columns.GridColumn();
			colNameForm = new DevExpress.XtraGrid.Columns.GridColumn();
			colCreatorName = new DevExpress.XtraGrid.Columns.GridColumn();
			colUsersWithAccess = new DevExpress.XtraGrid.Columns.GridColumn();
			panelControlStatus = new DevExpress.XtraEditors.PanelControl();
			labelControlStatus = new DevExpress.XtraEditors.LabelControl();
			tableLayoutPanelRoot.SuspendLayout();
			tableLayoutPanelMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerLeft).BeginInit();
			splitContainerLeft.Panel1.SuspendLayout();
			splitContainerLeft.Panel2.SuspendLayout();
			splitContainerLeft.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupControlUsers).BeginInit();
			groupControlUsers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)customGridControlUsers).BeginInit();
			((System.ComponentModel.ISupportInitialize)bindingSourceUsers).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewUsers).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControlObjects).BeginInit();
			groupControlObjects.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)customGridControlObjects).BeginInit();
			((System.ComponentModel.ISupportInitialize)bindingSourceObjects).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewObjects).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControlForms).BeginInit();
			groupControlForms.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)customGridControlForms).BeginInit();
			((System.ComponentModel.ISupportInitialize)bindingSourceForms).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewForms).BeginInit();
			((System.ComponentModel.ISupportInitialize)panelControlStatus).BeginInit();
			panelControlStatus.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanelRoot
			// 
			tableLayoutPanelRoot.ColumnCount = 1;
			tableLayoutPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanelRoot.Controls.Add(tableLayoutPanelMain, 0, 0);
			tableLayoutPanelRoot.Controls.Add(panelControlStatus, 0, 1);
			tableLayoutPanelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanelRoot.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanelRoot.Name = "tableLayoutPanelRoot";
			tableLayoutPanelRoot.RowCount = 2;
			tableLayoutPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			tableLayoutPanelRoot.Size = new System.Drawing.Size(1380, 780);
			tableLayoutPanelRoot.TabIndex = 0;
			// 
			// tableLayoutPanelMain
			// 
			tableLayoutPanelMain.ColumnCount = 2;
			tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
			tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
			tableLayoutPanelMain.Controls.Add(splitContainerLeft, 0, 0);
			tableLayoutPanelMain.Controls.Add(groupControlForms, 1, 0);
			tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanelMain.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanelMain.Name = "tableLayoutPanelMain";
			tableLayoutPanelMain.RowCount = 1;
			tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanelMain.Size = new System.Drawing.Size(1374, 746);
			tableLayoutPanelMain.TabIndex = 0;
			// 
			// splitContainerLeft
			// 
			splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainerLeft.Location = new System.Drawing.Point(3, 3);
			splitContainerLeft.Name = "splitContainerLeft";
			splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerLeft.Panel1
			// 
			splitContainerLeft.Panel1.Controls.Add(groupControlUsers);
			// 
			// splitContainerLeft.Panel2
			// 
			splitContainerLeft.Panel2.Controls.Add(groupControlObjects);
			splitContainerLeft.Size = new System.Drawing.Size(928, 740);
			splitContainerLeft.SplitterDistance = 310;
			splitContainerLeft.TabIndex = 0;
			// 
			// groupControlUsers
			// 
			groupControlUsers.Controls.Add(customGridControlUsers);
			groupControlUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			groupControlUsers.Location = new System.Drawing.Point(0, 0);
			groupControlUsers.Name = "groupControlUsers";
			groupControlUsers.Size = new System.Drawing.Size(928, 310);
			groupControlUsers.TabIndex = 0;
			groupControlUsers.Text = "Пользователи, у которых есть доступ к выбранной форме";
			// 
			// customGridControlUsers
			// 
			customGridControlUsers.DataSource = bindingSourceUsers;
			customGridControlUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			customGridControlUsers.Font = new System.Drawing.Font("Arial", 10F);
			customGridControlUsers.Location = new System.Drawing.Point(2, 23);
			customGridControlUsers.MainView = gridViewUsers;
			customGridControlUsers.Name = "customGridControlUsers";
			customGridControlUsers.Size = new System.Drawing.Size(924, 285);
			customGridControlUsers.TabIndex = 0;
			customGridControlUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUsers });
			// 
			// gridViewUsers
			// 
			gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colUserID, colUserName, colHasAccessForm, colUserRoleNames });
			gridViewUsers.GridControl = customGridControlUsers;
			gridViewUsers.Name = "gridViewUsers";
			gridViewUsers.OptionsBehavior.Editable = false;
			gridViewUsers.OptionsBehavior.ReadOnly = true;
			gridViewUsers.OptionsView.ShowGroupPanel = false;
			gridViewUsers.FocusedRowChanged += gridViewUsers_FocusedRowChanged;
			// 
			// colUserID
			// 
			colUserID.Caption = "ID";
			colUserID.FieldName = "UserID";
			colUserID.Name = "colUserID";
			// 
			// colUserName
			// 
			colUserName.Caption = "Пользователь";
			colUserName.FieldName = "UserName";
			colUserName.Name = "colUserName";
			colUserName.Visible = true;
			colUserName.VisibleIndex = 0;
			colUserName.Width = 180;
			// 
			// colHasAccessForm
			// 
			colHasAccessForm.Caption = "Доступ к форме";
			colHasAccessForm.FieldName = "HasAccessForm";
			colHasAccessForm.Name = "colHasAccessForm";
			colHasAccessForm.Visible = true;
			colHasAccessForm.VisibleIndex = 1;
			colHasAccessForm.Width = 130;
			// 
			// colUserRoleNames
			// 
			colUserRoleNames.Caption = "Роли, давшие доступ";
			colUserRoleNames.FieldName = "RoleNames";
			colUserRoleNames.Name = "colUserRoleNames";
			colUserRoleNames.Visible = true;
			colUserRoleNames.VisibleIndex = 2;
			colUserRoleNames.Width = 480;
			// 
			// groupControlObjects
			// 
			groupControlObjects.Controls.Add(customGridControlObjects);
			groupControlObjects.Dock = System.Windows.Forms.DockStyle.Fill;
			groupControlObjects.Location = new System.Drawing.Point(0, 0);
			groupControlObjects.Name = "groupControlObjects";
			groupControlObjects.Size = new System.Drawing.Size(928, 426);
			groupControlObjects.TabIndex = 0;
			groupControlObjects.Text = "Объекты выбранной формы и итоговые права выбранного пользователя";
			// 
			// customGridControlObjects
			// 
			customGridControlObjects.DataSource = bindingSourceObjects;
			customGridControlObjects.Dock = System.Windows.Forms.DockStyle.Fill;
			customGridControlObjects.Font = new System.Drawing.Font("Arial", 10F);
			customGridControlObjects.Location = new System.Drawing.Point(2, 23);
			customGridControlObjects.MainView = gridViewObjects;
			customGridControlObjects.Name = "customGridControlObjects";
			customGridControlObjects.Size = new System.Drawing.Size(924, 401);
			customGridControlObjects.TabIndex = 0;
			customGridControlObjects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewObjects });
			// 
			// gridViewObjects
			// 
			gridViewObjects.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colObjectID, colObjectNameRus, colObjectName, colObjectType, colHasAccessObject, colObjectRoleNames });
			gridViewObjects.GridControl = customGridControlObjects;
			gridViewObjects.Name = "gridViewObjects";
			gridViewObjects.OptionsBehavior.Editable = false;
			gridViewObjects.OptionsBehavior.ReadOnly = true;
			gridViewObjects.OptionsView.ShowGroupPanel = false;
			// 
			// colObjectID
			// 
			colObjectID.Caption = "ID";
			colObjectID.FieldName = "ObjectID";
			colObjectID.Name = "colObjectID";
			// 
			// colObjectNameRus
			// 
			colObjectNameRus.Caption = "Русское имя";
			colObjectNameRus.FieldName = "ObjectNameRus";
			colObjectNameRus.Name = "colObjectNameRus";
			colObjectNameRus.Visible = true;
			colObjectNameRus.VisibleIndex = 0;
			colObjectNameRus.Width = 200;
			// 
			// colObjectName
			// 
			colObjectName.Caption = "Имя объекта";
			colObjectName.FieldName = "ObjectName";
			colObjectName.Name = "colObjectName";
			colObjectName.Visible = true;
			colObjectName.VisibleIndex = 1;
			colObjectName.Width = 200;
			// 
			// colObjectType
			// 
			colObjectType.Caption = "Тип";
			colObjectType.FieldName = "ObjectType";
			colObjectType.Name = "colObjectType";
			colObjectType.Visible = true;
			colObjectType.VisibleIndex = 2;
			colObjectType.Width = 120;
			// 
			// colHasAccessObject
			// 
			colHasAccessObject.Caption = "Доступ";
			colHasAccessObject.FieldName = "HasAccessObject";
			colHasAccessObject.Name = "colHasAccessObject";
			colHasAccessObject.Visible = true;
			colHasAccessObject.VisibleIndex = 3;
			colHasAccessObject.Width = 110;
			// 
			// colObjectRoleNames
			// 
			colObjectRoleNames.Caption = "Роли, давшие доступ";
			colObjectRoleNames.FieldName = "RoleNames";
			colObjectRoleNames.Name = "colObjectRoleNames";
			colObjectRoleNames.Visible = true;
			colObjectRoleNames.VisibleIndex = 4;
			colObjectRoleNames.Width = 420;
			// 
			// groupControlForms
			// 
			groupControlForms.Controls.Add(customGridControlForms);
			groupControlForms.Dock = System.Windows.Forms.DockStyle.Fill;
			groupControlForms.Location = new System.Drawing.Point(937, 3);
			groupControlForms.Name = "groupControlForms";
			groupControlForms.Size = new System.Drawing.Size(434, 740);
			groupControlForms.TabIndex = 1;
			groupControlForms.Text = "Формы";
			// 
			// customGridControlForms
			// 
			customGridControlForms.DataSource = bindingSourceForms;
			customGridControlForms.Dock = System.Windows.Forms.DockStyle.Fill;
			customGridControlForms.Font = new System.Drawing.Font("Arial", 10F);
			customGridControlForms.Location = new System.Drawing.Point(2, 23);
			customGridControlForms.MainView = gridViewForms;
			customGridControlForms.Name = "customGridControlForms";
			customGridControlForms.Size = new System.Drawing.Size(430, 715);
			customGridControlForms.TabIndex = 0;
			customGridControlForms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewForms });
			// 
			// gridViewForms
			// 
			gridViewForms.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colProjectFormsID, colNameFormRus, colNameForm, colCreatorName, colUsersWithAccess });
			gridViewForms.GridControl = customGridControlForms;
			gridViewForms.Name = "gridViewForms";
			gridViewForms.OptionsBehavior.Editable = false;
			gridViewForms.OptionsBehavior.ReadOnly = true;
			gridViewForms.OptionsFind.AlwaysVisible = true;
			gridViewForms.OptionsView.ShowGroupPanel = false;
			gridViewForms.FocusedRowChanged += gridViewForms_FocusedRowChanged;
			// 
			// colProjectFormsID
			// 
			colProjectFormsID.Caption = "ID";
			colProjectFormsID.FieldName = "ProjectFormsID";
			colProjectFormsID.Name = "colProjectFormsID";
			// 
			// colNameFormRus
			// 
			colNameFormRus.Caption = "Русское имя";
			colNameFormRus.FieldName = "NameFormRus";
			colNameFormRus.Name = "colNameFormRus";
			colNameFormRus.Visible = true;
			colNameFormRus.VisibleIndex = 0;
			colNameFormRus.Width = 180;
			// 
			// colNameForm
			// 
			colNameForm.Caption = "Имя формы";
			colNameForm.FieldName = "NameForm";
			colNameForm.Name = "colNameForm";
			colNameForm.Visible = true;
			colNameForm.VisibleIndex = 1;
			colNameForm.Width = 160;
			// 
			// colCreatorName
			// 
			colCreatorName.Caption = "Создатель";
			colCreatorName.FieldName = "CreatorName";
			colCreatorName.Name = "colCreatorName";
			colCreatorName.Visible = true;
			colCreatorName.VisibleIndex = 2;
			colCreatorName.Width = 120;
			// 
			// colUsersWithAccess
			// 
			colUsersWithAccess.Caption = "Пользователей";
			colUsersWithAccess.FieldName = "UsersWithAccess";
			colUsersWithAccess.Name = "colUsersWithAccess";
			colUsersWithAccess.Visible = true;
			colUsersWithAccess.VisibleIndex = 3;
			colUsersWithAccess.Width = 90;
			// 
			// panelControlStatus
			// 
			panelControlStatus.Controls.Add(labelControlStatus);
			panelControlStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			panelControlStatus.Location = new System.Drawing.Point(3, 755);
			panelControlStatus.Name = "panelControlStatus";
			panelControlStatus.Size = new System.Drawing.Size(1374, 22);
			panelControlStatus.TabIndex = 1;
			// 
			// labelControlStatus
			// 
			labelControlStatus.Appearance.Options.UseTextOptions = true;
			labelControlStatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			labelControlStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			labelControlStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			labelControlStatus.Location = new System.Drawing.Point(2, 2);
			labelControlStatus.Name = "labelControlStatus";
			labelControlStatus.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			labelControlStatus.Size = new System.Drawing.Size(1370, 18);
			labelControlStatus.TabIndex = 0;
			labelControlStatus.Text = "Готово.";
			// 
			// FormAccessViewer
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1380, 780);
			Controls.Add(tableLayoutPanelRoot);
			MinimumSize = new System.Drawing.Size(1000, 600);
			Name = "FormAccessViewer";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Просмотр доступа к формам";
			Load += FormAccessViewer_Load;
			tableLayoutPanelRoot.ResumeLayout(false);
			tableLayoutPanelMain.ResumeLayout(false);
			splitContainerLeft.Panel1.ResumeLayout(false);
			splitContainerLeft.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerLeft).EndInit();
			splitContainerLeft.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)groupControlUsers).EndInit();
			groupControlUsers.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)customGridControlUsers).EndInit();
			((System.ComponentModel.ISupportInitialize)bindingSourceUsers).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewUsers).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControlObjects).EndInit();
			groupControlObjects.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)customGridControlObjects).EndInit();
			((System.ComponentModel.ISupportInitialize)bindingSourceObjects).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewObjects).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControlForms).EndInit();
			groupControlForms.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)customGridControlForms).EndInit();
			((System.ComponentModel.ISupportInitialize)bindingSourceForms).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewForms).EndInit();
			((System.ComponentModel.ISupportInitialize)panelControlStatus).EndInit();
			panelControlStatus.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRoot;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
		private System.Windows.Forms.SplitContainer splitContainerLeft;

		private DevExpress.XtraEditors.GroupControl groupControlForms;
		private DevExpress.XtraEditors.GroupControl groupControlUsers;
		private DevExpress.XtraEditors.GroupControl groupControlObjects;

		private CustomGridControl customGridControlForms;
		private CustomGridControl customGridControlUsers;
		private CustomGridControl customGridControlObjects;

		private DevExpress.XtraGrid.Views.Grid.GridView gridViewForms;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewObjects;

		private System.Windows.Forms.BindingSource bindingSourceForms;
		private System.Windows.Forms.BindingSource bindingSourceUsers;
		private System.Windows.Forms.BindingSource bindingSourceObjects;

		private DevExpress.XtraGrid.Columns.GridColumn colProjectFormsID;
		private DevExpress.XtraGrid.Columns.GridColumn colNameFormRus;
		private DevExpress.XtraGrid.Columns.GridColumn colNameForm;
		private DevExpress.XtraGrid.Columns.GridColumn colCreatorName;
		private DevExpress.XtraGrid.Columns.GridColumn colUsersWithAccess;

		private DevExpress.XtraGrid.Columns.GridColumn colUserID;
		private DevExpress.XtraGrid.Columns.GridColumn colUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colHasAccessForm;
		private DevExpress.XtraGrid.Columns.GridColumn colUserRoleNames;

		private DevExpress.XtraGrid.Columns.GridColumn colObjectID;
		private DevExpress.XtraGrid.Columns.GridColumn colObjectNameRus;
		private DevExpress.XtraGrid.Columns.GridColumn colObjectName;
		private DevExpress.XtraGrid.Columns.GridColumn colObjectType;
		private DevExpress.XtraGrid.Columns.GridColumn colHasAccessObject;
		private DevExpress.XtraGrid.Columns.GridColumn colObjectRoleNames;

		private DevExpress.XtraEditors.PanelControl panelControlStatus;
		private DevExpress.XtraEditors.LabelControl labelControlStatus;
	}
}