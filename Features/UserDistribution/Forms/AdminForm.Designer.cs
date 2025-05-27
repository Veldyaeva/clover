namespace SewingProduction.form.UserDistribution
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridViewRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.customGridControlObject = new SewingProduction.CustomGridControl();
            this.bindingSourceObject = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridViewObject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ObjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ObjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ObjectNameRus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserNameOb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ObjectType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MissingObj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AddedObj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customGridControlForms = new SewingProduction.CustomGridControl();
            this.bindingSourceForms = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewForms = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ProjectFormsID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NameForm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NameFormRus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Missing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Added = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bindingSourceGroup = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.customButtonAddObject = new SewingProduction.CustomButton();
            this.customButtonDeleteObject = new SewingProduction.CustomButton();
            this.customButtonLoadObject = new SewingProduction.CustomButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.customButtonLoadForm = new SewingProduction.CustomButton();
            this.customButtonFormAdd = new SewingProduction.CustomButton();
            this.customButtonDeleteForm = new SewingProduction.CustomButton();
            this.customCheckBoxMyForm = new SewingProduction.CustomCheckBox();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customButtonLoadMenu = new SewingProduction.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceGroup)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewRoles
            // 
            this.gridViewRoles.GridControl = this.customGridControlObject;
            this.gridViewRoles.Name = "gridViewRoles";
            // 
            // customGridControlObject
            // 
            this.customGridControlObject.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridControlObject.DataSource = this.bindingSourceObject;
            this.customGridControlObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControlObject.Font = new System.Drawing.Font("Arial", 10F);
            gridLevelNode1.LevelTemplate = this.gridViewRoles;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.LevelTemplate = this.gridViewUsers;
            gridLevelNode2.RelationName = "Level2";
            this.customGridControlObject.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.customGridControlObject.Location = new System.Drawing.Point(510, 68);
            this.customGridControlObject.MainView = this.gridViewObject;
            this.customGridControlObject.Name = "customGridControlObject";
            this.customGridControlObject.ObjectName = null;
            this.tableLayoutPanel1.SetRowSpan(this.customGridControlObject, 9);
            this.customGridControlObject.Size = new System.Drawing.Size(755, 583);
            this.customGridControlObject.TabIndex = 3;
            this.customGridControlObject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers,
            this.gridViewObject,
            this.gridViewRoles});
            this.customGridControlObject.Load += new System.EventHandler(this.customGridControlObject_Load);
            // 
            // gridViewUsers
            // 
            this.gridViewUsers.GridControl = this.customGridControlObject;
            this.gridViewUsers.Name = "gridViewUsers";
            // 
            // gridViewObject
            // 
            this.gridViewObject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ObjectID,
            this.ObjectName,
            this.ObjectNameRus,
            this.UserNameOb,
            this.ObjectType,
            this.MissingObj,
            this.AddedObj});
            this.gridViewObject.GridControl = this.customGridControlObject;
            this.gridViewObject.Name = "gridViewObject";
            this.gridViewObject.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewObject.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewObject.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridViewObject.OptionsEditForm.EditFormColumnCount = 1;
            this.gridViewObject.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewObject_RowStyle);
            this.gridViewObject.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridViewObject_InitNewRow);
            this.gridViewObject.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewObject_RowUpdated);
            // 
            // ObjectID
            // 
            this.ObjectID.Caption = "Ид обьекта";
            this.ObjectID.FieldName = "ObjectID";
            this.ObjectID.Name = "ObjectID";
            this.ObjectID.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.ObjectID.Visible = true;
            this.ObjectID.VisibleIndex = 0;
            this.ObjectID.Width = 85;
            // 
            // ObjectName
            // 
            this.ObjectName.Caption = "Имя объекта";
            this.ObjectName.FieldName = "ObjectName";
            this.ObjectName.Name = "ObjectName";
            this.ObjectName.Visible = true;
            this.ObjectName.VisibleIndex = 1;
            this.ObjectName.Width = 219;
            // 
            // ObjectNameRus
            // 
            this.ObjectNameRus.Caption = "Русское имя";
            this.ObjectNameRus.FieldName = "ObjectNameRus";
            this.ObjectNameRus.Name = "ObjectNameRus";
            this.ObjectNameRus.Visible = true;
            this.ObjectNameRus.VisibleIndex = 2;
            this.ObjectNameRus.Width = 214;
            // 
            // UserNameOb
            // 
            this.UserNameOb.Caption = "Создатель";
            this.UserNameOb.FieldName = "UserName";
            this.UserNameOb.Name = "UserNameOb";
            this.UserNameOb.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.UserNameOb.Visible = true;
            this.UserNameOb.VisibleIndex = 4;
            this.UserNameOb.Width = 110;
            // 
            // ObjectType
            // 
            this.ObjectType.Caption = "Тип";
            this.ObjectType.FieldName = "ObjectType";
            this.ObjectType.Name = "ObjectType";
            this.ObjectType.Visible = true;
            this.ObjectType.VisibleIndex = 3;
            this.ObjectType.Width = 102;
            // 
            // MissingObj
            // 
            this.MissingObj.Caption = "MissingObj";
            this.MissingObj.FieldName = "MissingObj";
            this.MissingObj.Name = "MissingObj";
            // 
            // AddedObj
            // 
            this.AddedObj.Caption = "AddedObj";
            this.AddedObj.FieldName = "AddedObj";
            this.AddedObj.Name = "AddedObj";
            // 
            // customGridControlForms
            // 
            this.customGridControlForms.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridControlForms.DataSource = this.bindingSourceForms;
            this.customGridControlForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControlForms.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControlForms.Location = new System.Drawing.Point(3, 68);
            this.customGridControlForms.MainView = this.gridViewForms;
            this.customGridControlForms.Name = "customGridControlForms";
            this.customGridControlForms.ObjectName = null;
            this.tableLayoutPanel1.SetRowSpan(this.customGridControlForms, 9);
            this.customGridControlForms.Size = new System.Drawing.Size(501, 583);
            this.customGridControlForms.TabIndex = 1;
            this.customGridControlForms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewForms});
            this.customGridControlForms.Load += new System.EventHandler(this.customGridControlForms_Load);
            this.customGridControlForms.Click += new System.EventHandler(this.customGridControlForms_Click);
            this.customGridControlForms.KeyUp += new System.Windows.Forms.KeyEventHandler(this.customGridControlForms_KeyUp);
            // 
            // gridViewForms
            // 
            this.gridViewForms.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ProjectFormsID,
            this.NameForm,
            this.NameFormRus,
            this.UserName,
            this.Missing,
            this.Added});
            this.gridViewForms.GridControl = this.customGridControlForms;
            this.gridViewForms.Name = "gridViewForms";
            this.gridViewForms.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewForms.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewForms.OptionsEditForm.EditFormColumnCount = 1;
            this.gridViewForms.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewForms_RowStyle);
            this.gridViewForms.EditFormHidden += new DevExpress.XtraGrid.Views.Grid.EditFormHiddenEventHandler(this.gridViewForms_EditFormHidden);
            this.gridViewForms.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridViewForms_InitNewRow);
            this.gridViewForms.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewForms_RowUpdated);
            // 
            // ProjectFormsID
            // 
            this.ProjectFormsID.Caption = "Ид формы";
            this.ProjectFormsID.FieldName = "ProjectFormsID";
            this.ProjectFormsID.Name = "ProjectFormsID";
            this.ProjectFormsID.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.ProjectFormsID.Visible = true;
            this.ProjectFormsID.VisibleIndex = 0;
            this.ProjectFormsID.Width = 72;
            // 
            // NameForm
            // 
            this.NameForm.Caption = "Имя формы";
            this.NameForm.FieldName = "NameForm";
            this.NameForm.Name = "NameForm";
            this.NameForm.Visible = true;
            this.NameForm.VisibleIndex = 1;
            this.NameForm.Width = 214;
            // 
            // NameFormRus
            // 
            this.NameFormRus.Caption = "Русское имя";
            this.NameFormRus.FieldName = "NameFormRus";
            this.NameFormRus.Name = "NameFormRus";
            this.NameFormRus.Visible = true;
            this.NameFormRus.VisibleIndex = 2;
            this.NameFormRus.Width = 207;
            // 
            // UserName
            // 
            this.UserName.Caption = "Создатель";
            this.UserName.FieldName = "UserName";
            this.UserName.Name = "UserName";
            this.UserName.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.UserName.Visible = true;
            this.UserName.VisibleIndex = 3;
            this.UserName.Width = 110;
            // 
            // Missing
            // 
            this.Missing.Caption = "Missing";
            this.Missing.FieldName = "Missing";
            this.Missing.Name = "Missing";
            // 
            // Added
            // 
            this.Added.Caption = "Added";
            this.Added.FieldName = "Added";
            this.Added.Name = "Added";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customGridControlForms, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customGridControlObject, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1268, 654);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel3.Controls.Add(this.customButtonAddObject, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.customButtonDeleteObject, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.customButtonLoadObject, 3, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(510, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(755, 59);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // customButtonAddObject
            // 
            this.customButtonAddObject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonAddObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonAddObject.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonAddObject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonAddObject.Location = new System.Drawing.Point(3, 32);
            this.customButtonAddObject.Name = "customButtonAddObject";
            this.customButtonAddObject.ObjectName = null;
            this.customButtonAddObject.Size = new System.Drawing.Size(182, 24);
            this.customButtonAddObject.TabIndex = 0;
            this.customButtonAddObject.Text = "Добавить Объект";
            this.customButtonAddObject.UseVisualStyleBackColor = false;
            this.customButtonAddObject.Click += new System.EventHandler(this.customButton1_Click);
            // 
            // customButtonDeleteObject
            // 
            this.customButtonDeleteObject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonDeleteObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonDeleteObject.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonDeleteObject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonDeleteObject.Location = new System.Drawing.Point(191, 32);
            this.customButtonDeleteObject.Name = "customButtonDeleteObject";
            this.customButtonDeleteObject.ObjectName = null;
            this.customButtonDeleteObject.Size = new System.Drawing.Size(182, 24);
            this.customButtonDeleteObject.TabIndex = 1;
            this.customButtonDeleteObject.Text = "Удалить Объект";
            this.customButtonDeleteObject.UseVisualStyleBackColor = false;
            this.customButtonDeleteObject.Click += new System.EventHandler(this.customButton2_Click);
            // 
            // customButtonLoadObject
            // 
            this.customButtonLoadObject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonLoadObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonLoadObject.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonLoadObject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonLoadObject.Location = new System.Drawing.Point(567, 32);
            this.customButtonLoadObject.Name = "customButtonLoadObject";
            this.customButtonLoadObject.ObjectName = null;
            this.customButtonLoadObject.Size = new System.Drawing.Size(185, 24);
            this.customButtonLoadObject.TabIndex = 2;
            this.customButtonLoadObject.Text = "Загрузить объекты";
            this.customButtonLoadObject.UseVisualStyleBackColor = false;
            this.customButtonLoadObject.Click += new System.EventHandler(this.customButtonLoadObject_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.customButtonLoadMenu, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.customButtonLoadForm, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.customButtonFormAdd, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.customButtonDeleteForm, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.customCheckBoxMyForm, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(501, 59);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // customButtonLoadForm
            // 
            this.customButtonLoadForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonLoadForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonLoadForm.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonLoadForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonLoadForm.Location = new System.Drawing.Point(3, 3);
            this.customButtonLoadForm.Name = "customButtonLoadForm";
            this.customButtonLoadForm.ObjectName = null;
            this.customButtonLoadForm.Size = new System.Drawing.Size(161, 23);
            this.customButtonLoadForm.TabIndex = 3;
            this.customButtonLoadForm.Text = "Загрузить формы";
            this.customButtonLoadForm.UseVisualStyleBackColor = false;
            this.customButtonLoadForm.Click += new System.EventHandler(this.customButtonLoadForm_Click);
            // 
            // customButtonFormAdd
            // 
            this.customButtonFormAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonFormAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonFormAdd.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonFormAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonFormAdd.Location = new System.Drawing.Point(3, 32);
            this.customButtonFormAdd.Name = "customButtonFormAdd";
            this.customButtonFormAdd.ObjectName = null;
            this.customButtonFormAdd.Size = new System.Drawing.Size(161, 24);
            this.customButtonFormAdd.TabIndex = 0;
            this.customButtonFormAdd.Text = "Добавить Форму";
            this.customButtonFormAdd.UseVisualStyleBackColor = false;
            this.customButtonFormAdd.Click += new System.EventHandler(this.customButtonFormAdd_Click);
            // 
            // customButtonDeleteForm
            // 
            this.customButtonDeleteForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonDeleteForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonDeleteForm.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonDeleteForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonDeleteForm.Location = new System.Drawing.Point(170, 32);
            this.customButtonDeleteForm.Name = "customButtonDeleteForm";
            this.customButtonDeleteForm.ObjectName = null;
            this.customButtonDeleteForm.Size = new System.Drawing.Size(161, 24);
            this.customButtonDeleteForm.TabIndex = 1;
            this.customButtonDeleteForm.Text = "Удалить Форму";
            this.customButtonDeleteForm.UseVisualStyleBackColor = false;
            this.customButtonDeleteForm.Click += new System.EventHandler(this.customButtonDeleteForm_Click);
            // 
            // customCheckBoxMyForm
            // 
            this.customCheckBoxMyForm.AutoSize = true;
            this.customCheckBoxMyForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customCheckBoxMyForm.Font = new System.Drawing.Font("Arial", 10F);
            this.customCheckBoxMyForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.customCheckBoxMyForm.Location = new System.Drawing.Point(337, 32);
            this.customCheckBoxMyForm.Name = "customCheckBoxMyForm";
            this.customCheckBoxMyForm.ObjectName = null;
            this.customCheckBoxMyForm.Size = new System.Drawing.Size(161, 24);
            this.customCheckBoxMyForm.TabIndex = 2;
            this.customCheckBoxMyForm.Text = "Созданные мною";
            this.customCheckBoxMyForm.UseVisualStyleBackColor = true;
            this.customCheckBoxMyForm.CheckedChanged += new System.EventHandler(this.customCheckBoxMyForm_CheckedChanged);
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "NameForm";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "NameForm";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.FieldName = "NameForm";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Создатель";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 3;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Создатель";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Русское имя";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 2;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Русское имя";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Русское имя";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 2;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Русское имя";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 2;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Русское имя";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 2;
            // 
            // customButtonLoadMenu
            // 
            this.customButtonLoadMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButtonLoadMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButtonLoadMenu.Font = new System.Drawing.Font("Arial", 10F);
            this.customButtonLoadMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButtonLoadMenu.Location = new System.Drawing.Point(170, 3);
            this.customButtonLoadMenu.Name = "customButtonLoadMenu";
            this.customButtonLoadMenu.ObjectName = null;
            this.customButtonLoadMenu.Size = new System.Drawing.Size(161, 23);
            this.customButtonLoadMenu.TabIndex = 4;
            this.customButtonLoadMenu.Text = "Загрузить меню";
            this.customButtonLoadMenu.UseVisualStyleBackColor = false;
            this.customButtonLoadMenu.Click += new System.EventHandler(this.customButtonLoadMenu_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 654);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AdminForm";
            this.Text = "Администрирование форм";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControlForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceGroup)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

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
    }
}