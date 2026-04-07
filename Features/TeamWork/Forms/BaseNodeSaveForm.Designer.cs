namespace SewingProduction.Features.TeamWork.Forms
{
    partial class BaseNodeSaveForm
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
            mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            summaryLabel = new System.Windows.Forms.Label();
            nameLabel = new System.Windows.Forms.Label();
            nameTextBox = new System.Windows.Forms.TextBox();
            descriptionLabel = new System.Windows.Forms.Label();
            descriptionTextBox = new System.Windows.Forms.TextBox();
            metadataLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            nodeGroupPanel = new System.Windows.Forms.TableLayoutPanel();
            nodeGroupLabel = new System.Windows.Forms.Label();
            nodeGroupComboBox = new System.Windows.Forms.ComboBox();
            productKindPanel = new System.Windows.Forms.TableLayoutPanel();
            productKindLabel = new System.Windows.Forms.Label();
            productKindComboBox = new System.Windows.Forms.ComboBox();
            productCategoryPanel = new System.Windows.Forms.TableLayoutPanel();
            productCategoryLabel = new System.Windows.Forms.Label();
            productCategoryComboBox = new System.Windows.Forms.ComboBox();
            operationsButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            deleteOperationButton = new System.Windows.Forms.Button();
            moveDownButton = new System.Windows.Forms.Button();
            moveUpButton = new System.Windows.Forms.Button();
            previewGrid = new System.Windows.Forms.DataGridView();
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            mainLayoutPanel.SuspendLayout();
            metadataLayoutPanel.SuspendLayout();
            nodeGroupPanel.SuspendLayout();
            productKindPanel.SuspendLayout();
            productCategoryPanel.SuspendLayout();
            operationsButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 1;
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.Controls.Add(summaryLabel, 0, 0);
            mainLayoutPanel.Controls.Add(nameLabel, 0, 1);
            mainLayoutPanel.Controls.Add(nameTextBox, 0, 2);
            mainLayoutPanel.Controls.Add(descriptionLabel, 0, 3);
            mainLayoutPanel.Controls.Add(descriptionTextBox, 0, 4);
            mainLayoutPanel.Controls.Add(metadataLayoutPanel, 0, 5);
            mainLayoutPanel.Controls.Add(operationsButtonsPanel, 0, 6);
            mainLayoutPanel.Controls.Add(previewGrid, 0, 7);
            mainLayoutPanel.Controls.Add(buttonsPanel, 0, 8);
            mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            mainLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            mainLayoutPanel.RowCount = 9;
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainLayoutPanel.Size = new System.Drawing.Size(861, 496);
            mainLayoutPanel.TabIndex = 0;
            // 
            // summaryLabel
            // 
            summaryLabel.AutoSize = true;
            summaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            summaryLabel.Location = new System.Drawing.Point(13, 9);
            summaryLabel.Name = "summaryLabel";
            summaryLabel.Size = new System.Drawing.Size(835, 15);
            summaryLabel.TabIndex = 0;
            summaryLabel.Text = "summaryLabel";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nameLabel.Location = new System.Drawing.Point(13, 33);
            nameLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(835, 15);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Название узла";
            // 
            // nameTextBox
            // 
            nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            nameTextBox.Location = new System.Drawing.Point(13, 50);
            nameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Например: Обработка манжеты";
            nameTextBox.Size = new System.Drawing.Size(835, 23);
            nameTextBox.TabIndex = 2;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            descriptionLabel.Location = new System.Drawing.Point(13, 84);
            descriptionLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(835, 15);
            descriptionLabel.TabIndex = 3;
            descriptionLabel.Text = "Описание";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            descriptionTextBox.Location = new System.Drawing.Point(13, 101);
            descriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            descriptionTextBox.Size = new System.Drawing.Size(835, 68);
            descriptionTextBox.TabIndex = 4;
            // 
            // metadataLayoutPanel
            // 
            metadataLayoutPanel.ColumnCount = 3;
            metadataLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            metadataLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            metadataLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            metadataLayoutPanel.Controls.Add(nodeGroupPanel, 1, 0);
            metadataLayoutPanel.Controls.Add(productKindPanel, 0, 0);
            metadataLayoutPanel.Controls.Add(productCategoryPanel, 2, 0);
            metadataLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            metadataLayoutPanel.Location = new System.Drawing.Point(13, 180);
            metadataLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            metadataLayoutPanel.Name = "metadataLayoutPanel";
            metadataLayoutPanel.RowCount = 1;
            metadataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            metadataLayoutPanel.Size = new System.Drawing.Size(835, 47);
            metadataLayoutPanel.TabIndex = 5;
            // 
            // nodeGroupPanel
            // 
            nodeGroupPanel.ColumnCount = 1;
            nodeGroupPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            nodeGroupPanel.Controls.Add(nodeGroupLabel, 0, 0);
            nodeGroupPanel.Controls.Add(nodeGroupComboBox, 0, 1);
            nodeGroupPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeGroupPanel.Location = new System.Drawing.Point(278, 0);
            nodeGroupPanel.Margin = new System.Windows.Forms.Padding(0);
            nodeGroupPanel.Name = "nodeGroupPanel";
            nodeGroupPanel.RowCount = 2;
            nodeGroupPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            nodeGroupPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            nodeGroupPanel.Size = new System.Drawing.Size(278, 47);
            nodeGroupPanel.TabIndex = 0;
            // 
            // nodeGroupLabel
            // 
            nodeGroupLabel.AutoSize = true;
            nodeGroupLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeGroupLabel.Location = new System.Drawing.Point(3, 0);
            nodeGroupLabel.Name = "nodeGroupLabel";
            nodeGroupLabel.Size = new System.Drawing.Size(272, 15);
            nodeGroupLabel.TabIndex = 0;
            nodeGroupLabel.Text = "Группа узла";
            // 
            // nodeGroupComboBox
            // 
            nodeGroupComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            nodeGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            nodeGroupComboBox.FormattingEnabled = true;
            nodeGroupComboBox.Location = new System.Drawing.Point(3, 17);
            nodeGroupComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            nodeGroupComboBox.Name = "nodeGroupComboBox";
            nodeGroupComboBox.Size = new System.Drawing.Size(272, 23);
            nodeGroupComboBox.TabIndex = 1;
            // 
            // productKindPanel
            // 
            productKindPanel.ColumnCount = 1;
            productKindPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            productKindPanel.Controls.Add(productKindLabel, 0, 0);
            productKindPanel.Controls.Add(productKindComboBox, 0, 1);
            productKindPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            productKindPanel.Location = new System.Drawing.Point(0, 0);
            productKindPanel.Margin = new System.Windows.Forms.Padding(0);
            productKindPanel.Name = "productKindPanel";
            productKindPanel.RowCount = 2;
            productKindPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productKindPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productKindPanel.Size = new System.Drawing.Size(278, 47);
            productKindPanel.TabIndex = 1;
            // 
            // productKindLabel
            // 
            productKindLabel.AutoSize = true;
            productKindLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            productKindLabel.Location = new System.Drawing.Point(3, 0);
            productKindLabel.Name = "productKindLabel";
            productKindLabel.Size = new System.Drawing.Size(272, 15);
            productKindLabel.TabIndex = 0;
            productKindLabel.Text = "Тип узла";
            // 
            // productKindComboBox
            // 
            productKindComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            productKindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            productKindComboBox.FormattingEnabled = true;
            productKindComboBox.Location = new System.Drawing.Point(3, 17);
            productKindComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            productKindComboBox.Name = "productKindComboBox";
            productKindComboBox.Size = new System.Drawing.Size(272, 23);
            productKindComboBox.TabIndex = 1;
            // 
            // productCategoryPanel
            // 
            productCategoryPanel.ColumnCount = 1;
            productCategoryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            productCategoryPanel.Controls.Add(productCategoryLabel, 0, 0);
            productCategoryPanel.Controls.Add(productCategoryComboBox, 0, 1);
            productCategoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            productCategoryPanel.Location = new System.Drawing.Point(556, 0);
            productCategoryPanel.Margin = new System.Windows.Forms.Padding(0);
            productCategoryPanel.Name = "productCategoryPanel";
            productCategoryPanel.RowCount = 2;
            productCategoryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productCategoryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productCategoryPanel.Size = new System.Drawing.Size(279, 47);
            productCategoryPanel.TabIndex = 2;
            // 
            // productCategoryLabel
            // 
            productCategoryLabel.AutoSize = true;
            productCategoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            productCategoryLabel.Location = new System.Drawing.Point(3, 0);
            productCategoryLabel.Name = "productCategoryLabel";
            productCategoryLabel.Size = new System.Drawing.Size(273, 15);
            productCategoryLabel.TabIndex = 0;
            productCategoryLabel.Text = "Категория изделия";
            // 
            // productCategoryComboBox
            // 
            productCategoryComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            productCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            productCategoryComboBox.FormattingEnabled = true;
            productCategoryComboBox.Location = new System.Drawing.Point(3, 17);
            productCategoryComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            productCategoryComboBox.Name = "productCategoryComboBox";
            productCategoryComboBox.Size = new System.Drawing.Size(273, 23);
            productCategoryComboBox.TabIndex = 1;
            // 
            // operationsButtonsPanel
            // 
            operationsButtonsPanel.AutoSize = true;
            operationsButtonsPanel.Controls.Add(deleteOperationButton);
            operationsButtonsPanel.Controls.Add(moveDownButton);
            operationsButtonsPanel.Controls.Add(moveUpButton);
            operationsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            operationsButtonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            operationsButtonsPanel.Location = new System.Drawing.Point(13, 236);
            operationsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            operationsButtonsPanel.Name = "operationsButtonsPanel";
            operationsButtonsPanel.Size = new System.Drawing.Size(835, 29);
            operationsButtonsPanel.TabIndex = 6;
            // 
            // deleteOperationButton
            // 
            deleteOperationButton.AutoSize = true;
            deleteOperationButton.Location = new System.Drawing.Point(744, 2);
            deleteOperationButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            deleteOperationButton.Name = "deleteOperationButton";
            deleteOperationButton.Size = new System.Drawing.Size(88, 25);
            deleteOperationButton.TabIndex = 0;
            deleteOperationButton.Text = "Удалить";
            deleteOperationButton.UseVisualStyleBackColor = true;
            deleteOperationButton.Click += DeleteOperationButton_Click;
            // 
            // moveDownButton
            // 
            moveDownButton.AutoSize = true;
            moveDownButton.Location = new System.Drawing.Point(647, 2);
            moveDownButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            moveDownButton.Name = "moveDownButton";
            moveDownButton.Size = new System.Drawing.Size(91, 25);
            moveDownButton.TabIndex = 1;
            moveDownButton.Text = "Вниз";
            moveDownButton.UseVisualStyleBackColor = true;
            moveDownButton.Click += MoveDownButton_Click;
            // 
            // moveUpButton
            // 
            moveUpButton.AutoSize = true;
            moveUpButton.Location = new System.Drawing.Point(550, 2);
            moveUpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            moveUpButton.Name = "moveUpButton";
            moveUpButton.Size = new System.Drawing.Size(91, 25);
            moveUpButton.TabIndex = 2;
            moveUpButton.Text = "Вверх";
            moveUpButton.UseVisualStyleBackColor = true;
            moveUpButton.Click += MoveUpButton_Click;
            // 
            // previewGrid
            // 
            previewGrid.AllowUserToAddRows = false;
            previewGrid.AllowUserToDeleteRows = false;
            previewGrid.AllowUserToResizeRows = false;
            previewGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            previewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            previewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            previewGrid.Location = new System.Drawing.Point(13, 267);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(835, 182);
            previewGrid.TabIndex = 7;
            // 
            // buttonsPanel
            // 
            buttonsPanel.AutoSize = true;
            buttonsPanel.Controls.Add(okButton);
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            buttonsPanel.Location = new System.Drawing.Point(13, 458);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(835, 29);
            buttonsPanel.TabIndex = 8;
            // 
            // okButton
            // 
            okButton.AutoSize = true;
            okButton.Location = new System.Drawing.Point(749, 2);
            okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(83, 25);
            okButton.TabIndex = 0;
            okButton.Text = "Сохранить";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.AutoSize = true;
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(666, 2);
            cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(77, 25);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // BaseNodeSaveForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(861, 496);
            Controls.Add(mainLayoutPanel);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            MinimumSize = new System.Drawing.Size(790, 475);
            Name = "BaseNodeSaveForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Сохранить как базовый узел";
            mainLayoutPanel.ResumeLayout(false);
            mainLayoutPanel.PerformLayout();
            metadataLayoutPanel.ResumeLayout(false);
            nodeGroupPanel.ResumeLayout(false);
            nodeGroupPanel.PerformLayout();
            productKindPanel.ResumeLayout(false);
            productKindPanel.PerformLayout();
            productCategoryPanel.ResumeLayout(false);
            productCategoryPanel.PerformLayout();
            operationsButtonsPanel.ResumeLayout(false);
            operationsButtonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TableLayoutPanel metadataLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel nodeGroupPanel;
        private System.Windows.Forms.Label nodeGroupLabel;
        private System.Windows.Forms.ComboBox nodeGroupComboBox;
        private System.Windows.Forms.TableLayoutPanel productKindPanel;
        private System.Windows.Forms.Label productKindLabel;
        private System.Windows.Forms.ComboBox productKindComboBox;
        private System.Windows.Forms.TableLayoutPanel productCategoryPanel;
        private System.Windows.Forms.Label productCategoryLabel;
        private System.Windows.Forms.ComboBox productCategoryComboBox;
        private System.Windows.Forms.FlowLayoutPanel operationsButtonsPanel;
        private System.Windows.Forms.Button deleteOperationButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.DataGridView previewGrid;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
