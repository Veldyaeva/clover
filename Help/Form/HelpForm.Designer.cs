namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            WebBrowserHelp = new System.Windows.Forms.WebBrowser();
            buttonBack = new DevExpress.XtraEditors.SimpleButton();
            SuspendLayout();
            // 
            // WebBrowserHelp
            // 
            WebBrowserHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            WebBrowserHelp.Location = new System.Drawing.Point(0, 0);
            WebBrowserHelp.Name = "WebBrowserHelp";
            WebBrowserHelp.Size = new System.Drawing.Size(1138, 533);
            WebBrowserHelp.TabIndex = 0;
            WebBrowserHelp.Navigating += webBrowserHelp_Navigating;
            // 
            // buttonBack
            // 
            buttonBack.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonBack.Location = new System.Drawing.Point(1026, 12);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new System.Drawing.Size(100, 23);
            buttonBack.TabIndex = 1;
            buttonBack.Text = "Назад";
            buttonBack.Click += buttonBack_Click;
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1138, 533);
            Controls.Add(buttonBack);
            Controls.Add(WebBrowserHelp);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "HelpForm";
            Text = "Помощь";
            Load += HelpForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.WebBrowser WebBrowserHelp;
        private DevExpress.XtraEditors.SimpleButton buttonBack;
    }
}