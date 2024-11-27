namespace SewingProduction.form
{
    partial class SpravForAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpravForAll));
            this.gridControlSprav = new DevExpress.XtraGrid.GridControl();
            this.spravList = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButtonRed = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.AddTab = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageAdd = new DevExpress.XtraTab.XtraTabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.labelKod = new System.Windows.Forms.Label();
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.simpleButtonAddOtm = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonAddSave = new DevExpress.XtraEditors.SimpleButton();
            this.nameColumnList = new System.Windows.Forms.BindingSource(this.components);
            this.simpleButtonDel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSprav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spravList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddTab)).BeginInit();
            this.AddTab.SuspendLayout();
            this.xtraTabPageAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nameColumnList)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlSprav
            // 
            this.gridControlSprav.DataSource = this.spravList;
            this.gridControlSprav.Location = new System.Drawing.Point(12, 12);
            this.gridControlSprav.MainView = this.gridView1;
            this.gridControlSprav.Name = "gridControlSprav";
            this.gridControlSprav.Size = new System.Drawing.Size(1387, 889);
            this.gridControlSprav.TabIndex = 0;
            this.gridControlSprav.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlSprav.Load += new System.EventHandler(this.gridControlSprav_Load);
            this.gridControlSprav.Click += new System.EventHandler(this.gridControlSprav_Click);
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
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // simpleButtonRed
            // 
            this.simpleButtonRed.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonRed.Appearance.Options.UseFont = true;
            this.simpleButtonRed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonRed.ImageOptions.Image")));
            this.simpleButtonRed.Location = new System.Drawing.Point(1009, 907);
            this.simpleButtonRed.Name = "simpleButtonRed";
            this.simpleButtonRed.Size = new System.Drawing.Size(192, 45);
            this.simpleButtonRed.TabIndex = 7;
            this.simpleButtonRed.Text = "Редактировать";
            this.simpleButtonRed.Click += new System.EventHandler(this.simpleButtonRed_Click);
            // 
            // simpleButtonAdd
            // 
            this.simpleButtonAdd.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonAdd.Appearance.Options.UseFont = true;
            this.simpleButtonAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonAdd.ImageOptions.Image")));
            this.simpleButtonAdd.Location = new System.Drawing.Point(1207, 907);
            this.simpleButtonAdd.Name = "simpleButtonAdd";
            this.simpleButtonAdd.Size = new System.Drawing.Size(192, 45);
            this.simpleButtonAdd.TabIndex = 6;
            this.simpleButtonAdd.Text = "Добавить";
            this.simpleButtonAdd.Click += new System.EventHandler(this.simpleButtonAdd_Click);
            // 
            // AddTab
            // 
            this.AddTab.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.AddTab.Appearance.Options.UseBackColor = true;
            this.AddTab.Location = new System.Drawing.Point(1405, 12);
            this.AddTab.Name = "AddTab";
            this.AddTab.SelectedTabPage = this.xtraTabPageAdd;
            this.AddTab.Size = new System.Drawing.Size(505, 889);
            this.AddTab.TabIndex = 8;
            this.AddTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageAdd});
            // 
            // xtraTabPageAdd
            // 
            this.xtraTabPageAdd.Appearance.PageClient.BackColor = System.Drawing.Color.Black;
            this.xtraTabPageAdd.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPageAdd.Controls.Add(this.simpleButtonDel);
            this.xtraTabPageAdd.Controls.Add(this.label10);
            this.xtraTabPageAdd.Controls.Add(this.textBox10);
            this.xtraTabPageAdd.Controls.Add(this.label9);
            this.xtraTabPageAdd.Controls.Add(this.textBox9);
            this.xtraTabPageAdd.Controls.Add(this.label8);
            this.xtraTabPageAdd.Controls.Add(this.textBox8);
            this.xtraTabPageAdd.Controls.Add(this.label7);
            this.xtraTabPageAdd.Controls.Add(this.textBox7);
            this.xtraTabPageAdd.Controls.Add(this.label6);
            this.xtraTabPageAdd.Controls.Add(this.textBox6);
            this.xtraTabPageAdd.Controls.Add(this.label5);
            this.xtraTabPageAdd.Controls.Add(this.textBox5);
            this.xtraTabPageAdd.Controls.Add(this.label4);
            this.xtraTabPageAdd.Controls.Add(this.textBox4);
            this.xtraTabPageAdd.Controls.Add(this.label3);
            this.xtraTabPageAdd.Controls.Add(this.textBox3);
            this.xtraTabPageAdd.Controls.Add(this.labelKod);
            this.xtraTabPageAdd.Controls.Add(this.textBoxKod);
            this.xtraTabPageAdd.Controls.Add(this.label2);
            this.xtraTabPageAdd.Controls.Add(this.textBox2);
            this.xtraTabPageAdd.Controls.Add(this.label1);
            this.xtraTabPageAdd.Controls.Add(this.textBox1);
            this.xtraTabPageAdd.Controls.Add(this.simpleButtonAddOtm);
            this.xtraTabPageAdd.Controls.Add(this.simpleButtonAddSave);
            this.xtraTabPageAdd.Name = "xtraTabPageAdd";
            this.xtraTabPageAdd.PageVisible = false;
            this.xtraTabPageAdd.Size = new System.Drawing.Size(503, 864);
            this.xtraTabPageAdd.Text = "Добавить/Редактировать";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(10, 450);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Поле10";
            // 
            // textBox10
            // 
            this.textBox10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox10.Location = new System.Drawing.Point(226, 447);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(269, 25);
            this.textBox10.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(10, 405);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 17);
            this.label9.TabIndex = 28;
            this.label9.Text = "Поле9";
            // 
            // textBox9
            // 
            this.textBox9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox9.Location = new System.Drawing.Point(226, 402);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(269, 25);
            this.textBox9.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(10, 361);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "Поле8";
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox8.Location = new System.Drawing.Point(226, 358);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(269, 25);
            this.textBox8.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(10, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Поле7";
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox7.Location = new System.Drawing.Point(226, 316);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(269, 25);
            this.textBox7.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(10, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Поле6";
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox6.Location = new System.Drawing.Point(226, 272);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(269, 25);
            this.textBox6.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(10, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Поле5";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox5.Location = new System.Drawing.Point(226, 227);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(269, 25);
            this.textBox5.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(10, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Поле4";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(226, 183);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(269, 25);
            this.textBox4.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(10, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Поле3";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox3.Location = new System.Drawing.Point(226, 141);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(269, 25);
            this.textBox3.TabIndex = 15;
            // 
            // labelKod
            // 
            this.labelKod.AutoSize = true;
            this.labelKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKod.Location = new System.Drawing.Point(10, 26);
            this.labelKod.Name = "labelKod";
            this.labelKod.Size = new System.Drawing.Size(34, 17);
            this.labelKod.TabIndex = 14;
            this.labelKod.Text = "Код";
            // 
            // textBoxKod
            // 
            this.textBoxKod.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxKod.Cursor = System.Windows.Forms.Cursors.No;
            this.textBoxKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxKod.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxKod.Location = new System.Drawing.Point(226, 23);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.ReadOnly = true;
            this.textBoxKod.Size = new System.Drawing.Size(63, 25);
            this.textBoxKod.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(10, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Поле2";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(226, 99);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(269, 25);
            this.textBox2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Поле1";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(226, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 25);
            this.textBox1.TabIndex = 9;
            // 
            // simpleButtonAddOtm
            // 
            this.simpleButtonAddOtm.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonAddOtm.Appearance.Options.UseFont = true;
            this.simpleButtonAddOtm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonAddOtm.ImageOptions.Image")));
            this.simpleButtonAddOtm.Location = new System.Drawing.Point(16, 808);
            this.simpleButtonAddOtm.Name = "simpleButtonAddOtm";
            this.simpleButtonAddOtm.Size = new System.Drawing.Size(148, 45);
            this.simpleButtonAddOtm.TabIndex = 8;
            this.simpleButtonAddOtm.Text = "Отмена";
            this.simpleButtonAddOtm.Click += new System.EventHandler(this.simpleButtonAddOtm_Click);
            // 
            // simpleButtonAddSave
            // 
            this.simpleButtonAddSave.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonAddSave.Appearance.Options.UseFont = true;
            this.simpleButtonAddSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonAddSave.ImageOptions.Image")));
            this.simpleButtonAddSave.Location = new System.Drawing.Point(342, 808);
            this.simpleButtonAddSave.Name = "simpleButtonAddSave";
            this.simpleButtonAddSave.Size = new System.Drawing.Size(148, 45);
            this.simpleButtonAddSave.TabIndex = 7;
            this.simpleButtonAddSave.Text = "Сохранить";
            this.simpleButtonAddSave.Click += new System.EventHandler(this.simpleButtonAddSave_Click);
            // 
            // simpleButtonDel
            // 
            this.simpleButtonDel.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonDel.Appearance.Options.UseFont = true;
            this.simpleButtonDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButtonDel.Location = new System.Drawing.Point(179, 808);
            this.simpleButtonDel.Name = "simpleButtonDel";
            this.simpleButtonDel.Size = new System.Drawing.Size(148, 45);
            this.simpleButtonDel.TabIndex = 9;
            this.simpleButtonDel.Text = "Удалить";
            this.simpleButtonDel.Click += new System.EventHandler(this.simpleButtonDel_Click);
            // 
            // SpravForAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1914, 961);
            this.Controls.Add(this.AddTab);
            this.Controls.Add(this.simpleButtonRed);
            this.Controls.Add(this.simpleButtonAdd);
            this.Controls.Add(this.gridControlSprav);
            this.Name = "SpravForAll";
            this.Text = "Cправочник";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpravForAll_FormClosing);
            this.Load += new System.EventHandler(this.SpravForAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSprav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spravList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddTab)).EndInit();
            this.AddTab.ResumeLayout(false);
            this.xtraTabPageAdd.ResumeLayout(false);
            this.xtraTabPageAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nameColumnList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlSprav;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource spravList;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRed;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAdd;
        private DevExpress.XtraTab.XtraTabControl AddTab;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdd;
        private System.Windows.Forms.Label labelKod;
        private System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAddOtm;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAddSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.BindingSource nameColumnList;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDel;
    }
}