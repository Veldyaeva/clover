namespace SewingProduction.Features.UserDistribution.Forms.MasterRight
{
    partial class MrChoiceForm
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
            customCheckedListBox1 = new CustomCheckedListBox();
            resourcesCheckedListBoxControl1 = new DevExpress.XtraScheduler.UI.ResourcesCheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)resourcesCheckedListBoxControl1).BeginInit();
            SuspendLayout();
            // 
            // customCheckedListBox1
            // 
            customCheckedListBox1.Font = new System.Drawing.Font("Arial", 10F);
            customCheckedListBox1.FormattingEnabled = true;
            customCheckedListBox1.Items.AddRange(new object[] { "Создать нового пользователя", "аа", "ааб" });
            customCheckedListBox1.Location = new System.Drawing.Point(12, 12);
            customCheckedListBox1.Name = "customCheckedListBox1";
            customCheckedListBox1.ObjectName = null;
            customCheckedListBox1.Size = new System.Drawing.Size(300, 256);
            customCheckedListBox1.TabIndex = 0;
            // 
            // resourcesCheckedListBoxControl1
            // 
            resourcesCheckedListBoxControl1.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] { new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null), new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null) });
            resourcesCheckedListBoxControl1.Location = new System.Drawing.Point(345, 38);
            resourcesCheckedListBoxControl1.Name = "resourcesCheckedListBoxControl1";
            resourcesCheckedListBoxControl1.Size = new System.Drawing.Size(268, 199);
            resourcesCheckedListBoxControl1.TabIndex = 1;
            // 
            // MrChoiceForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1185, 602);
            Controls.Add(resourcesCheckedListBoxControl1);
            Controls.Add(customCheckedListBox1);
            Name = "MrChoiceForm";
            Text = "MrChoiceForm";
            ((System.ComponentModel.ISupportInitialize)resourcesCheckedListBoxControl1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CustomCheckedListBox customCheckedListBox1;
        private DevExpress.XtraScheduler.UI.ResourcesCheckedListBoxControl resourcesCheckedListBoxControl1;
    }
}