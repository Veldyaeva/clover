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
            splitContainerControlMain = new DevExpress.XtraEditors.SplitContainerControl();
            panelControlSteps = new DevExpress.XtraEditors.PanelControl();
            customTextBoxHelp = new SewingProduction.Core.Class.CustomTextBox();
            listBoxControlSteps = new DevExpress.XtraEditors.ListBoxControl();
            panelContent = new DevExpress.XtraEditors.PanelControl();
            panelControlBottom = new DevExpress.XtraEditors.PanelControl();
            customButtonCancel = new SewingProduction.Core.Class.CustomButton();
            btnFinish = new SewingProduction.Core.Class.CustomButton();
            btnBack = new SewingProduction.Core.Class.CustomButton();
            btnNext = new SewingProduction.Core.Class.CustomButton();
            ((System.ComponentModel.ISupportInitialize)splitContainerControlMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControlMain.Panel1).BeginInit();
            splitContainerControlMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControlMain.Panel2).BeginInit();
            splitContainerControlMain.Panel2.SuspendLayout();
            splitContainerControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControlSteps).BeginInit();
            panelControlSteps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)listBoxControlSteps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelContent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControlBottom).BeginInit();
            panelControlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerControlMain
            // 
            splitContainerControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerControlMain.Location = new System.Drawing.Point(0, 0);
            splitContainerControlMain.Name = "splitContainerControlMain";
            // 
            // splitContainerControlMain.Panel1
            // 
            splitContainerControlMain.Panel1.Controls.Add(panelControlSteps);
            splitContainerControlMain.Panel1.Text = "Panel1";
            // 
            // splitContainerControlMain.Panel2
            // 
            splitContainerControlMain.Panel2.Controls.Add(panelContent);
            splitContainerControlMain.Panel2.Controls.Add(panelControlBottom);
            splitContainerControlMain.Panel2.Text = "Panel2";
            splitContainerControlMain.Size = new System.Drawing.Size(1400, 825);
            splitContainerControlMain.SplitterPosition = 263;
            splitContainerControlMain.TabIndex = 0;
            // 
            // panelControlSteps
            // 
            panelControlSteps.Controls.Add(listBoxControlSteps);
            panelControlSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControlSteps.Location = new System.Drawing.Point(0, 0);
            panelControlSteps.Name = "panelControlSteps";
            panelControlSteps.Size = new System.Drawing.Size(263, 825);
            panelControlSteps.TabIndex = 0;
            // 
            // customTextBoxHelp
            // 
            customTextBoxHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            customTextBoxHelp.ErrorColor = System.Drawing.Color.Red;
            customTextBoxHelp.ErrorMessage = null;
            customTextBoxHelp.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            customTextBoxHelp.Location = new System.Drawing.Point(141, 17);
            customTextBoxHelp.Multiline = true;
            customTextBoxHelp.Name = "customTextBoxHelp";
            customTextBoxHelp.Size = new System.Drawing.Size(595, 63);
            customTextBoxHelp.TabIndex = 1;
            // 
            // listBoxControlSteps
            // 
            listBoxControlSteps.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold);
            listBoxControlSteps.Appearance.Options.UseFont = true;
            listBoxControlSteps.Location = new System.Drawing.Point(5, 5);
            listBoxControlSteps.Name = "listBoxControlSteps";
            listBoxControlSteps.Size = new System.Drawing.Size(253, 256);
            listBoxControlSteps.TabIndex = 0;
            // 
            // panelContent
            // 
            panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContent.Location = new System.Drawing.Point(0, 0);
            panelContent.Name = "panelContent";
            panelContent.Size = new System.Drawing.Size(1127, 733);
            panelContent.TabIndex = 0;
            // 
            // panelControlBottom
            // 
            panelControlBottom.Controls.Add(customTextBoxHelp);
            panelControlBottom.Controls.Add(customButtonCancel);
            panelControlBottom.Controls.Add(btnFinish);
            panelControlBottom.Controls.Add(btnBack);
            panelControlBottom.Controls.Add(btnNext);
            panelControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelControlBottom.Location = new System.Drawing.Point(0, 733);
            panelControlBottom.Name = "panelControlBottom";
            panelControlBottom.Size = new System.Drawing.Size(1127, 92);
            panelControlBottom.TabIndex = 1;
            // 
            // customButtonCancel
            // 
            customButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            customButtonCancel.Font = new System.Drawing.Font("Arial", 10F);
            customButtonCancel.Location = new System.Drawing.Point(15, 17);
            customButtonCancel.Name = "customButtonCancel";
            customButtonCancel.Size = new System.Drawing.Size(120, 63);
            customButtonCancel.TabIndex = 0;
            customButtonCancel.Text = "Отмена";
            customButtonCancel.UseVisualStyleBackColor = true;
            customButtonCancel.Click += btnCancel_Click;
            // 
            // btnFinish
            // 
            btnFinish.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnFinish.Font = new System.Drawing.Font("Arial", 10F);
            btnFinish.Location = new System.Drawing.Point(742, 17);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new System.Drawing.Size(120, 63);
            btnFinish.TabIndex = 1;
            btnFinish.Text = "Завершить";
            btnFinish.UseVisualStyleBackColor = true;
            btnFinish.Click += btnFinish_Click;
            // 
            // btnBack
            // 
            btnBack.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnBack.Font = new System.Drawing.Font("Arial", 10F);
            btnBack.Location = new System.Drawing.Point(868, 17);
            btnBack.Name = "btnBack";
            btnBack.Size = new System.Drawing.Size(120, 63);
            btnBack.TabIndex = 2;
            btnBack.Text = "Назад";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnNext.Font = new System.Drawing.Font("Arial", 10F);
            btnNext.Location = new System.Drawing.Point(994, 17);
            btnNext.Name = "btnNext";
            btnNext.Size = new System.Drawing.Size(120, 63);
            btnNext.TabIndex = 3;
            btnNext.Text = "Далее";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // MrMainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1400, 825);
            Controls.Add(splitContainerControlMain);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MrMainForm";
            Text = "Мастер распределения прав";
            Load += MrMainForm_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControlMain.Panel1).EndInit();
            splitContainerControlMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControlMain.Panel2).EndInit();
            splitContainerControlMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControlMain).EndInit();
            splitContainerControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControlSteps).EndInit();
            panelControlSteps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)listBoxControlSteps).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelContent).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControlBottom).EndInit();
            panelControlBottom.ResumeLayout(false);
            panelControlBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlMain;
        private DevExpress.XtraEditors.PanelControl panelControlSteps;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlSteps;
        private DevExpress.XtraEditors.PanelControl panelContent;
        private DevExpress.XtraEditors.PanelControl panelControlBottom;
        private SewingProduction.Core.Class.CustomButton customButtonCancel;
        private SewingProduction.Core.Class.CustomButton btnFinish;
        private SewingProduction.Core.Class.CustomButton btnBack;
        private SewingProduction.Core.Class.CustomButton btnNext;
        private Core.Class.CustomTextBox customTextBoxHelp;
    }
}
