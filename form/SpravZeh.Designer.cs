namespace SewingProduction.form
{
    partial class SpravZeh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpravZeh));
            this.labelKod = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlSprav = new DevExpress.XtraGrid.GridControl();
            this.spravList = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.simpleButtonRed = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.AddTab = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageAdd = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxAdres = new System.Windows.Forms.TextBox();
            this.simpleButtonDel = new DevExpress.XtraEditors.SimpleButton();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.simpleButtonAddSave = new DevExpress.XtraEditors.SimpleButton();
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.simpleButtonAddOtm = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxVidProizv = new System.Windows.Forms.ComboBox();
            this.linkLabelVid = new System.Windows.Forms.LinkLabel();
            this.labelSave = new System.Windows.Forms.Label();
            this.nameColumnList = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSprav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spravList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddTab)).BeginInit();
            this.AddTab.SuspendLayout();
            this.xtraTabPageAdd.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nameColumnList)).BeginInit();
            this.SuspendLayout();
            // 
            // labelKod
            // 
            this.labelKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelKod.AutoSize = true;
            this.labelKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKod.Location = new System.Drawing.Point(3, 9);
            this.labelKod.Name = "labelKod";
            this.labelKod.Size = new System.Drawing.Size(34, 17);
            this.labelKod.TabIndex = 14;
            this.labelKod.Text = "Код";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Адрес";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Название";
            // 
            // gridView1
            // 
            this.gridView1.AppearancePrint.FilterPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridView1.AppearancePrint.FilterPanel.Options.UseBackColor = true;
            this.gridView1.AppearancePrint.Lines.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridView1.AppearancePrint.Lines.Options.UseBackColor = true;
            this.gridView1.GridControl = this.gridControlSprav;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridControlSprav
            // 
            this.gridControlSprav.DataSource = this.spravList;
            this.gridControlSprav.Location = new System.Drawing.Point(3, 3);
            this.gridControlSprav.MainView = this.gridView1;
            this.gridControlSprav.MaximumSize = new System.Drawing.Size(1400, 900);
            this.gridControlSprav.Name = "gridControlSprav";
            this.gridControlSprav.Size = new System.Drawing.Size(1352, 885);
            this.gridControlSprav.TabIndex = 0;
            this.gridControlSprav.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlSprav.Load += new System.EventHandler(this.gridControlSprav_Load);
            this.gridControlSprav.Click += new System.EventHandler(this.gridControlSprav_Click);
            this.gridControlSprav.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridControlSprav_KeyUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.simpleButtonRed, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.simpleButtonAdd, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 894);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1354, 60);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // simpleButtonRed
            // 
            this.simpleButtonRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonRed.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonRed.Appearance.Options.UseFont = true;
            this.simpleButtonRed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.simpleButtonRed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonRed.ImageOptions.Image")));
            this.simpleButtonRed.Location = new System.Drawing.Point(892, 3);
            this.simpleButtonRed.MaximumSize = new System.Drawing.Size(187, 43);
            this.simpleButtonRed.MinimumSize = new System.Drawing.Size(100, 30);
            this.simpleButtonRed.Name = "simpleButtonRed";
            this.simpleButtonRed.Size = new System.Drawing.Size(187, 43);
            this.simpleButtonRed.TabIndex = 9;
            this.simpleButtonRed.Text = "Редактировать";
            this.simpleButtonRed.Click += new System.EventHandler(this.simpleButtonRed_Click);
            // 
            // simpleButtonAdd
            // 
            this.simpleButtonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonAdd.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonAdd.Appearance.Options.UseFont = true;
            this.simpleButtonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.simpleButtonAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonAdd.ImageOptions.Image")));
            this.simpleButtonAdd.Location = new System.Drawing.Point(1164, 3);
            this.simpleButtonAdd.MaximumSize = new System.Drawing.Size(187, 43);
            this.simpleButtonAdd.MinimumSize = new System.Drawing.Size(100, 30);
            this.simpleButtonAdd.Name = "simpleButtonAdd";
            this.simpleButtonAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.simpleButtonAdd.Size = new System.Drawing.Size(187, 43);
            this.simpleButtonAdd.TabIndex = 6;
            this.simpleButtonAdd.Text = "Добавить";
            this.simpleButtonAdd.Click += new System.EventHandler(this.simpleButtonAdd_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.gridControlSprav, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.AddTab, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.71592F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.284079F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1914, 961);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // AddTab
            // 
            this.AddTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddTab.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.AddTab.Appearance.Options.UseBackColor = true;
            this.AddTab.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.AddTab.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.AddTab.Location = new System.Drawing.Point(1363, 3);
            this.AddTab.Name = "AddTab";
            this.AddTab.SelectedTabPage = this.xtraTabPageAdd;
            this.AddTab.Size = new System.Drawing.Size(548, 885);
            this.AddTab.TabIndex = 8;
            this.AddTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageAdd});
            // 
            // xtraTabPageAdd
            // 
            this.xtraTabPageAdd.Appearance.PageClient.BackColor = System.Drawing.Color.Black;
            this.xtraTabPageAdd.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPageAdd.Controls.Add(this.tableLayoutPanel3);
            this.xtraTabPageAdd.MaximumSize = new System.Drawing.Size(550, 850);
            this.xtraTabPageAdd.MinimumSize = new System.Drawing.Size(485, 585);
            this.xtraTabPageAdd.Name = "xtraTabPageAdd";
            this.xtraTabPageAdd.PageVisible = false;
            this.xtraTabPageAdd.Size = new System.Drawing.Size(546, 860);
            this.xtraTabPageAdd.Text = "Добавить/Редактировать";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tableLayoutPanel3.Controls.Add(this.labelKod, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxAdres, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonDel, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.textBoxName, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonAddSave, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.textBoxKod, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonAddOtm, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.comboBoxVidProizv, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.linkLabelVid, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelSave, 2, 4);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.MaximumSize = new System.Drawing.Size(550, 850);
            this.tableLayoutPanel3.MinimumSize = new System.Drawing.Size(488, 565);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(540, 850);
            this.tableLayoutPanel3.TabIndex = 31;
            // 
            // textBoxAdres
            // 
            this.textBoxAdres.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.SetColumnSpan(this.textBoxAdres, 2);
            this.textBoxAdres.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAdres.Location = new System.Drawing.Point(191, 110);
            this.textBoxAdres.Name = "textBoxAdres";
            this.textBoxAdres.Size = new System.Drawing.Size(320, 25);
            this.textBoxAdres.TabIndex = 15;
            // 
            // simpleButtonDel
            // 
            this.simpleButtonDel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButtonDel.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonDel.Appearance.Options.UseFont = true;
            this.simpleButtonDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonDel.ImageOptions.Image")));
            this.simpleButtonDel.Location = new System.Drawing.Point(208, 163);
            this.simpleButtonDel.Name = "simpleButtonDel";
            this.simpleButtonDel.Size = new System.Drawing.Size(148, 43);
            this.simpleButtonDel.TabIndex = 9;
            this.simpleButtonDel.Text = "Удалить";
            this.simpleButtonDel.Click += new System.EventHandler(this.simpleButtonDel_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxName.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.SetColumnSpan(this.textBoxName, 2);
            this.textBoxName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(191, 40);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(320, 25);
            this.textBoxName.TabIndex = 9;
            // 
            // simpleButtonAddSave
            // 
            this.simpleButtonAddSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButtonAddSave.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonAddSave.Appearance.Options.UseFont = true;
            this.simpleButtonAddSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonAddSave.ImageOptions.Image")));
            this.simpleButtonAddSave.Location = new System.Drawing.Point(384, 163);
            this.simpleButtonAddSave.Name = "simpleButtonAddSave";
            this.simpleButtonAddSave.Size = new System.Drawing.Size(148, 43);
            this.simpleButtonAddSave.TabIndex = 7;
            this.simpleButtonAddSave.Text = "Сохранить";
            this.simpleButtonAddSave.Click += new System.EventHandler(this.simpleButtonAddSave_Click);
            // 
            // textBoxKod
            // 
            this.textBoxKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxKod.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxKod.Cursor = System.Windows.Forms.Cursors.No;
            this.textBoxKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxKod.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxKod.Location = new System.Drawing.Point(191, 5);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.ReadOnly = true;
            this.textBoxKod.Size = new System.Drawing.Size(63, 25);
            this.textBoxKod.TabIndex = 13;
            // 
            // simpleButtonAddOtm
            // 
            this.simpleButtonAddOtm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButtonAddOtm.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonAddOtm.Appearance.Options.UseFont = true;
            this.simpleButtonAddOtm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonAddOtm.ImageOptions.Image")));
            this.simpleButtonAddOtm.Location = new System.Drawing.Point(20, 163);
            this.simpleButtonAddOtm.Name = "simpleButtonAddOtm";
            this.simpleButtonAddOtm.Size = new System.Drawing.Size(148, 43);
            this.simpleButtonAddOtm.TabIndex = 8;
            this.simpleButtonAddOtm.Text = "Отмена";
            this.simpleButtonAddOtm.Click += new System.EventHandler(this.simpleButtonAddOtm_Click);
            // 
            // comboBoxVidProizv
            // 
            this.comboBoxVidProizv.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.SetColumnSpan(this.comboBoxVidProizv, 2);
            this.comboBoxVidProizv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVidProizv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxVidProizv.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxVidProizv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxVidProizv.FormattingEnabled = true;
            this.comboBoxVidProizv.Location = new System.Drawing.Point(191, 75);
            this.comboBoxVidProizv.Name = "comboBoxVidProizv";
            this.comboBoxVidProizv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBoxVidProizv.Size = new System.Drawing.Size(320, 25);
            this.comboBoxVidProizv.TabIndex = 17;
            this.comboBoxVidProizv.Enter += new System.EventHandler(this.comboBoxVidProizv_Enter);
            // 
            // linkLabelVid
            // 
            this.linkLabelVid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabelVid.AutoSize = true;
            this.linkLabelVid.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelVid.LinkColor = System.Drawing.Color.Black;
            this.linkLabelVid.Location = new System.Drawing.Point(3, 79);
            this.linkLabelVid.Name = "linkLabelVid";
            this.linkLabelVid.Size = new System.Drawing.Size(134, 17);
            this.linkLabelVid.TabIndex = 18;
            this.linkLabelVid.TabStop = true;
            this.linkLabelVid.Text = "Вид производства";
            this.linkLabelVid.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelVid_LinkClicked);
            // 
            // labelSave
            // 
            this.labelSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelSave.AutoSize = true;
            this.labelSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSave.Location = new System.Drawing.Point(405, 141);
            this.labelSave.Name = "labelSave";
            this.labelSave.Size = new System.Drawing.Size(105, 19);
            this.labelSave.TabIndex = 32;
            this.labelSave.Text = "Сохранено!";
            this.labelSave.Visible = false;
            // 
            // SpravZeh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1914, 961);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "SpravZeh";
            this.Text = "SpravZeh";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpravForAll_FormClosing);
            this.Load += new System.EventHandler(this.SpravZeh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSprav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spravList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AddTab)).EndInit();
            this.AddTab.ResumeLayout(false);
            this.xtraTabPageAdd.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nameColumnList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelKod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox textBoxAdres;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDel;
        private System.Windows.Forms.TextBox textBoxName;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAddSave;
        private System.Windows.Forms.TextBox textBoxKod;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAddOtm;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControlSprav;
        private System.Windows.Forms.BindingSource spravList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRed;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraTab.XtraTabControl AddTab;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdd;
        private System.Windows.Forms.BindingSource nameColumnList;
        private System.Windows.Forms.ComboBox comboBoxVidProizv;
        private System.Windows.Forms.LinkLabel linkLabelVid;
        private System.Windows.Forms.Label labelSave;
    }
}