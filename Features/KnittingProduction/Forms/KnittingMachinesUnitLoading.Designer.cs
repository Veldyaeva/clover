namespace SewingProduction.Features.KnittingProduction.Forms
{
    partial class KnittingMachinesUnitLoading
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
            gridControlPlanSezonZadKnitMachine = new CustomGridControl();
            gridViewPlanSezonZadKnitMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridPlanSezonZadKnitMachineColumnArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnPszkmPszNom = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnHoursTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnDateZap = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnYearMonthPlanDate = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnKmlNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnYearNumberPlanDate = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnYearMonthText = new DevExpress.XtraGrid.Columns.GridColumn();
            gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt = new DevExpress.XtraGrid.Columns.GridColumn();
            labelKmlNumber = new CustomLabel();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            customGridControl1 = new CustomGridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            ((System.ComponentModel.ISupportInitialize)gridControlPlanSezonZadKnitMachine).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPlanSezonZadKnitMachine).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).BeginInit();
            SuspendLayout();
            // 
            // gridControlPlanSezonZadKnitMachine
            // 
            gridControlPlanSezonZadKnitMachine.Font = new System.Drawing.Font("Arial", 10F);
            gridControlPlanSezonZadKnitMachine.Location = new System.Drawing.Point(12, 62);
            gridControlPlanSezonZadKnitMachine.MainView = gridViewPlanSezonZadKnitMachine;
            gridControlPlanSezonZadKnitMachine.Name = "gridControlPlanSezonZadKnitMachine";
            gridControlPlanSezonZadKnitMachine.Size = new System.Drawing.Size(820, 517);
            gridControlPlanSezonZadKnitMachine.TabIndex = 0;
            gridControlPlanSezonZadKnitMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewPlanSezonZadKnitMachine });
            // 
            // gridViewPlanSezonZadKnitMachine
            // 
            gridViewPlanSezonZadKnitMachine.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridViewPlanSezonZadKnitMachine.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewPlanSezonZadKnitMachine.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridPlanSezonZadKnitMachineColumnArticul, gridPlanSezonZadKnitMachineColumnPszkmPszNom, gridPlanSezonZadKnitMachineColumnHoursTotal, gridPlanSezonZadKnitMachineColumnDateZap, gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom, gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo, gridPlanSezonZadKnitMachineColumnYearMonthPlanDate, gridPlanSezonZadKnitMachineColumnKmlNumber, gridPlanSezonZadKnitMachineColumnYearNumberPlanDate, gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate, gridPlanSezonZadKnitMachineColumnYearMonthText, gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt });
            gridViewPlanSezonZadKnitMachine.GridControl = gridControlPlanSezonZadKnitMachine;
            gridViewPlanSezonZadKnitMachine.GroupCount = 1;
            gridViewPlanSezonZadKnitMachine.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "hoursTotal", gridPlanSezonZadKnitMachineColumnHoursTotal, "(ч/ч: {0:0.##})") });
            gridViewPlanSezonZadKnitMachine.Name = "gridViewPlanSezonZadKnitMachine";
            gridViewPlanSezonZadKnitMachine.OptionsView.EnableAppearanceEvenRow = true;
            gridViewPlanSezonZadKnitMachine.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            gridViewPlanSezonZadKnitMachine.OptionsView.ShowFooter = true;
            gridViewPlanSezonZadKnitMachine.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridPlanSezonZadKnitMachineColumnKmlNumber, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridPlanSezonZadKnitMachineColumnYearNumberPlanDate, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate, DevExpress.Data.ColumnSortOrder.Ascending) });
            // 
            // gridPlanSezonZadKnitMachineColumnArticul
            // 
            gridPlanSezonZadKnitMachineColumnArticul.Caption = "артикул";
            gridPlanSezonZadKnitMachineColumnArticul.Name = "gridPlanSezonZadKnitMachineColumnArticul";
            gridPlanSezonZadKnitMachineColumnArticul.Visible = true;
            gridPlanSezonZadKnitMachineColumnArticul.VisibleIndex = 0;
            gridPlanSezonZadKnitMachineColumnArticul.Width = 83;
            // 
            // gridPlanSezonZadKnitMachineColumnPszkmPszNom
            // 
            gridPlanSezonZadKnitMachineColumnPszkmPszNom.Caption = "№ задания";
            gridPlanSezonZadKnitMachineColumnPszkmPszNom.Name = "gridPlanSezonZadKnitMachineColumnPszkmPszNom";
            gridPlanSezonZadKnitMachineColumnPszkmPszNom.Visible = true;
            gridPlanSezonZadKnitMachineColumnPszkmPszNom.VisibleIndex = 1;
            // 
            // gridPlanSezonZadKnitMachineColumnHoursTotal
            // 
            gridPlanSezonZadKnitMachineColumnHoursTotal.Caption = "ч/ч";
            gridPlanSezonZadKnitMachineColumnHoursTotal.Name = "gridPlanSezonZadKnitMachineColumnHoursTotal";
            gridPlanSezonZadKnitMachineColumnHoursTotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "hoursTotal", "(ч/ч: {0:0.##})") });
            gridPlanSezonZadKnitMachineColumnHoursTotal.Visible = true;
            gridPlanSezonZadKnitMachineColumnHoursTotal.VisibleIndex = 2;
            // 
            // gridPlanSezonZadKnitMachineColumnDateZap
            // 
            gridPlanSezonZadKnitMachineColumnDateZap.Caption = "МЗ";
            gridPlanSezonZadKnitMachineColumnDateZap.Name = "gridPlanSezonZadKnitMachineColumnDateZap";
            gridPlanSezonZadKnitMachineColumnDateZap.Visible = true;
            gridPlanSezonZadKnitMachineColumnDateZap.VisibleIndex = 3;
            // 
            // gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom
            // 
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom.Caption = "дата запуска";
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom.Name = "gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom";
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom.Visible = true;
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom.VisibleIndex = 4;
            // 
            // gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo
            // 
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo.Caption = "дата окончания";
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo.Name = "gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo";
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo.Visible = true;
            gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo.VisibleIndex = 5;
            // 
            // gridPlanSezonZadKnitMachineColumnYearMonthPlanDate
            // 
            gridPlanSezonZadKnitMachineColumnYearMonthPlanDate.Caption = "мес/год план.";
            gridPlanSezonZadKnitMachineColumnYearMonthPlanDate.Name = "gridPlanSezonZadKnitMachineColumnYearMonthPlanDate";
            gridPlanSezonZadKnitMachineColumnYearMonthPlanDate.Visible = true;
            gridPlanSezonZadKnitMachineColumnYearMonthPlanDate.VisibleIndex = 6;
            // 
            // gridPlanSezonZadKnitMachineColumnKmlNumber
            // 
            gridPlanSezonZadKnitMachineColumnKmlNumber.Caption = "№ в/м";
            gridPlanSezonZadKnitMachineColumnKmlNumber.Name = "gridPlanSezonZadKnitMachineColumnKmlNumber";
            gridPlanSezonZadKnitMachineColumnKmlNumber.Visible = true;
            gridPlanSezonZadKnitMachineColumnKmlNumber.VisibleIndex = 0;
            // 
            // gridPlanSezonZadKnitMachineColumnYearNumberPlanDate
            // 
            gridPlanSezonZadKnitMachineColumnYearNumberPlanDate.Caption = "год план";
            gridPlanSezonZadKnitMachineColumnYearNumberPlanDate.Name = "gridPlanSezonZadKnitMachineColumnYearNumberPlanDate";
            gridPlanSezonZadKnitMachineColumnYearNumberPlanDate.Visible = true;
            gridPlanSezonZadKnitMachineColumnYearNumberPlanDate.VisibleIndex = 8;
            // 
            // gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate
            // 
            gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate.Caption = "мес план";
            gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate.Name = "gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate";
            gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate.Visible = true;
            gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate.VisibleIndex = 7;
            // 
            // gridPlanSezonZadKnitMachineColumnYearMonthText
            // 
            gridPlanSezonZadKnitMachineColumnYearMonthText.Caption = "мг";
            gridPlanSezonZadKnitMachineColumnYearMonthText.FieldName = "gridPlanSezonZadKnitMachineColumnYearMonthText";
            gridPlanSezonZadKnitMachineColumnYearMonthText.Name = "gridPlanSezonZadKnitMachineColumnYearMonthText";
            gridPlanSezonZadKnitMachineColumnYearMonthText.UnboundDataType = typeof(string);
            gridPlanSezonZadKnitMachineColumnYearMonthText.Visible = true;
            gridPlanSezonZadKnitMachineColumnYearMonthText.VisibleIndex = 9;
            // 
            // gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt
            // 
            gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt.Caption = "мес/год";
            gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt.FieldName = "gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt";
            gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt.Name = "gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt";
            // 
            // labelKmlNumber
            // 
            labelKmlNumber.Font = new System.Drawing.Font("Arial", 10F);
            labelKmlNumber.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            labelKmlNumber.Location = new System.Drawing.Point(12, 12);
            labelKmlNumber.Name = "labelKmlNumber";
            labelKmlNumber.Size = new System.Drawing.Size(819, 46);
            labelKmlNumber.TabIndex = 1;
            labelKmlNumber.Text = "customLabel1";
            labelKmlNumber.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(customGridControl1);
            layoutControl1.Controls.Add(gridControlPlanSezonZadKnitMachine);
            layoutControl1.Controls.Add(labelKmlNumber);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(246, 0, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(844, 908);
            layoutControl1.TabIndex = 2;
            layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, simpleSeparator1, layoutControlItem2, simpleSeparator3, emptySpaceItem1, layoutControlItem3, simpleSeparator2 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(844, 908);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = labelKmlNumber;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(823, 50);
            layoutControlItem1.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            simpleSeparator1.Location = new System.Drawing.Point(823, 0);
            simpleSeparator1.Name = "simpleSeparator1";
            simpleSeparator1.Size = new System.Drawing.Size(1, 50);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = gridControlPlanSezonZadKnitMachine;
            layoutControlItem2.Location = new System.Drawing.Point(0, 50);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(824, 521);
            layoutControlItem2.TextVisible = false;
            // 
            // simpleSeparator3
            // 
            simpleSeparator3.Location = new System.Drawing.Point(0, 887);
            simpleSeparator3.Name = "simpleSeparator3";
            simpleSeparator3.Size = new System.Drawing.Size(824, 1);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(566, 571);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(258, 316);
            // 
            // customGridControl1
            // 
            customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl1.Location = new System.Drawing.Point(12, 583);
            customGridControl1.MainView = gridView1;
            customGridControl1.Name = "customGridControl1";
            customGridControl1.Size = new System.Drawing.Size(561, 312);
            customGridControl1.TabIndex = 4;
            customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(129, 199, 132);
            gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            gridView1.GridControl = customGridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = customGridControl1;
            layoutControlItem3.Location = new System.Drawing.Point(0, 571);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(565, 316);
            layoutControlItem3.TextVisible = false;
            // 
            // simpleSeparator2
            // 
            simpleSeparator2.Location = new System.Drawing.Point(565, 571);
            simpleSeparator2.Name = "simpleSeparator2";
            simpleSeparator2.Size = new System.Drawing.Size(1, 316);
            // 
            // KnittingMachinesUnitLoading
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(844, 908);
            Controls.Add(layoutControl1);
            Name = "KnittingMachinesUnitLoading";
            Text = "Загруз В/М";
            Load += KnittingMachinesUnitLoading_Load;
            ((System.ComponentModel.ISupportInitialize)gridControlPlanSezonZadKnitMachine).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPlanSezonZadKnitMachine).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CustomGridControl gridControlPlanSezonZadKnitMachine;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPlanSezonZadKnitMachine;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnArticul;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnPszkmPszNom;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnHoursTotal;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnDateZap;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnYearMonthPlanDate;
        private CustomLabel labelKmlNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnKmlNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnYearNumberPlanDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnYearMonthText;
        private DevExpress.XtraGrid.Columns.GridColumn gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private CustomGridControl customGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
    }
}