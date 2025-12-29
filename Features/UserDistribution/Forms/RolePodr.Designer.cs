namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class RolePodr
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            customGridControlRole = new Core.Class.CustomGridControl();
            gridViewRole = new DevExpress.XtraGrid.Views.Grid.GridView();
            RoleID = new DevExpress.XtraGrid.Columns.GridColumn();
            RoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            Description = new DevExpress.XtraGrid.Columns.GridColumn();
            IsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            customGridControlTable = new Core.Class.CustomGridControl();
            gridViewTable = new DevExpress.XtraGrid.Views.Grid.GridView();
            IdAtn = new DevExpress.XtraGrid.Columns.GridColumn();
            TableName = new DevExpress.XtraGrid.Columns.GridColumn();
            TableNameRus = new DevExpress.XtraGrid.Columns.GridColumn();
            customCheckBoxPodr = new CustomCheckBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControlRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTable).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(customGridControlRole, 1, 0);
            tableLayoutPanel1.Controls.Add(customGridControlTable, 0, 0);
            tableLayoutPanel1.Controls.Add(customCheckBoxPodr, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.38202F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.61797762F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(893, 623);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // customGridControlRole
            // 
            customGridControlRole.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlRole.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlRole.Location = new System.Drawing.Point(449, 3);
            customGridControlRole.MainView = gridViewRole;
            customGridControlRole.Name = "customGridControlRole";
            customGridControlRole.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEdit1 });
            customGridControlRole.Size = new System.Drawing.Size(441, 581);
            customGridControlRole.TabIndex = 12;
            customGridControlRole.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRole });
            // 
            // gridViewRole
            // 
            gridViewRole.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridViewRole.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewRole.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.FocusedRow.Options.UseFont = true;
            gridViewRole.Appearance.GroupRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.GroupRow.Options.UseFont = true;
            gridViewRole.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewRole.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewRole.Appearance.Row.Options.UseFont = true;
            gridViewRole.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { RoleID, RoleName, Description, IsSelected });
            gridViewRole.GridControl = customGridControlRole;
            gridViewRole.Name = "gridViewRole";
            gridViewRole.OptionsBehavior.Editable = false;
            gridViewRole.OptionsView.EnableAppearanceEvenRow = true;
            gridViewRole.CellValueChanged += gridViewRole_CellValueChanged;
            // 
            // RoleID
            // 
            RoleID.Caption = "ИД роли";
            RoleID.FieldName = "RoleID";
            RoleID.Name = "RoleID";
            RoleID.Visible = true;
            RoleID.VisibleIndex = 0;
            RoleID.Width = 56;
            // 
            // RoleName
            // 
            RoleName.Caption = "Имя роли";
            RoleName.FieldName = "RoleName";
            RoleName.Name = "RoleName";
            RoleName.Visible = true;
            RoleName.VisibleIndex = 1;
            RoleName.Width = 141;
            // 
            // Description
            // 
            Description.Caption = "Описание";
            Description.FieldName = "Description";
            Description.Name = "Description";
            Description.Visible = true;
            Description.VisibleIndex = 2;
            Description.Width = 196;
            // 
            // IsSelected
            // 
            IsSelected.Caption = "Выбор";
            IsSelected.ColumnEdit = repositoryItemCheckEdit1;
            IsSelected.FieldName = "IsSelected";
            IsSelected.Name = "IsSelected";
            IsSelected.Visible = true;
            IsSelected.VisibleIndex = 3;
            // 
            // repositoryItemCheckEdit1
            // 
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit1.ValueGrayed = false;
            // 
            // customGridControlTable
            // 
            customGridControlTable.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlTable.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlTable.Location = new System.Drawing.Point(3, 3);
            customGridControlTable.MainView = gridViewTable;
            customGridControlTable.Name = "customGridControlTable";
            customGridControlTable.Size = new System.Drawing.Size(440, 581);
            customGridControlTable.TabIndex = 13;
            customGridControlTable.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTable });
            // 
            // gridViewTable
            // 
            gridViewTable.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridViewTable.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewTable.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewTable.Appearance.FocusedRow.Options.UseFont = true;
            gridViewTable.Appearance.GroupRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewTable.Appearance.GroupRow.Options.UseFont = true;
            gridViewTable.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewTable.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewTable.Appearance.Preview.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewTable.Appearance.Preview.Options.UseFont = true;
            gridViewTable.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewTable.Appearance.Row.Options.UseFont = true;
            gridViewTable.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { IdAtn, TableName, TableNameRus });
            gridViewTable.GridControl = customGridControlTable;
            gridViewTable.Name = "gridViewTable";
            gridViewTable.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridViewTable.OptionsEditForm.EditFormColumnCount = 1;
            gridViewTable.OptionsView.EnableAppearanceEvenRow = true;
            gridViewTable.RowClick += gridViewTable_RowClick;
            // 
            // IdAtn
            // 
            IdAtn.Caption = "Ид таблицы";
            IdAtn.FieldName = "id_atn";
            IdAtn.Name = "IdAtn";
            IdAtn.Visible = true;
            IdAtn.VisibleIndex = 0;
            IdAtn.Width = 99;
            // 
            // TableName
            // 
            TableName.Caption = "Название SQL";
            TableName.FieldName = "name";
            TableName.Name = "TableName";
            TableName.Visible = true;
            TableName.VisibleIndex = 1;
            TableName.Width = 399;
            // 
            // TableNameRus
            // 
            TableNameRus.Caption = "Русское название";
            TableNameRus.FieldName = "name_rus";
            TableNameRus.Name = "TableNameRus";
            TableNameRus.Visible = true;
            TableNameRus.VisibleIndex = 2;
            TableNameRus.Width = 479;
            // 
            // customCheckBoxPodr
            // 
            customCheckBoxPodr.AutoSize = true;
            customCheckBoxPodr.Font = new System.Drawing.Font("Arial", 10F);
            customCheckBoxPodr.ForeColor = System.Drawing.Color.FromArgb(85, 45, 115);
            customCheckBoxPodr.Location = new System.Drawing.Point(3, 590);
            customCheckBoxPodr.Name = "customCheckBoxPodr";
            customCheckBoxPodr.Size = new System.Drawing.Size(129, 20);
            customCheckBoxPodr.TabIndex = 14;
            customCheckBoxPodr.Text = "Подразделения";
            customCheckBoxPodr.UseVisualStyleBackColor = true;
            customCheckBoxPodr.CheckedChanged += customCheckBoxPodr_CheckedChanged;
            // 
            // RolePodr
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(893, 623);
            Controls.Add(tableLayoutPanel1);
            Name = "RolePodr";
            Text = "Роли - Подразделения";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControlRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Core.Class.CustomGridControl customGridControlRole;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRole;
        private DevExpress.XtraGrid.Columns.GridColumn RoleID;
        private DevExpress.XtraGrid.Columns.GridColumn RoleName;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private Core.Class.CustomGridControl customGridControlTable;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTable;
        private DevExpress.XtraGrid.Columns.GridColumn IdAtn;
        private DevExpress.XtraGrid.Columns.GridColumn TableName;
        private DevExpress.XtraGrid.Columns.GridColumn TableNameRus;
        private CustomCheckBox customCheckBoxPodr;
        private DevExpress.XtraGrid.Columns.GridColumn IsSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}