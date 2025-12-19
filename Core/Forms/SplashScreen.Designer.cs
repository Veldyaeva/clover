namespace SewingProduction
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            imageSlider1 = new DevExpress.XtraEditors.Controls.ImageSlider();
            ((System.ComponentModel.ISupportInitialize)imageSlider1).BeginInit();
            SuspendLayout();
            // 
            // imageSlider1
            // 
            imageSlider1.CurrentImageIndex = 0;
            imageSlider1.Dock = System.Windows.Forms.DockStyle.Fill;
            imageSlider1.Images.Add((System.Drawing.Image)resources.GetObject("imageSlider1.Images"));
            imageSlider1.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.MiddleCenter;
            imageSlider1.Location = new System.Drawing.Point(0, 0);
            imageSlider1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            imageSlider1.Name = "imageSlider1";
            imageSlider1.Size = new System.Drawing.Size(933, 519);
            imageSlider1.TabIndex = 0;
            imageSlider1.Text = "imageSlider1";
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(imageSlider1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SplashScreen";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "SplashScreen";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)imageSlider1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Controls.ImageSlider imageSlider1;
    }
}