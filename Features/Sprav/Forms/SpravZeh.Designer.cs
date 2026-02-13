using SewingProduction.Core.Class;
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpravZeh));
            labelKod = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            gridViewZeh = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridControlSprav = new CustomGridControl();
            spravList = new System.Windows.Forms.BindingSource(components);
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            customButton1 = new CustomButton();
            customButton2 = new CustomButton();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            AddTab = new DevExpress.XtraTab.XtraTabControl();
            xtraTabPageAdd = new DevExpress.XtraTab.XtraTabPage();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            textBoxAdres = new System.Windows.Forms.TextBox();
            textBoxName = new System.Windows.Forms.TextBox();
            textBoxKod = new System.Windows.Forms.TextBox();
            comboBoxVidProizv = new System.Windows.Forms.ComboBox();
            linkLabelVid = new System.Windows.Forms.LinkLabel();
            labelSave = new System.Windows.Forms.Label();
            simpleButtonAddSave = new CustomButton();
            simpleButtonDel = new CustomButton();
            simpleButtonAddOtm = new CustomButton();
            nameColumnList = new System.Windows.Forms.BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)gridViewZeh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSprav).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spravList).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AddTab).BeginInit();
            AddTab.SuspendLayout();
            xtraTabPageAdd.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nameColumnList).BeginInit();
            SuspendLayout();
            // 
            // labelKod
            // 
            labelKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            labelKod.AutoSize = true;
            labelKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelKod.Location = new System.Drawing.Point(4, 11);
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
            label3.Location = new System.Drawing.Point(4, 131);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(50, 17);
            label3.TabIndex = 16;
            label3.Text = "Адрес";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label1.Location = new System.Drawing.Point(4, 51);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(74, 17);
            label1.TabIndex = 10;
            label1.Text = "Название";
            // 
            // gridViewZeh
            // 
            gridViewZeh.AppearancePrint.FilterPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            gridViewZeh.AppearancePrint.FilterPanel.Options.UseBackColor = true;
            gridViewZeh.AppearancePrint.Lines.BackColor = System.Drawing.SystemColors.ActiveCaption;
            gridViewZeh.AppearancePrint.Lines.Options.UseBackColor = true;
            gridViewZeh.DetailHeight = 404;
            gridViewZeh.GridControl = gridControlSprav;
            gridViewZeh.Name = "gridViewZeh";
            gridViewZeh.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridViewZeh.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridViewZeh.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewZeh.OptionsView.ShowGroupPanel = false;
            // 
            // gridControlSprav
            // 
            gridControlSprav.DataSource = spravList;
            gridControlSprav.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControlSprav.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridControlSprav.Font = new System.Drawing.Font("Arial", 10F);
            gridControlSprav.Location = new System.Drawing.Point(4, 3);
            gridControlSprav.MainView = gridViewZeh;
            gridControlSprav.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridControlSprav.MaximumSize = new System.Drawing.Size(1633, 1038);
            gridControlSprav.Name = "gridControlSprav";
            gridControlSprav.Size = new System.Drawing.Size(551, 701);
            gridControlSprav.TabIndex = 0;
            gridControlSprav.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewZeh });
            gridControlSprav.Load += gridControlSprav_Load;
            gridControlSprav.Click += gridControlSprav_Click;
            gridControlSprav.KeyUp += gridControlSprav_KeyUp;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tableLayoutPanel1.Controls.Add(customButton1, 2, 0);
            tableLayoutPanel1.Controls.Add(customButton2, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(4, 710);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new System.Drawing.Size(551, 50);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // customButton1
            // 
            // customButton1.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            customButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            customButton1.FlatAppearance.BorderSize = 0;
            customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            customButton1.Font = new System.Drawing.Font("Arial", 10F);
            customButton1.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            customButton1.Location = new System.Drawing.Point(471, 3);
            customButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButton1.Name = "customButton1";
            customButton1.Size = new System.Drawing.Size(76, 31);
            customButton1.TabIndex = 11;
            customButton1.Text = "Добавить";
            customButton1.UseVisualStyleBackColor = false;
            customButton1.Click += simpleButtonAdd_Click;
            // 
            // customButton2
            // 
            // customButton2.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            customButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            customButton2.FlatAppearance.BorderSize = 0;
            customButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            customButton2.Font = new System.Drawing.Font("Arial", 10F);
            customButton2.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            customButton2.Location = new System.Drawing.Point(389, 3);
            customButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customButton2.Name = "customButton2";
            customButton2.Size = new System.Drawing.Size(74, 31);
            customButton2.TabIndex = 12;
            customButton2.Text = "Редактировать";
            customButton2.UseVisualStyleBackColor = false;
            customButton2.Click += simpleButtonRed_Click;
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
            tableLayoutPanel2.TabIndex = 12;
            // 
            // AddTab
            // 
            AddTab.Appearance.BackColor = System.Drawing.Color.Transparent;
            AddTab.Appearance.Options.UseBackColor = true;
            AddTab.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            AddTab.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            AddTab.Dock = System.Windows.Forms.DockStyle.Fill;
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
            xtraTabPageAdd.Appearance.PageClient.BackColor = System.Drawing.Color.IndianRed;
            xtraTabPageAdd.Appearance.PageClient.Options.UseBackColor = true;
            xtraTabPageAdd.Controls.Add(tableLayoutPanel3);
            xtraTabPageAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            xtraTabPageAdd.MaximumSize = new System.Drawing.Size(642, 981);
            xtraTabPageAdd.MinimumSize = new System.Drawing.Size(566, 675);
            xtraTabPageAdd.Name = "xtraTabPageAdd";
            xtraTabPageAdd.PageVisible = false;
            xtraTabPageAdd.Size = new System.Drawing.Size(637, 676);
            xtraTabPageAdd.Text = "Добавить/Редактировать";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel3.Controls.Add(labelKod, 0, 0);
            tableLayoutPanel3.Controls.Add(label3, 0, 3);
            tableLayoutPanel3.Controls.Add(label1, 0, 1);
            tableLayoutPanel3.Controls.Add(textBoxAdres, 1, 3);
            tableLayoutPanel3.Controls.Add(textBoxName, 1, 1);
            tableLayoutPanel3.Controls.Add(textBoxKod, 1, 0);
            tableLayoutPanel3.Controls.Add(comboBoxVidProizv, 1, 2);
            tableLayoutPanel3.Controls.Add(linkLabelVid, 0, 2);
            tableLayoutPanel3.Controls.Add(labelSave, 2, 4);
            tableLayoutPanel3.Controls.Add(simpleButtonAddSave, 2, 5);
            tableLayoutPanel3.Controls.Add(simpleButtonDel, 1, 5);
            tableLayoutPanel3.Controls.Add(simpleButtonAddOtm, 0, 5);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel3.MaximumSize = new System.Drawing.Size(642, 981);
            tableLayoutPanel3.MinimumSize = new System.Drawing.Size(569, 652);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 6;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            tableLayoutPanel3.Size = new System.Drawing.Size(637, 676);
            tableLayoutPanel3.TabIndex = 31;
            // 
            // textBoxAdres
            // 
            textBoxAdres.Anchor = System.Windows.Forms.AnchorStyles.Left;
            tableLayoutPanel3.SetColumnSpan(textBoxAdres, 2);
            textBoxAdres.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBoxAdres.Location = new System.Drawing.Point(216, 127);
            textBoxAdres.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxAdres.Name = "textBoxAdres";
            textBoxAdres.Size = new System.Drawing.Size(373, 25);
            textBoxAdres.TabIndex = 15;
            // 
            // textBoxName
            // 
            textBoxName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // textBoxName.BackColor = System.Drawing.Color.White;
            tableLayoutPanel3.SetColumnSpan(textBoxName, 2);
            textBoxName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBoxName.Location = new System.Drawing.Point(216, 47);
            textBoxName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(373, 25);
            textBoxName.TabIndex = 9;
            // 
            // textBoxKod
            // 
            textBoxKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            textBoxKod.BackColor = System.Drawing.SystemColors.Control;
            textBoxKod.Cursor = System.Windows.Forms.Cursors.No;
            textBoxKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
            textBoxKod.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            textBoxKod.Location = new System.Drawing.Point(216, 7);
            textBoxKod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxKod.Name = "textBoxKod";
            textBoxKod.ReadOnly = true;
            textBoxKod.Size = new System.Drawing.Size(73, 25);
            textBoxKod.TabIndex = 13;
            // 
            // comboBoxVidProizv
            // 
            comboBoxVidProizv.Anchor = System.Windows.Forms.AnchorStyles.Left;
            tableLayoutPanel3.SetColumnSpan(comboBoxVidProizv, 2);
            comboBoxVidProizv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxVidProizv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            comboBoxVidProizv.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            comboBoxVidProizv.ForeColor = System.Drawing.SystemColors.WindowText;
            comboBoxVidProizv.FormattingEnabled = true;
            comboBoxVidProizv.Location = new System.Drawing.Point(216, 87);
            comboBoxVidProizv.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxVidProizv.Name = "comboBoxVidProizv";
            comboBoxVidProizv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            comboBoxVidProizv.Size = new System.Drawing.Size(373, 25);
            comboBoxVidProizv.TabIndex = 17;
            comboBoxVidProizv.Enter += comboBoxVidProizv_Enter;
            // 
            // linkLabelVid
            // 
            linkLabelVid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            linkLabelVid.AutoSize = true;
            linkLabelVid.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            // linkLabelVid.LinkColor = System.Drawing.Color.Black;
            linkLabelVid.Location = new System.Drawing.Point(4, 91);
            linkLabelVid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabelVid.Name = "linkLabelVid";
            linkLabelVid.Size = new System.Drawing.Size(134, 17);
            linkLabelVid.TabIndex = 18;
            linkLabelVid.TabStop = true;
            linkLabelVid.Text = "Вид производства";
            linkLabelVid.LinkClicked += linkLabelVid_LinkClicked;
            // 
            // labelSave
            // 
            labelSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            labelSave.AutoSize = true;
            labelSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelSave.Location = new System.Drawing.Point(478, 164);
            labelSave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelSave.Name = "labelSave";
            labelSave.Size = new System.Drawing.Size(105, 19);
            labelSave.TabIndex = 32;
            labelSave.Text = "Сохранено!";
            labelSave.Visible = false;
            // 
            // simpleButtonAddSave
            // 
            simpleButtonAddSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // simpleButtonAddSave.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            simpleButtonAddSave.FlatAppearance.BorderSize = 0;
            simpleButtonAddSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            simpleButtonAddSave.Font = new System.Drawing.Font("Arial", 10F);
            simpleButtonAddSave.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            simpleButtonAddSave.Location = new System.Drawing.Point(429, 186);
            simpleButtonAddSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonAddSave.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonAddSave.Name = "simpleButtonAddSave";
            simpleButtonAddSave.Size = new System.Drawing.Size(203, 51);
            simpleButtonAddSave.TabIndex = 33;
            simpleButtonAddSave.Text = "Сохранить";
            simpleButtonAddSave.UseVisualStyleBackColor = false;
            simpleButtonAddSave.Click += simpleButtonAddSave_Click;
            // 
            // simpleButtonDel
            // 
            simpleButtonDel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // simpleButtonDel.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            simpleButtonDel.FlatAppearance.BorderSize = 0;
            simpleButtonDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            simpleButtonDel.Font = new System.Drawing.Font("Arial", 10F);
            simpleButtonDel.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            simpleButtonDel.Location = new System.Drawing.Point(216, 186);
            simpleButtonDel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonDel.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonDel.Name = "simpleButtonDel";
            simpleButtonDel.Size = new System.Drawing.Size(203, 51);
            simpleButtonDel.TabIndex = 33;
            simpleButtonDel.Text = "Удалить";
            simpleButtonDel.UseVisualStyleBackColor = false;
            simpleButtonDel.Click += simpleButtonDel_Click;
            // 
            // simpleButtonAddOtm
            // 
            simpleButtonAddOtm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // simpleButtonAddOtm.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            simpleButtonAddOtm.FlatAppearance.BorderSize = 0;
            simpleButtonAddOtm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            simpleButtonAddOtm.Font = new System.Drawing.Font("Arial", 10F);
            simpleButtonAddOtm.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            simpleButtonAddOtm.Location = new System.Drawing.Point(4, 186);
            simpleButtonAddOtm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonAddOtm.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonAddOtm.Name = "simpleButtonAddOtm";
            simpleButtonAddOtm.Size = new System.Drawing.Size(203, 51);
            simpleButtonAddOtm.TabIndex = 33;
            simpleButtonAddOtm.Text = "Отмена";
            simpleButtonAddOtm.UseVisualStyleBackColor = false;
            simpleButtonAddOtm.Click += simpleButtonAddOtm_Click;
            // 
            // SpravZeh
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1206, 763);
            Controls.Add(tableLayoutPanel2);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SpravZeh";
            Text = "SpravZeh";
            FormClosing += SpravForAll_FormClosing;
            Load += SpravZeh_Load;
            ((System.ComponentModel.ISupportInitialize)gridViewZeh).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSprav).EndInit();
            ((System.ComponentModel.ISupportInitialize)spravList).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AddTab).EndInit();
            AddTab.ResumeLayout(false);
            xtraTabPageAdd.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nameColumnList).EndInit();
            ResumeLayout(false);
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
        private CustomGridControl gridControlSprav;
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