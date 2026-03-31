namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class MrChoiceForm
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
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            treeListSteps = new DevExpress.XtraTreeList.TreeList();
            colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItemTree = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)treeListSteps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemTree).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(treeListSteps);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(985, 530);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // treeListSteps
            // 
            treeListSteps.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { colName });
            treeListSteps.Location = new System.Drawing.Point(12, 12);
            treeListSteps.Name = "treeListSteps";
            treeListSteps.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeListSteps.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            treeListSteps.Size = new System.Drawing.Size(961, 506);
            treeListSteps.TabIndex = 4;
            treeListSteps.AfterCheckNode += treeListSteps_AfterCheckNode;
            // 
            // colName
            // 
            colName.Caption = "Шаг";
            colName.FieldName = "Name";
            colName.Name = "colName";
            colName.Visible = true;
            colName.VisibleIndex = 0;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItemTree });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(985, 530);
            Root.TextVisible = false;
            // 
            // layoutControlItemTree
            // 
            layoutControlItemTree.Control = treeListSteps;
            layoutControlItemTree.Location = new System.Drawing.Point(0, 0);
            layoutControlItemTree.Name = "layoutControlItemTree";
            layoutControlItemTree.Size = new System.Drawing.Size(965, 510);
            layoutControlItemTree.TextVisible = false;
            // 
            // MrChoiceForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(985, 530);
            Controls.Add(layoutControl1);
            Name = "MrChoiceForm";
            Text = "Выбор шагов мастера";
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)treeListSteps).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemTree).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTreeList.TreeList treeListSteps;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTree;
    }
}
