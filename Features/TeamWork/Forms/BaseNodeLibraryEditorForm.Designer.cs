namespace SewingProduction.Features.TeamWork.Forms
{
    partial class BaseNodeLibraryEditorForm
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
            editorPanel = new System.Windows.Forms.TableLayoutPanel();
            detailsLabel = new System.Windows.Forms.Label();
            previewGrid = new System.Windows.Forms.DataGridView();
            operationsButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            deleteOperationButton = new System.Windows.Forms.Button();
            moveDownButton = new System.Windows.Forms.Button();
            moveUpButton = new System.Windows.Forms.Button();
            codeLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            nodeCodeLabel = new System.Windows.Forms.Label();
            nodeCodeValueLabel = new System.Windows.Forms.Label();
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
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            closeButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            deleteButton = new System.Windows.Forms.Button();
            mainLayoutPanel.SuspendLayout();
            leftLayoutPanel.SuspendLayout();
            editorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            operationsButtonsPanel.SuspendLayout();
            codeLayoutPanel.SuspendLayout();
            metadataLayoutPanel.SuspendLayout();
            nodeGroupPanel.SuspendLayout();
            productKindPanel.SuspendLayout();
            productCategoryPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 2;
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.Controls.Add(leftLayoutPanel, 0, 0);
            mainLayoutPanel.Controls.Add(editorPanel, 1, 0);
            mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.Padding = new System.Windows.Forms.Padding(12);
            mainLayoutPanel.RowCount = 1;
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.Size = new System.Drawing.Size(1164, 681);
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
            leftLayoutPanel.Size = new System.Drawing.Size(274, 651);
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
            nodesListBox.Size = new System.Drawing.Size(262, 623);
            nodesListBox.TabIndex = 1;
            // 
            // editorPanel
            // 
            editorPanel.ColumnCount = 1;
            editorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            editorPanel.Controls.Add(detailsLabel, 0, 0);
            editorPanel.Controls.Add(previewGrid, 0, 1);
            editorPanel.Controls.Add(operationsButtonsPanel, 0, 2);
            editorPanel.Controls.Add(codeLayoutPanel, 0, 3);
            editorPanel.Controls.Add(nameLabel, 0, 4);
            editorPanel.Controls.Add(nameTextBox, 0, 5);
            editorPanel.Controls.Add(descriptionLabel, 0, 6);
            editorPanel.Controls.Add(descriptionTextBox, 0, 7);
            editorPanel.Controls.Add(metadataLayoutPanel, 0, 8);
            editorPanel.Controls.Add(buttonsPanel, 0, 9);
            editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            editorPanel.Location = new System.Drawing.Point(295, 15);
            editorPanel.Name = "editorPanel";
            editorPanel.RowCount = 10;
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            editorPanel.Size = new System.Drawing.Size(854, 651);
            editorPanel.TabIndex = 1;
            // 
            // detailsLabel
            // 
            detailsLabel.AutoSize = true;
            detailsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            detailsLabel.Location = new System.Drawing.Point(3, 0);
            detailsLabel.Name = "detailsLabel";
            detailsLabel.Size = new System.Drawing.Size(848, 20);
            detailsLabel.TabIndex = 0;
            detailsLabel.Text = "Выберите базовый узел для редактирования.";
            // 
            // previewGrid
            // 
            previewGrid.AllowUserToAddRows = false;
            previewGrid.AllowUserToDeleteRows = false;
            previewGrid.AllowUserToResizeRows = false;
            previewGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            previewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            previewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            previewGrid.Location = new System.Drawing.Point(3, 23);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(848, 281);
            previewGrid.TabIndex = 1;
            // 
            // operationsButtonsPanel
            // 
            operationsButtonsPanel.AutoSize = true;
            operationsButtonsPanel.Controls.Add(deleteOperationButton);
            operationsButtonsPanel.Controls.Add(moveDownButton);
            operationsButtonsPanel.Controls.Add(moveUpButton);
            operationsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            operationsButtonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            operationsButtonsPanel.Location = new System.Drawing.Point(3, 316);
            operationsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            operationsButtonsPanel.Name = "operationsButtonsPanel";
            operationsButtonsPanel.Size = new System.Drawing.Size(848, 18);
            operationsButtonsPanel.TabIndex = 2;
            // 
            // deleteOperationButton
            // 
            deleteOperationButton.AutoSize = true;
            deleteOperationButton.Location = new System.Drawing.Point(744, 3);
            deleteOperationButton.Name = "deleteOperationButton";
            deleteOperationButton.Size = new System.Drawing.Size(101, 30);
            deleteOperationButton.TabIndex = 0;
            deleteOperationButton.Text = "Удалить";
            deleteOperationButton.UseVisualStyleBackColor = true;
            deleteOperationButton.Click += DeleteOperationButton_Click;
            // 
            // moveDownButton
            // 
            moveDownButton.AutoSize = true;
            moveDownButton.Location = new System.Drawing.Point(634, 3);
            moveDownButton.Name = "moveDownButton";
            moveDownButton.Size = new System.Drawing.Size(104, 30);
            moveDownButton.TabIndex = 1;
            moveDownButton.Text = "Вниз";
            moveDownButton.UseVisualStyleBackColor = true;
            moveDownButton.Click += MoveDownButton_Click;
            // 
            // moveUpButton
            // 
            moveUpButton.AutoSize = true;
            moveUpButton.Location = new System.Drawing.Point(524, 3);
            moveUpButton.Name = "moveUpButton";
            moveUpButton.Size = new System.Drawing.Size(104, 30);
            moveUpButton.TabIndex = 2;
            moveUpButton.Text = "Вверх";
            moveUpButton.UseVisualStyleBackColor = true;
            moveUpButton.Click += MoveUpButton_Click;
            // 
            // codeLayoutPanel
            // 
            codeLayoutPanel.ColumnCount = 2;
            codeLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            codeLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            codeLayoutPanel.Controls.Add(nodeCodeLabel, 0, 0);
            codeLayoutPanel.Controls.Add(nodeCodeValueLabel, 1, 0);
            codeLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            codeLayoutPanel.Location = new System.Drawing.Point(3, 342);
            codeLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            codeLayoutPanel.Name = "codeLayoutPanel";
            codeLayoutPanel.RowCount = 1;
            codeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            codeLayoutPanel.Size = new System.Drawing.Size(848, 32);
            codeLayoutPanel.TabIndex = 3;
            // 
            // nodeCodeLabel
            // 
            nodeCodeLabel.AutoSize = true;
            nodeCodeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeCodeLabel.Location = new System.Drawing.Point(3, 0);
            nodeCodeLabel.Name = "nodeCodeLabel";
            nodeCodeLabel.Size = new System.Drawing.Size(164, 32);
            nodeCodeLabel.TabIndex = 0;
            nodeCodeLabel.Text = "Код узла";
            nodeCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nodeCodeValueLabel
            // 
            nodeCodeValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nodeCodeValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeCodeValueLabel.Location = new System.Drawing.Point(173, 0);
            nodeCodeValueLabel.Name = "nodeCodeValueLabel";
            nodeCodeValueLabel.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            nodeCodeValueLabel.Size = new System.Drawing.Size(672, 32);
            nodeCodeValueLabel.TabIndex = 1;
            nodeCodeValueLabel.Text = "-";
            nodeCodeValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nameLabel.Location = new System.Drawing.Point(3, 386);
            nameLabel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(848, 20);
            nameLabel.TabIndex = 4;
            nameLabel.Text = "Название узла";
            // 
            // nameTextBox
            // 
            nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            nameTextBox.Location = new System.Drawing.Point(3, 409);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new System.Drawing.Size(848, 27);
            nameTextBox.TabIndex = 5;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            descriptionLabel.Location = new System.Drawing.Point(3, 448);
            descriptionLabel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(848, 20);
            descriptionLabel.TabIndex = 6;
            descriptionLabel.Text = "Описание";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            descriptionTextBox.Location = new System.Drawing.Point(3, 471);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            descriptionTextBox.Size = new System.Drawing.Size(848, 90);
            descriptionTextBox.TabIndex = 7;
            // 
            // metadataLayoutPanel
            // 
            metadataLayoutPanel.ColumnCount = 3;
            metadataLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            metadataLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            metadataLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            metadataLayoutPanel.Controls.Add(nodeGroupPanel, 0, 0);
            metadataLayoutPanel.Controls.Add(productKindPanel, 1, 0);
            metadataLayoutPanel.Controls.Add(productCategoryPanel, 2, 0);
            metadataLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            metadataLayoutPanel.Location = new System.Drawing.Point(3, 573);
            metadataLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            metadataLayoutPanel.Name = "metadataLayoutPanel";
            metadataLayoutPanel.RowCount = 1;
            metadataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            metadataLayoutPanel.Size = new System.Drawing.Size(848, 63);
            metadataLayoutPanel.TabIndex = 8;
            // 
            // nodeGroupPanel
            // 
            nodeGroupPanel.ColumnCount = 1;
            nodeGroupPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            nodeGroupPanel.Controls.Add(nodeGroupLabel, 0, 0);
            nodeGroupPanel.Controls.Add(nodeGroupComboBox, 0, 1);
            nodeGroupPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeGroupPanel.Location = new System.Drawing.Point(0, 0);
            nodeGroupPanel.Margin = new System.Windows.Forms.Padding(0);
            nodeGroupPanel.Name = "nodeGroupPanel";
            nodeGroupPanel.RowCount = 2;
            nodeGroupPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            nodeGroupPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            nodeGroupPanel.Size = new System.Drawing.Size(282, 63);
            nodeGroupPanel.TabIndex = 0;
            // 
            // nodeGroupLabel
            // 
            nodeGroupLabel.AutoSize = true;
            nodeGroupLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeGroupLabel.Location = new System.Drawing.Point(3, 0);
            nodeGroupLabel.Name = "nodeGroupLabel";
            nodeGroupLabel.Size = new System.Drawing.Size(276, 20);
            nodeGroupLabel.TabIndex = 0;
            nodeGroupLabel.Text = "Группа узла";
            // 
            // nodeGroupComboBox
            // 
            nodeGroupComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            nodeGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            nodeGroupComboBox.FormattingEnabled = true;
            nodeGroupComboBox.Location = new System.Drawing.Point(3, 23);
            nodeGroupComboBox.Name = "nodeGroupComboBox";
            nodeGroupComboBox.Size = new System.Drawing.Size(276, 28);
            nodeGroupComboBox.TabIndex = 1;
            // 
            // productKindPanel
            // 
            productKindPanel.ColumnCount = 1;
            productKindPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            productKindPanel.Controls.Add(productKindLabel, 0, 0);
            productKindPanel.Controls.Add(productKindComboBox, 0, 1);
            productKindPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            productKindPanel.Location = new System.Drawing.Point(282, 0);
            productKindPanel.Margin = new System.Windows.Forms.Padding(0);
            productKindPanel.Name = "productKindPanel";
            productKindPanel.RowCount = 2;
            productKindPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productKindPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productKindPanel.Size = new System.Drawing.Size(282, 63);
            productKindPanel.TabIndex = 1;
            // 
            // productKindLabel
            // 
            productKindLabel.AutoSize = true;
            productKindLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            productKindLabel.Location = new System.Drawing.Point(3, 0);
            productKindLabel.Name = "productKindLabel";
            productKindLabel.Size = new System.Drawing.Size(276, 20);
            productKindLabel.TabIndex = 0;
            productKindLabel.Text = "Класс изделия";
            // 
            // productKindComboBox
            // 
            productKindComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            productKindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            productKindComboBox.FormattingEnabled = true;
            productKindComboBox.Location = new System.Drawing.Point(3, 23);
            productKindComboBox.Name = "productKindComboBox";
            productKindComboBox.Size = new System.Drawing.Size(276, 28);
            productKindComboBox.TabIndex = 1;
            // 
            // productCategoryPanel
            // 
            productCategoryPanel.ColumnCount = 1;
            productCategoryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            productCategoryPanel.Controls.Add(productCategoryLabel, 0, 0);
            productCategoryPanel.Controls.Add(productCategoryComboBox, 0, 1);
            productCategoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            productCategoryPanel.Location = new System.Drawing.Point(564, 0);
            productCategoryPanel.Margin = new System.Windows.Forms.Padding(0);
            productCategoryPanel.Name = "productCategoryPanel";
            productCategoryPanel.RowCount = 2;
            productCategoryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productCategoryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productCategoryPanel.Size = new System.Drawing.Size(284, 63);
            productCategoryPanel.TabIndex = 2;
            // 
            // productCategoryLabel
            // 
            productCategoryLabel.AutoSize = true;
            productCategoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            productCategoryLabel.Location = new System.Drawing.Point(3, 0);
            productCategoryLabel.Name = "productCategoryLabel";
            productCategoryLabel.Size = new System.Drawing.Size(278, 20);
            productCategoryLabel.TabIndex = 0;
            productCategoryLabel.Text = "Категория изделия";
            // 
            // productCategoryComboBox
            // 
            productCategoryComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            productCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            productCategoryComboBox.FormattingEnabled = true;
            productCategoryComboBox.Location = new System.Drawing.Point(3, 23);
            productCategoryComboBox.Name = "productCategoryComboBox";
            productCategoryComboBox.Size = new System.Drawing.Size(278, 28);
            productCategoryComboBox.TabIndex = 1;
            // 
            // buttonsPanel
            // 
            buttonsPanel.AutoSize = true;
            buttonsPanel.Controls.Add(closeButton);
            buttonsPanel.Controls.Add(saveButton);
            buttonsPanel.Controls.Add(deleteButton);
            buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            buttonsPanel.Location = new System.Drawing.Point(3, 648);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(848, 3);
            buttonsPanel.TabIndex = 9;
            // 
            // closeButton
            // 
            closeButton.AutoSize = true;
            closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            closeButton.Location = new System.Drawing.Point(760, 3);
            closeButton.Name = "closeButton";
            closeButton.Size = new System.Drawing.Size(85, 30);
            closeButton.TabIndex = 0;
            closeButton.Text = "Закрыть";
            closeButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            saveButton.AutoSize = true;
            saveButton.Location = new System.Drawing.Point(662, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(92, 30);
            saveButton.TabIndex = 1;
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.AutoSize = true;
            deleteButton.Location = new System.Drawing.Point(552, 3);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new System.Drawing.Size(104, 30);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Удалить узел";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // BaseNodeLibraryEditorForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new System.Drawing.Size(1164, 681);
            Controls.Add(mainLayoutPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MinimumSize = new System.Drawing.Size(980, 620);
            Name = "BaseNodeLibraryEditorForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Редактирование базовых узлов";
            mainLayoutPanel.ResumeLayout(false);
            leftLayoutPanel.ResumeLayout(false);
            leftLayoutPanel.PerformLayout();
            editorPanel.ResumeLayout(false);
            editorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            operationsButtonsPanel.ResumeLayout(false);
            operationsButtonsPanel.PerformLayout();
            codeLayoutPanel.ResumeLayout(false);
            codeLayoutPanel.PerformLayout();
            metadataLayoutPanel.ResumeLayout(false);
            nodeGroupPanel.ResumeLayout(false);
            nodeGroupPanel.PerformLayout();
            productKindPanel.ResumeLayout(false);
            productKindPanel.PerformLayout();
            productCategoryPanel.ResumeLayout(false);
            productCategoryPanel.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel leftLayoutPanel;
        private System.Windows.Forms.Label nodesLabel;
        private System.Windows.Forms.ListBox nodesListBox;
        private System.Windows.Forms.TableLayoutPanel editorPanel;
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.DataGridView previewGrid;
        private System.Windows.Forms.FlowLayoutPanel operationsButtonsPanel;
        private System.Windows.Forms.Button deleteOperationButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.TableLayoutPanel codeLayoutPanel;
        private System.Windows.Forms.Label nodeCodeLabel;
        private System.Windows.Forms.Label nodeCodeValueLabel;
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
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button deleteButton;
    }
}
