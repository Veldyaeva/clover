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
            this.gridControlSprav = new DevExpress.XtraGrid.GridControl();
            this.spravList = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButtonAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSprav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spravList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlSprav
            // 
            this.gridControlSprav.DataSource = this.spravList;
            this.gridControlSprav.Location = new System.Drawing.Point(12, 12);
            this.gridControlSprav.MainView = this.gridView1;
            this.gridControlSprav.Name = "gridControlSprav";
            this.gridControlSprav.Size = new System.Drawing.Size(639, 597);
            this.gridControlSprav.TabIndex = 0;
            this.gridControlSprav.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlSprav.Load += new System.EventHandler(this.gridControlSprav_Load);
            this.gridControlSprav.Click += new System.EventHandler(this.gridControlSprav_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlSprav;
            this.gridView1.Name = "gridView1";
            // 
            // simpleButtonAdd
            // 
            this.simpleButtonAdd.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButtonAdd.Appearance.Options.UseFont = true;
            this.simpleButtonAdd.Location = new System.Drawing.Point(687, 564);
            this.simpleButtonAdd.Name = "simpleButtonAdd";
            this.simpleButtonAdd.Size = new System.Drawing.Size(148, 45);
            this.simpleButtonAdd.TabIndex = 2;
            this.simpleButtonAdd.Text = "Добавление";
            this.simpleButtonAdd.Click += new System.EventHandler(this.simpleButtonAdd_Click);
            // 
            // SpravForAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 650);
            this.Controls.Add(this.simpleButtonAdd);
            this.Controls.Add(this.gridControlSprav);
            this.Name = "SpravForAll";
            this.Text = "Cправочник";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpravForAll_FormClosing);
            this.Load += new System.EventHandler(this.SpravForAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSprav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spravList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlSprav;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource spravList;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAdd;
    }
}