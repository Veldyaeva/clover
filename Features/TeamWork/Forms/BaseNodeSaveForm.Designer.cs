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
            components = new System.ComponentModel.Container();
            nameLabel = new System.Windows.Forms.Label();
            nameTextBox = new System.Windows.Forms.TextBox();
            descriptionTextBox = new System.Windows.Forms.TextBox();
            nodeGroupComboBox = new System.Windows.Forms.ComboBox();
            productKindComboBox = new System.Windows.Forms.ComboBox();
            productCategoryComboBox = new System.Windows.Forms.ComboBox();
            operationsButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            deleteOperationButton = new System.Windows.Forms.Button();
            moveDownButton = new System.Windows.Forms.Button();
            moveUpButton = new System.Windows.Forms.Button();
            previewGrid = new System.Windows.Forms.DataGridView();
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            previewPanel = new System.Windows.Forms.Panel();
            previewPictureBox = new System.Windows.Forms.PictureBox();
            previewImageStatusLabel = new System.Windows.Forms.Label();
            previewSourceLabel = new System.Windows.Forms.Label();
            previewTitleLabel = new System.Windows.Forms.Label();
            previewToolTip = new System.Windows.Forms.ToolTip(components);
            layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(components);
            BaseNodeSaveFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            previewPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            mainLayoutPanelitem = new DevExpress.XtraLayout.LayoutControlGroup();
            nameTextBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            descriptionTextBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            nameLabelitem = new DevExpress.XtraLayout.LayoutControlItem();
            metadataLayoutPanelitem = new DevExpress.XtraLayout.LayoutControlGroup();
            operationsButtonsPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            previewGriditem = new DevExpress.XtraLayout.LayoutControlItem();
            buttonsPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            nodeGroupComboBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            productKindComboBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            productCategoryComboBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            summaryLabel = new DevExpress.XtraLayout.SimpleLabelItem();
            operationsButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            buttonsPanel.SuspendLayout();
            previewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BaseNodeSaveFormlayoutControl1ConvertedLayout).BeginInit();
            BaseNodeSaveFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainLayoutPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nameTextBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)descriptionTextBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nameLabelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)metadataLayoutPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)operationsButtonsPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewGriditem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)buttonsPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nodeGroupComboBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productKindComboBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productCategoryComboBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)summaryLabel).BeginInit();
            SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.Location = new System.Drawing.Point(12, 12);
            nameLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(536, 20);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Название узла";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new System.Drawing.Point(12, 69);
            nameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Например: Обработка манжеты";
            nameTextBox.Size = new System.Drawing.Size(536, 20);
            nameTextBox.TabIndex = 2;
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Location = new System.Drawing.Point(12, 109);
            descriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            descriptionTextBox.Size = new System.Drawing.Size(536, 65);
            descriptionTextBox.TabIndex = 4;
            // 
            // nodeGroupComboBox
            // 
            nodeGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            nodeGroupComboBox.FormattingEnabled = true;
            nodeGroupComboBox.Location = new System.Drawing.Point(177, 194);
            nodeGroupComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            nodeGroupComboBox.Name = "nodeGroupComboBox";
            nodeGroupComboBox.Size = new System.Drawing.Size(148, 23);
            nodeGroupComboBox.TabIndex = 1;
            // 
            // productKindComboBox
            // 
            productKindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            productKindComboBox.FormattingEnabled = true;
            productKindComboBox.Location = new System.Drawing.Point(12, 194);
            productKindComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            productKindComboBox.Name = "productKindComboBox";
            productKindComboBox.Size = new System.Drawing.Size(161, 23);
            productKindComboBox.TabIndex = 1;
            // 
            // productCategoryComboBox
            // 
            productCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            productCategoryComboBox.FormattingEnabled = true;
            productCategoryComboBox.Location = new System.Drawing.Point(329, 194);
            productCategoryComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            productCategoryComboBox.Name = "productCategoryComboBox";
            productCategoryComboBox.Size = new System.Drawing.Size(219, 23);
            productCategoryComboBox.TabIndex = 1;
            // 
            // operationsButtonsPanel
            // 
            operationsButtonsPanel.Controls.Add(deleteOperationButton);
            operationsButtonsPanel.Controls.Add(moveDownButton);
            operationsButtonsPanel.Controls.Add(moveUpButton);
            operationsButtonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            operationsButtonsPanel.Location = new System.Drawing.Point(12, 219);
            operationsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            operationsButtonsPanel.Name = "operationsButtonsPanel";
            operationsButtonsPanel.Size = new System.Drawing.Size(536, 29);
            operationsButtonsPanel.TabIndex = 6;
            // 
            // deleteOperationButton
            // 
            deleteOperationButton.AutoSize = true;
            deleteOperationButton.Location = new System.Drawing.Point(445, 2);
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
            moveDownButton.Location = new System.Drawing.Point(348, 2);
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
            moveUpButton.Location = new System.Drawing.Point(251, 2);
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
            previewGrid.Location = new System.Drawing.Point(12, 252);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(536, 189);
            previewGrid.TabIndex = 7;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Controls.Add(okButton);
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            buttonsPanel.Location = new System.Drawing.Point(12, 445);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(536, 39);
            buttonsPanel.TabIndex = 8;
            // 
            // okButton
            // 
            okButton.AutoSize = true;
            okButton.Location = new System.Drawing.Point(450, 2);
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
            cancelButton.Location = new System.Drawing.Point(367, 2);
            cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(77, 25);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // previewPanel
            // 
            previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewPanel.Controls.Add(previewPictureBox);
            previewPanel.Controls.Add(previewImageStatusLabel);
            previewPanel.Controls.Add(previewSourceLabel);
            previewPanel.Controls.Add(previewTitleLabel);
            previewPanel.Location = new System.Drawing.Point(552, 12);
            previewPanel.Name = "previewPanel";
            previewPanel.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            previewPanel.Size = new System.Drawing.Size(297, 472);
            previewPanel.TabIndex = 1;
            // 
            // previewPictureBox
            // 
            previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            previewPictureBox.Location = new System.Drawing.Point(10, 68);
            previewPictureBox.Name = "previewPictureBox";
            previewPictureBox.Size = new System.Drawing.Size(275, 366);
            previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            previewPictureBox.TabIndex = 0;
            previewPictureBox.TabStop = false;
            // 
            // previewImageStatusLabel
            // 
            previewImageStatusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            previewImageStatusLabel.Location = new System.Drawing.Point(10, 434);
            previewImageStatusLabel.Name = "previewImageStatusLabel";
            previewImageStatusLabel.Size = new System.Drawing.Size(275, 24);
            previewImageStatusLabel.TabIndex = 1;
            previewImageStatusLabel.Text = "Изображение не задано";
            previewImageStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // previewSourceLabel
            // 
            previewSourceLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            previewSourceLabel.Dock = System.Windows.Forms.DockStyle.Top;
            previewSourceLabel.Location = new System.Drawing.Point(10, 32);
            previewSourceLabel.Name = "previewSourceLabel";
            previewSourceLabel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            previewSourceLabel.Size = new System.Drawing.Size(275, 36);
            previewSourceLabel.TabIndex = 2;
            previewSourceLabel.Text = "РТ: -";
            previewSourceLabel.Click += PreviewSourceLabel_Click;
            // 
            // previewTitleLabel
            // 
            previewTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            previewTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            previewTitleLabel.Location = new System.Drawing.Point(10, 12);
            previewTitleLabel.Name = "previewTitleLabel";
            previewTitleLabel.Size = new System.Drawing.Size(275, 20);
            previewTitleLabel.TabIndex = 3;
            previewTitleLabel.Text = "Источник базового узла";
            // 
            // BaseNodeSaveFormlayoutControl1ConvertedLayout
            // 
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(previewPanel);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(nameTextBox);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(descriptionTextBox);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(nameLabel);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(nodeGroupComboBox);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(productKindComboBox);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(productCategoryComboBox);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(operationsButtonsPanel);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(previewGrid);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Controls.Add(buttonsPanel);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Name = "BaseNodeSaveFormlayoutControl1ConvertedLayout";
            BaseNodeSaveFormlayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1121, 259, 650, 400);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Root = layoutControlGroup1;
            BaseNodeSaveFormlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(861, 496);
            BaseNodeSaveFormlayoutControl1ConvertedLayout.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { mainLayoutPanelitem, previewPanelitem });
            layoutControlGroup1.Name = "Root";
            layoutControlGroup1.Size = new System.Drawing.Size(861, 496);
            layoutControlGroup1.TextVisible = false;
            // 
            // previewPanelitem
            // 
            previewPanelitem.Control = previewPanel;
            previewPanelitem.Location = new System.Drawing.Point(540, 0);
            previewPanelitem.Name = "previewPanelitem";
            previewPanelitem.Size = new System.Drawing.Size(301, 476);
            previewPanelitem.TextVisible = false;
            // 
            // mainLayoutPanelitem
            // 
            mainLayoutPanelitem.GroupBordersVisible = false;
            mainLayoutPanelitem.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { nameTextBoxitem, descriptionTextBoxitem, nameLabelitem, metadataLayoutPanelitem, operationsButtonsPanelitem, previewGriditem, buttonsPanelitem, summaryLabel });
            mainLayoutPanelitem.Location = new System.Drawing.Point(0, 0);
            mainLayoutPanelitem.Name = "mainLayoutPanelitem";
            mainLayoutPanelitem.Size = new System.Drawing.Size(540, 476);
            mainLayoutPanelitem.Text = " ";
            // 
            // nameTextBoxitem
            // 
            nameTextBoxitem.Control = nameTextBox;
            nameTextBoxitem.Location = new System.Drawing.Point(0, 41);
            nameTextBoxitem.Name = "nameTextBoxitem";
            nameTextBoxitem.OptionsTableLayoutItem.RowIndex = 2;
            nameTextBoxitem.Size = new System.Drawing.Size(540, 40);
            nameTextBoxitem.Text = "summaryLabel";
            nameTextBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            nameTextBoxitem.TextSize = new System.Drawing.Size(99, 13);
            // 
            // descriptionTextBoxitem
            // 
            descriptionTextBoxitem.Control = descriptionTextBox;
            descriptionTextBoxitem.Location = new System.Drawing.Point(0, 81);
            descriptionTextBoxitem.Name = "descriptionTextBoxitem";
            descriptionTextBoxitem.OptionsTableLayoutItem.RowIndex = 4;
            descriptionTextBoxitem.Size = new System.Drawing.Size(540, 85);
            descriptionTextBoxitem.Text = "Описание";
            descriptionTextBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            descriptionTextBoxitem.TextSize = new System.Drawing.Size(99, 13);
            // 
            // nameLabelitem
            // 
            nameLabelitem.Control = nameLabel;
            nameLabelitem.Location = new System.Drawing.Point(0, 0);
            nameLabelitem.Name = "nameLabelitem";
            nameLabelitem.OptionsTableLayoutItem.RowIndex = 1;
            nameLabelitem.Size = new System.Drawing.Size(540, 24);
            nameLabelitem.TextVisible = false;
            // 
            // metadataLayoutPanelitem
            // 
            metadataLayoutPanelitem.GroupBordersVisible = false;
            metadataLayoutPanelitem.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { productKindComboBoxitem, nodeGroupComboBoxitem, productCategoryComboBoxitem });
            metadataLayoutPanelitem.Location = new System.Drawing.Point(0, 166);
            metadataLayoutPanelitem.Name = "metadataLayoutPanelitem";
            metadataLayoutPanelitem.OptionsTableLayoutItem.RowIndex = 5;
            metadataLayoutPanelitem.Size = new System.Drawing.Size(540, 41);
            metadataLayoutPanelitem.Text = " ";
            // 
            // operationsButtonsPanelitem
            // 
            operationsButtonsPanelitem.Control = operationsButtonsPanel;
            operationsButtonsPanelitem.Location = new System.Drawing.Point(0, 207);
            operationsButtonsPanelitem.Name = "operationsButtonsPanelitem";
            operationsButtonsPanelitem.OptionsTableLayoutItem.RowIndex = 6;
            operationsButtonsPanelitem.Size = new System.Drawing.Size(540, 33);
            operationsButtonsPanelitem.TextVisible = false;
            // 
            // previewGriditem
            // 
            previewGriditem.Control = previewGrid;
            previewGriditem.Location = new System.Drawing.Point(0, 240);
            previewGriditem.Name = "previewGriditem";
            previewGriditem.OptionsTableLayoutItem.RowIndex = 7;
            previewGriditem.Size = new System.Drawing.Size(540, 193);
            previewGriditem.TextVisible = false;
            // 
            // buttonsPanelitem
            // 
            buttonsPanelitem.Control = buttonsPanel;
            buttonsPanelitem.Location = new System.Drawing.Point(0, 433);
            buttonsPanelitem.Name = "buttonsPanelitem";
            buttonsPanelitem.OptionsTableLayoutItem.RowIndex = 8;
            buttonsPanelitem.Size = new System.Drawing.Size(540, 43);
            buttonsPanelitem.TextVisible = false;
            // 
            // nodeGroupComboBoxitem
            // 
            nodeGroupComboBoxitem.Control = nodeGroupComboBox;
            nodeGroupComboBoxitem.Location = new System.Drawing.Point(165, 0);
            nodeGroupComboBoxitem.Name = "nodeGroupComboBoxitem";
            nodeGroupComboBoxitem.OptionsTableLayoutItem.RowIndex = 1;
            nodeGroupComboBoxitem.Size = new System.Drawing.Size(152, 41);
            nodeGroupComboBoxitem.Text = "Группа узла";
            nodeGroupComboBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            nodeGroupComboBoxitem.TextSize = new System.Drawing.Size(99, 13);
            // 
            // productKindComboBoxitem
            // 
            productKindComboBoxitem.Control = productKindComboBox;
            productKindComboBoxitem.Location = new System.Drawing.Point(0, 0);
            productKindComboBoxitem.Name = "productKindComboBoxitem";
            productKindComboBoxitem.Size = new System.Drawing.Size(165, 41);
            productKindComboBoxitem.Text = "Тип узла";
            productKindComboBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            productKindComboBoxitem.TextSize = new System.Drawing.Size(99, 13);
            // 
            // productCategoryComboBoxitem
            // 
            productCategoryComboBoxitem.Control = productCategoryComboBox;
            productCategoryComboBoxitem.Location = new System.Drawing.Point(317, 0);
            productCategoryComboBoxitem.Name = "productCategoryComboBoxitem";
            productCategoryComboBoxitem.OptionsTableLayoutItem.RowIndex = 1;
            productCategoryComboBoxitem.Size = new System.Drawing.Size(223, 41);
            productCategoryComboBoxitem.Text = "Категория изделия";
            productCategoryComboBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            productCategoryComboBoxitem.TextSize = new System.Drawing.Size(99, 13);
            // 
            // summaryLabel
            // 
            summaryLabel.Location = new System.Drawing.Point(0, 24);
            summaryLabel.Name = "summaryLabel";
            summaryLabel.Size = new System.Drawing.Size(540, 17);
            summaryLabel.Text = " ";
            summaryLabel.TextSize = new System.Drawing.Size(99, 13);
            // 
            // BaseNodeSaveForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(861, 496);
            Controls.Add(BaseNodeSaveFormlayoutControl1ConvertedLayout);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            MinimumSize = new System.Drawing.Size(790, 475);
            Name = "BaseNodeSaveForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Сохранить как базовый узел";
            operationsButtonsPanel.ResumeLayout(false);
            operationsButtonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            previewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)BaseNodeSaveFormlayoutControl1ConvertedLayout).EndInit();
            BaseNodeSaveFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainLayoutPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nameTextBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)descriptionTextBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nameLabelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)metadataLayoutPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)operationsButtonsPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewGriditem).EndInit();
            ((System.ComponentModel.ISupportInitialize)buttonsPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nodeGroupComboBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)productKindComboBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)productCategoryComboBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)summaryLabel).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ComboBox nodeGroupComboBox;
        private System.Windows.Forms.ComboBox productKindComboBox;
        private System.Windows.Forms.ComboBox productCategoryComboBox;
        private System.Windows.Forms.FlowLayoutPanel operationsButtonsPanel;
        private System.Windows.Forms.Button deleteOperationButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.DataGridView previewGrid;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Label previewImageStatusLabel;
        private System.Windows.Forms.Label previewSourceLabel;
        private System.Windows.Forms.Label previewTitleLabel;
        private System.Windows.Forms.ToolTip previewToolTip;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraLayout.LayoutControl BaseNodeSaveFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem previewPanelitem;
        private DevExpress.XtraLayout.LayoutControlGroup mainLayoutPanelitem;
        private DevExpress.XtraLayout.LayoutControlItem nameTextBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem descriptionTextBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem nameLabelitem;
        private DevExpress.XtraLayout.LayoutControlGroup metadataLayoutPanelitem;
        private DevExpress.XtraLayout.LayoutControlItem nodeGroupComboBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem productCategoryComboBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem operationsButtonsPanelitem;
        private DevExpress.XtraLayout.LayoutControlItem previewGriditem;
        private DevExpress.XtraLayout.LayoutControlItem buttonsPanelitem;
        private DevExpress.XtraLayout.LayoutControlItem productKindComboBoxitem;
        private DevExpress.XtraLayout.SimpleLabelItem summaryLabel;
    }
}
