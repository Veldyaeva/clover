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
            customLabelTheme = new CustomLabel();
            customLabelSize = new CustomLabel();
            customComboBoxTheme = new CustomComboBox();
            customComboBoxSize = new CustomComboBox();
            SuspendLayout();
            // 
            // customLabelTheme
            // 
            customLabelTheme.AutoSize = true;
            customLabelTheme.BackColor = System.Drawing.Color.Transparent;
            customLabelTheme.Font = new System.Drawing.Font("Arial", 10F);
            customLabelTheme.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customLabelTheme.Location = new System.Drawing.Point(14, 25);
            customLabelTheme.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            customLabelTheme.Name = "customLabelTheme";
            customLabelTheme.Size = new System.Drawing.Size(41, 16);
            customLabelTheme.TabIndex = 0;
            customLabelTheme.Text = "Тема";
            // 
            // customLabelSize
            // 
            customLabelSize.AutoSize = true;
            customLabelSize.BackColor = System.Drawing.Color.Transparent;
            customLabelSize.Font = new System.Drawing.Font("Arial", 10F);
            customLabelSize.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customLabelSize.Location = new System.Drawing.Point(14, 69);
            customLabelSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            customLabelSize.Name = "customLabelSize";
            customLabelSize.Size = new System.Drawing.Size(56, 16);
            customLabelSize.TabIndex = 1;
            customLabelSize.Text = "Размер";
            // 
            // customComboBoxTheme
            // 
            customComboBoxTheme.BackColor = System.Drawing.Color.FromArgb(250, 240, 230);
            customComboBoxTheme.Font = new System.Drawing.Font("Arial", 10F);
            customComboBoxTheme.ForeColor = System.Drawing.Color.FromArgb(105, 75, 45);
            customComboBoxTheme.FormattingEnabled = true;
            customComboBoxTheme.Location = new System.Drawing.Point(90, 22);
            customComboBoxTheme.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customComboBoxTheme.Name = "customComboBoxTheme";
            customComboBoxTheme.Size = new System.Drawing.Size(140, 24);
            customComboBoxTheme.TabIndex = 3;
            customComboBoxTheme.SelectedIndexChanged += customComboBoxTheme_SelectedIndexChanged;
            // 
            // customComboBoxSize
            // 
            customComboBoxSize.BackColor = System.Drawing.Color.FromArgb(250, 240, 230);
            customComboBoxSize.Font = new System.Drawing.Font("Arial", 10F);
            customComboBoxSize.ForeColor = System.Drawing.Color.FromArgb(105, 75, 45);
            customComboBoxSize.FormattingEnabled = true;
            customComboBoxSize.Location = new System.Drawing.Point(90, 66);
            customComboBoxSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customComboBoxSize.Name = "customComboBoxSize";
            customComboBoxSize.Size = new System.Drawing.Size(140, 24);
            customComboBoxSize.TabIndex = 4;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(customComboBoxSize);
            Controls.Add(customComboBoxTheme);
            Controls.Add(customLabelSize);
            Controls.Add(customLabelTheme);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SettingsForm";
            Text = "SettingsForm";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomLabel customLabelTheme;
        private CustomLabel customLabelSize;
        private CustomComboBox customComboBoxTheme;
        private CustomComboBox customComboBoxSize;
    }
}