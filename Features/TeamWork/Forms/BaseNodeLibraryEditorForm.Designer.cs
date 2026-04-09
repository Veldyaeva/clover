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
            searchLabel = new System.Windows.Forms.Label();
            searchTextBox = new System.Windows.Forms.TextBox();
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
            previewPanel = new System.Windows.Forms.Panel();
            previewPictureBox = new System.Windows.Forms.PictureBox();
            previewImageStatusLabel = new System.Windows.Forms.Label();
            previewSourceLabel = new System.Windows.Forms.Label();
            previewTitleLabel = new System.Windows.Forms.Label();
            previewToolTip = new System.Windows.Forms.ToolTip(components);
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
            previewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).BeginInit();
            SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 3;
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 245F));
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            mainLayoutPanel.Controls.Add(previewPanel, 2, 0);
            mainLayoutPanel.Controls.Add(leftLayoutPanel, 0, 0);
            mainLayoutPanel.Controls.Add(editorPanel, 1, 0);
            mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            mainLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            mainLayoutPanel.RowCount = 1;
            mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayoutPanel.Size = new System.Drawing.Size(1229, 517);
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
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            leftLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            leftLayoutPanel.Size = new System.Drawing.Size(239, 495);
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
            searchTextBox.PlaceholderText = "Поиск по тегам, RT-коду, названию";
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
            nodesListBox.Size = new System.Drawing.Size(229, 430);
            nodesListBox.TabIndex = 3;
            nodesListBox.SelectedIndexChanged += NodesListBox_SelectedIndexChanged;
            // 
            // editorPanel
            // 
            editorPanel.ColumnCount = 1;
            editorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            editorPanel.Controls.Add(detailsLabel, 0, 0);
            editorPanel.Controls.Add(previewGrid, 0, 3);
            editorPanel.Controls.Add(operationsButtonsPanel, 0, 4);
            editorPanel.Controls.Add(codeLayoutPanel, 0, 3);
            editorPanel.Controls.Add(nameLabel, 0, 1);
            editorPanel.Controls.Add(nameTextBox, 0, 2);
            editorPanel.Controls.Add(descriptionLabel, 0, 7);
            editorPanel.Controls.Add(descriptionTextBox, 0, 8);
            editorPanel.Controls.Add(metadataLayoutPanel, 0, 2);
            editorPanel.Controls.Add(buttonsPanel, 0, 9);
            editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            editorPanel.Location = new System.Drawing.Point(258, 11);
            editorPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            editorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            editorPanel.Size = new System.Drawing.Size(813, 495);
            editorPanel.TabIndex = 1;
            // 
            // detailsLabel
            // 
            detailsLabel.AutoSize = true;
            detailsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            detailsLabel.Location = new System.Drawing.Point(3, 0);
            detailsLabel.Name = "detailsLabel";
            detailsLabel.Size = new System.Drawing.Size(807, 15);
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
            previewGrid.Location = new System.Drawing.Point(3, 141);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(807, 182);
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
            operationsButtonsPanel.Location = new System.Drawing.Point(3, 332);
            operationsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            operationsButtonsPanel.Name = "operationsButtonsPanel";
            operationsButtonsPanel.Size = new System.Drawing.Size(807, 29);
            operationsButtonsPanel.TabIndex = 2;
            // 
            // deleteOperationButton
            // 
            deleteOperationButton.AutoSize = true;
            deleteOperationButton.Location = new System.Drawing.Point(716, 2);
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
            moveDownButton.Location = new System.Drawing.Point(619, 2);
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
            moveUpButton.Location = new System.Drawing.Point(522, 2);
            moveUpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            moveUpButton.Name = "moveUpButton";
            moveUpButton.Size = new System.Drawing.Size(91, 25);
            moveUpButton.TabIndex = 2;
            moveUpButton.Text = "Вверх";
            moveUpButton.UseVisualStyleBackColor = true;
            moveUpButton.Click += MoveUpButton_Click;
            // 
            // codeLayoutPanel
            // 
            codeLayoutPanel.ColumnCount = 2;
            codeLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            codeLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            codeLayoutPanel.Controls.Add(nodeCodeLabel, 0, 0);
            codeLayoutPanel.Controls.Add(nodeCodeValueLabel, 1, 0);
            codeLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            codeLayoutPanel.Location = new System.Drawing.Point(3, 115);
            codeLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            codeLayoutPanel.Name = "codeLayoutPanel";
            codeLayoutPanel.RowCount = 1;
            codeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            codeLayoutPanel.Size = new System.Drawing.Size(807, 24);
            codeLayoutPanel.TabIndex = 3;
            // 
            // nodeCodeLabel
            // 
            nodeCodeLabel.AutoSize = true;
            nodeCodeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeCodeLabel.Location = new System.Drawing.Point(3, 0);
            nodeCodeLabel.Name = "nodeCodeLabel";
            nodeCodeLabel.Size = new System.Drawing.Size(143, 24);
            nodeCodeLabel.TabIndex = 0;
            nodeCodeLabel.Text = "Код узла";
            nodeCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            nodeCodeLabel.Visible = false;
            // 
            // nodeCodeValueLabel
            // 
            nodeCodeValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nodeCodeValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeCodeValueLabel.Location = new System.Drawing.Point(152, 0);
            nodeCodeValueLabel.Name = "nodeCodeValueLabel";
            nodeCodeValueLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            nodeCodeValueLabel.Size = new System.Drawing.Size(652, 24);
            nodeCodeValueLabel.TabIndex = 1;
            nodeCodeValueLabel.Text = "-";
            nodeCodeValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            nodeCodeValueLabel.Visible = false;
            nodeCodeValueLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            nodeCodeValueLabel.Click += NodeCodeValueLabel_Click;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nameLabel.Location = new System.Drawing.Point(3, 24);
            nameLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(807, 2);
            nameLabel.TabIndex = 4;
            nameLabel.Text = "Название узла";
            // 
            // nameTextBox
            // 
            nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            nameTextBox.Location = new System.Drawing.Point(3, 84);
            nameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new System.Drawing.Size(807, 23);
            nameTextBox.TabIndex = 5;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            descriptionLabel.Location = new System.Drawing.Point(3, 370);
            descriptionLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(807, 15);
            descriptionLabel.TabIndex = 6;
            descriptionLabel.Text = "Описание";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            descriptionTextBox.Location = new System.Drawing.Point(3, 387);
            descriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            descriptionTextBox.Size = new System.Drawing.Size(807, 68);
            descriptionTextBox.TabIndex = 7;
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
            metadataLayoutPanel.Location = new System.Drawing.Point(3, 35);
            metadataLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            metadataLayoutPanel.Name = "metadataLayoutPanel";
            metadataLayoutPanel.RowCount = 1;
            metadataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            metadataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            metadataLayoutPanel.Size = new System.Drawing.Size(807, 47);
            metadataLayoutPanel.TabIndex = 8;
            // 
            // nodeGroupPanel
            // 
            nodeGroupPanel.ColumnCount = 1;
            nodeGroupPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            nodeGroupPanel.Controls.Add(nodeGroupLabel, 0, 0);
            nodeGroupPanel.Controls.Add(nodeGroupComboBox, 0, 1);
            nodeGroupPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeGroupPanel.Location = new System.Drawing.Point(268, 0);
            nodeGroupPanel.Margin = new System.Windows.Forms.Padding(0);
            nodeGroupPanel.Name = "nodeGroupPanel";
            nodeGroupPanel.RowCount = 2;
            nodeGroupPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            nodeGroupPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            nodeGroupPanel.Size = new System.Drawing.Size(268, 47);
            nodeGroupPanel.TabIndex = 0;
            // 
            // nodeGroupLabel
            // 
            nodeGroupLabel.AutoSize = true;
            nodeGroupLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nodeGroupLabel.Location = new System.Drawing.Point(3, 0);
            nodeGroupLabel.Name = "nodeGroupLabel";
            nodeGroupLabel.Size = new System.Drawing.Size(262, 15);
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
            nodeGroupComboBox.Size = new System.Drawing.Size(262, 23);
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
            productKindPanel.Size = new System.Drawing.Size(268, 47);
            productKindPanel.TabIndex = 1;
            // 
            // productKindLabel
            // 
            productKindLabel.AutoSize = true;
            productKindLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            productKindLabel.Location = new System.Drawing.Point(3, 0);
            productKindLabel.Name = "productKindLabel";
            productKindLabel.Size = new System.Drawing.Size(262, 15);
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
            productKindComboBox.Size = new System.Drawing.Size(262, 23);
            productKindComboBox.TabIndex = 1;
            // 
            // productCategoryPanel
            // 
            productCategoryPanel.ColumnCount = 1;
            productCategoryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            productCategoryPanel.Controls.Add(productCategoryLabel, 0, 0);
            productCategoryPanel.Controls.Add(productCategoryComboBox, 0, 1);
            productCategoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            productCategoryPanel.Location = new System.Drawing.Point(536, 0);
            productCategoryPanel.Margin = new System.Windows.Forms.Padding(0);
            productCategoryPanel.Name = "productCategoryPanel";
            productCategoryPanel.RowCount = 2;
            productCategoryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productCategoryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            productCategoryPanel.Size = new System.Drawing.Size(271, 47);
            productCategoryPanel.TabIndex = 2;
            // 
            // productCategoryLabel
            // 
            productCategoryLabel.AutoSize = true;
            productCategoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            productCategoryLabel.Location = new System.Drawing.Point(3, 0);
            productCategoryLabel.Name = "productCategoryLabel";
            productCategoryLabel.Size = new System.Drawing.Size(265, 15);
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
            productCategoryComboBox.Size = new System.Drawing.Size(265, 23);
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
            buttonsPanel.Location = new System.Drawing.Point(3, 466);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(807, 29);
            buttonsPanel.TabIndex = 9;
            // 
            // closeButton
            // 
            closeButton.AutoSize = true;
            closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            closeButton.Location = new System.Drawing.Point(730, 2);
            closeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            closeButton.Name = "closeButton";
            closeButton.Size = new System.Drawing.Size(74, 25);
            closeButton.TabIndex = 0;
            closeButton.Text = "Закрыть";
            closeButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            saveButton.AutoSize = true;
            saveButton.Location = new System.Drawing.Point(644, 2);
            saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(80, 25);
            saveButton.TabIndex = 1;
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.AutoSize = true;
            deleteButton.Location = new System.Drawing.Point(547, 2);
            deleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new System.Drawing.Size(91, 25);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Удалить узел";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // previewPanel
            // 
            previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewPanel.Controls.Add(previewPictureBox);
            previewPanel.Controls.Add(previewImageStatusLabel);
            previewPanel.Controls.Add(previewSourceLabel);
            previewPanel.Controls.Add(previewTitleLabel);
            previewPanel.Dock = System.Windows.Forms.DockStyle.Right;
            previewPanel.Location = new System.Drawing.Point(1077, 12);
            previewPanel.Name = "previewPanel";
            previewPanel.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            previewPanel.Size = new System.Drawing.Size(139, 493);
            previewPanel.TabIndex = 1;
            // 
            // previewPictureBox
            // 
            previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            previewPictureBox.Location = new System.Drawing.Point(10, 68);
            previewPictureBox.Name = "previewPictureBox";
            previewPictureBox.Size = new System.Drawing.Size(117, 387);
            previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            previewPictureBox.TabIndex = 0;
            previewPictureBox.TabStop = false;
            // 
            // previewImageStatusLabel
            // 
            previewImageStatusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            previewImageStatusLabel.Location = new System.Drawing.Point(10, 455);
            previewImageStatusLabel.Name = "previewImageStatusLabel";
            previewImageStatusLabel.Size = new System.Drawing.Size(117, 24);
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
            previewSourceLabel.Size = new System.Drawing.Size(117, 36);
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
            previewTitleLabel.Size = new System.Drawing.Size(117, 20);
            previewTitleLabel.TabIndex = 3;
            previewTitleLabel.Text = "Источник базового узла";
            // 
            // BaseNodeLibraryEditorForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new System.Drawing.Size(1229, 517);
            Controls.Add(mainLayoutPanel);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            MinimumSize = new System.Drawing.Size(860, 475);
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
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Label previewImageStatusLabel;
        private System.Windows.Forms.Label previewSourceLabel;
        private System.Windows.Forms.Label previewTitleLabel;
        private System.Windows.Forms.ToolTip previewToolTip;
    }
}
