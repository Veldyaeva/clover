using System.Windows.Forms;
using SewingProduction;
using SewingProduction.Core.interfaces;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    partial class KnitterWorkSpace : CustomForm, IServiceBrokerHost
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
                components?.Dispose();
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KnitterWorkSpace));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions3 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions4 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions5 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            gridBand39 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn20 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand40 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand41 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn13 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand42 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand43 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn16 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand44 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn17 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand45 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn21 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand46 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn23 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand27 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand18 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand52 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn26 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand29 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand47 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn22 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn31 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand51 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn27 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand56 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand48 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn18 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand49 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn19 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand50 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn24 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            PlanZagrVyazGridControl = new SewingProduction.Core.Class.CustomGridControl();
            bandedGridView3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn28 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn25 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn33 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            FioGridLookUpEdit = new DevExpress.XtraEditors.GridLookUpEdit();
            FioGridLookUpEditView = new DevExpress.XtraGrid.Views.Grid.GridView();
            dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            textEdit1 = new DevExpress.XtraEditors.TextEdit();
            dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            TabGridLookUpEdit = new DevExpress.XtraEditors.GridLookUpEdit();
            gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            gridBand20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand22 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand23 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand24 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand15 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand16 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand17 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand19 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand14 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand13 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand12 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            bandedGridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand25 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand26 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            bandedGridColumn32 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridBand30 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand34 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand32 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand35 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand36 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBandQty = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand31 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand37 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand38 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand53 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand54 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand55 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand28 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand33 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)advBandedGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PlanZagrVyazGridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bandedGridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FioGridLookUpEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FioGridLookUpEditView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).BeginInit();
            dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TabGridLookUpEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLookUpEdit1View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            SuspendLayout();
            // 
            // advBandedGridView1
            // 
            advBandedGridView1.Appearance.BandPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            advBandedGridView1.Appearance.BandPanel.Options.UseBackColor = true;
            advBandedGridView1.Appearance.BandPanel.Options.UseFont = true;
            advBandedGridView1.Appearance.BandPanel.Options.UseTextOptions = true;
            advBandedGridView1.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            advBandedGridView1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand39, gridBand40, gridBand41, gridBand42, gridBand43, gridBand44, gridBand45, gridBand46, gridBand27, gridBand29, gridBand56 });
            advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { bandedGridColumn11, bandedGridColumn12, bandedGridColumn13, bandedGridColumn14, bandedGridColumn16, bandedGridColumn15, bandedGridColumn17, bandedGridColumn26, bandedGridColumn18, bandedGridColumn19, bandedGridColumn20, bandedGridColumn21, bandedGridColumn22, bandedGridColumn23, bandedGridColumn27, bandedGridColumn24, bandedGridColumn31 });
            advBandedGridView1.DetailHeight = 4038;
            advBandedGridView1.GridControl = PlanZagrVyazGridControl;
            advBandedGridView1.GroupFormat = "{1}";
            advBandedGridView1.GroupRowHeight = 40;
            advBandedGridView1.IndicatorWidth = 10;
            advBandedGridView1.Name = "advBandedGridView1";
            advBandedGridView1.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            advBandedGridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            advBandedGridView1.OptionsBehavior.AutoExpandAllGroups = true;
            advBandedGridView1.OptionsBehavior.SummariesIgnoreNullValues = true;
            advBandedGridView1.OptionsDetail.AllowExpandEmptyDetails = true;
            advBandedGridView1.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
            advBandedGridView1.OptionsDetail.AllowZoomDetail = false;
            advBandedGridView1.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            advBandedGridView1.OptionsDetail.EnableMasterViewMode = false;
            advBandedGridView1.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.CheckAllDetails;
            advBandedGridView1.OptionsMenu.ShowSummaryItemMode = DevExpress.Utils.DefaultBoolean.True;
            advBandedGridView1.OptionsPrint.MaxMergedCellHeight = 1080;
            advBandedGridView1.OptionsView.ColumnAutoWidth = true;
            advBandedGridView1.OptionsView.ShowColumnHeaders = false;
            advBandedGridView1.OptionsView.ShowDetailButtons = false;
            advBandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand39
            // 
            gridBand39.Caption = "таб. №";
            gridBand39.Columns.Add(bandedGridColumn20);
            gridBand39.Name = "gridBand39";
            gridBand39.OptionsBand.FixedWidth = true;
            gridBand39.VisibleIndex = 0;
            gridBand39.Width = 27;
            // 
            // bandedGridColumn20
            // 
            bandedGridColumn20.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            bandedGridColumn20.AppearanceCell.ForeColor = System.Drawing.Color.Silver;
            bandedGridColumn20.AppearanceCell.Options.UseFont = true;
            bandedGridColumn20.AppearanceCell.Options.UseForeColor = true;
            bandedGridColumn20.Caption = "таб. №";
            bandedGridColumn20.FieldName = "pzvTab";
            bandedGridColumn20.Name = "bandedGridColumn20";
            bandedGridColumn20.Visible = true;
            bandedGridColumn20.Width = 27;
            // 
            // gridBand40
            // 
            gridBand40.Caption = "№ операции";
            gridBand40.Columns.Add(bandedGridColumn11);
            gridBand40.Name = "gridBand40";
            gridBand40.OptionsBand.FixedWidth = true;
            gridBand40.VisibleIndex = 1;
            gridBand40.Width = 22;
            // 
            // bandedGridColumn11
            // 
            bandedGridColumn11.Caption = "№ операции";
            bandedGridColumn11.FieldName = "DisplayNumber";
            bandedGridColumn11.Name = "bandedGridColumn11";
            bandedGridColumn11.Visible = true;
            bandedGridColumn11.Width = 22;
            // 
            // gridBand41
            // 
            gridBand41.Caption = "Наименование операции";
            gridBand41.Columns.Add(bandedGridColumn13);
            gridBand41.Name = "gridBand41";
            gridBand41.OptionsBand.FixedWidth = true;
            gridBand41.RowCount = 2;
            gridBand41.VisibleIndex = 2;
            gridBand41.Width = 585;
            // 
            // bandedGridColumn13
            // 
            bandedGridColumn13.Caption = "Наименование операции";
            bandedGridColumn13.FieldName = "nrText";
            bandedGridColumn13.Name = "bandedGridColumn13";
            bandedGridColumn13.OptionsColumn.FixedWidth = true;
            bandedGridColumn13.Visible = true;
            bandedGridColumn13.Width = 585;
            // 
            // gridBand42
            // 
            gridBand42.Caption = "Разряд";
            gridBand42.Columns.Add(bandedGridColumn14);
            gridBand42.Name = "gridBand42";
            gridBand42.OptionsBand.FixedWidth = true;
            gridBand42.VisibleIndex = 3;
            gridBand42.Width = 29;
            // 
            // bandedGridColumn14
            // 
            bandedGridColumn14.Caption = "Разряд";
            bandedGridColumn14.FieldName = "nrRazryd";
            bandedGridColumn14.Name = "bandedGridColumn14";
            bandedGridColumn14.Visible = true;
            bandedGridColumn14.Width = 29;
            // 
            // gridBand43
            // 
            gridBand43.Caption = "Класс в/м";
            gridBand43.Columns.Add(bandedGridColumn16);
            gridBand43.Name = "gridBand43";
            gridBand43.OptionsBand.FixedWidth = true;
            gridBand43.VisibleIndex = 4;
            gridBand43.Width = 49;
            // 
            // bandedGridColumn16
            // 
            bandedGridColumn16.Caption = "Класс в/м";
            bandedGridColumn16.FieldName = "nrObor";
            bandedGridColumn16.Name = "bandedGridColumn16";
            bandedGridColumn16.Visible = true;
            bandedGridColumn16.Width = 49;
            // 
            // gridBand44
            // 
            gridBand44.AppearanceHeader.Options.UseTextOptions = true;
            gridBand44.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridBand44.Caption = "Норма вяз. 1 шт/сек";
            gridBand44.Columns.Add(bandedGridColumn17);
            gridBand44.Name = "gridBand44";
            gridBand44.OptionsBand.FixedWidth = true;
            gridBand44.VisibleIndex = 5;
            gridBand44.Width = 44;
            // 
            // bandedGridColumn17
            // 
            bandedGridColumn17.Caption = "Н. вр. вязания 1 шт/сек";
            bandedGridColumn17.FieldName = "pzvSek";
            bandedGridColumn17.Name = "bandedGridColumn17";
            bandedGridColumn17.Visible = true;
            bandedGridColumn17.Width = 44;
            // 
            // gridBand45
            // 
            gridBand45.AppearanceHeader.Options.UseTextOptions = true;
            gridBand45.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridBand45.Caption = "Норма обсл. 1шт/сек";
            gridBand45.Columns.Add(bandedGridColumn21);
            gridBand45.Name = "gridBand45";
            gridBand45.OptionsBand.FixedWidth = true;
            gridBand45.VisibleIndex = 6;
            gridBand45.Width = 44;
            // 
            // bandedGridColumn21
            // 
            bandedGridColumn21.Caption = "Н. вр. обслуж. 1шт/сек";
            bandedGridColumn21.FieldName = "koefObServ";
            bandedGridColumn21.Name = "bandedGridColumn21";
            bandedGridColumn21.Visible = true;
            bandedGridColumn21.Width = 44;
            // 
            // gridBand46
            // 
            gridBand46.AppearanceHeader.Options.UseTextOptions = true;
            gridBand46.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridBand46.Caption = "Норма обсл. факт в ч/ч";
            gridBand46.Columns.Add(bandedGridColumn23);
            gridBand46.Name = "gridBand46";
            gridBand46.OptionsBand.FixedWidth = true;
            gridBand46.VisibleIndex = 7;
            gridBand46.Width = 41;
            // 
            // bandedGridColumn23
            // 
            bandedGridColumn23.Caption = "Н. вр. обсл. факт в ч/ч";
            bandedGridColumn23.Name = "bandedGridColumn23";
            bandedGridColumn23.Visible = true;
            bandedGridColumn23.Width = 41;
            // 
            // gridBand27
            // 
            gridBand27.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            gridBand27.AppearanceHeader.Options.UseBackColor = true;
            gridBand27.Caption = "План";
            gridBand27.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand18, gridBand52 });
            gridBand27.Name = "gridBand27";
            gridBand27.OptionsBand.FixedWidth = true;
            gridBand27.VisibleIndex = 8;
            gridBand27.Width = 127;
            // 
            // gridBand18
            // 
            gridBand18.Caption = "Кол-во к выполнению";
            gridBand18.Columns.Add(bandedGridColumn12);
            gridBand18.Columns.Add(bandedGridColumn15);
            gridBand18.Name = "gridBand18";
            gridBand18.OptionsBand.FixedWidth = true;
            gridBand18.VisibleIndex = 0;
            gridBand18.Width = 58;
            // 
            // bandedGridColumn12
            // 
            bandedGridColumn12.Caption = "n1";
            bandedGridColumn12.FieldName = "nrN1";
            bandedGridColumn12.Name = "bandedGridColumn12";
            bandedGridColumn12.Width = 28;
            // 
            // bandedGridColumn15
            // 
            bandedGridColumn15.Caption = "Кол-во к выполнению";
            bandedGridColumn15.FieldName = "PlanKol_UI";
            bandedGridColumn15.Name = "bandedGridColumn15";
            bandedGridColumn15.Visible = true;
            bandedGridColumn15.Width = 58;
            // 
            // gridBand52
            // 
            gridBand52.Caption = "Часы назн";
            gridBand52.Columns.Add(bandedGridColumn26);
            gridBand52.Name = "gridBand52";
            gridBand52.OptionsBand.FixedWidth = true;
            gridBand52.VisibleIndex = 1;
            gridBand52.Width = 69;
            // 
            // bandedGridColumn26
            // 
            bandedGridColumn26.Caption = "Часы назн";
            bandedGridColumn26.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bandedGridColumn26.FieldName = "PlanChas_UI";
            bandedGridColumn26.Name = "bandedGridColumn26";
            bandedGridColumn26.Visible = true;
            bandedGridColumn26.Width = 69;
            // 
            // gridBand29
            // 
            gridBand29.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
            gridBand29.AppearanceHeader.Options.UseBackColor = true;
            gridBand29.Caption = "Факт";
            gridBand29.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand47, gridBand51 });
            gridBand29.Name = "gridBand29";
            gridBand29.OptionsBand.FixedWidth = true;
            gridBand29.VisibleIndex = 9;
            gridBand29.Width = 142;
            // 
            // gridBand47
            // 
            gridBand47.Caption = "Кол-во факт (шт)";
            gridBand47.Columns.Add(bandedGridColumn22);
            gridBand47.Columns.Add(bandedGridColumn31);
            gridBand47.Name = "gridBand47";
            gridBand47.OptionsBand.FixedWidth = true;
            gridBand47.VisibleIndex = 0;
            gridBand47.Width = 66;
            // 
            // bandedGridColumn22
            // 
            bandedGridColumn22.Caption = "Кол-во факт (шт)";
            bandedGridColumn22.FieldName = "FactKol_UI";
            bandedGridColumn22.Name = "bandedGridColumn22";
            bandedGridColumn22.Visible = true;
            bandedGridColumn22.Width = 66;
            // 
            // bandedGridColumn31
            // 
            bandedGridColumn31.Caption = "pzvRKol";
            bandedGridColumn31.FieldName = "pzvRKol";
            bandedGridColumn31.Name = "bandedGridColumn31";
            bandedGridColumn31.Width = 59;
            // 
            // gridBand51
            // 
            gridBand51.Caption = "Часы факт";
            gridBand51.Columns.Add(bandedGridColumn27);
            gridBand51.Name = "gridBand51";
            gridBand51.OptionsBand.FixedWidth = true;
            gridBand51.VisibleIndex = 1;
            gridBand51.Width = 76;
            // 
            // bandedGridColumn27
            // 
            bandedGridColumn27.Caption = "Часы факт";
            bandedGridColumn27.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bandedGridColumn27.FieldName = "FactChas_UI";
            bandedGridColumn27.Name = "bandedGridColumn27";
            bandedGridColumn27.Visible = true;
            bandedGridColumn27.Width = 76;
            // 
            // gridBand56
            // 
            gridBand56.Caption = "Даты";
            gridBand56.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand48, gridBand49, gridBand50 });
            gridBand56.Name = "gridBand56";
            gridBand56.OptionsBand.FixedWidth = true;
            gridBand56.VisibleIndex = 10;
            gridBand56.Width = 146;
            // 
            // gridBand48
            // 
            gridBand48.Caption = "Начато";
            gridBand48.Columns.Add(bandedGridColumn18);
            gridBand48.Name = "gridBand48";
            gridBand48.OptionsBand.FixedWidth = true;
            gridBand48.VisibleIndex = 0;
            gridBand48.Width = 45;
            // 
            // bandedGridColumn18
            // 
            bandedGridColumn18.Caption = "Начато";
            bandedGridColumn18.FieldName = "pzvDateStart";
            bandedGridColumn18.Name = "bandedGridColumn18";
            bandedGridColumn18.Visible = true;
            bandedGridColumn18.Width = 45;
            // 
            // gridBand49
            // 
            gridBand49.Caption = "Закончено";
            gridBand49.Columns.Add(bandedGridColumn19);
            gridBand49.Name = "gridBand49";
            gridBand49.OptionsBand.FixedWidth = true;
            gridBand49.VisibleIndex = 1;
            gridBand49.Width = 44;
            // 
            // bandedGridColumn19
            // 
            bandedGridColumn19.Caption = "Закончено";
            bandedGridColumn19.FieldName = "pzvDateEnd";
            bandedGridColumn19.Name = "bandedGridColumn19";
            bandedGridColumn19.Visible = true;
            bandedGridColumn19.Width = 44;
            // 
            // gridBand50
            // 
            gridBand50.Caption = "Подтв. маст. ";
            gridBand50.Columns.Add(bandedGridColumn24);
            gridBand50.Name = "gridBand50";
            gridBand50.OptionsBand.FixedWidth = true;
            gridBand50.VisibleIndex = 2;
            gridBand50.Width = 57;
            // 
            // bandedGridColumn24
            // 
            bandedGridColumn24.Caption = "Подтв. маст. ";
            bandedGridColumn24.FieldName = "pzvDateMast";
            bandedGridColumn24.Name = "bandedGridColumn24";
            bandedGridColumn24.Visible = true;
            bandedGridColumn24.Width = 57;
            // 
            // PlanZagrVyazGridControl
            // 
            PlanZagrVyazGridControl.Font = new System.Drawing.Font("Arial", 10F);
            gridLevelNode1.LevelTemplate = advBandedGridView1;
            gridLevelNode1.RelationName = "ArtNom";
            PlanZagrVyazGridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode1 });
            PlanZagrVyazGridControl.Location = new System.Drawing.Point(24, 81);
            PlanZagrVyazGridControl.MainView = bandedGridView3;
            PlanZagrVyazGridControl.Name = "PlanZagrVyazGridControl";
            PlanZagrVyazGridControl.Size = new System.Drawing.Size(1281, 512);
            PlanZagrVyazGridControl.TabIndex = 6;
            PlanZagrVyazGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { bandedGridView3, advBandedGridView1 });
            // 
            // bandedGridView3
            // 
            bandedGridView3.Appearance.BandPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            bandedGridView3.Appearance.BandPanel.Options.UseFont = true;
            bandedGridView3.Appearance.BandPanel.Options.UseTextOptions = true;
            bandedGridView3.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView3.Appearance.EvenRow.BackColor = System.Drawing.Color.LightBlue;
            bandedGridView3.Appearance.EvenRow.Options.UseBackColor = true;
            bandedGridView3.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightBlue;
            bandedGridView3.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            bandedGridView3.Appearance.FocusedRow.Options.UseBackColor = true;
            bandedGridView3.Appearance.FocusedRow.Options.UseFont = true;
            bandedGridView3.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand30, gridBand34, gridBand32, gridBand35, gridBand36, gridBandQty, gridBand31, gridBand53, gridBand28, gridBand33 });
            bandedGridView3.ChildGridLevelName = "ArtNom";
            bandedGridView3.ColumnPanelRowHeight = 10;
            bandedGridView3.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { gridColumn1, gridColumn2, gridColumn3, gridColumn4, gridColumn5, gridColumn6, bandedGridColumn33, gridColumn7, gridColumn10, bandedGridColumn28, bandedGridColumn25, gridColumn8, gridColumn9 });
            bandedGridView3.DetailHeight = 4038;
            bandedGridView3.GridControl = PlanZagrVyazGridControl;
            bandedGridView3.IndicatorWidth = 45;
            bandedGridView3.Name = "bandedGridView3";
            bandedGridView3.OptionsDetail.AllowExpandEmptyDetails = true;
            bandedGridView3.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
            bandedGridView3.OptionsDetail.AllowZoomDetail = false;
            bandedGridView3.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            bandedGridView3.OptionsDetail.ShowEmbeddedDetailIndent = DevExpress.Utils.DefaultBoolean.True;
            bandedGridView3.OptionsView.EnableAppearanceEvenRow = true;
            bandedGridView3.OptionsView.ShowColumnHeaders = false;
            bandedGridView3.OptionsView.ShowFooter = true;
            bandedGridView3.OptionsView.ShowGroupPanel = false;
            bandedGridView3.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending) });
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "№ В/м";
            gridColumn1.FieldName = "kmlNumber";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.Width = 138;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "Класс вязания";
            gridColumn3.FieldName = "name_class";
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.Width = 90;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "Артикул";
            gridColumn2.FieldName = "pzvArticul";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.Width = 173;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "№ задания";
            gridColumn4.FieldName = "pzvNomZad";
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.Width = 168;
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "№ рассчёта";
            gridColumn5.FieldName = "pzvNom";
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.Width = 176;
            // 
            // gridColumn10
            // 
            gridColumn10.Caption = "Кол-во";
            gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridColumn10.FieldName = "pzvKolNazn";
            gridColumn10.Name = "gridColumn10";
            gridColumn10.Width = 74;
            // 
            // gridColumn6
            // 
            gridColumn6.Caption = "назначено в м/ч";
            gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridColumn6.FieldName = "pzvChasNazn";
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Visible = true;
            gridColumn6.Width = 128;
            // 
            // gridColumn7
            // 
            gridColumn7.Caption = "назначено в ч/ч";
            gridColumn7.FieldName = "gridColumn7";
            gridColumn7.Name = "gridColumn7";
            gridColumn7.UnboundDataType = typeof(decimal);
            gridColumn7.UnboundExpression = "Round([pzvChasNazn] * [koefObServ], 2)";
            gridColumn7.Visible = true;
            gridColumn7.Width = 132;
            // 
            // bandedGridColumn28
            // 
            bandedGridColumn28.Caption = "факт в м/ч";
            bandedGridColumn28.FieldName = "pzvNChasi";
            bandedGridColumn28.Name = "bandedGridColumn28";
            bandedGridColumn28.Visible = true;
            bandedGridColumn28.Width = 144;
            // 
            // bandedGridColumn25
            // 
            bandedGridColumn25.Caption = "факт в ч/ч";
            bandedGridColumn25.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bandedGridColumn25.FieldName = "bandedGridColumn25";
            bandedGridColumn25.Name = "bandedGridColumn25";
            bandedGridColumn25.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "bandedGridColumn25", "{0:0.##}") });
            bandedGridColumn25.UnboundDataType = typeof(decimal);
            bandedGridColumn25.UnboundExpression = "Round([pzvNChasi] * [koefObServ], 2)";
            bandedGridColumn25.Visible = true;
            bandedGridColumn25.Width = 116;
            // 
            // gridColumn8
            // 
            gridColumn8.Caption = "Статус";
            gridColumn8.Name = "gridColumn8";
            gridColumn8.Visible = true;
            gridColumn8.Width = 324;
            // 
            // gridColumn9
            // 
            gridColumn9.Caption = "Время статуса";
            gridColumn9.Name = "gridColumn9";
            gridColumn9.Visible = true;
            gridColumn9.Width = 20;
            // 
            // bandedGridColumn33
            // 
            bandedGridColumn33.Caption = "коэф. обсл";
            bandedGridColumn33.FieldName = "koefObServ";
            bandedGridColumn33.Name = "bandedGridColumn33";
            // 
            // FioGridLookUpEdit
            // 
            FioGridLookUpEdit.Location = new System.Drawing.Point(357, 12);
            FioGridLookUpEdit.Name = "FioGridLookUpEdit";
            FioGridLookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            FioGridLookUpEdit.Properties.Appearance.Options.UseFont = true;
            FioGridLookUpEdit.Properties.NullText = "[Выберите сотрудника]";
            FioGridLookUpEdit.Properties.PopupView = FioGridLookUpEditView;
            FioGridLookUpEdit.Size = new System.Drawing.Size(175, 32);
            FioGridLookUpEdit.StyleController = dataLayoutControl1;
            FioGridLookUpEdit.TabIndex = 2;
            FioGridLookUpEdit.EditValueChanged += FioGridLookUpEdit_EditValueChanged;
            // 
            // FioGridLookUpEditView
            // 
            FioGridLookUpEditView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            FioGridLookUpEditView.Name = "FioGridLookUpEditView";
            FioGridLookUpEditView.OptionsSelection.EnableAppearanceFocusedCell = false;
            FioGridLookUpEditView.OptionsView.ShowGroupPanel = false;
            // 
            // dataLayoutControl1
            // 
            dataLayoutControl1.Controls.Add(PlanZagrVyazGridControl);
            dataLayoutControl1.Controls.Add(FioGridLookUpEdit);
            dataLayoutControl1.Controls.Add(textEdit1);
            dataLayoutControl1.Controls.Add(dateEdit1);
            dataLayoutControl1.Controls.Add(simpleButton2);
            dataLayoutControl1.Controls.Add(TabGridLookUpEdit);
            dataLayoutControl1.Dock = DockStyle.Fill;
            dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            dataLayoutControl1.Name = "dataLayoutControl1";
            dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(962, 395, 650, 400);
            dataLayoutControl1.Root = Root;
            dataLayoutControl1.Size = new System.Drawing.Size(1329, 617);
            dataLayoutControl1.TabIndex = 2;
            dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // textEdit1
            // 
            textEdit1.Location = new System.Drawing.Point(74, 12);
            textEdit1.Name = "textEdit1";
            textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            textEdit1.Properties.Appearance.Options.UseFont = true;
            textEdit1.Size = new System.Drawing.Size(124, 32);
            textEdit1.StyleController = dataLayoutControl1;
            textEdit1.TabIndex = 0;
            // 
            // dateEdit1
            // 
            dateEdit1.EditValue = null;
            dateEdit1.Location = new System.Drawing.Point(784, 12);
            dateEdit1.Name = "dateEdit1";
            dateEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            dateEdit1.Properties.Appearance.Options.UseFont = true;
            dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEdit1.Properties.MaskSettings.Set("mask", "D");
            dateEdit1.Size = new System.Drawing.Size(178, 32);
            dateEdit1.StyleController = dataLayoutControl1;
            dateEdit1.TabIndex = 4;
            // 
            // simpleButton2
            // 
            simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            simpleButton2.Appearance.Options.UseFont = true;
            simpleButton2.Location = new System.Drawing.Point(966, 12);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new System.Drawing.Size(159, 30);
            simpleButton2.StyleController = dataLayoutControl1;
            simpleButton2.TabIndex = 5;
            simpleButton2.Text = "Начать смену";
            simpleButton2.Click += simpleButton2_Click;
            simpleButton2.DoubleClick += simpleButton2_Click;
            // 
            // TabGridLookUpEdit
            // 
            TabGridLookUpEdit.Location = new System.Drawing.Point(605, 12);
            TabGridLookUpEdit.Name = "TabGridLookUpEdit";
            TabGridLookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            TabGridLookUpEdit.Properties.Appearance.Options.UseFont = true;
            TabGridLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            TabGridLookUpEdit.Properties.Name = "TabGridLookUpEdit";
            TabGridLookUpEdit.Properties.NullText = "";
            TabGridLookUpEdit.Properties.PopupSizeable = false;
            TabGridLookUpEdit.Properties.PopupView = gridLookUpEdit1View;
            TabGridLookUpEdit.Size = new System.Drawing.Size(125, 32);
            TabGridLookUpEdit.StyleController = dataLayoutControl1;
            TabGridLookUpEdit.TabIndex = 3;
            // 
            // gridLookUpEdit1View
            // 
            gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3, layoutControlItem4, layoutControlItem9, layoutControlItem5, emptySpaceItem1, simpleLabelItem1, layoutControlGroup1 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1329, 617);
            Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem2.Control = FioGridLookUpEdit;
            layoutControlItem2.CustomizationFormText = "ФИО оператора - ";
            layoutControlItem2.Location = new System.Drawing.Point(190, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(334, 36);
            layoutControlItem2.Text = "ФИО оператора -";
            layoutControlItem2.TextSize = new System.Drawing.Size(143, 19);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem3.Control = textEdit1;
            layoutControlItem3.CustomizationFormText = "Зона - ";
            layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(190, 36);
            layoutControlItem3.Text = "Зона - ";
            layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem3.TextSize = new System.Drawing.Size(57, 19);
            layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem4.Control = dateEdit1;
            layoutControlItem4.Location = new System.Drawing.Point(722, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(232, 36);
            layoutControlItem4.Text = "Дата";
            layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem4.TextSize = new System.Drawing.Size(45, 13);
            layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = simpleButton2;
            layoutControlItem9.Location = new System.Drawing.Point(954, 0);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new System.Drawing.Size(163, 36);
            layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem5.Control = TabGridLookUpEdit;
            layoutControlItem5.CustomizationFormText = "таб №  ";
            layoutControlItem5.Location = new System.Drawing.Point(524, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(198, 36);
            layoutControlItem5.Text = "таб №  ";
            layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem5.TextSize = new System.Drawing.Size(64, 19);
            layoutControlItem5.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(1117, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(21, 36);
            // 
            // simpleLabelItem1
            // 
            simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItem1.Location = new System.Drawing.Point(1138, 0);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(171, 36);
            simpleLabelItem1.Text = " ";
            simpleLabelItem1.TextSize = new System.Drawing.Size(143, 19);
            // 
            // layoutControlGroup1
            // 
            buttonImageOptions1.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("buttonImageOptions1.SvgImage");
            layoutControlGroup1.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Обновить", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Свернуть всё", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Показать всё", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, false, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Свернуть до пачки", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, false, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Показать всё", true, buttonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, false, null, -1) });
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 36);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new System.Drawing.Size(1309, 561);
            layoutControlGroup1.Text = " ";
            layoutControlGroup1.CustomButtonClick += layoutControlGroup1_CustomButtonClick;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = PlanZagrVyazGridControl;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(1285, 516);
            layoutControlItem1.TextVisible = false;
            // 
            // gridBand20
            // 
            gridBand20.Caption = "gridBand20";
            gridBand20.Name = "gridBand20";
            gridBand20.VisibleIndex = 0;
            gridBand20.Width = 37;
            // 
            // gridBand21
            // 
            gridBand21.Caption = "gridBand21";
            gridBand21.Name = "gridBand21";
            gridBand21.VisibleIndex = 1;
            gridBand21.Width = 37;
            // 
            // gridBand22
            // 
            gridBand22.Caption = "gridBand22";
            gridBand22.Name = "gridBand22";
            gridBand22.VisibleIndex = 2;
            gridBand22.Width = 37;
            // 
            // gridBand23
            // 
            gridBand23.Caption = "gridBand23";
            gridBand23.Name = "gridBand23";
            gridBand23.VisibleIndex = 3;
            gridBand23.Width = 37;
            // 
            // gridBand24
            // 
            gridBand24.Caption = "gridBand24";
            gridBand24.Name = "gridBand24";
            gridBand24.VisibleIndex = 4;
            gridBand24.Width = 85;
            // 
            // gridBand15
            // 
            gridBand15.Caption = "gridBand15";
            gridBand15.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand20, gridBand21, gridBand22, gridBand23, gridBand24 });
            gridBand15.Name = "gridBand15";
            gridBand15.Width = 233;
            // 
            // gridBand16
            // 
            gridBand16.Caption = "gridBand16";
            gridBand16.Name = "gridBand16";
            gridBand16.VisibleIndex = 0;
            gridBand16.Width = 48;
            // 
            // gridBand17
            // 
            gridBand17.Caption = "gridBand17";
            gridBand17.Name = "gridBand17";
            gridBand17.VisibleIndex = 1;
            gridBand17.Width = 45;
            // 
            // gridBand19
            // 
            gridBand19.Caption = "gridBand19";
            gridBand19.Name = "gridBand19";
            gridBand19.VisibleIndex = 2;
            gridBand19.Width = 79;
            // 
            // gridBand14
            // 
            gridBand14.Caption = "gridBand14";
            gridBand14.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand16, gridBand17, gridBand19 });
            gridBand14.Name = "gridBand14";
            gridBand14.Width = 172;
            // 
            // gridBand13
            // 
            gridBand13.Caption = "gridBand13";
            gridBand13.Name = "gridBand13";
            gridBand13.Width = 42;
            // 
            // gridBand12
            // 
            gridBand12.Caption = "gridBand12";
            gridBand12.Name = "gridBand12";
            gridBand12.Width = 43;
            // 
            // gridBand11
            // 
            gridBand11.Caption = "gridBand11";
            gridBand11.Name = "gridBand11";
            gridBand11.Width = 46;
            // 
            // gridBand10
            // 
            gridBand10.Caption = "gridBand10";
            gridBand10.Name = "gridBand10";
            gridBand10.Width = 45;
            // 
            // gridBand9
            // 
            gridBand9.Caption = "gridBand9";
            gridBand9.Name = "gridBand9";
            gridBand9.Width = 45;
            // 
            // bandedGridColumn1
            // 
            bandedGridColumn1.Caption = "bandedGridColumn1";
            bandedGridColumn1.Name = "bandedGridColumn1";
            bandedGridColumn1.OptionsColumn.ShowCaption = false;
            bandedGridColumn1.Visible = true;
            bandedGridColumn1.Width = 82;
            // 
            // bandedGridColumn2
            // 
            bandedGridColumn2.Caption = "bandedGridColumn2";
            bandedGridColumn2.FieldName = "pzvArticul";
            bandedGridColumn2.Name = "bandedGridColumn2";
            bandedGridColumn2.OptionsColumn.ShowCaption = false;
            bandedGridColumn2.Visible = true;
            bandedGridColumn2.Width = 78;
            // 
            // bandedGridColumn3
            // 
            bandedGridColumn3.Caption = "bandedGridColumn3";
            bandedGridColumn3.Name = "bandedGridColumn3";
            bandedGridColumn3.OptionsColumn.ShowCaption = false;
            bandedGridColumn3.Visible = true;
            // 
            // bandedGridColumn4
            // 
            bandedGridColumn4.Caption = "bandedGridColumn4";
            bandedGridColumn4.FieldName = "pzvNomZad";
            bandedGridColumn4.Name = "bandedGridColumn4";
            bandedGridColumn4.OptionsColumn.ShowCaption = false;
            bandedGridColumn4.Visible = true;
            // 
            // bandedGridColumn5
            // 
            bandedGridColumn5.Caption = "bandedGridColumn5";
            bandedGridColumn5.FieldName = "pzvNom";
            bandedGridColumn5.Name = "bandedGridColumn5";
            bandedGridColumn5.OptionsColumn.ShowCaption = false;
            bandedGridColumn5.Visible = true;
            bandedGridColumn5.Width = 20;
            // 
            // bandedGridColumn6
            // 
            bandedGridColumn6.Caption = "в м/ч";
            bandedGridColumn6.FieldName = "pzvNChasi";
            bandedGridColumn6.Name = "bandedGridColumn6";
            bandedGridColumn6.Visible = true;
            bandedGridColumn6.Width = 104;
            // 
            // bandedGridColumn7
            // 
            bandedGridColumn7.Caption = "в ч/ч";
            bandedGridColumn7.Name = "bandedGridColumn7";
            bandedGridColumn7.Visible = true;
            bandedGridColumn7.Width = 125;
            // 
            // bandedGridColumn8
            // 
            bandedGridColumn8.Caption = "bandedGridColumn8";
            bandedGridColumn8.Name = "bandedGridColumn8";
            bandedGridColumn8.OptionsColumn.ShowCaption = false;
            bandedGridColumn8.Visible = true;
            bandedGridColumn8.Width = 82;
            // 
            // bandedGridColumn9
            // 
            bandedGridColumn9.Caption = "bandedGridColumn9";
            bandedGridColumn9.Name = "bandedGridColumn9";
            bandedGridColumn9.OptionsColumn.ShowCaption = false;
            bandedGridColumn9.Visible = true;
            bandedGridColumn9.Width = 113;
            // 
            // bandedGridColumn10
            // 
            bandedGridColumn10.Caption = "bandedGridColumn10";
            bandedGridColumn10.Name = "bandedGridColumn10";
            bandedGridColumn10.Visible = true;
            // 
            // gridBand8
            // 
            gridBand8.Caption = "Время установки статуса";
            gridBand8.Columns.Add(bandedGridColumn9);
            gridBand8.Columns.Add(bandedGridColumn10);
            gridBand8.Columns.Add(bandedGridColumn6);
            gridBand8.Columns.Add(bandedGridColumn7);
            gridBand8.Columns.Add(bandedGridColumn5);
            gridBand8.Name = "gridBand8";
            gridBand8.VisibleIndex = 0;
            gridBand8.Width = 437;
            // 
            // gridBand5
            // 
            gridBand5.Caption = "№ рассчёта";
            gridBand5.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand8 });
            gridBand5.Name = "gridBand5";
            gridBand5.VisibleIndex = 0;
            gridBand5.Width = 437;
            // 
            // gridBand4
            // 
            gridBand4.Caption = "№ задания";
            gridBand4.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand5 });
            gridBand4.Name = "gridBand4";
            gridBand4.VisibleIndex = 0;
            gridBand4.Width = 437;
            // 
            // gridBand3
            // 
            gridBand3.Caption = "Класс вязания";
            gridBand3.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand4 });
            gridBand3.Name = "gridBand3";
            gridBand3.VisibleIndex = 0;
            gridBand3.Width = 437;
            // 
            // gridBand2
            // 
            gridBand2.Caption = "Артикул";
            gridBand2.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand3 });
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            gridBand2.Width = 437;
            // 
            // gridBand1
            // 
            gridBand1.Caption = "№ В/М";
            gridBand1.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            gridBand1.Width = 437;
            // 
            // gridBand6
            // 
            gridBand6.Caption = "Часы назначено";
            gridBand6.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            gridBand6.Name = "gridBand6";
            gridBand6.VisibleIndex = 0;
            gridBand6.Width = 437;
            // 
            // gridBand7
            // 
            gridBand7.Caption = "Статус";
            gridBand7.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand6 });
            gridBand7.Name = "gridBand7";
            gridBand7.Width = 437;
            // 
            // gridBand25
            // 
            gridBand25.Name = "gridBand25";
            // 
            // gridBand26
            // 
            gridBand26.Name = "gridBand26";
            // 
            // bandedGridColumn32
            // 
            bandedGridColumn32.Caption = "Разряд";
            bandedGridColumn32.FieldName = "nrRazryd";
            bandedGridColumn32.Name = "bandedGridColumn32";
            bandedGridColumn32.Visible = true;
            bandedGridColumn32.Width = 68;
            // 
            // gridBand30
            // 
            gridBand30.Caption = "№ В/м";
            gridBand30.Columns.Add(gridColumn1);
            gridBand30.Name = "gridBand30";
            gridBand30.OptionsBand.AllowMove = false;
            gridBand30.OptionsBand.AllowSize = false;
            gridBand30.OptionsBand.FixedWidth = true;
            gridBand30.VisibleIndex = 0;
            gridBand30.Width = 138;
            // 
            // gridBand34
            // 
            gridBand34.Caption = "Класс вязания";
            gridBand34.Columns.Add(gridColumn3);
            gridBand34.Name = "gridBand34";
            gridBand34.OptionsBand.AllowSize = false;
            gridBand34.OptionsBand.FixedWidth = true;
            gridBand34.VisibleIndex = 1;
            gridBand34.Width = 90;
            // 
            // gridBand32
            // 
            gridBand32.Caption = "Артикул";
            gridBand32.Columns.Add(gridColumn2);
            gridBand32.Name = "gridBand32";
            gridBand32.OptionsBand.AllowSize = false;
            gridBand32.OptionsBand.FixedWidth = true;
            gridBand32.VisibleIndex = 2;
            gridBand32.Width = 173;
            // 
            // gridBand35
            // 
            gridBand35.Caption = "№ задания";
            gridBand35.Columns.Add(gridColumn4);
            gridBand35.Name = "gridBand35";
            gridBand35.OptionsBand.AllowSize = false;
            gridBand35.OptionsBand.FixedWidth = true;
            gridBand35.VisibleIndex = 3;
            gridBand35.Width = 168;
            // 
            // gridBand36
            // 
            gridBand36.Caption = "№ рассчёта";
            gridBand36.Columns.Add(gridColumn5);
            gridBand36.Name = "gridBand36";
            gridBand36.OptionsBand.AllowSize = false;
            gridBand36.OptionsBand.FixedWidth = true;
            gridBand36.VisibleIndex = 4;
            gridBand36.Width = 176;
            // 
            // gridBandQty
            // 
            gridBandQty.Caption = "Кол-во";
            gridBandQty.Columns.Add(gridColumn10);
            gridBandQty.Name = "gridBandQty";
            gridBandQty.OptionsBand.AllowMove = false;
            gridBandQty.OptionsBand.AllowPress = false;
            gridBandQty.OptionsBand.AllowSize = false;
            gridBandQty.OptionsBand.FixedWidth = true;
            gridBandQty.Visible = false;
            gridBandQty.Width = 74;
            // 
            // gridBand31
            // 
            gridBand31.Caption = "Часы назначено";
            gridBand31.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand37, gridBand38 });
            gridBand31.Name = "gridBand31";
            gridBand31.OptionsBand.AllowSize = false;
            gridBand31.OptionsBand.FixedWidth = true;
            gridBand31.VisibleIndex = 5;
            gridBand31.Width = 260;
            // 
            // gridBand37
            // 
            gridBand37.Caption = "назначено в м/ч";
            gridBand37.Columns.Add(gridColumn6);
            gridBand37.Name = "gridBand37";
            gridBand37.OptionsBand.AllowMove = false;
            gridBand37.OptionsBand.AllowPress = false;
            gridBand37.OptionsBand.AllowSize = false;
            gridBand37.OptionsBand.FixedWidth = true;
            gridBand37.VisibleIndex = 0;
            gridBand37.Width = 128;
            // 
            // gridBand38
            // 
            gridBand38.Caption = "назначено в ч/ч";
            gridBand38.Columns.Add(gridColumn7);
            gridBand38.Name = "gridBand38";
            gridBand38.OptionsBand.AllowMove = false;
            gridBand38.OptionsBand.AllowPress = false;
            gridBand38.OptionsBand.AllowSize = false;
            gridBand38.OptionsBand.FixedWidth = true;
            gridBand38.VisibleIndex = 1;
            gridBand38.Width = 132;
            // 
            // gridBand53
            // 
            gridBand53.Caption = "Часы факт";
            gridBand53.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand54, gridBand55 });
            gridBand53.Name = "gridBand53";
            gridBand53.OptionsBand.AllowSize = false;
            gridBand53.OptionsBand.FixedWidth = true;
            gridBand53.VisibleIndex = 6;
            gridBand53.Width = 260;
            // 
            // gridBand54
            // 
            gridBand54.Caption = "В м/ч";
            gridBand54.Columns.Add(bandedGridColumn28);
            gridBand54.Name = "gridBand54";
            gridBand54.OptionsBand.FixedWidth = true;
            gridBand54.VisibleIndex = 0;
            gridBand54.Width = 144;
            // 
            // gridBand55
            // 
            gridBand55.Caption = "В ч/ч";
            gridBand55.Columns.Add(bandedGridColumn25);
            gridBand55.Name = "gridBand55";
            gridBand55.OptionsBand.FixedWidth = true;
            gridBand55.VisibleIndex = 1;
            gridBand55.Width = 116;
            // 
            // gridBand28
            // 
            gridBand28.Caption = " Статус";
            gridBand28.Columns.Add(gridColumn8);
            gridBand28.Name = "gridBand28";
            gridBand28.OptionsBand.AllowSize = false;
            gridBand28.VisibleIndex = 7;
            gridBand28.Width = 324;
            // 
            // gridBand33
            // 
            gridBand33.Caption = "Время статуса";
            gridBand33.Columns.Add(gridColumn9);
            gridBand33.Name = "gridBand33";
            gridBand33.Visible = false;
            gridBand33.Width = 20;
            // 
            // KnitterWorkSpace
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1329, 617);
            Controls.Add(dataLayoutControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "KnitterWorkSpace";
            Text = "Рабочее место вязальщицы";
            ((System.ComponentModel.ISupportInitialize)advBandedGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PlanZagrVyazGridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)bandedGridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)FioGridLookUpEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)FioGridLookUpEditView).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).EndInit();
            dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)TabGridLookUpEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLookUpEdit1View).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GridLookUpEdit FioGridLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView FioGridLookUpEditView;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand20;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand21;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand22;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand23;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand24;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand15;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand16;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand17;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand19;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand14;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand13;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand12;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand11;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand10;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private Core.Class.CustomGridControl PlanZagrVyazGridControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand25;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand26;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn12;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn13;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn16;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn15;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn17;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn21;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn20;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn18;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn19;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn22;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn23;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn24;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn10;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.GridLookUpEdit TabGridLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn31;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn32;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn26;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn27;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn28;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn25;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn33;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand39;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand40;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand41;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand42;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand43;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand44;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand45;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand46;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand27;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand18;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand52;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand29;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand47;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand51;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand56;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand48;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand49;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand50;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand30;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand34;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand32;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand35;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand36;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandQty;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand31;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand37;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand38;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand53;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand54;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand55;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand28;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand33;
    }
}