using SewingProduction.form.TeamWork;

namespace SewingProduction.form
{
    partial class NormOperNew
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customGridControl1 = new SewingProduction.CustomGridControl();
            this.normoperBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aCE_backupDataSet = new SewingProduction.form.TeamWork.ACE_backupDataSet();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colkod_o = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colrazryd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colspec = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colobor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkod_ob = new DevExpress.XtraGrid.Columns.GridColumn();
            this.proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colkod_proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext_ob = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext_proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext_vyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemResourcesComboBox1 = new DevExpress.XtraScheduler.UI.RepositoryItemResourcesComboBox();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.customOkButton1 = new SewingProduction.CustomOkButton();
            this.norm_operTableAdapter = new SewingProduction.form.TeamWork.ACE_backupDataSetTableAdapters.norm_operTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normoperBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aCE_backupDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemResourcesComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // customGridControl1
            // 
            this.customGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl1.DataSource = this.normoperBindingSource;
            this.customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControl1.Location = new System.Drawing.Point(-5, 109);
            this.customGridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.gridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemResourcesComboBox1,
            this.repositoryItemComboBox1});
            this.customGridControl1.Size = new System.Drawing.Size(957, 200);
            this.customGridControl1.TabIndex = 0;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // normoperBindingSource
            // 
            this.normoperBindingSource.DataMember = "norm_oper";
            this.normoperBindingSource.DataSource = this.aCE_backupDataSet;
            // 
            // aCE_backupDataSet
            // 
            this.aCE_backupDataSet.DataSetName = "ACE_backupDataSet";
            this.aCE_backupDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colkod_o,
            this.coltext,
            this.colsek,
            this.colrazryd,
            this.colspec,
            this.colobor,
            this.colkod_ob,
            this.proizv,
            this.colkod_proizv,
            this.coltext_ob,
            this.coltext_proizv,
            this.coltext_vyaz});
            this.gridView1.GridControl = this.customGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colkod_o
            // 
            this.colkod_o.FieldName = "kod_o";
            this.colkod_o.Name = "colkod_o";
            this.colkod_o.Visible = true;
            this.colkod_o.VisibleIndex = 0;
            // 
            // coltext
            // 
            this.coltext.FieldName = "text";
            this.coltext.Name = "coltext";
            this.coltext.Visible = true;
            this.coltext.VisibleIndex = 1;
            // 
            // colsek
            // 
            this.colsek.FieldName = "sek";
            this.colsek.Name = "colsek";
            this.colsek.Visible = true;
            this.colsek.VisibleIndex = 2;
            // 
            // colrazryd
            // 
            this.colrazryd.FieldName = "razryd";
            this.colrazryd.Name = "colrazryd";
            this.colrazryd.Visible = true;
            this.colrazryd.VisibleIndex = 3;
            // 
            // colspec
            // 
            this.colspec.FieldName = "spec";
            this.colspec.Name = "colspec";
            this.colspec.Visible = true;
            this.colspec.VisibleIndex = 4;
            // 
            // colobor
            // 
            this.colobor.FieldName = "obor";
            this.colobor.Name = "colobor";
            this.colobor.Visible = true;
            this.colobor.VisibleIndex = 5;
            // 
            // colkod_ob
            // 
            this.colkod_ob.FieldName = "kod_ob";
            this.colkod_ob.Name = "colkod_ob";
            this.colkod_ob.Visible = true;
            this.colkod_ob.VisibleIndex = 6;
            // 
            // proizv
            // 
            this.proizv.Caption = "proizv";
            this.proizv.ColumnEdit = this.repositoryItemComboBox1;
            this.proizv.FieldName = "kod_proizv";
            this.proizv.Name = "proizv";
            this.proizv.Visible = true;
            this.proizv.VisibleIndex = 7;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // colkod_proizv
            // 
            this.colkod_proizv.ColumnEdit = this.repositoryItemComboBox1;
            this.colkod_proizv.FieldName = "kod_proizv";
            this.colkod_proizv.Name = "colkod_proizv";
            this.colkod_proizv.Visible = true;
            this.colkod_proizv.VisibleIndex = 8;
            // 
            // coltext_ob
            // 
            this.coltext_ob.FieldName = "text_ob";
            this.coltext_ob.Name = "coltext_ob";
            this.coltext_ob.Visible = true;
            this.coltext_ob.VisibleIndex = 9;
            // 
            // coltext_proizv
            // 
            this.coltext_proizv.FieldName = "text_proizv";
            this.coltext_proizv.Name = "coltext_proizv";
            this.coltext_proizv.Visible = true;
            this.coltext_proizv.VisibleIndex = 10;
            // 
            // coltext_vyaz
            // 
            this.coltext_vyaz.FieldName = "text_vyaz";
            this.coltext_vyaz.Name = "coltext_vyaz";
            this.coltext_vyaz.Visible = true;
            this.coltext_vyaz.VisibleIndex = 11;
            // 
            // repositoryItemResourcesComboBox1
            // 
            this.repositoryItemResourcesComboBox1.AutoHeight = false;
            this.repositoryItemResourcesComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemResourcesComboBox1.Name = "repositoryItemResourcesComboBox1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(327, 42);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 4;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Все"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Вязальное"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Швейное"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Носки")});
            this.radioGroup1.Size = new System.Drawing.Size(393, 31);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(57, 42);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DisplayMember = "text";
            this.searchLookUpEdit1.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Properties.ValueMember = "id";
            this.searchLookUpEdit1.Size = new System.Drawing.Size(100, 20);
            this.searchLookUpEdit1.TabIndex = 2;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // customOkButton1
            // 
            this.customOkButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customOkButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.customOkButton1.Font = new System.Drawing.Font("Arial", 10F);
            this.customOkButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customOkButton1.Location = new System.Drawing.Point(220, 357);
            this.customOkButton1.Name = "customOkButton1";
            this.customOkButton1.Size = new System.Drawing.Size(136, 25);
            this.customOkButton1.TabIndex = 3;
            this.customOkButton1.Text = "customOkButton1";
            this.customOkButton1.UseVisualStyleBackColor = false;
            this.customOkButton1.Click += new System.EventHandler(this.customOkButton1_Click);
            // 
            // norm_operTableAdapter
            // 
            this.norm_operTableAdapter.ClearBeforeFill = true;
            // 
            // NormOperNew
            // 
            this.ClientSize = new System.Drawing.Size(948, 441);
            this.Controls.Add(this.customOkButton1);
            this.Controls.Add(this.searchLookUpEdit1);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.customGridControl1);
            this.Name = "NormOperNew";
            this.Load += new System.EventHandler(this.NormOperNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normoperBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aCE_backupDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemResourcesComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomGridControl customGridControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private CustomOkButton customOkButton1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o;
        private DevExpress.XtraGrid.Columns.GridColumn coltext;
        private DevExpress.XtraGrid.Columns.GridColumn colsek;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd;
        private DevExpress.XtraGrid.Columns.GridColumn colspec;
        private DevExpress.XtraGrid.Columns.GridColumn colobor;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_ob;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_proizv;
        private ACE_backupDataSet aCE_backupDataSet;
        private System.Windows.Forms.BindingSource normoperBindingSource;
        private TeamWork.ACE_backupDataSetTableAdapters.norm_operTableAdapter norm_operTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_ob;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_proizv;
        private DevExpress.XtraScheduler.UI.RepositoryItemResourcesComboBox repositoryItemResourcesComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn proizv;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_vyaz;
    }
}
