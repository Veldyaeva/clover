namespace SewingProduction.form.TeamWork.Forms
{
    partial class norm_raskrNew
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.difficult1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.Disk1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Bound1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.difficult2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.Disk2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Bound2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.difficult3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.Disk3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn13 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Bound3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn16 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new SewingProduction.CustomCancelButton();
            this.btnOk = new SewingProduction.CustomOkButton();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.customComboBox1 = new SewingProduction.CustomComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.tablePanel1.SetColumn(this.gridControl1, 0);
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(13, 67);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.tablePanel1.SetRow(this.gridControl1, 1);
            this.gridControl1.Size = new System.Drawing.Size(1326, 331);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.difficult1,
            this.difficult2,
            this.difficult3});
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(1099, 310, 266, 236);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // difficult1
            // 
            this.difficult1.Caption = "Сложность1";
            this.difficult1.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.Disk1,
            this.Bound1});
            this.difficult1.Name = "difficult1";
            this.difficult1.VisibleIndex = 0;
            this.difficult1.Width = 455;
            // 
            // Disk1
            // 
            this.Disk1.Caption = "Диск";
            this.Disk1.Columns.Add(this.gridColumn1);
            this.Disk1.Columns.Add(this.gridColumn2);
            this.Disk1.Columns.Add(this.gridColumn3);
            this.Disk1.Name = "Disk1";
            this.Disk1.VisibleIndex = 0;
            this.Disk1.Width = 212;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "рассекание";
            this.gridColumn1.FieldName = "dras1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.Width = 44;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "резка диском";
            this.gridColumn2.FieldName = "drez1";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.Width = 49;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "до проймы";
            this.gridColumn3.FieldName = "dpro1";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.Width = 119;
            // 
            // Bound1
            // 
            this.Bound1.Caption = "Лента";
            this.Bound1.Columns.Add(this.gridColumn4);
            this.Bound1.Columns.Add(this.gridColumn5);
            this.Bound1.Columns.Add(this.gridColumn6);
            this.Bound1.Name = "Bound1";
            this.Bound1.VisibleIndex = 1;
            this.Bound1.Width = 243;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Рассекание";
            this.gridColumn4.FieldName = "lras1";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.Width = 74;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "резка диском";
            this.gridColumn5.FieldName = "lrez1";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.Width = 50;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "до проймы";
            this.gridColumn6.FieldName = "lpro1";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.Width = 119;
            // 
            // difficult2
            // 
            this.difficult2.Caption = "Сложность2";
            this.difficult2.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.Disk2,
            this.Bound2});
            this.difficult2.Name = "difficult2";
            this.difficult2.VisibleIndex = 1;
            this.difficult2.Width = 411;
            // 
            // Disk2
            // 
            this.Disk2.Caption = "Диск";
            this.Disk2.Columns.Add(this.gridColumn7);
            this.Disk2.Columns.Add(this.gridColumn8);
            this.Disk2.Columns.Add(this.gridColumn9);
            this.Disk2.Name = "Disk2";
            this.Disk2.VisibleIndex = 0;
            this.Disk2.Width = 182;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Рассекание";
            this.gridColumn7.FieldName = "dras2";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.Width = 57;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "резка диском";
            this.gridColumn8.FieldName = "drez2";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.Width = 65;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "до проймы";
            this.gridColumn9.FieldName = "dpro2";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.Width = 60;
            // 
            // Bound2
            // 
            this.Bound2.Caption = "Лента";
            this.Bound2.Columns.Add(this.gridColumn10);
            this.Bound2.Columns.Add(this.gridColumn11);
            this.Bound2.Columns.Add(this.gridColumn12);
            this.Bound2.Name = "Bound2";
            this.Bound2.VisibleIndex = 1;
            this.Bound2.Width = 229;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Рассекание";
            this.gridColumn10.FieldName = "lras2";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.Width = 57;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "резка диском";
            this.gridColumn11.FieldName = "lrez2";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.Width = 66;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "до проймы";
            this.gridColumn12.FieldName = "lpro2";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.Width = 106;
            // 
            // difficult3
            // 
            this.difficult3.Caption = "Сложность3";
            this.difficult3.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.Disk3,
            this.Bound3});
            this.difficult3.Name = "difficult3";
            this.difficult3.VisibleIndex = 2;
            this.difficult3.Width = 618;
            // 
            // Disk3
            // 
            this.Disk3.Caption = "Диск";
            this.Disk3.Columns.Add(this.gridColumn13);
            this.Disk3.Columns.Add(this.gridColumn14);
            this.Disk3.Columns.Add(this.gridColumn15);
            this.Disk3.Name = "Disk3";
            this.Disk3.VisibleIndex = 0;
            this.Disk3.Width = 312;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Рассекание";
            this.gridColumn13.FieldName = "dras3";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.Width = 81;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "резка диском";
            this.gridColumn14.FieldName = "drez3";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.Width = 154;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "до проймы";
            this.gridColumn15.FieldName = "dpro3";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.Width = 77;
            // 
            // Bound3
            // 
            this.Bound3.Caption = "Лента";
            this.Bound3.Columns.Add(this.gridColumn16);
            this.Bound3.Columns.Add(this.gridColumn17);
            this.Bound3.Columns.Add(this.gridColumn18);
            this.Bound3.Name = "Bound3";
            this.Bound3.VisibleIndex = 1;
            this.Bound3.Width = 306;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Рассекание";
            this.gridColumn16.FieldName = "lras3";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.Width = 81;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "резка диском";
            this.gridColumn17.FieldName = "lrez3";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.Width = 148;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "до проймы";
            this.gridColumn18.FieldName = "lpro3";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.Width = 77;
            // 
            // panel1
            // 
            this.tablePanel1.SetColumn(this.panel1, 0);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Location = new System.Drawing.Point(13, 406);
            this.panel1.Name = "panel1";
            this.tablePanel1.SetRow(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(1326, 40);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.btnCancel.Location = new System.Drawing.Point(1239, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Arial", 10F);
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.btnOk.Location = new System.Drawing.Point(1158, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F)});
            this.tablePanel1.Controls.Add(this.customComboBox1);
            this.tablePanel1.Controls.Add(this.gridControl1);
            this.tablePanel1.Controls.Add(this.panel1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 55F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 335F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1352, 464);
            this.tablePanel1.TabIndex = 2;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // customComboBox1
            // 
            this.customComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.tablePanel1.SetColumn(this.customComboBox1, 0);
            this.customComboBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.customComboBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.customComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.customComboBox1.FormattingEnabled = true;
            this.customComboBox1.Location = new System.Drawing.Point(13, 12);
            this.customComboBox1.Name = "customComboBox1";
            this.tablePanel1.SetRow(this.customComboBox1, 0);
            this.customComboBox1.Size = new System.Drawing.Size(256, 24);
            this.customComboBox1.TabIndex = 2;
            // 
            // norm_raskrNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 464);
            this.Controls.Add(this.tablePanel1);
            this.Name = "norm_raskrNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор нормы раскроя";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.Panel panel1;
        private SewingProduction.CustomCancelButton btnCancel;
        private SewingProduction.CustomOkButton btnOk;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private CustomComboBox customComboBox1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn12;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn13;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn15;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn16;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn17;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn18;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand difficult1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Disk1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Bound1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand difficult2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Disk2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Bound2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand difficult3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Disk3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Bound3;
    }
}