namespace SewingProduction.Features.KnittingProduction.Forms
{
    partial class KnittingMachinesLoading
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
            customLabel1 = new CustomLabel();
            customComboBox1 = new CustomComboBox();
            customLabel2 = new CustomLabel();
            SuspendLayout();
            // 
            // customLabel1
            // 
            customLabel1.AutoSize = true;
            customLabel1.Font = new System.Drawing.Font("Arial", 10F);
            customLabel1.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabel1.Location = new System.Drawing.Point(848, 40);
            customLabel1.Name = "customLabel1";
            customLabel1.Size = new System.Drawing.Size(135, 16);
            customLabel1.TabIndex = 0;
            customLabel1.Text = "Текущий загруз В/М";
            // 
            // customComboBox1
            // 
            customComboBox1.BackColor = System.Drawing.Color.FromArgb(220, 240, 250);
            customComboBox1.Font = new System.Drawing.Font("Arial", 10F);
            customComboBox1.ForeColor = System.Drawing.Color.FromArgb(25, 75, 105);
            customComboBox1.FormattingEnabled = true;
            customComboBox1.Location = new System.Drawing.Point(101, 65);
            customComboBox1.Name = "customComboBox1";
            customComboBox1.Size = new System.Drawing.Size(121, 24);
            customComboBox1.TabIndex = 1;
            // 
            // customLabel2
            // 
            customLabel2.AutoSize = true;
            customLabel2.Font = new System.Drawing.Font("Arial", 10F);
            customLabel2.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabel2.Location = new System.Drawing.Point(22, 68);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new System.Drawing.Size(73, 16);
            customLabel2.TabIndex = 2;
            customLabel2.Text = "Класс В/М";
            // 
            // KnittingMachinesLoading
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1830, 890);
            Controls.Add(customLabel2);
            Controls.Add(customComboBox1);
            Controls.Add(customLabel1);
            Name = "KnittingMachinesLoading";
            Text = "Текущий загурз В/М";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomLabel customLabel1;
        private CustomComboBox customComboBox1;
        private CustomLabel customLabel2;
    }
}