using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.form.TeamWork;
using SewingProduction.Models;
using System;
using System.Windows.Forms;

namespace SewingProduction.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamWork));
            DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions11 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions12 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions13 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions14 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions15 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions16 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions17 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions18 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions19 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions20 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            repositoryItemCheckEdit1 = new RepositoryItemCheckEdit();
            repositoryItemButtonEdit2 = new RepositoryItemButtonEdit();
            xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            TabPage1 = new DevExpress.XtraTab.XtraTabPage();
            layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            pictureBox1 = new PictureBox();
            buffer = new CustomTextBox();
            gridControlKontTW = new CustomGridControl();
            gridView4 = new GridView();
            colkod_o2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colrazryd2 = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext2 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek3 = new DevExpress.XtraGrid.Columns.GridColumn();
            colannId5 = new DevExpress.XtraGrid.Columns.GridColumn();
            customGridControl4 = new CustomGridControl();
            gridView5 = new GridView();
            gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlRaskrTW = new CustomGridControl();
            gridViewRaskrTW = new GridView();
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
            gridView1 = new GridView();
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
            panelControl7 = new PanelControl();
            constructorTextBox = new RichTextBox();
            designerTextBox = new RichTextBox();
            label6 = new CustomLabel();
            label7 = new CustomLabel();
            ButtonApprovement = new CustomButton();
            commentRichTextBox = new RichTextBox();
            ANNgridControl = new CustomGridControl();
            ANNgridView = new GridView();
            colgroup = new DevExpress.XtraGrid.Columns.GridColumn();
            colarticul = new DevExpress.XtraGrid.Columns.GridColumn();
            colmod = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_vyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            coldateCreate = new DevExpress.XtraGrid.Columns.GridColumn();
            coldateUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemButtonEdit3 = new RepositoryItemButtonEdit();
            colsek_shv = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit2 = new RepositoryItemCheckEdit();
            colsek_vyazo = new DevExpress.XtraGrid.Columns.GridColumn();
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
            repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
            ButtonCopyWd = new CustomButton();
            customLabel8 = new CustomLabel();
            ButtonArchAndCopyWd = new CustomButton();
            label8 = new CustomLabel();
            PrintButton = new CustomSimpleButton();
            ButtonPreliminaryWd = new CustomButton();
            ButtonEditWd = new CustomButton();
            panel5 = new Panel();
            SortBox = new CustomCheckBox();
            archiveCheckBox = new CustomCheckBox();
            actualCheckBox = new CustomCheckBox();
            preliminaryCheckBox = new CustomCheckBox();
            customSimpleButton1 = new CustomSimpleButton();
            textEditMod = new TextEdit();
            textEditArt = new TextEdit();
            textEditSec = new TextEdit();
            textEditCreate = new TextEdit();
            layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator6 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem4 = new DevExpress.XtraLayout.SplitterItem();
            simpleSeparator4 = new DevExpress.XtraLayout.SimpleSeparator();
            simpleSeparator5 = new DevExpress.XtraLayout.SimpleSeparator();
            splitterItem5 = new DevExpress.XtraLayout.SplitterItem();
            splitterItem6 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            xtraTabPageArticles = new DevExpress.XtraTab.XtraTabPage();
            xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            xtraTabPageWorkDivisions = new DevExpress.XtraTab.XtraTabPage();
            panelControl2 = new PanelControl();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            customGridControl2 = new CustomGridControl();
            gridView3 = new GridView();
            customGridControl1 = new CustomGridControl();
            gridView2 = new GridView();
            customLabel2 = new CustomLabel();
            pictureBox2 = new PictureBox();
            gridControl_binded = new CustomGridControl();
            gridView_binded = new GridView();
            gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridView8 = new GridView();
            gridControlNZP = new CustomGridControl();
            gridViewNZP = new GridView();
            kodd_rt = new DevExpress.XtraGrid.Columns.GridColumn();
            colannId2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            kolNZP = new DevExpress.XtraGrid.Columns.GridColumn();
            PztCount = new DevExpress.XtraGrid.Columns.GridColumn();
            ButtonUnboundWd = new CustomSimpleButton();
            loadAllCheckBox = new CustomCheckBox();
            simpleButton1 = new SimpleButton();
            customGridControl3 = new CustomGridControl();
            gridView6 = new GridView();
            colannId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            coln3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControl_wdToBind = new CustomGridControl();
            gridView_wdToBind = new GridView();
            gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit3 = new RepositoryItemCheckEdit();
            colarticul1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlGroup14 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
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
            layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
            xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            splitContainerControl2 = new SplitContainerControl();
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
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl1).BeginInit();
            xtraTabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl2).BeginInit();
            layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlKontTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaskrTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRaskrTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaszTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditProizv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditOb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditPodr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl7).BeginInit();
            panelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ANNgridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textEditMod.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditArt.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditSec.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditCreate.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem32).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem34).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem35).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem29).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem30).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem25).BeginInit();
            xtraTabPageArticles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl2).BeginInit();
            xtraTabControl2.SuspendLayout();
            xtraTabPageWorkDivisions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl_binded).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView_binded).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlNZP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNZP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView6).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).BeginInit();
            xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel1).BeginInit();
            splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel2).BeginInit();
            splitContainerControl2.Panel2.SuspendLayout();
            splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)flyoutPanel1).BeginInit();
            flyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)flyoutPanelControl1).BeginInit();
            flyoutPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlPreArch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPreArch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit5).BeginInit();
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
            xtraTabControl1.AppearancePage.HeaderActive.Options.UseBackColor = true;
            xtraTabControl1.Dock = DockStyle.Fill;
            xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            xtraTabControl1.Margin = new Padding(4, 3, 4, 3);
            xtraTabControl1.Name = "xtraTabControl1";
            xtraTabControl1.SelectedTabPage = TabPage1;
            xtraTabControl1.Size = new System.Drawing.Size(1771, 965);
            xtraTabControl1.TabIndex = 0;
            xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { TabPage1, xtraTabPageArticles });
            xtraTabControl1.SelectedPageChanged += XtraTabControl1_SelectedPageChanged;
            // 
            // TabPage1
            // 
            TabPage1.Appearance.Header.BackColor = System.Drawing.Color.Transparent;
            TabPage1.Appearance.Header.Options.UseBackColor = true;
            TabPage1.Appearance.HeaderActive.BackColor = System.Drawing.Color.Transparent;
            TabPage1.Appearance.HeaderActive.Options.UseBackColor = true;
            TabPage1.Controls.Add(layoutControl2);
            TabPage1.Margin = new Padding(4, 3, 4, 3);
            TabPage1.Name = "TabPage1";
            TabPage1.Size = new System.Drawing.Size(1769, 940);
            TabPage1.Text = "1. Разделения труда";
            // 
            // layoutControl2
            // 
            layoutControl2.Controls.Add(pictureBox1);
            layoutControl2.Controls.Add(buffer);
            layoutControl2.Controls.Add(gridControlKontTW);
            layoutControl2.Controls.Add(customGridControl4);
            layoutControl2.Controls.Add(gridControlRaskrTW);
            layoutControl2.Controls.Add(gridControlRaszTW);
            layoutControl2.Controls.Add(RecoRichTextBox);
            layoutControl2.Controls.Add(panelControl7);
            layoutControl2.Controls.Add(commentRichTextBox);
            layoutControl2.Controls.Add(ANNgridControl);
            layoutControl2.Controls.Add(ButtonCopyWd);
            layoutControl2.Controls.Add(customLabel8);
            layoutControl2.Controls.Add(ButtonArchAndCopyWd);
            layoutControl2.Controls.Add(label8);
            layoutControl2.Controls.Add(PrintButton);
            layoutControl2.Controls.Add(ButtonPreliminaryWd);
            layoutControl2.Controls.Add(ButtonEditWd);
            layoutControl2.Controls.Add(panel5);
            layoutControl2.Controls.Add(customSimpleButton1);
            layoutControl2.Controls.Add(textEditMod);
            layoutControl2.Controls.Add(textEditArt);
            layoutControl2.Controls.Add(textEditSec);
            layoutControl2.Controls.Add(textEditCreate);
            layoutControl2.Dock = DockStyle.Fill;
            layoutControl2.Location = new System.Drawing.Point(0, 0);
            layoutControl2.Name = "layoutControl2";
            layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1270, 75, 650, 400);
            layoutControl2.Root = layoutControlGroup7;
            layoutControl2.Size = new System.Drawing.Size(1769, 940);
            layoutControl2.TabIndex = 11;
            layoutControl2.Text = "layoutControl2";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(603, 679);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(247, 247);
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
            buffer.Location = new System.Drawing.Point(12, 769);
            buffer.Margin = new Padding(4, 3, 4, 3);
            buffer.Multiline = true;
            buffer.Name = "buffer";
            buffer.Size = new System.Drawing.Size(175, 20);
            buffer.TabIndex = 19;
            buffer.Visible = false;
            // 
            // gridControlKontTW
            // 
            gridControlKontTW.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlKontTW.Font = new System.Drawing.Font("Arial", 10F);
            gridControlKontTW.Location = new System.Drawing.Point(876, 847);
            gridControlKontTW.MainView = gridView4;
            gridControlKontTW.Margin = new Padding(4, 3, 4, 3);
            gridControlKontTW.Name = "gridControlKontTW";
            gridControlKontTW.Size = new System.Drawing.Size(869, 67);
            gridControlKontTW.TabIndex = 18;
            gridControlKontTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView4 });
            // 
            // gridView4
            // 
            gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colkod_o2, gridColumn1, colrazryd2, coltext2, colsek3, colannId5 });
            gridView4.DetailHeight = 404;
            gridView4.GridControl = gridControlKontTW;
            gridView4.Name = "gridView4";
            gridView4.OptionsBehavior.Editable = false;
            gridView4.OptionsBehavior.ReadOnly = true;
            gridView4.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridView4.OptionsEditForm.PopupEditFormWidth = 933;
            gridView4.OptionsSelection.MultiSelect = true;
            gridView4.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView4.OptionsView.ShowGroupPanel = false;
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
            gridColumn1.FieldName = "N1";
            gridColumn1.MinWidth = 23;
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 1;
            gridColumn1.Width = 43;
            // 
            // colrazryd2
            // 
            colrazryd2.Caption = "разряд";
            colrazryd2.FieldName = "razryd";
            colrazryd2.MinWidth = 23;
            colrazryd2.Name = "colrazryd2";
            colrazryd2.Visible = true;
            colrazryd2.VisibleIndex = 2;
            colrazryd2.Width = 62;
            // 
            // coltext2
            // 
            coltext2.Caption = "наименование операции комплектовки";
            coltext2.FieldName = "Text";
            coltext2.MinWidth = 23;
            coltext2.Name = "coltext2";
            coltext2.Visible = true;
            coltext2.VisibleIndex = 3;
            coltext2.Width = 353;
            // 
            // colsek3
            // 
            colsek3.Caption = "сек.";
            colsek3.FieldName = "Sek";
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
            // customGridControl4
            // 
            customGridControl4.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            customGridControl4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            customGridControl4.Location = new System.Drawing.Point(203, 712);
            customGridControl4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            customGridControl4.LookAndFeel.UseDefaultLookAndFeel = false;
            customGridControl4.MainView = gridView5;
            customGridControl4.Margin = new Padding(4, 3, 4, 3);
            customGridControl4.Name = "customGridControl4";
            customGridControl4.Size = new System.Drawing.Size(384, 202);
            customGridControl4.TabIndex = 16;
            customGridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView5 });
            // 
            // gridView5
            // 
            gridView5.Appearance.SelectedRow.FontStyleDelta = System.Drawing.FontStyle.Bold;
            gridView5.Appearance.SelectedRow.Options.UseFont = true;
            gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn24, gridColumn25, gridColumn41, gridColumn42, gridColumn43, gridColumn44, gridColumn45, gridColumn46 });
            gridView5.DetailHeight = 404;
            gridView5.GridControl = customGridControl4;
            gridView5.GroupFormat = "{0}:  {1}{2}";
            gridView5.Name = "gridView5";
            gridView5.OptionsBehavior.Editable = false;
            gridView5.OptionsBehavior.ReadOnly = true;
            gridView5.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridView5.OptionsEditForm.PopupEditFormWidth = 933;
            gridView5.OptionsSelection.MultiSelect = true;
            gridView5.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView5.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridView5.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gridView5.OptionsView.ShowGroupPanel = false;
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
            gridColumn42.Width = 103;
            // 
            // gridColumn43
            // 
            gridColumn43.Caption = "Артикул";
            gridColumn43.FieldName = "articul";
            gridColumn43.MinWidth = 23;
            gridColumn43.Name = "gridColumn43";
            gridColumn43.Visible = true;
            gridColumn43.VisibleIndex = 1;
            gridColumn43.Width = 99;
            // 
            // gridColumn44
            // 
            gridColumn44.Caption = "Модель";
            gridColumn44.FieldName = "mod";
            gridColumn44.MinWidth = 23;
            gridColumn44.Name = "gridColumn44";
            gridColumn44.Visible = true;
            gridColumn44.VisibleIndex = 2;
            gridColumn44.Width = 110;
            // 
            // gridColumn45
            // 
            gridColumn45.Caption = "Наличие НЗП";
            gridColumn45.FieldName = "kolNZP";
            gridColumn45.MinWidth = 23;
            gridColumn45.Name = "gridColumn45";
            gridColumn45.Visible = true;
            gridColumn45.VisibleIndex = 3;
            gridColumn45.Width = 65;
            // 
            // gridColumn46
            // 
            gridColumn46.Caption = "Кол-во назн. опер.";
            gridColumn46.FieldName = "PZTCount";
            gridColumn46.MinWidth = 23;
            gridColumn46.Name = "gridColumn46";
            gridColumn46.Visible = true;
            gridColumn46.VisibleIndex = 4;
            gridColumn46.Width = 87;
            // 
            // gridControlRaskrTW
            // 
            gridControlRaskrTW.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlRaskrTW.Font = new System.Drawing.Font("Arial", 10F);
            gridControlRaskrTW.Location = new System.Drawing.Point(876, 508);
            gridControlRaskrTW.MainView = gridViewRaskrTW;
            gridControlRaskrTW.Margin = new Padding(4, 3, 4, 3);
            gridControlRaskrTW.Name = "gridControlRaskrTW";
            gridControlRaskrTW.Size = new System.Drawing.Size(869, 280);
            gridControlRaskrTW.TabIndex = 14;
            gridControlRaskrTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRaskrTW });
            // 
            // gridViewRaskrTW
            // 
            gridViewRaskrTW.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colid, colkod2, colkod_o1, gridColumn18, colrazryd1, coltext1, colsek2, gridColumn34, gridColumn33, gridColumn32, gridColumn31, colannId4 });
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
            colkod_o1.Visible = true;
            colkod_o1.VisibleIndex = 0;
            colkod_o1.Width = 52;
            // 
            // gridColumn18
            // 
            gridColumn18.Caption = "№ п/оп.";
            gridColumn18.FieldName = "N1";
            gridColumn18.MinWidth = 23;
            gridColumn18.Name = "gridColumn18";
            gridColumn18.Visible = true;
            gridColumn18.VisibleIndex = 1;
            gridColumn18.Width = 39;
            // 
            // colrazryd1
            // 
            colrazryd1.Caption = "разряд";
            colrazryd1.FieldName = "razryd";
            colrazryd1.MinWidth = 23;
            colrazryd1.Name = "colrazryd1";
            colrazryd1.Visible = true;
            colrazryd1.VisibleIndex = 2;
            colrazryd1.Width = 56;
            // 
            // coltext1
            // 
            coltext1.Caption = "наименование операции раскроя";
            coltext1.FieldName = "Text";
            coltext1.MinWidth = 23;
            coltext1.Name = "coltext1";
            coltext1.Visible = true;
            coltext1.VisibleIndex = 3;
            coltext1.Width = 291;
            // 
            // colsek2
            // 
            colsek2.Caption = "сек.";
            colsek2.FieldName = "Sek";
            colsek2.MinWidth = 23;
            colsek2.Name = "colsek2";
            colsek2.Visible = true;
            colsek2.VisibleIndex = 4;
            colsek2.Width = 69;
            // 
            // gridColumn34
            // 
            gridColumn34.Caption = "специальность";
            gridColumn34.FieldName = "Spec";
            gridColumn34.MinWidth = 23;
            gridColumn34.Name = "gridColumn34";
            gridColumn34.Visible = true;
            gridColumn34.VisibleIndex = 5;
            gridColumn34.Width = 85;
            // 
            // gridColumn33
            // 
            gridColumn33.Caption = "оборудование";
            gridColumn33.FieldName = "Obor";
            gridColumn33.MinWidth = 23;
            gridColumn33.Name = "gridColumn33";
            gridColumn33.Visible = true;
            gridColumn33.VisibleIndex = 6;
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
            gridControlRaszTW.Location = new System.Drawing.Point(864, 81);
            gridControlRaszTW.MainView = gridView1;
            gridControlRaszTW.Margin = new Padding(4, 3, 4, 3);
            gridControlRaszTW.Name = "gridControlRaszTW";
            gridControlRaszTW.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemLookUpEditProizv, repositoryItemLookUpEditOb, repositoryItemLookUpEditPodr });
            gridControlRaszTW.Size = new System.Drawing.Size(893, 380);
            gridControlRaszTW.TabIndex = 7;
            gridControlRaszTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { DisplayNumber, coln, coln1, colrazryd, coltext, colsek1, gridColumn30, gridColumn27, colobor, gridColumn26, gridColumn3, colkod_o, colannId3 });
            gridView1.DetailHeight = 404;
            gridView1.GridControl = gridControlRaszTW;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.OptionsEditForm.PopupEditFormWidth = 933;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(coln, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(coln1, DevExpress.Data.ColumnSortOrder.Ascending) });
            // 
            // DisplayNumber
            // 
            DisplayNumber.Caption = "№ оп.";
            DisplayNumber.FieldName = "DisplayNumber";
            DisplayNumber.Name = "DisplayNumber";
            DisplayNumber.Visible = true;
            DisplayNumber.VisibleIndex = 0;
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
            colrazryd.Caption = "разряд";
            colrazryd.FieldName = "razryd";
            colrazryd.MinWidth = 23;
            colrazryd.Name = "colrazryd";
            colrazryd.Visible = true;
            colrazryd.VisibleIndex = 1;
            colrazryd.Width = 56;
            // 
            // coltext
            // 
            coltext.Caption = "наименование операции пошива";
            coltext.FieldName = "Text";
            coltext.MinWidth = 23;
            coltext.Name = "coltext";
            coltext.Visible = true;
            coltext.VisibleIndex = 2;
            coltext.Width = 290;
            // 
            // colsek1
            // 
            colsek1.Caption = "сек.";
            colsek1.FieldName = "Sek";
            colsek1.MinWidth = 23;
            colsek1.Name = "colsek1";
            colsek1.Visible = true;
            colsek1.VisibleIndex = 3;
            colsek1.Width = 50;
            // 
            // gridColumn30
            // 
            gridColumn30.Caption = "спец-ть";
            gridColumn30.FieldName = "Spec";
            gridColumn30.MinWidth = 23;
            gridColumn30.Name = "gridColumn30";
            gridColumn30.Visible = true;
            gridColumn30.VisibleIndex = 4;
            gridColumn30.Width = 84;
            // 
            // gridColumn27
            // 
            gridColumn27.Caption = "производство";
            gridColumn27.FieldName = "TextProizv";
            gridColumn27.MinWidth = 23;
            gridColumn27.Name = "gridColumn27";
            gridColumn27.Visible = true;
            gridColumn27.VisibleIndex = 5;
            gridColumn27.Width = 69;
            // 
            // colobor
            // 
            colobor.Caption = "оборудование";
            colobor.FieldName = "TextOb";
            colobor.MinWidth = 23;
            colobor.Name = "colobor";
            colobor.Visible = true;
            colobor.VisibleIndex = 7;
            colobor.Width = 103;
            // 
            // gridColumn26
            // 
            gridColumn26.Caption = "вяз. подр.";
            gridColumn26.FieldName = "TextVyaz";
            gridColumn26.MinWidth = 23;
            gridColumn26.Name = "gridColumn26";
            gridColumn26.Visible = true;
            gridColumn26.VisibleIndex = 6;
            gridColumn26.Width = 65;
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
            RecoRichTextBox.Location = new System.Drawing.Point(12, 682);
            RecoRichTextBox.Margin = new Padding(4, 3, 4, 3);
            RecoRichTextBox.Name = "RecoRichTextBox";
            RecoRichTextBox.Size = new System.Drawing.Size(175, 83);
            RecoRichTextBox.TabIndex = 17;
            RecoRichTextBox.Text = "";
            // 
            // panelControl7
            // 
            panelControl7.Controls.Add(constructorTextBox);
            panelControl7.Controls.Add(designerTextBox);
            panelControl7.Controls.Add(label6);
            panelControl7.Controls.Add(label7);
            panelControl7.Controls.Add(ButtonApprovement);
            panelControl7.Location = new System.Drawing.Point(12, 330);
            panelControl7.Margin = new Padding(4, 3, 4, 3);
            panelControl7.Name = "panelControl7";
            panelControl7.Size = new System.Drawing.Size(175, 179);
            panelControl7.TabIndex = 13;
            // 
            // constructorTextBox
            // 
            constructorTextBox.Location = new System.Drawing.Point(1, 99);
            constructorTextBox.Name = "constructorTextBox";
            constructorTextBox.Size = new System.Drawing.Size(184, 39);
            constructorTextBox.TabIndex = 15;
            constructorTextBox.Text = "";
            // 
            // designerTextBox
            // 
            designerTextBox.Location = new System.Drawing.Point(-1, 38);
            designerTextBox.Name = "designerTextBox";
            designerTextBox.Size = new System.Drawing.Size(186, 39);
            designerTextBox.TabIndex = 14;
            designerTextBox.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Font = new System.Drawing.Font("Arial", 10F);
            label6.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            label6.Location = new System.Drawing.Point(6, 2);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(70, 16);
            label6.TabIndex = 0;
            label6.Text = "Дизайнер";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.Font = new System.Drawing.Font("Arial", 10F);
            label7.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            label7.Location = new System.Drawing.Point(6, 80);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(89, 16);
            label7.TabIndex = 1;
            label7.Text = "Конструктор";
            // 
            // ButtonApprovement
            // 
            ButtonApprovement.BackColor = System.Drawing.Color.White;
            ButtonApprovement.Font = new System.Drawing.Font("Arial", 10F);
            ButtonApprovement.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            ButtonApprovement.Location = new System.Drawing.Point(1, 145);
            ButtonApprovement.Margin = new Padding(4, 3, 4, 3);
            ButtonApprovement.Name = "ButtonApprovement";
            ButtonApprovement.Size = new System.Drawing.Size(213, 48);
            ButtonApprovement.TabIndex = 11;
            ButtonApprovement.Text = "Согласование с технологом";
            ButtonApprovement.UseVisualStyleBackColor = true;
            ButtonApprovement.Visible = false;
            // 
            // commentRichTextBox
            // 
            commentRichTextBox.Enabled = false;
            commentRichTextBox.Location = new System.Drawing.Point(12, 552);
            commentRichTextBox.Margin = new Padding(4, 3, 4, 3);
            commentRichTextBox.Name = "commentRichTextBox";
            commentRichTextBox.Size = new System.Drawing.Size(175, 95);
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
            ANNgridControl.Location = new System.Drawing.Point(203, 45);
            ANNgridControl.MainView = ANNgridView;
            ANNgridControl.Margin = new Padding(4, 3, 4, 3);
            ANNgridControl.Name = "ANNgridControl";
            ANNgridControl.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemButtonEdit1, repositoryItemCheckEdit2, repositoryItemButtonEdit3 });
            ANNgridControl.Size = new System.Drawing.Size(635, 617);
            ANNgridControl.TabIndex = 2;
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
            ANNgridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colgroup, colarticul, colmod, colsek, colsek_vyaz, coldateCreate, coldateUpdate, gridColumn19, colsek_shv, gridColumn5, gridColumn35, colsek_vyazo, colsek_vyaz5, colsek_vyaz7, colsek_vyaz12, colsek_vyaz10, colsek_vyaz6, colsek_kr, colslogn, colkomment, colReco, coldiz, colconstr, colannID });
            ANNgridView.CustomizationFormBounds = new System.Drawing.Rectangle(688, 388, 308, 314);
            ANNgridView.DetailHeight = 404;
            ANNgridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            ANNgridView.GridControl = ANNgridControl;
            ANNgridView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            ANNgridView.Name = "ANNgridView";
            ANNgridView.OptionsBehavior.Editable = false;
            ANNgridView.OptionsBehavior.ReadOnly = true;
            ANNgridView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            ANNgridView.OptionsEditForm.PopupEditFormWidth = 933;
            ANNgridView.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            ANNgridView.OptionsFind.AlwaysVisible = true;
            ANNgridView.OptionsFind.Behavior = FindPanelBehavior.Search;
            ANNgridView.OptionsFind.Condition = DevExpress.Data.Filtering.FilterCondition.Contains;
            ANNgridView.OptionsFind.FindDelay = 100;
            ANNgridView.OptionsFind.FindMode = FindMode.Always;
            ANNgridView.OptionsFind.FindNullPrompt = "Введите текст для поиска...";
            ANNgridView.OptionsFind.FindPanelLocation = GridFindPanelLocation.Panel;
            ANNgridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            ANNgridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            ANNgridView.OptionsView.ColumnAutoWidth = false;
            ANNgridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            ANNgridView.OptionsView.ShowGroupPanel = false;
            ANNgridView.OptionsView.ShowPreview = true;
            ANNgridView.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.True;
            ANNgridView.PreviewIndent = 1;
            ANNgridView.PreviewLineCount = 1;
            ANNgridView.RowHeight = 1;
            ANNgridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(colarticul, DevExpress.Data.ColumnSortOrder.Ascending) });
            ANNgridView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            ANNgridView.CalcPreviewText += ANNgridView_CalcPreviewText;
            ANNgridView.FocusedRowChanged += ANNgridView_FocusedRowChanged;
            // 
            // colgroup
            // 
            colgroup.Caption = "группа";
            colgroup.FieldName = "grup";
            colgroup.MinWidth = 23;
            colgroup.Name = "colgroup";
            colgroup.OptionsColumn.AllowEdit = false;
            colgroup.Visible = true;
            colgroup.VisibleIndex = 0;
            colgroup.Width = 64;
            // 
            // colarticul
            // 
            colarticul.Caption = "артикул";
            colarticul.FieldName = "Articul";
            colarticul.MinWidth = 23;
            colarticul.Name = "colarticul";
            colarticul.OptionsColumn.AllowEdit = false;
            colarticul.Visible = true;
            colarticul.VisibleIndex = 1;
            colarticul.Width = 86;
            // 
            // colmod
            // 
            colmod.Caption = "модель";
            colmod.FieldName = "Mod";
            colmod.MinWidth = 23;
            colmod.Name = "colmod";
            colmod.OptionsColumn.AllowEdit = false;
            colmod.Visible = true;
            colmod.VisibleIndex = 2;
            colmod.Width = 63;
            // 
            // colsek
            // 
            colsek.Caption = "сек. общ.";
            colsek.FieldName = "Sek";
            colsek.MinWidth = 23;
            colsek.Name = "colsek";
            colsek.OptionsColumn.AllowEdit = false;
            colsek.Visible = true;
            colsek.VisibleIndex = 3;
            colsek.Width = 56;
            // 
            // colsek_vyaz
            // 
            colsek_vyaz.Caption = "сек. вяз.";
            colsek_vyaz.FieldName = "SekVyaz";
            colsek_vyaz.MinWidth = 23;
            colsek_vyaz.Name = "colsek_vyaz";
            colsek_vyaz.OptionsColumn.AllowEdit = false;
            colsek_vyaz.Visible = true;
            colsek_vyaz.VisibleIndex = 4;
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
            // coldateUpdate
            // 
            coldateUpdate.Caption = "обновление";
            coldateUpdate.FieldName = "dateUpdate";
            coldateUpdate.MinWidth = 23;
            coldateUpdate.Name = "coldateUpdate";
            coldateUpdate.OptionsColumn.AllowEdit = false;
            coldateUpdate.Visible = true;
            coldateUpdate.VisibleIndex = 5;
            coldateUpdate.Width = 69;
            // 
            // gridColumn19
            // 
            gridColumn19.Caption = "обн.";
            gridColumn19.ColumnEdit = repositoryItemButtonEdit3;
            gridColumn19.FieldName = "Upd";
            gridColumn19.Name = "gridColumn19";
            gridColumn19.Visible = true;
            gridColumn19.VisibleIndex = 6;
            // 
            // repositoryItemButtonEdit3
            // 
            repositoryItemButtonEdit3.AutoHeight = false;
            repositoryItemButtonEdit3.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
            // 
            // colsek_shv
            // 
            colsek_shv.Caption = "сек. шв.";
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
            gridColumn5.Caption = "статус";
            gridColumn5.FieldName = "StatusText";
            gridColumn5.MinWidth = 23;
            gridColumn5.Name = "gridColumn5";
            gridColumn5.OptionsColumn.AllowEdit = false;
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 8;
            gridColumn5.Width = 63;
            // 
            // gridColumn35
            // 
            gridColumn35.Caption = "предв. архив";
            gridColumn35.ColumnEdit = repositoryItemCheckEdit2;
            gridColumn35.FieldName = "preArch";
            gridColumn35.MinWidth = 23;
            gridColumn35.Name = "gridColumn35";
            gridColumn35.OptionsColumn.AllowEdit = false;
            gridColumn35.Visible = true;
            gridColumn35.VisibleIndex = 9;
            gridColumn35.Width = 62;
            // 
            // repositoryItemCheckEdit2
            // 
            repositoryItemCheckEdit2.AutoHeight = false;
            repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // colsek_vyazo
            // 
            colsek_vyazo.Caption = "сек.отп.";
            colsek_vyazo.FieldName = "SekVyazo";
            colsek_vyazo.MinWidth = 23;
            colsek_vyazo.Name = "colsek_vyazo";
            colsek_vyazo.OptionsColumn.AllowEdit = false;
            colsek_vyazo.Visible = true;
            colsek_vyazo.VisibleIndex = 10;
            colsek_vyazo.Width = 87;
            // 
            // colsek_vyaz5
            // 
            colsek_vyaz5.Caption = "класс5";
            colsek_vyaz5.FieldName = "SekVyaz5";
            colsek_vyaz5.MinWidth = 23;
            colsek_vyaz5.Name = "colsek_vyaz5";
            colsek_vyaz5.OptionsColumn.AllowEdit = false;
            colsek_vyaz5.Visible = true;
            colsek_vyaz5.VisibleIndex = 11;
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
            colsek_vyaz7.VisibleIndex = 12;
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
            colsek_vyaz12.VisibleIndex = 13;
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
            colsek_vyaz10.VisibleIndex = 14;
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
            colsek_vyaz6.VisibleIndex = 15;
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
            colsek_kr.VisibleIndex = 16;
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
            colslogn.VisibleIndex = 17;
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
            colReco.VisibleIndex = 18;
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
            colannID.VisibleIndex = 19;
            colannID.Width = 87;
            // 
            // repositoryItemButtonEdit1
            // 
            repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // ButtonCopyWd
            // 
            ButtonCopyWd.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            ButtonCopyWd.Font = new System.Drawing.Font("Arial", 10F);
            ButtonCopyWd.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            ButtonCopyWd.Location = new System.Drawing.Point(12, 793);
            ButtonCopyWd.Margin = new Padding(4, 3, 4, 3);
            ButtonCopyWd.Name = "ButtonCopyWd";
            ButtonCopyWd.Size = new System.Drawing.Size(175, 133);
            ButtonCopyWd.TabIndex = 20;
            ButtonCopyWd.Text = "копировать РТ";
            ButtonCopyWd.UseVisualStyleBackColor = false;
            ButtonCopyWd.Visible = false;
            ButtonCopyWd.Click += ButtonCopyWd_Click;
            // 
            // customLabel8
            // 
            customLabel8.Font = new System.Drawing.Font("Arial", 10F);
            customLabel8.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customLabel8.Location = new System.Drawing.Point(12, 513);
            customLabel8.Margin = new Padding(4, 0, 4, 0);
            customLabel8.Name = "customLabel8";
            customLabel8.Size = new System.Drawing.Size(175, 35);
            customLabel8.TabIndex = 1;
            customLabel8.Text = "Рекомендации для планирования";
            // 
            // ButtonArchAndCopyWd
            // 
            ButtonArchAndCopyWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonArchAndCopyWd.FlatAppearance.BorderSize = 0;
            ButtonArchAndCopyWd.FlatStyle = FlatStyle.Flat;
            ButtonArchAndCopyWd.Font = new System.Drawing.Font("Arial", 12F);
            ButtonArchAndCopyWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonArchAndCopyWd.Location = new System.Drawing.Point(12, 209);
            ButtonArchAndCopyWd.Margin = new Padding(4, 3, 4, 3);
            ButtonArchAndCopyWd.Name = "ButtonArchAndCopyWd";
            ButtonArchAndCopyWd.Size = new System.Drawing.Size(175, 36);
            ButtonArchAndCopyWd.TabIndex = 9;
            ButtonArchAndCopyWd.Text = "архив+копия";
            ButtonArchAndCopyWd.UseVisualStyleBackColor = false;
            ButtonArchAndCopyWd.Visible = false;
            ButtonArchAndCopyWd.Click += ButtonArchAndCopyWd_Click;
            // 
            // label8
            // 
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Font = new System.Drawing.Font("Arial", 10F);
            label8.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            label8.Location = new System.Drawing.Point(12, 651);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(175, 27);
            label8.TabIndex = 1;
            label8.Text = "Особенности модели";
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
            PrintButton.Location = new System.Drawing.Point(12, 304);
            PrintButton.Margin = new Padding(4, 3, 4, 3);
            PrintButton.Name = "PrintButton";
            PrintButton.Size = new System.Drawing.Size(175, 22);
            PrintButton.StyleController = layoutControl2;
            PrintButton.TabIndex = 12;
            PrintButton.Text = "печать";
            PrintButton.Visible = false;
            PrintButton.Click += customSimpleButton1_Click;
            // 
            // ButtonPreliminaryWd
            // 
            ButtonPreliminaryWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonPreliminaryWd.FlatAppearance.BorderSize = 0;
            ButtonPreliminaryWd.FlatStyle = FlatStyle.Flat;
            ButtonPreliminaryWd.Font = new System.Drawing.Font("Arial", 10F);
            ButtonPreliminaryWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonPreliminaryWd.Location = new System.Drawing.Point(12, 275);
            ButtonPreliminaryWd.Margin = new Padding(4, 3, 4, 3);
            ButtonPreliminaryWd.Name = "ButtonPreliminaryWd";
            ButtonPreliminaryWd.Size = new System.Drawing.Size(175, 25);
            ButtonPreliminaryWd.TabIndex = 11;
            ButtonPreliminaryWd.Text = "добавить предв";
            ButtonPreliminaryWd.UseVisualStyleBackColor = false;
            ButtonPreliminaryWd.Visible = false;
            ButtonPreliminaryWd.Click += ButtonPreliminaryWd_Click;
            // 
            // ButtonEditWd
            // 
            ButtonEditWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonEditWd.FlatAppearance.BorderSize = 0;
            ButtonEditWd.FlatStyle = FlatStyle.Flat;
            ButtonEditWd.Font = new System.Drawing.Font("Arial", 12F);
            ButtonEditWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonEditWd.Location = new System.Drawing.Point(12, 170);
            ButtonEditWd.Margin = new Padding(4, 3, 4, 3);
            ButtonEditWd.Name = "ButtonEditWd";
            ButtonEditWd.Size = new System.Drawing.Size(175, 35);
            ButtonEditWd.TabIndex = 8;
            ButtonEditWd.Text = "редактировать РТ";
            ButtonEditWd.UseVisualStyleBackColor = false;
            ButtonEditWd.Visible = false;
            ButtonEditWd.Click += ButtonEditWd_Click;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(SortBox);
            panel5.Controls.Add(archiveCheckBox);
            panel5.Controls.Add(actualCheckBox);
            panel5.Controls.Add(preliminaryCheckBox);
            panel5.Location = new System.Drawing.Point(12, 12);
            panel5.Margin = new Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(175, 154);
            panel5.TabIndex = 0;
            // 
            // SortBox
            // 
            SortBox.AutoSize = true;
            SortBox.Font = new System.Drawing.Font("Arial", 10F);
            SortBox.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            SortBox.Location = new System.Drawing.Point(10, 118);
            SortBox.Margin = new Padding(4, 3, 4, 3);
            SortBox.Name = "SortBox";
            SortBox.Size = new System.Drawing.Size(120, 20);
            SortBox.TabIndex = 4;
            SortBox.Text = "Не описанные";
            SortBox.UseVisualStyleBackColor = true;
            SortBox.CheckedChanged += Filter_CheckedChanged;
            // 
            // archiveCheckBox
            // 
            archiveCheckBox.AutoSize = true;
            archiveCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            archiveCheckBox.ForeColor = System.Drawing.Color.FromArgb(128, 64, 0);
            archiveCheckBox.Location = new System.Drawing.Point(10, 72);
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
            actualCheckBox.Location = new System.Drawing.Point(10, 42);
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
            // customSimpleButton1
            // 
            customSimpleButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            customSimpleButton1.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSimpleButton1.Appearance.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customSimpleButton1.Appearance.Options.UseBackColor = true;
            customSimpleButton1.Appearance.Options.UseFont = true;
            customSimpleButton1.Appearance.Options.UseForeColor = true;
            customSimpleButton1.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButton1.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButton1.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButton1.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButton1.Location = new System.Drawing.Point(12, 249);
            customSimpleButton1.Name = "customSimpleButton1";
            customSimpleButton1.Size = new System.Drawing.Size(175, 22);
            customSimpleButton1.StyleController = layoutControl2;
            customSimpleButton1.TabIndex = 10;
            customSimpleButton1.Text = "дубль";
            customSimpleButton1.Visible = false;
            customSimpleButton1.Click += DuplicateWorkDivision_Click_Internal_Wrapper;
            // 
            // textEditMod
            // 
            textEditMod.Enabled = false;
            textEditMod.Location = new System.Drawing.Point(972, 45);
            textEditMod.Name = "textEditMod";
            textEditMod.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditMod.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            textEditMod.Properties.Appearance.Options.UseFont = true;
            textEditMod.Properties.Appearance.Options.UseForeColor = true;
            textEditMod.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditMod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            textEditMod.Properties.AppearanceDisabled.Options.UseFont = true;
            textEditMod.Properties.AppearanceDisabled.Options.UseForeColor = true;
            textEditMod.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            textEditMod.Size = new System.Drawing.Size(50, 20);
            textEditMod.StyleController = layoutControl2;
            textEditMod.TabIndex = 3;
            // 
            // textEditArt
            // 
            textEditArt.Enabled = false;
            textEditArt.Location = new System.Drawing.Point(1122, 45);
            textEditArt.Name = "textEditArt";
            textEditArt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditArt.Properties.Appearance.Options.UseFont = true;
            textEditArt.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditArt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            textEditArt.Properties.AppearanceDisabled.Options.UseFont = true;
            textEditArt.Properties.AppearanceDisabled.Options.UseForeColor = true;
            textEditArt.Size = new System.Drawing.Size(50, 20);
            textEditArt.StyleController = layoutControl2;
            textEditArt.TabIndex = 4;
            // 
            // textEditSec
            // 
            textEditSec.Enabled = false;
            textEditSec.Location = new System.Drawing.Point(1272, 45);
            textEditSec.Name = "textEditSec";
            textEditSec.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditSec.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            textEditSec.Properties.Appearance.Options.UseFont = true;
            textEditSec.Properties.Appearance.Options.UseForeColor = true;
            textEditSec.Size = new System.Drawing.Size(50, 20);
            textEditSec.StyleController = layoutControl2;
            textEditSec.TabIndex = 5;
            // 
            // textEditCreate
            // 
            textEditCreate.Enabled = false;
            textEditCreate.Location = new System.Drawing.Point(1422, 45);
            textEditCreate.Name = "textEditCreate";
            textEditCreate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            textEditCreate.Properties.Appearance.Options.UseFont = true;
            textEditCreate.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            textEditCreate.Properties.AppearanceDisabled.Options.UseForeColor = true;
            textEditCreate.Size = new System.Drawing.Size(323, 20);
            textEditCreate.StyleController = layoutControl2;
            textEditCreate.TabIndex = 6;
            // 
            // layoutControlGroup7
            // 
            layoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup7.GroupBordersVisible = false;
            layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem10, layoutControlItem12, layoutControlItem15, layoutControlItem16, layoutControlItem17, layoutControlItem18, layoutControlItem19, layoutControlItem20, layoutControlItem21, layoutControlItem22, layoutControlItem23, layoutControlItem24, layoutControlGroup8, layoutControlGroup9, layoutControlGroup10, splitterItem4, simpleSeparator4, simpleSeparator5, splitterItem5, splitterItem6, layoutControlItem28, layoutControlGroup11, layoutControlGroup12, layoutControlItem31, layoutControlItem25 });
            layoutControlGroup7.Name = "Root";
            layoutControlGroup7.Size = new System.Drawing.Size(1769, 940);
            layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.Control = panel5;
            layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            layoutControlItem10.Name = "layoutControlItem10";
            layoutControlItem10.Size = new System.Drawing.Size(179, 158);
            layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            layoutControlItem12.Control = ButtonEditWd;
            layoutControlItem12.Location = new System.Drawing.Point(0, 158);
            layoutControlItem12.Name = "layoutControlItem12";
            layoutControlItem12.Size = new System.Drawing.Size(179, 39);
            layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            layoutControlItem15.Control = ButtonArchAndCopyWd;
            layoutControlItem15.Location = new System.Drawing.Point(0, 197);
            layoutControlItem15.Name = "layoutControlItem15";
            layoutControlItem15.Size = new System.Drawing.Size(179, 40);
            layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            layoutControlItem16.Control = customSimpleButton1;
            layoutControlItem16.Location = new System.Drawing.Point(0, 237);
            layoutControlItem16.Name = "layoutControlItem16";
            layoutControlItem16.Size = new System.Drawing.Size(179, 26);
            layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            layoutControlItem17.Control = ButtonPreliminaryWd;
            layoutControlItem17.Location = new System.Drawing.Point(0, 263);
            layoutControlItem17.Name = "layoutControlItem17";
            layoutControlItem17.Size = new System.Drawing.Size(179, 29);
            layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            layoutControlItem18.Control = PrintButton;
            layoutControlItem18.Location = new System.Drawing.Point(0, 292);
            layoutControlItem18.Name = "layoutControlItem18";
            layoutControlItem18.Size = new System.Drawing.Size(179, 26);
            layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            layoutControlItem19.Control = panelControl7;
            layoutControlItem19.Location = new System.Drawing.Point(0, 318);
            layoutControlItem19.Name = "layoutControlItem19";
            layoutControlItem19.Size = new System.Drawing.Size(179, 183);
            layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            layoutControlItem20.Control = customLabel8;
            layoutControlItem20.Location = new System.Drawing.Point(0, 501);
            layoutControlItem20.Name = "layoutControlItem20";
            layoutControlItem20.Size = new System.Drawing.Size(179, 39);
            layoutControlItem20.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            layoutControlItem21.Control = label8;
            layoutControlItem21.Location = new System.Drawing.Point(0, 639);
            layoutControlItem21.Name = "layoutControlItem21";
            layoutControlItem21.Size = new System.Drawing.Size(179, 31);
            layoutControlItem21.TextVisible = false;
            // 
            // layoutControlItem22
            // 
            layoutControlItem22.Control = commentRichTextBox;
            layoutControlItem22.Location = new System.Drawing.Point(0, 540);
            layoutControlItem22.Name = "layoutControlItem22";
            layoutControlItem22.Size = new System.Drawing.Size(179, 99);
            layoutControlItem22.TextVisible = false;
            // 
            // layoutControlItem23
            // 
            layoutControlItem23.Control = RecoRichTextBox;
            layoutControlItem23.Location = new System.Drawing.Point(0, 670);
            layoutControlItem23.Name = "layoutControlItem23";
            layoutControlItem23.Size = new System.Drawing.Size(179, 87);
            layoutControlItem23.TextVisible = false;
            // 
            // layoutControlItem24
            // 
            layoutControlItem24.Control = ButtonCopyWd;
            layoutControlItem24.Location = new System.Drawing.Point(0, 781);
            layoutControlItem24.MinSize = new System.Drawing.Size(24, 24);
            layoutControlItem24.Name = "layoutControlItem24";
            layoutControlItem24.Size = new System.Drawing.Size(179, 137);
            layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem24.TextVisible = false;
            // 
            // layoutControlGroup8
            // 
            layoutControlGroup8.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Добавить предварительное", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, false, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Редактировать РТ", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Дубль", true, buttonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Архив+копия", true, buttonImageOptions7, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("  ", true, buttonImageOptions8, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("  ", true, buttonImageOptions9, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Печать", true, buttonImageOptions10, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup8.CustomizationFormText = "Разделения труда";
            layoutControlGroup8.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem26, simpleSeparator6 });
            layoutControlGroup8.Location = new System.Drawing.Point(179, 0);
            layoutControlGroup8.Name = "layoutControlGroup8";
            layoutControlGroup8.Size = new System.Drawing.Size(663, 667);
            layoutControlGroup8.Text = "Разделения труда";
            layoutControlGroup8.CustomButtonClick += layoutControlGroup8_CustomButtonClick;
            // 
            // layoutControlItem26
            // 
            layoutControlItem26.Control = ANNgridControl;
            layoutControlItem26.Location = new System.Drawing.Point(0, 0);
            layoutControlItem26.Name = "layoutControlItem26";
            layoutControlItem26.Size = new System.Drawing.Size(639, 621);
            layoutControlItem26.TextVisible = false;
            // 
            // simpleSeparator6
            // 
            simpleSeparator6.Location = new System.Drawing.Point(0, 621);
            simpleSeparator6.Name = "simpleSeparator6";
            simpleSeparator6.Size = new System.Drawing.Size(639, 1);
            // 
            // layoutControlGroup9
            // 
            layoutControlGroup9.CustomizationFormText = "Схема РТ";
            layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem32, layoutControlItem34, layoutControlItem35, layoutControlItem33 });
            layoutControlGroup9.Location = new System.Drawing.Point(852, 0);
            layoutControlGroup9.Name = "layoutControlGroup9";
            layoutControlGroup9.Size = new System.Drawing.Size(897, 69);
            layoutControlGroup9.Text = "Схема РТ";
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
            layoutControlItem32.Name = "layoutControlItem32";
            layoutControlItem32.Size = new System.Drawing.Size(150, 24);
            layoutControlItem32.Text = "Модель";
            layoutControlItem32.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem32.TextSize = new System.Drawing.Size(84, 13);
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
            layoutControlItem34.Location = new System.Drawing.Point(300, 0);
            layoutControlItem34.Name = "layoutControlItem34";
            layoutControlItem34.Size = new System.Drawing.Size(150, 24);
            layoutControlItem34.Text = "Сек.";
            layoutControlItem34.TextSize = new System.Drawing.Size(84, 13);
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
            layoutControlItem35.Location = new System.Drawing.Point(450, 0);
            layoutControlItem35.Name = "layoutControlItem35";
            layoutControlItem35.Size = new System.Drawing.Size(423, 24);
            layoutControlItem35.Text = "Дата созд.";
            layoutControlItem35.TextSize = new System.Drawing.Size(84, 13);
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
            layoutControlItem33.Location = new System.Drawing.Point(150, 0);
            layoutControlItem33.Name = "layoutControlItem33";
            layoutControlItem33.Size = new System.Drawing.Size(150, 24);
            layoutControlItem33.Text = "Наименование";
            layoutControlItem33.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlGroup10
            // 
            layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem29 });
            layoutControlGroup10.Location = new System.Drawing.Point(852, 463);
            layoutControlGroup10.Name = "layoutControlGroup10";
            layoutControlGroup10.Size = new System.Drawing.Size(897, 329);
            layoutControlGroup10.Text = "Нормы раскроя";
            // 
            // layoutControlItem29
            // 
            layoutControlItem29.Control = gridControlRaskrTW;
            layoutControlItem29.Location = new System.Drawing.Point(0, 0);
            layoutControlItem29.Name = "layoutControlItem29";
            layoutControlItem29.Size = new System.Drawing.Size(873, 284);
            layoutControlItem29.TextVisible = false;
            // 
            // splitterItem4
            // 
            splitterItem4.Location = new System.Drawing.Point(842, 0);
            splitterItem4.Name = "splitterItem4";
            splitterItem4.Size = new System.Drawing.Size(10, 918);
            // 
            // simpleSeparator4
            // 
            simpleSeparator4.Location = new System.Drawing.Point(0, 918);
            simpleSeparator4.Name = "simpleSeparator4";
            simpleSeparator4.Size = new System.Drawing.Size(1749, 1);
            // 
            // simpleSeparator5
            // 
            simpleSeparator5.Location = new System.Drawing.Point(0, 919);
            simpleSeparator5.Name = "simpleSeparator5";
            simpleSeparator5.Size = new System.Drawing.Size(1749, 1);
            // 
            // splitterItem5
            // 
            splitterItem5.Location = new System.Drawing.Point(852, 453);
            splitterItem5.Name = "splitterItem5";
            splitterItem5.Size = new System.Drawing.Size(897, 10);
            // 
            // splitterItem6
            // 
            splitterItem6.Location = new System.Drawing.Point(852, 792);
            splitterItem6.Name = "splitterItem6";
            splitterItem6.Size = new System.Drawing.Size(897, 10);
            // 
            // layoutControlItem28
            // 
            layoutControlItem28.Control = gridControlRaszTW;
            layoutControlItem28.Location = new System.Drawing.Point(852, 69);
            layoutControlItem28.Name = "layoutControlItem28";
            layoutControlItem28.Size = new System.Drawing.Size(897, 384);
            layoutControlItem28.TextVisible = false;
            // 
            // layoutControlGroup11
            // 
            layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem27 });
            layoutControlGroup11.Location = new System.Drawing.Point(179, 667);
            layoutControlGroup11.Name = "layoutControlGroup11";
            layoutControlGroup11.Size = new System.Drawing.Size(412, 251);
            layoutControlGroup11.Text = "Привязанные артикулы";
            // 
            // layoutControlItem27
            // 
            layoutControlItem27.Control = customGridControl4;
            layoutControlItem27.CustomizationFormText = "Привязанные артикулы";
            layoutControlItem27.HighlightFocusedItem = DevExpress.Utils.DefaultBoolean.True;
            layoutControlItem27.Location = new System.Drawing.Point(0, 0);
            layoutControlItem27.Name = "layoutControlItem27";
            layoutControlItem27.Size = new System.Drawing.Size(388, 206);
            layoutControlItem27.Text = "Привязанные артикулы";
            layoutControlItem27.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem27.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem30 });
            layoutControlGroup12.Location = new System.Drawing.Point(852, 802);
            layoutControlGroup12.Name = "layoutControlGroup12";
            layoutControlGroup12.Size = new System.Drawing.Size(897, 116);
            layoutControlGroup12.Text = "Операции комплектовки";
            // 
            // layoutControlItem30
            // 
            layoutControlItem30.Control = gridControlKontTW;
            layoutControlItem30.Location = new System.Drawing.Point(0, 0);
            layoutControlItem30.Name = "layoutControlItem30";
            layoutControlItem30.Size = new System.Drawing.Size(873, 71);
            layoutControlItem30.TextVisible = false;
            // 
            // layoutControlItem31
            // 
            layoutControlItem31.Control = pictureBox1;
            layoutControlItem31.Location = new System.Drawing.Point(591, 667);
            layoutControlItem31.Name = "layoutControlItem31";
            layoutControlItem31.Size = new System.Drawing.Size(251, 251);
            layoutControlItem31.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            layoutControlItem25.Control = buffer;
            layoutControlItem25.Location = new System.Drawing.Point(0, 757);
            layoutControlItem25.Name = "layoutControlItem25";
            layoutControlItem25.Size = new System.Drawing.Size(179, 24);
            layoutControlItem25.TextVisible = false;
            // 
            // xtraTabPageArticles
            // 
            xtraTabPageArticles.Controls.Add(xtraTabControl2);
            xtraTabPageArticles.Margin = new Padding(4, 3, 4, 3);
            xtraTabPageArticles.Name = "xtraTabPageArticles";
            xtraTabPageArticles.Size = new System.Drawing.Size(1769, 940);
            xtraTabPageArticles.Text = "2. Текущие работы";
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
            xtraTabControl2.Size = new System.Drawing.Size(1769, 940);
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
            xtraTabPageWorkDivisions.Size = new System.Drawing.Size(1767, 915);
            xtraTabPageWorkDivisions.Text = "Требуют увязки";
            // 
            // panelControl2
            // 
            panelControl2.Appearance.BackColor = System.Drawing.Color.Yellow;
            panelControl2.Appearance.Options.UseBackColor = true;
            panelControl2.Controls.Add(layoutControl1);
            panelControl2.Dock = DockStyle.Fill;
            panelControl2.Location = new System.Drawing.Point(0, 0);
            panelControl2.Margin = new Padding(4, 3, 4, 3);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1767, 915);
            panelControl2.TabIndex = 4;
            // 
            // layoutControl1
            // 
            layoutControl1.BackColor = System.Drawing.Color.Transparent;
            layoutControl1.Controls.Add(customGridControl2);
            layoutControl1.Controls.Add(customGridControl1);
            layoutControl1.Controls.Add(customLabel2);
            layoutControl1.Controls.Add(pictureBox2);
            layoutControl1.Controls.Add(gridControl_binded);
            layoutControl1.Controls.Add(gridControlNZP);
            layoutControl1.Controls.Add(ButtonUnboundWd);
            layoutControl1.Controls.Add(loadAllCheckBox);
            layoutControl1.Controls.Add(simpleButton1);
            layoutControl1.Controls.Add(customGridControl3);
            layoutControl1.Controls.Add(gridControl_wdToBind);
            layoutControl1.Controls.Add(gridControl_unboundArts);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(2, 2);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1046, 203, 650, 390);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1763, 911);
            layoutControl1.TabIndex = 4;
            layoutControl1.Text = "layoutControl1";
            // 
            // customGridControl2
            // 
            customGridControl2.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl2.Location = new System.Drawing.Point(558, 821);
            customGridControl2.MainView = gridView3;
            customGridControl2.Name = "customGridControl2";
            customGridControl2.Size = new System.Drawing.Size(278, 20);
            customGridControl2.TabIndex = 1;
            customGridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView3 });
            // 
            // gridView3
            // 
            gridView3.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridView3.Appearance.EvenRow.Options.UseBackColor = true;
            gridView3.GridControl = customGridControl2;
            gridView3.Name = "gridView3";
            gridView3.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // customGridControl1
            // 
            customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl1.Location = new System.Drawing.Point(558, 821);
            customGridControl1.MainView = gridView2;
            customGridControl1.Name = "customGridControl1";
            customGridControl1.Size = new System.Drawing.Size(278, 20);
            customGridControl1.TabIndex = 1;
            customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView2 });
            // 
            // gridView2
            // 
            gridView2.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 220, 240);
            gridView2.Appearance.EvenRow.Options.UseBackColor = true;
            gridView2.GridControl = customGridControl1;
            gridView2.Name = "gridView2";
            gridView2.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // customLabel2
            // 
            customLabel2.Font = new System.Drawing.Font("Arial", 10F);
            customLabel2.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabel2.Location = new System.Drawing.Point(24, 644);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new System.Drawing.Size(470, 20);
            customLabel2.TabIndex = 1;
            customLabel2.Text = "Увязанные в этом сеансе";
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new System.Drawing.Point(862, 786);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(258, 67);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // gridControl_binded
            // 
            gridControl_binded.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControl_binded.Font = new System.Drawing.Font("Arial", 10F);
            gridControl_binded.Location = new System.Drawing.Point(24, 668);
            gridControl_binded.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl_binded.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControl_binded.MainView = gridView_binded;
            gridControl_binded.Margin = new Padding(4, 3, 4, 3);
            gridControl_binded.Name = "gridControl_binded";
            gridControl_binded.Size = new System.Drawing.Size(470, 219);
            gridControl_binded.TabIndex = 2;
            gridControl_binded.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_binded, gridView8 });
            // 
            // gridView_binded
            // 
            gridView_binded.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn36, gridColumn37, gridColumn38, gridColumn39, gridColumn40 });
            gridView_binded.DetailHeight = 404;
            gridView_binded.GridControl = gridControl_binded;
            gridView_binded.Name = "gridView_binded";
            gridView_binded.OptionsEditForm.PopupEditFormWidth = 933;
            gridView_binded.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn36
            // 
            gridColumn36.Caption = "артикул";
            gridColumn36.FieldName = "Articul";
            gridColumn36.MinWidth = 23;
            gridColumn36.Name = "gridColumn36";
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
            gridColumn40.Visible = true;
            gridColumn40.VisibleIndex = 4;
            gridColumn40.Width = 87;
            // 
            // gridView8
            // 
            gridView8.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            gridView8.Appearance.EvenRow.Options.UseBackColor = true;
            gridView8.DetailHeight = 404;
            gridView8.GridControl = gridControl_binded;
            gridView8.Name = "gridView8";
            gridView8.OptionsEditForm.PopupEditFormWidth = 933;
            gridView8.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // gridControlNZP
            // 
            gridControlNZP.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlNZP.Font = new System.Drawing.Font("Arial", 10F);
            gridControlNZP.Location = new System.Drawing.Point(1136, 753);
            gridControlNZP.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControlNZP.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControlNZP.MainView = gridViewNZP;
            gridControlNZP.Margin = new Padding(4, 3, 4, 3);
            gridControlNZP.Name = "gridControlNZP";
            gridControlNZP.Size = new System.Drawing.Size(591, 112);
            gridControlNZP.TabIndex = 9;
            gridControlNZP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewNZP });
            // 
            // gridViewNZP
            // 
            gridViewNZP.Appearance.SelectedRow.FontStyleDelta = System.Drawing.FontStyle.Bold;
            gridViewNZP.Appearance.SelectedRow.Options.UseFont = true;
            gridViewNZP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { kodd_rt, colannId2, gridColumn20, gridColumn21, gridColumn22, gridColumn23, kolNZP, PztCount });
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
            gridViewNZP.FocusedRowChanged += gridView5_FocusedRowChanged;
            // 
            // kodd_rt
            // 
            kodd_rt.FieldName = "kodd_rt";
            kodd_rt.MinWidth = 23;
            kodd_rt.Name = "kodd_rt";
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
            gridColumn21.Visible = true;
            gridColumn21.VisibleIndex = 0;
            gridColumn21.Width = 103;
            // 
            // gridColumn22
            // 
            gridColumn22.Caption = "Артикул";
            gridColumn22.FieldName = "articul";
            gridColumn22.MinWidth = 23;
            gridColumn22.Name = "gridColumn22";
            gridColumn22.Visible = true;
            gridColumn22.VisibleIndex = 1;
            gridColumn22.Width = 99;
            // 
            // gridColumn23
            // 
            gridColumn23.Caption = "Модель";
            gridColumn23.FieldName = "mod";
            gridColumn23.MinWidth = 23;
            gridColumn23.Name = "gridColumn23";
            gridColumn23.Visible = true;
            gridColumn23.VisibleIndex = 2;
            gridColumn23.Width = 110;
            // 
            // kolNZP
            // 
            kolNZP.Caption = "Наличие НЗП";
            kolNZP.FieldName = "kolNZP";
            kolNZP.MinWidth = 23;
            kolNZP.Name = "kolNZP";
            kolNZP.Visible = true;
            kolNZP.VisibleIndex = 3;
            kolNZP.Width = 65;
            // 
            // PztCount
            // 
            PztCount.Caption = "Кол-во назн. опер.";
            PztCount.FieldName = "PZTCount";
            PztCount.MinWidth = 23;
            PztCount.Name = "PztCount";
            PztCount.Visible = true;
            PztCount.VisibleIndex = 4;
            PztCount.Width = 87;
            // 
            // ButtonUnboundWd
            // 
            ButtonUnboundWd.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            ButtonUnboundWd.AppearanceDisabled.BorderColor = System.Drawing.Color.Gray;
            ButtonUnboundWd.AppearanceDisabled.Options.UseBackColor = true;
            ButtonUnboundWd.AppearanceDisabled.Options.UseBorderColor = true;
            ButtonUnboundWd.Location = new System.Drawing.Point(901, 45);
            ButtonUnboundWd.Margin = new Padding(4, 3, 4, 3);
            ButtonUnboundWd.Name = "ButtonUnboundWd";
            ButtonUnboundWd.Size = new System.Drawing.Size(76, 36);
            ButtonUnboundWd.StyleController = layoutControl1;
            ButtonUnboundWd.TabIndex = 5;
            ButtonUnboundWd.Text = "Отвязать";
            ButtonUnboundWd.Click += ButtonUnboundWd_Click;
            // 
            // loadAllCheckBox
            // 
            loadAllCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            loadAllCheckBox.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            loadAllCheckBox.Location = new System.Drawing.Point(522, 45);
            loadAllCheckBox.Margin = new Padding(4, 3, 4, 3);
            loadAllCheckBox.Name = "loadAllCheckBox";
            loadAllCheckBox.Size = new System.Drawing.Size(174, 20);
            loadAllCheckBox.TabIndex = 3;
            loadAllCheckBox.Text = "Показать все РТ";
            loadAllCheckBox.UseVisualStyleBackColor = true;
            loadAllCheckBox.CheckedChanged += loadAllCheckBox_CheckedChanged;
            // 
            // simpleButton1
            // 
            simpleButton1.BackgroundImageLayout = ImageLayout.None;
            simpleButton1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.ImageOptions.Location = ImageLocation.MiddleCenter;
            simpleButton1.Location = new System.Drawing.Point(815, 45);
            simpleButton1.Margin = new Padding(4, 3, 4, 3);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(82, 36);
            simpleButton1.StyleController = layoutControl1;
            simpleButton1.TabIndex = 4;
            simpleButton1.Text = "Увязать";
            simpleButton1.Click += BindButton_Click;
            // 
            // customGridControl3
            // 
            customGridControl3.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            customGridControl3.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl3.Location = new System.Drawing.Point(558, 821);
            customGridControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            customGridControl3.MainView = gridView6;
            customGridControl3.Margin = new Padding(4, 3, 4, 3);
            customGridControl3.Name = "customGridControl3";
            customGridControl3.Size = new System.Drawing.Size(278, 20);
            customGridControl3.TabIndex = 8;
            customGridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView6 });
            customGridControl3.Click += ResetButton_Click;
            // 
            // gridView6
            // 
            gridView6.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
            gridView6.Appearance.SelectedRow.Options.UseBackColor = true;
            gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colannId1, coln3, gridColumn9, gridColumn10, gridColumn11 });
            gridView6.DetailHeight = 404;
            gridView6.GridControl = customGridControl3;
            gridView6.Name = "gridView6";
            gridView6.OptionsEditForm.PopupEditFormWidth = 933;
            gridView6.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridView6.OptionsView.ShowGroupPanel = false;
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
            coln3.Visible = true;
            coln3.VisibleIndex = 0;
            coln3.Width = 87;
            // 
            // gridColumn9
            // 
            gridColumn9.Caption = "№п/оп.";
            gridColumn9.FieldName = "N1";
            gridColumn9.MinWidth = 23;
            gridColumn9.Name = "gridColumn9";
            gridColumn9.Visible = true;
            gridColumn9.VisibleIndex = 1;
            gridColumn9.Width = 101;
            // 
            // gridColumn10
            // 
            gridColumn10.Caption = "Разряд";
            gridColumn10.FieldName = "razryd";
            gridColumn10.MinWidth = 23;
            gridColumn10.Name = "gridColumn10";
            gridColumn10.Visible = true;
            gridColumn10.VisibleIndex = 2;
            gridColumn10.Width = 131;
            // 
            // gridColumn11
            // 
            gridColumn11.Caption = "Наименование операции пошива";
            gridColumn11.FieldName = "Text";
            gridColumn11.MinWidth = 23;
            gridColumn11.Name = "gridColumn11";
            gridColumn11.Visible = true;
            gridColumn11.VisibleIndex = 3;
            gridColumn11.Width = 702;
            // 
            // gridControl_wdToBind
            // 
            gridControl_wdToBind.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControl_wdToBind.Font = new System.Drawing.Font("Arial", 10F);
            gridControl_wdToBind.Location = new System.Drawing.Point(534, 118);
            gridControl_wdToBind.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl_wdToBind.MainView = gridView_wdToBind;
            gridControl_wdToBind.Margin = new Padding(0);
            gridControl_wdToBind.Name = "gridControl_wdToBind";
            gridControl_wdToBind.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemCheckEdit3, repositoryItemCheckEdit4, repositoryItemCheckEdit6, repositoryItemCheckEdit7 });
            gridControl_wdToBind.Size = new System.Drawing.Size(1193, 584);
            gridControl_wdToBind.TabIndex = 6;
            gridControl_wdToBind.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_wdToBind });
            // 
            // gridView_wdToBind
            // 
            gridView_wdToBind.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Yellow;
            gridView_wdToBind.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            gridView_wdToBind.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn12, gridColumn13, colarticul1, gridColumn8, gridColumn29, gridColumn28, colstatus1, gridColumn6, colannId7 });
            gridView_wdToBind.DetailHeight = 404;
            gridView_wdToBind.GridControl = gridControl_wdToBind;
            gridView_wdToBind.Name = "gridView_wdToBind";
            gridView_wdToBind.OptionsCustomization.AllowColumnMoving = false;
            gridView_wdToBind.OptionsEditForm.PopupEditFormWidth = 933;
            gridView_wdToBind.OptionsFilter.AllowMRUFilterList = false;
            gridView_wdToBind.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            gridView_wdToBind.OptionsFind.AlwaysVisible = true;
            gridView_wdToBind.OptionsFind.FindDelay = 500;
            gridView_wdToBind.OptionsFind.FindNullPrompt = "Введите значение для поиска...";
            gridView_wdToBind.OptionsMenu.EnableColumnMenu = false;
            gridView_wdToBind.OptionsView.ShowAutoFilterRow = true;
            gridView_wdToBind.OptionsView.ShowGroupPanel = false;
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
            colarticul1.Caption = "артикул";
            colarticul1.FieldName = "Articul";
            colarticul1.MinWidth = 23;
            colarticul1.Name = "colarticul1";
            colarticul1.Visible = true;
            colarticul1.VisibleIndex = 1;
            colarticul1.Width = 79;
            // 
            // gridColumn8
            // 
            gridColumn8.Caption = "status";
            gridColumn8.FieldName = "Status";
            gridColumn8.MinWidth = 23;
            gridColumn8.Name = "gridColumn8";
            gridColumn8.Width = 87;
            // 
            // gridColumn29
            // 
            gridColumn29.Caption = "группа";
            gridColumn29.FieldName = "grup";
            gridColumn29.MinWidth = 23;
            gridColumn29.Name = "gridColumn29";
            gridColumn29.Visible = true;
            gridColumn29.VisibleIndex = 2;
            gridColumn29.Width = 87;
            // 
            // gridColumn28
            // 
            gridColumn28.Caption = "модель";
            gridColumn28.FieldName = "mod";
            gridColumn28.MinWidth = 23;
            gridColumn28.Name = "gridColumn28";
            gridColumn28.Visible = true;
            gridColumn28.VisibleIndex = 3;
            gridColumn28.Width = 87;
            // 
            // colstatus1
            // 
            colstatus1.Caption = "статус";
            colstatus1.FieldName = "Status";
            colstatus1.MinWidth = 23;
            colstatus1.Name = "colstatus1";
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
            gridControl_unboundArts.Size = new System.Drawing.Size(470, 544);
            gridControl_unboundArts.TabIndex = 0;
            gridControl_unboundArts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_unboundArts });
            // 
            // gridView_unboundArts
            // 
            gridView_unboundArts.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
            gridView_unboundArts.Appearance.SelectedRow.Options.UseBackColor = true;
            gridView_unboundArts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { код, артикул, gridColumn2, gridColumn7, группа, модель, gridColumn4 });
            gridView_unboundArts.DetailHeight = 404;
            gridView_unboundArts.GridControl = gridControl_unboundArts;
            gridView_unboundArts.Name = "gridView_unboundArts";
            gridView_unboundArts.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.False;
            gridView_unboundArts.OptionsDetail.EnableMasterViewMode = false;
            gridView_unboundArts.OptionsEditForm.PopupEditFormWidth = 933;
            gridView_unboundArts.OptionsFind.AlwaysVisible = true;
            gridView_unboundArts.OptionsFind.FindDelay = 500;
            gridView_unboundArts.OptionsFind.FindMode = FindMode.Always;
            gridView_unboundArts.OptionsFind.FindNullPrompt = "Введите значение для поиска...";
            gridView_unboundArts.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridView_unboundArts.OptionsView.ShowGroupPanel = false;
            gridView_unboundArts.ScrollStyle = ScrollStyleFlags.LiveVertScroll;
            gridView_unboundArts.FocusedRowChanged += gridView_unboundArts_FocusedRowChanged;
            // 
            // код
            // 
            код.Caption = "код";
            код.FieldName = "Kod";
            код.MinWidth = 23;
            код.Name = "код";
            код.Visible = true;
            код.VisibleIndex = 0;
            код.Width = 49;
            // 
            // артикул
            // 
            артикул.Caption = "артикул";
            артикул.FieldName = "Articul";
            артикул.MinWidth = 23;
            артикул.Name = "артикул";
            артикул.Visible = true;
            артикул.VisibleIndex = 1;
            артикул.Width = 76;
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
            группа.Caption = "группа";
            группа.FieldName = "grup";
            группа.MinWidth = 23;
            группа.Name = "группа";
            группа.Visible = true;
            группа.VisibleIndex = 3;
            группа.Width = 87;
            // 
            // модель
            // 
            модель.Caption = "модель";
            модель.FieldName = "mod";
            модель.MinWidth = 23;
            модель.Name = "модель";
            модель.Visible = true;
            модель.VisibleIndex = 4;
            модель.Width = 87;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "gridColumn4";
            gridColumn4.FieldName = "_isChecked";
            gridColumn4.MinWidth = 23;
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Width = 87;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup2, layoutControlGroup6 });
            Root.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            Root.Name = "Root";
            columnDefinition1.SizeType = SizeType.Percent;
            columnDefinition1.Width = 40D;
            columnDefinition2.SizeType = SizeType.Percent;
            columnDefinition2.Width = 100D;
            Root.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] { columnDefinition1, columnDefinition2 });
            rowDefinition1.Height = 100D;
            rowDefinition1.SizeType = SizeType.Percent;
            Root.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] { rowDefinition1 });
            Root.Size = new System.Drawing.Size(1763, 911);
            // 
            // layoutControlGroup2
            // 
            toolTipItem1.Text = "Создаёт разделение труда на основе выбранного артикула, сразу заполняя Группу, Модель и Артикул. Открывает окно редактирования, где можно добавить схему разделения";
            superToolTip1.Items.Add(toolTipItem1);
            layoutControlGroup2.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Добавить", true, buttonImageOptions11, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Добавить новое пустое разделение труда", -1, true, null, true, false, true, "btnAdd", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions12, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, false, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Редактировать", true, buttonImageOptions13, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Редактировать выбранное РТ", -1, true, null, true, false, true, "btnEdit", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions14, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, false, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Архив+копия", true, buttonImageOptions15, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Создать копию РТ и отправить базовое РТ в архив", -1, true, null, true, false, true, "btnArch", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions16, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Создать из артикула", true, buttonImageOptions17, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Создать РТ на основе выбранного артикула", -1, true, superToolTip1, true, false, true, "btnArt", -1) });
            layoutControlGroup2.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem7, simpleSeparator1, simpleSeparator2, layoutControlGroup14, emptySpaceItem1, layoutControlItem6, layoutControlItem5, emptySpaceItem2, splitterItem3, layoutControlGroup13 });
            layoutControlGroup2.Location = new System.Drawing.Point(498, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlGroup2.Size = new System.Drawing.Size(1245, 891);
            layoutControlGroup2.Text = "РТ для увязки";
            layoutControlGroup2.CustomButtonClick += layoutControlGroup2_CustomButtonClick;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = loadAllCheckBox;
            layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new System.Drawing.Size(178, 40);
            layoutControlItem7.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            simpleSeparator1.Location = new System.Drawing.Point(0, 674);
            simpleSeparator1.Name = "simpleSeparator1";
            simpleSeparator1.Size = new System.Drawing.Size(1221, 1);
            // 
            // simpleSeparator2
            // 
            simpleSeparator2.Location = new System.Drawing.Point(0, 673);
            simpleSeparator2.Name = "simpleSeparator2";
            simpleSeparator2.Size = new System.Drawing.Size(1221, 1);
            // 
            // layoutControlGroup14
            // 
            layoutControlGroup14.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Привязать", true, buttonImageOptions18, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Отвязать", true, buttonImageOptions19, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup14.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3 });
            layoutControlGroup14.Location = new System.Drawing.Point(0, 40);
            layoutControlGroup14.Name = "layoutControlGroup14";
            layoutControlGroup14.Size = new System.Drawing.Size(1221, 633);
            layoutControlGroup14.Text = " ";
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = gridControl_wdToBind;
            layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(1197, 588);
            layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(178, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(115, 40);
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = ButtonUnboundWd;
            layoutControlItem6.Location = new System.Drawing.Point(379, 0);
            layoutControlItem6.MinSize = new System.Drawing.Size(60, 26);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(80, 40);
            layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = simpleButton1;
            layoutControlItem5.Location = new System.Drawing.Point(293, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(86, 40);
            layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.Location = new System.Drawing.Point(459, 0);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(762, 40);
            // 
            // splitterItem3
            // 
            splitterItem3.Location = new System.Drawing.Point(0, 836);
            splitterItem3.Name = "splitterItem3";
            splitterItem3.Size = new System.Drawing.Size(1221, 10);
            // 
            // layoutControlGroup13
            // 
            layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem11, layoutControlGroup3 });
            layoutControlGroup13.Location = new System.Drawing.Point(0, 675);
            layoutControlGroup13.Name = "layoutControlGroup13";
            layoutControlGroup13.Size = new System.Drawing.Size(1221, 161);
            layoutControlGroup13.Text = "Привязанные артикулы";
            // 
            // layoutControlItem11
            // 
            layoutControlItem11.Control = gridControlNZP;
            layoutControlItem11.Location = new System.Drawing.Point(602, 0);
            layoutControlItem11.Name = "layoutControlItem11";
            layoutControlItem11.Size = new System.Drawing.Size(595, 116);
            layoutControlItem11.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { splitterItem2, layoutControlItem14, tabbedControlGroup1 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Size = new System.Drawing.Size(602, 116);
            layoutControlGroup3.Text = "Схема РТ";
            // 
            // splitterItem2
            // 
            splitterItem2.Location = new System.Drawing.Point(306, 0);
            splitterItem2.Name = "splitterItem2";
            splitterItem2.Size = new System.Drawing.Size(10, 71);
            // 
            // layoutControlItem14
            // 
            layoutControlItem14.Control = pictureBox2;
            layoutControlItem14.Location = new System.Drawing.Point(316, 0);
            layoutControlItem14.Name = "layoutControlItem14";
            layoutControlItem14.Size = new System.Drawing.Size(262, 71);
            layoutControlItem14.TextVisible = false;
            // 
            // tabbedControlGroup1
            // 
            tabbedControlGroup1.Location = new System.Drawing.Point(0, 0);
            tabbedControlGroup1.Name = "tabbedControlGroup1";
            tabbedControlGroup1.SelectedTabPage = layoutControlGroup1;
            tabbedControlGroup1.Size = new System.Drawing.Size(306, 71);
            tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlGroup4, layoutControlGroup5 });
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.CustomizationFormText = "Пошив";
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new System.Drawing.Size(282, 24);
            layoutControlGroup1.Text = "Пошив";
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = customGridControl3;
            layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(282, 24);
            layoutControlItem4.Text = "Пошив";
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem8 });
            layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Size = new System.Drawing.Size(282, 24);
            layoutControlGroup4.Text = "Раскрой";
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = customGridControl2;
            layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.Size = new System.Drawing.Size(282, 24);
            layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Size = new System.Drawing.Size(282, 24);
            layoutControlGroup5.Text = "Комплектовка";
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customGridControl1;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(282, 24);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            layoutControlGroup6.CaptionImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup6.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Создать из артикула", true, buttonImageOptions20, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            layoutControlGroup6.CustomizationFormText = " Артикулы для увязки";
            layoutControlGroup6.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, emptySpaceItem5, layoutControlItem13, layoutControlItem9, simpleSeparator3 });
            layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup6.Name = "layoutControlGroup6";
            layoutControlGroup6.Size = new System.Drawing.Size(498, 891);
            layoutControlGroup6.Text = "  Артикулы для увязки";
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = gridControl_unboundArts;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(474, 548);
            layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.Location = new System.Drawing.Point(0, 549);
            emptySpaceItem5.Name = "emptySpaceItem5";
            emptySpaceItem5.Size = new System.Drawing.Size(474, 50);
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = gridControl_binded;
            layoutControlItem13.Location = new System.Drawing.Point(0, 623);
            layoutControlItem13.Name = "layoutControlItem13";
            layoutControlItem13.Size = new System.Drawing.Size(474, 223);
            layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = customLabel2;
            layoutControlItem9.Location = new System.Drawing.Point(0, 599);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new System.Drawing.Size(474, 24);
            layoutControlItem9.TextVisible = false;
            // 
            // simpleSeparator3
            // 
            simpleSeparator3.Location = new System.Drawing.Point(0, 548);
            simpleSeparator3.Name = "simpleSeparator3";
            simpleSeparator3.Size = new System.Drawing.Size(474, 1);
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
            xtraTabPage3.Controls.Add(splitContainerControl2);
            xtraTabPage3.Margin = new Padding(4, 3, 4, 3);
            xtraTabPage3.Name = "xtraTabPage3";
            xtraTabPage3.Size = new System.Drawing.Size(1767, 915);
            xtraTabPage3.Text = "Предварительный архив";
            // 
            // splitContainerControl2
            // 
            splitContainerControl2.Dock = DockStyle.Fill;
            splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            splitContainerControl2.Margin = new Padding(4, 3, 4, 3);
            splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            splitContainerControl2.Panel1.Controls.Add(customButton2);
            splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            splitContainerControl2.Panel2.Controls.Add(flyoutPanel1);
            splitContainerControl2.Panel2.Controls.Add(gridControlPreArch);
            splitContainerControl2.Panel2.Text = "Panel2";
            splitContainerControl2.Size = new System.Drawing.Size(1767, 915);
            splitContainerControl2.SplitterPosition = 142;
            splitContainerControl2.TabIndex = 2;
            // 
            // customButton2
            // 
            customButton2.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            customButton2.FlatAppearance.BorderSize = 0;
            customButton2.FlatStyle = FlatStyle.Flat;
            customButton2.Font = new System.Drawing.Font("Arial", 12F);
            customButton2.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            customButton2.Location = new System.Drawing.Point(12, 22);
            customButton2.Margin = new Padding(4, 3, 4, 3);
            customButton2.Name = "customButton2";
            customButton2.Size = new System.Drawing.Size(118, 35);
            customButton2.TabIndex = 1;
            customButton2.Text = "В архив";
            customButton2.UseVisualStyleBackColor = false;
            customButton2.Click += customButton2_Click;
            // 
            // flyoutPanel1
            // 
            flyoutPanel1.Controls.Add(flyoutPanelControl1);
            flyoutPanel1.Location = new System.Drawing.Point(807, 252);
            flyoutPanel1.Margin = new Padding(4, 3, 4, 3);
            flyoutPanel1.Name = "flyoutPanel1";
            flyoutPanel1.OptionsButtonPanel.ButtonPanelHeight = 35;
            flyoutPanel1.Size = new System.Drawing.Size(444, 293);
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
            flyoutPanelControl1.Size = new System.Drawing.Size(444, 293);
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
            gridControlPreArch.Dock = DockStyle.Left;
            gridControlPreArch.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlPreArch.Font = new System.Drawing.Font("Arial", 10F);
            gridControlPreArch.Location = new System.Drawing.Point(0, 0);
            gridControlPreArch.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControlPreArch.MainView = gridViewPreArch;
            gridControlPreArch.Margin = new Padding(4, 3, 4, 3);
            gridControlPreArch.Name = "gridControlPreArch";
            gridControlPreArch.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemCheckEdit5 });
            gridControlPreArch.Size = new System.Drawing.Size(705, 915);
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
            // imageCollection1
            // 
            imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // TeamWork
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1771, 965);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlKontTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView4).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl4).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView5).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaskrTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRaskrTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaszTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditProizv).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditOb).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditPodr).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl7).EndInit();
            panelControl7.ResumeLayout(false);
            panelControl7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ANNgridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit3).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit2).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textEditMod.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditArt.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditSec.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditCreate.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem19).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem20).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem22).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem23).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem24).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem26).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem32).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem34).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem35).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem33).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem29).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator4).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator5).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem28).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem27).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem30).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem31).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem25).EndInit();
            xtraTabPageArticles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)xtraTabControl2).EndInit();
            xtraTabControl2.ResumeLayout(false);
            xtraTabPageWorkDivisions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customGridControl2).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl_binded).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView_binded).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView8).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlNZP).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewNZP).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl3).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView6).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup14).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).EndInit();
            xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel1).EndInit();
            splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel2).EndInit();
            splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2).EndInit();
            splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)flyoutPanel1).EndInit();
            flyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)flyoutPanelControl1).EndInit();
            flyoutPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlPreArch).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPreArch).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit5).EndInit();
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
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage TabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageArticles;
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
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private CustomButton customButton2;
        private CustomGridControl gridControlPreArch;
        private BindingSource sparticulBindingSource;
        private BindingSource sparticulBindingSource1;
        private ToolStrip fillBy1ToolStrip;
        private ToolStripButton fillBy1ToolStripButton;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPreArch;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private BindingSource normraszBindingSource1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageWorkDivisions;
        private BindingSource artnormnBindingSource2;
        RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private PanelControl panelControl2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private BindingSource artnormnBindingSource;
        private BindingSource artnormnBindingSource1;
        private PanelControl panelControl7;
        private CustomLabel label6;
        private CustomLabel label7;
        private CustomButton ButtonApprovement;
        private CustomButton ButtonEditWd;
        private CustomButton ButtonArchAndCopyWd;
        private CustomButton ButtonPreliminaryWd;
        private CustomButton ButtonCopyWd;
        private Panel panel5;
        private CustomCheckBox SortBox;
        private CustomCheckBox archiveCheckBox;
        private CustomCheckBox actualCheckBox;
        private CustomCheckBox preliminaryCheckBox;
        private DevExpress.Utils.FlyoutPanel flyoutPanel1;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private CustomCancelButton customCancelButton1;
        private CustomComboBox customComboBox1;
        private CustomButton customButton1;
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
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn colannId1;
        private DevExpress.XtraGrid.Columns.GridColumn coln3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private CustomCheckBox loadAllCheckBox;
        private CustomSimpleButton ButtonUnboundWd;
        private SimpleButton simpleButton1;
        private CustomGridControl gridControl_unboundArts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_unboundArts;
        private DevExpress.XtraGrid.Columns.GridColumn код;
        private DevExpress.XtraGrid.Columns.GridColumn артикул;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn группа;
        private DevExpress.XtraGrid.Columns.GridColumn модель;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView8;
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
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd2;
        private DevExpress.XtraGrid.Columns.GridColumn coltext2;
        private DevExpress.XtraGrid.Columns.GridColumn colsek3;
        private DevExpress.XtraGrid.Columns.GridColumn colannId5;
        private CustomGridControl gridControlRaszTW;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
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
        private CustomLabel customLabel8;
        private CustomLabel label8;
        private RichTextBox commentRichTextBox;
        private RichTextBox RecoRichTextBox;
        private CustomSimpleButton PrintButton;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private CustomLabel customLabel2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private CustomGridControl customGridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private CustomGridControl customGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private CustomTextBox buffer;
        private CustomSimpleButton customSimpleButton1;

        private async void DuplicateWorkDivision_Click_Internal_Wrapper(object sender, EventArgs e)
        {
            await DuplicateWorkDivision_Click_Internal(sender, e);
        }
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private RepositoryItemButtonEdit repositoryItemButtonEdit3;
        private RichTextBox constructorTextBox;
        private RichTextBox designerTextBox;
        private CustomGridControl customGridControl4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
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
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
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
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator4;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator5;
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
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn DisplayNumber;
    }
}
