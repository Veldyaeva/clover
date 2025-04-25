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
            this.simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControlLogin = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEditLogin = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLogin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton
            // 
            this.simpleButton.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButton.Appearance.Options.UseFont = true;
            this.simpleButton.Location = new System.Drawing.Point(209, 119);
            this.simpleButton.Name = "simpleButton";
            this.simpleButton.Size = new System.Drawing.Size(186, 42);
            this.simpleButton.TabIndex = 0;
            this.simpleButton.Text = "ВОЙТИ";
            this.simpleButton.Click += new System.EventHandler(this.simpleButton_Click);
            // 
            // labelControlLogin
            // 
            this.labelControlLogin.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlLogin.Appearance.Options.UseFont = true;
            this.labelControlLogin.Location = new System.Drawing.Point(31, 34);
            this.labelControlLogin.Name = "labelControlLogin";
            this.labelControlLogin.Size = new System.Drawing.Size(55, 22);
            this.labelControlLogin.TabIndex = 3;
            this.labelControlLogin.Text = "Логин";
            // 
            // labelControlPassword
            // 
            this.labelControlPassword.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlPassword.Appearance.Options.UseFont = true;
            this.labelControlPassword.Location = new System.Drawing.Point(31, 74);
            this.labelControlPassword.Name = "labelControlPassword";
            this.labelControlPassword.Size = new System.Drawing.Size(65, 22);
            this.labelControlPassword.TabIndex = 4;
            this.labelControlPassword.Text = "Пароль";
            // 
            // textEditPassword
            // 
            this.textEditPassword.EditValue = "0";
            this.textEditPassword.Location = new System.Drawing.Point(122, 73);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textEditPassword.Properties.Appearance.Options.UseFont = true;
            this.textEditPassword.Size = new System.Drawing.Size(273, 26);
            this.textEditPassword.TabIndex = 5;
            this.textEditPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            // 
            // comboBoxEditLogin
            // 
            this.comboBoxEditLogin.Location = new System.Drawing.Point(122, 33);
            this.comboBoxEditLogin.Name = "comboBoxEditLogin";
            this.comboBoxEditLogin.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxEditLogin.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditLogin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditLogin.Size = new System.Drawing.Size(273, 26);
            this.comboBoxEditLogin.TabIndex = 8;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(428, 183);
            this.Controls.Add(this.comboBoxEditLogin);
            this.Controls.Add(this.textEditPassword);
            this.Controls.Add(this.labelControlPassword);
            this.Controls.Add(this.labelControlLogin);
            this.Controls.Add(this.simpleButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(444, 222);
            this.MinimumSize = new System.Drawing.Size(444, 222);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLogin.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton;
        private DevExpress.XtraEditors.LabelControl labelControlLogin;
        private DevExpress.XtraEditors.LabelControl labelControlPassword;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLogin;
    }
}