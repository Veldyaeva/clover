namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            gridViewRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            customGridControlObject = new CustomGridControl();
            bindingSourceObject = new System.Windows.Forms.BindingSource(components);
            gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridViewObject = new DevExpress.XtraGrid.Views.Grid.GridView();
            ObjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            ObjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            ObjectNameRus = new DevExpress.XtraGrid.Columns.GridColumn();
            UserNameOb = new DevExpress.XtraGrid.Columns.GridColumn();
            ObjectType = new DevExpress.XtraGrid.Columns.GridColumn();
            MissingObj = new DevExpress.XtraGrid.Columns.GridColumn();
            AddedObj = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemLookUpEditCreator = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            customGridControlForms = new CustomGridControl();
            bindingSourceForms = new System.Windows.Forms.BindingSource(components);
            gridViewForms = new DevExpress.XtraGrid.Views.Grid.GridView();
            ProjectFormsID = new DevExpress.XtraGrid.Columns.GridColumn();
            NameForm = new DevExpress.XtraGrid.Columns.GridColumn();
            NameFormRus = new DevExpress.XtraGrid.Columns.GridColumn();
            UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            Missing = new DevExpress.XtraGrid.Columns.GridColumn();
            Added = new DevExpress.XtraGrid.Columns.GridColumn();
            bindingSourceGroup = new System.Windows.Forms.BindingSource(components);
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            customButtonAddObject = new CustomButton();
            customButtonDeleteObject = new CustomButton();
            customButtonLoadObject = new CustomButton();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            customButtonOpen = new CustomButton();
            customButtonLoadMenu = new CustomButton();
            customButtonLoadForm = new CustomButton();
            customButtonFormAdd = new CustomButton();
            customButtonDeleteForm = new CustomButton();
            customCheckBoxMyForm = new CustomCheckBox();
            behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(components);
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)gridViewRoles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditCreator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlForms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceForms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewForms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceGroup).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)behaviorManager1).BeginInit();
            SuspendLayout();
            // 
            // gridViewRoles
            // 
            gridViewRoles.DetailHeight = 404;
            gridViewRoles.GridControl = customGridControlObject;
            gridViewRoles.Name = "gridViewRoles";
            gridViewRoles.OptionsEditForm.PopupEditFormWidth = 933;
            // 
            // customGridControlObject
            // 
            customGridControlObject.DataSource = bindingSourceObject;
            customGridControlObject.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlObject.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlObject.Font = new System.Drawing.Font("Arial", 10F);
            gridLevelNode1.LevelTemplate = gridViewRoles;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.LevelTemplate = gridViewUsers;
            gridLevelNode2.RelationName = "Level2";
            customGridControlObject.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode1, gridLevelNode2 });
            customGridControlObject.Location = new System.Drawing.Point(595, 78);
            customGridControlObject.MainView = gridViewObject;
            customGridControlObject.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlObject.Name = "customGridControlObject";
            tableLayoutPanel1.SetRowSpan(customGridControlObject, 9);
            customGridControlObject.Size = new System.Drawing.Size(880, 674);
            customGridControlObject.TabIndex = 3;
            customGridControlObject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUsers, gridViewObject, gridViewRoles });
            customGridControlObject.Load += customGridControlObject_Load;
            // 
            // gridViewUsers
            // 
            gridViewUsers.DetailHeight = 404;
            gridViewUsers.GridControl = customGridControlObject;
            gridViewUsers.Name = "gridViewUsers";
            gridViewUsers.OptionsEditForm.PopupEditFormWidth = 933;
            // 
            // gridViewObject
            // 
            gridViewObject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ObjectID, ObjectName, ObjectNameRus, UserNameOb, ObjectType, MissingObj, AddedObj });
            gridViewObject.DetailHeight = 404;
            gridViewObject.GridControl = customGridControlObject;
            gridViewObject.Name = "gridViewObject";
            gridViewObject.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridViewObject.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridViewObject.OptionsDetail.AllowExpandEmptyDetails = true;
            gridViewObject.OptionsEditForm.EditFormColumnCount = 1;
            gridViewObject.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewObject.RowStyle += gridViewObject_RowStyle;
            gridViewObject.InitNewRow += gridViewObject_InitNewRow;
            gridViewObject.RowUpdated += gridViewObject_RowUpdated;
            // 
            // ObjectID
            // 
            ObjectID.Caption = "Ид обьекта";
            ObjectID.FieldName = "ObjectID";
            ObjectID.MinWidth = 23;
            ObjectID.Name = "ObjectID";
            ObjectID.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            ObjectID.Visible = true;
            ObjectID.VisibleIndex = 0;
            ObjectID.Width = 99;
            // 
            // ObjectName
            // 
            ObjectName.Caption = "Имя объекта";
            ObjectName.FieldName = "ObjectName";
            ObjectName.MinWidth = 23;
            ObjectName.Name = "ObjectName";
            ObjectName.Visible = true;
            ObjectName.VisibleIndex = 1;
            ObjectName.Width = 255;
            // 
            // ObjectNameRus
            // 
            ObjectNameRus.Caption = "Русское имя";
            ObjectNameRus.FieldName = "ObjectNameRus";
            ObjectNameRus.MinWidth = 23;
            ObjectNameRus.Name = "ObjectNameRus";
            ObjectNameRus.Visible = true;
            ObjectNameRus.VisibleIndex = 2;
            ObjectNameRus.Width = 250;
            // 
            // UserNameOb
            // 
            UserNameOb.Caption = "Создатель";
            UserNameOb.FieldName = "UserName";
            UserNameOb.MinWidth = 23;
            UserNameOb.Name = "UserNameOb";
            UserNameOb.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            UserNameOb.Visible = true;
            UserNameOb.VisibleIndex = 4;
            UserNameOb.Width = 128;
            // 
            // ObjectType
            // 
            ObjectType.Caption = "Тип";
            ObjectType.FieldName = "ObjectType";
            ObjectType.MinWidth = 23;
            ObjectType.Name = "ObjectType";
            ObjectType.Visible = true;
            ObjectType.VisibleIndex = 3;
            ObjectType.Width = 119;
            // 
            // MissingObj
            // 
            MissingObj.Caption = "MissingObj";
            MissingObj.FieldName = "MissingObj";
            MissingObj.MinWidth = 23;
            MissingObj.Name = "MissingObj";
            MissingObj.Width = 87;
            // 
            // AddedObj
            // 
            AddedObj.Caption = "AddedObj";
            AddedObj.FieldName = "AddedObj";
            AddedObj.MinWidth = 23;
            AddedObj.Name = "AddedObj";
            AddedObj.Width = 87;
            // 
            // repositoryItemLookUpEditCreator
            // 
            repositoryItemLookUpEditCreator.AutoHeight = false;
            repositoryItemLookUpEditCreator.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            repositoryItemLookUpEditCreator.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemLookUpEditCreator.DisplayMember = "UserName";
            repositoryItemLookUpEditCreator.Name = "repositoryItemLookUpEditCreator";
            repositoryItemLookUpEditCreator.NullText = "-";
            repositoryItemLookUpEditCreator.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            repositoryItemLookUpEditCreator.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditCreator.ValueMember = "CreatorID";
            // 
            // customGridControlForms
            // 
            customGridControlForms.DataSource = bindingSourceForms;
            customGridControlForms.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlForms.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlForms.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlForms.Location = new System.Drawing.Point(4, 78);
            customGridControlForms.MainView = gridViewForms;
            customGridControlForms.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridControlForms.Name = "customGridControlForms";
            customGridControlForms.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemLookUpEditCreator });
            tableLayoutPanel1.SetRowSpan(customGridControlForms, 9);
            customGridControlForms.Size = new System.Drawing.Size(583, 674);
            customGridControlForms.TabIndex = 1;
            customGridControlForms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewForms });
            customGridControlForms.Load += customGridControlForms_Load;
            customGridControlForms.Click += customGridControlForms_Click;
            customGridControlForms.KeyUp += customGridControlForms_KeyUp;
            // 
            // gridViewForms
            // 
            gridViewForms.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ProjectFormsID, NameForm, NameFormRus, UserName, Missing, Added });
            gridViewForms.DetailHeight = 404;
            gridViewForms.GridControl = customGridControlForms;
            gridViewForms.Name = "gridViewForms";
            gridViewForms.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridViewForms.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridViewForms.OptionsEditForm.EditFormColumnCount = 1;
            gridViewForms.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewForms.RowStyle += gridViewForms_RowStyle;
            gridViewForms.EditFormHidden += gridViewForms_EditFormHidden;
            gridViewForms.InitNewRow += gridViewForms_InitNewRow;
            gridViewForms.RowUpdated += gridViewForms_RowUpdated;
            // 
            // ProjectFormsID
            // 
            ProjectFormsID.Caption = "Ид формы";
            ProjectFormsID.FieldName = "ProjectFormsID";
            ProjectFormsID.MinWidth = 23;
            ProjectFormsID.Name = "ProjectFormsID";
            ProjectFormsID.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            ProjectFormsID.Visible = true;
            ProjectFormsID.VisibleIndex = 0;
            ProjectFormsID.Width = 84;
            // 
            // NameForm
            // 
            NameForm.Caption = "Имя формы";
            NameForm.FieldName = "NameForm";
            NameForm.MinWidth = 23;
            NameForm.Name = "NameForm";
            NameForm.Visible = true;
            NameForm.VisibleIndex = 1;
            NameForm.Width = 250;
            // 
            // NameFormRus
            // 
            NameFormRus.Caption = "Русское имя";
            NameFormRus.FieldName = "NameFormRus";
            NameFormRus.MinWidth = 23;
            NameFormRus.Name = "NameFormRus";
            NameFormRus.Visible = true;
            NameFormRus.VisibleIndex = 2;
            NameFormRus.Width = 241;
            // 
            // UserName
            // 
            UserName.Caption = "Создатель";
            UserName.ColumnEdit = repositoryItemLookUpEditCreator;
            UserName.FieldName = "CreatorID";
            UserName.MinWidth = 23;
            UserName.Name = "UserName";
            UserName.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            UserName.Visible = true;
            UserName.VisibleIndex = 3;
            UserName.Width = 128;
            // 
            // Missing
            // 
            Missing.Caption = "Missing";
            Missing.FieldName = "Missing";
            Missing.MinWidth = 23;
            Missing.Name = "Missing";
            Missing.Width = 87;
            // 
            // Added
            // 
            Added.Caption = "Added";
            Added.FieldName = "Added";
            Added.MinWidth = 23;
            Added.Name = "Added";
            Added.Width = 87;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel1.Controls.Add(customGridControlForms, 0, 1);
            tableLayoutPanel1.Controls.Add(customGridControlObject, 1, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
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
            tableLayoutPanel1.Size = new System.Drawing.Size(1479, 755);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            tableLayoutPanel3.Controls.Add(customButtonAddObject, 0, 1);
            tableLayoutPanel3.Controls.Add(customButtonDeleteObject, 1, 1);
            tableLayoutPanel3.Controls.Add(customButtonLoadObject, 3, 1);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(595, 3);
            tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new System.Drawing.Size(880, 69);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // customButtonAddObject
            // 
            customButtonAddObject.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonAddObject.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonAddObject.Font = new System.Drawing.Font("Arial", 10F);
            customButtonAddObject.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonAddObject.Location = new System.Drawing.Point(4, 37);
            customButtonAddObject.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonAddObject.Name = "customButtonAddObject";
            customButtonAddObject.Size = new System.Drawing.Size(212, 29);
            customButtonAddObject.TabIndex = 0;
            customButtonAddObject.Text = "Добавить Объект";
            customButtonAddObject.UseVisualStyleBackColor = false;
            customButtonAddObject.Click += customButton1_Click;
            // 
            // customButtonDeleteObject
            // 
            customButtonDeleteObject.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonDeleteObject.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonDeleteObject.Font = new System.Drawing.Font("Arial", 10F);
            customButtonDeleteObject.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonDeleteObject.Location = new System.Drawing.Point(224, 37);
            customButtonDeleteObject.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonDeleteObject.Name = "customButtonDeleteObject";
            customButtonDeleteObject.Size = new System.Drawing.Size(212, 29);
            customButtonDeleteObject.TabIndex = 1;
            customButtonDeleteObject.Text = "Удалить Объект";
            customButtonDeleteObject.UseVisualStyleBackColor = false;
            customButtonDeleteObject.Click += customButton2_Click;
            // 
            // customButtonLoadObject
            // 
            customButtonLoadObject.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonLoadObject.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonLoadObject.Font = new System.Drawing.Font("Arial", 10F);
            customButtonLoadObject.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonLoadObject.Location = new System.Drawing.Point(664, 37);
            customButtonLoadObject.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonLoadObject.Name = "customButtonLoadObject";
            customButtonLoadObject.Size = new System.Drawing.Size(212, 29);
            customButtonLoadObject.TabIndex = 2;
            customButtonLoadObject.Text = "Загрузить объекты";
            customButtonLoadObject.UseVisualStyleBackColor = false;
            customButtonLoadObject.Click += customButtonLoadObject_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel2.Controls.Add(customButtonOpen, 2, 1);
            tableLayoutPanel2.Controls.Add(customButtonLoadMenu, 1, 0);
            tableLayoutPanel2.Controls.Add(customButtonLoadForm, 0, 0);
            tableLayoutPanel2.Controls.Add(customButtonFormAdd, 0, 1);
            tableLayoutPanel2.Controls.Add(customButtonDeleteForm, 1, 1);
            tableLayoutPanel2.Controls.Add(customCheckBoxMyForm, 2, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(4, 3);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(583, 69);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // customButtonOpen
            // 
            customButtonOpen.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonOpen.Font = new System.Drawing.Font("Arial", 10F);
            customButtonOpen.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonOpen.Location = new System.Drawing.Point(392, 37);
            customButtonOpen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonOpen.Name = "customButtonOpen";
            customButtonOpen.Size = new System.Drawing.Size(187, 29);
            customButtonOpen.TabIndex = 5;
            customButtonOpen.Text = "Открыть форму";
            customButtonOpen.UseVisualStyleBackColor = false;
            customButtonOpen.Click += customButtonOpen_Click;
            // 
            // customButtonLoadMenu
            // 
            customButtonLoadMenu.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonLoadMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonLoadMenu.Font = new System.Drawing.Font("Arial", 10F);
            customButtonLoadMenu.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonLoadMenu.Location = new System.Drawing.Point(198, 3);
            customButtonLoadMenu.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonLoadMenu.Name = "customButtonLoadMenu";
            customButtonLoadMenu.Size = new System.Drawing.Size(186, 28);
            customButtonLoadMenu.TabIndex = 4;
            customButtonLoadMenu.Text = "Загрузить меню";
            customButtonLoadMenu.UseVisualStyleBackColor = false;
            customButtonLoadMenu.Click += customButtonLoadMenu_Click;
            // 
            // customButtonLoadForm
            // 
            customButtonLoadForm.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonLoadForm.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonLoadForm.Font = new System.Drawing.Font("Arial", 10F);
            customButtonLoadForm.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonLoadForm.Location = new System.Drawing.Point(4, 3);
            customButtonLoadForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonLoadForm.Name = "customButtonLoadForm";
            customButtonLoadForm.Size = new System.Drawing.Size(186, 28);
            customButtonLoadForm.TabIndex = 3;
            customButtonLoadForm.Text = "Загрузить формы";
            customButtonLoadForm.UseVisualStyleBackColor = false;
            customButtonLoadForm.Click += customButtonLoadForm_Click;
            // 
            // customButtonFormAdd
            // 
            customButtonFormAdd.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonFormAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonFormAdd.Font = new System.Drawing.Font("Arial", 10F);
            customButtonFormAdd.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonFormAdd.Location = new System.Drawing.Point(4, 37);
            customButtonFormAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonFormAdd.Name = "customButtonFormAdd";
            customButtonFormAdd.Size = new System.Drawing.Size(186, 29);
            customButtonFormAdd.TabIndex = 0;
            customButtonFormAdd.Text = "Добавить Форму";
            customButtonFormAdd.UseVisualStyleBackColor = false;
            customButtonFormAdd.Click += customButtonFormAdd_Click;
            // 
            // customButtonDeleteForm
            // 
            customButtonDeleteForm.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButtonDeleteForm.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonDeleteForm.Font = new System.Drawing.Font("Arial", 10F);
            customButtonDeleteForm.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButtonDeleteForm.Location = new System.Drawing.Point(198, 37);
            customButtonDeleteForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButtonDeleteForm.Name = "customButtonDeleteForm";
            customButtonDeleteForm.Size = new System.Drawing.Size(186, 29);
            customButtonDeleteForm.TabIndex = 1;
            customButtonDeleteForm.Text = "Удалить Форму";
            customButtonDeleteForm.UseVisualStyleBackColor = false;
            customButtonDeleteForm.Click += customButtonDeleteForm_Click;
            // 
            // customCheckBoxMyForm
            // 
            customCheckBoxMyForm.AutoSize = true;
            customCheckBoxMyForm.Dock = System.Windows.Forms.DockStyle.Fill;
            customCheckBoxMyForm.Font = new System.Drawing.Font("Arial", 10F);
            customCheckBoxMyForm.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            customCheckBoxMyForm.Location = new System.Drawing.Point(392, 3);
            customCheckBoxMyForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customCheckBoxMyForm.Name = "customCheckBoxMyForm";
            customCheckBoxMyForm.Size = new System.Drawing.Size(187, 28);
            customCheckBoxMyForm.TabIndex = 2;
            customCheckBoxMyForm.Text = "Созданные мною";
            customCheckBoxMyForm.UseVisualStyleBackColor = true;
            customCheckBoxMyForm.CheckedChanged += customCheckBoxMyForm_CheckedChanged;
            // 
            // gridColumn2
            // 
            gridColumn2.FieldName = "NameForm";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            gridColumn3.FieldName = "NameForm";
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            gridColumn4.FieldName = "NameForm";
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Visible = true;
            gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            gridColumn7.Name = "gridColumn7";
            gridColumn7.Visible = true;
            gridColumn7.VisibleIndex = 3;
            // 
            // gridColumn8
            // 
            gridColumn8.Name = "gridColumn8";
            gridColumn8.Visible = true;
            gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            gridColumn9.Name = "gridColumn9";
            gridColumn9.Visible = true;
            gridColumn9.VisibleIndex = 3;
            // 
            // gridColumn10
            // 
            gridColumn10.Name = "gridColumn10";
            gridColumn10.Visible = true;
            gridColumn10.VisibleIndex = 3;
            // 
            // gridColumn14
            // 
            gridColumn14.Caption = "Создатель";
            gridColumn14.Name = "gridColumn14";
            gridColumn14.Visible = true;
            gridColumn14.VisibleIndex = 3;
            // 
            // gridColumn11
            // 
            gridColumn11.Caption = "Создатель";
            gridColumn11.Name = "gridColumn11";
            gridColumn11.Visible = true;
            gridColumn11.VisibleIndex = 3;
            // 
            // gridColumn12
            // 
            gridColumn12.Caption = "Русское имя";
            gridColumn12.Name = "gridColumn12";
            gridColumn12.Visible = true;
            gridColumn12.VisibleIndex = 2;
            // 
            // gridColumn13
            // 
            gridColumn13.Caption = "Русское имя";
            gridColumn13.Name = "gridColumn13";
            gridColumn13.Visible = true;
            gridColumn13.VisibleIndex = 2;
            // 
            // gridColumn15
            // 
            gridColumn15.Caption = "Русское имя";
            gridColumn15.Name = "gridColumn15";
            gridColumn15.Visible = true;
            gridColumn15.VisibleIndex = 2;
            // 
            // gridColumn16
            // 
            gridColumn16.Caption = "Русское имя";
            gridColumn16.Name = "gridColumn16";
            gridColumn16.Visible = true;
            gridColumn16.VisibleIndex = 2;
            // 
            // gridColumn17
            // 
            gridColumn17.Caption = "Русское имя";
            gridColumn17.Name = "gridColumn17";
            gridColumn17.Visible = true;
            gridColumn17.VisibleIndex = 2;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1479, 755);
            Controls.Add(tableLayoutPanel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "AdminForm";
            Text = "Администрирование форм";
            Load += AdminForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridViewRoles).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditCreator).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlForms).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceForms).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewForms).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceGroup).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)behaviorManager1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CustomGridControl customGridControlForms;
        private System.Windows.Forms.BindingSource bindingSourceForms;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewForms;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.BindingSource bindingSourceGroup;
        private CustomGridControl customGridControlObject;
        private System.Windows.Forms.BindingSource bindingSourceObject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewObject;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CustomButton customButtonFormAdd;
        private CustomButton customButtonDeleteForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private CustomButton customButtonAddObject;
        private CustomButton customButtonDeleteObject;
        private CustomCheckBox customCheckBoxMyForm;
        private DevExpress.XtraGrid.Columns.GridColumn UserName;
        private DevExpress.XtraGrid.Columns.GridColumn NameFormRus;
        private DevExpress.XtraGrid.Columns.GridColumn NameForm;
        private DevExpress.XtraGrid.Columns.GridColumn ProjectFormsID;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectID;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectName;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectNameRus;
        private DevExpress.XtraGrid.Columns.GridColumn UserNameOb;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private CustomButton customButtonLoadObject;
        private DevExpress.XtraGrid.Columns.GridColumn ObjectType;
        private CustomButton customButtonLoadForm;
        private DevExpress.XtraGrid.Columns.GridColumn Missing;
        private DevExpress.XtraGrid.Columns.GridColumn Added;
        private DevExpress.XtraGrid.Columns.GridColumn MissingObj;
        private DevExpress.XtraGrid.Columns.GridColumn AddedObj;
        private CustomButton customButtonLoadMenu;
        private CustomButton customButtonOpen;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditCreator;
    }
}