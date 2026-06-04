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
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions3 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions4 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions5 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions6 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions7 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions8 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions9 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions10 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions11 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
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
            layoutControl3 = new CustomLayoutControl();
            textBoxYearIzNakl = new CustomTextBox();
            textBoxIzNakl = new CustomTextBox();
            searchLookUpEditArticul = new CustomSearchLookUpEdit();
            customSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            columnKo = new DevExpress.XtraGrid.Columns.GridColumn();
            columnGrup = new DevExpress.XtraGrid.Columns.GridColumn();
            columnArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            columnMod = new DevExpress.XtraGrid.Columns.GridColumn();
            tbNomZad = new CustomTextBox();
            customLabel2 = new CustomLabel();
            customRadioGroup2 = new CustomRadioGroup();
            customRadioGroup3 = new CustomRadioGroup();
            layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem64 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup24 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem152 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem63 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup28 = new DevExpress.XtraLayout.LayoutControlGroup();
            emptySpaceItem76 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem196 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem77 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem198 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem65 = new DevExpress.XtraLayout.EmptySpaceItem();
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
            layoutControl9 = new CustomLayoutControl();
            furnitZayavViewUpak = new FurnitZayavView();
            furnitZayavViewFurnit = new FurnitZayavView();
            tbDatZayav = new CustomTextBox();
            simpleButtonUpakDeliveryInfoShow = new CustomSimpleButton();
            label61 = new CustomLabel();
            tbOtgrStat = new CustomTextBox();
            simpleButtonFullKKPrint = new CustomSimpleButton();
            mtbData_cd = new CustomMaskedTextBox();
            simpleButtonZayavUpakPrint = new CustomSimpleButton();
            simpleButtonFurnDeliveryInfoShow = new CustomSimpleButton();
            label60 = new CustomLabel();
            mtbData_zeh = new CustomMaskedTextBox();
            tbIs_got = new CustomTextBox();
            simpleButtonUpakKKPrint = new CustomSimpleButton();
            label59 = new CustomLabel();
            simpleButtonZayavFurnPrint = new CustomSimpleButton();
            simpleButtonFurnKKPrint = new CustomSimpleButton();
            tbData_f_z_u = new CustomTextBox();
            tbUZSobrStat = new CustomTextBox();
            tbFurnKKStat = new CustomTextBox();
            tbData_f_o_u = new CustomTextBox();
            label25 = new CustomLabel();
            label56 = new CustomLabel();
            tbFurnZayav = new CustomTextBox();
            tbUZSozdStat = new CustomTextBox();
            tbUpakKKStat = new CustomTextBox();
            label54 = new CustomLabel();
            tbData_f_o = new CustomTextBox();
            label57 = new CustomLabel();
            tbFZSozdStat = new CustomTextBox();
            tbUpakZayav = new CustomTextBox();
            label55 = new CustomLabel();
            label58 = new CustomLabel();
            tbData_f_z = new CustomTextBox();
            tbFZSobrStat = new CustomTextBox();
            layoutControlGroup26 = new DevExpress.XtraLayout.LayoutControlGroup();
            emptySpaceItem75 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem192 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem193 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem4 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlGroup27 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem159 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem61 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem160 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem161 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem162 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem163 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem164 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem165 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem62 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem166 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem167 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem168 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem66 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem169 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem170 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem171 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem172 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem67 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem173 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem174 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem175 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem176 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem177 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem68 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem178 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem179 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem180 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem181 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem69 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem182 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem183 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem70 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem184 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem186 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem71 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem187 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem188 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem189 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem72 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem73 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem190 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem191 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem74 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem185 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem60 = new DevExpress.XtraLayout.EmptySpaceItem();
            RasInfo = new DevExpress.XtraTab.XtraTabPage();
            layoutControl7 = new CustomLayoutControl();
            layoutControlGroup22 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup23 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControl5 = new CustomLayoutControl();
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
            layoutControlGroup15 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup16 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem105 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem107 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControl6 = new CustomLayoutControl();
            mtbRzuVidStir = new CustomTextBoxEx();
            mtbRzuDataStCd = new CustomTextBoxEx();
            mtbRzuDataStR = new CustomTextBoxEx();
            mtbRzuDataStP = new CustomTextBoxEx();
            mtbRzuDataVCd = new CustomTextBoxEx();
            cbRzuStirFact = new CustomCheckBox();
            mtbRzuDataVChi = new CustomTextBoxEx();
            mtbRzuDataVR = new CustomTextBoxEx();
            mtbRzuDataVP = new CustomTextBoxEx();
            mtbRzuDataRasv = new CustomTextBoxEx();
            cbPszStirPlan = new CustomCheckBox();
            mtbRzuDataPrCd = new CustomTextBoxEx();
            mtbRzuDataPrKm = new CustomTextBoxEx();
            mtbRzuDataPrPe = new CustomTextBoxEx();
            mtbRzuDataPrR = new CustomTextBoxEx();
            mtbRzuDataPrP = new CustomTextBoxEx();
            mtbRzuDataRasp = new CustomTextBoxEx();
            cbRzuVishFact = new CustomCheckBox();
            cbPszPrintPlan = new CustomCheckBox();
            cbRzuPrintFact = new CustomCheckBox();
            cbPszVishPlan = new CustomCheckBox();
            layoutControlGroup17 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup21 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup18 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem111 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem115 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem117 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem119 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem121 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem123 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem41 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem42 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem43 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem44 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem45 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem46 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem39 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem112 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem40 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem113 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup19 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem127 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem128 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem47 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem48 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem138 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem139 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem140 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem141 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem142 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem49 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem50 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem51 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem52 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem59 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup20 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem131 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem53 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem133 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem54 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem148 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem149 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem150 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem151 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem55 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem56 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem57 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem58 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControl4 = new CustomLayoutControl();
            textBoxDataZa = new CustomTextBoxEx();
            mtbRzuData1С = new CustomTextBoxEx();
            mtbRzuDataCd = new CustomTextBoxEx();
            mtbRzuDataUp = new CustomTextBoxEx();
            mtbRzuDataRab = new CustomTextBoxEx();
            mtbRzuDataZeh = new CustomTextBoxEx();
            tbPszRpcNom = new CustomTextBoxEx();
            mtbRzuDataR = new CustomTextBoxEx();
            mtbRzuDataCdUt = new CustomTextBoxEx();
            mtbPsaDataCdPlan = new CustomTextBoxEx();
            mtbPsaDataZap = new CustomTextBoxEx();
            layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup14 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem106 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem28 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem87 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem29 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem89 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem30 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem91 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem31 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem93 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem32 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem95 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem33 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem97 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem34 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem99 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem35 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem101 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem36 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem103 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem37 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem38 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem156 = new DevExpress.XtraLayout.LayoutControlItem();
            xtraTabControl1 = new CustomTabControl();
            SockZadanyInfo = new DevExpress.XtraTab.XtraTabPage();
            layoutControl2 = new CustomLayoutControl();
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
            TabPageMgKart = new DevExpress.XtraTab.XtraTabPage();
            layoutControl8 = new CustomLayoutControl();
            gridControlNastilGroupView = new CustomGridControl();
            gridViewNastilGroupView = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridNastilGroupViewColumnMgKart = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnKodPr = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnTArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnSeb = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnVN = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnKol = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnKolOnr = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnKolPog = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnSumSeb = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnSumRash = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilGroupViewColumnSumVetM = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlNastilList = new CustomGridControl();
            gridViewNastilList = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridNastilListColumnMgKart = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnNakl = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnDateR = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnTArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnSebTM = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnTkanType = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnRazr = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnVN = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnKol = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnKolOnr = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnKolOr = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnKp = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnKolPog = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnKolO = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnTkanExpense = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnChyl = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnTab1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnTab2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnTab3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnFio1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnFio2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnFio3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridNastilListColumnProzVipad = new DevExpress.XtraGrid.Columns.GridColumn();
            layoutControlGroup25 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem143 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem145 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControl1 = new CustomLayoutControl();
            buttonVshivkiPrint = new CustomSimpleButton();
            textBoxRzId = new CustomTextBoxEx();
            TextBoxRecomendZad = new CustomTextBox();
            TextBoxRecomendNom = new CustomTextBox();
            tbRzuMgZakr = new CustomTextBoxEx();
            pbEskiz = new PictureBox();
            tbSostPoln = new CustomTextBox();
            tbRzuMod = new CustomTextBox();
            tbArtGrup = new CustomTextBox();
            tbPsaKombOsn = new CustomTextBox();
            tbPsaPrn = new CustomTextBox();
            tbPsaKodZv2 = new CustomTextBox();
            tbPsaPsaIDOsn = new CustomTextBox();
            tbPsaKodZv1 = new CustomTextBox();
            tbPsaKombIzd = new CustomTextBox();
            tbPsaPsaID = new CustomTextBox();
            tbArtTradeMark = new CustomTextBox();
            tbPsaNameSbit = new CustomTextBox();
            tbRzuNom = new CustomTextBox();
            cbIsChip = new CustomCheckBox();
            tbRzuArticul = new CustomTextBox();
            tbPsaMenName = new CustomTextBox();
            tbPsaTbID = new CustomTextBox();
            psaSezName = new CustomTextBox();
            tbPsaYear = new CustomTextBox();
            tbRzuKol = new CustomTextBox();
            tbPsaNomZad = new CustomTextBox();
            tbPsaNN = new CustomTextBox();
            tbRzuDostZeh = new CustomTextBox();
            tbRzuPach = new CustomTextBox();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem52 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem24 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem155 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem110 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem54 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem56 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem58 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem59 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem15 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem17 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem18 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem19 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem63 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem20 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem65 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem21 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem67 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem69 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem16 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem71 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem73 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem22 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem61 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem48 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem75 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem23 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem77 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem79 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem81 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem83 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem85 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem25 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem26 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem135 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem158 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem27 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem194 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            customTextBoxEx1 = new CustomTextBoxEx();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem78 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl3).BeginInit();
            layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchLookUpEditArticul.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEdit1View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customRadioGroup2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customRadioGroup3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem64).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem152).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem63).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem76).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem196).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem77).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem198).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem65).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)layoutControl9).BeginInit();
            layoutControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem75).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem192).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem193).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem159).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem61).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem160).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem161).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem162).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem163).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem164).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem165).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem62).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem166).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem167).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem168).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem66).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem169).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem170).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem171).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem172).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem67).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem173).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem174).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem175).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem176).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem177).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem68).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem178).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem179).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem180).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem181).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem69).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem182).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem183).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem70).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem184).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem186).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem71).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem187).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem188).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem189).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem72).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem73).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem190).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem191).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem74).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem185).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem60).BeginInit();
            RasInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl5).BeginInit();
            layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlPartNaklList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPartNaklList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem105).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem107).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl6).BeginInit();
            layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mtbRzuVidStir.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataStCd.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataStR.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataStP.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataVCd.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataVChi.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataVR.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataVP.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataRasv.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrCd.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrKm.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrPe.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrR.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrP.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataRasp.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem111).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem115).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem117).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem119).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem121).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem123).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem41).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem42).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem43).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem44).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem45).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem46).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem39).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem112).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem40).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem113).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem127).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem128).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem47).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem48).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem138).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem139).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem140).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem141).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem142).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem49).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem50).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem51).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem52).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem59).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem131).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem53).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem133).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem54).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem148).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem149).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem150).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem151).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem55).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem56).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem57).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem58).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl4).BeginInit();
            layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxDataZa.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuData1С.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataCd.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataUp.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataRab.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataZeh.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbPszRpcNom.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataR.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataCdUt.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbPsaDataCdPlan.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtbPsaDataZap.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem106).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem87).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem29).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem89).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem30).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem91).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem93).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem32).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem95).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem97).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem34).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem99).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem35).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem101).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem36).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem103).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem37).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem38).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem156).BeginInit();
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
            TabPageMgKart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl8).BeginInit();
            layoutControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlNastilGroupView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNastilGroupView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlNastilList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNastilList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem143).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem145).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxRzId.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbRzuMgZakr.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbEskiz).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem46).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem50).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem52).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem155).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem110).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem54).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem56).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem58).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem59).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem63).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem65).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem67).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem69).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem71).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem73).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem61).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem48).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem75).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem77).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem79).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem81).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem83).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem85).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem135).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem158).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem194).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customTextBoxEx1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem78).BeginInit();
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
            tbYearPach.BorderStyle = BorderStyle.FixedSingle;
            tbYearPach.ErrorColor = Color.Red;
            tbYearPach.ErrorMessage = null;
            tbYearPach.Font = new Font("Arial", 10F);
            tbYearPach.Location = new Point(876, 5);
            tbYearPach.Margin = new Padding(0);
            tbYearPach.Name = "tbYearPach";
            tbYearPach.Size = new Size(102, 24);
            tbYearPach.TabIndex = 4;
            // 
            // tbNomPach
            // 
            tbNomPach.BorderStyle = BorderStyle.FixedSingle;
            tbNomPach.ErrorColor = Color.Red;
            tbNomPach.ErrorMessage = null;
            tbNomPach.Font = new Font("Arial", 10F);
            tbNomPach.Location = new Point(758, 5);
            tbNomPach.Margin = new Padding(0);
            tbNomPach.Name = "tbNomPach";
            tbNomPach.Size = new Size(86, 24);
            tbNomPach.TabIndex = 3;
            tbNomPach.KeyDown += tbNomPach_KeyDown;
            // 
            // layoutControl3
            // 
            layoutControl3.Controls.Add(textBoxYearIzNakl);
            layoutControl3.Controls.Add(textBoxIzNakl);
            layoutControl3.Controls.Add(searchLookUpEditArticul);
            layoutControl3.Controls.Add(tbNomZad);
            layoutControl3.Controls.Add(customLabel2);
            layoutControl3.Controls.Add(customRadioGroup2);
            layoutControl3.Controls.Add(tbYearPach);
            layoutControl3.Controls.Add(customRadioGroup3);
            layoutControl3.Controls.Add(tbNomPach);
            layoutControl3.Font = new Font("Arial", 10F);
            layoutControl3.Location = new Point(2, 3);
            layoutControl3.Name = "layoutControl3";
            layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(824, 283, 650, 400);
            layoutControl3.Root = layoutControlGroup8;
            layoutControl3.Size = new Size(1821, 34);
            layoutControl3.TabIndex = 13;
            layoutControl3.Text = "layoutControl3";
            // 
            // textBoxYearIzNakl
            // 
            textBoxYearIzNakl.BorderStyle = BorderStyle.FixedSingle;
            textBoxYearIzNakl.ErrorColor = Color.Red;
            textBoxYearIzNakl.ErrorMessage = null;
            textBoxYearIzNakl.Font = new Font("Arial", 10F);
            textBoxYearIzNakl.Location = new Point(1641, 5);
            textBoxYearIzNakl.Name = "textBoxYearIzNakl";
            textBoxYearIzNakl.Size = new Size(102, 24);
            textBoxYearIzNakl.TabIndex = 10;
            // 
            // textBoxIzNakl
            // 
            textBoxIzNakl.BorderStyle = BorderStyle.FixedSingle;
            textBoxIzNakl.ErrorColor = Color.Red;
            textBoxIzNakl.ErrorMessage = null;
            textBoxIzNakl.Font = new Font("Arial", 10F);
            textBoxIzNakl.Location = new Point(1484, 5);
            textBoxIzNakl.Name = "textBoxIzNakl";
            textBoxIzNakl.Size = new Size(115, 24);
            textBoxIzNakl.TabIndex = 7;
            textBoxIzNakl.KeyDown += textBoxIzNakl_KeyDown;
            // 
            // searchLookUpEditArticul
            // 
            searchLookUpEditArticul.Location = new Point(1247, 6);
            searchLookUpEditArticul.Name = "searchLookUpEditArticul";
            searchLookUpEditArticul.Properties.Appearance.Font = new Font("Arial", 10F);
            searchLookUpEditArticul.Properties.Appearance.Options.UseFont = true;
            searchLookUpEditArticul.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search) });
            searchLookUpEditArticul.Properties.PopupView = customSearchLookUpEdit1View;
            searchLookUpEditArticul.Size = new Size(139, 22);
            searchLookUpEditArticul.StyleController = layoutControl3;
            searchLookUpEditArticul.TabIndex = 6;
            searchLookUpEditArticul.ButtonClick += searchLookUpEditArticul_ButtonClick;
            searchLookUpEditArticul.EditValueChanged += searchLookUpEditArticul_EditValueChanged;
            // 
            // customSearchLookUpEdit1View
            // 
            customSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { columnKo, columnGrup, columnArticul, columnMod });
            customSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            customSearchLookUpEdit1View.Name = "customSearchLookUpEdit1View";
            customSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            customSearchLookUpEdit1View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            customSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // columnKo
            // 
            columnKo.Caption = "Код";
            columnKo.Name = "columnKo";
            columnKo.Visible = true;
            columnKo.VisibleIndex = 0;
            // 
            // columnGrup
            // 
            columnGrup.Caption = "Группа";
            columnGrup.Name = "columnGrup";
            columnGrup.Visible = true;
            columnGrup.VisibleIndex = 1;
            // 
            // columnArticul
            // 
            columnArticul.Caption = "Артикул";
            columnArticul.Name = "columnArticul";
            columnArticul.Visible = true;
            columnArticul.VisibleIndex = 2;
            // 
            // columnMod
            // 
            columnMod.Caption = "Модель";
            columnMod.Name = "columnMod";
            columnMod.Visible = true;
            columnMod.VisibleIndex = 3;
            // 
            // tbNomZad
            // 
            tbNomZad.BorderStyle = BorderStyle.FixedSingle;
            tbNomZad.ErrorColor = Color.Red;
            tbNomZad.ErrorMessage = null;
            tbNomZad.Font = new Font("Arial", 10F);
            tbNomZad.Location = new Point(1068, 5);
            tbNomZad.Margin = new Padding(0);
            tbNomZad.Name = "tbNomZad";
            tbNomZad.Size = new Size(105, 24);
            tbNomZad.TabIndex = 5;
            tbNomZad.KeyDown += tbNomZad_KeyDown;
            // 
            // customLabel2
            // 
            customLabel2.Appearance.BackColor = Color.Transparent;
            customLabel2.Appearance.Font = new Font("Arial", 12F, FontStyle.Bold);
            customLabel2.Appearance.Options.UseBackColor = true;
            customLabel2.Appearance.Options.UseFont = true;
            customLabel2.Appearance.Options.UseTextOptions = true;
            customLabel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel2.Location = new Point(2, 2);
            customLabel2.Margin = new Padding(0);
            customLabel2.Name = "customLabel2";
            customLabel2.Padding = new Padding(9, 0, 0, 0);
            customLabel2.Size = new Size(67, 19);
            customLabel2.StyleController = layoutControl3;
            customLabel2.TabIndex = 1;
            customLabel2.Text = "ПОИСК";
            customLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customRadioGroup2
            // 
            customRadioGroup2.Location = new Point(73, 2);
            customRadioGroup2.Name = "customRadioGroup2";
            customRadioGroup2.ObjectName = null;
            customRadioGroup2.Properties.Appearance.Font = new Font("Arial", 10F);
            customRadioGroup2.Properties.Appearance.ForeColor = SystemColors.ControlText;
            customRadioGroup2.Properties.Appearance.Options.UseFont = true;
            customRadioGroup2.Properties.Appearance.Options.UseForeColor = true;
            customRadioGroup2.Properties.Columns = 4;
            customRadioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ШП"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ВЗП"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Носки"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ШПМ") });
            customRadioGroup2.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            customRadioGroup2.Properties.Padding = new Padding(1, 0, 0, 0);
            customRadioGroup2.Size = new Size(243, 30);
            customRadioGroup2.StyleController = layoutControl3;
            customRadioGroup2.TabIndex = 0;
            customRadioGroup2.SelectedIndexChanged += customRadioGroup2_SelectedIndexChanged;
            // 
            // customRadioGroup3
            // 
            customRadioGroup3.Location = new Point(330, 2);
            customRadioGroup3.Name = "customRadioGroup3";
            customRadioGroup3.ObjectName = null;
            customRadioGroup3.Properties.Appearance.Font = new Font("Arial", 10F);
            customRadioGroup3.Properties.Appearance.ForeColor = SystemColors.ControlText;
            customRadioGroup3.Properties.Appearance.Options.UseFont = true;
            customRadioGroup3.Properties.Appearance.Options.UseForeColor = true;
            customRadioGroup3.Properties.Columns = 4;
            customRadioGroup3.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "№ задания", true, null, "ProcessingByTaskNumber"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "№ пачки", true, null, "ProcessingByPachNumber"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Артикул"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "№ накладной") });
            customRadioGroup3.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            customRadioGroup3.Properties.Padding = new Padding(1, 0, 0, 0);
            customRadioGroup3.Size = new Size(364, 30);
            customRadioGroup3.StyleController = layoutControl3;
            customRadioGroup3.TabIndex = 2;
            customRadioGroup3.SelectedIndexChanged += customRadioGroup3_SelectedIndexChanged;
            customRadioGroup3.EditValueChanged += customRadioGroup3_EditValueChanged;
            // 
            // layoutControlGroup8
            // 
            layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup8.GroupBordersVisible = false;
            layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3, emptySpaceItem1, emptySpaceItem3, layoutControlGroup1, layoutControlGroup2, layoutControlItem1, emptySpaceItem64, layoutControlGroup24, emptySpaceItem63, layoutControlGroup28, emptySpaceItem65 });
            layoutControlGroup8.Name = "Root";
            layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup8.Size = new Size(1821, 34);
            layoutControlGroup8.TextLocation = DevExpress.Utils.Locations.Left;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = customRadioGroup2;
            layoutControlItem2.Location = new Point(71, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(247, 34);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = customRadioGroup3;
            layoutControlItem3.Location = new Point(328, 0);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(368, 34);
            layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new Point(696, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(10, 34);
            // 
            // emptySpaceItem3
            // 
            emptySpaceItem3.Location = new Point(983, 0);
            emptySpaceItem3.Name = "emptySpaceItem3";
            emptySpaceItem3.Size = new Size(10, 34);
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem5, emptySpaceItem2, layoutControlItem7 });
            layoutControlGroup1.Location = new Point(706, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup1.Size = new Size(277, 34);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = tbNomPach;
            layoutControlItem5.Location = new Point(0, 0);
            layoutControlItem5.MaxSize = new Size(137, 28);
            layoutControlItem5.MinSize = new Size(137, 28);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(137, 28);
            layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem5.Text = "№ пачки";
            layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem5.TextSize = new Size(46, 13);
            layoutControlItem5.TextToControlDistance = 1;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.Location = new Point(137, 0);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new Size(10, 28);
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = tbYearPach;
            layoutControlItem7.Location = new Point(147, 0);
            layoutControlItem7.MaxSize = new Size(124, 28);
            layoutControlItem7.MinSize = new Size(124, 28);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new Size(124, 28);
            layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem7.Text = "год";
            layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem7.TextSize = new Size(18, 13);
            layoutControlItem7.TextToControlDistance = 0;
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem9 });
            layoutControlGroup2.Location = new Point(993, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup2.Size = new Size(185, 34);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = tbNomZad;
            layoutControlItem9.Location = new Point(0, 0);
            layoutControlItem9.MaxSize = new Size(179, 28);
            layoutControlItem9.MinSize = new Size(179, 28);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new Size(179, 28);
            layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem9.Text = "№ задания";
            layoutControlItem9.TextSize = new Size(58, 13);
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customLabel2;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(71, 34);
            layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem64
            // 
            emptySpaceItem64.Location = new Point(318, 0);
            emptySpaceItem64.Name = "emptySpaceItem64";
            emptySpaceItem64.Size = new Size(10, 34);
            // 
            // layoutControlGroup24
            // 
            layoutControlGroup24.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem152 });
            layoutControlGroup24.Location = new Point(1193, 0);
            layoutControlGroup24.Name = "layoutControlGroup24";
            layoutControlGroup24.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            layoutControlGroup24.Size = new Size(199, 34);
            layoutControlGroup24.TextVisible = false;
            // 
            // layoutControlItem152
            // 
            layoutControlItem152.Control = searchLookUpEditArticul;
            layoutControlItem152.Location = new Point(0, 0);
            layoutControlItem152.MaxSize = new Size(191, 26);
            layoutControlItem152.MinSize = new Size(191, 26);
            layoutControlItem152.Name = "layoutControlItem152";
            layoutControlItem152.Size = new Size(191, 26);
            layoutControlItem152.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem152.Text = "Артикул";
            layoutControlItem152.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem152.TextSize = new Size(43, 13);
            layoutControlItem152.TextToControlDistance = 5;
            // 
            // emptySpaceItem63
            // 
            emptySpaceItem63.Location = new Point(1178, 0);
            emptySpaceItem63.Name = "emptySpaceItem63";
            emptySpaceItem63.Size = new Size(15, 34);
            // 
            // layoutControlGroup28
            // 
            layoutControlGroup28.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { emptySpaceItem76, layoutControlItem196, emptySpaceItem77, layoutControlItem198 });
            layoutControlGroup28.Location = new Point(1403, 0);
            layoutControlGroup28.Name = "layoutControlGroup28";
            layoutControlGroup28.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup28.Size = new Size(418, 34);
            layoutControlGroup28.TextVisible = false;
            // 
            // emptySpaceItem76
            // 
            emptySpaceItem76.Location = new Point(339, 0);
            emptySpaceItem76.Name = "emptySpaceItem76";
            emptySpaceItem76.Size = new Size(73, 28);
            // 
            // layoutControlItem196
            // 
            layoutControlItem196.Control = textBoxIzNakl;
            layoutControlItem196.Location = new Point(0, 0);
            layoutControlItem196.MaxSize = new Size(195, 28);
            layoutControlItem196.MinSize = new Size(195, 28);
            layoutControlItem196.Name = "layoutControlItem196";
            layoutControlItem196.Size = new Size(195, 28);
            layoutControlItem196.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem196.Text = "№ накладной";
            layoutControlItem196.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem196.TextSize = new Size(71, 13);
            layoutControlItem196.TextToControlDistance = 5;
            // 
            // emptySpaceItem77
            // 
            emptySpaceItem77.Location = new Point(195, 0);
            emptySpaceItem77.Name = "emptySpaceItem77";
            emptySpaceItem77.Size = new Size(15, 28);
            // 
            // layoutControlItem198
            // 
            layoutControlItem198.Control = textBoxYearIzNakl;
            layoutControlItem198.Location = new Point(210, 0);
            layoutControlItem198.MinSize = new Size(24, 24);
            layoutControlItem198.Name = "layoutControlItem198";
            layoutControlItem198.Size = new Size(129, 28);
            layoutControlItem198.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem198.Text = "год";
            layoutControlItem198.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem198.TextSize = new Size(18, 13);
            layoutControlItem198.TextToControlDistance = 5;
            // 
            // emptySpaceItem65
            // 
            emptySpaceItem65.Location = new Point(1392, 0);
            emptySpaceItem65.Name = "emptySpaceItem65";
            emptySpaceItem65.Size = new Size(11, 34);
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
            OtdelkaInfo.Size = new Size(1817, 640);
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
            splitContainer2.Size = new Size(1817, 640);
            splitContainer2.SplitterDistance = 317;
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
            customGroupBox7.Size = new Size(1817, 317);
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
            tableLayoutPanel1.Size = new Size(1809, 292);
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
            gridControlProizvCombIzdSP.Size = new Size(1801, 255);
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
            sbProizvCombIzdSP.Location = new Point(1675, 264);
            sbProizvCombIzdSP.Margin = new Padding(4, 3, 4, 3);
            sbProizvCombIzdSP.Name = "sbProizvCombIzdSP";
            sbProizvCombIzdSP.Size = new Size(130, 25);
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
            customGroupBox8.Size = new Size(1817, 318);
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
            gridControlProizvCombIzdVZP.Size = new Size(1809, 293);
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
            gridControlNaklList.Location = new Point(441, 28);
            gridControlNaklList.MainView = gridViewNaklList;
            gridControlNaklList.Margin = new Padding(4, 3, 4, 3);
            gridControlNaklList.Name = "gridControlNaklList";
            gridControlNaklList.Size = new Size(1369, 163);
            gridControlNaklList.TabIndex = 2;
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
            gridControlOtdelka.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlOtdelka.Font = new Font("Arial", 10F);
            gridControlOtdelka.Location = new Point(1270, 26);
            gridControlOtdelka.MainView = gridViewOtdelka;
            gridControlOtdelka.Margin = new Padding(4, 3, 4, 3);
            gridControlOtdelka.Name = "gridControlOtdelka";
            gridControlOtdelka.Size = new Size(542, 243);
            gridControlOtdelka.TabIndex = 9;
            gridControlOtdelka.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewOtdelka });
            // 
            // WorkInfo
            // 
            WorkInfo.Appearance.HeaderActive.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            WorkInfo.Appearance.HeaderActive.Options.UseFont = true;
            WorkInfo.Margin = new Padding(4, 3, 4, 3);
            WorkInfo.Name = "WorkInfo";
            WorkInfo.Size = new Size(1817, 640);
            WorkInfo.Text = "ВЫПОЛНЕННАЯ РАБОТА";
            // 
            // FurnInfo
            // 
            FurnInfo.Controls.Add(layoutControl9);
            FurnInfo.Margin = new Padding(4, 3, 4, 3);
            FurnInfo.Name = "FurnInfo";
            FurnInfo.Size = new Size(1817, 651);
            FurnInfo.Text = "КОНФЕКЦИОН";
            // 
            // layoutControl9
            // 
            layoutControl9.Controls.Add(furnitZayavViewUpak);
            layoutControl9.Controls.Add(furnitZayavViewFurnit);
            layoutControl9.Controls.Add(tbDatZayav);
            layoutControl9.Controls.Add(simpleButtonUpakDeliveryInfoShow);
            layoutControl9.Controls.Add(label61);
            layoutControl9.Controls.Add(tbOtgrStat);
            layoutControl9.Controls.Add(simpleButtonFullKKPrint);
            layoutControl9.Controls.Add(mtbData_cd);
            layoutControl9.Controls.Add(simpleButtonZayavUpakPrint);
            layoutControl9.Controls.Add(simpleButtonFurnDeliveryInfoShow);
            layoutControl9.Controls.Add(label60);
            layoutControl9.Controls.Add(mtbData_zeh);
            layoutControl9.Controls.Add(tbIs_got);
            layoutControl9.Controls.Add(simpleButtonUpakKKPrint);
            layoutControl9.Controls.Add(label59);
            layoutControl9.Controls.Add(simpleButtonZayavFurnPrint);
            layoutControl9.Controls.Add(simpleButtonFurnKKPrint);
            layoutControl9.Controls.Add(tbData_f_z_u);
            layoutControl9.Controls.Add(tbUZSobrStat);
            layoutControl9.Controls.Add(tbFurnKKStat);
            layoutControl9.Controls.Add(tbData_f_o_u);
            layoutControl9.Controls.Add(label25);
            layoutControl9.Controls.Add(label56);
            layoutControl9.Controls.Add(tbFurnZayav);
            layoutControl9.Controls.Add(tbUZSozdStat);
            layoutControl9.Controls.Add(tbUpakKKStat);
            layoutControl9.Controls.Add(label54);
            layoutControl9.Controls.Add(tbData_f_o);
            layoutControl9.Controls.Add(label57);
            layoutControl9.Controls.Add(tbFZSozdStat);
            layoutControl9.Controls.Add(tbUpakZayav);
            layoutControl9.Controls.Add(label55);
            layoutControl9.Controls.Add(label58);
            layoutControl9.Controls.Add(tbData_f_z);
            layoutControl9.Controls.Add(tbFZSobrStat);
            layoutControl9.Dock = DockStyle.Fill;
            layoutControl9.Font = new Font("Arial", 10F);
            layoutControl9.Location = new Point(0, 0);
            layoutControl9.Name = "layoutControl9";
            layoutControl9.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(2267, 491, 650, 400);
            layoutControl9.Root = layoutControlGroup26;
            layoutControl9.Size = new Size(1817, 651);
            layoutControl9.TabIndex = 6;
            layoutControl9.Text = "layoutControl9";
            // 
            // furnitZayavViewUpak
            // 
            furnitZayavViewUpak.Location = new Point(281, 335);
            furnitZayavViewUpak.Margin = new Padding(5, 3, 5, 3);
            furnitZayavViewUpak.Name = "furnitZayavViewUpak";
            furnitZayavViewUpak.Size = new Size(1532, 312);
            furnitZayavViewUpak.TabIndex = 26;
            // 
            // furnitZayavViewFurnit
            // 
            furnitZayavViewFurnit.Location = new Point(281, 4);
            furnitZayavViewFurnit.Margin = new Padding(5, 3, 5, 3);
            furnitZayavViewFurnit.Name = "furnitZayavViewFurnit";
            furnitZayavViewFurnit.Size = new Size(1532, 317);
            furnitZayavViewFurnit.TabIndex = 25;
            // 
            // tbDatZayav
            // 
            tbDatZayav.BorderStyle = BorderStyle.FixedSingle;
            tbDatZayav.ErrorColor = Color.Red;
            tbDatZayav.ErrorMessage = null;
            tbDatZayav.Font = new Font("Arial", 10F);
            tbDatZayav.Location = new Point(137, 620);
            tbDatZayav.Margin = new Padding(0);
            tbDatZayav.Name = "tbDatZayav";
            tbDatZayav.Size = new Size(125, 22);
            tbDatZayav.TabIndex = 24;
            // 
            // simpleButtonUpakDeliveryInfoShow
            // 
            simpleButtonUpakDeliveryInfoShow.Appearance.Font = new Font("Arial", 10F);
            simpleButtonUpakDeliveryInfoShow.Appearance.Options.UseFont = true;
            simpleButtonUpakDeliveryInfoShow.ImageOptions.Image = (Image)resources.GetObject("simpleButtonUpakDeliveryInfoShow.ImageOptions.Image");
            simpleButtonUpakDeliveryInfoShow.Location = new Point(9, 394);
            simpleButtonUpakDeliveryInfoShow.Margin = new Padding(4, 3, 4, 3);
            simpleButtonUpakDeliveryInfoShow.Name = "simpleButtonUpakDeliveryInfoShow";
            simpleButtonUpakDeliveryInfoShow.Size = new Size(253, 24);
            simpleButtonUpakDeliveryInfoShow.StyleController = layoutControl9;
            simpleButtonUpakDeliveryInfoShow.TabIndex = 19;
            simpleButtonUpakDeliveryInfoShow.Text = "Инфо по доставке упак";
            simpleButtonUpakDeliveryInfoShow.Click += simpleButtonUpakDeliveryInfoShow_Click;
            // 
            // label61
            // 
            label61.Appearance.BackColor = Color.Transparent;
            label61.Appearance.Font = new Font("Arial", 9F);
            label61.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label61.Appearance.Options.UseBackColor = true;
            label61.Appearance.Options.UseFont = true;
            label61.Appearance.Options.UseForeColor = true;
            label61.Appearance.Options.UseTextOptions = true;
            label61.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label61.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label61.Location = new Point(9, 595);
            label61.Margin = new Padding(4, 0, 4, 0);
            label61.Name = "label61";
            label61.Size = new Size(253, 21);
            label61.StyleController = layoutControl9;
            label61.TabIndex = 1;
            label61.Text = "Предположительная дата создания заявки (при выполнении всех условий)";
            label61.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tbOtgrStat
            // 
            tbOtgrStat.BorderStyle = BorderStyle.FixedSingle;
            tbOtgrStat.ErrorColor = Color.Red;
            tbOtgrStat.ErrorMessage = null;
            tbOtgrStat.Font = new Font("Arial", 10F);
            tbOtgrStat.Location = new Point(224, 550);
            tbOtgrStat.Margin = new Padding(0);
            tbOtgrStat.Name = "tbOtgrStat";
            tbOtgrStat.Size = new Size(38, 31);
            tbOtgrStat.TabIndex = 23;
            tbOtgrStat.TextAlign = HorizontalAlignment.Center;
            // 
            // simpleButtonFullKKPrint
            // 
            simpleButtonFullKKPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonFullKKPrint.Appearance.Options.UseFont = true;
            simpleButtonFullKKPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonFullKKPrint.ImageOptions.Image");
            simpleButtonFullKKPrint.Location = new Point(9, 30);
            simpleButtonFullKKPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonFullKKPrint.Name = "simpleButtonFullKKPrint";
            simpleButtonFullKKPrint.Size = new Size(253, 32);
            simpleButtonFullKKPrint.StyleController = layoutControl9;
            simpleButtonFullKKPrint.TabIndex = 0;
            simpleButtonFullKKPrint.Text = "КК общая (просмотр/печать)";
            simpleButtonFullKKPrint.Click += simpleButtonFullKKPrint_Click_1;
            // 
            // mtbData_cd
            // 
            mtbData_cd.Font = new Font("Arial", 9F);
            mtbData_cd.Location = new Point(101, 550);
            mtbData_cd.Margin = new Padding(4, 3, 4, 3);
            mtbData_cd.Mask = "00/00/0000";
            mtbData_cd.Name = "mtbData_cd";
            mtbData_cd.Size = new Size(107, 31);
            mtbData_cd.TabIndex = 22;
            // 
            // simpleButtonZayavUpakPrint
            // 
            simpleButtonZayavUpakPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonZayavUpakPrint.Appearance.Options.UseFont = true;
            simpleButtonZayavUpakPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonZayavUpakPrint.ImageOptions.Image");
            simpleButtonZayavUpakPrint.Location = new Point(9, 366);
            simpleButtonZayavUpakPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonZayavUpakPrint.Name = "simpleButtonZayavUpakPrint";
            simpleButtonZayavUpakPrint.Size = new Size(253, 24);
            simpleButtonZayavUpakPrint.StyleController = layoutControl9;
            simpleButtonZayavUpakPrint.TabIndex = 18;
            simpleButtonZayavUpakPrint.Text = "ПРОСМОТР / ПЕЧАТЬ";
            simpleButtonZayavUpakPrint.Click += simpleButtonZayavUpakPrint_Click;
            // 
            // simpleButtonFurnDeliveryInfoShow
            // 
            simpleButtonFurnDeliveryInfoShow.Appearance.Font = new Font("Arial", 10F);
            simpleButtonFurnDeliveryInfoShow.Appearance.Options.UseFont = true;
            simpleButtonFurnDeliveryInfoShow.ImageOptions.Image = (Image)resources.GetObject("simpleButtonFurnDeliveryInfoShow.ImageOptions.Image");
            simpleButtonFurnDeliveryInfoShow.Location = new Point(9, 216);
            simpleButtonFurnDeliveryInfoShow.Margin = new Padding(4, 3, 4, 3);
            simpleButtonFurnDeliveryInfoShow.Name = "simpleButtonFurnDeliveryInfoShow";
            simpleButtonFurnDeliveryInfoShow.Size = new Size(253, 23);
            simpleButtonFurnDeliveryInfoShow.StyleController = layoutControl9;
            simpleButtonFurnDeliveryInfoShow.TabIndex = 10;
            simpleButtonFurnDeliveryInfoShow.Text = "Инфо по доставке фурн";
            simpleButtonFurnDeliveryInfoShow.Click += simpleButtonFurnUpakDeliveryInfoShow_Click;
            // 
            // label60
            // 
            label60.Appearance.BackColor = Color.Transparent;
            label60.Appearance.Font = new Font("Arial", 9F);
            label60.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label60.Appearance.Options.UseBackColor = true;
            label60.Appearance.Options.UseFont = true;
            label60.Appearance.Options.UseForeColor = true;
            label60.Appearance.Options.UseTextOptions = true;
            label60.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label60.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label60.Location = new Point(9, 550);
            label60.Margin = new Padding(4, 0, 4, 0);
            label60.Name = "label60";
            label60.Size = new Size(88, 31);
            label60.StyleController = layoutControl9;
            label60.TabIndex = 1;
            label60.Text = "Дата отгрузки \r\nс производства";
            label60.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // mtbData_zeh
            // 
            mtbData_zeh.Font = new Font("Arial", 9F);
            mtbData_zeh.Location = new Point(101, 525);
            mtbData_zeh.Margin = new Padding(4, 3, 4, 3);
            mtbData_zeh.Mask = "00/00/0000";
            mtbData_zeh.Name = "mtbData_zeh";
            mtbData_zeh.Size = new Size(161, 21);
            mtbData_zeh.TabIndex = 20;
            // 
            // tbIs_got
            // 
            tbIs_got.BorderStyle = BorderStyle.FixedSingle;
            tbIs_got.ErrorColor = Color.Red;
            tbIs_got.ErrorMessage = null;
            tbIs_got.Font = new Font("Arial", 10F);
            tbIs_got.Location = new Point(224, 499);
            tbIs_got.Margin = new Padding(0);
            tbIs_got.Name = "tbIs_got";
            tbIs_got.Size = new Size(38, 22);
            tbIs_got.TabIndex = 21;
            tbIs_got.TextAlign = HorizontalAlignment.Center;
            // 
            // simpleButtonUpakKKPrint
            // 
            simpleButtonUpakKKPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonUpakKKPrint.Appearance.Options.UseFont = true;
            simpleButtonUpakKKPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonUpakKKPrint.ImageOptions.Image");
            simpleButtonUpakKKPrint.Location = new Point(9, 253);
            simpleButtonUpakKKPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonUpakKKPrint.Name = "simpleButtonUpakKKPrint";
            simpleButtonUpakKKPrint.Size = new Size(211, 37);
            simpleButtonUpakKKPrint.StyleController = layoutControl9;
            simpleButtonUpakKKPrint.TabIndex = 11;
            simpleButtonUpakKKPrint.Text = "КК на упаковку \r\n(просмотр/печать)";
            simpleButtonUpakKKPrint.Click += simpleButtonUpakKKPrint_Click;
            // 
            // label59
            // 
            label59.Appearance.BackColor = Color.Transparent;
            label59.Appearance.Font = new Font("Arial", 9F);
            label59.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label59.Appearance.Options.UseBackColor = true;
            label59.Appearance.Options.UseFont = true;
            label59.Appearance.Options.UseForeColor = true;
            label59.Appearance.Options.UseTextOptions = true;
            label59.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label59.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label59.Location = new Point(9, 499);
            label59.Margin = new Padding(4, 0, 4, 0);
            label59.Name = "label59";
            label59.Size = new Size(88, 22);
            label59.StyleController = layoutControl9;
            label59.TabIndex = 1;
            label59.Text = "Дата в цех";
            label59.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // simpleButtonZayavFurnPrint
            // 
            simpleButtonZayavFurnPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonZayavFurnPrint.Appearance.Options.UseFont = true;
            simpleButtonZayavFurnPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonZayavFurnPrint.ImageOptions.Image");
            simpleButtonZayavFurnPrint.Location = new Point(9, 189);
            simpleButtonZayavFurnPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonZayavFurnPrint.Name = "simpleButtonZayavFurnPrint";
            simpleButtonZayavFurnPrint.Size = new Size(253, 23);
            simpleButtonZayavFurnPrint.StyleController = layoutControl9;
            simpleButtonZayavFurnPrint.TabIndex = 9;
            simpleButtonZayavFurnPrint.Text = "ПРОСМОТР / ПЕЧАТЬ";
            simpleButtonZayavFurnPrint.Click += simpleButtonZayavFurnPrint_Click;
            // 
            // simpleButtonFurnKKPrint
            // 
            simpleButtonFurnKKPrint.Appearance.Font = new Font("Arial", 10F);
            simpleButtonFurnKKPrint.Appearance.Options.UseFont = true;
            simpleButtonFurnKKPrint.Appearance.Options.UseTextOptions = true;
            simpleButtonFurnKKPrint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            simpleButtonFurnKKPrint.ImageOptions.Image = (Image)resources.GetObject("simpleButtonFurnKKPrint.ImageOptions.Image");
            simpleButtonFurnKKPrint.Location = new Point(9, 76);
            simpleButtonFurnKKPrint.Margin = new Padding(4, 3, 4, 3);
            simpleButtonFurnKKPrint.Name = "simpleButtonFurnKKPrint";
            simpleButtonFurnKKPrint.Size = new Size(211, 37);
            simpleButtonFurnKKPrint.StyleController = layoutControl9;
            simpleButtonFurnKKPrint.TabIndex = 2;
            simpleButtonFurnKKPrint.Text = "КК на фурнитуру \r\n(просмотр/печать)";
            simpleButtonFurnKKPrint.Click += simpleButtonFurnKKPrint_Click;
            // 
            // tbData_f_z_u
            // 
            tbData_f_z_u.BorderStyle = BorderStyle.FixedSingle;
            tbData_f_z_u.ErrorColor = Color.Red;
            tbData_f_z_u.ErrorMessage = null;
            tbData_f_z_u.Font = new Font("Arial", 10F);
            tbData_f_z_u.Location = new Point(61, 342);
            tbData_f_z_u.Margin = new Padding(0);
            tbData_f_z_u.Name = "tbData_f_z_u";
            tbData_f_z_u.Size = new Size(146, 20);
            tbData_f_z_u.TabIndex = 16;
            // 
            // tbUZSobrStat
            // 
            tbUZSobrStat.BorderStyle = BorderStyle.FixedSingle;
            tbUZSobrStat.ErrorColor = Color.Red;
            tbUZSobrStat.ErrorMessage = null;
            tbUZSobrStat.Font = new Font("Arial", 10F);
            tbUZSobrStat.Location = new Point(224, 342);
            tbUZSobrStat.Margin = new Padding(0);
            tbUZSobrStat.Name = "tbUZSobrStat";
            tbUZSobrStat.Size = new Size(38, 20);
            tbUZSobrStat.TabIndex = 17;
            tbUZSobrStat.TextAlign = HorizontalAlignment.Center;
            // 
            // tbFurnKKStat
            // 
            tbFurnKKStat.BorderStyle = BorderStyle.FixedSingle;
            tbFurnKKStat.ErrorColor = Color.Red;
            tbFurnKKStat.ErrorMessage = null;
            tbFurnKKStat.Font = new Font("Arial", 10F);
            tbFurnKKStat.Location = new Point(224, 76);
            tbFurnKKStat.Margin = new Padding(0);
            tbFurnKKStat.Name = "tbFurnKKStat";
            tbFurnKKStat.Size = new Size(38, 20);
            tbFurnKKStat.TabIndex = 3;
            tbFurnKKStat.TextAlign = HorizontalAlignment.Center;
            // 
            // tbData_f_o_u
            // 
            tbData_f_o_u.BorderStyle = BorderStyle.FixedSingle;
            tbData_f_o_u.ErrorColor = Color.Red;
            tbData_f_o_u.ErrorMessage = null;
            tbData_f_o_u.Font = new Font("Arial", 10F);
            tbData_f_o_u.Location = new Point(60, 318);
            tbData_f_o_u.Margin = new Padding(0);
            tbData_f_o_u.Name = "tbData_f_o_u";
            tbData_f_o_u.Size = new Size(146, 20);
            tbData_f_o_u.TabIndex = 14;
            // 
            // label25
            // 
            label25.Appearance.BackColor = Color.Transparent;
            label25.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            label25.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label25.Appearance.Options.UseBackColor = true;
            label25.Appearance.Options.UseFont = true;
            label25.Appearance.Options.UseForeColor = true;
            label25.Appearance.Options.UseTextOptions = true;
            label25.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label25.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label25.Location = new Point(9, 117);
            label25.Margin = new Padding(4, 0, 4, 0);
            label25.Name = "label25";
            label25.Size = new Size(147, 20);
            label25.StyleController = layoutControl9;
            label25.TabIndex = 1;
            label25.Text = "Заявка на фурнитуру №";
            label25.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label56
            // 
            label56.Appearance.BackColor = Color.Transparent;
            label56.Appearance.Font = new Font("Arial", 9F);
            label56.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label56.Appearance.Options.UseBackColor = true;
            label56.Appearance.Options.UseFont = true;
            label56.Appearance.Options.UseForeColor = true;
            label56.Appearance.Options.UseTextOptions = true;
            label56.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label56.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label56.Location = new Point(9, 342);
            label56.Margin = new Padding(4, 0, 4, 0);
            label56.Name = "label56";
            label56.Size = new Size(48, 15);
            label56.StyleController = layoutControl9;
            label56.TabIndex = 1;
            label56.Text = "собрана";
            label56.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tbFurnZayav
            // 
            tbFurnZayav.BorderStyle = BorderStyle.FixedSingle;
            tbFurnZayav.ErrorColor = Color.Red;
            tbFurnZayav.ErrorMessage = null;
            tbFurnZayav.Font = new Font("Arial", 10F);
            tbFurnZayav.Location = new Point(160, 117);
            tbFurnZayav.Margin = new Padding(0);
            tbFurnZayav.Name = "tbFurnZayav";
            tbFurnZayav.Size = new Size(102, 20);
            tbFurnZayav.TabIndex = 4;
            // 
            // tbUZSozdStat
            // 
            tbUZSozdStat.BorderStyle = BorderStyle.FixedSingle;
            tbUZSozdStat.ErrorColor = Color.Red;
            tbUZSozdStat.ErrorMessage = null;
            tbUZSozdStat.Font = new Font("Arial", 10F);
            tbUZSozdStat.Location = new Point(224, 318);
            tbUZSozdStat.Margin = new Padding(0);
            tbUZSozdStat.Name = "tbUZSozdStat";
            tbUZSozdStat.Size = new Size(38, 20);
            tbUZSozdStat.TabIndex = 15;
            tbUZSozdStat.TextAlign = HorizontalAlignment.Center;
            // 
            // tbUpakKKStat
            // 
            tbUpakKKStat.BorderStyle = BorderStyle.FixedSingle;
            tbUpakKKStat.ErrorColor = Color.Red;
            tbUpakKKStat.ErrorMessage = null;
            tbUpakKKStat.Font = new Font("Arial", 10F);
            tbUpakKKStat.Location = new Point(224, 253);
            tbUpakKKStat.Margin = new Padding(0);
            tbUpakKKStat.Name = "tbUpakKKStat";
            tbUpakKKStat.Size = new Size(38, 20);
            tbUpakKKStat.TabIndex = 12;
            tbUpakKKStat.TextAlign = HorizontalAlignment.Center;
            // 
            // label54
            // 
            label54.Appearance.BackColor = Color.Transparent;
            label54.Appearance.Font = new Font("Arial", 9F);
            label54.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label54.Appearance.Options.UseBackColor = true;
            label54.Appearance.Options.UseFont = true;
            label54.Appearance.Options.UseForeColor = true;
            label54.Appearance.Options.UseTextOptions = true;
            label54.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label54.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label54.Location = new Point(9, 141);
            label54.Margin = new Padding(4, 0, 4, 0);
            label54.Name = "label54";
            label54.Size = new Size(60, 20);
            label54.StyleController = layoutControl9;
            label54.TabIndex = 1;
            label54.Text = "создана";
            label54.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tbData_f_o
            // 
            tbData_f_o.BorderStyle = BorderStyle.FixedSingle;
            tbData_f_o.ErrorColor = Color.Red;
            tbData_f_o.ErrorMessage = null;
            tbData_f_o.Font = new Font("Arial", 10F);
            tbData_f_o.Location = new Point(73, 141);
            tbData_f_o.Margin = new Padding(0);
            tbData_f_o.Name = "tbData_f_o";
            tbData_f_o.Size = new Size(137, 20);
            tbData_f_o.TabIndex = 5;
            // 
            // label57
            // 
            label57.Appearance.BackColor = Color.Transparent;
            label57.Appearance.Font = new Font("Arial", 9F);
            label57.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label57.Appearance.Options.UseBackColor = true;
            label57.Appearance.Options.UseFont = true;
            label57.Appearance.Options.UseForeColor = true;
            label57.Appearance.Options.UseTextOptions = true;
            label57.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label57.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label57.Location = new Point(9, 318);
            label57.Margin = new Padding(4, 0, 4, 0);
            label57.Name = "label57";
            label57.Size = new Size(47, 15);
            label57.StyleController = layoutControl9;
            label57.TabIndex = 1;
            label57.Text = "создана";
            label57.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tbFZSozdStat
            // 
            tbFZSozdStat.BorderStyle = BorderStyle.FixedSingle;
            tbFZSozdStat.ErrorColor = Color.Red;
            tbFZSozdStat.ErrorMessage = null;
            tbFZSozdStat.Font = new Font("Arial", 10F);
            tbFZSozdStat.Location = new Point(224, 141);
            tbFZSozdStat.Margin = new Padding(0);
            tbFZSozdStat.Name = "tbFZSozdStat";
            tbFZSozdStat.Size = new Size(38, 20);
            tbFZSozdStat.TabIndex = 6;
            tbFZSozdStat.TextAlign = HorizontalAlignment.Center;
            // 
            // tbUpakZayav
            // 
            tbUpakZayav.BorderStyle = BorderStyle.FixedSingle;
            tbUpakZayav.ErrorColor = Color.Red;
            tbUpakZayav.ErrorMessage = null;
            tbUpakZayav.Font = new Font("Arial", 10F);
            tbUpakZayav.Location = new Point(148, 294);
            tbUpakZayav.Margin = new Padding(0);
            tbUpakZayav.Name = "tbUpakZayav";
            tbUpakZayav.Size = new Size(114, 20);
            tbUpakZayav.TabIndex = 13;
            // 
            // label55
            // 
            label55.Appearance.BackColor = Color.Transparent;
            label55.Appearance.Font = new Font("Arial", 9F);
            label55.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label55.Appearance.Options.UseBackColor = true;
            label55.Appearance.Options.UseFont = true;
            label55.Appearance.Options.UseForeColor = true;
            label55.Appearance.Options.UseTextOptions = true;
            label55.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label55.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label55.Location = new Point(9, 165);
            label55.Margin = new Padding(4, 0, 4, 0);
            label55.Name = "label55";
            label55.Size = new Size(48, 15);
            label55.StyleController = layoutControl9;
            label55.TabIndex = 1;
            label55.Text = "собрана";
            label55.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label58
            // 
            label58.Appearance.BackColor = Color.Transparent;
            label58.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            label58.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
            label58.Appearance.Options.UseBackColor = true;
            label58.Appearance.Options.UseFont = true;
            label58.Appearance.Options.UseForeColor = true;
            label58.Appearance.Options.UseTextOptions = true;
            label58.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            label58.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label58.Location = new Point(9, 294);
            label58.Margin = new Padding(4, 0, 4, 0);
            label58.Name = "label58";
            label58.Size = new Size(135, 13);
            label58.StyleController = layoutControl9;
            label58.TabIndex = 1;
            label58.Text = "Заявка на упаковку №";
            label58.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tbData_f_z
            // 
            tbData_f_z.BorderStyle = BorderStyle.FixedSingle;
            tbData_f_z.ErrorColor = Color.Red;
            tbData_f_z.ErrorMessage = null;
            tbData_f_z.Font = new Font("Arial", 10F);
            tbData_f_z.Location = new Point(61, 165);
            tbData_f_z.Margin = new Padding(0);
            tbData_f_z.Name = "tbData_f_z";
            tbData_f_z.Size = new Size(146, 20);
            tbData_f_z.TabIndex = 7;
            // 
            // tbFZSobrStat
            // 
            tbFZSobrStat.BorderStyle = BorderStyle.FixedSingle;
            tbFZSobrStat.ErrorColor = Color.Red;
            tbFZSobrStat.ErrorMessage = null;
            tbFZSobrStat.Font = new Font("Arial", 10F);
            tbFZSobrStat.Location = new Point(224, 165);
            tbFZSobrStat.Margin = new Padding(0);
            tbFZSobrStat.Name = "tbFZSobrStat";
            tbFZSobrStat.Size = new Size(38, 20);
            tbFZSobrStat.TabIndex = 8;
            tbFZSobrStat.TextAlign = HorizontalAlignment.Center;
            // 
            // layoutControlGroup26
            // 
            layoutControlGroup26.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup26.GroupBordersVisible = false;
            layoutControlGroup26.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { emptySpaceItem75, layoutControlItem192, layoutControlItem193, splitterItem4, layoutControlGroup27 });
            layoutControlGroup26.Name = "Root";
            layoutControlGroup26.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup26.Size = new Size(1817, 651);
            layoutControlGroup26.TextVisible = false;
            // 
            // emptySpaceItem75
            // 
            emptySpaceItem75.Location = new Point(267, 0);
            emptySpaceItem75.Name = "emptySpaceItem75";
            emptySpaceItem75.Size = new Size(10, 647);
            // 
            // layoutControlItem192
            // 
            layoutControlItem192.Control = furnitZayavViewFurnit;
            layoutControlItem192.Location = new Point(277, 0);
            layoutControlItem192.Name = "layoutControlItem192";
            layoutControlItem192.Size = new Size(1536, 321);
            layoutControlItem192.TextVisible = false;
            // 
            // layoutControlItem193
            // 
            layoutControlItem193.Control = furnitZayavViewUpak;
            layoutControlItem193.Location = new Point(277, 331);
            layoutControlItem193.Name = "layoutControlItem193";
            layoutControlItem193.Size = new Size(1536, 316);
            layoutControlItem193.TextVisible = false;
            // 
            // splitterItem4
            // 
            splitterItem4.Location = new Point(277, 321);
            splitterItem4.Name = "splitterItem4";
            splitterItem4.Size = new Size(1536, 10);
            // 
            // layoutControlGroup27
            // 
            layoutControlGroup27.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem159, emptySpaceItem61, layoutControlItem160, layoutControlItem161, layoutControlItem162, layoutControlItem163, layoutControlItem164, layoutControlItem165, emptySpaceItem62, layoutControlItem166, layoutControlItem167, layoutControlItem168, emptySpaceItem66, layoutControlItem169, layoutControlItem170, layoutControlItem171, layoutControlItem172, emptySpaceItem67, layoutControlItem173, layoutControlItem174, layoutControlItem175, layoutControlItem176, layoutControlItem177, emptySpaceItem68, layoutControlItem178, layoutControlItem179, layoutControlItem180, layoutControlItem181, emptySpaceItem69, layoutControlItem182, layoutControlItem183, emptySpaceItem70, layoutControlItem184, layoutControlItem186, emptySpaceItem71, layoutControlItem187, layoutControlItem188, layoutControlItem189, emptySpaceItem72, emptySpaceItem73, layoutControlItem190, layoutControlItem191, emptySpaceItem74, layoutControlItem185, emptySpaceItem60 });
            layoutControlGroup27.Location = new Point(0, 0);
            layoutControlGroup27.Name = "layoutControlGroup27";
            layoutControlGroup27.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup27.Size = new Size(267, 647);
            layoutControlGroup27.Text = "УСЛОВИЯ ДЛЯ СОЗДАНИЯ ЗАЯВОК";
            // 
            // layoutControlItem159
            // 
            layoutControlItem159.Control = simpleButtonFullKKPrint;
            layoutControlItem159.Location = new Point(0, 0);
            layoutControlItem159.MaxSize = new Size(257, 36);
            layoutControlItem159.MinSize = new Size(257, 36);
            layoutControlItem159.Name = "layoutControlItem159";
            layoutControlItem159.Size = new Size(257, 36);
            layoutControlItem159.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem159.TextVisible = false;
            // 
            // emptySpaceItem61
            // 
            emptySpaceItem61.Location = new Point(0, 36);
            emptySpaceItem61.Name = "emptySpaceItem61";
            emptySpaceItem61.Size = new Size(257, 10);
            // 
            // layoutControlItem160
            // 
            layoutControlItem160.Control = simpleButtonFurnKKPrint;
            layoutControlItem160.Location = new Point(0, 46);
            layoutControlItem160.Name = "layoutControlItem160";
            layoutControlItem160.Size = new Size(215, 41);
            layoutControlItem160.TextVisible = false;
            // 
            // layoutControlItem161
            // 
            layoutControlItem161.Control = tbFurnKKStat;
            layoutControlItem161.Location = new Point(215, 46);
            layoutControlItem161.MaxSize = new Size(42, 24);
            layoutControlItem161.MinSize = new Size(42, 24);
            layoutControlItem161.Name = "layoutControlItem161";
            layoutControlItem161.Size = new Size(42, 41);
            layoutControlItem161.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem161.TextVisible = false;
            // 
            // layoutControlItem162
            // 
            layoutControlItem162.Control = label25;
            layoutControlItem162.Location = new Point(0, 87);
            layoutControlItem162.MinSize = new Size(24, 24);
            layoutControlItem162.Name = "layoutControlItem162";
            layoutControlItem162.Size = new Size(151, 24);
            layoutControlItem162.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem162.TextVisible = false;
            // 
            // layoutControlItem163
            // 
            layoutControlItem163.Control = tbFurnZayav;
            layoutControlItem163.Location = new Point(151, 87);
            layoutControlItem163.MinSize = new Size(24, 24);
            layoutControlItem163.Name = "layoutControlItem163";
            layoutControlItem163.Size = new Size(106, 24);
            layoutControlItem163.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem163.TextVisible = false;
            // 
            // layoutControlItem164
            // 
            layoutControlItem164.Control = label54;
            layoutControlItem164.Location = new Point(0, 111);
            layoutControlItem164.MinSize = new Size(24, 24);
            layoutControlItem164.Name = "layoutControlItem164";
            layoutControlItem164.Size = new Size(64, 24);
            layoutControlItem164.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem164.TextVisible = false;
            // 
            // layoutControlItem165
            // 
            layoutControlItem165.Control = tbData_f_o;
            layoutControlItem165.Location = new Point(64, 111);
            layoutControlItem165.Name = "layoutControlItem165";
            layoutControlItem165.Size = new Size(141, 24);
            layoutControlItem165.TextVisible = false;
            // 
            // emptySpaceItem62
            // 
            emptySpaceItem62.Location = new Point(205, 111);
            emptySpaceItem62.Name = "emptySpaceItem62";
            emptySpaceItem62.Size = new Size(10, 24);
            // 
            // layoutControlItem166
            // 
            layoutControlItem166.Control = tbFZSozdStat;
            layoutControlItem166.Location = new Point(215, 111);
            layoutControlItem166.MaxSize = new Size(42, 0);
            layoutControlItem166.MinSize = new Size(42, 24);
            layoutControlItem166.Name = "layoutControlItem166";
            layoutControlItem166.Size = new Size(42, 24);
            layoutControlItem166.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem166.TextVisible = false;
            // 
            // layoutControlItem167
            // 
            layoutControlItem167.Control = label55;
            layoutControlItem167.Location = new Point(0, 135);
            layoutControlItem167.Name = "layoutControlItem167";
            layoutControlItem167.Size = new Size(52, 24);
            layoutControlItem167.TextVisible = false;
            // 
            // layoutControlItem168
            // 
            layoutControlItem168.Control = tbData_f_z;
            layoutControlItem168.Location = new Point(52, 135);
            layoutControlItem168.Name = "layoutControlItem168";
            layoutControlItem168.Size = new Size(150, 24);
            layoutControlItem168.TextVisible = false;
            // 
            // emptySpaceItem66
            // 
            emptySpaceItem66.Location = new Point(202, 135);
            emptySpaceItem66.Name = "emptySpaceItem66";
            emptySpaceItem66.Size = new Size(13, 24);
            // 
            // layoutControlItem169
            // 
            layoutControlItem169.Control = tbFZSobrStat;
            layoutControlItem169.Location = new Point(215, 135);
            layoutControlItem169.MaxSize = new Size(42, 0);
            layoutControlItem169.MinSize = new Size(42, 24);
            layoutControlItem169.Name = "layoutControlItem169";
            layoutControlItem169.Size = new Size(42, 24);
            layoutControlItem169.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem169.TextVisible = false;
            // 
            // layoutControlItem170
            // 
            layoutControlItem170.Control = simpleButtonZayavFurnPrint;
            layoutControlItem170.Location = new Point(0, 159);
            layoutControlItem170.MinSize = new Size(167, 26);
            layoutControlItem170.Name = "layoutControlItem170";
            layoutControlItem170.Size = new Size(257, 27);
            layoutControlItem170.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem170.TextVisible = false;
            // 
            // layoutControlItem171
            // 
            layoutControlItem171.Control = simpleButtonFurnDeliveryInfoShow;
            layoutControlItem171.Location = new Point(0, 186);
            layoutControlItem171.MinSize = new Size(172, 26);
            layoutControlItem171.Name = "layoutControlItem171";
            layoutControlItem171.Size = new Size(257, 27);
            layoutControlItem171.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem171.TextVisible = false;
            // 
            // layoutControlItem172
            // 
            layoutControlItem172.Control = simpleButtonUpakKKPrint;
            layoutControlItem172.Location = new Point(0, 223);
            layoutControlItem172.MinSize = new Size(138, 41);
            layoutControlItem172.Name = "layoutControlItem172";
            layoutControlItem172.Size = new Size(215, 41);
            layoutControlItem172.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem172.TextVisible = false;
            // 
            // emptySpaceItem67
            // 
            emptySpaceItem67.Location = new Point(0, 213);
            emptySpaceItem67.Name = "emptySpaceItem67";
            emptySpaceItem67.Size = new Size(257, 10);
            // 
            // layoutControlItem173
            // 
            layoutControlItem173.Control = tbUpakKKStat;
            layoutControlItem173.Location = new Point(215, 223);
            layoutControlItem173.MaxSize = new Size(42, 24);
            layoutControlItem173.MinSize = new Size(42, 24);
            layoutControlItem173.Name = "layoutControlItem173";
            layoutControlItem173.Size = new Size(42, 41);
            layoutControlItem173.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem173.TextVisible = false;
            // 
            // layoutControlItem174
            // 
            layoutControlItem174.Control = label58;
            layoutControlItem174.Location = new Point(0, 264);
            layoutControlItem174.Name = "layoutControlItem174";
            layoutControlItem174.Size = new Size(139, 24);
            layoutControlItem174.TextVisible = false;
            // 
            // layoutControlItem175
            // 
            layoutControlItem175.Control = tbUpakZayav;
            layoutControlItem175.Location = new Point(139, 264);
            layoutControlItem175.Name = "layoutControlItem175";
            layoutControlItem175.Size = new Size(118, 24);
            layoutControlItem175.TextVisible = false;
            // 
            // layoutControlItem176
            // 
            layoutControlItem176.Control = label57;
            layoutControlItem176.Location = new Point(0, 288);
            layoutControlItem176.Name = "layoutControlItem176";
            layoutControlItem176.Size = new Size(51, 24);
            layoutControlItem176.TextVisible = false;
            // 
            // layoutControlItem177
            // 
            layoutControlItem177.Control = tbData_f_o_u;
            layoutControlItem177.Location = new Point(51, 288);
            layoutControlItem177.Name = "layoutControlItem177";
            layoutControlItem177.Size = new Size(150, 24);
            layoutControlItem177.TextVisible = false;
            // 
            // emptySpaceItem68
            // 
            emptySpaceItem68.Location = new Point(201, 288);
            emptySpaceItem68.Name = "emptySpaceItem68";
            emptySpaceItem68.Size = new Size(14, 24);
            // 
            // layoutControlItem178
            // 
            layoutControlItem178.Control = tbUZSozdStat;
            layoutControlItem178.Location = new Point(215, 288);
            layoutControlItem178.MaxSize = new Size(42, 0);
            layoutControlItem178.MinSize = new Size(42, 24);
            layoutControlItem178.Name = "layoutControlItem178";
            layoutControlItem178.Size = new Size(42, 24);
            layoutControlItem178.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem178.TextVisible = false;
            // 
            // layoutControlItem179
            // 
            layoutControlItem179.Control = label56;
            layoutControlItem179.Location = new Point(0, 312);
            layoutControlItem179.Name = "layoutControlItem179";
            layoutControlItem179.Size = new Size(52, 24);
            layoutControlItem179.TextVisible = false;
            // 
            // layoutControlItem180
            // 
            layoutControlItem180.Control = tbData_f_z_u;
            layoutControlItem180.Location = new Point(52, 312);
            layoutControlItem180.Name = "layoutControlItem180";
            layoutControlItem180.Size = new Size(150, 24);
            layoutControlItem180.TextVisible = false;
            // 
            // layoutControlItem181
            // 
            layoutControlItem181.Control = tbUZSobrStat;
            layoutControlItem181.Location = new Point(215, 312);
            layoutControlItem181.MaxSize = new Size(42, 0);
            layoutControlItem181.MinSize = new Size(42, 24);
            layoutControlItem181.Name = "layoutControlItem181";
            layoutControlItem181.Size = new Size(42, 24);
            layoutControlItem181.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem181.TextVisible = false;
            // 
            // emptySpaceItem69
            // 
            emptySpaceItem69.Location = new Point(202, 312);
            emptySpaceItem69.Name = "emptySpaceItem69";
            emptySpaceItem69.Size = new Size(13, 24);
            // 
            // layoutControlItem182
            // 
            layoutControlItem182.Control = simpleButtonZayavUpakPrint;
            layoutControlItem182.Location = new Point(0, 336);
            layoutControlItem182.MinSize = new Size(167, 26);
            layoutControlItem182.Name = "layoutControlItem182";
            layoutControlItem182.Size = new Size(257, 28);
            layoutControlItem182.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem182.TextVisible = false;
            // 
            // layoutControlItem183
            // 
            layoutControlItem183.Control = simpleButtonUpakDeliveryInfoShow;
            layoutControlItem183.Location = new Point(0, 364);
            layoutControlItem183.MinSize = new Size(169, 26);
            layoutControlItem183.Name = "layoutControlItem183";
            layoutControlItem183.Size = new Size(257, 28);
            layoutControlItem183.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem183.TextVisible = false;
            // 
            // emptySpaceItem70
            // 
            emptySpaceItem70.Location = new Point(0, 392);
            emptySpaceItem70.Name = "emptySpaceItem70";
            emptySpaceItem70.Size = new Size(257, 77);
            // 
            // layoutControlItem184
            // 
            layoutControlItem184.Control = label59;
            layoutControlItem184.Location = new Point(0, 469);
            layoutControlItem184.MinSize = new Size(24, 24);
            layoutControlItem184.Name = "layoutControlItem184";
            layoutControlItem184.Size = new Size(92, 26);
            layoutControlItem184.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem184.TextVisible = false;
            // 
            // layoutControlItem186
            // 
            layoutControlItem186.Control = tbIs_got;
            layoutControlItem186.Location = new Point(215, 469);
            layoutControlItem186.MaxSize = new Size(42, 0);
            layoutControlItem186.MinSize = new Size(42, 24);
            layoutControlItem186.Name = "layoutControlItem186";
            layoutControlItem186.Size = new Size(42, 26);
            layoutControlItem186.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem186.TextVisible = false;
            // 
            // emptySpaceItem71
            // 
            emptySpaceItem71.Location = new Point(92, 469);
            emptySpaceItem71.Name = "emptySpaceItem71";
            emptySpaceItem71.Size = new Size(123, 26);
            // 
            // layoutControlItem187
            // 
            layoutControlItem187.Control = label60;
            layoutControlItem187.Location = new Point(0, 520);
            layoutControlItem187.MinSize = new Size(24, 24);
            layoutControlItem187.Name = "layoutControlItem187";
            layoutControlItem187.Size = new Size(92, 35);
            layoutControlItem187.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem187.TextVisible = false;
            // 
            // layoutControlItem188
            // 
            layoutControlItem188.Control = mtbData_cd;
            layoutControlItem188.Location = new Point(92, 520);
            layoutControlItem188.MinSize = new Size(104, 24);
            layoutControlItem188.Name = "layoutControlItem188";
            layoutControlItem188.Size = new Size(111, 35);
            layoutControlItem188.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem188.TextVisible = false;
            // 
            // layoutControlItem189
            // 
            layoutControlItem189.Control = tbOtgrStat;
            layoutControlItem189.Location = new Point(215, 520);
            layoutControlItem189.MinSize = new Size(24, 24);
            layoutControlItem189.Name = "layoutControlItem189";
            layoutControlItem189.Size = new Size(42, 35);
            layoutControlItem189.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem189.TextVisible = false;
            // 
            // emptySpaceItem72
            // 
            emptySpaceItem72.Location = new Point(203, 520);
            emptySpaceItem72.Name = "emptySpaceItem72";
            emptySpaceItem72.Size = new Size(12, 35);
            // 
            // emptySpaceItem73
            // 
            emptySpaceItem73.Location = new Point(0, 555);
            emptySpaceItem73.Name = "emptySpaceItem73";
            emptySpaceItem73.Size = new Size(257, 10);
            // 
            // layoutControlItem190
            // 
            layoutControlItem190.Control = label61;
            layoutControlItem190.Location = new Point(0, 565);
            layoutControlItem190.MinSize = new Size(24, 24);
            layoutControlItem190.Name = "layoutControlItem190";
            layoutControlItem190.Size = new Size(257, 25);
            layoutControlItem190.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem190.TextVisible = false;
            // 
            // layoutControlItem191
            // 
            layoutControlItem191.Control = tbDatZayav;
            layoutControlItem191.Location = new Point(128, 590);
            layoutControlItem191.MinSize = new Size(24, 24);
            layoutControlItem191.Name = "layoutControlItem191";
            layoutControlItem191.Size = new Size(129, 26);
            layoutControlItem191.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem191.TextVisible = false;
            // 
            // emptySpaceItem74
            // 
            emptySpaceItem74.Location = new Point(0, 590);
            emptySpaceItem74.Name = "emptySpaceItem74";
            emptySpaceItem74.Size = new Size(128, 26);
            // 
            // layoutControlItem185
            // 
            layoutControlItem185.Control = mtbData_zeh;
            layoutControlItem185.Location = new Point(92, 495);
            layoutControlItem185.MinSize = new Size(104, 24);
            layoutControlItem185.Name = "layoutControlItem185";
            layoutControlItem185.Size = new Size(165, 25);
            layoutControlItem185.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem185.TextVisible = false;
            // 
            // emptySpaceItem60
            // 
            emptySpaceItem60.Location = new Point(0, 495);
            emptySpaceItem60.Name = "emptySpaceItem60";
            emptySpaceItem60.Size = new Size(92, 25);
            // 
            // RasInfo
            // 
            RasInfo.Appearance.Header.Font = new Font("Tahoma", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            RasInfo.Appearance.Header.Options.UseFont = true;
            RasInfo.Appearance.HeaderActive.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            RasInfo.Appearance.HeaderActive.Options.UseFont = true;
            RasInfo.Appearance.HeaderDisabled.Font = new Font("Tahoma", 10F);
            RasInfo.Appearance.HeaderDisabled.Options.UseFont = true;
            RasInfo.Controls.Add(layoutControl7);
            RasInfo.Controls.Add(layoutControl5);
            RasInfo.Controls.Add(layoutControl6);
            RasInfo.Controls.Add(layoutControl4);
            RasInfo.Margin = new Padding(4, 3, 4, 3);
            RasInfo.Name = "RasInfo";
            RasInfo.Size = new Size(1817, 651);
            RasInfo.Text = "ИНФОРМАЦИЯ ПО РАСЧЕТУ";
            // 
            // layoutControl7
            // 
            layoutControl7.Font = new Font("Arial", 10F);
            layoutControl7.Location = new Point(0, 551);
            layoutControl7.Name = "layoutControl7";
            layoutControl7.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(1174, 276, 650, 400);
            layoutControl7.Root = layoutControlGroup22;
            layoutControl7.Size = new Size(1817, 81);
            layoutControl7.TabIndex = 16;
            layoutControl7.Text = "layoutControl7";
            // 
            // layoutControlGroup22
            // 
            layoutControlGroup22.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup22.GroupBordersVisible = false;
            layoutControlGroup22.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup23 });
            layoutControlGroup22.Name = "Root";
            layoutControlGroup22.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup22.Size = new Size(1817, 81);
            layoutControlGroup22.TextVisible = false;
            // 
            // layoutControlGroup23
            // 
            buttonImageOptions1.Image = (Image)resources.GetObject("buttonImageOptions1.Image");
            buttonImageOptions3.Image = (Image)resources.GetObject("buttonImageOptions3.Image");
            buttonImageOptions5.Image = (Image)resources.GetObject("buttonImageOptions5.Image");
            layoutControlGroup23.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Задание общ.", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Задание упак.", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Сопроводительные реестры", true, buttonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup23.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup23.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { emptySpaceItem78 });
            layoutControlGroup23.Location = new Point(0, 0);
            layoutControlGroup23.Name = "layoutControlGroup23";
            layoutControlGroup23.Size = new Size(1817, 81);
            layoutControlGroup23.Text = "ДОКУМЕНТЫ";
            layoutControlGroup23.CustomButtonClick += layoutControlGroup23_CustomButtonClick;
            // 
            // layoutControl5
            // 
            layoutControl5.Controls.Add(gridControlPartNaklList);
            layoutControl5.Controls.Add(gridControlNaklList);
            layoutControl5.Font = new Font("Arial", 10F);
            layoutControl5.Location = new Point(0, 79);
            layoutControl5.Name = "layoutControl5";
            layoutControl5.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(811, 640, 650, 400);
            layoutControl5.Root = layoutControlGroup15;
            layoutControl5.Size = new Size(1817, 198);
            layoutControl5.TabIndex = 17;
            layoutControl5.Text = "layoutControl5";
            // 
            // gridControlPartNaklList
            // 
            gridControlPartNaklList.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlPartNaklList.Font = new Font("Arial", 10F);
            gridControlPartNaklList.Location = new Point(7, 28);
            gridControlPartNaklList.MainView = gridViewPartNaklList;
            gridControlPartNaklList.Margin = new Padding(4, 3, 4, 3);
            gridControlPartNaklList.Name = "gridControlPartNaklList";
            gridControlPartNaklList.Size = new Size(430, 163);
            gridControlPartNaklList.TabIndex = 0;
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
            // layoutControlGroup15
            // 
            layoutControlGroup15.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup15.GroupBordersVisible = false;
            layoutControlGroup15.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup16 });
            layoutControlGroup15.Name = "Root";
            layoutControlGroup15.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup15.Size = new Size(1817, 198);
            layoutControlGroup15.TextVisible = false;
            // 
            // layoutControlGroup16
            // 
            buttonImageOptions6.Image = (Image)resources.GetObject("buttonImageOptions6.Image");
            buttonImageOptions8.Image = (Image)resources.GetObject("buttonImageOptions8.Image");
            layoutControlGroup16.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Показать информацию по делению накладной", true, buttonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions7, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Печать накладной", true, buttonImageOptions8, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions9, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, false, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Накладная не создана. Причина", true, buttonImageOptions10, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, false, null, -1) });
            layoutControlGroup16.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup16.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem105, layoutControlItem107 });
            layoutControlGroup16.Location = new Point(0, 0);
            layoutControlGroup16.Name = "layoutControlGroup16";
            layoutControlGroup16.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup16.Size = new Size(1817, 198);
            layoutControlGroup16.Text = "НАКЛАДНЫЕ";
            layoutControlGroup16.TextLocation = DevExpress.Utils.Locations.Default;
            layoutControlGroup16.CustomButtonClick += layoutControlGroup16_CustomButtonClick;
            // 
            // layoutControlItem105
            // 
            layoutControlItem105.Control = gridControlNaklList;
            layoutControlItem105.Location = new Point(434, 0);
            layoutControlItem105.MinSize = new Size(104, 24);
            layoutControlItem105.Name = "layoutControlItem105";
            layoutControlItem105.Size = new Size(1373, 167);
            layoutControlItem105.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem105.TextVisible = false;
            // 
            // layoutControlItem107
            // 
            layoutControlItem107.Control = gridControlPartNaklList;
            layoutControlItem107.Location = new Point(0, 0);
            layoutControlItem107.MinSize = new Size(104, 24);
            layoutControlItem107.Name = "layoutControlItem107";
            layoutControlItem107.Size = new Size(434, 167);
            layoutControlItem107.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem107.TextVisible = false;
            layoutControlItem107.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControl6
            // 
            layoutControl6.Controls.Add(gridControlOtdelka);
            layoutControl6.Controls.Add(mtbRzuVidStir);
            layoutControl6.Controls.Add(mtbRzuDataStCd);
            layoutControl6.Controls.Add(mtbRzuDataStR);
            layoutControl6.Controls.Add(mtbRzuDataStP);
            layoutControl6.Controls.Add(mtbRzuDataVCd);
            layoutControl6.Controls.Add(cbRzuStirFact);
            layoutControl6.Controls.Add(mtbRzuDataVChi);
            layoutControl6.Controls.Add(mtbRzuDataVR);
            layoutControl6.Controls.Add(mtbRzuDataVP);
            layoutControl6.Controls.Add(mtbRzuDataRasv);
            layoutControl6.Controls.Add(cbPszStirPlan);
            layoutControl6.Controls.Add(mtbRzuDataPrCd);
            layoutControl6.Controls.Add(mtbRzuDataPrKm);
            layoutControl6.Controls.Add(mtbRzuDataPrPe);
            layoutControl6.Controls.Add(mtbRzuDataPrR);
            layoutControl6.Controls.Add(mtbRzuDataPrP);
            layoutControl6.Controls.Add(mtbRzuDataRasp);
            layoutControl6.Controls.Add(cbRzuVishFact);
            layoutControl6.Controls.Add(cbPszPrintPlan);
            layoutControl6.Controls.Add(cbRzuPrintFact);
            layoutControl6.Controls.Add(cbPszVishPlan);
            layoutControl6.Font = new Font("Arial", 10F);
            layoutControl6.Location = new Point(0, 276);
            layoutControl6.Name = "layoutControl6";
            layoutControl6.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(510, 137, 650, 601);
            layoutControl6.Root = layoutControlGroup17;
            layoutControl6.Size = new Size(1817, 274);
            layoutControl6.TabIndex = 18;
            layoutControl6.Text = "layoutControl6";
            // 
            // mtbRzuVidStir
            // 
            mtbRzuVidStir.Location = new Point(743, 238);
            mtbRzuVidStir.Name = "mtbRzuVidStir";
            mtbRzuVidStir.ObjectName = null;
            mtbRzuVidStir.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuVidStir.Properties.Appearance.Options.UseFont = true;
            mtbRzuVidStir.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuVidStir.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuVidStir.Size = new Size(120, 22);
            mtbRzuVidStir.StyleController = layoutControl6;
            mtbRzuVidStir.TabIndex = 21;
            // 
            // mtbRzuDataStCd
            // 
            mtbRzuDataStCd.Location = new Point(546, 238);
            mtbRzuDataStCd.Name = "mtbRzuDataStCd";
            mtbRzuDataStCd.ObjectName = null;
            mtbRzuDataStCd.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataStCd.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataStCd.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataStCd.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataStCd.Size = new Size(121, 22);
            mtbRzuDataStCd.StyleController = layoutControl6;
            mtbRzuDataStCd.TabIndex = 20;
            // 
            // mtbRzuDataStR
            // 
            mtbRzuDataStR.Location = new Point(346, 238);
            mtbRzuDataStR.Name = "mtbRzuDataStR";
            mtbRzuDataStR.ObjectName = null;
            mtbRzuDataStR.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataStR.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataStR.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataStR.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataStR.Size = new Size(122, 22);
            mtbRzuDataStR.StyleController = layoutControl6;
            mtbRzuDataStR.TabIndex = 19;
            // 
            // mtbRzuDataStP
            // 
            mtbRzuDataStP.Location = new Point(146, 238);
            mtbRzuDataStP.Name = "mtbRzuDataStP";
            mtbRzuDataStP.ObjectName = null;
            mtbRzuDataStP.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataStP.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataStP.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataStP.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataStP.Size = new Size(117, 22);
            mtbRzuDataStP.StyleController = layoutControl6;
            mtbRzuDataStP.TabIndex = 18;
            // 
            // mtbRzuDataVCd
            // 
            mtbRzuDataVCd.Location = new Point(902, 157);
            mtbRzuDataVCd.Name = "mtbRzuDataVCd";
            mtbRzuDataVCd.ObjectName = null;
            mtbRzuDataVCd.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataVCd.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataVCd.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataVCd.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataVCd.Size = new Size(86, 22);
            mtbRzuDataVCd.StyleController = layoutControl6;
            mtbRzuDataVCd.TabIndex = 15;
            // 
            // cbRzuStirFact
            // 
            cbRzuStirFact.Font = new Font("Arial", 10F);
            cbRzuStirFact.ForeColor = SystemColors.ControlText;
            cbRzuStirFact.Location = new Point(135, 214);
            cbRzuStirFact.Margin = new Padding(4, 3, 4, 3);
            cbRzuStirFact.Name = "cbRzuStirFact";
            cbRzuStirFact.Size = new Size(44, 20);
            cbRzuStirFact.TabIndex = 17;
            cbRzuStirFact.UseVisualStyleBackColor = true;
            // 
            // mtbRzuDataVChi
            // 
            mtbRzuDataVChi.Location = new Point(704, 157);
            mtbRzuDataVChi.Name = "mtbRzuDataVChi";
            mtbRzuDataVChi.ObjectName = null;
            mtbRzuDataVChi.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataVChi.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataVChi.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataVChi.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataVChi.Size = new Size(120, 22);
            mtbRzuDataVChi.StyleController = layoutControl6;
            mtbRzuDataVChi.TabIndex = 14;
            // 
            // mtbRzuDataVR
            // 
            mtbRzuDataVR.Location = new Point(531, 157);
            mtbRzuDataVR.Name = "mtbRzuDataVR";
            mtbRzuDataVR.ObjectName = null;
            mtbRzuDataVR.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataVR.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataVR.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataVR.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataVR.Size = new Size(75, 22);
            mtbRzuDataVR.StyleController = layoutControl6;
            mtbRzuDataVR.TabIndex = 13;
            // 
            // mtbRzuDataVP
            // 
            mtbRzuDataVP.Location = new Point(328, 157);
            mtbRzuDataVP.Name = "mtbRzuDataVP";
            mtbRzuDataVP.ObjectName = null;
            mtbRzuDataVP.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataVP.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataVP.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataVP.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataVP.Size = new Size(61, 22);
            mtbRzuDataVP.StyleController = layoutControl6;
            mtbRzuDataVP.TabIndex = 12;
            // 
            // mtbRzuDataRasv
            // 
            mtbRzuDataRasv.Location = new Point(105, 157);
            mtbRzuDataRasv.Name = "mtbRzuDataRasv";
            mtbRzuDataRasv.ObjectName = null;
            mtbRzuDataRasv.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataRasv.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataRasv.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataRasv.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataRasv.Size = new Size(69, 22);
            mtbRzuDataRasv.StyleController = layoutControl6;
            mtbRzuDataRasv.TabIndex = 11;
            // 
            // cbPszStirPlan
            // 
            cbPszStirPlan.Font = new Font("Arial", 10F);
            cbPszStirPlan.ForeColor = SystemColors.ControlText;
            cbPszStirPlan.Location = new Point(40, 214);
            cbPszStirPlan.Margin = new Padding(4, 3, 4, 3);
            cbPszStirPlan.Name = "cbPszStirPlan";
            cbPszStirPlan.Size = new Size(50, 20);
            cbPszStirPlan.TabIndex = 16;
            cbPszStirPlan.UseVisualStyleBackColor = true;
            // 
            // mtbRzuDataPrCd
            // 
            mtbRzuDataPrCd.Location = new Point(1031, 76);
            mtbRzuDataPrCd.Name = "mtbRzuDataPrCd";
            mtbRzuDataPrCd.ObjectName = null;
            mtbRzuDataPrCd.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataPrCd.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataPrCd.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataPrCd.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataPrCd.Size = new Size(87, 22);
            mtbRzuDataPrCd.StyleController = layoutControl6;
            mtbRzuDataPrCd.TabIndex = 8;
            // 
            // mtbRzuDataPrKm
            // 
            mtbRzuDataPrKm.Location = new Point(869, 76);
            mtbRzuDataPrKm.Name = "mtbRzuDataPrKm";
            mtbRzuDataPrKm.ObjectName = null;
            mtbRzuDataPrKm.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataPrKm.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataPrKm.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataPrKm.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataPrKm.Size = new Size(84, 22);
            mtbRzuDataPrKm.StyleController = layoutControl6;
            mtbRzuDataPrKm.TabIndex = 7;
            // 
            // mtbRzuDataPrPe
            // 
            mtbRzuDataPrPe.Location = new Point(711, 76);
            mtbRzuDataPrPe.Name = "mtbRzuDataPrPe";
            mtbRzuDataPrPe.ObjectName = null;
            mtbRzuDataPrPe.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataPrPe.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataPrPe.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataPrPe.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataPrPe.Size = new Size(76, 22);
            mtbRzuDataPrPe.StyleController = layoutControl6;
            mtbRzuDataPrPe.TabIndex = 6;
            // 
            // mtbRzuDataPrR
            // 
            mtbRzuDataPrR.Location = new Point(509, 76);
            mtbRzuDataPrR.Name = "mtbRzuDataPrR";
            mtbRzuDataPrR.ObjectName = null;
            mtbRzuDataPrR.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataPrR.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataPrR.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataPrR.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataPrR.Size = new Size(76, 22);
            mtbRzuDataPrR.StyleController = layoutControl6;
            mtbRzuDataPrR.TabIndex = 5;
            // 
            // mtbRzuDataPrP
            // 
            mtbRzuDataPrP.Location = new Point(309, 76);
            mtbRzuDataPrP.Name = "mtbRzuDataPrP";
            mtbRzuDataPrP.ObjectName = null;
            mtbRzuDataPrP.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataPrP.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataPrP.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataPrP.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataPrP.Size = new Size(74, 22);
            mtbRzuDataPrP.StyleController = layoutControl6;
            mtbRzuDataPrP.TabIndex = 4;
            // 
            // mtbRzuDataRasp
            // 
            mtbRzuDataRasp.Location = new Point(89, 76);
            mtbRzuDataRasp.Name = "mtbRzuDataRasp";
            mtbRzuDataRasp.ObjectName = null;
            mtbRzuDataRasp.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataRasp.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataRasp.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataRasp.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            mtbRzuDataRasp.Size = new Size(82, 22);
            mtbRzuDataRasp.StyleController = layoutControl6;
            mtbRzuDataRasp.TabIndex = 3;
            // 
            // cbRzuVishFact
            // 
            cbRzuVishFact.Font = new Font("Arial", 10F);
            cbRzuVishFact.ForeColor = SystemColors.ControlText;
            cbRzuVishFact.Location = new Point(135, 133);
            cbRzuVishFact.Margin = new Padding(4, 3, 4, 3);
            cbRzuVishFact.Name = "cbRzuVishFact";
            cbRzuVishFact.Size = new Size(45, 20);
            cbRzuVishFact.TabIndex = 10;
            cbRzuVishFact.UseVisualStyleBackColor = true;
            // 
            // cbPszPrintPlan
            // 
            cbPszPrintPlan.Font = new Font("Arial", 10F);
            cbPszPrintPlan.Location = new Point(40, 52);
            cbPszPrintPlan.Margin = new Padding(4, 3, 4, 3);
            cbPszPrintPlan.Name = "cbPszPrintPlan";
            cbPszPrintPlan.Size = new Size(47, 20);
            cbPszPrintPlan.TabIndex = 0;
            cbPszPrintPlan.UseVisualStyleBackColor = true;
            // 
            // cbRzuPrintFact
            // 
            cbRzuPrintFact.Font = new Font("Arial", 10F);
            cbRzuPrintFact.Location = new Point(132, 52);
            cbRzuPrintFact.Margin = new Padding(4, 3, 4, 3);
            cbRzuPrintFact.Name = "cbRzuPrintFact";
            cbRzuPrintFact.Size = new Size(45, 20);
            cbRzuPrintFact.TabIndex = 2;
            cbRzuPrintFact.UseVisualStyleBackColor = true;
            // 
            // cbPszVishPlan
            // 
            cbPszVishPlan.Font = new Font("Arial", 10F);
            cbPszVishPlan.ForeColor = SystemColors.ControlText;
            cbPszVishPlan.Location = new Point(40, 133);
            cbPszVishPlan.Margin = new Padding(4, 3, 4, 3);
            cbPszVishPlan.Name = "cbPszVishPlan";
            cbPszVishPlan.Size = new Size(50, 20);
            cbPszVishPlan.TabIndex = 9;
            cbPszVishPlan.UseVisualStyleBackColor = true;
            // 
            // layoutControlGroup17
            // 
            layoutControlGroup17.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup17.GroupBordersVisible = false;
            layoutControlGroup17.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup21 });
            layoutControlGroup17.Name = "Root";
            layoutControlGroup17.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup17.Size = new Size(1817, 274);
            layoutControlGroup17.TextVisible = false;
            // 
            // layoutControlGroup21
            // 
            layoutControlGroup21.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup18, layoutControlGroup19, layoutControlGroup20, layoutControlItem4 });
            layoutControlGroup21.Location = new Point(0, 0);
            layoutControlGroup21.Name = "layoutControlGroup21";
            layoutControlGroup21.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup21.Size = new Size(1817, 274);
            layoutControlGroup21.Text = "ОТДЕЛКА / ДОП. ОБРАБОТКА";
            // 
            // layoutControlGroup18
            // 
            layoutControlGroup18.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem111, layoutControlItem115, layoutControlItem117, layoutControlItem119, layoutControlItem121, layoutControlItem123, emptySpaceItem41, emptySpaceItem42, emptySpaceItem43, emptySpaceItem44, emptySpaceItem45, emptySpaceItem46, emptySpaceItem39, layoutControlItem112, emptySpaceItem40, layoutControlItem113 });
            layoutControlGroup18.Location = new Point(0, 0);
            layoutControlGroup18.Name = "layoutControlGroup18";
            layoutControlGroup18.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup18.Size = new Size(1265, 81);
            layoutControlGroup18.Text = "ПРИНТ";
            // 
            // layoutControlItem111
            // 
            layoutControlItem111.Control = mtbRzuDataRasp;
            layoutControlItem111.Location = new Point(0, 24);
            layoutControlItem111.MinSize = new Size(54, 26);
            layoutControlItem111.Name = "layoutControlItem111";
            layoutControlItem111.Size = new Size(165, 26);
            layoutControlItem111.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem111.Text = "Дата на принт";
            layoutControlItem111.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem111.TextSize = new Size(74, 13);
            layoutControlItem111.TextToControlDistance = 5;
            // 
            // layoutControlItem115
            // 
            layoutControlItem115.Control = mtbRzuDataPrP;
            layoutControlItem115.Location = new Point(175, 24);
            layoutControlItem115.MinSize = new Size(54, 26);
            layoutControlItem115.Name = "layoutControlItem115";
            layoutControlItem115.Size = new Size(202, 26);
            layoutControlItem115.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem115.Text = "Дата принято на принт";
            layoutControlItem115.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem115.TextSize = new Size(119, 13);
            layoutControlItem115.TextToControlDistance = 5;
            // 
            // layoutControlItem117
            // 
            layoutControlItem117.Control = mtbRzuDataPrR;
            layoutControlItem117.Location = new Point(387, 24);
            layoutControlItem117.MinSize = new Size(54, 26);
            layoutControlItem117.Name = "layoutControlItem117";
            layoutControlItem117.Size = new Size(192, 26);
            layoutControlItem117.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem117.Text = "Дата в работу принт";
            layoutControlItem117.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem117.TextSize = new Size(107, 13);
            layoutControlItem117.TextToControlDistance = 5;
            // 
            // layoutControlItem119
            // 
            layoutControlItem119.Control = mtbRzuDataPrPe;
            layoutControlItem119.Location = new Point(589, 24);
            layoutControlItem119.MinSize = new Size(54, 26);
            layoutControlItem119.Name = "layoutControlItem119";
            layoutControlItem119.Size = new Size(192, 26);
            layoutControlItem119.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem119.Text = "Дата на печку принт";
            layoutControlItem119.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem119.TextSize = new Size(107, 13);
            layoutControlItem119.TextToControlDistance = 5;
            // 
            // layoutControlItem121
            // 
            layoutControlItem121.Control = mtbRzuDataPrKm;
            layoutControlItem121.Location = new Point(791, 24);
            layoutControlItem121.MinSize = new Size(54, 26);
            layoutControlItem121.Name = "layoutControlItem121";
            layoutControlItem121.Size = new Size(156, 26);
            layoutControlItem121.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem121.Text = "Дата компл.";
            layoutControlItem121.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem121.TextSize = new Size(63, 13);
            layoutControlItem121.TextToControlDistance = 5;
            // 
            // layoutControlItem123
            // 
            layoutControlItem123.Control = mtbRzuDataPrCd;
            layoutControlItem123.Location = new Point(957, 24);
            layoutControlItem123.MinSize = new Size(54, 26);
            layoutControlItem123.Name = "layoutControlItem123";
            layoutControlItem123.Size = new Size(155, 26);
            layoutControlItem123.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem123.Text = "Дата сдачи";
            layoutControlItem123.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem123.TextSize = new Size(59, 13);
            layoutControlItem123.TextToControlDistance = 5;
            // 
            // emptySpaceItem41
            // 
            emptySpaceItem41.Location = new Point(165, 24);
            emptySpaceItem41.Name = "emptySpaceItem41";
            emptySpaceItem41.Size = new Size(10, 26);
            // 
            // emptySpaceItem42
            // 
            emptySpaceItem42.Location = new Point(377, 24);
            emptySpaceItem42.Name = "emptySpaceItem42";
            emptySpaceItem42.Size = new Size(10, 26);
            // 
            // emptySpaceItem43
            // 
            emptySpaceItem43.Location = new Point(579, 24);
            emptySpaceItem43.Name = "emptySpaceItem43";
            emptySpaceItem43.Size = new Size(10, 26);
            // 
            // emptySpaceItem44
            // 
            emptySpaceItem44.Location = new Point(781, 24);
            emptySpaceItem44.Name = "emptySpaceItem44";
            emptySpaceItem44.Size = new Size(10, 26);
            // 
            // emptySpaceItem45
            // 
            emptySpaceItem45.Location = new Point(947, 24);
            emptySpaceItem45.Name = "emptySpaceItem45";
            emptySpaceItem45.Size = new Size(10, 26);
            // 
            // emptySpaceItem46
            // 
            emptySpaceItem46.Location = new Point(171, 0);
            emptySpaceItem46.Name = "emptySpaceItem46";
            emptySpaceItem46.Size = new Size(1084, 24);
            // 
            // emptySpaceItem39
            // 
            emptySpaceItem39.Location = new Point(1112, 24);
            emptySpaceItem39.Name = "emptySpaceItem39";
            emptySpaceItem39.Size = new Size(143, 26);
            // 
            // layoutControlItem112
            // 
            layoutControlItem112.Control = cbPszPrintPlan;
            layoutControlItem112.Location = new Point(0, 0);
            layoutControlItem112.MinSize = new Size(54, 24);
            layoutControlItem112.Name = "layoutControlItem112";
            layoutControlItem112.Size = new Size(81, 24);
            layoutControlItem112.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem112.Text = "План";
            layoutControlItem112.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem112.TextSize = new Size(25, 13);
            layoutControlItem112.TextToControlDistance = 5;
            // 
            // emptySpaceItem40
            // 
            emptySpaceItem40.Location = new Point(81, 0);
            emptySpaceItem40.Name = "emptySpaceItem40";
            emptySpaceItem40.Size = new Size(10, 24);
            // 
            // layoutControlItem113
            // 
            layoutControlItem113.Control = cbRzuPrintFact;
            layoutControlItem113.Location = new Point(91, 0);
            layoutControlItem113.MinSize = new Size(55, 24);
            layoutControlItem113.Name = "layoutControlItem113";
            layoutControlItem113.Size = new Size(80, 24);
            layoutControlItem113.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem113.Text = "Факт";
            layoutControlItem113.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem113.TextSize = new Size(26, 13);
            layoutControlItem113.TextToControlDistance = 5;
            // 
            // layoutControlGroup19
            // 
            layoutControlGroup19.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem127, layoutControlItem128, emptySpaceItem47, emptySpaceItem48, layoutControlItem138, layoutControlItem139, layoutControlItem140, layoutControlItem141, layoutControlItem142, emptySpaceItem49, emptySpaceItem50, emptySpaceItem51, emptySpaceItem52, emptySpaceItem59 });
            layoutControlGroup19.Location = new Point(0, 81);
            layoutControlGroup19.Name = "layoutControlGroup19";
            layoutControlGroup19.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup19.Size = new Size(1265, 81);
            layoutControlGroup19.Text = "ВЫШИВКА";
            // 
            // layoutControlItem127
            // 
            layoutControlItem127.Control = cbPszVishPlan;
            layoutControlItem127.Location = new Point(0, 0);
            layoutControlItem127.MinSize = new Size(24, 24);
            layoutControlItem127.Name = "layoutControlItem127";
            layoutControlItem127.Size = new Size(84, 24);
            layoutControlItem127.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem127.Text = "План";
            layoutControlItem127.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem127.TextSize = new Size(25, 13);
            layoutControlItem127.TextToControlDistance = 5;
            // 
            // layoutControlItem128
            // 
            layoutControlItem128.Control = cbRzuVishFact;
            layoutControlItem128.Location = new Point(94, 0);
            layoutControlItem128.MinSize = new Size(24, 24);
            layoutControlItem128.Name = "layoutControlItem128";
            layoutControlItem128.Size = new Size(80, 24);
            layoutControlItem128.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem128.Text = "Факт";
            layoutControlItem128.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem128.TextSize = new Size(26, 13);
            layoutControlItem128.TextToControlDistance = 5;
            // 
            // emptySpaceItem47
            // 
            emptySpaceItem47.Location = new Point(84, 0);
            emptySpaceItem47.Name = "emptySpaceItem47";
            emptySpaceItem47.Size = new Size(10, 24);
            // 
            // emptySpaceItem48
            // 
            emptySpaceItem48.Location = new Point(174, 0);
            emptySpaceItem48.Name = "emptySpaceItem48";
            emptySpaceItem48.Size = new Size(1081, 24);
            // 
            // layoutControlItem138
            // 
            layoutControlItem138.Control = mtbRzuDataRasv;
            layoutControlItem138.Location = new Point(0, 24);
            layoutControlItem138.MinSize = new Size(54, 26);
            layoutControlItem138.Name = "layoutControlItem138";
            layoutControlItem138.Size = new Size(168, 26);
            layoutControlItem138.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem138.Text = "Дата на вышивку";
            layoutControlItem138.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem138.TextSize = new Size(90, 13);
            layoutControlItem138.TextToControlDistance = 5;
            // 
            // layoutControlItem139
            // 
            layoutControlItem139.Control = mtbRzuDataVP;
            layoutControlItem139.Location = new Point(178, 24);
            layoutControlItem139.MinSize = new Size(54, 26);
            layoutControlItem139.Name = "layoutControlItem139";
            layoutControlItem139.Size = new Size(205, 26);
            layoutControlItem139.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem139.Text = "Дата принято на вышивку";
            layoutControlItem139.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem139.TextSize = new Size(135, 13);
            layoutControlItem139.TextToControlDistance = 5;
            // 
            // layoutControlItem140
            // 
            layoutControlItem140.Control = mtbRzuDataVR;
            layoutControlItem140.Location = new Point(393, 24);
            layoutControlItem140.MinSize = new Size(54, 26);
            layoutControlItem140.Name = "layoutControlItem140";
            layoutControlItem140.Size = new Size(207, 26);
            layoutControlItem140.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem140.Text = "Дата в работу вышивка";
            layoutControlItem140.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem140.TextSize = new Size(123, 13);
            layoutControlItem140.TextToControlDistance = 5;
            // 
            // layoutControlItem141
            // 
            layoutControlItem141.Control = mtbRzuDataVChi;
            layoutControlItem141.Location = new Point(610, 24);
            layoutControlItem141.MinSize = new Size(54, 26);
            layoutControlItem141.Name = "layoutControlItem141";
            layoutControlItem141.Size = new Size(208, 26);
            layoutControlItem141.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem141.Text = "Дата на чистку";
            layoutControlItem141.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem141.TextSize = new Size(79, 13);
            layoutControlItem141.TextToControlDistance = 5;
            // 
            // layoutControlItem142
            // 
            layoutControlItem142.Control = mtbRzuDataVCd;
            layoutControlItem142.Location = new Point(828, 24);
            layoutControlItem142.MinSize = new Size(54, 26);
            layoutControlItem142.Name = "layoutControlItem142";
            layoutControlItem142.Size = new Size(154, 26);
            layoutControlItem142.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem142.Text = "Дата сдачи";
            layoutControlItem142.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem142.TextSize = new Size(59, 13);
            layoutControlItem142.TextToControlDistance = 5;
            // 
            // emptySpaceItem49
            // 
            emptySpaceItem49.Location = new Point(168, 24);
            emptySpaceItem49.Name = "emptySpaceItem49";
            emptySpaceItem49.Size = new Size(10, 26);
            // 
            // emptySpaceItem50
            // 
            emptySpaceItem50.Location = new Point(383, 24);
            emptySpaceItem50.Name = "emptySpaceItem50";
            emptySpaceItem50.Size = new Size(10, 26);
            // 
            // emptySpaceItem51
            // 
            emptySpaceItem51.Location = new Point(600, 24);
            emptySpaceItem51.Name = "emptySpaceItem51";
            emptySpaceItem51.Size = new Size(10, 26);
            // 
            // emptySpaceItem52
            // 
            emptySpaceItem52.Location = new Point(818, 24);
            emptySpaceItem52.Name = "emptySpaceItem52";
            emptySpaceItem52.Size = new Size(10, 26);
            // 
            // emptySpaceItem59
            // 
            emptySpaceItem59.Location = new Point(982, 24);
            emptySpaceItem59.Name = "emptySpaceItem59";
            emptySpaceItem59.Size = new Size(273, 26);
            // 
            // layoutControlGroup20
            // 
            layoutControlGroup20.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem131, emptySpaceItem53, layoutControlItem133, emptySpaceItem54, layoutControlItem148, layoutControlItem149, layoutControlItem150, layoutControlItem151, emptySpaceItem55, emptySpaceItem56, emptySpaceItem57, emptySpaceItem58 });
            layoutControlGroup20.Location = new Point(0, 162);
            layoutControlGroup20.Name = "layoutControlGroup20";
            layoutControlGroup20.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup20.Size = new Size(1265, 85);
            layoutControlGroup20.Text = "СТИРКА";
            // 
            // layoutControlItem131
            // 
            layoutControlItem131.Control = cbPszStirPlan;
            layoutControlItem131.Location = new Point(0, 0);
            layoutControlItem131.MinSize = new Size(24, 24);
            layoutControlItem131.Name = "layoutControlItem131";
            layoutControlItem131.Size = new Size(84, 24);
            layoutControlItem131.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem131.Text = "План";
            layoutControlItem131.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem131.TextSize = new Size(25, 13);
            layoutControlItem131.TextToControlDistance = 5;
            // 
            // emptySpaceItem53
            // 
            emptySpaceItem53.Location = new Point(84, 0);
            emptySpaceItem53.Name = "emptySpaceItem53";
            emptySpaceItem53.Size = new Size(10, 24);
            // 
            // layoutControlItem133
            // 
            layoutControlItem133.Control = cbRzuStirFact;
            layoutControlItem133.Location = new Point(94, 0);
            layoutControlItem133.MinSize = new Size(24, 24);
            layoutControlItem133.Name = "layoutControlItem133";
            layoutControlItem133.Size = new Size(79, 24);
            layoutControlItem133.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem133.Text = "Факт";
            layoutControlItem133.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem133.TextSize = new Size(26, 13);
            layoutControlItem133.TextToControlDistance = 5;
            // 
            // emptySpaceItem54
            // 
            emptySpaceItem54.Location = new Point(173, 0);
            emptySpaceItem54.Name = "emptySpaceItem54";
            emptySpaceItem54.Size = new Size(1082, 24);
            // 
            // layoutControlItem148
            // 
            layoutControlItem148.Control = mtbRzuDataStP;
            layoutControlItem148.Location = new Point(0, 24);
            layoutControlItem148.MinSize = new Size(54, 26);
            layoutControlItem148.Name = "layoutControlItem148";
            layoutControlItem148.Size = new Size(257, 30);
            layoutControlItem148.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem148.Text = "Дата принято на стирку";
            layoutControlItem148.TextSize = new Size(124, 13);
            // 
            // layoutControlItem149
            // 
            layoutControlItem149.Control = mtbRzuDataStR;
            layoutControlItem149.Location = new Point(267, 24);
            layoutControlItem149.MinSize = new Size(54, 26);
            layoutControlItem149.Name = "layoutControlItem149";
            layoutControlItem149.Size = new Size(195, 30);
            layoutControlItem149.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem149.Text = "Дата стирки";
            layoutControlItem149.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem149.TextSize = new Size(64, 13);
            layoutControlItem149.TextToControlDistance = 5;
            // 
            // layoutControlItem150
            // 
            layoutControlItem150.Control = mtbRzuDataStCd;
            layoutControlItem150.Location = new Point(472, 24);
            layoutControlItem150.MinSize = new Size(54, 26);
            layoutControlItem150.Name = "layoutControlItem150";
            layoutControlItem150.Size = new Size(189, 30);
            layoutControlItem150.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem150.Text = "Дата сдачи";
            layoutControlItem150.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem150.TextSize = new Size(59, 13);
            layoutControlItem150.TextToControlDistance = 5;
            // 
            // layoutControlItem151
            // 
            layoutControlItem151.Control = mtbRzuVidStir;
            layoutControlItem151.Location = new Point(671, 24);
            layoutControlItem151.MinSize = new Size(54, 26);
            layoutControlItem151.Name = "layoutControlItem151";
            layoutControlItem151.Size = new Size(186, 30);
            layoutControlItem151.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem151.Text = "Вид стирки";
            layoutControlItem151.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem151.TextSize = new Size(57, 13);
            layoutControlItem151.TextToControlDistance = 5;
            // 
            // emptySpaceItem55
            // 
            emptySpaceItem55.Location = new Point(257, 24);
            emptySpaceItem55.Name = "emptySpaceItem55";
            emptySpaceItem55.Size = new Size(10, 30);
            // 
            // emptySpaceItem56
            // 
            emptySpaceItem56.Location = new Point(462, 24);
            emptySpaceItem56.Name = "emptySpaceItem56";
            emptySpaceItem56.Size = new Size(10, 30);
            // 
            // emptySpaceItem57
            // 
            emptySpaceItem57.Location = new Point(661, 24);
            emptySpaceItem57.Name = "emptySpaceItem57";
            emptySpaceItem57.Size = new Size(10, 30);
            // 
            // emptySpaceItem58
            // 
            emptySpaceItem58.Location = new Point(857, 24);
            emptySpaceItem58.Name = "emptySpaceItem58";
            emptySpaceItem58.Size = new Size(398, 30);
            // 
            // layoutControl4
            // 
            layoutControl4.Controls.Add(textBoxDataZa);
            layoutControl4.Controls.Add(mtbRzuData1С);
            layoutControl4.Controls.Add(mtbRzuDataCd);
            layoutControl4.Controls.Add(mtbRzuDataUp);
            layoutControl4.Controls.Add(mtbRzuDataRab);
            layoutControl4.Controls.Add(mtbRzuDataZeh);
            layoutControl4.Controls.Add(tbPszRpcNom);
            layoutControl4.Controls.Add(mtbRzuDataR);
            layoutControl4.Controls.Add(mtbRzuDataCdUt);
            layoutControl4.Controls.Add(mtbPsaDataCdPlan);
            layoutControl4.Controls.Add(mtbPsaDataZap);
            layoutControl4.Font = new Font("Arial", 10F);
            layoutControl4.Location = new Point(0, 3);
            layoutControl4.Name = "layoutControl4";
            layoutControl4.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(2380, 340, 650, 400);
            layoutControl4.Root = layoutControlGroup13;
            layoutControl4.Size = new Size(1817, 77);
            layoutControl4.TabIndex = 16;
            layoutControl4.Text = "layoutControl4";
            // 
            // textBoxDataZa
            // 
            textBoxDataZa.Location = new Point(7, 44);
            textBoxDataZa.Name = "textBoxDataZa";
            textBoxDataZa.ObjectName = null;
            textBoxDataZa.Properties.Appearance.Font = new Font("Arial", 10F);
            textBoxDataZa.Properties.Appearance.Options.UseFont = true;
            textBoxDataZa.Size = new Size(161, 22);
            textBoxDataZa.StyleController = layoutControl4;
            textBoxDataZa.TabIndex = 0;
            // 
            // mtbRzuData1С
            // 
            mtbRzuData1С.Location = new Point(1639, 44);
            mtbRzuData1С.Name = "mtbRzuData1С";
            mtbRzuData1С.ObjectName = null;
            mtbRzuData1С.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuData1С.Properties.Appearance.Options.UseFont = true;
            mtbRzuData1С.Size = new Size(161, 22);
            mtbRzuData1С.StyleController = layoutControl4;
            mtbRzuData1С.TabIndex = 11;
            // 
            // mtbRzuDataCd
            // 
            mtbRzuDataCd.Location = new Point(1464, 44);
            mtbRzuDataCd.Name = "mtbRzuDataCd";
            mtbRzuDataCd.ObjectName = null;
            mtbRzuDataCd.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataCd.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataCd.Size = new Size(161, 22);
            mtbRzuDataCd.StyleController = layoutControl4;
            mtbRzuDataCd.TabIndex = 10;
            // 
            // mtbRzuDataUp
            // 
            mtbRzuDataUp.Location = new Point(1289, 44);
            mtbRzuDataUp.Name = "mtbRzuDataUp";
            mtbRzuDataUp.ObjectName = null;
            mtbRzuDataUp.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataUp.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataUp.Size = new Size(161, 22);
            mtbRzuDataUp.StyleController = layoutControl4;
            mtbRzuDataUp.TabIndex = 9;
            // 
            // mtbRzuDataRab
            // 
            mtbRzuDataRab.Location = new Point(1114, 44);
            mtbRzuDataRab.Name = "mtbRzuDataRab";
            mtbRzuDataRab.ObjectName = null;
            mtbRzuDataRab.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataRab.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataRab.Size = new Size(161, 22);
            mtbRzuDataRab.StyleController = layoutControl4;
            mtbRzuDataRab.TabIndex = 8;
            // 
            // mtbRzuDataZeh
            // 
            mtbRzuDataZeh.Location = new Point(939, 44);
            mtbRzuDataZeh.Name = "mtbRzuDataZeh";
            mtbRzuDataZeh.ObjectName = null;
            mtbRzuDataZeh.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataZeh.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataZeh.Size = new Size(161, 22);
            mtbRzuDataZeh.StyleController = layoutControl4;
            mtbRzuDataZeh.TabIndex = 7;
            // 
            // tbPszRpcNom
            // 
            tbPszRpcNom.Location = new Point(875, 44);
            tbPszRpcNom.Name = "tbPszRpcNom";
            tbPszRpcNom.ObjectName = null;
            tbPszRpcNom.Properties.Appearance.Font = new Font("Arial", 10F);
            tbPszRpcNom.Properties.Appearance.Options.UseFont = true;
            tbPszRpcNom.Size = new Size(50, 22);
            tbPszRpcNom.StyleController = layoutControl4;
            tbPszRpcNom.TabIndex = 6;
            // 
            // mtbRzuDataR
            // 
            mtbRzuDataR.Location = new Point(700, 44);
            mtbRzuDataR.Name = "mtbRzuDataR";
            mtbRzuDataR.ObjectName = null;
            mtbRzuDataR.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataR.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataR.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataR.Size = new Size(161, 22);
            mtbRzuDataR.StyleController = layoutControl4;
            mtbRzuDataR.TabIndex = 5;
            // 
            // mtbRzuDataCdUt
            // 
            mtbRzuDataCdUt.Location = new Point(490, 44);
            mtbRzuDataCdUt.Name = "mtbRzuDataCdUt";
            mtbRzuDataCdUt.ObjectName = null;
            mtbRzuDataCdUt.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbRzuDataCdUt.Properties.Appearance.Options.UseFont = true;
            mtbRzuDataCdUt.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbRzuDataCdUt.Size = new Size(196, 22);
            mtbRzuDataCdUt.StyleController = layoutControl4;
            mtbRzuDataCdUt.TabIndex = 4;
            // 
            // mtbPsaDataCdPlan
            // 
            mtbPsaDataCdPlan.Location = new Point(357, 44);
            mtbPsaDataCdPlan.Name = "mtbPsaDataCdPlan";
            mtbPsaDataCdPlan.ObjectName = null;
            mtbPsaDataCdPlan.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbPsaDataCdPlan.Properties.Appearance.Options.UseFont = true;
            mtbPsaDataCdPlan.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbPsaDataCdPlan.Size = new Size(119, 22);
            mtbPsaDataCdPlan.StyleController = layoutControl4;
            mtbPsaDataCdPlan.TabIndex = 3;
            // 
            // mtbPsaDataZap
            // 
            mtbPsaDataZap.Location = new Point(182, 44);
            mtbPsaDataZap.Name = "mtbPsaDataZap";
            mtbPsaDataZap.ObjectName = null;
            mtbPsaDataZap.Properties.Appearance.Font = new Font("Arial", 10F);
            mtbPsaDataZap.Properties.Appearance.Options.UseFont = true;
            mtbPsaDataZap.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            mtbPsaDataZap.Size = new Size(161, 22);
            mtbPsaDataZap.StyleController = layoutControl4;
            mtbPsaDataZap.TabIndex = 2;
            // 
            // layoutControlGroup13
            // 
            layoutControlGroup13.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup13.GroupBordersVisible = false;
            layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup14 });
            layoutControlGroup13.Name = "Root";
            layoutControlGroup13.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup13.Size = new Size(1817, 77);
            layoutControlGroup13.TextVisible = false;
            // 
            // layoutControlGroup14
            // 
            layoutControlGroup14.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem106, emptySpaceItem28, layoutControlItem87, emptySpaceItem29, layoutControlItem89, emptySpaceItem30, layoutControlItem91, emptySpaceItem31, layoutControlItem93, emptySpaceItem32, layoutControlItem95, emptySpaceItem33, layoutControlItem97, emptySpaceItem34, layoutControlItem99, emptySpaceItem35, layoutControlItem101, emptySpaceItem36, layoutControlItem103, emptySpaceItem37, emptySpaceItem38, layoutControlItem156 });
            layoutControlGroup14.Location = new Point(0, 0);
            layoutControlGroup14.Name = "layoutControlGroup14";
            layoutControlGroup14.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup14.Size = new Size(1817, 77);
            layoutControlGroup14.Text = "КОНТРОЛЬНЫЕ ДАТЫ";
            // 
            // layoutControlItem106
            // 
            layoutControlItem106.Control = mtbPsaDataZap;
            layoutControlItem106.Location = new Point(175, 0);
            layoutControlItem106.Name = "layoutControlItem106";
            layoutControlItem106.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem106.Size = new Size(165, 46);
            layoutControlItem106.Text = "Дата запуска";
            layoutControlItem106.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem106.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem28
            // 
            emptySpaceItem28.Location = new Point(340, 0);
            emptySpaceItem28.Name = "emptySpaceItem28";
            emptySpaceItem28.OptionsTableLayoutItem.RowIndex = 1;
            emptySpaceItem28.Size = new Size(10, 46);
            // 
            // layoutControlItem87
            // 
            layoutControlItem87.Control = mtbPsaDataCdPlan;
            layoutControlItem87.Location = new Point(350, 0);
            layoutControlItem87.MinSize = new Size(54, 26);
            layoutControlItem87.Name = "layoutControlItem87";
            layoutControlItem87.OptionsTableLayoutItem.RowIndex = 2;
            layoutControlItem87.Size = new Size(123, 46);
            layoutControlItem87.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem87.Text = "План. дата сдачи";
            layoutControlItem87.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem87.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem29
            // 
            emptySpaceItem29.Location = new Point(473, 0);
            emptySpaceItem29.Name = "emptySpaceItem29";
            emptySpaceItem29.OptionsTableLayoutItem.ColumnIndex = 1;
            emptySpaceItem29.OptionsTableLayoutItem.RowIndex = 2;
            emptySpaceItem29.Size = new Size(10, 46);
            // 
            // layoutControlItem89
            // 
            layoutControlItem89.Control = mtbRzuDataCdUt;
            layoutControlItem89.Location = new Point(483, 0);
            layoutControlItem89.MinSize = new Size(54, 26);
            layoutControlItem89.Name = "layoutControlItem89";
            layoutControlItem89.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem89.OptionsTableLayoutItem.RowIndex = 3;
            layoutControlItem89.Size = new Size(200, 46);
            layoutControlItem89.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem89.Text = "План. дата сдачи Уточненная";
            layoutControlItem89.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem89.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem30
            // 
            emptySpaceItem30.Location = new Point(683, 0);
            emptySpaceItem30.Name = "emptySpaceItem30";
            emptySpaceItem30.OptionsTableLayoutItem.RowIndex = 4;
            emptySpaceItem30.Size = new Size(10, 46);
            // 
            // layoutControlItem91
            // 
            layoutControlItem91.Control = mtbRzuDataR;
            layoutControlItem91.Location = new Point(693, 0);
            layoutControlItem91.MinSize = new Size(54, 26);
            layoutControlItem91.Name = "layoutControlItem91";
            layoutControlItem91.OptionsTableLayoutItem.RowIndex = 5;
            layoutControlItem91.Size = new Size(165, 46);
            layoutControlItem91.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem91.Text = "Дата раскроя";
            layoutControlItem91.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem91.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem31
            // 
            emptySpaceItem31.Location = new Point(858, 0);
            emptySpaceItem31.Name = "emptySpaceItem31";
            emptySpaceItem31.OptionsTableLayoutItem.ColumnIndex = 1;
            emptySpaceItem31.OptionsTableLayoutItem.RowIndex = 5;
            emptySpaceItem31.Size = new Size(10, 46);
            // 
            // layoutControlItem93
            // 
            layoutControlItem93.Control = tbPszRpcNom;
            layoutControlItem93.Location = new Point(868, 0);
            layoutControlItem93.MinSize = new Size(54, 26);
            layoutControlItem93.Name = "layoutControlItem93";
            layoutControlItem93.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem93.OptionsTableLayoutItem.RowIndex = 6;
            layoutControlItem93.Size = new Size(54, 46);
            layoutControlItem93.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem93.Text = "РЦ-";
            layoutControlItem93.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem93.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem32
            // 
            emptySpaceItem32.Location = new Point(922, 0);
            emptySpaceItem32.Name = "emptySpaceItem32";
            emptySpaceItem32.OptionsTableLayoutItem.RowIndex = 7;
            emptySpaceItem32.Size = new Size(10, 46);
            // 
            // layoutControlItem95
            // 
            layoutControlItem95.Control = mtbRzuDataZeh;
            layoutControlItem95.Location = new Point(932, 0);
            layoutControlItem95.MinSize = new Size(54, 26);
            layoutControlItem95.Name = "layoutControlItem95";
            layoutControlItem95.OptionsTableLayoutItem.RowIndex = 8;
            layoutControlItem95.Size = new Size(165, 46);
            layoutControlItem95.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem95.Text = "Дата в цех";
            layoutControlItem95.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem95.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem33
            // 
            emptySpaceItem33.Location = new Point(1097, 0);
            emptySpaceItem33.Name = "emptySpaceItem33";
            emptySpaceItem33.OptionsTableLayoutItem.ColumnIndex = 1;
            emptySpaceItem33.OptionsTableLayoutItem.RowIndex = 8;
            emptySpaceItem33.Size = new Size(10, 46);
            // 
            // layoutControlItem97
            // 
            layoutControlItem97.Control = mtbRzuDataRab;
            layoutControlItem97.Location = new Point(1107, 0);
            layoutControlItem97.MinSize = new Size(54, 26);
            layoutControlItem97.Name = "layoutControlItem97";
            layoutControlItem97.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem97.OptionsTableLayoutItem.RowIndex = 9;
            layoutControlItem97.Size = new Size(165, 46);
            layoutControlItem97.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem97.Text = "Дата в работу";
            layoutControlItem97.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem97.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem34
            // 
            emptySpaceItem34.Location = new Point(1272, 0);
            emptySpaceItem34.Name = "emptySpaceItem34";
            emptySpaceItem34.OptionsTableLayoutItem.RowIndex = 10;
            emptySpaceItem34.Size = new Size(10, 46);
            // 
            // layoutControlItem99
            // 
            layoutControlItem99.Control = mtbRzuDataUp;
            layoutControlItem99.Location = new Point(1282, 0);
            layoutControlItem99.MinSize = new Size(54, 26);
            layoutControlItem99.Name = "layoutControlItem99";
            layoutControlItem99.OptionsTableLayoutItem.RowIndex = 11;
            layoutControlItem99.Size = new Size(165, 46);
            layoutControlItem99.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem99.Text = "Дата на упаковку";
            layoutControlItem99.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem99.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem35
            // 
            emptySpaceItem35.Location = new Point(1447, 0);
            emptySpaceItem35.Name = "emptySpaceItem35";
            emptySpaceItem35.OptionsTableLayoutItem.ColumnIndex = 1;
            emptySpaceItem35.OptionsTableLayoutItem.RowIndex = 11;
            emptySpaceItem35.Size = new Size(10, 46);
            // 
            // layoutControlItem101
            // 
            layoutControlItem101.Control = mtbRzuDataCd;
            layoutControlItem101.Location = new Point(1457, 0);
            layoutControlItem101.MinSize = new Size(54, 26);
            layoutControlItem101.Name = "layoutControlItem101";
            layoutControlItem101.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem101.OptionsTableLayoutItem.RowIndex = 12;
            layoutControlItem101.Size = new Size(165, 46);
            layoutControlItem101.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem101.Text = "Дата СДАНО (осн. накл.)";
            layoutControlItem101.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem101.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem36
            // 
            emptySpaceItem36.Location = new Point(1622, 0);
            emptySpaceItem36.Name = "emptySpaceItem36";
            emptySpaceItem36.OptionsTableLayoutItem.RowIndex = 13;
            emptySpaceItem36.Size = new Size(10, 46);
            // 
            // layoutControlItem103
            // 
            layoutControlItem103.Control = mtbRzuData1С;
            layoutControlItem103.Location = new Point(1632, 0);
            layoutControlItem103.MinSize = new Size(54, 26);
            layoutControlItem103.Name = "layoutControlItem103";
            layoutControlItem103.OptionsTableLayoutItem.RowIndex = 14;
            layoutControlItem103.Size = new Size(165, 46);
            layoutControlItem103.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem103.Text = "Дата 1к.т. в 1С";
            layoutControlItem103.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem103.TextSize = new Size(154, 13);
            // 
            // emptySpaceItem37
            // 
            emptySpaceItem37.Location = new Point(165, 0);
            emptySpaceItem37.Name = "emptySpaceItem37";
            emptySpaceItem37.Size = new Size(10, 46);
            // 
            // emptySpaceItem38
            // 
            emptySpaceItem38.Location = new Point(1797, 0);
            emptySpaceItem38.Name = "emptySpaceItem38";
            emptySpaceItem38.Size = new Size(10, 46);
            // 
            // layoutControlItem156
            // 
            layoutControlItem156.Control = textBoxDataZa;
            layoutControlItem156.Location = new Point(0, 0);
            layoutControlItem156.MinSize = new Size(158, 42);
            layoutControlItem156.Name = "layoutControlItem156";
            layoutControlItem156.Size = new Size(165, 46);
            layoutControlItem156.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem156.Text = "Дата планирования";
            layoutControlItem156.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem156.TextSize = new Size(154, 13);
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
            xtraTabControl1.Location = new Point(4, 244);
            xtraTabControl1.Margin = new Padding(4, 3, 4, 3);
            xtraTabControl1.Name = "xtraTabControl1";
            xtraTabControl1.ObjectName = null;
            xtraTabControl1.SelectedTabPage = RasInfo;
            xtraTabControl1.Size = new Size(1819, 679);
            xtraTabControl1.TabIndex = 3;
            xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { RasInfo, FurnInfo, WorkInfo, OtdelkaInfo, SockZadanyInfo, TabPageMgKart });
            xtraTabControl1.SelectedPageChanged += xtraTabControl1_SelectedPageChanged;
            // 
            // SockZadanyInfo
            // 
            SockZadanyInfo.Controls.Add(layoutControl2);
            SockZadanyInfo.Name = "SockZadanyInfo";
            SockZadanyInfo.Size = new Size(1817, 640);
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
            layoutControl2.Font = new Font("Arial", 10F);
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
            TextBoxKolPlanZadany.Location = new Point(232, 132);
            TextBoxKolPlanZadany.Name = "TextBoxKolPlanZadany";
            TextBoxKolPlanZadany.ObjectName = null;
            TextBoxKolPlanZadany.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxKolPlanZadany.Properties.Appearance.Options.UseFont = true;
            TextBoxKolPlanZadany.Size = new Size(50, 22);
            TextBoxKolPlanZadany.StyleController = layoutControl2;
            TextBoxKolPlanZadany.TabIndex = 24;
            // 
            // customLabel5
            // 
            customLabel5.Appearance.Font = new Font("Arial", 10F);
            customLabel5.Appearance.Options.UseFont = true;
            customLabel5.Appearance.Options.UseTextOptions = true;
            customLabel5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel5.Location = new Point(24, 132);
            customLabel5.Name = "customLabel5";
            customLabel5.Size = new Size(204, 16);
            customLabel5.StyleController = layoutControl2;
            customLabel5.TabIndex = 23;
            customLabel5.Text = "План. кол-во по заданию пар/наб.";
            customLabel5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridControlSockDefectList
            // 
            gridControlSockDefectList.Font = new Font("Arial", 10F);
            gridControlSockDefectList.Location = new Point(365, 169);
            gridControlSockDefectList.MainView = gridViewSockDefectList;
            gridControlSockDefectList.Name = "gridControlSockDefectList";
            gridControlSockDefectList.Size = new Size(621, 171);
            gridControlSockDefectList.TabIndex = 22;
            gridControlSockDefectList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSockDefectList });
            // 
            // gridViewSockDefectList
            // 
            gridViewSockDefectList.Appearance.EvenRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewSockDefectList.Appearance.FocusedRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewSockDefectList.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
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
            TextBoxKnitTotalTime.BorderStyle = BorderStyle.FixedSingle;
            TextBoxKnitTotalTime.ErrorColor = Color.Red;
            TextBoxKnitTotalTime.ErrorMessage = null;
            TextBoxKnitTotalTime.Font = new Font("Arial", 10F);
            TextBoxKnitTotalTime.Location = new Point(693, 95);
            TextBoxKnitTotalTime.Name = "TextBoxKnitTotalTime";
            TextBoxKnitTotalTime.Size = new Size(293, 21);
            TextBoxKnitTotalTime.TabIndex = 21;
            // 
            // customLabel20
            // 
            customLabel20.Appearance.Font = new Font("Arial", 10F);
            customLabel20.Appearance.Options.UseFont = true;
            customLabel20.Appearance.Options.UseTextOptions = true;
            customLabel20.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel20.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel20.Location = new Point(598, 95);
            customLabel20.Name = "customLabel20";
            customLabel20.Size = new Size(91, 32);
            customLabel20.StyleController = layoutControl2;
            customLabel20.TabIndex = 20;
            customLabel20.Text = "Время вязания\r\nзадания";
            customLabel20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridControlSockDownTimeList
            // 
            gridControlSockDownTimeList.Font = new Font("Arial", 10F);
            gridControlSockDownTimeList.Location = new Point(224, 354);
            gridControlSockDownTimeList.MainView = gridViewSockDownTimeList;
            gridControlSockDownTimeList.Name = "gridControlSockDownTimeList";
            gridControlSockDownTimeList.Size = new Size(762, 217);
            gridControlSockDownTimeList.TabIndex = 19;
            gridControlSockDownTimeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSockDownTimeList });
            // 
            // gridViewSockDownTimeList
            // 
            gridViewSockDownTimeList.Appearance.EvenRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewSockDownTimeList.Appearance.FocusedRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewSockDownTimeList.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
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
            TextBoxKolFactSmen.Location = new Point(712, 132);
            TextBoxKolFactSmen.Name = "TextBoxKolFactSmen";
            TextBoxKolFactSmen.ObjectName = null;
            TextBoxKolFactSmen.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxKolFactSmen.Properties.Appearance.Options.UseFont = true;
            TextBoxKolFactSmen.Size = new Size(50, 22);
            TextBoxKolFactSmen.StyleController = layoutControl2;
            TextBoxKolFactSmen.TabIndex = 14;
            // 
            // TextBoxKolFactDelta
            // 
            TextBoxKolFactDelta.Location = new Point(936, 132);
            TextBoxKolFactDelta.Name = "TextBoxKolFactDelta";
            TextBoxKolFactDelta.ObjectName = null;
            TextBoxKolFactDelta.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxKolFactDelta.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxKolFactDelta.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxKolFactDelta.Properties.Appearance.Options.UseBackColor = true;
            TextBoxKolFactDelta.Properties.Appearance.Options.UseFont = true;
            TextBoxKolFactDelta.Properties.Appearance.Options.UseForeColor = true;
            TextBoxKolFactDelta.Size = new Size(50, 22);
            TextBoxKolFactDelta.StyleController = layoutControl2;
            TextBoxKolFactDelta.TabIndex = 15;
            // 
            // customLabel19
            // 
            customLabel19.Appearance.Font = new Font("Arial", 10F);
            customLabel19.Appearance.Options.UseFont = true;
            customLabel19.Appearance.Options.UseTextOptions = true;
            customLabel19.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel19.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel19.Location = new Point(776, 132);
            customLabel19.Name = "customLabel19";
            customLabel19.Size = new Size(156, 16);
            customLabel19.StyleController = layoutControl2;
            customLabel19.TabIndex = 1;
            customLabel19.Text = "Разница по датчикам, шт.";
            customLabel19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBoxKolFactZadany
            // 
            TextBoxKolFactZadany.Location = new Point(473, 132);
            TextBoxKolFactZadany.Name = "TextBoxKolFactZadany";
            TextBoxKolFactZadany.ObjectName = null;
            TextBoxKolFactZadany.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxKolFactZadany.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxKolFactZadany.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxKolFactZadany.Properties.Appearance.Options.UseBackColor = true;
            TextBoxKolFactZadany.Properties.Appearance.Options.UseFont = true;
            TextBoxKolFactZadany.Properties.Appearance.Options.UseForeColor = true;
            TextBoxKolFactZadany.Size = new Size(50, 22);
            TextBoxKolFactZadany.StyleController = layoutControl2;
            TextBoxKolFactZadany.TabIndex = 13;
            // 
            // customLabel18
            // 
            customLabel18.Appearance.Font = new Font("Arial", 10F);
            customLabel18.Appearance.Options.UseFont = true;
            customLabel18.Appearance.Options.UseTextOptions = true;
            customLabel18.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel18.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel18.Location = new Point(296, 132);
            customLabel18.Name = "customLabel18";
            customLabel18.Size = new Size(173, 16);
            customLabel18.StyleController = layoutControl2;
            customLabel18.TabIndex = 1;
            customLabel18.Text = "Факт. кол-во по заданию, шт";
            customLabel18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridControlSockServiceList
            // 
            gridControlSockServiceList.Font = new Font("Arial", 10F);
            gridControlSockServiceList.Location = new Point(1014, 330);
            gridControlSockServiceList.MainView = gridView2;
            gridControlSockServiceList.Name = "gridControlSockServiceList";
            gridControlSockServiceList.Size = new Size(791, 240);
            gridControlSockServiceList.TabIndex = 18;
            gridControlSockServiceList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView2 });
            // 
            // gridView2
            // 
            gridView2.Appearance.EvenRow.BackColor = Color.FromArgb(230, 230, 250);
            gridView2.Appearance.FocusedRow.BackColor = Color.FromArgb(230, 230, 250);
            gridView2.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
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
            gridControlSockZadanySmenList.Location = new Point(1014, 45);
            gridControlSockZadanySmenList.MainView = gridView1;
            gridControlSockZadanySmenList.Name = "gridControlSockZadanySmenList";
            gridControlSockZadanySmenList.Size = new Size(791, 226);
            gridControlSockZadanySmenList.TabIndex = 7;
            gridControlSockZadanySmenList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Appearance.EvenRow.BackColor = Color.FromArgb(230, 230, 250);
            gridView1.Appearance.FocusedRow.BackColor = Color.FromArgb(230, 230, 250);
            gridView1.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
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
            TextBoxKnitEndDate.BorderStyle = BorderStyle.FixedSingle;
            TextBoxKnitEndDate.ErrorColor = Color.Red;
            TextBoxKnitEndDate.ErrorMessage = null;
            TextBoxKnitEndDate.Font = new Font("Arial", 10F);
            TextBoxKnitEndDate.Location = new Point(362, 95);
            TextBoxKnitEndDate.Multiline = true;
            TextBoxKnitEndDate.Name = "TextBoxKnitEndDate";
            TextBoxKnitEndDate.Size = new Size(216, 21);
            TextBoxKnitEndDate.TabIndex = 12;
            // 
            // TextBoxKnitStartDate
            // 
            TextBoxKnitStartDate.BorderStyle = BorderStyle.FixedSingle;
            TextBoxKnitStartDate.ErrorColor = Color.Red;
            TextBoxKnitStartDate.ErrorMessage = null;
            TextBoxKnitStartDate.Font = new Font("Arial", 10F);
            TextBoxKnitStartDate.Location = new Point(76, 95);
            TextBoxKnitStartDate.Multiline = true;
            TextBoxKnitStartDate.Name = "TextBoxKnitStartDate";
            TextBoxKnitStartDate.Size = new Size(183, 21);
            TextBoxKnitStartDate.TabIndex = 11;
            // 
            // TextBoxAreaNumber
            // 
            TextBoxAreaNumber.BorderStyle = BorderStyle.FixedSingle;
            TextBoxAreaNumber.ErrorColor = Color.Red;
            TextBoxAreaNumber.ErrorMessage = null;
            TextBoxAreaNumber.Font = new Font("Arial", 10F);
            TextBoxAreaNumber.Location = new Point(57, 70);
            TextBoxAreaNumber.Multiline = true;
            TextBoxAreaNumber.Name = "TextBoxAreaNumber";
            TextBoxAreaNumber.Size = new Size(187, 20);
            TextBoxAreaNumber.TabIndex = 9;
            // 
            // TextBoxMachineNumber
            // 
            TextBoxMachineNumber.BorderStyle = BorderStyle.FixedSingle;
            TextBoxMachineNumber.ErrorColor = Color.Red;
            TextBoxMachineNumber.ErrorMessage = null;
            TextBoxMachineNumber.Font = new Font("Arial", 10F);
            TextBoxMachineNumber.Location = new Point(332, 70);
            TextBoxMachineNumber.Multiline = true;
            TextBoxMachineNumber.Name = "TextBoxMachineNumber";
            TextBoxMachineNumber.Size = new Size(654, 20);
            TextBoxMachineNumber.TabIndex = 10;
            // 
            // TextBoxTabFio
            // 
            TextBoxTabFio.BorderStyle = BorderStyle.FixedSingle;
            TextBoxTabFio.ErrorColor = Color.Red;
            TextBoxTabFio.ErrorMessage = null;
            TextBoxTabFio.Font = new Font("Arial", 10F);
            TextBoxTabFio.Location = new Point(113, 45);
            TextBoxTabFio.Multiline = true;
            TextBoxTabFio.Name = "TextBoxTabFio";
            TextBoxTabFio.Size = new Size(873, 20);
            TextBoxTabFio.TabIndex = 8;
            // 
            // customLabel14
            // 
            customLabel14.Appearance.Font = new Font("Arial", 10F);
            customLabel14.Appearance.Options.UseFont = true;
            customLabel14.Appearance.Options.UseTextOptions = true;
            customLabel14.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel14.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel14.Location = new Point(24, 354);
            customLabel14.Name = "customLabel14";
            customLabel14.Size = new Size(196, 217);
            customLabel14.StyleController = layoutControl2;
            customLabel14.TabIndex = 1;
            customLabel14.Text = "Простои - нет данных от датчика";
            customLabel14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customLabel13
            // 
            customLabel13.Appearance.Font = new Font("Arial", 10F);
            customLabel13.Appearance.Options.UseFont = true;
            customLabel13.Appearance.Options.UseTextOptions = true;
            customLabel13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel13.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel13.Location = new Point(537, 132);
            customLabel13.Name = "customLabel13";
            customLabel13.Size = new Size(171, 16);
            customLabel13.StyleController = layoutControl2;
            customLabel13.TabIndex = 1;
            customLabel13.Text = "Факт. кол-во по сменам, шт.";
            customLabel13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customLabel12
            // 
            customLabel12.Appearance.Font = new Font("Arial", 10F);
            customLabel12.Appearance.Options.UseFont = true;
            customLabel12.Appearance.Options.UseTextOptions = true;
            customLabel12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel12.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel12.Location = new Point(293, 95);
            customLabel12.Name = "customLabel12";
            customLabel12.Size = new Size(65, 32);
            customLabel12.StyleController = layoutControl2;
            customLabel12.TabIndex = 1;
            customLabel12.Text = "Окончание \r\nвязания";
            customLabel12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customLabel11
            // 
            customLabel11.Appearance.Font = new Font("Arial", 10F);
            customLabel11.Appearance.Options.UseFont = true;
            customLabel11.Appearance.Options.UseTextOptions = true;
            customLabel11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel11.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel11.Location = new Point(24, 95);
            customLabel11.Name = "customLabel11";
            customLabel11.Size = new Size(48, 32);
            customLabel11.StyleController = layoutControl2;
            customLabel11.TabIndex = 1;
            customLabel11.Text = "Начало \r\nвязания";
            customLabel11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customLabel10
            // 
            customLabel10.Appearance.Font = new Font("Arial", 10F);
            customLabel10.Appearance.Options.UseFont = true;
            customLabel10.Appearance.Options.UseTextOptions = true;
            customLabel10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel10.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel10.Location = new Point(24, 70);
            customLabel10.Name = "customLabel10";
            customLabel10.Size = new Size(29, 16);
            customLabel10.StyleController = layoutControl2;
            customLabel10.TabIndex = 1;
            customLabel10.Text = "Зона";
            customLabel10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customLabel9
            // 
            customLabel9.Appearance.Font = new Font("Arial", 10F);
            customLabel9.Appearance.Options.UseFont = true;
            customLabel9.Appearance.Options.UseTextOptions = true;
            customLabel9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel9.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel9.Location = new Point(279, 70);
            customLabel9.Name = "customLabel9";
            customLabel9.Size = new Size(49, 16);
            customLabel9.StyleController = layoutControl2;
            customLabel9.TabIndex = 1;
            customLabel9.Text = "Автомат";
            customLabel9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customLabel8
            // 
            customLabel8.Appearance.Font = new Font("Arial", 10F);
            customLabel8.Appearance.Options.UseFont = true;
            customLabel8.Appearance.Options.UseTextOptions = true;
            customLabel8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel8.Location = new Point(24, 45);
            customLabel8.Name = "customLabel8";
            customLabel8.Size = new Size(85, 16);
            customLabel8.StyleController = layoutControl2;
            customLabel8.TabIndex = 1;
            customLabel8.Text = "Таб. № - ФИО";
            customLabel8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBoxDefectCount
            // 
            TextBoxDefectCount.Location = new Point(170, 203);
            TextBoxDefectCount.Name = "TextBoxDefectCount";
            TextBoxDefectCount.ObjectName = null;
            TextBoxDefectCount.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxDefectCount.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxDefectCount.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxDefectCount.Properties.Appearance.Options.UseBackColor = true;
            TextBoxDefectCount.Properties.Appearance.Options.UseFont = true;
            TextBoxDefectCount.Properties.Appearance.Options.UseForeColor = true;
            TextBoxDefectCount.Size = new Size(165, 22);
            TextBoxDefectCount.StyleController = layoutControl2;
            TextBoxDefectCount.TabIndex = 6;
            // 
            // TextBoxDefectWeight
            // 
            TextBoxDefectWeight.Location = new Point(170, 169);
            TextBoxDefectWeight.Name = "TextBoxDefectWeight";
            TextBoxDefectWeight.ObjectName = null;
            TextBoxDefectWeight.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            TextBoxDefectWeight.Properties.Appearance.Font = new Font("Arial", 10F);
            TextBoxDefectWeight.Properties.Appearance.ForeColor = Color.FromArgb(85, 45, 115);
            TextBoxDefectWeight.Properties.Appearance.Options.UseBackColor = true;
            TextBoxDefectWeight.Properties.Appearance.Options.UseFont = true;
            TextBoxDefectWeight.Properties.Appearance.Options.UseForeColor = true;
            TextBoxDefectWeight.Size = new Size(165, 22);
            TextBoxDefectWeight.StyleController = layoutControl2;
            TextBoxDefectWeight.TabIndex = 16;
            // 
            // customLabel7
            // 
            customLabel7.Appearance.Font = new Font("Arial", 10F);
            customLabel7.Appearance.Options.UseFont = true;
            customLabel7.Appearance.Options.UseTextOptions = true;
            customLabel7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel7.Location = new Point(24, 169);
            customLabel7.Name = "customLabel7";
            customLabel7.Size = new Size(142, 171);
            customLabel7.StyleController = layoutControl2;
            customLabel7.TabIndex = 1;
            customLabel7.Text = "Брак";
            customLabel7.TextAlign = ContentAlignment.MiddleLeft;
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
            buttonImageOptions11.Image = (Image)resources.GetObject("buttonImageOptions11.Image");
            layoutControlGroup6.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Печать", true, buttonImageOptions11, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup6.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem26, layoutControlItem27, layoutControlItem29, layoutControlItem30, layoutControlItem31, emptySpaceItem8, layoutControlItem32, layoutControlItem33, layoutControlItem34, layoutControlItem35, emptySpaceItem9, layoutControlItem36, layoutControlItem37, emptySpaceItem4, layoutControlItem12, layoutControlItem28, layoutControlItem41, layoutControlItem42, emptySpaceItem6, layoutControlItem38, layoutControlItem40, emptySpaceItem7, layoutControlItem43, layoutControlItem44, layoutControlItem45, emptySpaceItem13, emptySpaceItem14, simpleSeparator2, simpleSeparator4, simpleSeparator5, simpleSeparator6, layoutControlItem24, layoutControlItem19, layoutControlItem25, layoutControlItem39, emptySpaceItem11, splitterItem1, layoutControlItem13, layoutControlItem14, emptySpaceItem12, emptySpaceItem10, emptySpaceItem5 });
            layoutControlGroup6.Location = new Point(0, 0);
            layoutControlGroup6.Name = "layoutControlGroup3";
            layoutControlGroup6.Size = new Size(990, 575);
            layoutControlGroup6.Text = "Вязание";
            layoutControlGroup6.CustomButtonClick += layoutControlGroup6_CustomButtonClick;
            // 
            // layoutControlItem26
            // 
            layoutControlItem26.Control = customLabel8;
            layoutControlItem26.Location = new Point(0, 0);
            layoutControlItem26.Name = "layoutControlItem10";
            layoutControlItem26.Size = new Size(89, 24);
            layoutControlItem26.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            layoutControlItem27.Control = customLabel11;
            layoutControlItem27.Location = new Point(0, 50);
            layoutControlItem27.Name = "layoutControlItem20";
            layoutControlItem27.Size = new Size(52, 36);
            layoutControlItem27.TextVisible = false;
            // 
            // layoutControlItem29
            // 
            layoutControlItem29.Control = customLabel14;
            layoutControlItem29.Location = new Point(0, 309);
            layoutControlItem29.MinSize = new Size(200, 20);
            layoutControlItem29.Name = "layoutControlItem23";
            layoutControlItem29.Size = new Size(200, 221);
            layoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem29.TextVisible = false;
            // 
            // layoutControlItem30
            // 
            layoutControlItem30.Control = customLabel10;
            layoutControlItem30.Location = new Point(0, 25);
            layoutControlItem30.Name = "layoutControlItem19";
            layoutControlItem30.Size = new Size(33, 24);
            layoutControlItem30.TextVisible = false;
            // 
            // layoutControlItem31
            // 
            layoutControlItem31.Control = TextBoxAreaNumber;
            layoutControlItem31.Location = new Point(33, 25);
            layoutControlItem31.MinSize = new Size(24, 24);
            layoutControlItem31.Name = "layoutControlItem26";
            layoutControlItem31.Size = new Size(191, 24);
            layoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem31.TextVisible = false;
            // 
            // emptySpaceItem8
            // 
            emptySpaceItem8.Location = new Point(224, 25);
            emptySpaceItem8.Name = "emptySpaceItem6";
            emptySpaceItem8.Size = new Size(31, 24);
            // 
            // layoutControlItem32
            // 
            layoutControlItem32.Control = TextBoxTabFio;
            layoutControlItem32.Location = new Point(89, 0);
            layoutControlItem32.MinSize = new Size(24, 24);
            layoutControlItem32.Name = "layoutControlItem24";
            layoutControlItem32.Size = new Size(877, 24);
            layoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem32.TextVisible = false;
            // 
            // layoutControlItem33
            // 
            layoutControlItem33.Control = customLabel9;
            layoutControlItem33.Location = new Point(255, 25);
            layoutControlItem33.Name = "layoutControlItem18";
            layoutControlItem33.Size = new Size(53, 24);
            layoutControlItem33.TextVisible = false;
            // 
            // layoutControlItem34
            // 
            layoutControlItem34.Control = TextBoxMachineNumber;
            layoutControlItem34.Location = new Point(308, 25);
            layoutControlItem34.MinSize = new Size(24, 24);
            layoutControlItem34.Name = "layoutControlItem25";
            layoutControlItem34.Size = new Size(658, 24);
            layoutControlItem34.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem34.TextVisible = false;
            // 
            // layoutControlItem35
            // 
            layoutControlItem35.Control = TextBoxKnitStartDate;
            layoutControlItem35.Location = new Point(52, 50);
            layoutControlItem35.MinSize = new Size(24, 24);
            layoutControlItem35.Name = "layoutControlItem27";
            layoutControlItem35.Size = new Size(187, 25);
            layoutControlItem35.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem35.TextVisible = false;
            // 
            // emptySpaceItem9
            // 
            emptySpaceItem9.Location = new Point(239, 50);
            emptySpaceItem9.Name = "emptySpaceItem7";
            emptySpaceItem9.Size = new Size(30, 36);
            // 
            // layoutControlItem36
            // 
            layoutControlItem36.Control = customLabel12;
            layoutControlItem36.Location = new Point(269, 50);
            layoutControlItem36.Name = "layoutControlItem21";
            layoutControlItem36.Size = new Size(69, 36);
            layoutControlItem36.TextVisible = false;
            // 
            // layoutControlItem37
            // 
            layoutControlItem37.Control = TextBoxKnitEndDate;
            layoutControlItem37.Location = new Point(338, 50);
            layoutControlItem37.MinSize = new Size(24, 24);
            layoutControlItem37.Name = "layoutControlItem28";
            layoutControlItem37.Size = new Size(220, 25);
            layoutControlItem37.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem37.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.Location = new Point(52, 75);
            emptySpaceItem4.Name = "item0";
            emptySpaceItem4.Size = new Size(187, 11);
            // 
            // layoutControlItem12
            // 
            layoutControlItem12.Control = customLabel18;
            layoutControlItem12.Location = new Point(272, 87);
            layoutControlItem12.Name = "item2";
            layoutControlItem12.Size = new Size(177, 36);
            layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem28
            // 
            layoutControlItem28.Control = customLabel13;
            layoutControlItem28.Location = new Point(513, 87);
            layoutControlItem28.Name = "layoutControlItem22";
            layoutControlItem28.Size = new Size(175, 36);
            layoutControlItem28.TextVisible = false;
            // 
            // layoutControlItem41
            // 
            layoutControlItem41.Control = customLabel19;
            layoutControlItem41.Location = new Point(752, 87);
            layoutControlItem41.Name = "layoutControlItem41";
            layoutControlItem41.Size = new Size(160, 36);
            layoutControlItem41.TextVisible = false;
            // 
            // layoutControlItem42
            // 
            layoutControlItem42.Control = TextBoxKolFactDelta;
            layoutControlItem42.Location = new Point(912, 87);
            layoutControlItem42.MinSize = new Size(54, 26);
            layoutControlItem42.Name = "layoutControlItem42";
            layoutControlItem42.Size = new Size(54, 36);
            layoutControlItem42.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem42.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            emptySpaceItem6.Location = new Point(742, 87);
            emptySpaceItem6.Name = "item5";
            emptySpaceItem6.Size = new Size(10, 36);
            // 
            // layoutControlItem38
            // 
            layoutControlItem38.Control = TextBoxKolFactSmen;
            layoutControlItem38.Location = new Point(688, 87);
            layoutControlItem38.MinSize = new Size(54, 26);
            layoutControlItem38.Name = "layoutControlItem38";
            layoutControlItem38.Size = new Size(54, 36);
            layoutControlItem38.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem38.TextVisible = false;
            // 
            // layoutControlItem40
            // 
            layoutControlItem40.Control = TextBoxKolFactZadany;
            layoutControlItem40.Location = new Point(449, 87);
            layoutControlItem40.MinSize = new Size(54, 26);
            layoutControlItem40.Name = "layoutControlItem40";
            layoutControlItem40.Size = new Size(54, 26);
            layoutControlItem40.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem40.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            emptySpaceItem7.Location = new Point(338, 75);
            emptySpaceItem7.Name = "item1";
            emptySpaceItem7.Size = new Size(220, 11);
            // 
            // layoutControlItem43
            // 
            layoutControlItem43.Control = gridControlSockDownTimeList;
            layoutControlItem43.Location = new Point(200, 309);
            layoutControlItem43.Name = "layoutControlItem43";
            layoutControlItem43.Size = new Size(766, 221);
            layoutControlItem43.TextVisible = false;
            // 
            // layoutControlItem44
            // 
            layoutControlItem44.Control = customLabel20;
            layoutControlItem44.Location = new Point(574, 50);
            layoutControlItem44.Name = "layoutControlItem44";
            layoutControlItem44.Size = new Size(95, 36);
            layoutControlItem44.TextVisible = false;
            // 
            // layoutControlItem45
            // 
            layoutControlItem45.Control = TextBoxKnitTotalTime;
            layoutControlItem45.Location = new Point(669, 50);
            layoutControlItem45.MinSize = new Size(24, 24);
            layoutControlItem45.Name = "layoutControlItem45";
            layoutControlItem45.Size = new Size(297, 25);
            layoutControlItem45.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem45.TextVisible = false;
            // 
            // emptySpaceItem13
            // 
            emptySpaceItem13.Location = new Point(558, 50);
            emptySpaceItem13.Name = "emptySpaceItem13";
            emptySpaceItem13.Size = new Size(16, 36);
            // 
            // emptySpaceItem14
            // 
            emptySpaceItem14.Location = new Point(669, 75);
            emptySpaceItem14.Name = "emptySpaceItem14";
            emptySpaceItem14.Size = new Size(297, 11);
            // 
            // simpleSeparator2
            // 
            simpleSeparator2.Location = new Point(0, 24);
            simpleSeparator2.Name = "simpleSeparator2";
            simpleSeparator2.Size = new Size(966, 1);
            // 
            // simpleSeparator4
            // 
            simpleSeparator4.Location = new Point(0, 49);
            simpleSeparator4.Name = "simpleSeparator4";
            simpleSeparator4.Size = new Size(966, 1);
            // 
            // simpleSeparator5
            // 
            simpleSeparator5.Location = new Point(0, 86);
            simpleSeparator5.Name = "simpleSeparator5";
            simpleSeparator5.Size = new Size(966, 1);
            // 
            // simpleSeparator6
            // 
            simpleSeparator6.Location = new Point(0, 123);
            simpleSeparator6.Name = "simpleSeparator6";
            simpleSeparator6.Size = new Size(966, 1);
            // 
            // layoutControlItem24
            // 
            layoutControlItem24.Control = TextBoxDefectWeight;
            layoutControlItem24.Location = new Point(146, 124);
            layoutControlItem24.MinSize = new Size(54, 26);
            layoutControlItem24.Name = "layoutControlItem16";
            layoutControlItem24.Size = new Size(195, 34);
            layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem24.Text = "кг";
            layoutControlItem24.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem24.TextSize = new Size(14, 13);
            // 
            // layoutControlItem19
            // 
            layoutControlItem19.Control = customLabel7;
            layoutControlItem19.Location = new Point(0, 124);
            layoutControlItem19.MinSize = new Size(24, 24);
            layoutControlItem19.Name = "layoutControlItem9";
            layoutControlItem19.Size = new Size(146, 175);
            layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            layoutControlItem25.Control = TextBoxDefectCount;
            layoutControlItem25.Location = new Point(146, 158);
            layoutControlItem25.MinSize = new Size(54, 26);
            layoutControlItem25.Name = "layoutControlItem17";
            layoutControlItem25.Size = new Size(195, 34);
            layoutControlItem25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem25.Text = "шт";
            layoutControlItem25.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem25.TextSize = new Size(14, 13);
            // 
            // layoutControlItem39
            // 
            layoutControlItem39.Control = gridControlSockDefectList;
            layoutControlItem39.Location = new Point(341, 124);
            layoutControlItem39.Name = "layoutControlItem39";
            layoutControlItem39.Size = new Size(625, 175);
            layoutControlItem39.TextVisible = false;
            // 
            // emptySpaceItem11
            // 
            emptySpaceItem11.Location = new Point(146, 192);
            emptySpaceItem11.Name = "emptySpaceItem11";
            emptySpaceItem11.Size = new Size(195, 107);
            // 
            // splitterItem1
            // 
            splitterItem1.Location = new Point(0, 299);
            splitterItem1.Name = "splitterItem1";
            splitterItem1.Size = new Size(966, 10);
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = customLabel5;
            layoutControlItem13.Location = new Point(0, 87);
            layoutControlItem13.Name = "layoutControlItem13";
            layoutControlItem13.Size = new Size(208, 36);
            layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            layoutControlItem14.Control = TextBoxKolPlanZadany;
            layoutControlItem14.Location = new Point(208, 87);
            layoutControlItem14.Name = "layoutControlItem14";
            layoutControlItem14.Size = new Size(54, 36);
            layoutControlItem14.TextVisible = false;
            // 
            // emptySpaceItem12
            // 
            emptySpaceItem12.Location = new Point(262, 87);
            emptySpaceItem12.Name = "emptySpaceItem12";
            emptySpaceItem12.Size = new Size(10, 36);
            // 
            // emptySpaceItem10
            // 
            emptySpaceItem10.Location = new Point(503, 87);
            emptySpaceItem10.Name = "emptySpaceItem10";
            emptySpaceItem10.Size = new Size(10, 36);
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.Location = new Point(449, 113);
            emptySpaceItem5.Name = "emptySpaceItem5";
            emptySpaceItem5.Size = new Size(54, 10);
            // 
            // simpleSeparator3
            // 
            simpleSeparator3.Location = new Point(990, 574);
            simpleSeparator3.Name = "simpleSeparator3";
            simpleSeparator3.Size = new Size(819, 1);
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
            layoutControlGroup4.Location = new Point(990, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Size = new Size(819, 275);
            layoutControlGroup4.Text = "Смены";
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.Control = gridControlSockZadanySmenList;
            layoutControlItem10.Location = new Point(0, 0);
            layoutControlItem10.Name = "item4";
            layoutControlItem10.Size = new Size(795, 230);
            layoutControlItem10.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem11 });
            layoutControlGroup7.Location = new Point(990, 285);
            layoutControlGroup7.Name = "layoutControlGroup7";
            layoutControlGroup7.Size = new Size(819, 289);
            layoutControlGroup7.Text = "Обслуживание оборудования";
            // 
            // layoutControlItem11
            // 
            layoutControlItem11.Control = gridControlSockServiceList;
            layoutControlItem11.Location = new Point(0, 0);
            layoutControlItem11.Name = "item6";
            layoutControlItem11.Size = new Size(795, 244);
            layoutControlItem11.TextVisible = false;
            // 
            // splitterItem2
            // 
            splitterItem2.Location = new Point(990, 275);
            splitterItem2.Name = "splitterItem2";
            splitterItem2.Size = new Size(819, 10);
            // 
            // TabPageMgKart
            // 
            TabPageMgKart.Controls.Add(layoutControl8);
            TabPageMgKart.Name = "TabPageMgKart";
            TabPageMgKart.Size = new Size(1817, 640);
            TabPageMgKart.Text = "КАРТА РАСКРОЯ";
            // 
            // layoutControl8
            // 
            layoutControl8.Controls.Add(gridControlNastilGroupView);
            layoutControl8.Controls.Add(gridControlNastilList);
            layoutControl8.Dock = DockStyle.Fill;
            layoutControl8.Font = new Font("Arial", 10F);
            layoutControl8.Location = new Point(0, 0);
            layoutControl8.Name = "layoutControl8";
            layoutControl8.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(1194, 590, 650, 400);
            layoutControl8.Root = layoutControlGroup25;
            layoutControl8.Size = new Size(1817, 640);
            layoutControl8.TabIndex = 0;
            layoutControl8.Text = "layoutControl8";
            // 
            // gridControlNastilGroupView
            // 
            gridControlNastilGroupView.Font = new Font("Arial", 10F);
            gridControlNastilGroupView.Location = new Point(12, 391);
            gridControlNastilGroupView.MainView = gridViewNastilGroupView;
            gridControlNastilGroupView.Name = "gridControlNastilGroupView";
            gridControlNastilGroupView.Size = new Size(1793, 237);
            gridControlNastilGroupView.TabIndex = 5;
            gridControlNastilGroupView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewNastilGroupView });
            // 
            // gridViewNastilGroupView
            // 
            gridViewNastilGroupView.Appearance.EvenRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewNastilGroupView.Appearance.FocusedRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewNastilGroupView.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridViewNastilGroupView.Appearance.FocusedRow.Options.UseFont = true;
            gridViewNastilGroupView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridNastilGroupViewColumnMgKart, gridNastilGroupViewColumnKodPr, gridNastilGroupViewColumnTArticul, gridNastilGroupViewColumnSeb, gridNastilGroupViewColumnVN, gridNastilGroupViewColumnKol, gridNastilGroupViewColumnKolOnr, gridNastilGroupViewColumnKolPog, gridNastilGroupViewColumnSumSeb, gridNastilGroupViewColumnSumRash, gridNastilGroupViewColumnSumVetM });
            gridViewNastilGroupView.GridControl = gridControlNastilGroupView;
            gridViewNastilGroupView.Name = "gridViewNastilGroupView";
            gridViewNastilGroupView.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // gridNastilGroupViewColumnMgKart
            // 
            gridNastilGroupViewColumnMgKart.Caption = "mg_kart";
            gridNastilGroupViewColumnMgKart.Name = "gridNastilGroupViewColumnMgKart";
            // 
            // gridNastilGroupViewColumnKodPr
            // 
            gridNastilGroupViewColumnKodPr.Caption = "Код ткани";
            gridNastilGroupViewColumnKodPr.Name = "gridNastilGroupViewColumnKodPr";
            gridNastilGroupViewColumnKodPr.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnKodPr.Visible = true;
            gridNastilGroupViewColumnKodPr.VisibleIndex = 0;
            gridNastilGroupViewColumnKodPr.Width = 100;
            // 
            // gridNastilGroupViewColumnTArticul
            // 
            gridNastilGroupViewColumnTArticul.Caption = "Ткань";
            gridNastilGroupViewColumnTArticul.Name = "gridNastilGroupViewColumnTArticul";
            gridNastilGroupViewColumnTArticul.Visible = true;
            gridNastilGroupViewColumnTArticul.VisibleIndex = 1;
            gridNastilGroupViewColumnTArticul.Width = 943;
            // 
            // gridNastilGroupViewColumnSeb
            // 
            gridNastilGroupViewColumnSeb.Caption = "Себ-ть";
            gridNastilGroupViewColumnSeb.Name = "gridNastilGroupViewColumnSeb";
            gridNastilGroupViewColumnSeb.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnSeb.Visible = true;
            gridNastilGroupViewColumnSeb.VisibleIndex = 2;
            gridNastilGroupViewColumnSeb.Width = 100;
            // 
            // gridNastilGroupViewColumnVN
            // 
            gridNastilGroupViewColumnVN.Caption = "V";
            gridNastilGroupViewColumnVN.Name = "gridNastilGroupViewColumnVN";
            gridNastilGroupViewColumnVN.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnVN.Visible = true;
            gridNastilGroupViewColumnVN.VisibleIndex = 3;
            gridNastilGroupViewColumnVN.Width = 50;
            // 
            // gridNastilGroupViewColumnKol
            // 
            gridNastilGroupViewColumnKol.Caption = "Кол. (м)";
            gridNastilGroupViewColumnKol.Name = "gridNastilGroupViewColumnKol";
            gridNastilGroupViewColumnKol.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnKol.Visible = true;
            gridNastilGroupViewColumnKol.VisibleIndex = 4;
            gridNastilGroupViewColumnKol.Width = 100;
            // 
            // gridNastilGroupViewColumnKolOnr
            // 
            gridNastilGroupViewColumnKolOnr.Caption = "Нерац. ост.";
            gridNastilGroupViewColumnKolOnr.Name = "gridNastilGroupViewColumnKolOnr";
            gridNastilGroupViewColumnKolOnr.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnKolOnr.Visible = true;
            gridNastilGroupViewColumnKolOnr.VisibleIndex = 5;
            gridNastilGroupViewColumnKolOnr.Width = 100;
            // 
            // gridNastilGroupViewColumnKolPog
            // 
            gridNastilGroupViewColumnKolPog.Caption = "Погр. +/-";
            gridNastilGroupViewColumnKolPog.Name = "gridNastilGroupViewColumnKolPog";
            gridNastilGroupViewColumnKolPog.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnKolPog.Visible = true;
            gridNastilGroupViewColumnKolPog.VisibleIndex = 6;
            gridNastilGroupViewColumnKolPog.Width = 100;
            // 
            // gridNastilGroupViewColumnSumSeb
            // 
            gridNastilGroupViewColumnSumSeb.Caption = "Сумма по ткани";
            gridNastilGroupViewColumnSumSeb.Name = "gridNastilGroupViewColumnSumSeb";
            gridNastilGroupViewColumnSumSeb.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnSumSeb.Visible = true;
            gridNastilGroupViewColumnSumSeb.VisibleIndex = 7;
            gridNastilGroupViewColumnSumSeb.Width = 100;
            // 
            // gridNastilGroupViewColumnSumRash
            // 
            gridNastilGroupViewColumnSumRash.Caption = "Расход ткани";
            gridNastilGroupViewColumnSumRash.Name = "gridNastilGroupViewColumnSumRash";
            gridNastilGroupViewColumnSumRash.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnSumRash.Visible = true;
            gridNastilGroupViewColumnSumRash.VisibleIndex = 8;
            gridNastilGroupViewColumnSumRash.Width = 100;
            // 
            // gridNastilGroupViewColumnSumVetM
            // 
            gridNastilGroupViewColumnSumVetM.Caption = "Кол-во ветоши";
            gridNastilGroupViewColumnSumVetM.Name = "gridNastilGroupViewColumnSumVetM";
            gridNastilGroupViewColumnSumVetM.OptionsColumn.FixedWidth = true;
            gridNastilGroupViewColumnSumVetM.Visible = true;
            gridNastilGroupViewColumnSumVetM.VisibleIndex = 9;
            gridNastilGroupViewColumnSumVetM.Width = 100;
            // 
            // gridControlNastilList
            // 
            gridControlNastilList.Font = new Font("Arial", 10F);
            gridControlNastilList.Location = new Point(12, 12);
            gridControlNastilList.MainView = gridViewNastilList;
            gridControlNastilList.Name = "gridControlNastilList";
            gridControlNastilList.Size = new Size(1793, 365);
            gridControlNastilList.TabIndex = 4;
            gridControlNastilList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewNastilList });
            gridControlNastilList.Click += gridControlNastilList_Click;
            // 
            // gridViewNastilList
            // 
            gridViewNastilList.Appearance.EvenRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewNastilList.Appearance.FocusedRow.BackColor = Color.FromArgb(200, 225, 255);
            gridViewNastilList.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridViewNastilList.Appearance.FocusedRow.Options.UseFont = true;
            gridViewNastilList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridNastilListColumnMgKart, gridNastilListColumnNakl, gridNastilListColumnDateR, gridNastilListColumnTArticul, gridNastilListColumnSebTM, gridNastilListColumnTkanType, gridNastilListColumnRazr, gridNastilListColumnVN, gridNastilListColumnKol, gridNastilListColumnKolOnr, gridNastilListColumnKolOr, gridNastilListColumnKp, gridNastilListColumnKolPog, gridNastilListColumnKolO, gridNastilListColumnTkanExpense, gridNastilListColumnChyl, gridNastilListColumnTab1, gridNastilListColumnTab2, gridNastilListColumnTab3, gridNastilListColumnFio1, gridNastilListColumnFio2, gridNastilListColumnFio3, gridNastilListColumnProzVipad });
            gridViewNastilList.GridControl = gridControlNastilList;
            gridViewNastilList.Name = "gridViewNastilList";
            gridViewNastilList.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // gridNastilListColumnMgKart
            // 
            gridNastilListColumnMgKart.Caption = "mg_kart";
            gridNastilListColumnMgKart.Name = "gridNastilListColumnMgKart";
            // 
            // gridNastilListColumnNakl
            // 
            gridNastilListColumnNakl.Caption = "Карта";
            gridNastilListColumnNakl.Name = "gridNastilListColumnNakl";
            gridNastilListColumnNakl.OptionsColumn.FixedWidth = true;
            gridNastilListColumnNakl.Visible = true;
            gridNastilListColumnNakl.VisibleIndex = 0;
            gridNastilListColumnNakl.Width = 55;
            // 
            // gridNastilListColumnDateR
            // 
            gridNastilListColumnDateR.Caption = "Дата";
            gridNastilListColumnDateR.Name = "gridNastilListColumnDateR";
            gridNastilListColumnDateR.OptionsColumn.FixedWidth = true;
            gridNastilListColumnDateR.Visible = true;
            gridNastilListColumnDateR.VisibleIndex = 1;
            // 
            // gridNastilListColumnTArticul
            // 
            gridNastilListColumnTArticul.Caption = "Ткань";
            gridNastilListColumnTArticul.Name = "gridNastilListColumnTArticul";
            gridNastilListColumnTArticul.Visible = true;
            gridNastilListColumnTArticul.VisibleIndex = 2;
            gridNastilListColumnTArticul.Width = 293;
            // 
            // gridNastilListColumnSebTM
            // 
            gridNastilListColumnSebTM.Caption = "Себ-ть";
            gridNastilListColumnSebTM.Name = "gridNastilListColumnSebTM";
            gridNastilListColumnSebTM.OptionsColumn.FixedWidth = true;
            gridNastilListColumnSebTM.Visible = true;
            gridNastilListColumnSebTM.VisibleIndex = 3;
            gridNastilListColumnSebTM.Width = 55;
            // 
            // gridNastilListColumnTkanType
            // 
            gridNastilListColumnTkanType.Caption = "Вид тк.";
            gridNastilListColumnTkanType.Name = "gridNastilListColumnTkanType";
            gridNastilListColumnTkanType.OptionsColumn.FixedWidth = true;
            gridNastilListColumnTkanType.Visible = true;
            gridNastilListColumnTkanType.VisibleIndex = 4;
            // 
            // gridNastilListColumnRazr
            // 
            gridNastilListColumnRazr.Caption = "Раз-д";
            gridNastilListColumnRazr.Name = "gridNastilListColumnRazr";
            gridNastilListColumnRazr.OptionsColumn.FixedWidth = true;
            gridNastilListColumnRazr.Visible = true;
            gridNastilListColumnRazr.VisibleIndex = 5;
            gridNastilListColumnRazr.Width = 50;
            // 
            // gridNastilListColumnVN
            // 
            gridNastilListColumnVN.Caption = "V";
            gridNastilListColumnVN.Name = "gridNastilListColumnVN";
            gridNastilListColumnVN.OptionsColumn.FixedWidth = true;
            gridNastilListColumnVN.Visible = true;
            gridNastilListColumnVN.VisibleIndex = 6;
            gridNastilListColumnVN.Width = 30;
            // 
            // gridNastilListColumnKol
            // 
            gridNastilListColumnKol.Caption = "Кол (м)";
            gridNastilListColumnKol.Name = "gridNastilListColumnKol";
            gridNastilListColumnKol.OptionsColumn.FixedWidth = true;
            gridNastilListColumnKol.Visible = true;
            gridNastilListColumnKol.VisibleIndex = 7;
            gridNastilListColumnKol.Width = 60;
            // 
            // gridNastilListColumnKolOnr
            // 
            gridNastilListColumnKolOnr.Caption = "Нерац. ост.";
            gridNastilListColumnKolOnr.Name = "gridNastilListColumnKolOnr";
            gridNastilListColumnKolOnr.OptionsColumn.FixedWidth = true;
            gridNastilListColumnKolOnr.Visible = true;
            gridNastilListColumnKolOnr.VisibleIndex = 8;
            gridNastilListColumnKolOnr.Width = 60;
            // 
            // gridNastilListColumnKolOr
            // 
            gridNastilListColumnKolOr.Caption = "Рац. ост.";
            gridNastilListColumnKolOr.Name = "gridNastilListColumnKolOr";
            gridNastilListColumnKolOr.OptionsColumn.FixedWidth = true;
            gridNastilListColumnKolOr.Visible = true;
            gridNastilListColumnKolOr.VisibleIndex = 9;
            gridNastilListColumnKolOr.Width = 60;
            // 
            // gridNastilListColumnKp
            // 
            gridNastilListColumnKp.Caption = "К/П";
            gridNastilListColumnKp.Name = "gridNastilListColumnKp";
            gridNastilListColumnKp.OptionsColumn.FixedWidth = true;
            gridNastilListColumnKp.Visible = true;
            gridNastilListColumnKp.VisibleIndex = 10;
            gridNastilListColumnKp.Width = 40;
            // 
            // gridNastilListColumnKolPog
            // 
            gridNastilListColumnKolPog.Caption = "Погр. +/-";
            gridNastilListColumnKolPog.Name = "gridNastilListColumnKolPog";
            gridNastilListColumnKolPog.OptionsColumn.FixedWidth = true;
            gridNastilListColumnKolPog.Visible = true;
            gridNastilListColumnKolPog.VisibleIndex = 11;
            gridNastilListColumnKolPog.Width = 60;
            // 
            // gridNastilListColumnKolO
            // 
            gridNastilListColumnKolO.Caption = "Конц. ост.";
            gridNastilListColumnKolO.Name = "gridNastilListColumnKolO";
            gridNastilListColumnKolO.OptionsColumn.FixedWidth = true;
            gridNastilListColumnKolO.Visible = true;
            gridNastilListColumnKolO.VisibleIndex = 12;
            gridNastilListColumnKolO.Width = 60;
            // 
            // gridNastilListColumnTkanExpense
            // 
            gridNastilListColumnTkanExpense.Caption = "Расход (м)";
            gridNastilListColumnTkanExpense.Name = "gridNastilListColumnTkanExpense";
            gridNastilListColumnTkanExpense.OptionsColumn.FixedWidth = true;
            gridNastilListColumnTkanExpense.Visible = true;
            gridNastilListColumnTkanExpense.VisibleIndex = 13;
            gridNastilListColumnTkanExpense.Width = 60;
            // 
            // gridNastilListColumnChyl
            // 
            gridNastilListColumnChyl.Caption = "Чул. (м)";
            gridNastilListColumnChyl.Name = "gridNastilListColumnChyl";
            gridNastilListColumnChyl.OptionsColumn.FixedWidth = true;
            gridNastilListColumnChyl.Visible = true;
            gridNastilListColumnChyl.VisibleIndex = 14;
            gridNastilListColumnChyl.Width = 60;
            // 
            // gridNastilListColumnTab1
            // 
            gridNastilListColumnTab1.Caption = "Таб.1";
            gridNastilListColumnTab1.Name = "gridNastilListColumnTab1";
            gridNastilListColumnTab1.OptionsColumn.FixedWidth = true;
            gridNastilListColumnTab1.Visible = true;
            gridNastilListColumnTab1.VisibleIndex = 15;
            gridNastilListColumnTab1.Width = 55;
            // 
            // gridNastilListColumnTab2
            // 
            gridNastilListColumnTab2.Caption = "Таб.2";
            gridNastilListColumnTab2.Name = "gridNastilListColumnTab2";
            gridNastilListColumnTab2.OptionsColumn.FixedWidth = true;
            gridNastilListColumnTab2.Visible = true;
            gridNastilListColumnTab2.VisibleIndex = 16;
            gridNastilListColumnTab2.Width = 55;
            // 
            // gridNastilListColumnTab3
            // 
            gridNastilListColumnTab3.Caption = "Таб.3";
            gridNastilListColumnTab3.Name = "gridNastilListColumnTab3";
            gridNastilListColumnTab3.OptionsColumn.FixedWidth = true;
            gridNastilListColumnTab3.Visible = true;
            gridNastilListColumnTab3.VisibleIndex = 17;
            gridNastilListColumnTab3.Width = 55;
            // 
            // gridNastilListColumnFio1
            // 
            gridNastilListColumnFio1.Caption = "ФИО1";
            gridNastilListColumnFio1.Name = "gridNastilListColumnFio1";
            gridNastilListColumnFio1.OptionsColumn.FixedWidth = true;
            gridNastilListColumnFio1.Visible = true;
            gridNastilListColumnFio1.VisibleIndex = 18;
            gridNastilListColumnFio1.Width = 150;
            // 
            // gridNastilListColumnFio2
            // 
            gridNastilListColumnFio2.Caption = "ФИО2";
            gridNastilListColumnFio2.Name = "gridNastilListColumnFio2";
            gridNastilListColumnFio2.OptionsColumn.FixedWidth = true;
            gridNastilListColumnFio2.Visible = true;
            gridNastilListColumnFio2.VisibleIndex = 19;
            gridNastilListColumnFio2.Width = 150;
            // 
            // gridNastilListColumnFio3
            // 
            gridNastilListColumnFio3.Caption = "ФИО3";
            gridNastilListColumnFio3.Name = "gridNastilListColumnFio3";
            gridNastilListColumnFio3.OptionsColumn.FixedWidth = true;
            gridNastilListColumnFio3.Visible = true;
            gridNastilListColumnFio3.VisibleIndex = 20;
            gridNastilListColumnFio3.Width = 150;
            // 
            // gridNastilListColumnProzVipad
            // 
            gridNastilListColumnProzVipad.Caption = "% выпадов";
            gridNastilListColumnProzVipad.Name = "gridNastilListColumnProzVipad";
            gridNastilListColumnProzVipad.OptionsColumn.FixedWidth = true;
            gridNastilListColumnProzVipad.Visible = true;
            gridNastilListColumnProzVipad.VisibleIndex = 21;
            gridNastilListColumnProzVipad.Width = 70;
            // 
            // layoutControlGroup25
            // 
            layoutControlGroup25.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup25.GroupBordersVisible = false;
            layoutControlGroup25.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem143, layoutControlItem145, splitterItem3 });
            layoutControlGroup25.Name = "Root";
            layoutControlGroup25.Size = new Size(1817, 640);
            layoutControlGroup25.TextVisible = false;
            // 
            // layoutControlItem143
            // 
            layoutControlItem143.Control = gridControlNastilList;
            layoutControlItem143.Location = new Point(0, 0);
            layoutControlItem143.Name = "layoutControlItem143";
            layoutControlItem143.Size = new Size(1797, 369);
            layoutControlItem143.TextVisible = false;
            // 
            // layoutControlItem145
            // 
            layoutControlItem145.Control = gridControlNastilGroupView;
            layoutControlItem145.Location = new Point(0, 379);
            layoutControlItem145.Name = "layoutControlItem145";
            layoutControlItem145.Size = new Size(1797, 241);
            layoutControlItem145.TextVisible = false;
            // 
            // splitterItem3
            // 
            splitterItem3.Location = new Point(0, 369);
            splitterItem3.Name = "splitterItem3";
            splitterItem3.Size = new Size(1797, 10);
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
            // layoutControlGroup5
            // 
            layoutControlGroup5.Location = new Point(0, 0);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Size = new Size(1840, 132);
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(buttonVshivkiPrint);
            layoutControl1.Controls.Add(textBoxRzId);
            layoutControl1.Controls.Add(TextBoxRecomendZad);
            layoutControl1.Controls.Add(TextBoxRecomendNom);
            layoutControl1.Controls.Add(tbRzuMgZakr);
            layoutControl1.Controls.Add(pbEskiz);
            layoutControl1.Controls.Add(tbSostPoln);
            layoutControl1.Controls.Add(tbRzuMod);
            layoutControl1.Controls.Add(tbArtGrup);
            layoutControl1.Controls.Add(tbPsaKombOsn);
            layoutControl1.Controls.Add(tbPsaPrn);
            layoutControl1.Controls.Add(tbPsaKodZv2);
            layoutControl1.Controls.Add(tbPsaPsaIDOsn);
            layoutControl1.Controls.Add(tbPsaKodZv1);
            layoutControl1.Controls.Add(tbPsaKombIzd);
            layoutControl1.Controls.Add(tbPsaPsaID);
            layoutControl1.Controls.Add(tbArtTradeMark);
            layoutControl1.Controls.Add(tbPsaNameSbit);
            layoutControl1.Controls.Add(tbRzuNom);
            layoutControl1.Controls.Add(cbIsChip);
            layoutControl1.Controls.Add(tbRzuArticul);
            layoutControl1.Controls.Add(tbPsaMenName);
            layoutControl1.Controls.Add(tbPsaTbID);
            layoutControl1.Controls.Add(psaSezName);
            layoutControl1.Controls.Add(tbPsaYear);
            layoutControl1.Controls.Add(tbRzuKol);
            layoutControl1.Controls.Add(tbPsaNomZad);
            layoutControl1.Controls.Add(tbPsaNN);
            layoutControl1.Controls.Add(tbRzuDostZeh);
            layoutControl1.Controls.Add(tbRzuPach);
            layoutControl1.Font = new Font("Arial", 10F);
            layoutControl1.Location = new Point(2, 37);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(931, 543, 650, 368);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(1821, 206);
            layoutControl1.TabIndex = 15;
            layoutControl1.Text = "layoutControl1";
            // 
            // buttonVshivkiPrint
            // 
            buttonVshivkiPrint.Appearance.BackColor = Color.FromArgb(200, 225, 255);
            buttonVshivkiPrint.Appearance.Font = new Font("Arial", 10F);
            buttonVshivkiPrint.Appearance.Options.UseBackColor = true;
            buttonVshivkiPrint.Appearance.Options.UseFont = true;
            buttonVshivkiPrint.AppearanceDisabled.BackColor = Color.Green;
            buttonVshivkiPrint.AppearanceDisabled.ForeColor = Color.GreenYellow;
            buttonVshivkiPrint.AppearanceDisabled.Options.UseBackColor = true;
            buttonVshivkiPrint.AppearanceDisabled.Options.UseForeColor = true;
            buttonVshivkiPrint.ImageOptions.Image = (Image)resources.GetObject("buttonVshivkiPrint.ImageOptions.Image");
            buttonVshivkiPrint.Location = new Point(956, 52);
            buttonVshivkiPrint.Name = "buttonVshivkiPrint";
            buttonVshivkiPrint.Size = new Size(73, 22);
            buttonVshivkiPrint.StyleController = layoutControl1;
            buttonVshivkiPrint.TabIndex = 33;
            buttonVshivkiPrint.Text = "Вшивки";
            buttonVshivkiPrint.Click += buttonVshivkiPrint_Click;
            // 
            // textBoxRzId
            // 
            textBoxRzId.Location = new Point(825, 52);
            textBoxRzId.Name = "textBoxRzId";
            textBoxRzId.ObjectName = null;
            textBoxRzId.Properties.Appearance.BackColor = SystemColors.Window;
            textBoxRzId.Properties.Appearance.Font = new Font("Arial", 10F);
            textBoxRzId.Properties.Appearance.ForeColor = SystemColors.ControlText;
            textBoxRzId.Properties.Appearance.Options.UseBackColor = true;
            textBoxRzId.Properties.Appearance.Options.UseFont = true;
            textBoxRzId.Properties.Appearance.Options.UseForeColor = true;
            textBoxRzId.Size = new Size(127, 22);
            textBoxRzId.StyleController = layoutControl1;
            textBoxRzId.TabIndex = 32;
            // 
            // TextBoxRecomendZad
            // 
            TextBoxRecomendZad.BorderStyle = BorderStyle.FixedSingle;
            TextBoxRecomendZad.ErrorColor = Color.Red;
            TextBoxRecomendZad.ErrorMessage = null;
            TextBoxRecomendZad.Font = new Font("Arial", 10F);
            TextBoxRecomendZad.Location = new Point(437, 148);
            TextBoxRecomendZad.Multiline = true;
            TextBoxRecomendZad.Name = "TextBoxRecomendZad";
            TextBoxRecomendZad.Size = new Size(291, 48);
            TextBoxRecomendZad.TabIndex = 30;
            // 
            // TextBoxRecomendNom
            // 
            TextBoxRecomendNom.BorderStyle = BorderStyle.FixedSingle;
            TextBoxRecomendNom.ErrorColor = Color.Red;
            TextBoxRecomendNom.ErrorMessage = null;
            TextBoxRecomendNom.Font = new Font("Arial", 10F);
            TextBoxRecomendNom.Location = new Point(93, 150);
            TextBoxRecomendNom.Multiline = true;
            TextBoxRecomendNom.Name = "TextBoxRecomendNom";
            TextBoxRecomendNom.Size = new Size(247, 46);
            TextBoxRecomendNom.TabIndex = 29;
            // 
            // tbRzuMgZakr
            // 
            tbRzuMgZakr.Location = new Point(95, 124);
            tbRzuMgZakr.Name = "tbRzuMgZakr";
            tbRzuMgZakr.ObjectName = null;
            tbRzuMgZakr.Properties.Appearance.Font = new Font("Arial", 10F);
            tbRzuMgZakr.Properties.Appearance.Options.UseFont = true;
            tbRzuMgZakr.Size = new Size(245, 22);
            tbRzuMgZakr.StyleController = layoutControl1;
            tbRzuMgZakr.TabIndex = 26;
            // 
            // pbEskiz
            // 
            pbEskiz.Location = new Point(1629, 26);
            pbEskiz.Margin = new Padding(4, 3, 4, 3);
            pbEskiz.Name = "pbEskiz";
            pbEskiz.Size = new Size(187, 175);
            pbEskiz.SizeMode = PictureBoxSizeMode.Zoom;
            pbEskiz.TabIndex = 1;
            pbEskiz.TabStop = false;
            // 
            // tbSostPoln
            // 
            tbSostPoln.BorderStyle = BorderStyle.FixedSingle;
            tbSostPoln.ErrorColor = Color.Red;
            tbSostPoln.ErrorMessage = null;
            tbSostPoln.Font = new Font("Arial", 10F);
            tbSostPoln.Location = new Point(802, 78);
            tbSostPoln.Multiline = true;
            tbSostPoln.Name = "tbSostPoln";
            tbSostPoln.Size = new Size(227, 94);
            tbSostPoln.TabIndex = 7;
            // 
            // tbRzuMod
            // 
            tbRzuMod.BorderStyle = BorderStyle.FixedSingle;
            tbRzuMod.ErrorColor = Color.Red;
            tbRzuMod.ErrorMessage = null;
            tbRzuMod.Font = new Font("Arial", 10F);
            tbRzuMod.Location = new Point(824, 176);
            tbRzuMod.Margin = new Padding(0);
            tbRzuMod.Name = "tbRzuMod";
            tbRzuMod.Size = new Size(205, 20);
            tbRzuMod.TabIndex = 11;
            // 
            // tbArtGrup
            // 
            tbArtGrup.BorderStyle = BorderStyle.FixedSingle;
            tbArtGrup.ErrorColor = Color.Red;
            tbArtGrup.ErrorMessage = null;
            tbArtGrup.Font = new Font("Arial", 10F);
            tbArtGrup.Location = new Point(495, 124);
            tbArtGrup.Margin = new Padding(0);
            tbArtGrup.Name = "tbArtGrup";
            tbArtGrup.Size = new Size(233, 20);
            tbArtGrup.TabIndex = 12;
            // 
            // tbPsaKombOsn
            // 
            tbPsaKombOsn.BorderStyle = BorderStyle.FixedSingle;
            tbPsaKombOsn.ErrorColor = Color.Red;
            tbPsaKombOsn.ErrorMessage = null;
            tbPsaKombOsn.Font = new Font("Arial", 10F);
            tbPsaKombOsn.Location = new Point(1499, 100);
            tbPsaKombOsn.Margin = new Padding(0);
            tbPsaKombOsn.Name = "tbPsaKombOsn";
            tbPsaKombOsn.Size = new Size(121, 20);
            tbPsaKombOsn.TabIndex = 22;
            // 
            // tbPsaPrn
            // 
            tbPsaPrn.BorderStyle = BorderStyle.FixedSingle;
            tbPsaPrn.ErrorColor = Color.Red;
            tbPsaPrn.ErrorMessage = null;
            tbPsaPrn.Font = new Font("Arial", 10F);
            tbPsaPrn.Location = new Point(567, 52);
            tbPsaPrn.Margin = new Padding(0);
            tbPsaPrn.Multiline = true;
            tbPsaPrn.Name = "tbPsaPrn";
            tbPsaPrn.Size = new Size(161, 20);
            tbPsaPrn.TabIndex = 6;
            // 
            // tbPsaKodZv2
            // 
            tbPsaKodZv2.BorderStyle = BorderStyle.FixedSingle;
            tbPsaKodZv2.ErrorColor = Color.Red;
            tbPsaKodZv2.ErrorMessage = null;
            tbPsaKodZv2.Font = new Font("Arial", 10F);
            tbPsaKodZv2.Location = new Point(620, 76);
            tbPsaKodZv2.Margin = new Padding(0);
            tbPsaKodZv2.Name = "tbPsaKodZv2";
            tbPsaKodZv2.Size = new Size(108, 20);
            tbPsaKodZv2.TabIndex = 9;
            // 
            // tbPsaPsaIDOsn
            // 
            tbPsaPsaIDOsn.BorderStyle = BorderStyle.FixedSingle;
            tbPsaPsaIDOsn.ErrorColor = Color.Red;
            tbPsaPsaIDOsn.ErrorMessage = null;
            tbPsaPsaIDOsn.Font = new Font("Arial", 10F);
            tbPsaPsaIDOsn.Location = new Point(1499, 76);
            tbPsaPsaIDOsn.Margin = new Padding(0);
            tbPsaPsaIDOsn.Name = "tbPsaPsaIDOsn";
            tbPsaPsaIDOsn.Size = new Size(121, 20);
            tbPsaPsaIDOsn.TabIndex = 19;
            // 
            // tbPsaKodZv1
            // 
            tbPsaKodZv1.BorderStyle = BorderStyle.FixedSingle;
            tbPsaKodZv1.ErrorColor = Color.Red;
            tbPsaKodZv1.ErrorMessage = null;
            tbPsaKodZv1.Font = new Font("Arial", 10F);
            tbPsaKodZv1.Location = new Point(443, 76);
            tbPsaKodZv1.Margin = new Padding(0);
            tbPsaKodZv1.Name = "tbPsaKodZv1";
            tbPsaKodZv1.Size = new Size(108, 20);
            tbPsaKodZv1.TabIndex = 8;
            // 
            // tbPsaKombIzd
            // 
            tbPsaKombIzd.BorderStyle = BorderStyle.FixedSingle;
            tbPsaKombIzd.ErrorColor = Color.Red;
            tbPsaKombIzd.ErrorMessage = null;
            tbPsaKombIzd.Font = new Font("Arial", 10F);
            tbPsaKombIzd.Location = new Point(1329, 100);
            tbPsaKombIzd.Margin = new Padding(0);
            tbPsaKombIzd.Name = "tbPsaKombIzd";
            tbPsaKombIzd.Size = new Size(103, 20);
            tbPsaKombIzd.TabIndex = 21;
            // 
            // tbPsaPsaID
            // 
            tbPsaPsaID.BorderStyle = BorderStyle.FixedSingle;
            tbPsaPsaID.ErrorColor = Color.Red;
            tbPsaPsaID.ErrorMessage = null;
            tbPsaPsaID.Font = new Font("Arial", 10F);
            tbPsaPsaID.Location = new Point(1326, 76);
            tbPsaPsaID.Margin = new Padding(0);
            tbPsaPsaID.Name = "tbPsaPsaID";
            tbPsaPsaID.Size = new Size(100, 20);
            tbPsaPsaID.TabIndex = 18;
            // 
            // tbArtTradeMark
            // 
            tbArtTradeMark.BorderStyle = BorderStyle.FixedSingle;
            tbArtTradeMark.ErrorColor = Color.Red;
            tbArtTradeMark.ErrorMessage = null;
            tbArtTradeMark.Font = new Font("Arial", 10F);
            tbArtTradeMark.Location = new Point(1128, 124);
            tbArtTradeMark.Margin = new Padding(0);
            tbArtTradeMark.Name = "tbArtTradeMark";
            tbArtTradeMark.Size = new Size(125, 20);
            tbArtTradeMark.TabIndex = 23;
            // 
            // tbPsaNameSbit
            // 
            tbPsaNameSbit.BorderStyle = BorderStyle.FixedSingle;
            tbPsaNameSbit.ErrorColor = Color.Red;
            tbPsaNameSbit.ErrorMessage = null;
            tbPsaNameSbit.Font = new Font("Arial", 10F);
            tbPsaNameSbit.Location = new Point(1113, 100);
            tbPsaNameSbit.Margin = new Padding(0);
            tbPsaNameSbit.Name = "tbPsaNameSbit";
            tbPsaNameSbit.Size = new Size(153, 20);
            tbPsaNameSbit.TabIndex = 20;
            // 
            // tbRzuNom
            // 
            tbRzuNom.BorderStyle = BorderStyle.FixedSingle;
            tbRzuNom.ErrorColor = Color.Red;
            tbRzuNom.ErrorMessage = null;
            tbRzuNom.Font = new Font("Arial", 10F);
            tbRzuNom.Location = new Point(33, 52);
            tbRzuNom.Margin = new Padding(0);
            tbRzuNom.Name = "tbRzuNom";
            tbRzuNom.Size = new Size(148, 20);
            tbRzuNom.TabIndex = 0;
            // 
            // cbIsChip
            // 
            cbIsChip.Enabled = false;
            cbIsChip.Font = new Font("Arial", 10F);
            cbIsChip.Location = new Point(1584, 52);
            cbIsChip.Margin = new Padding(4, 3, 4, 3);
            cbIsChip.Name = "cbIsChip";
            cbIsChip.Size = new Size(36, 20);
            cbIsChip.TabIndex = 16;
            cbIsChip.UseVisualStyleBackColor = true;
            // 
            // tbRzuArticul
            // 
            tbRzuArticul.BorderStyle = BorderStyle.FixedSingle;
            tbRzuArticul.ErrorColor = Color.Red;
            tbRzuArticul.ErrorMessage = null;
            tbRzuArticul.Font = new Font("Arial", 10F);
            tbRzuArticul.Location = new Point(436, 100);
            tbRzuArticul.Margin = new Padding(0);
            tbRzuArticul.Name = "tbRzuArticul";
            tbRzuArticul.Size = new Size(292, 20);
            tbRzuArticul.TabIndex = 10;
            // 
            // tbPsaMenName
            // 
            tbPsaMenName.BorderStyle = BorderStyle.FixedSingle;
            tbPsaMenName.ErrorColor = Color.Red;
            tbPsaMenName.ErrorMessage = null;
            tbPsaMenName.Font = new Font("Arial", 10F);
            tbPsaMenName.Location = new Point(1388, 124);
            tbPsaMenName.Margin = new Padding(0);
            tbPsaMenName.Multiline = true;
            tbPsaMenName.Name = "tbPsaMenName";
            tbPsaMenName.Size = new Size(232, 20);
            tbPsaMenName.TabIndex = 24;
            // 
            // tbPsaTbID
            // 
            tbPsaTbID.BorderStyle = BorderStyle.FixedSingle;
            tbPsaTbID.ErrorColor = Color.Red;
            tbPsaTbID.ErrorMessage = null;
            tbPsaTbID.Font = new Font("Arial", 10F);
            tbPsaTbID.Location = new Point(1072, 76);
            tbPsaTbID.Margin = new Padding(0);
            tbPsaTbID.Name = "tbPsaTbID";
            tbPsaTbID.Size = new Size(204, 20);
            tbPsaTbID.TabIndex = 17;
            // 
            // psaSezName
            // 
            psaSezName.BorderStyle = BorderStyle.FixedSingle;
            psaSezName.ErrorColor = Color.Red;
            psaSezName.ErrorMessage = null;
            psaSezName.Font = new Font("Arial", 10F);
            psaSezName.Location = new Point(1431, 52);
            psaSezName.Margin = new Padding(0);
            psaSezName.Name = "psaSezName";
            psaSezName.Size = new Size(115, 20);
            psaSezName.TabIndex = 15;
            // 
            // tbPsaYear
            // 
            tbPsaYear.BorderStyle = BorderStyle.FixedSingle;
            tbPsaYear.ErrorColor = Color.Red;
            tbPsaYear.ErrorMessage = null;
            tbPsaYear.Font = new Font("Arial", 10F);
            tbPsaYear.Location = new Point(1270, 52);
            tbPsaYear.Margin = new Padding(0);
            tbPsaYear.Name = "tbPsaYear";
            tbPsaYear.Size = new Size(112, 20);
            tbPsaYear.TabIndex = 14;
            // 
            // tbRzuKol
            // 
            tbRzuKol.BorderStyle = BorderStyle.FixedSingle;
            tbRzuKol.ErrorColor = Color.Red;
            tbRzuKol.ErrorMessage = null;
            tbRzuKol.Font = new Font("Arial", 10F);
            tbRzuKol.Location = new Point(240, 52);
            tbRzuKol.Margin = new Padding(0);
            tbRzuKol.Name = "tbRzuKol";
            tbRzuKol.Size = new Size(100, 20);
            tbRzuKol.TabIndex = 2;
            // 
            // tbPsaNomZad
            // 
            tbPsaNomZad.BorderStyle = BorderStyle.FixedSingle;
            tbPsaNomZad.ErrorColor = Color.Red;
            tbPsaNomZad.ErrorMessage = null;
            tbPsaNomZad.Font = new Font("Arial", 10F);
            tbPsaNomZad.Location = new Point(377, 52);
            tbPsaNomZad.Margin = new Padding(0);
            tbPsaNomZad.Name = "tbPsaNomZad";
            tbPsaNomZad.Size = new Size(140, 20);
            tbPsaNomZad.TabIndex = 5;
            // 
            // tbPsaNN
            // 
            tbPsaNN.BorderStyle = BorderStyle.FixedSingle;
            tbPsaNN.ErrorColor = Color.Red;
            tbPsaNN.ErrorMessage = null;
            tbPsaNN.Font = new Font("Arial", 10F);
            tbPsaNN.Location = new Point(1068, 52);
            tbPsaNN.Margin = new Padding(0);
            tbPsaNN.Name = "tbPsaNN";
            tbPsaNN.Size = new Size(164, 20);
            tbPsaNN.TabIndex = 13;
            // 
            // tbRzuDostZeh
            // 
            tbRzuDostZeh.BorderStyle = BorderStyle.FixedSingle;
            tbRzuDostZeh.ErrorColor = Color.Red;
            tbRzuDostZeh.ErrorMessage = null;
            tbRzuDostZeh.Font = new Font("Arial", 10F);
            tbRzuDostZeh.Location = new Point(78, 76);
            tbRzuDostZeh.Margin = new Padding(0);
            tbRzuDostZeh.Name = "tbRzuDostZeh";
            tbRzuDostZeh.Size = new Size(262, 20);
            tbRzuDostZeh.TabIndex = 3;
            // 
            // tbRzuPach
            // 
            tbRzuPach.BorderStyle = BorderStyle.FixedSingle;
            tbRzuPach.ErrorColor = Color.Red;
            tbRzuPach.ErrorMessage = null;
            tbRzuPach.Font = new Font("Arial", 10F);
            tbRzuPach.Location = new Point(66, 100);
            tbRzuPach.Margin = new Padding(0);
            tbRzuPach.Name = "tbRzuPach";
            tbRzuPach.Size = new Size(274, 20);
            tbRzuPach.TabIndex = 4;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup12 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            Root.Size = new Size(1821, 206);
            Root.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup9, layoutControlGroup11, layoutControlGroup10, layoutControlItem22 });
            layoutControlGroup12.Location = new Point(0, 0);
            layoutControlGroup12.Name = "layoutControlGroup12";
            layoutControlGroup12.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup12.Size = new Size(1821, 206);
            layoutControlGroup12.Text = "КАРТОЧКА РАСЧЕТА";
            // 
            // layoutControlGroup9
            // 
            layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem17, layoutControlItem46, layoutControlItem50, layoutControlItem52, emptySpaceItem24, layoutControlItem155, layoutControlItem110 });
            layoutControlGroup9.Location = new Point(0, 0);
            layoutControlGroup9.Name = "layoutControlGroup9";
            layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup9.Size = new Size(344, 179);
            layoutControlGroup9.Text = "Расчет";
            // 
            // layoutControlItem17
            // 
            layoutControlItem17.Control = tbRzuNom;
            layoutControlItem17.Location = new Point(0, 0);
            layoutControlItem17.Name = "layoutControlItem17";
            layoutControlItem17.Size = new Size(175, 24);
            layoutControlItem17.Text = "№";
            layoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem17.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem17.TextSize = new Size(13, 13);
            layoutControlItem17.TextToControlDistance = 10;
            // 
            // layoutControlItem46
            // 
            layoutControlItem46.Control = tbRzuKol;
            layoutControlItem46.Location = new Point(185, 0);
            layoutControlItem46.Name = "layoutControlItem46";
            layoutControlItem46.Size = new Size(149, 24);
            layoutControlItem46.Text = "Кол-во";
            layoutControlItem46.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem46.TextSize = new Size(35, 13);
            layoutControlItem46.TextToControlDistance = 10;
            // 
            // layoutControlItem50
            // 
            layoutControlItem50.Control = tbRzuDostZeh;
            layoutControlItem50.Location = new Point(0, 24);
            layoutControlItem50.Name = "layoutControlItem50";
            layoutControlItem50.Size = new Size(334, 24);
            layoutControlItem50.Text = "Бригада №";
            layoutControlItem50.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem50.TextSize = new Size(58, 13);
            layoutControlItem50.TextToControlDistance = 10;
            // 
            // layoutControlItem52
            // 
            layoutControlItem52.Control = tbRzuPach;
            layoutControlItem52.Location = new Point(0, 48);
            layoutControlItem52.Name = "layoutControlItem52";
            layoutControlItem52.Size = new Size(334, 24);
            layoutControlItem52.Text = "№ пачек";
            layoutControlItem52.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem52.TextSize = new Size(46, 13);
            layoutControlItem52.TextToControlDistance = 10;
            // 
            // emptySpaceItem24
            // 
            emptySpaceItem24.Location = new Point(175, 0);
            emptySpaceItem24.Name = "emptySpaceItem24";
            emptySpaceItem24.Size = new Size(10, 24);
            // 
            // layoutControlItem155
            // 
            layoutControlItem155.Control = tbRzuMgZakr;
            layoutControlItem155.Location = new Point(0, 72);
            layoutControlItem155.Name = "layoutControlItem155";
            layoutControlItem155.Size = new Size(334, 26);
            layoutControlItem155.Text = "Карта раскроя";
            layoutControlItem155.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem155.TextSize = new Size(75, 13);
            layoutControlItem155.TextToControlDistance = 10;
            // 
            // layoutControlItem110
            // 
            layoutControlItem110.Control = TextBoxRecomendNom;
            layoutControlItem110.Location = new Point(0, 98);
            layoutControlItem110.MinSize = new Size(24, 24);
            layoutControlItem110.Name = "layoutControlItem110";
            layoutControlItem110.Size = new Size(334, 50);
            layoutControlItem110.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem110.Text = "Рекомендации";
            layoutControlItem110.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem110.TextSize = new Size(73, 13);
            layoutControlItem110.TextToControlDistance = 10;
            // 
            // layoutControlGroup11
            // 
            layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem54, layoutControlItem21, layoutControlItem56, layoutControlItem58, layoutControlItem59, emptySpaceItem15, emptySpaceItem17, emptySpaceItem18, emptySpaceItem19, layoutControlItem63, emptySpaceItem20, layoutControlItem65, emptySpaceItem21, layoutControlItem67, layoutControlItem69, emptySpaceItem16, layoutControlItem71, layoutControlItem73, emptySpaceItem22, layoutControlItem61 });
            layoutControlGroup11.Location = new Point(1033, 0);
            layoutControlGroup11.Name = "layoutControlGroup11";
            layoutControlGroup11.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup11.Size = new Size(591, 179);
            layoutControlGroup11.Text = "Матрица";
            // 
            // layoutControlItem54
            // 
            layoutControlItem54.Control = tbPsaNN;
            layoutControlItem54.Location = new Point(0, 0);
            layoutControlItem54.Name = "layoutControlItem54";
            layoutControlItem54.Size = new Size(193, 24);
            layoutControlItem54.Text = "Код";
            layoutControlItem54.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem54.TextSize = new Size(20, 13);
            layoutControlItem54.TextToControlDistance = 5;
            // 
            // layoutControlItem21
            // 
            layoutControlItem21.Control = tbPsaTbID;
            layoutControlItem21.Location = new Point(0, 24);
            layoutControlItem21.Name = "layoutControlItem21";
            layoutControlItem21.Size = new Size(237, 24);
            layoutControlItem21.Text = "Блок";
            layoutControlItem21.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem21.TextSize = new Size(24, 13);
            layoutControlItem21.TextToControlDistance = 5;
            // 
            // layoutControlItem56
            // 
            layoutControlItem56.Control = tbPsaYear;
            layoutControlItem56.Location = new Point(203, 0);
            layoutControlItem56.Name = "layoutControlItem56";
            layoutControlItem56.Size = new Size(140, 24);
            layoutControlItem56.Text = "Год";
            layoutControlItem56.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem56.TextSize = new Size(19, 13);
            layoutControlItem56.TextToControlDistance = 5;
            // 
            // layoutControlItem58
            // 
            layoutControlItem58.Control = psaSezName;
            layoutControlItem58.Location = new Point(353, 0);
            layoutControlItem58.Name = "layoutControlItem58";
            layoutControlItem58.Size = new Size(154, 24);
            layoutControlItem58.Text = "Сезон";
            layoutControlItem58.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem58.TextSize = new Size(30, 13);
            layoutControlItem58.TextToControlDistance = 5;
            // 
            // layoutControlItem59
            // 
            layoutControlItem59.Control = cbIsChip;
            layoutControlItem59.Location = new Point(517, 0);
            layoutControlItem59.Name = "layoutControlItem59";
            layoutControlItem59.Size = new Size(64, 24);
            layoutControlItem59.Text = "Чип";
            layoutControlItem59.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem59.TextSize = new Size(19, 13);
            layoutControlItem59.TextToControlDistance = 5;
            // 
            // emptySpaceItem15
            // 
            emptySpaceItem15.Location = new Point(237, 24);
            emptySpaceItem15.Name = "emptySpaceItem15";
            emptySpaceItem15.Size = new Size(10, 24);
            // 
            // emptySpaceItem17
            // 
            emptySpaceItem17.Location = new Point(193, 0);
            emptySpaceItem17.Name = "emptySpaceItem17";
            emptySpaceItem17.Size = new Size(10, 24);
            // 
            // emptySpaceItem18
            // 
            emptySpaceItem18.Location = new Point(343, 0);
            emptySpaceItem18.Name = "emptySpaceItem18";
            emptySpaceItem18.Size = new Size(10, 24);
            // 
            // emptySpaceItem19
            // 
            emptySpaceItem19.Location = new Point(507, 0);
            emptySpaceItem19.Name = "emptySpaceItem19";
            emptySpaceItem19.Size = new Size(10, 24);
            // 
            // layoutControlItem63
            // 
            layoutControlItem63.Control = tbPsaNameSbit;
            layoutControlItem63.Location = new Point(0, 48);
            layoutControlItem63.Name = "layoutControlItem63";
            layoutControlItem63.Size = new Size(227, 24);
            layoutControlItem63.Text = "Канал сбыта";
            layoutControlItem63.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem63.TextSize = new Size(65, 13);
            layoutControlItem63.TextToControlDistance = 5;
            // 
            // emptySpaceItem20
            // 
            emptySpaceItem20.Location = new Point(227, 48);
            emptySpaceItem20.Name = "emptySpaceItem20";
            emptySpaceItem20.Size = new Size(10, 24);
            // 
            // layoutControlItem65
            // 
            layoutControlItem65.Control = tbArtTradeMark;
            layoutControlItem65.Location = new Point(0, 72);
            layoutControlItem65.Name = "layoutControlItem65";
            layoutControlItem65.Size = new Size(214, 76);
            layoutControlItem65.Text = "Торговая марка";
            layoutControlItem65.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem65.TextSize = new Size(80, 13);
            layoutControlItem65.TextToControlDistance = 5;
            // 
            // emptySpaceItem21
            // 
            emptySpaceItem21.Location = new Point(214, 72);
            emptySpaceItem21.Name = "emptySpaceItem21";
            emptySpaceItem21.Size = new Size(10, 76);
            // 
            // layoutControlItem67
            // 
            layoutControlItem67.Control = tbPsaPsaID;
            layoutControlItem67.Location = new Point(247, 24);
            layoutControlItem67.Name = "layoutControlItem67";
            layoutControlItem67.Size = new Size(140, 24);
            layoutControlItem67.Text = "psa_id";
            layoutControlItem67.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem67.TextSize = new Size(31, 13);
            layoutControlItem67.TextToControlDistance = 5;
            // 
            // layoutControlItem69
            // 
            layoutControlItem69.Control = tbPsaPsaIDOsn;
            layoutControlItem69.Location = new Point(397, 24);
            layoutControlItem69.Name = "layoutControlItem69";
            layoutControlItem69.Size = new Size(184, 24);
            layoutControlItem69.Text = "psa_id_osn";
            layoutControlItem69.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem69.TextSize = new Size(54, 13);
            layoutControlItem69.TextToControlDistance = 5;
            // 
            // emptySpaceItem16
            // 
            emptySpaceItem16.Location = new Point(387, 24);
            emptySpaceItem16.Name = "emptySpaceItem16";
            emptySpaceItem16.Size = new Size(10, 24);
            // 
            // layoutControlItem71
            // 
            layoutControlItem71.Control = tbPsaKombIzd;
            layoutControlItem71.Location = new Point(237, 48);
            layoutControlItem71.Name = "layoutControlItem71";
            layoutControlItem71.Size = new Size(156, 24);
            layoutControlItem71.Text = "komb_izd";
            layoutControlItem71.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem71.TextSize = new Size(44, 13);
            layoutControlItem71.TextToControlDistance = 5;
            // 
            // layoutControlItem73
            // 
            layoutControlItem73.Control = tbPsaKombOsn;
            layoutControlItem73.Location = new Point(403, 48);
            layoutControlItem73.Name = "layoutControlItem73";
            layoutControlItem73.Size = new Size(178, 24);
            layoutControlItem73.Text = "komb_osn";
            layoutControlItem73.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem73.TextSize = new Size(48, 13);
            layoutControlItem73.TextToControlDistance = 5;
            // 
            // emptySpaceItem22
            // 
            emptySpaceItem22.Location = new Point(393, 48);
            emptySpaceItem22.Name = "emptySpaceItem22";
            emptySpaceItem22.Size = new Size(10, 24);
            // 
            // layoutControlItem61
            // 
            layoutControlItem61.Control = tbPsaMenName;
            layoutControlItem61.Location = new Point(224, 72);
            layoutControlItem61.Name = "layoutControlItem61";
            layoutControlItem61.Size = new Size(357, 76);
            layoutControlItem61.Text = "Категория (менеджер)";
            layoutControlItem61.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem61.TextSize = new Size(116, 13);
            layoutControlItem61.TextToControlDistance = 5;
            // 
            // layoutControlGroup10
            // 
            layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem48, layoutControlItem18, layoutControlItem75, emptySpaceItem23, layoutControlItem77, layoutControlItem79, layoutControlItem81, layoutControlItem83, layoutControlItem85, emptySpaceItem25, emptySpaceItem26, layoutControlItem135, layoutControlItem158, emptySpaceItem27, layoutControlItem194 });
            layoutControlGroup10.Location = new Point(344, 0);
            layoutControlGroup10.Name = "layoutControlGroup10";
            layoutControlGroup10.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            layoutControlGroup10.Size = new Size(689, 179);
            layoutControlGroup10.Text = "Задание";
            // 
            // layoutControlItem48
            // 
            layoutControlItem48.Control = tbPsaNomZad;
            layoutControlItem48.Location = new Point(0, 0);
            layoutControlItem48.Name = "layoutControlItem48";
            layoutControlItem48.Size = new Size(167, 24);
            layoutControlItem48.Text = "№";
            layoutControlItem48.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem48.TextSize = new Size(13, 13);
            layoutControlItem48.TextToControlDistance = 10;
            // 
            // layoutControlItem18
            // 
            layoutControlItem18.Control = tbRzuArticul;
            layoutControlItem18.Location = new Point(0, 48);
            layoutControlItem18.Name = "layoutControlItem18";
            layoutControlItem18.Size = new Size(378, 24);
            layoutControlItem18.Text = "Артикул (шв.)";
            layoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem18.TextSize = new Size(72, 13);
            layoutControlItem18.TextToControlDistance = 10;
            // 
            // layoutControlItem75
            // 
            layoutControlItem75.Control = tbPsaPrn;
            layoutControlItem75.Location = new Point(177, 0);
            layoutControlItem75.Name = "layoutControlItem75";
            layoutControlItem75.Size = new Size(201, 24);
            layoutControlItem75.Text = "Цвет";
            layoutControlItem75.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem75.TextSize = new Size(26, 13);
            layoutControlItem75.TextToControlDistance = 10;
            // 
            // emptySpaceItem23
            // 
            emptySpaceItem23.Location = new Point(0, 24);
            emptySpaceItem23.Name = "emptySpaceItem23";
            emptySpaceItem23.Size = new Size(34, 24);
            // 
            // layoutControlItem77
            // 
            layoutControlItem77.Control = tbPsaKodZv1;
            layoutControlItem77.Location = new Point(34, 24);
            layoutControlItem77.Name = "layoutControlItem77";
            layoutControlItem77.Size = new Size(167, 24);
            layoutControlItem77.Text = "Код цв.1";
            layoutControlItem77.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem77.TextSize = new Size(45, 13);
            layoutControlItem77.TextToControlDistance = 10;
            // 
            // layoutControlItem79
            // 
            layoutControlItem79.Control = tbPsaKodZv2;
            layoutControlItem79.Location = new Point(211, 24);
            layoutControlItem79.Name = "layoutControlItem79";
            layoutControlItem79.Size = new Size(167, 24);
            layoutControlItem79.Text = "Код цв.2";
            layoutControlItem79.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem79.TextSize = new Size(45, 13);
            layoutControlItem79.TextToControlDistance = 10;
            // 
            // layoutControlItem81
            // 
            layoutControlItem81.Control = tbArtGrup;
            layoutControlItem81.Location = new Point(0, 72);
            layoutControlItem81.Name = "layoutControlItem81";
            layoutControlItem81.Size = new Size(378, 24);
            layoutControlItem81.Text = "Наименование (из справ.)";
            layoutControlItem81.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem81.TextSize = new Size(131, 13);
            layoutControlItem81.TextToControlDistance = 10;
            // 
            // layoutControlItem83
            // 
            layoutControlItem83.Control = tbRzuMod;
            layoutControlItem83.Location = new Point(388, 124);
            layoutControlItem83.MinSize = new Size(24, 24);
            layoutControlItem83.Name = "layoutControlItem83";
            layoutControlItem83.Size = new Size(291, 24);
            layoutControlItem83.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem83.Text = "Модель (торг.)";
            layoutControlItem83.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem83.TextSize = new Size(77, 13);
            layoutControlItem83.TextToControlDistance = 5;
            // 
            // layoutControlItem85
            // 
            layoutControlItem85.Control = tbSostPoln;
            layoutControlItem85.Location = new Point(388, 26);
            layoutControlItem85.MinSize = new Size(114, 30);
            layoutControlItem85.Name = "layoutControlItem85";
            layoutControlItem85.Size = new Size(291, 98);
            layoutControlItem85.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem85.Text = "Состав\r\n(из справ.)";
            layoutControlItem85.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem85.TextSize = new Size(55, 26);
            layoutControlItem85.TextToControlDistance = 5;
            // 
            // emptySpaceItem25
            // 
            emptySpaceItem25.Location = new Point(167, 0);
            emptySpaceItem25.Name = "emptySpaceItem25";
            emptySpaceItem25.Size = new Size(10, 24);
            // 
            // emptySpaceItem26
            // 
            emptySpaceItem26.Location = new Point(201, 24);
            emptySpaceItem26.Name = "emptySpaceItem26";
            emptySpaceItem26.Size = new Size(10, 24);
            // 
            // layoutControlItem135
            // 
            layoutControlItem135.Control = TextBoxRecomendZad;
            layoutControlItem135.Location = new Point(0, 96);
            layoutControlItem135.MinSize = new Size(24, 24);
            layoutControlItem135.Name = "layoutControlItem135";
            layoutControlItem135.Size = new Size(378, 52);
            layoutControlItem135.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem135.Text = "Рекомендации";
            layoutControlItem135.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem135.TextSize = new Size(73, 13);
            layoutControlItem135.TextToControlDistance = 10;
            // 
            // layoutControlItem158
            // 
            layoutControlItem158.Control = textBoxRzId;
            layoutControlItem158.Location = new Point(388, 0);
            layoutControlItem158.Name = "layoutControlItem158";
            layoutControlItem158.Size = new Size(214, 26);
            layoutControlItem158.Text = "Наряд-задание";
            layoutControlItem158.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem158.TextSize = new Size(78, 13);
            layoutControlItem158.TextToControlDistance = 5;
            // 
            // emptySpaceItem27
            // 
            emptySpaceItem27.Location = new Point(378, 0);
            emptySpaceItem27.Name = "emptySpaceItem27";
            emptySpaceItem27.Size = new Size(10, 148);
            // 
            // layoutControlItem194
            // 
            layoutControlItem194.Control = buttonVshivkiPrint;
            layoutControlItem194.Location = new Point(602, 0);
            layoutControlItem194.Name = "layoutControlItem194";
            layoutControlItem194.Size = new Size(77, 26);
            layoutControlItem194.TextVisible = false;
            // 
            // layoutControlItem22
            // 
            layoutControlItem22.Control = pbEskiz;
            layoutControlItem22.Location = new Point(1624, 0);
            layoutControlItem22.Name = "layoutControlItem22";
            layoutControlItem22.Size = new Size(191, 179);
            layoutControlItem22.TextVisible = false;
            // 
            // customTextBoxEx1
            // 
            customTextBoxEx1.Location = new Point(78, 28);
            customTextBoxEx1.Name = "customTextBoxEx1";
            customTextBoxEx1.ObjectName = null;
            customTextBoxEx1.Properties.Appearance.Font = new Font("Arial", 10F);
            customTextBoxEx1.Properties.Appearance.Options.UseFont = true;
            customTextBoxEx1.Size = new Size(78, 20);
            customTextBoxEx1.StyleController = layoutControl4;
            customTextBoxEx1.TabIndex = 17;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = gridControlOtdelka;
            layoutControlItem4.Location = new Point(1265, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(546, 247);
            layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem78
            // 
            emptySpaceItem78.Location = new Point(0, 0);
            emptySpaceItem78.Name = "emptySpaceItem78";
            emptySpaceItem78.Size = new Size(1793, 36);
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
            ((System.ComponentModel.ISupportInitialize)layoutControl3).EndInit();
            layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchLookUpEditArticul.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEdit1View).EndInit();
            ((System.ComponentModel.ISupportInitialize)customRadioGroup2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)customRadioGroup3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem64).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup24).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem152).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem63).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup28).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem76).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem196).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem77).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem198).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem65).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)layoutControl9).EndInit();
            layoutControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup26).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem75).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem192).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem193).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup27).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem159).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem61).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem160).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem161).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem162).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem163).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem164).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem165).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem62).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem166).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem167).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem168).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem66).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem169).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem170).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem171).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem172).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem67).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem173).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem174).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem175).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem176).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem177).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem68).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem178).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem179).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem180).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem181).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem69).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem182).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem183).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem70).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem184).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem186).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem71).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem187).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem188).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem189).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem72).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem73).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem190).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem191).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem74).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem185).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem60).EndInit();
            RasInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup22).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup23).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl5).EndInit();
            layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlPartNaklList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPartNaklList).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup15).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup16).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem105).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem107).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl6).EndInit();
            layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mtbRzuVidStir.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataStCd.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataStR.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataStP.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataVCd.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataVChi.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataVR.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataVP.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataRasv.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrCd.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrKm.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrPe.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrR.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataPrP.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataRasp.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup17).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup18).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem111).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem115).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem117).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem119).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem121).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem123).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem41).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem42).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem43).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem44).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem45).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem46).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem39).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem112).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem40).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem113).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup19).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem127).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem128).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem47).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem48).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem138).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem139).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem140).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem141).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem142).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem49).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem50).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem51).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem52).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem59).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup20).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem131).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem53).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem133).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem54).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem148).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem149).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem150).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem151).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem55).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem56).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem57).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem58).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl4).EndInit();
            layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)textBoxDataZa.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuData1С.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataCd.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataUp.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataRab.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataZeh.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbPszRpcNom.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataR.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbRzuDataCdUt.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbPsaDataCdPlan.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtbPsaDataZap.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup14).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem106).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem28).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem87).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem29).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem89).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem30).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem91).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem31).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem93).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem32).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem95).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem33).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem97).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem34).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem99).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem35).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem101).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem36).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem103).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem37).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem38).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem156).EndInit();
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
            TabPageMgKart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl8).EndInit();
            layoutControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlNastilGroupView).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNastilGroupView).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlNastilList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNastilList).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup25).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem143).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem145).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)textBoxRzId.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbRzuMgZakr.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbEskiz).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem46).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem50).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem52).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem24).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem155).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem110).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem54).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem56).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem58).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem59).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem15).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem17).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem18).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem19).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem63).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem20).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem65).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem67).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem69).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem71).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem73).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem22).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem61).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem48).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem75).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem23).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem77).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem79).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem81).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem83).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem85).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem25).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem26).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem135).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem158).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem27).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem194).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).EndInit();
            ((System.ComponentModel.ISupportInitialize)customTextBoxEx1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem78).EndInit();
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
        private CustomCheckBox cbRzuStirFact;
        private CustomCheckBox cbPszStirPlan;
        private CustomCheckBox cbRzuVishFact;
        private CustomCheckBox cbPszVishPlan;
        private CustomCheckBox cbRzuPrintFact;
        private CustomCheckBox cbPszPrintPlan;
        private CustomLabel customLabel2;
        private CustomTabControl xtraTabControl1;
        private CustomGridControl gridControlProizvCombIzdSP;
        private CustomGridControl gridControlProizvCombIzdVZP;
        private CustomGridControl gridControlNaklList;
        private CustomGridControl gridControlOtdelka;
        private CustomGridControl gridControlPartNaklList;
        private CustomSimpleButton sbProizvCombIzdSP;
        private SplitContainer splitContainer2;
        private CustomGroupBox customGroupBox7;
        private CustomGroupBox customGroupBox8;
        private TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNaklCountBefore;
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
        private DevExpress.XtraTab.XtraTabPage SockZadanyInfo;
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
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
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
        private CustomTextBox tbSostPoln;
        private CustomTextBox tbRzuMod;
        private CustomTextBox tbArtGrup;
        private CustomTextBox tbPsaKombOsn;
        private CustomTextBox tbPsaPrn;
        private CustomTextBox tbPsaKodZv2;
        private CustomTextBox tbPsaPsaIDOsn;
        private CustomTextBox tbPsaKodZv1;
        private CustomTextBox tbPsaKombIzd;
        private PictureBox pbEskiz;
        private CustomTextBox tbPsaPsaID;
        private CustomTextBox tbArtTradeMark;
        private CustomTextBox tbPsaNameSbit;
        private CustomTextBox tbRzuNom;
        private CustomCheckBox cbIsChip;
        private CustomTextBox tbRzuArticul;
        private CustomTextBox tbPsaMenName;
        private CustomTextBox tbPsaTbID;
        private CustomTextBox psaSezName;
        private CustomTextBox tbPsaYear;
        private CustomTextBox tbRzuKol;
        private CustomTextBox tbPsaNomZad;
        private CustomTextBox tbPsaNN;
        private CustomTextBox tbRzuDostZeh;
        private CustomTextBox tbRzuPach;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem54;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem56;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem58;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem59;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem15;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem17;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem18;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem63;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem65;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem67;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem69;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem71;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem73;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem61;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem46;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem52;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem24;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem48;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem75;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem77;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem79;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem81;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem83;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem85;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem25;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem26;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup13;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup14;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem28;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem29;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem30;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem31;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem32;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem33;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem34;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem35;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem36;
        private CustomTextBoxEx mtbPsaDataZap;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem106;
        private CustomTextBoxEx mtbPsaDataCdPlan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem87;
        private CustomTextBoxEx mtbRzuDataR;
        private CustomTextBoxEx mtbRzuDataCdUt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem89;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem91;
        private CustomTextBoxEx tbPszRpcNom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem93;
        private CustomTextBoxEx mtbRzuDataRab;
        private CustomTextBoxEx mtbRzuDataZeh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem95;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem97;
        private CustomTextBoxEx mtbRzuData1С;
        private CustomTextBoxEx mtbRzuDataCd;
        private CustomTextBoxEx mtbRzuDataUp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem99;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem101;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem103;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup15;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem105;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem107;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup17;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem112;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem113;
        private CustomTextBoxEx mtbRzuDataPrKm;
        private CustomTextBoxEx mtbRzuDataPrPe;
        private CustomTextBoxEx mtbRzuDataPrR;
        private CustomTextBoxEx mtbRzuDataPrP;
        private CustomTextBoxEx mtbRzuDataRasp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem111;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem115;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem117;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem119;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem121;
        private CustomTextBoxEx mtbRzuDataPrCd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem123;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem40;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem41;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem42;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem43;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem44;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem45;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem46;
        private CustomTextBoxEx mtbRzuDataVCd;
        private CustomTextBoxEx mtbRzuDataVChi;
        private CustomTextBoxEx mtbRzuDataVR;
        private CustomTextBoxEx mtbRzuDataVP;
        private CustomTextBoxEx mtbRzuDataRasv;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem127;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem128;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem47;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem48;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem138;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem139;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem140;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem141;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem142;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem49;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem50;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem51;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem52;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem131;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem53;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem133;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem54;
        private CustomTextBoxEx mtbRzuVidStir;
        private CustomTextBoxEx mtbRzuDataStCd;
        private CustomTextBoxEx mtbRzuDataStR;
        private CustomTextBoxEx mtbRzuDataStP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem148;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem149;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem150;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem151;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem55;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem56;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem59;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem57;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem58;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup21;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup22;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup23;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem39;
        private CustomSearchLookUpEdit searchLookUpEditArticul;
        private DevExpress.XtraGrid.Views.Grid.GridView customSearchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem152;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem64;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem63;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem65;
        private DevExpress.XtraGrid.Columns.GridColumn columnArticul;
        private DevExpress.XtraGrid.Columns.GridColumn columnGrup;
        private DevExpress.XtraGrid.Columns.GridColumn columnMod;
        private DevExpress.XtraGrid.Columns.GridColumn columnKo;
        private CustomTextBoxEx tbRzuMgZakr;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem155;
        private CustomTextBox TextBoxRecomendZad;
        private CustomTextBox TextBoxRecomendNom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem110;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem135;
        private DevExpress.XtraTab.XtraTabPage TabPageMgKart;
        private CustomGridControl gridControlNastilList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNastilList;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem143;
        private CustomGridControl gridControlNastilGroupView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNastilGroupView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem145;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnMgKart;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnNakl;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnDateR;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnTArticul;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnSebTM;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnTkanType;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnRazr;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnKol;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnKolOnr;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnKolOr;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnKp;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnKolPog;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnKolO;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnTkanExpense;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnChyl;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnTab1;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnTab2;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnTab3;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnFio1;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnFio2;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnFio3;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnProzVipad;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilListColumnVN;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnMgKart;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnKodPr;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnTArticul;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnSeb;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnVN;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnKol;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnKolOnr;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnKolPog;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnSumSeb;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnSumRash;
        private DevExpress.XtraGrid.Columns.GridColumn gridNastilGroupViewColumnSumVetM;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem37;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem38;
        private CustomTextBoxEx textBoxRzId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem158;
        private CustomTextBoxEx textBoxDataZa;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem156;
        private CustomTextBoxEx customTextBoxEx1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup26;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem159;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem61;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem160;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem161;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem162;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem163;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem164;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem165;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem62;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem166;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem167;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem168;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem66;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem169;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem170;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem171;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem172;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem67;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem173;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem174;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem175;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem176;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem177;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem68;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem178;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem179;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem180;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem181;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem69;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem182;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem183;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem70;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem184;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem185;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem186;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem71;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem187;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem188;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem189;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem72;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem73;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem190;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem191;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem74;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem75;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem192;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem193;
        private DevExpress.XtraLayout.SplitterItem splitterItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem60;
        private CustomSimpleButton buttonVshivkiPrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem194;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup28;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem76;
        private CustomTextBox textBoxYearIzNakl;
        private CustomTextBox textBoxIzNakl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem196;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem77;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem198;
        private CustomLayoutControl layoutControl2;
        private CustomLayoutControl layoutControl3;
        private CustomLayoutControl layoutControl1;
        private CustomLayoutControl layoutControl4;
        private CustomLayoutControl layoutControl5;
        private CustomLayoutControl layoutControl6;
        private CustomLayoutControl layoutControl7;
        private CustomLayoutControl layoutControl8;
        private CustomLayoutControl layoutControl9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem78;
    }
}
