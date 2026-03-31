namespace SewingProduction.Features.TeamWork.Forms
{
    partial class BaseNodeInsertForm
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
            tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            leftPanel = new System.Windows.Forms.TableLayoutPanel();
            nodesCaptionLabel = new System.Windows.Forms.Label();
            nodesListBox = new System.Windows.Forms.ListBox();
            topPanel = new System.Windows.Forms.TableLayoutPanel();
            positionCaptionLabel = new System.Windows.Forms.Label();
            positionComboBox = new System.Windows.Forms.ComboBox();
            detailsLabel = new System.Windows.Forms.Label();
            previewGrid = new System.Windows.Forms.DataGridView();
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            tableLayoutPanelMain.SuspendLayout();
            leftPanel.SuspendLayout();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(leftPanel, 0, 0);
            tableLayoutPanelMain.Controls.Add(topPanel, 1, 0);
            tableLayoutPanelMain.Controls.Add(previewGrid, 1, 1);
            tableLayoutPanelMain.Controls.Add(buttonsPanel, 1, 2);
            tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(12);
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanelMain.Size = new System.Drawing.Size(1104, 641);
            tableLayoutPanelMain.TabIndex = 0;
            tableLayoutPanelMain.SetRowSpan(leftPanel, 2);
            // 
            // leftPanel
            // 
            leftPanel.ColumnCount = 1;
            leftPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            leftPanel.Controls.Add(nodesCaptionLabel, 0, 0);
            leftPanel.Controls.Add(nodesListBox, 0, 1);
            leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            leftPanel.Location = new System.Drawing.Point(15, 15);
            leftPanel.Name = "leftPanel";
            leftPanel.RowCount = 2;
            leftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            leftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            leftPanel.Size = new System.Drawing.Size(294, 574);
            leftPanel.TabIndex = 0;
            // 
            // nodesCaptionLabel
            // 
            nodesCaptionLabel.AutoSize = true;
            nodesCaptionLabel.Location = new System.Drawing.Point(3, 0);
            nodesCaptionLabel.Name = "nodesCaptionLabel";
            nodesCaptionLabel.Size = new System.Drawing.Size(107, 20);
            nodesCaptionLabel.TabIndex = 0;
            nodesCaptionLabel.Text = "Базовые узлы";
            // 
            // nodesListBox
            // 
            nodesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            nodesListBox.FormattingEnabled = true;
            nodesListBox.Location = new System.Drawing.Point(0, 28);
            nodesListBox.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            nodesListBox.Name = "nodesListBox";
            nodesListBox.Size = new System.Drawing.Size(282, 546);
            nodesListBox.TabIndex = 1;
            // 
            // topPanel
            // 
            topPanel.ColumnCount = 1;
            topPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            topPanel.Controls.Add(positionCaptionLabel, 0, 0);
            topPanel.Controls.Add(positionComboBox, 0, 1);
            topPanel.Controls.Add(detailsLabel, 0, 2);
            topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            topPanel.Location = new System.Drawing.Point(315, 15);
            topPanel.Name = "topPanel";
            topPanel.RowCount = 3;
            topPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            topPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            topPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            topPanel.Size = new System.Drawing.Size(774, 90);
            topPanel.TabIndex = 1;
            // 
            // positionCaptionLabel
            // 
            positionCaptionLabel.AutoSize = true;
            positionCaptionLabel.Location = new System.Drawing.Point(3, 0);
            positionCaptionLabel.Name = "positionCaptionLabel";
            positionCaptionLabel.Size = new System.Drawing.Size(142, 20);
            positionCaptionLabel.TabIndex = 0;
            positionCaptionLabel.Text = "Куда вставить узел";
            // 
            // positionComboBox
            // 
            positionComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            positionComboBox.FormattingEnabled = true;
            positionComboBox.Location = new System.Drawing.Point(3, 28);
            positionComboBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            positionComboBox.Name = "positionComboBox";
            positionComboBox.Size = new System.Drawing.Size(768, 28);
            positionComboBox.TabIndex = 1;
            // 
            // detailsLabel
            // 
            detailsLabel.AutoSize = true;
            detailsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            detailsLabel.Location = new System.Drawing.Point(3, 66);
            detailsLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            detailsLabel.Name = "detailsLabel";
            detailsLabel.Size = new System.Drawing.Size(768, 20);
            detailsLabel.TabIndex = 2;
            detailsLabel.Text = "detailsLabel";
            // 
            // previewGrid
            // 
            previewGrid.AllowUserToAddRows = false;
            previewGrid.AllowUserToDeleteRows = false;
            previewGrid.AllowUserToResizeRows = false;
            previewGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            previewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            previewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            previewGrid.Location = new System.Drawing.Point(315, 117);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(774, 472);
            previewGrid.TabIndex = 2;
            // 
            // buttonsPanel
            // 
            buttonsPanel.AutoSize = true;
            buttonsPanel.Controls.Add(okButton);
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            buttonsPanel.Location = new System.Drawing.Point(315, 601);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(774, 28);
            buttonsPanel.TabIndex = 3;
            // 
            // okButton
            // 
            okButton.AutoSize = true;
            okButton.Location = new System.Drawing.Point(698, 3);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(73, 25);
            okButton.TabIndex = 0;
            okButton.Text = "Добавить";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.AutoSize = true;
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(622, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(70, 25);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // BaseNodeInsertForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(1104, 641);
            Controls.Add(tableLayoutPanelMain);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MinimumSize = new System.Drawing.Size(920, 560);
            Name = "BaseNodeInsertForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Добавить базовый узел";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel leftPanel;
        private System.Windows.Forms.Label nodesCaptionLabel;
        private System.Windows.Forms.ListBox nodesListBox;
        private System.Windows.Forms.TableLayoutPanel topPanel;
        private System.Windows.Forms.Label positionCaptionLabel;
        private System.Windows.Forms.ComboBox positionComboBox;
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.DataGridView previewGrid;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
