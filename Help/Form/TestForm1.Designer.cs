using SewingProduction.Core.Class;
namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class TestForm1
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
            customGridControl1 = new CustomGridControlColumn();
            bindingSource1 = new System.Windows.Forms.BindingSource(components);
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            TestID = new DevExpress.XtraGrid.Columns.GridColumn();
            TestName = new DevExpress.XtraGrid.Columns.GridColumn();
            TestFirst = new DevExpress.XtraGrid.Columns.GridColumn();
            TestSecond = new DevExpress.XtraGrid.Columns.GridColumn();
            customGroupBox1 = new CustomGroupBox();
            customLabel1 = new CustomLabel();
            customTextBox1 = new CustomTextBox();
            customButton1 = new CustomButton();
            customTextBox2 = new CustomTextBox();
            customButton2 = new CustomButton();
            customLabel2 = new CustomLabel();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            customGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // customGridControl1
            // 
            customGridControl1.DataSource = bindingSource1;
            customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl1.Location = new System.Drawing.Point(12, 12);
            customGridControl1.MainView = gridView1;
            customGridControl1.Name = "customGridControl1";
            customGridControl1.Size = new System.Drawing.Size(492, 195);
            customGridControl1.TabIndex = 0;
            customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
            gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { TestID, TestName, TestFirst, TestSecond });
            gridView1.GridControl = customGridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // TestID
            // 
            TestID.Caption = "TestID";
            TestID.FieldName = "TestID";
            TestID.Name = "TestID";
            TestID.Visible = true;
            TestID.VisibleIndex = 0;
            // 
            // TestName
            // 
            TestName.Caption = "TestName";
            TestName.FieldName = "TestName";
            TestName.Name = "TestName";
            TestName.Visible = true;
            TestName.VisibleIndex = 1;
            // 
            // TestFirst
            // 
            TestFirst.Caption = "TestFirst";
            TestFirst.FieldName = "TestFirst";
            TestFirst.Name = "TestFirst";
            TestFirst.Visible = true;
            TestFirst.VisibleIndex = 2;
            // 
            // TestSecond
            // 
            TestSecond.Caption = "TestSecond";
            TestSecond.FieldName = "TestSecond";
            TestSecond.Name = "TestSecond";
            TestSecond.Visible = true;
            TestSecond.VisibleIndex = 3;
            // 
            // customGroupBox1
            // 
            customGroupBox1.BackColor = System.Drawing.Color.Transparent;
            customGroupBox1.Controls.Add(customLabel1);
            customGroupBox1.Controls.Add(customTextBox1);
            customGroupBox1.Controls.Add(customButton1);
            customGroupBox1.Location = new System.Drawing.Point(12, 229);
            customGroupBox1.Name = "customGroupBox1";
            customGroupBox1.Size = new System.Drawing.Size(259, 250);
            customGroupBox1.TabIndex = 1;
            customGroupBox1.TabStop = false;
            customGroupBox1.Text = "customGroupBox1";
            // 
            // customLabel1
            // 
            customLabel1.AutoSize = true;
            customLabel1.Font = new System.Drawing.Font("Arial", 10F);
            customLabel1.ForeColor = System.Drawing.Color.Black;
            customLabel1.Location = new System.Drawing.Point(15, 41);
            customLabel1.Name = "customLabel1";
            customLabel1.Size = new System.Drawing.Size(95, 16);
            customLabel1.TabIndex = 0;
            customLabel1.Text = "customLabel1";
            // 
            // customTextBox1
            // 
            customTextBox1.BackColor = System.Drawing.Color.White;
            customTextBox1.Font = new System.Drawing.Font("Arial", 10F);
            customTextBox1.ForeColor = System.Drawing.Color.Black;
            customTextBox1.Location = new System.Drawing.Point(15, 153);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.Size = new System.Drawing.Size(187, 23);
            customTextBox1.TabIndex = 3;
            customTextBox1.Text = "customTextBox1";
            // 
            // customButton1
            // 
            customButton1.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            customButton1.Font = new System.Drawing.Font("Arial", 10F);
            customButton1.ForeColor = System.Drawing.Color.Black;
            customButton1.Location = new System.Drawing.Point(15, 90);
            customButton1.Name = "customButton1";
            customButton1.Size = new System.Drawing.Size(187, 30);
            customButton1.TabIndex = 2;
            customButton1.Text = "customButton1";
            customButton1.UseVisualStyleBackColor = false;
            // 
            // customTextBox2
            // 
            customTextBox2.BackColor = System.Drawing.Color.White;
            customTextBox2.Font = new System.Drawing.Font("Arial", 10F);
            customTextBox2.ForeColor = System.Drawing.Color.Black;
            customTextBox2.Location = new System.Drawing.Point(313, 382);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.Size = new System.Drawing.Size(191, 23);
            customTextBox2.TabIndex = 1;
            customTextBox2.Text = "customTextBox2";
            // 
            // customButton2
            // 
            customButton2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            customButton2.Font = new System.Drawing.Font("Arial", 10F);
            customButton2.ForeColor = System.Drawing.Color.Black;
            customButton2.Location = new System.Drawing.Point(313, 319);
            customButton2.Name = "customButton2";
            customButton2.Size = new System.Drawing.Size(191, 30);
            customButton2.TabIndex = 2;
            customButton2.Text = "customButton2";
            customButton2.UseVisualStyleBackColor = false;
            // 
            // customLabel2
            // 
            customLabel2.AutoSize = true;
            customLabel2.Font = new System.Drawing.Font("Arial", 10F);
            customLabel2.ForeColor = System.Drawing.Color.Black;
            customLabel2.Location = new System.Drawing.Point(313, 270);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new System.Drawing.Size(95, 16);
            customLabel2.TabIndex = 3;
            customLabel2.Text = "customLabel2";
            // 
            // TestForm1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(590, 508);
            Controls.Add(customLabel2);
            Controls.Add(customButton2);
            Controls.Add(customTextBox2);
            Controls.Add(customGroupBox1);
            Controls.Add(customGridControl1);
            Name = "TestForm1";
            Text = "TestForm1";
            FormClosing += ProductForm_FormClosing;
            Load += TestForm1_Load;
            ((System.ComponentModel.ISupportInitialize)customGridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            customGroupBox1.ResumeLayout(false);
            customGroupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomGridControlColumn customGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private CustomGroupBox customGroupBox1;
        private CustomLabel customLabel1;
        private CustomTextBox customTextBox1;
        private CustomButton customButton1;
        private CustomTextBox customTextBox2;
        private CustomButton customButton2;
        private CustomLabel customLabel2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn TestID;
        private DevExpress.XtraGrid.Columns.GridColumn TestName;
        private DevExpress.XtraGrid.Columns.GridColumn TestFirst;
        private DevExpress.XtraGrid.Columns.GridColumn TestSecond;
    }
}