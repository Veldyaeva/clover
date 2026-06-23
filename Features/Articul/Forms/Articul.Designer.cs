using DevExpress.XtraLayout;
using SewingProduction.Core.Class;
using SewingProduction.Core.Class.CustomControls;
using System.Drawing;

namespace SewingProduction.Features.Articul
{
    partial class Articul
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
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions3 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Articul));
            grArtDrName = new DevExpress.XtraGrid.Columns.GridColumn();
            bsArt = new System.Windows.Forms.BindingSource(components);
            bsArticul = new System.Windows.Forms.BindingSource(components);
            customButtonCopy = new CustomSimpleButton();
            customLayoutControl1 = new CustomLayoutControl();
            btnArticulEdit = new CustomSimpleButton();
            txbBrakAll = new DevExpress.XtraEditors.TextEdit();
            txtBrakPercent4 = new DevExpress.XtraEditors.TextEdit();
            txtBrakPercent5 = new DevExpress.XtraEditors.TextEdit();
            txtBrakPercent6 = new DevExpress.XtraEditors.TextEdit();
            txbKoefVedDG = new DevExpress.XtraEditors.TextEdit();
            txtBrakPercent7 = new DevExpress.XtraEditors.TextEdit();
            txtBrakPercent3 = new DevExpress.XtraEditors.TextEdit();
            txtBrakPercent2 = new DevExpress.XtraEditors.TextEdit();
            txbKoefPr = new DevExpress.XtraEditors.TextEdit();
            txtBrakPercent1 = new DevExpress.XtraEditors.TextEdit();
            chkPres = new CustomCheckBox();
            txbSebDop = new DevExpress.XtraEditors.TextEdit();
            chkStra = new CustomCheckBox();
            txbSumKomplNum = new DevExpress.XtraEditors.TextEdit();
            customCheckBox1 = new CustomCheckBox();
            chkBus = new CustomCheckBox();
            txbSumZarpl = new DevExpress.XtraEditors.TextEdit();
            chkV = new CustomCheckBox();
            txbNorm_t7 = new DevExpress.XtraEditors.TextEdit();
            chkP = new CustomCheckBox();
            txbSumSebRaskr = new DevExpress.XtraEditors.TextEdit();
            chbIsFurnit = new CustomCheckBox();
            txbNorm_t6 = new DevExpress.XtraEditors.TextEdit();
            chbIsUpak = new CustomCheckBox();
            txbSek = new DevExpress.XtraEditors.TextEdit();
            txbSumStrVznos = new DevExpress.XtraEditors.TextEdit();
            txbSebz = new DevExpress.XtraEditors.TextEdit();
            txbSekVyaz = new DevExpress.XtraEditors.TextEdit();
            txbSekShv = new DevExpress.XtraEditors.TextEdit();
            txbSekKr = new DevExpress.XtraEditors.TextEdit();
            txbSumDopOpl = new DevExpress.XtraEditors.TextEdit();
            txbNorm_t5 = new DevExpress.XtraEditors.TextEdit();
            txbNorm_t4 = new DevExpress.XtraEditors.TextEdit();
            txbNorm_t2 = new DevExpress.XtraEditors.TextEdit();
            txbNorm_t3 = new DevExpress.XtraEditors.TextEdit();
            txbTkanSeb_t7 = new DevExpress.XtraEditors.TextEdit();
            txbTkanSeb_t6 = new DevExpress.XtraEditors.TextEdit();
            txbTkanSeb_t5 = new DevExpress.XtraEditors.TextEdit();
            txbTkanSeb_t1 = new DevExpress.XtraEditors.TextEdit();
            txbTkanSeb_t4 = new DevExpress.XtraEditors.TextEdit();
            txbTkanSeb_t2 = new DevExpress.XtraEditors.TextEdit();
            txbTkanSeb_t3 = new DevExpress.XtraEditors.TextEdit();
            txbNorm_t1 = new DevExpress.XtraEditors.TextEdit();
            articulControl1 = new SewingProduction.Features.Articul.Forms.ArticulControl();
            gridArtDr = new DevExpress.XtraGrid.GridControl();
            bsArtDr = new System.Windows.Forms.BindingSource(components);
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            grArtDrEd = new DevExpress.XtraGrid.Columns.GridColumn();
            artDrSeb = new DevExpress.XtraGrid.Columns.GridColumn();
            grArtDrKol = new DevExpress.XtraGrid.Columns.GridColumn();
            grArtDrSum = new DevExpress.XtraGrid.Columns.GridColumn();
            grArtDrKod_fur = new DevExpress.XtraGrid.Columns.GridColumn();
            grArtDrKod_furn_ar = new DevExpress.XtraGrid.Columns.GridColumn();
            grArtDrData_nitki = new DevExpress.XtraGrid.Columns.GridColumn();
            gridArt = new DevExpress.XtraGrid.GridControl();
            gridControl1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            customTabControlKomplNabor = new CustomTabControl();
            cTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            cGridKomplSost = new CustomGridControl();
            bsSostKompl = new System.Windows.Forms.BindingSource(components);
            gridViewKomplSost = new DevExpress.XtraGrid.Views.Grid.GridView();
            grColKomplSostKod = new DevExpress.XtraGrid.Columns.GridColumn();
            grColKomplSostGrup_k = new DevExpress.XtraGrid.Columns.GridColumn();
            grColKomplSostGrup = new DevExpress.XtraGrid.Columns.GridColumn();
            grColKomplSostArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            grColKomplSostRazm = new DevExpress.XtraGrid.Columns.GridColumn();
            gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            cTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            cGridNaborSost = new CustomGridControl();
            bsSostNabor = new System.Windows.Forms.BindingSource(components);
            gridViewNaborSost = new DevExpress.XtraGrid.Views.Grid.GridView();
            grColNaborSostAg_naimen = new DevExpress.XtraGrid.Columns.GridColumn();
            grColNaborSostTk_name = new DevExpress.XtraGrid.Columns.GridColumn();
            grColNaborSostTat_name = new DevExpress.XtraGrid.Columns.GridColumn();
            grColNaborSostId_gost = new DevExpress.XtraGrid.Columns.GridColumn();
            grColNaborSostName_gost = new DevExpress.XtraGrid.Columns.GridColumn();
            grColNaborSostSostav = new DevExpress.XtraGrid.Columns.GridColumn();
            grColNaborSostRazm = new DevExpress.XtraGrid.Columns.GridColumn();
            txbSeb = new DevExpress.XtraEditors.TextEdit();
            txbNormt = new DevExpress.XtraEditors.TextEdit();
            txbKfKach1 = new DevExpress.XtraEditors.TextEdit();
            txbKfKach2 = new DevExpress.XtraEditors.TextEdit();
            txbKfKach3 = new DevExpress.XtraEditors.TextEdit();
            txbKfKach4 = new DevExpress.XtraEditors.TextEdit();
            txbKfKach5 = new DevExpress.XtraEditors.TextEdit();
            txbKfKach6 = new DevExpress.XtraEditors.TextEdit();
            txbKfKach7 = new DevExpress.XtraEditors.TextEdit();
            txbBrak2 = new DevExpress.XtraEditors.TextEdit();
            txbBrak3 = new DevExpress.XtraEditors.TextEdit();
            txbBrak4 = new DevExpress.XtraEditors.TextEdit();
            txbBrak5 = new DevExpress.XtraEditors.TextEdit();
            txbBrak6 = new DevExpress.XtraEditors.TextEdit();
            txbBrak7 = new DevExpress.XtraEditors.TextEdit();
            sButtodDeleteKod = new DevExpress.XtraEditors.SimpleButton();
            customSimpleButton7 = new CustomSimpleButton();
            btnPublishedArticles = new CustomSimpleButton();
            customButtonKompl = new CustomSimpleButton();
            csButtonNew = new CustomSimpleButton();
            csButtonEdit = new CustomSimpleButton();
            txbKoef = new DevExpress.XtraEditors.TextEdit();
            txbSebProizv = new DevExpress.XtraEditors.TextEdit();
            txbSebRecom = new DevExpress.XtraEditors.TextEdit();
            mtbDateOpis = new CustomMaskedTextBox();
            tkb2 = new DevExpress.XtraEditors.TextEdit();
            tkb3 = new DevExpress.XtraEditors.TextEdit();
            tkb4 = new DevExpress.XtraEditors.TextEdit();
            tkb5 = new DevExpress.XtraEditors.TextEdit();
            tkb6 = new DevExpress.XtraEditors.TextEdit();
            tkb7 = new DevExpress.XtraEditors.TextEdit();
            txbBrak1 = new DevExpress.XtraEditors.TextEdit();
            tkb1 = new DevExpress.XtraEditors.TextEdit();
            txbCalcSebRecom = new DevExpress.XtraEditors.TextEdit();
            textEdit1 = new DevExpress.XtraEditors.TextEdit();
            Root = new LayoutControlGroup();
            layoutControlItem5 = new LayoutControlItem();
            layoutControlGroup10 = new LayoutControlGroup();
            layoutControlGroup11 = new LayoutControlGroup();
            layoutControlGroup6 = new LayoutControlGroup();
            layoutControlItem65 = new LayoutControlItem();
            layoutControlItem66 = new LayoutControlItem();
            layoutControlItem67 = new LayoutControlItem();
            layoutControlItem68 = new LayoutControlItem();
            layoutControlItem69 = new LayoutControlItem();
            layoutControlItem100 = new LayoutControlItem();
            layoutControlItem64 = new LayoutControlItem();
            layoutControlItem70 = new LayoutControlItem();
            layoutControlItem71 = new LayoutControlItem();
            layoutControlItem72 = new LayoutControlItem();
            layoutControlItem74 = new LayoutControlItem();
            layoutControlItem76 = new LayoutControlItem();
            layoutControlItem73 = new LayoutControlItem();
            layoutControlItem75 = new LayoutControlItem();
            layoutControlItem63 = new LayoutControlItem();
            layoutControlItem77 = new LayoutControlItem();
            layoutControlGroup18 = new LayoutControlGroup();
            layoutControlItem48 = new LayoutControlItem();
            layoutControlItem7 = new LayoutControlItem();
            emptySpaceItem1 = new EmptySpaceItem();
            layoutControlGroup16 = new LayoutControlGroup();
            layoutControlItem12 = new LayoutControlItem();
            layoutControlItem13 = new LayoutControlItem();
            layoutControlItem28 = new LayoutControlItem();
            layoutControlItem29 = new LayoutControlItem();
            layoutControlItem30 = new LayoutControlItem();
            layoutControlItem31 = new LayoutControlItem();
            layoutControlItem15 = new LayoutControlItem();
            emptySpaceItem8 = new EmptySpaceItem();
            layoutControlItem16 = new LayoutControlItem();
            emptySpaceItem3 = new EmptySpaceItem();
            layoutControlItem17 = new LayoutControlItem();
            layoutControlItem18 = new LayoutControlItem();
            layoutControlItem19 = new LayoutControlItem();
            layoutControlItem20 = new LayoutControlItem();
            layoutControlItem21 = new LayoutControlItem();
            layoutControlItem22 = new LayoutControlItem();
            layoutControlGroup14 = new LayoutControlGroup();
            layoutControlItem49 = new LayoutControlItem();
            layoutControlItem50 = new LayoutControlItem();
            layoutControlItem51 = new LayoutControlItem();
            layoutControlItem52 = new LayoutControlItem();
            layoutControlItem53 = new LayoutControlItem();
            layoutControlItem54 = new LayoutControlItem();
            layoutControlItem55 = new LayoutControlItem();
            layoutControlGroup19 = new LayoutControlGroup();
            layoutControlItem11 = new LayoutControlItem();
            layoutControlItem86 = new LayoutControlItem();
            layoutControlItem96 = new LayoutControlItem();
            layoutControlItem97 = new LayoutControlItem();
            layoutControlItem98 = new LayoutControlItem();
            layoutControlItem99 = new LayoutControlItem();
            layoutControlItem101 = new LayoutControlItem();
            emptySpaceItem2 = new EmptySpaceItem();
            layoutControlGroup9 = new LayoutControlGroup();
            layoutControlItem6 = new LayoutControlItem();
            layoutControlItem42 = new LayoutControlItem();
            layoutControlItem43 = new LayoutControlItem();
            layoutControlItem44 = new LayoutControlItem();
            layoutControlItem45 = new LayoutControlItem();
            layoutControlItem46 = new LayoutControlItem();
            layoutControlItem47 = new LayoutControlItem();
            emptySpaceItem5 = new EmptySpaceItem();
            emptySpaceItem16 = new EmptySpaceItem();
            layoutControlGroup8 = new LayoutControlGroup();
            layoutControlItem37 = new LayoutControlItem();
            layoutControlItem36 = new LayoutControlItem();
            layoutControlItem38 = new LayoutControlItem();
            layoutControlItem39 = new LayoutControlItem();
            layoutControlItem40 = new LayoutControlItem();
            layoutControlItem41 = new LayoutControlItem();
            emptySpaceItem4 = new EmptySpaceItem();
            layoutControlItem81 = new LayoutControlItem();
            layoutControlItem83 = new LayoutControlItem();
            layoutControlItem85 = new LayoutControlItem();
            layoutControlItem87 = new LayoutControlItem();
            layoutControlItem3 = new LayoutControlItem();
            layoutControlItem94 = new LayoutControlItem();
            layoutControlItem1 = new LayoutControlItem();
            layoutControlItem95 = new LayoutControlItem();
            layoutControlItem82 = new LayoutControlItem();
            emptySpaceItem11 = new EmptySpaceItem();
            layoutControlItem33 = new LayoutControlItem();
            layoutControlItem32 = new LayoutControlItem();
            layoutControlItem35 = new LayoutControlItem();
            layoutControlItem34 = new LayoutControlItem();
            layoutControlItem10 = new LayoutControlItem();
            splitterItem4 = new SplitterItem();
            layoutControlGroup1 = new LayoutControlGroup();
            layoutControlItem2 = new LayoutControlItem();
            layoutControlGroup7 = new LayoutControlGroup();
            layoutControlItem92 = new LayoutControlItem();
            emptySpaceItem13 = new EmptySpaceItem();
            layoutControlItem88 = new LayoutControlItem();
            layoutControlItem93 = new LayoutControlItem();
            layoutControlItem90 = new LayoutControlItem();
            layoutControlItem89 = new LayoutControlItem();
            layoutControlItem80 = new LayoutControlItem();
            layoutControlItem79 = new LayoutControlItem();
            emptySpaceItem12 = new EmptySpaceItem();
            layoutControlItem91 = new LayoutControlItem();
            layoutControlItem4 = new LayoutControlItem();
            splitterItem3 = new SplitterItem();
            customTextBox1 = new CustomTextBox();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            label8 = new System.Windows.Forms.Label();
            layoutControl1 = new LayoutControl();
            layoutControlGroup12 = new LayoutControlGroup();
            lContrBoxMainInfo = new LayoutControl();
            layoutControlGroup5 = new LayoutControlGroup();
            splitterItem1 = new SplitterItem();
            splitterItem2 = new SplitterItem();
            layoutControlGroup3 = new LayoutControlGroup();
            tabbedControlGroup1 = new TabbedControlGroup();
            layoutControlGroup2 = new LayoutControlGroup();
            lCTabsControl = new LayoutControlGroup();
            lCPagesKoplNabor = new LayoutControlGroup();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            layoutControlItemKomplNabor = new LayoutControlItem();
            layoutControlItem78 = new LayoutControlItem();
            layoutControlItem9 = new LayoutControlItem();
            layoutControlGroup15 = new LayoutControlGroup();
            layoutControlItem56 = new LayoutControlItem();
            layoutControlItem57 = new LayoutControlItem();
            layoutControlItem58 = new LayoutControlItem();
            layoutControlItem59 = new LayoutControlItem();
            layoutControlItem60 = new LayoutControlItem();
            layoutControlItem61 = new LayoutControlItem();
            layoutControlItem62 = new LayoutControlItem();
            layoutControlGroup17 = new LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)bsArt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsArticul).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).BeginInit();
            customLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txbBrakAll.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent4.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent5.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent6.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKoefVedDG.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent7.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKoefPr.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSebDop.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSumKomplNum.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSumZarpl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t7.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSumSebRaskr.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t6.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSek.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSumStrVznos.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSebz.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSekVyaz.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSekShv.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSekKr.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSumDopOpl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t5.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t4.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t7.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t6.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t5.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t4.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridArtDr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsArtDr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridArt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customTabControlKomplNabor).BeginInit();
            customTabControlKomplNabor.SuspendLayout();
            cTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cGridKomplSost).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsSostKompl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewKomplSost).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).BeginInit();
            cTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cGridNaborSost).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsSostNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNaborSost).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSeb.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbNormt.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach4.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach5.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach6.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach7.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak4.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak5.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak6.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak7.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbKoef.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSebProizv.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbSebRecom.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tkb2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tkb3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tkb4.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tkb5.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tkb6.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tkb7.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tkb1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txbCalcSebRecom.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem65).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem66).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem67).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem68).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem69).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem100).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem64).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem70).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem71).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem72).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem74).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem76).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem73).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem75).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem63).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem77).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem48).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem29).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem30).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem49).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem50).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem51).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem52).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem53).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem54).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem55).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem86).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem96).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem97).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem98).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem99).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem101).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem42).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem43).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem44).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem45).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem46).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem47).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem37).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem36).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem38).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem39).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem40).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem41).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem81).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem83).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem85).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem87).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem94).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem95).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem82).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem32).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem35).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem34).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem92).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem88).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem93).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem90).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem89).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem80).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem79).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem91).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lContrBoxMainInfo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabbedControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lCTabsControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lCPagesKoplNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemKomplNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem78).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem56).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem57).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem58).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem59).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem60).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem61).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem62).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup17).BeginInit();
            SuspendLayout();
            // 
            // grArtDrName
            // 
            grArtDrName.Caption = "Название";
            grArtDrName.FieldName = "Articul_poln";
            grArtDrName.MinWidth = 23;
            grArtDrName.Name = "grArtDrName";
            grArtDrName.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            grArtDrName.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            grArtDrName.Visible = true;
            grArtDrName.VisibleIndex = 0;
            grArtDrName.Width = 321;
            // 
            // customButtonCopy
            // 
            customButtonCopy.Appearance.Font = new Font("Arial", 10F);
            customButtonCopy.Appearance.Options.UseFont = true;
            customButtonCopy.Location = new Point(290, 433);
            customButtonCopy.Margin = new System.Windows.Forms.Padding(1);
            customButtonCopy.Name = "customButtonCopy";
            customButtonCopy.Size = new Size(142, 26);
            customButtonCopy.StyleController = customLayoutControl1;
            customButtonCopy.TabIndex = 7;
            customButtonCopy.Text = "Копия";
            customButtonCopy.Click += customButtonCopy_Click;
            // 
            // customLayoutControl1
            // 
            customLayoutControl1.Controls.Add(btnArticulEdit);
            customLayoutControl1.Controls.Add(txbBrakAll);
            customLayoutControl1.Controls.Add(txtBrakPercent4);
            customLayoutControl1.Controls.Add(txtBrakPercent5);
            customLayoutControl1.Controls.Add(txtBrakPercent6);
            customLayoutControl1.Controls.Add(txbKoefVedDG);
            customLayoutControl1.Controls.Add(txtBrakPercent7);
            customLayoutControl1.Controls.Add(txtBrakPercent3);
            customLayoutControl1.Controls.Add(txtBrakPercent2);
            customLayoutControl1.Controls.Add(txbKoefPr);
            customLayoutControl1.Controls.Add(txtBrakPercent1);
            customLayoutControl1.Controls.Add(chkPres);
            customLayoutControl1.Controls.Add(txbSebDop);
            customLayoutControl1.Controls.Add(chkStra);
            customLayoutControl1.Controls.Add(txbSumKomplNum);
            customLayoutControl1.Controls.Add(customCheckBox1);
            customLayoutControl1.Controls.Add(chkBus);
            customLayoutControl1.Controls.Add(txbSumZarpl);
            customLayoutControl1.Controls.Add(chkV);
            customLayoutControl1.Controls.Add(txbNorm_t7);
            customLayoutControl1.Controls.Add(chkP);
            customLayoutControl1.Controls.Add(txbSumSebRaskr);
            customLayoutControl1.Controls.Add(chbIsFurnit);
            customLayoutControl1.Controls.Add(txbNorm_t6);
            customLayoutControl1.Controls.Add(chbIsUpak);
            customLayoutControl1.Controls.Add(txbSek);
            customLayoutControl1.Controls.Add(txbSumStrVznos);
            customLayoutControl1.Controls.Add(txbSebz);
            customLayoutControl1.Controls.Add(txbSekVyaz);
            customLayoutControl1.Controls.Add(txbSekShv);
            customLayoutControl1.Controls.Add(txbSekKr);
            customLayoutControl1.Controls.Add(txbSumDopOpl);
            customLayoutControl1.Controls.Add(txbNorm_t5);
            customLayoutControl1.Controls.Add(txbNorm_t4);
            customLayoutControl1.Controls.Add(txbNorm_t2);
            customLayoutControl1.Controls.Add(txbNorm_t3);
            customLayoutControl1.Controls.Add(txbTkanSeb_t7);
            customLayoutControl1.Controls.Add(txbTkanSeb_t6);
            customLayoutControl1.Controls.Add(txbTkanSeb_t5);
            customLayoutControl1.Controls.Add(txbTkanSeb_t1);
            customLayoutControl1.Controls.Add(txbTkanSeb_t4);
            customLayoutControl1.Controls.Add(txbTkanSeb_t2);
            customLayoutControl1.Controls.Add(txbTkanSeb_t3);
            customLayoutControl1.Controls.Add(txbNorm_t1);
            customLayoutControl1.Controls.Add(articulControl1);
            customLayoutControl1.Controls.Add(gridArtDr);
            customLayoutControl1.Controls.Add(gridArt);
            customLayoutControl1.Controls.Add(customTabControlKomplNabor);
            customLayoutControl1.Controls.Add(txbSeb);
            customLayoutControl1.Controls.Add(txbNormt);
            customLayoutControl1.Controls.Add(txbKfKach1);
            customLayoutControl1.Controls.Add(txbKfKach2);
            customLayoutControl1.Controls.Add(txbKfKach3);
            customLayoutControl1.Controls.Add(txbKfKach4);
            customLayoutControl1.Controls.Add(txbKfKach5);
            customLayoutControl1.Controls.Add(txbKfKach6);
            customLayoutControl1.Controls.Add(txbKfKach7);
            customLayoutControl1.Controls.Add(txbBrak2);
            customLayoutControl1.Controls.Add(txbBrak3);
            customLayoutControl1.Controls.Add(txbBrak4);
            customLayoutControl1.Controls.Add(txbBrak5);
            customLayoutControl1.Controls.Add(txbBrak6);
            customLayoutControl1.Controls.Add(txbBrak7);
            customLayoutControl1.Controls.Add(sButtodDeleteKod);
            customLayoutControl1.Controls.Add(customSimpleButton7);
            customLayoutControl1.Controls.Add(btnPublishedArticles);
            customLayoutControl1.Controls.Add(customButtonCopy);
            customLayoutControl1.Controls.Add(customButtonKompl);
            customLayoutControl1.Controls.Add(csButtonNew);
            customLayoutControl1.Controls.Add(csButtonEdit);
            customLayoutControl1.Controls.Add(txbKoef);
            customLayoutControl1.Controls.Add(txbSebProizv);
            customLayoutControl1.Controls.Add(txbSebRecom);
            customLayoutControl1.Controls.Add(mtbDateOpis);
            customLayoutControl1.Controls.Add(tkb2);
            customLayoutControl1.Controls.Add(tkb3);
            customLayoutControl1.Controls.Add(tkb4);
            customLayoutControl1.Controls.Add(tkb5);
            customLayoutControl1.Controls.Add(tkb6);
            customLayoutControl1.Controls.Add(tkb7);
            customLayoutControl1.Controls.Add(txbBrak1);
            customLayoutControl1.Controls.Add(tkb1);
            customLayoutControl1.Controls.Add(txbCalcSebRecom);
            customLayoutControl1.Controls.Add(textEdit1);
            customLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            customLayoutControl1.Font = new Font("Arial", 10F);
            customLayoutControl1.Location = new Point(0, 0);
            customLayoutControl1.Name = "customLayoutControl1";
            customLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(973, 214, 832, 704);
            customLayoutControl1.Root = Root;
            customLayoutControl1.Size = new Size(1519, 770);
            customLayoutControl1.TabIndex = 40;
            customLayoutControl1.Text = "customLayoutControl1";
            // 
            // btnArticulEdit
            // 
            btnArticulEdit.Appearance.Font = new Font("Arial", 10F);
            btnArticulEdit.Appearance.Options.UseFont = true;
            btnArticulEdit.Location = new Point(17, 463);
            btnArticulEdit.Name = "btnArticulEdit";
            btnArticulEdit.Size = new Size(206, 31);
            btnArticulEdit.StyleController = customLayoutControl1;
            btnArticulEdit.TabIndex = 8;
            btnArticulEdit.Text = "Редактирование состава набора ";
            btnArticulEdit.Click += customButton3_Click;
            // 
            // txbBrakAll
            // 
            txbBrakAll.Location = new Point(805, 467);
            txbBrakAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbBrakAll.Name = "txbBrakAll";
            txbBrakAll.Properties.DisplayFormat.FormatString = "F2";
            txbBrakAll.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrakAll.Properties.EditFormat.FormatString = "F2";
            txbBrakAll.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrakAll.Properties.ReadOnly = true;
            txbBrakAll.Size = new Size(235, 20);
            txbBrakAll.StyleController = customLayoutControl1;
            txbBrakAll.TabIndex = 49;
            // 
            // txtBrakPercent4
            // 
            txtBrakPercent4.Location = new Point(1044, 353);
            txtBrakPercent4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtBrakPercent4.Name = "txtBrakPercent4";
            txtBrakPercent4.Properties.DisplayFormat.FormatString = "F2";
            txtBrakPercent4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent4.Properties.EditFormat.FormatString = "F2";
            txtBrakPercent4.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent4.Properties.ReadOnly = true;
            txtBrakPercent4.Size = new Size(69, 20);
            txtBrakPercent4.StyleController = customLayoutControl1;
            txtBrakPercent4.TabIndex = 42;
            // 
            // txtBrakPercent5
            // 
            txtBrakPercent5.Location = new Point(1044, 377);
            txtBrakPercent5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtBrakPercent5.Name = "txtBrakPercent5";
            txtBrakPercent5.Properties.DisplayFormat.FormatString = "F2";
            txtBrakPercent5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent5.Properties.EditFormat.FormatString = "F2";
            txtBrakPercent5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent5.Properties.ReadOnly = true;
            txtBrakPercent5.Size = new Size(69, 20);
            txtBrakPercent5.StyleController = customLayoutControl1;
            txtBrakPercent5.TabIndex = 44;
            // 
            // txtBrakPercent6
            // 
            txtBrakPercent6.Location = new Point(1044, 401);
            txtBrakPercent6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtBrakPercent6.Name = "txtBrakPercent6";
            txtBrakPercent6.Properties.DisplayFormat.FormatString = "F2";
            txtBrakPercent6.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent6.Properties.EditFormat.FormatString = "F2";
            txtBrakPercent6.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent6.Properties.ReadOnly = true;
            txtBrakPercent6.Size = new Size(69, 20);
            txtBrakPercent6.StyleController = customLayoutControl1;
            txtBrakPercent6.TabIndex = 46;
            // 
            // txbKoefVedDG
            // 
            txbKoefVedDG.Location = new Point(1375, 598);
            txbKoefVedDG.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKoefVedDG.Name = "txbKoefVedDG";
            txbKoefVedDG.Properties.Appearance.Font = new Font("Arial", 10F);
            txbKoefVedDG.Properties.Appearance.Options.UseFont = true;
            txbKoefVedDG.Properties.DisplayFormat.FormatString = "F2";
            txbKoefVedDG.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txbKoefVedDG.Properties.EditFormat.FormatString = "F2";
            txbKoefVedDG.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txbKoefVedDG.Properties.ReadOnly = true;
            txbKoefVedDG.Size = new Size(109, 22);
            txbKoefVedDG.StyleController = customLayoutControl1;
            txbKoefVedDG.TabIndex = 78;
            // 
            // txtBrakPercent7
            // 
            txtBrakPercent7.Location = new Point(1044, 425);
            txtBrakPercent7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtBrakPercent7.Name = "txtBrakPercent7";
            txtBrakPercent7.Properties.DisplayFormat.FormatString = "F2";
            txtBrakPercent7.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent7.Properties.EditFormat.FormatString = "F2";
            txtBrakPercent7.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent7.Properties.ReadOnly = true;
            txtBrakPercent7.Size = new Size(69, 20);
            txtBrakPercent7.StyleController = customLayoutControl1;
            txtBrakPercent7.TabIndex = 48;
            // 
            // txtBrakPercent3
            // 
            txtBrakPercent3.Location = new Point(1044, 329);
            txtBrakPercent3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtBrakPercent3.Name = "txtBrakPercent3";
            txtBrakPercent3.Properties.DisplayFormat.FormatString = "F2";
            txtBrakPercent3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent3.Properties.EditFormat.FormatString = "F2";
            txtBrakPercent3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent3.Properties.ReadOnly = true;
            txtBrakPercent3.Size = new Size(69, 20);
            txtBrakPercent3.StyleController = customLayoutControl1;
            txtBrakPercent3.TabIndex = 40;
            // 
            // txtBrakPercent2
            // 
            txtBrakPercent2.Location = new Point(1044, 305);
            txtBrakPercent2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtBrakPercent2.Name = "txtBrakPercent2";
            txtBrakPercent2.Properties.DisplayFormat.FormatString = "f2";
            txtBrakPercent2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent2.Properties.EditFormat.FormatString = "f2";
            txtBrakPercent2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent2.Properties.ReadOnly = true;
            txtBrakPercent2.Size = new Size(69, 20);
            txtBrakPercent2.StyleController = customLayoutControl1;
            txtBrakPercent2.TabIndex = 38;
            // 
            // txbKoefPr
            // 
            txbKoefPr.Location = new Point(1375, 572);
            txbKoefPr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKoefPr.Name = "txbKoefPr";
            txbKoefPr.Properties.Appearance.Font = new Font("Arial", 10F);
            txbKoefPr.Properties.Appearance.Options.UseFont = true;
            txbKoefPr.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txbKoefPr.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txbKoefPr.Properties.ReadOnly = true;
            txbKoefPr.Size = new Size(109, 22);
            txbKoefPr.StyleController = customLayoutControl1;
            txbKoefPr.TabIndex = 77;
            // 
            // txtBrakPercent1
            // 
            txtBrakPercent1.Location = new Point(1044, 281);
            txtBrakPercent1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtBrakPercent1.Name = "txtBrakPercent1";
            txtBrakPercent1.Properties.DisplayFormat.FormatString = "f2";
            txtBrakPercent1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent1.Properties.EditFormat.FormatString = "f2";
            txtBrakPercent1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txtBrakPercent1.Properties.ReadOnly = true;
            txtBrakPercent1.Size = new Size(69, 20);
            txtBrakPercent1.StyleController = customLayoutControl1;
            txtBrakPercent1.TabIndex = 36;
            // 
            // chkPres
            // 
            chkPres.FlatStyle = System.Windows.Forms.FlatStyle.System;
            chkPres.Font = new Font("Arial", 10F);
            chkPres.Location = new Point(1125, 447);
            chkPres.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkPres.Name = "chkPres";
            chkPres.Size = new Size(97, 29);
            chkPres.TabIndex = 64;
            chkPres.Text = "пресс";
            chkPres.UseVisualStyleBackColor = true;
            // 
            // txbSebDop
            // 
            txbSebDop.Location = new Point(1375, 546);
            txbSebDop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSebDop.Name = "txbSebDop";
            txbSebDop.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSebDop.Properties.Appearance.Options.UseFont = true;
            txbSebDop.Properties.DisplayFormat.FormatString = "F2";
            txbSebDop.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSebDop.Properties.EditFormat.FormatString = "F2";
            txbSebDop.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSebDop.Properties.ReadOnly = true;
            txbSebDop.Size = new Size(109, 22);
            txbSebDop.StyleController = customLayoutControl1;
            txbSebDop.TabIndex = 76;
            // 
            // chkStra
            // 
            chkStra.FlatStyle = System.Windows.Forms.FlatStyle.System;
            chkStra.Font = new Font("Arial", 10F);
            chkStra.Location = new Point(1125, 416);
            chkStra.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkStra.Name = "chkStra";
            chkStra.Size = new Size(97, 27);
            chkStra.TabIndex = 63;
            chkStra.Text = "стразы";
            chkStra.UseVisualStyleBackColor = true;
            // 
            // txbSumKomplNum
            // 
            txbSumKomplNum.Location = new Point(1355, 452);
            txbSumKomplNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSumKomplNum.Name = "txbSumKomplNum";
            txbSumKomplNum.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSumKomplNum.Properties.Appearance.Options.UseFont = true;
            txbSumKomplNum.Properties.DisplayFormat.FormatString = "F2";
            txbSumKomplNum.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumKomplNum.Properties.EditFormat.FormatString = "F2";
            txbSumKomplNum.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumKomplNum.Properties.ReadOnly = true;
            txbSumKomplNum.Size = new Size(129, 22);
            txbSumKomplNum.StyleController = customLayoutControl1;
            txbSumKomplNum.TabIndex = 73;
            // 
            // customCheckBox1
            // 
            customCheckBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            customCheckBox1.Font = new Font("Arial", 10F);
            customCheckBox1.Location = new Point(1232, 522);
            customCheckBox1.Name = "customCheckBox1";
            customCheckBox1.Size = new Size(246, 20);
            customCheckBox1.TabIndex = 75;
            customCheckBox1.Text = "Старая модель, на пересчете у экономистов";
            customCheckBox1.UseVisualStyleBackColor = true;
            // 
            // chkBus
            // 
            chkBus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            chkBus.Font = new Font("Arial", 10F);
            chkBus.Location = new Point(1125, 385);
            chkBus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkBus.Name = "chkBus";
            chkBus.Size = new Size(97, 27);
            chkBus.TabIndex = 62;
            chkBus.Text = "бусины";
            chkBus.UseVisualStyleBackColor = true;
            // 
            // txbSumZarpl
            // 
            txbSumZarpl.Location = new Point(1355, 276);
            txbSumZarpl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSumZarpl.Name = "txbSumZarpl";
            txbSumZarpl.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSumZarpl.Properties.Appearance.Options.UseFont = true;
            txbSumZarpl.Properties.DisplayFormat.FormatString = "F2";
            txbSumZarpl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumZarpl.Properties.EditFormat.FormatString = "F2";
            txbSumZarpl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumZarpl.Properties.ReadOnly = true;
            txbSumZarpl.Size = new Size(129, 22);
            txbSumZarpl.StyleController = customLayoutControl1;
            txbSumZarpl.TabIndex = 67;
            // 
            // chkV
            // 
            chkV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            chkV.Font = new Font("Arial", 10F);
            chkV.Location = new Point(1125, 354);
            chkV.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkV.Name = "chkV";
            chkV.Size = new Size(97, 27);
            chkV.TabIndex = 61;
            chkV.Text = "вышивка";
            chkV.UseVisualStyleBackColor = true;
            // 
            // txbNorm_t7
            // 
            txbNorm_t7.Location = new Point(589, 437);
            txbNorm_t7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbNorm_t7.Name = "txbNorm_t7";
            txbNorm_t7.Properties.Appearance.Font = new Font("Arial", 9F);
            txbNorm_t7.Properties.Appearance.Options.UseFont = true;
            txbNorm_t7.Properties.ReadOnly = true;
            txbNorm_t7.Size = new Size(50, 22);
            txbNorm_t7.StyleController = customLayoutControl1;
            txbNorm_t7.TabIndex = 26;
            // 
            // chkP
            // 
            chkP.FlatStyle = System.Windows.Forms.FlatStyle.System;
            chkP.Font = new Font("Arial", 10F);
            chkP.Location = new Point(1125, 322);
            chkP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkP.Name = "chkP";
            chkP.Size = new Size(97, 28);
            chkP.TabIndex = 60;
            chkP.Text = "принт";
            chkP.UseVisualStyleBackColor = true;
            // 
            // txbSumSebRaskr
            // 
            txbSumSebRaskr.Location = new Point(1355, 408);
            txbSumSebRaskr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSumSebRaskr.Name = "txbSumSebRaskr";
            txbSumSebRaskr.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSumSebRaskr.Properties.Appearance.Options.UseFont = true;
            txbSumSebRaskr.Properties.DisplayFormat.FormatString = "F2";
            txbSumSebRaskr.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumSebRaskr.Properties.EditFormat.FormatString = "F2";
            txbSumSebRaskr.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumSebRaskr.Properties.ReadOnly = true;
            txbSumSebRaskr.Size = new Size(129, 22);
            txbSumSebRaskr.StyleController = customLayoutControl1;
            txbSumSebRaskr.TabIndex = 72;
            // 
            // chbIsFurnit
            // 
            chbIsFurnit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            chbIsFurnit.Font = new Font("Arial", 10F);
            chbIsFurnit.Location = new Point(1125, 290);
            chbIsFurnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chbIsFurnit.Name = "chbIsFurnit";
            chbIsFurnit.Size = new Size(97, 28);
            chbIsFurnit.TabIndex = 59;
            chbIsFurnit.Text = "фурнитура";
            chbIsFurnit.UseVisualStyleBackColor = true;
            // 
            // txbNorm_t6
            // 
            txbNorm_t6.Location = new Point(589, 411);
            txbNorm_t6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbNorm_t6.Name = "txbNorm_t6";
            txbNorm_t6.Properties.Appearance.Font = new Font("Arial", 9F);
            txbNorm_t6.Properties.Appearance.Options.UseFont = true;
            txbNorm_t6.Properties.ReadOnly = true;
            txbNorm_t6.Size = new Size(50, 22);
            txbNorm_t6.StyleController = customLayoutControl1;
            txbNorm_t6.TabIndex = 24;
            // 
            // chbIsUpak
            // 
            chbIsUpak.FlatStyle = System.Windows.Forms.FlatStyle.System;
            chbIsUpak.Font = new Font("Arial", 10F);
            chbIsUpak.Location = new Point(1125, 258);
            chbIsUpak.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chbIsUpak.Name = "chbIsUpak";
            chbIsUpak.Size = new Size(97, 28);
            chbIsUpak.TabIndex = 58;
            chbIsUpak.Text = "упаковка";
            chbIsUpak.UseVisualStyleBackColor = true;
            // 
            // txbSek
            // 
            txbSek.Location = new Point(1232, 276);
            txbSek.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSek.Name = "txbSek";
            txbSek.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSek.Properties.Appearance.Options.UseFont = true;
            txbSek.Properties.DisplayFormat.FormatString = "F2";
            txbSek.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSek.Properties.EditFormat.FormatString = "F2";
            txbSek.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSek.Properties.ReadOnly = true;
            txbSek.Size = new Size(119, 22);
            txbSek.StyleController = customLayoutControl1;
            txbSek.TabIndex = 65;
            // 
            // txbSumStrVznos
            // 
            txbSumStrVznos.Location = new Point(1355, 364);
            txbSumStrVznos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSumStrVznos.Name = "txbSumStrVznos";
            txbSumStrVznos.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSumStrVznos.Properties.Appearance.Options.UseFont = true;
            txbSumStrVznos.Properties.DisplayFormat.FormatString = "F2";
            txbSumStrVznos.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumStrVznos.Properties.EditFormat.FormatString = "F2";
            txbSumStrVznos.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumStrVznos.Properties.ReadOnly = true;
            txbSumStrVznos.Size = new Size(129, 22);
            txbSumStrVznos.StyleController = customLayoutControl1;
            txbSumStrVznos.TabIndex = 71;
            // 
            // txbSebz
            // 
            txbSebz.Location = new Point(1355, 496);
            txbSebz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSebz.Name = "txbSebz";
            txbSebz.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSebz.Properties.Appearance.Options.UseFont = true;
            txbSebz.Properties.DisplayFormat.FormatString = "F2";
            txbSebz.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSebz.Properties.EditFormat.FormatString = "F2";
            txbSebz.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSebz.Properties.ReadOnly = true;
            txbSebz.Size = new Size(129, 22);
            txbSebz.StyleController = customLayoutControl1;
            txbSebz.TabIndex = 74;
            // 
            // txbSekVyaz
            // 
            txbSekVyaz.Location = new Point(1258, 354);
            txbSekVyaz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSekVyaz.Name = "txbSekVyaz";
            txbSekVyaz.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSekVyaz.Properties.Appearance.Options.UseFont = true;
            txbSekVyaz.Properties.DisplayFormat.FormatString = "F2";
            txbSekVyaz.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSekVyaz.Properties.EditFormat.FormatString = "F2";
            txbSekVyaz.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSekVyaz.Properties.ReadOnly = true;
            txbSekVyaz.Size = new Size(93, 22);
            txbSekVyaz.StyleController = customLayoutControl1;
            txbSekVyaz.TabIndex = 70;
            // 
            // txbSekShv
            // 
            txbSekShv.Location = new Point(1258, 328);
            txbSekShv.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSekShv.Name = "txbSekShv";
            txbSekShv.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSekShv.Properties.Appearance.Options.UseFont = true;
            txbSekShv.Properties.DisplayFormat.FormatString = "F2";
            txbSekShv.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSekShv.Properties.EditFormat.FormatString = "F2";
            txbSekShv.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSekShv.Properties.ReadOnly = true;
            txbSekShv.Size = new Size(93, 22);
            txbSekShv.StyleController = customLayoutControl1;
            txbSekShv.TabIndex = 66;
            // 
            // txbSekKr
            // 
            txbSekKr.Location = new Point(1256, 302);
            txbSekKr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSekKr.Name = "txbSekKr";
            txbSekKr.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSekKr.Properties.Appearance.Options.UseFont = true;
            txbSekKr.Properties.DisplayFormat.FormatString = "F2";
            txbSekKr.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSekKr.Properties.EditFormat.FormatString = "F2";
            txbSekKr.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSekKr.Properties.ReadOnly = true;
            txbSekKr.Size = new Size(95, 22);
            txbSekKr.StyleController = customLayoutControl1;
            txbSekKr.TabIndex = 68;
            // 
            // txbSumDopOpl
            // 
            txbSumDopOpl.Location = new Point(1355, 320);
            txbSumDopOpl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSumDopOpl.Name = "txbSumDopOpl";
            txbSumDopOpl.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSumDopOpl.Properties.Appearance.Options.UseFont = true;
            txbSumDopOpl.Properties.DisplayFormat.FormatString = "F2";
            txbSumDopOpl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumDopOpl.Properties.EditFormat.FormatString = "F2";
            txbSumDopOpl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSumDopOpl.Properties.ReadOnly = true;
            txbSumDopOpl.Size = new Size(129, 22);
            txbSumDopOpl.StyleController = customLayoutControl1;
            txbSumDopOpl.TabIndex = 69;
            // 
            // txbNorm_t5
            // 
            txbNorm_t5.Location = new Point(589, 385);
            txbNorm_t5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbNorm_t5.Name = "txbNorm_t5";
            txbNorm_t5.Properties.Appearance.Font = new Font("Arial", 9F);
            txbNorm_t5.Properties.Appearance.Options.UseFont = true;
            txbNorm_t5.Properties.ReadOnly = true;
            txbNorm_t5.Size = new Size(50, 22);
            txbNorm_t5.StyleController = customLayoutControl1;
            txbNorm_t5.TabIndex = 22;
            // 
            // txbNorm_t4
            // 
            txbNorm_t4.Location = new Point(589, 359);
            txbNorm_t4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbNorm_t4.Name = "txbNorm_t4";
            txbNorm_t4.Properties.Appearance.Font = new Font("Arial", 9F);
            txbNorm_t4.Properties.Appearance.Options.UseFont = true;
            txbNorm_t4.Properties.ReadOnly = true;
            txbNorm_t4.Size = new Size(50, 22);
            txbNorm_t4.StyleController = customLayoutControl1;
            txbNorm_t4.TabIndex = 20;
            // 
            // txbNorm_t2
            // 
            txbNorm_t2.Location = new Point(589, 307);
            txbNorm_t2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbNorm_t2.Name = "txbNorm_t2";
            txbNorm_t2.Properties.Appearance.Font = new Font("Arial", 9F);
            txbNorm_t2.Properties.Appearance.Options.UseFont = true;
            txbNorm_t2.Properties.ReadOnly = true;
            txbNorm_t2.Size = new Size(50, 22);
            txbNorm_t2.StyleController = customLayoutControl1;
            txbNorm_t2.TabIndex = 16;
            // 
            // txbNorm_t3
            // 
            txbNorm_t3.Location = new Point(589, 333);
            txbNorm_t3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbNorm_t3.Name = "txbNorm_t3";
            txbNorm_t3.Properties.Appearance.Font = new Font("Arial", 9F);
            txbNorm_t3.Properties.Appearance.Options.UseFont = true;
            txbNorm_t3.Properties.ReadOnly = true;
            txbNorm_t3.Size = new Size(50, 22);
            txbNorm_t3.StyleController = customLayoutControl1;
            txbNorm_t3.TabIndex = 18;
            // 
            // txbTkanSeb_t7
            // 
            txbTkanSeb_t7.Location = new Point(658, 437);
            txbTkanSeb_t7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbTkanSeb_t7.Name = "txbTkanSeb_t7";
            txbTkanSeb_t7.Properties.Appearance.Font = new Font("Arial", 9F);
            txbTkanSeb_t7.Properties.Appearance.Options.UseFont = true;
            txbTkanSeb_t7.Properties.DisplayFormat.FormatString = "F2";
            txbTkanSeb_t7.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t7.Properties.EditFormat.FormatString = "F2";
            txbTkanSeb_t7.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t7.Properties.ReadOnly = true;
            txbTkanSeb_t7.Size = new Size(50, 22);
            txbTkanSeb_t7.StyleController = customLayoutControl1;
            txbTkanSeb_t7.TabIndex = 27;
            // 
            // txbTkanSeb_t6
            // 
            txbTkanSeb_t6.Location = new Point(658, 411);
            txbTkanSeb_t6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbTkanSeb_t6.Name = "txbTkanSeb_t6";
            txbTkanSeb_t6.Properties.Appearance.Font = new Font("Arial", 9F);
            txbTkanSeb_t6.Properties.Appearance.Options.UseFont = true;
            txbTkanSeb_t6.Properties.DisplayFormat.FormatString = "F2";
            txbTkanSeb_t6.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t6.Properties.EditFormat.FormatString = "F2";
            txbTkanSeb_t6.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t6.Properties.ReadOnly = true;
            txbTkanSeb_t6.Size = new Size(50, 22);
            txbTkanSeb_t6.StyleController = customLayoutControl1;
            txbTkanSeb_t6.TabIndex = 25;
            // 
            // txbTkanSeb_t5
            // 
            txbTkanSeb_t5.Location = new Point(658, 385);
            txbTkanSeb_t5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbTkanSeb_t5.Name = "txbTkanSeb_t5";
            txbTkanSeb_t5.Properties.Appearance.Font = new Font("Arial", 9F);
            txbTkanSeb_t5.Properties.Appearance.Options.UseFont = true;
            txbTkanSeb_t5.Properties.DisplayFormat.FormatString = "F2";
            txbTkanSeb_t5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t5.Properties.EditFormat.FormatString = "F2";
            txbTkanSeb_t5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t5.Properties.ReadOnly = true;
            txbTkanSeb_t5.Size = new Size(50, 22);
            txbTkanSeb_t5.StyleController = customLayoutControl1;
            txbTkanSeb_t5.TabIndex = 23;
            // 
            // txbTkanSeb_t1
            // 
            txbTkanSeb_t1.Location = new Point(658, 281);
            txbTkanSeb_t1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbTkanSeb_t1.Name = "txbTkanSeb_t1";
            txbTkanSeb_t1.Properties.Appearance.Font = new Font("Arial", 9F);
            txbTkanSeb_t1.Properties.Appearance.Options.UseFont = true;
            txbTkanSeb_t1.Properties.DisplayFormat.FormatString = "F2";
            txbTkanSeb_t1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t1.Properties.EditFormat.FormatString = "F2";
            txbTkanSeb_t1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t1.Properties.ReadOnly = true;
            txbTkanSeb_t1.Size = new Size(50, 22);
            txbTkanSeb_t1.StyleController = customLayoutControl1;
            txbTkanSeb_t1.TabIndex = 15;
            // 
            // txbTkanSeb_t4
            // 
            txbTkanSeb_t4.Location = new Point(658, 359);
            txbTkanSeb_t4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbTkanSeb_t4.Name = "txbTkanSeb_t4";
            txbTkanSeb_t4.Properties.Appearance.Font = new Font("Arial", 9F);
            txbTkanSeb_t4.Properties.Appearance.Options.UseFont = true;
            txbTkanSeb_t4.Properties.DisplayFormat.FormatString = "F2";
            txbTkanSeb_t4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t4.Properties.EditFormat.FormatString = "F2";
            txbTkanSeb_t4.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t4.Properties.ReadOnly = true;
            txbTkanSeb_t4.Size = new Size(50, 22);
            txbTkanSeb_t4.StyleController = customLayoutControl1;
            txbTkanSeb_t4.TabIndex = 21;
            // 
            // txbTkanSeb_t2
            // 
            txbTkanSeb_t2.Location = new Point(658, 307);
            txbTkanSeb_t2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbTkanSeb_t2.Name = "txbTkanSeb_t2";
            txbTkanSeb_t2.Properties.Appearance.Font = new Font("Arial", 9F);
            txbTkanSeb_t2.Properties.Appearance.Options.UseFont = true;
            txbTkanSeb_t2.Properties.DisplayFormat.FormatString = "F2";
            txbTkanSeb_t2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t2.Properties.EditFormat.FormatString = "F2";
            txbTkanSeb_t2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t2.Properties.ReadOnly = true;
            txbTkanSeb_t2.Size = new Size(50, 22);
            txbTkanSeb_t2.StyleController = customLayoutControl1;
            txbTkanSeb_t2.TabIndex = 17;
            // 
            // txbTkanSeb_t3
            // 
            txbTkanSeb_t3.Location = new Point(658, 333);
            txbTkanSeb_t3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbTkanSeb_t3.Name = "txbTkanSeb_t3";
            txbTkanSeb_t3.Properties.Appearance.Font = new Font("Arial", 9F);
            txbTkanSeb_t3.Properties.Appearance.Options.UseFont = true;
            txbTkanSeb_t3.Properties.DisplayFormat.FormatString = "F2";
            txbTkanSeb_t3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t3.Properties.EditFormat.FormatString = "F2";
            txbTkanSeb_t3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbTkanSeb_t3.Properties.ReadOnly = true;
            txbTkanSeb_t3.Size = new Size(50, 22);
            txbTkanSeb_t3.StyleController = customLayoutControl1;
            txbTkanSeb_t3.TabIndex = 19;
            // 
            // txbNorm_t1
            // 
            txbNorm_t1.Location = new Point(589, 281);
            txbNorm_t1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbNorm_t1.Name = "txbNorm_t1";
            txbNorm_t1.Properties.Appearance.Font = new Font("Arial", 9F);
            txbNorm_t1.Properties.Appearance.Options.UseFont = true;
            txbNorm_t1.Properties.ReadOnly = true;
            txbNorm_t1.Size = new Size(50, 22);
            txbNorm_t1.StyleController = customLayoutControl1;
            txbNorm_t1.TabIndex = 14;
            // 
            // articulControl1
            // 
            articulControl1.IsReadOnly = true;
            articulControl1.Location = new Point(461, -118);
            articulControl1.Name = "articulControl1";
            articulControl1.Size = new Size(1029, 324);
            articulControl1.TabIndex = 11;
            // 
            // gridArtDr
            // 
            gridArtDr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gridArtDr.DataSource = bsArtDr;
            gridArtDr.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridArtDr.Location = new Point(464, 514);
            gridArtDr.MainView = gridView1;
            gridArtDr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridArtDr.MinimumSize = new Size(614, 89);
            gridArtDr.Name = "gridArtDr";
            gridArtDr.Size = new Size(761, 226);
            gridArtDr.TabIndex = 84;
            gridArtDr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { grArtDrName, grArtDrEd, artDrSeb, grArtDrKol, grArtDrSum, grArtDrKod_fur, grArtDrKod_furn_ar, grArtDrData_nitki });
            gridView1.DetailHeight = 404;
            gridFormatRule1.ColumnApplyTo = grArtDrName;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.ForeColor = Color.Cyan;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Expression = "1=1";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridView1.FormatRules.Add(gridFormatRule1);
            gridView1.GridControl = gridArtDr;
            gridView1.Name = "gridView1";
            gridView1.OptionsEditForm.PopupEditFormWidth = 933;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(grArtDrName, DevExpress.Data.ColumnSortOrder.Ascending) });
            // 
            // grArtDrEd
            // 
            grArtDrEd.Caption = "Ед.изм.";
            grArtDrEd.FieldName = "T_ed";
            grArtDrEd.MinWidth = 23;
            grArtDrEd.Name = "grArtDrEd";
            grArtDrEd.Visible = true;
            grArtDrEd.VisibleIndex = 1;
            grArtDrEd.Width = 45;
            // 
            // artDrSeb
            // 
            artDrSeb.Caption = "Себ-ть";
            artDrSeb.FieldName = "T_seb";
            artDrSeb.MinWidth = 23;
            artDrSeb.Name = "artDrSeb";
            artDrSeb.Visible = true;
            artDrSeb.VisibleIndex = 2;
            artDrSeb.Width = 66;
            // 
            // grArtDrKol
            // 
            grArtDrKol.Caption = "Кол-во";
            grArtDrKol.FieldName = "Kol";
            grArtDrKol.MinWidth = 23;
            grArtDrKol.Name = "grArtDrKol";
            grArtDrKol.Visible = true;
            grArtDrKol.VisibleIndex = 3;
            grArtDrKol.Width = 49;
            // 
            // grArtDrSum
            // 
            grArtDrSum.Caption = "Сумма";
            grArtDrSum.FieldName = "Sum";
            grArtDrSum.MinWidth = 23;
            grArtDrSum.Name = "grArtDrSum";
            grArtDrSum.Visible = true;
            grArtDrSum.VisibleIndex = 4;
            grArtDrSum.Width = 66;
            // 
            // grArtDrKod_fur
            // 
            grArtDrKod_fur.Caption = "Код фурн";
            grArtDrKod_fur.FieldName = "Kod_fur";
            grArtDrKod_fur.MinWidth = 23;
            grArtDrKod_fur.Name = "grArtDrKod_fur";
            grArtDrKod_fur.Visible = true;
            grArtDrKod_fur.VisibleIndex = 5;
            grArtDrKod_fur.Width = 70;
            // 
            // grArtDrKod_furn_ar
            // 
            grArtDrKod_furn_ar.Caption = "Код арт фурн";
            grArtDrKod_furn_ar.FieldName = "Kod_furn_ar";
            grArtDrKod_furn_ar.MinWidth = 23;
            grArtDrKod_furn_ar.Name = "grArtDrKod_furn_ar";
            grArtDrKod_furn_ar.Visible = true;
            grArtDrKod_furn_ar.VisibleIndex = 6;
            grArtDrKod_furn_ar.Width = 48;
            // 
            // grArtDrData_nitki
            // 
            grArtDrData_nitki.Caption = "Дата нитки";
            grArtDrData_nitki.FieldName = "Data_nitki";
            grArtDrData_nitki.MinWidth = 23;
            grArtDrData_nitki.Name = "grArtDrData_nitki";
            grArtDrData_nitki.Visible = true;
            grArtDrData_nitki.VisibleIndex = 7;
            grArtDrData_nitki.Width = 103;
            // 
            // gridArt
            // 
            gridArt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gridArt.DataSource = bsArt;
            gridArt.EmbeddedNavigator.Appearance.Font = new Font("Arial", 8.25F);
            gridArt.EmbeddedNavigator.Appearance.Options.UseFont = true;
            gridArt.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridArt.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            gridArt.Location = new Point(15, -94);
            gridArt.MainView = gridControl1;
            gridArt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridArt.Name = "gridArt";
            gridArt.Size = new Size(429, 491);
            gridArt.TabIndex = 0;
            gridArt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridControl1, gridView3 });
            // 
            // gridControl1
            // 
            gridControl1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1, gridColumn2, gridColumn3, gridColumn4, gridColumn5, gridColumn6 });
            gridControl1.DetailHeight = 404;
            gridControl1.GridControl = gridArt;
            gridControl1.Name = "gridControl1";
            gridControl1.OptionsBehavior.Editable = false;
            gridControl1.OptionsCustomization.AllowFilter = false;
            gridControl1.OptionsEditForm.PopupEditFormWidth = 933;
            gridControl1.OptionsFind.AlwaysVisible = true;
            gridControl1.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Search;
            gridControl1.OptionsView.ShowAutoFilterRow = true;
            gridControl1.OptionsView.ShowGroupPanel = false;
            gridControl1.FocusedRowChanged += gridControl1_FocusedRowChanged;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Код";
            gridColumn1.FieldName = "Kod";
            gridColumn1.MinWidth = 23;
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Width = 91;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "Группа";
            gridColumn2.FieldName = "Grup";
            gridColumn2.MinWidth = 23;
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.Width = 110;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "Артикул";
            gridColumn3.FieldName = "Articul";
            gridColumn3.MinWidth = 23;
            gridColumn3.Name = "gridColumn3";
            gridColumn3.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 2;
            gridColumn3.Width = 98;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "Размер";
            gridColumn4.FieldName = "Razm";
            gridColumn4.MinWidth = 23;
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 3;
            gridColumn4.Width = 110;
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "Модель";
            gridColumn5.FieldName = "Mod";
            gridColumn5.MinWidth = 23;
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 4;
            gridColumn5.Width = 119;
            // 
            // gridColumn6
            // 
            gridColumn6.Caption = "ТМ";
            gridColumn6.FieldName = "Kle";
            gridColumn6.MinWidth = 23;
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Visible = true;
            gridColumn6.VisibleIndex = 5;
            gridColumn6.Width = 23;
            // 
            // gridView3
            // 
            gridView3.GridControl = gridArt;
            gridView3.Name = "gridView3";
            gridView3.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button;
            // 
            // customTabControlKomplNabor
            // 
            customTabControlKomplNabor.Appearance.Options.UseForeColor = true;
            customTabControlKomplNabor.Location = new Point(15, 512);
            customTabControlKomplNabor.Name = "customTabControlKomplNabor";
            customTabControlKomplNabor.ObjectName = null;
            customTabControlKomplNabor.SelectedTabPage = cTabPage1;
            customTabControlKomplNabor.Size = new Size(429, 237);
            customTabControlKomplNabor.TabIndex = 10;
            customTabControlKomplNabor.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { cTabPage1, cTabPage2 });
            // 
            // cTabPage1
            // 
            cTabPage1.Controls.Add(cGridKomplSost);
            cTabPage1.Name = "cTabPage1";
            cTabPage1.Size = new Size(427, 212);
            cTabPage1.Text = "Состав комплекта";
            // 
            // cGridKomplSost
            // 
            cGridKomplSost.DataSource = bsSostKompl;
            cGridKomplSost.Dock = System.Windows.Forms.DockStyle.Fill;
            cGridKomplSost.Font = new Font("Arial", 10F);
            cGridKomplSost.Location = new Point(0, 0);
            cGridKomplSost.MainView = gridViewKomplSost;
            cGridKomplSost.Name = "cGridKomplSost";
            cGridKomplSost.Size = new Size(427, 212);
            cGridKomplSost.TabIndex = 6;
            cGridKomplSost.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewKomplSost, gridView2 });
            // 
            // gridViewKomplSost
            // 
            gridViewKomplSost.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewKomplSost.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridViewKomplSost.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewKomplSost.Appearance.FocusedRow.Options.UseFont = true;
            gridViewKomplSost.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { grColKomplSostKod, grColKomplSostGrup_k, grColKomplSostGrup, grColKomplSostArticul, grColKomplSostRazm });
            gridViewKomplSost.GridControl = cGridKomplSost;
            gridViewKomplSost.Name = "gridViewKomplSost";
            gridViewKomplSost.OptionsBehavior.Editable = false;
            gridViewKomplSost.OptionsBehavior.ReadOnly = true;
            gridViewKomplSost.OptionsView.EnableAppearanceEvenRow = true;
            gridViewKomplSost.OptionsView.ShowGroupPanel = false;
            // 
            // grColKomplSostKod
            // 
            grColKomplSostKod.Caption = "Код";
            grColKomplSostKod.FieldName = "Kod_n";
            grColKomplSostKod.Name = "grColKomplSostKod";
            grColKomplSostKod.Visible = true;
            grColKomplSostKod.VisibleIndex = 0;
            // 
            // grColKomplSostGrup_k
            // 
            grColKomplSostGrup_k.Caption = "Группа компл";
            grColKomplSostGrup_k.FieldName = "Grup_k";
            grColKomplSostGrup_k.Name = "grColKomplSostGrup_k";
            grColKomplSostGrup_k.Visible = true;
            grColKomplSostGrup_k.VisibleIndex = 1;
            // 
            // grColKomplSostGrup
            // 
            grColKomplSostGrup.Caption = "Группа";
            grColKomplSostGrup.FieldName = "Grup";
            grColKomplSostGrup.Name = "grColKomplSostGrup";
            grColKomplSostGrup.Visible = true;
            grColKomplSostGrup.VisibleIndex = 2;
            // 
            // grColKomplSostArticul
            // 
            grColKomplSostArticul.Caption = "Артикул";
            grColKomplSostArticul.FieldName = "Articul";
            grColKomplSostArticul.Name = "grColKomplSostArticul";
            grColKomplSostArticul.Visible = true;
            grColKomplSostArticul.VisibleIndex = 3;
            // 
            // grColKomplSostRazm
            // 
            grColKomplSostRazm.Caption = "Размер";
            grColKomplSostRazm.FieldName = "Razm";
            grColKomplSostRazm.Name = "grColKomplSostRazm";
            grColKomplSostRazm.Visible = true;
            grColKomplSostRazm.VisibleIndex = 4;
            // 
            // gridView2
            // 
            gridView2.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridView2.Appearance.FocusedRow.Options.UseFont = true;
            gridView2.GridControl = cGridKomplSost;
            gridView2.Name = "gridView2";
            // 
            // cTabPage2
            // 
            cTabPage2.Controls.Add(cGridNaborSost);
            cTabPage2.Name = "cTabPage2";
            cTabPage2.Size = new Size(417, 178);
            cTabPage2.Text = "Состав набора";
            // 
            // cGridNaborSost
            // 
            cGridNaborSost.DataSource = bsSostNabor;
            cGridNaborSost.Dock = System.Windows.Forms.DockStyle.Fill;
            cGridNaborSost.Font = new Font("Arial", 10F);
            cGridNaborSost.Location = new Point(0, 0);
            cGridNaborSost.MainView = gridViewNaborSost;
            cGridNaborSost.Name = "cGridNaborSost";
            cGridNaborSost.Size = new Size(417, 178);
            cGridNaborSost.TabIndex = 1;
            cGridNaborSost.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewNaborSost });
            // 
            // gridViewNaborSost
            // 
            gridViewNaborSost.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewNaborSost.Appearance.FocusedRow.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            gridViewNaborSost.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewNaborSost.Appearance.FocusedRow.Options.UseFont = true;
            gridViewNaborSost.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { grColNaborSostAg_naimen, grColNaborSostTk_name, grColNaborSostTat_name, grColNaborSostId_gost, grColNaborSostName_gost, grColNaborSostSostav, grColNaborSostRazm });
            gridViewNaborSost.GridControl = cGridNaborSost;
            gridViewNaborSost.Name = "gridViewNaborSost";
            gridViewNaborSost.OptionsBehavior.ReadOnly = true;
            gridViewNaborSost.OptionsView.EnableAppearanceEvenRow = true;
            gridViewNaborSost.OptionsView.ShowGroupPanel = false;
            // 
            // grColNaborSostAg_naimen
            // 
            grColNaborSostAg_naimen.Caption = "Группа по ГОСТ";
            grColNaborSostAg_naimen.FieldName = "Ag_name_sokr";
            grColNaborSostAg_naimen.Name = "grColNaborSostAg_naimen";
            grColNaborSostAg_naimen.Visible = true;
            grColNaborSostAg_naimen.VisibleIndex = 0;
            // 
            // grColNaborSostTk_name
            // 
            grColNaborSostTk_name.Caption = "Часть в наборе";
            grColNaborSostTk_name.FieldName = "Tk_name";
            grColNaborSostTk_name.Name = "grColNaborSostTk_name";
            grColNaborSostTk_name.Visible = true;
            grColNaborSostTk_name.VisibleIndex = 1;
            grColNaborSostTk_name.Width = 70;
            // 
            // grColNaborSostTat_name
            // 
            grColNaborSostTat_name.Caption = "Ассортимент";
            grColNaborSostTat_name.FieldName = "Tat_name";
            grColNaborSostTat_name.Name = "grColNaborSostTat_name";
            grColNaborSostTat_name.Visible = true;
            grColNaborSostTat_name.VisibleIndex = 2;
            // 
            // grColNaborSostId_gost
            // 
            grColNaborSostId_gost.Caption = "ГОСТ";
            grColNaborSostId_gost.FieldName = "Id_gost";
            grColNaborSostId_gost.Name = "grColNaborSostId_gost";
            grColNaborSostId_gost.Visible = true;
            grColNaborSostId_gost.VisibleIndex = 3;
            grColNaborSostId_gost.Width = 52;
            // 
            // grColNaborSostName_gost
            // 
            grColNaborSostName_gost.Caption = "Название по ГОСТ";
            grColNaborSostName_gost.FieldName = "Name_gost";
            grColNaborSostName_gost.Name = "grColNaborSostName_gost";
            grColNaborSostName_gost.Visible = true;
            grColNaborSostName_gost.VisibleIndex = 4;
            grColNaborSostName_gost.Width = 97;
            // 
            // grColNaborSostSostav
            // 
            grColNaborSostSostav.Caption = "Состав";
            grColNaborSostSostav.FieldName = "Sostav";
            grColNaborSostSostav.Name = "grColNaborSostSostav";
            grColNaborSostSostav.Visible = true;
            grColNaborSostSostav.VisibleIndex = 5;
            // 
            // grColNaborSostRazm
            // 
            grColNaborSostRazm.Caption = "Размер";
            grColNaborSostRazm.FieldName = "Razm";
            grColNaborSostRazm.Name = "grColNaborSostRazm";
            grColNaborSostRazm.Visible = true;
            grColNaborSostRazm.VisibleIndex = 6;
            // 
            // txbSeb
            // 
            txbSeb.Location = new Point(469, 352);
            txbSeb.Name = "txbSeb";
            txbSeb.Properties.DisplayFormat.FormatString = "F2";
            txbSeb.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSeb.Properties.EditFormat.FormatString = "F2";
            txbSeb.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSeb.Properties.ReadOnly = true;
            txbSeb.Size = new Size(50, 20);
            txbSeb.StyleController = customLayoutControl1;
            txbSeb.TabIndex = 13;
            // 
            // txbNormt
            // 
            txbNormt.Location = new Point(469, 299);
            txbNormt.Name = "txbNormt";
            txbNormt.Properties.Appearance.Font = new Font("Arial", 9F);
            txbNormt.Properties.Appearance.Options.UseFont = true;
            txbNormt.Properties.DisplayFormat.FormatString = "F2";
            txbNormt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txbNormt.Properties.EditFormat.FormatString = "F2";
            txbNormt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txbNormt.Properties.ReadOnly = true;
            txbNormt.Size = new Size(50, 22);
            txbNormt.StyleController = customLayoutControl1;
            txbNormt.TabIndex = 12;
            // 
            // txbKfKach1
            // 
            txbKfKach1.EditValue = "";
            txbKfKach1.Location = new Point(745, 281);
            txbKfKach1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKfKach1.Name = "txbKfKach1";
            txbKfKach1.Properties.DisplayFormat.FormatString = "F2";
            txbKfKach1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach1.Properties.EditFormat.FormatString = "f2";
            txbKfKach1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach1.Properties.NullText = "' '";
            txbKfKach1.Properties.ReadOnly = true;
            txbKfKach1.Size = new Size(50, 20);
            txbKfKach1.StyleController = customLayoutControl1;
            txbKfKach1.TabIndex = 28;
            // 
            // txbKfKach2
            // 
            txbKfKach2.Location = new Point(745, 306);
            txbKfKach2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKfKach2.Name = "txbKfKach2";
            txbKfKach2.Properties.DisplayFormat.FormatString = "F2";
            txbKfKach2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach2.Properties.EditFormat.FormatString = "f2";
            txbKfKach2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach2.Properties.ReadOnly = true;
            txbKfKach2.Size = new Size(50, 20);
            txbKfKach2.StyleController = customLayoutControl1;
            txbKfKach2.TabIndex = 29;
            // 
            // txbKfKach3
            // 
            txbKfKach3.Location = new Point(745, 331);
            txbKfKach3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKfKach3.Name = "txbKfKach3";
            txbKfKach3.Properties.DisplayFormat.FormatString = "F2";
            txbKfKach3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach3.Properties.EditFormat.FormatString = "f2";
            txbKfKach3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach3.Properties.ReadOnly = true;
            txbKfKach3.Size = new Size(50, 20);
            txbKfKach3.StyleController = customLayoutControl1;
            txbKfKach3.TabIndex = 30;
            // 
            // txbKfKach4
            // 
            txbKfKach4.Location = new Point(745, 357);
            txbKfKach4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKfKach4.Name = "txbKfKach4";
            txbKfKach4.Properties.DisplayFormat.FormatString = "F2";
            txbKfKach4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach4.Properties.EditFormat.FormatString = "f2";
            txbKfKach4.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach4.Properties.ReadOnly = true;
            txbKfKach4.Size = new Size(50, 20);
            txbKfKach4.StyleController = customLayoutControl1;
            txbKfKach4.TabIndex = 31;
            // 
            // txbKfKach5
            // 
            txbKfKach5.Location = new Point(745, 382);
            txbKfKach5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKfKach5.Name = "txbKfKach5";
            txbKfKach5.Properties.DisplayFormat.FormatString = "F2";
            txbKfKach5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach5.Properties.EditFormat.FormatString = "f2";
            txbKfKach5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach5.Properties.ReadOnly = true;
            txbKfKach5.Size = new Size(50, 20);
            txbKfKach5.StyleController = customLayoutControl1;
            txbKfKach5.TabIndex = 32;
            // 
            // txbKfKach6
            // 
            txbKfKach6.Location = new Point(745, 408);
            txbKfKach6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKfKach6.Name = "txbKfKach6";
            txbKfKach6.Properties.DisplayFormat.FormatString = "F2";
            txbKfKach6.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach6.Properties.EditFormat.FormatString = "f2";
            txbKfKach6.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach6.Properties.ReadOnly = true;
            txbKfKach6.Size = new Size(50, 20);
            txbKfKach6.StyleController = customLayoutControl1;
            txbKfKach6.TabIndex = 33;
            // 
            // txbKfKach7
            // 
            txbKfKach7.Location = new Point(745, 433);
            txbKfKach7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKfKach7.Name = "txbKfKach7";
            txbKfKach7.Properties.DisplayFormat.FormatString = "F2";
            txbKfKach7.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach7.Properties.EditFormat.FormatString = "f2";
            txbKfKach7.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKfKach7.Properties.ReadOnly = true;
            txbKfKach7.Size = new Size(50, 20);
            txbKfKach7.StyleController = customLayoutControl1;
            txbKfKach7.TabIndex = 34;
            // 
            // txbBrak2
            // 
            txbBrak2.Location = new Point(805, 305);
            txbBrak2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbBrak2.Name = "txbBrak2";
            txbBrak2.Properties.DisplayFormat.FormatString = "F2";
            txbBrak2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak2.Properties.EditFormat.FormatString = "F2";
            txbBrak2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak2.Properties.ReadOnly = true;
            txbBrak2.Size = new Size(235, 20);
            txbBrak2.StyleController = customLayoutControl1;
            txbBrak2.TabIndex = 37;
            // 
            // txbBrak3
            // 
            txbBrak3.Location = new Point(805, 329);
            txbBrak3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbBrak3.Name = "txbBrak3";
            txbBrak3.Properties.DisplayFormat.FormatString = "F2";
            txbBrak3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak3.Properties.EditFormat.FormatString = "F2";
            txbBrak3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak3.Properties.ReadOnly = true;
            txbBrak3.Size = new Size(235, 20);
            txbBrak3.StyleController = customLayoutControl1;
            txbBrak3.TabIndex = 39;
            // 
            // txbBrak4
            // 
            txbBrak4.Location = new Point(805, 353);
            txbBrak4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbBrak4.Name = "txbBrak4";
            txbBrak4.Properties.DisplayFormat.FormatString = "F2";
            txbBrak4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak4.Properties.EditFormat.FormatString = "F2";
            txbBrak4.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak4.Properties.ReadOnly = true;
            txbBrak4.Size = new Size(235, 20);
            txbBrak4.StyleController = customLayoutControl1;
            txbBrak4.TabIndex = 41;
            // 
            // txbBrak5
            // 
            txbBrak5.Location = new Point(805, 377);
            txbBrak5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbBrak5.Name = "txbBrak5";
            txbBrak5.Properties.DisplayFormat.FormatString = "F2";
            txbBrak5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak5.Properties.EditFormat.FormatString = "F2";
            txbBrak5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak5.Properties.ReadOnly = true;
            txbBrak5.Size = new Size(235, 20);
            txbBrak5.StyleController = customLayoutControl1;
            txbBrak5.TabIndex = 43;
            // 
            // txbBrak6
            // 
            txbBrak6.Location = new Point(805, 401);
            txbBrak6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbBrak6.Name = "txbBrak6";
            txbBrak6.Properties.DisplayFormat.FormatString = "F2";
            txbBrak6.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak6.Properties.EditFormat.FormatString = "F2";
            txbBrak6.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak6.Properties.NullText = " ";
            txbBrak6.Properties.ReadOnly = true;
            txbBrak6.Size = new Size(235, 20);
            txbBrak6.StyleController = customLayoutControl1;
            txbBrak6.TabIndex = 45;
            // 
            // txbBrak7
            // 
            txbBrak7.Location = new Point(805, 425);
            txbBrak7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbBrak7.Name = "txbBrak7";
            txbBrak7.Properties.DisplayFormat.FormatString = "F2";
            txbBrak7.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak7.Properties.EditFormat.FormatString = "F2";
            txbBrak7.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak7.Properties.ReadOnly = true;
            txbBrak7.Size = new Size(235, 20);
            txbBrak7.StyleController = customLayoutControl1;
            txbBrak7.TabIndex = 47;
            // 
            // sButtodDeleteKod
            // 
            sButtodDeleteKod.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            sButtodDeleteKod.Appearance.Options.UseFont = true;
            sButtodDeleteKod.Location = new Point(17, 403);
            sButtodDeleteKod.Margin = new System.Windows.Forms.Padding(0);
            sButtodDeleteKod.MaximumSize = new Size(285, 26);
            sButtodDeleteKod.Name = "sButtodDeleteKod";
            sButtodDeleteKod.Size = new Size(133, 26);
            sButtodDeleteKod.StyleController = customLayoutControl1;
            sButtodDeleteKod.TabIndex = 2;
            sButtodDeleteKod.Text = "Удалить";
            sButtodDeleteKod.Click += sButtodDeleteKod_Click;
            // 
            // customSimpleButton7
            // 
            customSimpleButton7.Appearance.Font = new Font("Arial", 10F);
            customSimpleButton7.Appearance.Options.UseFont = true;
            customSimpleButton7.AppearanceDisabled.BackColor = Color.Green;
            customSimpleButton7.AppearanceDisabled.ForeColor = Color.GreenYellow;
            customSimpleButton7.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButton7.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButton7.Location = new Point(290, 403);
            customSimpleButton7.MaximumSize = new Size(285, 26);
            customSimpleButton7.Name = "customSimpleButton7";
            customSimpleButton7.Size = new Size(142, 26);
            customSimpleButton7.StyleController = customLayoutControl1;
            customSimpleButton7.TabIndex = 4;
            customSimpleButton7.Text = "Вшивки";
            customSimpleButton7.Click += customSimpleButton7_Click;
            // 
            // btnPublishedArticles
            // 
            btnPublishedArticles.Appearance.Font = new Font("Arial", 10F);
            btnPublishedArticles.Appearance.Options.UseFont = true;
            btnPublishedArticles.AppearanceDisabled.BackColor = Color.Green;
            btnPublishedArticles.AppearanceDisabled.ForeColor = Color.GreenYellow;
            btnPublishedArticles.AppearanceDisabled.Options.UseBackColor = true;
            btnPublishedArticles.AppearanceDisabled.Options.UseForeColor = true;
            btnPublishedArticles.Location = new Point(154, 433);
            btnPublishedArticles.MaximumSize = new Size(285, 26);
            btnPublishedArticles.Name = "btnPublishedArticles";
            btnPublishedArticles.Size = new Size(132, 26);
            btnPublishedArticles.StyleController = customLayoutControl1;
            btnPublishedArticles.TabIndex = 6;
            btnPublishedArticles.Text = "Опублик.артикулы";
            btnPublishedArticles.Click += btnPublishedArticles_Click;
            // 
            // customButtonKompl
            // 
            customButtonKompl.Appearance.Font = new Font("Arial", 10F);
            customButtonKompl.Appearance.Options.UseFont = true;
            customButtonKompl.Location = new Point(154, 403);
            customButtonKompl.Margin = new System.Windows.Forms.Padding(0);
            customButtonKompl.MaximumSize = new Size(285, 26);
            customButtonKompl.Name = "customButtonKompl";
            customButtonKompl.Size = new Size(132, 26);
            customButtonKompl.StyleController = customLayoutControl1;
            customButtonKompl.TabIndex = 3;
            customButtonKompl.Text = "Комплект";
            customButtonKompl.Click += customButtonKompl_Click;
            // 
            // csButtonNew
            // 
            csButtonNew.Appearance.Font = new Font("Arial", 10F);
            csButtonNew.Appearance.Options.UseFont = true;
            csButtonNew.AppearanceDisabled.BackColor = Color.Green;
            csButtonNew.AppearanceDisabled.ForeColor = Color.GreenYellow;
            csButtonNew.AppearanceDisabled.Options.UseBackColor = true;
            csButtonNew.AppearanceDisabled.Options.UseForeColor = true;
            csButtonNew.Location = new Point(17, 433);
            csButtonNew.MaximumSize = new Size(285, 26);
            csButtonNew.Name = "csButtonNew";
            csButtonNew.Size = new Size(133, 26);
            csButtonNew.StyleController = customLayoutControl1;
            csButtonNew.TabIndex = 5;
            csButtonNew.Text = "Добавить";
            csButtonNew.Click += csButtonNew_Click;
            // 
            // csButtonEdit
            // 
            csButtonEdit.Appearance.Font = new Font("Arial", 10F);
            csButtonEdit.Appearance.Options.UseFont = true;
            csButtonEdit.AppearanceDisabled.BackColor = Color.Green;
            csButtonEdit.AppearanceDisabled.ForeColor = Color.GreenYellow;
            csButtonEdit.AppearanceDisabled.Options.UseBackColor = true;
            csButtonEdit.AppearanceDisabled.Options.UseForeColor = true;
            csButtonEdit.Location = new Point(227, 463);
            csButtonEdit.Name = "csButtonEdit";
            csButtonEdit.Size = new Size(205, 31);
            csButtonEdit.StyleController = customLayoutControl1;
            csButtonEdit.TabIndex = 9;
            csButtonEdit.Text = "Редактировать изделие";
            csButtonEdit.Click += csButtonEdit_Click;
            // 
            // txbKoef
            // 
            txbKoef.Location = new Point(1375, 624);
            txbKoef.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbKoef.Name = "txbKoef";
            txbKoef.Properties.Appearance.Font = new Font("Arial", 10F);
            txbKoef.Properties.Appearance.Options.UseFont = true;
            txbKoef.Properties.DisplayFormat.FormatString = "F2";
            txbKoef.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKoef.Properties.EditFormat.FormatString = "F2";
            txbKoef.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbKoef.Properties.ReadOnly = true;
            txbKoef.Size = new Size(109, 22);
            txbKoef.StyleController = customLayoutControl1;
            txbKoef.TabIndex = 79;
            // 
            // txbSebProizv
            // 
            txbSebProizv.Location = new Point(1375, 650);
            txbSebProizv.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSebProizv.Name = "txbSebProizv";
            txbSebProizv.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSebProizv.Properties.Appearance.Options.UseFont = true;
            txbSebProizv.Properties.DisplayFormat.FormatString = "F2";
            txbSebProizv.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSebProizv.Properties.EditFormat.FormatString = "F2";
            txbSebProizv.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSebProizv.Properties.ReadOnly = true;
            txbSebProizv.Size = new Size(109, 22);
            txbSebProizv.StyleController = customLayoutControl1;
            txbSebProizv.TabIndex = 80;
            // 
            // txbSebRecom
            // 
            txbSebRecom.Location = new Point(1375, 700);
            txbSebRecom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txbSebRecom.Name = "txbSebRecom";
            txbSebRecom.Properties.Appearance.Font = new Font("Arial", 10F);
            txbSebRecom.Properties.Appearance.Options.UseFont = true;
            txbSebRecom.Properties.DisplayFormat.FormatString = "F2";
            txbSebRecom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSebRecom.Properties.EditFormat.FormatString = "F2";
            txbSebRecom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbSebRecom.Properties.ReadOnly = true;
            txbSebRecom.Size = new Size(109, 22);
            txbSebRecom.StyleController = customLayoutControl1;
            txbSebRecom.TabIndex = 82;
            // 
            // mtbDateOpis
            // 
            mtbDateOpis.Font = new Font("Arial", 10F);
            mtbDateOpis.Location = new Point(1375, 726);
            mtbDateOpis.Mask = "00/00/0000";
            mtbDateOpis.Name = "mtbDateOpis";
            mtbDateOpis.ReadOnly = true;
            mtbDateOpis.Size = new Size(109, 20);
            mtbDateOpis.TabIndex = 83;
            mtbDateOpis.ValidatingType = typeof(System.DateTime);
            // 
            // tkb2
            // 
            tkb2.Location = new Point(529, 305);
            tkb2.Name = "tkb2";
            tkb2.Properties.Appearance.Font = new Font("Arial", 10F);
            tkb2.Properties.Appearance.Options.UseFont = true;
            tkb2.Properties.NullText = " ";
            tkb2.Properties.ReadOnly = true;
            tkb2.Size = new Size(50, 22);
            tkb2.StyleController = customLayoutControl1;
            tkb2.TabIndex = 52;
            // 
            // tkb3
            // 
            tkb3.Location = new Point(529, 331);
            tkb3.Name = "tkb3";
            tkb3.Properties.Appearance.Font = new Font("Arial", 10F);
            tkb3.Properties.Appearance.Options.UseFont = true;
            tkb3.Properties.NullText = " ";
            tkb3.Properties.ReadOnly = true;
            tkb3.Size = new Size(50, 22);
            tkb3.StyleController = customLayoutControl1;
            tkb3.TabIndex = 53;
            // 
            // tkb4
            // 
            tkb4.Location = new Point(529, 357);
            tkb4.Name = "tkb4";
            tkb4.Properties.Appearance.Font = new Font("Arial", 10F);
            tkb4.Properties.Appearance.Options.UseFont = true;
            tkb4.Properties.NullText = " ";
            tkb4.Properties.ReadOnly = true;
            tkb4.Size = new Size(50, 22);
            tkb4.StyleController = customLayoutControl1;
            tkb4.TabIndex = 54;
            // 
            // tkb5
            // 
            tkb5.Location = new Point(529, 383);
            tkb5.Name = "tkb5";
            tkb5.Properties.Appearance.Font = new Font("Arial", 10F);
            tkb5.Properties.Appearance.Options.UseFont = true;
            tkb5.Properties.NullText = " ";
            tkb5.Properties.ReadOnly = true;
            tkb5.Size = new Size(50, 22);
            tkb5.StyleController = customLayoutControl1;
            tkb5.TabIndex = 55;
            // 
            // tkb6
            // 
            tkb6.Location = new Point(529, 409);
            tkb6.Name = "tkb6";
            tkb6.Properties.Appearance.Font = new Font("Arial", 10F);
            tkb6.Properties.Appearance.Options.UseFont = true;
            tkb6.Properties.NullText = " ";
            tkb6.Properties.ReadOnly = true;
            tkb6.Size = new Size(50, 22);
            tkb6.StyleController = customLayoutControl1;
            tkb6.TabIndex = 56;
            // 
            // tkb7
            // 
            tkb7.Location = new Point(529, 435);
            tkb7.Name = "tkb7";
            tkb7.Properties.Appearance.Font = new Font("Arial", 10F);
            tkb7.Properties.Appearance.Options.UseFont = true;
            tkb7.Properties.NullText = " ";
            tkb7.Properties.ReadOnly = true;
            tkb7.Size = new Size(50, 22);
            tkb7.StyleController = customLayoutControl1;
            tkb7.TabIndex = 57;
            // 
            // txbBrak1
            // 
            txbBrak1.Location = new Point(805, 281);
            txbBrak1.Name = "txbBrak1";
            txbBrak1.Properties.DisplayFormat.FormatString = "F2";
            txbBrak1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak1.Properties.EditFormat.FormatString = "F2";
            txbBrak1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            txbBrak1.Properties.ReadOnly = true;
            txbBrak1.Size = new Size(235, 20);
            txbBrak1.StyleController = customLayoutControl1;
            txbBrak1.TabIndex = 35;
            // 
            // tkb1
            // 
            tkb1.Location = new Point(529, 281);
            tkb1.Name = "tkb1";
            tkb1.Properties.NullText = " ";
            tkb1.Properties.ReadOnly = true;
            tkb1.Size = new Size(50, 20);
            tkb1.StyleController = customLayoutControl1;
            tkb1.TabIndex = 51;
            // 
            // txbCalcSebRecom
            // 
            txbCalcSebRecom.Location = new Point(1375, 676);
            txbCalcSebRecom.Name = "txbCalcSebRecom";
            txbCalcSebRecom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txbCalcSebRecom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txbCalcSebRecom.Properties.ReadOnly = true;
            txbCalcSebRecom.Size = new Size(109, 20);
            txbCalcSebRecom.StyleController = customLayoutControl1;
            txbCalcSebRecom.TabIndex = 81;
            // 
            // textEdit1
            // 
            textEdit1.Location = new Point(1044, 467);
            textEdit1.Name = "textEdit1";
            textEdit1.Size = new Size(69, 20);
            textEdit1.StyleController = customLayoutControl1;
            textEdit1.TabIndex = 50;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new BaseLayoutItem[] { layoutControlItem5, layoutControlGroup10, splitterItem4, layoutControlGroup1, splitterItem3 });
            Root.Name = "Root";
            Root.Size = new Size(1502, 904);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = articulControl1;
            layoutControlItem5.Location = new Point(449, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(1033, 328);
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup10
            // 
            layoutControlGroup10.AppearanceGroup.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            layoutControlGroup10.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup10.CustomizationFormText = " gridArtDr";
            layoutControlGroup10.Items.AddRange(new BaseLayoutItem[] { layoutControlGroup11, layoutControlGroup9, emptySpaceItem16, layoutControlGroup8, layoutControlItem10 });
            layoutControlGroup10.Location = new Point(449, 328);
            layoutControlGroup10.Name = "layoutControlGroup10";
            layoutControlGroup10.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup10.Size = new Size(1033, 556);
            layoutControlGroup10.Text = " Затраты на изготовление";
            // 
            // layoutControlGroup11
            // 
            layoutControlGroup11.AppearanceGroup.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            layoutControlGroup11.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup11.Items.AddRange(new BaseLayoutItem[] { layoutControlGroup6, layoutControlGroup18, layoutControlGroup16, layoutControlGroup14, layoutControlGroup19 });
            layoutControlGroup11.Location = new Point(0, 0);
            layoutControlGroup11.Name = "layoutControlGroup11";
            layoutControlGroup11.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            layoutControlGroup11.Size = new Size(658, 262);
            layoutControlGroup11.Text = "Ткань/пряжа";
            // 
            // layoutControlGroup6
            // 
            layoutControlGroup6.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup6.Items.AddRange(new BaseLayoutItem[] { layoutControlItem65, layoutControlItem66, layoutControlItem67, layoutControlItem68, layoutControlItem69, layoutControlItem100, layoutControlItem64, layoutControlItem70, layoutControlItem71, layoutControlItem72, layoutControlItem74, layoutControlItem76, layoutControlItem73, layoutControlItem75, layoutControlItem63, layoutControlItem77 });
            layoutControlGroup6.Location = new Point(336, 0);
            layoutControlGroup6.Name = "layoutControlGroup6";
            layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup6.Size = new Size(318, 237);
            layoutControlGroup6.Text = "Брак %";
            // 
            // layoutControlItem65
            // 
            layoutControlItem65.Control = txbBrak3;
            layoutControlItem65.Location = new Point(0, 48);
            layoutControlItem65.MinSize = new Size(54, 24);
            layoutControlItem65.Name = "layoutControlItem65";
            layoutControlItem65.Size = new Size(239, 24);
            layoutControlItem65.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem65.TextVisible = false;
            // 
            // layoutControlItem66
            // 
            layoutControlItem66.Control = txbBrak4;
            layoutControlItem66.Location = new Point(0, 72);
            layoutControlItem66.Name = "layoutControlItem66";
            layoutControlItem66.Size = new Size(239, 24);
            layoutControlItem66.TextVisible = false;
            // 
            // layoutControlItem67
            // 
            layoutControlItem67.Control = txbBrak5;
            layoutControlItem67.Location = new Point(0, 96);
            layoutControlItem67.Name = "layoutControlItem67";
            layoutControlItem67.Size = new Size(239, 24);
            layoutControlItem67.TextVisible = false;
            // 
            // layoutControlItem68
            // 
            layoutControlItem68.Control = txbBrak6;
            layoutControlItem68.Location = new Point(0, 120);
            layoutControlItem68.Name = "layoutControlItem68";
            layoutControlItem68.Size = new Size(239, 24);
            layoutControlItem68.TextVisible = false;
            // 
            // layoutControlItem69
            // 
            layoutControlItem69.Control = txbBrak7;
            layoutControlItem69.Location = new Point(0, 144);
            layoutControlItem69.Name = "layoutControlItem69";
            layoutControlItem69.Size = new Size(239, 24);
            layoutControlItem69.TextVisible = false;
            // 
            // layoutControlItem100
            // 
            layoutControlItem100.Control = txbBrak1;
            layoutControlItem100.Location = new Point(0, 0);
            layoutControlItem100.MinSize = new Size(54, 24);
            layoutControlItem100.Name = "layoutControlItem100";
            layoutControlItem100.Size = new Size(239, 24);
            layoutControlItem100.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem100.TextVisible = false;
            // 
            // layoutControlItem64
            // 
            layoutControlItem64.Control = txbBrak2;
            layoutControlItem64.Location = new Point(0, 24);
            layoutControlItem64.MinSize = new Size(54, 24);
            layoutControlItem64.Name = "layoutControlItem64";
            layoutControlItem64.Size = new Size(239, 24);
            layoutControlItem64.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem64.TextVisible = false;
            // 
            // layoutControlItem70
            // 
            layoutControlItem70.Control = txtBrakPercent1;
            layoutControlItem70.Location = new Point(239, 0);
            layoutControlItem70.MinSize = new Size(54, 24);
            layoutControlItem70.Name = "layoutControlItem70";
            layoutControlItem70.Size = new Size(73, 24);
            layoutControlItem70.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem70.TextVisible = false;
            // 
            // layoutControlItem71
            // 
            layoutControlItem71.Control = txtBrakPercent2;
            layoutControlItem71.Location = new Point(239, 24);
            layoutControlItem71.Name = "layoutControlItem71";
            layoutControlItem71.OptionsTableLayoutItem.RowIndex = 1;
            layoutControlItem71.Size = new Size(73, 24);
            layoutControlItem71.TextVisible = false;
            // 
            // layoutControlItem72
            // 
            layoutControlItem72.Control = txtBrakPercent3;
            layoutControlItem72.Location = new Point(239, 48);
            layoutControlItem72.Name = "layoutControlItem72";
            layoutControlItem72.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem72.OptionsTableLayoutItem.RowIndex = 1;
            layoutControlItem72.Size = new Size(73, 24);
            layoutControlItem72.TextVisible = false;
            // 
            // layoutControlItem74
            // 
            layoutControlItem74.Control = txtBrakPercent5;
            layoutControlItem74.Location = new Point(239, 96);
            layoutControlItem74.Name = "layoutControlItem74";
            layoutControlItem74.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem74.OptionsTableLayoutItem.RowIndex = 2;
            layoutControlItem74.Size = new Size(73, 24);
            layoutControlItem74.TextVisible = false;
            // 
            // layoutControlItem76
            // 
            layoutControlItem76.Control = txtBrakPercent7;
            layoutControlItem76.Location = new Point(239, 144);
            layoutControlItem76.Name = "layoutControlItem76";
            layoutControlItem76.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem76.OptionsTableLayoutItem.RowIndex = 3;
            layoutControlItem76.Size = new Size(73, 24);
            layoutControlItem76.TextVisible = false;
            // 
            // layoutControlItem73
            // 
            layoutControlItem73.Control = txtBrakPercent4;
            layoutControlItem73.Location = new Point(239, 72);
            layoutControlItem73.Name = "layoutControlItem73";
            layoutControlItem73.OptionsTableLayoutItem.RowIndex = 2;
            layoutControlItem73.Size = new Size(73, 24);
            layoutControlItem73.TextVisible = false;
            // 
            // layoutControlItem75
            // 
            layoutControlItem75.Control = txtBrakPercent6;
            layoutControlItem75.Location = new Point(239, 120);
            layoutControlItem75.Name = "layoutControlItem75";
            layoutControlItem75.OptionsTableLayoutItem.RowIndex = 3;
            layoutControlItem75.Size = new Size(73, 24);
            layoutControlItem75.TextVisible = false;
            // 
            // layoutControlItem63
            // 
            layoutControlItem63.Control = textEdit1;
            layoutControlItem63.Location = new Point(239, 168);
            layoutControlItem63.MinSize = new Size(54, 42);
            layoutControlItem63.Name = "layoutControlItem63";
            layoutControlItem63.Size = new Size(73, 42);
            layoutControlItem63.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem63.Text = "Брак, %";
            layoutControlItem63.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem63.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem63.TextSize = new Size(42, 13);
            layoutControlItem63.TextToControlDistance = 5;
            // 
            // layoutControlItem77
            // 
            layoutControlItem77.Control = txbBrakAll;
            layoutControlItem77.Location = new Point(0, 168);
            layoutControlItem77.Name = "layoutControlItem77";
            layoutControlItem77.OptionsTableLayoutItem.RowIndex = 4;
            layoutControlItem77.Size = new Size(239, 42);
            layoutControlItem77.Text = "руб.";
            layoutControlItem77.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem77.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem77.TextSize = new Size(22, 13);
            layoutControlItem77.TextToControlDistance = 5;
            // 
            // layoutControlGroup18
            // 
            layoutControlGroup18.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup18.Items.AddRange(new BaseLayoutItem[] { layoutControlItem48, layoutControlItem7, emptySpaceItem1 });
            layoutControlGroup18.Location = new Point(0, 0);
            layoutControlGroup18.Name = "layoutControlGroup18";
            layoutControlGroup18.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup18.Size = new Size(60, 237);
            layoutControlGroup18.Text = " ";
            // 
            // layoutControlItem48
            // 
            layoutControlItem48.Control = txbNormt;
            layoutControlItem48.Location = new Point(0, 0);
            layoutControlItem48.MinSize = new Size(54, 42);
            layoutControlItem48.Name = "layoutControlItem48";
            layoutControlItem48.Size = new Size(54, 53);
            layoutControlItem48.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem48.Text = "Норма, м.";
            layoutControlItem48.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem48.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem48.TextSize = new Size(48, 13);
            layoutControlItem48.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = txbSeb;
            layoutControlItem7.Location = new Point(0, 53);
            layoutControlItem7.MinSize = new Size(54, 42);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new Size(54, 56);
            layoutControlItem7.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem7.Text = "руб.";
            layoutControlItem7.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem7.TextSize = new Size(22, 13);
            layoutControlItem7.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new Point(0, 109);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(54, 101);
            // 
            // layoutControlGroup16
            // 
            layoutControlGroup16.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup16.Items.AddRange(new BaseLayoutItem[] { layoutControlItem12, layoutControlItem13, layoutControlItem28, layoutControlItem29, layoutControlItem30, layoutControlItem31, layoutControlItem15, emptySpaceItem8, layoutControlItem16, emptySpaceItem3, layoutControlItem17, layoutControlItem18, layoutControlItem19, layoutControlItem20, layoutControlItem21, layoutControlItem22 });
            layoutControlGroup16.Location = new Point(120, 0);
            layoutControlGroup16.Name = "layoutControlGroup16";
            layoutControlGroup16.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup16.Size = new Size(156, 237);
            layoutControlGroup16.Text = " ";
            // 
            // layoutControlItem12
            // 
            layoutControlItem12.Control = txbNorm_t2;
            layoutControlItem12.Location = new Point(0, 26);
            layoutControlItem12.Name = "layoutControlItem12";
            layoutControlItem12.Size = new Size(69, 26);
            layoutControlItem12.Text = "м.";
            layoutControlItem12.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem12.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem12.TextSize = new Size(10, 13);
            layoutControlItem12.TextToControlDistance = 5;
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = txbNorm_t3;
            layoutControlItem13.Location = new Point(0, 52);
            layoutControlItem13.Name = "layoutControlItem13";
            layoutControlItem13.Size = new Size(69, 26);
            layoutControlItem13.Text = "м.";
            layoutControlItem13.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem13.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem13.TextSize = new Size(10, 13);
            layoutControlItem13.TextToControlDistance = 5;
            // 
            // layoutControlItem28
            // 
            layoutControlItem28.Control = txbNorm_t4;
            layoutControlItem28.Location = new Point(0, 78);
            layoutControlItem28.Name = "layoutControlItem28";
            layoutControlItem28.Size = new Size(69, 26);
            layoutControlItem28.Text = "м.";
            layoutControlItem28.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem28.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem28.TextSize = new Size(10, 13);
            layoutControlItem28.TextToControlDistance = 5;
            // 
            // layoutControlItem29
            // 
            layoutControlItem29.Control = txbNorm_t5;
            layoutControlItem29.Location = new Point(0, 104);
            layoutControlItem29.Name = "layoutControlItem29";
            layoutControlItem29.Size = new Size(69, 26);
            layoutControlItem29.Text = "м.";
            layoutControlItem29.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem29.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem29.TextSize = new Size(10, 13);
            layoutControlItem29.TextToControlDistance = 5;
            // 
            // layoutControlItem30
            // 
            layoutControlItem30.Control = txbNorm_t6;
            layoutControlItem30.Location = new Point(0, 130);
            layoutControlItem30.Name = "layoutControlItem30";
            layoutControlItem30.Size = new Size(69, 26);
            layoutControlItem30.Text = "м.";
            layoutControlItem30.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem30.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem30.TextSize = new Size(10, 13);
            layoutControlItem30.TextToControlDistance = 5;
            // 
            // layoutControlItem31
            // 
            layoutControlItem31.Control = txbNorm_t7;
            layoutControlItem31.Location = new Point(0, 156);
            layoutControlItem31.Name = "layoutControlItem31";
            layoutControlItem31.Size = new Size(69, 26);
            layoutControlItem31.Text = "м.";
            layoutControlItem31.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem31.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem31.TextSize = new Size(10, 13);
            layoutControlItem31.TextToControlDistance = 5;
            // 
            // layoutControlItem15
            // 
            layoutControlItem15.Control = txbNorm_t1;
            layoutControlItem15.Location = new Point(0, 0);
            layoutControlItem15.Name = "layoutControlItem15";
            layoutControlItem15.Size = new Size(69, 26);
            layoutControlItem15.Text = "м.";
            layoutControlItem15.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem15.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem15.TextSize = new Size(10, 13);
            layoutControlItem15.TextToControlDistance = 5;
            // 
            // emptySpaceItem8
            // 
            emptySpaceItem8.Location = new Point(0, 182);
            emptySpaceItem8.Name = "emptySpaceItem8";
            emptySpaceItem8.Size = new Size(69, 28);
            // 
            // layoutControlItem16
            // 
            layoutControlItem16.Control = txbTkanSeb_t1;
            layoutControlItem16.Location = new Point(69, 0);
            layoutControlItem16.MinSize = new Size(81, 26);
            layoutControlItem16.Name = "layoutControlItem16";
            layoutControlItem16.Size = new Size(81, 26);
            layoutControlItem16.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem16.Text = "руб.";
            layoutControlItem16.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem16.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem16.TextSize = new Size(22, 13);
            layoutControlItem16.TextToControlDistance = 5;
            // 
            // emptySpaceItem3
            // 
            emptySpaceItem3.Location = new Point(69, 182);
            emptySpaceItem3.Name = "emptySpaceItem3";
            emptySpaceItem3.Size = new Size(81, 28);
            // 
            // layoutControlItem17
            // 
            layoutControlItem17.Control = txbTkanSeb_t2;
            layoutControlItem17.Location = new Point(69, 26);
            layoutControlItem17.MinSize = new Size(81, 26);
            layoutControlItem17.Name = "layoutControlItem17";
            layoutControlItem17.Size = new Size(81, 26);
            layoutControlItem17.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem17.Text = "руб.";
            layoutControlItem17.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem17.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem17.TextSize = new Size(22, 13);
            layoutControlItem17.TextToControlDistance = 5;
            // 
            // layoutControlItem18
            // 
            layoutControlItem18.Control = txbTkanSeb_t3;
            layoutControlItem18.Location = new Point(69, 52);
            layoutControlItem18.MinSize = new Size(81, 26);
            layoutControlItem18.Name = "layoutControlItem18";
            layoutControlItem18.Size = new Size(81, 26);
            layoutControlItem18.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem18.Text = "руб.";
            layoutControlItem18.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem18.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem18.TextSize = new Size(22, 13);
            layoutControlItem18.TextToControlDistance = 5;
            // 
            // layoutControlItem19
            // 
            layoutControlItem19.Control = txbTkanSeb_t4;
            layoutControlItem19.Location = new Point(69, 78);
            layoutControlItem19.MinSize = new Size(81, 26);
            layoutControlItem19.Name = "layoutControlItem19";
            layoutControlItem19.Size = new Size(81, 26);
            layoutControlItem19.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem19.Text = "руб.";
            layoutControlItem19.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem19.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem19.TextSize = new Size(22, 13);
            layoutControlItem19.TextToControlDistance = 5;
            // 
            // layoutControlItem20
            // 
            layoutControlItem20.Control = txbTkanSeb_t5;
            layoutControlItem20.Location = new Point(69, 104);
            layoutControlItem20.MinSize = new Size(81, 26);
            layoutControlItem20.Name = "layoutControlItem20";
            layoutControlItem20.Size = new Size(81, 26);
            layoutControlItem20.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem20.Text = "руб.";
            layoutControlItem20.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem20.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem20.TextSize = new Size(22, 13);
            layoutControlItem20.TextToControlDistance = 5;
            // 
            // layoutControlItem21
            // 
            layoutControlItem21.Control = txbTkanSeb_t6;
            layoutControlItem21.Location = new Point(69, 130);
            layoutControlItem21.MinSize = new Size(81, 26);
            layoutControlItem21.Name = "layoutControlItem21";
            layoutControlItem21.Size = new Size(81, 26);
            layoutControlItem21.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem21.Text = "руб.";
            layoutControlItem21.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem21.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem21.TextSize = new Size(22, 13);
            layoutControlItem21.TextToControlDistance = 5;
            // 
            // layoutControlItem22
            // 
            layoutControlItem22.Control = txbTkanSeb_t7;
            layoutControlItem22.Location = new Point(69, 156);
            layoutControlItem22.MinSize = new Size(81, 26);
            layoutControlItem22.Name = "layoutControlItem22";
            layoutControlItem22.Size = new Size(81, 26);
            layoutControlItem22.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem22.Text = "руб.";
            layoutControlItem22.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem22.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem22.TextSize = new Size(22, 13);
            layoutControlItem22.TextToControlDistance = 5;
            // 
            // layoutControlGroup14
            // 
            layoutControlGroup14.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup14.Items.AddRange(new BaseLayoutItem[] { layoutControlItem49, layoutControlItem50, layoutControlItem51, layoutControlItem52, layoutControlItem53, layoutControlItem54, layoutControlItem55 });
            layoutControlGroup14.Location = new Point(276, 0);
            layoutControlGroup14.Name = "layoutControlGroup14";
            layoutControlGroup14.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup14.Size = new Size(60, 237);
            layoutControlGroup14.Text = "Коэф.кач.пол.";
            // 
            // layoutControlItem49
            // 
            layoutControlItem49.Control = txbKfKach1;
            layoutControlItem49.Location = new Point(0, 0);
            layoutControlItem49.MinSize = new Size(54, 24);
            layoutControlItem49.Name = "layoutControlItem49";
            layoutControlItem49.Size = new Size(54, 25);
            layoutControlItem49.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem49.TextVisible = false;
            // 
            // layoutControlItem50
            // 
            layoutControlItem50.Control = txbKfKach2;
            layoutControlItem50.Location = new Point(0, 25);
            layoutControlItem50.MinSize = new Size(54, 24);
            layoutControlItem50.Name = "layoutControlItem50";
            layoutControlItem50.Size = new Size(54, 25);
            layoutControlItem50.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem50.TextVisible = false;
            // 
            // layoutControlItem51
            // 
            layoutControlItem51.Control = txbKfKach3;
            layoutControlItem51.Location = new Point(0, 50);
            layoutControlItem51.MinSize = new Size(54, 24);
            layoutControlItem51.Name = "layoutControlItem51";
            layoutControlItem51.Size = new Size(54, 26);
            layoutControlItem51.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem51.TextVisible = false;
            // 
            // layoutControlItem52
            // 
            layoutControlItem52.Control = txbKfKach4;
            layoutControlItem52.Location = new Point(0, 76);
            layoutControlItem52.MinSize = new Size(54, 24);
            layoutControlItem52.Name = "layoutControlItem52";
            layoutControlItem52.Size = new Size(54, 25);
            layoutControlItem52.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem52.TextVisible = false;
            // 
            // layoutControlItem53
            // 
            layoutControlItem53.Control = txbKfKach5;
            layoutControlItem53.Location = new Point(0, 101);
            layoutControlItem53.MinSize = new Size(54, 24);
            layoutControlItem53.Name = "layoutControlItem53";
            layoutControlItem53.Size = new Size(54, 26);
            layoutControlItem53.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem53.TextVisible = false;
            // 
            // layoutControlItem54
            // 
            layoutControlItem54.Control = txbKfKach6;
            layoutControlItem54.Location = new Point(0, 127);
            layoutControlItem54.MinSize = new Size(54, 24);
            layoutControlItem54.Name = "layoutControlItem54";
            layoutControlItem54.Size = new Size(54, 25);
            layoutControlItem54.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem54.TextVisible = false;
            // 
            // layoutControlItem55
            // 
            layoutControlItem55.Control = txbKfKach7;
            layoutControlItem55.Location = new Point(0, 152);
            layoutControlItem55.MinSize = new Size(54, 24);
            layoutControlItem55.Name = "layoutControlItem55";
            layoutControlItem55.Size = new Size(54, 58);
            layoutControlItem55.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem55.TextVisible = false;
            // 
            // layoutControlGroup19
            // 
            layoutControlGroup19.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup19.Items.AddRange(new BaseLayoutItem[] { layoutControlItem11, layoutControlItem86, layoutControlItem96, layoutControlItem97, layoutControlItem98, layoutControlItem99, layoutControlItem101, emptySpaceItem2 });
            layoutControlGroup19.Location = new Point(60, 0);
            layoutControlGroup19.Name = "layoutControlGroup19";
            layoutControlGroup19.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup19.Size = new Size(60, 237);
            layoutControlGroup19.Text = " ";
            // 
            // layoutControlItem11
            // 
            layoutControlItem11.Control = tkb2;
            layoutControlItem11.Location = new Point(0, 24);
            layoutControlItem11.MinSize = new Size(54, 26);
            layoutControlItem11.Name = "layoutControlItem11";
            layoutControlItem11.Size = new Size(54, 26);
            layoutControlItem11.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem11.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem11.TextToControlDistance = 0;
            layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem86
            // 
            layoutControlItem86.Control = tkb3;
            layoutControlItem86.Location = new Point(0, 50);
            layoutControlItem86.MinSize = new Size(54, 26);
            layoutControlItem86.Name = "layoutControlItem86";
            layoutControlItem86.Size = new Size(54, 26);
            layoutControlItem86.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem86.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem86.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem86.TextToControlDistance = 0;
            layoutControlItem86.TextVisible = false;
            // 
            // layoutControlItem96
            // 
            layoutControlItem96.Control = tkb4;
            layoutControlItem96.Location = new Point(0, 76);
            layoutControlItem96.MinSize = new Size(54, 26);
            layoutControlItem96.Name = "layoutControlItem96";
            layoutControlItem96.Size = new Size(54, 26);
            layoutControlItem96.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem96.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem96.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem96.TextToControlDistance = 0;
            layoutControlItem96.TextVisible = false;
            // 
            // layoutControlItem97
            // 
            layoutControlItem97.Control = tkb5;
            layoutControlItem97.Location = new Point(0, 102);
            layoutControlItem97.MinSize = new Size(54, 26);
            layoutControlItem97.Name = "layoutControlItem97";
            layoutControlItem97.Size = new Size(54, 26);
            layoutControlItem97.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem97.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem97.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem97.TextToControlDistance = 0;
            layoutControlItem97.TextVisible = false;
            // 
            // layoutControlItem98
            // 
            layoutControlItem98.Control = tkb6;
            layoutControlItem98.Location = new Point(0, 128);
            layoutControlItem98.MinSize = new Size(54, 26);
            layoutControlItem98.Name = "layoutControlItem98";
            layoutControlItem98.Size = new Size(54, 26);
            layoutControlItem98.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem98.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem98.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem98.TextToControlDistance = 0;
            layoutControlItem98.TextVisible = false;
            // 
            // layoutControlItem99
            // 
            layoutControlItem99.Control = tkb7;
            layoutControlItem99.Location = new Point(0, 154);
            layoutControlItem99.MinSize = new Size(54, 26);
            layoutControlItem99.Name = "layoutControlItem99";
            layoutControlItem99.Size = new Size(54, 26);
            layoutControlItem99.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem99.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem99.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem99.TextToControlDistance = 0;
            layoutControlItem99.TextVisible = false;
            // 
            // layoutControlItem101
            // 
            layoutControlItem101.Control = tkb1;
            layoutControlItem101.Location = new Point(0, 0);
            layoutControlItem101.MinSize = new Size(54, 24);
            layoutControlItem101.Name = "layoutControlItem101";
            layoutControlItem101.Size = new Size(54, 24);
            layoutControlItem101.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem101.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.Location = new Point(0, 180);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new Size(54, 30);
            // 
            // layoutControlGroup9
            // 
            layoutControlGroup9.Items.AddRange(new BaseLayoutItem[] { layoutControlItem6, layoutControlItem42, layoutControlItem43, layoutControlItem44, layoutControlItem45, layoutControlItem46, layoutControlItem47, emptySpaceItem5 });
            layoutControlGroup9.Location = new Point(658, 0);
            layoutControlGroup9.Name = "layoutControlGroup9";
            layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup9.Size = new Size(107, 262);
            layoutControlGroup9.Text = "Отделка";
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = chbIsUpak;
            layoutControlItem6.Location = new Point(0, 0);
            layoutControlItem6.MinSize = new Size(24, 24);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new Size(101, 32);
            layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem42
            // 
            layoutControlItem42.Control = chbIsFurnit;
            layoutControlItem42.Location = new Point(0, 32);
            layoutControlItem42.MinSize = new Size(24, 24);
            layoutControlItem42.Name = "layoutControlItem42";
            layoutControlItem42.Size = new Size(101, 32);
            layoutControlItem42.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem42.TextVisible = false;
            // 
            // layoutControlItem43
            // 
            layoutControlItem43.Control = chkP;
            layoutControlItem43.Location = new Point(0, 64);
            layoutControlItem43.MinSize = new Size(24, 24);
            layoutControlItem43.Name = "layoutControlItem43";
            layoutControlItem43.Size = new Size(101, 32);
            layoutControlItem43.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem43.TextVisible = false;
            // 
            // layoutControlItem44
            // 
            layoutControlItem44.Control = chkV;
            layoutControlItem44.Location = new Point(0, 96);
            layoutControlItem44.MinSize = new Size(24, 24);
            layoutControlItem44.Name = "layoutControlItem44";
            layoutControlItem44.Size = new Size(101, 31);
            layoutControlItem44.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem44.TextVisible = false;
            // 
            // layoutControlItem45
            // 
            layoutControlItem45.Control = chkBus;
            layoutControlItem45.Location = new Point(0, 127);
            layoutControlItem45.MinSize = new Size(24, 24);
            layoutControlItem45.Name = "layoutControlItem45";
            layoutControlItem45.Size = new Size(101, 31);
            layoutControlItem45.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem45.TextVisible = false;
            // 
            // layoutControlItem46
            // 
            layoutControlItem46.Control = chkStra;
            layoutControlItem46.Location = new Point(0, 158);
            layoutControlItem46.MinSize = new Size(24, 24);
            layoutControlItem46.Name = "layoutControlItem46";
            layoutControlItem46.Size = new Size(101, 31);
            layoutControlItem46.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem46.TextVisible = false;
            // 
            // layoutControlItem47
            // 
            layoutControlItem47.Control = chkPres;
            layoutControlItem47.Location = new Point(0, 189);
            layoutControlItem47.MinSize = new Size(24, 24);
            layoutControlItem47.Name = "layoutControlItem47";
            layoutControlItem47.Size = new Size(101, 33);
            layoutControlItem47.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem47.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.Location = new Point(0, 222);
            emptySpaceItem5.Name = "emptySpaceItem5";
            emptySpaceItem5.Size = new Size(101, 13);
            // 
            // emptySpaceItem16
            // 
            emptySpaceItem16.Location = new Point(0, 510);
            emptySpaceItem16.Name = "emptySpaceItem16";
            emptySpaceItem16.Size = new Size(765, 19);
            // 
            // layoutControlGroup8
            // 
            layoutControlGroup8.AppearanceGroup.Font = new Font("Arial", 9F, FontStyle.Bold);
            layoutControlGroup8.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup8.Items.AddRange(new BaseLayoutItem[] { layoutControlItem37, layoutControlItem36, layoutControlItem38, layoutControlItem39, layoutControlItem40, layoutControlItem41, emptySpaceItem4, layoutControlItem81, layoutControlItem83, layoutControlItem85, layoutControlItem87, layoutControlItem3, layoutControlItem94, layoutControlItem1, layoutControlItem95, layoutControlItem82, emptySpaceItem11, layoutControlItem33, layoutControlItem32, layoutControlItem35, layoutControlItem34 });
            layoutControlGroup8.Location = new Point(765, 0);
            layoutControlGroup8.Name = "layoutControlGroup8";
            layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup8.Size = new Size(262, 529);
            layoutControlGroup8.Text = "Зарплата";
            // 
            // layoutControlItem37
            // 
            layoutControlItem37.Control = txbSumDopOpl;
            layoutControlItem37.Location = new Point(123, 44);
            layoutControlItem37.Name = "layoutControlItem37";
            layoutControlItem37.Size = new Size(133, 44);
            layoutControlItem37.SizeConstraintsType = SizeConstraintsType.SupportHorzAlignment;
            layoutControlItem37.Text = "Доп. оплата";
            layoutControlItem37.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem37.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem37.TextSize = new Size(63, 13);
            layoutControlItem37.TextToControlDistance = 5;
            // 
            // layoutControlItem36
            // 
            layoutControlItem36.Control = txbSumZarpl;
            layoutControlItem36.Location = new Point(123, 0);
            layoutControlItem36.Name = "layoutControlItem36";
            layoutControlItem36.Size = new Size(133, 44);
            layoutControlItem36.SizeConstraintsType = SizeConstraintsType.SupportHorzAlignment;
            layoutControlItem36.Text = "Зарплата РТ";
            layoutControlItem36.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem36.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem36.TextSize = new Size(63, 13);
            layoutControlItem36.TextToControlDistance = 5;
            // 
            // layoutControlItem38
            // 
            layoutControlItem38.Control = txbSumStrVznos;
            layoutControlItem38.Location = new Point(123, 88);
            layoutControlItem38.Name = "layoutControlItem38";
            layoutControlItem38.Size = new Size(133, 44);
            layoutControlItem38.SizeConstraintsType = SizeConstraintsType.SupportHorzAlignment;
            layoutControlItem38.Text = "Стр. взносы";
            layoutControlItem38.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem38.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem38.TextSize = new Size(62, 13);
            layoutControlItem38.TextToControlDistance = 5;
            // 
            // layoutControlItem39
            // 
            layoutControlItem39.Control = txbSumSebRaskr;
            layoutControlItem39.Location = new Point(123, 132);
            layoutControlItem39.Name = "layoutControlItem39";
            layoutControlItem39.Size = new Size(133, 44);
            layoutControlItem39.SizeConstraintsType = SizeConstraintsType.SupportHorzAlignment;
            layoutControlItem39.Text = "Раскрой";
            layoutControlItem39.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem39.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem39.TextSize = new Size(41, 13);
            layoutControlItem39.TextToControlDistance = 5;
            // 
            // layoutControlItem40
            // 
            layoutControlItem40.Control = txbSumKomplNum;
            layoutControlItem40.Location = new Point(123, 176);
            layoutControlItem40.Name = "layoutControlItem40";
            layoutControlItem40.Size = new Size(133, 44);
            layoutControlItem40.SizeConstraintsType = SizeConstraintsType.SupportHorzAlignment;
            layoutControlItem40.Text = "Компл. и нум.";
            layoutControlItem40.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem40.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem40.TextSize = new Size(69, 13);
            layoutControlItem40.TextToControlDistance = 5;
            // 
            // layoutControlItem41
            // 
            layoutControlItem41.Control = txbSebz;
            layoutControlItem41.Location = new Point(123, 220);
            layoutControlItem41.Name = "layoutControlItem41";
            layoutControlItem41.Size = new Size(133, 44);
            layoutControlItem41.SizeConstraintsType = SizeConstraintsType.SupportHorzAlignment;
            layoutControlItem41.Text = "Зарплата";
            layoutControlItem41.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem41.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem41.TextSize = new Size(48, 13);
            layoutControlItem41.TextToControlDistance = 5;
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.Location = new Point(0, 122);
            emptySpaceItem4.Name = "emptySpaceItem4";
            emptySpaceItem4.Size = new Size(123, 142);
            // 
            // layoutControlItem81
            // 
            layoutControlItem81.Control = customCheckBox1;
            layoutControlItem81.Location = new Point(0, 264);
            layoutControlItem81.MinSize = new Size(24, 24);
            layoutControlItem81.Name = "layoutControlItem81";
            layoutControlItem81.Size = new Size(256, 24);
            layoutControlItem81.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem81.Text = " ";
            layoutControlItem81.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem81.TextLocation = DevExpress.Utils.Locations.Right;
            layoutControlItem81.TextSize = new Size(3, 13);
            layoutControlItem81.TextToControlDistance = 3;
            // 
            // layoutControlItem83
            // 
            layoutControlItem83.Control = txbSebDop;
            layoutControlItem83.Location = new Point(0, 288);
            layoutControlItem83.Name = "layoutControlItem83";
            layoutControlItem83.Size = new Size(256, 26);
            layoutControlItem83.Text = "Пр. затраты";
            layoutControlItem83.TextSize = new Size(131, 13);
            // 
            // layoutControlItem85
            // 
            layoutControlItem85.Control = txbKoefPr;
            layoutControlItem85.Location = new Point(0, 314);
            layoutControlItem85.Name = "layoutControlItem85";
            layoutControlItem85.Size = new Size(256, 26);
            layoutControlItem85.Text = "Коэф. затрат";
            layoutControlItem85.TextSize = new Size(131, 13);
            // 
            // layoutControlItem87
            // 
            layoutControlItem87.Control = txbKoefVedDG;
            layoutControlItem87.Location = new Point(0, 340);
            layoutControlItem87.Name = "layoutControlItem87";
            layoutControlItem87.Size = new Size(256, 26);
            layoutControlItem87.Text = "Коэф.ВЭД и ДГ";
            layoutControlItem87.TextSize = new Size(131, 13);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = txbKoef;
            layoutControlItem3.Location = new Point(0, 366);
            layoutControlItem3.Name = "txbKoef";
            layoutControlItem3.Size = new Size(256, 26);
            layoutControlItem3.Text = "Коэфф. наценки";
            layoutControlItem3.TextSize = new Size(131, 13);
            // 
            // layoutControlItem94
            // 
            layoutControlItem94.Control = txbSebProizv;
            layoutControlItem94.Location = new Point(0, 392);
            layoutControlItem94.Name = "txbSebProizv";
            layoutControlItem94.Size = new Size(256, 26);
            layoutControlItem94.Text = "Произв себ.";
            layoutControlItem94.TextSize = new Size(131, 13);
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txbCalcSebRecom;
            layoutControlItem1.Location = new Point(0, 418);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(256, 24);
            layoutControlItem1.Text = "Расч. реком. цена";
            layoutControlItem1.TextSize = new Size(131, 13);
            // 
            // layoutControlItem95
            // 
            layoutControlItem95.Control = txbSebRecom;
            layoutControlItem95.Location = new Point(0, 442);
            layoutControlItem95.Name = "txbSebRecom";
            layoutControlItem95.Size = new Size(256, 26);
            layoutControlItem95.Text = "Рек. цена";
            layoutControlItem95.TextSize = new Size(131, 13);
            // 
            // layoutControlItem82
            // 
            layoutControlItem82.Control = mtbDateOpis;
            layoutControlItem82.Location = new Point(0, 468);
            layoutControlItem82.Name = "layoutControlItem82";
            layoutControlItem82.Size = new Size(256, 24);
            layoutControlItem82.Text = "Дата расчета";
            layoutControlItem82.TextSize = new Size(131, 13);
            // 
            // emptySpaceItem11
            // 
            emptySpaceItem11.Location = new Point(0, 492);
            emptySpaceItem11.Name = "emptySpaceItem11";
            emptySpaceItem11.Size = new Size(256, 10);
            // 
            // layoutControlItem33
            // 
            layoutControlItem33.Control = txbSekVyaz;
            layoutControlItem33.Location = new Point(0, 96);
            layoutControlItem33.MinSize = new Size(80, 26);
            layoutControlItem33.Name = "layoutControlItem33";
            layoutControlItem33.Size = new Size(123, 26);
            layoutControlItem33.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem33.Text = "вяз.";
            layoutControlItem33.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem33.TextSize = new Size(21, 13);
            layoutControlItem33.TextToControlDistance = 5;
            // 
            // layoutControlItem32
            // 
            layoutControlItem32.Control = txbSek;
            layoutControlItem32.Location = new Point(0, 0);
            layoutControlItem32.MinSize = new Size(54, 26);
            layoutControlItem32.Name = "layoutControlItem32";
            layoutControlItem32.Size = new Size(123, 44);
            layoutControlItem32.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem32.Text = "Норма (сек)";
            layoutControlItem32.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem32.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem32.TextSize = new Size(59, 13);
            layoutControlItem32.TextToControlDistance = 5;
            // 
            // layoutControlItem35
            // 
            layoutControlItem35.Control = txbSekKr;
            layoutControlItem35.Location = new Point(0, 44);
            layoutControlItem35.MinSize = new Size(75, 26);
            layoutControlItem35.Name = "layoutControlItem35";
            layoutControlItem35.Size = new Size(123, 26);
            layoutControlItem35.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem35.Text = "кр. ";
            layoutControlItem35.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem35.TextSize = new Size(19, 13);
            layoutControlItem35.TextToControlDistance = 5;
            // 
            // layoutControlItem34
            // 
            layoutControlItem34.Control = txbSekShv;
            layoutControlItem34.Location = new Point(0, 70);
            layoutControlItem34.MinSize = new Size(77, 26);
            layoutControlItem34.Name = "layoutControlItem34";
            layoutControlItem34.Size = new Size(123, 26);
            layoutControlItem34.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem34.Text = "шв. ";
            layoutControlItem34.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem34.TextSize = new Size(21, 13);
            layoutControlItem34.TextToControlDistance = 5;
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.AppearanceItemCaption.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            layoutControlItem10.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem10.Control = gridArtDr;
            layoutControlItem10.Location = new Point(0, 262);
            layoutControlItem10.Name = "layoutControlItem10";
            layoutControlItem10.Size = new Size(765, 248);
            layoutControlItem10.Text = "    Фурнитура/упаковка";
            layoutControlItem10.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem10.TextSize = new Size(131, 15);
            // 
            // splitterItem4
            // 
            splitterItem4.Location = new Point(439, 0);
            splitterItem4.Name = "splitterItem4";
            splitterItem4.Size = new Size(10, 884);
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Карточка", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("✖ Архив", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Пряжа", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { layoutControlItem2, layoutControlGroup7, layoutControlItem4 });
            layoutControlGroup1.Location = new Point(0, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup1.Size = new Size(439, 874);
            layoutControlGroup1.Text = " ";
            layoutControlGroup1.CustomButtonClick += layoutControlGroup1_CustomButtonClick_1;
            layoutControlGroup1.CustomButtonUnchecked += layoutControlGroup1_CustomButtonUnchecked;
            layoutControlGroup1.CustomButtonChecked += layoutControlGroup1_CustomButtonChecked;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = gridArt;
            layoutControlItem2.Location = new Point(0, 0);
            layoutControlItem2.Name = "gridArt";
            layoutControlItem2.Size = new Size(433, 495);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            layoutControlGroup7.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup7.Items.AddRange(new BaseLayoutItem[] { layoutControlItem92, emptySpaceItem13, layoutControlItem88, layoutControlItem93, layoutControlItem90, layoutControlItem89, layoutControlItem80, layoutControlItem79, emptySpaceItem12, layoutControlItem91 });
            layoutControlGroup7.Location = new Point(0, 495);
            layoutControlGroup7.Name = "layoutControlGroup7";
            layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            layoutControlGroup7.Size = new Size(433, 111);
            layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlItem92
            // 
            layoutControlItem92.Control = customButtonKompl;
            layoutControlItem92.Location = new Point(137, 0);
            layoutControlItem92.MinSize = new Size(67, 30);
            layoutControlItem92.Name = "layoutControlItem92";
            layoutControlItem92.Size = new Size(136, 30);
            layoutControlItem92.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem92.Text = "Комплект";
            layoutControlItem92.TextVisible = false;
            // 
            // emptySpaceItem13
            // 
            emptySpaceItem13.Location = new Point(419, 0);
            emptySpaceItem13.Name = "emptySpaceItem13";
            emptySpaceItem13.Size = new Size(10, 95);
            // 
            // layoutControlItem88
            // 
            layoutControlItem88.Control = sButtodDeleteKod;
            layoutControlItem88.Location = new Point(0, 0);
            layoutControlItem88.MinSize = new Size(62, 26);
            layoutControlItem88.Name = "layoutControlItem88";
            layoutControlItem88.Size = new Size(137, 30);
            layoutControlItem88.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem88.Text = " Удалить";
            layoutControlItem88.TextVisible = false;
            // 
            // layoutControlItem93
            // 
            layoutControlItem93.Control = csButtonNew;
            layoutControlItem93.Location = new Point(0, 30);
            layoutControlItem93.MinSize = new Size(67, 26);
            layoutControlItem93.Name = "layoutControlItem93";
            layoutControlItem93.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem93.OptionsTableLayoutItem.RowIndex = 1;
            layoutControlItem93.Size = new Size(137, 30);
            layoutControlItem93.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem93.Text = " Добавить";
            layoutControlItem93.TextVisible = false;
            // 
            // layoutControlItem90
            // 
            layoutControlItem90.Control = btnPublishedArticles;
            layoutControlItem90.Location = new Point(137, 30);
            layoutControlItem90.MinSize = new Size(121, 30);
            layoutControlItem90.Name = "layoutControlItem90";
            layoutControlItem90.OptionsTableLayoutItem.RowIndex = 1;
            layoutControlItem90.Size = new Size(136, 30);
            layoutControlItem90.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem90.Text = " Опублик артикулы";
            layoutControlItem90.TextVisible = false;
            // 
            // layoutControlItem89
            // 
            layoutControlItem89.Control = customSimpleButton7;
            layoutControlItem89.Location = new Point(273, 0);
            layoutControlItem89.MinSize = new Size(58, 30);
            layoutControlItem89.Name = "layoutControlItem89";
            layoutControlItem89.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem89.OptionsTableLayoutItem.RowIndex = 2;
            layoutControlItem89.Size = new Size(146, 30);
            layoutControlItem89.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem89.Text = " Вшивки";
            layoutControlItem89.TextVisible = false;
            // 
            // layoutControlItem80
            // 
            layoutControlItem80.Control = btnArticulEdit;
            layoutControlItem80.Location = new Point(0, 60);
            layoutControlItem80.MinSize = new Size(210, 26);
            layoutControlItem80.Name = "layoutControlItem80";
            layoutControlItem80.Size = new Size(210, 35);
            layoutControlItem80.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem80.Text = "Редактирование состава набора";
            layoutControlItem80.TextVisible = false;
            // 
            // layoutControlItem79
            // 
            layoutControlItem79.Control = csButtonEdit;
            layoutControlItem79.Location = new Point(210, 60);
            layoutControlItem79.MinSize = new Size(155, 27);
            layoutControlItem79.Name = "layoutControlItem79";
            layoutControlItem79.Size = new Size(209, 35);
            layoutControlItem79.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem79.Text = " Редактировать изделие";
            layoutControlItem79.TextAlignMode = TextAlignModeItem.AutoSize;
            layoutControlItem79.TextToControlDistance = 0;
            layoutControlItem79.TextVisible = false;
            // 
            // emptySpaceItem12
            // 
            emptySpaceItem12.Location = new Point(0, 95);
            emptySpaceItem12.Name = "emptySpaceItem12";
            emptySpaceItem12.Size = new Size(429, 12);
            // 
            // layoutControlItem91
            // 
            layoutControlItem91.Control = customButtonCopy;
            layoutControlItem91.CustomizationFormText = "копия";
            layoutControlItem91.Location = new Point(273, 30);
            layoutControlItem91.MinSize = new Size(50, 25);
            layoutControlItem91.Name = "layoutControlItem91";
            layoutControlItem91.OptionsTableLayoutItem.RowIndex = 2;
            layoutControlItem91.Size = new Size(146, 30);
            layoutControlItem91.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem91.Text = "копия";
            layoutControlItem91.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = customTabControlKomplNabor;
            layoutControlItem4.Location = new Point(0, 606);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(433, 241);
            layoutControlItem4.TextVisible = false;
            // 
            // splitterItem3
            // 
            splitterItem3.Location = new Point(0, 874);
            splitterItem3.Name = "splitterItem3";
            splitterItem3.Size = new Size(439, 10);
            // 
            // customTextBox1
            // 
            customTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            customTextBox1.ErrorColor = Color.Red;
            customTextBox1.ErrorMessage = null;
            customTextBox1.Font = new Font("Arial", 10F);
            customTextBox1.Location = new Point(8, 358);
            customTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.ReadOnly = true;
            customTextBox1.Size = new Size(90, 16);
            customTextBox1.TabIndex = 49;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new Point(218, 242);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(85, 16);
            labelControl1.TabIndex = 51;
            labelControl1.Text = "labelControl1";
            // 
            // label8
            // 
            label8.Location = new Point(0, 0);
            label8.Name = "label8";
            label8.Size = new Size(100, 23);
            label8.TabIndex = 0;
            // 
            // layoutControl1
            // 
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = layoutControlGroup12;
            layoutControl1.Size = new Size(180, 120);
            layoutControl1.TabIndex = 0;
            // 
            // layoutControlGroup12
            // 
            layoutControlGroup12.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup12.GroupBordersVisible = false;
            layoutControlGroup12.Name = "layoutControlGroup12";
            layoutControlGroup12.Size = new Size(180, 120);
            // 
            // lContrBoxMainInfo
            // 
            lContrBoxMainInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lContrBoxMainInfo.BackColor = Color.Transparent;
            lContrBoxMainInfo.Location = new Point(589, 2);
            lContrBoxMainInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lContrBoxMainInfo.Name = "lContrBoxMainInfo";
            lContrBoxMainInfo.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(-650, 6, 650, 400);
            lContrBoxMainInfo.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lContrBoxMainInfo.Root = layoutControlGroup5;
            lContrBoxMainInfo.Size = new Size(1148, 249);
            lContrBoxMainInfo.TabIndex = 31;
            lContrBoxMainInfo.TabStop = false;
            lContrBoxMainInfo.Text = "lContrBoxMainInfo";
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup5.GroupBordersVisible = false;
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Size = new Size(1148, 249);
            // 
            // splitterItem1
            // 
            splitterItem1.Location = new Point(520, 0);
            splitterItem1.Name = "splitterItem1";
            splitterItem1.Size = new Size(6, 321);
            // 
            // splitterItem2
            // 
            splitterItem2.Location = new Point(520, 582);
            splitterItem2.Name = "splitterItem2";
            splitterItem2.Size = new Size(1233, 6);
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Location = new Point(0, 0);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Size = new Size(572, 127);
            layoutControlGroup3.Text = "Состав коплекта";
            // 
            // tabbedControlGroup1
            // 
            tabbedControlGroup1.Location = new Point(0, 207);
            tabbedControlGroup1.Name = "tabbedControlGroup1";
            tabbedControlGroup1.SelectedTabPage = layoutControlGroup2;
            tabbedControlGroup1.Size = new Size(545, 94);
            tabbedControlGroup1.TabPages.AddRange(new BaseLayoutItem[] { layoutControlGroup2 });
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Location = new Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Size = new Size(521, 47);
            // 
            // lCTabsControl
            // 
            lCTabsControl.Location = new Point(0, 0);
            lCTabsControl.Name = "lCPagesKoplNabor";
            lCTabsControl.Size = new Size(572, 127);
            lCTabsControl.Text = "Состав коплекта";
            // 
            // lCPagesKoplNabor
            // 
            lCPagesKoplNabor.Location = new Point(0, 0);
            lCPagesKoplNabor.Name = "lCGSostKompl";
            lCPagesKoplNabor.Size = new Size(572, 127);
            lCPagesKoplNabor.Text = "Состав коплекта";
            // 
            // layoutControlItemKomplNabor
            // 
            layoutControlItemKomplNabor.Control = customTabControlKomplNabor;
            layoutControlItemKomplNabor.Location = new Point(526, 582);
            layoutControlItemKomplNabor.Name = "layoutControlItemKomplNabor";
            layoutControlItemKomplNabor.Size = new Size(1227, 122);
            layoutControlItemKomplNabor.TextVisible = false;
            // 
            // layoutControlItem78
            // 
            layoutControlItem78.Location = new Point(0, 0);
            layoutControlItem78.Name = "layoutControlItem78";
            layoutControlItem78.TextSize = new Size(50, 20);
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = articulControl1;
            layoutControlItem9.Location = new Point(526, 0);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new Size(1227, 321);
            layoutControlItem9.TextVisible = false;
            // 
            // layoutControlGroup15
            // 
            layoutControlGroup15.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup15.Items.AddRange(new BaseLayoutItem[] { layoutControlItem56, layoutControlItem57, layoutControlItem58, layoutControlItem59, layoutControlItem60, layoutControlItem61, layoutControlItem62 });
            layoutControlGroup15.Location = new Point(291, 0);
            layoutControlGroup15.Name = "layoutControlGroup14";
            layoutControlGroup15.Size = new Size(78, 235);
            layoutControlGroup15.Text = "коэф.";
            // 
            // layoutControlItem56
            // 
            layoutControlItem56.Control = txbKfKach1;
            layoutControlItem56.Location = new Point(0, 0);
            layoutControlItem56.Name = "layoutControlItem49";
            layoutControlItem56.Size = new Size(70, 30);
            layoutControlItem56.TextVisible = false;
            // 
            // layoutControlItem57
            // 
            layoutControlItem57.Control = txbKfKach2;
            layoutControlItem57.Location = new Point(0, 30);
            layoutControlItem57.Name = "layoutControlItem50";
            layoutControlItem57.Size = new Size(70, 30);
            layoutControlItem57.TextVisible = false;
            // 
            // layoutControlItem58
            // 
            layoutControlItem58.Control = txbKfKach3;
            layoutControlItem58.Location = new Point(0, 60);
            layoutControlItem58.Name = "layoutControlItem51";
            layoutControlItem58.Size = new Size(70, 30);
            layoutControlItem58.TextVisible = false;
            // 
            // layoutControlItem59
            // 
            layoutControlItem59.Control = txbKfKach4;
            layoutControlItem59.Location = new Point(0, 90);
            layoutControlItem59.Name = "layoutControlItem52";
            layoutControlItem59.Size = new Size(70, 30);
            layoutControlItem59.TextVisible = false;
            // 
            // layoutControlItem60
            // 
            layoutControlItem60.Control = txbKfKach5;
            layoutControlItem60.Location = new Point(0, 120);
            layoutControlItem60.Name = "layoutControlItem53";
            layoutControlItem60.Size = new Size(70, 30);
            layoutControlItem60.TextVisible = false;
            // 
            // layoutControlItem61
            // 
            layoutControlItem61.Control = txbKfKach6;
            layoutControlItem61.Location = new Point(0, 150);
            layoutControlItem61.Name = "layoutControlItem54";
            layoutControlItem61.Size = new Size(70, 30);
            layoutControlItem61.TextVisible = false;
            // 
            // layoutControlItem62
            // 
            layoutControlItem62.Control = txbKfKach7;
            layoutControlItem62.Location = new Point(0, 180);
            layoutControlItem62.Name = "layoutControlItem55";
            layoutControlItem62.Size = new Size(70, 30);
            layoutControlItem62.TextVisible = false;
            // 
            // layoutControlGroup17
            // 
            layoutControlGroup17.Location = new Point(309, 344);
            layoutControlGroup17.Name = "layoutControlGroup17";
            layoutControlGroup17.Size = new Size(104, 284);
            // 
            // Articul
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(1519, 770);
            Controls.Add(customLayoutControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Articul";
            Text = "Справочник изделий";
            FormClosed += Articul_FormClosed;
            Load += Articul_Load;
            ((System.ComponentModel.ISupportInitialize)bsArt).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsArticul).EndInit();
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).EndInit();
            customLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txbBrakAll.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent4.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent5.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent6.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKoefVedDG.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent7.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKoefPr.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBrakPercent1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSebDop.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSumKomplNum.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSumZarpl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t7.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSumSebRaskr.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t6.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSek.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSumStrVznos.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSebz.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSekVyaz.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSekShv.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSekKr.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSumDopOpl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t5.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t4.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t7.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t6.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t5.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t4.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbTkanSeb_t3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbNorm_t1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridArtDr).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsArtDr).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridArt).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)customTabControlKomplNabor).EndInit();
            customTabControlKomplNabor.ResumeLayout(false);
            cTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cGridKomplSost).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsSostKompl).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewKomplSost).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).EndInit();
            cTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cGridNaborSost).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsSostNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNaborSost).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSeb.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbNormt.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach4.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach5.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach6.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKfKach7.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak4.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak5.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak6.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak7.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbKoef.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSebProizv.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbSebRecom.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tkb2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tkb3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tkb4.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tkb5.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tkb6.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tkb7.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbBrak1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tkb1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txbCalcSebRecom.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem65).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem66).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem67).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem68).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem69).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem100).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem64).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem70).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem71).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem72).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem74).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem76).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem73).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem75).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem63).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem77).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup18).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem48).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup16).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem28).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem29).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem30).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem31).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem19).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem20).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup14).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem49).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem50).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem51).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem52).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem53).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem54).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem55).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup19).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem86).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem96).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem97).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem98).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem99).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem101).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem42).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem43).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem44).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem45).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem46).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem47).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem37).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem36).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem38).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem39).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem40).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem41).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem81).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem83).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem85).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem87).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem94).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem95).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem82).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem33).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem32).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem35).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem34).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem92).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem88).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem93).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem90).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem89).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem80).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem79).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem91).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).EndInit();
            ((System.ComponentModel.ISupportInitialize)lContrBoxMainInfo).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabbedControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)lCTabsControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)lCPagesKoplNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemKomplNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem78).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup15).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem56).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem57).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem58).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem59).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem60).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem61).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem62).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup17).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.BindingSource bsArticul;
        private System.Windows.Forms.BindingSource bsArt;
        private DevExpress.XtraEditors.TextEdit txbMod;
        private System.Windows.Forms.PictureBox pictureBoxArticul;
        private CustomLabel label1;
        private CustomLabel label2;
        private CustomLabel label3;
        private CustomLabel customLabel1;
        private DevExpress.XtraEditors.TextEdit txbSeason;
        private CustomLabel customLabel2;
        private DevExpress.XtraEditors.TextEdit txbTM;
        private CustomLabel customLabel3;
        private DevExpress.XtraEditors.TextEdit txbAssort;
        private CustomLabel customLabel4;
        private DevExpress.XtraEditors.TextEdit txbCountry;
        private CustomLabel customLabel5;
        private DevExpress.XtraEditors.TextEdit txbGrupMenName;
        private CustomLabel customLabel6;
        private DevExpress.XtraEditors.TextEdit txbGrup;
        private CustomLabel customLabel8;
        private CustomLabel customLabel7;
        private DevExpress.XtraEditors.TextEdit txbIdGost;
        private CustomLabel customLabel10;
        private DevExpress.XtraEditors.TextEdit txbOpiGost;
        private CustomLabel customLabel9;
        private DevExpress.XtraEditors.TextEdit txbNameGost;
        private CustomLabel customLabel11;
        private DevExpress.XtraEditors.TextEdit txbSost;
        private CustomLabel customLabel13;
        private DevExpress.XtraEditors.TextEdit txbSost3;
        private CustomLabel customLabel12;
        private DevExpress.XtraEditors.TextEdit txbSost2;
        private CustomLabel customLabel15;
        private DevExpress.XtraEditors.TextEdit txbRazmPrint;
        private CustomLabel customLabel14;
        private DevExpress.XtraEditors.TextEdit txbRazm;
        private CustomLabel customLabel16;
        private CustomMaskedTextBox mtbDateOpis;
        private CustomLabel customLabel17;
        private DevExpress.XtraEditors.TextEdit txbScNomer;
        private CustomLabel customLabel18;
        private DevExpress.XtraEditors.TextEdit txbKodTnved;
        private CustomLabel customLabel19;
        private DevExpress.XtraEditors.TextEdit txbNDS;
        private CustomCheckBox chbKombIzd;
        private CustomCheckBox chbKombDet;
        private CustomCheckBox chbArh;
        private CustomSimpleButton customButtonCopy;
        private CustomSimpleButton customButtonKompl;
        private System.Windows.Forms.BindingSource bsArtDr;
        private DevExpress.XtraLayout.LayoutControl lContrBoxMainInfo;
        private DevExpress.XtraGrid.Columns.GridColumn grColKomplSostSost;
        private DevExpress.XtraGrid.Columns.GridColumn grColKomplHasDifference;
        private System.Windows.Forms.BindingSource bsSostKompl;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private CustomSimpleButton csButtonNew;
        private CustomSimpleButton btnPublishedArticles;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private System.Windows.Forms.BindingSource bsSostNabor;
        private DevExpress.XtraLayout.LayoutControlGroup lCTabsControl;
        private DevExpress.XtraLayout.LayoutControlGroup lCPagesKoplNabor;
        private DevExpress.XtraEditors.SimpleButton sButtodDeleteKod;
        private CustomSimpleButton customSimpleButton7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKomplNabor;
        private CustomTabControl customTabControlKomplNabor;
        private DevExpress.XtraTab.XtraTabPage cTabPage1;
        private CustomGridControl cGridKomplSost;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKomplSost;
        private DevExpress.XtraGrid.Columns.GridColumn grColKomplSostKod;
        private DevExpress.XtraGrid.Columns.GridColumn grColKomplSostGrup_k;
        private DevExpress.XtraGrid.Columns.GridColumn grColKomplSostGrup;
        private DevExpress.XtraGrid.Columns.GridColumn grColKomplSostArticul;
        private DevExpress.XtraGrid.Columns.GridColumn grColKomplSostRazm;
        private DevExpress.XtraTab.XtraTabPage cTabPage2;
        private CustomGridControl cGridNaborSost;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNaborSost;
        private DevExpress.XtraGrid.Columns.GridColumn grColNaborSostAg_naimen;
        private DevExpress.XtraGrid.Columns.GridColumn grColNaborSostTk_name;
        private DevExpress.XtraGrid.Columns.GridColumn grColNaborSostTat_name;
        private DevExpress.XtraGrid.Columns.GridColumn grColNaborSostId_gost;
        private DevExpress.XtraGrid.Columns.GridColumn grColNaborSostName_gost;
        private DevExpress.XtraGrid.Columns.GridColumn grColNaborSostSostav;
        private DevExpress.XtraGrid.Columns.GridColumn grColNaborSostRazm;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn grArtDrName;
        private DevExpress.XtraGrid.Columns.GridColumn grArtDrEd;
        private DevExpress.XtraGrid.Columns.GridColumn artDrSeb;
        private DevExpress.XtraGrid.Columns.GridColumn grArtDrKol;
        private DevExpress.XtraGrid.Columns.GridColumn grArtDrSum;
        private DevExpress.XtraGrid.Columns.GridColumn grArtDrKod_fur;
        private DevExpress.XtraGrid.Columns.GridColumn grArtDrKod_furn_ar;
        private DevExpress.XtraGrid.Columns.GridColumn grArtDrData_nitki;
        private DevExpress.XtraGrid.GridControl gridArtDr;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridArt;
        private DevExpress.XtraGrid.Views.Grid.GridView gridControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.TextEdit txbTkanSeb_t7;
        private DevExpress.XtraEditors.TextEdit txbTkanSeb_t6;
        private DevExpress.XtraEditors.TextEdit txbTkanSeb_t1;
        private DevExpress.XtraEditors.TextEdit txbTkanSeb_t5;
        private DevExpress.XtraEditors.TextEdit txbTkanSeb_t2;
        private DevExpress.XtraEditors.TextEdit txbTkanSeb_t4;
        private DevExpress.XtraEditors.TextEdit txbTkanSeb_t3;
        private DevExpress.XtraEditors.TextEdit txbNorm_t7;
        private DevExpress.XtraEditors.TextEdit txbNorm_t6;
        private DevExpress.XtraEditors.TextEdit txbNorm_t1;
        private DevExpress.XtraEditors.TextEdit txbNorm_t5;
        private DevExpress.XtraEditors.TextEdit txbNorm_t2;
        private DevExpress.XtraEditors.TextEdit txbNorm_t4;
        private DevExpress.XtraEditors.TextEdit txbNorm_t3;
        private CustomLayoutControl customLayoutControl1;
        private Forms.ArticulControl articulControl1;
        private CustomSimpleButton btnArticulEdit;
        private CustomTextBox customTextBox1;
        private DevExpress.XtraEditors.TextEdit txbSebRecom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txbKoef;
        private DevExpress.XtraEditors.TextEdit txbSebProizv;
        private DevExpress.XtraEditors.TextEdit txbKoefVedDG;
        private DevExpress.XtraEditors.TextEdit txbKoefPr;
        private DevExpress.XtraEditors.TextEdit txbSebDop;
        private CustomCheckBox customCheckBox1;
        private DevExpress.XtraEditors.TextEdit txbSumZarpl;
        private DevExpress.XtraEditors.TextEdit txbSumKomplNum;
        private DevExpress.XtraEditors.TextEdit txbSek;
        private DevExpress.XtraEditors.TextEdit txbSekVyaz;
        private DevExpress.XtraEditors.TextEdit txbSumSebRaskr;
        private DevExpress.XtraEditors.TextEdit txbSekShv;
        private DevExpress.XtraEditors.TextEdit txbSekKr;
        private DevExpress.XtraEditors.TextEdit txbSumStrVznos;
        private DevExpress.XtraEditors.TextEdit txbSumDopOpl;
        private DevExpress.XtraEditors.TextEdit txbSebz;
        private CustomCheckBox chkPres;
        private CustomCheckBox chkStra;
        private CustomCheckBox chkBus;
        private CustomCheckBox chkV;
        private CustomCheckBox chkP;
        private CustomCheckBox chbIsFurnit;
        private CustomCheckBox chbIsUpak;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private DevExpress.XtraEditors.TextEdit txbSeb;
        private DevExpress.XtraEditors.TextEdit txbNormt;
        private DevExpress.XtraEditors.TextEdit txbKfKach1;
        private DevExpress.XtraEditors.TextEdit txbKfKach2;
        private DevExpress.XtraEditors.TextEdit txbKfKach3;
        private DevExpress.XtraEditors.TextEdit txbKfKach4;
        private DevExpress.XtraEditors.TextEdit txbKfKach5;
        private DevExpress.XtraEditors.TextEdit txbKfKach6;
        private DevExpress.XtraEditors.TextEdit txbKfKach7;
        private DevExpress.XtraEditors.TextEdit txbBrak2;
        private DevExpress.XtraEditors.TextEdit txbBrak3;
        private DevExpress.XtraEditors.TextEdit txbBrak4;
        private DevExpress.XtraEditors.TextEdit txbBrak5;
        private DevExpress.XtraEditors.TextEdit txbBrak6;
        private DevExpress.XtraEditors.TextEdit txbBrak7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem37;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem42;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem43;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem44;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem45;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem46;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem47;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem49;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem51;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem52;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem53;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem54;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem55;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem64;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem65;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem66;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem67;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem68;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem69;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem56;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem57;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem58;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem59;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem60;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem61;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem62;
        private DevExpress.XtraEditors.TextEdit textEdit5;
        private DevExpress.XtraEditors.TextEdit textEdit6;
        private DevExpress.XtraEditors.TextEdit textEdit7;
        private DevExpress.XtraEditors.TextEdit textEdit8;
        private DevExpress.XtraEditors.TextEdit textEdit4;
        private DevExpress.XtraEditors.TextEdit txbBrakAll;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup16;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem48;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem78;
        private CustomSimpleButton csButtonEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem79;
        private DevExpress.XtraEditors.TextEdit txtBrakPercent3;
        private DevExpress.XtraEditors.TextEdit txtBrakPercent1;
        private DevExpress.XtraEditors.TextEdit txtBrakPercent4;
        private DevExpress.XtraEditors.TextEdit txtBrakPercent5;
        private DevExpress.XtraEditors.TextEdit txtBrakPercent6;
        private DevExpress.XtraEditors.TextEdit txtBrakPercent7;
        private DevExpress.XtraEditors.TextEdit txtBrakPercent2;
        private LayoutControlItem layoutControlItem80;
        private LayoutControlItem layoutControlItem81;
        private LayoutControlItem layoutControlItem83;
        private LayoutControlItem layoutControlItem85;
        private LayoutControlItem layoutControlItem87;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private LayoutControlGroup layoutControlGroup12;
        private SplitterItem splitterItem4;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem94;
        private LayoutControlItem layoutControlItem95;
        private EmptySpaceItem emptySpaceItem1;
        private LayoutControlItem layoutControlItem88;
        private LayoutControlItem layoutControlItem91;
        private LayoutControlItem layoutControlItem89;
        private LayoutControlItem layoutControlItem90;
        private LayoutControlItem layoutControlItem93;
        private LayoutControlItem layoutControlItem92;
   //     private DevExpress.XtraEditors.TextEdit textEdit1;
        private LayoutControlItem layoutControlItem82;
        private DevExpress.XtraEditors.TextEdit tkb2;
        private DevExpress.XtraEditors.TextEdit tkb3;
        private DevExpress.XtraEditors.TextEdit tkb4;
        private LayoutControlGroup layoutControlGroup19;
        private DevExpress.XtraEditors.TextEdit tkb5;
        private DevExpress.XtraEditors.TextEdit tkb6;
        private DevExpress.XtraEditors.TextEdit tkb7;
        private LayoutControlItem layoutControlItem11;
        private LayoutControlItem layoutControlItem86;
        private LayoutControlItem layoutControlItem96;
        private LayoutControlItem layoutControlItem97;
        private LayoutControlItem layoutControlItem98;
        private LayoutControlItem layoutControlItem99;
        private DevExpress.XtraEditors.TextEdit txbBrak1;
        private LayoutControlItem layoutControlItem100;
        private LayoutControlItem layoutControlItem70;
        private LayoutControlItem layoutControlItem71;
        private LayoutControlItem layoutControlItem72;
        private LayoutControlItem layoutControlItem74;
        private LayoutControlItem layoutControlItem76;
        private LayoutControlItem layoutControlItem73;
        private LayoutControlItem layoutControlItem75;
        private LayoutControlItem layoutControlItem77;
        private DevExpress.XtraEditors.TextEdit tkb1;
        private LayoutControlItem layoutControlItem101;
        private EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.TextEdit txbCalcSebRecom;
        private LayoutControlItem layoutControlItem1;
        private EmptySpaceItem emptySpaceItem11;
        private EmptySpaceItem emptySpaceItem13;
        private LayoutControlGroup layoutControlGroup7;
        private LayoutControlGroup layoutControlGroup11;
        private EmptySpaceItem emptySpaceItem12;
        private EmptySpaceItem emptySpaceItem5;
        private EmptySpaceItem emptySpaceItem16;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private LayoutControlItem layoutControlItem63;
        private EmptySpaceItem emptySpaceItem2;
    }
}