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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OborudBrig));
            this.gridBrig = new DevExpress.XtraGrid.GridControl();
            this.bindingBrig = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridZeh = new DevExpress.XtraGrid.GridControl();
            this.bindingZeh = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridOborud = new DevExpress.XtraGrid.GridControl();
            this.bindingOborud = new System.Windows.Forms.BindingSource(this.components);
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelBrig = new System.Windows.Forms.LinkLabel();
            this.linkLabelZeh = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingBrig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridZeh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingZeh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOborud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingOborud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridBrig
            // 
            this.gridBrig.DataSource = this.bindingBrig;
            resources.ApplyResources(this.gridBrig, "gridBrig");
            this.gridBrig.MainView = this.gridView1;
            this.gridBrig.Name = "gridBrig";
            this.gridBrig.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridBrig.Click += new System.EventHandler(this.gridBrig_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridBrig;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridZeh
            // 
            this.gridZeh.DataSource = this.bindingZeh;
            resources.ApplyResources(this.gridZeh, "gridZeh");
            this.gridZeh.MainView = this.gridView2;
            this.gridZeh.Name = "gridZeh";
            this.gridZeh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridZeh.Click += new System.EventHandler(this.gridZeh_Click);
            this.gridZeh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridZeh_KeyUp);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Transparent;
            this.gridView2.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gridView2.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.gridView2.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView2.GridControl = this.gridZeh;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridOborud
            // 
            this.gridOborud.DataSource = this.bindingOborud;
            resources.ApplyResources(this.gridOborud, "gridOborud");
            this.gridOborud.MainView = this.gridView3;
            this.gridOborud.Name = "gridOborud";
            this.gridOborud.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridOborud;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridView3.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView3_CellValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // linkLabelBrig
            // 
            resources.ApplyResources(this.linkLabelBrig, "linkLabelBrig");
            this.linkLabelBrig.LinkColor = System.Drawing.Color.Black;
            this.linkLabelBrig.Name = "linkLabelBrig";
            this.linkLabelBrig.TabStop = true;
            this.linkLabelBrig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelBrig_LinkClicked);
            // 
            // linkLabelZeh
            // 
            resources.ApplyResources(this.linkLabelZeh, "linkLabelZeh");
            this.linkLabelZeh.LinkColor = System.Drawing.Color.Black;
            this.linkLabelZeh.Name = "linkLabelZeh";
            this.linkLabelZeh.TabStop = true;
            this.linkLabelZeh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelZeh_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.linkLabelBrig, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridZeh, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.linkLabelZeh, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridOborud, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.gridBrig, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // OborudBrig
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OborudBrig";
            this.Activated += new System.EventHandler(this.OborudBrig_Activated);
            this.Load += new System.EventHandler(this.OborudBrig_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.gridBrig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingBrig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridZeh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingZeh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOborud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingOborud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridBrig;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridZeh;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridOborud;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.BindingSource bindingBrig;
        private System.Windows.Forms.BindingSource bindingZeh;
        private System.Windows.Forms.BindingSource bindingOborud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelZeh;
        private System.Windows.Forms.LinkLabel linkLabelBrig;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}