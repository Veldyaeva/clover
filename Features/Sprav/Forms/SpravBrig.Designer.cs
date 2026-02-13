using System.Collections.Generic;
using SewingProduction.Core.Class;
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpravBrig));
            nameColumnList = new System.Windows.Forms.BindingSource(components);
            textBoxKod = new System.Windows.Forms.TextBox();
            comboBoxZeh = new System.Windows.Forms.ComboBox();
            gridControlSprav = new CustomGridControl();
            spravList = new System.Windows.Forms.BindingSource(components);
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            id_brig = new DevExpress.XtraGrid.Columns.GridColumn();
            n_brig = new DevExpress.XtraGrid.Columns.GridColumn();
            brig = new DevExpress.XtraGrid.Columns.GridColumn();
            nameZeh = new DevExpress.XtraGrid.Columns.GridColumn();
            textBoxNBrig = new System.Windows.Forms.TextBox();
            textBoxBrig = new System.Windows.Forms.TextBox();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            labelKod = new System.Windows.Forms.Label();
            labelB = new System.Windows.Forms.Label();
            linkLabelZeh = new System.Windows.Forms.LinkLabel();
            labelN = new System.Windows.Forms.Label();
            labelSave = new System.Windows.Forms.Label();
            simpleButtonAddOtm = new CustomButton();
            simpleButtonDel = new CustomButton();
            simpleButtonAddSave = new CustomButton();
            xtraTabPageAdd = new DevExpress.XtraTab.XtraTabPage();
            AddTab = new DevExpress.XtraTab.XtraTabControl();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            simpleButtonAdd = new CustomButton();
            simpleButtonRed = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)nameColumnList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSprav).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spravList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            xtraTabPageAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AddTab).BeginInit();
            AddTab.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxKod
            // 
            textBoxKod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            textBoxKod.BackColor = System.Drawing.SystemColors.Control;
            textBoxKod.Cursor = System.Windows.Forms.Cursors.No;
            textBoxKod.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
            textBoxKod.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            textBoxKod.Location = new System.Drawing.Point(216, 3);
            textBoxKod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxKod.Name = "textBoxKod";
            textBoxKod.ReadOnly = true;
            textBoxKod.Size = new System.Drawing.Size(73, 25);
            textBoxKod.TabIndex = 13;
            // 
            // comboBoxZeh
            // 
            comboBoxZeh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            tableLayoutPanel3.SetColumnSpan(comboBoxZeh, 2);
            comboBoxZeh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxZeh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            comboBoxZeh.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            comboBoxZeh.ForeColor = System.Drawing.SystemColors.WindowText;
            comboBoxZeh.FormattingEnabled = true;
            comboBoxZeh.Location = new System.Drawing.Point(216, 96);
            comboBoxZeh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxZeh.Name = "comboBoxZeh";
            comboBoxZeh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            comboBoxZeh.Size = new System.Drawing.Size(397, 25);
            comboBoxZeh.TabIndex = 17;
            comboBoxZeh.Enter += comboBoxZeh_Enter;
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
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { id_brig, n_brig, brig, nameZeh });
            gridView1.DetailHeight = 404;
            gridView1.GridControl = gridControlSprav;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridView1.OptionsEditForm.PopupEditFormWidth = 933;
            gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // id_brig
            // 
            id_brig.Caption = "Код";
            id_brig.FieldName = "id_brig";
            id_brig.Name = "id_brig";
            id_brig.Visible = true;
            id_brig.VisibleIndex = 0;
            id_brig.Width = 87;
            // 
            // n_brig
            // 
            n_brig.Caption = "Номер";
            n_brig.FieldName = "n_brig";
            n_brig.Name = "n_brig";
            n_brig.Visible = true;
            n_brig.VisibleIndex = 1;
            n_brig.Width = 87;
            // 
            // brig
            // 
            brig.Caption = "Бригада";
            brig.FieldName = "brig";
            brig.Name = "brig";
            brig.Visible = true;
            brig.VisibleIndex = 2;
            brig.Width = 87;
            // 
            // nameZeh
            // 
            nameZeh.Caption = "Цех";
            nameZeh.FieldName = "nameZeh";
            nameZeh.Name = "nameZeh";
            nameZeh.Visible = true;
            nameZeh.VisibleIndex = 3;
            // 
            // textBoxNBrig
            // 
            textBoxNBrig.Anchor = System.Windows.Forms.AnchorStyles.Left;
            tableLayoutPanel3.SetColumnSpan(textBoxNBrig, 2);
            textBoxNBrig.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBoxNBrig.Location = new System.Drawing.Point(216, 65);
            textBoxNBrig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxNBrig.Name = "textBoxNBrig";
            textBoxNBrig.Size = new System.Drawing.Size(397, 25);
            textBoxNBrig.TabIndex = 15;
            // 
            // textBoxBrig
            // 
            textBoxBrig.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // textBoxBrig.BackColor = System.Drawing.Color.White;
            tableLayoutPanel3.SetColumnSpan(textBoxBrig, 2);
            textBoxBrig.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBoxBrig.Location = new System.Drawing.Point(216, 34);
            textBoxBrig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxBrig.Name = "textBoxBrig";
            textBoxBrig.Size = new System.Drawing.Size(397, 25);
            textBoxBrig.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333244F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333359F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333359F));
            tableLayoutPanel3.Controls.Add(labelKod, 0, 0);
            tableLayoutPanel3.Controls.Add(labelB, 0, 1);
            tableLayoutPanel3.Controls.Add(textBoxBrig, 1, 1);
            tableLayoutPanel3.Controls.Add(textBoxKod, 1, 0);
            tableLayoutPanel3.Controls.Add(linkLabelZeh, 0, 3);
            tableLayoutPanel3.Controls.Add(comboBoxZeh, 1, 3);
            tableLayoutPanel3.Controls.Add(labelN, 0, 2);
            tableLayoutPanel3.Controls.Add(textBoxNBrig, 1, 2);
            tableLayoutPanel3.Controls.Add(labelSave, 2, 4);
            tableLayoutPanel3.Controls.Add(simpleButtonAddOtm, 0, 5);
            tableLayoutPanel3.Controls.Add(simpleButtonDel, 1, 5);
            tableLayoutPanel3.Controls.Add(simpleButtonAddSave, 2, 5);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel3.MaximumSize = new System.Drawing.Size(642, 981);
            tableLayoutPanel3.MinimumSize = new System.Drawing.Size(525, 635);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 7;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new System.Drawing.Size(637, 676);
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
            // labelB
            // 
            labelB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            labelB.AutoSize = true;
            labelB.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelB.Location = new System.Drawing.Point(4, 38);
            labelB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelB.Name = "labelB";
            labelB.Size = new System.Drawing.Size(74, 17);
            labelB.TabIndex = 10;
            labelB.Text = "Название";
            // 
            // linkLabelZeh
            // 
            linkLabelZeh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            linkLabelZeh.AutoSize = true;
            linkLabelZeh.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            // linkLabelZeh.LinkColor = System.Drawing.Color.Black;
            linkLabelZeh.Location = new System.Drawing.Point(4, 100);
            linkLabelZeh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabelZeh.Name = "linkLabelZeh";
            linkLabelZeh.Size = new System.Drawing.Size(36, 17);
            linkLabelZeh.TabIndex = 18;
            linkLabelZeh.TabStop = true;
            linkLabelZeh.Text = "Цех";
            linkLabelZeh.LinkClicked += linkLabelVid_LinkClicked;
            // 
            // labelN
            // 
            labelN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            labelN.AutoSize = true;
            labelN.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelN.Location = new System.Drawing.Point(4, 69);
            labelN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelN.Name = "labelN";
            labelN.Size = new System.Drawing.Size(54, 17);
            labelN.TabIndex = 16;
            labelN.Text = "Номер";
            // 
            // labelSave
            // 
            labelSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            labelSave.AutoSize = true;
            labelSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelSave.Location = new System.Drawing.Point(478, 124);
            labelSave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelSave.Name = "labelSave";
            labelSave.Size = new System.Drawing.Size(105, 19);
            labelSave.TabIndex = 32;
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
            simpleButtonAddOtm.Location = new System.Drawing.Point(5, 146);
            simpleButtonAddOtm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonAddOtm.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonAddOtm.Name = "simpleButtonAddOtm";
            simpleButtonAddOtm.Size = new System.Drawing.Size(202, 47);
            simpleButtonAddOtm.TabIndex = 34;
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
            simpleButtonDel.Location = new System.Drawing.Point(216, 146);
            simpleButtonDel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonDel.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonDel.Name = "simpleButtonDel";
            simpleButtonDel.Size = new System.Drawing.Size(203, 47);
            simpleButtonDel.TabIndex = 34;
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
            simpleButtonAddSave.Location = new System.Drawing.Point(429, 146);
            simpleButtonAddSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButtonAddSave.MinimumSize = new System.Drawing.Size(0, 47);
            simpleButtonAddSave.Name = "simpleButtonAddSave";
            simpleButtonAddSave.Size = new System.Drawing.Size(203, 47);
            simpleButtonAddSave.TabIndex = 34;
            simpleButtonAddSave.Text = "Сохранить";
            simpleButtonAddSave.UseVisualStyleBackColor = false;
            simpleButtonAddSave.Click += simpleButtonAddSave_Click;
            // 
            // xtraTabPageAdd
            // 
            xtraTabPageAdd.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
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
            tableLayoutPanel2.TabIndex = 13;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tableLayoutPanel1.Controls.Add(simpleButtonAdd, 2, 0);
            tableLayoutPanel1.Controls.Add(simpleButtonRed, 1, 0);
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
            simpleButtonAdd.Size = new System.Drawing.Size(76, 31);
            simpleButtonAdd.TabIndex = 10;
            simpleButtonAdd.Text = "Добавить";
            simpleButtonAdd.UseVisualStyleBackColor = false;
            simpleButtonAdd.Click += simpleButtonAdd_Click;
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
            simpleButtonRed.Size = new System.Drawing.Size(74, 31);
            simpleButtonRed.TabIndex = 11;
            simpleButtonRed.Text = "Редактировать";
            simpleButtonRed.UseVisualStyleBackColor = false;
            simpleButtonRed.Click += simpleButtonRed_Click;
            // 
            // SpravBrig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1206, 763);
            Controls.Add(tableLayoutPanel2);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SpravBrig";
            Text = "SpravBrig";
            FormClosing += SpravForAll_FormClosing;
            Load += SpravBrig_Load;
            ((System.ComponentModel.ISupportInitialize)nameColumnList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSprav).EndInit();
            ((System.ComponentModel.ISupportInitialize)spravList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            xtraTabPageAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AddTab).EndInit();
            AddTab.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
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
        private CustomGridControl gridControlSprav;
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
        private DevExpress.XtraGrid.Columns.GridColumn id_brig;
        private DevExpress.XtraGrid.Columns.GridColumn n_brig;
        private DevExpress.XtraGrid.Columns.GridColumn brig;
        private DevExpress.XtraGrid.Columns.GridColumn nameZeh;
    }
}