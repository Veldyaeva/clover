namespace SewingProduction.Features.KnittingProduction.Forms
{
    partial class KnittingMachinesLoading
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
            customLabel1 = new CustomLabel();
            сomboBoxKnitMachineClassList = new CustomComboBox();
            customLabel2 = new CustomLabel();
            gridControlKnitMachineLoadInfo = new CustomGridControl();
            gridViewKnitMachineLoadInfoCards = new DevExpress.XtraGrid.Views.Card.CardView();
            gridColumnKnitMachineLoadInfoKmlNumberCard = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnKnitMachineLoadInfoYearMonthCard = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnKnitMachineLoadInfoCombinedPszNomCard = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            gridViewKnitMachineLoadInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumnKnitMachineLoadInfoYearMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnKnitMachineLoadInfoKmlNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnKnitMachineLoadInfoCombinedPszNom = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)gridControlKnitMachineLoadInfo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewKnitMachineLoadInfoCards).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemRichTextEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemMemoEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewKnitMachineLoadInfo).BeginInit();
            SuspendLayout();
            // 
            // customLabel1
            // 
            customLabel1.AutoSize = true;
            customLabel1.Font = new System.Drawing.Font("Arial", 10F);
            customLabel1.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabel1.Location = new System.Drawing.Point(848, 40);
            customLabel1.Name = "customLabel1";
            customLabel1.Size = new System.Drawing.Size(135, 16);
            customLabel1.TabIndex = 0;
            customLabel1.Text = "Текущий загруз В/М";
            // 
            // сomboBoxKnitMachineClassList
            // 
            сomboBoxKnitMachineClassList.BackColor = System.Drawing.Color.FromArgb(220, 240, 250);
            сomboBoxKnitMachineClassList.Font = new System.Drawing.Font("Arial", 10F);
            сomboBoxKnitMachineClassList.ForeColor = System.Drawing.Color.FromArgb(25, 75, 105);
            сomboBoxKnitMachineClassList.FormattingEnabled = true;
            сomboBoxKnitMachineClassList.Location = new System.Drawing.Point(90, 65);
            сomboBoxKnitMachineClassList.Name = "сomboBoxKnitMachineClassList";
            сomboBoxKnitMachineClassList.Size = new System.Drawing.Size(121, 24);
            сomboBoxKnitMachineClassList.TabIndex = 1;
            сomboBoxKnitMachineClassList.SelectedIndexChanged += сomboBoxKnitMachineAreaList_SelectedIndexChanged;
            сomboBoxKnitMachineClassList.DisplayMemberChanged += сomboBoxKnitMachineAreaList_DisplayMemberChanged;
            // 
            // customLabel2
            // 
            customLabel2.AutoSize = true;
            customLabel2.Font = new System.Drawing.Font("Arial", 10F);
            customLabel2.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabel2.Location = new System.Drawing.Point(11, 68);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new System.Drawing.Size(73, 16);
            customLabel2.TabIndex = 2;
            customLabel2.Text = "Класс В/М";
            // 
            // gridControlKnitMachineLoadInfo
            // 
            gridControlKnitMachineLoadInfo.Font = new System.Drawing.Font("Arial", 10F);
            gridControlKnitMachineLoadInfo.Location = new System.Drawing.Point(12, 95);
            gridControlKnitMachineLoadInfo.MainView = gridViewKnitMachineLoadInfoCards;
            gridControlKnitMachineLoadInfo.Name = "gridControlKnitMachineLoadInfo";
            gridControlKnitMachineLoadInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemMemoEdit1, repositoryItemRichTextEdit1 });
            gridControlKnitMachineLoadInfo.Size = new System.Drawing.Size(1806, 681);
            gridControlKnitMachineLoadInfo.TabIndex = 3;
            gridControlKnitMachineLoadInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewKnitMachineLoadInfoCards, gridViewKnitMachineLoadInfo });
            // 
            // gridViewKnitMachineLoadInfoCards
            // 
            gridViewKnitMachineLoadInfoCards.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnKnitMachineLoadInfoKmlNumberCard, gridColumnKnitMachineLoadInfoYearMonthCard, gridColumnKnitMachineLoadInfoCombinedPszNomCard });
            gridViewKnitMachineLoadInfoCards.DetailHeight = 700;
            gridViewKnitMachineLoadInfoCards.GridControl = gridControlKnitMachineLoadInfo;
            gridViewKnitMachineLoadInfoCards.MaximumCardRows = 3;
            gridViewKnitMachineLoadInfoCards.Name = "gridViewKnitMachineLoadInfoCards";
            gridViewKnitMachineLoadInfoCards.OptionsBehavior.FieldAutoHeight = true;
            gridViewKnitMachineLoadInfoCards.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            // 
            // gridColumnKnitMachineLoadInfoKmlNumberCard
            // 
            gridColumnKnitMachineLoadInfoKmlNumberCard.Name = "gridColumnKnitMachineLoadInfoKmlNumberCard";
            // 
            // gridColumnKnitMachineLoadInfoYearMonthCard
            // 
            gridColumnKnitMachineLoadInfoYearMonthCard.Name = "gridColumnKnitMachineLoadInfoYearMonthCard";
            gridColumnKnitMachineLoadInfoYearMonthCard.Visible = true;
            gridColumnKnitMachineLoadInfoYearMonthCard.VisibleIndex = 0;
            // 
            // gridColumnKnitMachineLoadInfoCombinedPszNomCard
            // 
            gridColumnKnitMachineLoadInfoCombinedPszNomCard.ColumnEdit = repositoryItemRichTextEdit1;
            gridColumnKnitMachineLoadInfoCombinedPszNomCard.Name = "gridColumnKnitMachineLoadInfoCombinedPszNomCard";
            gridColumnKnitMachineLoadInfoCombinedPszNomCard.Visible = true;
            gridColumnKnitMachineLoadInfoCombinedPszNomCard.VisibleIndex = 1;
            // 
            // repositoryItemRichTextEdit1
            // 
            repositoryItemRichTextEdit1.CustomHeight = 120;
            repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            repositoryItemRichTextEdit1.ShowCaretInReadOnly = false;
            // 
            // repositoryItemMemoEdit1
            // 
            repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gridViewKnitMachineLoadInfo
            // 
            gridViewKnitMachineLoadInfo.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridViewKnitMachineLoadInfo.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewKnitMachineLoadInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnKnitMachineLoadInfoYearMonth, gridColumnKnitMachineLoadInfoKmlNumber, gridColumnKnitMachineLoadInfoCombinedPszNom });
            gridViewKnitMachineLoadInfo.GridControl = gridControlKnitMachineLoadInfo;
            gridViewKnitMachineLoadInfo.Name = "gridViewKnitMachineLoadInfo";
            gridViewKnitMachineLoadInfo.OptionsView.EnableAppearanceEvenRow = true;
            gridViewKnitMachineLoadInfo.RowHeight = 150;
            // 
            // gridColumnKnitMachineLoadInfoYearMonth
            // 
            gridColumnKnitMachineLoadInfoYearMonth.Caption = "мес/год";
            gridColumnKnitMachineLoadInfoYearMonth.Name = "gridColumnKnitMachineLoadInfoYearMonth";
            gridColumnKnitMachineLoadInfoYearMonth.Visible = true;
            gridColumnKnitMachineLoadInfoYearMonth.VisibleIndex = 0;
            // 
            // gridColumnKnitMachineLoadInfoKmlNumber
            // 
            gridColumnKnitMachineLoadInfoKmlNumber.Caption = "№ вяз машины";
            gridColumnKnitMachineLoadInfoKmlNumber.Name = "gridColumnKnitMachineLoadInfoKmlNumber";
            gridColumnKnitMachineLoadInfoKmlNumber.Visible = true;
            gridColumnKnitMachineLoadInfoKmlNumber.VisibleIndex = 1;
            // 
            // gridColumnKnitMachineLoadInfoCombinedPszNom
            // 
            gridColumnKnitMachineLoadInfoCombinedPszNom.Caption = "задания";
            gridColumnKnitMachineLoadInfoCombinedPszNom.ColumnEdit = repositoryItemRichTextEdit1;
            gridColumnKnitMachineLoadInfoCombinedPszNom.Name = "gridColumnKnitMachineLoadInfoCombinedPszNom";
            gridColumnKnitMachineLoadInfoCombinedPszNom.Visible = true;
            gridColumnKnitMachineLoadInfoCombinedPszNom.VisibleIndex = 2;
            // 
            // KnittingMachinesLoading
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1830, 890);
            Controls.Add(gridControlKnitMachineLoadInfo);
            Controls.Add(customLabel2);
            Controls.Add(сomboBoxKnitMachineClassList);
            Controls.Add(customLabel1);
            Name = "KnittingMachinesLoading";
            Text = "Текущий загурз В/М";
            Load += KnittingMachinesLoading_Load;
            ((System.ComponentModel.ISupportInitialize)gridControlKnitMachineLoadInfo).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewKnitMachineLoadInfoCards).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemRichTextEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemMemoEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewKnitMachineLoadInfo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomLabel customLabel1;
        private CustomComboBox сomboBoxKnitMachineClassList;
        private CustomLabel customLabel2;
        private CustomGridControl gridControlKnitMachineLoadInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKnitMachineLoadInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnKnitMachineLoadInfoYearMonth;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnKnitMachineLoadInfoKmlNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnKnitMachineLoadInfoCombinedPszNom;
        private DevExpress.XtraGrid.Views.Card.CardView gridViewKnitMachineLoadInfoCards;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnKnitMachineLoadInfoYearMonthCard;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnKnitMachineLoadInfoKmlNumberCard;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnKnitMachineLoadInfoCombinedPszNomCard;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
    }
}