using SewingProduction.Core.Class;
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpravForAll));
            spravList = new System.Windows.Forms.BindingSource(components);
            nameColumnList = new System.Windows.Forms.BindingSource(components);
            AddTab = new DevExpress.XtraTab.XtraTabControl();
            xtraTabPageAdd = new DevExpress.XtraTab.XtraTabPage();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            labelKod = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            textBox8 = new System.Windows.Forms.TextBox();
            textBox7 = new System.Windows.Forms.TextBox();
            textBox6 = new System.Windows.Forms.TextBox();
            textBox5 = new System.Windows.Forms.TextBox();
            textBox4 = new System.Windows.Forms.TextBox();
            textBox3 = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox9 = new System.Windows.Forms.TextBox();
            textBox10 = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            textBoxKod = new System.Windows.Forms.TextBox();
            labelSave = new System.Windows.Forms.Label();
            simpleButtonAddOtm = new CustomButton();
            simpleButtonDel = new CustomButton();
            simpleButtonAddSave = new CustomButton();
            gridControlSprav = new CustomGridControlColumn();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            simpleButtonRed = new CustomButton();
            simpleButtonAdd = new CustomButton();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)spravList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nameColumnList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AddTab).BeginInit();
            AddTab.SuspendLayout();
            xtraTabPageAdd.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlSprav).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // AddTab
            // 
            AddTab.Appearance.BackColor = System.Drawing.Color.Transparent;
            AddTab.Appearance.Options.UseBackColor = true;
            AddTab.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            AddTab.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            AddTab.Dock = System.Windows.Forms.DockStyle.Right;
            AddTab.Location = new System.Drawing.Point(563, 3);
            AddTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            AddTab.Name = "AddTab";
            AddTab.SelectedTabPage = xtraTabPageAdd;
            AddTab.Size = new System.Drawing.Size(639, 701);
            AddTab.TabIndex = 8;
            AddTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { xtraTabPageAdd });
            // 
            // xtraTabPageAdd
            // 
            // xtraTabPageAdd.Appearance.PageClient.BackColor = System.Drawing.Color.Black;
            xtraTabPageAdd.Appearance.PageClient.Options.UseBackColor = true;
            xtraTabPageAdd.Controls.Add(tableLayoutPanel3);
            xtraTabPageAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            xtraTabPageAdd.MaximumSize = new System.Drawing.Size(630, 981);
            xtraTabPageAdd.MinimumSize = new System.Drawing.Size(566, 675);
            xtraTabPageAdd.Name = "xtraTabPageAdd";
            xtraTabPageAdd.PageVisible = false;
            xtraTabPageAdd.Size = new System.Drawing.Size(630, 676);
            xtraTabPageAdd.Text = "Добавить/Редактировать";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333244F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333359F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333359F));
            tableLayoutPanel3.Controls.Add(labelKod, 0, 0);
            tableLayoutPanel3.Controls.Add(label3, 0, 3);
            tableLayoutPanel3.Controls.Add(label1, 0, 1);
            tableLayoutPanel3.Controls.Add(label2, 0, 2);
            tableLayoutPanel3.Controls.Add(label4, 0, 4);
            tableLayoutPanel3.Controls.Add(label5, 0, 5);
            tableLayoutPanel3.Controls.Add(label6, 0, 6);
            tableLayoutPanel3.Controls.Add(label7, 0, 7);
            tableLayoutPanel3.Controls.Add(textBox8, 1, 8);
            tableLayoutPanel3.Controls.Add(textBox7, 1, 7);
            tableLayoutPanel3.Controls.Add(textBox6, 1, 6);
            tableLayoutPanel3.Controls.Add(textBox5, 1, 5);
            tableLayoutPanel3.Controls.Add(textBox4, 1, 4);
            tableLayoutPanel3.Controls.Add(textBox3, 1, 3);
            tableLayoutPanel3.Controls.Add(label8, 0, 8);
            tableLayoutPanel3.Controls.Add(textBox2, 1, 2);
            tableLayoutPanel3.Controls.Add(textBox1, 1, 1);
            tableLayoutPanel3.Controls.Add(textBox9, 1, 9);
            tableLayoutPanel3.Controls.Add(textBox10, 1, 10);
            tableLayoutPanel3.Controls.Add(label10, 0, 10);
            tableLayoutPanel3.Controls.Add(label9, 0, 9);
            tableLayoutPanel3.Controls.Add(textBoxKod, 1, 0);
            tableLayoutPanel3.Controls.Add(labelSave, 2, 11);
            tableLayoutPanel3.Controls.Add(simpleButtonAddOtm, 0, 12);
            tableLayoutPanel3.Controls.Add(simpleButtonDel, 1, 12);
            tableLayoutPanel3.Controls.Add(simpleButtonAddSave, 2, 12);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel3.MaximumSize = new System.Drawing.Size(630, 981);
            tableLayoutPanel3.MinimumSize = new System.Drawing.Size(350, 462);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 14;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new System.Drawing.Size(630, 676);
            tableLayoutPanel3.TabIndex = 31;
            // 
            // labelKod
            // 
            labelKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            labelKod.AutoSize = true;
            labelKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelKod.Location = new System.Drawing.Point(4, 7);
            labelKod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelKod.Name = "labelKod";
            labelKod.Size = new System.Drawing.Size(34, 17);
            labelKod.TabIndex = 14;
            labelKod.Text = "Код";
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label3.Location = new System.Drawing.Point(4, 100);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 17);
            label3.TabIndex = 16;
            label3.Text = "Поле3";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label1.Location = new System.Drawing.Point(4, 38);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 17);
            label1.TabIndex = 10;
            label1.Text = "Поле1";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label2.Location = new System.Drawing.Point(4, 69);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(52, 17);
            label2.TabIndex = 12;
            label2.Text = "Поле2";
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label4.Location = new System.Drawing.Point(4, 131);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 17);
            label4.TabIndex = 18;
            label4.Text = "Поле4";
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label5.Location = new System.Drawing.Point(4, 162);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(52, 17);
            label5.TabIndex = 20;
            label5.Text = "Поле5";
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label6.Location = new System.Drawing.Point(4, 193);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(52, 17);
            label6.TabIndex = 22;
            label6.Text = "Поле6";
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label7.Location = new System.Drawing.Point(4, 224);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(52, 17);
            label7.TabIndex = 24;
            label7.Text = "Поле7";
            // 
            // textBox8
            // 
            tableLayoutPanel3.SetColumnSpan(textBox8, 2);
            textBox8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox8.Location = new System.Drawing.Point(213, 251);
            textBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox8.Name = "textBox8";
            textBox8.Size = new System.Drawing.Size(373, 25);
            textBox8.TabIndex = 25;
            // 
            // textBox7
            // 
            tableLayoutPanel3.SetColumnSpan(textBox7, 2);
            textBox7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox7.Location = new System.Drawing.Point(213, 220);
            textBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox7.Name = "textBox7";
            textBox7.Size = new System.Drawing.Size(373, 25);
            textBox7.TabIndex = 23;
            // 
            // textBox6
            // 
            tableLayoutPanel3.SetColumnSpan(textBox6, 2);
            textBox6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox6.Location = new System.Drawing.Point(213, 189);
            textBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox6.Name = "textBox6";
            textBox6.Size = new System.Drawing.Size(373, 25);
            textBox6.TabIndex = 21;
            // 
            // textBox5
            // 
            tableLayoutPanel3.SetColumnSpan(textBox5, 2);
            textBox5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox5.Location = new System.Drawing.Point(213, 158);
            textBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(373, 25);
            textBox5.TabIndex = 19;
            // 
            // textBox4
            // 
            tableLayoutPanel3.SetColumnSpan(textBox4, 2);
            textBox4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox4.Location = new System.Drawing.Point(213, 127);
            textBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox4.Name = "textBox4";
            textBox4.Size = new System.Drawing.Size(373, 25);
            textBox4.TabIndex = 17;
            // 
            // textBox3
            // 
            tableLayoutPanel3.SetColumnSpan(textBox3, 2);
            textBox3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox3.Location = new System.Drawing.Point(213, 96);
            textBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(373, 25);
            textBox3.TabIndex = 15;
            // 
            // label8
            // 
            label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label8.Location = new System.Drawing.Point(4, 255);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(52, 17);
            label8.TabIndex = 26;
            label8.Text = "Поле8";
            // 
            // textBox2
            // 
            tableLayoutPanel3.SetColumnSpan(textBox2, 2);
            textBox2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox2.Location = new System.Drawing.Point(213, 65);
            textBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(373, 25);
            textBox2.TabIndex = 11;
            // 
            // textBox1
            // 
            // textBox1.BackColor = System.Drawing.Color.White;
            tableLayoutPanel3.SetColumnSpan(textBox1, 2);
            textBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox1.Location = new System.Drawing.Point(213, 34);
            textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(373, 25);
            textBox1.TabIndex = 9;
            // 
            // textBox9
            // 
            tableLayoutPanel3.SetColumnSpan(textBox9, 2);
            textBox9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox9.Location = new System.Drawing.Point(213, 282);
            textBox9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox9.Name = "textBox9";
            textBox9.Size = new System.Drawing.Size(373, 25);
            textBox9.TabIndex = 27;
            // 
            // textBox10
            // 
            tableLayoutPanel3.SetColumnSpan(textBox10, 2);
            textBox10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox10.Location = new System.Drawing.Point(213, 313);
            textBox10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox10.Name = "textBox10";
            textBox10.Size = new System.Drawing.Size(373, 25);
            textBox10.TabIndex = 29;
            // 
            // label10
            // 
            label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label10.Location = new System.Drawing.Point(4, 317);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(60, 17);
            label10.TabIndex = 30;
            label10.Text = "Поле10";
            // 
            // label9
            // 
            label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label9.Location = new System.Drawing.Point(4, 286);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(52, 17);
            label9.TabIndex = 28;
            label9.Text = "Поле9";
            // 
            // textBoxKod
            // 
            textBoxKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            textBoxKod.BackColor = System.Drawing.SystemColors.Control;
            textBoxKod.Cursor = System.Windows.Forms.Cursors.No;
            textBoxKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
            textBoxKod.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            textBoxKod.Location = new System.Drawing.Point(213, 3);
            textBoxKod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxKod.Name = "textBoxKod";
            textBoxKod.ReadOnly = true;
            textBoxKod.Size = new System.Drawing.Size(73, 25);
            textBoxKod.TabIndex = 13;
            // 
            // labelSave
            // 
            labelSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            labelSave.AutoSize = true;
            labelSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelSave.Location = new System.Drawing.Point(472, 341);
            labelSave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelSave.Name = "labelSave";
            labelSave.Size = new System.Drawing.Size(105, 19);
            labelSave.TabIndex = 31;
            labelSave.Text = "Сохранено!";
            labelSave.Visible = false;
            // 
            // simpleButtonAddOtm
            // 
            simpleButtonAddOtm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // simpleButtonAddOtm.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            simpleButtonAddOtm.FlatAppearance.BorderSize = 0;
            simpleButtonAddOtm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            simpleButtonAddOtm.Font = new System.Drawing.Font("Arial", 10F);
            simpleButtonAddOtm.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            simpleButtonAddOtm.Location = new System.Drawing.Point(4, 363);
            simpleButtonAddOtm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonAddOtm.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonAddOtm.Name = "simpleButtonAddOtm";
            simpleButtonAddOtm.Size = new System.Drawing.Size(201, 51);
            simpleButtonAddOtm.TabIndex = 32;
            simpleButtonAddOtm.Text = "Отмена";
            simpleButtonAddOtm.UseVisualStyleBackColor = false;
            simpleButtonAddOtm.Click += simpleButtonAddOtm_Click;
            // 
            // simpleButtonDel
            // 
            simpleButtonDel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // simpleButtonDel.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            simpleButtonDel.FlatAppearance.BorderSize = 0;
            simpleButtonDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            simpleButtonDel.Font = new System.Drawing.Font("Arial", 10F);
            simpleButtonDel.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            simpleButtonDel.Location = new System.Drawing.Point(213, 363);
            simpleButtonDel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonDel.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonDel.Name = "simpleButtonDel";
            simpleButtonDel.Size = new System.Drawing.Size(202, 51);
            simpleButtonDel.TabIndex = 32;
            simpleButtonDel.Text = "Удалить";
            simpleButtonDel.UseVisualStyleBackColor = false;
            simpleButtonDel.Click += simpleButtonDel_Click;
            // 
            // simpleButtonAddSave
            // 
            simpleButtonAddSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // simpleButtonAddSave.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            simpleButtonAddSave.FlatAppearance.BorderSize = 0;
            simpleButtonAddSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            simpleButtonAddSave.Font = new System.Drawing.Font("Arial", 10F);
            simpleButtonAddSave.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            simpleButtonAddSave.Location = new System.Drawing.Point(423, 363);
            simpleButtonAddSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonAddSave.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonAddSave.Name = "simpleButtonAddSave";
            simpleButtonAddSave.Size = new System.Drawing.Size(203, 51);
            simpleButtonAddSave.TabIndex = 32;
            simpleButtonAddSave.Text = "Сохранить";
            simpleButtonAddSave.UseVisualStyleBackColor = false;
            simpleButtonAddSave.Click += simpleButtonAddSave_Click;
            // 
            // gridControlSprav
            // 
            gridControlSprav.DataSource = spravList;
            gridControlSprav.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControlSprav.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridControlSprav.Font = new System.Drawing.Font("Arial", 10F);
            gridControlSprav.Location = new System.Drawing.Point(4, 3);
            gridControlSprav.MainView = gridView1;
            gridControlSprav.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridControlSprav.MaximumSize = new System.Drawing.Size(1633, 1038);
            gridControlSprav.Name = "gridControlSprav";
            gridControlSprav.Size = new System.Drawing.Size(551, 701);
            gridControlSprav.TabIndex = 0;
            gridControlSprav.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            gridControlSprav.Load += gridControlSprav_Load;
            gridControlSprav.Click += gridControlSprav_Click;
            gridControlSprav.KeyUp += gridControlSprav_KeyUp;
            // 
            // gridView1
            // 
            gridView1.AppearancePrint.FilterPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            gridView1.AppearancePrint.FilterPanel.Options.UseBackColor = true;
            gridView1.AppearancePrint.Lines.BackColor = System.Drawing.SystemColors.ActiveCaption;
            gridView1.AppearancePrint.Lines.Options.UseBackColor = true;
            gridView1.DetailHeight = 404;
            gridView1.GridControl = gridControlSprav;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridView1.OptionsEditForm.PopupEditFormWidth = 933;
            gridView1.CellValueChanged += gridView1_CellValueChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tableLayoutPanel1.Controls.Add(simpleButtonRed, 1, 0);
            tableLayoutPanel1.Controls.Add(simpleButtonAdd, 2, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(4, 710);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(551, 50);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // simpleButtonRed
            // 
            // simpleButtonRed.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            simpleButtonRed.Dock = System.Windows.Forms.DockStyle.Fill;
            simpleButtonRed.FlatAppearance.BorderSize = 0;
            simpleButtonRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            simpleButtonRed.Font = new System.Drawing.Font("Arial", 10F);
            simpleButtonRed.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            simpleButtonRed.Location = new System.Drawing.Point(389, 3);
            simpleButtonRed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonRed.Name = "simpleButtonRed";
            simpleButtonRed.Size = new System.Drawing.Size(74, 34);
            simpleButtonRed.TabIndex = 12;
            simpleButtonRed.Text = "Редактировать";
            simpleButtonRed.UseVisualStyleBackColor = false;
            simpleButtonRed.Click += simpleButtonRed_Click;
            // 
            // simpleButtonAdd
            // 
            // simpleButtonAdd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            simpleButtonAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            simpleButtonAdd.FlatAppearance.BorderSize = 0;
            simpleButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            simpleButtonAdd.Font = new System.Drawing.Font("Arial", 10F);
            simpleButtonAdd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            simpleButtonAdd.Location = new System.Drawing.Point(471, 3);
            simpleButtonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonAdd.Name = "simpleButtonAdd";
            simpleButtonAdd.Size = new System.Drawing.Size(76, 34);
            simpleButtonAdd.TabIndex = 13;
            simpleButtonAdd.Text = "Добавить";
            simpleButtonAdd.UseVisualStyleBackColor = false;
            simpleButtonAdd.Click += simpleButtonAdd_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel2.Controls.Add(gridControlSprav, 0, 0);
            tableLayoutPanel2.Controls.Add(AddTab, 1, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 1);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.71592F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.284079F));
            tableLayoutPanel2.Size = new System.Drawing.Size(1206, 763);
            tableLayoutPanel2.TabIndex = 11;
            // 
            // SpravForAll
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1206, 763);
            Controls.Add(tableLayoutPanel2);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(16, 39);
            Name = "SpravForAll";
            Text = "Cправочник";
            FormClosing += SpravForAll_FormClosing;
            Load += SpravForAll_Load;
            ((System.ComponentModel.ISupportInitialize)spravList).EndInit();
            ((System.ComponentModel.ISupportInitialize)nameColumnList).EndInit();
            ((System.ComponentModel.ISupportInitialize)AddTab).EndInit();
            AddTab.ResumeLayout(false);
            xtraTabPageAdd.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlSprav).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.BindingSource spravList;
        private System.Windows.Forms.BindingSource nameColumnList;
        private DevExpress.XtraTab.XtraTabControl AddTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomGridControlColumn gridControlSprav;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label labelKod;
        private System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelSave;
        private CustomButton simpleButtonRed;
        private CustomButton simpleButtonAdd;
        private CustomButton simpleButtonAddOtm;
        private CustomButton simpleButtonDel;
        private CustomButton simpleButtonAddSave;
    }
}