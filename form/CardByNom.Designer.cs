using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;

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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule12 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue12 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule13 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue13 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule14 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue14 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule15 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue15 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule16 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue16 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule17 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue17 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule18 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue18 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule19 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue19 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule20 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue20 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule21 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue21 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule22 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue22 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bsNaklList = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tbYearPach = new SewingProduction.CustomTextBox();
            this.tbNomPach = new SewingProduction.CustomTextBox();
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
            this.tbPsaKombIzd = new SewingProduction.CustomTextBox();
            this.tbPsaKombOsn = new SewingProduction.CustomTextBox();
            this.tbPsaPsaIDOsn = new SewingProduction.CustomTextBox();
            this.tbPsaPsaID = new SewingProduction.CustomTextBox();
            this.cbIsChip = new System.Windows.Forms.CheckBox();
            this.psaSezName = new SewingProduction.CustomTextBox();
            this.tbPsaYear = new SewingProduction.CustomTextBox();
            this.tbPsaNN = new SewingProduction.CustomTextBox();
            this.label68 = new SewingProduction.CustomLabel();
            this.label66 = new SewingProduction.CustomLabel();
            this.label67 = new SewingProduction.CustomLabel();
            this.label65 = new SewingProduction.CustomLabel();
            this.label62 = new SewingProduction.CustomLabel();
            this.label16 = new SewingProduction.CustomLabel();
            this.label17 = new SewingProduction.CustomLabel();
            this.label22 = new SewingProduction.CustomLabel();
            this.label53 = new SewingProduction.CustomLabel();
            this.label23 = new SewingProduction.CustomLabel();
            this.label20 = new SewingProduction.CustomLabel();
            this.pbEskiz = new System.Windows.Forms.PictureBox();
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
            this.label4 = new SewingProduction.CustomLabel();
            this.label21 = new SewingProduction.CustomLabel();
            this.label3 = new SewingProduction.CustomLabel();
            this.label2 = new SewingProduction.CustomLabel();
            this.label1 = new SewingProduction.CustomLabel();
            this.bsOtdelkaList = new System.Windows.Forms.BindingSource(this.components);
            this.bsRasInfo = new System.Windows.Forms.BindingSource(this.components);
            this.bsPartNaklList = new System.Windows.Forms.BindingSource(this.components);
            this.bsProizvCombIzdSP = new System.Windows.Forms.BindingSource(this.components);
            this.bsProizvCombIzdVZP = new System.Windows.Forms.BindingSource(this.components);
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bsIsChip = new System.Windows.Forms.BindingSource(this.components);
            this.bsFurnZayavInfo = new System.Windows.Forms.BindingSource(this.components);
            this.gridSplitContainer1 = new DevExpress.XtraGrid.GridSplitContainer();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.sbProizvCombIzdSP = new DevExpress.XtraEditors.SimpleButton();
            this.label70 = new SewingProduction.CustomLabel();
            this.label69 = new SewingProduction.CustomLabel();
            this.gcProizvCombIzdSP = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn52 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn55 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn56 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn58 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn74 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn75 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn76 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn77 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl4 = new DevExpress.XtraGrid.GridControl();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn59 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn60 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn61 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn62 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn63 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn64 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn65 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn66 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn67 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn68 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn69 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn70 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn71 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn72 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn73 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNaklList = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.furnitZayavViewFurnit = new SewingProduction.FurnitZayavView();
            this.furnitZayavViewUpak = new SewingProduction.FurnitZayavView();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.tbDatZayav = new SewingProduction.CustomTextBox();
            this.button10 = new SewingProduction.CustomButton();
            this.label61 = new SewingProduction.CustomLabel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnZayavUpakPrint = new SewingProduction.CustomButton();
            this.tbData_f_z_u = new SewingProduction.CustomTextBox();
            this.tbData_f_o_u = new SewingProduction.CustomTextBox();
            this.tbData_f_z = new SewingProduction.CustomTextBox();
            this.tbData_f_o = new SewingProduction.CustomTextBox();
            this.mtbData_cd = new SewingProduction.CustomMaskedTextBox();
            this.mtbData_zeh = new SewingProduction.CustomMaskedTextBox();
            this.tbOtgrStat = new SewingProduction.CustomTextBox();
            this.tbIs_got = new SewingProduction.CustomTextBox();
            this.label60 = new SewingProduction.CustomLabel();
            this.label59 = new SewingProduction.CustomLabel();
            this.tbUZSobrStat = new SewingProduction.CustomTextBox();
            this.label56 = new SewingProduction.CustomLabel();
            this.tbUZSozdStat = new SewingProduction.CustomTextBox();
            this.label57 = new SewingProduction.CustomLabel();
            this.tbUpakZayav = new SewingProduction.CustomTextBox();
            this.label58 = new SewingProduction.CustomLabel();
            this.btnZayavFurnPrint = new SewingProduction.CustomButton();
            this.tbFZSobrStat = new SewingProduction.CustomTextBox();
            this.label55 = new SewingProduction.CustomLabel();
            this.tbFZSozdStat = new SewingProduction.CustomTextBox();
            this.label54 = new SewingProduction.CustomLabel();
            this.tbFurnZayav = new SewingProduction.CustomTextBox();
            this.label25 = new SewingProduction.CustomLabel();
            this.btnFullKKPrint = new SewingProduction.CustomButton();
            this.tbUpakKKStat = new SewingProduction.CustomTextBox();
            this.tbFurnKKStat = new SewingProduction.CustomTextBox();
            this.btnUpakKKPrint = new SewingProduction.CustomButton();
            this.btnFurnKKPrint = new SewingProduction.CustomButton();
            this.label18 = new SewingProduction.CustomLabel();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.button4 = new SewingProduction.CustomButton();
            this.button5 = new SewingProduction.CustomButton();
            this.button6 = new SewingProduction.CustomButton();
            this.label52 = new SewingProduction.CustomLabel();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.cbRzuStirFact = new SewingProduction.CustomCheckBox();
            this.cbPszStirPlan = new SewingProduction.CustomCheckBox();
            this.cbRzuVishFact = new SewingProduction.CustomCheckBox();
            this.cbPszVishPlan = new SewingProduction.CustomCheckBox();
            this.cbRzuPrintFact = new SewingProduction.CustomCheckBox();
            this.cbPszPrintPlan = new SewingProduction.CustomCheckBox();
            this.mtbRzuDataPrCd = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataVCd = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataPrKm = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuVidStir = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataVChi = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataPrPe = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataStCd = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataVR = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataPrR = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataStR = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataVP = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataPrP = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataStP = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataRasv = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataRasp = new SewingProduction.CustomMaskedTextBox();
            this.label44 = new SewingProduction.CustomLabel();
            this.label49 = new SewingProduction.CustomLabel();
            this.label50 = new SewingProduction.CustomLabel();
            this.label51 = new SewingProduction.CustomLabel();
            this.label43 = new SewingProduction.CustomLabel();
            this.label45 = new SewingProduction.CustomLabel();
            this.label46 = new SewingProduction.CustomLabel();
            this.label47 = new SewingProduction.CustomLabel();
            this.label48 = new SewingProduction.CustomLabel();
            this.label42 = new SewingProduction.CustomLabel();
            this.label41 = new SewingProduction.CustomLabel();
            this.label40 = new SewingProduction.CustomLabel();
            this.label39 = new SewingProduction.CustomLabel();
            this.label38 = new SewingProduction.CustomLabel();
            this.label37 = new SewingProduction.CustomLabel();
            this.label35 = new SewingProduction.CustomLabel();
            this.label36 = new SewingProduction.CustomLabel();
            this.label32 = new SewingProduction.CustomLabel();
            this.label31 = new SewingProduction.CustomLabel();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.gcPartNaklList = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.button1 = new SewingProduction.CustomButton();
            this.btnPrintNaklXtraReport = new System.Windows.Forms.Button();
            this.btnNaklAbsent = new SewingProduction.CustomButton();
            this.btnNaklPart = new SewingProduction.CustomButton();
            this.btnNaklPrint = new SewingProduction.CustomButton();
            this.label24 = new SewingProduction.CustomLabel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.tbPszRpcNom = new SewingProduction.CustomTextBox();
            this.mtbRzuDataR = new SewingProduction.CustomMaskedTextBox();
            this.label64 = new SewingProduction.CustomLabel();
            this.mtbRzuData1С = new SewingProduction.CustomMaskedTextBox();
            this.label63 = new SewingProduction.CustomLabel();
            this.mtbRzuDataCd = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataUp = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataRab = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataZeh = new SewingProduction.CustomMaskedTextBox();
            this.mtbRzuDataCdUt = new SewingProduction.CustomMaskedTextBox();
            this.mtbPsaDataCdPlan = new SewingProduction.CustomMaskedTextBox();
            this.mtbPsaDataZap = new SewingProduction.CustomMaskedTextBox();
            this.label14 = new SewingProduction.CustomLabel();
            this.label33 = new SewingProduction.CustomLabel();
            this.label34 = new SewingProduction.CustomLabel();
            this.label26 = new SewingProduction.CustomLabel();
            this.label27 = new SewingProduction.CustomLabel();
            this.label28 = new SewingProduction.CustomLabel();
            this.label29 = new SewingProduction.CustomLabel();
            this.label30 = new SewingProduction.CustomLabel();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsNaklList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEskiz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOtdelkaList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRasInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPartNaklList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProizvCombIzdSP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProizvCombIzdVZP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsIsChip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFurnZayavInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel2)).BeginInit();
            this.gridSplitContainer1.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProizvCombIzdSP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNaklList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPartNaklList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn40
            // 
            this.gridColumn40.FieldName = "ChipInUTForeColor";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 3;
            this.gridColumn40.Width = 63;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.Caption = "ЧИП в УТ";
            this.gridColumn15.FieldName = "ChipInUT";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.FixedWidth = true;
            this.gridColumn15.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn15.OptionsFilter.AllowFilter = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 17;
            this.gridColumn15.Width = 40;
            // 
            // gridColumn44
            // 
            this.gridColumn44.FieldName = "ChipPechForeColor";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 2;
            this.gridColumn44.Width = 63;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "Печ. ЧИП";
            this.gridColumn14.FieldName = "ChipPech";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.FixedWidth = true;
            this.gridColumn14.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn14.OptionsFilter.AllowFilter = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 18;
            this.gridColumn14.Width = 40;
            // 
            // gridColumn45
            // 
            this.gridColumn45.FieldName = "ChipScanForeColor";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.Visible = true;
            this.gridColumn45.VisibleIndex = 1;
            this.gridColumn45.Width = 73;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "Скан. ЧИП";
            this.gridColumn13.FieldName = "ChipScan";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.FixedWidth = true;
            this.gridColumn13.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn13.OptionsFilter.AllowFilter = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 19;
            this.gridColumn13.Width = 40;
            // 
            // gridColumn46
            // 
            this.gridColumn46.FieldName = "ChipOtgrForeColor";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.Visible = true;
            this.gridColumn46.VisibleIndex = 0;
            this.gridColumn46.Width = 73;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "Отгр. ЧИП";
            this.gridColumn12.FieldName = "ChipOtgr";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.FixedWidth = true;
            this.gridColumn12.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn12.OptionsFilter.AllowFilter = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 20;
            this.gridColumn12.Width = 40;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.tbYearPach);
            this.panelControl1.Controls.Add(this.tbNomPach);
            this.panelControl1.Controls.Add(this.tbRzuKol);
            this.panelControl1.Controls.Add(this.tbPsaNomZad);
            this.panelControl1.Controls.Add(this.tbRzuDostZeh);
            this.panelControl1.Controls.Add(this.tbRzuPach);
            this.panelControl1.Controls.Add(this.tbRzuNom);
            this.panelControl1.Controls.Add(this.tbPsaKodZv2);
            this.panelControl1.Controls.Add(this.tbPsaKodZv1);
            this.panelControl1.Controls.Add(this.tbRzuMod);
            this.panelControl1.Controls.Add(this.tbRzuArticul);
            this.panelControl1.Controls.Add(this.tbPsaPrn);
            this.panelControl1.Controls.Add(this.tbSostPoln);
            this.panelControl1.Controls.Add(this.tbArtGrup);
            this.panelControl1.Controls.Add(this.tbArtTradeMark);
            this.panelControl1.Controls.Add(this.tbPsaMenName);
            this.panelControl1.Controls.Add(this.tbPsaNameSbit);
            this.panelControl1.Controls.Add(this.tbPsaTbID);
            this.panelControl1.Controls.Add(this.tbPsaKombIzd);
            this.panelControl1.Controls.Add(this.tbPsaKombOsn);
            this.panelControl1.Controls.Add(this.tbPsaPsaIDOsn);
            this.panelControl1.Controls.Add(this.tbPsaPsaID);
            this.panelControl1.Controls.Add(this.cbIsChip);
            this.panelControl1.Controls.Add(this.psaSezName);
            this.panelControl1.Controls.Add(this.tbPsaYear);
            this.panelControl1.Controls.Add(this.tbPsaNN);
            this.panelControl1.Controls.Add(this.label68);
            this.panelControl1.Controls.Add(this.label66);
            this.panelControl1.Controls.Add(this.label67);
            this.panelControl1.Controls.Add(this.label65);
            this.panelControl1.Controls.Add(this.label62);
            this.panelControl1.Controls.Add(this.label16);
            this.panelControl1.Controls.Add(this.label17);
            this.panelControl1.Controls.Add(this.label22);
            this.panelControl1.Controls.Add(this.label53);
            this.panelControl1.Controls.Add(this.label23);
            this.panelControl1.Controls.Add(this.label20);
            this.panelControl1.Controls.Add(this.pbEskiz);
            this.panelControl1.Controls.Add(this.label13);
            this.panelControl1.Controls.Add(this.label11);
            this.panelControl1.Controls.Add(this.label15);
            this.panelControl1.Controls.Add(this.label12);
            this.panelControl1.Controls.Add(this.label10);
            this.panelControl1.Controls.Add(this.label19);
            this.panelControl1.Controls.Add(this.label9);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label21);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Location = new System.Drawing.Point(4, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1569, 123);
            this.panelControl1.TabIndex = 3;
            // 
            // tbYearPach
            // 
            this.tbYearPach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbYearPach.Font = new System.Drawing.Font("Arial", 10F);
            this.tbYearPach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbYearPach.Location = new System.Drawing.Point(741, 2);
            this.tbYearPach.Margin = new System.Windows.Forms.Padding(0);
            this.tbYearPach.Name = "tbYearPach";
            this.tbYearPach.Size = new System.Drawing.Size(42, 23);
            this.tbYearPach.TabIndex = 7;
            // 
            // tbNomPach
            // 
            this.tbNomPach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbNomPach.Font = new System.Drawing.Font("Arial", 10F);
            this.tbNomPach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbNomPach.Location = new System.Drawing.Point(629, 2);
            this.tbNomPach.Margin = new System.Windows.Forms.Padding(0);
            this.tbNomPach.Name = "tbNomPach";
            this.tbNomPach.Size = new System.Drawing.Size(64, 23);
            this.tbNomPach.TabIndex = 5;
            this.tbNomPach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNomPach_KeyDown);
            // 
            // tbRzuKol
            // 
            this.tbRzuKol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbRzuKol.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuKol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuKol.Location = new System.Drawing.Point(259, 26);
            this.tbRzuKol.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuKol.Name = "tbRzuKol";
            this.tbRzuKol.Size = new System.Drawing.Size(52, 23);
            this.tbRzuKol.TabIndex = 29;
            // 
            // tbPsaNomZad
            // 
            this.tbPsaNomZad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaNomZad.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaNomZad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaNomZad.Location = new System.Drawing.Point(78, 50);
            this.tbPsaNomZad.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaNomZad.Name = "tbPsaNomZad";
            this.tbPsaNomZad.Size = new System.Drawing.Size(73, 23);
            this.tbPsaNomZad.TabIndex = 26;
            // 
            // tbRzuDostZeh
            // 
            this.tbRzuDostZeh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbRzuDostZeh.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuDostZeh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuDostZeh.Location = new System.Drawing.Point(228, 50);
            this.tbRzuDostZeh.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuDostZeh.Name = "tbRzuDostZeh";
            this.tbRzuDostZeh.Size = new System.Drawing.Size(83, 23);
            this.tbRzuDostZeh.TabIndex = 21;
            // 
            // tbRzuPach
            // 
            this.tbRzuPach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbRzuPach.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuPach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuPach.Location = new System.Drawing.Point(118, 74);
            this.tbRzuPach.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuPach.Name = "tbRzuPach";
            this.tbRzuPach.Size = new System.Drawing.Size(193, 23);
            this.tbRzuPach.TabIndex = 11;
            // 
            // tbRzuNom
            // 
            this.tbRzuNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbRzuNom.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuNom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuNom.Location = new System.Drawing.Point(78, 26);
            this.tbRzuNom.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuNom.Name = "tbRzuNom";
            this.tbRzuNom.Size = new System.Drawing.Size(73, 23);
            this.tbRzuNom.TabIndex = 9;
            // 
            // tbPsaKodZv2
            // 
            this.tbPsaKodZv2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaKodZv2.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaKodZv2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaKodZv2.Location = new System.Drawing.Point(565, 96);
            this.tbPsaKodZv2.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaKodZv2.Name = "tbPsaKodZv2";
            this.tbPsaKodZv2.Size = new System.Drawing.Size(65, 23);
            this.tbPsaKodZv2.TabIndex = 29;
            // 
            // tbPsaKodZv1
            // 
            this.tbPsaKodZv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaKodZv1.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaKodZv1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaKodZv1.Location = new System.Drawing.Point(426, 96);
            this.tbPsaKodZv1.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaKodZv1.Name = "tbPsaKodZv1";
            this.tbPsaKodZv1.Size = new System.Drawing.Size(65, 23);
            this.tbPsaKodZv1.TabIndex = 26;
            // 
            // tbRzuMod
            // 
            this.tbRzuMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbRzuMod.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuMod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuMod.Location = new System.Drawing.Point(426, 50);
            this.tbRzuMod.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuMod.Name = "tbRzuMod";
            this.tbRzuMod.Size = new System.Drawing.Size(204, 23);
            this.tbRzuMod.TabIndex = 19;
            // 
            // tbRzuArticul
            // 
            this.tbRzuArticul.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbRzuArticul.Font = new System.Drawing.Font("Arial", 10F);
            this.tbRzuArticul.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbRzuArticul.Location = new System.Drawing.Point(426, 26);
            this.tbRzuArticul.Margin = new System.Windows.Forms.Padding(0);
            this.tbRzuArticul.Name = "tbRzuArticul";
            this.tbRzuArticul.Size = new System.Drawing.Size(204, 23);
            this.tbRzuArticul.TabIndex = 17;
            // 
            // tbPsaPrn
            // 
            this.tbPsaPrn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaPrn.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaPrn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaPrn.Location = new System.Drawing.Point(426, 74);
            this.tbPsaPrn.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaPrn.Multiline = true;
            this.tbPsaPrn.Name = "tbPsaPrn";
            this.tbPsaPrn.Size = new System.Drawing.Size(204, 21);
            this.tbPsaPrn.TabIndex = 15;
            // 
            // tbSostPoln
            // 
            this.tbSostPoln.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbSostPoln.Font = new System.Drawing.Font("Arial", 10F);
            this.tbSostPoln.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbSostPoln.Location = new System.Drawing.Point(726, 50);
            this.tbSostPoln.Margin = new System.Windows.Forms.Padding(0);
            this.tbSostPoln.Multiline = true;
            this.tbSostPoln.Name = "tbSostPoln";
            this.tbSostPoln.Size = new System.Drawing.Size(195, 67);
            this.tbSostPoln.TabIndex = 25;
            // 
            // tbArtGrup
            // 
            this.tbArtGrup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbArtGrup.Font = new System.Drawing.Font("Arial", 10F);
            this.tbArtGrup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbArtGrup.Location = new System.Drawing.Point(726, 26);
            this.tbArtGrup.Margin = new System.Windows.Forms.Padding(0);
            this.tbArtGrup.Name = "tbArtGrup";
            this.tbArtGrup.Size = new System.Drawing.Size(195, 23);
            this.tbArtGrup.TabIndex = 21;
            // 
            // tbArtTradeMark
            // 
            this.tbArtTradeMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbArtTradeMark.Font = new System.Drawing.Font("Arial", 10F);
            this.tbArtTradeMark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbArtTradeMark.Location = new System.Drawing.Point(1018, 96);
            this.tbArtTradeMark.Margin = new System.Windows.Forms.Padding(0);
            this.tbArtTradeMark.Name = "tbArtTradeMark";
            this.tbArtTradeMark.Size = new System.Drawing.Size(70, 23);
            this.tbArtTradeMark.TabIndex = 40;
            // 
            // tbPsaMenName
            // 
            this.tbPsaMenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaMenName.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaMenName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaMenName.Location = new System.Drawing.Point(1004, 50);
            this.tbPsaMenName.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaMenName.Multiline = true;
            this.tbPsaMenName.Name = "tbPsaMenName";
            this.tbPsaMenName.Size = new System.Drawing.Size(174, 21);
            this.tbPsaMenName.TabIndex = 11;
            // 
            // tbPsaNameSbit
            // 
            this.tbPsaNameSbit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaNameSbit.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaNameSbit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaNameSbit.Location = new System.Drawing.Point(1004, 72);
            this.tbPsaNameSbit.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaNameSbit.Name = "tbPsaNameSbit";
            this.tbPsaNameSbit.Size = new System.Drawing.Size(174, 23);
            this.tbPsaNameSbit.TabIndex = 23;
            // 
            // tbPsaTbID
            // 
            this.tbPsaTbID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaTbID.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaTbID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaTbID.Location = new System.Drawing.Point(1004, 26);
            this.tbPsaTbID.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaTbID.Name = "tbPsaTbID";
            this.tbPsaTbID.Size = new System.Drawing.Size(136, 23);
            this.tbPsaTbID.TabIndex = 13;
            // 
            // tbPsaKombIzd
            // 
            this.tbPsaKombIzd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaKombIzd.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaKombIzd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaKombIzd.Location = new System.Drawing.Point(1385, 98);
            this.tbPsaKombIzd.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaKombIzd.Name = "tbPsaKombIzd";
            this.tbPsaKombIzd.Size = new System.Drawing.Size(52, 23);
            this.tbPsaKombIzd.TabIndex = 38;
            // 
            // tbPsaKombOsn
            // 
            this.tbPsaKombOsn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaKombOsn.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaKombOsn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaKombOsn.Location = new System.Drawing.Point(1385, 74);
            this.tbPsaKombOsn.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaKombOsn.Name = "tbPsaKombOsn";
            this.tbPsaKombOsn.Size = new System.Drawing.Size(52, 23);
            this.tbPsaKombOsn.TabIndex = 36;
            // 
            // tbPsaPsaIDOsn
            // 
            this.tbPsaPsaIDOsn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaPsaIDOsn.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaPsaIDOsn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaPsaIDOsn.Location = new System.Drawing.Point(1385, 50);
            this.tbPsaPsaIDOsn.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaPsaIDOsn.Name = "tbPsaPsaIDOsn";
            this.tbPsaPsaIDOsn.Size = new System.Drawing.Size(52, 23);
            this.tbPsaPsaIDOsn.TabIndex = 34;
            // 
            // tbPsaPsaID
            // 
            this.tbPsaPsaID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaPsaID.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaPsaID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaPsaID.Location = new System.Drawing.Point(1385, 26);
            this.tbPsaPsaID.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaPsaID.Name = "tbPsaPsaID";
            this.tbPsaPsaID.Size = new System.Drawing.Size(52, 23);
            this.tbPsaPsaID.TabIndex = 32;
            // 
            // cbIsChip
            // 
            this.cbIsChip.AutoSize = true;
            this.cbIsChip.Enabled = false;
            this.cbIsChip.Location = new System.Drawing.Point(1259, 100);
            this.cbIsChip.Name = "cbIsChip";
            this.cbIsChip.Size = new System.Drawing.Size(45, 17);
            this.cbIsChip.TabIndex = 30;
            this.cbIsChip.Text = "Чип";
            this.cbIsChip.UseVisualStyleBackColor = true;
            // 
            // psaSezName
            // 
            this.psaSezName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.psaSezName.Font = new System.Drawing.Font("Arial", 10F);
            this.psaSezName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.psaSezName.Location = new System.Drawing.Point(1257, 74);
            this.psaSezName.Margin = new System.Windows.Forms.Padding(0);
            this.psaSezName.Name = "psaSezName";
            this.psaSezName.Size = new System.Drawing.Size(47, 23);
            this.psaSezName.TabIndex = 19;
            // 
            // tbPsaYear
            // 
            this.tbPsaYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaYear.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaYear.Location = new System.Drawing.Point(1257, 50);
            this.tbPsaYear.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaYear.Name = "tbPsaYear";
            this.tbPsaYear.Size = new System.Drawing.Size(47, 23);
            this.tbPsaYear.TabIndex = 17;
            // 
            // tbPsaNN
            // 
            this.tbPsaNN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPsaNN.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPsaNN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPsaNN.Location = new System.Drawing.Point(1234, 26);
            this.tbPsaNN.Margin = new System.Windows.Forms.Padding(0);
            this.tbPsaNN.Name = "tbPsaNN";
            this.tbPsaNN.Size = new System.Drawing.Size(70, 23);
            this.tbPsaNN.TabIndex = 9;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.BackColor = System.Drawing.Color.Transparent;
            this.label68.Font = new System.Drawing.Font("Arial", 9F);
            this.label68.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label68.Location = new System.Drawing.Point(924, 100);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(97, 15);
            this.label68.TabIndex = 41;
            this.label68.Text = "Торговая марка";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.BackColor = System.Drawing.Color.Transparent;
            this.label66.Font = new System.Drawing.Font("Arial", 9F);
            this.label66.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label66.Location = new System.Drawing.Point(1316, 102);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(60, 15);
            this.label66.TabIndex = 39;
            this.label66.Text = "komb_izd";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Font = new System.Drawing.Font("Arial", 9F);
            this.label67.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label67.Location = new System.Drawing.Point(1316, 78);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(66, 15);
            this.label67.TabIndex = 37;
            this.label67.Text = "komb_osn";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.BackColor = System.Drawing.Color.Transparent;
            this.label65.Font = new System.Drawing.Font("Arial", 9F);
            this.label65.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label65.Location = new System.Drawing.Point(1316, 54);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(73, 15);
            this.label65.TabIndex = 35;
            this.label65.Text = "psa_id_osn";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Font = new System.Drawing.Font("Arial", 9F);
            this.label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label62.Location = new System.Drawing.Point(1316, 30);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(45, 15);
            this.label62.TabIndex = 33;
            this.label62.Text = "psa_id";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Arial", 9F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label16.Location = new System.Drawing.Point(1215, 74);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 15);
            this.label16.TabIndex = 20;
            this.label16.Text = "Сезон";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Arial", 9F);
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label17.Location = new System.Drawing.Point(1215, 54);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 15);
            this.label17.TabIndex = 18;
            this.label17.Text = "Год";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Arial", 9F);
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label22.Location = new System.Drawing.Point(513, 100);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 15);
            this.label22.TabIndex = 30;
            this.label22.Text = "Код цв.2";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.BackColor = System.Drawing.Color.Transparent;
            this.label53.Font = new System.Drawing.Font("Arial", 9F);
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label53.Location = new System.Drawing.Point(8, 54);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(71, 15);
            this.label53.TabIndex = 27;
            this.label53.Text = "№ задания";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Arial", 9F);
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label23.Location = new System.Drawing.Point(367, 100);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(54, 15);
            this.label23.TabIndex = 27;
            this.label23.Text = "Код цв.1";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Arial", 9F);
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label20.Location = new System.Drawing.Point(926, 47);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 30);
            this.label20.TabIndex = 12;
            this.label20.Text = "Категория\r\n(менеджер)";
            // 
            // pbEskiz
            // 
            this.pbEskiz.Location = new System.Drawing.Point(1439, 6);
            this.pbEskiz.Name = "pbEskiz";
            this.pbEskiz.Size = new System.Drawing.Size(118, 112);
            this.pbEskiz.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEskiz.TabIndex = 25;
            this.pbEskiz.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial", 9F);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(636, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 30);
            this.label13.TabIndex = 24;
            this.label13.Text = "Состав\r\n(из справ.)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(926, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 15);
            this.label11.TabIndex = 24;
            this.label11.Text = "Канал сбыта";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Arial", 9F);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(636, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 30);
            this.label15.TabIndex = 22;
            this.label15.Text = "Наименование\r\n(из справ.)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Arial", 9F);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(158, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 15);
            this.label12.TabIndex = 22;
            this.label12.Text = "Бригада №";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 9F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(324, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 15);
            this.label10.TabIndex = 20;
            this.label10.Text = "Модель (торг.)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Arial", 9F);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(926, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 15);
            this.label19.TabIndex = 14;
            this.label19.Text = "Блок";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(324, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 15);
            this.label9.TabIndex = 18;
            this.label9.Text = "Артикул (шв.)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(324, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "Цвет по заданию";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(158, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Кол-во в расчете";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(8, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "№ пачек в расчете";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(8, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "№ расчета";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(710, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "год";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Arial", 9F);
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label21.Location = new System.Drawing.Point(1149, 30);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(82, 15);
            this.label21.TabIndex = 10;
            this.label21.Text = "Код матрицы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(576, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "№ пачки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(497, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "ПОИСК:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "КАРТОЧКА РАСЧЕТА";
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
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.sbProizvCombIzdSP);
            this.xtraTabPage4.Controls.Add(this.label70);
            this.xtraTabPage4.Controls.Add(this.label69);
            this.xtraTabPage4.Controls.Add(this.gcProizvCombIzdSP);
            this.xtraTabPage4.Controls.Add(this.gridControl4);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1569, 575);
            this.xtraTabPage4.Text = "ДЕТАЛИ ОТДЕЛКИ";
            // 
            // sbProizvCombIzdSP
            // 
            this.sbProizvCombIzdSP.Location = new System.Drawing.Point(1475, 245);
            this.sbProizvCombIzdSP.Name = "sbProizvCombIzdSP";
            this.sbProizvCombIzdSP.Size = new System.Drawing.Size(72, 23);
            this.sbProizvCombIzdSP.TabIndex = 7;
            this.sbProizvCombIzdSP.Text = "Печать";
            this.sbProizvCombIzdSP.Click += new System.EventHandler(this.sbProizvCombIzdSP_Click);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.BackColor = System.Drawing.Color.Transparent;
            this.label70.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label70.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label70.Location = new System.Drawing.Point(3, 279);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(42, 19);
            this.label70.TabIndex = 6;
            this.label70.Text = "ВЗП";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.BackColor = System.Drawing.Color.Transparent;
            this.label69.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label69.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label69.Location = new System.Drawing.Point(3, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(38, 19);
            this.label69.TabIndex = 5;
            this.label69.Text = "ШП";
            // 
            // gcProizvCombIzdSP
            // 
            this.gcProizvCombIzdSP.DataSource = this.bsProizvCombIzdSP;
            this.gcProizvCombIzdSP.Location = new System.Drawing.Point(3, 21);
            this.gcProizvCombIzdSP.MainView = this.gridView4;
            this.gcProizvCombIzdSP.Name = "gcProizvCombIzdSP";
            this.gcProizvCombIzdSP.Size = new System.Drawing.Size(1544, 252);
            this.gcProizvCombIzdSP.TabIndex = 4;
            this.gcProizvCombIzdSP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.gridView4.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView4.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView4.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn47,
            this.gridColumn48,
            this.gridColumn49,
            this.gridColumn50,
            this.gridColumn51,
            this.gridColumn52,
            this.gridColumn53,
            this.gridColumn54,
            this.gridColumn55,
            this.gridColumn56,
            this.gridColumn58,
            this.gridColumn74,
            this.gridColumn75,
            this.gridColumn76,
            this.gridColumn77});
            this.gridView4.CustomizationFormBounds = new System.Drawing.Rectangle(3464, 607, 264, 272);
            this.gridView4.GridControl = this.gcProizvCombIzdSP;
            this.gridView4.GroupCount = 3;
            this.gridView4.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRaskr", null, "(Раскроено всего: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRab", null, "(В работе всего: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", null, "(Сдано на склад всего: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", null, "(Принято на склад фурнитуры всего: {0:0.##})")});
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView4.OptionsView.ShowFooter = true;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn49, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn48, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn54, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn47
            // 
            this.gridColumn47.Caption = "Модель";
            this.gridColumn47.FieldName = "RzuMod";
            this.gridColumn47.Name = "gridColumn47";
            this.gridColumn47.OptionsColumn.FixedWidth = true;
            this.gridColumn47.OptionsEditForm.Caption = "Psz Mod:";
            this.gridColumn47.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn47.OptionsFilter.AllowFilter = false;
            this.gridColumn47.Visible = true;
            this.gridColumn47.VisibleIndex = 2;
            this.gridColumn47.Width = 100;
            // 
            // gridColumn48
            // 
            this.gridColumn48.Caption = "Задание";
            this.gridColumn48.FieldName = "PszNom";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 0;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "Цвет";
            this.gridColumn49.FieldName = "PszZvet";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 0;
            // 
            // gridColumn50
            // 
            this.gridColumn50.Caption = "Артикул";
            this.gridColumn50.FieldName = "RzuArticul";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.OptionsColumn.FixedWidth = true;
            this.gridColumn50.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn50.OptionsFilter.AllowFilter = false;
            this.gridColumn50.Visible = true;
            this.gridColumn50.VisibleIndex = 1;
            this.gridColumn50.Width = 100;
            // 
            // gridColumn51
            // 
            this.gridColumn51.Caption = "Группа";
            this.gridColumn51.FieldName = "RzuGrup";
            this.gridColumn51.Name = "gridColumn51";
            this.gridColumn51.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn51.OptionsFilter.AllowFilter = false;
            this.gridColumn51.Visible = true;
            this.gridColumn51.VisibleIndex = 0;
            this.gridColumn51.Width = 299;
            // 
            // gridColumn52
            // 
            this.gridColumn52.Caption = "Размер";
            this.gridColumn52.FieldName = "RzuRazm";
            this.gridColumn52.Name = "gridColumn52";
            this.gridColumn52.OptionsColumn.FixedWidth = true;
            this.gridColumn52.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn52.OptionsFilter.AllowFilter = false;
            this.gridColumn52.Visible = true;
            this.gridColumn52.VisibleIndex = 3;
            this.gridColumn52.Width = 80;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "Кол-во";
            this.gridColumn53.FieldName = "KolItog";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.OptionsColumn.FixedWidth = true;
            this.gridColumn53.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn53.OptionsFilter.AllowFilter = false;
            this.gridColumn53.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolItog", "ИТОГО = {0:0.##}")});
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 4;
            this.gridColumn53.Width = 90;
            // 
            // gridColumn54
            // 
            this.gridColumn54.Caption = "№ накладной";
            this.gridColumn54.FieldName = "NIz";
            this.gridColumn54.Name = "gridColumn54";
            this.gridColumn54.Visible = true;
            this.gridColumn54.VisibleIndex = 0;
            // 
            // gridColumn55
            // 
            this.gridColumn55.Caption = "Кол-во раскроено";
            this.gridColumn55.FieldName = "KolRaskr";
            this.gridColumn55.Name = "gridColumn55";
            this.gridColumn55.OptionsColumn.FixedWidth = true;
            this.gridColumn55.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn55.OptionsFilter.AllowFilter = false;
            this.gridColumn55.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRaskr", "Раскроено: {0:0.##}")});
            this.gridColumn55.Visible = true;
            this.gridColumn55.VisibleIndex = 5;
            this.gridColumn55.Width = 150;
            // 
            // gridColumn56
            // 
            this.gridColumn56.Caption = "Количество в работе";
            this.gridColumn56.FieldName = "KolRab";
            this.gridColumn56.Name = "gridColumn56";
            this.gridColumn56.OptionsColumn.FixedWidth = true;
            this.gridColumn56.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn56.OptionsFilter.AllowFilter = false;
            this.gridColumn56.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolRab", "В работе: {0:0.##}")});
            this.gridColumn56.Visible = true;
            this.gridColumn56.VisibleIndex = 6;
            this.gridColumn56.Width = 150;
            // 
            // gridColumn58
            // 
            this.gridColumn58.Caption = "Кол-во прин. на скл. фурн.";
            this.gridColumn58.FieldName = "KolFurnPrinSkl";
            this.gridColumn58.Name = "gridColumn58";
            this.gridColumn58.OptionsColumn.FixedWidth = true;
            this.gridColumn58.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn58.OptionsFilter.AllowFilter = false;
            this.gridColumn58.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", "Прин. на скл. Фурн.: {0:0.##}")});
            this.gridColumn58.Visible = true;
            this.gridColumn58.VisibleIndex = 10;
            this.gridColumn58.Width = 150;
            // 
            // gridColumn74
            // 
            this.gridColumn74.Caption = "Дата в работу";
            this.gridColumn74.FieldName = "RzuDataRab";
            this.gridColumn74.Name = "gridColumn74";
            this.gridColumn74.OptionsColumn.FixedWidth = true;
            this.gridColumn74.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn74.OptionsFilter.AllowFilter = false;
            this.gridColumn74.Visible = true;
            this.gridColumn74.VisibleIndex = 7;
            this.gridColumn74.Width = 90;
            // 
            // gridColumn75
            // 
            this.gridColumn75.Caption = "Дата отгр. на склад";
            this.gridColumn75.FieldName = "NDostData";
            this.gridColumn75.Name = "gridColumn75";
            this.gridColumn75.OptionsColumn.FixedWidth = true;
            this.gridColumn75.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn75.OptionsFilter.AllowFilter = false;
            this.gridColumn75.Visible = true;
            this.gridColumn75.VisibleIndex = 9;
            this.gridColumn75.Width = 90;
            // 
            // gridColumn76
            // 
            this.gridColumn76.Caption = "Дата прин. на скл. фурн.";
            this.gridColumn76.FieldName = "DateFurnPrihSkl";
            this.gridColumn76.Name = "gridColumn76";
            this.gridColumn76.OptionsColumn.FixedWidth = true;
            this.gridColumn76.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn76.OptionsFilter.AllowFilter = false;
            this.gridColumn76.Visible = true;
            this.gridColumn76.VisibleIndex = 11;
            this.gridColumn76.Width = 90;
            // 
            // gridColumn77
            // 
            this.gridColumn77.Caption = "Кол-во отгр. на склад";
            this.gridColumn77.FieldName = "KolGI";
            this.gridColumn77.Name = "gridColumn77";
            this.gridColumn77.OptionsColumn.FixedWidth = true;
            this.gridColumn77.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn77.OptionsFilter.AllowFilter = false;
            this.gridColumn77.Visible = true;
            this.gridColumn77.VisibleIndex = 8;
            this.gridColumn77.Width = 150;
            // 
            // gridControl4
            // 
            this.gridControl4.DataSource = this.bsProizvCombIzdVZP;
            this.gridControl4.Location = new System.Drawing.Point(3, 301);
            this.gridControl4.MainView = this.gridView6;
            this.gridControl4.Name = "gridControl4";
            this.gridControl4.Size = new System.Drawing.Size(1544, 269);
            this.gridControl4.TabIndex = 4;
            this.gridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView6});
            // 
            // gridView6
            // 
            this.gridView6.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gridView6.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView6.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.gridView6.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView6.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView6.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn59,
            this.gridColumn60,
            this.gridColumn61,
            this.gridColumn62,
            this.gridColumn63,
            this.gridColumn64,
            this.gridColumn65,
            this.gridColumn66,
            this.gridColumn67,
            this.gridColumn68,
            this.gridColumn69,
            this.gridColumn70,
            this.gridColumn71,
            this.gridColumn72,
            this.gridColumn73});
            this.gridView6.CustomizationFormBounds = new System.Drawing.Rectangle(3464, 607, 264, 272);
            this.gridView6.GridControl = this.gridControl4;
            this.gridView6.GroupCount = 3;
            this.gridView6.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolVyaz", null, "(Вязание: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolOtparka", null, "(Отпарка: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", null, "(Сдано на склад всего: {0:0.##})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", null, "(Принято на склад фурнитуры всего: {0:0.##})")});
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView6.OptionsView.ShowFooter = true;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            this.gridView6.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn61, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn60, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn66, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn59
            // 
            this.gridColumn59.Caption = "Модель";
            this.gridColumn59.FieldName = "RzvMod";
            this.gridColumn59.Name = "gridColumn59";
            this.gridColumn59.OptionsColumn.FixedWidth = true;
            this.gridColumn59.OptionsEditForm.Caption = "Psz Mod:";
            this.gridColumn59.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn59.OptionsFilter.AllowFilter = false;
            this.gridColumn59.Visible = true;
            this.gridColumn59.VisibleIndex = 2;
            this.gridColumn59.Width = 100;
            // 
            // gridColumn60
            // 
            this.gridColumn60.Caption = "Задание";
            this.gridColumn60.FieldName = "PszNom";
            this.gridColumn60.Name = "gridColumn60";
            this.gridColumn60.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn60.OptionsFilter.AllowFilter = false;
            this.gridColumn60.Visible = true;
            this.gridColumn60.VisibleIndex = 0;
            // 
            // gridColumn61
            // 
            this.gridColumn61.Caption = "Цвет";
            this.gridColumn61.FieldName = "PszZvet";
            this.gridColumn61.Name = "gridColumn61";
            this.gridColumn61.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn61.OptionsFilter.AllowFilter = false;
            this.gridColumn61.Visible = true;
            this.gridColumn61.VisibleIndex = 0;
            // 
            // gridColumn62
            // 
            this.gridColumn62.Caption = "Артикул";
            this.gridColumn62.FieldName = "RzvArticul";
            this.gridColumn62.Name = "gridColumn62";
            this.gridColumn62.OptionsColumn.FixedWidth = true;
            this.gridColumn62.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn62.OptionsFilter.AllowFilter = false;
            this.gridColumn62.Visible = true;
            this.gridColumn62.VisibleIndex = 1;
            this.gridColumn62.Width = 100;
            // 
            // gridColumn63
            // 
            this.gridColumn63.Caption = "Группа";
            this.gridColumn63.FieldName = "RzvGrup";
            this.gridColumn63.Name = "gridColumn63";
            this.gridColumn63.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn63.OptionsFilter.AllowFilter = false;
            this.gridColumn63.Visible = true;
            this.gridColumn63.VisibleIndex = 0;
            this.gridColumn63.Width = 299;
            // 
            // gridColumn64
            // 
            this.gridColumn64.Caption = "Размер";
            this.gridColumn64.FieldName = "RzvRazm";
            this.gridColumn64.Name = "gridColumn64";
            this.gridColumn64.OptionsColumn.FixedWidth = true;
            this.gridColumn64.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn64.OptionsFilter.AllowFilter = false;
            this.gridColumn64.Visible = true;
            this.gridColumn64.VisibleIndex = 3;
            this.gridColumn64.Width = 80;
            // 
            // gridColumn65
            // 
            this.gridColumn65.Caption = "Кол-во";
            this.gridColumn65.FieldName = "KolItog";
            this.gridColumn65.Name = "gridColumn65";
            this.gridColumn65.OptionsColumn.FixedWidth = true;
            this.gridColumn65.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn65.OptionsFilter.AllowFilter = false;
            this.gridColumn65.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolItog", "ИТОГО = {0:0.##}")});
            this.gridColumn65.Visible = true;
            this.gridColumn65.VisibleIndex = 4;
            this.gridColumn65.Width = 90;
            // 
            // gridColumn66
            // 
            this.gridColumn66.Caption = "№ накладной";
            this.gridColumn66.FieldName = "NIz";
            this.gridColumn66.Name = "gridColumn66";
            this.gridColumn66.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn66.OptionsFilter.AllowFilter = false;
            this.gridColumn66.Visible = true;
            this.gridColumn66.VisibleIndex = 0;
            // 
            // gridColumn67
            // 
            this.gridColumn67.Caption = "Кол-во на вязании";
            this.gridColumn67.FieldName = "KolVyaz";
            this.gridColumn67.Name = "gridColumn67";
            this.gridColumn67.OptionsColumn.FixedWidth = true;
            this.gridColumn67.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn67.OptionsFilter.AllowFilter = false;
            this.gridColumn67.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolVyaz", "Вязание: {0:0.##}")});
            this.gridColumn67.Visible = true;
            this.gridColumn67.VisibleIndex = 5;
            this.gridColumn67.Width = 150;
            // 
            // gridColumn68
            // 
            this.gridColumn68.Caption = "Кол-во на отпарке";
            this.gridColumn68.FieldName = "KolOtparka";
            this.gridColumn68.Name = "gridColumn68";
            this.gridColumn68.OptionsColumn.FixedWidth = true;
            this.gridColumn68.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn68.OptionsFilter.AllowFilter = false;
            this.gridColumn68.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolOtparka", "Отпарка: {0:0.##}")});
            this.gridColumn68.Visible = true;
            this.gridColumn68.VisibleIndex = 6;
            this.gridColumn68.Width = 150;
            // 
            // gridColumn69
            // 
            this.gridColumn69.Caption = "Кол-во отгр. на склад";
            this.gridColumn69.FieldName = "KolGI";
            this.gridColumn69.Name = "gridColumn69";
            this.gridColumn69.OptionsColumn.FixedWidth = true;
            this.gridColumn69.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn69.OptionsFilter.AllowFilter = false;
            this.gridColumn69.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolGI", "Отгр. на скл.: {0:0.##}")});
            this.gridColumn69.Visible = true;
            this.gridColumn69.VisibleIndex = 8;
            this.gridColumn69.Width = 150;
            // 
            // gridColumn70
            // 
            this.gridColumn70.Caption = "Кол-во прин. на скл. фурн.";
            this.gridColumn70.FieldName = "KolFurnPrinSkl";
            this.gridColumn70.Name = "gridColumn70";
            this.gridColumn70.OptionsColumn.FixedWidth = true;
            this.gridColumn70.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn70.OptionsFilter.AllowFilter = false;
            this.gridColumn70.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KolFurnPrinSkl", "Прин. на скл. Фурн.: {0:0.##}")});
            this.gridColumn70.Visible = true;
            this.gridColumn70.VisibleIndex = 10;
            this.gridColumn70.Width = 150;
            // 
            // gridColumn71
            // 
            this.gridColumn71.Caption = "Дата отпарки";
            this.gridColumn71.FieldName = "RzvDateOkonV";
            this.gridColumn71.Name = "gridColumn71";
            this.gridColumn71.OptionsColumn.FixedWidth = true;
            this.gridColumn71.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn71.OptionsFilter.AllowFilter = false;
            this.gridColumn71.Visible = true;
            this.gridColumn71.VisibleIndex = 7;
            this.gridColumn71.Width = 90;
            // 
            // gridColumn72
            // 
            this.gridColumn72.Caption = "Дата отгр. на склад";
            this.gridColumn72.FieldName = "NDostData";
            this.gridColumn72.Name = "gridColumn72";
            this.gridColumn72.OptionsColumn.FixedWidth = true;
            this.gridColumn72.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn72.OptionsFilter.AllowFilter = false;
            this.gridColumn72.Visible = true;
            this.gridColumn72.VisibleIndex = 9;
            this.gridColumn72.Width = 90;
            // 
            // gridColumn73
            // 
            this.gridColumn73.Caption = "Дата прин. на скл. фурн.";
            this.gridColumn73.FieldName = "DateFurnPrihSkl";
            this.gridColumn73.Name = "gridColumn73";
            this.gridColumn73.OptionsColumn.FixedWidth = true;
            this.gridColumn73.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn73.OptionsFilter.AllowFilter = false;
            this.gridColumn73.Visible = true;
            this.gridColumn73.VisibleIndex = 11;
            this.gridColumn73.Width = 90;
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn40,
            this.gridColumn44,
            this.gridColumn45,
            this.gridColumn46});
            gridFormatRule12.Column = this.gridColumn40;
            gridFormatRule12.ColumnApplyTo = this.gridColumn15;
            gridFormatRule12.Name = "Format0";
            formatConditionRuleValue12.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue12.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue12.Appearance.Options.UseFont = true;
            formatConditionRuleValue12.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue12.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue12.Value1 = "red";
            gridFormatRule12.Rule = formatConditionRuleValue12;
            gridFormatRule13.Column = this.gridColumn40;
            gridFormatRule13.ColumnApplyTo = this.gridColumn15;
            gridFormatRule13.Name = "Format1";
            formatConditionRuleValue13.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue13.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue13.Appearance.Options.UseFont = true;
            formatConditionRuleValue13.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue13.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue13.Value1 = "green";
            gridFormatRule13.Rule = formatConditionRuleValue13;
            gridFormatRule14.Column = this.gridColumn44;
            gridFormatRule14.ColumnApplyTo = this.gridColumn14;
            gridFormatRule14.Name = "Format2";
            formatConditionRuleValue14.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue14.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue14.Appearance.Options.UseFont = true;
            formatConditionRuleValue14.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue14.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue14.Value1 = "green";
            gridFormatRule14.Rule = formatConditionRuleValue14;
            gridFormatRule15.Column = this.gridColumn44;
            gridFormatRule15.ColumnApplyTo = this.gridColumn14;
            gridFormatRule15.Name = "Format3";
            formatConditionRuleValue15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue15.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue15.Appearance.Options.UseFont = true;
            formatConditionRuleValue15.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue15.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue15.Value1 = "red";
            gridFormatRule15.Rule = formatConditionRuleValue15;
            gridFormatRule16.Column = this.gridColumn44;
            gridFormatRule16.ColumnApplyTo = this.gridColumn14;
            gridFormatRule16.Name = "Format4";
            formatConditionRuleValue16.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue16.Appearance.ForeColor = System.Drawing.Color.Gray;
            formatConditionRuleValue16.Appearance.Options.UseFont = true;
            formatConditionRuleValue16.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue16.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue16.Value1 = "gray";
            gridFormatRule16.Rule = formatConditionRuleValue16;
            gridFormatRule17.Column = this.gridColumn45;
            gridFormatRule17.ColumnApplyTo = this.gridColumn13;
            gridFormatRule17.Name = "Format5";
            formatConditionRuleValue17.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue17.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue17.Appearance.Options.UseFont = true;
            formatConditionRuleValue17.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue17.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue17.Value1 = "red";
            gridFormatRule17.Rule = formatConditionRuleValue17;
            gridFormatRule18.Column = this.gridColumn45;
            gridFormatRule18.ColumnApplyTo = this.gridColumn13;
            gridFormatRule18.Name = "Format6";
            formatConditionRuleValue18.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue18.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue18.Appearance.Options.UseFont = true;
            formatConditionRuleValue18.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue18.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue18.Value1 = "green";
            gridFormatRule18.Rule = formatConditionRuleValue18;
            gridFormatRule19.Column = this.gridColumn45;
            gridFormatRule19.ColumnApplyTo = this.gridColumn13;
            gridFormatRule19.Name = "Format7";
            formatConditionRuleValue19.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue19.Appearance.ForeColor = System.Drawing.Color.Gray;
            formatConditionRuleValue19.Appearance.Options.UseFont = true;
            formatConditionRuleValue19.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue19.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue19.Value1 = "gray";
            gridFormatRule19.Rule = formatConditionRuleValue19;
            gridFormatRule20.Column = this.gridColumn46;
            gridFormatRule20.ColumnApplyTo = this.gridColumn12;
            gridFormatRule20.Name = "Format8";
            formatConditionRuleValue20.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue20.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue20.Appearance.Options.UseFont = true;
            formatConditionRuleValue20.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue20.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue20.Value1 = "red";
            gridFormatRule20.Rule = formatConditionRuleValue20;
            gridFormatRule21.Column = this.gridColumn46;
            gridFormatRule21.ColumnApplyTo = this.gridColumn12;
            gridFormatRule21.Name = "Format9";
            formatConditionRuleValue21.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue21.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            formatConditionRuleValue21.Appearance.Options.UseFont = true;
            formatConditionRuleValue21.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue21.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue21.Value1 = "green";
            gridFormatRule21.Rule = formatConditionRuleValue21;
            gridFormatRule22.Column = this.gridColumn46;
            gridFormatRule22.ColumnApplyTo = this.gridColumn12;
            gridFormatRule22.Name = "Format10";
            formatConditionRuleValue22.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue22.Appearance.ForeColor = System.Drawing.Color.Gray;
            formatConditionRuleValue22.Appearance.Options.UseFont = true;
            formatConditionRuleValue22.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue22.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue22.Value1 = "gray";
            gridFormatRule22.Rule = formatConditionRuleValue22;
            this.gridView1.FormatRules.Add(gridFormatRule12);
            this.gridView1.FormatRules.Add(gridFormatRule13);
            this.gridView1.FormatRules.Add(gridFormatRule14);
            this.gridView1.FormatRules.Add(gridFormatRule15);
            this.gridView1.FormatRules.Add(gridFormatRule16);
            this.gridView1.FormatRules.Add(gridFormatRule17);
            this.gridView1.FormatRules.Add(gridFormatRule18);
            this.gridView1.FormatRules.Add(gridFormatRule19);
            this.gridView1.FormatRules.Add(gridFormatRule20);
            this.gridView1.FormatRules.Add(gridFormatRule21);
            this.gridView1.FormatRules.Add(gridFormatRule22);
            this.gridView1.GridControl = this.gcNaklList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Кол-во ДО";
            this.gridColumn1.FieldName = "kol_b";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 13;
            this.gridColumn1.Width = 60;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Причина деления";
            this.gridColumn2.FieldName = "prich";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 12;
            this.gridColumn2.Width = 38;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Склад отгрузки";
            this.gridColumn3.FieldName = "skl_naimen";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 11;
            this.gridColumn3.Width = 150;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.Caption = "№ накл. Глобал";
            this.gridColumn4.FieldName = "gl_nomer";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 10;
            this.gridColumn4.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn5.Caption = "Дата печати";
            this.gridColumn5.FieldName = "date_print";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 9;
            this.gridColumn5.Width = 90;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "№ отгр.";
            this.gridColumn6.FieldName = "dost_n";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn6.OptionsFilter.AllowFilter = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 8;
            this.gridColumn6.Width = 60;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn7.Caption = "Доставка на склад";
            this.gridColumn7.FieldName = "dost_data";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn7.OptionsFilter.AllowFilter = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 90;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn8.Caption = "Дата деления";
            this.gridColumn8.FieldName = "data_izm";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn8.OptionsFilter.AllowFilter = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 90;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "Дата накл.";
            this.gridColumn9.FieldName = "iz_data";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.FixedWidth = true;
            this.gridColumn9.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn9.OptionsFilter.AllowFilter = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            this.gridColumn9.Width = 90;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "№ накл.";
            this.gridColumn10.FieldName = "iz_nakl";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.FixedWidth = true;
            this.gridColumn10.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn10.OptionsFilter.AllowFilter = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 60;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn11.Caption = "Кол-во ПОСЛЕ";
            this.gridColumn11.FieldName = "kol_c";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.FixedWidth = true;
            this.gridColumn11.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn11.OptionsFilter.AllowFilter = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 14;
            this.gridColumn11.Width = 60;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Модель";
            this.gridColumn16.FieldName = "mod";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.FixedWidth = true;
            this.gridColumn16.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn16.OptionsFilter.AllowFilter = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 16;
            this.gridColumn16.Width = 120;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Артикул";
            this.gridColumn17.FieldName = "articul";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.FixedWidth = true;
            this.gridColumn17.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn17.OptionsFilter.AllowFilter = false;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 15;
            this.gridColumn17.Width = 120;
            // 
            // gcNaklList
            // 
            this.gcNaklList.DataSource = this.bsNaklList;
            this.gcNaklList.Location = new System.Drawing.Point(8, 24);
            this.gcNaklList.MainView = this.gridView1;
            this.gcNaklList.Name = "gcNaklList";
            this.gcNaklList.Size = new System.Drawing.Size(1545, 163);
            this.gcNaklList.TabIndex = 5;
            this.gcNaklList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.GridControl = this.gcNaklList;
            this.gridView5.Name = "gridView5";
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn29,
            this.gridColumn30,
            this.gridColumn31,
            this.gridColumn32,
            this.gridColumn33});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn29
            // 
            this.gridColumn29.Caption = "Вид изделия";
            this.gridColumn29.FieldName = "VidIzdName";
            this.gridColumn29.Name = "gridColumn29";
            this.gridColumn29.OptionsColumn.FixedWidth = true;
            this.gridColumn29.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn29.OptionsFilter.AllowFilter = false;
            this.gridColumn29.Visible = true;
            this.gridColumn29.VisibleIndex = 4;
            this.gridColumn29.Width = 120;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Деталь изделия";
            this.gridColumn30.FieldName = "DetIzdName";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.OptionsColumn.FixedWidth = true;
            this.gridColumn30.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn30.OptionsFilter.AllowFilter = false;
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 3;
            this.gridColumn30.Width = 100;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "Формат";
            this.gridColumn31.FieldName = "frt_naimen";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.OptionsColumn.FixedWidth = true;
            this.gridColumn31.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn31.OptionsFilter.AllowFilter = false;
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 2;
            this.gridColumn31.Width = 50;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "Кол-во/слож/цвет/прогон";
            this.gridColumn32.FieldName = "kol_sl_zv";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.OptionsColumn.FixedWidth = true;
            this.gridColumn32.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn32.OptionsFilter.AllowFilter = false;
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 1;
            this.gridColumn32.Width = 140;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "Вид отделки";
            this.gridColumn33.FieldName = "psa_field_name";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.OptionsColumn.FixedWidth = true;
            this.gridColumn33.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn33.OptionsFilter.AllowFilter = false;
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 0;
            this.gridColumn33.Width = 90;
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.bsOtdelkaList;
            this.gridControl2.Location = new System.Drawing.Point(1031, 2);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(522, 188);
            this.gridControl2.TabIndex = 5;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2,
            this.gridView7});
            // 
            // gridView7
            // 
            this.gridView7.GridControl = this.gridControl2;
            this.gridView7.Name = "gridView7";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.xtraTabPage3.Appearance.HeaderActive.Options.UseFont = true;
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1569, 575);
            this.xtraTabPage3.Text = "ВЫПОЛНЕННАЯ РАБОТА";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.furnitZayavViewFurnit);
            this.xtraTabPage2.Controls.Add(this.furnitZayavViewUpak);
            this.xtraTabPage2.Controls.Add(this.panelControl7);
            this.xtraTabPage2.Controls.Add(this.panelControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1569, 575);
            this.xtraTabPage2.Text = "КОНФЕКЦИОН";
            // 
            // furnitZayavViewFurnit
            // 
            this.furnitZayavViewFurnit.Location = new System.Drawing.Point(216, 12);
            this.furnitZayavViewFurnit.Name = "furnitZayavViewFurnit";
            this.furnitZayavViewFurnit.Size = new System.Drawing.Size(1349, 264);
            this.furnitZayavViewFurnit.TabIndex = 4;
            this.furnitZayavViewFurnit.ViewType = "";
            // 
            // furnitZayavViewUpak
            // 
            this.furnitZayavViewUpak.Location = new System.Drawing.Point(217, 308);
            this.furnitZayavViewUpak.Name = "furnitZayavViewUpak";
            this.furnitZayavViewUpak.Size = new System.Drawing.Size(1349, 264);
            this.furnitZayavViewUpak.TabIndex = 3;
            this.furnitZayavViewUpak.ViewType = "";
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.tbDatZayav);
            this.panelControl7.Controls.Add(this.button10);
            this.panelControl7.Controls.Add(this.label61);
            this.panelControl7.Location = new System.Drawing.Point(3, 489);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(210, 83);
            this.panelControl7.TabIndex = 1;
            // 
            // tbDatZayav
            // 
            this.tbDatZayav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbDatZayav.Font = new System.Drawing.Font("Arial", 10F);
            this.tbDatZayav.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbDatZayav.Location = new System.Drawing.Point(93, 31);
            this.tbDatZayav.Margin = new System.Windows.Forms.Padding(0);
            this.tbDatZayav.Name = "tbDatZayav";
            this.tbDatZayav.Size = new System.Drawing.Size(110, 23);
            this.tbDatZayav.TabIndex = 70;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Arial", 10F);
            this.button10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.button10.Location = new System.Drawing.Point(5, 58);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(200, 20);
            this.button10.TabIndex = 60;
            this.button10.Text = "Информация по доставке";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Font = new System.Drawing.Font("Arial", 9F);
            this.label61.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label61.Location = new System.Drawing.Point(2, 3);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(227, 30);
            this.label61.TabIndex = 0;
            this.label61.Text = "Предположительная дата создания \r\nзаявки (при выполнении всех условий)";
            this.label61.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnZayavUpakPrint);
            this.panelControl2.Controls.Add(this.tbData_f_z_u);
            this.panelControl2.Controls.Add(this.tbData_f_o_u);
            this.panelControl2.Controls.Add(this.tbData_f_z);
            this.panelControl2.Controls.Add(this.tbData_f_o);
            this.panelControl2.Controls.Add(this.mtbData_cd);
            this.panelControl2.Controls.Add(this.mtbData_zeh);
            this.panelControl2.Controls.Add(this.tbOtgrStat);
            this.panelControl2.Controls.Add(this.tbIs_got);
            this.panelControl2.Controls.Add(this.label60);
            this.panelControl2.Controls.Add(this.label59);
            this.panelControl2.Controls.Add(this.tbUZSobrStat);
            this.panelControl2.Controls.Add(this.label56);
            this.panelControl2.Controls.Add(this.tbUZSozdStat);
            this.panelControl2.Controls.Add(this.label57);
            this.panelControl2.Controls.Add(this.tbUpakZayav);
            this.panelControl2.Controls.Add(this.label58);
            this.panelControl2.Controls.Add(this.btnZayavFurnPrint);
            this.panelControl2.Controls.Add(this.tbFZSobrStat);
            this.panelControl2.Controls.Add(this.label55);
            this.panelControl2.Controls.Add(this.tbFZSozdStat);
            this.panelControl2.Controls.Add(this.label54);
            this.panelControl2.Controls.Add(this.tbFurnZayav);
            this.panelControl2.Controls.Add(this.label25);
            this.panelControl2.Controls.Add(this.btnFullKKPrint);
            this.panelControl2.Controls.Add(this.tbUpakKKStat);
            this.panelControl2.Controls.Add(this.tbFurnKKStat);
            this.panelControl2.Controls.Add(this.btnUpakKKPrint);
            this.panelControl2.Controls.Add(this.btnFurnKKPrint);
            this.panelControl2.Controls.Add(this.label18);
            this.panelControl2.Location = new System.Drawing.Point(3, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(210, 460);
            this.panelControl2.TabIndex = 0;
            // 
            // btnZayavUpakPrint
            // 
            this.btnZayavUpakPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnZayavUpakPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZayavUpakPrint.Font = new System.Drawing.Font("Arial", 10F);
            this.btnZayavUpakPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnZayavUpakPrint.Location = new System.Drawing.Point(5, 362);
            this.btnZayavUpakPrint.Name = "btnZayavUpakPrint";
            this.btnZayavUpakPrint.Size = new System.Drawing.Size(200, 24);
            this.btnZayavUpakPrint.TabIndex = 70;
            this.btnZayavUpakPrint.Text = "ПРОСМОТР / ПЕЧАТЬ";
            this.btnZayavUpakPrint.UseVisualStyleBackColor = true;
            this.btnZayavUpakPrint.Click += new System.EventHandler(this.btnZayavFurnPrint_Click);
            // 
            // tbData_f_z_u
            // 
            this.tbData_f_z_u.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbData_f_z_u.Font = new System.Drawing.Font("Arial", 10F);
            this.tbData_f_z_u.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbData_f_z_u.Location = new System.Drawing.Point(64, 337);
            this.tbData_f_z_u.Margin = new System.Windows.Forms.Padding(0);
            this.tbData_f_z_u.Name = "tbData_f_z_u";
            this.tbData_f_z_u.Size = new System.Drawing.Size(110, 23);
            this.tbData_f_z_u.TabIndex = 69;
            // 
            // tbData_f_o_u
            // 
            this.tbData_f_o_u.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbData_f_o_u.Font = new System.Drawing.Font("Arial", 10F);
            this.tbData_f_o_u.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbData_f_o_u.Location = new System.Drawing.Point(64, 313);
            this.tbData_f_o_u.Margin = new System.Windows.Forms.Padding(0);
            this.tbData_f_o_u.Name = "tbData_f_o_u";
            this.tbData_f_o_u.Size = new System.Drawing.Size(110, 23);
            this.tbData_f_o_u.TabIndex = 68;
            // 
            // tbData_f_z
            // 
            this.tbData_f_z.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbData_f_z.Font = new System.Drawing.Font("Arial", 10F);
            this.tbData_f_z.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbData_f_z.Location = new System.Drawing.Point(64, 219);
            this.tbData_f_z.Margin = new System.Windows.Forms.Padding(0);
            this.tbData_f_z.Name = "tbData_f_z";
            this.tbData_f_z.Size = new System.Drawing.Size(110, 23);
            this.tbData_f_z.TabIndex = 67;
            // 
            // tbData_f_o
            // 
            this.tbData_f_o.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbData_f_o.Font = new System.Drawing.Font("Arial", 10F);
            this.tbData_f_o.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbData_f_o.Location = new System.Drawing.Point(64, 195);
            this.tbData_f_o.Margin = new System.Windows.Forms.Padding(0);
            this.tbData_f_o.Name = "tbData_f_o";
            this.tbData_f_o.Size = new System.Drawing.Size(110, 23);
            this.tbData_f_o.TabIndex = 66;
            // 
            // mtbData_cd
            // 
            this.mtbData_cd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbData_cd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbData_cd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbData_cd.Location = new System.Drawing.Point(107, 424);
            this.mtbData_cd.Mask = "00/00/0000";
            this.mtbData_cd.Name = "mtbData_cd";
            this.mtbData_cd.Size = new System.Drawing.Size(63, 21);
            this.mtbData_cd.TabIndex = 65;
            // 
            // mtbData_zeh
            // 
            this.mtbData_zeh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbData_zeh.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbData_zeh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbData_zeh.Location = new System.Drawing.Point(107, 400);
            this.mtbData_zeh.Mask = "00/00/0000";
            this.mtbData_zeh.Name = "mtbData_zeh";
            this.mtbData_zeh.Size = new System.Drawing.Size(63, 21);
            this.mtbData_zeh.TabIndex = 64;
            // 
            // tbOtgrStat
            // 
            this.tbOtgrStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbOtgrStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbOtgrStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbOtgrStat.Location = new System.Drawing.Point(177, 424);
            this.tbOtgrStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbOtgrStat.Name = "tbOtgrStat";
            this.tbOtgrStat.Size = new System.Drawing.Size(26, 23);
            this.tbOtgrStat.TabIndex = 59;
            this.tbOtgrStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbIs_got
            // 
            this.tbIs_got.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbIs_got.Font = new System.Drawing.Font("Arial", 10F);
            this.tbIs_got.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbIs_got.Location = new System.Drawing.Point(177, 399);
            this.tbIs_got.Margin = new System.Windows.Forms.Padding(0);
            this.tbIs_got.Name = "tbIs_got";
            this.tbIs_got.Size = new System.Drawing.Size(26, 23);
            this.tbIs_got.TabIndex = 58;
            this.tbIs_got.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.label60.Font = new System.Drawing.Font("Arial", 9F);
            this.label60.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label60.Location = new System.Drawing.Point(6, 425);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(94, 30);
            this.label60.TabIndex = 55;
            this.label60.Text = "Дата отгрузки \r\nс производства";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Font = new System.Drawing.Font("Arial", 9F);
            this.label59.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label59.Location = new System.Drawing.Point(6, 400);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(65, 15);
            this.label59.TabIndex = 54;
            this.label59.Text = "Дата в цех";
            // 
            // tbUZSobrStat
            // 
            this.tbUZSobrStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbUZSobrStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbUZSobrStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbUZSobrStat.Location = new System.Drawing.Point(177, 337);
            this.tbUZSobrStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbUZSobrStat.Name = "tbUZSobrStat";
            this.tbUZSobrStat.Size = new System.Drawing.Size(26, 23);
            this.tbUZSobrStat.TabIndex = 52;
            this.tbUZSobrStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.label56.Font = new System.Drawing.Font("Arial", 9F);
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label56.Location = new System.Drawing.Point(16, 341);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(55, 15);
            this.label56.TabIndex = 49;
            this.label56.Text = "собрана";
            // 
            // tbUZSozdStat
            // 
            this.tbUZSozdStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbUZSozdStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbUZSozdStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbUZSozdStat.Location = new System.Drawing.Point(177, 313);
            this.tbUZSozdStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbUZSozdStat.Name = "tbUZSozdStat";
            this.tbUZSozdStat.Size = new System.Drawing.Size(26, 23);
            this.tbUZSozdStat.TabIndex = 48;
            this.tbUZSozdStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Font = new System.Drawing.Font("Arial", 9F);
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label57.Location = new System.Drawing.Point(16, 317);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(54, 15);
            this.label57.TabIndex = 47;
            this.label57.Text = "создана";
            // 
            // tbUpakZayav
            // 
            this.tbUpakZayav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbUpakZayav.Font = new System.Drawing.Font("Arial", 10F);
            this.tbUpakZayav.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbUpakZayav.Location = new System.Drawing.Point(158, 289);
            this.tbUpakZayav.Margin = new System.Windows.Forms.Padding(0);
            this.tbUpakZayav.Name = "tbUpakZayav";
            this.tbUpakZayav.Size = new System.Drawing.Size(45, 23);
            this.tbUpakZayav.TabIndex = 46;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label58.Location = new System.Drawing.Point(5, 290);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(142, 13);
            this.label58.TabIndex = 45;
            this.label58.Text = "Заявка на упаковку №";
            // 
            // btnZayavFurnPrint
            // 
            this.btnZayavFurnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnZayavFurnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZayavFurnPrint.Font = new System.Drawing.Font("Arial", 10F);
            this.btnZayavFurnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnZayavFurnPrint.Location = new System.Drawing.Point(6, 244);
            this.btnZayavFurnPrint.Name = "btnZayavFurnPrint";
            this.btnZayavFurnPrint.Size = new System.Drawing.Size(200, 24);
            this.btnZayavFurnPrint.TabIndex = 44;
            this.btnZayavFurnPrint.Text = "ПРОСМОТР / ПЕЧАТЬ";
            this.btnZayavFurnPrint.UseVisualStyleBackColor = true;
            this.btnZayavFurnPrint.Click += new System.EventHandler(this.btnZayavFurnPrint_Click);
            // 
            // tbFZSobrStat
            // 
            this.tbFZSobrStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbFZSobrStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbFZSobrStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbFZSobrStat.Location = new System.Drawing.Point(177, 219);
            this.tbFZSobrStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbFZSobrStat.Name = "tbFZSobrStat";
            this.tbFZSobrStat.Size = new System.Drawing.Size(26, 23);
            this.tbFZSobrStat.TabIndex = 43;
            this.tbFZSobrStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.BackColor = System.Drawing.Color.Transparent;
            this.label55.Font = new System.Drawing.Font("Arial", 9F);
            this.label55.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label55.Location = new System.Drawing.Point(16, 223);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(55, 15);
            this.label55.TabIndex = 40;
            this.label55.Text = "собрана";
            // 
            // tbFZSozdStat
            // 
            this.tbFZSozdStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbFZSozdStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbFZSozdStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbFZSozdStat.Location = new System.Drawing.Point(177, 195);
            this.tbFZSozdStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbFZSozdStat.Name = "tbFZSozdStat";
            this.tbFZSozdStat.Size = new System.Drawing.Size(26, 23);
            this.tbFZSozdStat.TabIndex = 39;
            this.tbFZSozdStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.BackColor = System.Drawing.Color.Transparent;
            this.label54.Font = new System.Drawing.Font("Arial", 9F);
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label54.Location = new System.Drawing.Point(16, 199);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(54, 15);
            this.label54.TabIndex = 38;
            this.label54.Text = "создана";
            // 
            // tbFurnZayav
            // 
            this.tbFurnZayav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbFurnZayav.Font = new System.Drawing.Font("Arial", 10F);
            this.tbFurnZayav.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbFurnZayav.Location = new System.Drawing.Point(158, 171);
            this.tbFurnZayav.Margin = new System.Windows.Forms.Padding(0);
            this.tbFurnZayav.Name = "tbFurnZayav";
            this.tbFurnZayav.Size = new System.Drawing.Size(45, 23);
            this.tbFurnZayav.TabIndex = 37;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label25.Location = new System.Drawing.Point(3, 175);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(150, 13);
            this.label25.TabIndex = 36;
            this.label25.Text = "Заявка на фурнитуру №";
            // 
            // btnFullKKPrint
            // 
            this.btnFullKKPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnFullKKPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullKKPrint.Font = new System.Drawing.Font("Arial", 10F);
            this.btnFullKKPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnFullKKPrint.Location = new System.Drawing.Point(6, 124);
            this.btnFullKKPrint.Name = "btnFullKKPrint";
            this.btnFullKKPrint.Size = new System.Drawing.Size(168, 42);
            this.btnFullKKPrint.TabIndex = 35;
            this.btnFullKKPrint.Text = "КК общая \r\n(просмотр/печать)";
            this.btnFullKKPrint.UseVisualStyleBackColor = true;
            this.btnFullKKPrint.Click += new System.EventHandler(this.btnFullKKPrint_Click);
            // 
            // tbUpakKKStat
            // 
            this.tbUpakKKStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbUpakKKStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbUpakKKStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbUpakKKStat.Location = new System.Drawing.Point(177, 94);
            this.tbUpakKKStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbUpakKKStat.Name = "tbUpakKKStat";
            this.tbUpakKKStat.Size = new System.Drawing.Size(26, 23);
            this.tbUpakKKStat.TabIndex = 34;
            this.tbUpakKKStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbFurnKKStat
            // 
            this.tbFurnKKStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbFurnKKStat.Font = new System.Drawing.Font("Arial", 10F);
            this.tbFurnKKStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbFurnKKStat.Location = new System.Drawing.Point(177, 54);
            this.tbFurnKKStat.Margin = new System.Windows.Forms.Padding(0);
            this.tbFurnKKStat.Name = "tbFurnKKStat";
            this.tbFurnKKStat.Size = new System.Drawing.Size(26, 23);
            this.tbFurnKKStat.TabIndex = 33;
            this.tbFurnKKStat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnUpakKKPrint
            // 
            this.btnUpakKKPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnUpakKKPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpakKKPrint.Font = new System.Drawing.Font("Arial", 10F);
            this.btnUpakKKPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnUpakKKPrint.Location = new System.Drawing.Point(6, 84);
            this.btnUpakKKPrint.Name = "btnUpakKKPrint";
            this.btnUpakKKPrint.Size = new System.Drawing.Size(168, 42);
            this.btnUpakKKPrint.TabIndex = 32;
            this.btnUpakKKPrint.Text = "КК на упаковку \r\n(просмотр/печать)";
            this.btnUpakKKPrint.UseVisualStyleBackColor = true;
            this.btnUpakKKPrint.Click += new System.EventHandler(this.btnUpakKKPrint_Click);
            // 
            // btnFurnKKPrint
            // 
            this.btnFurnKKPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnFurnKKPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFurnKKPrint.Font = new System.Drawing.Font("Arial", 10F);
            this.btnFurnKKPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnFurnKKPrint.Location = new System.Drawing.Point(6, 44);
            this.btnFurnKKPrint.Name = "btnFurnKKPrint";
            this.btnFurnKKPrint.Size = new System.Drawing.Size(168, 42);
            this.btnFurnKKPrint.TabIndex = 5;
            this.btnFurnKKPrint.Text = "КК на фурнитуру (просмотр/печать)";
            this.btnFurnKKPrint.UseVisualStyleBackColor = true;
            this.btnFurnKKPrint.Click += new System.EventHandler(this.btnFurnKKPrint_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(16, 2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(179, 38);
            this.label18.TabIndex = 4;
            this.label18.Text = "УСЛОВИЯ ДЛЯ\r\nСОЗДАНИЯ ЗАЯВОК:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
            this.xtraTabPage1.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.xtraTabPage1.Appearance.HeaderActive.Options.UseFont = true;
            this.xtraTabPage1.Appearance.HeaderDisabled.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xtraTabPage1.Appearance.HeaderDisabled.Options.UseFont = true;
            this.xtraTabPage1.Controls.Add(this.panelControl6);
            this.xtraTabPage1.Controls.Add(this.panelControl5);
            this.xtraTabPage1.Controls.Add(this.panelControl4);
            this.xtraTabPage1.Controls.Add(this.panelControl3);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1569, 575);
            this.xtraTabPage1.Text = "ИНФОРМАЦИЯ ПО РАСЧЕТУ";
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.button4);
            this.panelControl6.Controls.Add(this.button5);
            this.panelControl6.Controls.Add(this.button6);
            this.panelControl6.Controls.Add(this.label52);
            this.panelControl6.Location = new System.Drawing.Point(4, 517);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(1558, 55);
            this.panelControl6.TabIndex = 31;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Arial", 10F);
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.button4.Location = new System.Drawing.Point(499, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(205, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Сопроводительные реестры";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Arial", 10F);
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.button5.Location = new System.Drawing.Point(223, 25);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(205, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Задание упак.";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Arial", 10F);
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.button6.Location = new System.Drawing.Point(7, 25);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(171, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "Задание общ.";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.BackColor = System.Drawing.Color.Transparent;
            this.label52.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label52.Location = new System.Drawing.Point(3, 2);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(123, 19);
            this.label52.TabIndex = 4;
            this.label52.Text = "ДОКУМЕНТЫ:";
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.cbRzuStirFact);
            this.panelControl5.Controls.Add(this.cbPszStirPlan);
            this.panelControl5.Controls.Add(this.cbRzuVishFact);
            this.panelControl5.Controls.Add(this.cbPszVishPlan);
            this.panelControl5.Controls.Add(this.cbRzuPrintFact);
            this.panelControl5.Controls.Add(this.cbPszPrintPlan);
            this.panelControl5.Controls.Add(this.mtbRzuDataPrCd);
            this.panelControl5.Controls.Add(this.mtbRzuDataVCd);
            this.panelControl5.Controls.Add(this.mtbRzuDataPrKm);
            this.panelControl5.Controls.Add(this.mtbRzuVidStir);
            this.panelControl5.Controls.Add(this.mtbRzuDataVChi);
            this.panelControl5.Controls.Add(this.mtbRzuDataPrPe);
            this.panelControl5.Controls.Add(this.mtbRzuDataStCd);
            this.panelControl5.Controls.Add(this.mtbRzuDataVR);
            this.panelControl5.Controls.Add(this.mtbRzuDataPrR);
            this.panelControl5.Controls.Add(this.mtbRzuDataStR);
            this.panelControl5.Controls.Add(this.mtbRzuDataVP);
            this.panelControl5.Controls.Add(this.mtbRzuDataPrP);
            this.panelControl5.Controls.Add(this.mtbRzuDataStP);
            this.panelControl5.Controls.Add(this.mtbRzuDataRasv);
            this.panelControl5.Controls.Add(this.mtbRzuDataRasp);
            this.panelControl5.Controls.Add(this.label44);
            this.panelControl5.Controls.Add(this.label49);
            this.panelControl5.Controls.Add(this.label50);
            this.panelControl5.Controls.Add(this.label51);
            this.panelControl5.Controls.Add(this.label43);
            this.panelControl5.Controls.Add(this.label45);
            this.panelControl5.Controls.Add(this.label46);
            this.panelControl5.Controls.Add(this.label47);
            this.panelControl5.Controls.Add(this.label48);
            this.panelControl5.Controls.Add(this.label42);
            this.panelControl5.Controls.Add(this.label41);
            this.panelControl5.Controls.Add(this.label40);
            this.panelControl5.Controls.Add(this.label39);
            this.panelControl5.Controls.Add(this.label38);
            this.panelControl5.Controls.Add(this.label37);
            this.panelControl5.Controls.Add(this.label35);
            this.panelControl5.Controls.Add(this.label36);
            this.panelControl5.Controls.Add(this.label32);
            this.panelControl5.Controls.Add(this.gridControl2);
            this.panelControl5.Controls.Add(this.label31);
            this.panelControl5.Location = new System.Drawing.Point(4, 316);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(1558, 195);
            this.panelControl5.TabIndex = 30;
            // 
            // cbRzuStirFact
            // 
            this.cbRzuStirFact.AutoSize = true;
            this.cbRzuStirFact.Font = new System.Drawing.Font("Arial", 10F);
            this.cbRzuStirFact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbRzuStirFact.Location = new System.Drawing.Point(170, 142);
            this.cbRzuStirFact.Name = "cbRzuStirFact";
            this.cbRzuStirFact.Size = new System.Drawing.Size(58, 20);
            this.cbRzuStirFact.TabIndex = 72;
            this.cbRzuStirFact.Text = "Факт";
            this.cbRzuStirFact.UseVisualStyleBackColor = true;
            // 
            // cbPszStirPlan
            // 
            this.cbPszStirPlan.AutoSize = true;
            this.cbPszStirPlan.Font = new System.Drawing.Font("Arial", 10F);
            this.cbPszStirPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbPszStirPlan.Location = new System.Drawing.Point(106, 142);
            this.cbPszStirPlan.Name = "cbPszStirPlan";
            this.cbPszStirPlan.Size = new System.Drawing.Size(59, 20);
            this.cbPszStirPlan.TabIndex = 71;
            this.cbPszStirPlan.Text = "План";
            this.cbPszStirPlan.UseVisualStyleBackColor = true;
            // 
            // cbRzuVishFact
            // 
            this.cbRzuVishFact.AutoSize = true;
            this.cbRzuVishFact.Font = new System.Drawing.Font("Arial", 10F);
            this.cbRzuVishFact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbRzuVishFact.Location = new System.Drawing.Point(174, 88);
            this.cbRzuVishFact.Name = "cbRzuVishFact";
            this.cbRzuVishFact.Size = new System.Drawing.Size(58, 20);
            this.cbRzuVishFact.TabIndex = 70;
            this.cbRzuVishFact.Text = "Факт";
            this.cbRzuVishFact.UseVisualStyleBackColor = true;
            // 
            // cbPszVishPlan
            // 
            this.cbPszVishPlan.AutoSize = true;
            this.cbPszVishPlan.Font = new System.Drawing.Font("Arial", 10F);
            this.cbPszVishPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbPszVishPlan.Location = new System.Drawing.Point(110, 88);
            this.cbPszVishPlan.Name = "cbPszVishPlan";
            this.cbPszVishPlan.Size = new System.Drawing.Size(59, 20);
            this.cbPszVishPlan.TabIndex = 69;
            this.cbPszVishPlan.Text = "План";
            this.cbPszVishPlan.UseVisualStyleBackColor = true;
            // 
            // cbRzuPrintFact
            // 
            this.cbRzuPrintFact.AutoSize = true;
            this.cbRzuPrintFact.Font = new System.Drawing.Font("Arial", 10F);
            this.cbRzuPrintFact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbRzuPrintFact.Location = new System.Drawing.Point(155, 30);
            this.cbRzuPrintFact.Name = "cbRzuPrintFact";
            this.cbRzuPrintFact.Size = new System.Drawing.Size(58, 20);
            this.cbRzuPrintFact.TabIndex = 68;
            this.cbRzuPrintFact.Text = "Факт";
            this.cbRzuPrintFact.UseVisualStyleBackColor = true;
            // 
            // cbPszPrintPlan
            // 
            this.cbPszPrintPlan.AutoSize = true;
            this.cbPszPrintPlan.Font = new System.Drawing.Font("Arial", 10F);
            this.cbPszPrintPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.cbPszPrintPlan.Location = new System.Drawing.Point(91, 30);
            this.cbPszPrintPlan.Name = "cbPszPrintPlan";
            this.cbPszPrintPlan.Size = new System.Drawing.Size(59, 20);
            this.cbPszPrintPlan.TabIndex = 67;
            this.cbPszPrintPlan.Text = "План";
            this.cbPszPrintPlan.UseVisualStyleBackColor = true;
            // 
            // mtbRzuDataPrCd
            // 
            this.mtbRzuDataPrCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataPrCd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrCd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrCd.Location = new System.Drawing.Point(898, 55);
            this.mtbRzuDataPrCd.Mask = "00/00/0000";
            this.mtbRzuDataPrCd.Name = "mtbRzuDataPrCd";
            this.mtbRzuDataPrCd.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrCd.TabIndex = 66;
            // 
            // mtbRzuDataVCd
            // 
            this.mtbRzuDataVCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataVCd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataVCd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataVCd.Location = new System.Drawing.Point(765, 109);
            this.mtbRzuDataVCd.Mask = "00/00/0000";
            this.mtbRzuDataVCd.Name = "mtbRzuDataVCd";
            this.mtbRzuDataVCd.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataVCd.TabIndex = 65;
            // 
            // mtbRzuDataPrKm
            // 
            this.mtbRzuDataPrKm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataPrKm.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrKm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrKm.Location = new System.Drawing.Point(765, 55);
            this.mtbRzuDataPrKm.Mask = "00/00/0000";
            this.mtbRzuDataPrKm.Name = "mtbRzuDataPrKm";
            this.mtbRzuDataPrKm.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrKm.TabIndex = 64;
            // 
            // mtbRzuVidStir
            // 
            this.mtbRzuVidStir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuVidStir.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuVidStir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuVidStir.Location = new System.Drawing.Point(626, 163);
            this.mtbRzuVidStir.Mask = "00/00/0000";
            this.mtbRzuVidStir.Name = "mtbRzuVidStir";
            this.mtbRzuVidStir.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuVidStir.TabIndex = 63;
            // 
            // mtbRzuDataVChi
            // 
            this.mtbRzuDataVChi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataVChi.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataVChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataVChi.Location = new System.Drawing.Point(626, 109);
            this.mtbRzuDataVChi.Mask = "00/00/0000";
            this.mtbRzuDataVChi.Name = "mtbRzuDataVChi";
            this.mtbRzuDataVChi.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataVChi.TabIndex = 62;
            // 
            // mtbRzuDataPrPe
            // 
            this.mtbRzuDataPrPe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataPrPe.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrPe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrPe.Location = new System.Drawing.Point(626, 55);
            this.mtbRzuDataPrPe.Mask = "00/00/0000";
            this.mtbRzuDataPrPe.Name = "mtbRzuDataPrPe";
            this.mtbRzuDataPrPe.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrPe.TabIndex = 61;
            // 
            // mtbRzuDataStCd
            // 
            this.mtbRzuDataStCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataStCd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataStCd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataStCd.Location = new System.Drawing.Point(442, 163);
            this.mtbRzuDataStCd.Mask = "00/00/0000";
            this.mtbRzuDataStCd.Name = "mtbRzuDataStCd";
            this.mtbRzuDataStCd.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataStCd.TabIndex = 60;
            // 
            // mtbRzuDataVR
            // 
            this.mtbRzuDataVR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataVR.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataVR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataVR.Location = new System.Drawing.Point(442, 109);
            this.mtbRzuDataVR.Mask = "00/00/0000";
            this.mtbRzuDataVR.Name = "mtbRzuDataVR";
            this.mtbRzuDataVR.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataVR.TabIndex = 59;
            // 
            // mtbRzuDataPrR
            // 
            this.mtbRzuDataPrR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataPrR.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrR.Location = new System.Drawing.Point(442, 55);
            this.mtbRzuDataPrR.Mask = "00/00/0000";
            this.mtbRzuDataPrR.Name = "mtbRzuDataPrR";
            this.mtbRzuDataPrR.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrR.TabIndex = 58;
            // 
            // mtbRzuDataStR
            // 
            this.mtbRzuDataStR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataStR.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataStR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataStR.Location = new System.Drawing.Point(265, 163);
            this.mtbRzuDataStR.Mask = "00/00/0000";
            this.mtbRzuDataStR.Name = "mtbRzuDataStR";
            this.mtbRzuDataStR.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataStR.TabIndex = 57;
            // 
            // mtbRzuDataVP
            // 
            this.mtbRzuDataVP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataVP.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataVP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataVP.Location = new System.Drawing.Point(265, 109);
            this.mtbRzuDataVP.Mask = "00/00/0000";
            this.mtbRzuDataVP.Name = "mtbRzuDataVP";
            this.mtbRzuDataVP.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataVP.TabIndex = 56;
            // 
            // mtbRzuDataPrP
            // 
            this.mtbRzuDataPrP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataPrP.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataPrP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataPrP.Location = new System.Drawing.Point(265, 55);
            this.mtbRzuDataPrP.Mask = "00/00/0000";
            this.mtbRzuDataPrP.Name = "mtbRzuDataPrP";
            this.mtbRzuDataPrP.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataPrP.TabIndex = 55;
            // 
            // mtbRzuDataStP
            // 
            this.mtbRzuDataStP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataStP.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataStP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataStP.Location = new System.Drawing.Point(91, 163);
            this.mtbRzuDataStP.Mask = "00/00/0000";
            this.mtbRzuDataStP.Name = "mtbRzuDataStP";
            this.mtbRzuDataStP.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataStP.TabIndex = 54;
            // 
            // mtbRzuDataRasv
            // 
            this.mtbRzuDataRasv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataRasv.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataRasv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataRasv.Location = new System.Drawing.Point(91, 109);
            this.mtbRzuDataRasv.Mask = "00/00/0000";
            this.mtbRzuDataRasv.Name = "mtbRzuDataRasv";
            this.mtbRzuDataRasv.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataRasv.TabIndex = 53;
            // 
            // mtbRzuDataRasp
            // 
            this.mtbRzuDataRasp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataRasp.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataRasp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataRasp.Location = new System.Drawing.Point(91, 55);
            this.mtbRzuDataRasp.Mask = "00/00/0000";
            this.mtbRzuDataRasp.Name = "mtbRzuDataRasp";
            this.mtbRzuDataRasp.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataRasp.TabIndex = 38;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Font = new System.Drawing.Font("Arial", 9F);
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label44.Location = new System.Drawing.Point(538, 165);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(70, 15);
            this.label44.TabIndex = 52;
            this.label44.Text = "Вид стирки";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Font = new System.Drawing.Font("Arial", 9F);
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label49.Location = new System.Drawing.Point(359, 165);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(70, 15);
            this.label49.TabIndex = 50;
            this.label49.Text = "Дата сдачи";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.BackColor = System.Drawing.Color.Transparent;
            this.label50.Font = new System.Drawing.Font("Arial", 9F);
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label50.Location = new System.Drawing.Point(185, 165);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(75, 15);
            this.label50.TabIndex = 48;
            this.label50.Text = "Дата стирки";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.label51.Font = new System.Drawing.Font("Arial", 9F);
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label51.Location = new System.Drawing.Point(11, 158);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(84, 30);
            this.label51.TabIndex = 46;
            this.label51.Text = "Дата принято\r\nна стирку";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Font = new System.Drawing.Font("Arial", 9F);
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label43.Location = new System.Drawing.Point(722, 104);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(40, 30);
            this.label43.TabIndex = 42;
            this.label43.Text = "Дата\r\nсдачи";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.label45.Font = new System.Drawing.Font("Arial", 9F);
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label45.Location = new System.Drawing.Point(538, 111);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(89, 15);
            this.label45.TabIndex = 38;
            this.label45.Text = "Дата на чистку";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.Font = new System.Drawing.Font("Arial", 9F);
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label46.Location = new System.Drawing.Point(359, 104);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(84, 30);
            this.label46.TabIndex = 36;
            this.label46.Text = "Дата в работу\r\nвышивка";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.label47.Font = new System.Drawing.Font("Arial", 9F);
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label47.Location = new System.Drawing.Point(185, 104);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(84, 30);
            this.label47.TabIndex = 34;
            this.label47.Text = "Дата принято\r\nна вышивку";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.label48.Font = new System.Drawing.Font("Arial", 9F);
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label48.Location = new System.Drawing.Point(11, 104);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(72, 30);
            this.label48.TabIndex = 32;
            this.label48.Text = "Дата\r\nна вышивку";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Font = new System.Drawing.Font("Arial", 9F);
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label42.Location = new System.Drawing.Point(859, 50);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(40, 30);
            this.label42.TabIndex = 30;
            this.label42.Text = "Дата\r\nсдачи";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("Arial", 9F);
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label41.Location = new System.Drawing.Point(722, 50);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(46, 30);
            this.label41.TabIndex = 28;
            this.label41.Text = "Дата\r\nкомпл.";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Arial", 9F);
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label40.Location = new System.Drawing.Point(538, 50);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(85, 30);
            this.label40.TabIndex = 26;
            this.label40.Text = "Дата на печку\r\nпринт";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Arial", 9F);
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label39.Location = new System.Drawing.Point(359, 50);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(84, 30);
            this.label39.TabIndex = 24;
            this.label39.Text = "Дата в работу\r\nпринт";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.Font = new System.Drawing.Font("Arial", 9F);
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label38.Location = new System.Drawing.Point(185, 50);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(84, 30);
            this.label38.TabIndex = 22;
            this.label38.Text = "Дата принято\r\nна принт";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("Arial", 9F);
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label37.Location = new System.Drawing.Point(11, 50);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(57, 30);
            this.label37.TabIndex = 20;
            this.label37.Text = "Дата\r\nна принт";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label35.Location = new System.Drawing.Point(3, 138);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(77, 19);
            this.label35.TabIndex = 11;
            this.label35.Text = "СТИРКА";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label36.Location = new System.Drawing.Point(3, 84);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(98, 19);
            this.label36.TabIndex = 10;
            this.label36.Text = "ВЫШИВКА";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label32.Location = new System.Drawing.Point(3, 30);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(67, 19);
            this.label32.TabIndex = 9;
            this.label32.Text = "ПРИНТ";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label31.Location = new System.Drawing.Point(3, 2);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(263, 19);
            this.label31.TabIndex = 4;
            this.label31.Text = "ОТДЕЛКА / ДОП. ОБРАБОТКА:";
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.gcNaklList);
            this.panelControl4.Controls.Add(this.gcPartNaklList);
            this.panelControl4.Controls.Add(this.button1);
            this.panelControl4.Controls.Add(this.btnPrintNaklXtraReport);
            this.panelControl4.Controls.Add(this.btnNaklAbsent);
            this.panelControl4.Controls.Add(this.btnNaklPart);
            this.panelControl4.Controls.Add(this.btnNaklPrint);
            this.panelControl4.Controls.Add(this.label24);
            this.panelControl4.Location = new System.Drawing.Point(4, 90);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1558, 220);
            this.panelControl4.TabIndex = 28;
            // 
            // gcPartNaklList
            // 
            this.gcPartNaklList.DataSource = this.bsPartNaklList;
            this.gcPartNaklList.Location = new System.Drawing.Point(442, 2);
            this.gcPartNaklList.MainView = this.gridView3;
            this.gcPartNaklList.Name = "gcPartNaklList";
            this.gcPartNaklList.Size = new System.Drawing.Size(1160, 77);
            this.gcPartNaklList.TabIndex = 11;
            this.gcPartNaklList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            this.gcPartNaklList.Visible = false;
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn23,
            this.gridColumn25,
            this.gridColumn27,
            this.gridColumn28,
            this.gridColumn34,
            this.gridColumn35,
            this.gridColumn24,
            this.gridColumn26,
            this.gridColumn36,
            this.gridColumn37,
            this.gridColumn38,
            this.gridColumn39,
            this.gridColumn22,
            this.gridColumn42,
            this.gridColumn43});
            this.gridView3.GridControl = this.gcPartNaklList;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "iz_ob_prch";
            this.gridColumn18.FieldName = "iz_ob_prch";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn18.OptionsFilter.AllowFilter = false;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 11;
            this.gridColumn18.Width = 45;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "razm";
            this.gridColumn19.FieldName = "razm";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn19.OptionsFilter.AllowFilter = false;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 10;
            this.gridColumn19.Width = 132;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "mod";
            this.gridColumn20.FieldName = "mod";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn20.OptionsFilter.AllowFilter = false;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 9;
            this.gridColumn20.Width = 109;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "kol_new";
            this.gridColumn21.FieldName = "kol_new";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn21.OptionsFilter.AllowFilter = false;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 8;
            this.gridColumn21.Width = 63;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "kol_c";
            this.gridColumn23.FieldName = "kol_c";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn23.OptionsFilter.AllowFilter = false;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 7;
            this.gridColumn23.Width = 53;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "kol_b";
            this.gridColumn25.FieldName = "kol_b";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn25.OptionsFilter.AllowFilter = false;
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 6;
            this.gridColumn25.Width = 38;
            // 
            // gridColumn27
            // 
            this.gridColumn27.Caption = "iz_c";
            this.gridColumn27.FieldName = "iz_c";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn27.OptionsFilter.AllowFilter = false;
            this.gridColumn27.Visible = true;
            this.gridColumn27.VisibleIndex = 5;
            this.gridColumn27.Width = 58;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "skl_otgr_s";
            this.gridColumn28.FieldName = "skl_otgr_c";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn28.OptionsFilter.AllowFilter = false;
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 3;
            this.gridColumn28.Width = 35;
            // 
            // gridColumn34
            // 
            this.gridColumn34.Caption = "skl_otgr_b";
            this.gridColumn34.FieldName = "skl_otgr_b";
            this.gridColumn34.Name = "gridColumn34";
            this.gridColumn34.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn34.OptionsFilter.AllowFilter = false;
            this.gridColumn34.Visible = true;
            this.gridColumn34.VisibleIndex = 2;
            this.gridColumn34.Width = 40;
            // 
            // gridColumn35
            // 
            this.gridColumn35.Caption = "data_izm";
            this.gridColumn35.FieldName = "data_izm";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn35.OptionsFilter.AllowFilter = false;
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 1;
            this.gridColumn35.Width = 51;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "status";
            this.gridColumn24.FieldName = "status";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn24.OptionsFilter.AllowFilter = false;
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 12;
            this.gridColumn24.Width = 45;
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "skl_id_1c_b";
            this.gridColumn26.FieldName = "skl_id_1c_b";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn26.OptionsFilter.AllowFilter = false;
            this.gridColumn26.Visible = true;
            this.gridColumn26.VisibleIndex = 15;
            this.gridColumn26.Width = 52;
            // 
            // gridColumn36
            // 
            this.gridColumn36.Caption = "komp_del";
            this.gridColumn36.FieldName = "komp_del";
            this.gridColumn36.Name = "gridColumn36";
            this.gridColumn36.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn36.OptionsFilter.AllowFilter = false;
            this.gridColumn36.Visible = true;
            this.gridColumn36.VisibleIndex = 14;
            this.gridColumn36.Width = 98;
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "komp_name";
            this.gridColumn37.FieldName = "komp_name";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn37.OptionsFilter.AllowFilter = false;
            this.gridColumn37.Visible = true;
            this.gridColumn37.VisibleIndex = 13;
            this.gridColumn37.Width = 101;
            // 
            // gridColumn38
            // 
            this.gridColumn38.Caption = "iz_b";
            this.gridColumn38.FieldName = "iz_b";
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn38.OptionsFilter.AllowFilter = false;
            this.gridColumn38.Visible = true;
            this.gridColumn38.VisibleIndex = 4;
            this.gridColumn38.Width = 49;
            // 
            // gridColumn39
            // 
            this.gridColumn39.Caption = "ID";
            this.gridColumn39.FieldName = "id";
            this.gridColumn39.Name = "gridColumn39";
            this.gridColumn39.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn39.OptionsFilter.AllowFilter = false;
            this.gridColumn39.Visible = true;
            this.gridColumn39.VisibleIndex = 0;
            this.gridColumn39.Width = 49;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "n_pach";
            this.gridColumn22.FieldName = "n_pach";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn22.OptionsFilter.AllowFilter = false;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 18;
            this.gridColumn22.Width = 128;
            // 
            // gridColumn42
            // 
            this.gridColumn42.Caption = "prich_sokr";
            this.gridColumn42.FieldName = "prich_sokr";
            this.gridColumn42.Name = "gridColumn42";
            this.gridColumn42.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn42.OptionsFilter.AllowFilter = false;
            this.gridColumn42.Visible = true;
            this.gridColumn42.VisibleIndex = 17;
            this.gridColumn42.Width = 73;
            // 
            // gridColumn43
            // 
            this.gridColumn43.Caption = "skl_id_1c_c";
            this.gridColumn43.FieldName = "skl_id_1c_c";
            this.gridColumn43.Name = "gridColumn43";
            this.gridColumn43.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn43.OptionsFilter.AllowFilter = false;
            this.gridColumn43.Visible = true;
            this.gridColumn43.VisibleIndex = 16;
            this.gridColumn43.Width = 51;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 10F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.button1.Location = new System.Drawing.Point(653, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "testFioReport";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnPrintNaklXtraReport
            // 
            this.btnPrintNaklXtraReport.Location = new System.Drawing.Point(497, 193);
            this.btnPrintNaklXtraReport.Name = "btnPrintNaklXtraReport";
            this.btnPrintNaklXtraReport.Size = new System.Drawing.Size(75, 23);
            this.btnPrintNaklXtraReport.TabIndex = 9;
            this.btnPrintNaklXtraReport.Text = "button1";
            this.btnPrintNaklXtraReport.UseVisualStyleBackColor = true;
            this.btnPrintNaklXtraReport.Visible = false;
            this.btnPrintNaklXtraReport.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNaklAbsent
            // 
            this.btnNaklAbsent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnNaklAbsent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaklAbsent.Font = new System.Drawing.Font("Arial", 10F);
            this.btnNaklAbsent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnNaklAbsent.Location = new System.Drawing.Point(1141, 193);
            this.btnNaklAbsent.Name = "btnNaklAbsent";
            this.btnNaklAbsent.Size = new System.Drawing.Size(205, 23);
            this.btnNaklAbsent.TabIndex = 8;
            this.btnNaklAbsent.Text = "Накладная не создана. Причина";
            this.btnNaklAbsent.UseVisualStyleBackColor = true;
            this.btnNaklAbsent.Visible = false;
            // 
            // btnNaklPart
            // 
            this.btnNaklPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnNaklPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaklPart.Font = new System.Drawing.Font("Arial", 10F);
            this.btnNaklPart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnNaklPart.Location = new System.Drawing.Point(221, 193);
            this.btnNaklPart.Name = "btnNaklPart";
            this.btnNaklPart.Size = new System.Drawing.Size(268, 23);
            this.btnNaklPart.TabIndex = 7;
            this.btnNaklPart.Text = "Показать информацию по делению накладной";
            this.btnNaklPart.UseVisualStyleBackColor = true;
            this.btnNaklPart.Click += new System.EventHandler(this.btnNaklPart_Click);
            // 
            // btnNaklPrint
            // 
            this.btnNaklPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnNaklPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaklPrint.Font = new System.Drawing.Font("Arial", 10F);
            this.btnNaklPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.btnNaklPrint.Location = new System.Drawing.Point(5, 193);
            this.btnNaklPrint.Name = "btnNaklPrint";
            this.btnNaklPrint.Size = new System.Drawing.Size(171, 23);
            this.btnNaklPrint.TabIndex = 6;
            this.btnNaklPrint.Text = "Просмотр/Печать накладной";
            this.btnNaklPrint.UseVisualStyleBackColor = true;
            this.btnNaklPrint.Click += new System.EventHandler(this.btnNaklPrint_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label24.Location = new System.Drawing.Point(1, 2);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(123, 19);
            this.label24.TabIndex = 4;
            this.label24.Text = "НАКЛАДНЫЕ:";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.tbPszRpcNom);
            this.panelControl3.Controls.Add(this.mtbRzuDataR);
            this.panelControl3.Controls.Add(this.label64);
            this.panelControl3.Controls.Add(this.mtbRzuData1С);
            this.panelControl3.Controls.Add(this.label63);
            this.panelControl3.Controls.Add(this.mtbRzuDataCd);
            this.panelControl3.Controls.Add(this.mtbRzuDataUp);
            this.panelControl3.Controls.Add(this.mtbRzuDataRab);
            this.panelControl3.Controls.Add(this.mtbRzuDataZeh);
            this.panelControl3.Controls.Add(this.mtbRzuDataCdUt);
            this.panelControl3.Controls.Add(this.mtbPsaDataCdPlan);
            this.panelControl3.Controls.Add(this.mtbPsaDataZap);
            this.panelControl3.Controls.Add(this.label14);
            this.panelControl3.Controls.Add(this.label33);
            this.panelControl3.Controls.Add(this.label34);
            this.panelControl3.Controls.Add(this.label26);
            this.panelControl3.Controls.Add(this.label27);
            this.panelControl3.Controls.Add(this.label28);
            this.panelControl3.Controls.Add(this.label29);
            this.panelControl3.Controls.Add(this.label30);
            this.panelControl3.Location = new System.Drawing.Point(4, 9);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1558, 77);
            this.panelControl3.TabIndex = 27;
            // 
            // tbPszRpcNom
            // 
            this.tbPszRpcNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.tbPszRpcNom.Font = new System.Drawing.Font("Arial", 10F);
            this.tbPszRpcNom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.tbPszRpcNom.Location = new System.Drawing.Point(662, 38);
            this.tbPszRpcNom.Margin = new System.Windows.Forms.Padding(0);
            this.tbPszRpcNom.Name = "tbPszRpcNom";
            this.tbPszRpcNom.Size = new System.Drawing.Size(42, 23);
            this.tbPszRpcNom.TabIndex = 34;
            // 
            // mtbRzuDataR
            // 
            this.mtbRzuDataR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataR.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataR.Location = new System.Drawing.Point(581, 37);
            this.mtbRzuDataR.Mask = "00/00/0000";
            this.mtbRzuDataR.Name = "mtbRzuDataR";
            this.mtbRzuDataR.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataR.TabIndex = 41;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.Font = new System.Drawing.Font("Arial", 9F);
            this.label64.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label64.Location = new System.Drawing.Point(518, 32);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(54, 30);
            this.label64.TabIndex = 40;
            this.label64.Text = "Дата\r\nраскроя";
            // 
            // mtbRzuData1С
            // 
            this.mtbRzuData1С.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuData1С.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuData1С.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuData1С.Location = new System.Drawing.Point(1444, 38);
            this.mtbRzuData1С.Mask = "00/00/0000";
            this.mtbRzuData1С.Name = "mtbRzuData1С";
            this.mtbRzuData1С.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuData1С.TabIndex = 39;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.BackColor = System.Drawing.Color.Transparent;
            this.label63.Font = new System.Drawing.Font("Arial", 9F);
            this.label63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label63.Location = new System.Drawing.Point(1380, 32);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(58, 30);
            this.label63.TabIndex = 38;
            this.label63.Text = "Дата\r\n1к.т. в 1С";
            // 
            // mtbRzuDataCd
            // 
            this.mtbRzuDataCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataCd.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataCd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataCd.Location = new System.Drawing.Point(1283, 37);
            this.mtbRzuDataCd.Mask = "00/00/0000";
            this.mtbRzuDataCd.Name = "mtbRzuDataCd";
            this.mtbRzuDataCd.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataCd.TabIndex = 37;
            // 
            // mtbRzuDataUp
            // 
            this.mtbRzuDataUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataUp.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataUp.Location = new System.Drawing.Point(1111, 37);
            this.mtbRzuDataUp.Mask = "00/00/0000";
            this.mtbRzuDataUp.Name = "mtbRzuDataUp";
            this.mtbRzuDataUp.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataUp.TabIndex = 36;
            // 
            // mtbRzuDataRab
            // 
            this.mtbRzuDataRab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataRab.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataRab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataRab.Location = new System.Drawing.Point(938, 37);
            this.mtbRzuDataRab.Mask = "00/00/0000";
            this.mtbRzuDataRab.Name = "mtbRzuDataRab";
            this.mtbRzuDataRab.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataRab.TabIndex = 35;
            // 
            // mtbRzuDataZeh
            // 
            this.mtbRzuDataZeh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataZeh.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataZeh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataZeh.Location = new System.Drawing.Point(780, 37);
            this.mtbRzuDataZeh.Mask = "00/00/0000";
            this.mtbRzuDataZeh.Name = "mtbRzuDataZeh";
            this.mtbRzuDataZeh.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataZeh.TabIndex = 34;
            // 
            // mtbRzuDataCdUt
            // 
            this.mtbRzuDataCdUt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbRzuDataCdUt.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbRzuDataCdUt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbRzuDataCdUt.Location = new System.Drawing.Point(422, 37);
            this.mtbRzuDataCdUt.Mask = "00/00/0000";
            this.mtbRzuDataCdUt.Name = "mtbRzuDataCdUt";
            this.mtbRzuDataCdUt.Size = new System.Drawing.Size(66, 21);
            this.mtbRzuDataCdUt.TabIndex = 33;
            // 
            // mtbPsaDataCdPlan
            // 
            this.mtbPsaDataCdPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbPsaDataCdPlan.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbPsaDataCdPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbPsaDataCdPlan.Location = new System.Drawing.Point(222, 37);
            this.mtbPsaDataCdPlan.Mask = "00/00/0000";
            this.mtbPsaDataCdPlan.Name = "mtbPsaDataCdPlan";
            this.mtbPsaDataCdPlan.Size = new System.Drawing.Size(66, 21);
            this.mtbPsaDataCdPlan.TabIndex = 32;
            // 
            // mtbPsaDataZap
            // 
            this.mtbPsaDataZap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.mtbPsaDataZap.Font = new System.Drawing.Font("Arial", 9F);
            this.mtbPsaDataZap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(61)))), ((int)(((byte)(139)))));
            this.mtbPsaDataZap.Location = new System.Drawing.Point(61, 37);
            this.mtbPsaDataZap.Mask = "00/00/0000";
            this.mtbPsaDataZap.Name = "mtbPsaDataZap";
            this.mtbPsaDataZap.Size = new System.Drawing.Size(66, 21);
            this.mtbPsaDataZap.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Arial", 9F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(1204, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 30);
            this.label14.TabIndex = 30;
            this.label14.Text = "Дата СДАНО\r\n(осн. накл.)";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Font = new System.Drawing.Font("Arial", 9F);
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label33.Location = new System.Drawing.Point(1035, 32);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(73, 30);
            this.label33.TabIndex = 28;
            this.label33.Text = "Дата\r\nна упаковку";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Arial", 9F);
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label34.Location = new System.Drawing.Point(315, 32);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(111, 30);
            this.label34.TabIndex = 26;
            this.label34.Text = "План. дата \r\nсдачи Уточненная";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Arial", 9F);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label26.Location = new System.Drawing.Point(880, 32);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(54, 30);
            this.label26.TabIndex = 16;
            this.label26.Text = "Дата\r\nв работу";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Arial", 9F);
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label27.Location = new System.Drawing.Point(740, 32);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(35, 30);
            this.label27.TabIndex = 14;
            this.label27.Text = "Дата\r\nв цех";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Arial", 9F);
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label28.Location = new System.Drawing.Point(152, 32);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(69, 30);
            this.label28.TabIndex = 12;
            this.label28.Text = "План. дата\r\nсдачи";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Arial", 9F);
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label29.Location = new System.Drawing.Point(8, 32);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(51, 30);
            this.label29.TabIndex = 10;
            this.label29.Text = "Дата\r\nзапуска";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label30.Location = new System.Drawing.Point(7, 2);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(199, 19);
            this.label30.TabIndex = 3;
            this.label30.Text = "КОНТРОЛЬНЫЕ ДАТЫ:";
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
            this.xtraTabControl1.Location = new System.Drawing.Point(3, 141);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1571, 603);
            this.xtraTabControl1.TabIndex = 3;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4});
            this.xtraTabControl1.Selecting += new DevExpress.XtraTab.TabPageCancelEventHandler(this.xtraTabControl1_Selecting);
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
            // CardByNom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 750);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "CardByNom";
            this.Text = "Карточка расчета";
            this.Load += new System.EventHandler(this.CardByNom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsNaklList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEskiz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOtdelkaList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRasInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPartNaklList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProizvCombIzdSP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProizvCombIzdVZP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsIsChip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFurnZayavInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).EndInit();
            this.gridSplitContainer1.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProizvCombIzdSP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNaklList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            this.panelControl7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPartNaklList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.BindingSource bsNaklList;
        private System.Windows.Forms.BindingSource bsRasInfo;
        private System.Windows.Forms.BindingSource bsOtdelkaList;
        private System.Windows.Forms.PictureBox pbEskiz;
        private System.Windows.Forms.BindingSource bsPartNaklList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private System.Windows.Forms.BindingSource bsIsChip;
        private System.Windows.Forms.CheckBox cbIsChip;
        private System.Windows.Forms.BindingSource bsFurnZayavInfo;
        private System.Windows.Forms.BindingSource bsProizvCombIzdVZP;
        private System.Windows.Forms.BindingSource bsProizvCombIzdSP;
        private DevExpress.XtraGrid.GridSplitContainer gridSplitContainer1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraGrid.GridControl gcProizvCombIzdSP;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn47;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn52;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn55;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn56;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn58;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn74;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn75;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn76;
        private DevExpress.XtraGrid.GridControl gridControl4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn59;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn60;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn61;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn62;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn63;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn64;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn65;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn66;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn67;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn68;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn69;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn70;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn71;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn72;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn73;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraGrid.GridControl gcNaklList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private FurnitZayavView furnitZayavViewFurnit;
        private FurnitZayavView furnitZayavViewUpak;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.GridControl gcPartNaklList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn42;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn43;
        private System.Windows.Forms.Button btnPrintNaklXtraReport;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn77;
        private DevExpress.XtraEditors.SimpleButton sbProizvCombIzdSP;
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
        private CustomButton button10;
        private CustomTextBox tbData_f_z_u;
        private CustomTextBox tbData_f_o_u;
        private CustomTextBox tbData_f_z;
        private CustomTextBox tbData_f_o;
        private CustomTextBox tbOtgrStat;
        private CustomTextBox tbIs_got;
        private CustomTextBox tbUZSobrStat;
        private CustomTextBox tbUZSozdStat;
        private CustomTextBox tbUpakZayav;
        private CustomButton btnZayavFurnPrint;
        private CustomTextBox tbFZSobrStat;
        private CustomTextBox tbFZSozdStat;
        private CustomTextBox tbFurnZayav;
        private CustomButton btnFullKKPrint;
        private CustomTextBox tbUpakKKStat;
        private CustomTextBox tbFurnKKStat;
        private CustomButton btnUpakKKPrint;
        private CustomButton btnFurnKKPrint;
        private CustomButton button4;
        private CustomButton button5;
        private CustomButton button6;
        private CustomButton button1;
        private CustomButton btnNaklAbsent;
        private CustomButton btnNaklPart;
        private CustomButton btnNaklPrint;
        private CustomTextBox tbPszRpcNom;
        private CustomButton btnZayavUpakPrint;
        private CustomLabel label2;
        private CustomLabel label1;
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
        private CustomLabel label70;
        private CustomLabel label69;
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
        private CustomLabel label18;
        private CustomLabel label52;
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
        private CustomLabel label31;
        private CustomLabel label24;
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
        private CustomLabel label30;
        private CustomCheckBox cbRzuStirFact;
        private CustomCheckBox cbPszStirPlan;
        private CustomCheckBox cbRzuVishFact;
        private CustomCheckBox cbPszVishPlan;
        private CustomCheckBox cbRzuPrintFact;
        private CustomCheckBox cbPszPrintPlan;
    }
}