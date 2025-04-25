namespace SewingProduction.form.UserDistribution
{
    partial class ActionHistory
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
            this.customGridHistory = new SewingProduction.CustomGridControl();
            this.gridViewHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ActionHistoryID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Event = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NameObject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NameForm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EventDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Komp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.repositoryItemComboBoxForms = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxObject = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.customGridHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxObject)).BeginInit();
            this.SuspendLayout();
            // 
            // customGridHistory
            // 
            this.customGridHistory.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridHistory.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridHistory.Location = new System.Drawing.Point(0, 0);
            this.customGridHistory.MainView = this.gridViewHistory;
            this.customGridHistory.Name = "customGridHistory";
            this.customGridHistory.ObjectName = null;
            this.customGridHistory.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckedComboBoxEdit1,
            this.repositoryItemComboBoxForms,
            this.repositoryItemComboBoxObject});
            this.customGridHistory.Size = new System.Drawing.Size(1321, 651);
            this.customGridHistory.TabIndex = 1;
            this.customGridHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewHistory});
            // 
            // gridViewHistory
            // 
            this.gridViewHistory.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.gridViewHistory.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridViewHistory.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(200)))));
            this.gridViewHistory.Appearance.Row.Options.UseBackColor = true;
            this.gridViewHistory.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridViewHistory.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridViewHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ActionHistoryID,
            this.Event,
            this.UserID,
            this.UserName,
            this.NameObject,
            this.NameForm,
            this.EventDate,
            this.Komp});
            this.gridViewHistory.DetailHeight = 3500;
            this.gridViewHistory.GridControl = this.customGridHistory;
            this.gridViewHistory.Name = "gridViewHistory";
            this.gridViewHistory.OptionsDetail.ShowDetailTabs = false;
            this.gridViewHistory.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridViewHistory.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewHistory.OptionsView.ShowGroupPanel = false;
            // 
            // ActionHistoryID
            // 
            this.ActionHistoryID.Caption = "Ид";
            this.ActionHistoryID.FieldName = "UserID";
            this.ActionHistoryID.Name = "ActionHistoryID";
            this.ActionHistoryID.OptionsColumn.ReadOnly = true;
            this.ActionHistoryID.Width = 144;
            // 
            // Event
            // 
            this.Event.Caption = "Событие";
            this.Event.FieldName = "Event";
            this.Event.Name = "Event";
            this.Event.Visible = true;
            this.Event.VisibleIndex = 0;
            // 
            // UserID
            // 
            this.UserID.Caption = "UserID";
            this.UserID.FieldName = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.OptionsColumn.ReadOnly = true;
            this.UserID.Width = 596;
            // 
            // UserName
            // 
            this.UserName.Caption = "Имя пользователя";
            this.UserName.FieldName = "UserName";
            this.UserName.Name = "UserName";
            this.UserName.Visible = true;
            this.UserName.VisibleIndex = 3;
            // 
            // NameObject
            // 
            this.NameObject.Caption = "Объект";
            this.NameObject.FieldName = "NameObject";
            this.NameObject.Name = "NameObject";
            this.NameObject.Visible = true;
            this.NameObject.VisibleIndex = 4;
            // 
            // NameForm
            // 
            this.NameForm.Caption = "Форма";
            this.NameForm.FieldName = "NameForm";
            this.NameForm.Name = "NameForm";
            this.NameForm.Visible = true;
            this.NameForm.VisibleIndex = 5;
            // 
            // EventDate
            // 
            this.EventDate.Caption = "Дата";
            this.EventDate.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            this.EventDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.EventDate.FieldName = "EventDate";
            this.EventDate.Name = "EventDate";
            this.EventDate.Visible = true;
            this.EventDate.VisibleIndex = 1;
            // 
            // Komp
            // 
            this.Komp.Caption = "Компьютер";
            this.Komp.FieldName = "Komp";
            this.Komp.Name = "Komp";
            this.Komp.Visible = true;
            this.Komp.VisibleIndex = 2;
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // repositoryItemComboBoxForms
            // 
            this.repositoryItemComboBoxForms.AutoHeight = false;
            this.repositoryItemComboBoxForms.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxForms.Items.AddRange(new object[] {
            "Нет доступа",
            "Просмотр",
            "Редактор"});
            this.repositoryItemComboBoxForms.Name = "repositoryItemComboBoxForms";
            this.repositoryItemComboBoxForms.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemComboBoxObject
            // 
            this.repositoryItemComboBoxObject.AutoHeight = false;
            this.repositoryItemComboBoxObject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxObject.Name = "repositoryItemComboBoxObject";
            this.repositoryItemComboBoxObject.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // ActionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 651);
            this.Controls.Add(this.customGridHistory);
            this.Name = "ActionHistory";
            this.Text = "История действий";
            this.Load += new System.EventHandler(this.ActionHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customGridHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomGridControl customGridHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHistory;
        private DevExpress.XtraGrid.Columns.GridColumn ActionHistoryID;
        private DevExpress.XtraGrid.Columns.GridColumn Event;
        private DevExpress.XtraGrid.Columns.GridColumn UserID;
        private DevExpress.XtraGrid.Columns.GridColumn UserName;
        private DevExpress.XtraGrid.Columns.GridColumn NameObject;
        private DevExpress.XtraGrid.Columns.GridColumn NameForm;
        private DevExpress.XtraGrid.Columns.GridColumn EventDate;
        private DevExpress.XtraGrid.Columns.GridColumn Komp;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxForms;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxObject;
    }
}