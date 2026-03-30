namespace SewingProduction.Features.UserDistribution.Forms.MasterRight
{
    partial class MrMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MrMainForm));
            customTextBoxHelp = new SewingProduction.Core.Class.CustomTextBox();
            btnFinish = new SewingProduction.Core.Class.CustomButton();
            btnBack = new SewingProduction.Core.Class.CustomButton();
            btnNext = new SewingProduction.Core.Class.CustomButton();
            panelContent = new DevExpress.XtraEditors.PanelControl();
            listBoxControlSteps = new DevExpress.XtraEditors.ListBoxControl();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)panelContent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)listBoxControlSteps).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // customTextBoxHelp
            // 
            customTextBoxHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            customTextBoxHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            customTextBoxHelp.ErrorColor = System.Drawing.Color.Red;
            customTextBoxHelp.ErrorMessage = null;
            customTextBoxHelp.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            customTextBoxHelp.Location = new System.Drawing.Point(3, 3);
            customTextBoxHelp.Multiline = true;
            customTextBoxHelp.Name = "customTextBoxHelp";
            customTextBoxHelp.Size = new System.Drawing.Size(838, 65);
            customTextBoxHelp.TabIndex = 1;
            // 
            // btnFinish
            // 
            btnFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            btnFinish.Font = new System.Drawing.Font("Arial", 10F);
            btnFinish.Location = new System.Drawing.Point(847, 3);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new System.Drawing.Size(114, 65);
            btnFinish.TabIndex = 1;
            btnFinish.Text = "Завершить";
            btnFinish.UseVisualStyleBackColor = true;
            btnFinish.Click += btnFinish_Click;
            // 
            // btnBack
            // 
            btnBack.Dock = System.Windows.Forms.DockStyle.Fill;
            btnBack.Font = new System.Drawing.Font("Arial", 10F);
            btnBack.Location = new System.Drawing.Point(967, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new System.Drawing.Size(114, 65);
            btnBack.TabIndex = 2;
            btnBack.Text = "Назад";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.Dock = System.Windows.Forms.DockStyle.Fill;
            btnNext.Font = new System.Drawing.Font("Arial", 10F);
            btnNext.Location = new System.Drawing.Point(1087, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new System.Drawing.Size(116, 65);
            btnNext.TabIndex = 3;
            btnNext.Text = "Далее";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // panelContent
            // 
            panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContent.Location = new System.Drawing.Point(184, 3);
            panelContent.Name = "panelContent";
            tableLayoutPanel1.SetRowSpan(panelContent, 2);
            panelContent.Size = new System.Drawing.Size(1025, 556);
            panelContent.TabIndex = 0;
            // 
            // listBoxControlSteps
            // 
            listBoxControlSteps.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold);
            listBoxControlSteps.Appearance.Options.UseFont = true;
            listBoxControlSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            listBoxControlSteps.Location = new System.Drawing.Point(3, 3);
            listBoxControlSteps.Name = "listBoxControlSteps";
            listBoxControlSteps.Size = new System.Drawing.Size(175, 294);
            listBoxControlSteps.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            tableLayoutPanel1.Controls.Add(listBoxControlSteps, 0, 0);
            tableLayoutPanel1.Controls.Add(panelContent, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.Size = new System.Drawing.Size(1212, 639);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel2.Controls.Add(btnNext, 3, 0);
            tableLayoutPanel2.Controls.Add(btnBack, 2, 0);
            tableLayoutPanel2.Controls.Add(btnFinish, 1, 0);
            tableLayoutPanel2.Controls.Add(customTextBoxHelp, 0, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 565);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(1206, 71);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // MrMainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1212, 639);
            Controls.Add(tableLayoutPanel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MrMainForm";
            Text = "Мастер распределения прав";
            Load += MrMainForm_Load;
            ((System.ComponentModel.ISupportInitialize)panelContent).EndInit();
            ((System.ComponentModel.ISupportInitialize)listBoxControlSteps).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraEditors.ListBoxControl listBoxControlSteps;
        private DevExpress.XtraEditors.PanelControl panelContent;
        private SewingProduction.Core.Class.CustomButton btnFinish;
        private SewingProduction.Core.Class.CustomButton btnBack;
        private SewingProduction.Core.Class.CustomButton btnNext;
        private Core.Class.CustomTextBox customTextBoxHelp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
