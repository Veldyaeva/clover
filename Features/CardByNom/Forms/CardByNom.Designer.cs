using System;
using System.Drawing;
using System.Windows.Forms;
//using System.Windows.Media;

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
            this.components = new System.ComponentModel.Container();
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
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklChipInUT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklChipPech = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklChipScan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklChipOtgr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tbYearPach = new SewingProduction.CustomTextBox();
            this.tbNomPach = new SewingProduction.CustomTextBox();
            this.tbPsaKombIzd = new SewingProduction.CustomTextBox();
            this.tbPsaKombOsn = new SewingProduction.CustomTextBox();
            this.tbPsaPsaIDOsn = new SewingProduction.CustomTextBox();
            this.tbPsaPsaID = new SewingProduction.CustomTextBox();
            this.label66 = new SewingProduction.CustomLabel();
            this.label67 = new SewingProduction.CustomLabel();
            this.label65 = new SewingProduction.CustomLabel();
            this.label62 = new SewingProduction.CustomLabel();
            this.pbEskiz = new System.Windows.Forms.PictureBox();
            this.label4 = new SewingProduction.CustomLabel();
            this.label3 = new SewingProduction.CustomLabel();
            this.tbRzuKol = new SewingProduction.CustomTextBox();
            this.tbPsaNomZad = new SewingProduction.CustomTextBox();
            this.tbRzuDostZeh = new SewingProduction.CustomTextBox();
            this.tbRzuPach = new SewingProduction.CustomTextBox();
            this.tbRzuNom = new SewingProduction.CustomTextBox();
            this.tbPsaKodZv2 = new SewingProduction.CustomTextBox();
            this.tbPsaKodZv1 = new SewingProduction.CustomTextBox();
            this.tbRzuMod = new SewingProduction.CustomTextBox();
            this.tbRzuArticul = new SewingProduction.CustomTextBox();
            this.tbPsaPrn = new SewingProduction.CustomTextBox();
            this.tbSostPoln = new SewingProduction.CustomTextBox();
            this.tbArtGrup = new SewingProduction.CustomTextBox();
            this.tbArtTradeMark = new SewingProduction.CustomTextBox();
            this.tbPsaMenName = new SewingProduction.CustomTextBox();
            this.tbPsaNameSbit = new SewingProduction.CustomTextBox();
            this.tbPsaTbID = new SewingProduction.CustomTextBox();
            this.cbIsChip = new SewingProduction.CustomCheckBox();
            this.psaSezName = new SewingProduction.CustomTextBox();
            this.tbPsaYear = new SewingProduction.CustomTextBox();
            this.tbPsaNN = new SewingProduction.CustomTextBox();
            this.label68 = new SewingProduction.CustomLabel();
            this.label16 = new SewingProduction.CustomLabel();
            this.label17 = new SewingProduction.CustomLabel();
            this.label22 = new SewingProduction.CustomLabel();
            this.label53 = new SewingProduction.CustomLabel();
            this.label23 = new SewingProduction.CustomLabel();
            this.label20 = new SewingProduction.CustomLabel();
            this.label13 = new SewingProduction.CustomLabel();
            this.label11 = new SewingProduction.CustomLabel();
            this.label15 = new SewingProduction.CustomLabel();
            this.label12 = new SewingProduction.CustomLabel();
            this.label10 = new SewingProduction.CustomLabel();
            this.label19 = new SewingProduction.CustomLabel();
            this.label9 = new SewingProduction.CustomLabel();
            this.label8 = new SewingProduction.CustomLabel();
            this.label7 = new SewingProduction.CustomLabel();
            this.label6 = new SewingProduction.CustomLabel();
            this.label5 = new SewingProduction.CustomLabel();
            this.label21 = new SewingProduction.CustomLabel();
            this.bsProizvCombIzdSP = new System.Windows.Forms.BindingSource(this.components);
            this.bsProizvCombIzdVZP = new System.Windows.Forms.BindingSource(this.components);
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridSplitContainer1 = new DevExpress.XtraGrid.GridSplitContainer();
            this.OtdelkaInfo = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.customGroupBox7 = new SewingProduction.CustomGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControlProizvCombIzdSP = new SewingProduction.CustomGridControl();
            this.gridViewProizvCombIzdSP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnProizvCombIzdSpRzuMod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPPszNom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPPszZvet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPRzuArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPRzuGrup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSpRzuRazm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPKolItog = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPNIz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPKolRaskr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPKolRab = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPRzuDataRab = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPNDostData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdSPKolGI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sbProizvCombIzdSP = new SewingProduction.CustomSimpleButton();
            this.customGroupBox8 = new SewingProduction.CustomGroupBox();
            this.gridControlProizvCombIzdVZP = new SewingProduction.CustomGridControl();
            this.gridViewProizvCombIzdVZP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnProizvCombIzdVZPRzvMod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPPszNom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPPszZvet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPRzvArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPRzvGrup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPRzvRazm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPKolItog = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPNIz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPKolVyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPKolOtparka = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPKolGI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPRzvDateOkonV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPNDostData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridViewNaklList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNaklCountBefore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPrich = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklSklNaimen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklGlNomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklDatePrint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklDostN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklDostData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklDateIzm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklIzDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklIzNakl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklCountAfter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklMod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlNaklList = new SewingProduction.CustomGridControl();
            this.gridViewOtdelka = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnOtdelkaViNaim = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOtdelkaCaption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOtdelkaFrtNaimen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOtdelkaKolSlZv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOtdelkaPsaFieldName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlOtdelka = new SewingProduction.CustomGridControl();
            this.WorkInfo = new DevExpress.XtraTab.XtraTabPage();
            this.FurnInfo = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.furnitZayavViewFurnit = new SewingProduction.FurnitZayavView();
            this.furnitZayavViewUpak = new SewingProduction.FurnitZayavView();
            this.customGroupBox6 = new SewingProduction.CustomGroupBox();
            this.tablePanel7 = new DevExpress.Utils.Layout.TablePanel();
            this.simpleButtonFullKKPrint = new SewingProduction.CustomSimpleButton();
            this.simpleButtonUpakDeliveryInfoShow = new SewingProduction.CustomSimpleButton();
            this.simpleButtonFurnDeliveryInfoShow = new SewingProduction.CustomSimpleButton();
            this.simpleButtonZayavUpakPrint = new SewingProduction.CustomSimpleButton();
            this.simpleButtonUpakKKPrint = new SewingProduction.CustomSimpleButton();
            this.simpleButtonZayavFurnPrint = new SewingProduction.CustomSimpleButton();
            this.simpleButtonFurnKKPrint = new SewingProduction.CustomSimpleButton();
            this.tbDatZayav = new SewingProduction.CustomTextBox();
            this.tbOtgrStat = new SewingProduction.CustomTextBox();
            this.mtbData_cd = new SewingProduction.CustomMaskedTextBox();
            this.label61 = new SewingProduction.CustomLabel();
            this.mtbData_zeh = new SewingProduction.CustomMaskedTextBox();
            this.label60 = new SewingProduction.CustomLabel();
            this.tbIs_got = new SewingProduction.CustomTextBox();
            this.tbData_f_z_u = new SewingProduction.CustomTextBox();
            this.tbFurnKKStat = new SewingProduction.CustomTextBox();
            this.tbData_f_o_u = new SewingProduction.CustomTextBox();
            this.label59 = new SewingProduction.CustomLabel();
            this.tbData_f_z = new SewingProduction.CustomTextBox();
            this.tbUpakKKStat = new SewingProduction.CustomTextBox();
            this.tbUZSobrStat = new SewingProduction.CustomTextBox();
            this.tbData_f_o = new SewingProduction.CustomTextBox();
            this.label25 = new SewingProduction.CustomLabel();
            this.label56 = new SewingProduction.CustomLabel();
            this.tbFurnZayav = new SewingProduction.CustomTextBox();
            this.tbUZSozdStat = new SewingProduction.CustomTextBox();
            this.label54 = new SewingProduction.CustomLabel();
            this.tbFZSozdStat = new SewingProduction.CustomTextBox();
            this.label55 = new SewingProduction.CustomLabel();
            this.label57 = new SewingProduction.CustomLabel();
            this.tbFZSobrStat = new SewingProduction.CustomTextBox();
            this.tbUpakZayav = new SewingProduction.CustomTextBox();
            this.label58 = new SewingProduction.CustomLabel();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.RasInfo = new DevExpress.XtraTab.XtraTabPage();
            this.customGroupBox5 = new SewingProduction.CustomGroupBox();
            this.tablePanel6 = new DevExpress.Utils.Layout.TablePanel();
            this.simpleButtonReestrListPrint = new SewingProduction.CustomSimpleButton();
            this.simpleButtonPrintMLRTUpak = new SewingProduction.CustomSimpleButton();
            this.simpleButtonPrintMLRTAll = new SewingProduction.CustomSimpleButton();
            this.customGroupBox4 = new SewingProduction.CustomGroupBox();
            this.tablePanel5 = new DevExpress.Utils.Layout.TablePanel();
            this.mtbRzuVidStir = new SewingProduction.CustomMaskedTextBox();
            this.cbRzuStirFact = new SewingProduction.CustomCheckBox();
            this.label44 = new SewingProduction.CustomLabel();
            this.mtbRzuDataStCd = new SewingProduction.CustomMaskedTextBox();
            this.label32 = new SewingProduction.CustomLabel();
            this.mtbRzuDataStR = new SewingProduction.CustomMaskedTextBox();
            this.label49 = new SewingProduction.CustomLabel();
            this.cbPszStirPlan = new SewingProduction.CustomCheckBox();
            this.mtbRzuDataStP = new SewingProduction.CustomMaskedTextBox();
            this.cbPszPrintPlan = new SewingProduction.CustomCheckBox();
            this.label50 = new SewingProduction.CustomLabel();
            this.mtbRzuDataVCd = new SewingProduction.CustomMaskedTextBox();
            this.cbRzuVishFact = new SewingProduction.CustomCheckBox();
            this.cbRzuPrintFact = new SewingProduction.CustomCheckBox();
            this.label51 = new SewingProduction.CustomLabel();
            this.mtbRzuDataVChi = new SewingProduction.CustomMaskedTextBox();
            this.cbPszVishPlan = new SewingProduction.CustomCheckBox();
            this.label37 = new SewingProduction.CustomLabel();
            this.mtbRzuDataVR = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataPrCd = new SewingProduction.CustomMaskedTextBox();
            this.label35 = new SewingProduction.CustomLabel();
            this.mtbRzuDataRasp = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataVP = new SewingProduction.CustomMaskedTextBox();
            this.label43 = new SewingProduction.CustomLabel();
            this.label38 = new SewingProduction.CustomLabel();
            this.mtbRzuDataPrKm = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataRasv = new SewingProduction.CustomMaskedTextBox();
            this.label45 = new SewingProduction.CustomLabel();
            this.mtbRzuDataPrP = new SewingProduction.CustomMaskedTextBox();
            this.label39 = new SewingProduction.CustomLabel();
            this.mtbRzuDataPrR = new SewingProduction.CustomMaskedTextBox();
            this.label46 = new SewingProduction.CustomLabel();
            this.mtbRzuDataPrPe = new SewingProduction.CustomMaskedTextBox();
            this.label40 = new SewingProduction.CustomLabel();
            this.label41 = new SewingProduction.CustomLabel();
            this.label47 = new SewingProduction.CustomLabel();
            this.label42 = new SewingProduction.CustomLabel();
            this.label36 = new SewingProduction.CustomLabel();
            this.label48 = new SewingProduction.CustomLabel();
            this.customGroupBox3 = new SewingProduction.CustomGroupBox();
            this.tablePanel4 = new DevExpress.Utils.Layout.TablePanel();
            this.simpleButtonNaklPart = new SewingProduction.CustomSimpleButton();
            this.simpleButtonPrintNaklXtraReport = new SewingProduction.CustomSimpleButton();
            this.btnNaklAbsent = new SewingProduction.CustomButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlPartNaklList = new SewingProduction.CustomGridControl();
            this.gridViewPartNaklList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNaklPartIzObPrch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartRazm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartMod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartCountNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartCountAfter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartCountBefore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartIzNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartSklOtgrNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartSklOtgrOld = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartDateIzm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartSklID1COld = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartCompDel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartCompName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartIzOld = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartNPach = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartPrichSokr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNaklPartSklID1CNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnNaklPrint = new SewingProduction.CustomButton();
            this.ContDates = new SewingProduction.CustomGroupBox();
            this.tablePanel3 = new DevExpress.Utils.Layout.TablePanel();
            this.customLabel3 = new SewingProduction.CustomLabel();
            this.mtbRzuData1С = new SewingProduction.CustomMaskedTextBox();
            this.tbPszRpcNom = new SewingProduction.CustomTextBox();
            this.label63 = new SewingProduction.CustomLabel();
            this.label29 = new SewingProduction.CustomLabel();
            this.mtbRzuDataCd = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataR = new SewingProduction.CustomMaskedTextBox();
            this.label14 = new SewingProduction.CustomLabel();
            this.mtbRzuDataUp = new SewingProduction.CustomMaskedTextBox();
            this.mtbPsaDataZap = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataRab = new SewingProduction.CustomMaskedTextBox();
            this.label33 = new SewingProduction.CustomLabel();
            this.label64 = new SewingProduction.CustomLabel();
            this.mtbRzuDataZeh = new SewingProduction.CustomMaskedTextBox();
            this.label28 = new SewingProduction.CustomLabel();
            this.label26 = new SewingProduction.CustomLabel();
            this.mtbPsaDataCdPlan = new SewingProduction.CustomMaskedTextBox();
            this.label34 = new SewingProduction.CustomLabel();
            this.mtbRzuDataCdUt = new SewingProduction.CustomMaskedTextBox();
            this.label27 = new SewingProduction.CustomLabel();
            this.xtraTabControl1 = new SewingProduction.CustomTabControl();
            this.gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RasCard = new SewingProduction.CustomGroupBox();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.customLabel2 = new SewingProduction.CustomLabel();
            this.customLabel1 = new SewingProduction.CustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbEskiz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProizvCombIzdSP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProizvCombIzdVZP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel2)).BeginInit();
            this.gridSplitContainer1.SuspendLayout();
            this.OtdelkaInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.customGroupBox7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProizvCombIzdSP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProizvCombIzdSP)).BeginInit();
            this.customGroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProizvCombIzdVZP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProizvCombIzdVZP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNaklList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNaklList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOtdelka)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOtdelka)).BeginInit();
            this.FurnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.customGroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel7)).BeginInit();
            this.tablePanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.RasInfo.SuspendLayout();
            this.customGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel6)).BeginInit();
            this.tablePanel6.SuspendLayout();
            this.customGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel5)).BeginInit();
            this.tablePanel5.SuspendLayout();
            this.customGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel4)).BeginInit();
            this.tablePanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPartNaklList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPartNaklList)).BeginInit();
            this.ContDates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).BeginInit();
            this.tablePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.RasCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn40
            // 
            this.gridColumn40.FieldName = "ChipInUTForeColor";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Width = 63;
            // 
            // gridColumnNaklChipInUT
            // 
            this.gridColumnNaklChipInUT.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnNaklChipInUT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklChipInUT.Caption = "ЧИП в УТ";
            this.gridColumnNaklChipInUT.Name = "gridColumnNaklChipInUT";
            this.gridColumnNaklChipInUT.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklChipInUT.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklChipInUT.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklChipInUT.Visible = true;
            this.gridColumnNaklChipInUT.VisibleIndex = 13;
            this.gridColumnNaklChipInUT.Width = 40;
            // 
            // gridColumn44
            // 
            this.gridColumn44.FieldName = "ChipPechForeColor";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Width = 63;
            // 
            // gridColumnNaklChipPech
            // 
            this.gridColumnNaklChipPech.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnNaklChipPech.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklChipPech.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklChipPech.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklChipPech.Caption = "Печ. ЧИП";
            this.gridColumnNaklChipPech.Name = "gridColumnNaklChipPech";
            this.gridColumnNaklChipPech.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklChipPech.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklChipPech.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklChipPech.Visible = true;
            this.gridColumnNaklChipPech.VisibleIndex = 14;
            this.gridColumnNaklChipPech.Width = 40;
            // 
            // gridColumn45
            // 
            this.gridColumn45.FieldName = "ChipScanForeColor";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.Width = 73;
            // 
            // gridColumnNaklChipScan
            // 
            this.gridColumnNaklChipScan.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnNaklChipScan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklChipScan.Caption = "Скан. ЧИП";
            this.gridColumnNaklChipScan.Name = "gridColumnNaklChipScan";
            this.gridColumnNaklChipScan.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklChipScan.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklChipScan.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklChipScan.Visible = true;
            this.gridColumnNaklChipScan.VisibleIndex = 15;
            this.gridColumnNaklChipScan.Width = 40;
            // 
            // gridColumn46
            // 
            this.gridColumn46.FieldName = "ChipOtgrForeColor";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.Width = 73;
            // 
            // gridColumnNaklChipOtgr
            // 
            this.gridColumnNaklChipOtgr.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnNaklChipOtgr.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklChipOtgr.Caption = "Отгр. ЧИП";
            this.gridColumnNaklChipOtgr.Name = "gridColumnNaklChipOtgr";
            this.gridColumnNaklChipOtgr.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklChipOtgr.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklChipOtgr.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklChipOtgr.Visible = true;
            this.gridColumnNaklChipOtgr.VisibleIndex = 16;
            this.gridColumnNaklChipOtgr.Width = 40;
            // 
            // tbYearPach
            // 
            this.tbYearPach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel2.SetColumn(this.tbYearPach, 6);
            this.tbYearPach.Font = new System.Drawing.Font("Arial", 10F);
            this.tbYearPach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbYearPach.Location = new System.Drawing.Point(285, 15);
            this.tbYearPach.Margin = new System.Windows.Forms.Padding(0);
            this.tbYearPach.Name = "tbYearPach";
            this.tbYearPach.ObjectName = null;
            this.tablePanel2.SetRow(this.tbYearPach, 0);
            this.tbYearPach.Size = new System.Drawing.Size(50, 23);
            this.tbYearPach.TabIndex = 7;
            // 
            // tbNomPach
            // 
            this.tbNomPach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel2.SetColumn(this.tbNomPach, 3);
            this.tbNomPach.Font = new System.Drawing.Font("Arial", 10F);
            this.tbNomPach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbNomPach.Location = new System.Drawing.Point(166, 15);
            this.tbNomPach.Margin = new System.Windows.Forms.Padding(0);
            this.tbNomPach.Name = "tbNomPach";
            this.tbNomPach.ObjectName = null;
            this.tablePanel2.SetRow(this.tbNomPach, 0);
            this.tbNomPach.Size = new System.Drawing.Size(61, 23);
            this.tbNomPach.TabIndex = 5;
            this.tbNomPach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNomPach_KeyDown);
            // 
            // tbPsaKombIzd
            // 
            this.tbPsaKombIzd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaKombIzd, 22);
            this.tablePanel1.SetColumnSpan(this.tbPsaKombIzd, 2);
            this.tbPsaKombIzd.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaKombIzd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaKombIzd.Location = new System.Drawing.Point(1407, 96);
            this.tbPsaKombIzd.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaKombIzd.Name = "tbPsaKombIzd";
            this.tbPsaKombIzd.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaKombIzd, 3);
            this.tbPsaKombIzd.Size = new System.Drawing.Size(52, 23);
            this.tbPsaKombIzd.TabIndex = 38;
            // 
            // tbPsaKombOsn
            // 
            this.tbPsaKombOsn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaKombOsn, 27);
            this.tbPsaKombOsn.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaKombOsn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaKombOsn.Location = new System.Drawing.Point(1579, 96);
            this.tbPsaKombOsn.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaKombOsn.Name = "tbPsaKombOsn";
            this.tbPsaKombOsn.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaKombOsn, 3);
            this.tbPsaKombOsn.Size = new System.Drawing.Size(55, 23);
            this.tbPsaKombOsn.TabIndex = 36;
            // 
            // tbPsaPsaIDOsn
            // 
            this.tbPsaPsaIDOsn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaPsaIDOsn, 27);
            this.tbPsaPsaIDOsn.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaPsaIDOsn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaPsaIDOsn.Location = new System.Drawing.Point(1579, 74);
            this.tbPsaPsaIDOsn.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaPsaIDOsn.Name = "tbPsaPsaIDOsn";
            this.tbPsaPsaIDOsn.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaPsaIDOsn, 2);
            this.tbPsaPsaIDOsn.Size = new System.Drawing.Size(55, 23);
            this.tbPsaPsaIDOsn.TabIndex = 34;
            // 
            // tbPsaPsaID
            // 
            this.tbPsaPsaID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaPsaID, 22);
            this.tablePanel1.SetColumnSpan(this.tbPsaPsaID, 2);
            this.tbPsaPsaID.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaPsaID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaPsaID.Location = new System.Drawing.Point(1407, 74);
            this.tbPsaPsaID.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaPsaID.Name = "tbPsaPsaID";
            this.tbPsaPsaID.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaPsaID, 2);
            this.tbPsaPsaID.Size = new System.Drawing.Size(52, 23);
            this.tbPsaPsaID.TabIndex = 32;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label66, 21);
            this.label66.Font = new System.Drawing.Font("Arial", 10F);
            this.label66.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label66.Location = new System.Drawing.Point(1338, 100);
            this.label66.Name = "label66";
            this.label66.ObjectName = null;
            this.tablePanel1.SetRow(this.label66, 3);
            this.label66.Size = new System.Drawing.Size(66, 16);
            this.label66.TabIndex = 39;
            this.label66.Text = "komb_izd";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label67, 25);
            this.tablePanel1.SetColumnSpan(this.label67, 2);
            this.label67.Font = new System.Drawing.Font("Arial", 10F);
            this.label67.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label67.Location = new System.Drawing.Point(1482, 100);
            this.label67.Name = "label67";
            this.label67.ObjectName = null;
            this.tablePanel1.SetRow(this.label67, 3);
            this.label67.Size = new System.Drawing.Size(72, 16);
            this.label67.TabIndex = 37;
            this.label67.Text = "komb_osn";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label65, 25);
            this.tablePanel1.SetColumnSpan(this.label65, 2);
            this.label65.Font = new System.Drawing.Font("Arial", 10F);
            this.label65.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label65.Location = new System.Drawing.Point(1482, 77);
            this.label65.Name = "label65";
            this.label65.ObjectName = null;
            this.tablePanel1.SetRow(this.label65, 2);
            this.label65.Size = new System.Drawing.Size(80, 16);
            this.label65.TabIndex = 35;
            this.label65.Text = "psa_id_osn";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label62, 21);
            this.label62.Font = new System.Drawing.Font("Arial", 10F);
            this.label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label62.Location = new System.Drawing.Point(1338, 77);
            this.label62.Name = "label62";
            this.label62.ObjectName = null;
            this.tablePanel1.SetRow(this.label62, 2);
            this.label62.Size = new System.Drawing.Size(49, 16);
            this.label62.TabIndex = 33;
            this.label62.Text = "psa_id";
            // 
            // pbEskiz
            // 
            this.tablePanel1.SetColumn(this.pbEskiz, 28);
            this.pbEskiz.Location = new System.Drawing.Point(1636, 13);
            this.pbEskiz.Name = "pbEskiz";
            this.tablePanel1.SetRow(this.pbEskiz, 0);
            this.tablePanel1.SetRowSpan(this.pbEskiz, 4);
            this.pbEskiz.Size = new System.Drawing.Size(107, 103);
            this.pbEskiz.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEskiz.TabIndex = 25;
            this.pbEskiz.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel2.SetColumn(this.label4, 5);
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(250, 18);
            this.label4.Name = "label4";
            this.label4.ObjectName = null;
            this.tablePanel2.SetRow(this.label4, 0);
            this.label4.Size = new System.Drawing.Size(28, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "год";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel2.SetColumn(this.label3, 2);
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(100, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.ObjectName = null;
            this.tablePanel2.SetRow(this.label3, 0);
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "№ пачки";
            // 
            // tbRzuKol
            // 
            this.tbRzuKol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbRzuKol, 6);
            this.tbRzuKol.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuKol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuKol.Location = new System.Drawing.Point(326, 14);
            this.tbRzuKol.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuKol.Name = "tbRzuKol";
            this.tbRzuKol.ObjectName = null;
            this.tablePanel1.SetRow(this.tbRzuKol, 0);
            this.tbRzuKol.Size = new System.Drawing.Size(33, 23);
            this.tbRzuKol.TabIndex = 29;
            // 
            // tbPsaNomZad
            // 
            this.tbPsaNomZad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaNomZad, 1);
            this.tablePanel1.SetColumnSpan(this.tbPsaNomZad, 2);
            this.tbPsaNomZad.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaNomZad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaNomZad.Location = new System.Drawing.Point(99, 46);
            this.tbPsaNomZad.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaNomZad.Name = "tbPsaNomZad";
            this.tbPsaNomZad.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaNomZad, 1);
            this.tbPsaNomZad.Size = new System.Drawing.Size(80, 23);
            this.tbPsaNomZad.TabIndex = 26;
            // 
            // tbRzuDostZeh
            // 
            this.tbRzuDostZeh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbRzuDostZeh, 5);
            this.tablePanel1.SetColumnSpan(this.tbRzuDostZeh, 2);
            this.tbRzuDostZeh.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuDostZeh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuDostZeh.Location = new System.Drawing.Point(287, 46);
            this.tbRzuDostZeh.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuDostZeh.Name = "tbRzuDostZeh";
            this.tbRzuDostZeh.ObjectName = null;
            this.tablePanel1.SetRow(this.tbRzuDostZeh, 1);
            this.tbRzuDostZeh.Size = new System.Drawing.Size(72, 23);
            this.tbRzuDostZeh.TabIndex = 21;
            // 
            // tbRzuPach
            // 
            this.tbRzuPach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbRzuPach, 2);
            this.tablePanel1.SetColumnSpan(this.tbRzuPach, 5);
            this.tbRzuPach.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuPach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuPach.Location = new System.Drawing.Point(149, 74);
            this.tbRzuPach.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuPach.Name = "tbRzuPach";
            this.tbRzuPach.ObjectName = null;
            this.tablePanel1.SetRow(this.tbRzuPach, 2);
            this.tbRzuPach.Size = new System.Drawing.Size(210, 23);
            this.tbRzuPach.TabIndex = 11;
            // 
            // tbRzuNom
            // 
            this.tbRzuNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbRzuNom, 1);
            this.tablePanel1.SetColumnSpan(this.tbRzuNom, 2);
            this.tbRzuNom.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuNom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuNom.Location = new System.Drawing.Point(99, 14);
            this.tbRzuNom.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuNom.Name = "tbRzuNom";
            this.tbRzuNom.ObjectName = null;
            this.tablePanel1.SetRow(this.tbRzuNom, 0);
            this.tbRzuNom.Size = new System.Drawing.Size(80, 23);
            this.tbRzuNom.TabIndex = 9;
            // 
            // tbPsaKodZv2
            // 
            this.tbPsaKodZv2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaKodZv2, 13);
            this.tbPsaKodZv2.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaKodZv2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaKodZv2.Location = new System.Drawing.Point(624, 96);
            this.tbPsaKodZv2.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaKodZv2.Name = "tbPsaKodZv2";
            this.tbPsaKodZv2.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaKodZv2, 3);
            this.tbPsaKodZv2.Size = new System.Drawing.Size(48, 23);
            this.tbPsaKodZv2.TabIndex = 29;
            // 
            // tbPsaKodZv1
            // 
            this.tbPsaKodZv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaKodZv1, 9);
            this.tablePanel1.SetColumnSpan(this.tbPsaKodZv1, 2);
            this.tbPsaKodZv1.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaKodZv1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaKodZv1.Location = new System.Drawing.Point(487, 96);
            this.tbPsaKodZv1.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaKodZv1.Name = "tbPsaKodZv1";
            this.tbPsaKodZv1.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaKodZv1, 3);
            this.tbPsaKodZv1.Size = new System.Drawing.Size(48, 23);
            this.tbPsaKodZv1.TabIndex = 26;
            // 
            // tbRzuMod
            // 
            this.tbRzuMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbRzuMod, 9);
            this.tablePanel1.SetColumnSpan(this.tbRzuMod, 5);
            this.tbRzuMod.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuMod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuMod.Location = new System.Drawing.Point(487, 46);
            this.tbRzuMod.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuMod.Name = "tbRzuMod";
            this.tbRzuMod.ObjectName = null;
            this.tablePanel1.SetRow(this.tbRzuMod, 1);
            this.tbRzuMod.Size = new System.Drawing.Size(185, 23);
            this.tbRzuMod.TabIndex = 19;
            // 
            // tbRzuArticul
            // 
            this.tbRzuArticul.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbRzuArticul, 9);
            this.tablePanel1.SetColumnSpan(this.tbRzuArticul, 5);
            this.tbRzuArticul.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuArticul.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuArticul.Location = new System.Drawing.Point(487, 14);
            this.tbRzuArticul.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuArticul.Name = "tbRzuArticul";
            this.tbRzuArticul.ObjectName = null;
            this.tablePanel1.SetRow(this.tbRzuArticul, 0);
            this.tbRzuArticul.Size = new System.Drawing.Size(185, 23);
            this.tbRzuArticul.TabIndex = 17;
            // 
            // tbPsaPrn
            // 
            this.tbPsaPrn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaPrn, 10);
            this.tablePanel1.SetColumnSpan(this.tbPsaPrn, 4);
            this.tbPsaPrn.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaPrn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaPrn.Location = new System.Drawing.Point(509, 74);
            this.tbPsaPrn.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaPrn.Multiline = true;
            this.tbPsaPrn.Name = "tbPsaPrn";
            this.tbPsaPrn.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaPrn, 2);
            this.tbPsaPrn.Size = new System.Drawing.Size(163, 21);
            this.tbPsaPrn.TabIndex = 15;
            // 
            // tbSostPoln
            // 
            this.tbSostPoln.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbSostPoln, 16);
            this.tbSostPoln.Font = new System.Drawing.Font("Arial", 10F);
            this.tbSostPoln.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbSostPoln.Location = new System.Drawing.Point(806, 42);
            this.tbSostPoln.Margin = new System.Windows.Forms.Padding(0);
            this.tbSostPoln.Multiline = true;
            this.tbSostPoln.Name = "tbSostPoln";
            this.tbSostPoln.ObjectName = null;
            this.tablePanel1.SetRow(this.tbSostPoln, 1);
            this.tablePanel1.SetRowSpan(this.tbSostPoln, 3);
            this.tbSostPoln.Size = new System.Drawing.Size(197, 78);
            this.tbSostPoln.TabIndex = 25;
            // 
            // tbArtGrup
            // 
            this.tbArtGrup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbArtGrup, 16);
            this.tbArtGrup.Font = new System.Drawing.Font("Arial", 10F);
            this.tbArtGrup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbArtGrup.Location = new System.Drawing.Point(806, 14);
            this.tbArtGrup.Margin = new System.Windows.Forms.Padding(0);
            this.tbArtGrup.Name = "tbArtGrup";
            this.tbArtGrup.ObjectName = null;
            this.tablePanel1.SetRow(this.tbArtGrup, 0);
            this.tbArtGrup.Size = new System.Drawing.Size(197, 23);
            this.tbArtGrup.TabIndex = 21;
            // 
            // tbArtTradeMark
            // 
            this.tbArtTradeMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbArtTradeMark, 19);
            this.tbArtTradeMark.Font = new System.Drawing.Font("Arial", 10F);
            this.tbArtTradeMark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbArtTradeMark.Location = new System.Drawing.Point(1143, 96);
            this.tbArtTradeMark.Margin = new System.Windows.Forms.Padding(0);
            this.tbArtTradeMark.Name = "tbArtTradeMark";
            this.tbArtTradeMark.ObjectName = null;
            this.tablePanel1.SetRow(this.tbArtTradeMark, 3);
            this.tbArtTradeMark.Size = new System.Drawing.Size(172, 23);
            this.tbArtTradeMark.TabIndex = 40;
            // 
            // tbPsaMenName
            // 
            this.tbPsaMenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaMenName, 19);
            this.tbPsaMenName.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaMenName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaMenName.Location = new System.Drawing.Point(1143, 47);
            this.tbPsaMenName.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaMenName.Multiline = true;
            this.tbPsaMenName.Name = "tbPsaMenName";
            this.tbPsaMenName.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaMenName, 1);
            this.tbPsaMenName.Size = new System.Drawing.Size(172, 22);
            this.tbPsaMenName.TabIndex = 11;
            // 
            // tbPsaNameSbit
            // 
            this.tbPsaNameSbit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaNameSbit, 19);
            this.tbPsaNameSbit.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaNameSbit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaNameSbit.Location = new System.Drawing.Point(1143, 74);
            this.tbPsaNameSbit.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaNameSbit.Name = "tbPsaNameSbit";
            this.tbPsaNameSbit.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaNameSbit, 2);
            this.tbPsaNameSbit.Size = new System.Drawing.Size(172, 23);
            this.tbPsaNameSbit.TabIndex = 23;
            // 
            // tbPsaTbID
            // 
            this.tbPsaTbID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaTbID, 19);
            this.tbPsaTbID.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaTbID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaTbID.Location = new System.Drawing.Point(1143, 14);
            this.tbPsaTbID.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaTbID.Name = "tbPsaTbID";
            this.tbPsaTbID.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaTbID, 0);
            this.tbPsaTbID.Size = new System.Drawing.Size(172, 23);
            this.tbPsaTbID.TabIndex = 13;
            // 
            // cbIsChip
            // 
            this.cbIsChip.AutoSize = true;
            this.tablePanel1.SetColumn(this.cbIsChip, 27);
            this.cbIsChip.Enabled = false;
            this.cbIsChip.Font = new System.Drawing.Font("Arial", 10F);
            this.cbIsChip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.cbIsChip.Location = new System.Drawing.Point(1581, 16);
            this.cbIsChip.Name = "cbIsChip";
            this.cbIsChip.ObjectName = null;
            this.tablePanel1.SetRow(this.cbIsChip, 0);
            this.cbIsChip.Size = new System.Drawing.Size(50, 20);
            this.cbIsChip.TabIndex = 30;
            this.cbIsChip.Text = "Чип";
            this.cbIsChip.UseVisualStyleBackColor = true;
            // 
            // psaSezName
            // 
            this.psaSezName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.psaSezName, 27);
            this.psaSezName.Font = new System.Drawing.Font("Arial", 10F);
            this.psaSezName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.psaSezName.Location = new System.Drawing.Point(1579, 46);
            this.psaSezName.Margin = new System.Windows.Forms.Padding(0);
            this.psaSezName.Name = "psaSezName";
            this.psaSezName.ObjectName = null;
            this.tablePanel1.SetRow(this.psaSezName, 1);
            this.psaSezName.Size = new System.Drawing.Size(55, 23);
            this.psaSezName.TabIndex = 19;
            // 
            // tbPsaYear
            // 
            this.tbPsaYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaYear, 22);
            this.tablePanel1.SetColumnSpan(this.tbPsaYear, 2);
            this.tbPsaYear.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaYear.Location = new System.Drawing.Point(1407, 46);
            this.tbPsaYear.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaYear.Name = "tbPsaYear";
            this.tbPsaYear.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaYear, 1);
            this.tbPsaYear.Size = new System.Drawing.Size(52, 23);
            this.tbPsaYear.TabIndex = 17;
            // 
            // tbPsaNN
            // 
            this.tbPsaNN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel1.SetColumn(this.tbPsaNN, 23);
            this.tablePanel1.SetColumnSpan(this.tbPsaNN, 3);
            this.tbPsaNN.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaNN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaNN.Location = new System.Drawing.Point(1443, 14);
            this.tbPsaNN.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaNN.Name = "tbPsaNN";
            this.tbPsaNN.ObjectName = null;
            this.tablePanel1.SetRow(this.tbPsaNN, 0);
            this.tbPsaNN.Size = new System.Drawing.Size(116, 23);
            this.tbPsaNN.TabIndex = 9;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label68, 18);
            this.label68.Font = new System.Drawing.Font("Arial", 10F);
            this.label68.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label68.Location = new System.Drawing.Point(1026, 100);
            this.label68.Name = "label68";
            this.label68.ObjectName = null;
            this.tablePanel1.SetRow(this.label68, 3);
            this.label68.Size = new System.Drawing.Size(111, 16);
            this.label68.TabIndex = 41;
            this.label68.Text = "Торговая марка";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label16, 25);
            this.tablePanel1.SetColumnSpan(this.label16, 2);
            this.label16.Font = new System.Drawing.Font("Arial", 10F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label16.Location = new System.Drawing.Point(1482, 50);
            this.label16.Name = "label16";
            this.label16.ObjectName = null;
            this.tablePanel1.SetRow(this.label16, 1);
            this.label16.Size = new System.Drawing.Size(47, 16);
            this.label16.TabIndex = 20;
            this.label16.Text = "Сезон";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label17, 21);
            this.label17.Font = new System.Drawing.Font("Arial", 10F);
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label17.Location = new System.Drawing.Point(1338, 50);
            this.label17.Name = "label17";
            this.label17.ObjectName = null;
            this.tablePanel1.SetRow(this.label17, 1);
            this.label17.Size = new System.Drawing.Size(30, 16);
            this.label17.TabIndex = 18;
            this.label17.Text = "Год";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label22, 12);
            this.label22.Font = new System.Drawing.Font("Arial", 10F);
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label22.Location = new System.Drawing.Point(558, 100);
            this.label22.Name = "label22";
            this.label22.ObjectName = null;
            this.tablePanel1.SetRow(this.label22, 3);
            this.label22.Size = new System.Drawing.Size(63, 16);
            this.label22.TabIndex = 30;
            this.label22.Text = "Код цв.2";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label53, 0);
            this.label53.Font = new System.Drawing.Font("Arial", 10F);
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label53.Location = new System.Drawing.Point(14, 50);
            this.label53.Name = "label53";
            this.label53.ObjectName = null;
            this.tablePanel1.SetRow(this.label53, 1);
            this.label53.Size = new System.Drawing.Size(80, 16);
            this.label53.TabIndex = 27;
            this.label53.Text = "№ задания";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label23, 8);
            this.label23.Font = new System.Drawing.Font("Arial", 10F);
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label23.Location = new System.Drawing.Point(382, 100);
            this.label23.Name = "label23";
            this.label23.ObjectName = null;
            this.tablePanel1.SetRow(this.label23, 3);
            this.label23.Size = new System.Drawing.Size(63, 16);
            this.label23.TabIndex = 27;
            this.label23.Text = "Код цв.1";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label20, 18);
            this.label20.Font = new System.Drawing.Font("Arial", 10F);
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label20.Location = new System.Drawing.Point(1026, 42);
            this.label20.Name = "label20";
            this.label20.ObjectName = null;
            this.tablePanel1.SetRow(this.label20, 1);
            this.label20.Size = new System.Drawing.Size(84, 32);
            this.label20.TabIndex = 12;
            this.label20.Text = "Категория\r\n(менеджер)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label13, 15);
            this.label13.Font = new System.Drawing.Font("Arial", 10F);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(695, 42);
            this.label13.Name = "label13";
            this.label13.ObjectName = null;
            this.tablePanel1.SetRow(this.label13, 1);
            this.label13.Size = new System.Drawing.Size(77, 32);
            this.label13.TabIndex = 24;
            this.label13.Text = "Состав\r\n(из справ.)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label11, 18);
            this.label11.Font = new System.Drawing.Font("Arial", 10F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(1026, 77);
            this.label11.Name = "label11";
            this.label11.ObjectName = null;
            this.tablePanel1.SetRow(this.label11, 2);
            this.label11.Size = new System.Drawing.Size(91, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = "Канал сбыта";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label15, 15);
            this.label15.Font = new System.Drawing.Font("Arial", 10F);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(695, 10);
            this.label15.Name = "label15";
            this.label15.ObjectName = null;
            this.tablePanel1.SetRow(this.label15, 0);
            this.label15.Size = new System.Drawing.Size(105, 32);
            this.label15.TabIndex = 22;
            this.label15.Text = "Наименование\r\n(из справ.)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label12, 4);
            this.label12.Font = new System.Drawing.Font("Arial", 10F);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(202, 50);
            this.label12.Name = "label12";
            this.label12.ObjectName = null;
            this.tablePanel1.SetRow(this.label12, 1);
            this.label12.Size = new System.Drawing.Size(80, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "Бригада №";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label10, 8);
            this.label10.Font = new System.Drawing.Font("Arial", 10F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(382, 50);
            this.label10.Name = "label10";
            this.label10.ObjectName = null;
            this.tablePanel1.SetRow(this.label10, 1);
            this.label10.Size = new System.Drawing.Size(102, 16);
            this.label10.TabIndex = 20;
            this.label10.Text = "Модель (торг.)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label19, 18);
            this.label19.Font = new System.Drawing.Font("Arial", 10F);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(1026, 18);
            this.label19.Name = "label19";
            this.label19.ObjectName = null;
            this.tablePanel1.SetRow(this.label19, 0);
            this.label19.Size = new System.Drawing.Size(38, 16);
            this.label19.TabIndex = 14;
            this.label19.Text = "Блок";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label9, 8);
            this.label9.Font = new System.Drawing.Font("Arial", 10F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(382, 18);
            this.label9.Name = "label9";
            this.label9.ObjectName = null;
            this.tablePanel1.SetRow(this.label9, 0);
            this.label9.Size = new System.Drawing.Size(96, 16);
            this.label9.TabIndex = 18;
            this.label9.Text = "Артикул (шв.)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label8, 8);
            this.tablePanel1.SetColumnSpan(this.label8, 2);
            this.label8.Font = new System.Drawing.Font("Arial", 10F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(382, 77);
            this.label8.Name = "label8";
            this.label8.ObjectName = null;
            this.tablePanel1.SetRow(this.label8, 2);
            this.label8.Size = new System.Drawing.Size(120, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Цвет по заданию";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label7, 4);
            this.tablePanel1.SetColumnSpan(this.label7, 2);
            this.label7.Font = new System.Drawing.Font("Arial", 10F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(202, 18);
            this.label7.Name = "label7";
            this.label7.ObjectName = null;
            this.tablePanel1.SetRow(this.label7, 0);
            this.label7.Size = new System.Drawing.Size(119, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Кол-во в расчете";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label6, 0);
            this.tablePanel1.SetColumnSpan(this.label6, 2);
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(14, 77);
            this.label6.Name = "label6";
            this.label6.ObjectName = null;
            this.tablePanel1.SetRow(this.label6, 2);
            this.label6.Size = new System.Drawing.Size(131, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "№ пачек в расчете";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label5, 0);
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(14, 18);
            this.label5.Name = "label5";
            this.label5.ObjectName = null;
            this.tablePanel1.SetRow(this.label5, 0);
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "№ расчета";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.SetColumn(this.label21, 21);
            this.tablePanel1.SetColumnSpan(this.label21, 2);
            this.label21.Font = new System.Drawing.Font("Arial", 10F);
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label21.Location = new System.Drawing.Point(1338, 18);
            this.label21.Name = "label21";
            this.label21.ObjectName = null;
            this.tablePanel1.SetRow(this.label21, 0);
            this.label21.Size = new System.Drawing.Size(95, 16);
            this.label21.TabIndex = 10;
            this.label21.Text = "Код матрицы";
            // 
            // gridColumn41
            // 
            this.gridColumn41.Caption = "Склад отгр ДО";
            this.gridColumn41.FieldName = "skl_otgr_b";
            this.gridColumn41.Name = "gridColumn41";
            this.gridColumn41.Visible = true;
            this.gridColumn41.VisibleIndex = 7;
            this.gridColumn41.Width = 61;
            // 
            // gridSplitContainer1
            // 
            this.gridSplitContainer1.Grid = null;
            this.gridSplitContainer1.Location = new System.Drawing.Point(11, 14);
            this.gridSplitContainer1.Name = "gridSplitContainer1";
            this.gridSplitContainer1.Size = new System.Drawing.Size(400, 200);
            this.gridSplitContainer1.TabIndex = 2;
            // 
            // OtdelkaInfo
            // 
            this.OtdelkaInfo.Controls.Add(this.splitContainer2);
            this.OtdelkaInfo.Name = "OtdelkaInfo";
            this.OtdelkaInfo.Size = new System.Drawing.Size(1756, 601);
            this.OtdelkaInfo.Text = "ДЕТАЛИ ОТДЕЛКИ";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.customGroupBox7);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.customGroupBox8);
            this.splitContainer2.Size = new System.Drawing.Size(1756, 601);
            this.splitContainer2.SplitterDistance = 300;
            this.splitContainer2.TabIndex = 8;
            // 
            // customGroupBox7
            // 
            this.customGroupBox7.BackColor = System.Drawing.Color.Transparent;
            this.customGroupBox7.BorderColor = System.Drawing.Color.Black;
            this.customGroupBox7.BorderThickness = 1;
            this.customGroupBox7.Controls.Add(this.tableLayoutPanel1);
            this.customGroupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGroupBox7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customGroupBox7.Location = new System.Drawing.Point(0, 0);
            this.customGroupBox7.Name = "customGroupBox7";
            this.customGroupBox7.ObjectName = null;
            this.customGroupBox7.Size = new System.Drawing.Size(1756, 300);
            this.customGroupBox7.TabIndex = 0;
            this.customGroupBox7.TabStop = false;
            this.customGroupBox7.Text = "ШП";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.40385F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.596154F));
            this.tableLayoutPanel1.Controls.Add(this.gridControlProizvCombIzdSP, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbProizvCombIzdSP, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.70588F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.29412F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1750, 275);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControlProizvCombIzdSP
            // 
            this.gridControlProizvCombIzdSP.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.tableLayoutPanel1.SetColumnSpan(this.gridControlProizvCombIzdSP, 2);
            this.gridControlProizvCombIzdSP.DataSource = this.bsProizvCombIzdSP;
            this.gridControlProizvCombIzdSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProizvCombIzdSP.Font = new System.Drawing.Font("Arial", 10F);
            this.gridControlProizvCombIzdSP.Location = new System.Drawing.Point(3, 3);
            this.gridControlProizvCombIzdSP.MainView = this.gridViewProizvCombIzdSP;
            this.gridControlProizvCombIzdSP.Name = "gridControlProizvCombIzdSP";
            this.gridControlProizvCombIzdSP.ObjectName = null;
            this.gridControlProizvCombIzdSP.Size = new System.Drawing.Size(1744, 240);
            this.gridControlProizvCombIzdSP.TabIndex = 4;
            this.gridControlProizvCombIzdSP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProizvCombIzdSP});
            // 
            // gridViewProizvCombIzdSP
            // 
            this.gridViewProizvCombIzdSP.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.gridViewProizvCombIzdSP.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridViewProizvCombIzdSP.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewProizvCombIzdSP.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewProizvCombIzdSP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnProizvCombIzdSpRzuMod,
            this.gridColumnProizvCombIzdSPPszNom,
            this.gridColumnProizvCombIzdSPPszZvet,
            this.gridColumnProizvCombIzdSPRzuArticul,
            this.gridColumnProizvCombIzdSPRzuGrup,
            this.gridColumnProizvCombIzdSpRzuRazm,
            this.gridColumnProizvCombIzdSPKolItog,
            this.gridColumnProizvCombIzdSPNIz,
            this.gridColumnProizvCombIzdSPKolRaskr,
            this.gridColumnProizvCombIzdSPKolRab,
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl,
            this.gridColumnProizvCombIzdSPRzuDataRab,
            this.gridColumnProizvCombIzdSPNDostData,
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl,
            this.gridColumnProizvCombIzdSPKolGI});
            this.gridViewProizvCombIzdSP.CustomizationFormBounds = new System.Drawing.Rectangle(3464, 607, 264, 272);
            this.gridViewProizvCombIzdSP.GridControl = this.gridControlProizvCombIzdSP;
            this.gridViewProizvCombIzdSP.GroupCount = 3;
            this.gridViewProizvCombIzdSP.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRaskr", null, "(Раскроено всего: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRab", null, "(В работе всего: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", null, "(Сдано на склад всего: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", null, "(Принято на склад фурнитуры всего: {0:0.##})")});
            this.gridViewProizvCombIzdSP.Name = "gridViewProizvCombIzdSP";
            this.gridViewProizvCombIzdSP.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewProizvCombIzdSP.OptionsView.ShowFooter = true;
            this.gridViewProizvCombIzdSP.OptionsView.ShowGroupPanel = false;
            this.gridViewProizvCombIzdSP.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnProizvCombIzdSPPszZvet, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnProizvCombIzdSPPszNom, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnProizvCombIzdSPNIz, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumnProizvCombIzdSpRzuMod
            // 
            this.gridColumnProizvCombIzdSpRzuMod.Caption = "Модель";
            this.gridColumnProizvCombIzdSpRzuMod.Name = "gridColumnProizvCombIzdSpRzuMod";
            this.gridColumnProizvCombIzdSpRzuMod.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSpRzuMod.OptionsEditForm.Caption = "Psz Mod:";
            this.gridColumnProizvCombIzdSpRzuMod.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSpRzuMod.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSpRzuMod.Visible = true;
            this.gridColumnProizvCombIzdSpRzuMod.VisibleIndex = 2;
            this.gridColumnProizvCombIzdSpRzuMod.Width = 100;
            // 
            // gridColumnProizvCombIzdSPPszNom
            // 
            this.gridColumnProizvCombIzdSPPszNom.Caption = "Задание";
            this.gridColumnProizvCombIzdSPPszNom.Name = "gridColumnProizvCombIzdSPPszNom";
            this.gridColumnProizvCombIzdSPPszNom.Visible = true;
            this.gridColumnProizvCombIzdSPPszNom.VisibleIndex = 0;
            // 
            // gridColumnProizvCombIzdSPPszZvet
            // 
            this.gridColumnProizvCombIzdSPPszZvet.Caption = "Цвет";
            this.gridColumnProizvCombIzdSPPszZvet.Name = "gridColumnProizvCombIzdSPPszZvet";
            this.gridColumnProizvCombIzdSPPszZvet.Visible = true;
            this.gridColumnProizvCombIzdSPPszZvet.VisibleIndex = 0;
            // 
            // gridColumnProizvCombIzdSPRzuArticul
            // 
            this.gridColumnProizvCombIzdSPRzuArticul.Caption = "Артикул";
            this.gridColumnProizvCombIzdSPRzuArticul.Name = "gridColumnProizvCombIzdSPRzuArticul";
            this.gridColumnProizvCombIzdSPRzuArticul.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPRzuArticul.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPRzuArticul.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPRzuArticul.Visible = true;
            this.gridColumnProizvCombIzdSPRzuArticul.VisibleIndex = 1;
            this.gridColumnProizvCombIzdSPRzuArticul.Width = 100;
            // 
            // gridColumnProizvCombIzdSPRzuGrup
            // 
            this.gridColumnProizvCombIzdSPRzuGrup.Caption = "Группа";
            this.gridColumnProizvCombIzdSPRzuGrup.Name = "gridColumnProizvCombIzdSPRzuGrup";
            this.gridColumnProizvCombIzdSPRzuGrup.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPRzuGrup.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPRzuGrup.Visible = true;
            this.gridColumnProizvCombIzdSPRzuGrup.VisibleIndex = 0;
            this.gridColumnProizvCombIzdSPRzuGrup.Width = 299;
            // 
            // gridColumnProizvCombIzdSpRzuRazm
            // 
            this.gridColumnProizvCombIzdSpRzuRazm.Caption = "Размер";
            this.gridColumnProizvCombIzdSpRzuRazm.Name = "gridColumnProizvCombIzdSpRzuRazm";
            this.gridColumnProizvCombIzdSpRzuRazm.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSpRzuRazm.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSpRzuRazm.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSpRzuRazm.Visible = true;
            this.gridColumnProizvCombIzdSpRzuRazm.VisibleIndex = 3;
            this.gridColumnProizvCombIzdSpRzuRazm.Width = 80;
            // 
            // gridColumnProizvCombIzdSPKolItog
            // 
            this.gridColumnProizvCombIzdSPKolItog.Caption = "Кол-во";
            this.gridColumnProizvCombIzdSPKolItog.Name = "gridColumnProizvCombIzdSPKolItog";
            this.gridColumnProizvCombIzdSPKolItog.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPKolItog.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPKolItog.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPKolItog.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolItog", "ИТОГО = {0:0.##}")});
            this.gridColumnProizvCombIzdSPKolItog.Visible = true;
            this.gridColumnProizvCombIzdSPKolItog.VisibleIndex = 4;
            this.gridColumnProizvCombIzdSPKolItog.Width = 90;
            // 
            // gridColumnProizvCombIzdSPNIz
            // 
            this.gridColumnProizvCombIzdSPNIz.Caption = "№ накладной";
            this.gridColumnProizvCombIzdSPNIz.Name = "gridColumnProizvCombIzdSPNIz";
            this.gridColumnProizvCombIzdSPNIz.Visible = true;
            this.gridColumnProizvCombIzdSPNIz.VisibleIndex = 0;
            // 
            // gridColumnProizvCombIzdSPKolRaskr
            // 
            this.gridColumnProizvCombIzdSPKolRaskr.Caption = "Кол-во раскроено";
            this.gridColumnProizvCombIzdSPKolRaskr.Name = "gridColumnProizvCombIzdSPKolRaskr";
            this.gridColumnProizvCombIzdSPKolRaskr.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPKolRaskr.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPKolRaskr.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPKolRaskr.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRaskr", "Раскроено: {0:0.##}")});
            this.gridColumnProizvCombIzdSPKolRaskr.Visible = true;
            this.gridColumnProizvCombIzdSPKolRaskr.VisibleIndex = 5;
            this.gridColumnProizvCombIzdSPKolRaskr.Width = 150;
            // 
            // gridColumnProizvCombIzdSPKolRab
            // 
            this.gridColumnProizvCombIzdSPKolRab.Caption = "Количество в работе";
            this.gridColumnProizvCombIzdSPKolRab.Name = "gridColumnProizvCombIzdSPKolRab";
            this.gridColumnProizvCombIzdSPKolRab.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPKolRab.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPKolRab.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPKolRab.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRab", "В работе: {0:0.##}")});
            this.gridColumnProizvCombIzdSPKolRab.Visible = true;
            this.gridColumnProizvCombIzdSPKolRab.VisibleIndex = 6;
            this.gridColumnProizvCombIzdSPKolRab.Width = 150;
            // 
            // gridColumnProizvCombIzdSPKolFurnPrinSkl
            // 
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.Caption = "Кол-во прин. на скл. фурн.";
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.Name = "gridColumnProizvCombIzdSPKolFurnPrinSkl";
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", "Прин. на скл. Фурн.: {0:0.##}")});
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.Visible = true;
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.VisibleIndex = 10;
            this.gridColumnProizvCombIzdSPKolFurnPrinSkl.Width = 150;
            // 
            // gridColumnProizvCombIzdSPRzuDataRab
            // 
            this.gridColumnProizvCombIzdSPRzuDataRab.Caption = "Дата в работу";
            this.gridColumnProizvCombIzdSPRzuDataRab.Name = "gridColumnProizvCombIzdSPRzuDataRab";
            this.gridColumnProizvCombIzdSPRzuDataRab.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPRzuDataRab.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPRzuDataRab.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPRzuDataRab.Visible = true;
            this.gridColumnProizvCombIzdSPRzuDataRab.VisibleIndex = 7;
            this.gridColumnProizvCombIzdSPRzuDataRab.Width = 90;
            // 
            // gridColumnProizvCombIzdSPNDostData
            // 
            this.gridColumnProizvCombIzdSPNDostData.Caption = "Дата отгр. на склад";
            this.gridColumnProizvCombIzdSPNDostData.Name = "gridColumnProizvCombIzdSPNDostData";
            this.gridColumnProizvCombIzdSPNDostData.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPNDostData.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPNDostData.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPNDostData.Visible = true;
            this.gridColumnProizvCombIzdSPNDostData.VisibleIndex = 9;
            this.gridColumnProizvCombIzdSPNDostData.Width = 90;
            // 
            // gridColumnProizvCombIzdSPDateFurnPrihSkl
            // 
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl.Caption = "Дата прин. на скл. фурн.";
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl.Name = "gridColumnProizvCombIzdSPDateFurnPrihSkl";
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl.Visible = true;
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl.VisibleIndex = 11;
            this.gridColumnProizvCombIzdSPDateFurnPrihSkl.Width = 90;
            // 
            // gridColumnProizvCombIzdSPKolGI
            // 
            this.gridColumnProizvCombIzdSPKolGI.Caption = "Кол-во отгр. на склад";
            this.gridColumnProizvCombIzdSPKolGI.Name = "gridColumnProizvCombIzdSPKolGI";
            this.gridColumnProizvCombIzdSPKolGI.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdSPKolGI.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdSPKolGI.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdSPKolGI.Visible = true;
            this.gridColumnProizvCombIzdSPKolGI.VisibleIndex = 8;
            this.gridColumnProizvCombIzdSPKolGI.Width = 150;
            // 
            // sbProizvCombIzdSP
            // 
            this.sbProizvCombIzdSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sbProizvCombIzdSP.Location = new System.Drawing.Point(1620, 249);
            this.sbProizvCombIzdSP.Name = "sbProizvCombIzdSP";
            this.sbProizvCombIzdSP.Size = new System.Drawing.Size(127, 23);
            this.sbProizvCombIzdSP.TabIndex = 7;
            this.sbProizvCombIzdSP.Text = "Печать";
            this.sbProizvCombIzdSP.Click += new System.EventHandler(this.sbProizvCombIzdSP_Click);
            // 
            // customGroupBox8
            // 
            this.customGroupBox8.BackColor = System.Drawing.Color.Transparent;
            this.customGroupBox8.BorderColor = System.Drawing.Color.Black;
            this.customGroupBox8.BorderThickness = 1;
            this.customGroupBox8.Controls.Add(this.gridControlProizvCombIzdVZP);
            this.customGroupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGroupBox8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customGroupBox8.Location = new System.Drawing.Point(0, 0);
            this.customGroupBox8.Name = "customGroupBox8";
            this.customGroupBox8.ObjectName = null;
            this.customGroupBox8.Size = new System.Drawing.Size(1756, 297);
            this.customGroupBox8.TabIndex = 0;
            this.customGroupBox8.TabStop = false;
            this.customGroupBox8.Text = "ВЗП";
            // 
            // gridControlProizvCombIzdVZP
            // 
            this.gridControlProizvCombIzdVZP.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.gridControlProizvCombIzdVZP.DataSource = this.bsProizvCombIzdVZP;
            this.gridControlProizvCombIzdVZP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProizvCombIzdVZP.Font = new System.Drawing.Font("Arial", 10F);
            this.gridControlProizvCombIzdVZP.Location = new System.Drawing.Point(3, 22);
            this.gridControlProizvCombIzdVZP.MainView = this.gridViewProizvCombIzdVZP;
            this.gridControlProizvCombIzdVZP.Name = "gridControlProizvCombIzdVZP";
            this.gridControlProizvCombIzdVZP.ObjectName = null;
            this.gridControlProizvCombIzdVZP.Size = new System.Drawing.Size(1750, 272);
            this.gridControlProizvCombIzdVZP.TabIndex = 4;
            this.gridControlProizvCombIzdVZP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProizvCombIzdVZP});
            // 
            // gridViewProizvCombIzdVZP
            // 
            this.gridViewProizvCombIzdVZP.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gridViewProizvCombIzdVZP.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridViewProizvCombIzdVZP.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.gridViewProizvCombIzdVZP.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridViewProizvCombIzdVZP.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewProizvCombIzdVZP.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewProizvCombIzdVZP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnProizvCombIzdVZPRzvMod,
            this.gridColumnProizvCombIzdVZPPszNom,
            this.gridColumnProizvCombIzdVZPPszZvet,
            this.gridColumnProizvCombIzdVZPRzvArticul,
            this.gridColumnProizvCombIzdVZPRzvGrup,
            this.gridColumnProizvCombIzdVZPRzvRazm,
            this.gridColumnProizvCombIzdVZPKolItog,
            this.gridColumnProizvCombIzdVZPNIz,
            this.gridColumnProizvCombIzdVZPKolVyaz,
            this.gridColumnProizvCombIzdVZPKolOtparka,
            this.gridColumnProizvCombIzdVZPKolGI,
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl,
            this.gridColumnProizvCombIzdVZPRzvDateOkonV,
            this.gridColumnProizvCombIzdVZPNDostData,
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl});
            this.gridViewProizvCombIzdVZP.CustomizationFormBounds = new System.Drawing.Rectangle(3464, 607, 264, 272);
            this.gridViewProizvCombIzdVZP.GridControl = this.gridControlProizvCombIzdVZP;
            this.gridViewProizvCombIzdVZP.GroupCount = 3;
            this.gridViewProizvCombIzdVZP.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolVyaz", null, "(Вязание: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolOtparka", null, "(Отпарка: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", null, "(Сдано на склад всего: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", null, "(Принято на склад фурнитуры всего: {0:0.##})")});
            this.gridViewProizvCombIzdVZP.Name = "gridViewProizvCombIzdVZP";
            this.gridViewProizvCombIzdVZP.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewProizvCombIzdVZP.OptionsView.ShowFooter = true;
            this.gridViewProizvCombIzdVZP.OptionsView.ShowGroupPanel = false;
            this.gridViewProizvCombIzdVZP.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnProizvCombIzdVZPPszZvet, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnProizvCombIzdVZPPszNom, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnProizvCombIzdVZPNIz, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumnProizvCombIzdVZPRzvMod
            // 
            this.gridColumnProizvCombIzdVZPRzvMod.Caption = "Модель";
            this.gridColumnProizvCombIzdVZPRzvMod.Name = "gridColumnProizvCombIzdVZPRzvMod";
            this.gridColumnProizvCombIzdVZPRzvMod.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPRzvMod.OptionsEditForm.Caption = "Psz Mod:";
            this.gridColumnProizvCombIzdVZPRzvMod.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPRzvMod.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPRzvMod.Visible = true;
            this.gridColumnProizvCombIzdVZPRzvMod.VisibleIndex = 2;
            this.gridColumnProizvCombIzdVZPRzvMod.Width = 100;
            // 
            // gridColumnProizvCombIzdVZPPszNom
            // 
            this.gridColumnProizvCombIzdVZPPszNom.Caption = "Задание";
            this.gridColumnProizvCombIzdVZPPszNom.Name = "gridColumnProizvCombIzdVZPPszNom";
            this.gridColumnProizvCombIzdVZPPszNom.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPPszNom.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPPszNom.Visible = true;
            this.gridColumnProizvCombIzdVZPPszNom.VisibleIndex = 0;
            // 
            // gridColumnProizvCombIzdVZPPszZvet
            // 
            this.gridColumnProizvCombIzdVZPPszZvet.Caption = "Цвет";
            this.gridColumnProizvCombIzdVZPPszZvet.Name = "gridColumnProizvCombIzdVZPPszZvet";
            this.gridColumnProizvCombIzdVZPPszZvet.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPPszZvet.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPPszZvet.Visible = true;
            this.gridColumnProizvCombIzdVZPPszZvet.VisibleIndex = 0;
            // 
            // gridColumnProizvCombIzdVZPRzvArticul
            // 
            this.gridColumnProizvCombIzdVZPRzvArticul.Caption = "Артикул";
            this.gridColumnProizvCombIzdVZPRzvArticul.Name = "gridColumnProizvCombIzdVZPRzvArticul";
            this.gridColumnProizvCombIzdVZPRzvArticul.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPRzvArticul.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPRzvArticul.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPRzvArticul.Visible = true;
            this.gridColumnProizvCombIzdVZPRzvArticul.VisibleIndex = 1;
            this.gridColumnProizvCombIzdVZPRzvArticul.Width = 100;
            // 
            // gridColumnProizvCombIzdVZPRzvGrup
            // 
            this.gridColumnProizvCombIzdVZPRzvGrup.Caption = "Группа";
            this.gridColumnProizvCombIzdVZPRzvGrup.Name = "gridColumnProizvCombIzdVZPRzvGrup";
            this.gridColumnProizvCombIzdVZPRzvGrup.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPRzvGrup.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPRzvGrup.Visible = true;
            this.gridColumnProizvCombIzdVZPRzvGrup.VisibleIndex = 0;
            this.gridColumnProizvCombIzdVZPRzvGrup.Width = 299;
            // 
            // gridColumnProizvCombIzdVZPRzvRazm
            // 
            this.gridColumnProizvCombIzdVZPRzvRazm.Caption = "Размер";
            this.gridColumnProizvCombIzdVZPRzvRazm.Name = "gridColumnProizvCombIzdVZPRzvRazm";
            this.gridColumnProizvCombIzdVZPRzvRazm.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPRzvRazm.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPRzvRazm.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPRzvRazm.Visible = true;
            this.gridColumnProizvCombIzdVZPRzvRazm.VisibleIndex = 3;
            this.gridColumnProizvCombIzdVZPRzvRazm.Width = 80;
            // 
            // gridColumnProizvCombIzdVZPKolItog
            // 
            this.gridColumnProizvCombIzdVZPKolItog.Caption = "Кол-во";
            this.gridColumnProizvCombIzdVZPKolItog.Name = "gridColumnProizvCombIzdVZPKolItog";
            this.gridColumnProizvCombIzdVZPKolItog.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPKolItog.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPKolItog.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPKolItog.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolItog", "ИТОГО = {0:0.##}")});
            this.gridColumnProizvCombIzdVZPKolItog.Visible = true;
            this.gridColumnProizvCombIzdVZPKolItog.VisibleIndex = 4;
            this.gridColumnProizvCombIzdVZPKolItog.Width = 90;
            // 
            // gridColumnProizvCombIzdVZPNIz
            // 
            this.gridColumnProizvCombIzdVZPNIz.Caption = "№ накладной";
            this.gridColumnProizvCombIzdVZPNIz.Name = "gridColumnProizvCombIzdVZPNIz";
            this.gridColumnProizvCombIzdVZPNIz.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPNIz.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPNIz.Visible = true;
            this.gridColumnProizvCombIzdVZPNIz.VisibleIndex = 0;
            // 
            // gridColumnProizvCombIzdVZPKolVyaz
            // 
            this.gridColumnProizvCombIzdVZPKolVyaz.Caption = "Кол-во на вязании";
            this.gridColumnProizvCombIzdVZPKolVyaz.Name = "gridColumnProizvCombIzdVZPKolVyaz";
            this.gridColumnProizvCombIzdVZPKolVyaz.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPKolVyaz.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPKolVyaz.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPKolVyaz.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolVyaz", "Вязание: {0:0.##}")});
            this.gridColumnProizvCombIzdVZPKolVyaz.Visible = true;
            this.gridColumnProizvCombIzdVZPKolVyaz.VisibleIndex = 5;
            this.gridColumnProizvCombIzdVZPKolVyaz.Width = 150;
            // 
            // gridColumnProizvCombIzdVZPKolOtparka
            // 
            this.gridColumnProizvCombIzdVZPKolOtparka.Caption = "Кол-во на отпарке";
            this.gridColumnProizvCombIzdVZPKolOtparka.Name = "gridColumnProizvCombIzdVZPKolOtparka";
            this.gridColumnProizvCombIzdVZPKolOtparka.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPKolOtparka.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPKolOtparka.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPKolOtparka.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolOtparka", "Отпарка: {0:0.##}")});
            this.gridColumnProizvCombIzdVZPKolOtparka.Visible = true;
            this.gridColumnProizvCombIzdVZPKolOtparka.VisibleIndex = 6;
            this.gridColumnProizvCombIzdVZPKolOtparka.Width = 150;
            // 
            // gridColumnProizvCombIzdVZPKolGI
            // 
            this.gridColumnProizvCombIzdVZPKolGI.Caption = "Кол-во отгр. на склад";
            this.gridColumnProizvCombIzdVZPKolGI.Name = "gridColumnProizvCombIzdVZPKolGI";
            this.gridColumnProizvCombIzdVZPKolGI.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPKolGI.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPKolGI.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPKolGI.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", "Отгр. на скл.: {0:0.##}")});
            this.gridColumnProizvCombIzdVZPKolGI.Visible = true;
            this.gridColumnProizvCombIzdVZPKolGI.VisibleIndex = 8;
            this.gridColumnProizvCombIzdVZPKolGI.Width = 150;
            // 
            // gridColumnProizvCombIzdVZPKolFurnPrinSkl
            // 
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.Caption = "Кол-во прин. на скл. фурн.";
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.Name = "gridColumnProizvCombIzdVZPKolFurnPrinSkl";
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", "Прин. на скл. Фурн.: {0:0.##}")});
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.Visible = true;
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.VisibleIndex = 10;
            this.gridColumnProizvCombIzdVZPKolFurnPrinSkl.Width = 150;
            // 
            // gridColumnProizvCombIzdVZPRzvDateOkonV
            // 
            this.gridColumnProizvCombIzdVZPRzvDateOkonV.Caption = "Дата отпарки";
            this.gridColumnProizvCombIzdVZPRzvDateOkonV.Name = "gridColumnProizvCombIzdVZPRzvDateOkonV";
            this.gridColumnProizvCombIzdVZPRzvDateOkonV.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPRzvDateOkonV.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPRzvDateOkonV.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPRzvDateOkonV.Visible = true;
            this.gridColumnProizvCombIzdVZPRzvDateOkonV.VisibleIndex = 7;
            this.gridColumnProizvCombIzdVZPRzvDateOkonV.Width = 90;
            // 
            // gridColumnProizvCombIzdVZPNDostData
            // 
            this.gridColumnProizvCombIzdVZPNDostData.Caption = "Дата отгр. на склад";
            this.gridColumnProizvCombIzdVZPNDostData.Name = "gridColumnProizvCombIzdVZPNDostData";
            this.gridColumnProizvCombIzdVZPNDostData.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPNDostData.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPNDostData.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPNDostData.Visible = true;
            this.gridColumnProizvCombIzdVZPNDostData.VisibleIndex = 9;
            this.gridColumnProizvCombIzdVZPNDostData.Width = 90;
            // 
            // gridColumnProizvCombIzdVZPDateFurnPrihSkl
            // 
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl.Caption = "Дата прин. на скл. фурн.";
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl.Name = "gridColumnProizvCombIzdVZPDateFurnPrihSkl";
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl.OptionsColumn.FixedWidth = true;
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl.OptionsFilter.AllowFilter = false;
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl.Visible = true;
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl.VisibleIndex = 11;
            this.gridColumnProizvCombIzdVZPDateFurnPrihSkl.Width = 90;
            // 
            // gridViewNaklList
            // 
            this.gridViewNaklList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewNaklList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewNaklList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNaklCountBefore,
            this.gridColumnNaklPrich,
            this.gridColumnNaklSklNaimen,
            this.gridColumnNaklGlNomer,
            this.gridColumnNaklDatePrint,
            this.gridColumnNaklDostN,
            this.gridColumnNaklDostData,
            this.gridColumnNaklDateIzm,
            this.gridColumnNaklIzDate,
            this.gridColumnNaklIzNakl,
            this.gridColumnNaklCountAfter,
            this.gridColumnNaklChipOtgr,
            this.gridColumnNaklChipScan,
            this.gridColumnNaklChipPech,
            this.gridColumnNaklChipInUT,
            this.gridColumnNaklMod,
            this.gridColumnNaklArticul,
            this.gridColumn40,
            this.gridColumn44,
            this.gridColumn45,
            this.gridColumn46});
            gridFormatRule1.Column = this.gridColumn40;
            gridFormatRule1.ColumnApplyTo = this.gridColumnNaklChipInUT;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue1.Appearance.Options.UseFont = true;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = "red";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.gridColumn40;
            gridFormatRule2.ColumnApplyTo = this.gridColumnNaklChipInUT;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue2.Appearance.Options.UseFont = true;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = "green";
            gridFormatRule2.Rule = formatConditionRuleValue2;
            gridFormatRule3.Column = this.gridColumn44;
            gridFormatRule3.ColumnApplyTo = this.gridColumnNaklChipPech;
            gridFormatRule3.Name = "Format2";
            formatConditionRuleValue3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue3.Appearance.Options.UseFont = true;
            formatConditionRuleValue3.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue3.Value1 = "green";
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = this.gridColumn44;
            gridFormatRule4.ColumnApplyTo = this.gridColumnNaklChipPech;
            gridFormatRule4.Name = "Format3";
            formatConditionRuleValue4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue4.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue4.Appearance.Options.UseFont = true;
            formatConditionRuleValue4.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = "red";
            gridFormatRule4.Rule = formatConditionRuleValue4;
            gridFormatRule5.Column = this.gridColumn44;
            gridFormatRule5.ColumnApplyTo = this.gridColumnNaklChipPech;
            gridFormatRule5.Name = "Format4";
            formatConditionRuleValue5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue5.Appearance.ForeColor = System.Drawing.Color.Gray;
            formatConditionRuleValue5.Appearance.Options.UseFont = true;
            formatConditionRuleValue5.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue5.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue5.Value1 = "gray";
            gridFormatRule5.Rule = formatConditionRuleValue5;
            gridFormatRule6.Column = this.gridColumn45;
            gridFormatRule6.ColumnApplyTo = this.gridColumnNaklChipScan;
            gridFormatRule6.Name = "Format5";
            formatConditionRuleValue6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue6.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue6.Appearance.Options.UseFont = true;
            formatConditionRuleValue6.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue6.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue6.Value1 = "red";
            gridFormatRule6.Rule = formatConditionRuleValue6;
            gridFormatRule7.Column = this.gridColumn45;
            gridFormatRule7.ColumnApplyTo = this.gridColumnNaklChipScan;
            gridFormatRule7.Name = "Format6";
            formatConditionRuleValue7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue7.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue7.Appearance.Options.UseFont = true;
            formatConditionRuleValue7.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue7.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue7.Value1 = "green";
            gridFormatRule7.Rule = formatConditionRuleValue7;
            gridFormatRule8.Column = this.gridColumn45;
            gridFormatRule8.ColumnApplyTo = this.gridColumnNaklChipScan;
            gridFormatRule8.Name = "Format7";
            formatConditionRuleValue8.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue8.Appearance.ForeColor = System.Drawing.Color.Gray;
            formatConditionRuleValue8.Appearance.Options.UseFont = true;
            formatConditionRuleValue8.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue8.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue8.Value1 = "gray";
            gridFormatRule8.Rule = formatConditionRuleValue8;
            gridFormatRule9.Column = this.gridColumn46;
            gridFormatRule9.ColumnApplyTo = this.gridColumnNaklChipOtgr;
            gridFormatRule9.Name = "Format8";
            formatConditionRuleValue9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue9.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue9.Appearance.Options.UseFont = true;
            formatConditionRuleValue9.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue9.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue9.Value1 = "red";
            gridFormatRule9.Rule = formatConditionRuleValue9;
            gridFormatRule10.Column = this.gridColumn46;
            gridFormatRule10.ColumnApplyTo = this.gridColumnNaklChipOtgr;
            gridFormatRule10.Name = "Format9";
            formatConditionRuleValue10.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue10.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue10.Appearance.Options.UseFont = true;
            formatConditionRuleValue10.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue10.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue10.Value1 = "green";
            gridFormatRule10.Rule = formatConditionRuleValue10;
            gridFormatRule11.Column = this.gridColumn46;
            gridFormatRule11.ColumnApplyTo = this.gridColumnNaklChipOtgr;
            gridFormatRule11.Name = "Format10";
            formatConditionRuleValue11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue11.Appearance.ForeColor = System.Drawing.Color.Gray;
            formatConditionRuleValue11.Appearance.Options.UseFont = true;
            formatConditionRuleValue11.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue11.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue11.Value1 = "gray";
            gridFormatRule11.Rule = formatConditionRuleValue11;
            this.gridViewNaklList.FormatRules.Add(gridFormatRule1);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule2);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule3);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule4);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule5);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule6);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule7);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule8);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule9);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule10);
            this.gridViewNaklList.FormatRules.Add(gridFormatRule11);
            this.gridViewNaklList.GridControl = this.gridControlNaklList;
            this.gridViewNaklList.Name = "gridViewNaklList";
            this.gridViewNaklList.OptionsBehavior.Editable = false;
            this.gridViewNaklList.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gridViewNaklList.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewNaklList.OptionsView.RowAutoHeight = true;
            this.gridViewNaklList.OptionsView.ShowGroupPanel = false;
            this.gridViewNaklList.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            // 
            // gridColumnNaklCountBefore
            // 
            this.gridColumnNaklCountBefore.Caption = "Кол-во ДО";
            this.gridColumnNaklCountBefore.Name = "gridColumnNaklCountBefore";
            this.gridColumnNaklCountBefore.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklCountBefore.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklCountBefore.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklCountBefore.Visible = true;
            this.gridColumnNaklCountBefore.VisibleIndex = 9;
            this.gridColumnNaklCountBefore.Width = 60;
            // 
            // gridColumnNaklPrich
            // 
            this.gridColumnNaklPrich.Caption = "Причина деления";
            this.gridColumnNaklPrich.Name = "gridColumnNaklPrich";
            this.gridColumnNaklPrich.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPrich.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPrich.Visible = true;
            this.gridColumnNaklPrich.VisibleIndex = 8;
            this.gridColumnNaklPrich.Width = 38;
            // 
            // gridColumnNaklSklNaimen
            // 
            this.gridColumnNaklSklNaimen.Caption = "Склад отгрузки";
            this.gridColumnNaklSklNaimen.Name = "gridColumnNaklSklNaimen";
            this.gridColumnNaklSklNaimen.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklSklNaimen.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklSklNaimen.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklSklNaimen.Visible = true;
            this.gridColumnNaklSklNaimen.VisibleIndex = 7;
            this.gridColumnNaklSklNaimen.Width = 150;
            // 
            // gridColumnNaklGlNomer
            // 
            this.gridColumnNaklGlNomer.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklGlNomer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklGlNomer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumnNaklGlNomer.Caption = "№ накл. Глобал";
            this.gridColumnNaklGlNomer.Name = "gridColumnNaklGlNomer";
            this.gridColumnNaklGlNomer.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklGlNomer.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklGlNomer.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklGlNomer.Visible = true;
            this.gridColumnNaklGlNomer.VisibleIndex = 6;
            this.gridColumnNaklGlNomer.Width = 70;
            // 
            // gridColumnNaklDatePrint
            // 
            this.gridColumnNaklDatePrint.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklDatePrint.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklDatePrint.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumnNaklDatePrint.Caption = "Дата печати";
            this.gridColumnNaklDatePrint.Name = "gridColumnNaklDatePrint";
            this.gridColumnNaklDatePrint.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklDatePrint.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklDatePrint.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklDatePrint.Visible = true;
            this.gridColumnNaklDatePrint.VisibleIndex = 5;
            this.gridColumnNaklDatePrint.Width = 90;
            // 
            // gridColumnNaklDostN
            // 
            this.gridColumnNaklDostN.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklDostN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklDostN.Caption = "№ отгр.";
            this.gridColumnNaklDostN.Name = "gridColumnNaklDostN";
            this.gridColumnNaklDostN.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklDostN.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklDostN.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklDostN.Visible = true;
            this.gridColumnNaklDostN.VisibleIndex = 4;
            this.gridColumnNaklDostN.Width = 60;
            // 
            // gridColumnNaklDostData
            // 
            this.gridColumnNaklDostData.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklDostData.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklDostData.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumnNaklDostData.Caption = "Доставка на склад";
            this.gridColumnNaklDostData.Name = "gridColumnNaklDostData";
            this.gridColumnNaklDostData.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklDostData.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklDostData.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklDostData.Visible = true;
            this.gridColumnNaklDostData.VisibleIndex = 3;
            this.gridColumnNaklDostData.Width = 90;
            // 
            // gridColumnNaklDateIzm
            // 
            this.gridColumnNaklDateIzm.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklDateIzm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklDateIzm.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumnNaklDateIzm.Caption = "Дата деления";
            this.gridColumnNaklDateIzm.Name = "gridColumnNaklDateIzm";
            this.gridColumnNaklDateIzm.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklDateIzm.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklDateIzm.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklDateIzm.Visible = true;
            this.gridColumnNaklDateIzm.VisibleIndex = 2;
            this.gridColumnNaklDateIzm.Width = 90;
            // 
            // gridColumnNaklIzDate
            // 
            this.gridColumnNaklIzDate.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklIzDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklIzDate.Caption = "Дата накл.";
            this.gridColumnNaklIzDate.Name = "gridColumnNaklIzDate";
            this.gridColumnNaklIzDate.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklIzDate.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklIzDate.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklIzDate.Visible = true;
            this.gridColumnNaklIzDate.VisibleIndex = 1;
            this.gridColumnNaklIzDate.Width = 90;
            // 
            // gridColumnNaklIzNakl
            // 
            this.gridColumnNaklIzNakl.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklIzNakl.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklIzNakl.Caption = "№ накл.";
            this.gridColumnNaklIzNakl.FieldName = "IzNakl";
            this.gridColumnNaklIzNakl.Name = "gridColumnNaklIzNakl";
            this.gridColumnNaklIzNakl.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklIzNakl.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklIzNakl.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklIzNakl.Visible = true;
            this.gridColumnNaklIzNakl.VisibleIndex = 0;
            this.gridColumnNaklIzNakl.Width = 60;
            // 
            // gridColumnNaklCountAfter
            // 
            this.gridColumnNaklCountAfter.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNaklCountAfter.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNaklCountAfter.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumnNaklCountAfter.Caption = "Кол-во ПОСЛЕ";
            this.gridColumnNaklCountAfter.Name = "gridColumnNaklCountAfter";
            this.gridColumnNaklCountAfter.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklCountAfter.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklCountAfter.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklCountAfter.Visible = true;
            this.gridColumnNaklCountAfter.VisibleIndex = 10;
            this.gridColumnNaklCountAfter.Width = 60;
            // 
            // gridColumnNaklMod
            // 
            this.gridColumnNaklMod.Caption = "Модель";
            this.gridColumnNaklMod.Name = "gridColumnNaklMod";
            this.gridColumnNaklMod.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklMod.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklMod.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklMod.Visible = true;
            this.gridColumnNaklMod.VisibleIndex = 12;
            this.gridColumnNaklMod.Width = 120;
            // 
            // gridColumnNaklArticul
            // 
            this.gridColumnNaklArticul.Caption = "Артикул";
            this.gridColumnNaklArticul.Name = "gridColumnNaklArticul";
            this.gridColumnNaklArticul.OptionsColumn.FixedWidth = true;
            this.gridColumnNaklArticul.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklArticul.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklArticul.Visible = true;
            this.gridColumnNaklArticul.VisibleIndex = 11;
            this.gridColumnNaklArticul.Width = 120;
            // 
            // gridControlNaklList
            // 
            this.gridControlNaklList.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.gridControlNaklList.Font = new System.Drawing.Font("Arial", 10F);
            this.gridControlNaklList.Location = new System.Drawing.Point(-3, 5);
            this.gridControlNaklList.MainView = this.gridViewNaklList;
            this.gridControlNaklList.Name = "gridControlNaklList";
            this.gridControlNaklList.ObjectName = null;
            this.gridControlNaklList.Size = new System.Drawing.Size(1545, 137);
            this.gridControlNaklList.TabIndex = 5;
            this.gridControlNaklList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNaklList});
            // 
            // gridViewOtdelka
            // 
            this.gridViewOtdelka.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewOtdelka.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnOtdelkaViNaim,
            this.gridColumnOtdelkaCaption,
            this.gridColumnOtdelkaFrtNaimen,
            this.gridColumnOtdelkaKolSlZv,
            this.gridColumnOtdelkaPsaFieldName});
            this.gridViewOtdelka.GridControl = this.gridControlOtdelka;
            this.gridViewOtdelka.Name = "gridViewOtdelka";
            this.gridViewOtdelka.OptionsBehavior.Editable = false;
            this.gridViewOtdelka.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewOtdelka.OptionsView.RowAutoHeight = true;
            this.gridViewOtdelka.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnOtdelkaViNaim
            // 
            this.gridColumnOtdelkaViNaim.Caption = "Вид изделия";
            this.gridColumnOtdelkaViNaim.Name = "gridColumnOtdelkaViNaim";
            this.gridColumnOtdelkaViNaim.OptionsColumn.FixedWidth = true;
            this.gridColumnOtdelkaViNaim.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnOtdelkaViNaim.OptionsFilter.AllowFilter = false;
            this.gridColumnOtdelkaViNaim.Visible = true;
            this.gridColumnOtdelkaViNaim.VisibleIndex = 4;
            this.gridColumnOtdelkaViNaim.Width = 120;
            // 
            // gridColumnOtdelkaCaption
            // 
            this.gridColumnOtdelkaCaption.Caption = "Деталь изделия";
            this.gridColumnOtdelkaCaption.Name = "gridColumnOtdelkaCaption";
            this.gridColumnOtdelkaCaption.OptionsColumn.FixedWidth = true;
            this.gridColumnOtdelkaCaption.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnOtdelkaCaption.OptionsFilter.AllowFilter = false;
            this.gridColumnOtdelkaCaption.Visible = true;
            this.gridColumnOtdelkaCaption.VisibleIndex = 3;
            this.gridColumnOtdelkaCaption.Width = 100;
            // 
            // gridColumnOtdelkaFrtNaimen
            // 
            this.gridColumnOtdelkaFrtNaimen.Caption = "Формат";
            this.gridColumnOtdelkaFrtNaimen.Name = "gridColumnOtdelkaFrtNaimen";
            this.gridColumnOtdelkaFrtNaimen.OptionsColumn.FixedWidth = true;
            this.gridColumnOtdelkaFrtNaimen.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnOtdelkaFrtNaimen.OptionsFilter.AllowFilter = false;
            this.gridColumnOtdelkaFrtNaimen.Visible = true;
            this.gridColumnOtdelkaFrtNaimen.VisibleIndex = 2;
            this.gridColumnOtdelkaFrtNaimen.Width = 50;
            // 
            // gridColumnOtdelkaKolSlZv
            // 
            this.gridColumnOtdelkaKolSlZv.Caption = "Кол-во/слож/цвет/прогон";
            this.gridColumnOtdelkaKolSlZv.Name = "gridColumnOtdelkaKolSlZv";
            this.gridColumnOtdelkaKolSlZv.OptionsColumn.FixedWidth = true;
            this.gridColumnOtdelkaKolSlZv.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnOtdelkaKolSlZv.OptionsFilter.AllowFilter = false;
            this.gridColumnOtdelkaKolSlZv.Visible = true;
            this.gridColumnOtdelkaKolSlZv.VisibleIndex = 1;
            this.gridColumnOtdelkaKolSlZv.Width = 140;
            // 
            // gridColumnOtdelkaPsaFieldName
            // 
            this.gridColumnOtdelkaPsaFieldName.Caption = "Вид отделки";
            this.gridColumnOtdelkaPsaFieldName.Name = "gridColumnOtdelkaPsaFieldName";
            this.gridColumnOtdelkaPsaFieldName.OptionsColumn.FixedWidth = true;
            this.gridColumnOtdelkaPsaFieldName.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnOtdelkaPsaFieldName.OptionsFilter.AllowFilter = false;
            this.gridColumnOtdelkaPsaFieldName.Visible = true;
            this.gridColumnOtdelkaPsaFieldName.VisibleIndex = 0;
            this.gridColumnOtdelkaPsaFieldName.Width = 90;
            // 
            // gridControlOtdelka
            // 
            this.gridControlOtdelka.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.tablePanel5.SetColumn(this.gridControlOtdelka, 18);
            this.gridControlOtdelka.Font = new System.Drawing.Font("Arial", 10F);
            this.gridControlOtdelka.Location = new System.Drawing.Point(1023, 12);
            this.gridControlOtdelka.MainView = this.gridViewOtdelka;
            this.gridControlOtdelka.Name = "gridControlOtdelka";
            this.gridControlOtdelka.ObjectName = null;
            this.tablePanel5.SetRow(this.gridControlOtdelka, 0);
            this.tablePanel5.SetRowSpan(this.gridControlOtdelka, 6);
            this.gridControlOtdelka.Size = new System.Drawing.Size(533, 149);
            this.gridControlOtdelka.TabIndex = 5;
            this.gridControlOtdelka.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOtdelka});
            // 
            // WorkInfo
            // 
            this.WorkInfo.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.WorkInfo.Appearance.HeaderActive.Options.UseFont = true;
            this.WorkInfo.Name = "WorkInfo";
            this.WorkInfo.Size = new System.Drawing.Size(1756, 601);
            this.WorkInfo.Text = "ВЫПОЛНЕННАЯ РАБОТА";
            // 
            // FurnInfo
            // 
            this.FurnInfo.Controls.Add(this.splitContainer1);
            this.FurnInfo.Controls.Add(this.customGroupBox6);
            this.FurnInfo.Controls.Add(this.panelControl7);
            this.FurnInfo.Name = "FurnInfo";
            this.FurnInfo.Size = new System.Drawing.Size(1756, 601);
            this.FurnInfo.Text = "КОНФЕКЦИОН";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(234, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.furnitZayavViewFurnit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.furnitZayavViewUpak);
            this.splitContainer1.Size = new System.Drawing.Size(1338, 592);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.TabIndex = 6;
            // 
            // furnitZayavViewFurnit
            // 
            this.furnitZayavViewFurnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.furnitZayavViewFurnit.Location = new System.Drawing.Point(0, 0);
            this.furnitZayavViewFurnit.Name = "furnitZayavViewFurnit";
            this.furnitZayavViewFurnit.Size = new System.Drawing.Size(1338, 296);
            this.furnitZayavViewFurnit.TabIndex = 4;
            this.furnitZayavViewFurnit.ViewType = "";
            // 
            // furnitZayavViewUpak
            // 
            this.furnitZayavViewUpak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.furnitZayavViewUpak.Location = new System.Drawing.Point(0, 0);
            this.furnitZayavViewUpak.Name = "furnitZayavViewUpak";
            this.furnitZayavViewUpak.Size = new System.Drawing.Size(1338, 292);
            this.furnitZayavViewUpak.TabIndex = 3;
            this.furnitZayavViewUpak.ViewType = "";
            // 
            // customGroupBox6
            // 
            this.customGroupBox6.BackColor = System.Drawing.Color.Transparent;
            this.customGroupBox6.BorderColor = System.Drawing.Color.Black;
            this.customGroupBox6.BorderThickness = 1;
            this.customGroupBox6.Controls.Add(this.tablePanel7);
            this.customGroupBox6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customGroupBox6.Location = new System.Drawing.Point(1, 3);
            this.customGroupBox6.Name = "customGroupBox6";
            this.customGroupBox6.ObjectName = null;
            this.customGroupBox6.Size = new System.Drawing.Size(230, 595);
            this.customGroupBox6.TabIndex = 5;
            this.customGroupBox6.TabStop = false;
            this.customGroupBox6.Text = "УСЛОВИЯ ДЛЯ СОЗДАНИЯ ЗАЯВОК";
            // 
            // tablePanel7
            // 
            this.tablePanel7.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 72F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 58F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 14F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 20F)});
            this.tablePanel7.Controls.Add(this.simpleButtonFullKKPrint);
            this.tablePanel7.Controls.Add(this.simpleButtonUpakDeliveryInfoShow);
            this.tablePanel7.Controls.Add(this.simpleButtonFurnDeliveryInfoShow);
            this.tablePanel7.Controls.Add(this.simpleButtonZayavUpakPrint);
            this.tablePanel7.Controls.Add(this.simpleButtonUpakKKPrint);
            this.tablePanel7.Controls.Add(this.simpleButtonZayavFurnPrint);
            this.tablePanel7.Controls.Add(this.simpleButtonFurnKKPrint);
            this.tablePanel7.Controls.Add(this.tbDatZayav);
            this.tablePanel7.Controls.Add(this.tbOtgrStat);
            this.tablePanel7.Controls.Add(this.mtbData_cd);
            this.tablePanel7.Controls.Add(this.label61);
            this.tablePanel7.Controls.Add(this.mtbData_zeh);
            this.tablePanel7.Controls.Add(this.label60);
            this.tablePanel7.Controls.Add(this.tbIs_got);
            this.tablePanel7.Controls.Add(this.tbData_f_z_u);
            this.tablePanel7.Controls.Add(this.tbFurnKKStat);
            this.tablePanel7.Controls.Add(this.tbData_f_o_u);
            this.tablePanel7.Controls.Add(this.label59);
            this.tablePanel7.Controls.Add(this.tbData_f_z);
            this.tablePanel7.Controls.Add(this.tbUpakKKStat);
            this.tablePanel7.Controls.Add(this.tbUZSobrStat);
            this.tablePanel7.Controls.Add(this.tbData_f_o);
            this.tablePanel7.Controls.Add(this.label25);
            this.tablePanel7.Controls.Add(this.label56);
            this.tablePanel7.Controls.Add(this.tbFurnZayav);
            this.tablePanel7.Controls.Add(this.tbUZSozdStat);
            this.tablePanel7.Controls.Add(this.label54);
            this.tablePanel7.Controls.Add(this.tbFZSozdStat);
            this.tablePanel7.Controls.Add(this.label55);
            this.tablePanel7.Controls.Add(this.label57);
            this.tablePanel7.Controls.Add(this.tbFZSobrStat);
            this.tablePanel7.Controls.Add(this.tbUpakZayav);
            this.tablePanel7.Controls.Add(this.label58);
            this.tablePanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel7.Location = new System.Drawing.Point(3, 22);
            this.tablePanel7.Name = "tablePanel7";
            this.tablePanel7.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 45F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel7.Size = new System.Drawing.Size(224, 570);
            this.tablePanel7.TabIndex = 0;
            this.tablePanel7.UseSkinIndents = true;
            // 
            // simpleButtonFullKKPrint
            // 
            this.simpleButtonFullKKPrint.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonFullKKPrint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonFullKKPrint.Appearance.Options.UseFont = true;
            this.simpleButtonFullKKPrint.Appearance.Options.UseForeColor = true;
            this.tablePanel7.SetColumn(this.simpleButtonFullKKPrint, 0);
            this.tablePanel7.SetColumnSpan(this.simpleButtonFullKKPrint, 6);
            this.simpleButtonFullKKPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButtonFullKKPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonFullKKPrint.ImageOptions.Image")));
            this.simpleButtonFullKKPrint.Location = new System.Drawing.Point(13, 12);
            this.simpleButtonFullKKPrint.Name = "simpleButtonFullKKPrint";
            this.tablePanel7.SetRow(this.simpleButtonFullKKPrint, 0);
            this.simpleButtonFullKKPrint.Size = new System.Drawing.Size(198, 36);
            this.simpleButtonFullKKPrint.TabIndex = 78;
            this.simpleButtonFullKKPrint.Text = "КК общая (просмотр/печать)";
            this.simpleButtonFullKKPrint.Click += new System.EventHandler(this.simpleButtonFullKKPrint_Click_1);
            // 
            // simpleButtonUpakDeliveryInfoShow
            // 
            this.simpleButtonUpakDeliveryInfoShow.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonUpakDeliveryInfoShow.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonUpakDeliveryInfoShow.Appearance.Options.UseFont = true;
            this.simpleButtonUpakDeliveryInfoShow.Appearance.Options.UseForeColor = true;
            this.tablePanel7.SetColumn(this.simpleButtonUpakDeliveryInfoShow, 0);
            this.tablePanel7.SetColumnSpan(this.simpleButtonUpakDeliveryInfoShow, 6);
            this.simpleButtonUpakDeliveryInfoShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButtonUpakDeliveryInfoShow.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonUpakDeliveryInfoShow.ImageOptions.Image")));
            this.simpleButtonUpakDeliveryInfoShow.Location = new System.Drawing.Point(13, 376);
            this.simpleButtonUpakDeliveryInfoShow.Name = "simpleButtonUpakDeliveryInfoShow";
            this.tablePanel7.SetRow(this.simpleButtonUpakDeliveryInfoShow, 15);
            this.simpleButtonUpakDeliveryInfoShow.Size = new System.Drawing.Size(198, 21);
            this.simpleButtonUpakDeliveryInfoShow.TabIndex = 77;
            this.simpleButtonUpakDeliveryInfoShow.Text = "Инфо по доставке упак";
            this.simpleButtonUpakDeliveryInfoShow.Click += new System.EventHandler(this.simpleButtonUpakDeliveryInfoShow_Click);
            // 
            // simpleButtonFurnDeliveryInfoShow
            // 
            this.simpleButtonFurnDeliveryInfoShow.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonFurnDeliveryInfoShow.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonFurnDeliveryInfoShow.Appearance.Options.UseBackColor = true;
            this.simpleButtonFurnDeliveryInfoShow.Appearance.Options.UseFont = true;
            this.simpleButtonFurnDeliveryInfoShow.Appearance.Options.UseForeColor = true;
            this.tablePanel7.SetColumn(this.simpleButtonFurnDeliveryInfoShow, 0);
            this.tablePanel7.SetColumnSpan(this.simpleButtonFurnDeliveryInfoShow, 6);
            this.simpleButtonFurnDeliveryInfoShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButtonFurnDeliveryInfoShow.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonFurnDeliveryInfoShow.ImageOptions.Image")));
            this.simpleButtonFurnDeliveryInfoShow.Location = new System.Drawing.Point(13, 195);
            this.simpleButtonFurnDeliveryInfoShow.Name = "simpleButtonFurnDeliveryInfoShow";
            this.tablePanel7.SetRow(this.simpleButtonFurnDeliveryInfoShow, 7);
            this.simpleButtonFurnDeliveryInfoShow.Size = new System.Drawing.Size(198, 22);
            this.simpleButtonFurnDeliveryInfoShow.TabIndex = 76;
            this.simpleButtonFurnDeliveryInfoShow.Text = "Инфо по доставке фурн";
            this.simpleButtonFurnDeliveryInfoShow.Click += new System.EventHandler(this.simpleButtonFurnUpakDeliveryInfoShow_Click);
            // 
            // simpleButtonZayavUpakPrint
            // 
            this.simpleButtonZayavUpakPrint.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonZayavUpakPrint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonZayavUpakPrint.Appearance.Options.UseFont = true;
            this.simpleButtonZayavUpakPrint.Appearance.Options.UseForeColor = true;
            this.tablePanel7.SetColumn(this.simpleButtonZayavUpakPrint, 0);
            this.tablePanel7.SetColumnSpan(this.simpleButtonZayavUpakPrint, 6);
            this.simpleButtonZayavUpakPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButtonZayavUpakPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonZayavUpakPrint.ImageOptions.Image")));
            this.simpleButtonZayavUpakPrint.Location = new System.Drawing.Point(13, 351);
            this.simpleButtonZayavUpakPrint.Name = "simpleButtonZayavUpakPrint";
            this.tablePanel7.SetRow(this.simpleButtonZayavUpakPrint, 14);
            this.simpleButtonZayavUpakPrint.Size = new System.Drawing.Size(198, 21);
            this.simpleButtonZayavUpakPrint.TabIndex = 75;
            this.simpleButtonZayavUpakPrint.Text = "ПРОСМОТР / ПЕЧАТЬ";
            this.simpleButtonZayavUpakPrint.Click += new System.EventHandler(this.simpleButtonZayavUpakPrint_Click);
            // 
            // simpleButtonUpakKKPrint
            // 
            this.simpleButtonUpakKKPrint.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonUpakKKPrint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonUpakKKPrint.Appearance.Options.UseFont = true;
            this.simpleButtonUpakKKPrint.Appearance.Options.UseForeColor = true;
            this.tablePanel7.SetColumn(this.simpleButtonUpakKKPrint, 0);
            this.tablePanel7.SetColumnSpan(this.simpleButtonUpakKKPrint, 5);
            this.simpleButtonUpakKKPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButtonUpakKKPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonUpakKKPrint.ImageOptions.Image")));
            this.simpleButtonUpakKKPrint.Location = new System.Drawing.Point(13, 245);
            this.simpleButtonUpakKKPrint.Name = "simpleButtonUpakKKPrint";
            this.tablePanel7.SetRow(this.simpleButtonUpakKKPrint, 10);
            this.simpleButtonUpakKKPrint.Size = new System.Drawing.Size(173, 36);
            this.simpleButtonUpakKKPrint.TabIndex = 74;
            this.simpleButtonUpakKKPrint.Text = "КК на упаковку \r\n(просмотр/печать)";
            this.simpleButtonUpakKKPrint.Click += new System.EventHandler(this.simpleButtonUpakKKPrint_Click);
            // 
            // simpleButtonZayavFurnPrint
            // 
            this.simpleButtonZayavFurnPrint.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonZayavFurnPrint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonZayavFurnPrint.Appearance.Options.UseFont = true;
            this.simpleButtonZayavFurnPrint.Appearance.Options.UseForeColor = true;
            this.tablePanel7.SetColumn(this.simpleButtonZayavFurnPrint, 0);
            this.tablePanel7.SetColumnSpan(this.simpleButtonZayavFurnPrint, 6);
            this.simpleButtonZayavFurnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButtonZayavFurnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonZayavFurnPrint.ImageOptions.Image")));
            this.simpleButtonZayavFurnPrint.Location = new System.Drawing.Point(13, 170);
            this.simpleButtonZayavFurnPrint.Name = "simpleButtonZayavFurnPrint";
            this.tablePanel7.SetRow(this.simpleButtonZayavFurnPrint, 6);
            this.simpleButtonZayavFurnPrint.Size = new System.Drawing.Size(198, 21);
            this.simpleButtonZayavFurnPrint.TabIndex = 73;
            this.simpleButtonZayavFurnPrint.Text = "ПРОСМОТР / ПЕЧАТЬ";
            this.simpleButtonZayavFurnPrint.Click += new System.EventHandler(this.simpleButtonZayavFurnPrint_Click);
            // 
            // simpleButtonFurnKKPrint
            // 
            this.simpleButtonFurnKKPrint.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonFurnKKPrint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonFurnKKPrint.Appearance.Options.UseFont = true;
            this.simpleButtonFurnKKPrint.Appearance.Options.UseForeColor = true;
            this.tablePanel7.SetColumn(this.simpleButtonFurnKKPrint, 0);
            this.tablePanel7.SetColumnSpan(this.simpleButtonFurnKKPrint, 5);
            this.simpleButtonFurnKKPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButtonFurnKKPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonFurnKKPrint.ImageOptions.Image")));
            this.simpleButtonFurnKKPrint.Location = new System.Drawing.Point(13, 64);
            this.simpleButtonFurnKKPrint.Name = "simpleButtonFurnKKPrint";
            this.tablePanel7.SetRow(this.simpleButtonFurnKKPrint, 2);
            this.simpleButtonFurnKKPrint.Size = new System.Drawing.Size(173, 36);
            this.simpleButtonFurnKKPrint.TabIndex = 72;
            this.simpleButtonFurnKKPrint.Text = "КК на фурнитуру (просмотр/печать)";
            this.simpleButtonFurnKKPrint.Click += new System.EventHandler(this.simpleButtonFurnKKPrint_Click);
            // 
            // tbDatZayav
            // 
            this.tbDatZayav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbDatZayav, 1);
            this.tablePanel7.SetColumnSpan(this.tbDatZayav, 5);
            this.tbDatZayav.Font = new System.Drawing.Font("Arial", 10F);
            this.tbDatZayav.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbDatZayav.Location = new System.Drawing.Point(83, 528);
            this.tbDatZayav.Margin = new System.Windows.Forms.Padding(0);
            this.tbDatZayav.Name = "tbDatZayav";
            this.tbDatZayav.ObjectName = null;
            this.tablePanel7.SetRow(this.tbDatZayav, 21);
            this.tbDatZayav.Size = new System.Drawing.Size(130, 23);
            this.tbDatZayav.TabIndex = 70;
            // 
            // tbOtgrStat
            // 
            this.tbOtgrStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbOtgrStat, 5);
            this.tbOtgrStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbOtgrStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbOtgrStat.Location = new System.Drawing.Point(188, 436);
            this.tbOtgrStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbOtgrStat.Name = "tbOtgrStat";
            this.tbOtgrStat.ObjectName = null;
            this.tablePanel7.SetRow(this.tbOtgrStat, 18);
            this.tbOtgrStat.Size = new System.Drawing.Size(25, 23);
            this.tbOtgrStat.TabIndex = 59;
            this.tbOtgrStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mtbData_cd
            // 
            this.mtbData_cd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.mtbData_cd, 1);
            this.tablePanel7.SetColumnSpan(this.mtbData_cd, 3);
            this.mtbData_cd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbData_cd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbData_cd.Location = new System.Drawing.Point(85, 437);
            this.mtbData_cd.Mask = "00/00/0000";
            this.mtbData_cd.Name = "mtbData_cd";
            this.mtbData_cd.ObjectName = null;
            this.tablePanel7.SetRow(this.mtbData_cd, 18);
            this.mtbData_cd.Size = new System.Drawing.Size(93, 21);
            this.mtbData_cd.TabIndex = 65;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label61, 0);
            this.tablePanel7.SetColumnSpan(this.label61, 6);
            this.label61.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label61.Font = new System.Drawing.Font("Arial", 9F);
            this.label61.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label61.Location = new System.Drawing.Point(14, 475);
            this.label61.Name = "label61";
            this.label61.ObjectName = null;
            this.tablePanel7.SetRow(this.label61, 20);
            this.label61.Size = new System.Drawing.Size(196, 45);
            this.label61.TabIndex = 0;
            this.label61.Text = "Предположительная дата создания заявки (при выполнении всех условий)";
            this.label61.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mtbData_zeh
            // 
            this.mtbData_zeh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.mtbData_zeh, 1);
            this.tablePanel7.SetColumnSpan(this.mtbData_zeh, 3);
            this.mtbData_zeh.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbData_zeh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbData_zeh.Location = new System.Drawing.Point(85, 413);
            this.mtbData_zeh.Mask = "00/00/0000";
            this.mtbData_zeh.Name = "mtbData_zeh";
            this.mtbData_zeh.ObjectName = null;
            this.tablePanel7.SetRow(this.mtbData_zeh, 17);
            this.mtbData_zeh.Size = new System.Drawing.Size(93, 21);
            this.mtbData_zeh.TabIndex = 64;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label60, 0);
            this.label60.Font = new System.Drawing.Font("Arial", 9F);
            this.label60.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label60.Location = new System.Drawing.Point(14, 433);
            this.label60.Name = "label60";
            this.label60.ObjectName = null;
            this.tablePanel7.SetRow(this.label60, 18);
            this.label60.Size = new System.Drawing.Size(61, 30);
            this.label60.TabIndex = 55;
            this.label60.Text = "Дата отгрузки \r\nс производства";
            // 
            // tbIs_got
            // 
            this.tbIs_got.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbIs_got, 5);
            this.tbIs_got.Font = new System.Drawing.Font("Arial", 10F);
            this.tbIs_got.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbIs_got.Location = new System.Drawing.Point(188, 411);
            this.tbIs_got.Margin = new System.Windows.Forms.Padding(0);
            this.tbIs_got.Name = "tbIs_got";
            this.tbIs_got.ObjectName = null;
            this.tablePanel7.SetRow(this.tbIs_got, 17);
            this.tbIs_got.Size = new System.Drawing.Size(25, 23);
            this.tbIs_got.TabIndex = 58;
            this.tbIs_got.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbData_f_z_u
            // 
            this.tbData_f_z_u.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbData_f_z_u, 1);
            this.tablePanel7.SetColumnSpan(this.tbData_f_z_u, 3);
            this.tbData_f_z_u.Font = new System.Drawing.Font("Arial", 10F);
            this.tbData_f_z_u.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbData_f_z_u.Location = new System.Drawing.Point(83, 327);
            this.tbData_f_z_u.Margin = new System.Windows.Forms.Padding(0);
            this.tbData_f_z_u.Name = "tbData_f_z_u";
            this.tbData_f_z_u.ObjectName = null;
            this.tablePanel7.SetRow(this.tbData_f_z_u, 13);
            this.tbData_f_z_u.Size = new System.Drawing.Size(97, 23);
            this.tbData_f_z_u.TabIndex = 69;
            // 
            // tbFurnKKStat
            // 
            this.tbFurnKKStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbFurnKKStat, 5);
            this.tbFurnKKStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbFurnKKStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbFurnKKStat.Location = new System.Drawing.Point(188, 70);
            this.tbFurnKKStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbFurnKKStat.Name = "tbFurnKKStat";
            this.tbFurnKKStat.ObjectName = null;
            this.tablePanel7.SetRow(this.tbFurnKKStat, 2);
            this.tbFurnKKStat.Size = new System.Drawing.Size(25, 23);
            this.tbFurnKKStat.TabIndex = 33;
            this.tbFurnKKStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbData_f_o_u
            // 
            this.tbData_f_o_u.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbData_f_o_u, 1);
            this.tablePanel7.SetColumnSpan(this.tbData_f_o_u, 3);
            this.tbData_f_o_u.Font = new System.Drawing.Font("Arial", 10F);
            this.tbData_f_o_u.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbData_f_o_u.Location = new System.Drawing.Point(83, 305);
            this.tbData_f_o_u.Margin = new System.Windows.Forms.Padding(0);
            this.tbData_f_o_u.Name = "tbData_f_o_u";
            this.tbData_f_o_u.ObjectName = null;
            this.tablePanel7.SetRow(this.tbData_f_o_u, 12);
            this.tbData_f_o_u.Size = new System.Drawing.Size(97, 23);
            this.tbData_f_o_u.TabIndex = 68;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label59, 0);
            this.label59.Font = new System.Drawing.Font("Arial", 9F);
            this.label59.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label59.Location = new System.Drawing.Point(14, 414);
            this.label59.Name = "label59";
            this.label59.ObjectName = null;
            this.tablePanel7.SetRow(this.label59, 17);
            this.label59.Size = new System.Drawing.Size(65, 15);
            this.label59.TabIndex = 54;
            this.label59.Text = "Дата в цех";
            // 
            // tbData_f_z
            // 
            this.tbData_f_z.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbData_f_z, 1);
            this.tablePanel7.SetColumnSpan(this.tbData_f_z, 3);
            this.tbData_f_z.Font = new System.Drawing.Font("Arial", 10F);
            this.tbData_f_z.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbData_f_z.Location = new System.Drawing.Point(83, 146);
            this.tbData_f_z.Margin = new System.Windows.Forms.Padding(0);
            this.tbData_f_z.Name = "tbData_f_z";
            this.tbData_f_z.ObjectName = null;
            this.tablePanel7.SetRow(this.tbData_f_z, 5);
            this.tbData_f_z.Size = new System.Drawing.Size(97, 23);
            this.tbData_f_z.TabIndex = 67;
            // 
            // tbUpakKKStat
            // 
            this.tbUpakKKStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbUpakKKStat, 5);
            this.tbUpakKKStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbUpakKKStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbUpakKKStat.Location = new System.Drawing.Point(188, 251);
            this.tbUpakKKStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbUpakKKStat.Name = "tbUpakKKStat";
            this.tbUpakKKStat.ObjectName = null;
            this.tablePanel7.SetRow(this.tbUpakKKStat, 10);
            this.tbUpakKKStat.Size = new System.Drawing.Size(25, 23);
            this.tbUpakKKStat.TabIndex = 34;
            this.tbUpakKKStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbUZSobrStat
            // 
            this.tbUZSobrStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbUZSobrStat, 5);
            this.tbUZSobrStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbUZSobrStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbUZSobrStat.Location = new System.Drawing.Point(188, 327);
            this.tbUZSobrStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbUZSobrStat.Name = "tbUZSobrStat";
            this.tbUZSobrStat.ObjectName = null;
            this.tablePanel7.SetRow(this.tbUZSobrStat, 13);
            this.tbUZSobrStat.Size = new System.Drawing.Size(25, 23);
            this.tbUZSobrStat.TabIndex = 52;
            this.tbUZSobrStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbData_f_o
            // 
            this.tbData_f_o.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbData_f_o, 1);
            this.tablePanel7.SetColumnSpan(this.tbData_f_o, 3);
            this.tbData_f_o.Font = new System.Drawing.Font("Arial", 10F);
            this.tbData_f_o.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbData_f_o.Location = new System.Drawing.Point(83, 124);
            this.tbData_f_o.Margin = new System.Windows.Forms.Padding(0);
            this.tbData_f_o.Name = "tbData_f_o";
            this.tbData_f_o.ObjectName = null;
            this.tablePanel7.SetRow(this.tbData_f_o, 4);
            this.tbData_f_o.Size = new System.Drawing.Size(97, 23);
            this.tbData_f_o.TabIndex = 66;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label25, 0);
            this.tablePanel7.SetColumnSpan(this.label25, 4);
            this.label25.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label25.Location = new System.Drawing.Point(14, 106);
            this.label25.Name = "label25";
            this.label25.ObjectName = null;
            this.tablePanel7.SetRow(this.label25, 3);
            this.label25.Size = new System.Drawing.Size(150, 13);
            this.label25.TabIndex = 36;
            this.label25.Text = "Заявка на фурнитуру №";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label56, 0);
            this.label56.Font = new System.Drawing.Font("Arial", 9F);
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label56.Location = new System.Drawing.Point(14, 330);
            this.label56.Name = "label56";
            this.label56.ObjectName = null;
            this.tablePanel7.SetRow(this.label56, 13);
            this.label56.Size = new System.Drawing.Size(55, 15);
            this.label56.TabIndex = 49;
            this.label56.Text = "собрана";
            // 
            // tbFurnZayav
            // 
            this.tbFurnZayav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbFurnZayav, 3);
            this.tablePanel7.SetColumnSpan(this.tbFurnZayav, 3);
            this.tbFurnZayav.Font = new System.Drawing.Font("Arial", 10F);
            this.tbFurnZayav.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbFurnZayav.Location = new System.Drawing.Point(166, 102);
            this.tbFurnZayav.Margin = new System.Windows.Forms.Padding(0);
            this.tbFurnZayav.Name = "tbFurnZayav";
            this.tbFurnZayav.ObjectName = null;
            this.tablePanel7.SetRow(this.tbFurnZayav, 3);
            this.tbFurnZayav.Size = new System.Drawing.Size(47, 23);
            this.tbFurnZayav.TabIndex = 37;
            // 
            // tbUZSozdStat
            // 
            this.tbUZSozdStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbUZSozdStat, 5);
            this.tbUZSozdStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbUZSozdStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbUZSozdStat.Location = new System.Drawing.Point(188, 305);
            this.tbUZSozdStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbUZSozdStat.Name = "tbUZSozdStat";
            this.tbUZSozdStat.ObjectName = null;
            this.tablePanel7.SetRow(this.tbUZSozdStat, 12);
            this.tbUZSozdStat.Size = new System.Drawing.Size(25, 23);
            this.tbUZSozdStat.TabIndex = 48;
            this.tbUZSozdStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label54, 0);
            this.label54.Font = new System.Drawing.Font("Arial", 9F);
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label54.Location = new System.Drawing.Point(14, 127);
            this.label54.Name = "label54";
            this.label54.ObjectName = null;
            this.tablePanel7.SetRow(this.label54, 4);
            this.label54.Size = new System.Drawing.Size(54, 15);
            this.label54.TabIndex = 38;
            this.label54.Text = "создана";
            // 
            // tbFZSozdStat
            // 
            this.tbFZSozdStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbFZSozdStat, 5);
            this.tbFZSozdStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbFZSozdStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbFZSozdStat.Location = new System.Drawing.Point(188, 124);
            this.tbFZSozdStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbFZSozdStat.Name = "tbFZSozdStat";
            this.tbFZSozdStat.ObjectName = null;
            this.tablePanel7.SetRow(this.tbFZSozdStat, 4);
            this.tbFZSozdStat.Size = new System.Drawing.Size(25, 23);
            this.tbFZSozdStat.TabIndex = 39;
            this.tbFZSozdStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label55, 0);
            this.label55.Font = new System.Drawing.Font("Arial", 9F);
            this.label55.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label55.Location = new System.Drawing.Point(14, 149);
            this.label55.Name = "label55";
            this.label55.ObjectName = null;
            this.tablePanel7.SetRow(this.label55, 5);
            this.label55.Size = new System.Drawing.Size(55, 15);
            this.label55.TabIndex = 40;
            this.label55.Text = "собрана";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label57, 0);
            this.label57.Font = new System.Drawing.Font("Arial", 9F);
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label57.Location = new System.Drawing.Point(14, 308);
            this.label57.Name = "label57";
            this.label57.ObjectName = null;
            this.tablePanel7.SetRow(this.label57, 12);
            this.label57.Size = new System.Drawing.Size(54, 15);
            this.label57.TabIndex = 47;
            this.label57.Text = "создана";
            // 
            // tbFZSobrStat
            // 
            this.tbFZSobrStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbFZSobrStat, 5);
            this.tbFZSobrStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbFZSobrStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbFZSobrStat.Location = new System.Drawing.Point(188, 146);
            this.tbFZSobrStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbFZSobrStat.Name = "tbFZSobrStat";
            this.tbFZSobrStat.ObjectName = null;
            this.tablePanel7.SetRow(this.tbFZSobrStat, 5);
            this.tbFZSobrStat.Size = new System.Drawing.Size(25, 23);
            this.tbFZSobrStat.TabIndex = 43;
            this.tbFZSobrStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbUpakZayav
            // 
            this.tbUpakZayav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel7.SetColumn(this.tbUpakZayav, 3);
            this.tablePanel7.SetColumnSpan(this.tbUpakZayav, 3);
            this.tbUpakZayav.Font = new System.Drawing.Font("Arial", 10F);
            this.tbUpakZayav.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbUpakZayav.Location = new System.Drawing.Point(166, 283);
            this.tbUpakZayav.Margin = new System.Windows.Forms.Padding(0);
            this.tbUpakZayav.Name = "tbUpakZayav";
            this.tbUpakZayav.ObjectName = null;
            this.tablePanel7.SetRow(this.tbUpakZayav, 11);
            this.tbUpakZayav.Size = new System.Drawing.Size(47, 23);
            this.tbUpakZayav.TabIndex = 46;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel7.SetColumn(this.label58, 0);
            this.tablePanel7.SetColumnSpan(this.label58, 4);
            this.label58.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label58.Location = new System.Drawing.Point(14, 287);
            this.label58.Name = "label58";
            this.label58.ObjectName = null;
            this.tablePanel7.SetRow(this.label58, 11);
            this.label58.Size = new System.Drawing.Size(142, 13);
            this.label58.TabIndex = 45;
            this.label58.Text = "Заявка на упаковку №";
            // 
            // panelControl7
            // 
            this.panelControl7.Location = new System.Drawing.Point(3, 489);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(210, 83);
            this.panelControl7.TabIndex = 1;
            // 
            // RasInfo
            // 
            this.RasInfo.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RasInfo.Appearance.Header.Options.UseFont = true;
            this.RasInfo.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.RasInfo.Appearance.HeaderActive.Options.UseFont = true;
            this.RasInfo.Appearance.HeaderDisabled.Font = new System.Drawing.Font("Tahoma", 10F);
            this.RasInfo.Appearance.HeaderDisabled.Options.UseFont = true;
            this.RasInfo.Controls.Add(this.customGroupBox5);
            this.RasInfo.Controls.Add(this.customGroupBox4);
            this.RasInfo.Controls.Add(this.customGroupBox3);
            this.RasInfo.Controls.Add(this.ContDates);
            this.RasInfo.Name = "RasInfo";
            this.RasInfo.Size = new System.Drawing.Size(1756, 601);
            this.RasInfo.Text = "ИНФОРМАЦИЯ ПО РАСЧЕТУ";
            // 
            // customGroupBox5
            // 
            this.customGroupBox5.BackColor = System.Drawing.Color.Transparent;
            this.customGroupBox5.BorderColor = System.Drawing.Color.Black;
            this.customGroupBox5.BorderThickness = 1;
            this.customGroupBox5.Controls.Add(this.tablePanel6);
            this.customGroupBox5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customGroupBox5.Location = new System.Drawing.Point(2, 522);
            this.customGroupBox5.Name = "customGroupBox5";
            this.customGroupBox5.ObjectName = null;
            this.customGroupBox5.Size = new System.Drawing.Size(1575, 76);
            this.customGroupBox5.TabIndex = 9;
            this.customGroupBox5.TabStop = false;
            this.customGroupBox5.Text = "ДОКУМЕНТЫ";
            // 
            // tablePanel6
            // 
            this.tablePanel6.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 230F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 230F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 230F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F)});
            this.tablePanel6.Controls.Add(this.simpleButtonReestrListPrint);
            this.tablePanel6.Controls.Add(this.simpleButtonPrintMLRTUpak);
            this.tablePanel6.Controls.Add(this.simpleButtonPrintMLRTAll);
            this.tablePanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel6.Location = new System.Drawing.Point(3, 22);
            this.tablePanel6.Name = "tablePanel6";
            this.tablePanel6.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel6.Size = new System.Drawing.Size(1569, 51);
            this.tablePanel6.TabIndex = 0;
            this.tablePanel6.UseSkinIndents = true;
            // 
            // simpleButtonReestrListPrint
            // 
            this.simpleButtonReestrListPrint.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonReestrListPrint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonReestrListPrint.Appearance.Options.UseFont = true;
            this.simpleButtonReestrListPrint.Appearance.Options.UseForeColor = true;
            this.tablePanel6.SetColumn(this.simpleButtonReestrListPrint, 4);
            this.simpleButtonReestrListPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonReestrListPrint.ImageOptions.Image")));
            this.simpleButtonReestrListPrint.Location = new System.Drawing.Point(513, 12);
            this.simpleButtonReestrListPrint.Name = "simpleButtonReestrListPrint";
            this.tablePanel6.SetRow(this.simpleButtonReestrListPrint, 0);
            this.simpleButtonReestrListPrint.Size = new System.Drawing.Size(226, 25);
            this.simpleButtonReestrListPrint.TabIndex = 11;
            this.simpleButtonReestrListPrint.Text = "Сопроводительные реестры";
            this.simpleButtonReestrListPrint.Click += new System.EventHandler(this.simpleButtonReestrListPrint_Click);
            // 
            // simpleButtonPrintMLRTUpak
            // 
            this.simpleButtonPrintMLRTUpak.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonPrintMLRTUpak.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonPrintMLRTUpak.Appearance.Options.UseFont = true;
            this.simpleButtonPrintMLRTUpak.Appearance.Options.UseForeColor = true;
            this.tablePanel6.SetColumn(this.simpleButtonPrintMLRTUpak, 2);
            this.simpleButtonPrintMLRTUpak.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonPrintMLRTUpak.ImageOptions.Image")));
            this.simpleButtonPrintMLRTUpak.Location = new System.Drawing.Point(263, 12);
            this.simpleButtonPrintMLRTUpak.Name = "simpleButtonPrintMLRTUpak";
            this.tablePanel6.SetRow(this.simpleButtonPrintMLRTUpak, 0);
            this.simpleButtonPrintMLRTUpak.Size = new System.Drawing.Size(226, 25);
            this.simpleButtonPrintMLRTUpak.TabIndex = 10;
            this.simpleButtonPrintMLRTUpak.Text = "Задание упак.";
            this.simpleButtonPrintMLRTUpak.Click += new System.EventHandler(this.simpleButtonPrintMLRTUpak_Click);
            // 
            // simpleButtonPrintMLRTAll
            // 
            this.simpleButtonPrintMLRTAll.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonPrintMLRTAll.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonPrintMLRTAll.Appearance.Options.UseFont = true;
            this.simpleButtonPrintMLRTAll.Appearance.Options.UseForeColor = true;
            this.tablePanel6.SetColumn(this.simpleButtonPrintMLRTAll, 0);
            this.simpleButtonPrintMLRTAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonPrintMLRTAll.ImageOptions.Image")));
            this.simpleButtonPrintMLRTAll.Location = new System.Drawing.Point(13, 12);
            this.simpleButtonPrintMLRTAll.Name = "simpleButtonPrintMLRTAll";
            this.tablePanel6.SetRow(this.simpleButtonPrintMLRTAll, 0);
            this.simpleButtonPrintMLRTAll.Size = new System.Drawing.Size(226, 25);
            this.simpleButtonPrintMLRTAll.TabIndex = 9;
            this.simpleButtonPrintMLRTAll.Text = "Задание общ.";
            this.simpleButtonPrintMLRTAll.Click += new System.EventHandler(this.simpleButtonPrintMLRTAll_Click);
            // 
            // customGroupBox4
            // 
            this.customGroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.customGroupBox4.BorderColor = System.Drawing.Color.Black;
            this.customGroupBox4.BorderThickness = 1;
            this.customGroupBox4.Controls.Add(this.tablePanel5);
            this.customGroupBox4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customGroupBox4.Location = new System.Drawing.Point(2, 317);
            this.customGroupBox4.Name = "customGroupBox4";
            this.customGroupBox4.ObjectName = null;
            this.customGroupBox4.Size = new System.Drawing.Size(1575, 199);
            this.customGroupBox4.TabIndex = 12;
            this.customGroupBox4.TabStop = false;
            this.customGroupBox4.Text = "ОТДЕЛКА / ДОП. ОБРАБОТКА";
            // 
            // tablePanel5
            // 
            this.tablePanel5.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 95F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F)});
            this.tablePanel5.Controls.Add(this.mtbRzuVidStir);
            this.tablePanel5.Controls.Add(this.cbRzuStirFact);
            this.tablePanel5.Controls.Add(this.label44);
            this.tablePanel5.Controls.Add(this.mtbRzuDataStCd);
            this.tablePanel5.Controls.Add(this.label32);
            this.tablePanel5.Controls.Add(this.mtbRzuDataStR);
            this.tablePanel5.Controls.Add(this.label49);
            this.tablePanel5.Controls.Add(this.cbPszStirPlan);
            this.tablePanel5.Controls.Add(this.mtbRzuDataStP);
            this.tablePanel5.Controls.Add(this.cbPszPrintPlan);
            this.tablePanel5.Controls.Add(this.label50);
            this.tablePanel5.Controls.Add(this.mtbRzuDataVCd);
            this.tablePanel5.Controls.Add(this.cbRzuVishFact);
            this.tablePanel5.Controls.Add(this.cbRzuPrintFact);
            this.tablePanel5.Controls.Add(this.label51);
            this.tablePanel5.Controls.Add(this.mtbRzuDataVChi);
            this.tablePanel5.Controls.Add(this.cbPszVishPlan);
            this.tablePanel5.Controls.Add(this.label37);
            this.tablePanel5.Controls.Add(this.mtbRzuDataVR);
            this.tablePanel5.Controls.Add(this.mtbRzuDataPrCd);
            this.tablePanel5.Controls.Add(this.label35);
            this.tablePanel5.Controls.Add(this.mtbRzuDataRasp);
            this.tablePanel5.Controls.Add(this.mtbRzuDataVP);
            this.tablePanel5.Controls.Add(this.label43);
            this.tablePanel5.Controls.Add(this.label38);
            this.tablePanel5.Controls.Add(this.mtbRzuDataPrKm);
            this.tablePanel5.Controls.Add(this.mtbRzuDataRasv);
            this.tablePanel5.Controls.Add(this.label45);
            this.tablePanel5.Controls.Add(this.mtbRzuDataPrP);
            this.tablePanel5.Controls.Add(this.label39);
            this.tablePanel5.Controls.Add(this.mtbRzuDataPrR);
            this.tablePanel5.Controls.Add(this.label46);
            this.tablePanel5.Controls.Add(this.mtbRzuDataPrPe);
            this.tablePanel5.Controls.Add(this.label40);
            this.tablePanel5.Controls.Add(this.label41);
            this.tablePanel5.Controls.Add(this.label47);
            this.tablePanel5.Controls.Add(this.label42);
            this.tablePanel5.Controls.Add(this.gridControlOtdelka);
            this.tablePanel5.Controls.Add(this.label36);
            this.tablePanel5.Controls.Add(this.label48);
            this.tablePanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel5.Location = new System.Drawing.Point(3, 22);
            this.tablePanel5.Name = "tablePanel5";
            this.tablePanel5.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 21F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 21F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 21F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F)});
            this.tablePanel5.Size = new System.Drawing.Size(1569, 174);
            this.tablePanel5.TabIndex = 0;
            this.tablePanel5.UseSkinIndents = true;
            // 
            // mtbRzuVidStir
            // 
            this.mtbRzuVidStir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuVidStir, 10);
            this.mtbRzuVidStir.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuVidStir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuVidStir.Location = new System.Drawing.Point(648, 137);
            this.mtbRzuVidStir.Mask = "00/00/0000";
            this.mtbRzuVidStir.Name = "mtbRzuVidStir";
            this.mtbRzuVidStir.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuVidStir, 5);
            this.mtbRzuVidStir.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuVidStir.TabIndex = 63;
            // 
            // cbRzuStirFact
            // 
            this.cbRzuStirFact.AutoSize = true;
            this.tablePanel5.SetColumn(this.cbRzuStirFact, 4);
            this.cbRzuStirFact.Font = new System.Drawing.Font("Arial", 10F);
            this.cbRzuStirFact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbRzuStirFact.Location = new System.Drawing.Point(283, 114);
            this.cbRzuStirFact.Name = "cbRzuStirFact";
            this.cbRzuStirFact.ObjectName = null;
            this.tablePanel5.SetRow(this.cbRzuStirFact, 4);
            this.cbRzuStirFact.Size = new System.Drawing.Size(58, 17);
            this.cbRzuStirFact.TabIndex = 72;
            this.cbRzuStirFact.Text = "Факт";
            this.cbRzuStirFact.UseVisualStyleBackColor = true;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label44, 9);
            this.label44.Font = new System.Drawing.Font("Arial", 9F);
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label44.Location = new System.Drawing.Point(554, 140);
            this.label44.Name = "label44";
            this.label44.ObjectName = null;
            this.tablePanel5.SetRow(this.label44, 5);
            this.label44.Size = new System.Drawing.Size(70, 15);
            this.label44.TabIndex = 52;
            this.label44.Text = "Вид стирки";
            // 
            // mtbRzuDataStCd
            // 
            this.mtbRzuDataStCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataStCd, 7);
            this.mtbRzuDataStCd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataStCd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataStCd.Location = new System.Drawing.Point(463, 137);
            this.mtbRzuDataStCd.Mask = "00/00/0000";
            this.mtbRzuDataStCd.Name = "mtbRzuDataStCd";
            this.mtbRzuDataStCd.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataStCd, 5);
            this.mtbRzuDataStCd.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataStCd.TabIndex = 60;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label32, 0);
            this.label32.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label32.Location = new System.Drawing.Point(14, 12);
            this.label32.Name = "label32";
            this.label32.ObjectName = null;
            this.tablePanel5.SetRow(this.label32, 0);
            this.label32.Size = new System.Drawing.Size(54, 16);
            this.label32.TabIndex = 9;
            this.label32.Text = "ПРИНТ";
            // 
            // mtbRzuDataStR
            // 
            this.mtbRzuDataStR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataStR, 4);
            this.mtbRzuDataStR.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataStR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataStR.Location = new System.Drawing.Point(283, 137);
            this.mtbRzuDataStR.Mask = "00/00/0000";
            this.mtbRzuDataStR.Name = "mtbRzuDataStR";
            this.mtbRzuDataStR.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataStR, 5);
            this.mtbRzuDataStR.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataStR.TabIndex = 57;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label49, 6);
            this.label49.Font = new System.Drawing.Font("Arial", 9F);
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label49.Location = new System.Drawing.Point(374, 140);
            this.label49.Name = "label49";
            this.label49.ObjectName = null;
            this.tablePanel5.SetRow(this.label49, 5);
            this.label49.Size = new System.Drawing.Size(70, 15);
            this.label49.TabIndex = 50;
            this.label49.Text = "Дата сдачи";
            // 
            // cbPszStirPlan
            // 
            this.cbPszStirPlan.AutoSize = true;
            this.tablePanel5.SetColumn(this.cbPszStirPlan, 3);
            this.cbPszStirPlan.Font = new System.Drawing.Font("Arial", 10F);
            this.cbPszStirPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbPszStirPlan.Location = new System.Drawing.Point(193, 114);
            this.cbPszStirPlan.Name = "cbPszStirPlan";
            this.cbPszStirPlan.ObjectName = null;
            this.tablePanel5.SetRow(this.cbPszStirPlan, 4);
            this.cbPszStirPlan.Size = new System.Drawing.Size(59, 17);
            this.cbPszStirPlan.TabIndex = 71;
            this.cbPszStirPlan.Text = "План";
            this.cbPszStirPlan.UseVisualStyleBackColor = true;
            // 
            // mtbRzuDataStP
            // 
            this.mtbRzuDataStP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataStP, 1);
            this.mtbRzuDataStP.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataStP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataStP.Location = new System.Drawing.Point(103, 137);
            this.mtbRzuDataStP.Mask = "00/00/0000";
            this.mtbRzuDataStP.Name = "mtbRzuDataStP";
            this.mtbRzuDataStP.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataStP, 5);
            this.mtbRzuDataStP.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataStP.TabIndex = 54;
            // 
            // cbPszPrintPlan
            // 
            this.cbPszPrintPlan.AutoSize = true;
            this.tablePanel5.SetColumn(this.cbPszPrintPlan, 3);
            this.cbPszPrintPlan.Font = new System.Drawing.Font("Arial", 10F);
            this.cbPszPrintPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbPszPrintPlan.Location = new System.Drawing.Point(193, 12);
            this.cbPszPrintPlan.Name = "cbPszPrintPlan";
            this.cbPszPrintPlan.ObjectName = null;
            this.tablePanel5.SetRow(this.cbPszPrintPlan, 0);
            this.cbPszPrintPlan.Size = new System.Drawing.Size(59, 17);
            this.cbPszPrintPlan.TabIndex = 67;
            this.cbPszPrintPlan.Text = "План";
            this.cbPszPrintPlan.UseVisualStyleBackColor = true;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label50, 3);
            this.label50.Font = new System.Drawing.Font("Arial", 9F);
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label50.Location = new System.Drawing.Point(194, 140);
            this.label50.Name = "label50";
            this.label50.ObjectName = null;
            this.tablePanel5.SetRow(this.label50, 5);
            this.label50.Size = new System.Drawing.Size(75, 15);
            this.label50.TabIndex = 48;
            this.label50.Text = "Дата стирки";
            // 
            // mtbRzuDataVCd
            // 
            this.mtbRzuDataVCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataVCd, 13);
            this.mtbRzuDataVCd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataVCd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataVCd.Location = new System.Drawing.Point(793, 86);
            this.mtbRzuDataVCd.Mask = "00/00/0000";
            this.mtbRzuDataVCd.Name = "mtbRzuDataVCd";
            this.mtbRzuDataVCd.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataVCd, 3);
            this.mtbRzuDataVCd.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataVCd.TabIndex = 65;
            // 
            // cbRzuVishFact
            // 
            this.cbRzuVishFact.AutoSize = true;
            this.tablePanel5.SetColumn(this.cbRzuVishFact, 4);
            this.cbRzuVishFact.Font = new System.Drawing.Font("Arial", 10F);
            this.cbRzuVishFact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbRzuVishFact.Location = new System.Drawing.Point(283, 63);
            this.cbRzuVishFact.Name = "cbRzuVishFact";
            this.cbRzuVishFact.ObjectName = null;
            this.tablePanel5.SetRow(this.cbRzuVishFact, 2);
            this.cbRzuVishFact.Size = new System.Drawing.Size(58, 17);
            this.cbRzuVishFact.TabIndex = 70;
            this.cbRzuVishFact.Text = "Факт";
            this.cbRzuVishFact.UseVisualStyleBackColor = true;
            // 
            // cbRzuPrintFact
            // 
            this.cbRzuPrintFact.AutoSize = true;
            this.tablePanel5.SetColumn(this.cbRzuPrintFact, 4);
            this.cbRzuPrintFact.Font = new System.Drawing.Font("Arial", 10F);
            this.cbRzuPrintFact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbRzuPrintFact.Location = new System.Drawing.Point(283, 12);
            this.cbRzuPrintFact.Name = "cbRzuPrintFact";
            this.cbRzuPrintFact.ObjectName = null;
            this.tablePanel5.SetRow(this.cbRzuPrintFact, 0);
            this.cbRzuPrintFact.Size = new System.Drawing.Size(58, 17);
            this.cbRzuPrintFact.TabIndex = 68;
            this.cbRzuPrintFact.Text = "Факт";
            this.cbRzuPrintFact.UseVisualStyleBackColor = true;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label51, 0);
            this.label51.Font = new System.Drawing.Font("Arial", 9F);
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label51.Location = new System.Drawing.Point(14, 133);
            this.label51.Name = "label51";
            this.label51.ObjectName = null;
            this.tablePanel5.SetRow(this.label51, 5);
            this.label51.Size = new System.Drawing.Size(84, 30);
            this.label51.TabIndex = 46;
            this.label51.Text = "Дата принято\r\nна стирку";
            // 
            // mtbRzuDataVChi
            // 
            this.mtbRzuDataVChi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataVChi, 10);
            this.mtbRzuDataVChi.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataVChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataVChi.Location = new System.Drawing.Point(648, 86);
            this.mtbRzuDataVChi.Mask = "00/00/0000";
            this.mtbRzuDataVChi.Name = "mtbRzuDataVChi";
            this.mtbRzuDataVChi.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataVChi, 3);
            this.mtbRzuDataVChi.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataVChi.TabIndex = 62;
            // 
            // cbPszVishPlan
            // 
            this.cbPszVishPlan.AutoSize = true;
            this.tablePanel5.SetColumn(this.cbPszVishPlan, 3);
            this.cbPszVishPlan.Font = new System.Drawing.Font("Arial", 10F);
            this.cbPszVishPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbPszVishPlan.Location = new System.Drawing.Point(193, 63);
            this.cbPszVishPlan.Name = "cbPszVishPlan";
            this.cbPszVishPlan.ObjectName = null;
            this.tablePanel5.SetRow(this.cbPszVishPlan, 2);
            this.cbPszVishPlan.Size = new System.Drawing.Size(59, 17);
            this.cbPszVishPlan.TabIndex = 69;
            this.cbPszVishPlan.Text = "План";
            this.cbPszVishPlan.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label37, 0);
            this.label37.Font = new System.Drawing.Font("Arial", 9F);
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label37.Location = new System.Drawing.Point(14, 31);
            this.label37.Name = "label37";
            this.label37.ObjectName = null;
            this.tablePanel5.SetRow(this.label37, 1);
            this.label37.Size = new System.Drawing.Size(57, 30);
            this.label37.TabIndex = 20;
            this.label37.Text = "Дата\r\nна принт";
            // 
            // mtbRzuDataVR
            // 
            this.mtbRzuDataVR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataVR, 7);
            this.mtbRzuDataVR.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataVR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataVR.Location = new System.Drawing.Point(463, 86);
            this.mtbRzuDataVR.Mask = "00/00/0000";
            this.mtbRzuDataVR.Name = "mtbRzuDataVR";
            this.mtbRzuDataVR.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataVR, 3);
            this.mtbRzuDataVR.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataVR.TabIndex = 59;
            // 
            // mtbRzuDataPrCd
            // 
            this.mtbRzuDataPrCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataPrCd, 16);
            this.mtbRzuDataPrCd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrCd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrCd.Location = new System.Drawing.Point(933, 35);
            this.mtbRzuDataPrCd.Mask = "00/00/0000";
            this.mtbRzuDataPrCd.Name = "mtbRzuDataPrCd";
            this.mtbRzuDataPrCd.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataPrCd, 1);
            this.mtbRzuDataPrCd.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrCd.TabIndex = 66;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label35, 0);
            this.tablePanel5.SetColumnSpan(this.label35, 2);
            this.label35.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label35.Location = new System.Drawing.Point(14, 114);
            this.label35.Name = "label35";
            this.label35.ObjectName = null;
            this.tablePanel5.SetRow(this.label35, 4);
            this.label35.Size = new System.Drawing.Size(62, 16);
            this.label35.TabIndex = 11;
            this.label35.Text = "СТИРКА";
            // 
            // mtbRzuDataRasp
            // 
            this.mtbRzuDataRasp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataRasp, 1);
            this.mtbRzuDataRasp.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataRasp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataRasp.Location = new System.Drawing.Point(103, 35);
            this.mtbRzuDataRasp.Mask = "00/00/0000";
            this.mtbRzuDataRasp.Name = "mtbRzuDataRasp";
            this.mtbRzuDataRasp.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataRasp, 1);
            this.mtbRzuDataRasp.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataRasp.TabIndex = 38;
            // 
            // mtbRzuDataVP
            // 
            this.mtbRzuDataVP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataVP, 4);
            this.mtbRzuDataVP.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataVP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataVP.Location = new System.Drawing.Point(283, 86);
            this.mtbRzuDataVP.Mask = "00/00/0000";
            this.mtbRzuDataVP.Name = "mtbRzuDataVP";
            this.mtbRzuDataVP.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataVP, 3);
            this.mtbRzuDataVP.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataVP.TabIndex = 56;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label43, 12);
            this.label43.Font = new System.Drawing.Font("Arial", 9F);
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label43.Location = new System.Drawing.Point(739, 82);
            this.label43.Name = "label43";
            this.label43.ObjectName = null;
            this.tablePanel5.SetRow(this.label43, 3);
            this.label43.Size = new System.Drawing.Size(40, 30);
            this.label43.TabIndex = 42;
            this.label43.Text = "Дата\r\nсдачи";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label38, 3);
            this.label38.Font = new System.Drawing.Font("Arial", 9F);
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label38.Location = new System.Drawing.Point(194, 31);
            this.label38.Name = "label38";
            this.label38.ObjectName = null;
            this.tablePanel5.SetRow(this.label38, 1);
            this.label38.Size = new System.Drawing.Size(84, 30);
            this.label38.TabIndex = 22;
            this.label38.Text = "Дата принято\r\nна принт";
            // 
            // mtbRzuDataPrKm
            // 
            this.mtbRzuDataPrKm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataPrKm, 13);
            this.mtbRzuDataPrKm.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrKm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrKm.Location = new System.Drawing.Point(793, 35);
            this.mtbRzuDataPrKm.Mask = "00/00/0000";
            this.mtbRzuDataPrKm.Name = "mtbRzuDataPrKm";
            this.mtbRzuDataPrKm.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataPrKm, 1);
            this.mtbRzuDataPrKm.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrKm.TabIndex = 64;
            // 
            // mtbRzuDataRasv
            // 
            this.mtbRzuDataRasv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataRasv, 1);
            this.mtbRzuDataRasv.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataRasv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataRasv.Location = new System.Drawing.Point(103, 86);
            this.mtbRzuDataRasv.Mask = "00/00/0000";
            this.mtbRzuDataRasv.Name = "mtbRzuDataRasv";
            this.mtbRzuDataRasv.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataRasv, 3);
            this.mtbRzuDataRasv.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataRasv.TabIndex = 53;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label45, 9);
            this.label45.Font = new System.Drawing.Font("Arial", 9F);
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label45.Location = new System.Drawing.Point(554, 89);
            this.label45.Name = "label45";
            this.label45.ObjectName = null;
            this.tablePanel5.SetRow(this.label45, 3);
            this.label45.Size = new System.Drawing.Size(89, 15);
            this.label45.TabIndex = 38;
            this.label45.Text = "Дата на чистку";
            // 
            // mtbRzuDataPrP
            // 
            this.mtbRzuDataPrP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataPrP, 4);
            this.mtbRzuDataPrP.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrP.Location = new System.Drawing.Point(283, 35);
            this.mtbRzuDataPrP.Mask = "00/00/0000";
            this.mtbRzuDataPrP.Name = "mtbRzuDataPrP";
            this.mtbRzuDataPrP.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataPrP, 1);
            this.mtbRzuDataPrP.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrP.TabIndex = 55;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label39, 6);
            this.label39.Font = new System.Drawing.Font("Arial", 9F);
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label39.Location = new System.Drawing.Point(374, 31);
            this.label39.Name = "label39";
            this.label39.ObjectName = null;
            this.tablePanel5.SetRow(this.label39, 1);
            this.label39.Size = new System.Drawing.Size(84, 30);
            this.label39.TabIndex = 24;
            this.label39.Text = "Дата в работу\r\nпринт";
            // 
            // mtbRzuDataPrR
            // 
            this.mtbRzuDataPrR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataPrR, 7);
            this.mtbRzuDataPrR.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrR.Location = new System.Drawing.Point(463, 35);
            this.mtbRzuDataPrR.Mask = "00/00/0000";
            this.mtbRzuDataPrR.Name = "mtbRzuDataPrR";
            this.mtbRzuDataPrR.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataPrR, 1);
            this.mtbRzuDataPrR.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrR.TabIndex = 58;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label46, 6);
            this.label46.Font = new System.Drawing.Font("Arial", 9F);
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label46.Location = new System.Drawing.Point(374, 82);
            this.label46.Name = "label46";
            this.label46.ObjectName = null;
            this.tablePanel5.SetRow(this.label46, 3);
            this.label46.Size = new System.Drawing.Size(84, 30);
            this.label46.TabIndex = 36;
            this.label46.Text = "Дата в работу\r\nвышивка";
            // 
            // mtbRzuDataPrPe
            // 
            this.mtbRzuDataPrPe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel5.SetColumn(this.mtbRzuDataPrPe, 10);
            this.mtbRzuDataPrPe.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrPe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrPe.Location = new System.Drawing.Point(648, 35);
            this.mtbRzuDataPrPe.Mask = "00/00/0000";
            this.mtbRzuDataPrPe.Name = "mtbRzuDataPrPe";
            this.mtbRzuDataPrPe.ObjectName = null;
            this.tablePanel5.SetRow(this.mtbRzuDataPrPe, 1);
            this.mtbRzuDataPrPe.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrPe.TabIndex = 61;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label40, 9);
            this.label40.Font = new System.Drawing.Font("Arial", 9F);
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label40.Location = new System.Drawing.Point(554, 31);
            this.label40.Name = "label40";
            this.label40.ObjectName = null;
            this.tablePanel5.SetRow(this.label40, 1);
            this.label40.Size = new System.Drawing.Size(85, 30);
            this.label40.TabIndex = 26;
            this.label40.Text = "Дата на печку\r\nпринт";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label41, 12);
            this.label41.Font = new System.Drawing.Font("Arial", 9F);
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label41.Location = new System.Drawing.Point(739, 31);
            this.label41.Name = "label41";
            this.label41.ObjectName = null;
            this.tablePanel5.SetRow(this.label41, 1);
            this.label41.Size = new System.Drawing.Size(46, 30);
            this.label41.TabIndex = 28;
            this.label41.Text = "Дата\r\nкомпл.";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label47, 3);
            this.label47.Font = new System.Drawing.Font("Arial", 9F);
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label47.Location = new System.Drawing.Point(194, 82);
            this.label47.Name = "label47";
            this.label47.ObjectName = null;
            this.tablePanel5.SetRow(this.label47, 3);
            this.label47.Size = new System.Drawing.Size(84, 30);
            this.label47.TabIndex = 34;
            this.label47.Text = "Дата принято\r\nна вышивку";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label42, 15);
            this.label42.Font = new System.Drawing.Font("Arial", 9F);
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label42.Location = new System.Drawing.Point(884, 31);
            this.label42.Name = "label42";
            this.label42.ObjectName = null;
            this.tablePanel5.SetRow(this.label42, 1);
            this.label42.Size = new System.Drawing.Size(40, 30);
            this.label42.TabIndex = 30;
            this.label42.Text = "Дата\r\nсдачи";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label36, 0);
            this.tablePanel5.SetColumnSpan(this.label36, 2);
            this.label36.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label36.Location = new System.Drawing.Point(14, 63);
            this.label36.Name = "label36";
            this.label36.ObjectName = null;
            this.tablePanel5.SetRow(this.label36, 2);
            this.label36.Size = new System.Drawing.Size(83, 16);
            this.label36.TabIndex = 10;
            this.label36.Text = "ВЫШИВКА";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel5.SetColumn(this.label48, 0);
            this.label48.Font = new System.Drawing.Font("Arial", 9F);
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label48.Location = new System.Drawing.Point(14, 82);
            this.label48.Name = "label48";
            this.label48.ObjectName = null;
            this.tablePanel5.SetRow(this.label48, 3);
            this.label48.Size = new System.Drawing.Size(72, 30);
            this.label48.TabIndex = 32;
            this.label48.Text = "Дата\r\nна вышивку";
            // 
            // customGroupBox3
            // 
            this.customGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.customGroupBox3.BorderColor = System.Drawing.Color.Black;
            this.customGroupBox3.BorderThickness = 1;
            this.customGroupBox3.Controls.Add(this.tablePanel4);
            this.customGroupBox3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customGroupBox3.Location = new System.Drawing.Point(2, 86);
            this.customGroupBox3.Name = "customGroupBox3";
            this.customGroupBox3.ObjectName = null;
            this.customGroupBox3.Size = new System.Drawing.Size(1573, 221);
            this.customGroupBox3.TabIndex = 33;
            this.customGroupBox3.TabStop = false;
            this.customGroupBox3.Text = "НАКЛАДНЫЕ";
            // 
            // tablePanel4
            // 
            this.tablePanel4.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 309F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 159F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 642F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 143F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 245F)});
            this.tablePanel4.Controls.Add(this.simpleButtonNaklPart);
            this.tablePanel4.Controls.Add(this.simpleButtonPrintNaklXtraReport);
            this.tablePanel4.Controls.Add(this.btnNaklAbsent);
            this.tablePanel4.Controls.Add(this.panelControl1);
            this.tablePanel4.Controls.Add(this.btnNaklPrint);
            this.tablePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel4.Location = new System.Drawing.Point(3, 22);
            this.tablePanel4.Name = "tablePanel4";
            this.tablePanel4.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 146F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel4.Size = new System.Drawing.Size(1567, 196);
            this.tablePanel4.TabIndex = 0;
            this.tablePanel4.UseSkinIndents = true;
            // 
            // simpleButtonNaklPart
            // 
            this.simpleButtonNaklPart.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonNaklPart.Appearance.ForeColor = System.Drawing.Color.Purple;
            this.simpleButtonNaklPart.Appearance.Options.UseFont = true;
            this.simpleButtonNaklPart.Appearance.Options.UseForeColor = true;
            this.tablePanel4.SetColumn(this.simpleButtonNaklPart, 1);
            this.simpleButtonNaklPart.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonNaklPart.ImageOptions.Image")));
            this.simpleButtonNaklPart.Location = new System.Drawing.Point(18, 158);
            this.simpleButtonNaklPart.Name = "simpleButtonNaklPart";
            this.tablePanel4.SetRow(this.simpleButtonNaklPart, 1);
            this.simpleButtonNaklPart.Size = new System.Drawing.Size(305, 25);
            this.simpleButtonNaklPart.TabIndex = 12;
            this.simpleButtonNaklPart.Text = "Показать информацию по делению накладной";
            this.simpleButtonNaklPart.Click += new System.EventHandler(this.simpleButtonNaklPart_Click);
            // 
            // simpleButtonPrintNaklXtraReport
            // 
            this.simpleButtonPrintNaklXtraReport.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.simpleButtonPrintNaklXtraReport.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.simpleButtonPrintNaklXtraReport.Appearance.Options.UseFont = true;
            this.simpleButtonPrintNaklXtraReport.Appearance.Options.UseForeColor = true;
            this.tablePanel4.SetColumn(this.simpleButtonPrintNaklXtraReport, 3);
            this.simpleButtonPrintNaklXtraReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonPrintNaklXtraReport.ImageOptions.Image")));
            this.simpleButtonPrintNaklXtraReport.Location = new System.Drawing.Point(347, 158);
            this.simpleButtonPrintNaklXtraReport.Name = "simpleButtonPrintNaklXtraReport";
            this.tablePanel4.SetRow(this.simpleButtonPrintNaklXtraReport, 1);
            this.simpleButtonPrintNaklXtraReport.Size = new System.Drawing.Size(155, 25);
            this.simpleButtonPrintNaklXtraReport.TabIndex = 11;
            this.simpleButtonPrintNaklXtraReport.Text = "Печать накладной";
            this.simpleButtonPrintNaklXtraReport.Click += new System.EventHandler(this.simpleButtonPrintNaklXtraReport_Click);
            // 
            // btnNaklAbsent
            // 
            this.btnNaklAbsent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.tablePanel4.SetColumn(this.btnNaklAbsent, 6);
            this.btnNaklAbsent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNaklAbsent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaklAbsent.Font = new System.Drawing.Font("Arial", 10F);
            this.btnNaklAbsent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnNaklAbsent.Location = new System.Drawing.Point(1291, 158);
            this.btnNaklAbsent.Name = "btnNaklAbsent";
            this.btnNaklAbsent.ObjectName = null;
            this.tablePanel4.SetRow(this.btnNaklAbsent, 1);
            this.btnNaklAbsent.Size = new System.Drawing.Size(263, 25);
            this.btnNaklAbsent.TabIndex = 8;
            this.btnNaklAbsent.Text = "Накладная не создана. Причина";
            this.btnNaklAbsent.UseVisualStyleBackColor = true;
            this.btnNaklAbsent.Visible = false;
            // 
            // panelControl1
            // 
            this.tablePanel4.SetColumn(this.panelControl1, 0);
            this.tablePanel4.SetColumnSpan(this.panelControl1, 7);
            this.panelControl1.Controls.Add(this.gridControlPartNaklList);
            this.panelControl1.Controls.Add(this.gridControlNaklList);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(13, 12);
            this.panelControl1.Name = "panelControl1";
            this.tablePanel4.SetRow(this.panelControl1, 0);
            this.panelControl1.Size = new System.Drawing.Size(1541, 142);
            this.panelControl1.TabIndex = 0;
            // 
            // gridControlPartNaklList
            // 
            this.gridControlPartNaklList.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.gridControlPartNaklList.Font = new System.Drawing.Font("Arial", 10F);
            this.gridControlPartNaklList.Location = new System.Drawing.Point(7, 28);
            this.gridControlPartNaklList.MainView = this.gridViewPartNaklList;
            this.gridControlPartNaklList.Name = "gridControlPartNaklList";
            this.gridControlPartNaklList.ObjectName = null;
            this.gridControlPartNaklList.Size = new System.Drawing.Size(1160, 114);
            this.gridControlPartNaklList.TabIndex = 11;
            this.gridControlPartNaklList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPartNaklList});
            this.gridControlPartNaklList.Visible = false;
            // 
            // gridViewPartNaklList
            // 
            this.gridViewPartNaklList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNaklPartIzObPrch,
            this.gridColumnNaklPartRazm,
            this.gridColumnNaklPartMod,
            this.gridColumnNaklPartCountNew,
            this.gridColumnNaklPartCountAfter,
            this.gridColumnNaklPartCountBefore,
            this.gridColumnNaklPartIzNew,
            this.gridColumnNaklPartSklOtgrNew,
            this.gridColumnNaklPartSklOtgrOld,
            this.gridColumnNaklPartDateIzm,
            this.gridColumnNaklPartStatus,
            this.gridColumnNaklPartSklID1COld,
            this.gridColumnNaklPartCompDel,
            this.gridColumnNaklPartCompName,
            this.gridColumnNaklPartIzOld,
            this.gridColumnNaklPartID,
            this.gridColumnNaklPartNPach,
            this.gridColumnNaklPartPrichSokr,
            this.gridColumnNaklPartSklID1CNew});
            this.gridViewPartNaklList.GridControl = this.gridControlPartNaklList;
            this.gridViewPartNaklList.Name = "gridViewPartNaklList";
            this.gridViewPartNaklList.OptionsBehavior.Editable = false;
            this.gridViewPartNaklList.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewPartNaklList.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnNaklPartIzObPrch
            // 
            this.gridColumnNaklPartIzObPrch.Caption = "iz_ob_prch";
            this.gridColumnNaklPartIzObPrch.Name = "gridColumnNaklPartIzObPrch";
            this.gridColumnNaklPartIzObPrch.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartIzObPrch.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartIzObPrch.Visible = true;
            this.gridColumnNaklPartIzObPrch.VisibleIndex = 11;
            this.gridColumnNaklPartIzObPrch.Width = 45;
            // 
            // gridColumnNaklPartRazm
            // 
            this.gridColumnNaklPartRazm.Caption = "razm";
            this.gridColumnNaklPartRazm.Name = "gridColumnNaklPartRazm";
            this.gridColumnNaklPartRazm.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartRazm.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartRazm.Visible = true;
            this.gridColumnNaklPartRazm.VisibleIndex = 10;
            this.gridColumnNaklPartRazm.Width = 132;
            // 
            // gridColumnNaklPartMod
            // 
            this.gridColumnNaklPartMod.Caption = "mod";
            this.gridColumnNaklPartMod.Name = "gridColumnNaklPartMod";
            this.gridColumnNaklPartMod.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartMod.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartMod.Visible = true;
            this.gridColumnNaklPartMod.VisibleIndex = 9;
            this.gridColumnNaklPartMod.Width = 109;
            // 
            // gridColumnNaklPartCountNew
            // 
            this.gridColumnNaklPartCountNew.Caption = "kol_new";
            this.gridColumnNaklPartCountNew.Name = "gridColumnNaklPartCountNew";
            this.gridColumnNaklPartCountNew.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartCountNew.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartCountNew.Visible = true;
            this.gridColumnNaklPartCountNew.VisibleIndex = 8;
            this.gridColumnNaklPartCountNew.Width = 63;
            // 
            // gridColumnNaklPartCountAfter
            // 
            this.gridColumnNaklPartCountAfter.Caption = "kol_c";
            this.gridColumnNaklPartCountAfter.Name = "gridColumnNaklPartCountAfter";
            this.gridColumnNaklPartCountAfter.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartCountAfter.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartCountAfter.Visible = true;
            this.gridColumnNaklPartCountAfter.VisibleIndex = 7;
            this.gridColumnNaklPartCountAfter.Width = 53;
            // 
            // gridColumnNaklPartCountBefore
            // 
            this.gridColumnNaklPartCountBefore.Caption = "kol_b";
            this.gridColumnNaklPartCountBefore.Name = "gridColumnNaklPartCountBefore";
            this.gridColumnNaklPartCountBefore.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartCountBefore.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartCountBefore.Visible = true;
            this.gridColumnNaklPartCountBefore.VisibleIndex = 6;
            this.gridColumnNaklPartCountBefore.Width = 38;
            // 
            // gridColumnNaklPartIzNew
            // 
            this.gridColumnNaklPartIzNew.Caption = "iz_c";
            this.gridColumnNaklPartIzNew.Name = "gridColumnNaklPartIzNew";
            this.gridColumnNaklPartIzNew.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartIzNew.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartIzNew.Visible = true;
            this.gridColumnNaklPartIzNew.VisibleIndex = 5;
            this.gridColumnNaklPartIzNew.Width = 58;
            // 
            // gridColumnNaklPartSklOtgrNew
            // 
            this.gridColumnNaklPartSklOtgrNew.Caption = "skl_otgr_s";
            this.gridColumnNaklPartSklOtgrNew.Name = "gridColumnNaklPartSklOtgrNew";
            this.gridColumnNaklPartSklOtgrNew.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartSklOtgrNew.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartSklOtgrNew.Visible = true;
            this.gridColumnNaklPartSklOtgrNew.VisibleIndex = 3;
            this.gridColumnNaklPartSklOtgrNew.Width = 35;
            // 
            // gridColumnNaklPartSklOtgrOld
            // 
            this.gridColumnNaklPartSklOtgrOld.Caption = "skl_otgr_b";
            this.gridColumnNaklPartSklOtgrOld.Name = "gridColumnNaklPartSklOtgrOld";
            this.gridColumnNaklPartSklOtgrOld.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartSklOtgrOld.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartSklOtgrOld.Visible = true;
            this.gridColumnNaklPartSklOtgrOld.VisibleIndex = 2;
            this.gridColumnNaklPartSklOtgrOld.Width = 40;
            // 
            // gridColumnNaklPartDateIzm
            // 
            this.gridColumnNaklPartDateIzm.Caption = "data_izm";
            this.gridColumnNaklPartDateIzm.Name = "gridColumnNaklPartDateIzm";
            this.gridColumnNaklPartDateIzm.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartDateIzm.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartDateIzm.Visible = true;
            this.gridColumnNaklPartDateIzm.VisibleIndex = 1;
            this.gridColumnNaklPartDateIzm.Width = 51;
            // 
            // gridColumnNaklPartStatus
            // 
            this.gridColumnNaklPartStatus.Caption = "status";
            this.gridColumnNaklPartStatus.Name = "gridColumnNaklPartStatus";
            this.gridColumnNaklPartStatus.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartStatus.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartStatus.Visible = true;
            this.gridColumnNaklPartStatus.VisibleIndex = 12;
            this.gridColumnNaklPartStatus.Width = 45;
            // 
            // gridColumnNaklPartSklID1COld
            // 
            this.gridColumnNaklPartSklID1COld.Caption = "skl_id_1c_b";
            this.gridColumnNaklPartSklID1COld.Name = "gridColumnNaklPartSklID1COld";
            this.gridColumnNaklPartSklID1COld.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartSklID1COld.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartSklID1COld.Visible = true;
            this.gridColumnNaklPartSklID1COld.VisibleIndex = 15;
            this.gridColumnNaklPartSklID1COld.Width = 52;
            // 
            // gridColumnNaklPartCompDel
            // 
            this.gridColumnNaklPartCompDel.Caption = "komp_del";
            this.gridColumnNaklPartCompDel.Name = "gridColumnNaklPartCompDel";
            this.gridColumnNaklPartCompDel.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartCompDel.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartCompDel.Visible = true;
            this.gridColumnNaklPartCompDel.VisibleIndex = 14;
            this.gridColumnNaklPartCompDel.Width = 98;
            // 
            // gridColumnNaklPartCompName
            // 
            this.gridColumnNaklPartCompName.Caption = "komp_name";
            this.gridColumnNaklPartCompName.Name = "gridColumnNaklPartCompName";
            this.gridColumnNaklPartCompName.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartCompName.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartCompName.Visible = true;
            this.gridColumnNaklPartCompName.VisibleIndex = 13;
            this.gridColumnNaklPartCompName.Width = 101;
            // 
            // gridColumnNaklPartIzOld
            // 
            this.gridColumnNaklPartIzOld.Caption = "iz_b";
            this.gridColumnNaklPartIzOld.Name = "gridColumnNaklPartIzOld";
            this.gridColumnNaklPartIzOld.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartIzOld.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartIzOld.Visible = true;
            this.gridColumnNaklPartIzOld.VisibleIndex = 4;
            this.gridColumnNaklPartIzOld.Width = 49;
            // 
            // gridColumnNaklPartID
            // 
            this.gridColumnNaklPartID.Caption = "ID";
            this.gridColumnNaklPartID.Name = "gridColumnNaklPartID";
            this.gridColumnNaklPartID.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartID.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartID.Visible = true;
            this.gridColumnNaklPartID.VisibleIndex = 0;
            this.gridColumnNaklPartID.Width = 49;
            // 
            // gridColumnNaklPartNPach
            // 
            this.gridColumnNaklPartNPach.Caption = "n_pach";
            this.gridColumnNaklPartNPach.Name = "gridColumnNaklPartNPach";
            this.gridColumnNaklPartNPach.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartNPach.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartNPach.Visible = true;
            this.gridColumnNaklPartNPach.VisibleIndex = 18;
            this.gridColumnNaklPartNPach.Width = 128;
            // 
            // gridColumnNaklPartPrichSokr
            // 
            this.gridColumnNaklPartPrichSokr.Caption = "prich_sokr";
            this.gridColumnNaklPartPrichSokr.Name = "gridColumnNaklPartPrichSokr";
            this.gridColumnNaklPartPrichSokr.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartPrichSokr.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartPrichSokr.Visible = true;
            this.gridColumnNaklPartPrichSokr.VisibleIndex = 17;
            this.gridColumnNaklPartPrichSokr.Width = 73;
            // 
            // gridColumnNaklPartSklID1CNew
            // 
            this.gridColumnNaklPartSklID1CNew.Caption = "skl_id_1c_c";
            this.gridColumnNaklPartSklID1CNew.Name = "gridColumnNaklPartSklID1CNew";
            this.gridColumnNaklPartSklID1CNew.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNaklPartSklID1CNew.OptionsFilter.AllowFilter = false;
            this.gridColumnNaklPartSklID1CNew.Visible = true;
            this.gridColumnNaklPartSklID1CNew.VisibleIndex = 16;
            this.gridColumnNaklPartSklID1CNew.Width = 51;
            // 
            // btnNaklPrint
            // 
            this.btnNaklPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.tablePanel4.SetColumn(this.btnNaklPrint, 0);
            this.btnNaklPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaklPrint.Font = new System.Drawing.Font("Arial", 10F);
            this.btnNaklPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnNaklPrint.Location = new System.Drawing.Point(13, 159);
            this.btnNaklPrint.Name = "btnNaklPrint";
            this.btnNaklPrint.ObjectName = null;
            this.tablePanel4.SetRow(this.btnNaklPrint, 1);
            this.btnNaklPrint.Size = new System.Drawing.Size(1, 23);
            this.btnNaklPrint.TabIndex = 6;
            this.btnNaklPrint.Text = "Просмотр/Печать накладной";
            this.btnNaklPrint.UseVisualStyleBackColor = true;
            this.btnNaklPrint.Click += new System.EventHandler(this.btnNaklPrint_Click);
            // 
            // ContDates
            // 
            this.ContDates.BackColor = System.Drawing.Color.Transparent;
            this.ContDates.BorderColor = System.Drawing.Color.Black;
            this.ContDates.BorderThickness = 1;
            this.ContDates.Controls.Add(this.tablePanel3);
            this.ContDates.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.ContDates.Location = new System.Drawing.Point(3, 3);
            this.ContDates.Name = "ContDates";
            this.ContDates.ObjectName = null;
            this.ContDates.Size = new System.Drawing.Size(1750, 77);
            this.ContDates.TabIndex = 32;
            this.ContDates.TabStop = false;
            this.ContDates.Text = "КОНТРОЛЬНЫЕ ДАТЫ";
            // 
            // tablePanel3
            // 
            this.tablePanel3.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 95F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 16F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 75F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 21F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 115F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 34F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 20F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 8F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 75F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 24F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 38F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 70F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 95F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F)});
            this.tablePanel3.Controls.Add(this.customLabel3);
            this.tablePanel3.Controls.Add(this.mtbRzuData1С);
            this.tablePanel3.Controls.Add(this.tbPszRpcNom);
            this.tablePanel3.Controls.Add(this.label63);
            this.tablePanel3.Controls.Add(this.label29);
            this.tablePanel3.Controls.Add(this.mtbRzuDataCd);
            this.tablePanel3.Controls.Add(this.mtbRzuDataR);
            this.tablePanel3.Controls.Add(this.label14);
            this.tablePanel3.Controls.Add(this.mtbRzuDataUp);
            this.tablePanel3.Controls.Add(this.mtbPsaDataZap);
            this.tablePanel3.Controls.Add(this.mtbRzuDataRab);
            this.tablePanel3.Controls.Add(this.label33);
            this.tablePanel3.Controls.Add(this.label64);
            this.tablePanel3.Controls.Add(this.mtbRzuDataZeh);
            this.tablePanel3.Controls.Add(this.label28);
            this.tablePanel3.Controls.Add(this.label26);
            this.tablePanel3.Controls.Add(this.mtbPsaDataCdPlan);
            this.tablePanel3.Controls.Add(this.label34);
            this.tablePanel3.Controls.Add(this.mtbRzuDataCdUt);
            this.tablePanel3.Controls.Add(this.label27);
            this.tablePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel3.Location = new System.Drawing.Point(3, 22);
            this.tablePanel3.Name = "tablePanel3";
            this.tablePanel3.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F)});
            this.tablePanel3.Size = new System.Drawing.Size(1744, 52);
            this.tablePanel3.TabIndex = 0;
            this.tablePanel3.UseSkinIndents = true;
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.customLabel3, 12);
            this.customLabel3.Font = new System.Drawing.Font("Arial", 9F);
            this.customLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customLabel3.Location = new System.Drawing.Point(803, 18);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.ObjectName = null;
            this.tablePanel3.SetRow(this.customLabel3, 0);
            this.customLabel3.Size = new System.Drawing.Size(28, 15);
            this.customLabel3.TabIndex = 42;
            this.customLabel3.Text = "РЦ-";
            // 
            // mtbRzuData1С
            // 
            this.mtbRzuData1С.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbRzuData1С, 27);
            this.mtbRzuData1С.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuData1С.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuData1С.Location = new System.Drawing.Point(1660, 15);
            this.mtbRzuData1С.Mask = "00/00/0000";
            this.mtbRzuData1С.Name = "mtbRzuData1С";
            this.mtbRzuData1С.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbRzuData1С, 0);
            this.mtbRzuData1С.Size = new System.Drawing.Size(71, 21);
            this.mtbRzuData1С.TabIndex = 39;
            // 
            // tbPszRpcNom
            // 
            this.tbPszRpcNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.tbPszRpcNom, 13);
            this.tbPszRpcNom.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPszRpcNom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPszRpcNom.Location = new System.Drawing.Point(834, 14);
            this.tbPszRpcNom.Margin = new System.Windows.Forms.Padding(0);
            this.tbPszRpcNom.Name = "tbPszRpcNom";
            this.tbPszRpcNom.ObjectName = null;
            this.tablePanel3.SetRow(this.tbPszRpcNom, 0);
            this.tbPszRpcNom.Size = new System.Drawing.Size(20, 23);
            this.tbPszRpcNom.TabIndex = 34;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label63, 26);
            this.label63.Font = new System.Drawing.Font("Arial", 9F);
            this.label63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label63.Location = new System.Drawing.Point(1566, 18);
            this.label63.Name = "label63";
            this.label63.ObjectName = null;
            this.tablePanel3.SetRow(this.label63, 0);
            this.label63.Size = new System.Drawing.Size(88, 15);
            this.label63.TabIndex = 38;
            this.label63.Text = "Дата 1к.т. в 1С";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label29, 0);
            this.label29.Font = new System.Drawing.Font("Arial", 9F);
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label29.Location = new System.Drawing.Point(14, 18);
            this.label29.Name = "label29";
            this.label29.ObjectName = null;
            this.tablePanel3.SetRow(this.label29, 0);
            this.label29.Size = new System.Drawing.Size(81, 15);
            this.label29.TabIndex = 10;
            this.label29.Text = "Дата запуска";
            // 
            // mtbRzuDataCd
            // 
            this.mtbRzuDataCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbRzuDataCd, 25);
            this.mtbRzuDataCd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataCd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataCd.Location = new System.Drawing.Point(1485, 15);
            this.mtbRzuDataCd.Mask = "00/00/0000";
            this.mtbRzuDataCd.Name = "mtbRzuDataCd";
            this.mtbRzuDataCd.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbRzuDataCd, 0);
            this.mtbRzuDataCd.Size = new System.Drawing.Size(76, 21);
            this.mtbRzuDataCd.TabIndex = 37;
            // 
            // mtbRzuDataR
            // 
            this.mtbRzuDataR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbRzuDataR, 10);
            this.mtbRzuDataR.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataR.Location = new System.Drawing.Point(694, 15);
            this.mtbRzuDataR.Mask = "00/00/0000";
            this.mtbRzuDataR.Name = "mtbRzuDataR";
            this.mtbRzuDataR.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbRzuDataR, 0);
            this.mtbRzuDataR.Size = new System.Drawing.Size(76, 21);
            this.mtbRzuDataR.TabIndex = 41;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label14, 24);
            this.label14.Font = new System.Drawing.Font("Arial", 9F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(1396, 10);
            this.label14.Name = "label14";
            this.label14.ObjectName = null;
            this.tablePanel3.SetRow(this.label14, 0);
            this.label14.Size = new System.Drawing.Size(82, 30);
            this.label14.TabIndex = 30;
            this.label14.Text = "Дата СДАНО (осн. накл.)";
            // 
            // mtbRzuDataUp
            // 
            this.mtbRzuDataUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbRzuDataUp, 22);
            this.mtbRzuDataUp.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataUp.Location = new System.Drawing.Point(1295, 15);
            this.mtbRzuDataUp.Mask = "00/00/0000";
            this.mtbRzuDataUp.Name = "mtbRzuDataUp";
            this.mtbRzuDataUp.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbRzuDataUp, 0);
            this.mtbRzuDataUp.Size = new System.Drawing.Size(76, 21);
            this.mtbRzuDataUp.TabIndex = 36;
            // 
            // mtbPsaDataZap
            // 
            this.mtbPsaDataZap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbPsaDataZap, 1);
            this.mtbPsaDataZap.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbPsaDataZap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbPsaDataZap.Location = new System.Drawing.Point(108, 15);
            this.mtbPsaDataZap.Mask = "00/00/0000";
            this.mtbPsaDataZap.Name = "mtbPsaDataZap";
            this.mtbPsaDataZap.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbPsaDataZap, 0);
            this.mtbPsaDataZap.Size = new System.Drawing.Size(76, 21);
            this.mtbPsaDataZap.TabIndex = 31;
            // 
            // mtbRzuDataRab
            // 
            this.mtbRzuDataRab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbRzuDataRab, 19);
            this.mtbRzuDataRab.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataRab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataRab.Location = new System.Drawing.Point(1125, 15);
            this.mtbRzuDataRab.Mask = "00/00/0000";
            this.mtbRzuDataRab.Name = "mtbRzuDataRab";
            this.mtbRzuDataRab.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbRzuDataRab, 0);
            this.mtbRzuDataRab.Size = new System.Drawing.Size(76, 21);
            this.mtbRzuDataRab.TabIndex = 35;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label33, 21);
            this.label33.Font = new System.Drawing.Font("Arial", 9F);
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label33.Location = new System.Drawing.Point(1226, 10);
            this.label33.Name = "label33";
            this.label33.ObjectName = null;
            this.tablePanel3.SetRow(this.label33, 0);
            this.label33.Size = new System.Drawing.Size(56, 30);
            this.label33.TabIndex = 28;
            this.label33.Text = "Дата на упаковку";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label64, 9);
            this.label64.Font = new System.Drawing.Font("Arial", 9F);
            this.label64.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label64.Location = new System.Drawing.Point(605, 18);
            this.label64.Name = "label64";
            this.label64.ObjectName = null;
            this.tablePanel3.SetRow(this.label64, 0);
            this.label64.Size = new System.Drawing.Size(84, 15);
            this.label64.TabIndex = 40;
            this.label64.Text = "Дата раскроя";
            // 
            // mtbRzuDataZeh
            // 
            this.mtbRzuDataZeh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbRzuDataZeh, 16);
            this.mtbRzuDataZeh.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataZeh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataZeh.Location = new System.Drawing.Point(951, 15);
            this.mtbRzuDataZeh.Mask = "00/00/0000";
            this.mtbRzuDataZeh.Name = "mtbRzuDataZeh";
            this.mtbRzuDataZeh.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbRzuDataZeh, 0);
            this.mtbRzuDataZeh.Size = new System.Drawing.Size(76, 21);
            this.mtbRzuDataZeh.TabIndex = 34;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label28, 3);
            this.label28.Font = new System.Drawing.Font("Arial", 9F);
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label28.Location = new System.Drawing.Point(205, 10);
            this.label28.Name = "label28";
            this.label28.ObjectName = null;
            this.tablePanel3.SetRow(this.label28, 0);
            this.label28.Size = new System.Drawing.Size(69, 30);
            this.label28.TabIndex = 12;
            this.label28.Text = "План. дата сдачи";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label26, 18);
            this.label26.Font = new System.Drawing.Font("Arial", 9F);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label26.Location = new System.Drawing.Point(1056, 10);
            this.label26.Name = "label26";
            this.label26.ObjectName = null;
            this.tablePanel3.SetRow(this.label26, 0);
            this.label26.Size = new System.Drawing.Size(46, 30);
            this.label26.TabIndex = 16;
            this.label26.Text = "Дата в работу";
            // 
            // mtbPsaDataCdPlan
            // 
            this.mtbPsaDataCdPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbPsaDataCdPlan, 4);
            this.mtbPsaDataCdPlan.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbPsaDataCdPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbPsaDataCdPlan.Location = new System.Drawing.Point(279, 15);
            this.mtbPsaDataCdPlan.Mask = "00/00/0000";
            this.mtbPsaDataCdPlan.Name = "mtbPsaDataCdPlan";
            this.mtbPsaDataCdPlan.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbPsaDataCdPlan, 0);
            this.mtbPsaDataCdPlan.Size = new System.Drawing.Size(76, 21);
            this.mtbPsaDataCdPlan.TabIndex = 32;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label34, 6);
            this.label34.Font = new System.Drawing.Font("Arial", 9F);
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label34.Location = new System.Drawing.Point(381, 10);
            this.label34.Name = "label34";
            this.label34.ObjectName = null;
            this.tablePanel3.SetRow(this.label34, 0);
            this.label34.Size = new System.Drawing.Size(108, 30);
            this.label34.TabIndex = 26;
            this.label34.Text = "План. дата сдачи Уточненная";
            // 
            // mtbRzuDataCdUt
            // 
            this.mtbRzuDataCdUt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tablePanel3.SetColumn(this.mtbRzuDataCdUt, 7);
            this.mtbRzuDataCdUt.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataCdUt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataCdUt.Location = new System.Drawing.Point(495, 15);
            this.mtbRzuDataCdUt.Mask = "00/00/0000";
            this.mtbRzuDataCdUt.Name = "mtbRzuDataCdUt";
            this.mtbRzuDataCdUt.ObjectName = null;
            this.tablePanel3.SetRow(this.mtbRzuDataCdUt, 0);
            this.mtbRzuDataCdUt.Size = new System.Drawing.Size(76, 21);
            this.mtbRzuDataCdUt.TabIndex = 33;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel3.SetColumn(this.label27, 15);
            this.label27.Font = new System.Drawing.Font("Arial", 9F);
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label27.Location = new System.Drawing.Point(877, 18);
            this.label27.Name = "label27";
            this.label27.ObjectName = null;
            this.tablePanel3.SetRow(this.label27, 0);
            this.label27.Size = new System.Drawing.Size(65, 15);
            this.label27.TabIndex = 14;
            this.label27.Text = "Дата в цех";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.xtraTabControl1.Appearance.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl1.Enabled = false;
            this.xtraTabControl1.Location = new System.Drawing.Point(4, 182);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.RasInfo;
            this.xtraTabControl1.Size = new System.Drawing.Size(1758, 629);
            this.xtraTabControl1.TabIndex = 3;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.RasInfo,
            this.FurnInfo,
            this.WorkInfo,
            this.OtdelkaInfo});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // gridColumn57
            // 
            this.gridColumn57.Caption = "Отгр. на склад";
            this.gridColumn57.FieldName = "KolGI";
            this.gridColumn57.Name = "gridColumn57";
            this.gridColumn57.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", "Отгр. на скл.: {0:0.##}")});
            this.gridColumn57.Visible = true;
            this.gridColumn57.VisibleIndex = 8;
            // 
            // RasCard
            // 
            this.RasCard.BackColor = System.Drawing.Color.Transparent;
            this.RasCard.BorderColor = System.Drawing.Color.Black;
            this.RasCard.BorderThickness = 1;
            this.RasCard.Controls.Add(this.tablePanel1);
            this.RasCard.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RasCard.Location = new System.Drawing.Point(0, 26);
            this.RasCard.Name = "RasCard";
            this.RasCard.ObjectName = null;
            this.RasCard.Size = new System.Drawing.Size(1762, 156);
            this.RasCard.TabIndex = 4;
            this.RasCard.TabStop = false;
            this.RasCard.Text = "КАРТОЧКА РАСЧЕТА";
            // 
            // tablePanel1
            // 
            this.tablePanel1.AllowDrop = true;
            this.tablePanel1.AutoSize = true;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 88F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 7.530001F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 88F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 39F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 33F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 7.549999F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 108F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 13.69F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 69F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 48F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 7.59F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 114F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 197F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 8F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 171.72F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 7.540001F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 72F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 36F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 16F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 7.699997F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 8F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 54.93F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 84F)});
            this.tablePanel1.Controls.Add(this.pbEskiz);
            this.tablePanel1.Controls.Add(this.tbPsaKombOsn);
            this.tablePanel1.Controls.Add(this.tbPsaPsaIDOsn);
            this.tablePanel1.Controls.Add(this.label67);
            this.tablePanel1.Controls.Add(this.label65);
            this.tablePanel1.Controls.Add(this.tbPsaKombIzd);
            this.tablePanel1.Controls.Add(this.label66);
            this.tablePanel1.Controls.Add(this.tbPsaPsaID);
            this.tablePanel1.Controls.Add(this.tbPsaNameSbit);
            this.tablePanel1.Controls.Add(this.cbIsChip);
            this.tablePanel1.Controls.Add(this.tbArtTradeMark);
            this.tablePanel1.Controls.Add(this.label62);
            this.tablePanel1.Controls.Add(this.tbPsaYear);
            this.tablePanel1.Controls.Add(this.psaSezName);
            this.tablePanel1.Controls.Add(this.label17);
            this.tablePanel1.Controls.Add(this.label16);
            this.tablePanel1.Controls.Add(this.label21);
            this.tablePanel1.Controls.Add(this.tbPsaNomZad);
            this.tablePanel1.Controls.Add(this.tbRzuNom);
            this.tablePanel1.Controls.Add(this.label68);
            this.tablePanel1.Controls.Add(this.label5);
            this.tablePanel1.Controls.Add(this.tbPsaMenName);
            this.tablePanel1.Controls.Add(this.tbRzuArticul);
            this.tablePanel1.Controls.Add(this.tbPsaNN);
            this.tablePanel1.Controls.Add(this.label53);
            this.tablePanel1.Controls.Add(this.label11);
            this.tablePanel1.Controls.Add(this.tbPsaTbID);
            this.tablePanel1.Controls.Add(this.tbArtGrup);
            this.tablePanel1.Controls.Add(this.label6);
            this.tablePanel1.Controls.Add(this.label20);
            this.tablePanel1.Controls.Add(this.tbRzuMod);
            this.tablePanel1.Controls.Add(this.tbPsaPrn);
            this.tablePanel1.Controls.Add(this.tbRzuPach);
            this.tablePanel1.Controls.Add(this.label19);
            this.tablePanel1.Controls.Add(this.tbSostPoln);
            this.tablePanel1.Controls.Add(this.label13);
            this.tablePanel1.Controls.Add(this.label7);
            this.tablePanel1.Controls.Add(this.tbRzuKol);
            this.tablePanel1.Controls.Add(this.label12);
            this.tablePanel1.Controls.Add(this.tbPsaKodZv2);
            this.tablePanel1.Controls.Add(this.tbRzuDostZeh);
            this.tablePanel1.Controls.Add(this.tbPsaKodZv1);
            this.tablePanel1.Controls.Add(this.label9);
            this.tablePanel1.Controls.Add(this.label10);
            this.tablePanel1.Controls.Add(this.label8);
            this.tablePanel1.Controls.Add(this.label23);
            this.tablePanel1.Controls.Add(this.label22);
            this.tablePanel1.Controls.Add(this.label15);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(3, 22);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F)});
            this.tablePanel1.Size = new System.Drawing.Size(1756, 131);
            this.tablePanel1.TabIndex = 5;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // tablePanel2
            // 
            this.tablePanel2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel2.Appearance.Options.UseBackColor = true;
            this.tablePanel2.AutoSize = true;
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 69F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 66F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 61F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 8F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 38F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F)});
            this.tablePanel2.Controls.Add(this.customLabel2);
            this.tablePanel2.Controls.Add(this.customLabel1);
            this.tablePanel2.Controls.Add(this.label3);
            this.tablePanel2.Controls.Add(this.tbYearPach);
            this.tablePanel2.Controls.Add(this.tbNomPach);
            this.tablePanel2.Controls.Add(this.label4);
            this.tablePanel2.Location = new System.Drawing.Point(613, -10);
            this.tablePanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel2.Size = new System.Drawing.Size(346, 54);
            this.tablePanel2.TabIndex = 9;
            this.tablePanel2.UseSkinIndents = true;
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel2.SetColumn(this.customLabel2, 0);
            this.customLabel2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customLabel2.Location = new System.Drawing.Point(11, 17);
            this.customLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.ObjectName = null;
            this.tablePanel2.SetRow(this.customLabel2, 0);
            this.customLabel2.Size = new System.Drawing.Size(67, 19);
            this.customLabel2.TabIndex = 10;
            this.customLabel2.Text = "ПОИСК";
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel2.SetColumn(this.customLabel1, 2);
            this.customLabel1.Font = new System.Drawing.Font("Arial", 10F);
            this.customLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customLabel1.Location = new System.Drawing.Point(100, 18);
            this.customLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.ObjectName = null;
            this.tablePanel2.SetRow(this.customLabel1, 0);
            this.customLabel1.Size = new System.Drawing.Size(63, 16);
            this.customLabel1.TabIndex = 9;
            this.customLabel1.Text = "№ пачки";
            // 
            // CardByNom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1831, 837);
            this.Controls.Add(this.tablePanel2);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.RasCard);
            this.Name = "CardByNom";
            this.Text = "Карточка расчета";
            this.Load += new System.EventHandler(this.CardByNom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbEskiz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProizvCombIzdSP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProizvCombIzdVZP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).EndInit();
            this.gridSplitContainer1.ResumeLayout(false);
            this.OtdelkaInfo.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.customGroupBox7.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProizvCombIzdSP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProizvCombIzdSP)).EndInit();
            this.customGroupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProizvCombIzdVZP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProizvCombIzdVZP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNaklList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNaklList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOtdelka)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOtdelka)).EndInit();
            this.FurnInfo.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.customGroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel7)).EndInit();
            this.tablePanel7.ResumeLayout(false);
            this.tablePanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.RasInfo.ResumeLayout(false);
            this.customGroupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel6)).EndInit();
            this.tablePanel6.ResumeLayout(false);
            this.customGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel5)).EndInit();
            this.tablePanel5.ResumeLayout(false);
            this.tablePanel5.PerformLayout();
            this.customGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel4)).EndInit();
            this.tablePanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPartNaklList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPartNaklList)).EndInit();
            this.ContDates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).EndInit();
            this.tablePanel3.ResumeLayout(false);
            this.tablePanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.RasCard.ResumeLayout(false);
            this.RasCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            this.tablePanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbEskiz;
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
        private CustomTextBox tbRzuNom;
        private CustomTextBox tbYearPach;
        private CustomTextBox tbPsaNameSbit;
        private CustomTextBox tbRzuDostZeh;
        private CustomTextBox tbRzuMod;
        private CustomTextBox tbRzuArticul;
        private CustomTextBox tbPsaPrn;
        private CustomTextBox tbRzuPach;
        private CustomTextBox tbPsaKodZv2;
        private CustomTextBox tbPsaKodZv1;
        private CustomTextBox tbSostPoln;
        private CustomTextBox tbArtGrup;
        private CustomTextBox psaSezName;
        private CustomTextBox tbPsaYear;
        private CustomTextBox tbPsaTbID;
        private CustomTextBox tbPsaMenName;
        private CustomTextBox tbPsaNN;
        private CustomTextBox tbPsaNomZad;
        private CustomTextBox tbRzuKol;
        private CustomTextBox tbPsaPsaID;
        private CustomTextBox tbPsaKombIzd;
        private CustomTextBox tbPsaKombOsn;
        private CustomTextBox tbPsaPsaIDOsn;
        private CustomTextBox tbArtTradeMark;
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
        private CustomLabel label5;
        private CustomLabel label4;
        private CustomLabel label3;
        private CustomLabel label11;
        private CustomLabel label12;
        private CustomLabel label10;
        private CustomLabel label9;
        private CustomLabel label8;
        private CustomLabel label7;
        private CustomLabel label6;
        private CustomLabel label22;
        private CustomLabel label23;
        private CustomLabel label13;
        private CustomLabel label15;
        private CustomLabel label16;
        private CustomLabel label17;
        private CustomLabel label19;
        private CustomLabel label20;
        private CustomLabel label21;
        private CustomLabel label53;
        private CustomLabel label62;
        private CustomLabel label66;
        private CustomLabel label67;
        private CustomLabel label65;
        private CustomLabel label68;
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
        private CustomGroupBox RasCard;
        private CustomCheckBox cbIsChip;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
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
    }
}