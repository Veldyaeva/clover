namespace SewingProduction.Features.TeamWork.Forms
{
    partial class BaseNodeOperationEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            operationNameTextBox = new System.Windows.Forms.TextBox();
            equipmentTextBox = new System.Windows.Forms.TextBox();
            specTextBox = new System.Windows.Forms.TextBox();
            secondsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            razrydNumericUpDown = new System.Windows.Forms.NumericUpDown();
            kodObNumericUpDown = new System.Windows.Forms.NumericUpDown();
            kodProizvNumericUpDown = new System.Windows.Forms.NumericUpDown();
            kodPodrNumericUpDown = new System.Windows.Forms.NumericUpDown();
            operationCodeTextBox = new System.Windows.Forms.TextBox();
            chapterLabel = new System.Windows.Forms.Label();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            chapterTitleLabel = new System.Windows.Forms.Label();
            operationNameLabel = new System.Windows.Forms.Label();
            operationCodeLabel = new System.Windows.Forms.Label();
            equipmentLabel = new System.Windows.Forms.Label();
            specLabel = new System.Windows.Forms.Label();
            secondsLabel = new System.Windows.Forms.Label();
            razrydLabel = new System.Windows.Forms.Label();
            kodObLabel = new System.Windows.Forms.Label();
            kodProizvLabel = new System.Windows.Forms.Label();
            kodPodrLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)secondsNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)razrydNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kodObNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kodProizvNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kodPodrNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // operationNameTextBox
            // 
            operationNameTextBox.Location = new System.Drawing.Point(12, 72);
            operationNameTextBox.Name = "operationNameTextBox";
            operationNameTextBox.Size = new System.Drawing.Size(560, 23);
            operationNameTextBox.TabIndex = 0;
            // 
            // equipmentTextBox
            // 
            equipmentTextBox.Location = new System.Drawing.Point(12, 160);
            equipmentTextBox.Name = "equipmentTextBox";
            equipmentTextBox.Size = new System.Drawing.Size(272, 23);
            equipmentTextBox.TabIndex = 2;
            // 
            // specTextBox
            // 
            specTextBox.Location = new System.Drawing.Point(290, 160);
            specTextBox.Name = "specTextBox";
            specTextBox.Size = new System.Drawing.Size(282, 23);
            specTextBox.TabIndex = 3;
            // 
            // secondsNumericUpDown
            // 
            secondsNumericUpDown.Location = new System.Drawing.Point(12, 248);
            secondsNumericUpDown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            secondsNumericUpDown.Name = "secondsNumericUpDown";
            secondsNumericUpDown.Size = new System.Drawing.Size(130, 23);
            secondsNumericUpDown.TabIndex = 4;
            // 
            // razrydNumericUpDown
            // 
            razrydNumericUpDown.Location = new System.Drawing.Point(148, 248);
            razrydNumericUpDown.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            razrydNumericUpDown.Name = "razrydNumericUpDown";
            razrydNumericUpDown.Size = new System.Drawing.Size(130, 23);
            razrydNumericUpDown.TabIndex = 5;
            // 
            // kodObNumericUpDown
            // 
            kodObNumericUpDown.Location = new System.Drawing.Point(284, 248);
            kodObNumericUpDown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            kodObNumericUpDown.Name = "kodObNumericUpDown";
            kodObNumericUpDown.Size = new System.Drawing.Size(92, 23);
            kodObNumericUpDown.TabIndex = 6;
            // 
            // kodProizvNumericUpDown
            // 
            kodProizvNumericUpDown.Location = new System.Drawing.Point(382, 248);
            kodProizvNumericUpDown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            kodProizvNumericUpDown.Name = "kodProizvNumericUpDown";
            kodProizvNumericUpDown.Size = new System.Drawing.Size(92, 23);
            kodProizvNumericUpDown.TabIndex = 7;
            kodProizvNumericUpDown.Visible = false;
            // 
            // kodPodrNumericUpDown
            // 
            kodPodrNumericUpDown.Location = new System.Drawing.Point(480, 248);
            kodPodrNumericUpDown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            kodPodrNumericUpDown.Name = "kodPodrNumericUpDown";
            kodPodrNumericUpDown.Size = new System.Drawing.Size(92, 23);
            kodPodrNumericUpDown.TabIndex = 8;
            kodPodrNumericUpDown.Visible = false;
            // 
            // operationCodeTextBox
            // 
            operationCodeTextBox.Location = new System.Drawing.Point(12, 116);
            operationCodeTextBox.Name = "operationCodeTextBox";
            operationCodeTextBox.Size = new System.Drawing.Size(170, 23);
            operationCodeTextBox.TabIndex = 1;
            // 
            // chapterLabel
            // 
            chapterLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            chapterLabel.Location = new System.Drawing.Point(12, 28);
            chapterLabel.Name = "chapterLabel";
            chapterLabel.Size = new System.Drawing.Size(170, 23);
            chapterLabel.TabIndex = 10;
            chapterLabel.Text = "-";
            chapterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // okButton
            // 
            okButton.Location = new System.Drawing.Point(416, 304);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 27);
            okButton.TabIndex = 9;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(497, 304);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 27);
            cancelButton.TabIndex = 10;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // chapterTitleLabel
            // 
            chapterTitleLabel.AutoSize = true;
            chapterTitleLabel.Location = new System.Drawing.Point(12, 9);
            chapterTitleLabel.Name = "chapterTitleLabel";
            chapterTitleLabel.Size = new System.Drawing.Size(77, 15);
            chapterTitleLabel.TabIndex = 13;
            chapterTitleLabel.Text = "№ операции";
            // 
            // operationNameLabel
            // 
            operationNameLabel.AutoSize = true;
            operationNameLabel.Location = new System.Drawing.Point(12, 54);
            operationNameLabel.Name = "operationNameLabel";
            operationNameLabel.Size = new System.Drawing.Size(116, 15);
            operationNameLabel.TabIndex = 14;
            operationNameLabel.Text = "Название операции";
            // 
            // operationCodeLabel
            // 
            operationCodeLabel.AutoSize = true;
            operationCodeLabel.Location = new System.Drawing.Point(12, 98);
            operationCodeLabel.Name = "operationCodeLabel";
            operationCodeLabel.Size = new System.Drawing.Size(84, 15);
            operationCodeLabel.TabIndex = 15;
            operationCodeLabel.Text = "Код операции";
            // 
            // equipmentLabel
            // 
            equipmentLabel.AutoSize = true;
            equipmentLabel.Location = new System.Drawing.Point(12, 142);
            equipmentLabel.Name = "equipmentLabel";
            equipmentLabel.Size = new System.Drawing.Size(88, 15);
            equipmentLabel.TabIndex = 16;
            equipmentLabel.Text = "Оборудование";
            // 
            // specLabel
            // 
            specLabel.AutoSize = true;
            specLabel.Location = new System.Drawing.Point(290, 142);
            specLabel.Name = "specLabel";
            specLabel.Size = new System.Drawing.Size(92, 15);
            specLabel.TabIndex = 17;
            specLabel.Text = "Специальность";
            // 
            // secondsLabel
            // 
            secondsLabel.AutoSize = true;
            secondsLabel.Location = new System.Drawing.Point(12, 230);
            secondsLabel.Name = "secondsLabel";
            secondsLabel.Size = new System.Drawing.Size(55, 15);
            secondsLabel.TabIndex = 18;
            secondsLabel.Text = "Секунды";
            // 
            // razrydLabel
            // 
            razrydLabel.AutoSize = true;
            razrydLabel.Location = new System.Drawing.Point(148, 230);
            razrydLabel.Name = "razrydLabel";
            razrydLabel.Size = new System.Drawing.Size(44, 15);
            razrydLabel.TabIndex = 19;
            razrydLabel.Text = "Разряд";
            // 
            // kodObLabel
            // 
            kodObLabel.AutoSize = true;
            kodObLabel.Location = new System.Drawing.Point(284, 230);
            kodObLabel.Name = "kodObLabel";
            kodObLabel.Size = new System.Drawing.Size(47, 15);
            kodObLabel.TabIndex = 20;
            kodObLabel.Text = "Код об.";
            // 
            // kodProizvLabel
            // 
            kodProizvLabel.AutoSize = true;
            kodProizvLabel.Location = new System.Drawing.Point(382, 230);
            kodProizvLabel.Name = "kodProizvLabel";
            kodProizvLabel.Size = new System.Drawing.Size(72, 15);
            kodProizvLabel.TabIndex = 21;
            kodProizvLabel.Text = "Код произв.";
            kodProizvLabel.Visible = false;
            // 
            // kodPodrLabel
            // 
            kodPodrLabel.AutoSize = true;
            kodPodrLabel.Location = new System.Drawing.Point(480, 230);
            kodPodrLabel.Name = "kodPodrLabel";
            kodPodrLabel.Size = new System.Drawing.Size(77, 15);
            kodPodrLabel.TabIndex = 22;
            kodPodrLabel.Text = "Код подразд.";
            kodPodrLabel.Visible = false;
            // 
            // BaseNodeOperationEditForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(584, 343);
            Controls.Add(kodPodrLabel);
            Controls.Add(kodProizvLabel);
            Controls.Add(kodObLabel);
            Controls.Add(razrydLabel);
            Controls.Add(secondsLabel);
            Controls.Add(specLabel);
            Controls.Add(equipmentLabel);
            Controls.Add(operationCodeLabel);
            Controls.Add(operationNameLabel);
            Controls.Add(chapterTitleLabel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(chapterLabel);
            Controls.Add(operationCodeTextBox);
            Controls.Add(kodPodrNumericUpDown);
            Controls.Add(kodProizvNumericUpDown);
            Controls.Add(kodObNumericUpDown);
            Controls.Add(razrydNumericUpDown);
            Controls.Add(secondsNumericUpDown);
            Controls.Add(specTextBox);
            Controls.Add(equipmentTextBox);
            Controls.Add(operationNameTextBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BaseNodeOperationEditForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Параметры операции узла";
            ((System.ComponentModel.ISupportInitialize)secondsNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)razrydNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)kodObNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)kodProizvNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)kodPodrNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox operationNameTextBox;
        private System.Windows.Forms.TextBox equipmentTextBox;
        private System.Windows.Forms.TextBox specTextBox;
        private System.Windows.Forms.NumericUpDown secondsNumericUpDown;
        private System.Windows.Forms.NumericUpDown razrydNumericUpDown;
        private System.Windows.Forms.NumericUpDown kodObNumericUpDown;
        private System.Windows.Forms.NumericUpDown kodProizvNumericUpDown;
        private System.Windows.Forms.NumericUpDown kodPodrNumericUpDown;
        private System.Windows.Forms.TextBox operationCodeTextBox;
        private System.Windows.Forms.Label chapterLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label chapterTitleLabel;
        private System.Windows.Forms.Label operationNameLabel;
        private System.Windows.Forms.Label operationCodeLabel;
        private System.Windows.Forms.Label equipmentLabel;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.Label secondsLabel;
        private System.Windows.Forms.Label razrydLabel;
        private System.Windows.Forms.Label kodObLabel;
        private System.Windows.Forms.Label kodProizvLabel;
        private System.Windows.Forms.Label kodPodrLabel;
    }
}
