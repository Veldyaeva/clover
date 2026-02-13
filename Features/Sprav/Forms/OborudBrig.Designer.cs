using SewingProduction.Core.Class;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OborudBrig));
            gridBrig = new CustomGridControl();
            bindingBrig = new System.Windows.Forms.BindingSource(components);
            gridViewBrig = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridZeh = new CustomGridControl();
            bindingZeh = new System.Windows.Forms.BindingSource(components);
            gridViewZeh = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridOborud = new CustomGridControl();
            bindingOborud = new System.Windows.Forms.BindingSource(components);
            gridViewOborud = new DevExpress.XtraGrid.Views.Grid.GridView();
            label2 = new System.Windows.Forms.Label();
            linkLabelBrig = new System.Windows.Forms.LinkLabel();
            linkLabelZeh = new System.Windows.Forms.LinkLabel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)gridBrig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingBrig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewBrig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridZeh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingZeh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewZeh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridOborud).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingOborud).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewOborud).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // gridBrig
            // 
            gridBrig.DataSource = bindingBrig;
            resources.ApplyResources(gridBrig, "gridBrig");
            gridBrig.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridBrig.EmbeddedNavigator.Margin");
            gridBrig.MainView = gridViewBrig;
            gridBrig.Name = "gridBrig";
            gridBrig.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewBrig });
            gridBrig.Click += gridBrig_Click;
            // 
            // gridViewBrig
            // 
            gridViewBrig.DetailHeight = 404;
            gridViewBrig.GridControl = gridBrig;
            gridViewBrig.Name = "gridViewBrig";
            gridViewBrig.OptionsBehavior.ReadOnly = true;
            gridViewBrig.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewBrig.OptionsView.ShowGroupPanel = false;
            // 
            // gridZeh
            // 
            gridZeh.DataSource = bindingZeh;
            resources.ApplyResources(gridZeh, "gridZeh");
            gridZeh.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridZeh.EmbeddedNavigator.Margin");
            gridZeh.MainView = gridViewZeh;
            gridZeh.Name = "gridZeh";
            gridZeh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewZeh });
            gridZeh.Click += gridZeh_Click;
            gridZeh.KeyUp += gridZeh_KeyUp;
            // 
            // gridViewZeh
            // 
            gridViewZeh.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Transparent;
            gridViewZeh.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            gridViewZeh.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            gridViewZeh.Appearance.OddRow.Options.UseBackColor = true;
            gridViewZeh.DetailHeight = 404;
            gridViewZeh.GridControl = gridZeh;
            gridViewZeh.Name = "gridViewZeh";
            gridViewZeh.OptionsBehavior.Editable = false;
            gridViewZeh.OptionsBehavior.ReadOnly = true;
            gridViewZeh.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewZeh.OptionsView.ShowGroupPanel = false;
            gridViewZeh.FocusedRowChanged += gridViewZeh_FocusedRowChanged;
            // 
            // gridOborud
            // 
            gridOborud.DataSource = bindingOborud;
            resources.ApplyResources(gridOborud, "gridOborud");
            gridOborud.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridOborud.EmbeddedNavigator.Margin");
            gridOborud.MainView = gridViewOborud;
            gridOborud.Name = "gridOborud";
            gridOborud.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewOborud });
            // 
            // gridViewOborud
            // 
            gridViewOborud.DetailHeight = 404;
            gridViewOborud.GridControl = gridOborud;
            gridViewOborud.Name = "gridViewOborud";
            gridViewOborud.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridViewOborud.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridViewOborud.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewOborud.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewOborud.OptionsView.ShowGroupPanel = false;
            gridViewOborud.CellValueChanged += gridView3_CellValueChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // linkLabelBrig
            // 
            resources.ApplyResources(linkLabelBrig, "linkLabelBrig");
            linkLabelBrig.LinkColor = System.Drawing.Color.Black;
            linkLabelBrig.Name = "linkLabelBrig";
            linkLabelBrig.TabStop = true;
            linkLabelBrig.LinkClicked += linkLabelBrig_LinkClicked;
            // 
            // linkLabelZeh
            // 
            resources.ApplyResources(linkLabelZeh, "linkLabelZeh");
            linkLabelZeh.LinkColor = System.Drawing.Color.Black;
            linkLabelZeh.Name = "linkLabelZeh";
            linkLabelZeh.TabStop = true;
            linkLabelZeh.LinkClicked += labelZeh_LinkClicked;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(linkLabelBrig, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 2, 0);
            tableLayoutPanel1.Controls.Add(gridZeh, 0, 1);
            tableLayoutPanel1.Controls.Add(linkLabelZeh, 0, 0);
            tableLayoutPanel1.Controls.Add(gridOborud, 2, 1);
            tableLayoutPanel1.Controls.Add(gridBrig, 1, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // OborudBrig
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "OborudBrig";
            Activated += OborudBrig_Activated;
            FormClosing += OborudBrig_FormClosing;
            Load += OborudBrig_Load_1;
            ((System.ComponentModel.ISupportInitialize)gridBrig).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingBrig).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewBrig).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridZeh).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingZeh).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewZeh).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridOborud).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingOborud).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewOborud).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CustomGridControl gridBrig;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBrig;
        private CustomGridControl gridZeh;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewZeh;
        private CustomGridControl gridOborud;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOborud;
        private System.Windows.Forms.BindingSource bindingBrig;
        private System.Windows.Forms.BindingSource bindingZeh;
        private System.Windows.Forms.BindingSource bindingOborud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelZeh;
        private System.Windows.Forms.LinkLabel linkLabelBrig;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}