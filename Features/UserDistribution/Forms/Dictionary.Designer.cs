namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class Dictionary
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
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            customGridControlColumn = new CustomGridControl();
            bindingSourceColumn = new System.Windows.Forms.BindingSource(components);
            gridViewColumn = new DevExpress.XtraGrid.Views.Grid.GridView();
            customGridControlTable = new CustomGridControl();
            bindingSourceTable = new System.Windows.Forms.BindingSource(components);
            gridViewTable = new DevExpress.XtraGrid.Views.Grid.GridView();
            IdAtn = new DevExpress.XtraGrid.Columns.GridColumn();
            TableName = new DevExpress.XtraGrid.Columns.GridColumn();
            TableNameRus = new DevExpress.XtraGrid.Columns.GridColumn();
            customTextBoxAddTable = new CustomTextBox();
            customLabelAddTable = new CustomLabel();
            customButtonAddTable = new CustomButton();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControlColumn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceColumn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewColumn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTable).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(customGridControlColumn, 0, 5);
            tableLayoutPanel1.Controls.Add(customGridControlTable, 0, 0);
            tableLayoutPanel1.Controls.Add(customTextBoxAddTable, 4, 1);
            tableLayoutPanel1.Controls.Add(customLabelAddTable, 4, 0);
            tableLayoutPanel1.Controls.Add(customButtonAddTable, 4, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 10;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1264, 662);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // customGridControlColumn
            // 
            tableLayoutPanel1.SetColumnSpan(customGridControlColumn, 4);
            customGridControlColumn.DataSource = bindingSourceColumn;
            customGridControlColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlColumn.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlColumn.Location = new System.Drawing.Point(3, 333);
            customGridControlColumn.MainView = gridViewColumn;
            customGridControlColumn.Name = "customGridControlColumn";
            tableLayoutPanel1.SetRowSpan(customGridControlColumn, 5);
            customGridControlColumn.Size = new System.Drawing.Size(1002, 326);
            customGridControlColumn.TabIndex = 3;
            customGridControlColumn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewColumn });
            // 
            // gridViewColumn
            // 
            gridViewColumn.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridViewColumn.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewColumn.GridControl = customGridControlColumn;
            gridViewColumn.Name = "gridViewColumn";
            gridViewColumn.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // customGridControlTable
            // 
            tableLayoutPanel1.SetColumnSpan(customGridControlTable, 4);
            customGridControlTable.DataSource = bindingSourceTable;
            customGridControlTable.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControlTable.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlTable.Location = new System.Drawing.Point(3, 3);
            customGridControlTable.MainView = gridViewTable;
            customGridControlTable.Name = "customGridControlTable";
            tableLayoutPanel1.SetRowSpan(customGridControlTable, 5);
            customGridControlTable.Size = new System.Drawing.Size(1002, 324);
            customGridControlTable.TabIndex = 1;
            customGridControlTable.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTable });
            customGridControlTable.Load += customGridControlTable_Load;
            customGridControlTable.Click += customGridControlTable_Click;
            // 
            // gridViewTable
            // 
            gridViewTable.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridViewTable.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewTable.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { IdAtn, TableName, TableNameRus });
            gridViewTable.GridControl = customGridControlTable;
            gridViewTable.Name = "gridViewTable";
            gridViewTable.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // IdAtn
            // 
            IdAtn.Caption = "Ид";
            IdAtn.FieldName = "IdAtn";
            IdAtn.Name = "IdAtn";
            IdAtn.Visible = true;
            IdAtn.VisibleIndex = 0;
            IdAtn.Width = 99;
            // 
            // TableName
            // 
            TableName.Caption = "Название SQL";
            TableName.FieldName = "Name";
            TableName.Name = "TableName";
            TableName.Visible = true;
            TableName.VisibleIndex = 1;
            TableName.Width = 399;
            // 
            // TableNameRus
            // 
            TableNameRus.Caption = "Русское название";
            TableNameRus.FieldName = "NameRus";
            TableNameRus.Name = "TableNameRus";
            TableNameRus.Visible = true;
            TableNameRus.VisibleIndex = 2;
            TableNameRus.Width = 479;
            // 
            // customTextBoxAddTable
            // 
            customTextBoxAddTable.BackColor = System.Drawing.Color.FromArgb(220, 240, 250);
            customTextBoxAddTable.Dock = System.Windows.Forms.DockStyle.Fill;
            customTextBoxAddTable.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxAddTable.ForeColor = System.Drawing.Color.FromArgb(25, 75, 105);
            customTextBoxAddTable.Location = new System.Drawing.Point(1011, 69);
            customTextBoxAddTable.Name = "customTextBoxAddTable";
            customTextBoxAddTable.Size = new System.Drawing.Size(250, 23);
            customTextBoxAddTable.TabIndex = 2;
            // 
            // customLabelAddTable
            // 
            customLabelAddTable.AutoSize = true;
            customLabelAddTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            customLabelAddTable.Font = new System.Drawing.Font("Arial", 10F);
            customLabelAddTable.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabelAddTable.Location = new System.Drawing.Point(1011, 50);
            customLabelAddTable.Name = "customLabelAddTable";
            customLabelAddTable.Size = new System.Drawing.Size(250, 16);
            customLabelAddTable.TabIndex = 4;
            customLabelAddTable.Text = "Добавить таблицу из SQL";
            // 
            // customButtonAddTable
            // 
            customButtonAddTable.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            customButtonAddTable.Dock = System.Windows.Forms.DockStyle.Fill;
            customButtonAddTable.Font = new System.Drawing.Font("Arial", 10F);
            customButtonAddTable.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customButtonAddTable.Location = new System.Drawing.Point(1011, 135);
            customButtonAddTable.Name = "customButtonAddTable";
            customButtonAddTable.Size = new System.Drawing.Size(250, 60);
            customButtonAddTable.TabIndex = 5;
            customButtonAddTable.Text = "Добавить";
            customButtonAddTable.UseVisualStyleBackColor = false;
            // 
            // Dictionary
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1264, 662);
            Controls.Add(tableLayoutPanel1);
            Name = "Dictionary";
            Text = "Описатель";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControlColumn).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceColumn).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewColumn).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTable;
        private CustomGridControl customGridControlColumn;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewColumn;
        private CustomTextBox customTextBoxAddTable;
        private CustomLabel customLabelAddTable;
        private CustomButton customButtonAddTable;
        private CustomGridControl customGridControlTable;
        private System.Windows.Forms.BindingSource bindingSourceTable;
        private System.Windows.Forms.BindingSource bindingSourceColumn;
        private DevExpress.XtraGrid.Columns.GridColumn IdAtn;
        private DevExpress.XtraGrid.Columns.GridColumn TableName;
        private DevExpress.XtraGrid.Columns.GridColumn TableNameRus;
    }
}