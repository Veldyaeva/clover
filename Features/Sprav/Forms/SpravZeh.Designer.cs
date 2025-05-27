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
            this.labelKod = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridViewZeh = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlSprav = new DevExpress.XtraGrid.GridControl();
            this.spravList = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customButton1 = new SewingProduction.CustomButton();
            this.customButton2 = new SewingProduction.CustomButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.AddTab = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageAdd = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxAdres = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.comboBoxVidProizv = new System.Windows.Forms.ComboBox();
            this.linkLabelVid = new System.Windows.Forms.LinkLabel();
            this.labelSave = new System.Windows.Forms.Label();
            this.simpleButtonAddSave = new SewingProduction.CustomButton();
            this.simpleButtonDel = new SewingProduction.CustomButton();
            this.simpleButtonAddOtm = new SewingProduction.CustomButton();
            this.nameColumnList = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewZeh)).BeginInit();
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
            // gridViewZeh
            // 
            this.gridViewZeh.AppearancePrint.FilterPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridViewZeh.AppearancePrint.FilterPanel.Options.UseBackColor = true;
            this.gridViewZeh.AppearancePrint.Lines.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridViewZeh.AppearancePrint.Lines.Options.UseBackColor = true;
            this.gridViewZeh.GridControl = this.gridControlSprav;
            this.gridViewZeh.Name = "gridViewZeh";
            this.gridViewZeh.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewZeh.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewZeh.OptionsView.ShowGroupPanel = false;
            // 
            // gridControlSprav
            // 
            this.gridControlSprav.DataSource = this.spravList;
            this.gridControlSprav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSprav.Location = new System.Drawing.Point(3, 3);
            this.gridControlSprav.MainView = this.gridViewZeh;
            this.gridControlSprav.MaximumSize = new System.Drawing.Size(1400, 900);
            this.gridControlSprav.Name = "gridControlSprav";
            this.gridControlSprav.Size = new System.Drawing.Size(474, 606);
            this.gridControlSprav.TabIndex = 0;
            this.gridControlSprav.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewZeh});
            this.gridControlSprav.Load += new System.EventHandler(this.gridControlSprav_Load);
            this.gridControlSprav.Click += new System.EventHandler(this.gridControlSprav_Click);
            this.gridControlSprav.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridControlSprav_KeyUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.customButton1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.customButton2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 615);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(474, 43);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.customButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButton1.FlatAppearance.BorderSize = 0;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Font = new System.Drawing.Font("Arial", 10F);
            this.customButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.customButton1.Location = new System.Drawing.Point(405, 3);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(66, 26);
            this.customButton1.TabIndex = 11;
            this.customButton1.Text = "Добавить";
            this.customButton1.UseVisualStyleBackColor = false;
            this.customButton1.Click += new System.EventHandler(this.simpleButtonAdd_Click);
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.customButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customButton2.FlatAppearance.BorderSize = 0;
            this.customButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton2.Font = new System.Drawing.Font("Arial", 10F);
            this.customButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.customButton2.Location = new System.Drawing.Point(334, 3);
            this.customButton2.Name = "customButton2";
            this.customButton2.Size = new System.Drawing.Size(65, 26);
            this.customButton2.TabIndex = 12;
            this.customButton2.Text = "Редактировать";
            this.customButton2.UseVisualStyleBackColor = false;
            this.customButton2.Click += new System.EventHandler(this.simpleButtonRed_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1034, 661);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // AddTab
            // 
            this.AddTab.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.AddTab.Appearance.Options.UseBackColor = true;
            this.AddTab.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.AddTab.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.AddTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddTab.Location = new System.Drawing.Point(483, 3);
            this.AddTab.Name = "AddTab";
            this.AddTab.SelectedTabPage = this.xtraTabPageAdd;
            this.AddTab.Size = new System.Drawing.Size(548, 606);
            this.AddTab.TabIndex = 8;
            this.AddTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageAdd});
            // 
            // xtraTabPageAdd
            // 
            this.xtraTabPageAdd.Appearance.PageClient.BackColor = System.Drawing.Color.IndianRed;
            this.xtraTabPageAdd.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPageAdd.Controls.Add(this.tableLayoutPanel3);
            this.xtraTabPageAdd.MaximumSize = new System.Drawing.Size(550, 850);
            this.xtraTabPageAdd.MinimumSize = new System.Drawing.Size(485, 585);
            this.xtraTabPageAdd.Name = "xtraTabPageAdd";
            this.xtraTabPageAdd.PageVisible = false;
            this.xtraTabPageAdd.Size = new System.Drawing.Size(546, 581);
            this.xtraTabPageAdd.Text = "Добавить/Редактировать";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.labelKod, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxAdres, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.textBoxName, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxKod, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.comboBoxVidProizv, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.linkLabelVid, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelSave, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonAddSave, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonDel, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonAddOtm, 0, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(546, 581);
            this.tableLayoutPanel3.TabIndex = 31;
            // 
            // textBoxAdres
            // 
            this.textBoxAdres.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.SetColumnSpan(this.textBoxAdres, 2);
            this.textBoxAdres.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAdres.Location = new System.Drawing.Point(185, 110);
            this.textBoxAdres.Name = "textBoxAdres";
            this.textBoxAdres.Size = new System.Drawing.Size(320, 25);
            this.textBoxAdres.TabIndex = 15;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxName.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.SetColumnSpan(this.textBoxName, 2);
            this.textBoxName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(185, 40);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(320, 25);
            this.textBoxName.TabIndex = 9;
            // 
            // textBoxKod
            // 
            this.textBoxKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxKod.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxKod.Cursor = System.Windows.Forms.Cursors.No;
            this.textBoxKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxKod.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxKod.Location = new System.Drawing.Point(185, 5);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.ReadOnly = true;
            this.textBoxKod.Size = new System.Drawing.Size(63, 25);
            this.textBoxKod.TabIndex = 13;
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
            this.comboBoxVidProizv.Location = new System.Drawing.Point(185, 75);
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
            this.labelSave.Location = new System.Drawing.Point(402, 141);
            this.labelSave.Name = "labelSave";
            this.labelSave.Size = new System.Drawing.Size(105, 19);
            this.labelSave.TabIndex = 32;
            this.labelSave.Text = "Сохранено!";
            this.labelSave.Visible = false;
            // 
            // simpleButtonAddSave
            // 
            this.simpleButtonAddSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButtonAddSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.simpleButtonAddSave.FlatAppearance.BorderSize = 0;
            this.simpleButtonAddSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButtonAddSave.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonAddSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.simpleButtonAddSave.Location = new System.Drawing.Point(368, 534);
            this.simpleButtonAddSave.Name = "simpleButtonAddSave";
            this.simpleButtonAddSave.Size = new System.Drawing.Size(174, 44);
            this.simpleButtonAddSave.TabIndex = 33;
            this.simpleButtonAddSave.Text = "Сохранить";
            this.simpleButtonAddSave.UseVisualStyleBackColor = false;
            this.simpleButtonAddSave.Click += new System.EventHandler(this.simpleButtonAddSave_Click);
            // 
            // simpleButtonDel
            // 
            this.simpleButtonDel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButtonDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.simpleButtonDel.FlatAppearance.BorderSize = 0;
            this.simpleButtonDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButtonDel.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.simpleButtonDel.Location = new System.Drawing.Point(186, 534);
            this.simpleButtonDel.Name = "simpleButtonDel";
            this.simpleButtonDel.Size = new System.Drawing.Size(174, 44);
            this.simpleButtonDel.TabIndex = 33;
            this.simpleButtonDel.Text = "Удалить";
            this.simpleButtonDel.UseVisualStyleBackColor = false;
            this.simpleButtonDel.Click += new System.EventHandler(this.simpleButtonDel_Click);
            // 
            // simpleButtonAddOtm
            // 
            this.simpleButtonAddOtm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButtonAddOtm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.simpleButtonAddOtm.FlatAppearance.BorderSize = 0;
            this.simpleButtonAddOtm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButtonAddOtm.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonAddOtm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.simpleButtonAddOtm.Location = new System.Drawing.Point(4, 534);
            this.simpleButtonAddOtm.Name = "simpleButtonAddOtm";
            this.simpleButtonAddOtm.Size = new System.Drawing.Size(174, 44);
            this.simpleButtonAddOtm.TabIndex = 33;
            this.simpleButtonAddOtm.Text = "Отмена";
            this.simpleButtonAddOtm.UseVisualStyleBackColor = false;
            this.simpleButtonAddOtm.Click += new System.EventHandler(this.simpleButtonAddOtm_Click);
            // 
            // SpravZeh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 661);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "SpravZeh";
            this.Text = "SpravZeh";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpravForAll_FormClosing);
            this.Load += new System.EventHandler(this.SpravZeh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewZeh)).EndInit();
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
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxKod;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewZeh;
        private DevExpress.XtraGrid.GridControl gridControlSprav;
        private System.Windows.Forms.BindingSource spravList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraTab.XtraTabControl AddTab;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdd;
        private System.Windows.Forms.BindingSource nameColumnList;
        private System.Windows.Forms.ComboBox comboBoxVidProizv;
        private System.Windows.Forms.LinkLabel linkLabelVid;
        private System.Windows.Forms.Label labelSave;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomButton simpleButtonAddOtm;
        private CustomButton simpleButtonDel;
        private CustomButton simpleButtonAddSave;
    }
}