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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionHistory));
            customGridHistory = new CustomGridControl();
            gridViewHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            ActionHistoryID = new DevExpress.XtraGrid.Columns.GridColumn();
            Event = new DevExpress.XtraGrid.Columns.GridColumn();
            UserID = new DevExpress.XtraGrid.Columns.GridColumn();
            UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            NameObject = new DevExpress.XtraGrid.Columns.GridColumn();
            NameForm = new DevExpress.XtraGrid.Columns.GridColumn();
            EventDate = new DevExpress.XtraGrid.Columns.GridColumn();
            Komp = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            repositoryItemComboBoxForms = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            repositoryItemComboBoxObject = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)customGridHistory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewHistory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckedComboBoxEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxForms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxObject).BeginInit();
            SuspendLayout();
            // 
            // customGridHistory
            // 
            customGridHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridHistory.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridHistory.Font = new System.Drawing.Font("Arial", 10F);
            customGridHistory.Location = new System.Drawing.Point(0, 0);
            customGridHistory.MainView = gridViewHistory;
            customGridHistory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customGridHistory.Name = "customGridHistory";
            customGridHistory.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckedComboBoxEdit1, repositoryItemComboBoxForms, repositoryItemComboBoxObject });
            customGridHistory.Size = new System.Drawing.Size(1541, 751);
            customGridHistory.TabIndex = 1;
            customGridHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewHistory });
            // 
            // gridViewHistory
            // 
            gridViewHistory.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            gridViewHistory.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewHistory.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(250, 250, 200);
            gridViewHistory.Appearance.Row.Options.UseBackColor = true;
            gridViewHistory.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(255, 192, 255);
            gridViewHistory.AppearancePrint.EvenRow.Options.UseBackColor = true;
            gridViewHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ActionHistoryID, Event, UserID, UserName, NameObject, NameForm, EventDate, Komp });
            gridViewHistory.DetailHeight = 4038;
            gridViewHistory.GridControl = customGridHistory;
            gridViewHistory.Name = "gridViewHistory";
            gridViewHistory.OptionsDetail.ShowDetailTabs = false;
            gridViewHistory.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewHistory.OptionsPrint.EnableAppearanceEvenRow = true;
            gridViewHistory.OptionsView.EnableAppearanceEvenRow = true;
            gridViewHistory.OptionsView.ShowGroupPanel = false;
            // 
            // ActionHistoryID
            // 
            ActionHistoryID.Caption = "Ид";
            ActionHistoryID.FieldName = "UserID";
            ActionHistoryID.MinWidth = 23;
            ActionHistoryID.Name = "ActionHistoryID";
            ActionHistoryID.OptionsColumn.ReadOnly = true;
            ActionHistoryID.Width = 168;
            // 
            // Event
            // 
            Event.Caption = "Событие";
            Event.FieldName = "Event";
            Event.MinWidth = 23;
            Event.Name = "Event";
            Event.Visible = true;
            Event.VisibleIndex = 0;
            Event.Width = 87;
            // 
            // UserID
            // 
            UserID.Caption = "UserID";
            UserID.FieldName = "UserID";
            UserID.MinWidth = 23;
            UserID.Name = "UserID";
            UserID.OptionsColumn.ReadOnly = true;
            UserID.Width = 695;
            // 
            // UserName
            // 
            UserName.Caption = "Имя пользователя";
            UserName.FieldName = "UserName";
            UserName.MinWidth = 23;
            UserName.Name = "UserName";
            UserName.Visible = true;
            UserName.VisibleIndex = 3;
            UserName.Width = 87;
            // 
            // NameObject
            // 
            NameObject.Caption = "Объект";
            NameObject.FieldName = "NameObject";
            NameObject.MinWidth = 23;
            NameObject.Name = "NameObject";
            NameObject.Visible = true;
            NameObject.VisibleIndex = 4;
            NameObject.Width = 87;
            // 
            // NameForm
            // 
            NameForm.Caption = "Форма";
            NameForm.FieldName = "NameForm";
            NameForm.MinWidth = 23;
            NameForm.Name = "NameForm";
            NameForm.Visible = true;
            NameForm.VisibleIndex = 5;
            NameForm.Width = 87;
            // 
            // EventDate
            // 
            EventDate.Caption = "Дата";
            EventDate.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            EventDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            EventDate.FieldName = "EventDate";
            EventDate.MinWidth = 23;
            EventDate.Name = "EventDate";
            EventDate.Visible = true;
            EventDate.VisibleIndex = 1;
            EventDate.Width = 87;
            // 
            // Komp
            // 
            Komp.Caption = "Компьютер";
            Komp.FieldName = "Komp";
            Komp.MinWidth = 23;
            Komp.Name = "Komp";
            Komp.Visible = true;
            Komp.VisibleIndex = 2;
            Komp.Width = 87;
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // repositoryItemComboBoxForms
            // 
            repositoryItemComboBoxForms.AutoHeight = false;
            repositoryItemComboBoxForms.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBoxForms.Items.AddRange(new object[] { "Нет доступа", "Просмотр", "Редактор" });
            repositoryItemComboBoxForms.Name = "repositoryItemComboBoxForms";
            repositoryItemComboBoxForms.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemComboBoxObject
            // 
            repositoryItemComboBoxObject.AutoHeight = false;
            repositoryItemComboBoxObject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBoxObject.Name = "repositoryItemComboBoxObject";
            repositoryItemComboBoxObject.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // ActionHistory
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1541, 751);
            Controls.Add(customGridHistory);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ActionHistory";
            Text = "История действий";
            Load += ActionHistory_Load;
            ((System.ComponentModel.ISupportInitialize)customGridHistory).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewHistory).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckedComboBoxEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxForms).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBoxObject).EndInit();
            ResumeLayout(false);
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