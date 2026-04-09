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
            previewPanel = new System.Windows.Forms.Panel();
            previewPictureBox = new System.Windows.Forms.PictureBox();
            previewImageStatusLabel = new System.Windows.Forms.Label();
            previewSourceLabel = new System.Windows.Forms.Label();
            previewTitleLabel = new System.Windows.Forms.Label();
            searchTextBox = new System.Windows.Forms.TextBox();
            nodeTypeFilterComboBox = new System.Windows.Forms.ComboBox();
            productCategoryFilterComboBox = new System.Windows.Forms.ComboBox();
            nodeGroupFilterComboBox = new System.Windows.Forms.ComboBox();
            nodesListBox = new System.Windows.Forms.ListBox();
            positionComboBox = new System.Windows.Forms.ComboBox();
            detailsLabel = new System.Windows.Forms.Label();
            previewGrid = new System.Windows.Forms.DataGridView();
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            editNodesButton = new System.Windows.Forms.Button();
            previewToolTip = new System.Windows.Forms.ToolTip(components);
            layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(components);
            BaseNodeInsertFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            leftLayoutPanelitem = new DevExpress.XtraLayout.LayoutControlGroup();
            searchTextBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            nodesListBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            nodeTypeFilterComboBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            productCategoryFilterComboBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            nodeGroupFilterComboBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            previewGriditem = new DevExpress.XtraLayout.LayoutControlItem();
            buttonsPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            previewPanelitem = new DevExpress.XtraLayout.LayoutControlItem();
            rightTopLayoutPanelitem = new DevExpress.XtraLayout.LayoutControlGroup();
            positionComboBoxitem = new DevExpress.XtraLayout.LayoutControlItem();
            detailsLabelitem = new DevExpress.XtraLayout.LayoutControlItem();
            previewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BaseNodeInsertFormlayoutControl1ConvertedLayout).BeginInit();
            BaseNodeInsertFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)leftLayoutPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)searchTextBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nodesListBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nodeTypeFilterComboBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productCategoryFilterComboBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nodeGroupFilterComboBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewGriditem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)buttonsPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rightTopLayoutPanelitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)positionComboBoxitem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)detailsLabelitem).BeginInit();
            SuspendLayout();
            // 
            // previewPanel
            // 
            previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewPanel.Controls.Add(previewPictureBox);
            previewPanel.Controls.Add(previewImageStatusLabel);
            previewPanel.Controls.Add(previewSourceLabel);
            previewPanel.Controls.Add(previewTitleLabel);
            previewPanel.Location = new System.Drawing.Point(992, 12);
            previewPanel.Name = "previewPanel";
            previewPanel.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            previewPanel.Size = new System.Drawing.Size(314, 432);
            previewPanel.TabIndex = 1;
            // 
            // previewPictureBox
            // 
            previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            previewPictureBox.Location = new System.Drawing.Point(10, 68);
            previewPictureBox.Name = "previewPictureBox";
            previewPictureBox.Size = new System.Drawing.Size(292, 326);
            previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            previewPictureBox.TabIndex = 0;
            previewPictureBox.TabStop = false;
            // 
            // previewImageStatusLabel
            // 
            previewImageStatusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            previewImageStatusLabel.Location = new System.Drawing.Point(10, 394);
            previewImageStatusLabel.Name = "previewImageStatusLabel";
            previewImageStatusLabel.Size = new System.Drawing.Size(292, 24);
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
            previewSourceLabel.Size = new System.Drawing.Size(292, 36);
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
            previewTitleLabel.Size = new System.Drawing.Size(292, 20);
            previewTitleLabel.TabIndex = 3;
            previewTitleLabel.Text = "Источник базового узла";
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new System.Drawing.Point(12, 28);
            searchTextBox.Margin = new System.Windows.Forms.Padding(0, 6, 10, 0);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Поиск по тегам и RT-коду";
            searchTextBox.Size = new System.Drawing.Size(430, 20);
            searchTextBox.TabIndex = 0;
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            // 
            // nodeTypeFilterComboBox
            // 
            nodeTypeFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            nodeTypeFilterComboBox.FormattingEnabled = true;
            nodeTypeFilterComboBox.Location = new System.Drawing.Point(12, 68);
            nodeTypeFilterComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            nodeTypeFilterComboBox.Name = "nodeTypeFilterComboBox";
            nodeTypeFilterComboBox.Size = new System.Drawing.Size(118, 23);
            nodeTypeFilterComboBox.TabIndex = 2;
            nodeTypeFilterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;
            // 
            // productCategoryFilterComboBox
            // 
            productCategoryFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            productCategoryFilterComboBox.FormattingEnabled = true;
            productCategoryFilterComboBox.Location = new System.Drawing.Point(134, 68);
            productCategoryFilterComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            productCategoryFilterComboBox.Name = "productCategoryFilterComboBox";
            productCategoryFilterComboBox.Size = new System.Drawing.Size(145, 23);
            productCategoryFilterComboBox.TabIndex = 3;
            productCategoryFilterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;
            // 
            // nodeGroupFilterComboBox
            // 
            nodeGroupFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            nodeGroupFilterComboBox.FormattingEnabled = true;
            nodeGroupFilterComboBox.Location = new System.Drawing.Point(283, 68);
            nodeGroupFilterComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            nodeGroupFilterComboBox.Name = "nodeGroupFilterComboBox";
            nodeGroupFilterComboBox.Size = new System.Drawing.Size(159, 23);
            nodeGroupFilterComboBox.TabIndex = 4;
            nodeGroupFilterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;
            // 
            // nodesListBox
            // 
            nodesListBox.DisplayMember = "DisplayName";
            nodesListBox.FormattingEnabled = true;
            nodesListBox.HorizontalScrollbar = true;
            nodesListBox.ItemHeight = 15;
            nodesListBox.Location = new System.Drawing.Point(12, 109);
            nodesListBox.Margin = new System.Windows.Forms.Padding(0, 6, 10, 0);
            nodesListBox.Name = "nodesListBox";
            nodesListBox.ScrollAlwaysVisible = true;
            nodesListBox.Size = new System.Drawing.Size(430, 364);
            nodesListBox.TabIndex = 5;
            nodesListBox.SelectedIndexChanged += NodesListBox_SelectedIndexChanged;
            // 
            // positionComboBox
            // 
            positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            positionComboBox.FormattingEnabled = true;
            positionComboBox.Location = new System.Drawing.Point(446, 28);
            positionComboBox.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            positionComboBox.Name = "positionComboBox";
            positionComboBox.Size = new System.Drawing.Size(542, 23);
            positionComboBox.TabIndex = 6;
            // 
            // detailsLabel
            // 
            detailsLabel.Location = new System.Drawing.Point(446, 53);
            detailsLabel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            detailsLabel.Name = "detailsLabel";
            detailsLabel.Size = new System.Drawing.Size(542, 62);
            detailsLabel.TabIndex = 1;
            detailsLabel.Text = "detailsLabel";
            // 
            // previewGrid
            // 
            previewGrid.AllowUserToAddRows = false;
            previewGrid.AllowUserToDeleteRows = false;
            previewGrid.AllowUserToResizeRows = false;
            previewGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            previewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            previewGrid.Location = new System.Drawing.Point(446, 119);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(542, 325);
            previewGrid.TabIndex = 8;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Controls.Add(okButton);
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Controls.Add(editNodesButton);
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            buttonsPanel.Location = new System.Drawing.Point(446, 448);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(860, 35);
            buttonsPanel.TabIndex = 7;
            // 
            // okButton
            // 
            okButton.AutoSize = true;
            okButton.Location = new System.Drawing.Point(774, 2);
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
            cancelButton.Location = new System.Drawing.Point(691, 2);
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
            editNodesButton.Location = new System.Drawing.Point(549, 2);
            editNodesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            editNodesButton.Name = "editNodesButton";
            editNodesButton.Size = new System.Drawing.Size(136, 25);
            editNodesButton.TabIndex = 2;
            editNodesButton.Text = "Редактировать узлы";
            editNodesButton.UseVisualStyleBackColor = true;
            editNodesButton.Click += EditNodesButton_Click;
            // 
            // BaseNodeInsertFormlayoutControl1ConvertedLayout
            // 
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(previewPanel);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(searchTextBox);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(nodeTypeFilterComboBox);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(productCategoryFilterComboBox);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(nodeGroupFilterComboBox);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(nodesListBox);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(positionComboBox);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(detailsLabel);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(previewGrid);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Controls.Add(buttonsPanel);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Name = "BaseNodeInsertFormlayoutControl1ConvertedLayout";
            BaseNodeInsertFormlayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(261, 257, 650, 400);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Root = layoutControlGroup1;
            BaseNodeInsertFormlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(1318, 495);
            BaseNodeInsertFormlayoutControl1ConvertedLayout.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { leftLayoutPanelitem, previewGriditem, buttonsPanelitem, previewPanelitem, rightTopLayoutPanelitem });
            layoutControlGroup1.Name = "Root";
            layoutControlGroup1.Size = new System.Drawing.Size(1318, 495);
            layoutControlGroup1.TextVisible = false;
            // 
            // leftLayoutPanelitem
            // 
            leftLayoutPanelitem.CustomizationFormText = " поиск";
            leftLayoutPanelitem.GroupBordersVisible = false;
            leftLayoutPanelitem.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { searchTextBoxitem, nodesListBoxitem, nodeTypeFilterComboBoxitem, productCategoryFilterComboBoxitem, nodeGroupFilterComboBoxitem });
            leftLayoutPanelitem.Location = new System.Drawing.Point(0, 0);
            leftLayoutPanelitem.Name = "leftLayoutPanelitem";
            leftLayoutPanelitem.OptionsTableLayoutItem.RowSpan = 3;
            leftLayoutPanelitem.Size = new System.Drawing.Size(434, 475);
            leftLayoutPanelitem.Text = " ";
            // 
            // searchTextBoxitem
            // 
            searchTextBoxitem.Control = searchTextBox;
            searchTextBoxitem.Location = new System.Drawing.Point(0, 0);
            searchTextBoxitem.Name = "searchTextBoxitem";
            searchTextBoxitem.OptionsTableLayoutItem.RowIndex = 2;
            searchTextBoxitem.Size = new System.Drawing.Size(434, 40);
            searchTextBoxitem.Text = "Поиск";
            searchTextBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            searchTextBoxitem.TextSize = new System.Drawing.Size(102, 13);
            // 
            // nodesListBoxitem
            // 
            nodesListBoxitem.Control = nodesListBox;
            nodesListBoxitem.Location = new System.Drawing.Point(0, 81);
            nodesListBoxitem.Name = "nodesListBoxitem";
            nodesListBoxitem.OptionsTableLayoutItem.RowIndex = 4;
            nodesListBoxitem.Size = new System.Drawing.Size(434, 394);
            nodesListBoxitem.Text = "Базовые узлы";
            nodesListBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            nodesListBoxitem.TextSize = new System.Drawing.Size(102, 13);
            // 
            // productKindFilterComboBoxitem
            // 
            nodeTypeFilterComboBoxitem.Control = nodeTypeFilterComboBox;
            nodeTypeFilterComboBoxitem.Location = new System.Drawing.Point(0, 40);
            nodeTypeFilterComboBoxitem.Name = "productKindFilterComboBoxitem";
            nodeTypeFilterComboBoxitem.Size = new System.Drawing.Size(122, 41);
            nodeTypeFilterComboBoxitem.Text = "Тип узла";
            nodeTypeFilterComboBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            nodeTypeFilterComboBoxitem.TextSize = new System.Drawing.Size(102, 13);
            // 
            // productCategoryFilterComboBoxitem
            // 
            productCategoryFilterComboBoxitem.Control = productCategoryFilterComboBox;
            productCategoryFilterComboBoxitem.Location = new System.Drawing.Point(122, 40);
            productCategoryFilterComboBoxitem.Name = "productCategoryFilterComboBoxitem";
            productCategoryFilterComboBoxitem.OptionsTableLayoutItem.RowIndex = 3;
            productCategoryFilterComboBoxitem.Size = new System.Drawing.Size(149, 41);
            productCategoryFilterComboBoxitem.Text = "Категория изделия";
            productCategoryFilterComboBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            productCategoryFilterComboBoxitem.TextSize = new System.Drawing.Size(102, 13);
            // 
            // nodeGroupFilterComboBoxitem
            // 
            nodeGroupFilterComboBoxitem.Control = nodeGroupFilterComboBox;
            nodeGroupFilterComboBoxitem.Location = new System.Drawing.Point(271, 40);
            nodeGroupFilterComboBoxitem.Name = "nodeGroupFilterComboBoxitem";
            nodeGroupFilterComboBoxitem.OptionsTableLayoutItem.RowIndex = 5;
            nodeGroupFilterComboBoxitem.Size = new System.Drawing.Size(163, 41);
            nodeGroupFilterComboBoxitem.Text = "Группа узла";
            nodeGroupFilterComboBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            nodeGroupFilterComboBoxitem.TextSize = new System.Drawing.Size(102, 13);
            // 
            // previewGriditem
            // 
            previewGriditem.Control = previewGrid;
            previewGriditem.Location = new System.Drawing.Point(434, 107);
            previewGriditem.Name = "previewGriditem";
            previewGriditem.OptionsTableLayoutItem.ColumnIndex = 1;
            previewGriditem.OptionsTableLayoutItem.RowIndex = 1;
            previewGriditem.Size = new System.Drawing.Size(546, 329);
            previewGriditem.TextVisible = false;
            // 
            // buttonsPanelitem
            // 
            buttonsPanelitem.Control = buttonsPanel;
            buttonsPanelitem.Location = new System.Drawing.Point(434, 436);
            buttonsPanelitem.Name = "buttonsPanelitem";
            buttonsPanelitem.OptionsTableLayoutItem.ColumnIndex = 1;
            buttonsPanelitem.OptionsTableLayoutItem.RowIndex = 2;
            buttonsPanelitem.Size = new System.Drawing.Size(864, 39);
            buttonsPanelitem.TextVisible = false;
            // 
            // previewPanelitem
            // 
            previewPanelitem.Control = previewPanel;
            previewPanelitem.Location = new System.Drawing.Point(980, 0);
            previewPanelitem.Name = "previewPanelitem";
            previewPanelitem.OptionsTableLayoutItem.ColumnIndex = 2;
            previewPanelitem.Size = new System.Drawing.Size(318, 436);
            previewPanelitem.TextVisible = false;
            // 
            // rightTopLayoutPanelitem
            // 
            rightTopLayoutPanelitem.GroupBordersVisible = false;
            rightTopLayoutPanelitem.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { positionComboBoxitem, detailsLabelitem });
            rightTopLayoutPanelitem.Location = new System.Drawing.Point(434, 0);
            rightTopLayoutPanelitem.Name = "rightTopLayoutPanelitem";
            rightTopLayoutPanelitem.OptionsTableLayoutItem.ColumnIndex = 1;
            rightTopLayoutPanelitem.Size = new System.Drawing.Size(546, 107);
            rightTopLayoutPanelitem.Text = " ";
            // 
            // positionComboBoxitem
            // 
            positionComboBoxitem.Control = positionComboBox;
            positionComboBoxitem.Location = new System.Drawing.Point(0, 0);
            positionComboBoxitem.Name = "positionComboBoxitem";
            positionComboBoxitem.OptionsTableLayoutItem.RowIndex = 1;
            positionComboBoxitem.Size = new System.Drawing.Size(546, 41);
            positionComboBoxitem.Text = "Куда вставить узел";
            positionComboBoxitem.TextLocation = DevExpress.Utils.Locations.Top;
            positionComboBoxitem.TextSize = new System.Drawing.Size(102, 13);
            // 
            // detailsLabelitem
            // 
            detailsLabelitem.Control = detailsLabel;
            detailsLabelitem.Location = new System.Drawing.Point(0, 41);
            detailsLabelitem.Name = "detailsLabelitem";
            detailsLabelitem.OptionsTableLayoutItem.RowIndex = 2;
            detailsLabelitem.Size = new System.Drawing.Size(546, 66);
            detailsLabelitem.TextVisible = false;
            // 
            // BaseNodeInsertForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(1318, 495);
            Controls.Add(BaseNodeInsertFormlayoutControl1ConvertedLayout);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            MinimumSize = new System.Drawing.Size(807, 430);
            Name = "BaseNodeInsertForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Добавить базовый узел";
            previewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BaseNodeInsertFormlayoutControl1ConvertedLayout).EndInit();
            BaseNodeInsertFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)leftLayoutPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)searchTextBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nodesListBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nodeTypeFilterComboBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)productCategoryFilterComboBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nodeGroupFilterComboBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewGriditem).EndInit();
            ((System.ComponentModel.ISupportInitialize)buttonsPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)rightTopLayoutPanelitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)positionComboBoxitem).EndInit();
            ((System.ComponentModel.ISupportInitialize)detailsLabelitem).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ComboBox nodeTypeFilterComboBox;
        private System.Windows.Forms.ComboBox productCategoryFilterComboBox;
        private System.Windows.Forms.ComboBox nodeGroupFilterComboBox;
        private System.Windows.Forms.ListBox nodesListBox;
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
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraLayout.LayoutControl BaseNodeInsertFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem previewPanelitem;
        private DevExpress.XtraLayout.LayoutControlGroup leftLayoutPanelitem;
        private DevExpress.XtraLayout.LayoutControlItem searchTextBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem productCategoryFilterComboBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem nodeGroupFilterComboBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem nodesListBoxitem;
        private DevExpress.XtraLayout.LayoutControlGroup rightTopLayoutPanelitem;
        private DevExpress.XtraLayout.LayoutControlItem positionComboBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem detailsLabelitem;
        private DevExpress.XtraLayout.LayoutControlItem nodeTypeFilterComboBoxitem;
        private DevExpress.XtraLayout.LayoutControlItem previewGriditem;
        private DevExpress.XtraLayout.LayoutControlItem buttonsPanelitem;
    }
}
