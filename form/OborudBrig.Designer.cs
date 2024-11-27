namespace SewingProduction.form
{
    partial class OborudBrig
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
            this.gridZeh = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridBrig = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridOborud = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bindingBrig = new System.Windows.Forms.BindingSource(this.components);
            this.bindingZehList = new System.Windows.Forms.BindingSource(this.components);
            this.bindingOborudBrig = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridZeh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOborud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingBrig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingZehList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingOborudBrig)).BeginInit();
            this.SuspendLayout();
            // 
            // gridZeh
            // 
            this.gridZeh.DataSource = this.bindingBrig;
            this.gridZeh.Location = new System.Drawing.Point(12, 87);
            this.gridZeh.MainView = this.gridView1;
            this.gridZeh.Name = "gridZeh";
            this.gridZeh.Size = new System.Drawing.Size(304, 862);
            this.gridZeh.TabIndex = 0;
            this.gridZeh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridZeh;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridBrig
            // 
            this.gridBrig.DataSource = this.bindingZehList;
            this.gridBrig.Location = new System.Drawing.Point(345, 87);
            this.gridBrig.MainView = this.gridView2;
            this.gridBrig.Name = "gridBrig";
            this.gridBrig.Size = new System.Drawing.Size(304, 862);
            this.gridBrig.TabIndex = 1;
            this.gridBrig.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridBrig;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridOborud
            // 
            this.gridOborud.DataSource = this.bindingOborudBrig;
            this.gridOborud.Location = new System.Drawing.Point(687, 87);
            this.gridOborud.MainView = this.gridView3;
            this.gridOborud.Name = "gridOborud";
            this.gridOborud.Size = new System.Drawing.Size(477, 862);
            this.gridOborud.TabIndex = 2;
            this.gridOborud.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridOborud;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // OborudBrig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1914, 961);
            this.Controls.Add(this.gridOborud);
            this.Controls.Add(this.gridBrig);
            this.Controls.Add(this.gridZeh);
            this.Name = "OborudBrig";
            this.Text = "Оборудование в бригадах";
            ((System.ComponentModel.ISupportInitialize)(this.gridZeh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOborud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingBrig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingZehList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingOborudBrig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridZeh;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridBrig;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridOborud;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.BindingSource bindingBrig;
        private System.Windows.Forms.BindingSource bindingZehList;
        private System.Windows.Forms.BindingSource bindingOborudBrig;
    }
}