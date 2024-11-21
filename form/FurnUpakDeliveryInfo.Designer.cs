namespace SewingProduction.form
{
    partial class FurnUpakDeliveryInfo
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
            this.tbKodF = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnKodFDelivInfo = new System.Windows.Forms.Button();
            this.gcReestrFurn = new DevExpress.XtraGrid.GridControl();
            this.bsReestrFurn = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.bsReestrFurnSost = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label4 = new System.Windows.Forms.Label();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.bsReestrFurnShtr = new System.Windows.Forms.BindingSource(this.components);
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label5 = new System.Windows.Forms.Label();
            this.gridControl4 = new DevExpress.XtraGrid.GridControl();
            this.bsReestrfurndeliverybag = new System.Windows.Forms.BindingSource(this.components);
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label6 = new System.Windows.Forms.Label();
            this.gridControl5 = new DevExpress.XtraGrid.GridControl();
            this.bsReestrfurndeliverybagsost = new System.Windows.Forms.BindingSource(this.components);
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gcReestrFurn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrFurn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrFurnSost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrFurnShtr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrfurndeliverybag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrfurndeliverybagsost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            this.SuspendLayout();
            // 
            // tbKodF
            // 
            this.tbKodF.Location = new System.Drawing.Point(63, 12);
            this.tbKodF.Name = "tbKodF";
            this.tbKodF.Size = new System.Drawing.Size(111, 20);
            this.tbKodF.TabIndex = 0;
            this.tbKodF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbKodF_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Заявка";
            // 
            // btnKodFDelivInfo
            // 
            this.btnKodFDelivInfo.Location = new System.Drawing.Point(228, 12);
            this.btnKodFDelivInfo.Name = "btnKodFDelivInfo";
            this.btnKodFDelivInfo.Size = new System.Drawing.Size(75, 23);
            this.btnKodFDelivInfo.TabIndex = 2;
            this.btnKodFDelivInfo.Text = "Поиск";
            this.btnKodFDelivInfo.UseVisualStyleBackColor = true;
            this.btnKodFDelivInfo.Click += new System.EventHandler(this.btnKodFDelivInfo_Click);
            // 
            // gcReestrFurn
            // 
            this.gcReestrFurn.DataSource = this.bsReestrFurn;
            this.gcReestrFurn.Location = new System.Drawing.Point(16, 56);
            this.gcReestrFurn.MainView = this.gridView1;
            this.gcReestrFurn.Name = "gcReestrFurn";
            this.gcReestrFurn.Size = new System.Drawing.Size(1320, 185);
            this.gcReestrFurn.TabIndex = 3;
            this.gcReestrFurn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcReestrFurn;
            this.gridView1.Name = "gridView1";
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gcReestrFurn_FocusedRowChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Заборные карты";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Состав заборной карты";
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.bsReestrFurnSost;
            this.gridControl2.Location = new System.Drawing.Point(16, 271);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(720, 185);
            this.gridControl2.TabIndex = 5;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(952, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "ШК заборной карты";
            // 
            // gridControl3
            // 
            this.gridControl3.DataSource = this.bsReestrFurnShtr;
            this.gridControl3.Location = new System.Drawing.Point(955, 271);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(381, 185);
            this.gridControl3.TabIndex = 7;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 468);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Места для доставки";
            // 
            // gridControl4
            // 
            this.gridControl4.DataSource = this.bsReestrfurndeliverybag;
            this.gridControl4.Location = new System.Drawing.Point(16, 487);
            this.gridControl4.MainView = this.gridView4;
            this.gridControl4.Name = "gridControl4";
            this.gridControl4.Size = new System.Drawing.Size(720, 185);
            this.gridControl4.TabIndex = 9;
            this.gridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.gridControl4;
            this.gridView4.Name = "gridView4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(952, 471);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "ШК транспортировочных мест";
            // 
            // gridControl5
            // 
            this.gridControl5.DataSource = this.bsReestrfurndeliverybagsost;
            this.gridControl5.Location = new System.Drawing.Point(955, 487);
            this.gridControl5.MainView = this.gridView5;
            this.gridControl5.Name = "gridControl5";
            this.gridControl5.Size = new System.Drawing.Size(381, 185);
            this.gridControl5.TabIndex = 11;
            this.gridControl5.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.GridControl = this.gridControl5;
            this.gridView5.Name = "gridView5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1342, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 83);
            this.button1.TabIndex = 13;
            this.button1.Text = "Печать\r\nсостава\r\nреестра";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1342, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 83);
            this.button2.TabIndex = 14;
            this.button2.Text = "Добавить\r\nв реестр";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(742, 271);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 83);
            this.button3.TabIndex = 16;
            this.button3.Text = "Удалить\r\nпозицию\r\nиз\r\nреестра";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1342, 271);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 83);
            this.button4.TabIndex = 15;
            this.button4.Text = "Печать\r\nШК\r\nЗаборной\r\nкарты";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1342, 487);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(72, 83);
            this.button5.TabIndex = 17;
            this.button5.Text = "Печать\r\nШК\r\nГрузового\r\nместа";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            // 
            // FurnUpakDeliveryInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 750);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gridControl5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gridControl4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gridControl3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gcReestrFurn);
            this.Controls.Add(this.btnKodFDelivInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbKodF);
            this.Name = "FurnUpakDeliveryInfo";
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FurnUpakDeliveryInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcReestrFurn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrFurn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrFurnSost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrFurnShtr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrfurndeliverybag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReestrfurndeliverybagsost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbKodF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKodFDelivInfo;
        private DevExpress.XtraGrid.GridControl gcReestrFurn;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraGrid.GridControl gridControl4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.GridControl gridControl5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.BindingSource bsReestrFurn;
        private System.Windows.Forms.BindingSource bsReestrFurnSost;
        private System.Windows.Forms.BindingSource bsReestrFurnShtr;
        private System.Windows.Forms.BindingSource bsReestrfurndeliverybag;
        private System.Windows.Forms.BindingSource bsReestrfurndeliverybagsost;
    }
}