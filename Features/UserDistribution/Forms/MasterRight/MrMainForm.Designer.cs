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
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Location = new System.Drawing.Point(12, 12);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1224, 600);
            panelControl1.TabIndex = 0;
            // 
            // MrMainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1248, 661);
            Controls.Add(panelControl1);
            Name = "MrMainForm";
            Text = "Мастер распределения прав";
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}