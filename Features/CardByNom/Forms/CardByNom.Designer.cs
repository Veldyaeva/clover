using System;
using System.Drawing;
using System.Windows.Forms;
//using System.Windows.Media;
using SewingProduction.Core.Class;

namespace SewingProduction
{
    partial class CardByNom
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue5 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule6 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue6 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule7 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue7 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule8 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue8 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule9 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue9 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule10 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue10 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule11 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue11 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardByNom));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklChipInUT = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklChipPech = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklChipScan = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklChipOtgr = new DevExpress.XtraGrid.Columns.GridColumn();
            tbYearPach = new CustomTextBox();
            tbNomPach = new CustomTextBox();
            label4 = new CustomLabel();
            bsProizvCombIzdSP = new BindingSource(components);
            bsProizvCombIzdVZP = new BindingSource(components);
            gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSplitContainer1 = new DevExpress.XtraGrid.GridSplitContainer();
            OtdelkaInfo = new DevExpress.XtraTab.XtraTabPage();
            splitContainer2 = new SplitContainer();
            customGroupBox7 = new CustomGroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            gridControlProizvCombIzdSP = new CustomGridControl();
            gridViewProizvCombIzdSP = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumnProizvCombIzdSpRzuMod = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPPszNom = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPPszZvet = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPRzuArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPRzuGrup = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSpRzuRazm = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPKolItog = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPNIz = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPKolRaskr = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPKolRab = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPKolFurnPrinSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPRzuDataRab = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPNDostData = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPDateFurnPrihSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdSPKolGI = new DevExpress.XtraGrid.Columns.GridColumn();
            sbProizvCombIzdSP = new CustomSimpleButton();
            customGroupBox8 = new CustomGroupBox();
            gridControlProizvCombIzdVZP = new CustomGridControl();
            gridViewProizvCombIzdVZP = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumnProizvCombIzdVZPRzvMod = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPPszNom = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPPszZvet = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPRzvArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPRzvGrup = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPRzvRazm = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPKolItog = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPNIz = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPKolVyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPKolOtparka = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPKolGI = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPKolFurnPrinSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPRzvDateOkonV = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPNDostData = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnProizvCombIzdVZPDateFurnPrihSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            gridViewNaklList = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumnNaklCountBefore = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPrich = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklSklNaimen = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklGlNomer = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklDatePrint = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklDostN = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklDostData = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklDateIzm = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklIzDate = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklIzNakl = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklCountAfter = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklMod = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlNaklList = new CustomGridControl();
            gridViewOtdelka = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumnOtdelkaViNaim = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnOtdelkaCaption = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnOtdelkaFrtNaimen = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnOtdelkaKolSlZv = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnOtdelkaPsaFieldName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlOtdelka = new CustomGridControl();
            WorkInfo = new DevExpress.XtraTab.XtraTabPage();
            FurnInfo = new DevExpress.XtraTab.XtraTabPage();
            splitContainer1 = new SplitContainer();
            furnitZayavViewFurnit = new FurnitZayavView();
            furnitZayavViewUpak = new FurnitZayavView();
            customGroupBox6 = new CustomGroupBox();
            tablePanel7 = new DevExpress.Utils.Layout.TablePanel();
            simpleButtonFullKKPrint = new CustomSimpleButton();
            simpleButtonUpakDeliveryInfoShow = new CustomSimpleButton();
            simpleButtonFurnDeliveryInfoShow = new CustomSimpleButton();
            simpleButtonZayavUpakPrint = new CustomSimpleButton();
            simpleButtonUpakKKPrint = new CustomSimpleButton();
            simpleButtonZayavFurnPrint = new CustomSimpleButton();
            simpleButtonFurnKKPrint = new CustomSimpleButton();
            tbDatZayav = new CustomTextBox();
            tbOtgrStat = new CustomTextBox();
            mtbData_cd = new CustomMaskedTextBox();
            label61 = new CustomLabel();
            mtbData_zeh = new CustomMaskedTextBox();
            label60 = new CustomLabel();
            tbIs_got = new CustomTextBox();
            tbData_f_z_u = new CustomTextBox();
            tbFurnKKStat = new CustomTextBox();
            tbData_f_o_u = new CustomTextBox();
            label59 = new CustomLabel();
            tbData_f_z = new CustomTextBox();
            tbUpakKKStat = new CustomTextBox();
            tbUZSobrStat = new CustomTextBox();
            tbData_f_o = new CustomTextBox();
            label25 = new CustomLabel();
            label56 = new CustomLabel();
            tbFurnZayav = new CustomTextBox();
            tbUZSozdStat = new CustomTextBox();
            label54 = new CustomLabel();
            tbFZSozdStat = new CustomTextBox();
            label55 = new CustomLabel();
            label57 = new CustomLabel();
            tbFZSobrStat = new CustomTextBox();
            tbUpakZayav = new CustomTextBox();
            label58 = new CustomLabel();
            panelControl7 = new DevExpress.XtraEditors.PanelControl();
            RasInfo = new DevExpress.XtraTab.XtraTabPage();
            customGroupBox5 = new CustomGroupBox();
            tablePanel6 = new DevExpress.Utils.Layout.TablePanel();
            simpleButtonReestrListPrint = new CustomSimpleButton();
            simpleButtonPrintMLRTUpak = new CustomSimpleButton();
            simpleButtonPrintMLRTAll = new CustomSimpleButton();
            customGroupBox4 = new CustomGroupBox();
            tablePanel5 = new DevExpress.Utils.Layout.TablePanel();
            mtbRzuVidStir = new CustomMaskedTextBox();
            cbRzuStirFact = new CustomCheckBox();
            label44 = new CustomLabel();
            mtbRzuDataStCd = new CustomMaskedTextBox();
            label32 = new CustomLabel();
            mtbRzuDataStR = new CustomMaskedTextBox();
            label49 = new CustomLabel();
            cbPszStirPlan = new CustomCheckBox();
            mtbRzuDataStP = new CustomMaskedTextBox();
            cbPszPrintPlan = new CustomCheckBox();
            label50 = new CustomLabel();
            mtbRzuDataVCd = new CustomMaskedTextBox();
            cbRzuVishFact = new CustomCheckBox();
            cbRzuPrintFact = new CustomCheckBox();
            label51 = new CustomLabel();
            mtbRzuDataVChi = new CustomMaskedTextBox();
            cbPszVishPlan = new CustomCheckBox();
            label37 = new CustomLabel();
            mtbRzuDataVR = new CustomMaskedTextBox();
            mtbRzuDataPrCd = new CustomMaskedTextBox();
            label35 = new CustomLabel();
            mtbRzuDataRasp = new CustomMaskedTextBox();
            mtbRzuDataVP = new CustomMaskedTextBox();
            label43 = new CustomLabel();
            label38 = new CustomLabel();
            mtbRzuDataPrKm = new CustomMaskedTextBox();
            mtbRzuDataRasv = new CustomMaskedTextBox();
            label45 = new CustomLabel();
            mtbRzuDataPrP = new CustomMaskedTextBox();
            label39 = new CustomLabel();
            mtbRzuDataPrR = new CustomMaskedTextBox();
            label46 = new CustomLabel();
            mtbRzuDataPrPe = new CustomMaskedTextBox();
            label40 = new CustomLabel();
            label41 = new CustomLabel();
            label47 = new CustomLabel();
            label42 = new CustomLabel();
            label36 = new CustomLabel();
            label48 = new CustomLabel();
            customGroupBox3 = new CustomGroupBox();
            tablePanel4 = new DevExpress.Utils.Layout.TablePanel();
            simpleButtonNaklPart = new CustomSimpleButton();
            simpleButtonPrintNaklXtraReport = new CustomSimpleButton();
            btnNaklAbsent = new CustomButton();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            gridControlPartNaklList = new CustomGridControl();
            gridViewPartNaklList = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumnNaklPartIzObPrch = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartRazm = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartMod = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartCountNew = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartCountAfter = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartCountBefore = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartIzNew = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartSklOtgrNew = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartSklOtgrOld = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartDateIzm = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartSklID1COld = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartCompDel = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartCompName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartIzOld = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartID = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartNPach = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartPrichSokr = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNaklPartSklID1CNew = new DevExpress.XtraGrid.Columns.GridColumn();
            btnNaklPrint = new CustomButton();
            ContDates = new CustomGroupBox();
            tablePanel3 = new DevExpress.Utils.Layout.TablePanel();
            customLabel3 = new CustomLabel();
            mtbRzuData1С = new CustomMaskedTextBox();
            tbPszRpcNom = new CustomTextBox();
            label63 = new CustomLabel();
            label29 = new CustomLabel();
            mtbRzuDataCd = new CustomMaskedTextBox();
            mtbRzuDataR = new CustomMaskedTextBox();
            label14 = new CustomLabel();
            mtbRzuDataUp = new CustomMaskedTextBox();
            mtbPsaDataZap = new CustomMaskedTextBox();
            mtbRzuDataRab = new CustomMaskedTextBox();
            label33 = new CustomLabel();
            label64 = new CustomLabel();
            mtbRzuDataZeh = new CustomMaskedTextBox();
            label28 = new CustomLabel();
            label26 = new CustomLabel();
            mtbPsaDataCdPlan = new CustomMaskedTextBox();
            label34 = new CustomLabel();
            mtbRzuDataCdUt = new CustomMaskedTextBox();
            label27 = new CustomLabel();
            xtraTabControl1 = new CustomTabControl();
            SockZadanyInfo = new DevExpress.XtraTab.XtraTabPage();
            layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            TextBoxKolPlanZadany = new CustomTextBoxEx();
            customLabel5 = new CustomLabel();
            gridControlSockDefectList = new CustomGridControl();
            gridViewSockDefectList = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridSockDefectListColumnVspdid = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDefectListColumnNomZadany = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDefectListColumnIsdefect = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDefectListColumnKg = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDefectListColumnKolAll = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDefectListColumnKolDefect = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDefectListColumnIdspj = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDefectListColumnIdndsp = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDefectListColumnNamedefect = new DevExpress.XtraGrid.Columns.GridColumn();
            TextBoxKnitTotalTime = new CustomTextBox();
            customLabel20 = new CustomLabel();
            gridControlSockDownTimeList = new CustomGridControl();
            gridViewSockDownTimeList = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridSockDownTimeListColumnKzPszNom = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnKmaNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnKmlNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnKmlInvNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnKmlID = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnTextObS = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnKdtlDateStart = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnKdtlDateEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnDiffPeriod = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnDaysDiff = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockDownTimeListColumnTimeDiff = new DevExpress.XtraGrid.Columns.GridColumn();
            TextBoxKolFactSmen = new CustomTextBoxEx();
            TextBoxKolFactDelta = new CustomTextBoxEx();
            customLabel19 = new CustomLabel();
            TextBoxKolFactZadany = new CustomTextBoxEx();
            customLabel18 = new CustomLabel();
            gridControlSockServiceList = new CustomGridControl();
            gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridSockServiceListColumnKzPszNom = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnKmaNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnKmlInvNum = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnKmlNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnTextObS = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnDirectorName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnResultName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnResultText = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnDateEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnMechanic = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnDiffPeriod = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnDaysDiff = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockServiceListColumnTimeDiff = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlSockZadanySmenList = new CustomGridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridSockZadanySmenListColumnKzDateAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKwsTabStart = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnFioSt = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKzDateEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKwsTabEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnFioEn = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKolFact = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnChasVyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnDiffPeriod = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKmaNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKmlNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKzID = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKzKwsID = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKzKmlID = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKzKmaID = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKzEnded = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnDivider = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKwsKmsID = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSockZadanySmenListColumnKmlInvNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            TextBoxKnitEndDate = new CustomTextBox();
            TextBoxKnitStartDate = new CustomTextBox();
            TextBoxAreaNumber = new CustomTextBox();
            TextBoxMachineNumber = new CustomTextBox();
            TextBoxTabFio = new CustomTextBox();
            customLabel14 = new CustomLabel();
            customLabel13 = new CustomLabel();
            customLabel12 = new CustomLabel();
            customLabel11 = new CustomLabel();
            customLabel10 = new CustomLabel();
            customLabel9 = new CustomLabel();
            customLabel8 = new CustomLabel();
            TextBoxDefectCount = new CustomTextBoxEx();
            TextBoxDefectWeight = new CustomTextBoxEx();
            customLabel7 = new CustomLabel();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem43 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem44 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem45 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem13 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem14 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            simpleSeparator4 = new DevExpress.XtraLayout.SimpleSeparator();
            simpleSeparator5 = new DevExpress.XtraLayout.SimpleSeparator();
            simpleSeparator6 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem12 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
            simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            customLabel2 = new CustomLabel();
            customLabel1 = new CustomLabel();
            tbNomZad = new CustomTextBox();
            customLabel4 = new CustomLabel();
            customRadioGroup3 = new CustomRadioGroup();
            layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            customRadioGroup2 = new CustomRadioGroup();
            layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            pbEskiz = new PictureBox();
            tbSostPoln = new CustomTextBox();
            tbRzuMod = new CustomTextBox();
            label13 = new CustomLabel();
            tbArtGrup = new CustomTextBox();
            tbPsaKombOsn = new CustomTextBox();
            label10 = new CustomLabel();
            tbPsaPrn = new CustomTextBox();
            label67 = new CustomLabel();
            tbPsaKodZv2 = new CustomTextBox();
            label15 = new CustomLabel();
            tbPsaPsaIDOsn = new CustomTextBox();
            tbPsaKodZv1 = new CustomTextBox();
            label22 = new CustomLabel();
            tbPsaKombIzd = new CustomTextBox();
            label23 = new CustomLabel();
            label66 = new CustomLabel();
            label8 = new CustomLabel();
            label65 = new CustomLabel();
            label9 = new CustomLabel();
            tbPsaPsaID = new CustomTextBox();
            label5 = new CustomLabel();
            label62 = new CustomLabel();
            tbArtTradeMark = new CustomTextBox();
            tbPsaNameSbit = new CustomTextBox();
            tbRzuNom = new CustomTextBox();
            label68 = new CustomLabel();
            cbIsChip = new CustomCheckBox();
            tbRzuArticul = new CustomTextBox();
            label19 = new CustomLabel();
            label11 = new CustomLabel();
            tbPsaMenName = new CustomTextBox();
            tbPsaTbID = new CustomTextBox();
            psaSezName = new CustomTextBox();
            tbPsaYear = new CustomTextBox();
            label20 = new CustomLabel();
            label16 = new CustomLabel();
            label7 = new CustomLabel();
            tbRzuKol = new CustomTextBox();
            label17 = new CustomLabel();
            label53 = new CustomLabel();
            tbPsaNomZad = new CustomTextBox();
            label21 = new CustomLabel();
            label12 = new CustomLabel();
            tbPsaNN = new CustomTextBox();
            tbRzuDostZeh = new CustomTextBox();
            label6 = new CustomLabel();
            tbRzuPach = new CustomTextBox();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem49 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem51 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem52 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem24 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem53 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem54 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem55 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem56 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem57 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem58 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem59 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem15 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem17 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem18 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem19 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem62 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem63 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem20 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem64 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem65 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem21 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem66 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem67 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem68 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem69 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem16 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem70 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem71 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem72 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem73 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem22 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem60 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem61 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem47 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem48 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem74 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem75 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem23 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem76 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem77 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem78 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem79 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem80 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem81 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem82 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem83 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem84 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem85 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem25 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem26 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem27 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)bsProizvCombIzdSP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsProizvCombIzdVZP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridSplitContainer1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridSplitContainer1.Panel1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridSplitContainer1.Panel2).BeginInit();
            gridSplitContainer1.SuspendLayout();
            OtdelkaInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            customGroupBox7.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlProizvCombIzdSP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProizvCombIzdSP).BeginInit();
            customGroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlProizvCombIzdVZP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProizvCombIzdVZP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNaklList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlNaklList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewOtdelka).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlOtdelka).BeginInit();
            FurnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            customGroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablePanel7).BeginInit();
            tablePanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl7).BeginInit();
            RasInfo.SuspendLayout();
            customGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablePanel6).BeginInit();
            tablePanel6.SuspendLayout();
            customGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablePanel5).BeginInit();
            tablePanel5.SuspendLayout();
            customGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablePanel4).BeginInit();
            tablePanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlPartNaklList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPartNaklList).BeginInit();
            ContDates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablePanel3).BeginInit();
            tablePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl1).BeginInit();
            xtraTabControl1.SuspendLayout();
            SockZadanyInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl2).BeginInit();
            layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TextBoxKolPlanZadany.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSockDefectList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSockDefectList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSockDownTimeList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSockDownTimeList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxKolFactSmen.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxKolFactDelta.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxKolFactZadany.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSockServiceList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSockZadanySmenList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxDefectCount.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxDefectWeight.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem29).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem30).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem32).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem34).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem35).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem36).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem37).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem41).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem42).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem38).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem40).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem43).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem44).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem45).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem39).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customRadioGroup3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl3).BeginInit();
            layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customRadioGroup2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbEskiz).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem46).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem49).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem50).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem51).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem52).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem53).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem54).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem55).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem56).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem57).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem58).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem59).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem62).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem63).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem64).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem65).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem66).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem67).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem68).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem69).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem70).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem71).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem72).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem73).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem60).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem61).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem47).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem48).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem74).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem75).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem76).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem77).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem78).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem79).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem80).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem81).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem82).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem83).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem84).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem85).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem27).BeginInit();
            SuspendLayout();
            // 
            // gridColumn40
            // 
            gridColumn40.FieldName = "ChipInUTForeColor";
            gridColumn40.MinWidth = 23;
            gridColumn40.Name = "gridColumn40";
            gridColumn40.Width = 73;
            // 
            // gridColumnNaklChipInUT
            // 
            gridColumnNaklChipInUT.AppearanceCell.Options.UseTextOptions = true;
            gridColumnNaklChipInUT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklChipInUT.Caption = "ЧИП в УТ";
            gridColumnNaklChipInUT.MinWidth = 23;
            gridColumnNaklChipInUT.Name = "gridColumnNaklChipInUT";
            gridColumnNaklChipInUT.OptionsColumn.FixedWidth = true;
            gridColumnNaklChipInUT.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklChipInUT.OptionsFilter.AllowFilter = false;
            gridColumnNaklChipInUT.Visible = true;
            gridColumnNaklChipInUT.VisibleIndex = 13;
            gridColumnNaklChipInUT.Width = 47;
            // 
            // gridColumn44
            // 
            gridColumn44.FieldName = "ChipPechForeColor";
            gridColumn44.MinWidth = 23;
            gridColumn44.Name = "gridColumn44";
            gridColumn44.Width = 73;
            // 
            // gridColumnNaklChipPech
            // 
            gridColumnNaklChipPech.AppearanceCell.Options.UseTextOptions = true;
            gridColumnNaklChipPech.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklChipPech.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklChipPech.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklChipPech.Caption = "Печ. ЧИП";
            gridColumnNaklChipPech.MinWidth = 23;
            gridColumnNaklChipPech.Name = "gridColumnNaklChipPech";
            gridColumnNaklChipPech.OptionsColumn.FixedWidth = true;
            gridColumnNaklChipPech.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklChipPech.OptionsFilter.AllowFilter = false;
            gridColumnNaklChipPech.Visible = true;
            gridColumnNaklChipPech.VisibleIndex = 14;
            gridColumnNaklChipPech.Width = 47;
            // 
            // gridColumn45
            // 
            gridColumn45.FieldName = "ChipScanForeColor";
            gridColumn45.MinWidth = 23;
            gridColumn45.Name = "gridColumn45";
            gridColumn45.Width = 85;
            // 
            // gridColumnNaklChipScan
            // 
            gridColumnNaklChipScan.AppearanceCell.Options.UseTextOptions = true;
            gridColumnNaklChipScan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklChipScan.Caption = "Скан. ЧИП";
            gridColumnNaklChipScan.MinWidth = 23;
            gridColumnNaklChipScan.Name = "gridColumnNaklChipScan";
            gridColumnNaklChipScan.OptionsColumn.FixedWidth = true;
            gridColumnNaklChipScan.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklChipScan.OptionsFilter.AllowFilter = false;
            gridColumnNaklChipScan.Visible = true;
            gridColumnNaklChipScan.VisibleIndex = 15;
            gridColumnNaklChipScan.Width = 47;
            // 
            // gridColumn46
            // 
            gridColumn46.FieldName = "ChipOtgrForeColor";
            gridColumn46.MinWidth = 23;
            gridColumn46.Name = "gridColumn46";
            gridColumn46.Width = 85;
            // 
            // gridColumnNaklChipOtgr
            // 
            gridColumnNaklChipOtgr.AppearanceCell.Options.UseTextOptions = true;
            gridColumnNaklChipOtgr.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklChipOtgr.Caption = "Отгр. ЧИП";
            gridColumnNaklChipOtgr.MinWidth = 23;
            gridColumnNaklChipOtgr.Name = "gridColumnNaklChipOtgr";
            gridColumnNaklChipOtgr.OptionsColumn.FixedWidth = true;
            gridColumnNaklChipOtgr.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklChipOtgr.OptionsFilter.AllowFilter = false;
            gridColumnNaklChipOtgr.Visible = true;
            gridColumnNaklChipOtgr.VisibleIndex = 16;
            gridColumnNaklChipOtgr.Width = 47;
            // 
            // tbYearPach
            // 
            tbYearPach.BackColor = Color.FromArgb(248, 248, 255);
            tbYearPach.Font = new Font("Arial", 10F);
            tbYearPach.ForeColor = Color.FromArgb(72, 61, 139);
            tbYearPach.Location = new Point(739, 5);
            tbYearPach.Margin = new Padding(0);
            tbYearPach.Name = "tbYearPach";
            tbYearPach.Size = new Size(46, 20);
            tbYearPach.TabIndex = 0;
            // 
            // tbNomPach
            // 
            tbNomPach.BackColor = Color.FromArgb(248, 248, 255);
            tbNomPach.Font = new Font("Arial", 10F);
            tbNomPach.ForeColor = Color.FromArgb(72, 61, 139);
            tbNomPach.Location = new Point(610, 5);
            tbNomPach.Margin = new Padding(0);
            tbNomPach.Name = "tbNomPach";
            tbNomPach.Size = new Size(81, 20);
            tbNomPach.TabIndex = 3;
            tbNomPach.KeyDown += tbNomPach_KeyDown;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 10F);
            label4.ForeColor = Color.FromArgb(0, 0, 0);
            label4.Location = new Point(706, 5);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(29, 22);
            label4.TabIndex = 1;
            label4.Text = "год";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gridColumn41
            // 
            gridColumn41.Caption = "Склад отгр ДО";
            gridColumn41.FieldName = "skl_otgr_b";
            gridColumn41.Name = "gridColumn41";
            gridColumn41.Visible = true;
            gridColumn41.VisibleIndex = 7;
            gridColumn41.Width = 61;
            // 
            // gridSplitContainer1
            // 
            gridSplitContainer1.Grid = null;
            gridSplitContainer1.Location = new Point(11, 14);
            gridSplitContainer1.Name = "gridSplitContainer1";
            gridSplitContainer1.Size = new Size(400, 200);
            gridSplitContainer1.TabIndex = 2;
            // 
            // OtdelkaInfo
            // 
            OtdelkaInfo.Controls.Add(splitContainer2);
            OtdelkaInfo.Margin = new Padding(4, 3, 4, 3);
            OtdelkaInfo.Name = "OtdelkaInfo";
            OtdelkaInfo.Size = new Size(1848, 698);
            OtdelkaInfo.Text = "ДЕТАЛИ ОТДЕЛКИ";
            // 
            // splitContainer2
            // 
            splitContainer2.BackColor = Color.Transparent;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(4, 3, 4, 3);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(customGroupBox7);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(customGroupBox8);
            splitContainer2.Size = new Size(1848, 698);
            splitContainer2.SplitterDistance = 348;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 8;
            // 
            // customGroupBox7
            // 
            customGroupBox7.BackColor = Color.Transparent;
            customGroupBox7.Controls.Add(tableLayoutPanel1);
            customGroupBox7.Dock = DockStyle.Fill;
            customGroupBox7.Font = new Font("Arial", 12F, FontStyle.Bold);
            customGroupBox7.Location = new Point(0, 0);
            customGroupBox7.Margin = new Padding(4, 3, 4, 3);
            customGroupBox7.Name = "customGroupBox7";
            customGroupBox7.Padding = new Padding(4, 3, 4, 3);
            customGroupBox7.Size = new Size(1848, 348);
            customGroupBox7.TabIndex = 0;
            customGroupBox7.TabStop = false;
            customGroupBox7.Text = "ШП";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92.40385F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.596154F));
            tableLayoutPanel1.Controls.Add(gridControlProizvCombIzdSP, 0, 0);
            tableLayoutPanel1.Controls.Add(sbProizvCombIzdSP, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(4, 22);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 89.70588F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.29412F));
            tableLayoutPanel1.Size = new Size(1840, 323);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControlProizvCombIzdSP
            // 
            tableLayoutPanel1.SetColumnSpan(gridControlProizvCombIzdSP, 2);
            gridControlProizvCombIzdSP.DataSource = bsProizvCombIzdSP;
            gridControlProizvCombIzdSP.Dock = DockStyle.Fill;
            gridControlProizvCombIzdSP.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlProizvCombIzdSP.Font = new Font("Arial", 10F);
            gridControlProizvCombIzdSP.Location = new Point(4, 3);
            gridControlProizvCombIzdSP.MainView = gridViewProizvCombIzdSP;
            gridControlProizvCombIzdSP.Margin = new Padding(4, 3, 4, 3);
            gridControlProizvCombIzdSP.Name = "gridControlProizvCombIzdSP";
            gridControlProizvCombIzdSP.Size = new Size(1832, 283);
            gridControlProizvCombIzdSP.TabIndex = 4;
            gridControlProizvCombIzdSP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewProizvCombIzdSP });
            // 
            // gridViewProizvCombIzdSP
            // 
            gridViewProizvCombIzdSP.Appearance.GroupFooter.Options.UseTextOptions = true;
            gridViewProizvCombIzdSP.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridViewProizvCombIzdSP.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewProizvCombIzdSP.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewProizvCombIzdSP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnProizvCombIzdSpRzuMod, gridColumnProizvCombIzdSPPszNom, gridColumnProizvCombIzdSPPszZvet, gridColumnProizvCombIzdSPRzuArticul, gridColumnProizvCombIzdSPRzuGrup, gridColumnProizvCombIzdSpRzuRazm, gridColumnProizvCombIzdSPKolItog, gridColumnProizvCombIzdSPNIz, gridColumnProizvCombIzdSPKolRaskr, gridColumnProizvCombIzdSPKolRab, gridColumnProizvCombIzdSPKolFurnPrinSkl, gridColumnProizvCombIzdSPRzuDataRab, gridColumnProizvCombIzdSPNDostData, gridColumnProizvCombIzdSPDateFurnPrihSkl, gridColumnProizvCombIzdSPKolGI });
            gridViewProizvCombIzdSP.CustomizationFormBounds = new Rectangle(3464, 607, 308, 314);
            gridViewProizvCombIzdSP.DetailHeight = 404;
            gridViewProizvCombIzdSP.GridControl = gridControlProizvCombIzdSP;
            gridViewProizvCombIzdSP.GroupCount = 3;
            gridViewProizvCombIzdSP.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRaskr", null, "(Раскроено всего: {0:0.##})"), new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRab", null, "(В работе всего: {0:0.##})"), new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", null, "(Сдано на склад всего: {0:0.##})"), new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", null, "(Принято на склад фурнитуры всего: {0:0.##})") });
            gridViewProizvCombIzdSP.Name = "gridViewProizvCombIzdSP";
            gridViewProizvCombIzdSP.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewProizvCombIzdSP.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewProizvCombIzdSP.OptionsView.ShowFooter = true;
            gridViewProizvCombIzdSP.OptionsView.ShowGroupPanel = false;
            gridViewProizvCombIzdSP.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridColumnProizvCombIzdSPPszZvet, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridColumnProizvCombIzdSPPszNom, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridColumnProizvCombIzdSPNIz, DevExpress.Data.ColumnSortOrder.Ascending) });
            // 
            // gridColumnProizvCombIzdSpRzuMod
            // 
            gridColumnProizvCombIzdSpRzuMod.Caption = "Модель";
            gridColumnProizvCombIzdSpRzuMod.MinWidth = 23;
            gridColumnProizvCombIzdSpRzuMod.Name = "gridColumnProizvCombIzdSpRzuMod";
            gridColumnProizvCombIzdSpRzuMod.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSpRzuMod.OptionsEditForm.Caption = "Psz Mod:";
            gridColumnProizvCombIzdSpRzuMod.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSpRzuMod.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSpRzuMod.Visible = true;
            gridColumnProizvCombIzdSpRzuMod.VisibleIndex = 2;
            gridColumnProizvCombIzdSpRzuMod.Width = 117;
            // 
            // gridColumnProizvCombIzdSPPszNom
            // 
            gridColumnProizvCombIzdSPPszNom.Caption = "Задание";
            gridColumnProizvCombIzdSPPszNom.MinWidth = 23;
            gridColumnProizvCombIzdSPPszNom.Name = "gridColumnProizvCombIzdSPPszNom";
            gridColumnProizvCombIzdSPPszNom.Visible = true;
            gridColumnProizvCombIzdSPPszNom.VisibleIndex = 0;
            gridColumnProizvCombIzdSPPszNom.Width = 87;
            // 
            // gridColumnProizvCombIzdSPPszZvet
            // 
            gridColumnProizvCombIzdSPPszZvet.Caption = "Цвет";
            gridColumnProizvCombIzdSPPszZvet.MinWidth = 23;
            gridColumnProizvCombIzdSPPszZvet.Name = "gridColumnProizvCombIzdSPPszZvet";
            gridColumnProizvCombIzdSPPszZvet.Visible = true;
            gridColumnProizvCombIzdSPPszZvet.VisibleIndex = 0;
            gridColumnProizvCombIzdSPPszZvet.Width = 87;
            // 
            // gridColumnProizvCombIzdSPRzuArticul
            // 
            gridColumnProizvCombIzdSPRzuArticul.Caption = "Артикул";
            gridColumnProizvCombIzdSPRzuArticul.MinWidth = 23;
            gridColumnProizvCombIzdSPRzuArticul.Name = "gridColumnProizvCombIzdSPRzuArticul";
            gridColumnProizvCombIzdSPRzuArticul.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPRzuArticul.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPRzuArticul.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPRzuArticul.Visible = true;
            gridColumnProizvCombIzdSPRzuArticul.VisibleIndex = 1;
            gridColumnProizvCombIzdSPRzuArticul.Width = 117;
            // 
            // gridColumnProizvCombIzdSPRzuGrup
            // 
            gridColumnProizvCombIzdSPRzuGrup.Caption = "Группа";
            gridColumnProizvCombIzdSPRzuGrup.MinWidth = 23;
            gridColumnProizvCombIzdSPRzuGrup.Name = "gridColumnProizvCombIzdSPRzuGrup";
            gridColumnProizvCombIzdSPRzuGrup.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPRzuGrup.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPRzuGrup.Visible = true;
            gridColumnProizvCombIzdSPRzuGrup.VisibleIndex = 0;
            gridColumnProizvCombIzdSPRzuGrup.Width = 349;
            // 
            // gridColumnProizvCombIzdSpRzuRazm
            // 
            gridColumnProizvCombIzdSpRzuRazm.Caption = "Размер";
            gridColumnProizvCombIzdSpRzuRazm.MinWidth = 23;
            gridColumnProizvCombIzdSpRzuRazm.Name = "gridColumnProizvCombIzdSpRzuRazm";
            gridColumnProizvCombIzdSpRzuRazm.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSpRzuRazm.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSpRzuRazm.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSpRzuRazm.Visible = true;
            gridColumnProizvCombIzdSpRzuRazm.VisibleIndex = 3;
            gridColumnProizvCombIzdSpRzuRazm.Width = 93;
            // 
            // gridColumnProizvCombIzdSPKolItog
            // 
            gridColumnProizvCombIzdSPKolItog.Caption = "Кол-во";
            gridColumnProizvCombIzdSPKolItog.MinWidth = 23;
            gridColumnProizvCombIzdSPKolItog.Name = "gridColumnProizvCombIzdSPKolItog";
            gridColumnProizvCombIzdSPKolItog.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPKolItog.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPKolItog.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPKolItog.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolItog", "ИТОГО = {0:0.##}") });
            gridColumnProizvCombIzdSPKolItog.Visible = true;
            gridColumnProizvCombIzdSPKolItog.VisibleIndex = 4;
            gridColumnProizvCombIzdSPKolItog.Width = 105;
            // 
            // gridColumnProizvCombIzdSPNIz
            // 
            gridColumnProizvCombIzdSPNIz.Caption = "№ накладной";
            gridColumnProizvCombIzdSPNIz.MinWidth = 23;
            gridColumnProizvCombIzdSPNIz.Name = "gridColumnProizvCombIzdSPNIz";
            gridColumnProizvCombIzdSPNIz.Visible = true;
            gridColumnProizvCombIzdSPNIz.VisibleIndex = 0;
            gridColumnProizvCombIzdSPNIz.Width = 87;
            // 
            // gridColumnProizvCombIzdSPKolRaskr
            // 
            gridColumnProizvCombIzdSPKolRaskr.Caption = "Кол-во раскроено";
            gridColumnProizvCombIzdSPKolRaskr.MinWidth = 23;
            gridColumnProizvCombIzdSPKolRaskr.Name = "gridColumnProizvCombIzdSPKolRaskr";
            gridColumnProizvCombIzdSPKolRaskr.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPKolRaskr.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPKolRaskr.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPKolRaskr.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRaskr", "Раскроено: {0:0.##}") });
            gridColumnProizvCombIzdSPKolRaskr.Visible = true;
            gridColumnProizvCombIzdSPKolRaskr.VisibleIndex = 5;
            gridColumnProizvCombIzdSPKolRaskr.Width = 175;
            // 
            // gridColumnProizvCombIzdSPKolRab
            // 
            gridColumnProizvCombIzdSPKolRab.Caption = "Количество в работе";
            gridColumnProizvCombIzdSPKolRab.MinWidth = 23;
            gridColumnProizvCombIzdSPKolRab.Name = "gridColumnProizvCombIzdSPKolRab";
            gridColumnProizvCombIzdSPKolRab.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPKolRab.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPKolRab.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPKolRab.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRab", "В работе: {0:0.##}") });
            gridColumnProizvCombIzdSPKolRab.Visible = true;
            gridColumnProizvCombIzdSPKolRab.VisibleIndex = 6;
            gridColumnProizvCombIzdSPKolRab.Width = 175;
            // 
            // gridColumnProizvCombIzdSPKolFurnPrinSkl
            // 
            gridColumnProizvCombIzdSPKolFurnPrinSkl.Caption = "Кол-во прин. на скл. фурн.";
            gridColumnProizvCombIzdSPKolFurnPrinSkl.MinWidth = 23;
            gridColumnProizvCombIzdSPKolFurnPrinSkl.Name = "gridColumnProizvCombIzdSPKolFurnPrinSkl";
            gridColumnProizvCombIzdSPKolFurnPrinSkl.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPKolFurnPrinSkl.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPKolFurnPrinSkl.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPKolFurnPrinSkl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", "Прин. на скл. Фурн.: {0:0.##}") });
            gridColumnProizvCombIzdSPKolFurnPrinSkl.Visible = true;
            gridColumnProizvCombIzdSPKolFurnPrinSkl.VisibleIndex = 10;
            gridColumnProizvCombIzdSPKolFurnPrinSkl.Width = 175;
            // 
            // gridColumnProizvCombIzdSPRzuDataRab
            // 
            gridColumnProizvCombIzdSPRzuDataRab.Caption = "Дата в работу";
            gridColumnProizvCombIzdSPRzuDataRab.MinWidth = 23;
            gridColumnProizvCombIzdSPRzuDataRab.Name = "gridColumnProizvCombIzdSPRzuDataRab";
            gridColumnProizvCombIzdSPRzuDataRab.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPRzuDataRab.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPRzuDataRab.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPRzuDataRab.Visible = true;
            gridColumnProizvCombIzdSPRzuDataRab.VisibleIndex = 7;
            gridColumnProizvCombIzdSPRzuDataRab.Width = 105;
            // 
            // gridColumnProizvCombIzdSPNDostData
            // 
            gridColumnProizvCombIzdSPNDostData.Caption = "Дата отгр. на склад";
            gridColumnProizvCombIzdSPNDostData.MinWidth = 23;
            gridColumnProizvCombIzdSPNDostData.Name = "gridColumnProizvCombIzdSPNDostData";
            gridColumnProizvCombIzdSPNDostData.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPNDostData.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPNDostData.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPNDostData.Visible = true;
            gridColumnProizvCombIzdSPNDostData.VisibleIndex = 9;
            gridColumnProizvCombIzdSPNDostData.Width = 105;
            // 
            // gridColumnProizvCombIzdSPDateFurnPrihSkl
            // 
            gridColumnProizvCombIzdSPDateFurnPrihSkl.Caption = "Дата прин. на скл. фурн.";
            gridColumnProizvCombIzdSPDateFurnPrihSkl.MinWidth = 23;
            gridColumnProizvCombIzdSPDateFurnPrihSkl.Name = "gridColumnProizvCombIzdSPDateFurnPrihSkl";
            gridColumnProizvCombIzdSPDateFurnPrihSkl.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPDateFurnPrihSkl.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPDateFurnPrihSkl.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPDateFurnPrihSkl.Visible = true;
            gridColumnProizvCombIzdSPDateFurnPrihSkl.VisibleIndex = 11;
            gridColumnProizvCombIzdSPDateFurnPrihSkl.Width = 105;
            // 
            // gridColumnProizvCombIzdSPKolGI
            // 
            gridColumnProizvCombIzdSPKolGI.Caption = "Кол-во отгр. на склад";
            gridColumnProizvCombIzdSPKolGI.MinWidth = 23;
            gridColumnProizvCombIzdSPKolGI.Name = "gridColumnProizvCombIzdSPKolGI";
            gridColumnProizvCombIzdSPKolGI.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdSPKolGI.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdSPKolGI.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdSPKolGI.Visible = true;
            gridColumnProizvCombIzdSPKolGI.VisibleIndex = 8;
            gridColumnProizvCombIzdSPKolGI.Width = 175;
            // 
            // sbProizvCombIzdSP
            // 
            sbProizvCombIzdSP.Dock = DockStyle.Fill;
            sbProizvCombIzdSP.Location = new Point(1704, 292);
            sbProizvCombIzdSP.Margin = new Padding(4, 3, 4, 3);
            sbProizvCombIzdSP.Name = "sbProizvCombIzdSP";
            sbProizvCombIzdSP.Size = new Size(132, 28);
            sbProizvCombIzdSP.TabIndex = 7;
            sbProizvCombIzdSP.Text = "Печать";
            sbProizvCombIzdSP.Click += sbProizvCombIzdSP_Click;
            // 
            // customGroupBox8
            // 
            customGroupBox8.BackColor = Color.Transparent;
            customGroupBox8.Controls.Add(gridControlProizvCombIzdVZP);
            customGroupBox8.Dock = DockStyle.Fill;
            customGroupBox8.Font = new Font("Arial", 12F, FontStyle.Bold);
            customGroupBox8.Location = new Point(0, 0);
            customGroupBox8.Margin = new Padding(4, 3, 4, 3);
            customGroupBox8.Name = "customGroupBox8";
            customGroupBox8.Padding = new Padding(4, 3, 4, 3);
            customGroupBox8.Size = new Size(1848, 345);
            customGroupBox8.TabIndex = 0;
            customGroupBox8.TabStop = false;
            customGroupBox8.Text = "ВЗП";
            // 
            // gridControlProizvCombIzdVZP
            // 
            gridControlProizvCombIzdVZP.DataSource = bsProizvCombIzdVZP;
            gridControlProizvCombIzdVZP.Dock = DockStyle.Fill;
            gridControlProizvCombIzdVZP.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlProizvCombIzdVZP.Font = new Font("Arial", 10F);
            gridControlProizvCombIzdVZP.Location = new Point(4, 22);
            gridControlProizvCombIzdVZP.MainView = gridViewProizvCombIzdVZP;
            gridControlProizvCombIzdVZP.Margin = new Padding(4, 3, 4, 3);
            gridControlProizvCombIzdVZP.Name = "gridControlProizvCombIzdVZP";
            gridControlProizvCombIzdVZP.Size = new Size(1840, 320);
            gridControlProizvCombIzdVZP.TabIndex = 4;
            gridControlProizvCombIzdVZP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewProizvCombIzdVZP });
            // 
            // gridViewProizvCombIzdVZP
            // 
            gridViewProizvCombIzdVZP.Appearance.FooterPanel.Options.UseTextOptions = true;
            gridViewProizvCombIzdVZP.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridViewProizvCombIzdVZP.Appearance.GroupFooter.Options.UseTextOptions = true;
            gridViewProizvCombIzdVZP.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridViewProizvCombIzdVZP.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewProizvCombIzdVZP.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewProizvCombIzdVZP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnProizvCombIzdVZPRzvMod, gridColumnProizvCombIzdVZPPszNom, gridColumnProizvCombIzdVZPPszZvet, gridColumnProizvCombIzdVZPRzvArticul, gridColumnProizvCombIzdVZPRzvGrup, gridColumnProizvCombIzdVZPRzvRazm, gridColumnProizvCombIzdVZPKolItog, gridColumnProizvCombIzdVZPNIz, gridColumnProizvCombIzdVZPKolVyaz, gridColumnProizvCombIzdVZPKolOtparka, gridColumnProizvCombIzdVZPKolGI, gridColumnProizvCombIzdVZPKolFurnPrinSkl, gridColumnProizvCombIzdVZPRzvDateOkonV, gridColumnProizvCombIzdVZPNDostData, gridColumnProizvCombIzdVZPDateFurnPrihSkl });
            gridViewProizvCombIzdVZP.CustomizationFormBounds = new Rectangle(3464, 607, 308, 314);
            gridViewProizvCombIzdVZP.DetailHeight = 404;
            gridViewProizvCombIzdVZP.GridControl = gridControlProizvCombIzdVZP;
            gridViewProizvCombIzdVZP.GroupCount = 3;
            gridViewProizvCombIzdVZP.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolVyaz", null, "(Вязание: {0:0.##})"), new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolOtparka", null, "(Отпарка: {0:0.##})"), new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", null, "(Сдано на склад всего: {0:0.##})"), new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", null, "(Принято на склад фурнитуры всего: {0:0.##})") });
            gridViewProizvCombIzdVZP.Name = "gridViewProizvCombIzdVZP";
            gridViewProizvCombIzdVZP.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewProizvCombIzdVZP.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewProizvCombIzdVZP.OptionsView.ShowFooter = true;
            gridViewProizvCombIzdVZP.OptionsView.ShowGroupPanel = false;
            gridViewProizvCombIzdVZP.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridColumnProizvCombIzdVZPPszZvet, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridColumnProizvCombIzdVZPPszNom, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridColumnProizvCombIzdVZPNIz, DevExpress.Data.ColumnSortOrder.Ascending) });
            // 
            // gridColumnProizvCombIzdVZPRzvMod
            // 
            gridColumnProizvCombIzdVZPRzvMod.Caption = "Модель";
            gridColumnProizvCombIzdVZPRzvMod.MinWidth = 23;
            gridColumnProizvCombIzdVZPRzvMod.Name = "gridColumnProizvCombIzdVZPRzvMod";
            gridColumnProizvCombIzdVZPRzvMod.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPRzvMod.OptionsEditForm.Caption = "Psz Mod:";
            gridColumnProizvCombIzdVZPRzvMod.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPRzvMod.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPRzvMod.Visible = true;
            gridColumnProizvCombIzdVZPRzvMod.VisibleIndex = 2;
            gridColumnProizvCombIzdVZPRzvMod.Width = 117;
            // 
            // gridColumnProizvCombIzdVZPPszNom
            // 
            gridColumnProizvCombIzdVZPPszNom.Caption = "Задание";
            gridColumnProizvCombIzdVZPPszNom.MinWidth = 23;
            gridColumnProizvCombIzdVZPPszNom.Name = "gridColumnProizvCombIzdVZPPszNom";
            gridColumnProizvCombIzdVZPPszNom.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPPszNom.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPPszNom.Visible = true;
            gridColumnProizvCombIzdVZPPszNom.VisibleIndex = 0;
            gridColumnProizvCombIzdVZPPszNom.Width = 87;
            // 
            // gridColumnProizvCombIzdVZPPszZvet
            // 
            gridColumnProizvCombIzdVZPPszZvet.Caption = "Цвет";
            gridColumnProizvCombIzdVZPPszZvet.MinWidth = 23;
            gridColumnProizvCombIzdVZPPszZvet.Name = "gridColumnProizvCombIzdVZPPszZvet";
            gridColumnProizvCombIzdVZPPszZvet.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPPszZvet.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPPszZvet.Visible = true;
            gridColumnProizvCombIzdVZPPszZvet.VisibleIndex = 0;
            gridColumnProizvCombIzdVZPPszZvet.Width = 87;
            // 
            // gridColumnProizvCombIzdVZPRzvArticul
            // 
            gridColumnProizvCombIzdVZPRzvArticul.Caption = "Артикул";
            gridColumnProizvCombIzdVZPRzvArticul.MinWidth = 23;
            gridColumnProizvCombIzdVZPRzvArticul.Name = "gridColumnProizvCombIzdVZPRzvArticul";
            gridColumnProizvCombIzdVZPRzvArticul.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPRzvArticul.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPRzvArticul.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPRzvArticul.Visible = true;
            gridColumnProizvCombIzdVZPRzvArticul.VisibleIndex = 1;
            gridColumnProizvCombIzdVZPRzvArticul.Width = 117;
            // 
            // gridColumnProizvCombIzdVZPRzvGrup
            // 
            gridColumnProizvCombIzdVZPRzvGrup.Caption = "Группа";
            gridColumnProizvCombIzdVZPRzvGrup.MinWidth = 23;
            gridColumnProizvCombIzdVZPRzvGrup.Name = "gridColumnProizvCombIzdVZPRzvGrup";
            gridColumnProizvCombIzdVZPRzvGrup.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPRzvGrup.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPRzvGrup.Visible = true;
            gridColumnProizvCombIzdVZPRzvGrup.VisibleIndex = 0;
            gridColumnProizvCombIzdVZPRzvGrup.Width = 349;
            // 
            // gridColumnProizvCombIzdVZPRzvRazm
            // 
            gridColumnProizvCombIzdVZPRzvRazm.Caption = "Размер";
            gridColumnProizvCombIzdVZPRzvRazm.MinWidth = 23;
            gridColumnProizvCombIzdVZPRzvRazm.Name = "gridColumnProizvCombIzdVZPRzvRazm";
            gridColumnProizvCombIzdVZPRzvRazm.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPRzvRazm.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPRzvRazm.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPRzvRazm.Visible = true;
            gridColumnProizvCombIzdVZPRzvRazm.VisibleIndex = 3;
            gridColumnProizvCombIzdVZPRzvRazm.Width = 93;
            // 
            // gridColumnProizvCombIzdVZPKolItog
            // 
            gridColumnProizvCombIzdVZPKolItog.Caption = "Кол-во";
            gridColumnProizvCombIzdVZPKolItog.MinWidth = 23;
            gridColumnProizvCombIzdVZPKolItog.Name = "gridColumnProizvCombIzdVZPKolItog";
            gridColumnProizvCombIzdVZPKolItog.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPKolItog.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPKolItog.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPKolItog.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolItog", "ИТОГО = {0:0.##}") });
            gridColumnProizvCombIzdVZPKolItog.Visible = true;
            gridColumnProizvCombIzdVZPKolItog.VisibleIndex = 4;
            gridColumnProizvCombIzdVZPKolItog.Width = 105;
            // 
            // gridColumnProizvCombIzdVZPNIz
            // 
            gridColumnProizvCombIzdVZPNIz.Caption = "№ накладной";
            gridColumnProizvCombIzdVZPNIz.MinWidth = 23;
            gridColumnProizvCombIzdVZPNIz.Name = "gridColumnProizvCombIzdVZPNIz";
            gridColumnProizvCombIzdVZPNIz.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPNIz.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPNIz.Visible = true;
            gridColumnProizvCombIzdVZPNIz.VisibleIndex = 0;
            gridColumnProizvCombIzdVZPNIz.Width = 87;
            // 
            // gridColumnProizvCombIzdVZPKolVyaz
            // 
            gridColumnProizvCombIzdVZPKolVyaz.Caption = "Кол-во на вязании";
            gridColumnProizvCombIzdVZPKolVyaz.MinWidth = 23;
            gridColumnProizvCombIzdVZPKolVyaz.Name = "gridColumnProizvCombIzdVZPKolVyaz";
            gridColumnProizvCombIzdVZPKolVyaz.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPKolVyaz.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPKolVyaz.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPKolVyaz.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolVyaz", "Вязание: {0:0.##}") });
            gridColumnProizvCombIzdVZPKolVyaz.Visible = true;
            gridColumnProizvCombIzdVZPKolVyaz.VisibleIndex = 5;
            gridColumnProizvCombIzdVZPKolVyaz.Width = 175;
            // 
            // gridColumnProizvCombIzdVZPKolOtparka
            // 
            gridColumnProizvCombIzdVZPKolOtparka.Caption = "Кол-во на отпарке";
            gridColumnProizvCombIzdVZPKolOtparka.MinWidth = 23;
            gridColumnProizvCombIzdVZPKolOtparka.Name = "gridColumnProizvCombIzdVZPKolOtparka";
            gridColumnProizvCombIzdVZPKolOtparka.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPKolOtparka.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPKolOtparka.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPKolOtparka.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolOtparka", "Отпарка: {0:0.##}") });
            gridColumnProizvCombIzdVZPKolOtparka.Visible = true;
            gridColumnProizvCombIzdVZPKolOtparka.VisibleIndex = 6;
            gridColumnProizvCombIzdVZPKolOtparka.Width = 175;
            // 
            // gridColumnProizvCombIzdVZPKolGI
            // 
            gridColumnProizvCombIzdVZPKolGI.Caption = "Кол-во отгр. на склад";
            gridColumnProizvCombIzdVZPKolGI.MinWidth = 23;
            gridColumnProizvCombIzdVZPKolGI.Name = "gridColumnProizvCombIzdVZPKolGI";
            gridColumnProizvCombIzdVZPKolGI.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPKolGI.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPKolGI.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPKolGI.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", "Отгр. на скл.: {0:0.##}") });
            gridColumnProizvCombIzdVZPKolGI.Visible = true;
            gridColumnProizvCombIzdVZPKolGI.VisibleIndex = 8;
            gridColumnProizvCombIzdVZPKolGI.Width = 175;
            // 
            // gridColumnProizvCombIzdVZPKolFurnPrinSkl
            // 
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.Caption = "Кол-во прин. на скл. фурн.";
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.MinWidth = 23;
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.Name = "gridColumnProizvCombIzdVZPKolFurnPrinSkl";
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", "Прин. на скл. Фурн.: {0:0.##}") });
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.Visible = true;
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.VisibleIndex = 10;
            gridColumnProizvCombIzdVZPKolFurnPrinSkl.Width = 175;
            // 
            // gridColumnProizvCombIzdVZPRzvDateOkonV
            // 
            gridColumnProizvCombIzdVZPRzvDateOkonV.Caption = "Дата отпарки";
            gridColumnProizvCombIzdVZPRzvDateOkonV.MinWidth = 23;
            gridColumnProizvCombIzdVZPRzvDateOkonV.Name = "gridColumnProizvCombIzdVZPRzvDateOkonV";
            gridColumnProizvCombIzdVZPRzvDateOkonV.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPRzvDateOkonV.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPRzvDateOkonV.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPRzvDateOkonV.Visible = true;
            gridColumnProizvCombIzdVZPRzvDateOkonV.VisibleIndex = 7;
            gridColumnProizvCombIzdVZPRzvDateOkonV.Width = 105;
            // 
            // gridColumnProizvCombIzdVZPNDostData
            // 
            gridColumnProizvCombIzdVZPNDostData.Caption = "Дата отгр. на склад";
            gridColumnProizvCombIzdVZPNDostData.MinWidth = 23;
            gridColumnProizvCombIzdVZPNDostData.Name = "gridColumnProizvCombIzdVZPNDostData";
            gridColumnProizvCombIzdVZPNDostData.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPNDostData.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPNDostData.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPNDostData.Visible = true;
            gridColumnProizvCombIzdVZPNDostData.VisibleIndex = 9;
            gridColumnProizvCombIzdVZPNDostData.Width = 105;
            // 
            // gridColumnProizvCombIzdVZPDateFurnPrihSkl
            // 
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.Caption = "Дата прин. на скл. фурн.";
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.MinWidth = 23;
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.Name = "gridColumnProizvCombIzdVZPDateFurnPrihSkl";
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.OptionsColumn.FixedWidth = true;
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.OptionsFilter.AllowAutoFilter = false;
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.OptionsFilter.AllowFilter = false;
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.Visible = true;
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.VisibleIndex = 11;
            gridColumnProizvCombIzdVZPDateFurnPrihSkl.Width = 105;
            // 
            // gridViewNaklList
            // 
            gridViewNaklList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewNaklList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewNaklList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnNaklCountBefore, gridColumnNaklPrich, gridColumnNaklSklNaimen, gridColumnNaklGlNomer, gridColumnNaklDatePrint, gridColumnNaklDostN, gridColumnNaklDostData, gridColumnNaklDateIzm, gridColumnNaklIzDate, gridColumnNaklIzNakl, gridColumnNaklCountAfter, gridColumnNaklChipOtgr, gridColumnNaklChipScan, gridColumnNaklChipPech, gridColumnNaklChipInUT, gridColumnNaklMod, gridColumnNaklArticul, gridColumn40, gridColumn44, gridColumn45, gridColumn46 });
            gridViewNaklList.DetailHeight = 404;
            gridFormatRule1.Column = gridColumn40;
            gridFormatRule1.ColumnApplyTo = gridColumnNaklChipInUT;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue1.Appearance.ForeColor = Color.Red;
            formatConditionRuleValue1.Appearance.Options.UseFont = true;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = "red";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = gridColumn40;
            gridFormatRule2.ColumnApplyTo = gridColumnNaklChipInUT;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue2.Appearance.ForeColor = Color.FromArgb(0, 192, 0);
            formatConditionRuleValue2.Appearance.Options.UseFont = true;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = "green";
            gridFormatRule2.Rule = formatConditionRuleValue2;
            gridFormatRule3.Column = gridColumn44;
            gridFormatRule3.ColumnApplyTo = gridColumnNaklChipPech;
            gridFormatRule3.Name = "Format2";
            formatConditionRuleValue3.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue3.Appearance.ForeColor = Color.FromArgb(0, 192, 0);
            formatConditionRuleValue3.Appearance.Options.UseFont = true;
            formatConditionRuleValue3.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.Value1 = "green";
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = gridColumn44;
            gridFormatRule4.ColumnApplyTo = gridColumnNaklChipPech;
            gridFormatRule4.Name = "Format3";
            formatConditionRuleValue4.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue4.Appearance.ForeColor = Color.Red;
            formatConditionRuleValue4.Appearance.Options.UseFont = true;
            formatConditionRuleValue4.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = "red";
            gridFormatRule4.Rule = formatConditionRuleValue4;
            gridFormatRule5.Column = gridColumn44;
            gridFormatRule5.ColumnApplyTo = gridColumnNaklChipPech;
            gridFormatRule5.Name = "Format4";
            formatConditionRuleValue5.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue5.Appearance.ForeColor = Color.Gray;
            formatConditionRuleValue5.Appearance.Options.UseFont = true;
            formatConditionRuleValue5.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue5.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue5.Value1 = "gray";
            gridFormatRule5.Rule = formatConditionRuleValue5;
            gridFormatRule6.Column = gridColumn45;
            gridFormatRule6.ColumnApplyTo = gridColumnNaklChipScan;
            gridFormatRule6.Name = "Format5";
            formatConditionRuleValue6.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue6.Appearance.ForeColor = Color.Red;
            formatConditionRuleValue6.Appearance.Options.UseFont = true;
            formatConditionRuleValue6.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue6.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue6.Value1 = "red";
            gridFormatRule6.Rule = formatConditionRuleValue6;
            gridFormatRule7.Column = gridColumn45;
            gridFormatRule7.ColumnApplyTo = gridColumnNaklChipScan;
            gridFormatRule7.Name = "Format6";
            formatConditionRuleValue7.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue7.Appearance.ForeColor = Color.FromArgb(0, 192, 0);
            formatConditionRuleValue7.Appearance.Options.UseFont = true;
            formatConditionRuleValue7.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue7.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue7.Value1 = "green";
            gridFormatRule7.Rule = formatConditionRuleValue7;
            gridFormatRule8.Column = gridColumn45;
            gridFormatRule8.ColumnApplyTo = gridColumnNaklChipScan;
            gridFormatRule8.Name = "Format7";
            formatConditionRuleValue8.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue8.Appearance.ForeColor = Color.Gray;
            formatConditionRuleValue8.Appearance.Options.UseFont = true;
            formatConditionRuleValue8.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue8.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue8.Value1 = "gray";
            gridFormatRule8.Rule = formatConditionRuleValue8;
            gridFormatRule9.Column = gridColumn46;
            gridFormatRule9.ColumnApplyTo = gridColumnNaklChipOtgr;
            gridFormatRule9.Name = "Format8";
            formatConditionRuleValue9.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue9.Appearance.ForeColor = Color.Red;
            formatConditionRuleValue9.Appearance.Options.UseFont = true;
            formatConditionRuleValue9.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue9.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue9.Value1 = "red";
            gridFormatRule9.Rule = formatConditionRuleValue9;
            gridFormatRule10.Column = gridColumn46;
            gridFormatRule10.ColumnApplyTo = gridColumnNaklChipOtgr;
            gridFormatRule10.Name = "Format9";
            formatConditionRuleValue10.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue10.Appearance.ForeColor = Color.FromArgb(0, 192, 0);
            formatConditionRuleValue10.Appearance.Options.UseFont = true;
            formatConditionRuleValue10.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue10.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue10.Value1 = "green";
            gridFormatRule10.Rule = formatConditionRuleValue10;
            gridFormatRule11.Column = gridColumn46;
            gridFormatRule11.ColumnApplyTo = gridColumnNaklChipOtgr;
            gridFormatRule11.Name = "Format10";
            formatConditionRuleValue11.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            formatConditionRuleValue11.Appearance.ForeColor = Color.Gray;
            formatConditionRuleValue11.Appearance.Options.UseFont = true;
            formatConditionRuleValue11.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue11.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue11.Value1 = "gray";
            gridFormatRule11.Rule = formatConditionRuleValue11;
            gridViewNaklList.FormatRules.Add(gridFormatRule1);
            gridViewNaklList.FormatRules.Add(gridFormatRule2);
            gridViewNaklList.FormatRules.Add(gridFormatRule3);
            gridViewNaklList.FormatRules.Add(gridFormatRule4);
            gridViewNaklList.FormatRules.Add(gridFormatRule5);
            gridViewNaklList.FormatRules.Add(gridFormatRule6);
            gridViewNaklList.FormatRules.Add(gridFormatRule7);
            gridViewNaklList.FormatRules.Add(gridFormatRule8);
            gridViewNaklList.FormatRules.Add(gridFormatRule9);
            gridViewNaklList.FormatRules.Add(gridFormatRule10);
            gridViewNaklList.FormatRules.Add(gridFormatRule11);
            gridViewNaklList.GridControl = gridControlNaklList;
            gridViewNaklList.Name = "gridViewNaklList";
            gridViewNaklList.OptionsBehavior.Editable = false;
            gridViewNaklList.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewNaklList.OptionsMenu.ShowConditionalFormattingItem = true;
            gridViewNaklList.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewNaklList.OptionsView.RowAutoHeight = true;
            gridViewNaklList.OptionsView.ShowGroupPanel = false;
            gridViewNaklList.CustomDrawCell += gridView1_CustomDrawCell;
            // 
            // gridColumnNaklCountBefore
            // 
            gridColumnNaklCountBefore.Caption = "Кол-во ДО";
            gridColumnNaklCountBefore.MinWidth = 23;
            gridColumnNaklCountBefore.Name = "gridColumnNaklCountBefore";
            gridColumnNaklCountBefore.OptionsColumn.FixedWidth = true;
            gridColumnNaklCountBefore.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklCountBefore.OptionsFilter.AllowFilter = false;
            gridColumnNaklCountBefore.Visible = true;
            gridColumnNaklCountBefore.VisibleIndex = 9;
            gridColumnNaklCountBefore.Width = 70;
            // 
            // gridColumnNaklPrich
            // 
            gridColumnNaklPrich.Caption = "Причина деления";
            gridColumnNaklPrich.MinWidth = 23;
            gridColumnNaklPrich.Name = "gridColumnNaklPrich";
            gridColumnNaklPrich.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPrich.OptionsFilter.AllowFilter = false;
            gridColumnNaklPrich.Visible = true;
            gridColumnNaklPrich.VisibleIndex = 8;
            gridColumnNaklPrich.Width = 44;
            // 
            // gridColumnNaklSklNaimen
            // 
            gridColumnNaklSklNaimen.Caption = "Склад отгрузки";
            gridColumnNaklSklNaimen.MinWidth = 23;
            gridColumnNaklSklNaimen.Name = "gridColumnNaklSklNaimen";
            gridColumnNaklSklNaimen.OptionsColumn.FixedWidth = true;
            gridColumnNaklSklNaimen.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklSklNaimen.OptionsFilter.AllowFilter = false;
            gridColumnNaklSklNaimen.Visible = true;
            gridColumnNaklSklNaimen.VisibleIndex = 7;
            gridColumnNaklSklNaimen.Width = 175;
            // 
            // gridColumnNaklGlNomer
            // 
            gridColumnNaklGlNomer.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklGlNomer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklGlNomer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridColumnNaklGlNomer.Caption = "№ накл. Глобал";
            gridColumnNaklGlNomer.MinWidth = 23;
            gridColumnNaklGlNomer.Name = "gridColumnNaklGlNomer";
            gridColumnNaklGlNomer.OptionsColumn.FixedWidth = true;
            gridColumnNaklGlNomer.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklGlNomer.OptionsFilter.AllowFilter = false;
            gridColumnNaklGlNomer.Visible = true;
            gridColumnNaklGlNomer.VisibleIndex = 6;
            gridColumnNaklGlNomer.Width = 82;
            // 
            // gridColumnNaklDatePrint
            // 
            gridColumnNaklDatePrint.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklDatePrint.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklDatePrint.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridColumnNaklDatePrint.Caption = "Дата печати";
            gridColumnNaklDatePrint.MinWidth = 23;
            gridColumnNaklDatePrint.Name = "gridColumnNaklDatePrint";
            gridColumnNaklDatePrint.OptionsColumn.FixedWidth = true;
            gridColumnNaklDatePrint.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklDatePrint.OptionsFilter.AllowFilter = false;
            gridColumnNaklDatePrint.Visible = true;
            gridColumnNaklDatePrint.VisibleIndex = 5;
            gridColumnNaklDatePrint.Width = 105;
            // 
            // gridColumnNaklDostN
            // 
            gridColumnNaklDostN.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklDostN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklDostN.Caption = "№ отгр.";
            gridColumnNaklDostN.MinWidth = 23;
            gridColumnNaklDostN.Name = "gridColumnNaklDostN";
            gridColumnNaklDostN.OptionsColumn.FixedWidth = true;
            gridColumnNaklDostN.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklDostN.OptionsFilter.AllowFilter = false;
            gridColumnNaklDostN.Visible = true;
            gridColumnNaklDostN.VisibleIndex = 4;
            gridColumnNaklDostN.Width = 70;
            // 
            // gridColumnNaklDostData
            // 
            gridColumnNaklDostData.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklDostData.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklDostData.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridColumnNaklDostData.Caption = "Доставка на склад";
            gridColumnNaklDostData.MinWidth = 23;
            gridColumnNaklDostData.Name = "gridColumnNaklDostData";
            gridColumnNaklDostData.OptionsColumn.FixedWidth = true;
            gridColumnNaklDostData.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklDostData.OptionsFilter.AllowFilter = false;
            gridColumnNaklDostData.Visible = true;
            gridColumnNaklDostData.VisibleIndex = 3;
            gridColumnNaklDostData.Width = 105;
            // 
            // gridColumnNaklDateIzm
            // 
            gridColumnNaklDateIzm.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklDateIzm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklDateIzm.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridColumnNaklDateIzm.Caption = "Дата деления";
            gridColumnNaklDateIzm.MinWidth = 23;
            gridColumnNaklDateIzm.Name = "gridColumnNaklDateIzm";
            gridColumnNaklDateIzm.OptionsColumn.FixedWidth = true;
            gridColumnNaklDateIzm.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklDateIzm.OptionsFilter.AllowFilter = false;
            gridColumnNaklDateIzm.Visible = true;
            gridColumnNaklDateIzm.VisibleIndex = 2;
            gridColumnNaklDateIzm.Width = 105;
            // 
            // gridColumnNaklIzDate
            // 
            gridColumnNaklIzDate.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklIzDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklIzDate.Caption = "Дата накл.";
            gridColumnNaklIzDate.MinWidth = 23;
            gridColumnNaklIzDate.Name = "gridColumnNaklIzDate";
            gridColumnNaklIzDate.OptionsColumn.FixedWidth = true;
            gridColumnNaklIzDate.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklIzDate.OptionsFilter.AllowFilter = false;
            gridColumnNaklIzDate.Visible = true;
            gridColumnNaklIzDate.VisibleIndex = 1;
            gridColumnNaklIzDate.Width = 105;
            // 
            // gridColumnNaklIzNakl
            // 
            gridColumnNaklIzNakl.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklIzNakl.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklIzNakl.Caption = "№ накл.";
            gridColumnNaklIzNakl.FieldName = "IzNakl";
            gridColumnNaklIzNakl.MinWidth = 23;
            gridColumnNaklIzNakl.Name = "gridColumnNaklIzNakl";
            gridColumnNaklIzNakl.OptionsColumn.FixedWidth = true;
            gridColumnNaklIzNakl.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklIzNakl.OptionsFilter.AllowFilter = false;
            gridColumnNaklIzNakl.Visible = true;
            gridColumnNaklIzNakl.VisibleIndex = 0;
            gridColumnNaklIzNakl.Width = 70;
            // 
            // gridColumnNaklCountAfter
            // 
            gridColumnNaklCountAfter.AppearanceHeader.Options.UseTextOptions = true;
            gridColumnNaklCountAfter.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumnNaklCountAfter.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridColumnNaklCountAfter.Caption = "Кол-во ПОСЛЕ";
            gridColumnNaklCountAfter.MinWidth = 23;
            gridColumnNaklCountAfter.Name = "gridColumnNaklCountAfter";
            gridColumnNaklCountAfter.OptionsColumn.FixedWidth = true;
            gridColumnNaklCountAfter.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklCountAfter.OptionsFilter.AllowFilter = false;
            gridColumnNaklCountAfter.Visible = true;
            gridColumnNaklCountAfter.VisibleIndex = 10;
            gridColumnNaklCountAfter.Width = 70;
            // 
            // gridColumnNaklMod
            // 
            gridColumnNaklMod.Caption = "Модель";
            gridColumnNaklMod.MinWidth = 23;
            gridColumnNaklMod.Name = "gridColumnNaklMod";
            gridColumnNaklMod.OptionsColumn.FixedWidth = true;
            gridColumnNaklMod.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklMod.OptionsFilter.AllowFilter = false;
            gridColumnNaklMod.Visible = true;
            gridColumnNaklMod.VisibleIndex = 12;
            gridColumnNaklMod.Width = 140;
            // 
            // gridColumnNaklArticul
            // 
            gridColumnNaklArticul.Caption = "Артикул";
            gridColumnNaklArticul.MinWidth = 23;
            gridColumnNaklArticul.Name = "gridColumnNaklArticul";
            gridColumnNaklArticul.OptionsColumn.FixedWidth = true;
            gridColumnNaklArticul.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklArticul.OptionsFilter.AllowFilter = false;
            gridColumnNaklArticul.Visible = true;
            gridColumnNaklArticul.VisibleIndex = 11;
            gridColumnNaklArticul.Width = 140;
            // 
            // gridControlNaklList
            // 
            gridControlNaklList.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlNaklList.Font = new Font("Arial", 10F);
            gridControlNaklList.Location = new Point(-4, 6);
            gridControlNaklList.MainView = gridViewNaklList;
            gridControlNaklList.Margin = new Padding(4, 3, 4, 3);
            gridControlNaklList.Name = "gridControlNaklList";
            gridControlNaklList.Size = new Size(1802, 158);
            gridControlNaklList.TabIndex = 5;
            gridControlNaklList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewNaklList });
            // 
            // gridViewOtdelka
            // 
            gridViewOtdelka.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewOtdelka.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnOtdelkaViNaim, gridColumnOtdelkaCaption, gridColumnOtdelkaFrtNaimen, gridColumnOtdelkaKolSlZv, gridColumnOtdelkaPsaFieldName });
            gridViewOtdelka.DetailHeight = 404;
            gridViewOtdelka.GridControl = gridControlOtdelka;
            gridViewOtdelka.Name = "gridViewOtdelka";
            gridViewOtdelka.OptionsBehavior.Editable = false;
            gridViewOtdelka.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewOtdelka.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewOtdelka.OptionsView.RowAutoHeight = true;
            gridViewOtdelka.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnOtdelkaViNaim
            // 
            gridColumnOtdelkaViNaim.Caption = "Вид изделия";
            gridColumnOtdelkaViNaim.MinWidth = 23;
            gridColumnOtdelkaViNaim.Name = "gridColumnOtdelkaViNaim";
            gridColumnOtdelkaViNaim.OptionsColumn.FixedWidth = true;
            gridColumnOtdelkaViNaim.OptionsFilter.AllowAutoFilter = false;
            gridColumnOtdelkaViNaim.OptionsFilter.AllowFilter = false;
            gridColumnOtdelkaViNaim.Visible = true;
            gridColumnOtdelkaViNaim.VisibleIndex = 4;
            gridColumnOtdelkaViNaim.Width = 140;
            // 
            // gridColumnOtdelkaCaption
            // 
            gridColumnOtdelkaCaption.Caption = "Деталь изделия";
            gridColumnOtdelkaCaption.MinWidth = 23;
            gridColumnOtdelkaCaption.Name = "gridColumnOtdelkaCaption";
            gridColumnOtdelkaCaption.OptionsColumn.FixedWidth = true;
            gridColumnOtdelkaCaption.OptionsFilter.AllowAutoFilter = false;
            gridColumnOtdelkaCaption.OptionsFilter.AllowFilter = false;
            gridColumnOtdelkaCaption.Visible = true;
            gridColumnOtdelkaCaption.VisibleIndex = 3;
            gridColumnOtdelkaCaption.Width = 117;
            // 
            // gridColumnOtdelkaFrtNaimen
            // 
            gridColumnOtdelkaFrtNaimen.Caption = "Формат";
            gridColumnOtdelkaFrtNaimen.MinWidth = 23;
            gridColumnOtdelkaFrtNaimen.Name = "gridColumnOtdelkaFrtNaimen";
            gridColumnOtdelkaFrtNaimen.OptionsColumn.FixedWidth = true;
            gridColumnOtdelkaFrtNaimen.OptionsFilter.AllowAutoFilter = false;
            gridColumnOtdelkaFrtNaimen.OptionsFilter.AllowFilter = false;
            gridColumnOtdelkaFrtNaimen.Visible = true;
            gridColumnOtdelkaFrtNaimen.VisibleIndex = 2;
            gridColumnOtdelkaFrtNaimen.Width = 58;
            // 
            // gridColumnOtdelkaKolSlZv
            // 
            gridColumnOtdelkaKolSlZv.Caption = "Кол-во/слож/цвет/прогон";
            gridColumnOtdelkaKolSlZv.MinWidth = 23;
            gridColumnOtdelkaKolSlZv.Name = "gridColumnOtdelkaKolSlZv";
            gridColumnOtdelkaKolSlZv.OptionsColumn.FixedWidth = true;
            gridColumnOtdelkaKolSlZv.OptionsFilter.AllowAutoFilter = false;
            gridColumnOtdelkaKolSlZv.OptionsFilter.AllowFilter = false;
            gridColumnOtdelkaKolSlZv.Visible = true;
            gridColumnOtdelkaKolSlZv.VisibleIndex = 1;
            gridColumnOtdelkaKolSlZv.Width = 163;
            // 
            // gridColumnOtdelkaPsaFieldName
            // 
            gridColumnOtdelkaPsaFieldName.Caption = "Вид отделки";
            gridColumnOtdelkaPsaFieldName.MinWidth = 23;
            gridColumnOtdelkaPsaFieldName.Name = "gridColumnOtdelkaPsaFieldName";
            gridColumnOtdelkaPsaFieldName.OptionsColumn.FixedWidth = true;
            gridColumnOtdelkaPsaFieldName.OptionsFilter.AllowAutoFilter = false;
            gridColumnOtdelkaPsaFieldName.OptionsFilter.AllowFilter = false;
            gridColumnOtdelkaPsaFieldName.Visible = true;
            gridColumnOtdelkaPsaFieldName.VisibleIndex = 0;
            gridColumnOtdelkaPsaFieldName.Width = 105;
            // 
            // gridControlOtdelka
            // 
            tablePanel5.SetColumn(gridControlOtdelka, 18);
            gridControlOtdelka.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlOtdelka.Font = new Font("Arial", 10F);
            gridControlOtdelka.Location = new Point(1025, 16);
            gridControlOtdelka.MainView = gridViewOtdelka;
            gridControlOtdelka.Margin = new Padding(4, 3, 4, 3);
            gridControlOtdelka.Name = "gridControlOtdelka";
            tablePanel5.SetRow(gridControlOtdelka, 0);
            tablePanel5.SetRowSpan(gridControlOtdelka, 6);
            gridControlOtdelka.Size = new Size(789, 172);
            gridControlOtdelka.TabIndex = 5;
            gridControlOtdelka.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewOtdelka });
            // 
            // WorkInfo
            // 
            WorkInfo.Appearance.HeaderActive.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            WorkInfo.Appearance.HeaderActive.Options.UseFont = true;
            WorkInfo.Margin = new Padding(4, 3, 4, 3);
            WorkInfo.Name = "WorkInfo";
            WorkInfo.Size = new Size(1848, 698);
            WorkInfo.Text = "ВЫПОЛНЕННАЯ РАБОТА";
            // 
            // FurnInfo
            // 
            FurnInfo.Controls.Add(splitContainer1);
            FurnInfo.Controls.Add(customGroupBox6);
            FurnInfo.Controls.Add(panelControl7);
            FurnInfo.Margin = new Padding(4, 3, 4, 3);
            FurnInfo.Name = "FurnInfo";
            FurnInfo.Size = new Size(1848, 698);
            FurnInfo.Text = "КОНФЕКЦИОН";
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(273, 6);
            splitContainer1.Margin = new Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(furnitZayavViewFurnit);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(furnitZayavViewUpak);
            splitContainer1.Size = new Size(1561, 683);
            splitContainer1.SplitterDistance = 341;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 6;
            // 
            // furnitZayavViewFurnit
            // 
            furnitZayavViewFurnit.Dock = DockStyle.Fill;
            furnitZayavViewFurnit.Location = new Point(0, 0);
            furnitZayavViewFurnit.Margin = new Padding(5, 3, 5, 3);
            furnitZayavViewFurnit.Name = "furnitZayavViewFurnit";
            furnitZayavViewFurnit.Size = new Size(1561, 341);
            furnitZayavViewFurnit.TabIndex = 4;
            // 
            // furnitZayavViewUpak
            // 
            furnitZayavViewUpak.Dock = DockStyle.Fill;
            furnitZayavViewUpak.Location = new Point(0, 0);
            furnitZayavViewUpak.Margin = new Padding(5, 3, 5, 3);
            furnitZayavViewUpak.Name = "furnitZayavViewUpak";
            furnitZayavViewUpak.Size = new Size(1561, 337);
            furnitZayavViewUpak.TabIndex = 3;
            // 
            // customGroupBox6
            // 
            customGroupBox6.BackColor = Color.Transparent;
            customGroupBox6.Controls.Add(tablePanel7);
            customGroupBox6.Font = new Font("Arial", 12F, FontStyle.Bold);
            customGroupBox6.Location = new Point(1, 3);
            customGroupBox6.Margin = new Padding(4, 3, 4, 3);
            customGroupBox6.Name = "customGroupBox6";
            customGroupBox6.Padding = new Padding(4, 3, 4, 3);
            customGroupBox6.Size = new Size(268, 687);
            customGroupBox6.TabIndex = 5;
            customGroupBox6.TabStop = false;
            customGroupBox6.Text = "УСЛОВИЯ ДЛЯ СОЗДАНИЯ ЗАЯВОК";
            // 
            // tablePanel7
            // 
            tablePanel7.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 72F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 58F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 14F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 20F) });
            tablePanel7.Controls.Add(simpleButtonFullKKPrint);
            tablePanel7.Controls.Add(simpleButtonUpakDeliveryInfoShow);
            tablePanel7.Controls.Add(simpleButtonFurnDeliveryInfoShow);
            tablePanel7.Controls.Add(simpleButtonZayavUpakPrint);
            tablePanel7.Controls.Add(simpleButtonUpakKKPrint);
            tablePanel7.Controls.Add(simpleButtonZayavFurnPrint);
            tablePanel7.Controls.Add(simpleButtonFurnKKPrint);
            tablePanel7.Controls.Add(tbDatZayav);
            tablePanel7.Controls.Add(tbOtgrStat);
            tablePanel7.Controls.Add(mtbData_cd);
            tablePanel7.Controls.Add(label61);
            tablePanel7.Controls.Add(mtbData_zeh);
            tablePanel7.Controls.Add(label60);
            tablePanel7.Controls.Add(tbIs_got);
            tablePanel7.Controls.Add(tbData_f_z_u);
            tablePanel7.Controls.Add(tbFurnKKStat);
            tablePanel7.Controls.Add(tbData_f_o_u);
            tablePanel7.Controls.Add(label59);
            tablePanel7.Controls.Add(tbData_f_z);
            tablePanel7.Controls.Add(tbUpakKKStat);
            tablePanel7.Controls.Add(tbUZSobrStat);
            tablePanel7.Controls.Add(tbData_f_o);
            tablePanel7.Controls.Add(label25);
            tablePanel7.Controls.Add(label56);
            tablePanel7.Controls.Add(tbFurnZayav);
            tablePanel7.Controls.Add(tbUZSozdStat);
            tablePanel7.Controls.Add(label54);
            tablePanel7.Controls.Add(tbFZSozdStat);
            tablePanel7.Controls.Add(label55);
            tablePanel7.Controls.Add(label57);
            tablePanel7.Controls.Add(tbFZSobrStat);
            tablePanel7.Controls.Add(tbUpakZayav);
            tablePanel7.Controls.Add(label58);
            tablePanel7.Dock = DockStyle.Fill;
            tablePanel7.Location = new Point(4, 22);
            tablePanel7.Margin = new Padding(4, 3, 4, 3);
            tablePanel7.Name = "tablePanel7";
            tablePanel7.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 45F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel7.Size = new Size(260, 662);
            tablePanel7.TabIndex = 0;
            tablePanel7.UseSkinIndents = true;
            // 
            // simpleButtonFullKKPrint
            // 
            simpleButtonFullKKPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonFullKKPrint.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonFullKKPrint.Appearance.Options.UseFont = true;
            simpleButtonFullKKPrint.Appearance.Options.UseForeColor = true;
            tablePanel7.SetColumn(simpleButtonFullKKPrint, 0);
            tablePanel7.SetColumnSpan(simpleButtonFullKKPrint, 6);
            simpleButtonFullKKPrint.Dock = DockStyle.Fill;
            simpleButtonFullKKPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonFullKKPrint.ImageOptions.Image");
            simpleButtonFullKKPrint.Location = new Point(15, 13);
            simpleButtonFullKKPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonFullKKPrint.Name = "simpleButtonFullKKPrint";
            tablePanel7.SetRow(simpleButtonFullKKPrint, 0);
            simpleButtonFullKKPrint.Size = new Size(230, 34);
            simpleButtonFullKKPrint.TabIndex = 78;
            simpleButtonFullKKPrint.Text = "КК общая (просмотр/печать)";
            simpleButtonFullKKPrint.Click += simpleButtonFullKKPrint_Click_1;
            // 
            // simpleButtonUpakDeliveryInfoShow
            // 
            simpleButtonUpakDeliveryInfoShow.Appearance.Font = new Font("Arial", 10F);
            simpleButtonUpakDeliveryInfoShow.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonUpakDeliveryInfoShow.Appearance.Options.UseFont = true;
            simpleButtonUpakDeliveryInfoShow.Appearance.Options.UseForeColor = true;
            tablePanel7.SetColumn(simpleButtonUpakDeliveryInfoShow, 0);
            tablePanel7.SetColumnSpan(simpleButtonUpakDeliveryInfoShow, 6);
            simpleButtonUpakDeliveryInfoShow.Dock = DockStyle.Fill;
            simpleButtonUpakDeliveryInfoShow.ImageOptions.Image = (Image)resources.GetObject("simpleButtonUpakDeliveryInfoShow.ImageOptions.Image");
            simpleButtonUpakDeliveryInfoShow.Location = new Point(15, 377);
            simpleButtonUpakDeliveryInfoShow.Margin = new Padding(4, 3, 4, 3);
            simpleButtonUpakDeliveryInfoShow.Name = "simpleButtonUpakDeliveryInfoShow";
            tablePanel7.SetRow(simpleButtonUpakDeliveryInfoShow, 15);
            simpleButtonUpakDeliveryInfoShow.Size = new Size(230, 19);
            simpleButtonUpakDeliveryInfoShow.TabIndex = 77;
            simpleButtonUpakDeliveryInfoShow.Text = "Инфо по доставке упак";
            simpleButtonUpakDeliveryInfoShow.Click += simpleButtonUpakDeliveryInfoShow_Click;
            // 
            // simpleButtonFurnDeliveryInfoShow
            // 
            simpleButtonFurnDeliveryInfoShow.Appearance.Font = new Font("Arial", 10F);
            simpleButtonFurnDeliveryInfoShow.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonFurnDeliveryInfoShow.Appearance.Options.UseBackColor = true;
            simpleButtonFurnDeliveryInfoShow.Appearance.Options.UseFont = true;
            simpleButtonFurnDeliveryInfoShow.Appearance.Options.UseForeColor = true;
            tablePanel7.SetColumn(simpleButtonFurnDeliveryInfoShow, 0);
            tablePanel7.SetColumnSpan(simpleButtonFurnDeliveryInfoShow, 6);
            simpleButtonFurnDeliveryInfoShow.Dock = DockStyle.Fill;
            simpleButtonFurnDeliveryInfoShow.ImageOptions.Image = (Image)resources.GetObject("simpleButtonFurnDeliveryInfoShow.ImageOptions.Image");
            simpleButtonFurnDeliveryInfoShow.Location = new Point(15, 196);
            simpleButtonFurnDeliveryInfoShow.Margin = new Padding(4, 3, 4, 3);
            simpleButtonFurnDeliveryInfoShow.Name = "simpleButtonFurnDeliveryInfoShow";
            tablePanel7.SetRow(simpleButtonFurnDeliveryInfoShow, 7);
            simpleButtonFurnDeliveryInfoShow.Size = new Size(230, 20);
            simpleButtonFurnDeliveryInfoShow.TabIndex = 76;
            simpleButtonFurnDeliveryInfoShow.Text = "Инфо по доставке фурн";
            simpleButtonFurnDeliveryInfoShow.Click += simpleButtonFurnUpakDeliveryInfoShow_Click;
            // 
            // simpleButtonZayavUpakPrint
            // 
            simpleButtonZayavUpakPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonZayavUpakPrint.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonZayavUpakPrint.Appearance.Options.UseFont = true;
            simpleButtonZayavUpakPrint.Appearance.Options.UseForeColor = true;
            tablePanel7.SetColumn(simpleButtonZayavUpakPrint, 0);
            tablePanel7.SetColumnSpan(simpleButtonZayavUpakPrint, 6);
            simpleButtonZayavUpakPrint.Dock = DockStyle.Fill;
            simpleButtonZayavUpakPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonZayavUpakPrint.ImageOptions.Image");
            simpleButtonZayavUpakPrint.Location = new Point(15, 352);
            simpleButtonZayavUpakPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonZayavUpakPrint.Name = "simpleButtonZayavUpakPrint";
            tablePanel7.SetRow(simpleButtonZayavUpakPrint, 14);
            simpleButtonZayavUpakPrint.Size = new Size(230, 19);
            simpleButtonZayavUpakPrint.TabIndex = 75;
            simpleButtonZayavUpakPrint.Text = "ПРОСМОТР / ПЕЧАТЬ";
            simpleButtonZayavUpakPrint.Click += simpleButtonZayavUpakPrint_Click;
            // 
            // simpleButtonUpakKKPrint
            // 
            simpleButtonUpakKKPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonUpakKKPrint.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonUpakKKPrint.Appearance.Options.UseFont = true;
            simpleButtonUpakKKPrint.Appearance.Options.UseForeColor = true;
            tablePanel7.SetColumn(simpleButtonUpakKKPrint, 0);
            tablePanel7.SetColumnSpan(simpleButtonUpakKKPrint, 5);
            simpleButtonUpakKKPrint.Dock = DockStyle.Fill;
            simpleButtonUpakKKPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonUpakKKPrint.ImageOptions.Image");
            simpleButtonUpakKKPrint.Location = new Point(15, 246);
            simpleButtonUpakKKPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonUpakKKPrint.Name = "simpleButtonUpakKKPrint";
            tablePanel7.SetRow(simpleButtonUpakKKPrint, 10);
            simpleButtonUpakKKPrint.Size = new Size(169, 34);
            simpleButtonUpakKKPrint.TabIndex = 74;
            simpleButtonUpakKKPrint.Text = "КК на упаковку \r\n(просмотр/печать)";
            simpleButtonUpakKKPrint.Click += simpleButtonUpakKKPrint_Click;
            // 
            // simpleButtonZayavFurnPrint
            // 
            simpleButtonZayavFurnPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonZayavFurnPrint.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonZayavFurnPrint.Appearance.Options.UseFont = true;
            simpleButtonZayavFurnPrint.Appearance.Options.UseForeColor = true;
            tablePanel7.SetColumn(simpleButtonZayavFurnPrint, 0);
            tablePanel7.SetColumnSpan(simpleButtonZayavFurnPrint, 6);
            simpleButtonZayavFurnPrint.Dock = DockStyle.Fill;
            simpleButtonZayavFurnPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonZayavFurnPrint.ImageOptions.Image");
            simpleButtonZayavFurnPrint.Location = new Point(15, 171);
            simpleButtonZayavFurnPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonZayavFurnPrint.Name = "simpleButtonZayavFurnPrint";
            tablePanel7.SetRow(simpleButtonZayavFurnPrint, 6);
            simpleButtonZayavFurnPrint.Size = new Size(230, 19);
            simpleButtonZayavFurnPrint.TabIndex = 73;
            simpleButtonZayavFurnPrint.Text = "ПРОСМОТР / ПЕЧАТЬ";
            simpleButtonZayavFurnPrint.Click += simpleButtonZayavFurnPrint_Click;
            // 
            // simpleButtonFurnKKPrint
            // 
            simpleButtonFurnKKPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonFurnKKPrint.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonFurnKKPrint.Appearance.Options.UseFont = true;
            simpleButtonFurnKKPrint.Appearance.Options.UseForeColor = true;
            simpleButtonFurnKKPrint.Appearance.Options.UseTextOptions = true;
            simpleButtonFurnKKPrint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            tablePanel7.SetColumn(simpleButtonFurnKKPrint, 0);
            tablePanel7.SetColumnSpan(simpleButtonFurnKKPrint, 5);
            simpleButtonFurnKKPrint.Dock = DockStyle.Fill;
            simpleButtonFurnKKPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonFurnKKPrint.ImageOptions.Image");
            simpleButtonFurnKKPrint.Location = new Point(15, 65);
            simpleButtonFurnKKPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonFurnKKPrint.Name = "simpleButtonFurnKKPrint";
            tablePanel7.SetRow(simpleButtonFurnKKPrint, 2);
            simpleButtonFurnKKPrint.Size = new Size(169, 34);
            simpleButtonFurnKKPrint.TabIndex = 72;
            simpleButtonFurnKKPrint.Text = "КК на фурнитуру (просмотр/печать)";
            simpleButtonFurnKKPrint.Click += simpleButtonFurnKKPrint_Click;
            // 
            // tbDatZayav
            // 
            tbDatZayav.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbDatZayav, 1);
            tablePanel7.SetColumnSpan(tbDatZayav, 5);
            tbDatZayav.Font = new Font("Arial", 10F);
            tbDatZayav.ForeColor = Color.FromArgb(72, 61, 139);
            tbDatZayav.Location = new Point(83, 574);
            tbDatZayav.Margin = new Padding(0);
            tbDatZayav.Name = "tbDatZayav";
            tablePanel7.SetRow(tbDatZayav, 21);
            tbDatZayav.Size = new Size(166, 23);
            tbDatZayav.TabIndex = 70;
            // 
            // tbOtgrStat
            // 
            tbOtgrStat.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbOtgrStat, 5);
            tbOtgrStat.Font = new Font("Arial", 10F);
            tbOtgrStat.ForeColor = Color.FromArgb(72, 61, 139);
            tbOtgrStat.Location = new Point(188, 436);
            tbOtgrStat.Margin = new Padding(0);
            tbOtgrStat.Name = "tbOtgrStat";
            tablePanel7.SetRow(tbOtgrStat, 18);
            tbOtgrStat.Size = new Size(61, 23);
            tbOtgrStat.TabIndex = 59;
            tbOtgrStat.TextAlign = HorizontalAlignment.Center;
            // 
            // mtbData_cd
            // 
            mtbData_cd.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(mtbData_cd, 1);
            tablePanel7.SetColumnSpan(mtbData_cd, 3);
            mtbData_cd.Font = new Font("Arial", 9F);
            mtbData_cd.ForeColor = Color.FromArgb(72, 61, 139);
            mtbData_cd.Location = new Point(87, 437);
            mtbData_cd.Margin = new Padding(4, 3, 4, 3);
            mtbData_cd.Mask = "00/00/0000";
            mtbData_cd.Name = "mtbData_cd";
            tablePanel7.SetRow(mtbData_cd, 18);
            mtbData_cd.Size = new Size(89, 21);
            mtbData_cd.TabIndex = 65;
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label61, 0);
            tablePanel7.SetColumnSpan(label61, 6);
            label61.Dock = DockStyle.Fill;
            label61.Font = new Font("Arial", 9F);
            label61.ForeColor = Color.FromArgb(0, 0, 0);
            label61.Location = new Point(15, 475);
            label61.Margin = new Padding(4, 0, 4, 0);
            label61.Name = "label61";
            tablePanel7.SetRow(label61, 20);
            label61.Size = new Size(230, 45);
            label61.TabIndex = 0;
            label61.Text = "Предположительная дата создания заявки (при выполнении всех условий)";
            label61.TextAlign = ContentAlignment.TopCenter;
            // 
            // mtbData_zeh
            // 
            mtbData_zeh.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(mtbData_zeh, 1);
            tablePanel7.SetColumnSpan(mtbData_zeh, 3);
            mtbData_zeh.Font = new Font("Arial", 9F);
            mtbData_zeh.ForeColor = Color.FromArgb(72, 61, 139);
            mtbData_zeh.Location = new Point(87, 414);
            mtbData_zeh.Margin = new Padding(4, 3, 4, 3);
            mtbData_zeh.Mask = "00/00/0000";
            mtbData_zeh.Name = "mtbData_zeh";
            tablePanel7.SetRow(mtbData_zeh, 17);
            mtbData_zeh.Size = new Size(89, 21);
            mtbData_zeh.TabIndex = 64;
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label60, 0);
            label60.Font = new Font("Arial", 9F);
            label60.ForeColor = Color.FromArgb(0, 0, 0);
            label60.Location = new Point(15, 433);
            label60.Margin = new Padding(4, 0, 4, 0);
            label60.Name = "label60";
            tablePanel7.SetRow(label60, 18);
            label60.Size = new Size(61, 30);
            label60.TabIndex = 55;
            label60.Text = "Дата отгрузки \r\nс производства";
            // 
            // tbIs_got
            // 
            tbIs_got.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbIs_got, 5);
            tbIs_got.Font = new Font("Arial", 10F);
            tbIs_got.ForeColor = Color.FromArgb(72, 61, 139);
            tbIs_got.Location = new Point(188, 411);
            tbIs_got.Margin = new Padding(0);
            tbIs_got.Name = "tbIs_got";
            tablePanel7.SetRow(tbIs_got, 17);
            tbIs_got.Size = new Size(61, 23);
            tbIs_got.TabIndex = 58;
            tbIs_got.TextAlign = HorizontalAlignment.Center;
            // 
            // tbData_f_z_u
            // 
            tbData_f_z_u.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbData_f_z_u, 1);
            tablePanel7.SetColumnSpan(tbData_f_z_u, 3);
            tbData_f_z_u.Font = new Font("Arial", 10F);
            tbData_f_z_u.ForeColor = Color.FromArgb(72, 61, 139);
            tbData_f_z_u.Location = new Point(83, 327);
            tbData_f_z_u.Margin = new Padding(0);
            tbData_f_z_u.Name = "tbData_f_z_u";
            tablePanel7.SetRow(tbData_f_z_u, 13);
            tbData_f_z_u.Size = new Size(97, 23);
            tbData_f_z_u.TabIndex = 69;
            // 
            // tbFurnKKStat
            // 
            tbFurnKKStat.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbFurnKKStat, 5);
            tbFurnKKStat.Font = new Font("Arial", 10F);
            tbFurnKKStat.ForeColor = Color.FromArgb(72, 61, 139);
            tbFurnKKStat.Location = new Point(188, 70);
            tbFurnKKStat.Margin = new Padding(0);
            tbFurnKKStat.Name = "tbFurnKKStat";
            tablePanel7.SetRow(tbFurnKKStat, 2);
            tbFurnKKStat.Size = new Size(61, 23);
            tbFurnKKStat.TabIndex = 33;
            tbFurnKKStat.TextAlign = HorizontalAlignment.Center;
            // 
            // tbData_f_o_u
            // 
            tbData_f_o_u.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbData_f_o_u, 1);
            tablePanel7.SetColumnSpan(tbData_f_o_u, 3);
            tbData_f_o_u.Font = new Font("Arial", 10F);
            tbData_f_o_u.ForeColor = Color.FromArgb(72, 61, 139);
            tbData_f_o_u.Location = new Point(83, 305);
            tbData_f_o_u.Margin = new Padding(0);
            tbData_f_o_u.Name = "tbData_f_o_u";
            tablePanel7.SetRow(tbData_f_o_u, 12);
            tbData_f_o_u.Size = new Size(97, 23);
            tbData_f_o_u.TabIndex = 68;
            // 
            // label59
            // 
            label59.AutoSize = true;
            label59.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label59, 0);
            label59.Font = new Font("Arial", 9F);
            label59.ForeColor = Color.FromArgb(0, 0, 0);
            label59.Location = new Point(15, 411);
            label59.Margin = new Padding(4, 0, 4, 0);
            label59.Name = "label59";
            tablePanel7.SetRow(label59, 17);
            label59.Size = new Size(46, 22);
            label59.TabIndex = 54;
            label59.Text = "Дата в цех";
            // 
            // tbData_f_z
            // 
            tbData_f_z.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbData_f_z, 1);
            tablePanel7.SetColumnSpan(tbData_f_z, 3);
            tbData_f_z.Font = new Font("Arial", 10F);
            tbData_f_z.ForeColor = Color.FromArgb(72, 61, 139);
            tbData_f_z.Location = new Point(83, 146);
            tbData_f_z.Margin = new Padding(0);
            tbData_f_z.Name = "tbData_f_z";
            tablePanel7.SetRow(tbData_f_z, 5);
            tbData_f_z.Size = new Size(97, 23);
            tbData_f_z.TabIndex = 67;
            // 
            // tbUpakKKStat
            // 
            tbUpakKKStat.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbUpakKKStat, 5);
            tbUpakKKStat.Font = new Font("Arial", 10F);
            tbUpakKKStat.ForeColor = Color.FromArgb(72, 61, 139);
            tbUpakKKStat.Location = new Point(188, 251);
            tbUpakKKStat.Margin = new Padding(0);
            tbUpakKKStat.Name = "tbUpakKKStat";
            tablePanel7.SetRow(tbUpakKKStat, 10);
            tbUpakKKStat.Size = new Size(61, 23);
            tbUpakKKStat.TabIndex = 34;
            tbUpakKKStat.TextAlign = HorizontalAlignment.Center;
            // 
            // tbUZSobrStat
            // 
            tbUZSobrStat.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbUZSobrStat, 5);
            tbUZSobrStat.Font = new Font("Arial", 10F);
            tbUZSobrStat.ForeColor = Color.FromArgb(72, 61, 139);
            tbUZSobrStat.Location = new Point(188, 327);
            tbUZSobrStat.Margin = new Padding(0);
            tbUZSobrStat.Name = "tbUZSobrStat";
            tablePanel7.SetRow(tbUZSobrStat, 13);
            tbUZSobrStat.Size = new Size(61, 23);
            tbUZSobrStat.TabIndex = 52;
            tbUZSobrStat.TextAlign = HorizontalAlignment.Center;
            // 
            // tbData_f_o
            // 
            tbData_f_o.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbData_f_o, 1);
            tablePanel7.SetColumnSpan(tbData_f_o, 3);
            tbData_f_o.Font = new Font("Arial", 10F);
            tbData_f_o.ForeColor = Color.FromArgb(72, 61, 139);
            tbData_f_o.Location = new Point(83, 124);
            tbData_f_o.Margin = new Padding(0);
            tbData_f_o.Name = "tbData_f_o";
            tablePanel7.SetRow(tbData_f_o, 4);
            tbData_f_o.Size = new Size(97, 23);
            tbData_f_o.TabIndex = 66;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label25, 0);
            tablePanel7.SetColumnSpan(label25, 4);
            label25.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            label25.ForeColor = Color.FromArgb(0, 0, 0);
            label25.Location = new Point(15, 106);
            label25.Margin = new Padding(4, 0, 4, 0);
            label25.Name = "label25";
            tablePanel7.SetRow(label25, 3);
            label25.Size = new Size(150, 13);
            label25.TabIndex = 36;
            label25.Text = "Заявка на фурнитуру №";
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label56, 0);
            label56.Font = new Font("Arial", 9F);
            label56.ForeColor = Color.FromArgb(0, 0, 0);
            label56.Location = new Point(15, 330);
            label56.Margin = new Padding(4, 0, 4, 0);
            label56.Name = "label56";
            tablePanel7.SetRow(label56, 13);
            label56.Size = new Size(55, 15);
            label56.TabIndex = 49;
            label56.Text = "собрана";
            // 
            // tbFurnZayav
            // 
            tbFurnZayav.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbFurnZayav, 3);
            tablePanel7.SetColumnSpan(tbFurnZayav, 3);
            tbFurnZayav.Font = new Font("Arial", 10F);
            tbFurnZayav.ForeColor = Color.FromArgb(72, 61, 139);
            tbFurnZayav.Location = new Point(166, 102);
            tbFurnZayav.Margin = new Padding(0);
            tbFurnZayav.Name = "tbFurnZayav";
            tablePanel7.SetRow(tbFurnZayav, 3);
            tbFurnZayav.Size = new Size(83, 23);
            tbFurnZayav.TabIndex = 37;
            // 
            // tbUZSozdStat
            // 
            tbUZSozdStat.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbUZSozdStat, 5);
            tbUZSozdStat.Font = new Font("Arial", 10F);
            tbUZSozdStat.ForeColor = Color.FromArgb(72, 61, 139);
            tbUZSozdStat.Location = new Point(188, 305);
            tbUZSozdStat.Margin = new Padding(0);
            tbUZSozdStat.Name = "tbUZSozdStat";
            tablePanel7.SetRow(tbUZSozdStat, 12);
            tbUZSozdStat.Size = new Size(61, 23);
            tbUZSozdStat.TabIndex = 48;
            tbUZSozdStat.TextAlign = HorizontalAlignment.Center;
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label54, 0);
            label54.Font = new Font("Arial", 9F);
            label54.ForeColor = Color.FromArgb(0, 0, 0);
            label54.Location = new Point(15, 127);
            label54.Margin = new Padding(4, 0, 4, 0);
            label54.Name = "label54";
            tablePanel7.SetRow(label54, 4);
            label54.Size = new Size(54, 15);
            label54.TabIndex = 38;
            label54.Text = "создана";
            // 
            // tbFZSozdStat
            // 
            tbFZSozdStat.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbFZSozdStat, 5);
            tbFZSozdStat.Font = new Font("Arial", 10F);
            tbFZSozdStat.ForeColor = Color.FromArgb(72, 61, 139);
            tbFZSozdStat.Location = new Point(188, 124);
            tbFZSozdStat.Margin = new Padding(0);
            tbFZSozdStat.Name = "tbFZSozdStat";
            tablePanel7.SetRow(tbFZSozdStat, 4);
            tbFZSozdStat.Size = new Size(61, 23);
            tbFZSozdStat.TabIndex = 39;
            tbFZSozdStat.TextAlign = HorizontalAlignment.Center;
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label55, 0);
            label55.Font = new Font("Arial", 9F);
            label55.ForeColor = Color.FromArgb(0, 0, 0);
            label55.Location = new Point(15, 149);
            label55.Margin = new Padding(4, 0, 4, 0);
            label55.Name = "label55";
            tablePanel7.SetRow(label55, 5);
            label55.Size = new Size(55, 15);
            label55.TabIndex = 40;
            label55.Text = "собрана";
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label57, 0);
            label57.Font = new Font("Arial", 9F);
            label57.ForeColor = Color.FromArgb(0, 0, 0);
            label57.Location = new Point(15, 308);
            label57.Margin = new Padding(4, 0, 4, 0);
            label57.Name = "label57";
            tablePanel7.SetRow(label57, 12);
            label57.Size = new Size(54, 15);
            label57.TabIndex = 47;
            label57.Text = "создана";
            // 
            // tbFZSobrStat
            // 
            tbFZSobrStat.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbFZSobrStat, 5);
            tbFZSobrStat.Font = new Font("Arial", 10F);
            tbFZSobrStat.ForeColor = Color.FromArgb(72, 61, 139);
            tbFZSobrStat.Location = new Point(188, 146);
            tbFZSobrStat.Margin = new Padding(0);
            tbFZSobrStat.Name = "tbFZSobrStat";
            tablePanel7.SetRow(tbFZSobrStat, 5);
            tbFZSobrStat.Size = new Size(61, 23);
            tbFZSobrStat.TabIndex = 43;
            tbFZSobrStat.TextAlign = HorizontalAlignment.Center;
            // 
            // tbUpakZayav
            // 
            tbUpakZayav.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel7.SetColumn(tbUpakZayav, 3);
            tablePanel7.SetColumnSpan(tbUpakZayav, 3);
            tbUpakZayav.Font = new Font("Arial", 10F);
            tbUpakZayav.ForeColor = Color.FromArgb(72, 61, 139);
            tbUpakZayav.Location = new Point(166, 283);
            tbUpakZayav.Margin = new Padding(0);
            tbUpakZayav.Name = "tbUpakZayav";
            tablePanel7.SetRow(tbUpakZayav, 11);
            tbUpakZayav.Size = new Size(83, 23);
            tbUpakZayav.TabIndex = 46;
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.BackColor = Color.Transparent;
            tablePanel7.SetColumn(label58, 0);
            tablePanel7.SetColumnSpan(label58, 4);
            label58.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            label58.ForeColor = Color.FromArgb(0, 0, 0);
            label58.Location = new Point(15, 287);
            label58.Margin = new Padding(4, 0, 4, 0);
            label58.Name = "label58";
            tablePanel7.SetRow(label58, 11);
            label58.Size = new Size(142, 13);
            label58.TabIndex = 45;
            label58.Text = "Заявка на упаковку №";
            // 
            // panelControl7
            // 
            panelControl7.Location = new Point(4, 564);
            panelControl7.Margin = new Padding(4, 3, 4, 3);
            panelControl7.Name = "panelControl7";
            panelControl7.Size = new Size(245, 96);
            panelControl7.TabIndex = 1;
            // 
            // RasInfo
            // 
            RasInfo.Appearance.Header.Font = new Font("Tahoma", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            RasInfo.Appearance.Header.Options.UseFont = true;
            RasInfo.Appearance.HeaderActive.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            RasInfo.Appearance.HeaderActive.Options.UseFont = true;
            RasInfo.Appearance.HeaderDisabled.Font = new Font("Tahoma", 10F);
            RasInfo.Appearance.HeaderDisabled.Options.UseFont = true;
            RasInfo.Controls.Add(customGroupBox5);
            RasInfo.Controls.Add(customGroupBox4);
            RasInfo.Controls.Add(customGroupBox3);
            RasInfo.Controls.Add(ContDates);
            RasInfo.Margin = new Padding(4, 3, 4, 3);
            RasInfo.Name = "RasInfo";
            RasInfo.Size = new Size(1848, 698);
            RasInfo.Text = "ИНФОРМАЦИЯ ПО РАСЧЕТУ";
            // 
            // customGroupBox5
            // 
            customGroupBox5.BackColor = Color.Transparent;
            customGroupBox5.Controls.Add(tablePanel6);
            customGroupBox5.Font = new Font("Arial", 12F, FontStyle.Bold);
            customGroupBox5.Location = new Point(2, 602);
            customGroupBox5.Margin = new Padding(4, 3, 4, 3);
            customGroupBox5.Name = "customGroupBox5";
            customGroupBox5.Padding = new Padding(4, 3, 4, 3);
            customGroupBox5.Size = new Size(1837, 88);
            customGroupBox5.TabIndex = 9;
            customGroupBox5.TabStop = false;
            customGroupBox5.Text = "ДОКУМЕНТЫ";
            // 
            // tablePanel6
            // 
            tablePanel6.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 230F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 55F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 230F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 230F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F) });
            tablePanel6.Controls.Add(simpleButtonReestrListPrint);
            tablePanel6.Controls.Add(simpleButtonPrintMLRTUpak);
            tablePanel6.Controls.Add(simpleButtonPrintMLRTAll);
            tablePanel6.Dock = DockStyle.Fill;
            tablePanel6.Location = new Point(4, 22);
            tablePanel6.Margin = new Padding(4, 3, 4, 3);
            tablePanel6.Name = "tablePanel6";
            tablePanel6.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel6.Size = new Size(1829, 63);
            tablePanel6.TabIndex = 0;
            tablePanel6.UseSkinIndents = true;
            // 
            // simpleButtonReestrListPrint
            // 
            simpleButtonReestrListPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonReestrListPrint.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonReestrListPrint.Appearance.Options.UseFont = true;
            simpleButtonReestrListPrint.Appearance.Options.UseForeColor = true;
            tablePanel6.SetColumn(simpleButtonReestrListPrint, 4);
            simpleButtonReestrListPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonReestrListPrint.ImageOptions.Image");
            simpleButtonReestrListPrint.Location = new Point(515, 16);
            simpleButtonReestrListPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonReestrListPrint.Name = "simpleButtonReestrListPrint";
            tablePanel6.SetRow(simpleButtonReestrListPrint, 0);
            simpleButtonReestrListPrint.Size = new Size(222, 29);
            simpleButtonReestrListPrint.TabIndex = 11;
            simpleButtonReestrListPrint.Text = "Сопроводительные реестры";
            simpleButtonReestrListPrint.Click += simpleButtonReestrListPrint_Click;
            // 
            // simpleButtonPrintMLRTUpak
            // 
            simpleButtonPrintMLRTUpak.Appearance.Font = new Font("Arial", 10F);
            simpleButtonPrintMLRTUpak.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonPrintMLRTUpak.Appearance.Options.UseFont = true;
            simpleButtonPrintMLRTUpak.Appearance.Options.UseForeColor = true;
            tablePanel6.SetColumn(simpleButtonPrintMLRTUpak, 2);
            simpleButtonPrintMLRTUpak.ImageOptions.Image = (Image)resources.GetObject("simpleButtonPrintMLRTUpak.ImageOptions.Image");
            simpleButtonPrintMLRTUpak.Location = new Point(265, 16);
            simpleButtonPrintMLRTUpak.Margin = new Padding(4, 3, 4, 3);
            simpleButtonPrintMLRTUpak.Name = "simpleButtonPrintMLRTUpak";
            tablePanel6.SetRow(simpleButtonPrintMLRTUpak, 0);
            simpleButtonPrintMLRTUpak.Size = new Size(222, 29);
            simpleButtonPrintMLRTUpak.TabIndex = 10;
            simpleButtonPrintMLRTUpak.Text = "Задание упак.";
            simpleButtonPrintMLRTUpak.Click += simpleButtonPrintMLRTUpak_Click;
            // 
            // simpleButtonPrintMLRTAll
            // 
            simpleButtonPrintMLRTAll.Appearance.Font = new Font("Arial", 10F);
            simpleButtonPrintMLRTAll.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonPrintMLRTAll.Appearance.Options.UseFont = true;
            simpleButtonPrintMLRTAll.Appearance.Options.UseForeColor = true;
            tablePanel6.SetColumn(simpleButtonPrintMLRTAll, 0);
            simpleButtonPrintMLRTAll.ImageOptions.Image = (Image)resources.GetObject("simpleButtonPrintMLRTAll.ImageOptions.Image");
            simpleButtonPrintMLRTAll.Location = new Point(15, 16);
            simpleButtonPrintMLRTAll.Margin = new Padding(4, 3, 4, 3);
            simpleButtonPrintMLRTAll.Name = "simpleButtonPrintMLRTAll";
            tablePanel6.SetRow(simpleButtonPrintMLRTAll, 0);
            simpleButtonPrintMLRTAll.Size = new Size(222, 29);
            simpleButtonPrintMLRTAll.TabIndex = 9;
            simpleButtonPrintMLRTAll.Text = "Задание общ.";
            simpleButtonPrintMLRTAll.Click += simpleButtonPrintMLRTAll_Click;
            // 
            // customGroupBox4
            // 
            customGroupBox4.BackColor = Color.Transparent;
            customGroupBox4.Controls.Add(tablePanel5);
            customGroupBox4.Font = new Font("Arial", 12F, FontStyle.Bold);
            customGroupBox4.Location = new Point(2, 366);
            customGroupBox4.Margin = new Padding(4, 3, 4, 3);
            customGroupBox4.Name = "customGroupBox4";
            customGroupBox4.Padding = new Padding(4, 3, 4, 3);
            customGroupBox4.Size = new Size(1837, 230);
            customGroupBox4.TabIndex = 12;
            customGroupBox4.TabStop = false;
            customGroupBox4.Text = "ОТДЕЛКА / ДОП. ОБРАБОТКА";
            // 
            // tablePanel5
            // 
            tablePanel5.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 95F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 55F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F) });
            tablePanel5.Controls.Add(mtbRzuVidStir);
            tablePanel5.Controls.Add(cbRzuStirFact);
            tablePanel5.Controls.Add(label44);
            tablePanel5.Controls.Add(mtbRzuDataStCd);
            tablePanel5.Controls.Add(label32);
            tablePanel5.Controls.Add(mtbRzuDataStR);
            tablePanel5.Controls.Add(label49);
            tablePanel5.Controls.Add(cbPszStirPlan);
            tablePanel5.Controls.Add(mtbRzuDataStP);
            tablePanel5.Controls.Add(cbPszPrintPlan);
            tablePanel5.Controls.Add(label50);
            tablePanel5.Controls.Add(mtbRzuDataVCd);
            tablePanel5.Controls.Add(cbRzuVishFact);
            tablePanel5.Controls.Add(cbRzuPrintFact);
            tablePanel5.Controls.Add(label51);
            tablePanel5.Controls.Add(mtbRzuDataVChi);
            tablePanel5.Controls.Add(cbPszVishPlan);
            tablePanel5.Controls.Add(label37);
            tablePanel5.Controls.Add(mtbRzuDataVR);
            tablePanel5.Controls.Add(mtbRzuDataPrCd);
            tablePanel5.Controls.Add(label35);
            tablePanel5.Controls.Add(mtbRzuDataRasp);
            tablePanel5.Controls.Add(mtbRzuDataVP);
            tablePanel5.Controls.Add(label43);
            tablePanel5.Controls.Add(label38);
            tablePanel5.Controls.Add(mtbRzuDataPrKm);
            tablePanel5.Controls.Add(mtbRzuDataRasv);
            tablePanel5.Controls.Add(label45);
            tablePanel5.Controls.Add(mtbRzuDataPrP);
            tablePanel5.Controls.Add(label39);
            tablePanel5.Controls.Add(mtbRzuDataPrR);
            tablePanel5.Controls.Add(label46);
            tablePanel5.Controls.Add(mtbRzuDataPrPe);
            tablePanel5.Controls.Add(label40);
            tablePanel5.Controls.Add(label41);
            tablePanel5.Controls.Add(label47);
            tablePanel5.Controls.Add(label42);
            tablePanel5.Controls.Add(gridControlOtdelka);
            tablePanel5.Controls.Add(label36);
            tablePanel5.Controls.Add(label48);
            tablePanel5.Dock = DockStyle.Fill;
            tablePanel5.Location = new Point(4, 22);
            tablePanel5.Margin = new Padding(4, 3, 4, 3);
            tablePanel5.Name = "tablePanel5";
            tablePanel5.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 21F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 21F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 21F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F) });
            tablePanel5.Size = new Size(1829, 205);
            tablePanel5.TabIndex = 0;
            tablePanel5.UseSkinIndents = true;
            // 
            // mtbRzuVidStir
            // 
            mtbRzuVidStir.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuVidStir, 10);
            mtbRzuVidStir.Font = new Font("Arial", 9F);
            mtbRzuVidStir.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuVidStir.Location = new Point(650, 153);
            mtbRzuVidStir.Margin = new Padding(4, 3, 4, 3);
            mtbRzuVidStir.Mask = "00/00/0000";
            mtbRzuVidStir.Name = "mtbRzuVidStir";
            tablePanel5.SetRow(mtbRzuVidStir, 5);
            mtbRzuVidStir.Size = new Size(62, 21);
            mtbRzuVidStir.TabIndex = 63;
            // 
            // cbRzuStirFact
            // 
            cbRzuStirFact.AutoSize = true;
            tablePanel5.SetColumn(cbRzuStirFact, 4);
            cbRzuStirFact.Font = new Font("Arial", 10F);
            cbRzuStirFact.ForeColor = Color.FromArgb(72, 61, 139);
            cbRzuStirFact.Location = new Point(285, 115);
            cbRzuStirFact.Margin = new Padding(4, 3, 4, 3);
            cbRzuStirFact.Name = "cbRzuStirFact";
            tablePanel5.SetRow(cbRzuStirFact, 4);
            cbRzuStirFact.Size = new Size(58, 15);
            cbRzuStirFact.TabIndex = 72;
            cbRzuStirFact.Text = "Факт";
            cbRzuStirFact.UseVisualStyleBackColor = true;
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label44, 9);
            label44.Font = new Font("Arial", 9F);
            label44.ForeColor = Color.FromArgb(0, 0, 0);
            label44.Location = new Point(555, 156);
            label44.Margin = new Padding(4, 0, 4, 0);
            label44.Name = "label44";
            tablePanel5.SetRow(label44, 5);
            label44.Size = new Size(70, 15);
            label44.TabIndex = 52;
            label44.Text = "Вид стирки";
            // 
            // mtbRzuDataStCd
            // 
            mtbRzuDataStCd.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataStCd, 7);
            mtbRzuDataStCd.Font = new Font("Arial", 9F);
            mtbRzuDataStCd.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataStCd.Location = new Point(465, 153);
            mtbRzuDataStCd.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataStCd.Mask = "00/00/0000";
            mtbRzuDataStCd.Name = "mtbRzuDataStCd";
            tablePanel5.SetRow(mtbRzuDataStCd, 5);
            mtbRzuDataStCd.Size = new Size(62, 21);
            mtbRzuDataStCd.TabIndex = 60;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label32, 0);
            label32.Font = new Font("Arial", 10F, FontStyle.Bold);
            label32.ForeColor = Color.FromArgb(0, 0, 0);
            label32.Location = new Point(15, 12);
            label32.Margin = new Padding(4, 0, 4, 0);
            label32.Name = "label32";
            tablePanel5.SetRow(label32, 0);
            label32.Size = new Size(54, 16);
            label32.TabIndex = 9;
            label32.Text = "ПРИНТ";
            // 
            // mtbRzuDataStR
            // 
            mtbRzuDataStR.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataStR, 4);
            mtbRzuDataStR.Font = new Font("Arial", 9F);
            mtbRzuDataStR.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataStR.Location = new Point(285, 153);
            mtbRzuDataStR.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataStR.Mask = "00/00/0000";
            mtbRzuDataStR.Name = "mtbRzuDataStR";
            tablePanel5.SetRow(mtbRzuDataStR, 5);
            mtbRzuDataStR.Size = new Size(62, 21);
            mtbRzuDataStR.TabIndex = 57;
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label49, 6);
            label49.Font = new Font("Arial", 9F);
            label49.ForeColor = Color.FromArgb(0, 0, 0);
            label49.Location = new Point(375, 156);
            label49.Margin = new Padding(4, 0, 4, 0);
            label49.Name = "label49";
            tablePanel5.SetRow(label49, 5);
            label49.Size = new Size(70, 15);
            label49.TabIndex = 50;
            label49.Text = "Дата сдачи";
            // 
            // cbPszStirPlan
            // 
            cbPszStirPlan.AutoSize = true;
            tablePanel5.SetColumn(cbPszStirPlan, 3);
            cbPszStirPlan.Font = new Font("Arial", 10F);
            cbPszStirPlan.ForeColor = Color.FromArgb(72, 61, 139);
            cbPszStirPlan.Location = new Point(195, 115);
            cbPszStirPlan.Margin = new Padding(4, 3, 4, 3);
            cbPszStirPlan.Name = "cbPszStirPlan";
            tablePanel5.SetRow(cbPszStirPlan, 4);
            cbPszStirPlan.Size = new Size(59, 15);
            cbPszStirPlan.TabIndex = 71;
            cbPszStirPlan.Text = "План";
            cbPszStirPlan.UseVisualStyleBackColor = true;
            // 
            // mtbRzuDataStP
            // 
            mtbRzuDataStP.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataStP, 1);
            mtbRzuDataStP.Font = new Font("Arial", 9F);
            mtbRzuDataStP.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataStP.Location = new Point(105, 153);
            mtbRzuDataStP.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataStP.Mask = "00/00/0000";
            mtbRzuDataStP.Name = "mtbRzuDataStP";
            tablePanel5.SetRow(mtbRzuDataStP, 5);
            mtbRzuDataStP.Size = new Size(62, 21);
            mtbRzuDataStP.TabIndex = 54;
            // 
            // cbPszPrintPlan
            // 
            cbPszPrintPlan.AutoSize = true;
            tablePanel5.SetColumn(cbPszPrintPlan, 3);
            cbPszPrintPlan.Font = new Font("Arial", 10F);
            cbPszPrintPlan.ForeColor = Color.FromArgb(72, 61, 139);
            cbPszPrintPlan.Location = new Point(195, 13);
            cbPszPrintPlan.Margin = new Padding(4, 3, 4, 3);
            cbPszPrintPlan.Name = "cbPszPrintPlan";
            tablePanel5.SetRow(cbPszPrintPlan, 0);
            cbPszPrintPlan.Size = new Size(59, 15);
            cbPszPrintPlan.TabIndex = 67;
            cbPszPrintPlan.Text = "План";
            cbPszPrintPlan.UseVisualStyleBackColor = true;
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label50, 3);
            label50.Font = new Font("Arial", 9F);
            label50.ForeColor = Color.FromArgb(0, 0, 0);
            label50.Location = new Point(195, 156);
            label50.Margin = new Padding(4, 0, 4, 0);
            label50.Name = "label50";
            tablePanel5.SetRow(label50, 5);
            label50.Size = new Size(75, 15);
            label50.TabIndex = 48;
            label50.Text = "Дата стирки";
            // 
            // mtbRzuDataVCd
            // 
            mtbRzuDataVCd.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataVCd, 13);
            mtbRzuDataVCd.Font = new Font("Arial", 9F);
            mtbRzuDataVCd.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataVCd.Location = new Point(795, 86);
            mtbRzuDataVCd.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataVCd.Mask = "00/00/0000";
            mtbRzuDataVCd.Name = "mtbRzuDataVCd";
            tablePanel5.SetRow(mtbRzuDataVCd, 3);
            mtbRzuDataVCd.Size = new Size(62, 21);
            mtbRzuDataVCd.TabIndex = 65;
            // 
            // cbRzuVishFact
            // 
            cbRzuVishFact.AutoSize = true;
            tablePanel5.SetColumn(cbRzuVishFact, 4);
            cbRzuVishFact.Font = new Font("Arial", 10F);
            cbRzuVishFact.ForeColor = Color.FromArgb(72, 61, 139);
            cbRzuVishFact.Location = new Point(285, 64);
            cbRzuVishFact.Margin = new Padding(4, 3, 4, 3);
            cbRzuVishFact.Name = "cbRzuVishFact";
            tablePanel5.SetRow(cbRzuVishFact, 2);
            cbRzuVishFact.Size = new Size(58, 15);
            cbRzuVishFact.TabIndex = 70;
            cbRzuVishFact.Text = "Факт";
            cbRzuVishFact.UseVisualStyleBackColor = true;
            // 
            // cbRzuPrintFact
            // 
            cbRzuPrintFact.AutoSize = true;
            tablePanel5.SetColumn(cbRzuPrintFact, 4);
            cbRzuPrintFact.Font = new Font("Arial", 10F);
            cbRzuPrintFact.ForeColor = Color.FromArgb(72, 61, 139);
            cbRzuPrintFact.Location = new Point(285, 13);
            cbRzuPrintFact.Margin = new Padding(4, 3, 4, 3);
            cbRzuPrintFact.Name = "cbRzuPrintFact";
            tablePanel5.SetRow(cbRzuPrintFact, 0);
            cbRzuPrintFact.Size = new Size(58, 15);
            cbRzuPrintFact.TabIndex = 68;
            cbRzuPrintFact.Text = "Факт";
            cbRzuPrintFact.UseVisualStyleBackColor = true;
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label51, 0);
            label51.Font = new Font("Arial", 9F);
            label51.ForeColor = Color.FromArgb(0, 0, 0);
            label51.Location = new Point(15, 141);
            label51.Margin = new Padding(4, 0, 4, 0);
            label51.Name = "label51";
            tablePanel5.SetRow(label51, 5);
            label51.Size = new Size(60, 45);
            label51.TabIndex = 46;
            label51.Text = "Дата принято\r\nна стирку";
            // 
            // mtbRzuDataVChi
            // 
            mtbRzuDataVChi.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataVChi, 10);
            mtbRzuDataVChi.Font = new Font("Arial", 9F);
            mtbRzuDataVChi.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataVChi.Location = new Point(650, 86);
            mtbRzuDataVChi.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataVChi.Mask = "00/00/0000";
            mtbRzuDataVChi.Name = "mtbRzuDataVChi";
            tablePanel5.SetRow(mtbRzuDataVChi, 3);
            mtbRzuDataVChi.Size = new Size(62, 21);
            mtbRzuDataVChi.TabIndex = 62;
            // 
            // cbPszVishPlan
            // 
            cbPszVishPlan.AutoSize = true;
            tablePanel5.SetColumn(cbPszVishPlan, 3);
            cbPszVishPlan.Font = new Font("Arial", 10F);
            cbPszVishPlan.ForeColor = Color.FromArgb(72, 61, 139);
            cbPszVishPlan.Location = new Point(195, 64);
            cbPszVishPlan.Margin = new Padding(4, 3, 4, 3);
            cbPszVishPlan.Name = "cbPszVishPlan";
            tablePanel5.SetRow(cbPszVishPlan, 2);
            cbPszVishPlan.Size = new Size(59, 15);
            cbPszVishPlan.TabIndex = 69;
            cbPszVishPlan.Text = "План";
            cbPszVishPlan.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label37, 0);
            label37.Font = new Font("Arial", 9F);
            label37.ForeColor = Color.FromArgb(0, 0, 0);
            label37.Location = new Point(15, 31);
            label37.Margin = new Padding(4, 0, 4, 0);
            label37.Name = "label37";
            tablePanel5.SetRow(label37, 1);
            label37.Size = new Size(57, 30);
            label37.TabIndex = 20;
            label37.Text = "Дата\r\nна принт";
            // 
            // mtbRzuDataVR
            // 
            mtbRzuDataVR.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataVR, 7);
            mtbRzuDataVR.Font = new Font("Arial", 9F);
            mtbRzuDataVR.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataVR.Location = new Point(465, 86);
            mtbRzuDataVR.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataVR.Mask = "00/00/0000";
            mtbRzuDataVR.Name = "mtbRzuDataVR";
            tablePanel5.SetRow(mtbRzuDataVR, 3);
            mtbRzuDataVR.Size = new Size(62, 21);
            mtbRzuDataVR.TabIndex = 59;
            // 
            // mtbRzuDataPrCd
            // 
            mtbRzuDataPrCd.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataPrCd, 16);
            mtbRzuDataPrCd.Font = new Font("Arial", 9F);
            mtbRzuDataPrCd.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataPrCd.Location = new Point(935, 35);
            mtbRzuDataPrCd.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataPrCd.Mask = "00/00/0000";
            mtbRzuDataPrCd.Name = "mtbRzuDataPrCd";
            tablePanel5.SetRow(mtbRzuDataPrCd, 1);
            mtbRzuDataPrCd.Size = new Size(62, 21);
            mtbRzuDataPrCd.TabIndex = 66;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label35, 0);
            tablePanel5.SetColumnSpan(label35, 2);
            label35.Font = new Font("Arial", 10F, FontStyle.Bold);
            label35.ForeColor = Color.FromArgb(0, 0, 0);
            label35.Location = new Point(15, 114);
            label35.Margin = new Padding(4, 0, 4, 0);
            label35.Name = "label35";
            tablePanel5.SetRow(label35, 4);
            label35.Size = new Size(62, 16);
            label35.TabIndex = 11;
            label35.Text = "СТИРКА";
            // 
            // mtbRzuDataRasp
            // 
            mtbRzuDataRasp.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataRasp, 1);
            mtbRzuDataRasp.Font = new Font("Arial", 9F);
            mtbRzuDataRasp.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataRasp.Location = new Point(105, 35);
            mtbRzuDataRasp.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataRasp.Mask = "00/00/0000";
            mtbRzuDataRasp.Name = "mtbRzuDataRasp";
            tablePanel5.SetRow(mtbRzuDataRasp, 1);
            mtbRzuDataRasp.Size = new Size(62, 21);
            mtbRzuDataRasp.TabIndex = 38;
            // 
            // mtbRzuDataVP
            // 
            mtbRzuDataVP.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataVP, 4);
            mtbRzuDataVP.Font = new Font("Arial", 9F);
            mtbRzuDataVP.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataVP.Location = new Point(285, 86);
            mtbRzuDataVP.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataVP.Mask = "00/00/0000";
            mtbRzuDataVP.Name = "mtbRzuDataVP";
            tablePanel5.SetRow(mtbRzuDataVP, 3);
            mtbRzuDataVP.Size = new Size(62, 21);
            mtbRzuDataVP.TabIndex = 56;
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label43, 12);
            label43.Font = new Font("Arial", 9F);
            label43.ForeColor = Color.FromArgb(0, 0, 0);
            label43.Location = new Point(740, 82);
            label43.Margin = new Padding(4, 0, 4, 0);
            label43.Name = "label43";
            tablePanel5.SetRow(label43, 3);
            label43.Size = new Size(40, 30);
            label43.TabIndex = 42;
            label43.Text = "Дата\r\nсдачи";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label38, 3);
            label38.Font = new Font("Arial", 9F);
            label38.ForeColor = Color.FromArgb(0, 0, 0);
            label38.Location = new Point(195, 31);
            label38.Margin = new Padding(4, 0, 4, 0);
            label38.Name = "label38";
            tablePanel5.SetRow(label38, 1);
            label38.Size = new Size(57, 30);
            label38.TabIndex = 22;
            label38.Text = "Дата принято\r\nна принт";
            // 
            // mtbRzuDataPrKm
            // 
            mtbRzuDataPrKm.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataPrKm, 13);
            mtbRzuDataPrKm.Font = new Font("Arial", 9F);
            mtbRzuDataPrKm.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataPrKm.Location = new Point(795, 35);
            mtbRzuDataPrKm.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataPrKm.Mask = "00/00/0000";
            mtbRzuDataPrKm.Name = "mtbRzuDataPrKm";
            tablePanel5.SetRow(mtbRzuDataPrKm, 1);
            mtbRzuDataPrKm.Size = new Size(62, 21);
            mtbRzuDataPrKm.TabIndex = 64;
            // 
            // mtbRzuDataRasv
            // 
            mtbRzuDataRasv.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataRasv, 1);
            mtbRzuDataRasv.Font = new Font("Arial", 9F);
            mtbRzuDataRasv.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataRasv.Location = new Point(105, 86);
            mtbRzuDataRasv.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataRasv.Mask = "00/00/0000";
            mtbRzuDataRasv.Name = "mtbRzuDataRasv";
            tablePanel5.SetRow(mtbRzuDataRasv, 3);
            mtbRzuDataRasv.Size = new Size(62, 21);
            mtbRzuDataRasv.TabIndex = 53;
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label45, 9);
            label45.Font = new Font("Arial", 9F);
            label45.ForeColor = Color.FromArgb(0, 0, 0);
            label45.Location = new Point(555, 82);
            label45.Margin = new Padding(4, 0, 4, 0);
            label45.Name = "label45";
            tablePanel5.SetRow(label45, 3);
            label45.Size = new Size(54, 30);
            label45.TabIndex = 38;
            label45.Text = "Дата на чистку";
            // 
            // mtbRzuDataPrP
            // 
            mtbRzuDataPrP.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataPrP, 4);
            mtbRzuDataPrP.Font = new Font("Arial", 9F);
            mtbRzuDataPrP.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataPrP.Location = new Point(285, 35);
            mtbRzuDataPrP.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataPrP.Mask = "00/00/0000";
            mtbRzuDataPrP.Name = "mtbRzuDataPrP";
            tablePanel5.SetRow(mtbRzuDataPrP, 1);
            mtbRzuDataPrP.Size = new Size(62, 21);
            mtbRzuDataPrP.TabIndex = 55;
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label39, 6);
            label39.Font = new Font("Arial", 9F);
            label39.ForeColor = Color.FromArgb(0, 0, 0);
            label39.Location = new Point(375, 31);
            label39.Margin = new Padding(4, 0, 4, 0);
            label39.Name = "label39";
            tablePanel5.SetRow(label39, 1);
            label39.Size = new Size(46, 30);
            label39.TabIndex = 24;
            label39.Text = "Дата в работу\r\nпринт";
            // 
            // mtbRzuDataPrR
            // 
            mtbRzuDataPrR.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataPrR, 7);
            mtbRzuDataPrR.Font = new Font("Arial", 9F);
            mtbRzuDataPrR.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataPrR.Location = new Point(465, 35);
            mtbRzuDataPrR.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataPrR.Mask = "00/00/0000";
            mtbRzuDataPrR.Name = "mtbRzuDataPrR";
            tablePanel5.SetRow(mtbRzuDataPrR, 1);
            mtbRzuDataPrR.Size = new Size(62, 21);
            mtbRzuDataPrR.TabIndex = 58;
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label46, 6);
            label46.Font = new Font("Arial", 9F);
            label46.ForeColor = Color.FromArgb(0, 0, 0);
            label46.Location = new Point(375, 82);
            label46.Margin = new Padding(4, 0, 4, 0);
            label46.Name = "label46";
            tablePanel5.SetRow(label46, 3);
            label46.Size = new Size(57, 30);
            label46.TabIndex = 36;
            label46.Text = "Дата в работу\r\nвышивка";
            // 
            // mtbRzuDataPrPe
            // 
            mtbRzuDataPrPe.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel5.SetColumn(mtbRzuDataPrPe, 10);
            mtbRzuDataPrPe.Font = new Font("Arial", 9F);
            mtbRzuDataPrPe.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataPrPe.Location = new Point(650, 35);
            mtbRzuDataPrPe.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataPrPe.Mask = "00/00/0000";
            mtbRzuDataPrPe.Name = "mtbRzuDataPrPe";
            tablePanel5.SetRow(mtbRzuDataPrPe, 1);
            mtbRzuDataPrPe.Size = new Size(62, 21);
            mtbRzuDataPrPe.TabIndex = 61;
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label40, 9);
            label40.Font = new Font("Arial", 9F);
            label40.ForeColor = Color.FromArgb(0, 0, 0);
            label40.Location = new Point(555, 31);
            label40.Margin = new Padding(4, 0, 4, 0);
            label40.Name = "label40";
            tablePanel5.SetRow(label40, 1);
            label40.Size = new Size(85, 30);
            label40.TabIndex = 26;
            label40.Text = "Дата на печку\r\nпринт";
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label41, 12);
            label41.Font = new Font("Arial", 9F);
            label41.ForeColor = Color.FromArgb(0, 0, 0);
            label41.Location = new Point(740, 31);
            label41.Margin = new Padding(4, 0, 4, 0);
            label41.Name = "label41";
            tablePanel5.SetRow(label41, 1);
            label41.Size = new Size(46, 30);
            label41.TabIndex = 28;
            label41.Text = "Дата\r\nкомпл.";
            // 
            // label47
            // 
            label47.AutoSize = true;
            label47.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label47, 3);
            label47.Font = new Font("Arial", 9F);
            label47.ForeColor = Color.FromArgb(0, 0, 0);
            label47.Location = new Point(195, 82);
            label47.Margin = new Padding(4, 0, 4, 0);
            label47.Name = "label47";
            tablePanel5.SetRow(label47, 3);
            label47.Size = new Size(72, 30);
            label47.TabIndex = 34;
            label47.Text = "Дата принято\r\nна вышивку";
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label42, 15);
            label42.Font = new Font("Arial", 9F);
            label42.ForeColor = Color.FromArgb(0, 0, 0);
            label42.Location = new Point(885, 31);
            label42.Margin = new Padding(4, 0, 4, 0);
            label42.Name = "label42";
            tablePanel5.SetRow(label42, 1);
            label42.Size = new Size(40, 30);
            label42.TabIndex = 30;
            label42.Text = "Дата\r\nсдачи";
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label36, 0);
            tablePanel5.SetColumnSpan(label36, 2);
            label36.Font = new Font("Arial", 10F, FontStyle.Bold);
            label36.ForeColor = Color.FromArgb(0, 0, 0);
            label36.Location = new Point(15, 63);
            label36.Margin = new Padding(4, 0, 4, 0);
            label36.Name = "label36";
            tablePanel5.SetRow(label36, 2);
            label36.Size = new Size(83, 16);
            label36.TabIndex = 10;
            label36.Text = "ВЫШИВКА";
            // 
            // label48
            // 
            label48.AutoSize = true;
            label48.BackColor = Color.Transparent;
            tablePanel5.SetColumn(label48, 0);
            label48.Font = new Font("Arial", 9F);
            label48.ForeColor = Color.FromArgb(0, 0, 0);
            label48.Location = new Point(15, 82);
            label48.Margin = new Padding(4, 0, 4, 0);
            label48.Name = "label48";
            tablePanel5.SetRow(label48, 3);
            label48.Size = new Size(72, 30);
            label48.TabIndex = 32;
            label48.Text = "Дата\r\nна вышивку";
            // 
            // customGroupBox3
            // 
            customGroupBox3.BackColor = Color.Transparent;
            customGroupBox3.Controls.Add(tablePanel4);
            customGroupBox3.Font = new Font("Arial", 12F, FontStyle.Bold);
            customGroupBox3.Location = new Point(2, 99);
            customGroupBox3.Margin = new Padding(4, 3, 4, 3);
            customGroupBox3.Name = "customGroupBox3";
            customGroupBox3.Padding = new Padding(4, 3, 4, 3);
            customGroupBox3.Size = new Size(1835, 255);
            customGroupBox3.TabIndex = 33;
            customGroupBox3.TabStop = false;
            customGroupBox3.Text = "НАКЛАДНЫЕ";
            // 
            // tablePanel4
            // 
            tablePanel4.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 5F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 309F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 159F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 642F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 143F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 245F) });
            tablePanel4.Controls.Add(simpleButtonNaklPart);
            tablePanel4.Controls.Add(simpleButtonPrintNaklXtraReport);
            tablePanel4.Controls.Add(btnNaklAbsent);
            tablePanel4.Controls.Add(panelControl1);
            tablePanel4.Controls.Add(btnNaklPrint);
            tablePanel4.Dock = DockStyle.Fill;
            tablePanel4.Location = new Point(4, 22);
            tablePanel4.Margin = new Padding(4, 3, 4, 3);
            tablePanel4.Name = "tablePanel4";
            tablePanel4.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 146F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel4.Size = new Size(1827, 230);
            tablePanel4.TabIndex = 0;
            tablePanel4.UseSkinIndents = true;
            // 
            // simpleButtonNaklPart
            // 
            simpleButtonNaklPart.Appearance.Font = new Font("Arial", 10F);
            simpleButtonNaklPart.Appearance.ForeColor = Color.Purple;
            simpleButtonNaklPart.Appearance.Options.UseFont = true;
            simpleButtonNaklPart.Appearance.Options.UseForeColor = true;
            tablePanel4.SetColumn(simpleButtonNaklPart, 1);
            simpleButtonNaklPart.ImageOptions.Image = (Image)resources.GetObject("simpleButtonNaklPart.ImageOptions.Image");
            simpleButtonNaklPart.Location = new Point(20, 173);
            simpleButtonNaklPart.Margin = new Padding(4, 3, 4, 3);
            simpleButtonNaklPart.Name = "simpleButtonNaklPart";
            tablePanel4.SetRow(simpleButtonNaklPart, 1);
            simpleButtonNaklPart.Size = new Size(301, 29);
            simpleButtonNaklPart.TabIndex = 12;
            simpleButtonNaklPart.Text = "Показать информацию по делению накладной";
            simpleButtonNaklPart.Click += simpleButtonNaklPart_Click;
            // 
            // simpleButtonPrintNaklXtraReport
            // 
            simpleButtonPrintNaklXtraReport.Appearance.Font = new Font("Arial", 10F);
            simpleButtonPrintNaklXtraReport.Appearance.ForeColor = Color.FromArgb(139, 69, 19);
            simpleButtonPrintNaklXtraReport.Appearance.Options.UseFont = true;
            simpleButtonPrintNaklXtraReport.Appearance.Options.UseForeColor = true;
            tablePanel4.SetColumn(simpleButtonPrintNaklXtraReport, 3);
            simpleButtonPrintNaklXtraReport.ImageOptions.Image = (Image)resources.GetObject("simpleButtonPrintNaklXtraReport.ImageOptions.Image");
            simpleButtonPrintNaklXtraReport.Location = new Point(349, 173);
            simpleButtonPrintNaklXtraReport.Margin = new Padding(4, 3, 4, 3);
            simpleButtonPrintNaklXtraReport.Name = "simpleButtonPrintNaklXtraReport";
            tablePanel4.SetRow(simpleButtonPrintNaklXtraReport, 1);
            simpleButtonPrintNaklXtraReport.Size = new Size(151, 29);
            simpleButtonPrintNaklXtraReport.TabIndex = 11;
            simpleButtonPrintNaklXtraReport.Text = "Печать накладной";
            simpleButtonPrintNaklXtraReport.Click += simpleButtonPrintNaklXtraReport_Click;
            // 
            // btnNaklAbsent
            // 
            btnNaklAbsent.BackColor = Color.FromArgb(230, 230, 250);
            tablePanel4.SetColumn(btnNaklAbsent, 6);
            btnNaklAbsent.Dock = DockStyle.Fill;
            btnNaklAbsent.FlatStyle = FlatStyle.Flat;
            btnNaklAbsent.Font = new Font("Arial", 10F);
            btnNaklAbsent.ForeColor = Color.FromArgb(106, 90, 205);
            btnNaklAbsent.Location = new Point(1293, 159);
            btnNaklAbsent.Margin = new Padding(4, 3, 4, 3);
            btnNaklAbsent.Name = "btnNaklAbsent";
            tablePanel4.SetRow(btnNaklAbsent, 1);
            btnNaklAbsent.Size = new Size(519, 57);
            btnNaklAbsent.TabIndex = 8;
            btnNaklAbsent.Text = "Накладная не создана. Причина";
            btnNaklAbsent.UseVisualStyleBackColor = true;
            btnNaklAbsent.Visible = false;
            // 
            // panelControl1
            // 
            tablePanel4.SetColumn(panelControl1, 0);
            tablePanel4.SetColumnSpan(panelControl1, 7);
            panelControl1.Controls.Add(gridControlPartNaklList);
            panelControl1.Controls.Add(gridControlNaklList);
            panelControl1.Dock = DockStyle.Fill;
            panelControl1.Location = new Point(15, 13);
            panelControl1.Margin = new Padding(4, 3, 4, 3);
            panelControl1.Name = "panelControl1";
            tablePanel4.SetRow(panelControl1, 0);
            panelControl1.Size = new Size(1797, 140);
            panelControl1.TabIndex = 0;
            // 
            // gridControlPartNaklList
            // 
            gridControlPartNaklList.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlPartNaklList.Font = new Font("Arial", 10F);
            gridControlPartNaklList.Location = new Point(8, 32);
            gridControlPartNaklList.MainView = gridViewPartNaklList;
            gridControlPartNaklList.Margin = new Padding(4, 3, 4, 3);
            gridControlPartNaklList.Name = "gridControlPartNaklList";
            gridControlPartNaklList.Size = new Size(1353, 132);
            gridControlPartNaklList.TabIndex = 11;
            gridControlPartNaklList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewPartNaklList });
            gridControlPartNaklList.Visible = false;
            // 
            // gridViewPartNaklList
            // 
            gridViewPartNaklList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnNaklPartIzObPrch, gridColumnNaklPartRazm, gridColumnNaklPartMod, gridColumnNaklPartCountNew, gridColumnNaklPartCountAfter, gridColumnNaklPartCountBefore, gridColumnNaklPartIzNew, gridColumnNaklPartSklOtgrNew, gridColumnNaklPartSklOtgrOld, gridColumnNaklPartDateIzm, gridColumnNaklPartStatus, gridColumnNaklPartSklID1COld, gridColumnNaklPartCompDel, gridColumnNaklPartCompName, gridColumnNaklPartIzOld, gridColumnNaklPartID, gridColumnNaklPartNPach, gridColumnNaklPartPrichSokr, gridColumnNaklPartSklID1CNew });
            gridViewPartNaklList.DetailHeight = 404;
            gridViewPartNaklList.GridControl = gridControlPartNaklList;
            gridViewPartNaklList.Name = "gridViewPartNaklList";
            gridViewPartNaklList.OptionsBehavior.Editable = false;
            gridViewPartNaklList.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewPartNaklList.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewPartNaklList.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnNaklPartIzObPrch
            // 
            gridColumnNaklPartIzObPrch.Caption = "iz_ob_prch";
            gridColumnNaklPartIzObPrch.MinWidth = 23;
            gridColumnNaklPartIzObPrch.Name = "gridColumnNaklPartIzObPrch";
            gridColumnNaklPartIzObPrch.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartIzObPrch.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartIzObPrch.Visible = true;
            gridColumnNaklPartIzObPrch.VisibleIndex = 11;
            gridColumnNaklPartIzObPrch.Width = 52;
            // 
            // gridColumnNaklPartRazm
            // 
            gridColumnNaklPartRazm.Caption = "razm";
            gridColumnNaklPartRazm.MinWidth = 23;
            gridColumnNaklPartRazm.Name = "gridColumnNaklPartRazm";
            gridColumnNaklPartRazm.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartRazm.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartRazm.Visible = true;
            gridColumnNaklPartRazm.VisibleIndex = 10;
            gridColumnNaklPartRazm.Width = 154;
            // 
            // gridColumnNaklPartMod
            // 
            gridColumnNaklPartMod.Caption = "mod";
            gridColumnNaklPartMod.MinWidth = 23;
            gridColumnNaklPartMod.Name = "gridColumnNaklPartMod";
            gridColumnNaklPartMod.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartMod.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartMod.Visible = true;
            gridColumnNaklPartMod.VisibleIndex = 9;
            gridColumnNaklPartMod.Width = 127;
            // 
            // gridColumnNaklPartCountNew
            // 
            gridColumnNaklPartCountNew.Caption = "kol_new";
            gridColumnNaklPartCountNew.MinWidth = 23;
            gridColumnNaklPartCountNew.Name = "gridColumnNaklPartCountNew";
            gridColumnNaklPartCountNew.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartCountNew.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartCountNew.Visible = true;
            gridColumnNaklPartCountNew.VisibleIndex = 8;
            gridColumnNaklPartCountNew.Width = 73;
            // 
            // gridColumnNaklPartCountAfter
            // 
            gridColumnNaklPartCountAfter.Caption = "kol_c";
            gridColumnNaklPartCountAfter.MinWidth = 23;
            gridColumnNaklPartCountAfter.Name = "gridColumnNaklPartCountAfter";
            gridColumnNaklPartCountAfter.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartCountAfter.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartCountAfter.Visible = true;
            gridColumnNaklPartCountAfter.VisibleIndex = 7;
            gridColumnNaklPartCountAfter.Width = 62;
            // 
            // gridColumnNaklPartCountBefore
            // 
            gridColumnNaklPartCountBefore.Caption = "kol_b";
            gridColumnNaklPartCountBefore.MinWidth = 23;
            gridColumnNaklPartCountBefore.Name = "gridColumnNaklPartCountBefore";
            gridColumnNaklPartCountBefore.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartCountBefore.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartCountBefore.Visible = true;
            gridColumnNaklPartCountBefore.VisibleIndex = 6;
            gridColumnNaklPartCountBefore.Width = 44;
            // 
            // gridColumnNaklPartIzNew
            // 
            gridColumnNaklPartIzNew.Caption = "iz_c";
            gridColumnNaklPartIzNew.MinWidth = 23;
            gridColumnNaklPartIzNew.Name = "gridColumnNaklPartIzNew";
            gridColumnNaklPartIzNew.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartIzNew.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartIzNew.Visible = true;
            gridColumnNaklPartIzNew.VisibleIndex = 5;
            gridColumnNaklPartIzNew.Width = 68;
            // 
            // gridColumnNaklPartSklOtgrNew
            // 
            gridColumnNaklPartSklOtgrNew.Caption = "skl_otgr_s";
            gridColumnNaklPartSklOtgrNew.MinWidth = 23;
            gridColumnNaklPartSklOtgrNew.Name = "gridColumnNaklPartSklOtgrNew";
            gridColumnNaklPartSklOtgrNew.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartSklOtgrNew.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartSklOtgrNew.Visible = true;
            gridColumnNaklPartSklOtgrNew.VisibleIndex = 3;
            gridColumnNaklPartSklOtgrNew.Width = 41;
            // 
            // gridColumnNaklPartSklOtgrOld
            // 
            gridColumnNaklPartSklOtgrOld.Caption = "skl_otgr_b";
            gridColumnNaklPartSklOtgrOld.MinWidth = 23;
            gridColumnNaklPartSklOtgrOld.Name = "gridColumnNaklPartSklOtgrOld";
            gridColumnNaklPartSklOtgrOld.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartSklOtgrOld.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartSklOtgrOld.Visible = true;
            gridColumnNaklPartSklOtgrOld.VisibleIndex = 2;
            gridColumnNaklPartSklOtgrOld.Width = 47;
            // 
            // gridColumnNaklPartDateIzm
            // 
            gridColumnNaklPartDateIzm.Caption = "data_izm";
            gridColumnNaklPartDateIzm.MinWidth = 23;
            gridColumnNaklPartDateIzm.Name = "gridColumnNaklPartDateIzm";
            gridColumnNaklPartDateIzm.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartDateIzm.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartDateIzm.Visible = true;
            gridColumnNaklPartDateIzm.VisibleIndex = 1;
            gridColumnNaklPartDateIzm.Width = 59;
            // 
            // gridColumnNaklPartStatus
            // 
            gridColumnNaklPartStatus.Caption = "status";
            gridColumnNaklPartStatus.MinWidth = 23;
            gridColumnNaklPartStatus.Name = "gridColumnNaklPartStatus";
            gridColumnNaklPartStatus.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartStatus.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartStatus.Visible = true;
            gridColumnNaklPartStatus.VisibleIndex = 12;
            gridColumnNaklPartStatus.Width = 52;
            // 
            // gridColumnNaklPartSklID1COld
            // 
            gridColumnNaklPartSklID1COld.Caption = "skl_id_1c_b";
            gridColumnNaklPartSklID1COld.MinWidth = 23;
            gridColumnNaklPartSklID1COld.Name = "gridColumnNaklPartSklID1COld";
            gridColumnNaklPartSklID1COld.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartSklID1COld.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartSklID1COld.Visible = true;
            gridColumnNaklPartSklID1COld.VisibleIndex = 15;
            gridColumnNaklPartSklID1COld.Width = 61;
            // 
            // gridColumnNaklPartCompDel
            // 
            gridColumnNaklPartCompDel.Caption = "komp_del";
            gridColumnNaklPartCompDel.MinWidth = 23;
            gridColumnNaklPartCompDel.Name = "gridColumnNaklPartCompDel";
            gridColumnNaklPartCompDel.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartCompDel.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartCompDel.Visible = true;
            gridColumnNaklPartCompDel.VisibleIndex = 14;
            gridColumnNaklPartCompDel.Width = 114;
            // 
            // gridColumnNaklPartCompName
            // 
            gridColumnNaklPartCompName.Caption = "komp_name";
            gridColumnNaklPartCompName.MinWidth = 23;
            gridColumnNaklPartCompName.Name = "gridColumnNaklPartCompName";
            gridColumnNaklPartCompName.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartCompName.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartCompName.Visible = true;
            gridColumnNaklPartCompName.VisibleIndex = 13;
            gridColumnNaklPartCompName.Width = 118;
            // 
            // gridColumnNaklPartIzOld
            // 
            gridColumnNaklPartIzOld.Caption = "iz_b";
            gridColumnNaklPartIzOld.MinWidth = 23;
            gridColumnNaklPartIzOld.Name = "gridColumnNaklPartIzOld";
            gridColumnNaklPartIzOld.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartIzOld.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartIzOld.Visible = true;
            gridColumnNaklPartIzOld.VisibleIndex = 4;
            gridColumnNaklPartIzOld.Width = 57;
            // 
            // gridColumnNaklPartID
            // 
            gridColumnNaklPartID.Caption = "ID";
            gridColumnNaklPartID.MinWidth = 23;
            gridColumnNaklPartID.Name = "gridColumnNaklPartID";
            gridColumnNaklPartID.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartID.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartID.Visible = true;
            gridColumnNaklPartID.VisibleIndex = 0;
            gridColumnNaklPartID.Width = 57;
            // 
            // gridColumnNaklPartNPach
            // 
            gridColumnNaklPartNPach.Caption = "n_pach";
            gridColumnNaklPartNPach.MinWidth = 23;
            gridColumnNaklPartNPach.Name = "gridColumnNaklPartNPach";
            gridColumnNaklPartNPach.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartNPach.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartNPach.Visible = true;
            gridColumnNaklPartNPach.VisibleIndex = 18;
            gridColumnNaklPartNPach.Width = 149;
            // 
            // gridColumnNaklPartPrichSokr
            // 
            gridColumnNaklPartPrichSokr.Caption = "prich_sokr";
            gridColumnNaklPartPrichSokr.MinWidth = 23;
            gridColumnNaklPartPrichSokr.Name = "gridColumnNaklPartPrichSokr";
            gridColumnNaklPartPrichSokr.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartPrichSokr.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartPrichSokr.Visible = true;
            gridColumnNaklPartPrichSokr.VisibleIndex = 17;
            gridColumnNaklPartPrichSokr.Width = 85;
            // 
            // gridColumnNaklPartSklID1CNew
            // 
            gridColumnNaklPartSklID1CNew.Caption = "skl_id_1c_c";
            gridColumnNaklPartSklID1CNew.MinWidth = 23;
            gridColumnNaklPartSklID1CNew.Name = "gridColumnNaklPartSklID1CNew";
            gridColumnNaklPartSklID1CNew.OptionsFilter.AllowAutoFilter = false;
            gridColumnNaklPartSklID1CNew.OptionsFilter.AllowFilter = false;
            gridColumnNaklPartSklID1CNew.Visible = true;
            gridColumnNaklPartSklID1CNew.VisibleIndex = 16;
            gridColumnNaklPartSklID1CNew.Width = 59;
            // 
            // btnNaklPrint
            // 
            btnNaklPrint.BackColor = Color.FromArgb(230, 230, 250);
            tablePanel4.SetColumn(btnNaklPrint, 0);
            btnNaklPrint.FlatStyle = FlatStyle.Flat;
            btnNaklPrint.Font = new Font("Arial", 10F);
            btnNaklPrint.ForeColor = Color.FromArgb(106, 90, 205);
            btnNaklPrint.Location = new Point(0, 0);
            btnNaklPrint.Margin = new Padding(4, 3, 4, 3);
            btnNaklPrint.Name = "btnNaklPrint";
            tablePanel4.SetRow(btnNaklPrint, 1);
            btnNaklPrint.Size = new Size(0, 0);
            btnNaklPrint.TabIndex = 6;
            btnNaklPrint.Text = "Просмотр/Печать накладной";
            btnNaklPrint.UseVisualStyleBackColor = true;
            btnNaklPrint.Click += btnNaklPrint_Click;
            // 
            // ContDates
            // 
            ContDates.BackColor = Color.Transparent;
            ContDates.Controls.Add(tablePanel3);
            ContDates.Font = new Font("Arial", 12F, FontStyle.Bold);
            ContDates.Location = new Point(4, 3);
            ContDates.Margin = new Padding(4, 3, 4, 3);
            ContDates.Name = "ContDates";
            ContDates.Padding = new Padding(4, 3, 4, 3);
            ContDates.Size = new Size(1845, 89);
            ContDates.TabIndex = 32;
            ContDates.TabStop = false;
            ContDates.Text = "КОНТРОЛЬНЫЕ ДАТЫ";
            // 
            // tablePanel3
            // 
            tablePanel3.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 16F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 75F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 21F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 125F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 29F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 28F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 20F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 8F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 65F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 24F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 38F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 95F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F) });
            tablePanel3.Controls.Add(customLabel3);
            tablePanel3.Controls.Add(mtbRzuData1С);
            tablePanel3.Controls.Add(tbPszRpcNom);
            tablePanel3.Controls.Add(label63);
            tablePanel3.Controls.Add(label29);
            tablePanel3.Controls.Add(mtbRzuDataCd);
            tablePanel3.Controls.Add(mtbRzuDataR);
            tablePanel3.Controls.Add(label14);
            tablePanel3.Controls.Add(mtbRzuDataUp);
            tablePanel3.Controls.Add(mtbPsaDataZap);
            tablePanel3.Controls.Add(mtbRzuDataRab);
            tablePanel3.Controls.Add(label33);
            tablePanel3.Controls.Add(label64);
            tablePanel3.Controls.Add(mtbRzuDataZeh);
            tablePanel3.Controls.Add(label28);
            tablePanel3.Controls.Add(label26);
            tablePanel3.Controls.Add(mtbPsaDataCdPlan);
            tablePanel3.Controls.Add(label34);
            tablePanel3.Controls.Add(mtbRzuDataCdUt);
            tablePanel3.Controls.Add(label27);
            tablePanel3.Dock = DockStyle.Fill;
            tablePanel3.Location = new Point(4, 22);
            tablePanel3.Margin = new Padding(4, 3, 4, 3);
            tablePanel3.Name = "tablePanel3";
            tablePanel3.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F) });
            tablePanel3.Size = new Size(1837, 64);
            tablePanel3.TabIndex = 0;
            tablePanel3.UseSkinIndents = true;
            // 
            // customLabel3
            // 
            customLabel3.AutoSize = true;
            customLabel3.BackColor = Color.Transparent;
            tablePanel3.SetColumn(customLabel3, 12);
            customLabel3.Font = new Font("Arial", 9F);
            customLabel3.ForeColor = Color.FromArgb(0, 0, 0);
            customLabel3.Location = new Point(775, 24);
            customLabel3.Margin = new Padding(4, 0, 4, 0);
            customLabel3.Name = "customLabel3";
            tablePanel3.SetRow(customLabel3, 0);
            customLabel3.Size = new Size(28, 15);
            customLabel3.TabIndex = 42;
            customLabel3.Text = "РЦ-";
            // 
            // mtbRzuData1С
            // 
            mtbRzuData1С.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbRzuData1С, 27);
            mtbRzuData1С.Font = new Font("Arial", 9F);
            mtbRzuData1С.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuData1С.Location = new Point(1625, 21);
            mtbRzuData1С.Margin = new Padding(4, 3, 4, 3);
            mtbRzuData1С.Mask = "00/00/0000";
            mtbRzuData1С.Name = "mtbRzuData1С";
            tablePanel3.SetRow(mtbRzuData1С, 0);
            mtbRzuData1С.Size = new Size(197, 21);
            mtbRzuData1С.TabIndex = 39;
            // 
            // tbPszRpcNom
            // 
            tbPszRpcNom.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(tbPszRpcNom, 13);
            tbPszRpcNom.Font = new Font("Arial", 10F);
            tbPszRpcNom.ForeColor = Color.FromArgb(72, 61, 139);
            tbPszRpcNom.Location = new Point(811, 20);
            tbPszRpcNom.Margin = new Padding(0);
            tbPszRpcNom.Name = "tbPszRpcNom";
            tablePanel3.SetRow(tbPszRpcNom, 0);
            tbPszRpcNom.Size = new Size(20, 23);
            tbPszRpcNom.TabIndex = 34;
            // 
            // label63
            // 
            label63.AutoSize = true;
            label63.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label63, 26);
            label63.Font = new Font("Arial", 9F);
            label63.ForeColor = Color.FromArgb(0, 0, 0);
            label63.Location = new Point(1530, 16);
            label63.Margin = new Padding(4, 0, 4, 0);
            label63.Name = "label63";
            tablePanel3.SetRow(label63, 0);
            label63.Size = new Size(72, 30);
            label63.TabIndex = 38;
            label63.Text = "Дата 1к.т. в 1С";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label29, 0);
            label29.Font = new Font("Arial", 9F);
            label29.ForeColor = Color.FromArgb(0, 0, 0);
            label29.Location = new Point(15, 16);
            label29.Margin = new Padding(4, 0, 4, 0);
            label29.Name = "label29";
            tablePanel3.SetRow(label29, 0);
            label29.Size = new Size(51, 30);
            label29.TabIndex = 10;
            label29.Text = "Дата запуска";
            // 
            // mtbRzuDataCd
            // 
            mtbRzuDataCd.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbRzuDataCd, 25);
            mtbRzuDataCd.Font = new Font("Arial", 9F);
            mtbRzuDataCd.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataCd.Location = new Point(1450, 21);
            mtbRzuDataCd.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataCd.Mask = "00/00/0000";
            mtbRzuDataCd.Name = "mtbRzuDataCd";
            tablePanel3.SetRow(mtbRzuDataCd, 0);
            mtbRzuDataCd.Size = new Size(72, 21);
            mtbRzuDataCd.TabIndex = 37;
            // 
            // mtbRzuDataR
            // 
            mtbRzuDataR.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbRzuDataR, 10);
            mtbRzuDataR.Font = new Font("Arial", 9F);
            mtbRzuDataR.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataR.Location = new Point(675, 21);
            mtbRzuDataR.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataR.Mask = "00/00/0000";
            mtbRzuDataR.Name = "mtbRzuDataR";
            tablePanel3.SetRow(mtbRzuDataR, 0);
            mtbRzuDataR.Size = new Size(72, 21);
            mtbRzuDataR.TabIndex = 41;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label14, 24);
            label14.Font = new Font("Arial", 9F);
            label14.ForeColor = Color.FromArgb(0, 0, 0);
            label14.Location = new Point(1360, 16);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            tablePanel3.SetRow(label14, 0);
            label14.Size = new Size(82, 30);
            label14.TabIndex = 30;
            label14.Text = "Дата СДАНО (осн. накл.)";
            // 
            // mtbRzuDataUp
            // 
            mtbRzuDataUp.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbRzuDataUp, 22);
            mtbRzuDataUp.Font = new Font("Arial", 9F);
            mtbRzuDataUp.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataUp.Location = new Point(1260, 21);
            mtbRzuDataUp.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataUp.Mask = "00/00/0000";
            mtbRzuDataUp.Name = "mtbRzuDataUp";
            tablePanel3.SetRow(mtbRzuDataUp, 0);
            mtbRzuDataUp.Size = new Size(72, 21);
            mtbRzuDataUp.TabIndex = 36;
            // 
            // mtbPsaDataZap
            // 
            mtbPsaDataZap.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbPsaDataZap, 1);
            mtbPsaDataZap.Font = new Font("Arial", 9F);
            mtbPsaDataZap.ForeColor = Color.FromArgb(72, 61, 139);
            mtbPsaDataZap.Location = new Point(95, 21);
            mtbPsaDataZap.Margin = new Padding(4, 3, 4, 3);
            mtbPsaDataZap.Mask = "00/00/0000";
            mtbPsaDataZap.Name = "mtbPsaDataZap";
            tablePanel3.SetRow(mtbPsaDataZap, 0);
            mtbPsaDataZap.Size = new Size(72, 21);
            mtbPsaDataZap.TabIndex = 31;
            // 
            // mtbRzuDataRab
            // 
            mtbRzuDataRab.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbRzuDataRab, 19);
            mtbRzuDataRab.Font = new Font("Arial", 9F);
            mtbRzuDataRab.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataRab.Location = new Point(1090, 21);
            mtbRzuDataRab.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataRab.Mask = "00/00/0000";
            mtbRzuDataRab.Name = "mtbRzuDataRab";
            tablePanel3.SetRow(mtbRzuDataRab, 0);
            mtbRzuDataRab.Size = new Size(72, 21);
            mtbRzuDataRab.TabIndex = 35;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label33, 21);
            label33.Font = new Font("Arial", 9F);
            label33.ForeColor = Color.FromArgb(0, 0, 0);
            label33.Location = new Point(1190, 16);
            label33.Margin = new Padding(4, 0, 4, 0);
            label33.Name = "label33";
            tablePanel3.SetRow(label33, 0);
            label33.Size = new Size(56, 30);
            label33.TabIndex = 28;
            label33.Text = "Дата на упаковку";
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label64, 9);
            label64.Font = new Font("Arial", 9F);
            label64.ForeColor = Color.FromArgb(0, 0, 0);
            label64.Location = new Point(595, 16);
            label64.Margin = new Padding(4, 0, 4, 0);
            label64.Name = "label64";
            tablePanel3.SetRow(label64, 0);
            label64.Size = new Size(54, 30);
            label64.TabIndex = 40;
            label64.Text = "Дата раскроя";
            // 
            // mtbRzuDataZeh
            // 
            mtbRzuDataZeh.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbRzuDataZeh, 16);
            mtbRzuDataZeh.Font = new Font("Arial", 9F);
            mtbRzuDataZeh.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataZeh.Location = new Point(920, 21);
            mtbRzuDataZeh.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataZeh.Mask = "00/00/0000";
            mtbRzuDataZeh.Name = "mtbRzuDataZeh";
            tablePanel3.SetRow(mtbRzuDataZeh, 0);
            mtbRzuDataZeh.Size = new Size(72, 21);
            mtbRzuDataZeh.TabIndex = 34;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label28, 3);
            label28.Font = new Font("Arial", 9F);
            label28.ForeColor = Color.FromArgb(0, 0, 0);
            label28.Location = new Point(195, 10);
            label28.Margin = new Padding(4, 0, 4, 0);
            label28.Name = "label28";
            tablePanel3.SetRow(label28, 0);
            label28.Size = new Size(43, 43);
            label28.TabIndex = 12;
            label28.Text = "План. дата сдачи";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label26, 18);
            label26.Font = new Font("Arial", 9F);
            label26.ForeColor = Color.FromArgb(0, 0, 0);
            label26.Location = new Point(1020, 16);
            label26.Margin = new Padding(4, 0, 4, 0);
            label26.Name = "label26";
            tablePanel3.SetRow(label26, 0);
            label26.Size = new Size(46, 30);
            label26.TabIndex = 16;
            label26.Text = "Дата в работу";
            // 
            // mtbPsaDataCdPlan
            // 
            mtbPsaDataCdPlan.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbPsaDataCdPlan, 4);
            mtbPsaDataCdPlan.Font = new Font("Arial", 9F);
            mtbPsaDataCdPlan.ForeColor = Color.FromArgb(72, 61, 139);
            mtbPsaDataCdPlan.Location = new Point(270, 21);
            mtbPsaDataCdPlan.Margin = new Padding(4, 3, 4, 3);
            mtbPsaDataCdPlan.Mask = "00/00/0000";
            mtbPsaDataCdPlan.Name = "mtbPsaDataCdPlan";
            tablePanel3.SetRow(mtbPsaDataCdPlan, 0);
            mtbPsaDataCdPlan.Size = new Size(72, 21);
            mtbPsaDataCdPlan.TabIndex = 32;
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label34, 6);
            label34.Font = new Font("Arial", 9F);
            label34.ForeColor = Color.FromArgb(0, 0, 0);
            label34.Location = new Point(370, 16);
            label34.Margin = new Padding(4, 0, 4, 0);
            label34.Name = "label34";
            tablePanel3.SetRow(label34, 0);
            label34.Size = new Size(108, 30);
            label34.TabIndex = 26;
            label34.Text = "План. дата сдачи Уточненная";
            // 
            // mtbRzuDataCdUt
            // 
            mtbRzuDataCdUt.BackColor = Color.FromArgb(248, 248, 255);
            tablePanel3.SetColumn(mtbRzuDataCdUt, 7);
            mtbRzuDataCdUt.Font = new Font("Arial", 9F);
            mtbRzuDataCdUt.ForeColor = Color.FromArgb(72, 61, 139);
            mtbRzuDataCdUt.Location = new Point(495, 21);
            mtbRzuDataCdUt.Margin = new Padding(4, 3, 4, 3);
            mtbRzuDataCdUt.Mask = "00/00/0000";
            mtbRzuDataCdUt.Name = "mtbRzuDataCdUt";
            tablePanel3.SetRow(mtbRzuDataCdUt, 0);
            mtbRzuDataCdUt.Size = new Size(72, 21);
            mtbRzuDataCdUt.TabIndex = 33;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.BackColor = Color.Transparent;
            tablePanel3.SetColumn(label27, 15);
            label27.Font = new Font("Arial", 9F);
            label27.ForeColor = Color.FromArgb(0, 0, 0);
            label27.Location = new Point(855, 16);
            label27.Margin = new Padding(4, 0, 4, 0);
            label27.Name = "label27";
            tablePanel3.SetRow(label27, 0);
            label27.Size = new Size(46, 30);
            label27.TabIndex = 14;
            label27.Text = "Дата в цех";
            // 
            // xtraTabControl1
            // 
            xtraTabControl1.Appearance.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            xtraTabControl1.Appearance.Options.UseFont = true;
            xtraTabControl1.AppearancePage.Header.Font = new Font("Tahoma", 10F);
            xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            xtraTabControl1.AppearancePage.HeaderActive.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
            xtraTabControl1.Enabled = false;
            xtraTabControl1.Location = new Point(5, 210);
            xtraTabControl1.Margin = new Padding(4, 3, 4, 3);
            xtraTabControl1.Name = "xtraTabControl1";
            xtraTabControl1.ObjectName = null;
            xtraTabControl1.SelectedTabPage = RasInfo;
            xtraTabControl1.Size = new Size(1850, 726);
            xtraTabControl1.TabIndex = 3;
            xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { RasInfo, FurnInfo, WorkInfo, OtdelkaInfo, SockZadanyInfo });
            xtraTabControl1.SelectedPageChanged += xtraTabControl1_SelectedPageChanged;
            // 
            // SockZadanyInfo
            // 
            SockZadanyInfo.Controls.Add(layoutControl2);
            SockZadanyInfo.Name = "SockZadanyInfo";
            SockZadanyInfo.Size = new Size(1848, 698);
            SockZadanyInfo.Text = "НОСКИ. ОТЧЕТ ПО ЗАДАНИЮ";
            // 
            // layoutControl2
            // 
            layoutControl2.Controls.Add(TextBoxKolPlanZadany);
            layoutControl2.Controls.Add(customLabel5);
            layoutControl2.Controls.Add(gridControlSockDefectList);
            layoutControl2.Controls.Add(TextBoxKnitTotalTime);
            layoutControl2.Controls.Add(customLabel20);
            layoutControl2.Controls.Add(gridControlSockDownTimeList);
            layoutControl2.Controls.Add(TextBoxKolFactSmen);
            layoutControl2.Controls.Add(TextBoxKolFactDelta);
            layoutControl2.Controls.Add(customLabel19);
            layoutControl2.Controls.Add(TextBoxKolFactZadany);
            layoutControl2.Controls.Add(customLabel18);
            layoutControl2.Controls.Add(gridControlSockServiceList);
            layoutControl2.Controls.Add(gridControlSockZadanySmenList);
            layoutControl2.Controls.Add(TextBoxKnitEndDate);
            layoutControl2.Controls.Add(TextBoxKnitStartDate);
            layoutControl2.Controls.Add(TextBoxAreaNumber);
            layoutControl2.Controls.Add(TextBoxMachineNumber);
            layoutControl2.Controls.Add(TextBoxTabFio);
            layoutControl2.Controls.Add(customLabel14);
            layoutControl2.Controls.Add(customLabel13);
            layoutControl2.Controls.Add(customLabel12);
            layoutControl2.Controls.Add(customLabel11);
            layoutControl2.Controls.Add(customLabel10);
            layoutControl2.Controls.Add(customLabel9);
            layoutControl2.Controls.Add(customLabel8);
            layoutControl2.Controls.Add(TextBoxDefectCount);
            layoutControl2.Controls.Add(TextBoxDefectWeight);
            layoutControl2.Controls.Add(customLabel7);
            layoutControl2.Location = new Point(8, 8);
            layoutControl2.Name = "layoutControl2";
            layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(1070, 182, 650, 400);
            layoutControl2.Root = layoutControlGroup3;
            layoutControl2.Size = new Size(1829, 596);
            layoutControl2.TabIndex = 1;
            layoutControl2.Text = "layoutControl2";
            // 
            // TextBoxKolPlanZadany
            // 
            TextBoxKolPlanZadany.Location = new Point(138, 168);
            TextBoxKolPlanZadany.Name = "TextBoxKolPlanZadany";
            TextBoxKolPlanZadany.ObjectName = null;
            TextBoxKolPlanZadany.Properties.Appearance.BackColor = Color.FromArgb(230, 245, 255);
            TextBoxKolPlanZadany.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxKolPlanZadany.Properties.Appearance.ForeColor = Color.FromArgb(50, 90, 160);
            TextBoxKolPlanZadany.Properties.Appearance.Options.UseBackColor = true;
            TextBoxKolPlanZadany.Properties.Appearance.Options.UseFont = true;
            TextBoxKolPlanZadany.Properties.Appearance.Options.UseForeColor = true;
            TextBoxKolPlanZadany.Size = new Size(67, 22);
            TextBoxKolPlanZadany.StyleController = layoutControl2;
            TextBoxKolPlanZadany.TabIndex = 24;
            // 
            // customLabel5
            // 
            customLabel5.Font = new Font("Arial", 10F);
            customLabel5.ForeColor = Color.FromArgb(30, 70, 140);
            customLabel5.Location = new Point(24, 168);
            customLabel5.Name = "customLabel5";
            customLabel5.Size = new Size(110, 32);
            customLabel5.TabIndex = 23;
            customLabel5.Text = "План. кол-во по заданию, шт.";
            // 
            // gridControlSockDefectList
            // 
            gridControlSockDefectList.Font = new Font("Arial", 10F);
            gridControlSockDefectList.Location = new Point(290, 205);
            gridControlSockDefectList.MainView = gridViewSockDefectList;
            gridControlSockDefectList.Name = "gridControlSockDefectList";
            gridControlSockDefectList.Size = new Size(483, 150);
            gridControlSockDefectList.TabIndex = 22;
            gridControlSockDefectList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSockDefectList });
            // 
            // gridViewSockDefectList
            // 
            gridViewSockDefectList.Appearance.EvenRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewSockDefectList.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewSockDefectList.Appearance.FocusedRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewSockDefectList.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridViewSockDefectList.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewSockDefectList.Appearance.FocusedRow.Options.UseFont = true;
            gridViewSockDefectList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridSockDefectListColumnVspdid, gridSockDefectListColumnNomZadany, gridSockDefectListColumnIsdefect, gridSockDefectListColumnKg, gridSockDefectListColumnKolAll, gridSockDefectListColumnKolDefect, gridSockDefectListColumnIdspj, gridSockDefectListColumnIdndsp, gridSockDefectListColumnNamedefect });
            gridViewSockDefectList.GridControl = gridControlSockDefectList;
            gridViewSockDefectList.Name = "gridViewSockDefectList";
            gridViewSockDefectList.OptionsView.EnableAppearanceEvenRow = true;
            gridViewSockDefectList.OptionsView.ShowFooter = true;
            gridViewSockDefectList.OptionsView.ShowGroupPanel = false;
            // 
            // gridSockDefectListColumnVspdid
            // 
            gridSockDefectListColumnVspdid.Caption = "vspdid";
            gridSockDefectListColumnVspdid.Name = "gridSockDefectListColumnVspdid";
            // 
            // gridSockDefectListColumnNomZadany
            // 
            gridSockDefectListColumnNomZadany.Caption = "nom_zadany";
            gridSockDefectListColumnNomZadany.Name = "gridSockDefectListColumnNomZadany";
            // 
            // gridSockDefectListColumnIsdefect
            // 
            gridSockDefectListColumnIsdefect.Caption = "isdefect";
            gridSockDefectListColumnIsdefect.Name = "gridSockDefectListColumnIsdefect";
            // 
            // gridSockDefectListColumnKg
            // 
            gridSockDefectListColumnKg.Caption = "kg";
            gridSockDefectListColumnKg.Name = "gridSockDefectListColumnKg";
            // 
            // gridSockDefectListColumnKolAll
            // 
            gridSockDefectListColumnKolAll.Caption = "kolAll";
            gridSockDefectListColumnKolAll.Name = "gridSockDefectListColumnKolAll";
            // 
            // gridSockDefectListColumnKolDefect
            // 
            gridSockDefectListColumnKolDefect.Caption = "Количество, шт.";
            gridSockDefectListColumnKolDefect.Name = "gridSockDefectListColumnKolDefect";
            gridSockDefectListColumnKolDefect.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "kolDefect", "{0:0.##}") });
            gridSockDefectListColumnKolDefect.Visible = true;
            gridSockDefectListColumnKolDefect.VisibleIndex = 0;
            gridSockDefectListColumnKolDefect.Width = 121;
            // 
            // gridSockDefectListColumnIdspj
            // 
            gridSockDefectListColumnIdspj.Caption = "idspj";
            gridSockDefectListColumnIdspj.Name = "gridSockDefectListColumnIdspj";
            // 
            // gridSockDefectListColumnIdndsp
            // 
            gridSockDefectListColumnIdndsp.Caption = "id_ndsp";
            gridSockDefectListColumnIdndsp.Name = "gridSockDefectListColumnIdndsp";
            // 
            // gridSockDefectListColumnNamedefect
            // 
            gridSockDefectListColumnNamedefect.Caption = "Причина брака";
            gridSockDefectListColumnNamedefect.Name = "gridSockDefectListColumnNamedefect";
            gridSockDefectListColumnNamedefect.Visible = true;
            gridSockDefectListColumnNamedefect.VisibleIndex = 1;
            gridSockDefectListColumnNamedefect.Width = 337;
            // 
            // TextBoxKnitTotalTime
            // 
            TextBoxKnitTotalTime.BackColor = Color.FromArgb(230, 245, 255);
            TextBoxKnitTotalTime.Font = new Font("Arial", 10F);
            TextBoxKnitTotalTime.ForeColor = Color.FromArgb(50, 90, 160);
            TextBoxKnitTotalTime.Location = new Point(663, 133);
            TextBoxKnitTotalTime.Name = "TextBoxKnitTotalTime";
            TextBoxKnitTotalTime.Size = new Size(110, 20);
            TextBoxKnitTotalTime.TabIndex = 21;
            // 
            // customLabel20
            // 
            customLabel20.Font = new Font("Arial", 10F);
            customLabel20.ForeColor = Color.FromArgb(30, 70, 140);
            customLabel20.Location = new Point(533, 133);
            customLabel20.Name = "customLabel20";
            customLabel20.Size = new Size(126, 30);
            customLabel20.TabIndex = 20;
            customLabel20.Text = "Время вязания\r\nзадания";
            // 
            // gridControlSockDownTimeList
            // 
            gridControlSockDownTimeList.Font = new Font("Arial", 10F);
            gridControlSockDownTimeList.Location = new Point(138, 369);
            gridControlSockDownTimeList.MainView = gridViewSockDownTimeList;
            gridControlSockDownTimeList.Name = "gridControlSockDownTimeList";
            gridControlSockDownTimeList.Size = new Size(635, 202);
            gridControlSockDownTimeList.TabIndex = 19;
            gridControlSockDownTimeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSockDownTimeList });
            // 
            // gridViewSockDownTimeList
            // 
            gridViewSockDownTimeList.Appearance.EvenRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewSockDownTimeList.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewSockDownTimeList.Appearance.FocusedRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewSockDownTimeList.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridViewSockDownTimeList.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewSockDownTimeList.Appearance.FocusedRow.Options.UseFont = true;
            gridViewSockDownTimeList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridSockDownTimeListColumnKzPszNom, gridSockDownTimeListColumnKmaNumber, gridSockDownTimeListColumnKmlNumber, gridSockDownTimeListColumnKmlInvNumber, gridSockDownTimeListColumnKmlID, gridSockDownTimeListColumnTextObS, gridSockDownTimeListColumnKdtlDateStart, gridSockDownTimeListColumnKdtlDateEnd, gridSockDownTimeListColumnDiffPeriod, gridSockDownTimeListColumnDaysDiff, gridSockDownTimeListColumnTimeDiff });
            gridViewSockDownTimeList.GridControl = gridControlSockDownTimeList;
            gridViewSockDownTimeList.Name = "gridViewSockDownTimeList";
            gridViewSockDownTimeList.OptionsView.EnableAppearanceEvenRow = true;
            gridViewSockDownTimeList.OptionsView.ShowFooter = true;
            gridViewSockDownTimeList.OptionsView.ShowGroupPanel = false;
            // 
            // gridSockDownTimeListColumnKzPszNom
            // 
            gridSockDownTimeListColumnKzPszNom.Caption = "№ задания";
            gridSockDownTimeListColumnKzPszNom.Name = "gridSockDownTimeListColumnKzPszNom";
            // 
            // gridSockDownTimeListColumnKmaNumber
            // 
            gridSockDownTimeListColumnKmaNumber.Caption = "Зона";
            gridSockDownTimeListColumnKmaNumber.Name = "gridSockDownTimeListColumnKmaNumber";
            gridSockDownTimeListColumnKmaNumber.OptionsColumn.FixedWidth = true;
            gridSockDownTimeListColumnKmaNumber.Visible = true;
            gridSockDownTimeListColumnKmaNumber.VisibleIndex = 0;
            gridSockDownTimeListColumnKmaNumber.Width = 45;
            // 
            // gridSockDownTimeListColumnKmlNumber
            // 
            gridSockDownTimeListColumnKmlNumber.Caption = "В/М";
            gridSockDownTimeListColumnKmlNumber.Name = "gridSockDownTimeListColumnKmlNumber";
            gridSockDownTimeListColumnKmlNumber.OptionsColumn.FixedWidth = true;
            gridSockDownTimeListColumnKmlNumber.Visible = true;
            gridSockDownTimeListColumnKmlNumber.VisibleIndex = 1;
            gridSockDownTimeListColumnKmlNumber.Width = 40;
            // 
            // gridSockDownTimeListColumnKmlInvNumber
            // 
            gridSockDownTimeListColumnKmlInvNumber.Caption = "В/М инв. №";
            gridSockDownTimeListColumnKmlInvNumber.Name = "gridSockDownTimeListColumnKmlInvNumber";
            gridSockDownTimeListColumnKmlInvNumber.OptionsColumn.FixedWidth = true;
            gridSockDownTimeListColumnKmlInvNumber.Visible = true;
            gridSockDownTimeListColumnKmlInvNumber.VisibleIndex = 2;
            gridSockDownTimeListColumnKmlInvNumber.Width = 55;
            // 
            // gridSockDownTimeListColumnKmlID
            // 
            gridSockDownTimeListColumnKmlID.Caption = "kmlID";
            gridSockDownTimeListColumnKmlID.Name = "gridSockDownTimeListColumnKmlID";
            // 
            // gridSockDownTimeListColumnTextObS
            // 
            gridSockDownTimeListColumnTextObS.Caption = "Вид оборудования";
            gridSockDownTimeListColumnTextObS.Name = "gridSockDownTimeListColumnTextObS";
            gridSockDownTimeListColumnTextObS.OptionsColumn.FixedWidth = true;
            gridSockDownTimeListColumnTextObS.Visible = true;
            gridSockDownTimeListColumnTextObS.VisibleIndex = 3;
            gridSockDownTimeListColumnTextObS.Width = 80;
            // 
            // gridSockDownTimeListColumnKdtlDateStart
            // 
            gridSockDownTimeListColumnKdtlDateStart.Caption = "Дата/время Начало простоя";
            gridSockDownTimeListColumnKdtlDateStart.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            gridSockDownTimeListColumnKdtlDateStart.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridSockDownTimeListColumnKdtlDateStart.Name = "gridSockDownTimeListColumnKdtlDateStart";
            gridSockDownTimeListColumnKdtlDateStart.OptionsColumn.FixedWidth = true;
            gridSockDownTimeListColumnKdtlDateStart.Visible = true;
            gridSockDownTimeListColumnKdtlDateStart.VisibleIndex = 4;
            gridSockDownTimeListColumnKdtlDateStart.Width = 120;
            // 
            // gridSockDownTimeListColumnKdtlDateEnd
            // 
            gridSockDownTimeListColumnKdtlDateEnd.Caption = "Дата/время Конец простоя";
            gridSockDownTimeListColumnKdtlDateEnd.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            gridSockDownTimeListColumnKdtlDateEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridSockDownTimeListColumnKdtlDateEnd.Name = "gridSockDownTimeListColumnKdtlDateEnd";
            gridSockDownTimeListColumnKdtlDateEnd.OptionsColumn.FixedWidth = true;
            gridSockDownTimeListColumnKdtlDateEnd.Visible = true;
            gridSockDownTimeListColumnKdtlDateEnd.VisibleIndex = 5;
            gridSockDownTimeListColumnKdtlDateEnd.Width = 120;
            // 
            // gridSockDownTimeListColumnDiffPeriod
            // 
            gridSockDownTimeListColumnDiffPeriod.Caption = "Продолжительность простоя";
            gridSockDownTimeListColumnDiffPeriod.Name = "gridSockDownTimeListColumnDiffPeriod";
            gridSockDownTimeListColumnDiffPeriod.OptionsColumn.FixedWidth = true;
            gridSockDownTimeListColumnDiffPeriod.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TimeDiff", "{0:dd' д 'hh\\:mm\\:ss}") });
            gridSockDownTimeListColumnDiffPeriod.Visible = true;
            gridSockDownTimeListColumnDiffPeriod.VisibleIndex = 6;
            gridSockDownTimeListColumnDiffPeriod.Width = 120;
            // 
            // gridSockDownTimeListColumnDaysDiff
            // 
            gridSockDownTimeListColumnDaysDiff.Caption = "DaysDiff";
            gridSockDownTimeListColumnDaysDiff.Name = "gridSockDownTimeListColumnDaysDiff";
            // 
            // gridSockDownTimeListColumnTimeDiff
            // 
            gridSockDownTimeListColumnTimeDiff.Caption = "TimeDiff";
            gridSockDownTimeListColumnTimeDiff.Name = "gridSockDownTimeListColumnTimeDiff";
            // 
            // TextBoxKolFactSmen
            // 
            TextBoxKolFactSmen.Location = new Point(528, 168);
            TextBoxKolFactSmen.Name = "TextBoxKolFactSmen";
            TextBoxKolFactSmen.ObjectName = null;
            TextBoxKolFactSmen.Properties.Appearance.BackColor = Color.FromArgb(230, 245, 255);
            TextBoxKolFactSmen.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxKolFactSmen.Properties.Appearance.ForeColor = Color.FromArgb(50, 90, 160);
            TextBoxKolFactSmen.Properties.Appearance.Options.UseBackColor = true;
            TextBoxKolFactSmen.Properties.Appearance.Options.UseFont = true;
            TextBoxKolFactSmen.Properties.Appearance.Options.UseForeColor = true;
            TextBoxKolFactSmen.Size = new Size(66, 22);
            TextBoxKolFactSmen.StyleController = layoutControl2;
            TextBoxKolFactSmen.TabIndex = 14;
            // 
            // TextBoxKolFactDelta
            // 
            TextBoxKolFactDelta.Location = new Point(722, 168);
            TextBoxKolFactDelta.Name = "TextBoxKolFactDelta";
            TextBoxKolFactDelta.ObjectName = null;
            TextBoxKolFactDelta.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxKolFactDelta.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxKolFactDelta.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxKolFactDelta.Properties.Appearance.Options.UseBackColor = true;
            TextBoxKolFactDelta.Properties.Appearance.Options.UseFont = true;
            TextBoxKolFactDelta.Properties.Appearance.Options.UseForeColor = true;
            TextBoxKolFactDelta.Size = new Size(51, 22);
            TextBoxKolFactDelta.StyleController = layoutControl2;
            TextBoxKolFactDelta.TabIndex = 15;
            // 
            // customLabel19
            // 
            customLabel19.Font = new Font("Arial", 10F);
            customLabel19.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel19.Location = new Point(608, 168);
            customLabel19.Name = "customLabel19";
            customLabel19.Size = new Size(110, 32);
            customLabel19.TabIndex = 1;
            customLabel19.Text = "Разница по датчикам, шт.";
            // 
            // TextBoxKolFactZadany
            // 
            TextBoxKolFactZadany.Location = new Point(335, 168);
            TextBoxKolFactZadany.Name = "TextBoxKolFactZadany";
            TextBoxKolFactZadany.ObjectName = null;
            TextBoxKolFactZadany.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxKolFactZadany.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxKolFactZadany.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxKolFactZadany.Properties.Appearance.Options.UseBackColor = true;
            TextBoxKolFactZadany.Properties.Appearance.Options.UseFont = true;
            TextBoxKolFactZadany.Properties.Appearance.Options.UseForeColor = true;
            TextBoxKolFactZadany.Size = new Size(65, 22);
            TextBoxKolFactZadany.StyleController = layoutControl2;
            TextBoxKolFactZadany.TabIndex = 13;
            // 
            // customLabel18
            // 
            customLabel18.Font = new Font("Arial", 10F);
            customLabel18.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel18.Location = new Point(219, 168);
            customLabel18.Name = "customLabel18";
            customLabel18.Size = new Size(112, 32);
            customLabel18.TabIndex = 1;
            customLabel18.Text = "Факт. кол-во по заданию, шт";
            // 
            // gridControlSockServiceList
            // 
            gridControlSockServiceList.Font = new Font("Arial", 10F);
            gridControlSockServiceList.Location = new Point(801, 330);
            gridControlSockServiceList.MainView = gridView2;
            gridControlSockServiceList.Name = "gridControlSockServiceList";
            gridControlSockServiceList.Size = new Size(1004, 240);
            gridControlSockServiceList.TabIndex = 18;
            gridControlSockServiceList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView2 });
            // 
            // gridView2
            // 
            gridView2.Appearance.EvenRow.BackColor = Color.FromArgb(230, 230, 250);
            gridView2.Appearance.EvenRow.Options.UseBackColor = true;
            gridView2.Appearance.FocusedRow.BackColor = Color.FromArgb(230, 230, 250);
            gridView2.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridView2.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView2.Appearance.FocusedRow.Options.UseFont = true;
            gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridSockServiceListColumnKzPszNom, gridSockServiceListColumnKmaNumber, gridSockServiceListColumnKmlInvNum, gridSockServiceListColumnKmlNumber, gridSockServiceListColumnTextObS, gridSockServiceListColumnDirectorName, gridSockServiceListColumnDate, gridSockServiceListColumnResultName, gridSockServiceListColumnResultText, gridSockServiceListColumnDateEnd, gridSockServiceListColumnMechanic, gridSockServiceListColumnDiffPeriod, gridSockServiceListColumnDaysDiff, gridSockServiceListColumnTimeDiff });
            gridView2.GridControl = gridControlSockServiceList;
            gridView2.Name = "gridView2";
            gridView2.OptionsView.EnableAppearanceEvenRow = true;
            gridView2.OptionsView.ShowFooter = true;
            gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridSockServiceListColumnKzPszNom
            // 
            gridSockServiceListColumnKzPszNom.Caption = "№ задания";
            gridSockServiceListColumnKzPszNom.Name = "gridSockServiceListColumnKzPszNom";
            // 
            // gridSockServiceListColumnKmaNumber
            // 
            gridSockServiceListColumnKmaNumber.Caption = "Зона";
            gridSockServiceListColumnKmaNumber.Name = "gridSockServiceListColumnKmaNumber";
            gridSockServiceListColumnKmaNumber.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnKmaNumber.Visible = true;
            gridSockServiceListColumnKmaNumber.VisibleIndex = 0;
            gridSockServiceListColumnKmaNumber.Width = 45;
            // 
            // gridSockServiceListColumnKmlInvNum
            // 
            gridSockServiceListColumnKmlInvNum.Caption = "В/М инв. №";
            gridSockServiceListColumnKmlInvNum.Name = "gridSockServiceListColumnKmlInvNum";
            gridSockServiceListColumnKmlInvNum.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnKmlInvNum.Visible = true;
            gridSockServiceListColumnKmlInvNum.VisibleIndex = 2;
            gridSockServiceListColumnKmlInvNum.Width = 55;
            // 
            // gridSockServiceListColumnKmlNumber
            // 
            gridSockServiceListColumnKmlNumber.Caption = "В/М";
            gridSockServiceListColumnKmlNumber.Name = "gridSockServiceListColumnKmlNumber";
            gridSockServiceListColumnKmlNumber.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnKmlNumber.Visible = true;
            gridSockServiceListColumnKmlNumber.VisibleIndex = 1;
            gridSockServiceListColumnKmlNumber.Width = 40;
            // 
            // gridSockServiceListColumnTextObS
            // 
            gridSockServiceListColumnTextObS.Caption = "Вид оборудования";
            gridSockServiceListColumnTextObS.Name = "gridSockServiceListColumnTextObS";
            gridSockServiceListColumnTextObS.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnTextObS.Visible = true;
            gridSockServiceListColumnTextObS.VisibleIndex = 3;
            gridSockServiceListColumnTextObS.Width = 80;
            // 
            // gridSockServiceListColumnDirectorName
            // 
            gridSockServiceListColumnDirectorName.Caption = "ФИО (кто вызвал)";
            gridSockServiceListColumnDirectorName.Name = "gridSockServiceListColumnDirectorName";
            gridSockServiceListColumnDirectorName.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnDirectorName.Visible = true;
            gridSockServiceListColumnDirectorName.VisibleIndex = 4;
            gridSockServiceListColumnDirectorName.Width = 110;
            // 
            // gridSockServiceListColumnDate
            // 
            gridSockServiceListColumnDate.Caption = "Дата/время Начало простоя";
            gridSockServiceListColumnDate.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            gridSockServiceListColumnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridSockServiceListColumnDate.Name = "gridSockServiceListColumnDate";
            gridSockServiceListColumnDate.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnDate.Visible = true;
            gridSockServiceListColumnDate.VisibleIndex = 5;
            gridSockServiceListColumnDate.Width = 120;
            // 
            // gridSockServiceListColumnResultName
            // 
            gridSockServiceListColumnResultName.Caption = "Описание проблемы";
            gridSockServiceListColumnResultName.Name = "gridSockServiceListColumnResultName";
            gridSockServiceListColumnResultName.Visible = true;
            gridSockServiceListColumnResultName.VisibleIndex = 6;
            gridSockServiceListColumnResultName.Width = 106;
            // 
            // gridSockServiceListColumnResultText
            // 
            gridSockServiceListColumnResultText.Caption = "Принятые меры";
            gridSockServiceListColumnResultText.Name = "gridSockServiceListColumnResultText";
            gridSockServiceListColumnResultText.Visible = true;
            gridSockServiceListColumnResultText.VisibleIndex = 7;
            gridSockServiceListColumnResultText.Width = 108;
            // 
            // gridSockServiceListColumnDateEnd
            // 
            gridSockServiceListColumnDateEnd.Caption = "Дата/время Конец простоя";
            gridSockServiceListColumnDateEnd.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            gridSockServiceListColumnDateEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridSockServiceListColumnDateEnd.Name = "gridSockServiceListColumnDateEnd";
            gridSockServiceListColumnDateEnd.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnDateEnd.Visible = true;
            gridSockServiceListColumnDateEnd.VisibleIndex = 8;
            gridSockServiceListColumnDateEnd.Width = 120;
            // 
            // gridSockServiceListColumnMechanic
            // 
            gridSockServiceListColumnMechanic.Caption = "ФИО механика / таб";
            gridSockServiceListColumnMechanic.Name = "gridSockServiceListColumnMechanic";
            gridSockServiceListColumnMechanic.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnMechanic.Visible = true;
            gridSockServiceListColumnMechanic.VisibleIndex = 9;
            gridSockServiceListColumnMechanic.Width = 110;
            // 
            // gridSockServiceListColumnDiffPeriod
            // 
            gridSockServiceListColumnDiffPeriod.Caption = "Продолжительность остановки оборудования час/мин/сек (9-6)";
            gridSockServiceListColumnDiffPeriod.Name = "gridSockServiceListColumnDiffPeriod";
            gridSockServiceListColumnDiffPeriod.OptionsColumn.FixedWidth = true;
            gridSockServiceListColumnDiffPeriod.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TimeDiff", "{0:dd' д 'hh\\:mm\\:ss}") });
            gridSockServiceListColumnDiffPeriod.Visible = true;
            gridSockServiceListColumnDiffPeriod.VisibleIndex = 10;
            gridSockServiceListColumnDiffPeriod.Width = 85;
            // 
            // gridSockServiceListColumnDaysDiff
            // 
            gridSockServiceListColumnDaysDiff.Caption = "DaysDiff";
            gridSockServiceListColumnDaysDiff.Name = "gridSockServiceListColumnDaysDiff";
            // 
            // gridSockServiceListColumnTimeDiff
            // 
            gridSockServiceListColumnTimeDiff.Caption = "TimeDiff";
            gridSockServiceListColumnTimeDiff.Name = "gridSockServiceListColumnTimeDiff";
            // 
            // gridControlSockZadanySmenList
            // 
            gridControlSockZadanySmenList.Font = new Font("Arial", 10F);
            gridControlSockZadanySmenList.Location = new Point(801, 45);
            gridControlSockZadanySmenList.MainView = gridView1;
            gridControlSockZadanySmenList.Name = "gridControlSockZadanySmenList";
            gridControlSockZadanySmenList.Size = new Size(1004, 226);
            gridControlSockZadanySmenList.TabIndex = 7;
            gridControlSockZadanySmenList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Appearance.EvenRow.BackColor = Color.FromArgb(230, 230, 250);
            gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            gridView1.Appearance.FocusedRow.BackColor = Color.FromArgb(230, 230, 250);
            gridView1.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView1.Appearance.FocusedRow.Options.UseFont = true;
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridSockZadanySmenListColumnKzDateAdd, gridSockZadanySmenListColumnKwsTabStart, gridSockZadanySmenListColumnFioSt, gridSockZadanySmenListColumnKzDateEnd, gridSockZadanySmenListColumnKwsTabEnd, gridSockZadanySmenListColumnFioEn, gridSockZadanySmenListColumnKolFact, gridSockZadanySmenListColumnChasVyaz, gridSockZadanySmenListColumnDiffPeriod, gridSockZadanySmenListColumnKmaNumber, gridSockZadanySmenListColumnKmlNumber, gridSockZadanySmenListColumnKzID, gridSockZadanySmenListColumnKzKwsID, gridSockZadanySmenListColumnKzKmlID, gridSockZadanySmenListColumnKzKmaID, gridSockZadanySmenListColumnKzEnded, gridSockZadanySmenListColumnDivider, gridSockZadanySmenListColumnKwsKmsID, gridSockZadanySmenListColumnKmlInvNumber });
            gridView1.GridControl = gridControlSockZadanySmenList;
            gridView1.Name = "gridView1";
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridSockZadanySmenListColumnKzDateAdd
            // 
            gridSockZadanySmenListColumnKzDateAdd.Caption = "Дата/время начала вязания";
            gridSockZadanySmenListColumnKzDateAdd.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            gridSockZadanySmenListColumnKzDateAdd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridSockZadanySmenListColumnKzDateAdd.Name = "gridSockZadanySmenListColumnKzDateAdd";
            gridSockZadanySmenListColumnKzDateAdd.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnKzDateAdd.Visible = true;
            gridSockZadanySmenListColumnKzDateAdd.VisibleIndex = 3;
            gridSockZadanySmenListColumnKzDateAdd.Width = 120;
            // 
            // gridSockZadanySmenListColumnKwsTabStart
            // 
            gridSockZadanySmenListColumnKwsTabStart.Caption = "Таб. №";
            gridSockZadanySmenListColumnKwsTabStart.Name = "gridSockZadanySmenListColumnKwsTabStart";
            gridSockZadanySmenListColumnKwsTabStart.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnKwsTabStart.Visible = true;
            gridSockZadanySmenListColumnKwsTabStart.VisibleIndex = 4;
            gridSockZadanySmenListColumnKwsTabStart.Width = 50;
            // 
            // gridSockZadanySmenListColumnFioSt
            // 
            gridSockZadanySmenListColumnFioSt.Caption = "ФИО";
            gridSockZadanySmenListColumnFioSt.Name = "gridSockZadanySmenListColumnFioSt";
            gridSockZadanySmenListColumnFioSt.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnFioSt.Visible = true;
            gridSockZadanySmenListColumnFioSt.VisibleIndex = 5;
            gridSockZadanySmenListColumnFioSt.Width = 120;
            // 
            // gridSockZadanySmenListColumnKzDateEnd
            // 
            gridSockZadanySmenListColumnKzDateEnd.Caption = "Дата/время окончания вязания";
            gridSockZadanySmenListColumnKzDateEnd.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            gridSockZadanySmenListColumnKzDateEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridSockZadanySmenListColumnKzDateEnd.Name = "gridSockZadanySmenListColumnKzDateEnd";
            gridSockZadanySmenListColumnKzDateEnd.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnKzDateEnd.Visible = true;
            gridSockZadanySmenListColumnKzDateEnd.VisibleIndex = 6;
            gridSockZadanySmenListColumnKzDateEnd.Width = 120;
            // 
            // gridSockZadanySmenListColumnKwsTabEnd
            // 
            gridSockZadanySmenListColumnKwsTabEnd.Caption = "Таб. №";
            gridSockZadanySmenListColumnKwsTabEnd.Name = "gridSockZadanySmenListColumnKwsTabEnd";
            gridSockZadanySmenListColumnKwsTabEnd.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnKwsTabEnd.Visible = true;
            gridSockZadanySmenListColumnKwsTabEnd.VisibleIndex = 7;
            gridSockZadanySmenListColumnKwsTabEnd.Width = 50;
            // 
            // gridSockZadanySmenListColumnFioEn
            // 
            gridSockZadanySmenListColumnFioEn.Caption = "ФИО";
            gridSockZadanySmenListColumnFioEn.Name = "gridSockZadanySmenListColumnFioEn";
            gridSockZadanySmenListColumnFioEn.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnFioEn.Visible = true;
            gridSockZadanySmenListColumnFioEn.VisibleIndex = 8;
            gridSockZadanySmenListColumnFioEn.Width = 120;
            // 
            // gridSockZadanySmenListColumnKolFact
            // 
            gridSockZadanySmenListColumnKolFact.Caption = "Кол- во";
            gridSockZadanySmenListColumnKolFact.Name = "gridSockZadanySmenListColumnKolFact";
            gridSockZadanySmenListColumnKolFact.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnKolFact.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "kolFakt", "{0:0.##}") });
            gridSockZadanySmenListColumnKolFact.Visible = true;
            gridSockZadanySmenListColumnKolFact.VisibleIndex = 9;
            gridSockZadanySmenListColumnKolFact.Width = 50;
            // 
            // gridSockZadanySmenListColumnChasVyaz
            // 
            gridSockZadanySmenListColumnChasVyaz.Caption = "Итого, час";
            gridSockZadanySmenListColumnChasVyaz.Name = "gridSockZadanySmenListColumnChasVyaz";
            gridSockZadanySmenListColumnChasVyaz.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnChasVyaz.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ChasVyaz", "{0:0.##}") });
            gridSockZadanySmenListColumnChasVyaz.Visible = true;
            gridSockZadanySmenListColumnChasVyaz.VisibleIndex = 10;
            gridSockZadanySmenListColumnChasVyaz.Width = 70;
            // 
            // gridSockZadanySmenListColumnDiffPeriod
            // 
            gridSockZadanySmenListColumnDiffPeriod.Caption = "Разница между сменами, дн.";
            gridSockZadanySmenListColumnDiffPeriod.Name = "gridSockZadanySmenListColumnDiffPeriod";
            gridSockZadanySmenListColumnDiffPeriod.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TimeDiff", "{0:dd' д 'hh\\:mm\\:ss}") });
            gridSockZadanySmenListColumnDiffPeriod.Visible = true;
            gridSockZadanySmenListColumnDiffPeriod.VisibleIndex = 11;
            gridSockZadanySmenListColumnDiffPeriod.Width = 139;
            // 
            // gridSockZadanySmenListColumnKmaNumber
            // 
            gridSockZadanySmenListColumnKmaNumber.Caption = "Зона";
            gridSockZadanySmenListColumnKmaNumber.Name = "gridSockZadanySmenListColumnKmaNumber";
            gridSockZadanySmenListColumnKmaNumber.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnKmaNumber.Visible = true;
            gridSockZadanySmenListColumnKmaNumber.VisibleIndex = 0;
            gridSockZadanySmenListColumnKmaNumber.Width = 45;
            // 
            // gridSockZadanySmenListColumnKmlNumber
            // 
            gridSockZadanySmenListColumnKmlNumber.Caption = "В/М";
            gridSockZadanySmenListColumnKmlNumber.Name = "gridSockZadanySmenListColumnKmlNumber";
            gridSockZadanySmenListColumnKmlNumber.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnKmlNumber.Visible = true;
            gridSockZadanySmenListColumnKmlNumber.VisibleIndex = 1;
            gridSockZadanySmenListColumnKmlNumber.Width = 40;
            // 
            // gridSockZadanySmenListColumnKzID
            // 
            gridSockZadanySmenListColumnKzID.Caption = "kzID";
            gridSockZadanySmenListColumnKzID.Name = "gridSockZadanySmenListColumnKzID";
            // 
            // gridSockZadanySmenListColumnKzKwsID
            // 
            gridSockZadanySmenListColumnKzKwsID.Caption = "kzKwsID";
            gridSockZadanySmenListColumnKzKwsID.Name = "gridSockZadanySmenListColumnKzKwsID";
            // 
            // gridSockZadanySmenListColumnKzKmlID
            // 
            gridSockZadanySmenListColumnKzKmlID.Caption = "kzKmlID";
            gridSockZadanySmenListColumnKzKmlID.Name = "gridSockZadanySmenListColumnKzKmlID";
            // 
            // gridSockZadanySmenListColumnKzKmaID
            // 
            gridSockZadanySmenListColumnKzKmaID.Caption = "kzKmaID";
            gridSockZadanySmenListColumnKzKmaID.Name = "gridSockZadanySmenListColumnKzKmaID";
            // 
            // gridSockZadanySmenListColumnKzEnded
            // 
            gridSockZadanySmenListColumnKzEnded.Caption = "kzEnded";
            gridSockZadanySmenListColumnKzEnded.Name = "gridSockZadanySmenListColumnKzEnded";
            // 
            // gridSockZadanySmenListColumnDivider
            // 
            gridSockZadanySmenListColumnDivider.Caption = "divider";
            gridSockZadanySmenListColumnDivider.Name = "gridSockZadanySmenListColumnDivider";
            // 
            // gridSockZadanySmenListColumnKwsKmsID
            // 
            gridSockZadanySmenListColumnKwsKmsID.Caption = "kwsKmsID";
            gridSockZadanySmenListColumnKwsKmsID.Name = "gridSockZadanySmenListColumnKwsKmsID";
            // 
            // gridSockZadanySmenListColumnKmlInvNumber
            // 
            gridSockZadanySmenListColumnKmlInvNumber.Caption = "В/М инв.№";
            gridSockZadanySmenListColumnKmlInvNumber.Name = "gridSockZadanySmenListColumnKmlInvNumber";
            gridSockZadanySmenListColumnKmlInvNumber.OptionsColumn.FixedWidth = true;
            gridSockZadanySmenListColumnKmlInvNumber.Visible = true;
            gridSockZadanySmenListColumnKmlInvNumber.VisibleIndex = 2;
            gridSockZadanySmenListColumnKmlInvNumber.Width = 55;
            // 
            // TextBoxKnitEndDate
            // 
            TextBoxKnitEndDate.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxKnitEndDate.Font = new Font("Arial", 10F);
            TextBoxKnitEndDate.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxKnitEndDate.Location = new Point(386, 133);
            TextBoxKnitEndDate.Multiline = true;
            TextBoxKnitEndDate.Name = "TextBoxKnitEndDate";
            TextBoxKnitEndDate.Size = new Size(133, 20);
            TextBoxKnitEndDate.TabIndex = 12;
            // 
            // TextBoxKnitStartDate
            // 
            TextBoxKnitStartDate.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxKnitStartDate.Font = new Font("Arial", 10F);
            TextBoxKnitStartDate.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxKnitStartDate.Location = new Point(138, 133);
            TextBoxKnitStartDate.Multiline = true;
            TextBoxKnitStartDate.Name = "TextBoxKnitStartDate";
            TextBoxKnitStartDate.Size = new Size(127, 20);
            TextBoxKnitStartDate.TabIndex = 11;
            // 
            // TextBoxAreaNumber
            // 
            TextBoxAreaNumber.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxAreaNumber.Font = new Font("Arial", 10F);
            TextBoxAreaNumber.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxAreaNumber.Location = new Point(138, 108);
            TextBoxAreaNumber.Multiline = true;
            TextBoxAreaNumber.Name = "TextBoxAreaNumber";
            TextBoxAreaNumber.Size = new Size(127, 20);
            TextBoxAreaNumber.TabIndex = 9;
            // 
            // TextBoxMachineNumber
            // 
            TextBoxMachineNumber.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxMachineNumber.Font = new Font("Arial", 10F);
            TextBoxMachineNumber.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxMachineNumber.Location = new Point(386, 108);
            TextBoxMachineNumber.Multiline = true;
            TextBoxMachineNumber.Name = "TextBoxMachineNumber";
            TextBoxMachineNumber.Size = new Size(387, 20);
            TextBoxMachineNumber.TabIndex = 10;
            // 
            // TextBoxTabFio
            // 
            TextBoxTabFio.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxTabFio.Font = new Font("Arial", 10F);
            TextBoxTabFio.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxTabFio.Location = new Point(138, 45);
            TextBoxTabFio.Multiline = true;
            TextBoxTabFio.Name = "TextBoxTabFio";
            TextBoxTabFio.Size = new Size(635, 58);
            TextBoxTabFio.TabIndex = 8;
            // 
            // customLabel14
            // 
            customLabel14.Font = new Font("Arial", 10F);
            customLabel14.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel14.Location = new Point(24, 369);
            customLabel14.Name = "customLabel14";
            customLabel14.Size = new Size(110, 202);
            customLabel14.TabIndex = 1;
            customLabel14.Text = "Простои";
            // 
            // customLabel13
            // 
            customLabel13.Font = new Font("Arial", 10F);
            customLabel13.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel13.Location = new Point(414, 168);
            customLabel13.Name = "customLabel13";
            customLabel13.Size = new Size(110, 32);
            customLabel13.TabIndex = 1;
            customLabel13.Text = "Факт. кол-во по сменам, шт.";
            // 
            // customLabel12
            // 
            customLabel12.Font = new Font("Arial", 10F);
            customLabel12.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel12.Location = new Point(290, 133);
            customLabel12.Name = "customLabel12";
            customLabel12.Size = new Size(92, 30);
            customLabel12.TabIndex = 1;
            customLabel12.Text = "Окончание \r\nвязания";
            // 
            // customLabel11
            // 
            customLabel11.Font = new Font("Arial", 10F);
            customLabel11.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel11.Location = new Point(24, 133);
            customLabel11.Name = "customLabel11";
            customLabel11.Size = new Size(110, 30);
            customLabel11.TabIndex = 1;
            customLabel11.Text = "Начало \r\nвязания";
            // 
            // customLabel10
            // 
            customLabel10.Font = new Font("Arial", 10F);
            customLabel10.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel10.Location = new Point(24, 108);
            customLabel10.Name = "customLabel10";
            customLabel10.Size = new Size(110, 20);
            customLabel10.TabIndex = 1;
            customLabel10.Text = "Зона";
            // 
            // customLabel9
            // 
            customLabel9.Font = new Font("Arial", 10F);
            customLabel9.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel9.Location = new Point(290, 108);
            customLabel9.Name = "customLabel9";
            customLabel9.Size = new Size(92, 20);
            customLabel9.TabIndex = 1;
            customLabel9.Text = "Автомат";
            // 
            // customLabel8
            // 
            customLabel8.Font = new Font("Arial", 10F);
            customLabel8.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel8.Location = new Point(24, 45);
            customLabel8.Name = "customLabel8";
            customLabel8.Size = new Size(110, 58);
            customLabel8.TabIndex = 1;
            customLabel8.Text = "Таб. № - ФИО";
            // 
            // TextBoxDefectCount
            // 
            TextBoxDefectCount.Location = new Point(138, 235);
            TextBoxDefectCount.Name = "TextBoxDefectCount";
            TextBoxDefectCount.ObjectName = null;
            TextBoxDefectCount.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxDefectCount.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxDefectCount.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxDefectCount.Properties.Appearance.Options.UseBackColor = true;
            TextBoxDefectCount.Properties.Appearance.Options.UseFont = true;
            TextBoxDefectCount.Properties.Appearance.Options.UseForeColor = true;
            TextBoxDefectCount.Size = new Size(122, 22);
            TextBoxDefectCount.StyleController = layoutControl2;
            TextBoxDefectCount.TabIndex = 6;
            // 
            // TextBoxDefectWeight
            // 
            TextBoxDefectWeight.Location = new Point(138, 205);
            TextBoxDefectWeight.Name = "TextBoxDefectWeight";
            TextBoxDefectWeight.ObjectName = null;
            TextBoxDefectWeight.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxDefectWeight.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxDefectWeight.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxDefectWeight.Properties.Appearance.Options.UseBackColor = true;
            TextBoxDefectWeight.Properties.Appearance.Options.UseFont = true;
            TextBoxDefectWeight.Properties.Appearance.Options.UseForeColor = true;
            TextBoxDefectWeight.Size = new Size(122, 22);
            TextBoxDefectWeight.StyleController = layoutControl2;
            TextBoxDefectWeight.TabIndex = 16;
            // 
            // customLabel7
            // 
            customLabel7.Font = new Font("Arial", 10F);
            customLabel7.ForeColor = Color.FromArgb(72, 61, 139);
            customLabel7.Location = new Point(24, 205);
            customLabel7.Name = "customLabel7";
            customLabel7.Size = new Size(110, 150);
            customLabel7.TabIndex = 1;
            customLabel7.Text = "Брак";
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup3.GroupBordersVisible = false;
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup6, simpleSeparator3, simpleSeparator1, layoutControlGroup4, layoutControlGroup7, splitterItem2 });
            layoutControlGroup3.Name = "Root";
            layoutControlGroup3.Size = new Size(1829, 596);
            layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            buttonImageOptions1.Image = (Image)resources.GetObject("buttonImageOptions1.Image");
            layoutControlGroup6.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Печать", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup6.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem26, layoutControlItem27, layoutControlItem29, layoutControlItem30, layoutControlItem31, emptySpaceItem8, layoutControlItem32, layoutControlItem33, layoutControlItem34, layoutControlItem35, emptySpaceItem9, layoutControlItem36, layoutControlItem37, emptySpaceItem4, layoutControlItem12, layoutControlItem28, layoutControlItem41, layoutControlItem42, emptySpaceItem6, layoutControlItem38, layoutControlItem40, emptySpaceItem7, layoutControlItem43, layoutControlItem44, layoutControlItem45, emptySpaceItem13, emptySpaceItem14, simpleSeparator2, simpleSeparator4, simpleSeparator5, simpleSeparator6, layoutControlItem24, layoutControlItem19, layoutControlItem25, layoutControlItem39, emptySpaceItem11, splitterItem1, layoutControlItem13, layoutControlItem14, emptySpaceItem12, emptySpaceItem10, emptySpaceItem5 });
            layoutControlGroup6.Location = new Point(0, 0);
            layoutControlGroup6.Name = "layoutControlGroup3";
            layoutControlGroup6.Size = new Size(777, 575);
            layoutControlGroup6.Text = "Вязание";
            layoutControlGroup6.CustomButtonClick += layoutControlGroup6_CustomButtonClick;
            // 
            // layoutControlItem26
            // 
            layoutControlItem26.Control = customLabel8;
            layoutControlItem26.Location = new Point(0, 0);
            layoutControlItem26.Name = "layoutControlItem10";
            layoutControlItem26.Size = new Size(114, 62);
            layoutControlItem26.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            layoutControlItem27.Control = customLabel11;
            layoutControlItem27.Location = new Point(0, 88);
            layoutControlItem27.Name = "layoutControlItem20";
            layoutControlItem27.Size = new Size(114, 34);
            layoutControlItem27.TextVisible = false;
            // 
            // layoutControlItem29
            // 
            layoutControlItem29.Control = customLabel14;
            layoutControlItem29.Location = new Point(0, 324);
            layoutControlItem29.Name = "layoutControlItem23";
            layoutControlItem29.Size = new Size(114, 206);
            layoutControlItem29.TextVisible = false;
            // 
            // layoutControlItem30
            // 
            layoutControlItem30.Control = customLabel10;
            layoutControlItem30.Location = new Point(0, 63);
            layoutControlItem30.Name = "layoutControlItem19";
            layoutControlItem30.Size = new Size(114, 24);
            layoutControlItem30.TextVisible = false;
            // 
            // layoutControlItem31
            // 
            layoutControlItem31.Control = TextBoxAreaNumber;
            layoutControlItem31.Location = new Point(114, 63);
            layoutControlItem31.MinSize = new Size(24, 24);
            layoutControlItem31.Name = "layoutControlItem26";
            layoutControlItem31.Size = new Size(131, 24);
            layoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem31.TextVisible = false;
            // 
            // emptySpaceItem8
            // 
            emptySpaceItem8.Location = new Point(245, 63);
            emptySpaceItem8.Name = "emptySpaceItem6";
            emptySpaceItem8.Size = new Size(21, 24);
            // 
            // layoutControlItem32
            // 
            layoutControlItem32.Control = TextBoxTabFio;
            layoutControlItem32.Location = new Point(114, 0);
            layoutControlItem32.MinSize = new Size(24, 24);
            layoutControlItem32.Name = "layoutControlItem24";
            layoutControlItem32.Size = new Size(639, 62);
            layoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem32.TextVisible = false;
            // 
            // layoutControlItem33
            // 
            layoutControlItem33.Control = customLabel9;
            layoutControlItem33.Location = new Point(266, 63);
            layoutControlItem33.Name = "layoutControlItem18";
            layoutControlItem33.Size = new Size(96, 24);
            layoutControlItem33.TextVisible = false;
            // 
            // layoutControlItem34
            // 
            layoutControlItem34.Control = TextBoxMachineNumber;
            layoutControlItem34.Location = new Point(362, 63);
            layoutControlItem34.MinSize = new Size(24, 24);
            layoutControlItem34.Name = "layoutControlItem25";
            layoutControlItem34.Size = new Size(391, 24);
            layoutControlItem34.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem34.TextVisible = false;
            // 
            // layoutControlItem35
            // 
            layoutControlItem35.Control = TextBoxKnitStartDate;
            layoutControlItem35.Location = new Point(114, 88);
            layoutControlItem35.MinSize = new Size(24, 24);
            layoutControlItem35.Name = "layoutControlItem27";
            layoutControlItem35.Size = new Size(131, 24);
            layoutControlItem35.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem35.TextVisible = false;
            // 
            // emptySpaceItem9
            // 
            emptySpaceItem9.Location = new Point(245, 88);
            emptySpaceItem9.Name = "emptySpaceItem7";
            emptySpaceItem9.Size = new Size(21, 34);
            // 
            // layoutControlItem36
            // 
            layoutControlItem36.Control = customLabel12;
            layoutControlItem36.Location = new Point(266, 88);
            layoutControlItem36.Name = "layoutControlItem21";
            layoutControlItem36.Size = new Size(96, 34);
            layoutControlItem36.TextVisible = false;
            // 
            // layoutControlItem37
            // 
            layoutControlItem37.Control = TextBoxKnitEndDate;
            layoutControlItem37.Location = new Point(362, 88);
            layoutControlItem37.MinSize = new Size(24, 24);
            layoutControlItem37.Name = "layoutControlItem28";
            layoutControlItem37.Size = new Size(137, 24);
            layoutControlItem37.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem37.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.Location = new Point(114, 112);
            emptySpaceItem4.Name = "item0";
            emptySpaceItem4.Size = new Size(131, 10);
            // 
            // layoutControlItem12
            // 
            layoutControlItem12.Control = customLabel18;
            layoutControlItem12.Location = new Point(195, 123);
            layoutControlItem12.Name = "item2";
            layoutControlItem12.Size = new Size(116, 36);
            layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem28
            // 
            layoutControlItem28.Control = customLabel13;
            layoutControlItem28.Location = new Point(390, 123);
            layoutControlItem28.Name = "layoutControlItem22";
            layoutControlItem28.Size = new Size(114, 36);
            layoutControlItem28.TextVisible = false;
            // 
            // layoutControlItem41
            // 
            layoutControlItem41.Control = customLabel19;
            layoutControlItem41.Location = new Point(584, 123);
            layoutControlItem41.Name = "layoutControlItem41";
            layoutControlItem41.Size = new Size(114, 36);
            layoutControlItem41.TextVisible = false;
            // 
            // layoutControlItem42
            // 
            layoutControlItem42.Control = TextBoxKolFactDelta;
            layoutControlItem42.Location = new Point(698, 123);
            layoutControlItem42.MinSize = new Size(54, 26);
            layoutControlItem42.Name = "layoutControlItem42";
            layoutControlItem42.Size = new Size(55, 36);
            layoutControlItem42.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem42.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            emptySpaceItem6.Location = new Point(574, 123);
            emptySpaceItem6.Name = "item5";
            emptySpaceItem6.Size = new Size(10, 36);
            // 
            // layoutControlItem38
            // 
            layoutControlItem38.Control = TextBoxKolFactSmen;
            layoutControlItem38.Location = new Point(504, 123);
            layoutControlItem38.MinSize = new Size(54, 26);
            layoutControlItem38.Name = "layoutControlItem38";
            layoutControlItem38.Size = new Size(70, 36);
            layoutControlItem38.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem38.TextVisible = false;
            // 
            // layoutControlItem40
            // 
            layoutControlItem40.Control = TextBoxKolFactZadany;
            layoutControlItem40.Location = new Point(311, 123);
            layoutControlItem40.MinSize = new Size(54, 26);
            layoutControlItem40.Name = "layoutControlItem40";
            layoutControlItem40.Size = new Size(69, 26);
            layoutControlItem40.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem40.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            emptySpaceItem7.Location = new Point(362, 112);
            emptySpaceItem7.Name = "item1";
            emptySpaceItem7.Size = new Size(137, 10);
            // 
            // layoutControlItem43
            // 
            layoutControlItem43.Control = gridControlSockDownTimeList;
            layoutControlItem43.Location = new Point(114, 324);
            layoutControlItem43.Name = "layoutControlItem43";
            layoutControlItem43.Size = new Size(639, 206);
            layoutControlItem43.TextVisible = false;
            // 
            // layoutControlItem44
            // 
            layoutControlItem44.Control = customLabel20;
            layoutControlItem44.Location = new Point(509, 88);
            layoutControlItem44.Name = "layoutControlItem44";
            layoutControlItem44.Size = new Size(130, 34);
            layoutControlItem44.TextVisible = false;
            // 
            // layoutControlItem45
            // 
            layoutControlItem45.Control = TextBoxKnitTotalTime;
            layoutControlItem45.Location = new Point(639, 88);
            layoutControlItem45.MinSize = new Size(24, 24);
            layoutControlItem45.Name = "layoutControlItem45";
            layoutControlItem45.Size = new Size(114, 24);
            layoutControlItem45.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem45.TextVisible = false;
            // 
            // emptySpaceItem13
            // 
            emptySpaceItem13.Location = new Point(499, 88);
            emptySpaceItem13.Name = "emptySpaceItem13";
            emptySpaceItem13.Size = new Size(10, 34);
            // 
            // emptySpaceItem14
            // 
            emptySpaceItem14.Location = new Point(639, 112);
            emptySpaceItem14.Name = "emptySpaceItem14";
            emptySpaceItem14.Size = new Size(114, 10);
            // 
            // simpleSeparator2
            // 
            simpleSeparator2.Location = new Point(0, 62);
            simpleSeparator2.Name = "simpleSeparator2";
            simpleSeparator2.Size = new Size(753, 1);
            // 
            // simpleSeparator4
            // 
            simpleSeparator4.Location = new Point(0, 87);
            simpleSeparator4.Name = "simpleSeparator4";
            simpleSeparator4.Size = new Size(753, 1);
            // 
            // simpleSeparator5
            // 
            simpleSeparator5.Location = new Point(0, 122);
            simpleSeparator5.Name = "simpleSeparator5";
            simpleSeparator5.Size = new Size(753, 1);
            // 
            // simpleSeparator6
            // 
            simpleSeparator6.Location = new Point(0, 159);
            simpleSeparator6.Name = "simpleSeparator6";
            simpleSeparator6.Size = new Size(753, 1);
            // 
            // layoutControlItem24
            // 
            layoutControlItem24.Control = TextBoxDefectWeight;
            layoutControlItem24.Location = new Point(114, 160);
            layoutControlItem24.MinSize = new Size(54, 26);
            layoutControlItem24.Name = "layoutControlItem16";
            layoutControlItem24.Size = new Size(152, 30);
            layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem24.Text = "кг";
            layoutControlItem24.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem24.TextSize = new Size(14, 13);
            // 
            // layoutControlItem19
            // 
            layoutControlItem19.Control = customLabel7;
            layoutControlItem19.Location = new Point(0, 160);
            layoutControlItem19.MinSize = new Size(24, 24);
            layoutControlItem19.Name = "layoutControlItem9";
            layoutControlItem19.Size = new Size(114, 154);
            layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            layoutControlItem25.Control = TextBoxDefectCount;
            layoutControlItem25.Location = new Point(114, 190);
            layoutControlItem25.MinSize = new Size(54, 26);
            layoutControlItem25.Name = "layoutControlItem17";
            layoutControlItem25.Size = new Size(152, 30);
            layoutControlItem25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem25.Text = "шт";
            layoutControlItem25.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem25.TextSize = new Size(14, 13);
            // 
            // layoutControlItem39
            // 
            layoutControlItem39.Control = gridControlSockDefectList;
            layoutControlItem39.Location = new Point(266, 160);
            layoutControlItem39.Name = "layoutControlItem39";
            layoutControlItem39.Size = new Size(487, 154);
            layoutControlItem39.TextVisible = false;
            // 
            // emptySpaceItem11
            // 
            emptySpaceItem11.Location = new Point(114, 220);
            emptySpaceItem11.Name = "emptySpaceItem11";
            emptySpaceItem11.Size = new Size(152, 94);
            // 
            // splitterItem1
            // 
            splitterItem1.Location = new Point(0, 314);
            splitterItem1.Name = "splitterItem1";
            splitterItem1.Size = new Size(753, 10);
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = customLabel5;
            layoutControlItem13.Location = new Point(0, 123);
            layoutControlItem13.Name = "layoutControlItem13";
            layoutControlItem13.Size = new Size(114, 36);
            layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            layoutControlItem14.Control = TextBoxKolPlanZadany;
            layoutControlItem14.Location = new Point(114, 123);
            layoutControlItem14.Name = "layoutControlItem14";
            layoutControlItem14.Size = new Size(71, 36);
            layoutControlItem14.TextVisible = false;
            // 
            // emptySpaceItem12
            // 
            emptySpaceItem12.Location = new Point(185, 123);
            emptySpaceItem12.Name = "emptySpaceItem12";
            emptySpaceItem12.Size = new Size(10, 36);
            // 
            // emptySpaceItem10
            // 
            emptySpaceItem10.Location = new Point(380, 123);
            emptySpaceItem10.Name = "emptySpaceItem10";
            emptySpaceItem10.Size = new Size(10, 36);
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.Location = new Point(311, 149);
            emptySpaceItem5.Name = "emptySpaceItem5";
            emptySpaceItem5.Size = new Size(69, 10);
            // 
            // simpleSeparator3
            // 
            simpleSeparator3.Location = new Point(777, 574);
            simpleSeparator3.Name = "simpleSeparator3";
            simpleSeparator3.Size = new Size(1032, 1);
            // 
            // simpleSeparator1
            // 
            simpleSeparator1.Location = new Point(0, 575);
            simpleSeparator1.Name = "simpleSeparator1";
            simpleSeparator1.Size = new Size(1809, 1);
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem10 });
            layoutControlGroup4.Location = new Point(777, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Size = new Size(1032, 275);
            layoutControlGroup4.Text = "Смены";
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.Control = gridControlSockZadanySmenList;
            layoutControlItem10.Location = new Point(0, 0);
            layoutControlItem10.Name = "item4";
            layoutControlItem10.Size = new Size(1008, 230);
            layoutControlItem10.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem11 });
            layoutControlGroup7.Location = new Point(777, 285);
            layoutControlGroup7.Name = "layoutControlGroup7";
            layoutControlGroup7.Size = new Size(1032, 289);
            layoutControlGroup7.Text = "Обслуживание оборудования";
            // 
            // layoutControlItem11
            // 
            layoutControlItem11.Control = gridControlSockServiceList;
            layoutControlItem11.Location = new Point(0, 0);
            layoutControlItem11.Name = "item6";
            layoutControlItem11.Size = new Size(1008, 244);
            layoutControlItem11.TextVisible = false;
            // 
            // splitterItem2
            // 
            splitterItem2.Location = new Point(777, 275);
            splitterItem2.Name = "splitterItem2";
            splitterItem2.Size = new Size(1032, 10);
            // 
            // gridColumn57
            // 
            gridColumn57.Caption = "Отгр. на склад";
            gridColumn57.FieldName = "KolGI";
            gridColumn57.Name = "gridColumn57";
            gridColumn57.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", "Отгр. на скл.: {0:0.##}") });
            gridColumn57.Visible = true;
            gridColumn57.VisibleIndex = 8;
            // 
            // customLabel2
            // 
            customLabel2.BackColor = Color.Transparent;
            customLabel2.Font = new Font("Arial", 12F, FontStyle.Bold);
            customLabel2.ForeColor = Color.FromArgb(0, 0, 0);
            customLabel2.Location = new Point(2, 2);
            customLabel2.Margin = new Padding(0);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new Size(109, 28);
            customLabel2.TabIndex = 1;
            customLabel2.Text = "ПОИСК";
            customLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customLabel1
            // 
            customLabel1.BackColor = Color.Transparent;
            customLabel1.Font = new Font("Arial", 10F);
            customLabel1.ForeColor = Color.FromArgb(0, 0, 0);
            customLabel1.Location = new Point(540, 5);
            customLabel1.Margin = new Padding(0);
            customLabel1.Name = "customLabel1";
            customLabel1.Size = new Size(66, 22);
            customLabel1.TabIndex = 1;
            customLabel1.Text = "№ пачки";
            customLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbNomZad
            // 
            tbNomZad.BackColor = Color.FromArgb(248, 248, 255);
            tbNomZad.Font = new Font("Arial", 10F);
            tbNomZad.ForeColor = Color.FromArgb(72, 61, 139);
            tbNomZad.Location = new Point(931, 5);
            tbNomZad.Margin = new Padding(0);
            tbNomZad.Name = "tbNomZad";
            tbNomZad.Size = new Size(122, 20);
            tbNomZad.TabIndex = 2;
            tbNomZad.KeyDown += tbNomZad_KeyDown;
            // 
            // customLabel4
            // 
            customLabel4.BackColor = Color.Transparent;
            customLabel4.Font = new Font("Arial", 10F);
            customLabel4.ForeColor = Color.FromArgb(0, 0, 0);
            customLabel4.Location = new Point(840, 5);
            customLabel4.Margin = new Padding(0);
            customLabel4.Name = "customLabel4";
            customLabel4.Size = new Size(87, 22);
            customLabel4.TabIndex = 1;
            customLabel4.Text = "№ задания";
            customLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // customRadioGroup3
            // 
            customRadioGroup3.Location = new Point(341, 2);
            customRadioGroup3.Name = "customRadioGroup3";
            customRadioGroup3.ObjectName = null;
            customRadioGroup3.Properties.Appearance.Font = new Font("Arial", 10F);
            customRadioGroup3.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            customRadioGroup3.Properties.Appearance.Options.UseFont = true;
            customRadioGroup3.Properties.Appearance.Options.UseForeColor = true;
            customRadioGroup3.Properties.Columns = 2;
            customRadioGroup3.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "№ задания", true, null, "ProcessingByTaskNumber"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "№ пачки", true, null, "ProcessingByPachNumber") });
            customRadioGroup3.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            customRadioGroup3.Properties.Padding = new Padding(1, 0, 0, 0);
            customRadioGroup3.Size = new Size(175, 28);
            customRadioGroup3.StyleController = layoutControl3;
            customRadioGroup3.TabIndex = 2;
            customRadioGroup3.SelectedIndexChanged += customRadioGroup3_SelectedIndexChanged;
            customRadioGroup3.EditValueChanged += customRadioGroup3_EditValueChanged;
            // 
            // layoutControl3
            // 
            layoutControl3.Controls.Add(tbNomZad);
            layoutControl3.Controls.Add(customLabel2);
            layoutControl3.Controls.Add(customLabel4);
            layoutControl3.Controls.Add(customRadioGroup2);
            layoutControl3.Controls.Add(tbYearPach);
            layoutControl3.Controls.Add(customRadioGroup3);
            layoutControl3.Controls.Add(label4);
            layoutControl3.Controls.Add(customLabel1);
            layoutControl3.Controls.Add(tbNomPach);
            layoutControl3.Location = new Point(2, 3);
            layoutControl3.Name = "layoutControl3";
            layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(824, 283, 650, 400);
            layoutControl3.Root = layoutControlGroup8;
            layoutControl3.Size = new Size(1058, 32);
            layoutControl3.TabIndex = 13;
            layoutControl3.Text = "layoutControl3";
            // 
            // customRadioGroup2
            // 
            customRadioGroup2.Location = new Point(115, 2);
            customRadioGroup2.Name = "customRadioGroup2";
            customRadioGroup2.ObjectName = null;
            customRadioGroup2.Properties.Appearance.Font = new Font("Arial", 10F);
            customRadioGroup2.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            customRadioGroup2.Properties.Appearance.Options.UseFont = true;
            customRadioGroup2.Properties.Appearance.Options.UseForeColor = true;
            customRadioGroup2.Properties.Columns = 4;
            customRadioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ШП"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ВЗП"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Носки"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ШПМ") });
            customRadioGroup2.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            customRadioGroup2.Properties.Padding = new Padding(1, 0, 0, 0);
            customRadioGroup2.Size = new Size(222, 28);
            customRadioGroup2.StyleController = layoutControl3;
            customRadioGroup2.TabIndex = 0;
            customRadioGroup2.SelectedIndexChanged += customRadioGroup2_SelectedIndexChanged;
            // 
            // layoutControlGroup8
            // 
            layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup8.GroupBordersVisible = false;
            layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3, emptySpaceItem1, emptySpaceItem3, layoutControlGroup1, layoutControlGroup2, layoutControlItem1 });
            layoutControlGroup8.Name = "Root";
            layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup8.Size = new Size(1058, 32);
            layoutControlGroup8.TextLocation = DevExpress.Utils.Locations.Left;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = customRadioGroup2;
            layoutControlItem2.Location = new Point(113, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(226, 32);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = customRadioGroup3;
            layoutControlItem3.Location = new Point(339, 0);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(179, 32);
            layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new Point(518, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(17, 32);
            // 
            // emptySpaceItem3
            // 
            emptySpaceItem3.Location = new Point(790, 0);
            emptySpaceItem3.Name = "emptySpaceItem3";
            emptySpaceItem3.Size = new Size(45, 32);
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4, layoutControlItem5, emptySpaceItem2, layoutControlItem6, layoutControlItem7 });
            layoutControlGroup1.Location = new Point(535, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup1.Size = new Size(255, 32);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = customLabel1;
            layoutControlItem4.Location = new Point(0, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(70, 26);
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = tbNomPach;
            layoutControlItem5.Location = new Point(70, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(85, 26);
            layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.Location = new Point(155, 0);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new Size(11, 26);
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = label4;
            layoutControlItem6.Location = new Point(166, 0);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new Size(33, 26);
            layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = tbYearPach;
            layoutControlItem7.Location = new Point(199, 0);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new Size(50, 26);
            layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem8, layoutControlItem9 });
            layoutControlGroup2.Location = new Point(835, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup2.Size = new Size(223, 32);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = customLabel4;
            layoutControlItem8.Location = new Point(0, 0);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.Size = new Size(91, 26);
            layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = tbNomZad;
            layoutControlItem9.Location = new Point(91, 0);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new Size(126, 26);
            layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customLabel2;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(113, 32);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Location = new Point(0, 0);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Size = new Size(1840, 132);
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(pbEskiz);
            layoutControl1.Controls.Add(tbSostPoln);
            layoutControl1.Controls.Add(tbRzuMod);
            layoutControl1.Controls.Add(label13);
            layoutControl1.Controls.Add(tbArtGrup);
            layoutControl1.Controls.Add(tbPsaKombOsn);
            layoutControl1.Controls.Add(label10);
            layoutControl1.Controls.Add(tbPsaPrn);
            layoutControl1.Controls.Add(label67);
            layoutControl1.Controls.Add(tbPsaKodZv2);
            layoutControl1.Controls.Add(label15);
            layoutControl1.Controls.Add(tbPsaPsaIDOsn);
            layoutControl1.Controls.Add(tbPsaKodZv1);
            layoutControl1.Controls.Add(label22);
            layoutControl1.Controls.Add(tbPsaKombIzd);
            layoutControl1.Controls.Add(label23);
            layoutControl1.Controls.Add(label66);
            layoutControl1.Controls.Add(label8);
            layoutControl1.Controls.Add(label65);
            layoutControl1.Controls.Add(label9);
            layoutControl1.Controls.Add(tbPsaPsaID);
            layoutControl1.Controls.Add(label5);
            layoutControl1.Controls.Add(label62);
            layoutControl1.Controls.Add(tbArtTradeMark);
            layoutControl1.Controls.Add(tbPsaNameSbit);
            layoutControl1.Controls.Add(tbRzuNom);
            layoutControl1.Controls.Add(label68);
            layoutControl1.Controls.Add(cbIsChip);
            layoutControl1.Controls.Add(tbRzuArticul);
            layoutControl1.Controls.Add(label19);
            layoutControl1.Controls.Add(label11);
            layoutControl1.Controls.Add(tbPsaMenName);
            layoutControl1.Controls.Add(tbPsaTbID);
            layoutControl1.Controls.Add(psaSezName);
            layoutControl1.Controls.Add(tbPsaYear);
            layoutControl1.Controls.Add(label20);
            layoutControl1.Controls.Add(label16);
            layoutControl1.Controls.Add(label7);
            layoutControl1.Controls.Add(tbRzuKol);
            layoutControl1.Controls.Add(label17);
            layoutControl1.Controls.Add(label53);
            layoutControl1.Controls.Add(tbPsaNomZad);
            layoutControl1.Controls.Add(label21);
            layoutControl1.Controls.Add(label12);
            layoutControl1.Controls.Add(tbPsaNN);
            layoutControl1.Controls.Add(tbRzuDostZeh);
            layoutControl1.Controls.Add(label6);
            layoutControl1.Controls.Add(tbRzuPach);
            layoutControl1.Location = new Point(2, 37);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(820, 315, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(1831, 170);
            layoutControl1.TabIndex = 15;
            layoutControl1.Text = "layoutControl1";
            // 
            // pbEskiz
            // 
            pbEskiz.Location = new Point(1711, 59);
            pbEskiz.Margin = new Padding(4, 3, 4, 3);
            pbEskiz.Name = "pbEskiz";
            pbEskiz.Size = new Size(103, 94);
            pbEskiz.SizeMode = PictureBoxSizeMode.Zoom;
            pbEskiz.TabIndex = 1;
            pbEskiz.TabStop = false;
            // 
            // tbSostPoln
            // 
            tbSostPoln.BackColor = Color.FromArgb(230, 245, 255);
            tbSostPoln.Font = new Font("Arial", 10F);
            tbSostPoln.ForeColor = Color.FromArgb(50, 90, 160);
            tbSostPoln.Location = new Point(888, 59);
            tbSostPoln.Multiline = true;
            tbSostPoln.Name = "tbSostPoln";
            tbSostPoln.Size = new Size(469, 25);
            tbSostPoln.TabIndex = 38;
            // 
            // tbRzuMod
            // 
            tbRzuMod.BackColor = Color.FromArgb(248, 248, 255);
            tbRzuMod.Font = new Font("Arial", 10F);
            tbRzuMod.ForeColor = Color.FromArgb(72, 61, 139);
            tbRzuMod.Location = new Point(888, 88);
            tbRzuMod.Margin = new Padding(0);
            tbRzuMod.Name = "tbRzuMod";
            tbRzuMod.Size = new Size(469, 65);
            tbRzuMod.TabIndex = 19;
            // 
            // label13
            // 
            label13.BackColor = Color.Transparent;
            label13.Font = new Font("Arial", 10F);
            label13.ForeColor = Color.FromArgb(0, 0, 0);
            label13.Location = new Point(864, 59);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(20, 25);
            label13.TabIndex = 24;
            label13.Text = "Состав\r\n(из справ.)";
            // 
            // tbArtGrup
            // 
            tbArtGrup.BackColor = Color.FromArgb(248, 248, 255);
            tbArtGrup.Font = new Font("Arial", 10F);
            tbArtGrup.ForeColor = Color.FromArgb(72, 61, 139);
            tbArtGrup.Location = new Point(440, 131);
            tbArtGrup.Margin = new Padding(0);
            tbArtGrup.Name = "tbArtGrup";
            tbArtGrup.Size = new Size(194, 20);
            tbArtGrup.TabIndex = 21;
            // 
            // tbPsaKombOsn
            // 
            tbPsaKombOsn.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaKombOsn.Font = new Font("Arial", 10F);
            tbPsaKombOsn.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaKombOsn.Location = new Point(1686, 107);
            tbPsaKombOsn.Margin = new Padding(0);
            tbPsaKombOsn.Name = "tbPsaKombOsn";
            tbPsaKombOsn.Size = new Size(21, 20);
            tbPsaKombOsn.TabIndex = 19;
            // 
            // label10
            // 
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial", 10F);
            label10.ForeColor = Color.FromArgb(0, 0, 0);
            label10.Location = new Point(864, 88);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(20, 65);
            label10.TabIndex = 20;
            label10.Text = "Модель (торг.)";
            // 
            // tbPsaPrn
            // 
            tbPsaPrn.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaPrn.Font = new Font("Arial", 10F);
            tbPsaPrn.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaPrn.Location = new Point(440, 59);
            tbPsaPrn.Margin = new Padding(0);
            tbPsaPrn.Multiline = true;
            tbPsaPrn.Name = "tbPsaPrn";
            tbPsaPrn.Size = new Size(194, 20);
            tbPsaPrn.TabIndex = 6;
            // 
            // label67
            // 
            label67.BackColor = Color.Transparent;
            label67.Font = new Font("Arial", 10F);
            label67.ForeColor = Color.FromArgb(0, 0, 0);
            label67.Location = new Point(1640, 107);
            label67.Margin = new Padding(4, 0, 4, 0);
            label67.Name = "label67";
            label67.Size = new Size(42, 20);
            label67.TabIndex = 37;
            label67.Text = "komb_osn";
            // 
            // tbPsaKodZv2
            // 
            tbPsaKodZv2.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaKodZv2.Font = new Font("Arial", 10F);
            tbPsaKodZv2.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaKodZv2.Location = new Point(554, 83);
            tbPsaKodZv2.Margin = new Padding(0);
            tbPsaKodZv2.Name = "tbPsaKodZv2";
            tbPsaKodZv2.Size = new Size(80, 20);
            tbPsaKodZv2.TabIndex = 8;
            // 
            // label15
            // 
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Arial", 10F);
            label15.ForeColor = Color.FromArgb(0, 0, 0);
            label15.Location = new Point(253, 131);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(183, 22);
            label15.TabIndex = 22;
            label15.Text = "Наименование (из справ.)";
            // 
            // tbPsaPsaIDOsn
            // 
            tbPsaPsaIDOsn.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaPsaIDOsn.Font = new Font("Arial", 10F);
            tbPsaPsaIDOsn.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaPsaIDOsn.Location = new Point(1687, 83);
            tbPsaPsaIDOsn.Margin = new Padding(0);
            tbPsaPsaIDOsn.Name = "tbPsaPsaIDOsn";
            tbPsaPsaIDOsn.Size = new Size(20, 20);
            tbPsaPsaIDOsn.TabIndex = 16;
            // 
            // tbPsaKodZv1
            // 
            tbPsaKodZv1.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaKodZv1.Font = new Font("Arial", 10F);
            tbPsaKodZv1.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaKodZv1.Location = new Point(391, 83);
            tbPsaKodZv1.Margin = new Padding(0);
            tbPsaKodZv1.Name = "tbPsaKodZv1";
            tbPsaKodZv1.Size = new Size(80, 20);
            tbPsaKodZv1.TabIndex = 7;
            // 
            // label22
            // 
            label22.BackColor = Color.Transparent;
            label22.Font = new Font("Arial", 10F);
            label22.ForeColor = Color.FromArgb(0, 0, 0);
            label22.Location = new Point(485, 83);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(65, 20);
            label22.TabIndex = 1;
            label22.Text = "Код цв.2";
            // 
            // tbPsaKombIzd
            // 
            tbPsaKombIzd.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaKombIzd.Font = new Font("Arial", 10F);
            tbPsaKombIzd.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaKombIzd.Location = new Point(1562, 107);
            tbPsaKombIzd.Margin = new Padding(0);
            tbPsaKombIzd.Name = "tbPsaKombIzd";
            tbPsaKombIzd.Size = new Size(51, 20);
            tbPsaKombIzd.TabIndex = 18;
            // 
            // label23
            // 
            label23.BackColor = Color.Transparent;
            label23.Font = new Font("Arial", 10F);
            label23.ForeColor = Color.FromArgb(0, 0, 0);
            label23.Location = new Point(321, 83);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(66, 20);
            label23.TabIndex = 1;
            label23.Text = "Код цв.1";
            // 
            // label66
            // 
            label66.BackColor = Color.Transparent;
            label66.Font = new Font("Arial", 10F);
            label66.ForeColor = Color.FromArgb(0, 0, 0);
            label66.Location = new Point(1506, 107);
            label66.Margin = new Padding(4, 0, 4, 0);
            label66.Name = "label66";
            label66.Size = new Size(52, 20);
            label66.TabIndex = 1;
            label66.Text = "komb_izd";
            // 
            // label8
            // 
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial", 10F);
            label8.ForeColor = Color.FromArgb(0, 0, 0);
            label8.Location = new Point(391, 59);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(45, 20);
            label8.TabIndex = 1;
            label8.Text = "Цвет";
            // 
            // label65
            // 
            label65.BackColor = Color.Transparent;
            label65.Font = new Font("Arial", 10F);
            label65.ForeColor = Color.FromArgb(0, 0, 0);
            label65.Location = new Point(1647, 83);
            label65.Margin = new Padding(4, 0, 4, 0);
            label65.Name = "label65";
            label65.Size = new Size(36, 20);
            label65.TabIndex = 1;
            label65.Text = "psa_id_osn";
            // 
            // label9
            // 
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Arial", 10F);
            label9.ForeColor = Color.FromArgb(0, 0, 0);
            label9.Location = new Point(253, 107);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(134, 20);
            label9.TabIndex = 1;
            label9.Text = "Артикул (шв.)";
            // 
            // tbPsaPsaID
            // 
            tbPsaPsaID.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaPsaID.Font = new Font("Arial", 10F);
            tbPsaPsaID.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaPsaID.Location = new Point(1558, 83);
            tbPsaPsaID.Margin = new Padding(0);
            tbPsaPsaID.Name = "tbPsaPsaID";
            tbPsaPsaID.Size = new Size(62, 20);
            tbPsaPsaID.TabIndex = 15;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 10F);
            label5.ForeColor = Color.FromArgb(0, 0, 0);
            label5.Location = new Point(17, 59);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(25, 20);
            label5.TabIndex = 1;
            label5.Text = "№";
            // 
            // label62
            // 
            label62.BackColor = Color.Transparent;
            label62.Font = new Font("Arial", 10F);
            label62.ForeColor = Color.FromArgb(0, 0, 0);
            label62.Location = new Point(1506, 83);
            label62.Margin = new Padding(4, 0, 4, 0);
            label62.Name = "label62";
            label62.Size = new Size(48, 20);
            label62.TabIndex = 1;
            label62.Text = "psa_id";
            // 
            // tbArtTradeMark
            // 
            tbArtTradeMark.BackColor = Color.FromArgb(248, 248, 255);
            tbArtTradeMark.Font = new Font("Arial", 10F);
            tbArtTradeMark.ForeColor = Color.FromArgb(72, 61, 139);
            tbArtTradeMark.Location = new Point(1435, 131);
            tbArtTradeMark.Margin = new Padding(0);
            tbArtTradeMark.Name = "tbArtTradeMark";
            tbArtTradeMark.Size = new Size(46, 20);
            tbArtTradeMark.TabIndex = 20;
            // 
            // tbPsaNameSbit
            // 
            tbPsaNameSbit.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaNameSbit.Font = new Font("Arial", 10F);
            tbPsaNameSbit.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaNameSbit.Location = new Point(1435, 107);
            tbPsaNameSbit.Margin = new Padding(0);
            tbPsaNameSbit.Name = "tbPsaNameSbit";
            tbPsaNameSbit.Size = new Size(46, 20);
            tbPsaNameSbit.TabIndex = 17;
            // 
            // tbRzuNom
            // 
            tbRzuNom.BackColor = Color.FromArgb(248, 248, 255);
            tbRzuNom.Font = new Font("Arial", 10F);
            tbRzuNom.ForeColor = Color.FromArgb(72, 61, 139);
            tbRzuNom.Location = new Point(46, 59);
            tbRzuNom.Margin = new Padding(0);
            tbRzuNom.Name = "tbRzuNom";
            tbRzuNom.Size = new Size(61, 20);
            tbRzuNom.TabIndex = 0;
            // 
            // label68
            // 
            label68.BackColor = Color.Transparent;
            label68.Font = new Font("Arial", 10F);
            label68.ForeColor = Color.FromArgb(0, 0, 0);
            label68.Location = new Point(1385, 131);
            label68.Margin = new Padding(4, 0, 4, 0);
            label68.Name = "label68";
            label68.Size = new Size(46, 22);
            label68.TabIndex = 1;
            label68.Text = "Торговая марка";
            // 
            // cbIsChip
            // 
            cbIsChip.Enabled = false;
            cbIsChip.Font = new Font("Arial", 10F);
            cbIsChip.ForeColor = Color.FromArgb(120, 60, 30);
            cbIsChip.Location = new Point(1686, 59);
            cbIsChip.Margin = new Padding(4, 3, 4, 3);
            cbIsChip.Name = "cbIsChip";
            cbIsChip.Size = new Size(21, 20);
            cbIsChip.TabIndex = 13;
            cbIsChip.Text = "Чип";
            cbIsChip.UseVisualStyleBackColor = true;
            // 
            // tbRzuArticul
            // 
            tbRzuArticul.BackColor = Color.FromArgb(248, 248, 255);
            tbRzuArticul.Font = new Font("Arial", 10F);
            tbRzuArticul.ForeColor = Color.FromArgb(72, 61, 139);
            tbRzuArticul.Location = new Point(391, 107);
            tbRzuArticul.Margin = new Padding(0);
            tbRzuArticul.Name = "tbRzuArticul";
            tbRzuArticul.Size = new Size(243, 20);
            tbRzuArticul.TabIndex = 9;
            // 
            // label19
            // 
            label19.BackColor = Color.Transparent;
            label19.Font = new Font("Arial", 10F);
            label19.ForeColor = Color.FromArgb(0, 0, 0);
            label19.Location = new Point(1385, 83);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(46, 20);
            label19.TabIndex = 1;
            label19.Text = "Блок";
            // 
            // label11
            // 
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Arial", 10F);
            label11.ForeColor = Color.FromArgb(0, 0, 0);
            label11.Location = new Point(1385, 107);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(46, 20);
            label11.TabIndex = 1;
            label11.Text = "Канал сбыта";
            // 
            // tbPsaMenName
            // 
            tbPsaMenName.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaMenName.Font = new Font("Arial", 10F);
            tbPsaMenName.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaMenName.Location = new Point(1638, 131);
            tbPsaMenName.Margin = new Padding(0);
            tbPsaMenName.Multiline = true;
            tbPsaMenName.Name = "tbPsaMenName";
            tbPsaMenName.Size = new Size(69, 20);
            tbPsaMenName.TabIndex = 21;
            // 
            // tbPsaTbID
            // 
            tbPsaTbID.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaTbID.Font = new Font("Arial", 10F);
            tbPsaTbID.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaTbID.Location = new Point(1435, 83);
            tbPsaTbID.Margin = new Padding(0);
            tbPsaTbID.Name = "tbPsaTbID";
            tbPsaTbID.Size = new Size(46, 20);
            tbPsaTbID.TabIndex = 14;
            // 
            // psaSezName
            // 
            psaSezName.BackColor = Color.FromArgb(248, 248, 255);
            psaSezName.Font = new Font("Arial", 10F);
            psaSezName.ForeColor = Color.FromArgb(72, 61, 139);
            psaSezName.Location = new Point(1652, 59);
            psaSezName.Margin = new Padding(0);
            psaSezName.Name = "psaSezName";
            psaSezName.Size = new Size(20, 20);
            psaSezName.TabIndex = 12;
            // 
            // tbPsaYear
            // 
            tbPsaYear.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaYear.Font = new Font("Arial", 10F);
            tbPsaYear.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaYear.Location = new Point(1558, 59);
            tbPsaYear.Margin = new Padding(0);
            tbPsaYear.Name = "tbPsaYear";
            tbPsaYear.Size = new Size(46, 20);
            tbPsaYear.TabIndex = 11;
            // 
            // label20
            // 
            label20.BackColor = Color.Transparent;
            label20.Font = new Font("Arial", 10F);
            label20.ForeColor = Color.FromArgb(0, 0, 0);
            label20.Location = new Point(1506, 131);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(128, 22);
            label20.TabIndex = 1;
            label20.Text = "Категория (менеджер)";
            // 
            // label16
            // 
            label16.BackColor = Color.Transparent;
            label16.Font = new Font("Arial", 10F);
            label16.ForeColor = Color.FromArgb(0, 0, 0);
            label16.Location = new Point(1627, 59);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(21, 20);
            label16.TabIndex = 1;
            label16.Text = "Сезон";
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial", 10F);
            label7.ForeColor = Color.FromArgb(0, 0, 0);
            label7.Location = new Point(121, 59);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(54, 20);
            label7.TabIndex = 1;
            label7.Text = "Кол-во";
            // 
            // tbRzuKol
            // 
            tbRzuKol.BackColor = Color.FromArgb(248, 248, 255);
            tbRzuKol.Font = new Font("Arial", 10F);
            tbRzuKol.ForeColor = Color.FromArgb(72, 61, 139);
            tbRzuKol.Location = new Point(179, 59);
            tbRzuKol.Margin = new Padding(0);
            tbRzuKol.Name = "tbRzuKol";
            tbRzuKol.Size = new Size(46, 20);
            tbRzuKol.TabIndex = 2;
            // 
            // label17
            // 
            label17.BackColor = Color.Transparent;
            label17.Font = new Font("Arial", 10F);
            label17.ForeColor = Color.FromArgb(0, 0, 0);
            label17.Location = new Point(1506, 59);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(48, 20);
            label17.TabIndex = 1;
            label17.Text = "Год";
            // 
            // label53
            // 
            label53.BackColor = Color.Transparent;
            label53.Font = new Font("Arial", 10F);
            label53.ForeColor = Color.FromArgb(0, 0, 0);
            label53.Location = new Point(253, 59);
            label53.Margin = new Padding(4, 0, 4, 0);
            label53.Name = "label53";
            label53.Size = new Size(28, 20);
            label53.TabIndex = 1;
            label53.Text = "№";
            // 
            // tbPsaNomZad
            // 
            tbPsaNomZad.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaNomZad.Font = new Font("Arial", 10F);
            tbPsaNomZad.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaNomZad.Location = new Point(285, 59);
            tbPsaNomZad.Margin = new Padding(0);
            tbPsaNomZad.Name = "tbPsaNomZad";
            tbPsaNomZad.Size = new Size(92, 20);
            tbPsaNomZad.TabIndex = 5;
            // 
            // label21
            // 
            label21.BackColor = Color.Transparent;
            label21.Font = new Font("Arial", 10F);
            label21.ForeColor = Color.FromArgb(0, 0, 0);
            label21.Location = new Point(1385, 59);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(46, 20);
            label21.TabIndex = 1;
            label21.Text = "Код";
            // 
            // label12
            // 
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Arial", 10F);
            label12.ForeColor = Color.FromArgb(0, 0, 0);
            label12.Location = new Point(17, 83);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(64, 20);
            label12.TabIndex = 1;
            label12.Text = "Бригада №";
            // 
            // tbPsaNN
            // 
            tbPsaNN.BackColor = Color.FromArgb(248, 248, 255);
            tbPsaNN.Font = new Font("Arial", 10F);
            tbPsaNN.ForeColor = Color.FromArgb(72, 61, 139);
            tbPsaNN.Location = new Point(1435, 59);
            tbPsaNN.Margin = new Padding(0);
            tbPsaNN.Name = "tbPsaNN";
            tbPsaNN.Size = new Size(46, 20);
            tbPsaNN.TabIndex = 10;
            // 
            // tbRzuDostZeh
            // 
            tbRzuDostZeh.BackColor = Color.FromArgb(248, 248, 255);
            tbRzuDostZeh.Font = new Font("Arial", 10F);
            tbRzuDostZeh.ForeColor = Color.FromArgb(72, 61, 139);
            tbRzuDostZeh.Location = new Point(85, 83);
            tbRzuDostZeh.Margin = new Padding(0);
            tbRzuDostZeh.Name = "tbRzuDostZeh";
            tbRzuDostZeh.Size = new Size(140, 20);
            tbRzuDostZeh.TabIndex = 3;
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial", 10F);
            label6.ForeColor = Color.FromArgb(0, 0, 0);
            label6.Location = new Point(17, 107);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(64, 46);
            label6.TabIndex = 1;
            label6.Text = "№ пачек";
            // 
            // tbRzuPach
            // 
            tbRzuPach.BackColor = Color.FromArgb(248, 248, 255);
            tbRzuPach.Font = new Font("Arial", 10F);
            tbRzuPach.ForeColor = Color.FromArgb(72, 61, 139);
            tbRzuPach.Location = new Point(85, 107);
            tbRzuPach.Margin = new Padding(0);
            tbRzuPach.Name = "tbRzuPach";
            tbRzuPach.Size = new Size(140, 20);
            tbRzuPach.TabIndex = 4;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup12 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            Root.Size = new Size(1831, 170);
            Root.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup9, layoutControlGroup11, layoutControlGroup10 });
            layoutControlGroup12.Location = new Point(0, 0);
            layoutControlGroup12.Name = "layoutControlGroup12";
            layoutControlGroup12.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup12.Size = new Size(1831, 170);
            layoutControlGroup12.Text = "КАРТОЧКА РАСЧЕТА";
            // 
            // layoutControlGroup9
            // 
            layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem15, layoutControlItem17, layoutControlItem23, layoutControlItem46, layoutControlItem49, layoutControlItem50, layoutControlItem51, layoutControlItem52, emptySpaceItem24 });
            layoutControlGroup9.Location = new Point(0, 0);
            layoutControlGroup9.Name = "layoutControlGroup9";
            layoutControlGroup9.Size = new Size(236, 143);
            layoutControlGroup9.Text = "Расчет";
            // 
            // layoutControlItem15
            // 
            layoutControlItem15.Control = label5;
            layoutControlItem15.Location = new Point(0, 0);
            layoutControlItem15.Name = "layoutControlItem15";
            layoutControlItem15.Size = new Size(29, 24);
            layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            layoutControlItem17.Control = tbRzuNom;
            layoutControlItem17.Location = new Point(29, 0);
            layoutControlItem17.Name = "layoutControlItem17";
            layoutControlItem17.Size = new Size(65, 24);
            layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem23
            // 
            layoutControlItem23.Control = label7;
            layoutControlItem23.Location = new Point(104, 0);
            layoutControlItem23.Name = "layoutControlItem23";
            layoutControlItem23.Size = new Size(58, 24);
            layoutControlItem23.TextVisible = false;
            // 
            // layoutControlItem46
            // 
            layoutControlItem46.Control = tbRzuKol;
            layoutControlItem46.Location = new Point(162, 0);
            layoutControlItem46.Name = "layoutControlItem46";
            layoutControlItem46.Size = new Size(50, 24);
            layoutControlItem46.TextVisible = false;
            // 
            // layoutControlItem49
            // 
            layoutControlItem49.Control = label12;
            layoutControlItem49.Location = new Point(0, 24);
            layoutControlItem49.Name = "layoutControlItem49";
            layoutControlItem49.Size = new Size(68, 24);
            layoutControlItem49.TextVisible = false;
            // 
            // layoutControlItem50
            // 
            layoutControlItem50.Control = tbRzuDostZeh;
            layoutControlItem50.Location = new Point(68, 24);
            layoutControlItem50.Name = "layoutControlItem50";
            layoutControlItem50.Size = new Size(144, 24);
            layoutControlItem50.TextVisible = false;
            // 
            // layoutControlItem51
            // 
            layoutControlItem51.Control = label6;
            layoutControlItem51.Location = new Point(0, 48);
            layoutControlItem51.Name = "layoutControlItem51";
            layoutControlItem51.Size = new Size(68, 50);
            layoutControlItem51.TextVisible = false;
            // 
            // layoutControlItem52
            // 
            layoutControlItem52.Control = tbRzuPach;
            layoutControlItem52.Location = new Point(68, 48);
            layoutControlItem52.Name = "layoutControlItem52";
            layoutControlItem52.Size = new Size(144, 50);
            layoutControlItem52.TextVisible = false;
            // 
            // emptySpaceItem24
            // 
            emptySpaceItem24.Location = new Point(94, 0);
            emptySpaceItem24.Name = "emptySpaceItem24";
            emptySpaceItem24.Size = new Size(10, 24);
            // 
            // layoutControlGroup11
            // 
            layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem53, layoutControlItem54, layoutControlItem20, layoutControlItem21, layoutControlItem55, layoutControlItem56, layoutControlItem57, layoutControlItem58, layoutControlItem59, emptySpaceItem15, emptySpaceItem17, emptySpaceItem18, emptySpaceItem19, layoutControlItem62, layoutControlItem63, emptySpaceItem20, layoutControlItem64, layoutControlItem65, emptySpaceItem21, layoutControlItem66, layoutControlItem67, layoutControlItem68, layoutControlItem69, emptySpaceItem16, layoutControlItem70, layoutControlItem71, layoutControlItem72, layoutControlItem73, emptySpaceItem22, layoutControlItem60, layoutControlItem61, layoutControlItem22 });
            layoutControlGroup11.Location = new Point(1368, 0);
            layoutControlGroup11.Name = "layoutControlGroup11";
            layoutControlGroup11.Size = new Size(457, 143);
            layoutControlGroup11.Text = "Матрица";
            // 
            // layoutControlItem53
            // 
            layoutControlItem53.Control = label21;
            layoutControlItem53.Location = new Point(0, 0);
            layoutControlItem53.Name = "layoutControlItem53";
            layoutControlItem53.Size = new Size(50, 24);
            layoutControlItem53.TextVisible = false;
            // 
            // layoutControlItem54
            // 
            layoutControlItem54.Control = tbPsaNN;
            layoutControlItem54.Location = new Point(50, 0);
            layoutControlItem54.Name = "layoutControlItem54";
            layoutControlItem54.Size = new Size(50, 24);
            layoutControlItem54.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            layoutControlItem20.Control = label19;
            layoutControlItem20.Location = new Point(0, 24);
            layoutControlItem20.Name = "layoutControlItem20";
            layoutControlItem20.Size = new Size(50, 24);
            layoutControlItem20.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            layoutControlItem21.Control = tbPsaTbID;
            layoutControlItem21.Location = new Point(50, 24);
            layoutControlItem21.Name = "layoutControlItem21";
            layoutControlItem21.Size = new Size(50, 24);
            layoutControlItem21.TextVisible = false;
            // 
            // layoutControlItem55
            // 
            layoutControlItem55.Control = label17;
            layoutControlItem55.Location = new Point(121, 0);
            layoutControlItem55.Name = "layoutControlItem55";
            layoutControlItem55.Size = new Size(52, 24);
            layoutControlItem55.TextVisible = false;
            // 
            // layoutControlItem56
            // 
            layoutControlItem56.Control = tbPsaYear;
            layoutControlItem56.Location = new Point(173, 0);
            layoutControlItem56.Name = "layoutControlItem56";
            layoutControlItem56.Size = new Size(50, 24);
            layoutControlItem56.TextVisible = false;
            // 
            // layoutControlItem57
            // 
            layoutControlItem57.Control = label16;
            layoutControlItem57.Location = new Point(242, 0);
            layoutControlItem57.Name = "layoutControlItem57";
            layoutControlItem57.Size = new Size(25, 24);
            layoutControlItem57.TextVisible = false;
            // 
            // layoutControlItem58
            // 
            layoutControlItem58.Control = psaSezName;
            layoutControlItem58.Location = new Point(267, 0);
            layoutControlItem58.Name = "layoutControlItem58";
            layoutControlItem58.Size = new Size(24, 24);
            layoutControlItem58.TextVisible = false;
            // 
            // layoutControlItem59
            // 
            layoutControlItem59.Control = cbIsChip;
            layoutControlItem59.Location = new Point(301, 0);
            layoutControlItem59.Name = "layoutControlItem59";
            layoutControlItem59.Size = new Size(25, 24);
            layoutControlItem59.TextVisible = false;
            // 
            // emptySpaceItem15
            // 
            emptySpaceItem15.Location = new Point(100, 24);
            emptySpaceItem15.Name = "emptySpaceItem15";
            emptySpaceItem15.Size = new Size(21, 24);
            // 
            // emptySpaceItem17
            // 
            emptySpaceItem17.Location = new Point(100, 0);
            emptySpaceItem17.Name = "emptySpaceItem17";
            emptySpaceItem17.Size = new Size(21, 24);
            // 
            // emptySpaceItem18
            // 
            emptySpaceItem18.Location = new Point(223, 0);
            emptySpaceItem18.Name = "emptySpaceItem18";
            emptySpaceItem18.Size = new Size(19, 24);
            // 
            // emptySpaceItem19
            // 
            emptySpaceItem19.Location = new Point(291, 0);
            emptySpaceItem19.Name = "emptySpaceItem19";
            emptySpaceItem19.Size = new Size(10, 24);
            // 
            // layoutControlItem62
            // 
            layoutControlItem62.Control = label11;
            layoutControlItem62.Location = new Point(0, 48);
            layoutControlItem62.Name = "layoutControlItem62";
            layoutControlItem62.Size = new Size(50, 24);
            layoutControlItem62.TextVisible = false;
            // 
            // layoutControlItem63
            // 
            layoutControlItem63.Control = tbPsaNameSbit;
            layoutControlItem63.Location = new Point(50, 48);
            layoutControlItem63.Name = "layoutControlItem63";
            layoutControlItem63.Size = new Size(50, 24);
            layoutControlItem63.TextVisible = false;
            // 
            // emptySpaceItem20
            // 
            emptySpaceItem20.Location = new Point(100, 48);
            emptySpaceItem20.Name = "emptySpaceItem20";
            emptySpaceItem20.Size = new Size(21, 24);
            // 
            // layoutControlItem64
            // 
            layoutControlItem64.Control = label68;
            layoutControlItem64.Location = new Point(0, 72);
            layoutControlItem64.Name = "layoutControlItem64";
            layoutControlItem64.Size = new Size(50, 26);
            layoutControlItem64.TextVisible = false;
            // 
            // layoutControlItem65
            // 
            layoutControlItem65.Control = tbArtTradeMark;
            layoutControlItem65.Location = new Point(50, 72);
            layoutControlItem65.Name = "layoutControlItem65";
            layoutControlItem65.Size = new Size(50, 26);
            layoutControlItem65.TextVisible = false;
            // 
            // emptySpaceItem21
            // 
            emptySpaceItem21.Location = new Point(100, 72);
            emptySpaceItem21.Name = "emptySpaceItem21";
            emptySpaceItem21.Size = new Size(21, 26);
            // 
            // layoutControlItem66
            // 
            layoutControlItem66.Control = label62;
            layoutControlItem66.Location = new Point(121, 24);
            layoutControlItem66.Name = "layoutControlItem66";
            layoutControlItem66.Size = new Size(52, 24);
            layoutControlItem66.TextVisible = false;
            // 
            // layoutControlItem67
            // 
            layoutControlItem67.Control = tbPsaPsaID;
            layoutControlItem67.Location = new Point(173, 24);
            layoutControlItem67.Name = "layoutControlItem67";
            layoutControlItem67.Size = new Size(66, 24);
            layoutControlItem67.TextVisible = false;
            // 
            // layoutControlItem68
            // 
            layoutControlItem68.Control = label65;
            layoutControlItem68.Location = new Point(262, 24);
            layoutControlItem68.Name = "layoutControlItem68";
            layoutControlItem68.Size = new Size(40, 24);
            layoutControlItem68.TextVisible = false;
            // 
            // layoutControlItem69
            // 
            layoutControlItem69.Control = tbPsaPsaIDOsn;
            layoutControlItem69.Location = new Point(302, 24);
            layoutControlItem69.Name = "layoutControlItem69";
            layoutControlItem69.Size = new Size(24, 24);
            layoutControlItem69.TextVisible = false;
            // 
            // emptySpaceItem16
            // 
            emptySpaceItem16.Location = new Point(239, 24);
            emptySpaceItem16.Name = "emptySpaceItem16";
            emptySpaceItem16.Size = new Size(23, 24);
            // 
            // layoutControlItem70
            // 
            layoutControlItem70.Control = label66;
            layoutControlItem70.Location = new Point(121, 48);
            layoutControlItem70.Name = "layoutControlItem70";
            layoutControlItem70.Size = new Size(56, 24);
            layoutControlItem70.TextVisible = false;
            // 
            // layoutControlItem71
            // 
            layoutControlItem71.Control = tbPsaKombIzd;
            layoutControlItem71.Location = new Point(177, 48);
            layoutControlItem71.Name = "layoutControlItem71";
            layoutControlItem71.Size = new Size(55, 24);
            layoutControlItem71.TextVisible = false;
            // 
            // layoutControlItem72
            // 
            layoutControlItem72.Control = label67;
            layoutControlItem72.Location = new Point(255, 48);
            layoutControlItem72.Name = "layoutControlItem72";
            layoutControlItem72.Size = new Size(46, 24);
            layoutControlItem72.TextVisible = false;
            // 
            // layoutControlItem73
            // 
            layoutControlItem73.Control = tbPsaKombOsn;
            layoutControlItem73.Location = new Point(301, 48);
            layoutControlItem73.Name = "layoutControlItem73";
            layoutControlItem73.Size = new Size(25, 24);
            layoutControlItem73.TextVisible = false;
            // 
            // emptySpaceItem22
            // 
            emptySpaceItem22.Location = new Point(232, 48);
            emptySpaceItem22.Name = "emptySpaceItem22";
            emptySpaceItem22.Size = new Size(23, 24);
            // 
            // layoutControlItem60
            // 
            layoutControlItem60.Control = label20;
            layoutControlItem60.Location = new Point(121, 72);
            layoutControlItem60.Name = "layoutControlItem60";
            layoutControlItem60.Size = new Size(132, 26);
            layoutControlItem60.TextVisible = false;
            // 
            // layoutControlItem61
            // 
            layoutControlItem61.Control = tbPsaMenName;
            layoutControlItem61.Location = new Point(253, 72);
            layoutControlItem61.Name = "layoutControlItem61";
            layoutControlItem61.Size = new Size(73, 26);
            layoutControlItem61.TextVisible = false;
            // 
            // layoutControlItem22
            // 
            layoutControlItem22.Control = pbEskiz;
            layoutControlItem22.Location = new Point(326, 0);
            layoutControlItem22.Name = "layoutControlItem22";
            layoutControlItem22.Size = new Size(107, 98);
            layoutControlItem22.TextVisible = false;
            // 
            // layoutControlGroup10
            // 
            layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem47, layoutControlItem48, layoutControlItem16, layoutControlItem18, layoutControlItem74, layoutControlItem75, emptySpaceItem23, layoutControlItem76, layoutControlItem77, layoutControlItem78, layoutControlItem79, layoutControlItem80, layoutControlItem81, layoutControlItem82, layoutControlItem83, layoutControlItem84, layoutControlItem85, emptySpaceItem25, emptySpaceItem26, emptySpaceItem27 });
            layoutControlGroup10.Location = new Point(236, 0);
            layoutControlGroup10.Name = "layoutControlGroup10";
            layoutControlGroup10.Size = new Size(1132, 143);
            layoutControlGroup10.Text = "Задание";
            // 
            // layoutControlItem47
            // 
            layoutControlItem47.Control = label53;
            layoutControlItem47.Location = new Point(0, 0);
            layoutControlItem47.Name = "layoutControlItem47";
            layoutControlItem47.Size = new Size(32, 24);
            layoutControlItem47.TextVisible = false;
            // 
            // layoutControlItem48
            // 
            layoutControlItem48.Control = tbPsaNomZad;
            layoutControlItem48.Location = new Point(32, 0);
            layoutControlItem48.Name = "layoutControlItem48";
            layoutControlItem48.Size = new Size(96, 24);
            layoutControlItem48.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            layoutControlItem16.Control = label9;
            layoutControlItem16.Location = new Point(0, 48);
            layoutControlItem16.Name = "layoutControlItem16";
            layoutControlItem16.Size = new Size(138, 24);
            layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            layoutControlItem18.Control = tbRzuArticul;
            layoutControlItem18.Location = new Point(138, 48);
            layoutControlItem18.Name = "layoutControlItem18";
            layoutControlItem18.Size = new Size(247, 24);
            layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem74
            // 
            layoutControlItem74.Control = label8;
            layoutControlItem74.Location = new Point(138, 0);
            layoutControlItem74.Name = "layoutControlItem74";
            layoutControlItem74.Size = new Size(49, 24);
            layoutControlItem74.TextVisible = false;
            // 
            // layoutControlItem75
            // 
            layoutControlItem75.Control = tbPsaPrn;
            layoutControlItem75.Location = new Point(187, 0);
            layoutControlItem75.Name = "layoutControlItem75";
            layoutControlItem75.Size = new Size(198, 24);
            layoutControlItem75.TextVisible = false;
            // 
            // emptySpaceItem23
            // 
            emptySpaceItem23.Location = new Point(0, 24);
            emptySpaceItem23.Name = "emptySpaceItem23";
            emptySpaceItem23.Size = new Size(68, 24);
            // 
            // layoutControlItem76
            // 
            layoutControlItem76.Control = label23;
            layoutControlItem76.Location = new Point(68, 24);
            layoutControlItem76.Name = "layoutControlItem76";
            layoutControlItem76.Size = new Size(70, 24);
            layoutControlItem76.TextVisible = false;
            // 
            // layoutControlItem77
            // 
            layoutControlItem77.Control = tbPsaKodZv1;
            layoutControlItem77.Location = new Point(138, 24);
            layoutControlItem77.Name = "layoutControlItem77";
            layoutControlItem77.Size = new Size(84, 24);
            layoutControlItem77.TextVisible = false;
            // 
            // layoutControlItem78
            // 
            layoutControlItem78.Control = label22;
            layoutControlItem78.Location = new Point(232, 24);
            layoutControlItem78.Name = "layoutControlItem78";
            layoutControlItem78.Size = new Size(69, 24);
            layoutControlItem78.TextVisible = false;
            // 
            // layoutControlItem79
            // 
            layoutControlItem79.Control = tbPsaKodZv2;
            layoutControlItem79.Location = new Point(301, 24);
            layoutControlItem79.Name = "layoutControlItem79";
            layoutControlItem79.Size = new Size(84, 24);
            layoutControlItem79.TextVisible = false;
            // 
            // layoutControlItem80
            // 
            layoutControlItem80.Control = label15;
            layoutControlItem80.Location = new Point(0, 72);
            layoutControlItem80.Name = "layoutControlItem80";
            layoutControlItem80.Size = new Size(187, 26);
            layoutControlItem80.TextVisible = false;
            // 
            // layoutControlItem81
            // 
            layoutControlItem81.Control = tbArtGrup;
            layoutControlItem81.Location = new Point(187, 72);
            layoutControlItem81.Name = "layoutControlItem81";
            layoutControlItem81.Size = new Size(198, 26);
            layoutControlItem81.TextVisible = false;
            // 
            // layoutControlItem82
            // 
            layoutControlItem82.Control = label10;
            layoutControlItem82.Location = new Point(611, 29);
            layoutControlItem82.Name = "layoutControlItem82";
            layoutControlItem82.Size = new Size(24, 69);
            layoutControlItem82.TextVisible = false;
            // 
            // layoutControlItem83
            // 
            layoutControlItem83.Control = tbRzuMod;
            layoutControlItem83.Location = new Point(635, 29);
            layoutControlItem83.MinSize = new Size(24, 24);
            layoutControlItem83.Name = "layoutControlItem83";
            layoutControlItem83.Size = new Size(473, 69);
            layoutControlItem83.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem83.TextVisible = false;
            // 
            // layoutControlItem84
            // 
            layoutControlItem84.Control = label13;
            layoutControlItem84.Location = new Point(611, 0);
            layoutControlItem84.Name = "layoutControlItem84";
            layoutControlItem84.Size = new Size(24, 29);
            layoutControlItem84.TextVisible = false;
            // 
            // layoutControlItem85
            // 
            layoutControlItem85.Control = tbSostPoln;
            layoutControlItem85.Location = new Point(635, 0);
            layoutControlItem85.MinSize = new Size(24, 24);
            layoutControlItem85.Name = "layoutControlItem85";
            layoutControlItem85.Size = new Size(473, 29);
            layoutControlItem85.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem85.TextVisible = false;
            // 
            // emptySpaceItem25
            // 
            emptySpaceItem25.Location = new Point(128, 0);
            emptySpaceItem25.Name = "emptySpaceItem25";
            emptySpaceItem25.Size = new Size(10, 24);
            // 
            // emptySpaceItem26
            // 
            emptySpaceItem26.Location = new Point(222, 24);
            emptySpaceItem26.Name = "emptySpaceItem26";
            emptySpaceItem26.Size = new Size(10, 24);
            // 
            // emptySpaceItem27
            // 
            emptySpaceItem27.Location = new Point(385, 0);
            emptySpaceItem27.Name = "emptySpaceItem27";
            emptySpaceItem27.Size = new Size(226, 98);
            // 
            // CardByNom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2136, 966);
            Controls.Add(layoutControl3);
            Controls.Add(xtraTabControl1);
            Controls.Add(layoutControl1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "CardByNom";
            Text = "Карточка расчета";
            Load += CardByNom_Load;
            ((System.ComponentModel.ISupportInitialize)bsProizvCombIzdSP).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsProizvCombIzdVZP).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridSplitContainer1.Panel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridSplitContainer1.Panel2).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridSplitContainer1).EndInit();
            gridSplitContainer1.ResumeLayout(false);
            OtdelkaInfo.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            customGroupBox7.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlProizvCombIzdSP).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProizvCombIzdSP).EndInit();
            customGroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlProizvCombIzdVZP).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProizvCombIzdVZP).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNaklList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlNaklList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewOtdelka).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlOtdelka).EndInit();
            FurnInfo.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            customGroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablePanel7).EndInit();
            tablePanel7.ResumeLayout(false);
            tablePanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl7).EndInit();
            RasInfo.ResumeLayout(false);
            customGroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablePanel6).EndInit();
            tablePanel6.ResumeLayout(false);
            customGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablePanel5).EndInit();
            tablePanel5.ResumeLayout(false);
            tablePanel5.PerformLayout();
            customGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablePanel4).EndInit();
            tablePanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlPartNaklList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPartNaklList).EndInit();
            ContDates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablePanel3).EndInit();
            tablePanel3.ResumeLayout(false);
            tablePanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl1).EndInit();
            xtraTabControl1.ResumeLayout(false);
            SockZadanyInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl2).EndInit();
            layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TextBoxKolPlanZadany.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSockDefectList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSockDefectList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSockDownTimeList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSockDownTimeList).EndInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxKolFactSmen.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxKolFactDelta.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxKolFactZadany.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSockServiceList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSockZadanySmenList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxDefectCount.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)TextBoxDefectWeight.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem26).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem27).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem29).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem30).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem31).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem32).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem33).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem34).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem35).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem36).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem37).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem28).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem41).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem42).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem38).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem40).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem43).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem44).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem45).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem14).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator4).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator5).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem24).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem19).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem25).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem39).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem11).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)customRadioGroup3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl3).EndInit();
            layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customRadioGroup2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbEskiz).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem23).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem46).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem49).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem50).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem51).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem52).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem24).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem53).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem54).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem20).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem55).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem56).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem57).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem58).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem59).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem15).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem17).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem18).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem19).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem62).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem63).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem20).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem64).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem65).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem66).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem67).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem68).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem69).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem70).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem71).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem72).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem73).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem22).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem60).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem61).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem47).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem48).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem74).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem75).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem23).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem76).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem77).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem78).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem79).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem80).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem81).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem82).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem83).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem84).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem85).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem25).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem26).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem27).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private System.Windows.Forms.BindingSource bsProizvCombIzdVZP;
        private System.Windows.Forms.BindingSource bsProizvCombIzdSP;
        private DevExpress.XtraGrid.GridSplitContainer gridSplitContainer1;
        private DevExpress.XtraTab.XtraTabPage OtdelkaInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProizvCombIzdSP;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSpRzuMod;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPPszNom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPPszZvet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPRzuArticul;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPRzuGrup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSpRzuRazm;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPKolItog;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPNIz;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPKolRaskr;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPKolRab;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPKolFurnPrinSkl;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPRzuDataRab;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPNDostData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPDateFurnPrihSkl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProizvCombIzdVZP;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPRzvMod;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPPszNom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPPszZvet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPRzvArticul;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPRzvGrup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPRzvRazm;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPKolItog;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPNIz;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPKolVyaz;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPKolOtparka;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPKolGI;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPKolFurnPrinSkl;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPRzvDateOkonV;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPNDostData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdVZPDateFurnPrihSkl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNaklList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnKolB;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPrich;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklSklNaimen;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklGlNomer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklDatePrint;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklDostN;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklDostData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklDateIzm;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklIzDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklIzNakl;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklCountAfter;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklChipOtgr;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklChipScan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklChipPech;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklChipInUT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklMod;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklArticul;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOtdelka;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOtdelkaViNaim;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOtdelkaCaption;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOtdelkaFrtNaimen;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOtdelkaKolSlZv;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOtdelkaPsaFieldName;
        private DevExpress.XtraTab.XtraTabPage WorkInfo;
        private DevExpress.XtraTab.XtraTabPage FurnInfo;
        private FurnitZayavView furnitZayavViewFurnit;
        private FurnitZayavView furnitZayavViewUpak;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraTab.XtraTabPage RasInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPartNaklList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartIzObPrch;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartRazm;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartMod;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartCountNew;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartCountAfter;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartCountBefore;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartIzNew;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartSklOtgrNew;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartSklOtgrOld;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartDateIzm;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartSklID1COld;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartCompDel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartCompName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartIzOld;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartNPach;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartPrichSokr;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklPartSklID1CNew;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProizvCombIzdSPKolGI;
        private CustomTextBox tbNomPach;
        private CustomTextBox tbYearPach;
        private CustomTextBox tbDatZayav;
        private CustomTextBox tbData_f_z_u;
        private CustomTextBox tbData_f_o_u;
        private CustomTextBox tbData_f_z;
        private CustomTextBox tbData_f_o;
        private CustomTextBox tbOtgrStat;
        private CustomTextBox tbIs_got;
        private CustomTextBox tbUZSobrStat;
        private CustomTextBox tbUZSozdStat;
        private CustomTextBox tbUpakZayav;
        private CustomTextBox tbFZSobrStat;
        private CustomTextBox tbFZSozdStat;
        private CustomTextBox tbFurnZayav;
        private CustomTextBox tbUpakKKStat;
        private CustomTextBox tbFurnKKStat;
        private CustomButton btnNaklAbsent;
        private CustomButton btnNaklPrint;
        private CustomTextBox tbPszRpcNom;
        private CustomLabel label4;
        private CustomLabel label61;
        private CustomMaskedTextBox mtbData_cd;
        private CustomMaskedTextBox mtbData_zeh;
        private CustomLabel label60;
        private CustomLabel label59;
        private CustomLabel label56;
        private CustomLabel label57;
        private CustomLabel label58;
        private CustomLabel label55;
        private CustomLabel label54;
        private CustomLabel label25;
        private CustomMaskedTextBox mtbRzuDataPrCd;
        private CustomMaskedTextBox mtbRzuDataVCd;
        private CustomMaskedTextBox mtbRzuDataPrKm;
        private CustomMaskedTextBox mtbRzuVidStir;
        private CustomMaskedTextBox mtbRzuDataVChi;
        private CustomMaskedTextBox mtbRzuDataPrPe;
        private CustomMaskedTextBox mtbRzuDataStCd;
        private CustomMaskedTextBox mtbRzuDataVR;
        private CustomMaskedTextBox mtbRzuDataPrR;
        private CustomMaskedTextBox mtbRzuDataStR;
        private CustomMaskedTextBox mtbRzuDataVP;
        private CustomMaskedTextBox mtbRzuDataPrP;
        private CustomMaskedTextBox mtbRzuDataStP;
        private CustomMaskedTextBox mtbRzuDataRasv;
        private CustomMaskedTextBox mtbRzuDataRasp;
        private CustomLabel label44;
        private CustomLabel label49;
        private CustomLabel label50;
        private CustomLabel label51;
        private CustomLabel label43;
        private CustomLabel label45;
        private CustomLabel label46;
        private CustomLabel label47;
        private CustomLabel label48;
        private CustomLabel label42;
        private CustomLabel label41;
        private CustomLabel label40;
        private CustomLabel label39;
        private CustomLabel label38;
        private CustomLabel label37;
        private CustomLabel label35;
        private CustomLabel label36;
        private CustomLabel label32;
        private CustomMaskedTextBox mtbRzuDataR;
        private CustomLabel label64;
        private CustomMaskedTextBox mtbRzuData1С;
        private CustomLabel label63;
        private CustomMaskedTextBox mtbRzuDataCd;
        private CustomMaskedTextBox mtbRzuDataUp;
        private CustomMaskedTextBox mtbRzuDataRab;
        private CustomMaskedTextBox mtbRzuDataZeh;
        private CustomMaskedTextBox mtbRzuDataCdUt;
        private CustomMaskedTextBox mtbPsaDataCdPlan;
        private CustomMaskedTextBox mtbPsaDataZap;
        private CustomLabel label14;
        private CustomLabel label33;
        private CustomLabel label34;
        private CustomLabel label26;
        private CustomLabel label27;
        private CustomLabel label28;
        private CustomLabel label29;
        private CustomCheckBox cbRzuStirFact;
        private CustomCheckBox cbPszStirPlan;
        private CustomCheckBox cbRzuVishFact;
        private CustomCheckBox cbPszVishPlan;
        private CustomCheckBox cbRzuPrintFact;
        private CustomCheckBox cbPszPrintPlan;
        private CustomLabel customLabel2;
        private CustomLabel customLabel1;
        private CustomTabControl xtraTabControl1;
        private CustomGroupBox ContDates;
        private DevExpress.Utils.Layout.TablePanel tablePanel3;
        private CustomGridControl gridControlProizvCombIzdSP;
        private CustomGridControl gridControlProizvCombIzdVZP;
        private CustomGridControl gridControlNaklList;
        private CustomGridControl gridControlOtdelka;
        private CustomGridControl gridControlPartNaklList;
        private CustomGroupBox customGroupBox3;
        private DevExpress.Utils.Layout.TablePanel tablePanel4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private CustomGroupBox customGroupBox4;
        private DevExpress.Utils.Layout.TablePanel tablePanel5;
        private CustomGroupBox customGroupBox5;
        private DevExpress.Utils.Layout.TablePanel tablePanel6;
        private CustomSimpleButton sbProizvCombIzdSP;
        private CustomGroupBox customGroupBox6;
        private DevExpress.Utils.Layout.TablePanel tablePanel7;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private CustomGroupBox customGroupBox7;
        private CustomGroupBox customGroupBox8;
        private TableLayoutPanel tableLayoutPanel1;
        private CustomLabel customLabel3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklCountBefore;
        private CustomSimpleButton simpleButtonPrintNaklXtraReport;
        private CustomSimpleButton simpleButtonNaklPart;
        private CustomSimpleButton simpleButtonPrintMLRTAll;
        private CustomSimpleButton simpleButtonPrintMLRTUpak;
        private CustomSimpleButton simpleButtonReestrListPrint;
        private CustomSimpleButton simpleButtonFurnKKPrint;
        private CustomSimpleButton simpleButtonZayavFurnPrint;
        private CustomSimpleButton simpleButtonUpakKKPrint;
        private CustomSimpleButton simpleButtonZayavUpakPrint;
        private CustomSimpleButton simpleButtonFurnDeliveryInfoShow;
        private CustomSimpleButton simpleButtonUpakDeliveryInfoShow;
        private CustomSimpleButton simpleButtonFullKKPrint;
        private CustomRadioGroup customRadioGroup3;
        private CustomRadioGroup customRadioGroup2;
        private CustomTextBox tbNomZad;
        private CustomLabel customLabel4;
        private DevExpress.XtraTab.XtraTabPage SockZadanyInfo;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private CustomTextBox TextBoxKnitEndDate;
        private CustomTextBox TextBoxKnitStartDate;
        private CustomTextBox TextBoxAreaNumber;
        private CustomTextBox TextBoxMachineNumber;
        private CustomLabel customLabel14;
        private CustomLabel customLabel13;
        private CustomLabel customLabel12;
        private CustomLabel customLabel11;
        private CustomLabel customLabel10;
        private CustomLabel customLabel9;
        private CustomLabel customLabel8;
        private CustomTextBoxEx TextBoxDefectCount;
        private CustomTextBoxEx TextBoxDefectWeight;
        private CustomLabel customLabel7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem37;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private CustomTextBox TextBoxTabFio;
        private CustomGridControl gridControlSockZadanySmenList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKzDateAdd;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKwsTabStart;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnFioSt;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKzDateEnd;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKwsTabEnd;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnFioEn;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKolFact;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnChasVyaz;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnTimeDiff;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKzID;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKzKwsID;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKzKmlID;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKzKmaID;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKzEnded;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKmlNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKmaNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnDivider;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKwsKmsID;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnDiffPeriod;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private CustomGridControl gridControlSockServiceList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnKzPszNom;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnKmaNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnKmlInvNum;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnKmlNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnTextObS;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnDirectorName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnResultName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnResultText;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnDateEnd;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnMechanic;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnDiffPeriod;
        private CustomTextBoxEx TextBoxKolFactZadany;
        private CustomLabel customLabel18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private CustomTextBoxEx TextBoxKolFactDelta;
        private CustomLabel customLabel19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem42;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private CustomTextBoxEx TextBoxKolFactSmen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockZadanySmenListColumnKmlInvNumber;
        private CustomGridControl gridControlSockDownTimeList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSockDownTimeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem43;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnKzPszNom;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnKmaNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnKmlNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnKmlInvNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnKmlID;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnTextObS;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnKdtlDateStart;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnKdtlDateEnd;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnDiffPeriod;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnDaysDiff;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDownTimeListColumnTimeDiff;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnDaysDiff;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockServiceListColumnTimeDiff;
        private CustomLabel customLabel20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem44;
        private CustomTextBox TextBoxKnitTotalTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem45;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem14;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator4;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator5;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator6;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private CustomGridControl gridControlSockDefectList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSockDefectList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnVspdid;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnNomZadany;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnIsdefect;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnKg;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnKolAll;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnKolDefect;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnIdspj;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnIdndsp;
        private DevExpress.XtraGrid.Columns.GridColumn gridSockDefectListColumnNamedefect;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem11;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private CustomLabel customLabel5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private CustomTextBoxEx TextBoxKolPlanZadany;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private CustomTextBox tbSostPoln;
        private CustomTextBox tbRzuMod;
        private CustomLabel label13;
        private CustomTextBox tbArtGrup;
        private CustomTextBox tbPsaKombOsn;
        private CustomLabel label10;
        private CustomTextBox tbPsaPrn;
        private CustomLabel label67;
        private CustomTextBox tbPsaKodZv2;
        private CustomLabel label15;
        private CustomTextBox tbPsaPsaIDOsn;
        private CustomTextBox tbPsaKodZv1;
        private CustomLabel label22;
        private CustomTextBox tbPsaKombIzd;
        private PictureBox pbEskiz;
        private CustomLabel label23;
        private CustomLabel label66;
        private CustomLabel label8;
        private CustomLabel label65;
        private CustomLabel label9;
        private CustomTextBox tbPsaPsaID;
        private CustomLabel label5;
        private CustomLabel label62;
        private CustomTextBox tbArtTradeMark;
        private CustomTextBox tbPsaNameSbit;
        private CustomTextBox tbRzuNom;
        private CustomLabel label68;
        private CustomCheckBox cbIsChip;
        private CustomTextBox tbRzuArticul;
        private CustomLabel label19;
        private CustomLabel label11;
        private CustomTextBox tbPsaMenName;
        private CustomTextBox tbPsaTbID;
        private CustomTextBox psaSezName;
        private CustomTextBox tbPsaYear;
        private CustomLabel label20;
        private CustomLabel label16;
        private CustomLabel label7;
        private CustomTextBox tbRzuKol;
        private CustomLabel label17;
        private CustomLabel label53;
        private CustomTextBox tbPsaNomZad;
        private CustomLabel label21;
        private CustomLabel label12;
        private CustomTextBox tbPsaNN;
        private CustomTextBox tbRzuDostZeh;
        private CustomLabel label6;
        private CustomTextBox tbRzuPach;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem53;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem54;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem55;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem56;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem57;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem58;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem59;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem15;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem17;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem18;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem62;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem63;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem64;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem65;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem66;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem67;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem68;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem69;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem70;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem71;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem72;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem73;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem60;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem61;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem46;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem49;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem51;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem52;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem24;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem47;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem48;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem74;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem75;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem76;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem77;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem78;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem79;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem80;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem81;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem82;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem83;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem84;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem85;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem25;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem26;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
    }
}