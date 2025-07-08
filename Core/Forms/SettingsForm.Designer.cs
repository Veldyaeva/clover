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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            customLabelTheme = new CustomLabel();
            customComboBoxTheme = new CustomComboBox();
            customComboBoxSizeText = new CustomComboBox();
            customLabelSizeText = new CustomLabel();
            customCheckBoxSaveOpenTabs = new CustomCheckBox();
            customButtonClearProfile = new CustomButton();
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
            // customComboBoxTheme
            // 
            customComboBoxTheme.BackColor = System.Drawing.Color.FromArgb(250, 240, 230);
            customComboBoxTheme.Font = new System.Drawing.Font("Arial", 10F);
            customComboBoxTheme.ForeColor = System.Drawing.Color.FromArgb(105, 75, 45);
            customComboBoxTheme.FormattingEnabled = true;
            customComboBoxTheme.Location = new System.Drawing.Point(125, 22);
            customComboBoxTheme.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customComboBoxTheme.Name = "customComboBoxTheme";
            customComboBoxTheme.Size = new System.Drawing.Size(140, 24);
            customComboBoxTheme.TabIndex = 3;
            customComboBoxTheme.SelectedIndexChanged += customComboBoxTheme_SelectedIndexChanged;
            // 
            // customComboBoxSizeText
            // 
            customComboBoxSizeText.BackColor = System.Drawing.Color.FromArgb(250, 240, 230);
            customComboBoxSizeText.Font = new System.Drawing.Font("Arial", 10F);
            customComboBoxSizeText.ForeColor = System.Drawing.Color.FromArgb(105, 75, 45);
            customComboBoxSizeText.FormattingEnabled = true;
            customComboBoxSizeText.Location = new System.Drawing.Point(125, 67);
            customComboBoxSizeText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customComboBoxSizeText.Name = "customComboBoxSizeText";
            customComboBoxSizeText.Size = new System.Drawing.Size(140, 24);
            customComboBoxSizeText.TabIndex = 6;
            customComboBoxSizeText.SelectedIndexChanged += customComboBoxSizeText_SelectedIndexChanged;
            // 
            // customLabelSizeText
            // 
            customLabelSizeText.AutoSize = true;
            customLabelSizeText.BackColor = System.Drawing.Color.Transparent;
            customLabelSizeText.Font = new System.Drawing.Font("Arial", 10F);
            customLabelSizeText.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customLabelSizeText.Location = new System.Drawing.Point(14, 70);
            customLabelSizeText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            customLabelSizeText.Name = "customLabelSizeText";
            customLabelSizeText.Size = new System.Drawing.Size(103, 16);
            customLabelSizeText.TabIndex = 5;
            customLabelSizeText.Text = "Размер текста";
            // 
            // customCheckBoxSaveOpenTabs
            // 
            customCheckBoxSaveOpenTabs.AutoSize = true;
            customCheckBoxSaveOpenTabs.Font = new System.Drawing.Font("Arial", 10F);
            customCheckBoxSaveOpenTabs.ForeColor = System.Drawing.Color.Black;
            customCheckBoxSaveOpenTabs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            customCheckBoxSaveOpenTabs.Location = new System.Drawing.Point(14, 120);
            customCheckBoxSaveOpenTabs.Name = "customCheckBoxSaveOpenTabs";
            customCheckBoxSaveOpenTabs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            customCheckBoxSaveOpenTabs.Size = new System.Drawing.Size(227, 20);
            customCheckBoxSaveOpenTabs.TabIndex = 8;
            customCheckBoxSaveOpenTabs.Text = "Сохранение открытых вкладок";
            customCheckBoxSaveOpenTabs.UseVisualStyleBackColor = true;
            customCheckBoxSaveOpenTabs.CheckedChanged += customCheckBoxSaveOpenTabs_CheckedChanged;
            // 
            // customButtonClearProfile
            // 
            customButtonClearProfile.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            customButtonClearProfile.Font = new System.Drawing.Font("Arial", 10F);
            customButtonClearProfile.ForeColor = System.Drawing.Color.Black;
            customButtonClearProfile.Location = new System.Drawing.Point(14, 171);
            customButtonClearProfile.Name = "customButtonClearProfile";
            customButtonClearProfile.Size = new System.Drawing.Size(251, 29);
            customButtonClearProfile.TabIndex = 9;
            customButtonClearProfile.Text = "Очистить историю профилей";
            customButtonClearProfile.UseVisualStyleBackColor = false;
            customButtonClearProfile.Click += customButtonClearProfile_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(customButtonClearProfile);
            Controls.Add(customCheckBoxSaveOpenTabs);
            Controls.Add(customComboBoxSizeText);
            Controls.Add(customLabelSizeText);
            Controls.Add(customComboBoxTheme);
            Controls.Add(customLabelTheme);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SettingsForm";
            Text = "SettingsForm";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomLabel customLabelTheme;
        private CustomComboBox customComboBoxTheme;
        private CustomComboBox customComboBoxSizeText;
        private CustomLabel customLabelSizeText;
        private CustomCheckBox customCheckBoxSaveOpenTabs;
        private CustomButton customButtonClearProfile;
    }
}