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
            searchLabel = new System.Windows.Forms.Label();
            searchTextBox = new System.Windows.Forms.TextBox();
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
            previewPanel = new System.Windows.Forms.Panel();
            previewPictureBox = new System.Windows.Forms.PictureBox();
            previewImageStatusLabel = new System.Windows.Forms.Label();
            previewSourceLabel = new System.Windows.Forms.Label();
            previewTitleLabel = new System.Windows.Forms.Label();
            previewToolTip = new System.Windows.Forms.ToolTip(components);
            mainLayoutPanel.SuspendLayout();
            leftLayoutPanel.SuspendLayout();
            rightTopLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            buttonsPanel.SuspendLayout();
            previewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).BeginInit();
            SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 3;
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 245F));
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            mainLayoutPanel.Controls.Add(previewPanel, 2, 0);
            mainLayoutPanel.Controls.Add(leftLayoutPanel, 0, 0);
            mainLayoutPanel.Controls.Add(rightTopLayoutPanel, 1, 0);
            mainLayoutPanel.Controls.Add(previewGrid, 1, 1);
            mainLayoutPanel.Controls.Add(buttonsPanel, 1, 2);
            mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            mainLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            mainLayoutPanel.RowCount = 3;
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.Size = new System.Drawing.Size(1318, 495);
            mainLayoutPanel.TabIndex = 0;
            // 
            // leftLayoutPanel
            // 
            leftLayoutPanel.ColumnCount = 1;
            leftLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            leftLayoutPanel.Controls.Add(nodesLabel, 0, 0);
            leftLayoutPanel.Controls.Add(searchLabel, 0, 1);
            leftLayoutPanel.Controls.Add(searchTextBox, 0, 2);
            leftLayoutPanel.Controls.Add(nodesListBox, 0, 3);
            leftLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            leftLayoutPanel.Location = new System.Drawing.Point(13, 11);
            leftLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            leftLayoutPanel.Name = "leftLayoutPanel";
            leftLayoutPanel.RowCount = 4;
            mainLayoutPanel.SetRowSpan(leftLayoutPanel, 3);
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            leftLayoutPanel.Size = new System.Drawing.Size(239, 453);
            leftLayoutPanel.TabIndex = 0;
            // 
            // nodesLabel
            // 
            nodesLabel.AutoSize = true;
            nodesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodesLabel.Location = new System.Drawing.Point(3, 0);
            nodesLabel.Name = "nodesLabel";
            nodesLabel.Size = new System.Drawing.Size(233, 15);
            nodesLabel.TabIndex = 0;
            nodesLabel.Text = "Базовые узлы";
            // 
            // searchLabel
            // 
            searchLabel.AutoSize = true;
            searchLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            searchLabel.Location = new System.Drawing.Point(3, 15);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new System.Drawing.Size(233, 15);
            searchLabel.TabIndex = 1;
            searchLabel.Text = "Поиск";
            // 
            // searchTextBox
            // 
            searchTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            searchTextBox.Location = new System.Drawing.Point(0, 36);
            searchTextBox.Margin = new System.Windows.Forms.Padding(0, 6, 10, 0);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Поиск по тегам и RT-коду";
            searchTextBox.Size = new System.Drawing.Size(229, 23);
            searchTextBox.TabIndex = 2;
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            // 
            // nodesListBox
            // 
            nodesListBox.DisplayMember = "DisplayName";
            nodesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            nodesListBox.FormattingEnabled = true;
            nodesListBox.HorizontalScrollbar = true;
            nodesListBox.ItemHeight = 15;
            nodesListBox.Location = new System.Drawing.Point(0, 65);
            nodesListBox.Margin = new System.Windows.Forms.Padding(0, 6, 10, 0);
            nodesListBox.Name = "nodesListBox";
            nodesListBox.ScrollAlwaysVisible = true;
            nodesListBox.Size = new System.Drawing.Size(229, 388);
            nodesListBox.TabIndex = 3;
            nodesListBox.SelectedIndexChanged += NodesListBox_SelectedIndexChanged;
            // 
            // rightTopLayoutPanel
            // 
            rightTopLayoutPanel.ColumnCount = 1;
            rightTopLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rightTopLayoutPanel.Controls.Add(positionLabel, 0, 0);
            rightTopLayoutPanel.Controls.Add(positionComboBox, 0, 1);
            rightTopLayoutPanel.Controls.Add(detailsLabel, 0, 2);
            rightTopLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            rightTopLayoutPanel.Location = new System.Drawing.Point(258, 11);
            rightTopLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            rightTopLayoutPanel.Name = "rightTopLayoutPanel";
            rightTopLayoutPanel.RowCount = 3;
            rightTopLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            rightTopLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            rightTopLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            rightTopLayoutPanel.Size = new System.Drawing.Size(855, 80);
            rightTopLayoutPanel.TabIndex = 1;
            // 
            // positionLabel
            // 
            positionLabel.AutoSize = true;
            positionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            positionLabel.Location = new System.Drawing.Point(3, 0);
            positionLabel.Name = "positionLabel";
            positionLabel.Size = new System.Drawing.Size(849, 15);
            positionLabel.TabIndex = 0;
            positionLabel.Text = "Куда вставить узел";
            // 
            // positionComboBox
            // 
            positionComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            positionComboBox.FormattingEnabled = true;
            positionComboBox.Location = new System.Drawing.Point(3, 21);
            positionComboBox.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            positionComboBox.Name = "positionComboBox";
            positionComboBox.Size = new System.Drawing.Size(849, 23);
            positionComboBox.TabIndex = 1;
            // 
            // detailsLabel
            // 
            detailsLabel.AutoSize = true;
            detailsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            detailsLabel.Location = new System.Drawing.Point(3, 52);
            detailsLabel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            detailsLabel.Name = "detailsLabel";
            detailsLabel.Size = new System.Drawing.Size(849, 28);
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
            previewGrid.Location = new System.Drawing.Point(258, 100);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(855, 328);
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
            buttonsPanel.Location = new System.Drawing.Point(258, 437);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(855, 29);
            buttonsPanel.TabIndex = 3;
            // 
            // okButton
            // 
            okButton.AutoSize = true;
            okButton.Location = new System.Drawing.Point(769, 2);
            okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(83, 25);
            okButton.TabIndex = 0;
            okButton.Text = "Добавить";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.AutoSize = true;
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(686, 2);
            cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(77, 25);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // editNodesButton
            // 
            editNodesButton.AutoSize = true;
            editNodesButton.Location = new System.Drawing.Point(544, 2);
            editNodesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            editNodesButton.Name = "editNodesButton";
            editNodesButton.Size = new System.Drawing.Size(136, 25);
            editNodesButton.TabIndex = 2;
            editNodesButton.Text = "Редактировать узлы";
            editNodesButton.UseVisualStyleBackColor = true;
            editNodesButton.Click += EditNodesButton_Click;
            // 
            // previewPanel
            // 
            previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewPanel.Controls.Add(previewPictureBox);
            previewPanel.Controls.Add(previewImageStatusLabel);
            previewPanel.Controls.Add(previewSourceLabel);
            previewPanel.Controls.Add(previewTitleLabel);
            previewPanel.Dock = System.Windows.Forms.DockStyle.Right;
            previewPanel.Location = new System.Drawing.Point(1119, 12);
            previewPanel.Name = "previewPanel";
            previewPanel.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            previewPanel.Size = new System.Drawing.Size(186, 495);
            previewPanel.TabIndex = 1;
            // 
            // previewPictureBox
            // 
            previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            previewPictureBox.Location = new System.Drawing.Point(10, 68);
            previewPictureBox.Name = "previewPictureBox";
            previewPictureBox.Size = new System.Drawing.Size(164, 389);
            previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            previewPictureBox.TabIndex = 0;
            previewPictureBox.TabStop = false;
            // 
            // previewImageStatusLabel
            // 
            previewImageStatusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            previewImageStatusLabel.Location = new System.Drawing.Point(10, 457);
            previewImageStatusLabel.Name = "previewImageStatusLabel";
            previewImageStatusLabel.Size = new System.Drawing.Size(164, 24);
            previewImageStatusLabel.TabIndex = 1;
            previewImageStatusLabel.Text = "Изображение не задано";
            previewImageStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // previewSourceLabel
            // 
            previewSourceLabel.Dock = System.Windows.Forms.DockStyle.Top;
            previewSourceLabel.Location = new System.Drawing.Point(10, 32);
            previewSourceLabel.Name = "previewSourceLabel";
            previewSourceLabel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            previewSourceLabel.Size = new System.Drawing.Size(164, 36);
            previewSourceLabel.TabIndex = 2;
            previewSourceLabel.Text = "РТ: -";
            previewSourceLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            previewSourceLabel.Click += PreviewSourceLabel_Click;
            // 
            // previewTitleLabel
            // 
            previewTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            previewTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            previewTitleLabel.Location = new System.Drawing.Point(10, 12);
            previewTitleLabel.Name = "previewTitleLabel";
            previewTitleLabel.Size = new System.Drawing.Size(164, 20);
            previewTitleLabel.TabIndex = 3;
            previewTitleLabel.Text = "Источник базового узла";
            // 
            // BaseNodeInsertForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(1318, 495);
            Controls.Add(mainLayoutPanel);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            MinimumSize = new System.Drawing.Size(807, 430);
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
            previewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel leftLayoutPanel;
        private System.Windows.Forms.Label nodesLabel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchTextBox;
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
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Label previewImageStatusLabel;
        private System.Windows.Forms.Label previewSourceLabel;
        private System.Windows.Forms.Label previewTitleLabel;
        private System.Windows.Forms.ToolTip previewToolTip;
    }
}
