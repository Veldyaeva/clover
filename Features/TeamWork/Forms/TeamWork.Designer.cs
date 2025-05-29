using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamWork));
            DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions3 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions4 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions5 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions6 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions7 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            repositoryItemCheckEdit1 = new RepositoryItemCheckEdit();
            repositoryItemButtonEdit2 = new RepositoryItemButtonEdit();
            xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            TabPage1 = new DevExpress.XtraTab.XtraTabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBox1 = new PictureBox();
            gridControlKontTW = new CustomGridControl();
            gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colkod_o2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colrazryd2 = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext2 = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek3 = new DevExpress.XtraGrid.Columns.GridColumn();
            colannId5 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControlRaszTW = new CustomGridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            ANNgridControl = new CustomGridControl();
            ANNgridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            colgroup = new DevExpress.XtraGrid.Columns.GridColumn();
            colarticul = new DevExpress.XtraGrid.Columns.GridColumn();
            colmod = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_vyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            coldateCreate = new DevExpress.XtraGrid.Columns.GridColumn();
            coldateUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek_shv = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit2 = new RepositoryItemCheckEdit();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            gridControlRaskrTW = new CustomGridControl();
            gridViewRaskrTW = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            customLabel8 = new CustomLabel();
            label8 = new CustomLabel();
            commentRichTextBox = new RichTextBox();
            RecoRichTextBox = new RichTextBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            customButton9 = new CustomButton();
            ButtonEditWd = new CustomButton();
            customButton8 = new CustomButton();
            buffer = new CustomTextBox();
            ButtonArchAndCopyWd = new CustomButton();
            ButtonPreliminaryWd = new CustomButton();
            ButtonCopyWd = new CustomButton();
            PrintButton = new CustomSimpleButton();
            panelControl7 = new PanelControl();
            constructorTextBox = new CustomTextBox();
            designerTextBox = new CustomTextBox();
            label6 = new CustomLabel();
            label7 = new CustomLabel();
            ButtonApprovement = new CustomButton();
            panel5 = new Panel();
            SortBox = new CustomCheckBox();
            archiveCheckBox = new CustomCheckBox();
            actualCheckBox = new CustomCheckBox();
            preliminaryCheckBox = new CustomCheckBox();
            panel2 = new Panel();
            customLabel3 = new CustomLabel();
            searchControl1 = new SearchControl();
            customButton12 = new CustomButton();
            filterTextBox1 = new CustomTextBox();
            model = new RadioButton();
            articul = new RadioButton();
            kode = new RadioButton();
            group = new RadioButton();
            xtraTabPageArticles = new DevExpress.XtraTab.XtraTabPage();
            xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            xtraTabPageWorkDivisions = new DevExpress.XtraTab.XtraTabPage();
            panelControl2 = new PanelControl();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            customLabel2 = new CustomLabel();
            customLabel1 = new CustomLabel();
            pictureBox2 = new PictureBox();
            gridControl_binded = new CustomGridControl();
            gridView_binded = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridView8 = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridControlNZP = new CustomGridControl();
            gridViewNZP = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colannId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            coln3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridControl_wdToBind = new CustomGridControl();
            gridView_wdToBind = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            gridView_unboundArts = new DevExpress.XtraGrid.Views.Grid.GridView();
            код = new DevExpress.XtraGrid.Columns.GridColumn();
            артикул = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            группа = new DevExpress.XtraGrid.Columns.GridColumn();
            модель = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
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
            gridViewPreArch = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlKontTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaszTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditProizv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditOb).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditPodr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaskrTW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRaskrTW).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl7).BeginInit();
            panelControl7.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            xtraTabPageArticles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl2).BeginInit();
            xtraTabControl2.SuspendLayout();
            xtraTabPageWorkDivisions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
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
            TabPage1.Controls.Add(tableLayoutPanel3);
            TabPage1.Margin = new Padding(4, 3, 4, 3);
            TabPage1.Name = "TabPage1";
            TabPage1.Size = new System.Drawing.Size(1769, 940);
            TabPage1.Text = "1. Разделения труда";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 244F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.77276F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 1, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new System.Drawing.Size(1769, 940);
            tableLayoutPanel3.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 328F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 363F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 827F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
            tableLayoutPanel1.Controls.Add(pictureBox1, 2, 3);
            tableLayoutPanel1.Controls.Add(gridControlKontTW, 2, 2);
            tableLayoutPanel1.Controls.Add(gridControlRaszTW, 2, 0);
            tableLayoutPanel1.Controls.Add(ANNgridControl, 0, 0);
            tableLayoutPanel1.Controls.Add(gridControlRaskrTW, 2, 1);
            tableLayoutPanel1.Controls.Add(customLabel8, 1, 3);
            tableLayoutPanel1.Controls.Add(label8, 0, 3);
            tableLayoutPanel1.Controls.Add(commentRichTextBox, 0, 4);
            tableLayoutPanel1.Controls.Add(RecoRichTextBox, 1, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(248, 3);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 128F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 108F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1517, 934);
            tableLayoutPanel1.TabIndex = 24;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new System.Drawing.Point(695, 667);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            tableLayoutPanel1.SetRowSpan(pictureBox1, 3);
            pictureBox1.Size = new System.Drawing.Size(819, 264);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // gridControlKontTW
            // 
            gridControlKontTW.Dock = DockStyle.Fill;
            gridControlKontTW.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlKontTW.Font = new System.Drawing.Font("Arial", 10F);
            gridControlKontTW.Location = new System.Drawing.Point(695, 559);
            gridControlKontTW.MainView = gridView4;
            gridControlKontTW.Margin = new Padding(4, 3, 4, 3);
            gridControlKontTW.Name = "gridControlKontTW";
            gridControlKontTW.Size = new System.Drawing.Size(819, 102);
            gridControlKontTW.TabIndex = 8;
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
            gridView4.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
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
            // gridControlRaszTW
            // 
            gridControlRaszTW.Dock = DockStyle.Fill;
            gridControlRaszTW.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlRaszTW.Font = new System.Drawing.Font("Arial", 10F);
            gridControlRaszTW.Location = new System.Drawing.Point(695, 3);
            gridControlRaszTW.MainView = gridView1;
            gridControlRaszTW.Margin = new Padding(4, 3, 4, 3);
            gridControlRaszTW.Name = "gridControlRaszTW";
            gridControlRaszTW.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemLookUpEditProizv, repositoryItemLookUpEditOb, repositoryItemLookUpEditPodr });
            gridControlRaszTW.Size = new System.Drawing.Size(819, 422);
            gridControlRaszTW.TabIndex = 6;
            gridControlRaszTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { coln, coln1, colrazryd, coltext, colsek1, gridColumn30, gridColumn27, colobor, gridColumn26, gridColumn3, colkod_o, colannId3 });
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
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(coln, DevExpress.Data.ColumnSortOrder.Ascending), new DevExpress.XtraGrid.Columns.GridColumnSortInfo(coln1, DevExpress.Data.ColumnSortOrder.Ascending) });
            // 
            // coln
            // 
            coln.Caption = "№ оп.";
            coln.FieldName = "N";
            coln.MinWidth = 23;
            coln.Name = "coln";
            coln.Visible = true;
            coln.VisibleIndex = 0;
            coln.Width = 55;
            // 
            // coln1
            // 
            coln1.Caption = "№ п/оп.";
            coln1.FieldName = "N1";
            coln1.MinWidth = 23;
            coln1.Name = "coln1";
            coln1.Visible = true;
            coln1.VisibleIndex = 1;
            coln1.Width = 45;
            // 
            // colrazryd
            // 
            colrazryd.Caption = "разряд";
            colrazryd.FieldName = "razryd";
            colrazryd.MinWidth = 23;
            colrazryd.Name = "colrazryd";
            colrazryd.Visible = true;
            colrazryd.VisibleIndex = 2;
            colrazryd.Width = 62;
            // 
            // coltext
            // 
            coltext.Caption = "наименование операции пошива";
            coltext.FieldName = "Text";
            coltext.MinWidth = 23;
            coltext.Name = "coltext";
            coltext.Visible = true;
            coltext.VisibleIndex = 3;
            coltext.Width = 261;
            // 
            // colsek1
            // 
            colsek1.Caption = "сек.";
            colsek1.FieldName = "Sek";
            colsek1.MinWidth = 23;
            colsek1.Name = "colsek1";
            colsek1.Visible = true;
            colsek1.VisibleIndex = 4;
            colsek1.Width = 49;
            // 
            // gridColumn30
            // 
            gridColumn30.Caption = "спец-ть";
            gridColumn30.FieldName = "Spec";
            gridColumn30.MinWidth = 23;
            gridColumn30.Name = "gridColumn30";
            gridColumn30.Visible = true;
            gridColumn30.VisibleIndex = 5;
            gridColumn30.Width = 87;
            // 
            // gridColumn27
            // 
            gridColumn27.Caption = "производство";
            gridColumn27.FieldName = "TextProizv";
            gridColumn27.MinWidth = 23;
            gridColumn27.Name = "gridColumn27";
            gridColumn27.Visible = true;
            gridColumn27.VisibleIndex = 6;
            gridColumn27.Width = 64;
            // 
            // colobor
            // 
            colobor.Caption = "оборудование";
            colobor.FieldName = "TextOb";
            colobor.MinWidth = 23;
            colobor.Name = "colobor";
            colobor.Visible = true;
            colobor.VisibleIndex = 9;
            colobor.Width = 68;
            // 
            // gridColumn26
            // 
            gridColumn26.Caption = "вяз. подр.";
            gridColumn26.FieldName = "TextVyaz";
            gridColumn26.MinWidth = 23;
            gridColumn26.Name = "gridColumn26";
            gridColumn26.Visible = true;
            gridColumn26.VisibleIndex = 7;
            gridColumn26.Width = 59;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "Код";
            gridColumn3.FieldName = "Kod";
            gridColumn3.MinWidth = 23;
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 8;
            gridColumn3.Width = 43;
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
            // ANNgridControl
            // 
            tableLayoutPanel1.SetColumnSpan(ANNgridControl, 2);
            ANNgridControl.Dock = DockStyle.Fill;
            ANNgridControl.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.Transparent;
            ANNgridControl.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Transparent;
            ANNgridControl.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            ANNgridControl.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
            ANNgridControl.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            ANNgridControl.Font = new System.Drawing.Font("Arial", 10F);
            ANNgridControl.Location = new System.Drawing.Point(4, 3);
            ANNgridControl.MainView = ANNgridView;
            ANNgridControl.Margin = new Padding(4, 3, 4, 3);
            ANNgridControl.Name = "ANNgridControl";
            ANNgridControl.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemButtonEdit1, repositoryItemCheckEdit2 });
            tableLayoutPanel1.SetRowSpan(ANNgridControl, 3);
            ANNgridControl.Size = new System.Drawing.Size(683, 658);
            ANNgridControl.TabIndex = 7;
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
            ANNgridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colgroup, colarticul, colmod, colsek, colsek_vyaz, coldateCreate, coldateUpdate, colsek_shv, gridColumn35, gridColumn5, colsek_vyazo, colsek_vyaz5, colsek_vyaz7, colsek_vyaz12, colsek_vyaz10, colsek_vyaz6, colsek_kr, colslogn, colkomment, colReco, coldiz, colconstr, colannID });
            ANNgridView.CustomizationFormBounds = new System.Drawing.Rectangle(688, 702, 308, 314);
            ANNgridView.DetailHeight = 404;
            ANNgridView.GridControl = ANNgridControl;
            ANNgridView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            ANNgridView.Name = "ANNgridView";
            ANNgridView.OptionsBehavior.Editable = false;
            ANNgridView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            ANNgridView.OptionsEditForm.PopupEditFormWidth = 933;
            ANNgridView.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            ANNgridView.OptionsSelection.MultiSelect = true;
            ANNgridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            ANNgridView.OptionsView.ColumnAutoWidth = false;
            ANNgridView.OptionsView.ShowGroupPanel = false;
            ANNgridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(colarticul, DevExpress.Data.ColumnSortOrder.Ascending) });
            ANNgridView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            ANNgridView.FocusedRowChanged += ANNgridView_FocusedRowChanged;
            // 
            // colgroup
            // 
            colgroup.Caption = "группа";
            colgroup.FieldName = "grup";
            colgroup.MinWidth = 23;
            colgroup.Name = "colgroup";
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
            colarticul.Visible = true;
            colarticul.VisibleIndex = 1;
            colarticul.Width = 99;
            // 
            // colmod
            // 
            colmod.Caption = "модель";
            colmod.FieldName = "mod";
            colmod.MinWidth = 23;
            colmod.Name = "colmod";
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
            coldateCreate.Visible = true;
            coldateCreate.VisibleIndex = 5;
            coldateCreate.Width = 87;
            // 
            // coldateUpdate
            // 
            coldateUpdate.Caption = "обновление";
            coldateUpdate.FieldName = "dateUpdate";
            coldateUpdate.MinWidth = 23;
            coldateUpdate.Name = "coldateUpdate";
            coldateUpdate.Visible = true;
            coldateUpdate.VisibleIndex = 6;
            coldateUpdate.Width = 87;
            // 
            // colsek_shv
            // 
            colsek_shv.Caption = "сек. шв.";
            colsek_shv.FieldName = "SekShv";
            colsek_shv.MinWidth = 23;
            colsek_shv.Name = "colsek_shv";
            colsek_shv.Visible = true;
            colsek_shv.VisibleIndex = 7;
            colsek_shv.Width = 62;
            // 
            // gridColumn35
            // 
            gridColumn35.Caption = "предв. архив";
            gridColumn35.ColumnEdit = repositoryItemCheckEdit2;
            gridColumn35.FieldName = "preArch";
            gridColumn35.MinWidth = 23;
            gridColumn35.Name = "gridColumn35";
            gridColumn35.Visible = true;
            gridColumn35.VisibleIndex = 10;
            gridColumn35.Width = 87;
            // 
            // repositoryItemCheckEdit2
            // 
            repositoryItemCheckEdit2.AutoHeight = false;
            repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "статус";
            gridColumn5.FieldName = "StatusText";
            gridColumn5.MinWidth = 23;
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 9;
            gridColumn5.Width = 87;
            // 
            // colsek_vyazo
            // 
            colsek_vyazo.Caption = "сек.отп.";
            colsek_vyazo.FieldName = "SekVyazo";
            colsek_vyazo.MinWidth = 23;
            colsek_vyazo.Name = "colsek_vyazo";
            colsek_vyazo.Visible = true;
            colsek_vyazo.VisibleIndex = 8;
            colsek_vyazo.Width = 87;
            // 
            // colsek_vyaz5
            // 
            colsek_vyaz5.Caption = "класс5";
            colsek_vyaz5.FieldName = "SekVyaz5";
            colsek_vyaz5.MinWidth = 23;
            colsek_vyaz5.Name = "colsek_vyaz5";
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
            colkomment.Width = 155;
            // 
            // colReco
            // 
            colReco.Caption = "рекомендации";
            colReco.FieldName = "annRecommendation";
            colReco.MinWidth = 23;
            colReco.Name = "colReco";
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
            coldiz.Width = 87;
            // 
            // colconstr
            // 
            colconstr.Caption = "конструктор";
            colconstr.FieldName = "Constr";
            colconstr.MinWidth = 23;
            colconstr.Name = "colconstr";
            colconstr.Width = 87;
            // 
            // colannID
            // 
            colannID.FieldName = "AnnID";
            colannID.MinWidth = 23;
            colannID.Name = "colannID";
            colannID.Visible = true;
            colannID.VisibleIndex = 19;
            colannID.Width = 87;
            // 
            // repositoryItemButtonEdit1
            // 
            repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // gridControlRaskrTW
            // 
            gridControlRaskrTW.Dock = DockStyle.Fill;
            gridControlRaskrTW.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControlRaskrTW.Font = new System.Drawing.Font("Arial", 10F);
            gridControlRaskrTW.Location = new System.Drawing.Point(695, 431);
            gridControlRaskrTW.MainView = gridViewRaskrTW;
            gridControlRaskrTW.Margin = new Padding(4, 3, 4, 3);
            gridControlRaskrTW.Name = "gridControlRaskrTW";
            gridControlRaskrTW.Size = new System.Drawing.Size(819, 122);
            gridControlRaskrTW.TabIndex = 7;
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
            gridViewRaskrTW.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
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
            colkod_o1.Width = 58;
            // 
            // gridColumn18
            // 
            gridColumn18.Caption = "№ п/оп.";
            gridColumn18.FieldName = "N1";
            gridColumn18.MinWidth = 23;
            gridColumn18.Name = "gridColumn18";
            gridColumn18.Visible = true;
            gridColumn18.VisibleIndex = 1;
            gridColumn18.Width = 43;
            // 
            // colrazryd1
            // 
            colrazryd1.Caption = "разряд";
            colrazryd1.FieldName = "razryd";
            colrazryd1.MinWidth = 23;
            colrazryd1.Name = "colrazryd1";
            colrazryd1.Visible = true;
            colrazryd1.VisibleIndex = 2;
            colrazryd1.Width = 62;
            // 
            // coltext1
            // 
            coltext1.Caption = "наименование операции раскроя";
            coltext1.FieldName = "Text";
            coltext1.MinWidth = 23;
            coltext1.Name = "coltext1";
            coltext1.Visible = true;
            coltext1.VisibleIndex = 3;
            coltext1.Width = 355;
            // 
            // colsek2
            // 
            colsek2.Caption = "сек.";
            colsek2.FieldName = "Sek";
            colsek2.MinWidth = 23;
            colsek2.Name = "colsek2";
            colsek2.Visible = true;
            colsek2.VisibleIndex = 4;
            colsek2.Width = 71;
            // 
            // gridColumn34
            // 
            gridColumn34.Caption = "специальность";
            gridColumn34.FieldName = "Spec";
            gridColumn34.MinWidth = 23;
            gridColumn34.Name = "gridColumn34";
            gridColumn34.Visible = true;
            gridColumn34.VisibleIndex = 7;
            gridColumn34.Width = 87;
            // 
            // gridColumn33
            // 
            gridColumn33.Caption = "оборудование";
            gridColumn33.FieldName = "Obor";
            gridColumn33.MinWidth = 23;
            gridColumn33.Name = "gridColumn33";
            gridColumn33.Visible = true;
            gridColumn33.VisibleIndex = 8;
            gridColumn33.Width = 87;
            // 
            // gridColumn32
            // 
            gridColumn32.Caption = "код оп.";
            gridColumn32.FieldName = "kod_o";
            gridColumn32.MinWidth = 23;
            gridColumn32.Name = "gridColumn32";
            gridColumn32.Visible = true;
            gridColumn32.VisibleIndex = 6;
            gridColumn32.Width = 87;
            // 
            // gridColumn31
            // 
            gridColumn31.Caption = "код из.";
            gridColumn31.FieldName = "Kod";
            gridColumn31.MinWidth = 23;
            gridColumn31.Name = "gridColumn31";
            gridColumn31.Visible = true;
            gridColumn31.VisibleIndex = 5;
            gridColumn31.Width = 87;
            // 
            // colannId4
            // 
            colannId4.FieldName = "annId";
            colannId4.MinWidth = 23;
            colannId4.Name = "colannId4";
            colannId4.Width = 76;
            // 
            // customLabel8
            // 
            customLabel8.AutoSize = true;
            customLabel8.Font = new System.Drawing.Font("Arial", 10F);
            customLabel8.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customLabel8.Location = new System.Drawing.Point(332, 664);
            customLabel8.Margin = new Padding(4, 0, 4, 0);
            customLabel8.Name = "customLabel8";
            customLabel8.Size = new System.Drawing.Size(231, 16);
            customLabel8.TabIndex = 9;
            customLabel8.Text = "Рекомендации для планирования";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Font = new System.Drawing.Font("Arial", 10F);
            label8.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            label8.Location = new System.Drawing.Point(4, 664);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(149, 16);
            label8.TabIndex = 2;
            label8.Text = "Особенности модели";
            // 
            // commentRichTextBox
            // 
            commentRichTextBox.Dock = DockStyle.Fill;
            commentRichTextBox.Enabled = false;
            commentRichTextBox.Location = new System.Drawing.Point(4, 699);
            commentRichTextBox.Margin = new Padding(4, 3, 4, 3);
            commentRichTextBox.Name = "commentRichTextBox";
            tableLayoutPanel1.SetRowSpan(commentRichTextBox, 2);
            commentRichTextBox.Size = new System.Drawing.Size(320, 232);
            commentRichTextBox.TabIndex = 5;
            commentRichTextBox.Text = "";
            // 
            // RecoRichTextBox
            // 
            RecoRichTextBox.Dock = DockStyle.Fill;
            RecoRichTextBox.Location = new System.Drawing.Point(332, 699);
            RecoRichTextBox.Margin = new Padding(4, 3, 4, 3);
            RecoRichTextBox.Name = "RecoRichTextBox";
            tableLayoutPanel1.SetRowSpan(RecoRichTextBox, 2);
            RecoRichTextBox.Size = new System.Drawing.Size(355, 232);
            RecoRichTextBox.TabIndex = 10;
            RecoRichTextBox.Text = "";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel4, 0, 2);
            tableLayoutPanel5.Controls.Add(panel5, 0, 1);
            tableLayoutPanel5.Controls.Add(panel2, 0, 0);
            tableLayoutPanel5.Location = new System.Drawing.Point(4, 3);
            tableLayoutPanel5.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 16.45885F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 16.70823F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 58.0540543F));
            tableLayoutPanel5.Size = new System.Drawing.Size(233, 925);
            tableLayoutPanel5.TabIndex = 20;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(customButton9, 0, 0);
            tableLayoutPanel4.Controls.Add(ButtonEditWd, 0, 2);
            tableLayoutPanel4.Controls.Add(customButton8, 0, 1);
            tableLayoutPanel4.Controls.Add(buffer, 0, 8);
            tableLayoutPanel4.Controls.Add(ButtonArchAndCopyWd, 0, 3);
            tableLayoutPanel4.Controls.Add(ButtonPreliminaryWd, 0, 4);
            tableLayoutPanel4.Controls.Add(ButtonCopyWd, 0, 7);
            tableLayoutPanel4.Controls.Add(PrintButton, 0, 5);
            tableLayoutPanel4.Controls.Add(panelControl7, 0, 6);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new System.Drawing.Point(4, 338);
            tableLayoutPanel4.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 9;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 219F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanel4.Size = new System.Drawing.Size(225, 584);
            tableLayoutPanel4.TabIndex = 21;
            // 
            // customButton9
            // 
            customButton9.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            customButton9.FlatAppearance.BorderSize = 0;
            customButton9.FlatStyle = FlatStyle.Flat;
            customButton9.Font = new System.Drawing.Font("Arial", 12F);
            customButton9.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            customButton9.Location = new System.Drawing.Point(4, 3);
            customButton9.Margin = new Padding(4, 3, 4, 3);
            customButton9.Name = "customButton9";
            customButton9.Size = new System.Drawing.Size(172, 1);
            customButton9.TabIndex = 13;
            customButton9.Text = "конф. карта";
            customButton9.UseVisualStyleBackColor = false;
            customButton9.Visible = false;
            // 
            // ButtonEditWd
            // 
            ButtonEditWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonEditWd.FlatAppearance.BorderSize = 0;
            ButtonEditWd.FlatStyle = FlatStyle.Flat;
            ButtonEditWd.Font = new System.Drawing.Font("Arial", 12F);
            ButtonEditWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonEditWd.Location = new System.Drawing.Point(4, 7);
            ButtonEditWd.Margin = new Padding(4, 3, 4, 3);
            ButtonEditWd.Name = "ButtonEditWd";
            ButtonEditWd.Size = new System.Drawing.Size(217, 35);
            ButtonEditWd.TabIndex = 4;
            ButtonEditWd.Text = "редактировать РТ";
            ButtonEditWd.UseVisualStyleBackColor = false;
            ButtonEditWd.Click += ButtonEditWd_Click;
            // 
            // customButton8
            // 
            customButton8.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            customButton8.FlatAppearance.BorderSize = 0;
            customButton8.FlatStyle = FlatStyle.Flat;
            customButton8.Font = new System.Drawing.Font("Arial", 12F);
            customButton8.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            customButton8.Location = new System.Drawing.Point(4, 5);
            customButton8.Margin = new Padding(4, 3, 4, 3);
            customButton8.Name = "customButton8";
            customButton8.Size = new System.Drawing.Size(172, 1);
            customButton8.TabIndex = 12;
            customButton8.Text = "печать РТ";
            customButton8.UseVisualStyleBackColor = false;
            customButton8.Visible = false;
            // 
            // buffer
            // 
            buffer.BackColor = System.Drawing.Color.FromArgb(255, 245, 230);
            buffer.Enabled = false;
            buffer.Font = new System.Drawing.Font("Arial", 10F);
            buffer.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            buffer.Location = new System.Drawing.Point(4, 444);
            buffer.Margin = new Padding(4, 3, 4, 3);
            buffer.Multiline = true;
            buffer.Name = "buffer";
            buffer.Size = new System.Drawing.Size(217, 118);
            buffer.TabIndex = 20;
            // 
            // ButtonArchAndCopyWd
            // 
            ButtonArchAndCopyWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonArchAndCopyWd.FlatAppearance.BorderSize = 0;
            ButtonArchAndCopyWd.FlatStyle = FlatStyle.Flat;
            ButtonArchAndCopyWd.Font = new System.Drawing.Font("Arial", 12F);
            ButtonArchAndCopyWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonArchAndCopyWd.Location = new System.Drawing.Point(4, 48);
            ButtonArchAndCopyWd.Margin = new Padding(4, 3, 4, 3);
            ButtonArchAndCopyWd.Name = "ButtonArchAndCopyWd";
            ButtonArchAndCopyWd.Size = new System.Drawing.Size(217, 35);
            ButtonArchAndCopyWd.TabIndex = 14;
            ButtonArchAndCopyWd.Text = "архив+копия";
            ButtonArchAndCopyWd.UseVisualStyleBackColor = false;
            ButtonArchAndCopyWd.Click += ButtonArchAndCopyWd_Click;
            // 
            // ButtonPreliminaryWd
            // 
            ButtonPreliminaryWd.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            ButtonPreliminaryWd.FlatAppearance.BorderSize = 0;
            ButtonPreliminaryWd.FlatStyle = FlatStyle.Flat;
            ButtonPreliminaryWd.Font = new System.Drawing.Font("Arial", 10F);
            ButtonPreliminaryWd.ForeColor = System.Drawing.Color.FromArgb(106, 90, 205);
            ButtonPreliminaryWd.Location = new System.Drawing.Point(4, 89);
            ButtonPreliminaryWd.Margin = new Padding(4, 3, 4, 3);
            ButtonPreliminaryWd.Name = "ButtonPreliminaryWd";
            ButtonPreliminaryWd.Size = new System.Drawing.Size(217, 48);
            ButtonPreliminaryWd.TabIndex = 15;
            ButtonPreliminaryWd.Text = "создать предварительное ";
            ButtonPreliminaryWd.UseVisualStyleBackColor = false;
            ButtonPreliminaryWd.Click += ButtonPreliminaryWd_Click;
            // 
            // ButtonCopyWd
            // 
            ButtonCopyWd.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            ButtonCopyWd.Font = new System.Drawing.Font("Arial", 10F);
            ButtonCopyWd.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            ButtonCopyWd.Location = new System.Drawing.Point(4, 400);
            ButtonCopyWd.Margin = new Padding(4, 3, 4, 3);
            ButtonCopyWd.Name = "ButtonCopyWd";
            ButtonCopyWd.Size = new System.Drawing.Size(217, 38);
            ButtonCopyWd.TabIndex = 19;
            ButtonCopyWd.Text = "копировать РТ";
            ButtonCopyWd.UseVisualStyleBackColor = false;
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
            PrintButton.Location = new System.Drawing.Point(4, 143);
            PrintButton.Margin = new Padding(4, 3, 4, 3);
            PrintButton.Name = "PrintButton";
            PrintButton.Size = new System.Drawing.Size(217, 32);
            PrintButton.TabIndex = 14;
            PrintButton.Text = "печать";
            PrintButton.Click += customSimpleButton1_Click;
            // 
            // panelControl7
            // 
            panelControl7.Controls.Add(constructorTextBox);
            panelControl7.Controls.Add(designerTextBox);
            panelControl7.Controls.Add(label6);
            panelControl7.Controls.Add(label7);
            panelControl7.Controls.Add(ButtonApprovement);
            panelControl7.Location = new System.Drawing.Point(4, 181);
            panelControl7.Margin = new Padding(4, 3, 4, 3);
            panelControl7.Name = "panelControl7";
            panelControl7.Size = new System.Drawing.Size(217, 213);
            panelControl7.TabIndex = 22;
            // 
            // constructorTextBox
            // 
            constructorTextBox.BackColor = System.Drawing.Color.FromArgb(255, 245, 230);
            constructorTextBox.Enabled = false;
            constructorTextBox.Font = new System.Drawing.Font("Arial", 10F);
            constructorTextBox.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            constructorTextBox.Location = new System.Drawing.Point(4, 89);
            constructorTextBox.Margin = new Padding(4, 3, 4, 3);
            constructorTextBox.Name = "constructorTextBox";
            constructorTextBox.Size = new System.Drawing.Size(213, 23);
            constructorTextBox.TabIndex = 13;
            // 
            // designerTextBox
            // 
            designerTextBox.BackColor = System.Drawing.Color.FromArgb(255, 245, 230);
            designerTextBox.Enabled = false;
            designerTextBox.Font = new System.Drawing.Font("Arial", 10F);
            designerTextBox.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            designerTextBox.Location = new System.Drawing.Point(4, 27);
            designerTextBox.Margin = new Padding(4, 3, 4, 3);
            designerTextBox.Name = "designerTextBox";
            designerTextBox.Size = new System.Drawing.Size(213, 23);
            designerTextBox.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Font = new System.Drawing.Font("Arial", 10F);
            label6.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            label6.Location = new System.Drawing.Point(8, 5);
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
            label7.Location = new System.Drawing.Point(6, 66);
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
            ButtonApprovement.Location = new System.Drawing.Point(4, 122);
            ButtonApprovement.Margin = new Padding(4, 3, 4, 3);
            ButtonApprovement.Name = "ButtonApprovement";
            ButtonApprovement.Size = new System.Drawing.Size(213, 48);
            ButtonApprovement.TabIndex = 11;
            ButtonApprovement.Text = "Согласование с технологом";
            ButtonApprovement.UseVisualStyleBackColor = true;
            ButtonApprovement.Visible = false;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(SortBox);
            panel5.Controls.Add(archiveCheckBox);
            panel5.Controls.Add(actualCheckBox);
            panel5.Controls.Add(preliminaryCheckBox);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new System.Drawing.Point(4, 169);
            panel5.Margin = new Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(225, 163);
            panel5.TabIndex = 12;
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
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(customLabel3);
            panel2.Controls.Add(searchControl1);
            panel2.Controls.Add(customButton12);
            panel2.Controls.Add(filterTextBox1);
            panel2.Controls.Add(model);
            panel2.Controls.Add(articul);
            panel2.Controls.Add(kode);
            panel2.Controls.Add(group);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new System.Drawing.Point(4, 3);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(225, 145);
            panel2.TabIndex = 11;
            // 
            // customLabel3
            // 
            customLabel3.AutoSize = true;
            customLabel3.Font = new System.Drawing.Font("Arial", 10F);
            customLabel3.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customLabel3.Location = new System.Drawing.Point(6, 143);
            customLabel3.Margin = new Padding(4, 0, 4, 0);
            customLabel3.Name = "customLabel3";
            customLabel3.Size = new System.Drawing.Size(0, 16);
            customLabel3.TabIndex = 7;
            customLabel3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // searchControl1
            // 
            searchControl1.Location = new System.Drawing.Point(4, 3);
            searchControl1.Margin = new Padding(4, 3, 4, 3);
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.AllowAutoApply = false;
            searchControl1.Properties.Buttons.AddRange(new EditorButton[] { new ClearButton(), new SearchButton(), new MRUButton() });
            searchControl1.Properties.ShowDefaultButtonsMode = ShowDefaultButtonsMode.AutoShowClear;
            searchControl1.Properties.ShowMRUButton = true;
            searchControl1.Properties.QueryIsSearchColumn += searchControl1_QueryIsSearchColumn;
            searchControl1.Properties.ButtonClick += SearchButton_Click;
            searchControl1.Size = new System.Drawing.Size(223, 20);
            searchControl1.TabIndex = 6;
            // 
            // customButton12
            // 
            customButton12.BackColor = System.Drawing.Color.FromArgb(255, 204, 178);
            customButton12.FlatAppearance.BorderSize = 0;
            customButton12.FlatStyle = FlatStyle.Flat;
            customButton12.Font = new System.Drawing.Font("Arial", 10F);
            customButton12.ForeColor = System.Drawing.Color.FromArgb(210, 105, 30);
            customButton12.Location = new System.Drawing.Point(140, 185);
            customButton12.Margin = new Padding(4, 3, 4, 3);
            customButton12.Name = "customButton12";
            customButton12.Size = new System.Drawing.Size(33, 27);
            customButton12.TabIndex = 4;
            customButton12.Text = "Фильтр";
            customButton12.UseVisualStyleBackColor = false;
            customButton12.Visible = false;
            // 
            // filterTextBox1
            // 
            filterTextBox1.BackColor = System.Drawing.Color.FromArgb(255, 239, 213);
            filterTextBox1.Font = new System.Drawing.Font("Arial", 10F);
            filterTextBox1.ForeColor = System.Drawing.Color.FromArgb(128, 64, 0);
            filterTextBox1.Location = new System.Drawing.Point(27, 185);
            filterTextBox1.Margin = new Padding(4, 3, 4, 3);
            filterTextBox1.Name = "filterTextBox1";
            filterTextBox1.Size = new System.Drawing.Size(145, 23);
            filterTextBox1.TabIndex = 5;
            filterTextBox1.Visible = false;
            // 
            // model
            // 
            model.AutoSize = true;
            model.Location = new System.Drawing.Point(6, 36);
            model.Margin = new Padding(4, 3, 4, 3);
            model.Name = "model";
            model.Size = new System.Drawing.Size(77, 17);
            model.TabIndex = 2;
            model.Text = "по модели";
            model.UseVisualStyleBackColor = true;
            model.CheckedChanged += search_CheckedChanged;
            // 
            // articul
            // 
            articul.AutoSize = true;
            articul.Checked = true;
            articul.Location = new System.Drawing.Point(6, 62);
            articul.Margin = new Padding(4, 3, 4, 3);
            articul.Name = "articul";
            articul.Size = new System.Drawing.Size(88, 17);
            articul.TabIndex = 1;
            articul.TabStop = true;
            articul.Text = "по артикулу";
            articul.UseVisualStyleBackColor = true;
            articul.CheckedChanged += search_CheckedChanged;
            // 
            // kode
            // 
            kode.AutoSize = true;
            kode.Location = new System.Drawing.Point(6, 115);
            kode.Margin = new Padding(4, 3, 4, 3);
            kode.Name = "kode";
            kode.Size = new System.Drawing.Size(65, 17);
            kode.TabIndex = 0;
            kode.Text = "по коду";
            kode.UseVisualStyleBackColor = true;
            kode.CheckedChanged += search_CheckedChanged;
            // 
            // group
            // 
            group.AutoSize = true;
            group.Location = new System.Drawing.Point(6, 89);
            group.Margin = new Padding(4, 3, 4, 3);
            group.Name = "group";
            group.Size = new System.Drawing.Size(75, 17);
            group.TabIndex = 3;
            group.Text = "по группе";
            group.UseVisualStyleBackColor = true;
            group.CheckedChanged += search_CheckedChanged;
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
            layoutControl1.Controls.Add(customLabel2);
            layoutControl1.Controls.Add(customLabel1);
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
            // customLabel2
            // 
            customLabel2.Font = new System.Drawing.Font("Arial", 10F);
            customLabel2.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabel2.Location = new System.Drawing.Point(24, 648);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new System.Drawing.Size(843, 20);
            customLabel2.TabIndex = 1;
            customLabel2.Text = "Увязанные в этом сеансе";
            // 
            // customLabel1
            // 
            customLabel1.Font = new System.Drawing.Font("Arial", 10F);
            customLabel1.ForeColor = System.Drawing.Color.FromArgb(20, 70, 100);
            customLabel1.Location = new System.Drawing.Point(24, 59);
            customLabel1.Name = "customLabel1";
            customLabel1.Size = new System.Drawing.Size(167, 30);
            customLabel1.TabIndex = 1;
            customLabel1.Text = "Артикулы для увязки";
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new System.Drawing.Point(1426, 500);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(301, 375);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // gridControl_binded
            // 
            gridControl_binded.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            gridControl_binded.Font = new System.Drawing.Font("Arial", 10F);
            gridControl_binded.Location = new System.Drawing.Point(24, 672);
            gridControl_binded.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl_binded.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControl_binded.MainView = gridView_binded;
            gridControl_binded.Margin = new Padding(4, 3, 4, 3);
            gridControl_binded.Name = "gridControl_binded";
            gridControl_binded.Size = new System.Drawing.Size(843, 215);
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
            gridControlNZP.Location = new System.Drawing.Point(1155, 382);
            gridControlNZP.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControlNZP.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControlNZP.MainView = gridViewNZP;
            gridControlNZP.Margin = new Padding(4, 3, 4, 3);
            gridControlNZP.Name = "gridControlNZP";
            gridControlNZP.Size = new System.Drawing.Size(584, 70);
            gridControlNZP.TabIndex = 5;
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
            gridViewNZP.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
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
            ButtonUnboundWd.Location = new System.Drawing.Point(1052, 400);
            ButtonUnboundWd.Margin = new Padding(4, 3, 4, 3);
            ButtonUnboundWd.Name = "ButtonUnboundWd";
            ButtonUnboundWd.Size = new System.Drawing.Size(56, 33);
            ButtonUnboundWd.StyleController = layoutControl1;
            ButtonUnboundWd.TabIndex = 7;
            ButtonUnboundWd.Text = "Отвязать";
            // 
            // loadAllCheckBox
            // 
            loadAllCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            loadAllCheckBox.ForeColor = System.Drawing.Color.FromArgb(120, 60, 30);
            loadAllCheckBox.Location = new System.Drawing.Point(895, 45);
            loadAllCheckBox.Margin = new Padding(4, 3, 4, 3);
            loadAllCheckBox.Name = "loadAllCheckBox";
            loadAllCheckBox.Size = new System.Drawing.Size(844, 20);
            loadAllCheckBox.TabIndex = 3;
            loadAllCheckBox.Text = "Показать все РТ";
            loadAllCheckBox.UseVisualStyleBackColor = true;
            loadAllCheckBox.CheckedChanged += customCheckBox4_CheckedChanged;
            // 
            // simpleButton1
            // 
            simpleButton1.BackgroundImageLayout = ImageLayout.None;
            simpleButton1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.ImageOptions.Location = ImageLocation.MiddleCenter;
            simpleButton1.Location = new System.Drawing.Point(931, 392);
            simpleButton1.Margin = new Padding(4, 3, 4, 3);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(100, 36);
            simpleButton1.StyleController = layoutControl1;
            simpleButton1.TabIndex = 6;
            simpleButton1.Click += BindButton_Click;
            // 
            // customGridControl3
            // 
            customGridControl3.EmbeddedNavigator.Margin = new Padding(4, 3, 4, 3);
            customGridControl3.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl3.Location = new System.Drawing.Point(907, 500);
            customGridControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            customGridControl3.MainView = gridView6;
            customGridControl3.Margin = new Padding(4, 3, 4, 3);
            customGridControl3.Name = "customGridControl3";
            customGridControl3.Size = new System.Drawing.Size(505, 375);
            customGridControl3.TabIndex = 8;
            customGridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView6 });
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
            gridControl_wdToBind.Location = new System.Drawing.Point(895, 69);
            gridControl_wdToBind.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl_wdToBind.MainView = gridView_wdToBind;
            gridControl_wdToBind.Margin = new Padding(0);
            gridControl_wdToBind.Name = "gridControl_wdToBind";
            gridControl_wdToBind.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemCheckEdit3, repositoryItemCheckEdit4, repositoryItemCheckEdit6, repositoryItemCheckEdit7 });
            gridControl_wdToBind.Size = new System.Drawing.Size(844, 298);
            gridControl_wdToBind.TabIndex = 4;
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
            colstatus1.Visible = true;
            colstatus1.VisibleIndex = 4;
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
            gridControl_unboundArts.Location = new System.Drawing.Point(24, 93);
            gridControl_unboundArts.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridControl_unboundArts.MainView = gridView_unboundArts;
            gridControl_unboundArts.Margin = new Padding(4, 3, 4, 3);
            gridControl_unboundArts.Name = "gridControl_unboundArts";
            gridControl_unboundArts.Size = new System.Drawing.Size(843, 501);
            gridControl_unboundArts.TabIndex = 0;
            gridControl_unboundArts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_unboundArts });
            gridControl_unboundArts.Click += gridControl_unboundArts_Click;
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
            gridView_unboundArts.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridView_unboundArts.OptionsView.ShowGroupPanel = false;
            gridView_unboundArts.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
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
            columnDefinition1.Width = 100D;
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
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3, layoutControlItem5, layoutControlItem7, layoutControlGroup3, layoutControlItem11, emptySpaceItem2, layoutControlItem6, emptySpaceItem3, emptySpaceItem1, emptySpaceItem4, emptySpaceItem6, emptySpaceItem8, emptySpaceItem9, simpleSeparator1, simpleSeparator2, splitterItem1, splitterItem3 });
            layoutControlGroup2.Location = new System.Drawing.Point(871, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlGroup2.Size = new System.Drawing.Size(872, 891);
            layoutControlGroup2.Text = "РТ для увязки";
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = gridControl_wdToBind;
            layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(848, 302);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = simpleButton1;
            layoutControlItem5.Location = new System.Drawing.Point(36, 347);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(104, 40);
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = loadAllCheckBox;
            layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new System.Drawing.Size(848, 24);
            layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4, splitterItem2, layoutControlItem14 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 422);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Size = new System.Drawing.Size(848, 424);
            layoutControlGroup3.Text = "Схема РТ";
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = customGridControl3;
            layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(509, 379);
            layoutControlItem4.TextVisible = false;
            // 
            // splitterItem2
            // 
            splitterItem2.Location = new System.Drawing.Point(509, 0);
            splitterItem2.Name = "splitterItem2";
            splitterItem2.Size = new System.Drawing.Size(10, 379);
            // 
            // layoutControlItem14
            // 
            layoutControlItem14.Control = pictureBox2;
            layoutControlItem14.Location = new System.Drawing.Point(519, 0);
            layoutControlItem14.Name = "layoutControlItem14";
            layoutControlItem14.Size = new System.Drawing.Size(305, 379);
            layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            layoutControlItem11.Control = gridControlNZP;
            layoutControlItem11.Location = new System.Drawing.Point(260, 337);
            layoutControlItem11.Name = "layoutControlItem11";
            layoutControlItem11.Size = new System.Drawing.Size(588, 74);
            layoutControlItem11.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.Location = new System.Drawing.Point(140, 337);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(17, 74);
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = ButtonUnboundWd;
            layoutControlItem6.Location = new System.Drawing.Point(157, 355);
            layoutControlItem6.MinSize = new System.Drawing.Size(60, 26);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(60, 37);
            layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            emptySpaceItem3.Location = new System.Drawing.Point(217, 337);
            emptySpaceItem3.Name = "emptySpaceItem3";
            emptySpaceItem3.Size = new System.Drawing.Size(43, 74);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(157, 392);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(60, 19);
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.Location = new System.Drawing.Point(36, 387);
            emptySpaceItem4.MinSize = new System.Drawing.Size(104, 24);
            emptySpaceItem4.Name = "emptySpaceItem4";
            emptySpaceItem4.Size = new System.Drawing.Size(104, 24);
            emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            // 
            // emptySpaceItem6
            // 
            emptySpaceItem6.Location = new System.Drawing.Point(157, 337);
            emptySpaceItem6.Name = "emptySpaceItem6";
            emptySpaceItem6.Size = new System.Drawing.Size(60, 18);
            // 
            // emptySpaceItem8
            // 
            emptySpaceItem8.Location = new System.Drawing.Point(36, 337);
            emptySpaceItem8.Name = "emptySpaceItem8";
            emptySpaceItem8.Size = new System.Drawing.Size(104, 10);
            // 
            // emptySpaceItem9
            // 
            emptySpaceItem9.Location = new System.Drawing.Point(0, 337);
            emptySpaceItem9.Name = "emptySpaceItem9";
            emptySpaceItem9.Size = new System.Drawing.Size(36, 74);
            // 
            // simpleSeparator1
            // 
            simpleSeparator1.Location = new System.Drawing.Point(0, 421);
            simpleSeparator1.Name = "simpleSeparator1";
            simpleSeparator1.Size = new System.Drawing.Size(848, 1);
            // 
            // simpleSeparator2
            // 
            simpleSeparator2.Location = new System.Drawing.Point(0, 336);
            simpleSeparator2.Name = "simpleSeparator2";
            simpleSeparator2.Size = new System.Drawing.Size(848, 1);
            // 
            // splitterItem1
            // 
            splitterItem1.Location = new System.Drawing.Point(0, 411);
            splitterItem1.Name = "splitterItem1";
            splitterItem1.Size = new System.Drawing.Size(848, 10);
            // 
            // splitterItem3
            // 
            splitterItem3.Location = new System.Drawing.Point(0, 326);
            splitterItem3.Name = "splitterItem3";
            splitterItem3.Size = new System.Drawing.Size(848, 10);
            // 
            // layoutControlGroup6
            // 
            layoutControlGroup6.CaptionImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            buttonImageOptions1.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("buttonImageOptions1.SvgImage");
            buttonImageOptions3.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("buttonImageOptions3.SvgImage");
            buttonImageOptions5.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("buttonImageOptions5.SvgImage");
            buttonImageOptions7.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("buttonImageOptions7.SvgImage");
            toolTipItem1.Text = "Создаёт разделение труда на основе выбранного артикула, сразу заполняя Группу, Модель и Артикул. Открывает окно редактирования, где можно добавить схему разделения";
            superToolTip1.Items.Add(toolTipItem1);
            layoutControlGroup6.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Добавить", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Добавить новое пустое разделение труда", -1, true, null, true, false, true, "btnAdd", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, false, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Редактировать", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Редактировать выбранное РТ", -1, true, null, true, false, true, "btnEdit", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, false, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Архив+копия", true, buttonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Создать копию РТ и отправить базовое РТ в архив", -1, true, null, true, false, true, "btnArch", -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("|", true, buttonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Создать из артикула", true, buttonImageOptions7, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Создать РТ на основе выбранного артикула", -1, true, superToolTip1, true, false, true, "btnArt", -1) });
            layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, emptySpaceItem5, layoutControlItem13, emptySpaceItem7, layoutControlItem1, layoutControlItem9, simpleSeparator3 });
            layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup6.Name = "layoutControlGroup6";
            layoutControlGroup6.Size = new System.Drawing.Size(871, 891);
            layoutControlGroup6.Text = " ";
            layoutControlGroup6.CustomButtonClick += layoutControlGroup6_CustomButtonClick;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = gridControl_unboundArts;
            layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(847, 505);
            layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.Location = new System.Drawing.Point(0, 540);
            emptySpaceItem5.Name = "emptySpaceItem5";
            emptySpaceItem5.Size = new System.Drawing.Size(847, 49);
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = gridControl_binded;
            layoutControlItem13.Location = new System.Drawing.Point(0, 613);
            layoutControlItem13.Name = "layoutControlItem13";
            layoutControlItem13.Size = new System.Drawing.Size(847, 219);
            layoutControlItem13.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            emptySpaceItem7.Location = new System.Drawing.Point(171, 0);
            emptySpaceItem7.Name = "emptySpaceItem7";
            emptySpaceItem7.Size = new System.Drawing.Size(676, 34);
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customLabel1;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(171, 34);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = customLabel2;
            layoutControlItem9.Location = new System.Drawing.Point(0, 589);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new System.Drawing.Size(847, 24);
            layoutControlItem9.TextVisible = false;
            // 
            // simpleSeparator3
            // 
            simpleSeparator3.Location = new System.Drawing.Point(0, 539);
            simpleSeparator3.Name = "simpleSeparator3";
            simpleSeparator3.Size = new System.Drawing.Size(847, 1);
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
            ClientSize = new System.Drawing.Size(1771, 965);
            Controls.Add(xtraTabControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "TeamWork";
            Text = "Нормативные расценки";
            FormClosing += TeamWork_FormClosing;
            Load += TeamWorkForm_Load;
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).EndInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl1).EndInit();
            xtraTabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlKontTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView4).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaszTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditProizv).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditOb).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEditPodr).EndInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)ANNgridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit2).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRaskrTW).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRaskrTW).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl7).EndInit();
            panelControl7.ResumeLayout(false);
            panelControl7.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            xtraTabPageArticles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)xtraTabControl2).EndInit();
            xtraTabControl2.ResumeLayout(false);
            xtraTabPageWorkDivisions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
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
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel5;
        private PanelControl panelControl7;
        private CustomLabel label6;
        private CustomLabel label7;
        private CustomButton ButtonApprovement;
        private TableLayoutPanel tableLayoutPanel4;
        private CustomButton customButton9;
        private CustomButton ButtonEditWd;
        private CustomButton customButton8;
        private CustomButton ButtonArchAndCopyWd;
        private CustomButton ButtonPreliminaryWd;
        private CustomButton ButtonCopyWd;
        private CustomTextBox buffer;
        private Panel panel5;
        private CustomCheckBox SortBox;
        private CustomCheckBox archiveCheckBox;
        private CustomCheckBox actualCheckBox;
        private CustomCheckBox preliminaryCheckBox;
        private Panel panel2;
        private SearchControl searchControl1;
        private CustomButton customButton12;
        private CustomTextBox filterTextBox1;
        private RadioButton model;
        private RadioButton articul;
        private RadioButton kode;
        private RadioButton group;
        private DevExpress.Utils.FlyoutPanel flyoutPanel1;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private CustomCancelButton customCancelButton1;
        private CustomComboBox customComboBox1;
        private CustomButton customButton1;
        private CustomLabel customLabel3;
        private ErrorProvider errorProvider1;
        private BindingSource desBindingSource;
        private BindingSource constrBindingSource;
        private CustomTextBox constructorTextBox;
        private CustomTextBox designerTextBox;
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
        private TableLayoutPanel tableLayoutPanel1;
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
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private CustomLabel customLabel1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private CustomLabel customLabel2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
    }
}
