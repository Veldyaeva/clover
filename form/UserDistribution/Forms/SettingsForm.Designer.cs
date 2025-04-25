namespace SewingProduction.form
{
    partial class SettingsForm
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
            this.customLabelTheme = new SewingProduction.CustomLabel();
            this.customLabelSize = new SewingProduction.CustomLabel();
            this.SuspendLayout();
            // 
            // customLabelTheme
            // 
            this.customLabelTheme.AutoSize = true;
            this.customLabelTheme.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTheme.Font = new System.Drawing.Font("Arial", 10F);
            this.customLabelTheme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customLabelTheme.Location = new System.Drawing.Point(12, 22);
            this.customLabelTheme.Name = "customLabelTheme";
            this.customLabelTheme.Size = new System.Drawing.Size(41, 16);
            this.customLabelTheme.TabIndex = 0;
            this.customLabelTheme.Text = "Тема";
            // 
            // customLabelSize
            // 
            this.customLabelSize.AutoSize = true;
            this.customLabelSize.BackColor = System.Drawing.Color.Transparent;
            this.customLabelSize.Font = new System.Drawing.Font("Arial", 10F);
            this.customLabelSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customLabelSize.Location = new System.Drawing.Point(12, 60);
            this.customLabelSize.Name = "customLabelSize";
            this.customLabelSize.Size = new System.Drawing.Size(56, 16);
            this.customLabelSize.TabIndex = 1;
            this.customLabelSize.Text = "Размер";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.customLabelSize);
            this.Controls.Add(this.customLabelTheme);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomLabel customLabelTheme;
        private CustomLabel customLabelSize;
    }
}