using SewingProduction.Core.Class;

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
            customCheckBoxSokrNameTabs = new CustomCheckBox();
            customCheckBoxPovtOpenTabs = new CustomCheckBox();
            customComboBoxRejimRab = new CustomComboBox();
            customLabelRejimRab = new CustomLabel();
            customButtonSaveExit = new CustomButton();
            colorPickEditLabel = new DevExpress.XtraEditors.ColorPickEdit();
            colorPickEditTextBox = new DevExpress.XtraEditors.ColorPickEdit();
            ((System.ComponentModel.ISupportInitialize)colorPickEditLabel.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorPickEditTextBox.Properties).BeginInit();
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
            customComboBoxSizeText.Location = new System.Drawing.Point(125, 201);
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
            customLabelSizeText.Location = new System.Drawing.Point(14, 204);
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
            customCheckBoxSaveOpenTabs.Location = new System.Drawing.Point(327, 21);
            customCheckBoxSaveOpenTabs.MinimumSize = new System.Drawing.Size(251, 0);
            customCheckBoxSaveOpenTabs.Name = "customCheckBoxSaveOpenTabs";
            customCheckBoxSaveOpenTabs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            customCheckBoxSaveOpenTabs.Size = new System.Drawing.Size(251, 20);
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
            customButtonClearProfile.Location = new System.Drawing.Point(654, 17);
            customButtonClearProfile.Name = "customButtonClearProfile";
            customButtonClearProfile.Size = new System.Drawing.Size(251, 29);
            customButtonClearProfile.TabIndex = 9;
            customButtonClearProfile.Text = "Очистить историю профилей";
            customButtonClearProfile.UseVisualStyleBackColor = false;
            customButtonClearProfile.Click += customButtonClearProfile_Click;
            // 
            // customCheckBoxSokrNameTabs
            // 
            customCheckBoxSokrNameTabs.AutoSize = true;
            customCheckBoxSokrNameTabs.Font = new System.Drawing.Font("Arial", 10F);
            customCheckBoxSokrNameTabs.ForeColor = System.Drawing.Color.Black;
            customCheckBoxSokrNameTabs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            customCheckBoxSokrNameTabs.Location = new System.Drawing.Point(327, 66);
            customCheckBoxSokrNameTabs.MinimumSize = new System.Drawing.Size(251, 0);
            customCheckBoxSokrNameTabs.Name = "customCheckBoxSokrNameTabs";
            customCheckBoxSokrNameTabs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            customCheckBoxSokrNameTabs.Size = new System.Drawing.Size(251, 20);
            customCheckBoxSokrNameTabs.TabIndex = 10;
            customCheckBoxSokrNameTabs.Text = "Сокращенное название вкладок";
            customCheckBoxSokrNameTabs.UseVisualStyleBackColor = true;
            customCheckBoxSokrNameTabs.CheckedChanged += customCheckBoxSokrNameVklad_CheckedChanged;
            // 
            // customCheckBoxPovtOpenTabs
            // 
            customCheckBoxPovtOpenTabs.AutoSize = true;
            customCheckBoxPovtOpenTabs.Font = new System.Drawing.Font("Arial", 10F);
            customCheckBoxPovtOpenTabs.ForeColor = System.Drawing.Color.Black;
            customCheckBoxPovtOpenTabs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            customCheckBoxPovtOpenTabs.Location = new System.Drawing.Point(327, 107);
            customCheckBoxPovtOpenTabs.MinimumSize = new System.Drawing.Size(251, 0);
            customCheckBoxPovtOpenTabs.Name = "customCheckBoxPovtOpenTabs";
            customCheckBoxPovtOpenTabs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            customCheckBoxPovtOpenTabs.Size = new System.Drawing.Size(251, 20);
            customCheckBoxPovtOpenTabs.TabIndex = 11;
            customCheckBoxPovtOpenTabs.Text = "Повторное открытие вкладок";
            customCheckBoxPovtOpenTabs.UseVisualStyleBackColor = true;
            customCheckBoxPovtOpenTabs.CheckedChanged += customCheckBoxPovtOpenTabs_CheckedChanged;
            // 
            // customComboBoxRejimRab
            // 
            customComboBoxRejimRab.BackColor = System.Drawing.Color.FromArgb(250, 240, 230);
            customComboBoxRejimRab.Font = new System.Drawing.Font("Arial", 10F);
            customComboBoxRejimRab.ForeColor = System.Drawing.Color.FromArgb(105, 75, 45);
            customComboBoxRejimRab.FormattingEnabled = true;
            customComboBoxRejimRab.Location = new System.Drawing.Point(125, 244);
            customComboBoxRejimRab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customComboBoxRejimRab.Name = "customComboBoxRejimRab";
            customComboBoxRejimRab.Size = new System.Drawing.Size(140, 24);
            customComboBoxRejimRab.TabIndex = 13;
            // 
            // customLabelRejimRab
            // 
            customLabelRejimRab.AutoSize = true;
            customLabelRejimRab.BackColor = System.Drawing.Color.Transparent;
            customLabelRejimRab.Font = new System.Drawing.Font("Arial", 10F);
            customLabelRejimRab.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customLabelRejimRab.Location = new System.Drawing.Point(14, 247);
            customLabelRejimRab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            customLabelRejimRab.Name = "customLabelRejimRab";
            customLabelRejimRab.Size = new System.Drawing.Size(103, 16);
            customLabelRejimRab.TabIndex = 12;
            customLabelRejimRab.Text = "Режим работы";
            // 
            // customButtonSaveExit
            // 
            customButtonSaveExit.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            customButtonSaveExit.Font = new System.Drawing.Font("Arial", 10F);
            customButtonSaveExit.ForeColor = System.Drawing.Color.Black;
            customButtonSaveExit.Location = new System.Drawing.Point(327, 478);
            customButtonSaveExit.Name = "customButtonSaveExit";
            customButtonSaveExit.Size = new System.Drawing.Size(251, 29);
            customButtonSaveExit.TabIndex = 14;
            customButtonSaveExit.Text = "Сохранить и Выйти";
            customButtonSaveExit.UseVisualStyleBackColor = false;
            customButtonSaveExit.Click += customButtonSaveExit_Click;
            // 
            // colorPickEditLabel
            // 
            colorPickEditLabel.EditValue = System.Drawing.Color.Empty;
            colorPickEditLabel.Location = new System.Drawing.Point(125, 63);
            colorPickEditLabel.Name = "colorPickEditLabel";
            colorPickEditLabel.Properties.AutomaticColor = System.Drawing.Color.Black;
            colorPickEditLabel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            colorPickEditLabel.Size = new System.Drawing.Size(140, 20);
            colorPickEditLabel.TabIndex = 15;
            colorPickEditLabel.EditValueChanged += colorPickEditLabel_EditValueChanged;
            // 
            // colorPickEditTextBox
            // 
            colorPickEditTextBox.EditValue = System.Drawing.Color.Empty;
            colorPickEditTextBox.Location = new System.Drawing.Point(125, 107);
            colorPickEditTextBox.Name = "colorPickEditTextBox";
            colorPickEditTextBox.Properties.AutomaticColor = System.Drawing.Color.Black;
            colorPickEditTextBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            colorPickEditTextBox.Size = new System.Drawing.Size(140, 20);
            colorPickEditTextBox.TabIndex = 16;
            colorPickEditTextBox.EditValueChanged += colorPickEditTextBox_EditValueChanged;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(colorPickEditTextBox);
            Controls.Add(colorPickEditLabel);
            Controls.Add(customButtonSaveExit);
            Controls.Add(customComboBoxRejimRab);
            Controls.Add(customLabelRejimRab);
            Controls.Add(customCheckBoxPovtOpenTabs);
            Controls.Add(customCheckBoxSokrNameTabs);
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
            ((System.ComponentModel.ISupportInitialize)colorPickEditLabel.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorPickEditTextBox.Properties).EndInit();
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
        private CustomCheckBox customCheckBoxSokrNameTabs;
        private CustomCheckBox customCheckBoxPovtOpenTabs;
        private CustomComboBox customComboBoxRejimRab;
        private CustomLabel customLabelRejimRab;
        private CustomButton customButtonSaveExit;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditLabel;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditTextBox;
    }
}