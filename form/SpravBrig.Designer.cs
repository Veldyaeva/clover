namespace SewingProduction.form
{
    partial class SpravBrig
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
            this.nameColumnList = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.comboBoxZeh = new System.Windows.Forms.ComboBox();
            this.gridControlSprav = new DevExpress.XtraGrid.GridControl();
            this.spravList = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.textBoxNBrig = new System.Windows.Forms.TextBox();
            this.textBoxBrig = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelKod = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.linkLabelZeh = new System.Windows.Forms.LinkLabel();
            this.labelN = new System.Windows.Forms.Label();
            this.labelSave = new System.Windows.Forms.Label();
            this.simpleButtonAddOtm = new SewingProduction.CustomButton();
            this.simpleButtonDel = new SewingProduction.CustomButton();
            this.simpleButtonAddSave = new SewingProduction.CustomButton();
            this.xtraTabPageAdd = new DevExpress.XtraTab.XtraTabPage();
            this.AddTab = new DevExpress.XtraTab.XtraTabControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.simpleButtonAdd = new SewingProduction.CustomButton();
            this.simpleButtonRed = new SewingProduction.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.nameColumnList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSprav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spravList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.xtraTabPageAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddTab)).BeginInit();
            this.AddTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxKod
            // 
            this.textBoxKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxKod.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxKod.Cursor = System.Windows.Forms.Cursors.No;
            this.textBoxKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxKod.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxKod.Location = new System.Drawing.Point(184, 5);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.ReadOnly = true;
            this.textBoxKod.Size = new System.Drawing.Size(63, 25);
            this.textBoxKod.TabIndex = 13;
            // 
            // comboBoxZeh
            // 
            this.comboBoxZeh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.SetColumnSpan(this.comboBoxZeh, 2);
            this.comboBoxZeh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZeh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxZeh.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxZeh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxZeh.FormattingEnabled = true;
            this.comboBoxZeh.Location = new System.Drawing.Point(184, 110);
            this.comboBoxZeh.Name = "comboBoxZeh";
            this.comboBoxZeh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBoxZeh.Size = new System.Drawing.Size(341, 25);
            this.comboBoxZeh.TabIndex = 17;
            this.comboBoxZeh.Enter += new System.EventHandler(this.comboBoxZeh_Enter);
            // 
            // gridControlSprav
            // 
            this.gridControlSprav.DataSource = this.spravList;
            this.gridControlSprav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSprav.Location = new System.Drawing.Point(3, 3);
            this.gridControlSprav.MainView = this.gridView1;
            this.gridControlSprav.MaximumSize = new System.Drawing.Size(1400, 900);
            this.gridControlSprav.Name = "gridControlSprav";
            this.gridControlSprav.Size = new System.Drawing.Size(474, 606);
            this.gridControlSprav.TabIndex = 0;
            this.gridControlSprav.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlSprav.Load += new System.EventHandler(this.gridControlSprav_Load);
            this.gridControlSprav.Click += new System.EventHandler(this.gridControlSprav_Click);
            this.gridControlSprav.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridControlSprav_KeyUp);
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
            // textBoxNBrig
            // 
            this.textBoxNBrig.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.SetColumnSpan(this.textBoxNBrig, 2);
            this.textBoxNBrig.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNBrig.Location = new System.Drawing.Point(184, 75);
            this.textBoxNBrig.Name = "textBoxNBrig";
            this.textBoxNBrig.Size = new System.Drawing.Size(341, 25);
            this.textBoxNBrig.TabIndex = 15;
            // 
            // textBoxBrig
            // 
            this.textBoxBrig.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxBrig.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.SetColumnSpan(this.textBoxBrig, 2);
            this.textBoxBrig.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBrig.Location = new System.Drawing.Point(184, 40);
            this.textBoxBrig.Name = "textBoxBrig";
            this.textBoxBrig.Size = new System.Drawing.Size(341, 25);
            this.textBoxBrig.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.Controls.Add(this.labelKod, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelB, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxBrig, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxKod, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.linkLabelZeh, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.comboBoxZeh, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.labelN, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.textBoxNBrig, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelSave, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonAddOtm, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonDel, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.simpleButtonAddSave, 2, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.MaximumSize = new System.Drawing.Size(550, 850);
            this.tableLayoutPanel3.MinimumSize = new System.Drawing.Size(450, 550);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(546, 585);
            this.tableLayoutPanel3.TabIndex = 31;
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
            // labelB
            // 
            this.labelB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelB.AutoSize = true;
            this.labelB.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelB.Location = new System.Drawing.Point(3, 44);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(74, 17);
            this.labelB.TabIndex = 10;
            this.labelB.Text = "Название";
            // 
            // linkLabelZeh
            // 
            this.linkLabelZeh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabelZeh.AutoSize = true;
            this.linkLabelZeh.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelZeh.LinkColor = System.Drawing.Color.Black;
            this.linkLabelZeh.Location = new System.Drawing.Point(3, 114);
            this.linkLabelZeh.Name = "linkLabelZeh";
            this.linkLabelZeh.Size = new System.Drawing.Size(36, 17);
            this.linkLabelZeh.TabIndex = 18;
            this.linkLabelZeh.TabStop = true;
            this.linkLabelZeh.Text = "Цех";
            this.linkLabelZeh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelVid_LinkClicked);
            // 
            // labelN
            // 
            this.labelN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelN.AutoSize = true;
            this.labelN.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelN.Location = new System.Drawing.Point(3, 79);
            this.labelN.Name = "labelN";
            this.labelN.Size = new System.Drawing.Size(54, 17);
            this.labelN.TabIndex = 16;
            this.labelN.Text = "Номер";
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
            // simpleButtonAddOtm
            // 
            this.simpleButtonAddOtm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButtonAddOtm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.simpleButtonAddOtm.FlatAppearance.BorderSize = 0;
            this.simpleButtonAddOtm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButtonAddOtm.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonAddOtm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.simpleButtonAddOtm.Location = new System.Drawing.Point(4, 541);
            this.simpleButtonAddOtm.Name = "simpleButtonAddOtm";
            this.simpleButtonAddOtm.Size = new System.Drawing.Size(173, 41);
            this.simpleButtonAddOtm.TabIndex = 34;
            this.simpleButtonAddOtm.Text = "Отмена";
            this.simpleButtonAddOtm.UseVisualStyleBackColor = false;
            this.simpleButtonAddOtm.Click += new System.EventHandler(this.simpleButtonAddOtm_Click);
            // 
            // simpleButtonDel
            // 
            this.simpleButtonDel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButtonDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.simpleButtonDel.FlatAppearance.BorderSize = 0;
            this.simpleButtonDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButtonDel.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.simpleButtonDel.Location = new System.Drawing.Point(185, 541);
            this.simpleButtonDel.Name = "simpleButtonDel";
            this.simpleButtonDel.Size = new System.Drawing.Size(174, 41);
            this.simpleButtonDel.TabIndex = 34;
            this.simpleButtonDel.Text = "Удалить";
            this.simpleButtonDel.UseVisualStyleBackColor = false;
            this.simpleButtonDel.Click += new System.EventHandler(this.simpleButtonDel_Click);
            // 
            // simpleButtonAddSave
            // 
            this.simpleButtonAddSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButtonAddSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.simpleButtonAddSave.FlatAppearance.BorderSize = 0;
            this.simpleButtonAddSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButtonAddSave.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonAddSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.simpleButtonAddSave.Location = new System.Drawing.Point(367, 541);
            this.simpleButtonAddSave.Name = "simpleButtonAddSave";
            this.simpleButtonAddSave.Size = new System.Drawing.Size(174, 41);
            this.simpleButtonAddSave.TabIndex = 34;
            this.simpleButtonAddSave.Text = "Сохранить";
            this.simpleButtonAddSave.UseVisualStyleBackColor = false;
            this.simpleButtonAddSave.Click += new System.EventHandler(this.simpleButtonAddSave_Click);
            // 
            // xtraTabPageAdd
            // 
            this.xtraTabPageAdd.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.xtraTabPageAdd.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPageAdd.Controls.Add(this.tableLayoutPanel3);
            this.xtraTabPageAdd.MaximumSize = new System.Drawing.Size(550, 850);
            this.xtraTabPageAdd.MinimumSize = new System.Drawing.Size(485, 585);
            this.xtraTabPageAdd.Name = "xtraTabPageAdd";
            this.xtraTabPageAdd.PageVisible = false;
            this.xtraTabPageAdd.Size = new System.Drawing.Size(546, 585);
            this.xtraTabPageAdd.Text = "Добавить/Редактировать";
            // 
            // AddTab
            // 
            this.AddTab.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.AddTab.Appearance.Options.UseBackColor = true;
            this.AddTab.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.AddTab.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.AddTab.Dock = System.Windows.Forms.DockStyle.Right;
            this.AddTab.Location = new System.Drawing.Point(483, 3);
            this.AddTab.Name = "AddTab";
            this.AddTab.SelectedTabPage = this.xtraTabPageAdd;
            this.AddTab.Size = new System.Drawing.Size(548, 606);
            this.AddTab.TabIndex = 8;
            this.AddTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageAdd});
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
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.simpleButtonAdd, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.simpleButtonRed, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 615);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(474, 43);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // simpleButtonAdd
            // 
            this.simpleButtonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
<<<<<<< HEAD
            this.simpleButtonAdd.Btn = null;
            this.simpleButtonAdd.ComponentBackColor = System.Drawing.Color.Empty;
            this.simpleButtonAdd.ComponentFontColor = System.Drawing.Color.Empty;
            this.simpleButtonAdd.ComponentSize = new System.Drawing.Size(0, 0);
            this.simpleButtonAdd.Dock = System.Windows.Forms.DockStyle.Fill;
=======
>>>>>>> b2d3435266bb10bae216c2c0adc49d8f201a2ed9
            this.simpleButtonAdd.FlatAppearance.BorderSize = 0;
            this.simpleButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButtonAdd.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.simpleButtonAdd.Location = new System.Drawing.Point(405, 3);
            this.simpleButtonAdd.Name = "simpleButtonAdd";
            this.simpleButtonAdd.Size = new System.Drawing.Size(66, 26);
            this.simpleButtonAdd.TabIndex = 10;
            this.simpleButtonAdd.Text = "Добавить";
            this.simpleButtonAdd.UseVisualStyleBackColor = false;
            this.simpleButtonAdd.Click += new System.EventHandler(this.simpleButtonAdd_Click);
            // 
            // simpleButtonRed
            // 
            this.simpleButtonRed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
<<<<<<< HEAD
            this.simpleButtonRed.Btn = null;
            this.simpleButtonRed.ComponentBackColor = System.Drawing.Color.Empty;
            this.simpleButtonRed.ComponentFontColor = System.Drawing.Color.Empty;
            this.simpleButtonRed.ComponentSize = new System.Drawing.Size(0, 0);
            this.simpleButtonRed.Dock = System.Windows.Forms.DockStyle.Fill;
=======
>>>>>>> b2d3435266bb10bae216c2c0adc49d8f201a2ed9
            this.simpleButtonRed.FlatAppearance.BorderSize = 0;
            this.simpleButtonRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButtonRed.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonRed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.simpleButtonRed.Location = new System.Drawing.Point(334, 3);
            this.simpleButtonRed.Name = "simpleButtonRed";
            this.simpleButtonRed.Size = new System.Drawing.Size(65, 26);
            this.simpleButtonRed.TabIndex = 11;
            this.simpleButtonRed.Text = "Редактировать";
            this.simpleButtonRed.UseVisualStyleBackColor = false;
            this.simpleButtonRed.Click += new System.EventHandler(this.simpleButtonRed_Click);
            // 
            // SpravBrig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 661);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "SpravBrig";
            this.Text = "SpravBrig";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpravForAll_FormClosing);
            this.Load += new System.EventHandler(this.SpravBrig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nameColumnList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSprav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spravList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.xtraTabPageAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AddTab)).EndInit();
            this.AddTab.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource nameColumnList;
        private System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.ComboBox comboBoxZeh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelKod;
        private System.Windows.Forms.Label labelN;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.TextBox textBoxNBrig;
        private System.Windows.Forms.TextBox textBoxBrig;
        private System.Windows.Forms.LinkLabel linkLabelZeh;
        private DevExpress.XtraGrid.GridControl gridControlSprav;
        private System.Windows.Forms.BindingSource spravList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdd;
        private DevExpress.XtraTab.XtraTabControl AddTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelSave;
        private CustomButton simpleButtonAdd;
        private CustomButton simpleButtonRed;
        private CustomButton simpleButtonAddOtm;
        private CustomButton simpleButtonAddSave;
        private CustomButton simpleButtonDel;
    }
}