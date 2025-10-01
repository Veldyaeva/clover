using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.form.TeamWork;
using SewingProduction.Models;
using System;
using System.Windows.Forms;
using SewingProduction.Core.Class;

namespace SewingProduction.Features.TeamWork.Forms
{
    partial class TeamWork : CustomForm
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
            if (disposing)
            {
                _myDataArtBindingSource?.Dispose();
                _myDataAnnBindingSource?.Dispose();
                _normRaszBindingSourceArticles?.Dispose();
                _preArchBindingSource?.Dispose();
                components?.Dispose();
            }
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
            EditorButtonImageOptions editorButtonImageOptions8 = new EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamWork));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions9 = new EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions10 = new EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions11 = new EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions12 = new EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions13 = new EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            EditorButtonImageOptions editorButtonImageOptions14 = new EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions34 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions35 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions36 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions37 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions38 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions39 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions40 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions41 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions42 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions43 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions44 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions45 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions46 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions47 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions48 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions49 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions50 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions51 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions52 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions53 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions54 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions55 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions56 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions57 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions58 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions59 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions60 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions61 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions62 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions63 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions64 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions65 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions66 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            repositoryItemCheckEdit1 = new RepositoryItemCheckEdit();
            repositoryItemButtonEdit2 = new RepositoryItemButtonEdit();
            xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            TabPage1 = new CustomTabPage();
            layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            customSimpleButtonRaszLog = new CustomSimpleButton();
            customSimpleButtonAnnLog = new CustomSimpleButton();
            ButtonArchAndCopyWd = new CustomButton();
            ButtonDouble = new CustomButton();
            ButtonEditOnlyAdv = new CustomActionButton();
            ButtonEditWd = new CustomButton();
            toggleSwitchKit = new ToggleSwitch();
            KITlabel = new CustomLabel();
            ButtonPreliminaryWd = new CustomButton();
            customSimpleButtonDel = new CustomSimpleButton();
            customSimpleButtonUpd = new CustomSimpleButton();
            customSimpleButtonUnbind = new CustomSimpleButton();
            customSimpleButtonArchARTICUL = new CustomSimpleButton();
            customSimpleButtonArch = new CustomSimpleButton();
            printButtonPlus = new CustomSimpleButton();
            constructorTextBox = new RichTextBox();
            designerTextBox = new RichTextBox();
            pictureBox1 = new PictureBox();
            buffer = new CustomTextBox();
            gridControlKontTW = new CustomGridControl();
            gridViewKontTW = new GridView();
            colkod_o2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colrazryd2 = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext2 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek3 = new DevExpress.XtraGrid.Columns.GridColumn();
            colannId5 = new DevExpress.XtraGrid.Columns.GridColumn();
            GridControlBindedArts = new CustomGridControl();
            gridViewBindedArts = new GridView();
            gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn56 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            data_r = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlRaskrTW = new CustomGridControl();
            gridViewRaskrTW = new GridView();
            gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
            colid = new DevExpress.XtraGrid.Columns.GridColumn();
            colkod2 = new DevExpress.XtraGrid.Columns.GridColumn();
            colkod_o1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            colrazryd1 = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            colannId4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlRaszTW = new CustomGridControl();
            gridViewRaszTW = new GridView();
            DisplayNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            coln = new DevExpress.XtraGrid.Columns.GridColumn();
            coln1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colrazryd = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            colobor = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            colkod_o = new DevExpress.XtraGrid.Columns.GridColumn();
            colannId3 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemLookUpEditProizv = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEditOb = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEditPodr = new RepositoryItemLookUpEdit();
            RecoRichTextBox = new RichTextBox();
            commentRichTextBox = new RichTextBox();
            ANNgridControl = new CustomGridControl();
            ANNgridView = new GridView();
            colgroup = new DevExpress.XtraGrid.Columns.GridColumn();
            colarticul = new DevExpress.XtraGrid.Columns.GridColumn();
            colmod = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek = new DevExpress.XtraGrid.Columns.GridColumn();
            coldateUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemDateEdit1 = new RepositoryItemDateEdit();
            colsek_vyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            coldateCreate = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_shv = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_vyazo = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit2 = new RepositoryItemCheckEdit();
            coldateAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_vyaz5 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_vyaz7 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_vyaz12 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_vyaz10 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_vyaz6 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_kr = new DevExpress.XtraGrid.Columns.GridColumn();
            colslogn = new DevExpress.XtraGrid.Columns.GridColumn();
            colkomment = new DevExpress.XtraGrid.Columns.GridColumn();
            colReco = new DevExpress.XtraGrid.Columns.GridColumn();
            coldiz = new DevExpress.XtraGrid.Columns.GridColumn();
            colconstr = new DevExpress.XtraGrid.Columns.GridColumn();
            colannID = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn66 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit3 = new RepositoryItemButtonEdit();
            ButtonCopyWd = new CustomButton();
            PrintButton = new CustomSimpleButton();
            panel5 = new Panel();
            SortBox = new CustomCheckBox();
            archiveCheckBox = new CustomCheckBox();
            actualCheckBox = new CustomCheckBox();
            preliminaryCheckBox = new CustomCheckBox();
            textEditMod = new TextEdit();
            textEditArt = new TextEdit();
            textEditSec = new TextEdit();
            textEditCreate = new TextEdit();
            layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem4 = new DevExpress.XtraLayout.SplitterItem();
            splitterItem5 = new DevExpress.XtraLayout.SplitterItem();
            splitterItem6 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator6 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlGroup15 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroupForAdmins = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem43 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem44 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem45 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            editBtns = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlEditWd = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlEditOnlyAdv = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup18 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            xtraTabPageArticles = new CustomTabPage();
            xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            xtraTabPageWorkDivisions = new CustomTabPage();
            panelControl2 = new PanelControl();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl1 = new SearchControl();
            pictureBox3 = new PictureBox();
            customGridControl2 = new CustomGridControl();
            normRaskArt = new GridView();
            Kod_o = new DevExpress.XtraGrid.Columns.GridColumn();
            TextRask = new DevExpress.XtraGrid.Columns.GridColumn();
            razryd = new DevExpress.XtraGrid.Columns.GridColumn();
            sek = new DevExpress.XtraGrid.Columns.GridColumn();
            spec = new DevExpress.XtraGrid.Columns.GridColumn();
            Obor = new DevExpress.XtraGrid.Columns.GridColumn();
            customGridControl1 = new CustomGridControl();
            normKontTab = new GridView();
            customLabel2 = new CustomLabel();
            pictureBox2 = new PictureBox();
            gridControl_binded = new CustomGridControl();
            gridView_binded = new GridView();
            gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlNZP = new CustomGridControl();
            gridViewNZP = new GridView();
            gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit8 = new RepositoryItemCheckEdit();
            kodd_rt = new DevExpress.XtraGrid.Columns.GridColumn();
            colannId2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn55 = new DevExpress.XtraGrid.Columns.GridColumn();
            kolNZP = new DevExpress.XtraGrid.Columns.GridColumn();
            PztCount = new DevExpress.XtraGrid.Columns.GridColumn();
            customGridControl3 = new CustomGridControl();
            normRaszTab = new GridView();
            colannId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            coln3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControl_wdToBind = new CustomGridControl();
            gridView_wdToBind = new GridView();
            gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit3 = new RepositoryItemCheckEdit();
            colarticul1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn59 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn58 = new DevExpress.XtraGrid.Columns.GridColumn();
            colstatus1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            colannId7 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit4 = new RepositoryItemCheckEdit();
            repositoryItemCheckEdit6 = new RepositoryItemCheckEdit();
            repositoryItemCheckEdit7 = new RepositoryItemCheckEdit();
            gridControl_unboundArts = new CustomGridControl();
            gridView_unboundArts = new GridView();
            код = new DevExpress.XtraGrid.Columns.GridColumn();
            артикул = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            группа = new DevExpress.XtraGrid.Columns.GridColumn();
            модель = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn52 = new DevExpress.XtraGrid.Columns.GridColumn();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup14 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            splitterItem7 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            splitterItem8 = new DevExpress.XtraLayout.SplitterItem();
            xtraTabPage3 = new CustomTabPage();
            layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            customButton2 = new CustomButton();
            flyoutPanel1 = new DevExpress.Utils.FlyoutPanel();
            flyoutPanelControl1 = new DevExpress.Utils.FlyoutPanelControl();
            customCancelButton1 = new CustomCancelButton();
            customComboBox1 = new CustomComboBox();
            customButton1 = new CustomButton();
            gridControlPreArch = new CustomGridControl();
            gridViewPreArch = new GridView();
            gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit5 = new RepositoryItemCheckEdit();
            gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlArch = new CustomGridControl();
            gridViewArch = new GridView();
            gridColumn60 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn61 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn62 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn63 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn64 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn65 = new DevExpress.XtraGrid.Columns.GridColumn();
            layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup17 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroupPreArch = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup19 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            artnormnBindingSource1 = new BindingSource(components);
            normraszBindingSource = new BindingSource(components);
            normraskBindingSource = new BindingSource(components);
            normkontBindingSource = new BindingSource(components);
            normdopobrBindingSource = new BindingSource(components);
            normraszBindingSource1 = new BindingSource(components);
            artnormnBindingSource = new BindingSource(components);
            sparticulBindingSource = new BindingSource(components);
            artnormnBindingSource2 = new BindingSource(components);
            sparticulBindingSource1 = new BindingSource(components);
            imageCollection1 = new DevExpress.Utils.ImageCollection(components);
            errorProvider1 = new ErrorProvider(components);
            desBindingSource = new BindingSource(components);
            constrBindingSource = new BindingSource(components);
            layoutControlItem49 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl1).BeginInit();
            xtraTabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl2).BeginInit();
            layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)toggleSwitchKit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlKontTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewKontTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridControlBindedArts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewBindedArts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaskrTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRaskrTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaszTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRaszTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditProizv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditOb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditPodr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDateEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit3).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textEditMod.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditArt.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditSec.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditCreate.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem29).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupForAdmins).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem43).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem41).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem42).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem44).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem45).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)editBtns).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlEditWd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem46).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem36).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlEditOnlyAdv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem30).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem35).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem32).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem34).BeginInit();
            xtraTabPageArticles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl2).BeginInit();
            xtraTabControl2.SuspendLayout();
            xtraTabPageWorkDivisions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normRaskArt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normKontTab).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl_binded).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView_binded).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlNZP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNZP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normRaszTab).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl_wdToBind).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView_wdToBind).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl_unboundArts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView_unboundArts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabbedControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem37).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem8).BeginInit();
            xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl3).BeginInit();
            layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)flyoutPanel1).BeginInit();
            flyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)flyoutPanelControl1).BeginInit();
            flyoutPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlPreArch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPreArch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlArch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem40).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem39).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupPreArch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem38).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)artnormnBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normraszBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normraskBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normkontBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normdopobrBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normraszBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)artnormnBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sparticulBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)artnormnBindingSource2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sparticulBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageCollection1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)desBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)constrBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem49).BeginInit();
            SuspendLayout();
            // 
            // repositoryItemCheckEdit1
            // 
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            repositoryItemCheckEdit1.NullStyle = StyleIndeterminate.Unchecked;
            // 
            // repositoryItemButtonEdit2
            // 
            repositoryItemButtonEdit2.AutoHeight = false;
            repositoryItemButtonEdit2.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            // 
            // xtraTabControl1
            // 
            xtraTabControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            xtraTabControl1.Appearance.Options.UseBackColor = true;
            xtraTabControl1.AppearancePage.Header.BackColor = System.Drawing.Color.Transparent;
            xtraTabControl1.AppearancePage.Header.Options.UseBackColor = true;
            xtraTabControl1.AppearancePage.HeaderActive.BackColor = System.Drawing.Color.Transparent;
            xtraTabControl1.AppearancePage.HeaderActive.ForeColor = System.Drawing.Color.Black;
            xtraTabControl1.AppearancePage.HeaderActive.Options.UseBackColor = true;
            xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
            xtraTabControl1.AppearancePage.HeaderActive.Options.UseForeColor = true;
            editorButtonImageOptions8.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            editorButtonImageOptions8.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions8.Image");
            xtraTabControl1.CustomHeaderButtons.AddRange(new DevExpress.XtraTab.Buttons.CustomHeaderButton[] { new DevExpress.XtraTab.Buttons.CustomHeaderButton(ButtonPredefines.Glyph, "", -1, true, true, editorButtonImageOptions8, serializableAppearanceObject8, "", null, null), new DevExpress.XtraTab.Buttons.CustomHeaderButton(ButtonPredefines.Ellipsis, "34534534", -1, true, false, editorButtonImageOptions9, serializableAppearanceObject9, "", null, null), new DevExpress.XtraTab.Buttons.CustomHeaderButton(ButtonPredefines.Redo, "", -1, true, false, editorButtonImageOptions10, serializableAppearanceObject10, "", null, null), new DevExpress.XtraTab.Buttons.CustomHeaderButton(ButtonPredefines.OK, "", -1, true, false, editorButtonImageOptions11, serializableAppearanceObject11, "", null, null), new DevExpress.XtraTab.Buttons.CustomHeaderButton(ButtonPredefines.Plus, "", -1, true, false, editorButtonImageOptions12, serializableAppearanceObject12, "", null, null), new DevExpress.XtraTab.Buttons.CustomHeaderButton(ButtonPredefines.Ellipsis, "", -1, true, false, editorButtonImageOptions13, serializableAppearanceObject13, "", null, null), new DevExpress.XtraTab.Buttons.CustomHeaderButton(ButtonPredefines.Delete, "", -1, true, false, editorButtonImageOptions14, serializableAppearanceObject14, "", null, null) });
            xtraTabControl1.Dock = DockStyle.Fill;
            xtraTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
            xtraTabControl1.HeaderButtons = DevExpress.XtraTab.TabButtons.Prev | DevExpress.XtraTab.TabButtons.Next | DevExpress.XtraTab.TabButtons.Default;
            xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            xtraTabControl1.Margin = new Padding(4, 3, 4, 3);
            xtraTabControl1.Name = "xtraTabControl1";
            xtraTabControl1.SelectedTabPage = TabPage1;
            xtraTabControl1.ShowHeaderFocus = DevExpress.Utils.DefaultBoolean.True;
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            xtraTabControl1.Size = new System.Drawing.Size(1849, 766);
            xtraTabControl1.TabIndex = 0;
            xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { TabPage1, xtraTabPageArticles });
            xtraTabControl1.SelectedPageChanged += XtraTabControl1_SelectedPageChanged;
            xtraTabControl1.CustomHeaderButtonClick += xtraTabControl1_CustomHeaderButtonClick;
            // 
            // TabPage1
            // 
            TabPage1.Appearance.Header.BackColor = System.Drawing.Color.Transparent;
            TabPage1.Appearance.Header.Options.UseBackColor = true;
            TabPage1.Appearance.HeaderActive.BackColor = System.Drawing.Color.Transparent;
            TabPage1.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            TabPage1.Appearance.HeaderActive.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            TabPage1.Appearance.HeaderActive.Options.UseBackColor = true;
            TabPage1.Appearance.HeaderActive.Options.UseFont = true;
            TabPage1.Appearance.HeaderActive.Options.UseForeColor = true;
            TabPage1.Appearance.PageClient.BackColor = System.Drawing.Color.Transparent;
            TabPage1.Appearance.PageClient.Options.UseBackColor = true;
            TabPage1.Controls.Add(layoutControl2);
            TabPage1.Margin = new Padding(4, 3, 4, 3);
            TabPage1.Name = "TabPage1";
            TabPage1.Size = new System.Drawing.Size(1847, 740);
            TabPage1.Text = "1. Разделения труда                        ";
            // 
            // layoutControl2
            // 
            layoutControl2.Controls.Add(customSimpleButtonRaszLog);
            layoutControl2.Controls.Add(customSimpleButtonAnnLog);
            layoutControl2.Controls.Add(ButtonArchAndCopyWd);
            layoutControl2.Controls.Add(ButtonDouble);
            layoutControl2.Controls.Add(ButtonEditOnlyAdv);
            layoutControl2.Controls.Add(ButtonEditWd);
            layoutControl2.Controls.Add(toggleSwitchKit);
            layoutControl2.Controls.Add(KITlabel);
            layoutControl2.Controls.Add(ButtonPreliminaryWd);
            layoutControl2.Controls.Add(customSimpleButtonDel);
            layoutControl2.Controls.Add(customSimpleButtonUpd);
            layoutControl2.Controls.Add(customSimpleButtonUnbind);
            layoutControl2.Controls.Add(customSimpleButtonArchARTICUL);
            layoutControl2.Controls.Add(customSimpleButtonArch);
            layoutControl2.Controls.Add(printButtonPlus);
            layoutControl2.Controls.Add(constructorTextBox);
            layoutControl2.Controls.Add(designerTextBox);
            layoutControl2.Controls.Add(pictureBox1);
            layoutControl2.Controls.Add(buffer);
            layoutControl2.Controls.Add(gridControlKontTW);
            layoutControl2.Controls.Add(GridControlBindedArts);
            layoutControl2.Controls.Add(gridControlRaskrTW);
            layoutControl2.Controls.Add(gridControlRaszTW);
            layoutControl2.Controls.Add(RecoRichTextBox);
            layoutControl2.Controls.Add(commentRichTextBox);
            layoutControl2.Controls.Add(ANNgridControl);
            layoutControl2.Controls.Add(ButtonCopyWd);
            layoutControl2.Controls.Add(PrintButton);
            layoutControl2.Controls.Add(panel5);
            layoutControl2.Controls.Add(textEditMod);
            layoutControl2.Controls.Add(textEditArt);
            layoutControl2.Controls.Add(textEditSec);
            layoutControl2.Controls.Add(textEditCreate);
            layoutControl2.Dock = DockStyle.Fill;
            layoutControl2.Location = new System.Drawing.Point(0, 0);
            layoutControl2.Name = "layoutControl2";
            layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(690, 338, 650, 621);
            layoutControl2.Root = layoutControlGroup7;
            layoutControl2.Size = new System.Drawing.Size(1847, 740);
            layoutControl2.TabIndex = 11;
            layoutControl2.Text = "layoutControl2";
            // 
            // customSimpleButtonRaszLog
            // 
            customSimpleButtonRaszLog.Appearance.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            customSimpleButtonRaszLog.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSimpleButtonRaszLog.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 105, 148);
            customSimpleButtonRaszLog.Appearance.Options.UseBackColor = true;
            customSimpleButtonRaszLog.Appearance.Options.UseFont = true;
            customSimpleButtonRaszLog.Appearance.Options.UseForeColor = true;
            customSimpleButtonRaszLog.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButtonRaszLog.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButtonRaszLog.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButtonRaszLog.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButtonRaszLog.Location = new System.Drawing.Point(36, 656);
            customSimpleButtonRaszLog.Name = "customSimpleButtonRaszLog";
            customSimpleButtonRaszLog.Size = new System.Drawing.Size(212, 22);
            customSimpleButtonRaszLog.StyleController = layoutControl2;
            customSimpleButtonRaszLog.TabIndex = 26;
            customSimpleButtonRaszLog.Text = "RaszLog";
            customSimpleButtonRaszLog.Click += customSimpleButtonRaszLog_Click;
            // 
            // customSimpleButtonAnnLog
            // 
            customSimpleButtonAnnLog.Appearance.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            customSimpleButtonAnnLog.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSimpleButtonAnnLog.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 105, 148);
            customSimpleButtonAnnLog.Appearance.Options.UseBackColor = true;
            customSimpleButtonAnnLog.Appearance.Options.UseFont = true;
            customSimpleButtonAnnLog.Appearance.Options.UseForeColor = true;
            customSimpleButtonAnnLog.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButtonAnnLog.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButtonAnnLog.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButtonAnnLog.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButtonAnnLog.Location = new System.Drawing.Point(197, 630);
            customSimpleButtonAnnLog.Name = "customSimpleButtonAnnLog";
            customSimpleButtonAnnLog.Size = new System.Drawing.Size(51, 22);
            customSimpleButtonAnnLog.StyleController = layoutControl2;
            customSimpleButtonAnnLog.TabIndex = 25;
            customSimpleButtonAnnLog.Text = "AnnLog";
            customSimpleButtonAnnLog.Click += customSimpleButtonAnnLog_Click;
            // 
            // ButtonArchAndCopyWd
            // 
            ButtonArchAndCopyWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonArchAndCopyWd.FlatAppearance.BorderSize = 0;
            ButtonArchAndCopyWd.FlatStyle = FlatStyle.Flat;
            ButtonArchAndCopyWd.Font = new System.Drawing.Font("Arial", 12F);
            ButtonArchAndCopyWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonArchAndCopyWd.Location = new System.Drawing.Point(36, 45);
            ButtonArchAndCopyWd.Margin = new Padding(4, 3, 4, 3);
            ButtonArchAndCopyWd.Name = "ButtonArchAndCopyWd";
            ButtonArchAndCopyWd.Size = new System.Drawing.Size(212, 25);
            ButtonArchAndCopyWd.TabIndex = 9;
            ButtonArchAndCopyWd.Text = "архив+копия";
            ButtonArchAndCopyWd.UseVisualStyleBackColor = false;
            ButtonArchAndCopyWd.Click += ButtonArchAndCopyWd_Click;
            // 
            // ButtonDouble
            // 
            ButtonDouble.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            ButtonDouble.FlatAppearance.BorderSize = 0;
            ButtonDouble.FlatStyle = FlatStyle.Flat;
            ButtonDouble.Font = new System.Drawing.Font("Arial", 10F);
            ButtonDouble.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            ButtonDouble.Location = new System.Drawing.Point(36, 16);
            ButtonDouble.Margin = new Padding(4, 3, 4, 3);
            ButtonDouble.Name = "ButtonDouble";
            ButtonDouble.Size = new System.Drawing.Size(212, 25);
            ButtonDouble.TabIndex = 10;
            ButtonDouble.Text = "дубль";
            ButtonDouble.UseVisualStyleBackColor = false;
            ButtonDouble.Click += ButtonDouble_Click;
            // 
            // ButtonEditOnlyAdv
            // 
            ButtonEditOnlyAdv.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
            ButtonEditOnlyAdv.Font = new System.Drawing.Font("Arial", 10F);
            ButtonEditOnlyAdv.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            ButtonEditOnlyAdv.Location = new System.Drawing.Point(144, -13);
            ButtonEditOnlyAdv.Margin = new Padding(0);
            ButtonEditOnlyAdv.Name = "ButtonEditOnlyAdv";
            ButtonEditOnlyAdv.Size = new System.Drawing.Size(104, 25);
            ButtonEditOnlyAdv.TabIndex = 12;
            ButtonEditOnlyAdv.Text = "редактировать РТ";
            ButtonEditOnlyAdv.UseVisualStyleBackColor = false;
            ButtonEditOnlyAdv.Click += ButtonEditOnlyAdv_Click;
            // 
            // ButtonEditWd
            // 
            ButtonEditWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonEditWd.FlatAppearance.BorderSize = 0;
            ButtonEditWd.FlatStyle = FlatStyle.Flat;
            ButtonEditWd.Font = new System.Drawing.Font("Arial", 12F);
            ButtonEditWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonEditWd.Location = new System.Drawing.Point(36, -13);
            ButtonEditWd.Margin = new Padding(4, 3, 4, 3);
            ButtonEditWd.Name = "ButtonEditWd";
            ButtonEditWd.Size = new System.Drawing.Size(104, 25);
            ButtonEditWd.TabIndex = 8;
            ButtonEditWd.Text = "редактировать РТ";
            ButtonEditWd.UseVisualStyleBackColor = false;
            ButtonEditWd.Visible = false;
            ButtonEditWd.Click += ButtonEditWd_Click;
            // 
            // toggleSwitchKit
            // 
            toggleSwitchKit.Location = new System.Drawing.Point(36, -114);
            toggleSwitchKit.Name = "toggleSwitchKit";
            toggleSwitchKit.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            toggleSwitchKit.Properties.Appearance.Options.UseFont = true;
            toggleSwitchKit.Properties.ContentAlignment = DevExpress.Utils.HorzAlignment.Near;
            toggleSwitchKit.Properties.EditorToThumbWidthRatio = 2F;
            toggleSwitchKit.Properties.OffText = "Обычный режим";
            toggleSwitchKit.Properties.OnText = "Режим комплекта";
            toggleSwitchKit.Size = new System.Drawing.Size(212, 20);
            toggleSwitchKit.StyleController = layoutControl2;
            toggleSwitchKit.TabIndex = 4;
            toggleSwitchKit.Toggled += ModeRadio_CheckedChanged;
            // 
            // KITlabel
            // 
            KITlabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            KITlabel.ForeColor = System.Drawing.Color.Red;
            KITlabel.Location = new System.Drawing.Point(36, -90);
            KITlabel.Name = "KITlabel";
            KITlabel.Size = new System.Drawing.Size(212, 20);
            KITlabel.TabIndex = 1;
            KITlabel.Text = "РАБОТА В РЕЖИМЕ КОМПЛЕКТА\r\nДЛЯ СОЗДАНИЯ ОДИНОЧНЫХ РТ\r\nПЕРЕЙДИТЕ В ОБЫЧНЫЙ РЕЖИМ";
            KITlabel.Visible = false;
            // 
            // ButtonPreliminaryWd
            // 
            ButtonPreliminaryWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonPreliminaryWd.FlatAppearance.BorderSize = 0;
            ButtonPreliminaryWd.FlatStyle = FlatStyle.Flat;
            ButtonPreliminaryWd.Font = new System.Drawing.Font("Arial", 10F);
            ButtonPreliminaryWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonPreliminaryWd.Location = new System.Drawing.Point(36, -66);
            ButtonPreliminaryWd.Margin = new Padding(4, 3, 4, 3);
            ButtonPreliminaryWd.Name = "ButtonPreliminaryWd";
            ButtonPreliminaryWd.Size = new System.Drawing.Size(212, 25);
            ButtonPreliminaryWd.TabIndex = 11;
            ButtonPreliminaryWd.Text = "добавить предв";
            ButtonPreliminaryWd.UseVisualStyleBackColor = false;
            ButtonPreliminaryWd.Visible = false;
            ButtonPreliminaryWd.Click += ButtonPreliminaryWd_Click;
            // 
            // customSimpleButtonDel
            // 
            customSimpleButtonDel.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
            customSimpleButtonDel.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSimpleButtonDel.Appearance.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customSimpleButtonDel.Appearance.Options.UseBackColor = true;
            customSimpleButtonDel.Appearance.Options.UseFont = true;
            customSimpleButtonDel.Appearance.Options.UseForeColor = true;
            customSimpleButtonDel.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButtonDel.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButtonDel.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButtonDel.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButtonDel.Location = new System.Drawing.Point(36, 630);
            customSimpleButtonDel.Name = "customSimpleButtonDel";
            customSimpleButtonDel.Size = new System.Drawing.Size(157, 22);
            customSimpleButtonDel.StyleController = layoutControl2;
            customSimpleButtonDel.TabIndex = 1;
            customSimpleButtonDel.Text = "Пометка на удаление РТ";
            customSimpleButtonDel.Click += customSimpleButtonDel_Click;
            // 
            // customSimpleButtonUpd
            // 
            customSimpleButtonUpd.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
            customSimpleButtonUpd.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSimpleButtonUpd.Appearance.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customSimpleButtonUpd.Appearance.Options.UseBackColor = true;
            customSimpleButtonUpd.Appearance.Options.UseFont = true;
            customSimpleButtonUpd.Appearance.Options.UseForeColor = true;
            customSimpleButtonUpd.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButtonUpd.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButtonUpd.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButtonUpd.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButtonUpd.Location = new System.Drawing.Point(36, 682);
            customSimpleButtonUpd.Name = "customSimpleButtonUpd";
            customSimpleButtonUpd.Size = new System.Drawing.Size(107, 22);
            customSimpleButtonUpd.StyleController = layoutControl2;
            customSimpleButtonUpd.TabIndex = 1;
            customSimpleButtonUpd.Text = "Утвердить";
            customSimpleButtonUpd.Click += customSimpleButtonUpd_Click;
            // 
            // customSimpleButtonUnbind
            // 
            customSimpleButtonUnbind.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
            customSimpleButtonUnbind.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSimpleButtonUnbind.Appearance.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customSimpleButtonUnbind.Appearance.Options.UseBackColor = true;
            customSimpleButtonUnbind.Appearance.Options.UseFont = true;
            customSimpleButtonUnbind.Appearance.Options.UseForeColor = true;
            customSimpleButtonUnbind.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButtonUnbind.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButtonUnbind.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButtonUnbind.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButtonUnbind.Location = new System.Drawing.Point(36, 604);
            customSimpleButtonUnbind.Name = "customSimpleButtonUnbind";
            customSimpleButtonUnbind.Size = new System.Drawing.Size(155, 22);
            customSimpleButtonUnbind.StyleController = layoutControl2;
            customSimpleButtonUnbind.TabIndex = 1;
            customSimpleButtonUnbind.Text = "Отвязать артикул от РТ";
            customSimpleButtonUnbind.Click += customSimpleButton5_Click;
            // 
            // customSimpleButtonArchARTICUL
            // 
            customSimpleButtonArchARTICUL.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
            customSimpleButtonArchARTICUL.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSimpleButtonArchARTICUL.Appearance.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customSimpleButtonArchARTICUL.Appearance.Options.UseBackColor = true;
            customSimpleButtonArchARTICUL.Appearance.Options.UseFont = true;
            customSimpleButtonArchARTICUL.Appearance.Options.UseForeColor = true;
            customSimpleButtonArchARTICUL.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButtonArchARTICUL.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButtonArchARTICUL.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButtonArchARTICUL.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButtonArchARTICUL.Location = new System.Drawing.Point(195, 604);
            customSimpleButtonArchARTICUL.Name = "customSimpleButtonArchARTICUL";
            customSimpleButtonArchARTICUL.Size = new System.Drawing.Size(53, 22);
            customSimpleButtonArchARTICUL.StyleController = layoutControl2;
            customSimpleButtonArchARTICUL.TabIndex = 1;
            customSimpleButtonArchARTICUL.Text = "Арх арт";
            customSimpleButtonArchARTICUL.Click += customSimpleButtonArchArt_Click;
            // 
            // customSimpleButtonArch
            // 
            customSimpleButtonArch.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
            customSimpleButtonArch.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSimpleButtonArch.Appearance.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customSimpleButtonArch.Appearance.Options.UseBackColor = true;
            customSimpleButtonArch.Appearance.Options.UseFont = true;
            customSimpleButtonArch.Appearance.Options.UseForeColor = true;
            customSimpleButtonArch.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButtonArch.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButtonArch.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButtonArch.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButtonArch.Location = new System.Drawing.Point(147, 682);
            customSimpleButtonArch.Name = "customSimpleButtonArch";
            customSimpleButtonArch.Size = new System.Drawing.Size(101, 22);
            customSimpleButtonArch.StyleController = layoutControl2;
            customSimpleButtonArch.TabIndex = 1;
            customSimpleButtonArch.Text = "Архив РТ";
            customSimpleButtonArch.Click += customSimpleButtonArch_Click;
            // 
            // printButtonPlus
            // 
            printButtonPlus.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
            printButtonPlus.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            printButtonPlus.Appearance.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            printButtonPlus.Appearance.Options.UseBackColor = true;
            printButtonPlus.Appearance.Options.UseFont = true;
            printButtonPlus.Appearance.Options.UseForeColor = true;
            printButtonPlus.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            printButtonPlus.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            printButtonPlus.AppearanceDisabled.Options.UseBackColor = true;
            printButtonPlus.AppearanceDisabled.Options.UseForeColor = true;
            printButtonPlus.Location = new System.Drawing.Point(151, -152);
            printButtonPlus.Name = "printButtonPlus";
            printButtonPlus.Size = new System.Drawing.Size(109, 22);
            printButtonPlus.StyleController = layoutControl2;
            printButtonPlus.TabIndex = 3;
            printButtonPlus.Text = "печать +";
            printButtonPlus.Click += printButtonPlus_Click;
            // 
            // constructorTextBox
            // 
            constructorTextBox.Enabled = false;
            constructorTextBox.Location = new System.Drawing.Point(24, 433);
            constructorTextBox.Name = "constructorTextBox";
            constructorTextBox.Size = new System.Drawing.Size(228, 26);
            constructorTextBox.TabIndex = 13;
            constructorTextBox.Text = "";
            // 
            // designerTextBox
            // 
            designerTextBox.Enabled = false;
            designerTextBox.Location = new System.Drawing.Point(24, 382);
            designerTextBox.Name = "designerTextBox";
            designerTextBox.Size = new System.Drawing.Size(228, 26);
            designerTextBox.TabIndex = 12;
            designerTextBox.Text = "";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(24, 86);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(236, 170);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // buffer
            // 
            buffer.BackColor = System.Drawing.Color.FromArgb(255, 245, 230);
            buffer.Enabled = false;
            buffer.Font = new System.Drawing.Font("Arial", 10F);
            buffer.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            buffer.Location = new System.Drawing.Point(24, 297);
            buffer.Margin = new Padding(4, 3, 4, 3);
            buffer.Multiline = true;
            buffer.Name = "buffer";
            buffer.Size = new System.Drawing.Size(228, 60);
            buffer.TabIndex = 11;
            buffer.Visible = false;
            // 
            // gridControlKontTW
            // 
            gridControlKontTW.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlKontTW.Font = new System.Drawing.Font("Arial", 10F);
            gridControlKontTW.Location = new System.Drawing.Point(933, 642);
            gridControlKontTW.MainView = gridViewKontTW;
            gridControlKontTW.Margin = new Padding(4, 3, 4, 3);
            gridControlKontTW.Name = "gridControlKontTW";
            gridControlKontTW.Size = new System.Drawing.Size(873, 74);
            gridControlKontTW.TabIndex = 24;
            gridControlKontTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewKontTW });
            // 
            // gridViewKontTW
            // 
            gridViewKontTW.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colkod_o2, gridColumn1, colrazryd2, coltext2, colsek3, colannId5 });
            gridViewKontTW.DetailHeight = 404;
            gridViewKontTW.GridControl = gridControlKontTW;
            gridViewKontTW.Name = "gridViewKontTW";
            gridViewKontTW.OptionsBehavior.Editable = false;
            gridViewKontTW.OptionsBehavior.ReadOnly = true;
            gridViewKontTW.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridViewKontTW.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewKontTW.OptionsSelection.MultiSelect = true;
            gridViewKontTW.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridViewKontTW.OptionsView.ShowGroupPanel = false;
            gridViewKontTW.PopupMenuShowing += gridViewRaszTW_PopupMenuShowing;
            // 
            // colkod_o2
            // 
            colkod_o2.Caption = "№ оп.";
            colkod_o2.FieldName = "kod_o";
            colkod_o2.MinWidth = 23;
            colkod_o2.Name = "colkod_o2";
            colkod_o2.Visible = true;
            colkod_o2.VisibleIndex = 0;
            colkod_o2.Width = 61;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "№ п/оп.";
            gridColumn1.FieldName = "n1";
            gridColumn1.MinWidth = 23;
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 1;
            gridColumn1.Width = 43;
            // 
            // colrazryd2
            // 
            colrazryd2.Caption = "Разряд";
            colrazryd2.FieldName = "razryd";
            colrazryd2.MinWidth = 23;
            colrazryd2.Name = "colrazryd2";
            colrazryd2.Visible = true;
            colrazryd2.VisibleIndex = 2;
            colrazryd2.Width = 62;
            // 
            // coltext2
            // 
            coltext2.Caption = "Наименование операции комплектовки";
            coltext2.FieldName = "text";
            coltext2.MinWidth = 23;
            coltext2.Name = "coltext2";
            coltext2.Visible = true;
            coltext2.VisibleIndex = 3;
            coltext2.Width = 353;
            // 
            // colsek3
            // 
            colsek3.Caption = "Сек.";
            colsek3.FieldName = "sek";
            colsek3.MinWidth = 23;
            colsek3.Name = "colsek3";
            colsek3.Visible = true;
            colsek3.VisibleIndex = 4;
            colsek3.Width = 70;
            // 
            // colannId5
            // 
            colannId5.FieldName = "annId";
            colannId5.MinWidth = 23;
            colannId5.Name = "colannId5";
            colannId5.Width = 76;
            // 
            // GridControlBindedArts
            // 
            GridControlBindedArts.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            GridControlBindedArts.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            GridControlBindedArts.Location = new System.Drawing.Point(301, 393);
            GridControlBindedArts.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            GridControlBindedArts.LookAndFeel.UseDefaultLookAndFeel = false;
            GridControlBindedArts.MainView = gridViewBindedArts;
            GridControlBindedArts.Margin = new Padding(4, 3, 4, 3);
            GridControlBindedArts.Name = "GridControlBindedArts";
            GridControlBindedArts.Size = new System.Drawing.Size(582, 311);
            GridControlBindedArts.TabIndex = 17;
            GridControlBindedArts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewBindedArts });
            // 
            // gridViewBindedArts
            // 
            gridViewBindedArts.Appearance.SelectedRow.FontStyleDelta = System.Drawing.FontStyle.Bold;
            gridViewBindedArts.Appearance.SelectedRow.Options.UseFont = true;
            gridViewBindedArts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn24, gridColumn25, gridColumn41, gridColumn42, gridColumn43, gridColumn44, gridColumn57, gridColumn56, gridColumn45, gridColumn46, data_r });
            gridViewBindedArts.DetailHeight = 404;
            gridViewBindedArts.GridControl = GridControlBindedArts;
            gridViewBindedArts.GroupFormat = "{0}:  {1}{2}";
            gridViewBindedArts.Name = "gridViewBindedArts";
            gridViewBindedArts.OptionsBehavior.Editable = false;
            gridViewBindedArts.OptionsBehavior.ReadOnly = true;
            gridViewBindedArts.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridViewBindedArts.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewBindedArts.OptionsFind.FindFilterColumns = "";
            gridViewBindedArts.OptionsFind.FindNullPrompt = "Введите артикул для поиска...";
            gridViewBindedArts.OptionsSelection.MultiSelect = true;
            gridViewBindedArts.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridViewBindedArts.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridViewBindedArts.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gridViewBindedArts.OptionsView.ShowGroupPanel = false;
            gridViewBindedArts.PopupMenuShowing += gridViewRaszTW_PopupMenuShowing;
            // 
            // gridColumn24
            // 
            gridColumn24.FieldName = "kodd_rt";
            gridColumn24.MinWidth = 23;
            gridColumn24.Name = "gridColumn24";
            gridColumn24.Width = 87;
            // 
            // gridColumn25
            // 
            gridColumn25.FieldName = "annId";
            gridColumn25.MinWidth = 23;
            gridColumn25.Name = "gridColumn25";
            gridColumn25.Width = 49;
            // 
            // gridColumn41
            // 
            gridColumn41.Caption = "Код";
            gridColumn41.FieldName = "kodd";
            gridColumn41.MinWidth = 23;
            gridColumn41.Name = "gridColumn41";
            gridColumn41.Width = 84;
            // 
            // gridColumn42
            // 
            gridColumn42.Caption = "Группа";
            gridColumn42.FieldName = "grup";
            gridColumn42.MinWidth = 23;
            gridColumn42.Name = "gridColumn42";
            gridColumn42.Visible = true;
            gridColumn42.VisibleIndex = 0;
            gridColumn42.Width = 91;
            // 
            // gridColumn43
            // 
            gridColumn43.Caption = "Артикул";
            gridColumn43.FieldName = "articul";
            gridColumn43.MinWidth = 23;
            gridColumn43.Name = "gridColumn43";
            gridColumn43.Visible = true;
            gridColumn43.VisibleIndex = 1;
            gridColumn43.Width = 87;
            // 
            // gridColumn44
            // 
            gridColumn44.Caption = "Модель";
            gridColumn44.FieldName = "mod";
            gridColumn44.MinWidth = 23;
            gridColumn44.Name = "gridColumn44";
            gridColumn44.Visible = true;
            gridColumn44.VisibleIndex = 2;
            gridColumn44.Width = 81;
            // 
            // gridColumn57
            // 
            gridColumn57.Caption = "Мин размер";
            gridColumn57.FieldName = "minSize";
            gridColumn57.Name = "gridColumn57";
            gridColumn57.Visible = true;
            gridColumn57.VisibleIndex = 3;
            gridColumn57.Width = 102;
            // 
            // gridColumn56
            // 
            gridColumn56.Caption = "Макс размер";
            gridColumn56.FieldName = "maxSize";
            gridColumn56.Name = "gridColumn56";
            gridColumn56.Visible = true;
            gridColumn56.VisibleIndex = 4;
            gridColumn56.Width = 94;
            // 
            // gridColumn45
            // 
            gridColumn45.Caption = "Наличие НЗП";
            gridColumn45.FieldName = "kolNZP";
            gridColumn45.MinWidth = 23;
            gridColumn45.Name = "gridColumn45";
            gridColumn45.Visible = true;
            gridColumn45.VisibleIndex = 5;
            gridColumn45.Width = 63;
            // 
            // gridColumn46
            // 
            gridColumn46.Caption = "Кол-во назн. опер.";
            gridColumn46.FieldName = "PZTCount";
            gridColumn46.MinWidth = 23;
            gridColumn46.Name = "gridColumn46";
            gridColumn46.Visible = true;
            gridColumn46.VisibleIndex = 6;
            gridColumn46.Width = 83;
            // 
            // data_r
            // 
            data_r.Caption = "Дата посл. пачки";
            data_r.FieldName = "data_r";
            data_r.Name = "data_r";
            data_r.Visible = true;
            data_r.VisibleIndex = 7;
            data_r.Width = 82;
            // 
            // gridControlRaskrTW
            // 
            gridControlRaskrTW.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlRaskrTW.Font = new System.Drawing.Font("Arial", 10F);
            gridControlRaskrTW.Location = new System.Drawing.Point(933, 415);
            gridControlRaskrTW.MainView = gridViewRaskrTW;
            gridControlRaskrTW.Margin = new Padding(0);
            gridControlRaskrTW.Name = "gridControlRaskrTW";
            gridControlRaskrTW.Size = new System.Drawing.Size(873, 168);
            gridControlRaskrTW.TabIndex = 23;
            gridControlRaskrTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRaskrTW });
            // 
            // gridViewRaskrTW
            // 
            gridViewRaskrTW.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn47, colid, colkod2, colkod_o1, gridColumn18, colrazryd1, coltext1, colsek2, gridColumn34, gridColumn33, gridColumn32, gridColumn31, colannId4 });
            gridViewRaskrTW.DetailHeight = 404;
            gridViewRaskrTW.GridControl = gridControlRaskrTW;
            gridViewRaskrTW.Name = "gridViewRaskrTW";
            gridViewRaskrTW.OptionsBehavior.Editable = false;
            gridViewRaskrTW.OptionsBehavior.ReadOnly = true;
            gridViewRaskrTW.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridViewRaskrTW.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewRaskrTW.OptionsSelection.MultiSelect = true;
            gridViewRaskrTW.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridViewRaskrTW.OptionsView.ShowGroupPanel = false;
            gridViewRaskrTW.PopupMenuShowing += gridViewRaskrTW_PopupMenuShowing;
            // 
            // gridColumn47
            // 
            gridColumn47.Caption = "№оп.";
            gridColumn47.FieldName = "DisplayNumber";
            gridColumn47.Name = "gridColumn47";
            gridColumn47.Visible = true;
            gridColumn47.VisibleIndex = 0;
            // 
            // colid
            // 
            colid.FieldName = "id";
            colid.MinWidth = 23;
            colid.Name = "colid";
            colid.Width = 87;
            // 
            // colkod2
            // 
            colkod2.FieldName = "Kod";
            colkod2.MinWidth = 23;
            colkod2.Name = "colkod2";
            colkod2.Width = 87;
            // 
            // colkod_o1
            // 
            colkod_o1.Caption = "№оп.";
            colkod_o1.FieldName = "N";
            colkod_o1.MinWidth = 23;
            colkod_o1.Name = "colkod_o1";
            colkod_o1.Width = 52;
            // 
            // gridColumn18
            // 
            gridColumn18.Caption = "№ п/оп.";
            gridColumn18.FieldName = "N1";
            gridColumn18.MinWidth = 23;
            gridColumn18.Name = "gridColumn18";
            gridColumn18.Width = 39;
            // 
            // colrazryd1
            // 
            colrazryd1.Caption = "Разряд";
            colrazryd1.FieldName = "razryd";
            colrazryd1.MinWidth = 23;
            colrazryd1.Name = "colrazryd1";
            colrazryd1.Visible = true;
            colrazryd1.VisibleIndex = 1;
            colrazryd1.Width = 56;
            // 
            // coltext1
            // 
            coltext1.Caption = "Наименование операции раскроя";
            coltext1.FieldName = "TextRask";
            coltext1.MinWidth = 23;
            coltext1.Name = "coltext1";
            coltext1.Visible = true;
            coltext1.VisibleIndex = 2;
            coltext1.Width = 291;
            // 
            // colsek2
            // 
            colsek2.Caption = "Сек.";
            colsek2.FieldName = "Sek";
            colsek2.MinWidth = 23;
            colsek2.Name = "colsek2";
            colsek2.Visible = true;
            colsek2.VisibleIndex = 3;
            colsek2.Width = 69;
            // 
            // gridColumn34
            // 
            gridColumn34.Caption = "Специальность";
            gridColumn34.FieldName = "Spec";
            gridColumn34.MinWidth = 23;
            gridColumn34.Name = "gridColumn34";
            gridColumn34.Visible = true;
            gridColumn34.VisibleIndex = 4;
            gridColumn34.Width = 85;
            // 
            // gridColumn33
            // 
            gridColumn33.Caption = "Оборудование";
            gridColumn33.FieldName = "Obor";
            gridColumn33.MinWidth = 23;
            gridColumn33.Name = "gridColumn33";
            gridColumn33.Visible = true;
            gridColumn33.VisibleIndex = 5;
            gridColumn33.Width = 90;
            // 
            // gridColumn32
            // 
            gridColumn32.Caption = "код оп.";
            gridColumn32.FieldName = "kod_o";
            gridColumn32.MinWidth = 23;
            gridColumn32.Name = "gridColumn32";
            gridColumn32.Width = 85;
            // 
            // gridColumn31
            // 
            gridColumn31.Caption = "код из.";
            gridColumn31.FieldName = "Kod";
            gridColumn31.MinWidth = 23;
            gridColumn31.Name = "gridColumn31";
            gridColumn31.Width = 85;
            // 
            // colannId4
            // 
            colannId4.FieldName = "annId";
            colannId4.MinWidth = 23;
            colannId4.Name = "colannId4";
            colannId4.Width = 76;
            // 
            // gridControlRaszTW
            // 
            gridControlRaszTW.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlRaszTW.Font = new System.Drawing.Font("Arial", 10F);
            gridControlRaszTW.Location = new System.Drawing.Point(921, -205);
            gridControlRaszTW.MainView = gridViewRaszTW;
            gridControlRaszTW.Margin = new Padding(4, 3, 4, 3);
            gridControlRaszTW.Name = "gridControlRaszTW";
            gridControlRaszTW.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemLookUpEditProizv, repositoryItemLookUpEditOb, repositoryItemLookUpEditPodr });
            gridControlRaszTW.Size = new System.Drawing.Size(897, 573);
            gridControlRaszTW.TabIndex = 22;
            gridControlRaszTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRaszTW });
            // 
            // gridViewRaszTW
            // 
            gridViewRaszTW.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { DisplayNumber, coln, coln1, colrazryd, coltext, colsek1, gridColumn30, gridColumn27, colobor, gridColumn26, gridColumn3, colkod_o, colannId3 });
            gridViewRaszTW.DetailHeight = 404;
            gridViewRaszTW.GridControl = gridControlRaszTW;
            gridViewRaszTW.Name = "gridViewRaszTW";
            gridViewRaszTW.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewRaszTW.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewRaszTW.OptionsBehavior.Editable = false;
            gridViewRaszTW.OptionsBehavior.ReadOnly = true;
            gridViewRaszTW.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridViewRaszTW.OptionsCustomization.AllowSort = false;
            gridViewRaszTW.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewRaszTW.OptionsSelection.MultiSelect = true;
            gridViewRaszTW.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridViewRaszTW.OptionsView.ShowGroupPanel = false;
            gridViewRaszTW.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(coln, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(coln1, DevExpress.Data.ColumnSortOrder.Ascending) });
            gridViewRaszTW.PopupMenuShowing += gridViewRaszTW_PopupMenuShowing;
            // 
            // DisplayNumber
            // 
            DisplayNumber.Caption = "№ оп.";
            DisplayNumber.FieldName = "DisplayNumber";
            DisplayNumber.Name = "DisplayNumber";
            DisplayNumber.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            DisplayNumber.Visible = true;
            DisplayNumber.VisibleIndex = 0;
            DisplayNumber.Width = 88;
            // 
            // coln
            // 
            coln.Caption = "№ оп.";
            coln.FieldName = "N";
            coln.MinWidth = 23;
            coln.Name = "coln";
            coln.Width = 49;
            // 
            // coln1
            // 
            coln1.Caption = "№ п/оп.";
            coln1.FieldName = "N1";
            coln1.MinWidth = 23;
            coln1.Name = "coln1";
            coln1.Width = 40;
            // 
            // colrazryd
            // 
            colrazryd.Caption = "Разряд";
            colrazryd.FieldName = "razryd";
            colrazryd.MinWidth = 23;
            colrazryd.Name = "colrazryd";
            colrazryd.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colrazryd.Visible = true;
            colrazryd.VisibleIndex = 1;
            colrazryd.Width = 66;
            // 
            // coltext
            // 
            coltext.Caption = "Наименование операции пошива";
            coltext.FieldName = "Text";
            coltext.MinWidth = 23;
            coltext.Name = "coltext";
            coltext.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            coltext.Visible = true;
            coltext.VisibleIndex = 2;
            coltext.Width = 311;
            // 
            // colsek1
            // 
            colsek1.Caption = "Сек.";
            colsek1.FieldName = "Sek";
            colsek1.MinWidth = 23;
            colsek1.Name = "colsek1";
            colsek1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colsek1.Visible = true;
            colsek1.VisibleIndex = 3;
            colsek1.Width = 63;
            // 
            // gridColumn30
            // 
            gridColumn30.Caption = "Спец-ть";
            gridColumn30.FieldName = "Spec";
            gridColumn30.MinWidth = 23;
            gridColumn30.Name = "gridColumn30";
            gridColumn30.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridColumn30.Visible = true;
            gridColumn30.VisibleIndex = 4;
            gridColumn30.Width = 106;
            // 
            // gridColumn27
            // 
            gridColumn27.Caption = "Производство";
            gridColumn27.FieldName = "TextProizv";
            gridColumn27.MinWidth = 23;
            gridColumn27.Name = "gridColumn27";
            gridColumn27.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridColumn27.Visible = true;
            gridColumn27.VisibleIndex = 5;
            gridColumn27.Width = 86;
            // 
            // colobor
            // 
            colobor.Caption = "Оборудование";
            colobor.FieldName = "Obor";
            colobor.MinWidth = 23;
            colobor.Name = "colobor";
            colobor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colobor.Visible = true;
            colobor.VisibleIndex = 7;
            colobor.Width = 137;
            // 
            // gridColumn26
            // 
            gridColumn26.Caption = "Вяз. подр.";
            gridColumn26.FieldName = "TextVyaz";
            gridColumn26.MinWidth = 23;
            gridColumn26.Name = "gridColumn26";
            gridColumn26.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridColumn26.Visible = true;
            gridColumn26.VisibleIndex = 6;
            gridColumn26.Width = 82;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "Код";
            gridColumn3.FieldName = "Kod";
            gridColumn3.MinWidth = 23;
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Width = 46;
            // 
            // colkod_o
            // 
            colkod_o.FieldName = "kod_o";
            colkod_o.MinWidth = 23;
            colkod_o.Name = "colkod_o";
            colkod_o.Width = 82;
            // 
            // colannId3
            // 
            colannId3.FieldName = "AnnID";
            colannId3.MinWidth = 23;
            colannId3.Name = "colannId3";
            colannId3.Width = 87;
            // 
            // repositoryItemLookUpEditProizv
            // 
            repositoryItemLookUpEditProizv.AutoHeight = false;
            repositoryItemLookUpEditProizv.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemLookUpEditProizv.DisplayMember = "TextProizv";
            repositoryItemLookUpEditProizv.Name = "repositoryItemLookUpEditProizv";
            repositoryItemLookUpEditProizv.ValueMember = "kod_proizv";
            // 
            // repositoryItemLookUpEditOb
            // 
            repositoryItemLookUpEditOb.AutoHeight = false;
            repositoryItemLookUpEditOb.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemLookUpEditOb.DisplayMember = "TextOb";
            repositoryItemLookUpEditOb.Name = "repositoryItemLookUpEditOb";
            repositoryItemLookUpEditOb.ValueMember = "Kod_ob";
            // 
            // repositoryItemLookUpEditPodr
            // 
            repositoryItemLookUpEditPodr.AutoHeight = false;
            repositoryItemLookUpEditPodr.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemLookUpEditPodr.DisplayMember = "TextPodr";
            repositoryItemLookUpEditPodr.Name = "repositoryItemLookUpEditPodr";
            repositoryItemLookUpEditPodr.ValueMember = "Kod_podr";
            // 
            // RecoRichTextBox
            // 
            RecoRichTextBox.Enabled = false;
            RecoRichTextBox.Location = new System.Drawing.Point(24, 484);
            RecoRichTextBox.Margin = new Padding(4, 3, 4, 3);
            RecoRichTextBox.Name = "RecoRichTextBox";
            RecoRichTextBox.Size = new System.Drawing.Size(228, 39);
            RecoRichTextBox.TabIndex = 14;
            RecoRichTextBox.Text = "";
            // 
            // commentRichTextBox
            // 
            commentRichTextBox.Enabled = false;
            commentRichTextBox.Location = new System.Drawing.Point(24, 548);
            commentRichTextBox.Margin = new Padding(4, 3, 4, 3);
            commentRichTextBox.Name = "commentRichTextBox";
            commentRichTextBox.Size = new System.Drawing.Size(228, 40);
            commentRichTextBox.TabIndex = 15;
            commentRichTextBox.Text = "";
            // 
            // ANNgridControl
            // 
            ANNgridControl.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.Transparent;
            ANNgridControl.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Transparent;
            ANNgridControl.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            ANNgridControl.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
            ANNgridControl.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            ANNgridControl.Font = new System.Drawing.Font("Arial", 10F);
            ANNgridControl.Location = new System.Drawing.Point(289, -256);
            ANNgridControl.MainView = ANNgridView;
            ANNgridControl.Margin = new Padding(4, 3, 4, 3);
            ANNgridControl.Name = "ANNgridControl";
            ANNgridControl.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemButtonEdit1, repositoryItemCheckEdit2, repositoryItemButtonEdit3, repositoryItemDateEdit1 });
            ANNgridControl.Size = new System.Drawing.Size(606, 612);
            ANNgridControl.TabIndex = 16;
            ANNgridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { ANNgridView });
            // 
            // ANNgridView
            // 
            ANNgridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            ANNgridView.Appearance.FocusedRow.Options.UseFont = true;
            ANNgridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            ANNgridView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            ANNgridView.Appearance.SelectedRow.FontStyleDelta = System.Drawing.FontStyle.Bold;
            ANNgridView.Appearance.SelectedRow.Options.UseFont = true;
            ANNgridView.Appearance.SelectedRow.Options.UseTextOptions = true;
            ANNgridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colgroup, colarticul, colmod, colsek, coldateUpdate, colsek_vyaz, coldateCreate, gridColumn19, colsek_shv, gridColumn5, colsek_vyazo, gridColumn35, coldateAdd, colsek_vyaz5, colsek_vyaz7, colsek_vyaz12, colsek_vyaz10, colsek_vyaz6, colsek_kr, colslogn, colkomment, colReco, coldiz, colconstr, colannID, gridColumn66 });
            ANNgridView.CustomizationFormBounds = new System.Drawing.Rectangle(688, 388, 308, 314);
            ANNgridView.DetailHeight = 404;
            ANNgridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            ANNgridView.GridControl = ANNgridControl;
            ANNgridView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            ANNgridView.Name = "ANNgridView";
            ANNgridView.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            ANNgridView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            ANNgridView.OptionsEditForm.PopupEditFormWidth = 933;
            ANNgridView.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            ANNgridView.OptionsFind.AlwaysVisible = true;
            ANNgridView.OptionsFind.Behavior = FindPanelBehavior.Filter;
            ANNgridView.OptionsFind.Condition = DevExpress.Data.Filtering.FilterCondition.Contains;
            ANNgridView.OptionsFind.FindDelay = 100;
            ANNgridView.OptionsFind.FindFilterColumns = "Articul;Mod;grup";
            ANNgridView.OptionsFind.FindMode = FindMode.Always;
            ANNgridView.OptionsFind.FindNullPrompt = "Введите текст для поиска...";
            ANNgridView.OptionsFind.FindPanelLocation = GridFindPanelLocation.Panel;
            ANNgridView.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            ANNgridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            ANNgridView.OptionsSelection.MultiSelect = true;
            ANNgridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            ANNgridView.OptionsView.AutoCalcPreviewLineCount = true;
            ANNgridView.OptionsView.ColumnAutoWidth = false;
            ANNgridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            ANNgridView.OptionsView.RowAutoHeight = true;
            ANNgridView.OptionsView.ShowGroupPanel = false;
            ANNgridView.OptionsView.ShowPreview = true;
            ANNgridView.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.True;
            ANNgridView.PreviewIndent = 10;
            ANNgridView.PreviewLineCount = 2;
            ANNgridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(colannID, DevExpress.Data.ColumnSortOrder.Descending) });
            ANNgridView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            ANNgridView.PopupMenuShowing += ANNgridView_PopupMenuShowing;
            ANNgridView.FocusedRowChanged += ANNgridView_FocusedRowChanged;
            // 
            // colgroup
            // 
            colgroup.Caption = "Группа";
            colgroup.FieldName = "grup";
            colgroup.MinWidth = 23;
            colgroup.Name = "colgroup";
            colgroup.OptionsColumn.AllowEdit = false;
            colgroup.Width = 64;
            // 
            // colarticul
            // 
            colarticul.Caption = "Артикул";
            colarticul.FieldName = "Articul";
            colarticul.MinWidth = 23;
            colarticul.Name = "colarticul";
            colarticul.OptionsColumn.AllowEdit = false;
            colarticul.Visible = true;
            colarticul.VisibleIndex = 2;
            colarticul.Width = 86;
            // 
            // colmod
            // 
            colmod.Caption = "Модель";
            colmod.FieldName = "Mod";
            colmod.MinWidth = 23;
            colmod.Name = "colmod";
            colmod.OptionsColumn.AllowEdit = false;
            colmod.Visible = true;
            colmod.VisibleIndex = 3;
            colmod.Width = 63;
            // 
            // colsek
            // 
            colsek.Caption = "Сек. общ.";
            colsek.FieldName = "Sek";
            colsek.MinWidth = 23;
            colsek.Name = "colsek";
            colsek.OptionsColumn.AllowEdit = false;
            colsek.Visible = true;
            colsek.VisibleIndex = 4;
            colsek.Width = 56;
            // 
            // coldateUpdate
            // 
            coldateUpdate.Caption = "Дата утверждения";
            coldateUpdate.ColumnEdit = repositoryItemDateEdit1;
            coldateUpdate.DisplayFormat.FormatString = "d";
            coldateUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            coldateUpdate.FieldName = "dateUpdate";
            coldateUpdate.MinWidth = 23;
            coldateUpdate.Name = "coldateUpdate";
            coldateUpdate.Visible = true;
            coldateUpdate.VisibleIndex = 5;
            coldateUpdate.Width = 69;
            // 
            // repositoryItemDateEdit1
            // 
            repositoryItemDateEdit1.AutoHeight = false;
            repositoryItemDateEdit1.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // colsek_vyaz
            // 
            colsek_vyaz.Caption = "Сек. вяз.";
            colsek_vyaz.FieldName = "SekVyaz";
            colsek_vyaz.MinWidth = 23;
            colsek_vyaz.Name = "colsek_vyaz";
            colsek_vyaz.OptionsColumn.AllowEdit = false;
            colsek_vyaz.Visible = true;
            colsek_vyaz.VisibleIndex = 6;
            colsek_vyaz.Width = 64;
            // 
            // coldateCreate
            // 
            coldateCreate.Caption = "создание";
            coldateCreate.FieldName = "dateCreate";
            coldateCreate.MinWidth = 23;
            coldateCreate.Name = "coldateCreate";
            coldateCreate.OptionsColumn.AllowEdit = false;
            coldateCreate.Width = 87;
            // 
            // gridColumn19
            // 
            gridColumn19.Caption = "обн.";
            gridColumn19.FieldName = "Upd";
            gridColumn19.Name = "gridColumn19";
            // 
            // colsek_shv
            // 
            colsek_shv.Caption = "Сек. шв.";
            colsek_shv.FieldName = "SekShv";
            colsek_shv.MinWidth = 23;
            colsek_shv.Name = "colsek_shv";
            colsek_shv.OptionsColumn.AllowEdit = false;
            colsek_shv.Visible = true;
            colsek_shv.VisibleIndex = 7;
            colsek_shv.Width = 62;
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "Статус";
            gridColumn5.FieldName = "StatusText";
            gridColumn5.MinWidth = 23;
            gridColumn5.Name = "gridColumn5";
            gridColumn5.OptionsColumn.AllowEdit = false;
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 8;
            gridColumn5.Width = 63;
            // 
            // colsek_vyazo
            // 
            colsek_vyazo.Caption = "Сек.отп.";
            colsek_vyazo.FieldName = "SekVyazo";
            colsek_vyazo.MinWidth = 23;
            colsek_vyazo.Name = "colsek_vyazo";
            colsek_vyazo.OptionsColumn.AllowEdit = false;
            colsek_vyazo.Visible = true;
            colsek_vyazo.VisibleIndex = 9;
            colsek_vyazo.Width = 87;
            // 
            // gridColumn35
            // 
            gridColumn35.Caption = "Предв. архив";
            gridColumn35.ColumnEdit = repositoryItemCheckEdit2;
            gridColumn35.FieldName = "preArch";
            gridColumn35.MinWidth = 23;
            gridColumn35.Name = "gridColumn35";
            gridColumn35.OptionsColumn.AllowEdit = false;
            gridColumn35.Visible = true;
            gridColumn35.VisibleIndex = 10;
            gridColumn35.Width = 62;
            // 
            // repositoryItemCheckEdit2
            // 
            repositoryItemCheckEdit2.AutoHeight = false;
            repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // coldateAdd
            // 
            coldateAdd.Caption = "Дата добавления";
            coldateAdd.FieldName = "dateAdd";
            coldateAdd.Name = "coldateAdd";
            coldateAdd.Visible = true;
            coldateAdd.VisibleIndex = 11;
            // 
            // colsek_vyaz5
            // 
            colsek_vyaz5.Caption = "класс5";
            colsek_vyaz5.FieldName = "SekVyaz5";
            colsek_vyaz5.MinWidth = 23;
            colsek_vyaz5.Name = "colsek_vyaz5";
            colsek_vyaz5.OptionsColumn.AllowEdit = false;
            colsek_vyaz5.Visible = true;
            colsek_vyaz5.VisibleIndex = 12;
            colsek_vyaz5.Width = 87;
            // 
            // colsek_vyaz7
            // 
            colsek_vyaz7.Caption = "класс 7";
            colsek_vyaz7.FieldName = "SekVyaz7";
            colsek_vyaz7.MinWidth = 23;
            colsek_vyaz7.Name = "colsek_vyaz7";
            colsek_vyaz7.OptionsColumn.AllowEdit = false;
            colsek_vyaz7.Visible = true;
            colsek_vyaz7.VisibleIndex = 13;
            colsek_vyaz7.Width = 87;
            // 
            // colsek_vyaz12
            // 
            colsek_vyaz12.Caption = "класс 12";
            colsek_vyaz12.FieldName = "SekVyaz12";
            colsek_vyaz12.MinWidth = 23;
            colsek_vyaz12.Name = "colsek_vyaz12";
            colsek_vyaz12.OptionsColumn.AllowEdit = false;
            colsek_vyaz12.Visible = true;
            colsek_vyaz12.VisibleIndex = 14;
            colsek_vyaz12.Width = 87;
            // 
            // colsek_vyaz10
            // 
            colsek_vyaz10.Caption = "класс 10";
            colsek_vyaz10.FieldName = "SekVyaz10";
            colsek_vyaz10.MinWidth = 23;
            colsek_vyaz10.Name = "colsek_vyaz10";
            colsek_vyaz10.OptionsColumn.AllowEdit = false;
            colsek_vyaz10.Visible = true;
            colsek_vyaz10.VisibleIndex = 15;
            colsek_vyaz10.Width = 87;
            // 
            // colsek_vyaz6
            // 
            colsek_vyaz6.Caption = "класс. 6";
            colsek_vyaz6.FieldName = "SekVyaz6";
            colsek_vyaz6.MinWidth = 23;
            colsek_vyaz6.Name = "colsek_vyaz6";
            colsek_vyaz6.OptionsColumn.AllowEdit = false;
            colsek_vyaz6.Visible = true;
            colsek_vyaz6.VisibleIndex = 16;
            colsek_vyaz6.Width = 87;
            // 
            // colsek_kr
            // 
            colsek_kr.Caption = "кручение";
            colsek_kr.FieldName = "SekKr";
            colsek_kr.MinWidth = 23;
            colsek_kr.Name = "colsek_kr";
            colsek_kr.OptionsColumn.AllowEdit = false;
            colsek_kr.Visible = true;
            colsek_kr.VisibleIndex = 17;
            colsek_kr.Width = 87;
            // 
            // colslogn
            // 
            colslogn.Caption = "сложность";
            colslogn.FieldName = "Slogn";
            colslogn.MinWidth = 23;
            colslogn.Name = "colslogn";
            colslogn.OptionsColumn.AllowEdit = false;
            colslogn.Visible = true;
            colslogn.VisibleIndex = 18;
            colslogn.Width = 87;
            // 
            // colkomment
            // 
            colkomment.Caption = "комментарий";
            colkomment.FieldName = "Komment";
            colkomment.MinWidth = 23;
            colkomment.Name = "colkomment";
            colkomment.OptionsColumn.AllowEdit = false;
            colkomment.Width = 155;
            // 
            // colReco
            // 
            colReco.Caption = "рекомендации";
            colReco.FieldName = "annRecommendation";
            colReco.MinWidth = 23;
            colReco.Name = "colReco";
            colReco.OptionsColumn.AllowEdit = false;
            colReco.Visible = true;
            colReco.VisibleIndex = 19;
            colReco.Width = 87;
            // 
            // coldiz
            // 
            coldiz.Caption = "дизайнер";
            coldiz.FieldName = "Diz";
            coldiz.MinWidth = 23;
            coldiz.Name = "coldiz";
            coldiz.OptionsColumn.AllowEdit = false;
            coldiz.Width = 87;
            // 
            // colconstr
            // 
            colconstr.Caption = "конструктор";
            colconstr.FieldName = "Constr";
            colconstr.MinWidth = 23;
            colconstr.Name = "colconstr";
            colconstr.OptionsColumn.AllowEdit = false;
            colconstr.Width = 87;
            // 
            // colannID
            // 
            colannID.FieldName = "AnnID";
            colannID.MinWidth = 23;
            colannID.Name = "colannID";
            colannID.OptionsColumn.AllowEdit = false;
            colannID.Visible = true;
            colannID.VisibleIndex = 20;
            colannID.Width = 87;
            // 
            // gridColumn66
            // 
            gridColumn66.Caption = "статус";
            gridColumn66.FieldName = "Status";
            gridColumn66.Name = "gridColumn66";
            gridColumn66.Visible = true;
            gridColumn66.VisibleIndex = 1;
            // 
            // repositoryItemButtonEdit1
            // 
            repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // repositoryItemButtonEdit3
            // 
            repositoryItemButtonEdit3.AutoHeight = false;
            repositoryItemButtonEdit3.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
            // 
            // ButtonCopyWd
            // 
            ButtonCopyWd.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            ButtonCopyWd.Font = new System.Drawing.Font("Arial", 10F);
            ButtonCopyWd.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            ButtonCopyWd.Location = new System.Drawing.Point(24, 260);
            ButtonCopyWd.Margin = new Padding(4, 3, 4, 3);
            ButtonCopyWd.Name = "ButtonCopyWd";
            ButtonCopyWd.Size = new System.Drawing.Size(228, 33);
            ButtonCopyWd.TabIndex = 10;
            ButtonCopyWd.Text = "копировать в буфер";
            ButtonCopyWd.UseVisualStyleBackColor = false;
            ButtonCopyWd.Visible = false;
            ButtonCopyWd.Click += ButtonCopyWd_Click;
            // 
            // PrintButton
            // 
            PrintButton.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            PrintButton.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            PrintButton.Appearance.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            PrintButton.Appearance.Options.UseBackColor = true;
            PrintButton.Appearance.Options.UseFont = true;
            PrintButton.Appearance.Options.UseForeColor = true;
            PrintButton.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            PrintButton.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            PrintButton.AppearanceDisabled.Options.UseBackColor = true;
            PrintButton.AppearanceDisabled.Options.UseForeColor = true;
            PrintButton.Location = new System.Drawing.Point(24, -152);
            PrintButton.Margin = new Padding(4, 3, 4, 3);
            PrintButton.Name = "PrintButton";
            PrintButton.Size = new System.Drawing.Size(123, 22);
            PrintButton.StyleController = layoutControl2;
            PrintButton.TabIndex = 2;
            PrintButton.Text = "печать";
            PrintButton.Visible = false;
            PrintButton.Click += PrintWorkDivisionScheme_Click;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(SortBox);
            panel5.Controls.Add(archiveCheckBox);
            panel5.Controls.Add(actualCheckBox);
            panel5.Controls.Add(preliminaryCheckBox);
            panel5.Location = new System.Drawing.Point(24, -278);
            panel5.Margin = new Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(228, 122);
            panel5.TabIndex = 0;
            // 
            // SortBox
            // 
            SortBox.AutoSize = true;
            SortBox.Font = new System.Drawing.Font("Arial", 10F);
            SortBox.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            SortBox.Location = new System.Drawing.Point(10, 97);
            SortBox.Margin = new Padding(4, 3, 4, 3);
            SortBox.Name = "SortBox";
            SortBox.Size = new System.Drawing.Size(143, 20);
            SortBox.TabIndex = 4;
            SortBox.Text = "Не утверждённые";
            SortBox.UseVisualStyleBackColor = true;
            SortBox.CheckedChanged += Filter_CheckedChanged;
            // 
            // archiveCheckBox
            // 
            archiveCheckBox.AutoSize = true;
            archiveCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            archiveCheckBox.ForeColor = System.Drawing.Color.FromArgb(128, 64, 0);
            archiveCheckBox.Location = new System.Drawing.Point(10, 63);
            archiveCheckBox.Margin = new Padding(4, 3, 4, 3);
            archiveCheckBox.Name = "archiveCheckBox";
            archiveCheckBox.Size = new System.Drawing.Size(90, 20);
            archiveCheckBox.TabIndex = 3;
            archiveCheckBox.Text = "Архивные";
            archiveCheckBox.UseVisualStyleBackColor = true;
            archiveCheckBox.CheckedChanged += Filter_CheckedChanged;
            // 
            // actualCheckBox
            // 
            actualCheckBox.AutoSize = true;
            actualCheckBox.Checked = true;
            actualCheckBox.CheckState = CheckState.Checked;
            actualCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            actualCheckBox.ForeColor = System.Drawing.Color.FromArgb(128, 64, 0);
            actualCheckBox.Location = new System.Drawing.Point(10, 36);
            actualCheckBox.Margin = new Padding(4, 3, 4, 3);
            actualCheckBox.Name = "actualCheckBox";
            actualCheckBox.Size = new System.Drawing.Size(105, 20);
            actualCheckBox.TabIndex = 2;
            actualCheckBox.Text = "Актуальные";
            actualCheckBox.UseVisualStyleBackColor = true;
            actualCheckBox.CheckedChanged += Filter_CheckedChanged;
            // 
            // preliminaryCheckBox
            // 
            preliminaryCheckBox.AutoSize = true;
            preliminaryCheckBox.Checked = true;
            preliminaryCheckBox.CheckState = CheckState.Checked;
            preliminaryCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            preliminaryCheckBox.ForeColor = System.Drawing.Color.FromArgb(128, 64, 0);
            preliminaryCheckBox.Location = new System.Drawing.Point(10, 12);
            preliminaryCheckBox.Margin = new Padding(4, 3, 4, 3);
            preliminaryCheckBox.Name = "preliminaryCheckBox";
            preliminaryCheckBox.Size = new System.Drawing.Size(147, 20);
            preliminaryCheckBox.TabIndex = 1;
            preliminaryCheckBox.Text = "Предварительные";
            preliminaryCheckBox.UseVisualStyleBackColor = true;
            preliminaryCheckBox.CheckedChanged += Filter_CheckedChanged;
            // 
            // textEditMod
            // 
            textEditMod.Enabled = false;
            textEditMod.Location = new System.Drawing.Point(933, -241);
            textEditMod.Name = "textEditMod";
            textEditMod.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditMod.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            textEditMod.Properties.Appearance.Options.UseFont = true;
            textEditMod.Properties.Appearance.Options.UseForeColor = true;
            textEditMod.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditMod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            textEditMod.Properties.AppearanceDisabled.Options.UseFont = true;
            textEditMod.Properties.AppearanceDisabled.Options.UseForeColor = true;
            textEditMod.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            textEditMod.Size = new System.Drawing.Size(212, 20);
            textEditMod.StyleController = layoutControl2;
            textEditMod.TabIndex = 18;
            // 
            // textEditArt
            // 
            textEditArt.Enabled = false;
            textEditArt.Location = new System.Drawing.Point(1149, -241);
            textEditArt.Name = "textEditArt";
            textEditArt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditArt.Properties.Appearance.Options.UseFont = true;
            textEditArt.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditArt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            textEditArt.Properties.AppearanceDisabled.Options.UseFont = true;
            textEditArt.Properties.AppearanceDisabled.Options.UseForeColor = true;
            textEditArt.Size = new System.Drawing.Size(457, 20);
            textEditArt.StyleController = layoutControl2;
            textEditArt.TabIndex = 19;
            // 
            // textEditSec
            // 
            textEditSec.Enabled = false;
            textEditSec.Location = new System.Drawing.Point(1610, -241);
            textEditSec.Name = "textEditSec";
            textEditSec.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditSec.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            textEditSec.Properties.Appearance.Options.UseFont = true;
            textEditSec.Properties.Appearance.Options.UseForeColor = true;
            textEditSec.Size = new System.Drawing.Size(95, 20);
            textEditSec.StyleController = layoutControl2;
            textEditSec.TabIndex = 20;
            // 
            // textEditCreate
            // 
            textEditCreate.Enabled = false;
            textEditCreate.Location = new System.Drawing.Point(1709, -241);
            textEditCreate.Name = "textEditCreate";
            textEditCreate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditCreate.Properties.Appearance.Options.UseFont = true;
            textEditCreate.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            textEditCreate.Properties.AppearanceDisabled.Options.UseForeColor = true;
            textEditCreate.Size = new System.Drawing.Size(97, 20);
            textEditCreate.StyleController = layoutControl2;
            textEditCreate.TabIndex = 6;
            // 
            // layoutControlGroup7
            // 
            layoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup7.GroupBordersVisible = false;
            layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup8, layoutControlGroup10, splitterItem4, splitterItem5, splitterItem6, layoutControlItem28, simpleSeparator6, layoutControlGroup15, layoutControlGroup12, layoutControlGroup9 });
            layoutControlGroup7.Name = "Root";
            layoutControlGroup7.Size = new System.Drawing.Size(1830, 1042);
            layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlGroup8
            // 
            layoutControlGroup8.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlGroup8.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup8.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Italic);
            layoutControlGroup8.AppearanceItemCaption.Options.UseFont = true;
            layoutControlGroup8.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Добавить предварительное", true, buttonImageOptions34, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions35, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, false, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Редактировать РТ", true, buttonImageOptions36, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions37, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Дубль", true, buttonImageOptions38, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions39, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Архив РТ", true, buttonImageOptions40, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("  ", true, buttonImageOptions41, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("  ", true, buttonImageOptions42, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Печать", true, buttonImageOptions43, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions44, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Печать+", true, buttonImageOptions45, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions46, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("", true, buttonImageOptions47, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup8.CustomizationFormText = "Разделения труда";
            layoutControlGroup8.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            layoutControlGroup8.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem26, layoutControlGroup11 });
            layoutControlGroup8.Location = new System.Drawing.Point(265, 0);
            layoutControlGroup8.Name = "layoutControlGroup8";
            layoutControlGroup8.Size = new System.Drawing.Size(634, 1022);
            layoutControlGroup8.Text = "Разделения труда";
            layoutControlGroup8.CustomButtonClick += layoutControlGroup8_CustomButtonClick;
            // 
            // layoutControlItem26
            // 
            layoutControlItem26.Control = ANNgridControl;
            layoutControlItem26.Location = new System.Drawing.Point(0, 0);
            layoutControlItem26.MinSize = new System.Drawing.Size(104, 24);
            layoutControlItem26.Name = "layoutControlItem26";
            layoutControlItem26.Size = new System.Drawing.Size(610, 616);
            layoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem26.TextVisible = false;
            // 
            // layoutControlGroup11
            // 
            layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem27 });
            layoutControlGroup11.Location = new System.Drawing.Point(0, 616);
            layoutControlGroup11.Name = "layoutControlGroup11";
            layoutControlGroup11.Size = new System.Drawing.Size(610, 360);
            layoutControlGroup11.Text = "Привязанные артикулы";
            // 
            // layoutControlItem27
            // 
            layoutControlItem27.Control = GridControlBindedArts;
            layoutControlItem27.CustomizationFormText = "Привязанные артикулы";
            layoutControlItem27.HighlightFocusedItem = DevExpress.Utils.DefaultBoolean.True;
            layoutControlItem27.Location = new System.Drawing.Point(0, 0);
            layoutControlItem27.Name = "layoutControlItem27";
            layoutControlItem27.Size = new System.Drawing.Size(586, 315);
            layoutControlItem27.Text = "Привязанные артикулы";
            layoutControlItem27.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem27.TextVisible = false;
            // 
            // layoutControlGroup10
            // 
            layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem29 });
            layoutControlGroup10.Location = new System.Drawing.Point(909, 672);
            layoutControlGroup10.Name = "layoutControlGroup10";
            layoutControlGroup10.Size = new System.Drawing.Size(901, 217);
            layoutControlGroup10.Text = "Нормы раскроя";
            // 
            // layoutControlItem29
            // 
            layoutControlItem29.Control = gridControlRaskrTW;
            layoutControlItem29.Location = new System.Drawing.Point(0, 0);
            layoutControlItem29.MaxSize = new System.Drawing.Size(0, 172);
            layoutControlItem29.MinSize = new System.Drawing.Size(104, 172);
            layoutControlItem29.Name = "layoutControlItem29";
            layoutControlItem29.Size = new System.Drawing.Size(877, 172);
            layoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem29.TextVisible = false;
            // 
            // splitterItem4
            // 
            splitterItem4.Location = new System.Drawing.Point(899, 0);
            splitterItem4.Name = "splitterItem4";
            splitterItem4.Size = new System.Drawing.Size(10, 1022);
            // 
            // splitterItem5
            // 
            splitterItem5.Location = new System.Drawing.Point(909, 662);
            splitterItem5.Name = "splitterItem5";
            splitterItem5.Size = new System.Drawing.Size(901, 10);
            // 
            // splitterItem6
            // 
            splitterItem6.Location = new System.Drawing.Point(909, 889);
            splitterItem6.Name = "splitterItem6";
            splitterItem6.Size = new System.Drawing.Size(901, 10);
            // 
            // layoutControlItem28
            // 
            layoutControlItem28.Control = gridControlRaszTW;
            layoutControlItem28.CustomizationFormText = "norm_rasz";
            layoutControlItem28.Location = new System.Drawing.Point(909, 85);
            layoutControlItem28.Name = "layoutControlItem28";
            layoutControlItem28.Size = new System.Drawing.Size(901, 577);
            layoutControlItem28.TextVisible = false;
            // 
            // simpleSeparator6
            // 
            simpleSeparator6.Location = new System.Drawing.Point(264, 0);
            simpleSeparator6.Name = "simpleSeparator6";
            simpleSeparator6.OptionsTableLayoutItem.ColumnIndex = 1;
            simpleSeparator6.Size = new System.Drawing.Size(1, 1022);
            // 
            // layoutControlGroup15
            // 
            layoutControlGroup15.CustomizationFormText = "leftPanel";
            layoutControlGroup15.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem21, layoutControlItem23, layoutControlItem22, layoutControlItem25, layoutControlItem20, layoutControlItem24, layoutControlItem31, layoutControlItem18, layoutControlItem10, layoutControlItem15, layoutControlGroupForAdmins, editBtns, layoutControlGroup18 });
            layoutControlGroup15.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup15.Name = "layoutControlGroup15";
            layoutControlGroup15.Size = new System.Drawing.Size(264, 1022);
            layoutControlGroup15.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            layoutControlItem21.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F);
            layoutControlItem21.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem21.Control = constructorTextBox;
            layoutControlItem21.Location = new System.Drawing.Point(0, 690);
            layoutControlItem21.MaxSize = new System.Drawing.Size(232, 51);
            layoutControlItem21.MinSize = new System.Drawing.Size(232, 51);
            layoutControlItem21.Name = "layoutControlItem21";
            layoutControlItem21.Size = new System.Drawing.Size(240, 51);
            layoutControlItem21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem21.Text = "Конструктор";
            layoutControlItem21.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem21.TextSize = new System.Drawing.Size(228, 18);
            // 
            // layoutControlItem23
            // 
            layoutControlItem23.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F);
            layoutControlItem23.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem23.Control = RecoRichTextBox;
            layoutControlItem23.Location = new System.Drawing.Point(0, 741);
            layoutControlItem23.MaxSize = new System.Drawing.Size(232, 64);
            layoutControlItem23.MinSize = new System.Drawing.Size(232, 64);
            layoutControlItem23.Name = "layoutControlItem23";
            layoutControlItem23.Size = new System.Drawing.Size(240, 64);
            layoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem23.Tag = "";
            layoutControlItem23.Text = "Рекомендации для планирования";
            layoutControlItem23.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem23.TextSize = new System.Drawing.Size(228, 18);
            // 
            // layoutControlItem22
            // 
            layoutControlItem22.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F);
            layoutControlItem22.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem22.Control = commentRichTextBox;
            layoutControlItem22.Location = new System.Drawing.Point(0, 805);
            layoutControlItem22.MaxSize = new System.Drawing.Size(232, 65);
            layoutControlItem22.MinSize = new System.Drawing.Size(232, 65);
            layoutControlItem22.Name = "layoutControlItem22";
            layoutControlItem22.Size = new System.Drawing.Size(240, 65);
            layoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem22.Text = "Особенности модели";
            layoutControlItem22.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem22.TextSize = new System.Drawing.Size(228, 18);
            // 
            // layoutControlItem25
            // 
            layoutControlItem25.Control = buffer;
            layoutControlItem25.CustomizationFormText = "bufferEdit";
            layoutControlItem25.Location = new System.Drawing.Point(0, 575);
            layoutControlItem25.MaxSize = new System.Drawing.Size(232, 64);
            layoutControlItem25.MinSize = new System.Drawing.Size(232, 64);
            layoutControlItem25.Name = "layoutControlItem25";
            layoutControlItem25.Size = new System.Drawing.Size(240, 64);
            layoutControlItem25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem25.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            layoutControlItem20.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F);
            layoutControlItem20.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem20.Control = designerTextBox;
            layoutControlItem20.Location = new System.Drawing.Point(0, 639);
            layoutControlItem20.MaxSize = new System.Drawing.Size(232, 51);
            layoutControlItem20.MinSize = new System.Drawing.Size(232, 51);
            layoutControlItem20.Name = "layoutControlItem20";
            layoutControlItem20.Size = new System.Drawing.Size(240, 51);
            layoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem20.Text = "Дизайнер";
            layoutControlItem20.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem20.TextSize = new System.Drawing.Size(228, 18);
            // 
            // layoutControlItem24
            // 
            layoutControlItem24.Control = ButtonCopyWd;
            layoutControlItem24.CustomizationFormText = "bufferBtn";
            layoutControlItem24.Location = new System.Drawing.Point(0, 538);
            layoutControlItem24.MaxSize = new System.Drawing.Size(232, 37);
            layoutControlItem24.MinSize = new System.Drawing.Size(232, 37);
            layoutControlItem24.Name = "layoutControlItem24";
            layoutControlItem24.Size = new System.Drawing.Size(240, 37);
            layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem24.TextVisible = false;
            // 
            // layoutControlItem31
            // 
            layoutControlItem31.Control = pictureBox1;
            layoutControlItem31.CustomizationFormText = "pictureBox";
            layoutControlItem31.Location = new System.Drawing.Point(0, 364);
            layoutControlItem31.MinSize = new System.Drawing.Size(104, 174);
            layoutControlItem31.Name = "layoutControlItem31";
            layoutControlItem31.Size = new System.Drawing.Size(240, 174);
            layoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem31.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            layoutControlItem18.Control = PrintButton;
            layoutControlItem18.CustomizationFormText = "printBtn";
            layoutControlItem18.Location = new System.Drawing.Point(0, 126);
            layoutControlItem18.Name = "layoutControlItem18";
            layoutControlItem18.Size = new System.Drawing.Size(127, 26);
            layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.Control = panel5;
            layoutControlItem10.CustomizationFormText = "filtres";
            layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            layoutControlItem10.MaxSize = new System.Drawing.Size(232, 126);
            layoutControlItem10.MinSize = new System.Drawing.Size(232, 126);
            layoutControlItem10.Name = "layoutControlItem10";
            layoutControlItem10.Size = new System.Drawing.Size(240, 126);
            layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            layoutControlItem15.Control = printButtonPlus;
            layoutControlItem15.CustomizationFormText = "print+Btn";
            layoutControlItem15.Location = new System.Drawing.Point(127, 126);
            layoutControlItem15.Name = "layoutControlItem15";
            layoutControlItem15.Size = new System.Drawing.Size(113, 26);
            layoutControlItem15.TextVisible = false;
            // 
            // layoutControlGroupForAdmins
            // 
            layoutControlGroupForAdmins.CustomizationFormText = "ButtonsForAdmins";
            layoutControlGroupForAdmins.ExpandOnDoubleClick = true;
            layoutControlGroupForAdmins.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem43, layoutControlItem41, layoutControlItem42, layoutControlItem44, layoutControlItem45, layoutControlItem12, layoutControlItem16 });
            layoutControlGroupForAdmins.Location = new System.Drawing.Point(0, 870);
            layoutControlGroupForAdmins.Name = "layoutControlGroupForAdmins";
            layoutControlGroupForAdmins.Size = new System.Drawing.Size(240, 128);
            layoutControlGroupForAdmins.Text = "ButtonForAdmins";
            layoutControlGroupForAdmins.TextVisible = false;
            // 
            // layoutControlItem43
            // 
            layoutControlItem43.Control = customSimpleButtonUnbind;
            layoutControlItem43.Location = new System.Drawing.Point(0, 0);
            layoutControlItem43.Name = "layoutControlItem43";
            layoutControlItem43.Size = new System.Drawing.Size(159, 26);
            layoutControlItem43.TextVisible = false;
            // 
            // layoutControlItem41
            // 
            layoutControlItem41.Control = customSimpleButtonArch;
            layoutControlItem41.Location = new System.Drawing.Point(111, 78);
            layoutControlItem41.MaxSize = new System.Drawing.Size(0, 26);
            layoutControlItem41.MinSize = new System.Drawing.Size(69, 26);
            layoutControlItem41.Name = "layoutControlItem41";
            layoutControlItem41.Size = new System.Drawing.Size(105, 26);
            layoutControlItem41.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem41.TextVisible = false;
            // 
            // layoutControlItem42
            // 
            layoutControlItem42.Control = customSimpleButtonArchARTICUL;
            layoutControlItem42.Location = new System.Drawing.Point(159, 0);
            layoutControlItem42.Name = "layoutControlItem42";
            layoutControlItem42.Size = new System.Drawing.Size(57, 26);
            layoutControlItem42.TextVisible = false;
            // 
            // layoutControlItem44
            // 
            layoutControlItem44.Control = customSimpleButtonUpd;
            layoutControlItem44.Location = new System.Drawing.Point(0, 78);
            layoutControlItem44.Name = "layoutControlItem44";
            layoutControlItem44.Size = new System.Drawing.Size(111, 26);
            layoutControlItem44.TextVisible = false;
            // 
            // layoutControlItem45
            // 
            layoutControlItem45.Control = customSimpleButtonDel;
            layoutControlItem45.Location = new System.Drawing.Point(0, 26);
            layoutControlItem45.MaxSize = new System.Drawing.Size(0, 26);
            layoutControlItem45.MinSize = new System.Drawing.Size(161, 26);
            layoutControlItem45.Name = "layoutControlItem45";
            layoutControlItem45.Size = new System.Drawing.Size(161, 26);
            layoutControlItem45.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem45.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            layoutControlItem12.Control = customSimpleButtonAnnLog;
            layoutControlItem12.Location = new System.Drawing.Point(161, 26);
            layoutControlItem12.Name = "layoutControlItem12";
            layoutControlItem12.Size = new System.Drawing.Size(55, 26);
            layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            layoutControlItem16.Control = customSimpleButtonRaszLog;
            layoutControlItem16.Location = new System.Drawing.Point(0, 52);
            layoutControlItem16.Name = "layoutControlItem16";
            layoutControlItem16.Size = new System.Drawing.Size(216, 26);
            layoutControlItem16.TextVisible = false;
            // 
            // editBtns
            // 
            editBtns.CustomizationFormText = "editBtns";
            editBtns.ExpandOnDoubleClick = true;
            editBtns.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlEditWd, layoutControlItem46, layoutControlItem36, layoutControlEditOnlyAdv });
            editBtns.Location = new System.Drawing.Point(0, 253);
            editBtns.Name = "editBtns";
            editBtns.Size = new System.Drawing.Size(240, 111);
            editBtns.TextVisible = false;
            // 
            // layoutControlEditWd
            // 
            layoutControlEditWd.Control = ButtonEditWd;
            layoutControlEditWd.CustomizationFormText = "EditWD";
            layoutControlEditWd.Location = new System.Drawing.Point(0, 0);
            layoutControlEditWd.MaxSize = new System.Drawing.Size(0, 29);
            layoutControlEditWd.MinSize = new System.Drawing.Size(24, 29);
            layoutControlEditWd.Name = "layoutControlEditWd";
            layoutControlEditWd.Size = new System.Drawing.Size(108, 29);
            layoutControlEditWd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlEditWd.TextVisible = false;
            // 
            // layoutControlItem46
            // 
            layoutControlItem46.Control = ButtonArchAndCopyWd;
            layoutControlItem46.CustomizationFormText = "Arch+Copy";
            layoutControlItem46.Location = new System.Drawing.Point(0, 58);
            layoutControlItem46.MaxSize = new System.Drawing.Size(0, 29);
            layoutControlItem46.MinSize = new System.Drawing.Size(24, 29);
            layoutControlItem46.Name = "layoutControlItem46";
            layoutControlItem46.Size = new System.Drawing.Size(216, 29);
            layoutControlItem46.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem46.TextVisible = false;
            // 
            // layoutControlItem36
            // 
            layoutControlItem36.Control = ButtonDouble;
            layoutControlItem36.CustomizationFormText = "Double";
            layoutControlItem36.Location = new System.Drawing.Point(0, 29);
            layoutControlItem36.MaxSize = new System.Drawing.Size(0, 29);
            layoutControlItem36.MinSize = new System.Drawing.Size(24, 29);
            layoutControlItem36.Name = "layoutControlItem36";
            layoutControlItem36.Size = new System.Drawing.Size(216, 29);
            layoutControlItem36.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem36.TextVisible = false;
            // 
            // layoutControlEditOnlyAdv
            // 
            layoutControlEditOnlyAdv.Control = ButtonEditOnlyAdv;
            layoutControlEditOnlyAdv.CustomizationFormText = "AdvancedEditWD";
            layoutControlEditOnlyAdv.Location = new System.Drawing.Point(108, 0);
            layoutControlEditOnlyAdv.MaxSize = new System.Drawing.Size(0, 29);
            layoutControlEditOnlyAdv.MinSize = new System.Drawing.Size(24, 29);
            layoutControlEditOnlyAdv.Name = "layoutControlEditOnlyAdv";
            layoutControlEditOnlyAdv.Size = new System.Drawing.Size(108, 29);
            layoutControlEditOnlyAdv.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlEditOnlyAdv.TextVisible = false;
            // 
            // layoutControlGroup18
            // 
            layoutControlGroup18.CustomizationFormText = "Переключатель режимов";
            layoutControlGroup18.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem7, layoutControlItem6, layoutControlItem5 });
            layoutControlGroup18.Location = new System.Drawing.Point(0, 152);
            layoutControlGroup18.Name = "layoutControlGroup18";
            layoutControlGroup18.Size = new System.Drawing.Size(240, 101);
            layoutControlGroup18.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = toggleSwitchKit;
            layoutControlItem7.CustomizationFormText = "kit toggle";
            layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new System.Drawing.Size(216, 24);
            layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = KITlabel;
            layoutControlItem6.CustomizationFormText = "kit label";
            layoutControlItem6.Location = new System.Drawing.Point(0, 24);
            layoutControlItem6.MinSize = new System.Drawing.Size(24, 24);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(216, 24);
            layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem6.TextVisible = false;
            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInRuntime;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = ButtonPreliminaryWd;
            layoutControlItem5.CustomizationFormText = "kit create";
            layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            layoutControlItem5.MaxSize = new System.Drawing.Size(0, 29);
            layoutControlItem5.MinSize = new System.Drawing.Size(24, 29);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(216, 29);
            layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem30 });
            layoutControlGroup12.Location = new System.Drawing.Point(909, 899);
            layoutControlGroup12.Name = "layoutControlGroup12";
            layoutControlGroup12.Size = new System.Drawing.Size(901, 123);
            layoutControlGroup12.Text = "Операции комплектовки";
            // 
            // layoutControlItem30
            // 
            layoutControlItem30.Control = gridControlKontTW;
            layoutControlItem30.Location = new System.Drawing.Point(0, 0);
            layoutControlItem30.MaxSize = new System.Drawing.Size(0, 78);
            layoutControlItem30.MinSize = new System.Drawing.Size(104, 78);
            layoutControlItem30.Name = "layoutControlItem30";
            layoutControlItem30.Size = new System.Drawing.Size(877, 78);
            layoutControlItem30.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem30.TextVisible = false;
            // 
            // layoutControlGroup9
            // 
            layoutControlGroup9.CustomizationFormText = "Схема РТ";
            layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem35, layoutControlItem32, layoutControlItem33, layoutControlItem34 });
            layoutControlGroup9.Location = new System.Drawing.Point(909, 0);
            layoutControlGroup9.Name = "layoutControlGroup9";
            layoutControlGroup9.Size = new System.Drawing.Size(901, 85);
            layoutControlGroup9.Text = "Схема РТ";
            // 
            // layoutControlItem35
            // 
            layoutControlItem35.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlItem35.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            layoutControlItem35.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem35.AppearanceItemCaption.Options.UseForeColor = true;
            layoutControlItem35.AppearanceItemCaptionDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlItem35.AppearanceItemCaptionDisabled.ForeColor = System.Drawing.Color.Black;
            layoutControlItem35.AppearanceItemCaptionDisabled.Options.UseFont = true;
            layoutControlItem35.AppearanceItemCaptionDisabled.Options.UseForeColor = true;
            layoutControlItem35.Control = textEditCreate;
            layoutControlItem35.Location = new System.Drawing.Point(776, 0);
            layoutControlItem35.MinSize = new System.Drawing.Size(50, 40);
            layoutControlItem35.Name = "layoutControlItem35";
            layoutControlItem35.Size = new System.Drawing.Size(101, 40);
            layoutControlItem35.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem35.Text = "Дата созд.";
            layoutControlItem35.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem35.TextSize = new System.Drawing.Size(228, 13);
            // 
            // layoutControlItem32
            // 
            layoutControlItem32.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlItem32.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            layoutControlItem32.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem32.AppearanceItemCaption.Options.UseForeColor = true;
            layoutControlItem32.AppearanceItemCaption.Options.UseTextOptions = true;
            layoutControlItem32.AppearanceItemCaptionDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlItem32.AppearanceItemCaptionDisabled.ForeColor = System.Drawing.Color.Black;
            layoutControlItem32.AppearanceItemCaptionDisabled.Options.UseFont = true;
            layoutControlItem32.AppearanceItemCaptionDisabled.Options.UseForeColor = true;
            layoutControlItem32.Control = textEditMod;
            layoutControlItem32.Location = new System.Drawing.Point(0, 0);
            layoutControlItem32.MinSize = new System.Drawing.Size(1, 40);
            layoutControlItem32.Name = "layoutControlItem32";
            layoutControlItem32.Size = new System.Drawing.Size(216, 40);
            layoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem32.Text = "Модель";
            layoutControlItem32.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem32.TextSize = new System.Drawing.Size(228, 13);
            // 
            // layoutControlItem33
            // 
            layoutControlItem33.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlItem33.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            layoutControlItem33.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem33.AppearanceItemCaption.Options.UseForeColor = true;
            layoutControlItem33.AppearanceItemCaptionDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlItem33.AppearanceItemCaptionDisabled.ForeColor = System.Drawing.Color.Black;
            layoutControlItem33.AppearanceItemCaptionDisabled.Options.UseFont = true;
            layoutControlItem33.AppearanceItemCaptionDisabled.Options.UseForeColor = true;
            layoutControlItem33.Control = textEditArt;
            layoutControlItem33.Location = new System.Drawing.Point(216, 0);
            layoutControlItem33.Name = "layoutControlItem33";
            layoutControlItem33.Size = new System.Drawing.Size(461, 40);
            layoutControlItem33.Text = "Артикул";
            layoutControlItem33.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem33.TextSize = new System.Drawing.Size(228, 13);
            // 
            // layoutControlItem34
            // 
            layoutControlItem34.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlItem34.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            layoutControlItem34.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem34.AppearanceItemCaption.Options.UseForeColor = true;
            layoutControlItem34.AppearanceItemCaptionDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            layoutControlItem34.AppearanceItemCaptionDisabled.ForeColor = System.Drawing.Color.Black;
            layoutControlItem34.AppearanceItemCaptionDisabled.Options.UseFont = true;
            layoutControlItem34.AppearanceItemCaptionDisabled.Options.UseForeColor = true;
            layoutControlItem34.Control = textEditSec;
            layoutControlItem34.Location = new System.Drawing.Point(677, 0);
            layoutControlItem34.MinSize = new System.Drawing.Size(50, 40);
            layoutControlItem34.Name = "layoutControlItem34";
            layoutControlItem34.Size = new System.Drawing.Size(99, 40);
            layoutControlItem34.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem34.Text = "Сек.";
            layoutControlItem34.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem34.TextSize = new System.Drawing.Size(228, 13);
            // 
            // xtraTabPageArticles
            // 
            xtraTabPageArticles.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            xtraTabPageArticles.Appearance.HeaderActive.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            xtraTabPageArticles.Appearance.HeaderActive.Options.UseFont = true;
            xtraTabPageArticles.Appearance.HeaderActive.Options.UseForeColor = true;
            xtraTabPageArticles.Controls.Add(xtraTabControl2);
            xtraTabPageArticles.Margin = new Padding(4, 3, 4, 3);
            xtraTabPageArticles.Name = "xtraTabPageArticles";
            xtraTabPageArticles.Size = new System.Drawing.Size(1847, 740);
            xtraTabPageArticles.Text = "2. Текущие работы                             ";
            // 
            // xtraTabControl2
            // 
            xtraTabControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            xtraTabControl2.Appearance.Options.UseBackColor = true;
            xtraTabControl2.AppearancePage.HeaderDisabled.BackColor = System.Drawing.Color.Transparent;
            xtraTabControl2.AppearancePage.HeaderDisabled.Options.UseBackColor = true;
            xtraTabControl2.AppearancePage.HeaderHotTracked.BackColor = System.Drawing.Color.Transparent;
            xtraTabControl2.AppearancePage.HeaderHotTracked.Options.UseBackColor = true;
            xtraTabControl2.AppearancePage.PageClient.BackColor = System.Drawing.Color.Transparent;
            xtraTabControl2.AppearancePage.PageClient.Options.UseBackColor = true;
            xtraTabControl2.Dock = DockStyle.Fill;
            xtraTabControl2.Location = new System.Drawing.Point(0, 0);
            xtraTabControl2.LookAndFeel.SkinMaskColor = System.Drawing.Color.Transparent;
            xtraTabControl2.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Transparent;
            xtraTabControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            xtraTabControl2.Margin = new Padding(4, 3, 4, 3);
            xtraTabControl2.Name = "xtraTabControl2";
            xtraTabControl2.SelectedTabPage = xtraTabPageWorkDivisions;
            xtraTabControl2.Size = new System.Drawing.Size(1847, 740);
            xtraTabControl2.TabIndex = 0;
            xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { xtraTabPageWorkDivisions, xtraTabPage3 });
            // 
            // xtraTabPageWorkDivisions
            // 
            xtraTabPageWorkDivisions.Appearance.PageClient.BackColor = System.Drawing.Color.Transparent;
            xtraTabPageWorkDivisions.Appearance.PageClient.Options.UseBackColor = true;
            xtraTabPageWorkDivisions.BackgroundImageLayout = ImageLayout.Stretch;
            xtraTabPageWorkDivisions.Controls.Add(panelControl2);
            xtraTabPageWorkDivisions.Margin = new Padding(4, 3, 4, 3);
            xtraTabPageWorkDivisions.Name = "xtraTabPageWorkDivisions";
            xtraTabPageWorkDivisions.Size = new System.Drawing.Size(1845, 715);
            xtraTabPageWorkDivisions.Text = "Требуют увязки";
            // 
            // panelControl2
            // 
            panelControl2.Appearance.BackColor = System.Drawing.Color.Yellow;
            panelControl2.Appearance.Options.UseBackColor = true;
            panelControl2.AutoSize = true;
            panelControl2.Controls.Add(layoutControl1);
            panelControl2.Dock = DockStyle.Fill;
            panelControl2.Location = new System.Drawing.Point(0, 0);
            panelControl2.Margin = new Padding(4, 3, 4, 3);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1845, 715);
            panelControl2.TabIndex = 4;
            // 
            // layoutControl1
            // 
            layoutControl1.BackColor = System.Drawing.Color.Transparent;
            layoutControl1.Controls.Add(searchControl1);
            layoutControl1.Controls.Add(pictureBox3);
            layoutControl1.Controls.Add(customGridControl2);
            layoutControl1.Controls.Add(customGridControl1);
            layoutControl1.Controls.Add(customLabel2);
            layoutControl1.Controls.Add(pictureBox2);
            layoutControl1.Controls.Add(gridControl_binded);
            layoutControl1.Controls.Add(gridControlNZP);
            layoutControl1.Controls.Add(customGridControl3);
            layoutControl1.Controls.Add(gridControl_wdToBind);
            layoutControl1.Controls.Add(gridControl_unboundArts);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(2, 2);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(390, 213, 650, 390);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1841, 711);
            layoutControl1.TabIndex = 4;
            layoutControl1.Text = "layoutControl1";
            // 
            // searchControl1
            // 
            searchControl1.Location = new System.Drawing.Point(1525, 78);
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Buttons.AddRange(new EditorButton[] { new ClearButton(), new SearchButton(), new MRUButton() });
            searchControl1.Properties.ShowDefaultButtonsMode = ShowDefaultButtonsMode.Always;
            searchControl1.Properties.ShowMRUButton = true;
            searchControl1.Size = new System.Drawing.Size(280, 20);
            searchControl1.StyleController = layoutControl1;
            searchControl1.TabIndex = 4;
            searchControl1.TextChanged += searchControl1_TextChanged;
            searchControl1.KeyPress += searchControl1_KeyPress;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new System.Drawing.Point(24, 369);
            pictureBox3.Margin = new Padding(4, 3, 4, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new System.Drawing.Size(582, 205);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // customGridControl2
            // 
            customGridControl2.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl2.Location = new System.Drawing.Point(668, 446);
            customGridControl2.MainView = normRaskArt;
            customGridControl2.Name = "customGridControl2";
            customGridControl2.Size = new System.Drawing.Size(861, 217);
            customGridControl2.TabIndex = 1;
            customGridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { normRaskArt });
            // 
            // normRaskArt
            // 
            normRaskArt.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            normRaskArt.Appearance.EvenRow.Options.UseBackColor = true;
            normRaskArt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { Kod_o, TextRask, razryd, sek, spec, Obor });
            normRaskArt.GridControl = customGridControl2;
            normRaskArt.Name = "normRaskArt";
            normRaskArt.OptionsView.EnableAppearanceEvenRow = true;
            normRaskArt.OptionsView.ShowDetailButtons = false;
            normRaskArt.OptionsView.ShowGroupPanel = false;
            normRaskArt.OptionsView.ShowIndicator = false;
            // 
            // Kod_o
            // 
            Kod_o.Caption = "№";
            Kod_o.FieldName = "Kod_o";
            Kod_o.Name = "Kod_o";
            Kod_o.Visible = true;
            Kod_o.VisibleIndex = 0;
            Kod_o.Width = 50;
            // 
            // TextRask
            // 
            TextRask.Caption = "наименование операции раскроя";
            TextRask.FieldName = "TextRask";
            TextRask.Name = "TextRask";
            TextRask.Visible = true;
            TextRask.VisibleIndex = 1;
            TextRask.Width = 300;
            // 
            // razryd
            // 
            razryd.Caption = "разряд";
            razryd.FieldName = "razryd";
            razryd.Name = "razryd";
            razryd.Visible = true;
            razryd.VisibleIndex = 2;
            razryd.Width = 80;
            // 
            // sek
            // 
            sek.Caption = "сек.";
            sek.FieldName = "Sek";
            sek.Name = "sek";
            sek.Visible = true;
            sek.VisibleIndex = 3;
            sek.Width = 60;
            // 
            // spec
            // 
            spec.Caption = "спец-ть";
            spec.FieldName = "Spec";
            spec.Name = "spec";
            spec.Visible = true;
            spec.VisibleIndex = 4;
            spec.Width = 100;
            // 
            // Obor
            // 
            Obor.Caption = "обор.";
            Obor.FieldName = "Obor";
            Obor.Name = "Obor";
            Obor.Visible = true;
            Obor.VisibleIndex = 5;
            Obor.Width = 150;
            // 
            // customGridControl1
            // 
            customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl1.Location = new System.Drawing.Point(668, 446);
            customGridControl1.MainView = normKontTab;
            customGridControl1.Name = "customGridControl1";
            customGridControl1.Size = new System.Drawing.Size(861, 217);
            customGridControl1.TabIndex = 1;
            customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { normKontTab });
            // 
            // normKontTab
            // 
            normKontTab.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            normKontTab.Appearance.EvenRow.Options.UseBackColor = true;
            normKontTab.GridControl = customGridControl1;
            normKontTab.Name = "normKontTab";
            normKontTab.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // customLabel2
            // 
            customLabel2.Font = new System.Drawing.Font("Arial", 10F);
            customLabel2.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabel2.Location = new System.Drawing.Point(24, 578);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new System.Drawing.Size(582, 24);
            customLabel2.TabIndex = 1;
            customLabel2.Text = "Увязанные в этом сеансе";
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new System.Drawing.Point(1555, 411);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(250, 264);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // gridControl_binded
            // 
            gridControl_binded.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControl_binded.Font = new System.Drawing.Font("Arial", 10F);
            gridControl_binded.Location = new System.Drawing.Point(24, 606);
            gridControl_binded.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl_binded.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControl_binded.MainView = gridView_binded;
            gridControl_binded.Margin = new Padding(4, 3, 4, 3);
            gridControl_binded.Name = "gridControl_binded";
            gridControl_binded.Size = new System.Drawing.Size(582, 81);
            gridControl_binded.TabIndex = 2;
            gridControl_binded.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_binded });
            // 
            // gridView_binded
            // 
            gridView_binded.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn36, gridColumn37, gridColumn38, gridColumn39, gridColumn40 });
            gridView_binded.DetailHeight = 404;
            gridView_binded.GridControl = gridControl_binded;
            gridView_binded.Name = "gridView_binded";
            gridView_binded.OptionsEditForm.PopupEditFormWidth = 933;
            gridView_binded.OptionsView.ShowGroupPanel = false;
            gridView_binded.PopupMenuShowing += gridView_binded_PopupMenuShowing;
            // 
            // gridColumn36
            // 
            gridColumn36.Caption = "артикул";
            gridColumn36.FieldName = "Articul";
            gridColumn36.MinWidth = 23;
            gridColumn36.Name = "gridColumn36";
            gridColumn36.OptionsColumn.AllowEdit = false;
            gridColumn36.Visible = true;
            gridColumn36.VisibleIndex = 0;
            gridColumn36.Width = 87;
            // 
            // gridColumn37
            // 
            gridColumn37.Caption = "код";
            gridColumn37.FieldName = "Kod";
            gridColumn37.MinWidth = 23;
            gridColumn37.Name = "gridColumn37";
            gridColumn37.OptionsColumn.AllowEdit = false;
            gridColumn37.Visible = true;
            gridColumn37.VisibleIndex = 1;
            gridColumn37.Width = 87;
            // 
            // gridColumn38
            // 
            gridColumn38.Caption = "группа";
            gridColumn38.FieldName = "grup";
            gridColumn38.MinWidth = 23;
            gridColumn38.Name = "gridColumn38";
            gridColumn38.OptionsColumn.AllowEdit = false;
            gridColumn38.Visible = true;
            gridColumn38.VisibleIndex = 2;
            gridColumn38.Width = 87;
            // 
            // gridColumn39
            // 
            gridColumn39.Caption = "модель";
            gridColumn39.FieldName = "mod";
            gridColumn39.MinWidth = 23;
            gridColumn39.Name = "gridColumn39";
            gridColumn39.OptionsColumn.AllowEdit = false;
            gridColumn39.Visible = true;
            gridColumn39.VisibleIndex = 3;
            gridColumn39.Width = 87;
            // 
            // gridColumn40
            // 
            gridColumn40.Caption = "ранее увязанные";
            gridColumn40.FieldName = "BindedArt";
            gridColumn40.MinWidth = 23;
            gridColumn40.Name = "gridColumn40";
            gridColumn40.OptionsColumn.AllowEdit = false;
            gridColumn40.Visible = true;
            gridColumn40.VisibleIndex = 4;
            gridColumn40.Width = 87;
            // 
            // gridControlNZP
            // 
            gridControlNZP.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlNZP.Font = new System.Drawing.Font("Arial", 10F);
            gridControlNZP.Location = new System.Drawing.Point(1318, 102);
            gridControlNZP.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControlNZP.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControlNZP.MainView = gridViewNZP;
            gridControlNZP.Margin = new Padding(4, 3, 4, 3);
            gridControlNZP.Name = "gridControlNZP";
            gridControlNZP.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemCheckEdit8 });
            gridControlNZP.Size = new System.Drawing.Size(487, 250);
            gridControlNZP.TabIndex = 5;
            gridControlNZP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewNZP });
            // 
            // gridViewNZP
            // 
            gridViewNZP.Appearance.SelectedRow.FontStyleDelta = System.Drawing.FontStyle.Bold;
            gridViewNZP.Appearance.SelectedRow.Options.UseFont = true;
            gridViewNZP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn53, kodd_rt, colannId2, gridColumn20, gridColumn21, gridColumn22, gridColumn23, gridColumn54, gridColumn55, kolNZP, PztCount });
            gridViewNZP.DetailHeight = 404;
            gridViewNZP.GridControl = gridControlNZP;
            gridViewNZP.GroupFormat = "{0}:  {1}{2}";
            gridViewNZP.Name = "gridViewNZP";
            gridViewNZP.OptionsBehavior.Editable = false;
            gridViewNZP.OptionsBehavior.ReadOnly = true;
            gridViewNZP.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridViewNZP.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewNZP.OptionsSelection.MultiSelect = true;
            gridViewNZP.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridViewNZP.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridViewNZP.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gridViewNZP.OptionsView.ShowGroupPanel = false;
            gridViewNZP.PopupMenuShowing += gridView_unboundArts_PopupMenuShowing;
            gridViewNZP.FocusedRowChanged += gridViewNZP_FocusedRowChanged;
            // 
            // gridColumn53
            // 
            gridColumn53.Caption = " ";
            gridColumn53.ColumnEdit = repositoryItemCheckEdit8;
            gridColumn53.FieldName = "IsChecked";
            gridColumn53.Name = "gridColumn53";
            gridColumn53.Visible = true;
            gridColumn53.VisibleIndex = 0;
            gridColumn53.Width = 36;
            // 
            // repositoryItemCheckEdit8
            // 
            repositoryItemCheckEdit8.AutoHeight = false;
            repositoryItemCheckEdit8.Name = "repositoryItemCheckEdit8";
            // 
            // kodd_rt
            // 
            kodd_rt.Caption = "Код";
            kodd_rt.FieldName = "kodd_rt";
            kodd_rt.MinWidth = 23;
            kodd_rt.Name = "kodd_rt";
            kodd_rt.Visible = true;
            kodd_rt.VisibleIndex = 8;
            kodd_rt.Width = 87;
            // 
            // colannId2
            // 
            colannId2.FieldName = "annId";
            colannId2.MinWidth = 23;
            colannId2.Name = "colannId2";
            colannId2.Width = 49;
            // 
            // gridColumn20
            // 
            gridColumn20.Caption = "Код";
            gridColumn20.FieldName = "kodd";
            gridColumn20.MinWidth = 23;
            gridColumn20.Name = "gridColumn20";
            gridColumn20.Width = 84;
            // 
            // gridColumn21
            // 
            gridColumn21.Caption = "Группа";
            gridColumn21.FieldName = "grup";
            gridColumn21.MinWidth = 23;
            gridColumn21.Name = "gridColumn21";
            gridColumn21.OptionsColumn.AllowEdit = false;
            gridColumn21.Visible = true;
            gridColumn21.VisibleIndex = 1;
            gridColumn21.Width = 108;
            // 
            // gridColumn22
            // 
            gridColumn22.Caption = "Артикул";
            gridColumn22.FieldName = "articul";
            gridColumn22.MinWidth = 23;
            gridColumn22.Name = "gridColumn22";
            gridColumn22.OptionsColumn.AllowEdit = false;
            gridColumn22.Visible = true;
            gridColumn22.VisibleIndex = 2;
            gridColumn22.Width = 102;
            // 
            // gridColumn23
            // 
            gridColumn23.Caption = "Модель";
            gridColumn23.FieldName = "mod";
            gridColumn23.MinWidth = 23;
            gridColumn23.Name = "gridColumn23";
            gridColumn23.OptionsColumn.AllowEdit = false;
            gridColumn23.Visible = true;
            gridColumn23.VisibleIndex = 3;
            gridColumn23.Width = 115;
            // 
            // gridColumn54
            // 
            gridColumn54.Caption = "Мин размер";
            gridColumn54.FieldName = "minSize";
            gridColumn54.Name = "gridColumn54";
            gridColumn54.Visible = true;
            gridColumn54.VisibleIndex = 4;
            // 
            // gridColumn55
            // 
            gridColumn55.Caption = "Макс размер";
            gridColumn55.FieldName = "maxSize";
            gridColumn55.Name = "gridColumn55";
            gridColumn55.Visible = true;
            gridColumn55.VisibleIndex = 5;
            // 
            // kolNZP
            // 
            kolNZP.Caption = "Наличие НЗП";
            kolNZP.FieldName = "kolNZP";
            kolNZP.MinWidth = 23;
            kolNZP.Name = "kolNZP";
            kolNZP.OptionsColumn.AllowEdit = false;
            kolNZP.Visible = true;
            kolNZP.VisibleIndex = 6;
            kolNZP.Width = 67;
            // 
            // PztCount
            // 
            PztCount.Caption = "Кол-во назн. опер.";
            PztCount.FieldName = "PZTCount";
            PztCount.MinWidth = 23;
            PztCount.Name = "PztCount";
            PztCount.OptionsColumn.AllowEdit = false;
            PztCount.Visible = true;
            PztCount.VisibleIndex = 7;
            PztCount.Width = 98;
            // 
            // customGridControl3
            // 
            customGridControl3.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            customGridControl3.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl3.Location = new System.Drawing.Point(668, 446);
            customGridControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            customGridControl3.MainView = normRaszTab;
            customGridControl3.Margin = new Padding(4, 3, 4, 3);
            customGridControl3.Name = "customGridControl3";
            customGridControl3.Size = new System.Drawing.Size(861, 217);
            customGridControl3.TabIndex = 7;
            customGridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { normRaszTab });
            // 
            // normRaszTab
            // 
            normRaszTab.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
            normRaszTab.Appearance.SelectedRow.Options.UseBackColor = true;
            normRaszTab.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colannId1, coln3, gridColumn9, gridColumn10, gridColumn11, gridColumn49, gridColumn50 });
            normRaszTab.DetailHeight = 404;
            normRaszTab.GridControl = customGridControl3;
            normRaszTab.Name = "normRaszTab";
            normRaszTab.OptionsEditForm.PopupEditFormWidth = 933;
            normRaszTab.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            normRaszTab.OptionsView.ShowGroupPanel = false;
            normRaszTab.PopupMenuShowing += gridView_unboundArts_PopupMenuShowing;
            // 
            // colannId1
            // 
            colannId1.FieldName = "annId";
            colannId1.MinWidth = 23;
            colannId1.Name = "colannId1";
            colannId1.Width = 104;
            // 
            // coln3
            // 
            coln3.Caption = "№оп.";
            coln3.FieldName = "N";
            coln3.MinWidth = 23;
            coln3.Name = "coln3";
            coln3.OptionsColumn.AllowEdit = false;
            coln3.Visible = true;
            coln3.VisibleIndex = 0;
            coln3.Width = 57;
            // 
            // gridColumn9
            // 
            gridColumn9.Caption = "№п/оп.";
            gridColumn9.FieldName = "N1";
            gridColumn9.MinWidth = 23;
            gridColumn9.Name = "gridColumn9";
            gridColumn9.OptionsColumn.AllowEdit = false;
            gridColumn9.Visible = true;
            gridColumn9.VisibleIndex = 1;
            gridColumn9.Width = 66;
            // 
            // gridColumn10
            // 
            gridColumn10.Caption = "Разряд";
            gridColumn10.FieldName = "razryd";
            gridColumn10.MinWidth = 23;
            gridColumn10.Name = "gridColumn10";
            gridColumn10.OptionsColumn.AllowEdit = false;
            gridColumn10.Visible = true;
            gridColumn10.VisibleIndex = 2;
            gridColumn10.Width = 86;
            // 
            // gridColumn11
            // 
            gridColumn11.Caption = "Наименование операции пошива";
            gridColumn11.FieldName = "Text";
            gridColumn11.MinWidth = 23;
            gridColumn11.Name = "gridColumn11";
            gridColumn11.OptionsColumn.AllowEdit = false;
            gridColumn11.Visible = true;
            gridColumn11.VisibleIndex = 3;
            gridColumn11.Width = 300;
            // 
            // gridColumn49
            // 
            gridColumn49.Caption = "Оборудование";
            gridColumn49.FieldName = "Obor";
            gridColumn49.Name = "gridColumn49";
            gridColumn49.Visible = true;
            gridColumn49.VisibleIndex = 5;
            gridColumn49.Width = 150;
            // 
            // gridColumn50
            // 
            gridColumn50.Caption = "Сек.";
            gridColumn50.FieldName = "Sek";
            gridColumn50.Name = "gridColumn50";
            gridColumn50.Visible = true;
            gridColumn50.VisibleIndex = 4;
            // 
            // gridControl_wdToBind
            // 
            gridControl_wdToBind.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControl_wdToBind.Font = new System.Drawing.Font("Arial", 10F);
            gridControl_wdToBind.Location = new System.Drawing.Point(656, 78);
            gridControl_wdToBind.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl_wdToBind.MainView = gridView_wdToBind;
            gridControl_wdToBind.Margin = new Padding(0);
            gridControl_wdToBind.Name = "gridControl_wdToBind";
            gridControl_wdToBind.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemCheckEdit3, repositoryItemCheckEdit4, repositoryItemCheckEdit6, repositoryItemCheckEdit7 });
            gridControl_wdToBind.Size = new System.Drawing.Size(624, 274);
            gridControl_wdToBind.TabIndex = 3;
            gridControl_wdToBind.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_wdToBind });
            // 
            // gridView_wdToBind
            // 
            gridView_wdToBind.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Yellow;
            gridView_wdToBind.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            gridView_wdToBind.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn12, gridColumn13, colarticul1, gridColumn59, gridColumn29, gridColumn28, gridColumn8, gridColumn58, colstatus1, gridColumn6, colannId7 });
            gridView_wdToBind.DetailHeight = 404;
            gridView_wdToBind.GridControl = gridControl_wdToBind;
            gridView_wdToBind.Name = "gridView_wdToBind";
            gridView_wdToBind.OptionsCustomization.AllowColumnMoving = false;
            gridView_wdToBind.OptionsEditForm.PopupEditFormWidth = 933;
            gridView_wdToBind.OptionsFilter.AllowMRUFilterList = false;
            gridView_wdToBind.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            gridView_wdToBind.OptionsFind.AllowFindPanel = false;
            gridView_wdToBind.OptionsFind.FindDelay = 500;
            gridView_wdToBind.OptionsFind.FindNullPrompt = "Введите значение для поиска...";
            gridView_wdToBind.OptionsMenu.EnableColumnMenu = false;
            gridView_wdToBind.OptionsView.ShowAutoFilterRow = true;
            gridView_wdToBind.OptionsView.ShowGroupPanel = false;
            gridView_wdToBind.PopupMenuShowing += gridView_unboundArts_PopupMenuShowing;
            gridView_wdToBind.FocusedRowChanged += gridViewWdToBind_FocusedRowChanged;
            // 
            // gridColumn12
            // 
            gridColumn12.Caption = "код";
            gridColumn12.FieldName = "Kod";
            gridColumn12.MinWidth = 23;
            gridColumn12.Name = "gridColumn12";
            gridColumn12.OptionsFilter.AllowAutoFilter = false;
            gridColumn12.Width = 128;
            // 
            // gridColumn13
            // 
            gridColumn13.Caption = " ";
            gridColumn13.ColumnEdit = repositoryItemCheckEdit3;
            gridColumn13.FieldName = "IsChecked";
            gridColumn13.MinWidth = 23;
            gridColumn13.Name = "gridColumn13";
            gridColumn13.OptionsColumn.FixedWidth = true;
            gridColumn13.OptionsFilter.AllowAutoFilter = false;
            gridColumn13.OptionsFilter.AllowFilter = false;
            gridColumn13.OptionsFilter.ShowEmptyDateFilter = false;
            gridColumn13.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;
            gridColumn13.UnboundDataType = typeof(bool);
            gridColumn13.Visible = true;
            gridColumn13.VisibleIndex = 0;
            gridColumn13.Width = 28;
            // 
            // repositoryItemCheckEdit3
            // 
            repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            repositoryItemCheckEdit3.NullStyle = StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit3.ValueGrayed = false;
            // 
            // colarticul1
            // 
            colarticul1.Caption = "Артикул";
            colarticul1.FieldName = "Articul";
            colarticul1.MinWidth = 23;
            colarticul1.Name = "colarticul1";
            colarticul1.OptionsColumn.AllowEdit = false;
            colarticul1.Visible = true;
            colarticul1.VisibleIndex = 1;
            // 
            // gridColumn59
            // 
            gridColumn59.FieldName = "size_label";
            gridColumn59.Name = "gridColumn59";
            gridColumn59.Visible = true;
            gridColumn59.VisibleIndex = 2;
            // 
            // gridColumn29
            // 
            gridColumn29.Caption = "Группа";
            gridColumn29.FieldName = "grup";
            gridColumn29.MinWidth = 23;
            gridColumn29.Name = "gridColumn29";
            gridColumn29.OptionsColumn.AllowEdit = false;
            gridColumn29.Visible = true;
            gridColumn29.VisibleIndex = 3;
            // 
            // gridColumn28
            // 
            gridColumn28.Caption = "Модель";
            gridColumn28.FieldName = "mod";
            gridColumn28.MinWidth = 23;
            gridColumn28.Name = "gridColumn28";
            gridColumn28.OptionsColumn.AllowEdit = false;
            gridColumn28.Visible = true;
            gridColumn28.VisibleIndex = 4;
            // 
            // gridColumn8
            // 
            gridColumn8.Caption = "Статус";
            gridColumn8.FieldName = "Status";
            gridColumn8.MinWidth = 23;
            gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn58
            // 
            gridColumn58.Caption = "Дата утв";
            gridColumn58.FieldName = "dateUpdate";
            gridColumn58.Name = "gridColumn58";
            gridColumn58.Visible = true;
            gridColumn58.VisibleIndex = 5;
            // 
            // colstatus1
            // 
            colstatus1.Caption = "Статус";
            colstatus1.FieldName = "Stat";
            colstatus1.MinWidth = 23;
            colstatus1.Name = "colstatus1";
            colstatus1.Visible = true;
            colstatus1.VisibleIndex = 6;
            colstatus1.Width = 120;
            // 
            // gridColumn6
            // 
            gridColumn6.Caption = "gridColumn6";
            gridColumn6.FieldName = "_isChecked";
            gridColumn6.MinWidth = 23;
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Width = 87;
            // 
            // colannId7
            // 
            colannId7.FieldName = "AnnID";
            colannId7.MinWidth = 23;
            colannId7.Name = "colannId7";
            colannId7.Width = 103;
            // 
            // repositoryItemCheckEdit4
            // 
            repositoryItemCheckEdit4.AutoHeight = false;
            repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            repositoryItemCheckEdit4.NullStyle = StyleIndeterminate.Unchecked;
            // 
            // repositoryItemCheckEdit6
            // 
            repositoryItemCheckEdit6.AutoHeight = false;
            repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            // 
            // repositoryItemCheckEdit7
            // 
            repositoryItemCheckEdit7.AutoHeight = false;
            repositoryItemCheckEdit7.Name = "repositoryItemCheckEdit7";
            // 
            // gridControl_unboundArts
            // 
            gridControl_unboundArts.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControl_unboundArts.Font = new System.Drawing.Font("Arial", 10F);
            gridControl_unboundArts.Location = new System.Drawing.Point(24, 45);
            gridControl_unboundArts.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl_unboundArts.MainView = gridView_unboundArts;
            gridControl_unboundArts.Margin = new Padding(4, 3, 4, 3);
            gridControl_unboundArts.Name = "gridControl_unboundArts";
            gridControl_unboundArts.Size = new System.Drawing.Size(582, 309);
            gridControl_unboundArts.TabIndex = 0;
            gridControl_unboundArts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_unboundArts });
            // 
            // gridView_unboundArts
            // 
            gridView_unboundArts.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
            gridView_unboundArts.Appearance.SelectedRow.Options.UseBackColor = true;
            gridView_unboundArts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { код, артикул, gridColumn2, gridColumn7, группа, модель, gridColumn4, gridColumn51, gridColumn52 });
            gridView_unboundArts.DetailHeight = 404;
            gridView_unboundArts.GridControl = gridControl_unboundArts;
            gridView_unboundArts.Name = "gridView_unboundArts";
            gridView_unboundArts.OptionsBehavior.AllowIncrementalSearch = true;
            gridView_unboundArts.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.False;
            gridView_unboundArts.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            gridView_unboundArts.OptionsClipboard.AllowTxtFormat = DevExpress.Utils.DefaultBoolean.True;
            gridView_unboundArts.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridView_unboundArts.OptionsDetail.EnableMasterViewMode = false;
            gridView_unboundArts.OptionsEditForm.PopupEditFormWidth = 933;
            gridView_unboundArts.OptionsFind.AlwaysVisible = true;
            gridView_unboundArts.OptionsFind.FindDelay = 500;
            gridView_unboundArts.OptionsFind.FindMode = FindMode.Always;
            gridView_unboundArts.OptionsFind.FindNullPrompt = "Введите значение для поиска...";
            gridView_unboundArts.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridView_unboundArts.OptionsView.ShowGroupPanel = false;
            gridView_unboundArts.ScrollStyle = ScrollStyleFlags.LiveVertScroll;
            gridView_unboundArts.PopupMenuShowing += gridView_unboundArts_PopupMenuShowing;
            gridView_unboundArts.FocusedRowChanged += gridView_unboundArts_FocusedRowChanged;
            // 
            // код
            // 
            код.Caption = "Код";
            код.FieldName = "kodd_rt";
            код.MinWidth = 23;
            код.Name = "код";
            код.OptionsColumn.AllowEdit = false;
            код.Visible = true;
            код.VisibleIndex = 0;
            код.Width = 53;
            // 
            // артикул
            // 
            артикул.Caption = "Артикул";
            артикул.FieldName = "ArticulForRT";
            артикул.MinWidth = 23;
            артикул.Name = "артикул";
            артикул.OptionsColumn.AllowEdit = false;
            артикул.Visible = true;
            артикул.VisibleIndex = 1;
            артикул.Width = 83;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = " ";
            gridColumn2.ColumnEdit = repositoryItemCheckEdit1;
            gridColumn2.FieldName = "IsChecked";
            gridColumn2.MinWidth = 23;
            gridColumn2.Name = "gridColumn2";
            gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridColumn2.OptionsColumn.FixedWidth = true;
            gridColumn2.OptionsFilter.AllowAutoFilter = false;
            gridColumn2.OptionsFilter.AllowFilter = false;
            gridColumn2.OptionsFilter.AllowInHeaderSearch = DevExpress.Utils.DefaultBoolean.False;
            gridColumn2.UnboundDataType = typeof(bool);
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2;
            gridColumn2.Width = 31;
            // 
            // gridColumn7
            // 
            gridColumn7.Caption = "Ранее увязанные";
            gridColumn7.ColumnEdit = repositoryItemButtonEdit2;
            gridColumn7.FieldName = "BindedArt";
            gridColumn7.MinWidth = 23;
            gridColumn7.Name = "gridColumn7";
            gridColumn7.Width = 129;
            // 
            // группа
            // 
            группа.Caption = "Группа";
            группа.FieldName = "grup";
            группа.MinWidth = 23;
            группа.Name = "группа";
            группа.OptionsColumn.AllowEdit = false;
            группа.Visible = true;
            группа.VisibleIndex = 3;
            группа.Width = 95;
            // 
            // модель
            // 
            модель.Caption = "Модель";
            модель.FieldName = "mod";
            модель.MinWidth = 23;
            модель.Name = "модель";
            модель.OptionsColumn.AllowEdit = false;
            модель.Visible = true;
            модель.VisibleIndex = 4;
            модель.Width = 67;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "gridColumn4";
            gridColumn4.FieldName = "_isChecked";
            gridColumn4.MinWidth = 23;
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Width = 87;
            // 
            // gridColumn51
            // 
            gridColumn51.Caption = "Мин. размер";
            gridColumn51.FieldName = "minSizeAll";
            gridColumn51.Name = "gridColumn51";
            gridColumn51.Visible = true;
            gridColumn51.VisibleIndex = 5;
            gridColumn51.Width = 56;
            // 
            // gridColumn52
            // 
            gridColumn52.Caption = "Макс. размер";
            gridColumn52.FieldName = "maxSizeAll";
            gridColumn52.Name = "gridColumn52";
            gridColumn52.Visible = true;
            gridColumn52.VisibleIndex = 6;
            gridColumn52.Width = 60;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup2, layoutControlGroup6, splitterItem8 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1841, 711);
            // 
            // layoutControlGroup2
            // 
            toolTipItem2.Text = "Создаёт разделение труда на основе выбранного артикула, сразу заполняя Группу, Модель и Артикул. Открывает окно редактирования, где можно добавить схему разделения";
            superToolTip2.Items.Add(toolTipItem2);
            layoutControlGroup2.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Показать все РТ", true, buttonImageOptions48, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, true, false, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("                                                ", true, buttonImageOptions49, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Добавить", true, buttonImageOptions50, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Добавить новое пустое разделение труда", -1, true, null, true, false, true, "btnAdd", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions51, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, false, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Дубль", true, buttonImageOptions52, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Дублировать выбранное РТ", -1, true, null, true, false, true, "btnEdit", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions53, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, false, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Архив+копия", true, buttonImageOptions54, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Создать копию РТ и отправить базовое РТ в архив", -1, true, null, true, false, true, "btnArch", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions55, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Создать из артикула", true, buttonImageOptions56, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Создать РТ на основе выбранного артикула", -1, true, superToolTip2, true, false, false, "btnArt", -1) });
            layoutControlGroup2.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup14, layoutControlGroup3, layoutControlGroup13, splitterItem3, splitterItem7 });
            layoutControlGroup2.Location = new System.Drawing.Point(620, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlGroup2.Size = new System.Drawing.Size(1201, 691);
            layoutControlGroup2.Text = "РТ для увязки";
            layoutControlGroup2.CustomButtonClick += layoutControlGroup2_CustomButtonClick;
            // 
            // layoutControlGroup14
            // 
            buttonImageOptions63.Image = (System.Drawing.Image)resources.GetObject("buttonImageOptions63.Image");
            layoutControlGroup14.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Увязать", true, buttonImageOptions57, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions58, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Отвязать", true, buttonImageOptions59, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions60, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Проставить утверждение", true, buttonImageOptions61, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|                         ", true, buttonImageOptions62, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton(" все РТ", true, buttonImageOptions63, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "Показать все РТ", -1, true, null, true, true, true, null, -1) });
            layoutControlGroup14.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3 });
            layoutControlGroup14.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup14.Name = "layoutControlGroup14";
            layoutControlGroup14.Size = new System.Drawing.Size(652, 323);
            layoutControlGroup14.Text = " ";
            layoutControlGroup14.CustomButtonClick += layoutControlGroup14_CustomButtonClick;
            layoutControlGroup14.CustomButtonUnchecked += layoutControlGroup14_CustomButtonUnchecked;
            layoutControlGroup14.CustomButtonChecked += layoutControlGroup14_CustomButtonChecked;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = gridControl_wdToBind;
            layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(628, 278);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { splitterItem2, layoutControlItem14, tabbedControlGroup1 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 333);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Size = new System.Drawing.Size(1177, 313);
            layoutControlGroup3.Text = "Схема РТ";
            // 
            // splitterItem2
            // 
            splitterItem2.Location = new System.Drawing.Point(889, 0);
            splitterItem2.Name = "splitterItem2";
            splitterItem2.Size = new System.Drawing.Size(10, 268);
            // 
            // layoutControlItem14
            // 
            layoutControlItem14.Control = pictureBox2;
            layoutControlItem14.Location = new System.Drawing.Point(899, 0);
            layoutControlItem14.Name = "layoutControlItem14";
            layoutControlItem14.Size = new System.Drawing.Size(254, 268);
            layoutControlItem14.TextVisible = false;
            // 
            // tabbedControlGroup1
            // 
            tabbedControlGroup1.Location = new System.Drawing.Point(0, 0);
            tabbedControlGroup1.Name = "tabbedControlGroup1";
            tabbedControlGroup1.SelectedTabPage = layoutControlGroup1;
            tabbedControlGroup1.Size = new System.Drawing.Size(889, 268);
            tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlGroup4, layoutControlGroup5 });
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.CustomizationFormText = "Пошив";
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new System.Drawing.Size(865, 221);
            layoutControlGroup1.Text = "Пошив";
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = customGridControl3;
            layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(865, 221);
            layoutControlItem4.Text = "Пошив";
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem8 });
            layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Size = new System.Drawing.Size(865, 221);
            layoutControlGroup4.Text = "Раскрой";
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = customGridControl2;
            layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.Size = new System.Drawing.Size(865, 221);
            layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Size = new System.Drawing.Size(865, 221);
            layoutControlGroup5.Text = "Комплектовка";
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customGridControl1;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(865, 221);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup13
            // 
            layoutControlGroup13.CustomizationFormText = "Увязанные артикулы";
            layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem11, layoutControlItem37 });
            layoutControlGroup13.Location = new System.Drawing.Point(662, 0);
            layoutControlGroup13.Name = "layoutControlGroup13";
            layoutControlGroup13.Size = new System.Drawing.Size(515, 323);
            layoutControlGroup13.Text = "Увязанные артикулы";
            // 
            // layoutControlItem11
            // 
            layoutControlItem11.Control = gridControlNZP;
            layoutControlItem11.Location = new System.Drawing.Point(0, 24);
            layoutControlItem11.Name = "layoutControlItem11";
            layoutControlItem11.Size = new System.Drawing.Size(491, 254);
            layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem37
            // 
            layoutControlItem37.Control = searchControl1;
            layoutControlItem37.Location = new System.Drawing.Point(0, 0);
            layoutControlItem37.Name = "layoutControlItem37";
            layoutControlItem37.Size = new System.Drawing.Size(491, 24);
            layoutControlItem37.Text = "Поиск по увязанному артикулу (Enter)";
            layoutControlItem37.TextSize = new System.Drawing.Size(195, 13);
            // 
            // splitterItem3
            // 
            splitterItem3.Location = new System.Drawing.Point(0, 323);
            splitterItem3.Name = "splitterItem3";
            splitterItem3.Size = new System.Drawing.Size(1177, 10);
            // 
            // splitterItem7
            // 
            splitterItem7.Location = new System.Drawing.Point(652, 0);
            splitterItem7.Name = "splitterItem7";
            splitterItem7.Size = new System.Drawing.Size(10, 323);
            // 
            // layoutControlGroup6
            // 
            layoutControlGroup6.CaptionImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup6.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Создать из артикула", true, buttonImageOptions64, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup6.CustomizationFormText = " Артикулы для увязки";
            layoutControlGroup6.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem13, layoutControlItem9, simpleSeparator3, layoutControlItem19, splitterItem1 });
            layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup6.Name = "layoutControlGroup6";
            layoutControlGroup6.Size = new System.Drawing.Size(610, 691);
            layoutControlGroup6.Text = "  Артикулы для увязки";
            layoutControlGroup6.CustomButtonClick += layoutControlGroup6_CustomButtonClick;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = gridControl_unboundArts;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(586, 313);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = gridControl_binded;
            layoutControlItem13.Location = new System.Drawing.Point(0, 561);
            layoutControlItem13.Name = "layoutControlItem13";
            layoutControlItem13.Size = new System.Drawing.Size(586, 85);
            layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = customLabel2;
            layoutControlItem9.Location = new System.Drawing.Point(0, 533);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new System.Drawing.Size(586, 28);
            layoutControlItem9.TextVisible = false;
            // 
            // simpleSeparator3
            // 
            simpleSeparator3.Location = new System.Drawing.Point(0, 313);
            simpleSeparator3.Name = "simpleSeparator3";
            simpleSeparator3.Size = new System.Drawing.Size(586, 1);
            // 
            // layoutControlItem19
            // 
            layoutControlItem19.Control = pictureBox3;
            layoutControlItem19.Location = new System.Drawing.Point(0, 324);
            layoutControlItem19.Name = "layoutControlItem19";
            layoutControlItem19.Size = new System.Drawing.Size(586, 209);
            layoutControlItem19.TextVisible = false;
            // 
            // splitterItem1
            // 
            splitterItem1.Location = new System.Drawing.Point(0, 314);
            splitterItem1.Name = "splitterItem1";
            splitterItem1.Size = new System.Drawing.Size(586, 10);
            // 
            // splitterItem8
            // 
            splitterItem8.Location = new System.Drawing.Point(610, 0);
            splitterItem8.Name = "splitterItem8";
            splitterItem8.Size = new System.Drawing.Size(10, 691);
            // 
            // xtraTabPage3
            // 
            xtraTabPage3.Appearance.Header.BackColor = System.Drawing.Color.Transparent;
            xtraTabPage3.Appearance.Header.ForeColor = System.Drawing.Color.Black;
            xtraTabPage3.Appearance.Header.Options.UseBackColor = true;
            xtraTabPage3.Appearance.Header.Options.UseForeColor = true;
            xtraTabPage3.Appearance.HeaderActive.BackColor = System.Drawing.Color.Transparent;
            xtraTabPage3.Appearance.HeaderActive.Options.UseBackColor = true;
            xtraTabPage3.Appearance.HeaderHotTracked.BackColor = System.Drawing.Color.Transparent;
            xtraTabPage3.Appearance.HeaderHotTracked.Options.UseBackColor = true;
            xtraTabPage3.Appearance.PageClient.BackColor = System.Drawing.Color.Transparent;
            xtraTabPage3.Appearance.PageClient.Options.UseBackColor = true;
            xtraTabPage3.Controls.Add(layoutControl3);
            xtraTabPage3.Margin = new Padding(4, 3, 4, 3);
            xtraTabPage3.Name = "xtraTabPage3";
            xtraTabPage3.Size = new System.Drawing.Size(1845, 715);
            xtraTabPage3.Text = "Работа с архивом";
            // 
            // layoutControl3
            // 
            layoutControl3.Controls.Add(customButton2);
            layoutControl3.Controls.Add(flyoutPanel1);
            layoutControl3.Controls.Add(gridControlPreArch);
            layoutControl3.Controls.Add(gridControlArch);
            layoutControl3.Dock = DockStyle.Fill;
            layoutControl3.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem40, layoutControlItem39 });
            layoutControl3.Location = new System.Drawing.Point(0, 0);
            layoutControl3.Name = "layoutControl3";
            layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(634, 0, 650, 400);
            layoutControl3.Root = layoutControlGroup17;
            layoutControl3.Size = new System.Drawing.Size(1845, 715);
            layoutControl3.TabIndex = 3;
            layoutControl3.Text = "layoutControl3";
            // 
            // customButton2
            // 
            customButton2.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            customButton2.FlatAppearance.BorderSize = 0;
            customButton2.FlatStyle = FlatStyle.Flat;
            customButton2.Font = new System.Drawing.Font("Arial", 12F);
            customButton2.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            customButton2.Location = new System.Drawing.Point(12, 12);
            customButton2.Margin = new Padding(4, 3, 4, 3);
            customButton2.Name = "customButton2";
            customButton2.Size = new System.Drawing.Size(908, 878);
            customButton2.TabIndex = 0;
            customButton2.Text = "В архив";
            customButton2.UseVisualStyleBackColor = false;
            customButton2.Click += customButton2_Click;
            // 
            // flyoutPanel1
            // 
            flyoutPanel1.Controls.Add(flyoutPanelControl1);
            flyoutPanel1.Location = new System.Drawing.Point(12, 894);
            flyoutPanel1.Margin = new Padding(4, 3, 4, 3);
            flyoutPanel1.Name = "flyoutPanel1";
            flyoutPanel1.OptionsButtonPanel.ButtonPanelHeight = 35;
            flyoutPanel1.Size = new System.Drawing.Size(1821, 9);
            flyoutPanel1.TabIndex = 1;
            // 
            // flyoutPanelControl1
            // 
            flyoutPanelControl1.Controls.Add(customCancelButton1);
            flyoutPanelControl1.Controls.Add(customComboBox1);
            flyoutPanelControl1.Controls.Add(customButton1);
            flyoutPanelControl1.Dock = DockStyle.Fill;
            flyoutPanelControl1.FlyoutPanel = flyoutPanel1;
            flyoutPanelControl1.Location = new System.Drawing.Point(0, 0);
            flyoutPanelControl1.Margin = new Padding(4, 3, 4, 3);
            flyoutPanelControl1.Name = "flyoutPanelControl1";
            flyoutPanelControl1.Size = new System.Drawing.Size(1821, 9);
            flyoutPanelControl1.TabIndex = 0;
            // 
            // customCancelButton1
            // 
            customCancelButton1.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customCancelButton1.DialogResult = DialogResult.Cancel;
            customCancelButton1.Font = new System.Drawing.Font("Arial", 10F);
            customCancelButton1.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customCancelButton1.Location = new System.Drawing.Point(322, 38);
            customCancelButton1.Margin = new Padding(4, 3, 4, 3);
            customCancelButton1.Name = "customCancelButton1";
            customCancelButton1.Size = new System.Drawing.Size(88, 29);
            customCancelButton1.TabIndex = 2;
            customCancelButton1.Text = "customCancelButton1";
            customCancelButton1.UseVisualStyleBackColor = false;
            // 
            // customComboBox1
            // 
            customComboBox1.BackColor = System.Drawing.Color.FromArgb(255, 245, 230);
            customComboBox1.Font = new System.Drawing.Font("Arial", 10F);
            customComboBox1.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            customComboBox1.FormattingEnabled = true;
            customComboBox1.Location = new System.Drawing.Point(254, 144);
            customComboBox1.Margin = new Padding(4, 3, 4, 3);
            customComboBox1.Name = "customComboBox1";
            customComboBox1.Size = new System.Drawing.Size(140, 24);
            customComboBox1.TabIndex = 1;
            // 
            // customButton1
            // 
            customButton1.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            customButton1.Font = new System.Drawing.Font("Arial", 10F);
            customButton1.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customButton1.Location = new System.Drawing.Point(61, 39);
            customButton1.Margin = new Padding(4, 3, 4, 3);
            customButton1.Name = "customButton1";
            customButton1.Size = new System.Drawing.Size(88, 29);
            customButton1.TabIndex = 0;
            customButton1.Text = "customButton1";
            customButton1.UseVisualStyleBackColor = false;
            // 
            // gridControlPreArch
            // 
            gridControlPreArch.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlPreArch.Font = new System.Drawing.Font("Arial", 10F);
            gridControlPreArch.Location = new System.Drawing.Point(24, 45);
            gridControlPreArch.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControlPreArch.MainView = gridViewPreArch;
            gridControlPreArch.Margin = new Padding(4, 3, 4, 3);
            gridControlPreArch.Name = "gridControlPreArch";
            gridControlPreArch.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemCheckEdit5 });
            gridControlPreArch.Size = new System.Drawing.Size(658, 646);
            gridControlPreArch.TabIndex = 0;
            gridControlPreArch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewPreArch });
            // 
            // gridViewPreArch
            // 
            gridViewPreArch.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
            gridViewPreArch.Appearance.SelectedRow.Options.UseBackColor = true;
            gridViewPreArch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn14, gridColumn16, gridColumn15, gridColumn17 });
            gridViewPreArch.DetailHeight = 404;
            gridViewPreArch.GridControl = gridControlPreArch;
            gridViewPreArch.Name = "gridViewPreArch";
            gridViewPreArch.OptionsEditForm.PopupEditFormWidth = 933;
            gridViewPreArch.OptionsView.RowAutoHeight = true;
            gridViewPreArch.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn14
            // 
            gridColumn14.Caption = "код";
            gridColumn14.FieldName = "Kod";
            gridColumn14.MinWidth = 23;
            gridColumn14.Name = "gridColumn14";
            gridColumn14.Visible = true;
            gridColumn14.VisibleIndex = 0;
            gridColumn14.Width = 87;
            // 
            // gridColumn16
            // 
            gridColumn16.Caption = "артикул";
            gridColumn16.FieldName = "Articul";
            gridColumn16.MinWidth = 23;
            gridColumn16.Name = "gridColumn16";
            gridColumn16.Visible = true;
            gridColumn16.VisibleIndex = 1;
            gridColumn16.Width = 87;
            // 
            // gridColumn15
            // 
            gridColumn15.Caption = " ";
            gridColumn15.ColumnEdit = repositoryItemCheckEdit5;
            gridColumn15.FieldName = "IsChecked";
            gridColumn15.MinWidth = 23;
            gridColumn15.Name = "gridColumn15";
            gridColumn15.UnboundDataType = typeof(bool);
            gridColumn15.Visible = true;
            gridColumn15.VisibleIndex = 2;
            gridColumn15.Width = 87;
            // 
            // repositoryItemCheckEdit5
            // 
            repositoryItemCheckEdit5.AutoHeight = false;
            repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            // 
            // gridColumn17
            // 
            gridColumn17.Caption = "наличие НЗП шт.";
            gridColumn17.MinWidth = 23;
            gridColumn17.Name = "gridColumn17";
            gridColumn17.Visible = true;
            gridColumn17.VisibleIndex = 3;
            gridColumn17.Width = 87;
            // 
            // gridControlArch
            // 
            gridControlArch.Font = new System.Drawing.Font("Arial", 10F);
            gridControlArch.Location = new System.Drawing.Point(710, 45);
            gridControlArch.MainView = gridViewArch;
            gridControlArch.Name = "gridControlArch";
            gridControlArch.Size = new System.Drawing.Size(1111, 646);
            gridControlArch.TabIndex = 2;
            gridControlArch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewArch });
            // 
            // gridViewArch
            // 
            gridViewArch.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridViewArch.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewArch.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridViewArch.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridViewArch.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewArch.Appearance.FocusedRow.Options.UseFont = true;
            gridViewArch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn66, gridColumn60, gridColumn61, gridColumn62, gridColumn63, gridColumn64, gridColumn65 });
            gridViewArch.GridControl = gridControlArch;
            gridViewArch.Name = "gridViewArch";
            gridViewArch.OptionsBehavior.Editable = false;
            gridViewArch.OptionsFind.AlwaysVisible = true;
            gridViewArch.OptionsFind.Behavior = FindPanelBehavior.Filter;
            gridViewArch.OptionsFind.FindDelay = 500;
            gridViewArch.OptionsFind.FindNullPrompt = "Введите значение для поиска...";
            gridViewArch.OptionsFind.FindPanelLocation = GridFindPanelLocation.Panel;
            gridViewArch.OptionsSelection.MultiSelect = true;
            gridViewArch.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridViewArch.OptionsView.EnableAppearanceEvenRow = true;
            gridViewArch.OptionsView.GroupFooterShowMode = GroupFooterShowMode.Hidden;
            gridViewArch.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn60
            // 
            gridColumn60.Caption = "Артикул";
            gridColumn60.FieldName = "Articul";
            gridColumn60.Name = "gridColumn60";
            gridColumn60.Visible = true;
            gridColumn60.VisibleIndex = 0;
            gridColumn60.Width = 150;
            // 
            // gridColumn61
            // 
            gridColumn61.Caption = "Группа";
            gridColumn61.FieldName = "grup";
            gridColumn61.Name = "gridColumn61";
            gridColumn61.Visible = true;
            gridColumn61.VisibleIndex = 2;
            gridColumn61.Width = 100;
            // 
            // gridColumn62
            // 
            gridColumn62.Caption = "Модель";
            gridColumn62.FieldName = "Mod";
            gridColumn62.Name = "gridColumn62";
            gridColumn62.Visible = true;
            gridColumn62.VisibleIndex = 3;
            gridColumn62.Width = 120;
            // 
            // gridColumn63
            // 
            gridColumn63.Caption = "Размеры";
            gridColumn63.FieldName = "Size_label";
            gridColumn63.Name = "gridColumn63";
            gridColumn63.Visible = true;
            gridColumn63.VisibleIndex = 4;
            gridColumn63.Width = 100;
            // 
            // gridColumn64
            // 
            gridColumn64.Caption = "Статус";
            gridColumn64.FieldName = "StatusText";
            gridColumn64.Name = "gridColumn64";
            gridColumn64.Visible = true;
            gridColumn64.VisibleIndex = 5;
            gridColumn64.Width = 120;
            // 
            // gridColumn65
            // 
            gridColumn65.Caption = "ID";
            gridColumn65.FieldName = "AnnID";
            gridColumn65.Name = "gridColumn65";
            gridColumn65.Visible = true;
            gridColumn65.VisibleIndex = 6;
            gridColumn65.Width = 80;
            // 
            // layoutControlItem40
            // 
            layoutControlItem40.Control = customButton2;
            layoutControlItem40.Location = new System.Drawing.Point(0, 0);
            layoutControlItem40.Name = "layoutControlItem40";
            layoutControlItem40.Size = new System.Drawing.Size(912, 882);
            layoutControlItem40.TextVisible = false;
            // 
            // layoutControlItem39
            // 
            layoutControlItem39.Control = flyoutPanel1;
            layoutControlItem39.Location = new System.Drawing.Point(0, 882);
            layoutControlItem39.Name = "layoutControlItem39";
            layoutControlItem39.Size = new System.Drawing.Size(1825, 13);
            layoutControlItem39.TextVisible = false;
            // 
            // layoutControlGroup17
            // 
            layoutControlGroup17.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton(), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton() });
            layoutControlGroup17.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup17.GroupBordersVisible = false;
            layoutControlGroup17.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroupPreArch, layoutControlGroup19 });
            layoutControlGroup17.Name = "Root";
            layoutControlGroup17.Size = new System.Drawing.Size(1845, 715);
            layoutControlGroup17.TextVisible = false;
            // 
            // layoutControlGroupPreArch
            // 
            layoutControlGroupPreArch.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("В архив", true, buttonImageOptions65, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroupPreArch.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroupPreArch.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem38 });
            layoutControlGroupPreArch.Location = new System.Drawing.Point(0, 0);
            layoutControlGroupPreArch.Name = "layoutControlGroupPreArch";
            layoutControlGroupPreArch.Size = new System.Drawing.Size(686, 695);
            layoutControlGroupPreArch.Text = "Предварительный архив";
            layoutControlGroupPreArch.CustomButtonClick += layoutControlGroupPreArch_CustomButtonClick;
            // 
            // layoutControlItem38
            // 
            layoutControlItem38.Control = gridControlPreArch;
            layoutControlItem38.Location = new System.Drawing.Point(0, 0);
            layoutControlItem38.Name = "layoutControlItem38";
            layoutControlItem38.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem38.Size = new System.Drawing.Size(662, 650);
            layoutControlItem38.TextVisible = false;
            // 
            // layoutControlGroup19
            // 
            layoutControlGroup19.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Восстановить из архива", true, buttonImageOptions66, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup19.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup19.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem17 });
            layoutControlGroup19.Location = new System.Drawing.Point(686, 0);
            layoutControlGroup19.Name = "layoutControlGroup19";
            layoutControlGroup19.Size = new System.Drawing.Size(1139, 695);
            layoutControlGroup19.Text = "Архив";
            layoutControlGroup19.CustomButtonClick += layoutControlGroup19_CustomButtonClick;
            // 
            // layoutControlItem17
            // 
            layoutControlItem17.Control = gridControlArch;
            layoutControlItem17.Location = new System.Drawing.Point(0, 0);
            layoutControlItem17.Name = "layoutControlItem17";
            layoutControlItem17.Size = new System.Drawing.Size(1115, 650);
            layoutControlItem17.TextVisible = false;
            // 
            // gridColumn48
            // 
            gridColumn48.Caption = "Дата обн.";
            gridColumn48.FieldName = "dateUpdate";
            gridColumn48.Name = "gridColumn48";
            gridColumn48.Visible = true;
            gridColumn48.VisibleIndex = 5;
            gridColumn48.Width = 57;
            // 
            // imageCollection1
            // 
            imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // layoutControlItem49
            // 
            layoutControlItem49.Control = ButtonEditWd;
            layoutControlItem49.Location = new System.Drawing.Point(113, 0);
            layoutControlItem49.Name = "layoutControlItem49";
            layoutControlItem49.Size = new System.Drawing.Size(113, 24);
            layoutControlItem49.TextVisible = false;
            // 
            // TeamWork
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1849, 766);
            Controls.Add(xtraTabControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "TeamWork";
            Text = "Нормативные расценки";
            WindowState = FormWindowState.Maximized;
            FormClosing += TeamWork_FormClosing;
            Load += TeamWorkForm_Load;
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).EndInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl1).EndInit();
            xtraTabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl2).EndInit();
            layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)toggleSwitchKit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlKontTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewKontTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridControlBindedArts).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewBindedArts).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaskrTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRaskrTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaszTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRaszTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditProizv).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditOb).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditPodr).EndInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDateEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit2).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit3).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textEditMod.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditArt.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditSec.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditCreate.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem26).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem27).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem29).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem28).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup15).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem23).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem25).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem20).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem24).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem31).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupForAdmins).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem43).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem41).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem42).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem44).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem45).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)editBtns).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlEditWd).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem46).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem36).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlEditOnlyAdv).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup18).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem30).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem35).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem32).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem33).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem34).EndInit();
            xtraTabPageArticles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)xtraTabControl2).EndInit();
            xtraTabControl2.ResumeLayout(false);
            xtraTabPageWorkDivisions.ResumeLayout(false);
            xtraTabPageWorkDivisions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl2).EndInit();
            ((System.ComponentModel.ISupportInitialize)normRaskArt).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)normKontTab).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl_binded).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView_binded).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlNZP).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNZP).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit8).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl3).EndInit();
            ((System.ComponentModel.ISupportInitialize)normRaszTab).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl_wdToBind).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView_wdToBind).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit3).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit4).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit6).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit7).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl_unboundArts).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView_unboundArts).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup14).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabbedControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem37).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem19).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem8).EndInit();
            xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl3).EndInit();
            layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)flyoutPanel1).EndInit();
            flyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)flyoutPanelControl1).EndInit();
            flyoutPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlPreArch).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPreArch).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit5).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlArch).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArch).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem40).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem39).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup17).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupPreArch).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem38).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup19).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).EndInit();
            ((System.ComponentModel.ISupportInitialize)artnormnBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)normraszBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)normraskBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)normkontBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)normdopobrBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)normraszBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)artnormnBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)sparticulBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)artnormnBindingSource2).EndInit();
            ((System.ComponentModel.ISupportInitialize)sparticulBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageCollection1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)desBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)constrBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem49).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private CustomTabPage TabPage1;
        private CustomTabPage xtraTabPageArticles;
        private System.Windows.Forms.BindingSource normraszBindingSource;
        private System.Windows.Forms.BindingSource normraskBindingSource;
        private System.Windows.Forms.BindingSource normkontBindingSource;
        private System.Windows.Forms.BindingSource normdopobrBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn modDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn razmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sost2oldDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekshvDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn shrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn katnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebzDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn koefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebrekomDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebproizvDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn poDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebzsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekDataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz5DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz7DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz12DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyazDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pictDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodshtrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodshtrkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn grDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn kleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gruppDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodtovDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gruppaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pgruppaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkbDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodt1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkb1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normt1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opist1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodt2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkb2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normt2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opist2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodt3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkb3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normt3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opist3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodt4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkb4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normt4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opist4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bazaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodvDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodt5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkb5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normt5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opist5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodt6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkb6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normt6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opist6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebdopDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebt1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebt3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebt2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebt4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebt5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebt6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz6DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz10DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyazoDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn normapryzDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idgostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idsvyazDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn koefprDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn obizdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stavkandsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodt7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkb7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normt7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebt7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opist7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sposobupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcountryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldprchDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz70DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz3DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn koefdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stndsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updrazmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn glrekomDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekkrDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn arhDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ndsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn edizmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brakt1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brakt2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brakt3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brakt4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brakt5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brakt6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brakt7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brakavgDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cenaprdcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kombdetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kombizdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sostavDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kkgm1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kkgm2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kkgm3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kkgm4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kkgm5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kkgm6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kkgm7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sost2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isfurnitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isupakDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz14DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcgidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gbmidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcgpidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gbtidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn busDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn straDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ppresDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sebuslDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artsegmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artfamilyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artclassDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artblockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scidnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodtnvedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateopisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mtrlupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mtrlpdklDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vidobuvDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mtrldownDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqlpraddDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqlprupdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kompnameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateaddDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz62DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz71DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekvyaz72DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sost3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn agidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn krujDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodlv3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekcordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn normcordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgmidnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateutvkkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sekDataGridViewTextBoxColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn colkod;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private CustomTabPage xtraTabPage3;
        private BindingSource sparticulBindingSource;
        private BindingSource sparticulBindingSource1;
        private ToolStrip fillBy1ToolStrip;
        private ToolStripButton fillBy1ToolStripButton;
        private BindingSource normraszBindingSource1;
        private CustomTabPage xtraTabPageWorkDivisions;
        private BindingSource artnormnBindingSource2;
        RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
        private RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private PanelControl panelControl2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private BindingSource artnormnBindingSource;
        private BindingSource artnormnBindingSource1;
        private CustomButton ButtonEditWd;
        private CustomButton ButtonArchAndCopyWd;
        private CustomButton ButtonPreliminaryWd;
        private CustomButton ButtonCopyWd;
        private Panel panel5;
        private CustomCheckBox SortBox;
        private CustomCheckBox archiveCheckBox;
        private CustomCheckBox actualCheckBox;
        private CustomCheckBox preliminaryCheckBox;
        private ErrorProvider errorProvider1;
        private BindingSource desBindingSource;
        private BindingSource constrBindingSource;
        private CustomGridControl gridControl_wdToBind;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_wdToBind;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        public RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colarticul1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn colstatus1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn colannId7;
        private RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private RepositoryItemCheckEdit repositoryItemCheckEdit6;
        private RepositoryItemCheckEdit repositoryItemCheckEdit7;
        private CustomGridControl gridControlNZP;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNZP;
        private DevExpress.XtraGrid.Columns.GridColumn kodd_rt;
        private DevExpress.XtraGrid.Columns.GridColumn colannId2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn kolNZP;
        private DevExpress.XtraGrid.Columns.GridColumn PztCount;
        private CustomGridControl customGridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView normRaszTab;
        private DevExpress.XtraGrid.Columns.GridColumn colannId1;
        private DevExpress.XtraGrid.Columns.GridColumn coln3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private CustomGridControl gridControl_unboundArts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_unboundArts;
        private DevExpress.XtraGrid.Columns.GridColumn код;
        private DevExpress.XtraGrid.Columns.GridColumn артикул;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn группа;
        private DevExpress.XtraGrid.Columns.GridColumn модель;
        private DevExpress.XtraGrid.Columns.GridColumn размер;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private CustomGridControl gridControl_binded;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_binded;
        private PictureBox pictureBox2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private PictureBox pictureBox1;
        private CustomGridControl gridControlKontTW;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKontTW;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd2;
        private DevExpress.XtraGrid.Columns.GridColumn coltext2;
        private DevExpress.XtraGrid.Columns.GridColumn colsek3;
        private DevExpress.XtraGrid.Columns.GridColumn colannId5;
        private CustomGridControl gridControlRaszTW;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRaszTW;
        private DevExpress.XtraGrid.Columns.GridColumn coln;
        private DevExpress.XtraGrid.Columns.GridColumn coln1;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd;
        private DevExpress.XtraGrid.Columns.GridColumn coltext;
        private DevExpress.XtraGrid.Columns.GridColumn colsek1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn colobor;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o;
        private DevExpress.XtraGrid.Columns.GridColumn colannId3;
        private RepositoryItemLookUpEdit repositoryItemLookUpEditProizv;
        private RepositoryItemLookUpEdit repositoryItemLookUpEditOb;
        private RepositoryItemLookUpEdit repositoryItemLookUpEditPodr;
        private CustomGridControl ANNgridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView ANNgridView;
        private DevExpress.XtraGrid.Columns.GridColumn colgroup;
        private DevExpress.XtraGrid.Columns.GridColumn colarticul;
        private DevExpress.XtraGrid.Columns.GridColumn colmod;
        private DevExpress.XtraGrid.Columns.GridColumn colsek;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz;
        private DevExpress.XtraGrid.Columns.GridColumn coldateCreate;
        private DevExpress.XtraGrid.Columns.GridColumn coldateUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_shv;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyazo;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz5;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz7;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz12;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz10;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz6;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_kr;
        private DevExpress.XtraGrid.Columns.GridColumn colslogn;
        private DevExpress.XtraGrid.Columns.GridColumn colkomment;
        private DevExpress.XtraGrid.Columns.GridColumn colReco;
        private DevExpress.XtraGrid.Columns.GridColumn coldiz;
        private DevExpress.XtraGrid.Columns.GridColumn colconstr;
        private DevExpress.XtraGrid.Columns.GridColumn colannID;
        private RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private CustomGridControl gridControlRaskrTW;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRaskrTW;
        private DevExpress.XtraGrid.Columns.GridColumn colid;
        private DevExpress.XtraGrid.Columns.GridColumn colkod2;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd1;
        private DevExpress.XtraGrid.Columns.GridColumn coltext1;
        private DevExpress.XtraGrid.Columns.GridColumn colsek2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn colannId4;
        private RichTextBox commentRichTextBox;
        private RichTextBox RecoRichTextBox;
        private CustomSimpleButton PrintButton;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private CustomLabel customLabel2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private CustomGridControl customGridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView normRaskArt;
        private CustomGridControl customGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView normKontTab;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private CustomTextBox buffer;
        private CustomButton ButtonDouble;

        //private async void DuplicateWorkDivision_Click_Internal_Wrapper(object sender, EventArgs e)
        //{
        //    await DuplicateWorkDivision_Click_Internal(sender, e);
        //}
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private RepositoryItemButtonEdit repositoryItemButtonEdit3;
        private RichTextBox constructorTextBox;
        private RichTextBox designerTextBox;
        private CustomGridControl GridControlBindedArts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBindedArts;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn42;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn43;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.SplitterItem splitterItem5;
        private DevExpress.XtraLayout.SplitterItem splitterItem4;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator6;
        private DevExpress.XtraLayout.SplitterItem splitterItem6;
        private TextEdit textEditMod;
        private TextEdit textEditArt;
        private TextEdit textEditSec;
        private TextEdit textEditCreate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup14;
        private DevExpress.XtraGrid.Columns.GridColumn DisplayNumber;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn47;
        private PictureBox pictureBox3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraGrid.Columns.GridColumn data_r;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup15;
        private CustomSimpleButton customSimpleButtonArch;
        private CustomSimpleButton customSimpleButtonUpd;
        private CustomSimpleButton customSimpleButtonArchARTICUL;
        private CustomSimpleButton customSimpleButtonDel;
        private CustomSimpleButton customSimpleButtonUnbind;
        private CustomSimpleButton printButtonPlus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn52;
        private DevExpress.XtraGrid.Columns.GridColumn Kod_o;
        private DevExpress.XtraGrid.Columns.GridColumn NameOfOperationRask;
        private DevExpress.XtraGrid.Columns.GridColumn razryd;
        private DevExpress.XtraGrid.Columns.GridColumn sek;
        private DevExpress.XtraGrid.Columns.GridColumn spec;
        private DevExpress.XtraGrid.Columns.GridColumn Obor;
        private DevExpress.XtraGrid.Columns.GridColumn NameOfOperationKont;
        private DevExpress.XtraGrid.Columns.GridColumn TextRask;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private RepositoryItemCheckEdit repositoryItemCheckEdit8;
        private DevExpress.XtraGrid.Columns.GridColumn coldateAdd;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn55;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn56;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn58;
        private RepositoryItemDateEdit repositoryItemDateEdit1;
        private TextEdit textEdit1;
        private SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem37;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem7;
        private DevExpress.XtraLayout.SplitterItem splitterItem8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn59;
        private CustomButton customButton2;
        private CustomGridControl gridControlPreArch;
        private GridView gridViewPreArch;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private CustomGridControl gridControlArch;
        private GridView gridViewArch;
        private DevExpress.Utils.FlyoutPanel flyoutPanel1;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private CustomCancelButton customCancelButton1;
        private CustomComboBox customComboBox1;
        private CustomButton customButton1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn60;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn61;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn62;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn63;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupPreArch;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn64;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn65;
        private CustomLabel KITlabel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn66;
        private ToggleSwitch toggleSwitchKit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem42;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupForAdmins;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem43;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem44;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem45;
        private DevExpress.XtraLayout.LayoutControlGroup editBtns;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem49;
        private CustomActionButton ButtonEditOnlyAdv;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlEditWd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem46;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlEditOnlyAdv;
        private CustomSimpleButton customSimpleButtonRaszLog;
        private CustomSimpleButton customSimpleButtonAnnLog;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
    }
}
