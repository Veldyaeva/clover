using System.Drawing;

namespace SewingProduction.form
{
    partial class Articul
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
            this.bsArt = new System.Windows.Forms.BindingSource(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.pictureBoxArticul = new System.Windows.Forms.PictureBox();
            this.cbTM = new SewingProduction.CustomComboBox();
            this.bsTM = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new SewingProduction.CustomLabel();
            this.txtMod = new SewingProduction.CustomTextBox();
            this.label2 = new SewingProduction.CustomLabel();
            this.txbArticul = new SewingProduction.CustomTextBox();
            this.txbPo = new SewingProduction.CustomTextBox();
            this.label1 = new SewingProduction.CustomLabel();
            this.txbKod = new SewingProduction.CustomTextBox();
            this.customButtonAdd = new SewingProduction.CustomButton();
            this.bsArticul = new System.Windows.Forms.BindingSource(this.components);
            this.gridArt = new DevExpress.XtraGrid.GridControl();
            this.gridArticul = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArticul)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsArticul)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArticul)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl1.Controls.Add(this.pictureBoxArticul);
            this.groupControl1.Controls.Add(this.cbTM);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.txtMod);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.txbArticul);
            this.groupControl1.Controls.Add(this.txbPo);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.txbKod);
            this.groupControl1.Location = new System.Drawing.Point(471, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(714, 257);
            this.groupControl1.TabIndex = 2;
            // 
            // pictureBoxArticul
            // 
            this.pictureBoxArticul.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxArticul.Name = "pictureBoxArticul";
            this.pictureBoxArticul.Size = new System.Drawing.Size(219, 222);
            this.pictureBoxArticul.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxArticul.TabIndex = 8;
            this.pictureBoxArticul.TabStop = false;
            // 
            // cbTM
            // 
            this.cbTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.cbTM.DataSource = this.bsTM;
            this.cbTM.Font = new System.Drawing.Font("Arial", 10F);
            this.cbTM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbTM.FormattingEnabled = true;
            this.cbTM.Location = new System.Drawing.Point(275, 89);
            this.cbTM.Name = "cbTM";
            this.cbTM.Size = new System.Drawing.Size(110, 24);
            this.cbTM.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(224, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Модель";
            // 
            // txtMod
            // 
            this.txtMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.txtMod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMod.Font = new System.Drawing.Font("Arial", 12F);
            this.txtMod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.txtMod.Location = new System.Drawing.Point(275, 64);
            this.txtMod.Name = "txtMod";
            this.txtMod.Size = new System.Drawing.Size(74, 19);
            this.txtMod.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(220, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Артикул";
            // 
            // txbArticul
            // 
            this.txbArticul.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.txbArticul.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbArticul.Font = new System.Drawing.Font("Arial", 12F);
            this.txbArticul.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.txbArticul.Location = new System.Drawing.Point(275, 39);
            this.txbArticul.Name = "txbArticul";
            this.txbArticul.Size = new System.Drawing.Size(74, 19);
            this.txbArticul.TabIndex = 3;
            // 
            // txbPo
            // 
            this.txbPo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.txbPo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbPo.Font = new System.Drawing.Font("Arial", 12F);
            this.txbPo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.txbPo.Location = new System.Drawing.Point(355, 13);
            this.txbPo.Name = "txbPo";
            this.txbPo.Size = new System.Drawing.Size(23, 19);
            this.txbPo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(243, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Код";
            // 
            // txbKod
            // 
            this.txbKod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.txbKod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbKod.Font = new System.Drawing.Font("Arial", 12F);
            this.txbKod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.txbKod.Location = new System.Drawing.Point(275, 14);
            this.txbKod.Name = "txbKod";
            this.txbKod.Size = new System.Drawing.Size(74, 19);
            this.txbKod.TabIndex = 0;
            this.txbKod.TextChanged += new System.EventHandler(this.txbKod_TextChanged);
            // 
            // customButtonAdd
            // 
            this.customButtonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.customButtonAdd.Btn = null;
            this.customButtonAdd.ComponentBackColor = System.Drawing.Color.Empty;
            this.customButtonAdd.ComponentFontColor = System.Drawing.Color.Empty;
            this.customButtonAdd.ComponentSize = new System.Drawing.Size(0, 0);
            this.customButtonAdd.FlatAppearance.BorderSize = 0;
            this.customButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAdd.Font = new System.Drawing.Font("Arial", 12F);
            this.customButtonAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.customButtonAdd.Location = new System.Drawing.Point(1, 588);
            this.customButtonAdd.Name = "customButtonAdd";
            this.customButtonAdd.Size = new System.Drawing.Size(244, 30);
            this.customButtonAdd.TabIndex = 1;
            this.customButtonAdd.Text = "customButton1";
            this.customButtonAdd.UseVisualStyleBackColor = false;
            this.customButtonAdd.Click += new System.EventHandler(this.customButtonAdd_Click);
            // 
            // gridArt
            // 
<<<<<<< HEAD
            this.gridArt.DataSource = this.bsArt;
            this.gridArt.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
            this.gridArt.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.gridArt.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridArt.Location = new System.Drawing.Point(1, 1);
            this.gridArt.MainView = this.gridArticul;
            this.gridArt.Name = "gridArt";
            this.gridArt.Size = new System.Drawing.Size(464, 581);
            this.gridArt.TabIndex = 3;
            this.gridArt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridArticul});
            // 
            // gridArticul
            // 
            this.gridArticul.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridArticul.GridControl = this.gridArt;
            this.gridArticul.Name = "gridArticul";
            this.gridArticul.OptionsBehavior.Editable = false;
            this.gridArticul.OptionsFind.AlwaysVisible = true;
            this.gridArticul.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Filter;
            this.gridArticul.OptionsView.ShowAutoFilterRow = true;
            this.gridArticul.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Код";
            this.gridColumn1.FieldName = "kod";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 78;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Группа";
            this.gridColumn2.FieldName = "grup";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 94;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Артикул";
            this.gridColumn3.FieldName = "articul";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 84;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Размер";
            this.gridColumn4.FieldName = "razm";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 94;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Модель";
            this.gridColumn5.FieldName = "mod";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 102;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "ТМ";
            this.gridColumn6.FieldName = "kle";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 20;
=======
            this.customButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.customButton1.FlatAppearance.BorderSize = 0;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Font = new System.Drawing.Font("Arial", 12F);
            this.customButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.customButton1.Location = new System.Drawing.Point(1, 588);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(244, 30);
            this.customButton1.TabIndex = 1;
            this.customButton1.Text = "customButton1";
            this.customButton1.UseVisualStyleBackColor = false;
>>>>>>> b2d3435266bb10bae216c2c0adc49d8f201a2ed9
            // 
            // Articul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 700);
            this.Controls.Add(this.gridArt);
            this.Controls.Add(this.customButtonAdd);
            this.Controls.Add(this.groupControl1);
            this.Name = "Articul";
            this.Text = "Справочник изделий";
            this.Load += new System.EventHandler(this.Articul_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArticul)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsArticul)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArticul)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private CustomTextBox txbKod;
        private CustomTextBox txbPo;
        private CustomButton customButtonAdd;
        private CustomTextBox txbArticul;
        private System.Windows.Forms.BindingSource bsArticul;
        private System.Windows.Forms.BindingSource bsArt;
        private CustomTextBox txtMod;
        private SewingProduction.CustomComboBox cbTM;
        //private System.Windows.Forms.ComboBox cbTM;
        private System.Windows.Forms.BindingSource bsTM;
        private System.Windows.Forms.PictureBox pictureBoxArticul;
        private CustomLabel label1;
        private CustomLabel label2;
        private CustomLabel label3;
        private DevExpress.XtraGrid.GridControl gridArt;
        private DevExpress.XtraGrid.Views.Grid.GridView gridArticul;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}