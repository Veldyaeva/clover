namespace SewingProduction.form.UserDistribution
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            simpleButton = new DevExpress.XtraEditors.SimpleButton();
            labelControlLogin = new DevExpress.XtraEditors.LabelControl();
            labelControlPassword = new DevExpress.XtraEditors.LabelControl();
            textEditPassword = new DevExpress.XtraEditors.TextEdit();
            comboBoxEditLogin = new DevExpress.XtraEditors.ComboBoxEdit();
            customCheckBox1 = new CustomCheckBox();
            labelGlaz = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)textEditPassword.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)comboBoxEditLogin.Properties).BeginInit();
            SuspendLayout();
            // 
            // simpleButton
            // 
            simpleButton.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            simpleButton.Appearance.Options.UseFont = true;
            simpleButton.Location = new System.Drawing.Point(244, 137);
            simpleButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            simpleButton.Name = "simpleButton";
            simpleButton.Size = new System.Drawing.Size(217, 48);
            simpleButton.TabIndex = 0;
            simpleButton.Text = "ВОЙТИ";
            simpleButton.Click += simpleButton_Click;
            // 
            // labelControlLogin
            // 
            labelControlLogin.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelControlLogin.Appearance.Options.UseFont = true;
            labelControlLogin.Location = new System.Drawing.Point(36, 39);
            labelControlLogin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            labelControlLogin.Name = "labelControlLogin";
            labelControlLogin.Size = new System.Drawing.Size(55, 22);
            labelControlLogin.TabIndex = 3;
            labelControlLogin.Text = "Логин";
            // 
            // labelControlPassword
            // 
            labelControlPassword.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            labelControlPassword.Appearance.Options.UseFont = true;
            labelControlPassword.Location = new System.Drawing.Point(36, 85);
            labelControlPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            labelControlPassword.Name = "labelControlPassword";
            labelControlPassword.Size = new System.Drawing.Size(65, 22);
            labelControlPassword.TabIndex = 4;
            labelControlPassword.Text = "Пароль";
            // 
            // textEditPassword
            // 
            textEditPassword.EditValue = "";
            textEditPassword.Location = new System.Drawing.Point(142, 84);
            textEditPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textEditPassword.Name = "textEditPassword";
            textEditPassword.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textEditPassword.Properties.Appearance.Options.UseFont = true;
            textEditPassword.Properties.UseSystemPasswordChar = true;
            textEditPassword.Size = new System.Drawing.Size(318, 26);
            textEditPassword.TabIndex = 5;
            textEditPassword.KeyDown += LoginForm_KeyDown;
            // 
            // comboBoxEditLogin
            // 
            comboBoxEditLogin.Location = new System.Drawing.Point(142, 38);
            comboBoxEditLogin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxEditLogin.Name = "comboBoxEditLogin";
            comboBoxEditLogin.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            comboBoxEditLogin.Properties.Appearance.Options.UseFont = true;
            comboBoxEditLogin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            comboBoxEditLogin.Size = new System.Drawing.Size(318, 26);
            comboBoxEditLogin.TabIndex = 8;
            // 
            // customCheckBox1
            // 
            customCheckBox1.AutoSize = true;
            customCheckBox1.Font = new System.Drawing.Font("Arial", 10F);
            customCheckBox1.ForeColor = System.Drawing.Color.Black;
            customCheckBox1.Location = new System.Drawing.Point(49, 154);
            customCheckBox1.Name = "customCheckBox1";
            customCheckBox1.Size = new System.Drawing.Size(149, 20);
            customCheckBox1.TabIndex = 9;
            customCheckBox1.Text = "Сохранять пароль";
            customCheckBox1.UseVisualStyleBackColor = true;
            customCheckBox1.Visible = false;
            // 
            // labelGlaz
            // 
            labelGlaz.AutoSize = true;
            labelGlaz.BackColor = System.Drawing.Color.White;
            labelGlaz.Font = new System.Drawing.Font("Showcard Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelGlaz.Location = new System.Drawing.Point(434, 89);
            labelGlaz.Name = "labelGlaz";
            labelGlaz.Size = new System.Drawing.Size(22, 15);
            labelGlaz.TabIndex = 10;
            labelGlaz.Text = "👁";
            labelGlaz.MouseLeave += labelGlaz_MouseLeave;
            labelGlaz.MouseMove += labelGlaz_MouseMove;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Menu;
            ClientSize = new System.Drawing.Size(499, 211);
            Controls.Add(labelGlaz);
            Controls.Add(customCheckBox1);
            Controls.Add(comboBoxEditLogin);
            Controls.Add(textEditPassword);
            Controls.Add(labelControlPassword);
            Controls.Add(labelControlLogin);
            Controls.Add(simpleButton);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximumSize = new System.Drawing.Size(515, 250);
            MinimumSize = new System.Drawing.Size(515, 250);
            Name = "LoginForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Авторизация";
            KeyDown += LoginForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)textEditPassword.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)comboBoxEditLogin.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton;
        private DevExpress.XtraEditors.LabelControl labelControlLogin;
        private DevExpress.XtraEditors.LabelControl labelControlPassword;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLogin;
        private CustomCheckBox customCheckBox1;
        private System.Windows.Forms.Label labelGlaz;
    }
}