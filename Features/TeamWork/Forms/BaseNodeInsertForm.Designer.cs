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
            components = new System.ComponentModel.Container();
            mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            leftLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            nodesLabel = new System.Windows.Forms.Label();
            nodesListBox = new System.Windows.Forms.ListBox();
            rightTopLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            positionLabel = new System.Windows.Forms.Label();
            positionComboBox = new System.Windows.Forms.ComboBox();
            detailsLabel = new System.Windows.Forms.Label();
            previewGrid = new System.Windows.Forms.DataGridView();
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            editNodesButton = new System.Windows.Forms.Button();
            mainLayoutPanel.SuspendLayout();
            leftLayoutPanel.SuspendLayout();
            rightTopLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 2;
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.Controls.Add(leftLayoutPanel, 0, 0);
            mainLayoutPanel.Controls.Add(rightTopLayoutPanel, 1, 0);
            mainLayoutPanel.Controls.Add(previewGrid, 1, 1);
            mainLayoutPanel.Controls.Add(buttonsPanel, 1, 2);
            mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.Padding = new System.Windows.Forms.Padding(12);
            mainLayoutPanel.RowCount = 3;
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.Size = new System.Drawing.Size(1044, 621);
            mainLayoutPanel.TabIndex = 0;
            // 
            // leftLayoutPanel
            // 
            leftLayoutPanel.ColumnCount = 1;
            leftLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            leftLayoutPanel.Controls.Add(nodesLabel, 0, 0);
            leftLayoutPanel.Controls.Add(nodesListBox, 0, 1);
            leftLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            leftLayoutPanel.Location = new System.Drawing.Point(15, 15);
            leftLayoutPanel.Name = "leftLayoutPanel";
            leftLayoutPanel.RowCount = 2;
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            leftLayoutPanel.Size = new System.Drawing.Size(274, 550);
            leftLayoutPanel.TabIndex = 0;
            // 
            // nodesLabel
            // 
            nodesLabel.AutoSize = true;
            nodesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodesLabel.Location = new System.Drawing.Point(3, 0);
            nodesLabel.Name = "nodesLabel";
            nodesLabel.Size = new System.Drawing.Size(268, 20);
            nodesLabel.TabIndex = 0;
            nodesLabel.Text = "Базовые узлы";
            // 
            // nodesListBox
            // 
            nodesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            nodesListBox.FormattingEnabled = true;
            nodesListBox.ItemHeight = 20;
            nodesListBox.Location = new System.Drawing.Point(0, 28);
            nodesListBox.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            nodesListBox.Name = "nodesListBox";
            nodesListBox.Size = new System.Drawing.Size(262, 522);
            nodesListBox.TabIndex = 1;
            // 
            // rightTopLayoutPanel
            // 
            rightTopLayoutPanel.ColumnCount = 1;
            rightTopLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rightTopLayoutPanel.Controls.Add(positionLabel, 0, 0);
            rightTopLayoutPanel.Controls.Add(positionComboBox, 0, 1);
            rightTopLayoutPanel.Controls.Add(detailsLabel, 0, 2);
            rightTopLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            rightTopLayoutPanel.Location = new System.Drawing.Point(295, 15);
            rightTopLayoutPanel.Name = "rightTopLayoutPanel";
            rightTopLayoutPanel.RowCount = 3;
            rightTopLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            rightTopLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            rightTopLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            rightTopLayoutPanel.Size = new System.Drawing.Size(734, 107);
            rightTopLayoutPanel.TabIndex = 1;
            // 
            // positionLabel
            // 
            positionLabel.AutoSize = true;
            positionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            positionLabel.Location = new System.Drawing.Point(3, 0);
            positionLabel.Name = "positionLabel";
            positionLabel.Size = new System.Drawing.Size(728, 20);
            positionLabel.TabIndex = 0;
            positionLabel.Text = "Куда вставить узел";
            // 
            // positionComboBox
            // 
            positionComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            positionComboBox.FormattingEnabled = true;
            positionComboBox.Location = new System.Drawing.Point(3, 28);
            positionComboBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            positionComboBox.Name = "positionComboBox";
            positionComboBox.Size = new System.Drawing.Size(728, 28);
            positionComboBox.TabIndex = 1;
            // 
            // detailsLabel
            // 
            detailsLabel.AutoSize = true;
            detailsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            detailsLabel.Location = new System.Drawing.Point(3, 66);
            detailsLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            detailsLabel.Name = "detailsLabel";
            detailsLabel.Size = new System.Drawing.Size(728, 41);
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
            previewGrid.Location = new System.Drawing.Point(295, 134);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(734, 431);
            previewGrid.TabIndex = 2;
            // 
            // buttonsPanel
            // 
            buttonsPanel.AutoSize = true;
            buttonsPanel.Controls.Add(okButton);
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Controls.Add(editNodesButton);
            buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            buttonsPanel.Location = new System.Drawing.Point(295, 577);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(734, 32);
            buttonsPanel.TabIndex = 3;
            // 
            // okButton
            // 
            okButton.AutoSize = true;
            okButton.Location = new System.Drawing.Point(636, 3);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(95, 30);
            okButton.TabIndex = 0;
            okButton.Text = "Добавить";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.AutoSize = true;
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(542, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(88, 30);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // editNodesButton
            // 
            editNodesButton.AutoSize = true;
            editNodesButton.Location = new System.Drawing.Point(380, 3);
            editNodesButton.Name = "editNodesButton";
            editNodesButton.Size = new System.Drawing.Size(156, 30);
            editNodesButton.TabIndex = 2;
            editNodesButton.Text = "Редактировать узлы";
            editNodesButton.UseVisualStyleBackColor = true;
            editNodesButton.Click += EditNodesButton_Click;
            // 
            // BaseNodeInsertForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(1044, 621);
            Controls.Add(mainLayoutPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MinimumSize = new System.Drawing.Size(920, 560);
            Name = "BaseNodeInsertForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Добавить базовый узел";
            mainLayoutPanel.ResumeLayout(false);
            mainLayoutPanel.PerformLayout();
            leftLayoutPanel.ResumeLayout(false);
            leftLayoutPanel.PerformLayout();
            rightTopLayoutPanel.ResumeLayout(false);
            rightTopLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel leftLayoutPanel;
        private System.Windows.Forms.Label nodesLabel;
        private System.Windows.Forms.ListBox nodesListBox;
        private System.Windows.Forms.TableLayoutPanel rightTopLayoutPanel;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.ComboBox positionComboBox;
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.DataGridView previewGrid;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button editNodesButton;
    }
}
