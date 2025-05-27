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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colkod_o = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colspec = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colrazryd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colobor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext_ob = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkod_ob = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colkod_proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext_proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext_vyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemResourcesComboBox1 = new DevExpress.XtraScheduler.UI.RepositoryItemResourcesComboBox();
            this.normoperBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.customOkButton1 = new SewingProduction.CustomOkButton();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemResourcesComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normoperBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
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
            this.customGridControl1.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.tablePanel1.SetColumn(this.customGridControl1, 0);
            this.customGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControl1.Location = new System.Drawing.Point(13, 74);
            this.customGridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.gridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.ObjectName = null;
            this.customGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemResourcesComboBox1,
            this.repositoryItemComboBox1});
            this.tablePanel1.SetRow(this.customGridControl1, 2);
            this.customGridControl1.Size = new System.Drawing.Size(922, 354);
            this.customGridControl1.TabIndex = 0;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colkod_o,
            this.coltext,
            this.colspec,
            this.colrazryd,
            this.colobor,
            this.coltext_ob,
            this.colkod_ob,
            this.colsek,
            this.proizv,
            this.colkod_proizv,
            this.coltext_proizv,
            this.coltext_vyaz,
            this.gridColumn6});
            this.gridView1.GridControl = this.customGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.customOkButton1_Click);
            // 
            // colkod_o
            // 
            this.colkod_o.Caption = "Код оп.";
            this.colkod_o.FieldName = "kod_o";
            this.colkod_o.Name = "colkod_o";
            this.colkod_o.Visible = true;
            this.colkod_o.VisibleIndex = 0;
            // 
            // coltext
            // 
            this.coltext.Caption = "Наименование";
            this.coltext.FieldName = "text";
            this.coltext.Name = "coltext";
            this.coltext.Visible = true;
            this.coltext.VisibleIndex = 1;
            this.coltext.Width = 278;
            // 
            // colspec
            // 
            this.colspec.Caption = "Специальность";
            this.colspec.FieldName = "spec";
            this.colspec.Name = "colspec";
            this.colspec.Visible = true;
            this.colspec.VisibleIndex = 2;
            this.colspec.Width = 117;
            // 
            // colrazryd
            // 
            this.colrazryd.Caption = "Разряд";
            this.colrazryd.FieldName = "razryd";
            this.colrazryd.Name = "colrazryd";
            this.colrazryd.Visible = true;
            this.colrazryd.VisibleIndex = 3;
            this.colrazryd.Width = 48;
            // 
            // colobor
            // 
            this.colobor.Caption = "Оборуд";
            this.colobor.FieldName = "obor";
            this.colobor.Name = "colobor";
            this.colobor.Visible = true;
            this.colobor.VisibleIndex = 4;
            this.colobor.Width = 48;
            // 
            // coltext_ob
            // 
            this.coltext_ob.Caption = "Оборудование";
            this.coltext_ob.FieldName = "text_ob";
            this.coltext_ob.Name = "coltext_ob";
            this.coltext_ob.Visible = true;
            this.coltext_ob.VisibleIndex = 5;
            this.coltext_ob.Width = 48;
            // 
            // colkod_ob
            // 
            this.colkod_ob.Caption = "Код оборудования";
            this.colkod_ob.FieldName = "kod_ob";
            this.colkod_ob.Name = "colkod_ob";
            this.colkod_ob.Width = 48;
            // 
            // colsek
            // 
            this.colsek.FieldName = "sek";
            this.colsek.Name = "colsek";
            // 
            // proizv
            // 
            this.proizv.Caption = "proizv";
            this.proizv.ColumnEdit = this.repositoryItemComboBox1;
            this.proizv.FieldName = "kod_proizv";
            this.proizv.Name = "proizv";
            this.proizv.Width = 48;
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
            this.colkod_proizv.Width = 48;
            // 
            // coltext_proizv
            // 
            this.coltext_proizv.Caption = "Произв.";
            this.coltext_proizv.FieldName = "text_proizv";
            this.coltext_proizv.Name = "coltext_proizv";
            // 
            // coltext_vyaz
            // 
            this.coltext_vyaz.FieldName = "text_vyaz";
            this.coltext_vyaz.Name = "coltext_vyaz";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Код";
            this.gridColumn6.FieldName = "kod";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
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
            this.tablePanel2.SetColumn(this.radioGroup1, 1);
            this.radioGroup1.Location = new System.Drawing.Point(234, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 4;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Все"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Вязальное"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Швейное"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Носки")});
            this.tablePanel2.SetRow(this.radioGroup1, 0);
            this.radioGroup1.Size = new System.Drawing.Size(572, 21);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // customOkButton1
            // 
            this.customOkButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.tablePanel2.SetColumn(this.customOkButton1, 2);
            this.customOkButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.customOkButton1.Font = new System.Drawing.Font("Arial", 10F);
            this.customOkButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customOkButton1.Location = new System.Drawing.Point(809, 12);
            this.customOkButton1.Name = "customOkButton1";
            this.customOkButton1.ObjectName = null;
            this.tablePanel2.SetRow(this.customOkButton1, 0);
            this.customOkButton1.Size = new System.Drawing.Size(100, 21);
            this.customOkButton1.TabIndex = 3;
            this.customOkButton1.Text = "Ок";
            this.customOkButton1.UseVisualStyleBackColor = false;
            this.customOkButton1.Click += new System.EventHandler(this.customOkButton1_Click);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26.35F)});
            this.tablePanel1.Controls.Add(this.tablePanel2);
            this.tablePanel1.Controls.Add(this.customGridControl1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(948, 441);
            this.tablePanel1.TabIndex = 4;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // tablePanel2
            // 
            this.tablePanel1.SetColumn(this.tablePanel2, 0);
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26.96F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.36F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 12.68F)});
            this.tablePanel2.Controls.Add(this.searchControl1);
            this.tablePanel2.Controls.Add(this.customOkButton1);
            this.tablePanel2.Controls.Add(this.radioGroup1);
            this.tablePanel2.Location = new System.Drawing.Point(13, 12);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel1.SetRow(this.tablePanel2, 0);
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F)});
            this.tablePanel2.Size = new System.Drawing.Size(922, 46);
            this.tablePanel2.TabIndex = 4;
            this.tablePanel2.UseSkinIndents = true;
            // 
            // searchControl1
            // 
            this.searchControl1.Client = this.customGridControl1;
            this.tablePanel2.SetColumn(this.searchControl1, 0);
            this.searchControl1.Location = new System.Drawing.Point(13, 12);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.customGridControl1;
            this.tablePanel2.SetRow(this.searchControl1, 0);
            this.searchControl1.Size = new System.Drawing.Size(217, 20);
            this.searchControl1.TabIndex = 4;
            // 
            // NormOperNew
            // 
            this.ClientSize = new System.Drawing.Size(948, 441);
            this.Controls.Add(this.tablePanel1);
            this.Name = "NormOperNew";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NormOperNew_FormClosing);
            this.Load += new System.EventHandler(this.NormOperNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemResourcesComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normoperBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomGridControl customGridControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
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
        private System.Windows.Forms.BindingSource normoperBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_ob;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_proizv;
        private DevExpress.XtraScheduler.UI.RepositoryItemResourcesComboBox repositoryItemResourcesComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn proizv;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_vyaz;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}
