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
            this.customComboBox1 = new SewingProduction.CustomComboBox();
            this.customComboBox2 = new SewingProduction.CustomComboBox();
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
            this.customLabelTheme.ObjectName = null;
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
            this.customLabelSize.ObjectName = null;
            this.customLabelSize.Size = new System.Drawing.Size(56, 16);
            this.customLabelSize.TabIndex = 1;
            this.customLabelSize.Text = "Размер";
            // 
            // customComboBox1
            // 
            this.customComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(240)))), ((int)(((byte)(230)))));
            this.customComboBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.customComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(75)))), ((int)(((byte)(45)))));
            this.customComboBox1.FormattingEnabled = true;
            this.customComboBox1.Location = new System.Drawing.Point(77, 19);
            this.customComboBox1.Name = "customComboBox1";
            this.customComboBox1.ObjectName = null;
            this.customComboBox1.Size = new System.Drawing.Size(121, 24);
            this.customComboBox1.TabIndex = 3;
            // 
            // customComboBox2
            // 
            this.customComboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(240)))), ((int)(((byte)(230)))));
            this.customComboBox2.Font = new System.Drawing.Font("Arial", 10F);
            this.customComboBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(75)))), ((int)(((byte)(45)))));
            this.customComboBox2.FormattingEnabled = true;
            this.customComboBox2.Location = new System.Drawing.Point(77, 57);
            this.customComboBox2.Name = "customComboBox2";
            this.customComboBox2.ObjectName = null;
            this.customComboBox2.Size = new System.Drawing.Size(121, 24);
            this.customComboBox2.TabIndex = 4;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.customComboBox2);
            this.Controls.Add(this.customComboBox1);
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
        private CustomComboBox customComboBox1;
        private CustomComboBox customComboBox2;
    }
}