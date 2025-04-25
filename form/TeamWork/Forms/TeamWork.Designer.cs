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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamWork));
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.TabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.constructorTextBox = new SewingProduction.CustomTextBox();
            this.designerTextBox = new SewingProduction.CustomTextBox();
            this.label6 = new SewingProduction.CustomLabel();
            this.label7 = new SewingProduction.CustomLabel();
            this.ButtonApprovement = new SewingProduction.CustomButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.customButton9 = new SewingProduction.CustomButton();
            this.ButtonEditWd = new SewingProduction.CustomButton();
            this.customButton8 = new SewingProduction.CustomButton();
            this.ButtonArchAndCopyWd = new SewingProduction.CustomButton();
            this.ButtonPreliminaryWd = new SewingProduction.CustomButton();
            this.ButtonCopyWd = new SewingProduction.CustomButton();
            this.buffer = new SewingProduction.CustomTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.SortBox = new SewingProduction.CustomCheckBox();
            this.archiveCheckBox = new SewingProduction.CustomCheckBox();
            this.actualCheckBox = new SewingProduction.CustomCheckBox();
            this.preliminaryCheckBox = new SewingProduction.CustomCheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.customLabel3 = new SewingProduction.CustomLabel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.ANNgridControl = new DevExpress.XtraGrid.GridControl();
            this.ANNgridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colgroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colarticul = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldateCreate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldateUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_shv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyazo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_kr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colslogn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkomment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldiz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colconstr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridView15 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.customButton12 = new SewingProduction.CustomButton();
            this.filterTextBox1 = new SewingProduction.CustomTextBox();
            this.model = new System.Windows.Forms.RadioButton();
            this.articul = new System.Windows.Forms.RadioButton();
            this.kode = new System.Windows.Forms.RadioButton();
            this.group = new System.Windows.Forms.RadioButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.customGridControl5 = new SewingProduction.CustomGridControl();
            this.gridView10 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.kodd_rt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannId2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.kolNZP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PztCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView14 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ButtonUnboundWd = new SewingProduction.CustomSimpleButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControlRaszTW = new DevExpress.XtraGrid.GridControl();
            this.normraszBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coln = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coln1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colrazryd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colobor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkod_o = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannId3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView13 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlRaskrTW = new DevExpress.XtraGrid.GridControl();
            this.normraskBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkod2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkod_o1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colrazryd1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannId4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlKontTW = new DevExpress.XtraGrid.GridControl();
            this.normkontBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colkod_o2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colrazryd2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltext2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannId5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlDopObrTW = new DevExpress.XtraGrid.GridControl();
            this.normdopobrBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colsek_p = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_p_tamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_v = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_stra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannId6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label8 = new SewingProduction.CustomLabel();
            this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.xtraTabPageArticles = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageWorkDivisions = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.customGridControl3 = new SewingProduction.CustomGridControl();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colannId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coln3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.loadAllCheckBox = new SewingProduction.CustomCheckBox();
            this.customLabel5 = new SewingProduction.CustomLabel();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.customLabel4 = new SewingProduction.CustomLabel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.customLabel2 = new SewingProduction.CustomLabel();
            this.customLabel1 = new SewingProduction.CustomLabel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.actualCheckBox1 = new SewingProduction.CustomCheckBox();
            this.preliminaryCheckBox1 = new SewingProduction.CustomCheckBox();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.customGridControl2 = new SewingProduction.CustomGridControl();
            this.gridView8 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colarticul1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colannId7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colstatus1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.customGridControl6 = new SewingProduction.CustomGridControl();
            this.gridView12 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.customGridControl1 = new SewingProduction.CustomGridControl();
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.код = new DevExpress.XtraGrid.Columns.GridColumn();
            this.артикул = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.группа = new DevExpress.XtraGrid.Columns.GridColumn();
            this.модель = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView11 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.customButton2 = new SewingProduction.CustomButton();
            this.flyoutPanel1 = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl1 = new DevExpress.Utils.FlyoutPanelControl();
            this.customCancelButton1 = new SewingProduction.CustomCancelButton();
            this.customComboBox1 = new SewingProduction.CustomComboBox();
            this.customButton1 = new SewingProduction.CustomButton();
            this.gridControlPreArch = new SewingProduction.CustomGridControl();
            this.artnormnBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewPreArch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.normraszBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.artnormnBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sparticulBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.artnormnBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.sparticulBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.desBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.constrBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ANNgridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ANNgridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView14)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRaszTW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normraszBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRaskrTW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normraskBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlKontTW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normkontBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDopObrTW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normdopobrBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            this.xtraTabPageArticles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabPageWorkDivisions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3.Panel1)).BeginInit();
            this.splitContainerControl3.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3.Panel2)).BeginInit();
            this.splitContainerControl3.Panel2.SuspendLayout();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            this.splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.Panel2.SuspendLayout();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).BeginInit();
            this.flyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).BeginInit();
            this.flyoutPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPreArch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.artnormnBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPreArch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normraszBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.artnormnBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sparticulBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.artnormnBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sparticulBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.constrBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl1.Appearance.Options.UseBackColor = true;
            this.xtraTabControl1.AppearancePage.Header.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl1.AppearancePage.Header.Options.UseBackColor = true;
            this.xtraTabControl1.AppearancePage.HeaderActive.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseBackColor = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.TabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1518, 836);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabPage1,
            this.xtraTabPageArticles});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.XtraTabControl1_SelectedPageChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Appearance.Header.BackColor = System.Drawing.Color.Transparent;
            this.TabPage1.Appearance.Header.Options.UseBackColor = true;
            this.TabPage1.Appearance.HeaderActive.BackColor = System.Drawing.Color.Transparent;
            this.TabPage1.Appearance.HeaderActive.Options.UseBackColor = true;
            this.TabPage1.Controls.Add(this.tableLayoutPanel3);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(1516, 811);
            this.TabPage1.Text = "1. Разделения труда";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 209F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.splitContainerControl1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1516, 811);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.panelControl7, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(200, 786);
            this.tableLayoutPanel5.TabIndex = 20;
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.constructorTextBox);
            this.panelControl7.Controls.Add(this.designerTextBox);
            this.panelControl7.Controls.Add(this.label6);
            this.panelControl7.Controls.Add(this.label7);
            this.panelControl7.Controls.Add(this.ButtonApprovement);
            this.panelControl7.Location = new System.Drawing.Point(3, 631);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(194, 152);
            this.panelControl7.TabIndex = 22;
            // 
            // constructorTextBox
            // 
            this.constructorTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.constructorTextBox.Font = new System.Drawing.Font("Arial", 10F);
            this.constructorTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.constructorTextBox.Location = new System.Drawing.Point(3, 77);
            this.constructorTextBox.Name = "constructorTextBox";
            this.constructorTextBox.ObjectName = null;
            this.constructorTextBox.Size = new System.Drawing.Size(188, 23);
            this.constructorTextBox.TabIndex = 13;
            // 
            // designerTextBox
            // 
            this.designerTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.designerTextBox.Font = new System.Drawing.Font("Arial", 10F);
            this.designerTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.designerTextBox.Location = new System.Drawing.Point(3, 23);
            this.designerTextBox.Name = "designerTextBox";
            this.designerTextBox.ObjectName = null;
            this.designerTextBox.Size = new System.Drawing.Size(188, 23);
            this.designerTextBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.label6.Location = new System.Drawing.Point(7, 4);
            this.label6.Name = "label6";
            this.label6.ObjectName = null;
            this.label6.Size = new System.Drawing.Size(70, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Дизайнер";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 10F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.label7.Location = new System.Drawing.Point(5, 57);
            this.label7.Name = "label7";
            this.label7.ObjectName = null;
            this.label7.Size = new System.Drawing.Size(89, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Конструктор";
            // 
            // ButtonApprovement
            // 
            this.ButtonApprovement.BackColor = System.Drawing.Color.White;
            this.ButtonApprovement.Font = new System.Drawing.Font("Arial", 10F);
            this.ButtonApprovement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.ButtonApprovement.Location = new System.Drawing.Point(3, 106);
            this.ButtonApprovement.Name = "ButtonApprovement";
            this.ButtonApprovement.ObjectName = null;
            this.ButtonApprovement.Size = new System.Drawing.Size(186, 58);
            this.ButtonApprovement.TabIndex = 11;
            this.ButtonApprovement.Text = "Согласование с технологом";
            this.ButtonApprovement.UseVisualStyleBackColor = true;
            this.ButtonApprovement.Visible = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.customButton9, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.ButtonEditWd, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.customButton8, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.ButtonArchAndCopyWd, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.ButtonPreliminaryWd, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.ButtonCopyWd, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.buffer, 0, 6);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 317);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(194, 308);
            this.tableLayoutPanel4.TabIndex = 21;
            // 
            // customButton9
            // 
            this.customButton9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.customButton9.FlatAppearance.BorderSize = 0;
            this.customButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton9.Font = new System.Drawing.Font("Arial", 12F);
            this.customButton9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.customButton9.Location = new System.Drawing.Point(3, 3);
            this.customButton9.Name = "customButton9";
            this.customButton9.ObjectName = null;
            this.customButton9.Size = new System.Drawing.Size(147, 1);
            this.customButton9.TabIndex = 13;
            this.customButton9.Text = "конф. карта";
            this.customButton9.UseVisualStyleBackColor = false;
            this.customButton9.Visible = false;
            // 
            // ButtonEditWd
            // 
            this.ButtonEditWd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.ButtonEditWd.FlatAppearance.BorderSize = 0;
            this.ButtonEditWd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonEditWd.Font = new System.Drawing.Font("Arial", 12F);
            this.ButtonEditWd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.ButtonEditWd.Location = new System.Drawing.Point(3, 7);
            this.ButtonEditWd.Name = "ButtonEditWd";
            this.ButtonEditWd.ObjectName = null;
            this.ButtonEditWd.Size = new System.Drawing.Size(188, 30);
            this.ButtonEditWd.TabIndex = 4;
            this.ButtonEditWd.Text = "редактировать РТ";
            this.ButtonEditWd.UseVisualStyleBackColor = false;
            this.ButtonEditWd.Click += new System.EventHandler(this.ButtonEditWd_Click);
            // 
            // customButton8
            // 
            this.customButton8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.customButton8.FlatAppearance.BorderSize = 0;
            this.customButton8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton8.Font = new System.Drawing.Font("Arial", 12F);
            this.customButton8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.customButton8.Location = new System.Drawing.Point(3, 5);
            this.customButton8.Name = "customButton8";
            this.customButton8.ObjectName = null;
            this.customButton8.Size = new System.Drawing.Size(147, 1);
            this.customButton8.TabIndex = 12;
            this.customButton8.Text = "печать РТ";
            this.customButton8.UseVisualStyleBackColor = false;
            this.customButton8.Visible = false;
            // 
            // ButtonArchAndCopyWd
            // 
            this.ButtonArchAndCopyWd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.ButtonArchAndCopyWd.FlatAppearance.BorderSize = 0;
            this.ButtonArchAndCopyWd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonArchAndCopyWd.Font = new System.Drawing.Font("Arial", 12F);
            this.ButtonArchAndCopyWd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.ButtonArchAndCopyWd.Location = new System.Drawing.Point(3, 43);
            this.ButtonArchAndCopyWd.Name = "ButtonArchAndCopyWd";
            this.ButtonArchAndCopyWd.ObjectName = null;
            this.ButtonArchAndCopyWd.Size = new System.Drawing.Size(188, 30);
            this.ButtonArchAndCopyWd.TabIndex = 14;
            this.ButtonArchAndCopyWd.Text = "архив+копия";
            this.ButtonArchAndCopyWd.UseVisualStyleBackColor = false;
            this.ButtonArchAndCopyWd.Click += new System.EventHandler(this.ButtonArchAndCopyWd_Click);
            // 
            // ButtonPreliminaryWd
            // 
            this.ButtonPreliminaryWd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.ButtonPreliminaryWd.FlatAppearance.BorderSize = 0;
            this.ButtonPreliminaryWd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPreliminaryWd.Font = new System.Drawing.Font("Arial", 10F);
            this.ButtonPreliminaryWd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.ButtonPreliminaryWd.Location = new System.Drawing.Point(3, 79);
            this.ButtonPreliminaryWd.Name = "ButtonPreliminaryWd";
            this.ButtonPreliminaryWd.ObjectName = null;
            this.ButtonPreliminaryWd.Size = new System.Drawing.Size(188, 42);
            this.ButtonPreliminaryWd.TabIndex = 15;
            this.ButtonPreliminaryWd.Text = "Добавить предварительное ";
            this.ButtonPreliminaryWd.UseVisualStyleBackColor = false;
            this.ButtonPreliminaryWd.Click += new System.EventHandler(this.ButtonPreliminaryWd_Click);
            // 
            // ButtonCopyWd
            // 
            this.ButtonCopyWd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.ButtonCopyWd.Font = new System.Drawing.Font("Arial", 10F);
            this.ButtonCopyWd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.ButtonCopyWd.Location = new System.Drawing.Point(3, 127);
            this.ButtonCopyWd.Name = "ButtonCopyWd";
            this.ButtonCopyWd.ObjectName = null;
            this.ButtonCopyWd.Size = new System.Drawing.Size(188, 36);
            this.ButtonCopyWd.TabIndex = 19;
            this.ButtonCopyWd.Text = "Копировать РТ";
            this.ButtonCopyWd.UseVisualStyleBackColor = false;
            this.ButtonCopyWd.Click += new System.EventHandler(this.ButtonCopyWd_Click);
            // 
            // buffer
            // 
            this.buffer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.buffer.Enabled = false;
            this.buffer.Font = new System.Drawing.Font("Arial", 10F);
            this.buffer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.buffer.Location = new System.Drawing.Point(3, 169);
            this.buffer.Multiline = true;
            this.buffer.Name = "buffer";
            this.buffer.ObjectName = null;
            this.buffer.Size = new System.Drawing.Size(188, 83);
            this.buffer.TabIndex = 20;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.SortBox);
            this.panel5.Controls.Add(this.archiveCheckBox);
            this.panel5.Controls.Add(this.actualCheckBox);
            this.panel5.Controls.Add(this.preliminaryCheckBox);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 160);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(194, 151);
            this.panel5.TabIndex = 12;
            // 
            // SortBox
            // 
            this.SortBox.AutoSize = true;
            this.SortBox.Font = new System.Drawing.Font("Arial", 10F);
            this.SortBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.SortBox.Location = new System.Drawing.Point(9, 102);
            this.SortBox.Name = "SortBox";
            this.SortBox.ObjectName = null;
            this.SortBox.Size = new System.Drawing.Size(120, 20);
            this.SortBox.TabIndex = 4;
            this.SortBox.Text = "Не описанные";
            this.SortBox.UseVisualStyleBackColor = true;
            // 
            // archiveCheckBox
            // 
            this.archiveCheckBox.AutoSize = true;
            this.archiveCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            this.archiveCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.archiveCheckBox.Location = new System.Drawing.Point(9, 62);
            this.archiveCheckBox.Name = "archiveCheckBox";
            this.archiveCheckBox.ObjectName = null;
            this.archiveCheckBox.Size = new System.Drawing.Size(90, 20);
            this.archiveCheckBox.TabIndex = 3;
            this.archiveCheckBox.Text = "Архивные";
            this.archiveCheckBox.UseVisualStyleBackColor = true;
            this.archiveCheckBox.CheckedChanged += new System.EventHandler(this.Filter_CheckedChanged);
            // 
            // actualCheckBox
            // 
            this.actualCheckBox.AutoSize = true;
            this.actualCheckBox.Checked = true;
            this.actualCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.actualCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            this.actualCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.actualCheckBox.Location = new System.Drawing.Point(9, 36);
            this.actualCheckBox.Name = "actualCheckBox";
            this.actualCheckBox.ObjectName = null;
            this.actualCheckBox.Size = new System.Drawing.Size(105, 20);
            this.actualCheckBox.TabIndex = 2;
            this.actualCheckBox.Text = "Актуальные";
            this.actualCheckBox.UseVisualStyleBackColor = true;
            this.actualCheckBox.CheckedChanged += new System.EventHandler(this.Filter_CheckedChanged);
            // 
            // preliminaryCheckBox
            // 
            this.preliminaryCheckBox.AutoSize = true;
            this.preliminaryCheckBox.Checked = true;
            this.preliminaryCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.preliminaryCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            this.preliminaryCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.preliminaryCheckBox.Location = new System.Drawing.Point(9, 10);
            this.preliminaryCheckBox.Name = "preliminaryCheckBox";
            this.preliminaryCheckBox.ObjectName = null;
            this.preliminaryCheckBox.Size = new System.Drawing.Size(147, 20);
            this.preliminaryCheckBox.TabIndex = 1;
            this.preliminaryCheckBox.Text = "Предварительные";
            this.preliminaryCheckBox.UseVisualStyleBackColor = true;
            this.preliminaryCheckBox.CheckedChanged += new System.EventHandler(this.Filter_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.customLabel3);
            this.panel2.Controls.Add(this.searchControl1);
            this.panel2.Controls.Add(this.customButton12);
            this.panel2.Controls.Add(this.filterTextBox1);
            this.panel2.Controls.Add(this.model);
            this.panel2.Controls.Add(this.articul);
            this.panel2.Controls.Add(this.kode);
            this.panel2.Controls.Add(this.group);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 151);
            this.panel2.TabIndex = 11;
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.Font = new System.Drawing.Font("Arial", 10F);
            this.customLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customLabel3.Location = new System.Drawing.Point(5, 124);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.ObjectName = null;
            this.customLabel3.Size = new System.Drawing.Size(0, 16);
            this.customLabel3.TabIndex = 7;
            this.customLabel3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // searchControl1
            // 
            this.searchControl1.Client = this.ANNgridControl;
            this.searchControl1.Location = new System.Drawing.Point(3, 3);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.AllowAutoApply = false;
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton(),
            new DevExpress.XtraEditors.Repository.MRUButton()});
            this.searchControl1.Properties.Client = this.ANNgridControl;
            this.searchControl1.Properties.ShowDefaultButtonsMode = DevExpress.XtraEditors.Repository.ShowDefaultButtonsMode.AutoShowClear;
            this.searchControl1.Properties.ShowMRUButton = true;
            this.searchControl1.Properties.QueryIsSearchColumn += new DevExpress.XtraEditors.QueryIsSearchColumnEventHandler(this.searchControl1_QueryIsSearchColumn);
            this.searchControl1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.SearchButton_Click);
            this.searchControl1.Size = new System.Drawing.Size(191, 20);
            this.searchControl1.TabIndex = 6;
            // 
            // ANNgridControl
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ANNgridControl, 2);
            this.ANNgridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ANNgridControl.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ANNgridControl.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.ANNgridControl.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.ANNgridControl.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
            this.ANNgridControl.Location = new System.Drawing.Point(3, 3);
            this.ANNgridControl.MainView = this.ANNgridView;
            this.ANNgridControl.Name = "ANNgridControl";
            this.ANNgridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemCheckEdit2});
            this.ANNgridControl.Size = new System.Drawing.Size(925, 577);
            this.ANNgridControl.TabIndex = 7;
            this.ANNgridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ANNgridView,
            this.gridView15});
            this.ANNgridControl.Leave += new System.EventHandler(this.gridControl2_Leave);
            // 
            // ANNgridView
            // 
            this.ANNgridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ANNgridView.Appearance.FocusedRow.Options.UseFont = true;
            this.ANNgridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.ANNgridView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ANNgridView.Appearance.SelectedRow.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ANNgridView.Appearance.SelectedRow.Options.UseFont = true;
            this.ANNgridView.Appearance.SelectedRow.Options.UseTextOptions = true;
            this.ANNgridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colgroup,
            this.colarticul,
            this.colmod,
            this.colsek,
            this.colsek_vyaz,
            this.coldateCreate,
            this.coldateUpdate,
            this.colsek_shv,
            this.gridColumn35,
            this.gridColumn5,
            this.colsek_vyazo,
            this.colsek_vyaz5,
            this.colsek_vyaz7,
            this.colsek_vyaz12,
            this.colsek_vyaz10,
            this.colsek_vyaz6,
            this.colsek_kr,
            this.colslogn,
            this.colkomment,
            this.coldiz,
            this.colconstr,
            this.colannID});
            this.ANNgridView.CustomizationFormBounds = new System.Drawing.Rectangle(688, 702, 264, 272);
            this.ANNgridView.GridControl = this.ANNgridControl;
            this.ANNgridView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.ANNgridView.Name = "ANNgridView";
            this.ANNgridView.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            this.ANNgridView.OptionsView.ColumnAutoWidth = false;
            this.ANNgridView.OptionsView.ShowGroupPanel = false;
            this.ANNgridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colarticul, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.ANNgridView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.ANNgridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView3_FocusedRowChanged);
            // 
            // colgroup
            // 
            this.colgroup.Caption = "группа";
            this.colgroup.FieldName = "Group";
            this.colgroup.Name = "colgroup";
            this.colgroup.Visible = true;
            this.colgroup.VisibleIndex = 0;
            this.colgroup.Width = 55;
            // 
            // colarticul
            // 
            this.colarticul.Caption = "артикул";
            this.colarticul.FieldName = "Articul";
            this.colarticul.Name = "colarticul";
            this.colarticul.Visible = true;
            this.colarticul.VisibleIndex = 1;
            this.colarticul.Width = 85;
            // 
            // colmod
            // 
            this.colmod.Caption = "модель";
            this.colmod.FieldName = "Mod";
            this.colmod.Name = "colmod";
            this.colmod.Visible = true;
            this.colmod.VisibleIndex = 2;
            this.colmod.Width = 54;
            // 
            // colsek
            // 
            this.colsek.Caption = "сек. общ.";
            this.colsek.FieldName = "Sek";
            this.colsek.Name = "colsek";
            this.colsek.Visible = true;
            this.colsek.VisibleIndex = 3;
            this.colsek.Width = 61;
            // 
            // colsek_vyaz
            // 
            this.colsek_vyaz.Caption = "сек. вяз.";
            this.colsek_vyaz.FieldName = "SekVyaz";
            this.colsek_vyaz.Name = "colsek_vyaz";
            this.colsek_vyaz.Visible = true;
            this.colsek_vyaz.VisibleIndex = 4;
            // 
            // coldateCreate
            // 
            this.coldateCreate.Caption = "создание";
            this.coldateCreate.FieldName = "dateCreate";
            this.coldateCreate.Name = "coldateCreate";
            this.coldateCreate.Visible = true;
            this.coldateCreate.VisibleIndex = 5;
            // 
            // coldateUpdate
            // 
            this.coldateUpdate.Caption = "обновление";
            this.coldateUpdate.FieldName = "dateUpdate";
            this.coldateUpdate.Name = "coldateUpdate";
            this.coldateUpdate.Visible = true;
            this.coldateUpdate.VisibleIndex = 6;
            // 
            // colsek_shv
            // 
            this.colsek_shv.Caption = "сек. шв.";
            this.colsek_shv.FieldName = "SekShv";
            this.colsek_shv.Name = "colsek_shv";
            this.colsek_shv.Visible = true;
            this.colsek_shv.VisibleIndex = 7;
            // 
            // gridColumn35
            // 
            this.gridColumn35.Caption = "предв. архив";
            this.gridColumn35.ColumnEdit = this.repositoryItemCheckEdit2;
            this.gridColumn35.FieldName = "preArch";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 10;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "статус";
            this.gridColumn5.FieldName = "StatusText";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 9;
            // 
            // colsek_vyazo
            // 
            this.colsek_vyazo.Caption = "сек.отп.";
            this.colsek_vyazo.FieldName = "SekVyazo";
            this.colsek_vyazo.Name = "colsek_vyazo";
            this.colsek_vyazo.Visible = true;
            this.colsek_vyazo.VisibleIndex = 8;
            // 
            // colsek_vyaz5
            // 
            this.colsek_vyaz5.Caption = "класс5";
            this.colsek_vyaz5.FieldName = "SekVyaz5";
            this.colsek_vyaz5.Name = "colsek_vyaz5";
            this.colsek_vyaz5.Visible = true;
            this.colsek_vyaz5.VisibleIndex = 11;
            // 
            // colsek_vyaz7
            // 
            this.colsek_vyaz7.Caption = "класс 7";
            this.colsek_vyaz7.FieldName = "SekVyaz7";
            this.colsek_vyaz7.Name = "colsek_vyaz7";
            this.colsek_vyaz7.Visible = true;
            this.colsek_vyaz7.VisibleIndex = 12;
            // 
            // colsek_vyaz12
            // 
            this.colsek_vyaz12.Caption = "класс 12";
            this.colsek_vyaz12.FieldName = "SekVyaz12";
            this.colsek_vyaz12.Name = "colsek_vyaz12";
            this.colsek_vyaz12.Visible = true;
            this.colsek_vyaz12.VisibleIndex = 13;
            // 
            // colsek_vyaz10
            // 
            this.colsek_vyaz10.Caption = "класс 10";
            this.colsek_vyaz10.FieldName = "SekVyaz10";
            this.colsek_vyaz10.Name = "colsek_vyaz10";
            this.colsek_vyaz10.Visible = true;
            this.colsek_vyaz10.VisibleIndex = 14;
            // 
            // colsek_vyaz6
            // 
            this.colsek_vyaz6.Caption = "класс. 6";
            this.colsek_vyaz6.FieldName = "SekVyaz6";
            this.colsek_vyaz6.Name = "colsek_vyaz6";
            this.colsek_vyaz6.Visible = true;
            this.colsek_vyaz6.VisibleIndex = 15;
            // 
            // colsek_kr
            // 
            this.colsek_kr.Caption = "кручение";
            this.colsek_kr.FieldName = "SekKr";
            this.colsek_kr.Name = "colsek_kr";
            this.colsek_kr.Visible = true;
            this.colsek_kr.VisibleIndex = 16;
            // 
            // colslogn
            // 
            this.colslogn.Caption = "сложность";
            this.colslogn.FieldName = "Slogn";
            this.colslogn.Name = "colslogn";
            this.colslogn.Visible = true;
            this.colslogn.VisibleIndex = 17;
            // 
            // colkomment
            // 
            this.colkomment.Caption = "комментарий";
            this.colkomment.FieldName = "Komment";
            this.colkomment.Name = "colkomment";
            this.colkomment.Visible = true;
            this.colkomment.VisibleIndex = 18;
            this.colkomment.Width = 133;
            // 
            // coldiz
            // 
            this.coldiz.Caption = "дизайнер";
            this.coldiz.FieldName = "Diz";
            this.coldiz.Name = "coldiz";
            this.coldiz.Visible = true;
            this.coldiz.VisibleIndex = 19;
            // 
            // colconstr
            // 
            this.colconstr.Caption = "конструктор";
            this.colconstr.FieldName = "Constr";
            this.colconstr.Name = "colconstr";
            this.colconstr.Visible = true;
            this.colconstr.VisibleIndex = 20;
            // 
            // colannID
            // 
            this.colannID.FieldName = "AnnID";
            this.colannID.Name = "colannID";
            this.colannID.Visible = true;
            this.colannID.VisibleIndex = 21;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // gridView15
            // 
            this.gridView15.GridControl = this.ANNgridControl;
            this.gridView15.Name = "gridView15";
            // 
            // customButton12
            // 
            this.customButton12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(178)))));
            this.customButton12.FlatAppearance.BorderSize = 0;
            this.customButton12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton12.Font = new System.Drawing.Font("Arial", 10F);
            this.customButton12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(105)))), ((int)(((byte)(30)))));
            this.customButton12.Location = new System.Drawing.Point(120, 160);
            this.customButton12.Name = "customButton12";
            this.customButton12.ObjectName = null;
            this.customButton12.Size = new System.Drawing.Size(28, 23);
            this.customButton12.TabIndex = 4;
            this.customButton12.Text = "Фильтр";
            this.customButton12.UseVisualStyleBackColor = false;
            this.customButton12.Visible = false;
            // 
            // filterTextBox1
            // 
            this.filterTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(213)))));
            this.filterTextBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.filterTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.filterTextBox1.Location = new System.Drawing.Point(23, 160);
            this.filterTextBox1.Name = "filterTextBox1";
            this.filterTextBox1.ObjectName = null;
            this.filterTextBox1.Size = new System.Drawing.Size(125, 23);
            this.filterTextBox1.TabIndex = 5;
            this.filterTextBox1.Visible = false;
            // 
            // model
            // 
            this.model.AutoSize = true;
            this.model.Location = new System.Drawing.Point(5, 31);
            this.model.Name = "model";
            this.model.Size = new System.Drawing.Size(77, 17);
            this.model.TabIndex = 2;
            this.model.Text = "по модели";
            this.model.UseVisualStyleBackColor = true;
            this.model.CheckedChanged += new System.EventHandler(this.search_CheckedChanged);
            // 
            // articul
            // 
            this.articul.AutoSize = true;
            this.articul.Location = new System.Drawing.Point(5, 54);
            this.articul.Name = "articul";
            this.articul.Size = new System.Drawing.Size(88, 17);
            this.articul.TabIndex = 1;
            this.articul.Text = "по артикулу";
            this.articul.UseVisualStyleBackColor = true;
            this.articul.CheckedChanged += new System.EventHandler(this.search_CheckedChanged);
            // 
            // kode
            // 
            this.kode.AutoSize = true;
            this.kode.Location = new System.Drawing.Point(5, 100);
            this.kode.Name = "kode";
            this.kode.Size = new System.Drawing.Size(65, 17);
            this.kode.TabIndex = 0;
            this.kode.Text = "по коду";
            this.kode.UseVisualStyleBackColor = true;
            this.kode.CheckedChanged += new System.EventHandler(this.search_CheckedChanged);
            // 
            // group
            // 
            this.group.AutoSize = true;
            this.group.Checked = true;
            this.group.Location = new System.Drawing.Point(5, 77);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(75, 17);
            this.group.TabIndex = 3;
            this.group.TabStop = true;
            this.group.Text = "по группе";
            this.group.UseVisualStyleBackColor = true;
            this.group.CheckedChanged += new System.EventHandler(this.search_CheckedChanged);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(212, 3);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1301, 805);
            this.splitContainerControl1.SplitterPosition = 924;
            this.splitContainerControl1.TabIndex = 21;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 649F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 282F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ANNgridControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customGridControl5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ButtonUnboundWd, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(924, 805);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(652, 586);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 2);
            this.pictureBox1.Size = new System.Drawing.Size(276, 216);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // customGridControl5
            // 
            this.customGridControl5.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridControl5.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl5.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControl5.Location = new System.Drawing.Point(3, 586);
            this.customGridControl5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.customGridControl5.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl5.MainView = this.gridView10;
            this.customGridControl5.Name = "customGridControl5";
            this.customGridControl5.ObjectName = null;
            this.customGridControl5.Size = new System.Drawing.Size(643, 174);
            this.customGridControl5.TabIndex = 3;
            this.customGridControl5.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView10,
            this.gridView3,
            this.gridView14});
            // 
            // gridView10
            // 
            this.gridView10.Appearance.SelectedRow.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.gridView10.Appearance.SelectedRow.Options.UseFont = true;
            this.gridView10.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.kodd_rt,
            this.colannId2,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23,
            this.kolNZP,
            this.PztCount});
            this.gridView10.GridControl = this.customGridControl5;
            this.gridView10.GroupFormat = "{0}:  {1}{2}";
            this.gridView10.Name = "gridView10";
            this.gridView10.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView10.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView10.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.gridView10.OptionsView.ShowGroupPanel = false;
            // 
            // kodd_rt
            // 
            this.kodd_rt.FieldName = "kodd_rt";
            this.kodd_rt.Name = "kodd_rt";
            this.kodd_rt.Visible = true;
            this.kodd_rt.VisibleIndex = 0;
            // 
            // colannId2
            // 
            this.colannId2.FieldName = "annId";
            this.colannId2.Name = "colannId2";
            this.colannId2.Visible = true;
            this.colannId2.VisibleIndex = 1;
            this.colannId2.Width = 42;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Код";
            this.gridColumn20.FieldName = "kodd";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 2;
            this.gridColumn20.Width = 72;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Группа";
            this.gridColumn21.FieldName = "grup";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 3;
            this.gridColumn21.Width = 88;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Артикул";
            this.gridColumn22.FieldName = "articul";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 4;
            this.gridColumn22.Width = 85;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Модель";
            this.gridColumn23.FieldName = "mod";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 5;
            this.gridColumn23.Width = 94;
            // 
            // kolNZP
            // 
            this.kolNZP.Caption = "Наличие НЗП";
            this.kolNZP.FieldName = "kolNZP";
            this.kolNZP.Name = "kolNZP";
            this.kolNZP.Visible = true;
            this.kolNZP.VisibleIndex = 6;
            this.kolNZP.Width = 56;
            // 
            // PztCount
            // 
            this.PztCount.Caption = "Кол-во назн. опер.";
            this.PztCount.FieldName = "PZTCount";
            this.PztCount.Name = "PztCount";
            this.PztCount.Visible = true;
            this.PztCount.VisibleIndex = 7;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.customGridControl5;
            this.gridView3.Name = "gridView3";
            // 
            // gridView14
            // 
            this.gridView14.GridControl = this.customGridControl5;
            this.gridView14.Name = "gridView14";
            // 
            // ButtonUnboundWd
            // 
            this.ButtonUnboundWd.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.ButtonUnboundWd.AppearanceDisabled.BorderColor = System.Drawing.Color.Gray;
            this.ButtonUnboundWd.AppearanceDisabled.Options.UseBackColor = true;
            this.ButtonUnboundWd.AppearanceDisabled.Options.UseBorderColor = true;
            this.ButtonUnboundWd.Location = new System.Drawing.Point(3, 766);
            this.ButtonUnboundWd.Name = "ButtonUnboundWd";
            this.ButtonUnboundWd.Size = new System.Drawing.Size(75, 23);
            this.ButtonUnboundWd.TabIndex = 8;
            this.ButtonUnboundWd.Text = "Отвязать";
            this.ButtonUnboundWd.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.gridControlRaszTW, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gridControlRaskrTW, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.gridControlKontTW, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.gridControlDopObrTW, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.commentRichTextBox, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.06778F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75606F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.02086F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.98748F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(367, 805);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // gridControlRaszTW
            // 
            this.gridControlRaszTW.DataSource = this.normraszBindingSource;
            this.gridControlRaszTW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlRaszTW.Location = new System.Drawing.Point(3, 3);
            this.gridControlRaszTW.MainView = this.gridView1;
            this.gridControlRaszTW.Name = "gridControlRaszTW";
            this.gridControlRaszTW.Size = new System.Drawing.Size(361, 391);
            this.gridControlRaszTW.TabIndex = 6;
            this.gridControlRaszTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView13});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coln,
            this.coln1,
            this.colrazryd,
            this.coltext,
            this.colsek1,
            this.gridColumn30,
            this.gridColumn27,
            this.colobor,
            this.gridColumn26,
            this.gridColumn3,
            this.colkod_o,
            this.colannId3});
            this.gridView1.GridControl = this.gridControlRaszTW;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coln, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coln1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // coln
            // 
            this.coln.Caption = "№ оп.";
            this.coln.FieldName = "N";
            this.coln.Name = "coln";
            this.coln.Visible = true;
            this.coln.VisibleIndex = 0;
            this.coln.Width = 47;
            // 
            // coln1
            // 
            this.coln1.Caption = "№ п/оп.";
            this.coln1.FieldName = "N1";
            this.coln1.Name = "coln1";
            this.coln1.Visible = true;
            this.coln1.VisibleIndex = 1;
            this.coln1.Width = 39;
            // 
            // colrazryd
            // 
            this.colrazryd.Caption = "разряд";
            this.colrazryd.FieldName = "Razryad";
            this.colrazryd.Name = "colrazryd";
            this.colrazryd.Visible = true;
            this.colrazryd.VisibleIndex = 2;
            this.colrazryd.Width = 53;
            // 
            // coltext
            // 
            this.coltext.Caption = "наименование операции пошива";
            this.coltext.FieldName = "Text";
            this.coltext.Name = "coltext";
            this.coltext.Visible = true;
            this.coltext.VisibleIndex = 3;
            this.coltext.Width = 306;
            // 
            // colsek1
            // 
            this.colsek1.Caption = "сек.";
            this.colsek1.FieldName = "Sek";
            this.colsek1.Name = "colsek1";
            this.colsek1.Visible = true;
            this.colsek1.VisibleIndex = 4;
            this.colsek1.Width = 42;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "спец-ть";
            this.gridColumn30.FieldName = "spec";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 5;
            // 
            // gridColumn27
            // 
            this.gridColumn27.Caption = "производство";
            this.gridColumn27.FieldName = "proizv";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.Visible = true;
            this.gridColumn27.VisibleIndex = 6;
            // 
            // colobor
            // 
            this.colobor.Caption = "оборудование";
            this.colobor.FieldName = "Obor";
            this.colobor.Name = "colobor";
            this.colobor.Visible = true;
            this.colobor.VisibleIndex = 9;
            this.colobor.Width = 31;
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "вяз. подр.";
            this.gridColumn26.FieldName = "vyazPodr";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.Visible = true;
            this.gridColumn26.VisibleIndex = 7;
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "Kod";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 8;
            this.gridColumn3.Width = 37;
            // 
            // colkod_o
            // 
            this.colkod_o.FieldName = "KodO";
            this.colkod_o.Name = "colkod_o";
            this.colkod_o.Width = 70;
            // 
            // colannId3
            // 
            this.colannId3.FieldName = "AnnID";
            this.colannId3.Name = "colannId3";
            // 
            // gridView13
            // 
            this.gridView13.GridControl = this.gridControlRaszTW;
            this.gridView13.Name = "gridView13";
            // 
            // gridControlRaskrTW
            // 
            this.gridControlRaskrTW.DataSource = this.normraskBindingSource;
            this.gridControlRaskrTW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlRaskrTW.Location = new System.Drawing.Point(3, 400);
            this.gridControlRaskrTW.MainView = this.gridView2;
            this.gridControlRaskrTW.Name = "gridControlRaskrTW";
            this.gridControlRaskrTW.Size = new System.Drawing.Size(361, 129);
            this.gridControlRaskrTW.TabIndex = 7;
            this.gridControlRaskrTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colid,
            this.colkod2,
            this.colkod_o1,
            this.gridColumn18,
            this.colrazryd1,
            this.coltext1,
            this.colsek2,
            this.gridColumn34,
            this.gridColumn33,
            this.gridColumn32,
            this.gridColumn31,
            this.colannId4});
            this.gridView2.GridControl = this.gridControlRaskrTW;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colid
            // 
            this.colid.FieldName = "id";
            this.colid.Name = "colid";
            // 
            // colkod2
            // 
            this.colkod2.FieldName = "Kod";
            this.colkod2.Name = "colkod2";
            // 
            // colkod_o1
            // 
            this.colkod_o1.Caption = "№оп.";
            this.colkod_o1.FieldName = "KodO";
            this.colkod_o1.Name = "colkod_o1";
            this.colkod_o1.Visible = true;
            this.colkod_o1.VisibleIndex = 0;
            this.colkod_o1.Width = 50;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "№ п/оп.";
            this.gridColumn18.FieldName = "N1";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 1;
            this.gridColumn18.Width = 37;
            // 
            // colrazryd1
            // 
            this.colrazryd1.Caption = "разряд";
            this.colrazryd1.FieldName = "Razryad";
            this.colrazryd1.Name = "colrazryd1";
            this.colrazryd1.Visible = true;
            this.colrazryd1.VisibleIndex = 2;
            this.colrazryd1.Width = 53;
            // 
            // coltext1
            // 
            this.coltext1.Caption = "наименование операции раскроя";
            this.coltext1.FieldName = "Text";
            this.coltext1.Name = "coltext1";
            this.coltext1.Visible = true;
            this.coltext1.VisibleIndex = 3;
            this.coltext1.Width = 304;
            // 
            // colsek2
            // 
            this.colsek2.Caption = "сек.";
            this.colsek2.FieldName = "Sek";
            this.colsek2.Name = "colsek2";
            this.colsek2.Visible = true;
            this.colsek2.VisibleIndex = 4;
            this.colsek2.Width = 61;
            // 
            // gridColumn34
            // 
            this.gridColumn34.Caption = "специальность";
            this.gridColumn34.FieldName = "Spec";
            this.gridColumn34.Name = "gridColumn34";
            this.gridColumn34.Visible = true;
            this.gridColumn34.VisibleIndex = 7;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "оборудование";
            this.gridColumn33.FieldName = "Obor";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 8;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "код оп.";
            this.gridColumn32.FieldName = "KodO";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 6;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "код из.";
            this.gridColumn31.FieldName = "Kod";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 5;
            // 
            // colannId4
            // 
            this.colannId4.FieldName = "annId";
            this.colannId4.Name = "colannId4";
            this.colannId4.Width = 65;
            // 
            // gridControlKontTW
            // 
            this.gridControlKontTW.DataSource = this.normkontBindingSource;
            this.gridControlKontTW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlKontTW.Location = new System.Drawing.Point(3, 535);
            this.gridControlKontTW.MainView = this.gridView4;
            this.gridControlKontTW.Name = "gridControlKontTW";
            this.gridControlKontTW.Size = new System.Drawing.Size(361, 102);
            this.gridControlKontTW.TabIndex = 8;
            this.gridControlKontTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colkod_o2,
            this.gridColumn1,
            this.colrazryd2,
            this.coltext2,
            this.colsek3,
            this.colannId5});
            this.gridView4.GridControl = this.gridControlKontTW;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // colkod_o2
            // 
            this.colkod_o2.Caption = "№ оп.";
            this.colkod_o2.FieldName = "KodO";
            this.colkod_o2.Name = "colkod_o2";
            this.colkod_o2.Visible = true;
            this.colkod_o2.VisibleIndex = 0;
            this.colkod_o2.Width = 52;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "№ п/оп.";
            this.gridColumn1.FieldName = "N1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 37;
            // 
            // colrazryd2
            // 
            this.colrazryd2.Caption = "разряд";
            this.colrazryd2.FieldName = "Razryad";
            this.colrazryd2.Name = "colrazryd2";
            this.colrazryd2.Visible = true;
            this.colrazryd2.VisibleIndex = 2;
            this.colrazryd2.Width = 53;
            // 
            // coltext2
            // 
            this.coltext2.Caption = "наименование операции комплектовки";
            this.coltext2.FieldName = "Text";
            this.coltext2.Name = "coltext2";
            this.coltext2.Visible = true;
            this.coltext2.VisibleIndex = 3;
            this.coltext2.Width = 303;
            // 
            // colsek3
            // 
            this.colsek3.Caption = "сек.";
            this.colsek3.FieldName = "Sek";
            this.colsek3.Name = "colsek3";
            this.colsek3.Visible = true;
            this.colsek3.VisibleIndex = 4;
            this.colsek3.Width = 60;
            // 
            // colannId5
            // 
            this.colannId5.FieldName = "annId";
            this.colannId5.Name = "colannId5";
            this.colannId5.Width = 65;
            // 
            // gridControlDopObrTW
            // 
            this.gridControlDopObrTW.DataSource = this.normdopobrBindingSource;
            this.gridControlDopObrTW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDopObrTW.Location = new System.Drawing.Point(3, 643);
            this.gridControlDopObrTW.MainView = this.gridView5;
            this.gridControlDopObrTW.Name = "gridControlDopObrTW";
            this.gridControlDopObrTW.Size = new System.Drawing.Size(361, 73);
            this.gridControlDopObrTW.TabIndex = 9;
            this.gridControlDopObrTW.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colsek_p,
            this.colsek_p_tamp,
            this.colsek_v,
            this.colsek_stra,
            this.colannId6});
            this.gridView5.GridControl = this.gridControlDopObrTW;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView5_FocusedRowChanged);
            // 
            // colsek_p
            // 
            this.colsek_p.Caption = "принт (сек.)";
            this.colsek_p.FieldName = "SekP";
            this.colsek_p.Name = "colsek_p";
            this.colsek_p.Visible = true;
            this.colsek_p.VisibleIndex = 0;
            // 
            // colsek_p_tamp
            // 
            this.colsek_p_tamp.Caption = "тамп. печать(сек)";
            this.colsek_p_tamp.FieldName = "SekTamp";
            this.colsek_p_tamp.Name = "colsek_p_tamp";
            this.colsek_p_tamp.Visible = true;
            this.colsek_p_tamp.VisibleIndex = 1;
            // 
            // colsek_v
            // 
            this.colsek_v.Caption = "вышивка+пайетки (сек)";
            this.colsek_v.FieldName = "SekV";
            this.colsek_v.Name = "colsek_v";
            this.colsek_v.Visible = true;
            this.colsek_v.VisibleIndex = 2;
            // 
            // colsek_stra
            // 
            this.colsek_stra.Caption = "стразы (сек)";
            this.colsek_stra.FieldName = "SekStra";
            this.colsek_stra.Name = "colsek_stra";
            this.colsek_stra.Visible = true;
            this.colsek_stra.VisibleIndex = 3;
            // 
            // colannId6
            // 
            this.colannId6.FieldName = "annId";
            this.colannId6.Name = "colannId6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 10F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.label8.Location = new System.Drawing.Point(3, 719);
            this.label8.Name = "label8";
            this.label8.ObjectName = null;
            this.label8.Size = new System.Drawing.Size(149, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Особенности модели";
            // 
            // commentRichTextBox
            // 
            this.commentRichTextBox.Location = new System.Drawing.Point(3, 742);
            this.commentRichTextBox.Name = "commentRichTextBox";
            this.commentRichTextBox.Size = new System.Drawing.Size(353, 60);
            this.commentRichTextBox.TabIndex = 5;
            this.commentRichTextBox.Text = "";
            // 
            // xtraTabPageArticles
            // 
            this.xtraTabPageArticles.Controls.Add(this.xtraTabControl2);
            this.xtraTabPageArticles.Name = "xtraTabPageArticles";
            this.xtraTabPageArticles.Size = new System.Drawing.Size(1516, 811);
            this.xtraTabPageArticles.Text = "2. Текущие работы";
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl2.Appearance.Options.UseBackColor = true;
            this.xtraTabControl2.AppearancePage.HeaderDisabled.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl2.AppearancePage.HeaderDisabled.Options.UseBackColor = true;
            this.xtraTabControl2.AppearancePage.HeaderHotTracked.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl2.AppearancePage.HeaderHotTracked.Options.UseBackColor = true;
            this.xtraTabControl2.AppearancePage.PageClient.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabControl2.AppearancePage.PageClient.Options.UseBackColor = true;
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl2.LookAndFeel.SkinMaskColor = System.Drawing.Color.Transparent;
            this.xtraTabControl2.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Transparent;
            this.xtraTabControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPageWorkDivisions;
            this.xtraTabControl2.Size = new System.Drawing.Size(1516, 811);
            this.xtraTabControl2.TabIndex = 0;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageWorkDivisions,
            this.xtraTabPage3});
            // 
            // xtraTabPageWorkDivisions
            // 
            this.xtraTabPageWorkDivisions.Appearance.PageClient.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabPageWorkDivisions.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPageWorkDivisions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xtraTabPageWorkDivisions.Controls.Add(this.panelControl2);
            this.xtraTabPageWorkDivisions.Name = "xtraTabPageWorkDivisions";
            this.xtraTabPageWorkDivisions.Size = new System.Drawing.Size(1514, 786);
            this.xtraTabPageWorkDivisions.Text = "Требуют увязки";
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.Controls.Add(this.tablePanel2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1514, 786);
            this.panelControl2.TabIndex = 4;
            // 
            // tablePanel2
            // 
            this.tablePanel2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel2.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.tablePanel2.Appearance.Options.UseBackColor = true;
            this.tablePanel2.Appearance.Options.UseBorderColor = true;
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 41.7F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 103F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 57.4F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 42.6F)});
            this.tablePanel2.Controls.Add(this.customGridControl3);
            this.tablePanel2.Controls.Add(this.panelControl6);
            this.tablePanel2.Controls.Add(this.panelControl5);
            this.tablePanel2.Controls.Add(this.panelControl4);
            this.tablePanel2.Controls.Add(this.panelControl3);
            this.tablePanel2.Controls.Add(this.splitContainerControl3);
            this.tablePanel2.Controls.Add(this.customGridControl1);
            this.tablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel2.Location = new System.Drawing.Point(2, 2);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 285F)});
            this.tablePanel2.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel2.Size = new System.Drawing.Size(1510, 782);
            this.tablePanel2.TabIndex = 3;
            this.tablePanel2.UseSkinIndents = true;
            // 
            // customGridControl3
            // 
            this.customGridControl3.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.tablePanel2.SetColumn(this.customGridControl3, 3);
            this.customGridControl3.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl3.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControl3.Location = new System.Drawing.Point(1085, 38);
            this.customGridControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.customGridControl3.MainView = this.gridView6;
            this.customGridControl3.Name = "customGridControl3";
            this.customGridControl3.ObjectName = null;
            this.tablePanel2.SetRow(this.customGridControl3, 1);
            this.customGridControl3.Size = new System.Drawing.Size(412, 731);
            this.customGridControl3.TabIndex = 3;
            this.customGridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView6});
            // 
            // gridView6
            // 
            this.gridView6.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
            this.gridView6.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colannId1,
            this.coln3,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.gridView6.GridControl = this.customGridControl3;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // colannId1
            // 
            this.colannId1.FieldName = "annId";
            this.colannId1.Name = "colannId1";
            this.colannId1.Width = 89;
            // 
            // coln3
            // 
            this.coln3.Caption = "№оп.";
            this.coln3.FieldName = "N";
            this.coln3.Name = "coln3";
            this.coln3.Visible = true;
            this.coln3.VisibleIndex = 0;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "№п/оп.";
            this.gridColumn9.FieldName = "N1";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            this.gridColumn9.Width = 87;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Разряд";
            this.gridColumn10.FieldName = "Razryad";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            this.gridColumn10.Width = 112;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Наименование операции пошива";
            this.gridColumn11.FieldName = "Text";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            this.gridColumn11.Width = 602;
            // 
            // panelControl6
            // 
            this.tablePanel2.SetColumn(this.panelControl6, 1);
            this.panelControl6.Location = new System.Drawing.Point(421, 12);
            this.panelControl6.Name = "panelControl6";
            this.tablePanel2.SetRow(this.panelControl6, 0);
            this.panelControl6.Size = new System.Drawing.Size(99, 22);
            this.panelControl6.TabIndex = 12;
            // 
            // panelControl5
            // 
            this.tablePanel2.SetColumn(this.panelControl5, 2);
            this.panelControl5.Controls.Add(this.loadAllCheckBox);
            this.panelControl5.Controls.Add(this.customLabel5);
            this.panelControl5.Location = new System.Drawing.Point(524, 12);
            this.panelControl5.Name = "panelControl5";
            this.tablePanel2.SetRow(this.panelControl5, 0);
            this.panelControl5.Size = new System.Drawing.Size(557, 22);
            this.panelControl5.TabIndex = 11;
            // 
            // loadAllCheckBox
            // 
            this.loadAllCheckBox.AutoSize = true;
            this.loadAllCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            this.loadAllCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.loadAllCheckBox.Location = new System.Drawing.Point(158, 4);
            this.loadAllCheckBox.Name = "loadAllCheckBox";
            this.loadAllCheckBox.ObjectName = null;
            this.loadAllCheckBox.Size = new System.Drawing.Size(133, 20);
            this.loadAllCheckBox.TabIndex = 9;
            this.loadAllCheckBox.Text = "Показать все РТ";
            this.loadAllCheckBox.UseVisualStyleBackColor = true;
            this.loadAllCheckBox.CheckedChanged += new System.EventHandler(this.customCheckBox4_CheckedChanged);
            // 
            // customLabel5
            // 
            this.customLabel5.AutoSize = true;
            this.customLabel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.customLabel5.Font = new System.Drawing.Font("Arial", 10F);
            this.customLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customLabel5.Location = new System.Drawing.Point(5, 4);
            this.customLabel5.Name = "customLabel5";
            this.customLabel5.ObjectName = null;
            this.customLabel5.Size = new System.Drawing.Size(98, 16);
            this.customLabel5.TabIndex = 8;
            this.customLabel5.Text = "РТ для увязки";
            // 
            // panelControl4
            // 
            this.tablePanel2.SetColumn(this.panelControl4, 0);
            this.panelControl4.Controls.Add(this.customLabel4);
            this.panelControl4.Location = new System.Drawing.Point(13, 12);
            this.panelControl4.Name = "panelControl4";
            this.tablePanel2.SetRow(this.panelControl4, 0);
            this.panelControl4.Size = new System.Drawing.Size(404, 22);
            this.panelControl4.TabIndex = 9;
            // 
            // customLabel4
            // 
            this.customLabel4.AutoSize = true;
            this.customLabel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.customLabel4.Font = new System.Drawing.Font("Arial", 10F);
            this.customLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customLabel4.Location = new System.Drawing.Point(5, 4);
            this.customLabel4.Name = "customLabel4";
            this.customLabel4.ObjectName = null;
            this.customLabel4.Size = new System.Drawing.Size(144, 16);
            this.customLabel4.TabIndex = 7;
            this.customLabel4.Text = "Артикулы для увязки";
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.tablePanel2.SetColumn(this.panelControl3, 1);
            this.panelControl3.ContentImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelControl3.Controls.Add(this.customLabel2);
            this.panelControl3.Controls.Add(this.customLabel1);
            this.panelControl3.Controls.Add(this.simpleButton2);
            this.panelControl3.Controls.Add(this.simpleButton1);
            this.panelControl3.Controls.Add(this.actualCheckBox1);
            this.panelControl3.Controls.Add(this.preliminaryCheckBox1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(421, 38);
            this.panelControl3.Name = "panelControl3";
            this.tablePanel2.SetRow(this.panelControl3, 1);
            this.panelControl3.Size = new System.Drawing.Size(99, 731);
            this.panelControl3.TabIndex = 0;
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Font = new System.Drawing.Font("Arial", 10F);
            this.customLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customLabel2.Location = new System.Drawing.Point(9, 104);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.ObjectName = null;
            this.customLabel2.Size = new System.Drawing.Size(70, 16);
            this.customLabel2.TabIndex = 10;
            this.customLabel2.Text = "Добавить";
            this.customLabel2.Visible = false;
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Arial", 10F);
            this.customLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customLabel1.Location = new System.Drawing.Point(20, 47);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.ObjectName = null;
            this.customLabel1.Size = new System.Drawing.Size(59, 16);
            this.customLabel1.TabIndex = 9;
            this.customLabel1.Text = "Увязать";
            // 
            // simpleButton2
            // 
            this.simpleButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton2.Location = new System.Drawing.Point(10, 123);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Visible = false;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.Location = new System.Drawing.Point(10, 66);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Click += new System.EventHandler(this.BindButton_Click);
            // 
            // actualCheckBox1
            // 
            this.actualCheckBox1.AutoSize = true;
            this.actualCheckBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.actualCheckBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.actualCheckBox1.Location = new System.Drawing.Point(26, 39);
            this.actualCheckBox1.Name = "actualCheckBox1";
            this.actualCheckBox1.ObjectName = null;
            this.actualCheckBox1.Size = new System.Drawing.Size(105, 20);
            this.actualCheckBox1.TabIndex = 5;
            this.actualCheckBox1.Text = "Актуальные";
            this.actualCheckBox1.UseVisualStyleBackColor = true;
            this.actualCheckBox1.Visible = false;
            this.actualCheckBox1.CheckedChanged += new System.EventHandler(this.customCheckBox6_CheckedChanged);
            // 
            // preliminaryCheckBox1
            // 
            this.preliminaryCheckBox1.AutoSize = true;
            this.preliminaryCheckBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.preliminaryCheckBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.preliminaryCheckBox1.Location = new System.Drawing.Point(8, 9);
            this.preliminaryCheckBox1.Name = "preliminaryCheckBox1";
            this.preliminaryCheckBox1.ObjectName = null;
            this.preliminaryCheckBox1.Size = new System.Drawing.Size(147, 20);
            this.preliminaryCheckBox1.TabIndex = 4;
            this.preliminaryCheckBox1.Text = "Предварительные";
            this.preliminaryCheckBox1.UseVisualStyleBackColor = true;
            this.preliminaryCheckBox1.Visible = false;
            this.preliminaryCheckBox1.CheckedChanged += new System.EventHandler(this.customCheckBox6_CheckedChanged);
            // 
            // splitContainerControl3
            // 
            this.tablePanel2.SetColumn(this.splitContainerControl3, 2);
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Horizontal = false;
            this.splitContainerControl3.Location = new System.Drawing.Point(524, 38);
            this.splitContainerControl3.Name = "splitContainerControl3";
            // 
            // splitContainerControl3.Panel1
            // 
            this.splitContainerControl3.Panel1.Controls.Add(this.customGridControl2);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            // 
            // splitContainerControl3.Panel2
            // 
            this.splitContainerControl3.Panel2.Controls.Add(this.customGridControl6);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.tablePanel2.SetRow(this.splitContainerControl3, 1);
            this.splitContainerControl3.Size = new System.Drawing.Size(557, 731);
            this.splitContainerControl3.SplitterPosition = 463;
            this.splitContainerControl3.TabIndex = 6;
            // 
            // customGridControl2
            // 
            this.customGridControl2.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridControl2.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl2.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControl2.Location = new System.Drawing.Point(0, 0);
            this.customGridControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.customGridControl2.MainView = this.gridView8;
            this.customGridControl2.Margin = new System.Windows.Forms.Padding(0);
            this.customGridControl2.Name = "customGridControl2";
            this.customGridControl2.ObjectName = null;
            this.customGridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit3,
            this.repositoryItemCheckEdit4,
            this.repositoryItemCheckEdit6,
            this.repositoryItemCheckEdit7});
            this.customGridControl2.Size = new System.Drawing.Size(557, 463);
            this.customGridControl2.TabIndex = 0;
            this.customGridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView8});
            // 
            // gridView8
            // 
            this.gridView8.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Yellow;
            this.gridView8.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn12,
            this.colarticul1,
            this.gridColumn13,
            this.colannId7,
            this.gridColumn8,
            this.colstatus1,
            this.gridColumn29,
            this.gridColumn28,
            this.gridColumn6});
            this.gridView8.GridControl = this.customGridControl2;
            this.gridView8.Name = "gridView8";
            this.gridView8.OptionsCustomization.AllowColumnMoving = false;
            this.gridView8.OptionsFilter.AllowMRUFilterList = false;
            this.gridView8.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            this.gridView8.OptionsMenu.EnableColumnMenu = false;
            this.gridView8.OptionsView.ShowAutoFilterRow = true;
            this.gridView8.OptionsView.ShowGroupPanel = false;
            this.gridView8.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView8_FocusedRowChanged);
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "код";
            this.gridColumn12.FieldName = "Kod";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            this.gridColumn12.Width = 110;
            // 
            // colarticul1
            // 
            this.colarticul1.Caption = "артикул";
            this.colarticul1.FieldName = "Articul";
            this.colarticul1.Name = "colarticul1";
            this.colarticul1.Visible = true;
            this.colarticul1.VisibleIndex = 2;
            this.colarticul1.Width = 68;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = " ";
            this.gridColumn13.ColumnEdit = this.repositoryItemCheckEdit3;
            this.gridColumn13.FieldName = "IsChecked";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.FixedWidth = true;
            this.gridColumn13.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn13.OptionsFilter.AllowFilter = false;
            this.gridColumn13.OptionsFilter.ShowEmptyDateFilter = false;
            this.gridColumn13.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;
            this.gridColumn13.UnboundDataType = typeof(bool);
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            this.gridColumn13.Width = 24;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            this.repositoryItemCheckEdit3.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit3.ValueGrayed = false;
            // 
            // colannId7
            // 
            this.colannId7.FieldName = "AnnId";
            this.colannId7.Name = "colannId7";
            this.colannId7.Visible = true;
            this.colannId7.VisibleIndex = 3;
            this.colannId7.Width = 88;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "status";
            this.gridColumn8.FieldName = "Status";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // colstatus1
            // 
            this.colstatus1.Caption = "статус";
            this.colstatus1.FieldName = "Stat";
            this.colstatus1.Name = "colstatus1";
            this.colstatus1.Visible = true;
            this.colstatus1.VisibleIndex = 5;
            this.colstatus1.Width = 64;
            // 
            // gridColumn29
            // 
            this.gridColumn29.Caption = "группа";
            this.gridColumn29.FieldName = "Group";
            this.gridColumn29.Name = "gridColumn29";
            this.gridColumn29.Visible = true;
            this.gridColumn29.VisibleIndex = 4;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "модель";
            this.gridColumn28.FieldName = "Model";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 6;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "gridColumn6";
            this.gridColumn6.FieldName = "_isChecked";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            this.repositoryItemCheckEdit4.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // repositoryItemCheckEdit6
            // 
            this.repositoryItemCheckEdit6.AutoHeight = false;
            this.repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            // 
            // repositoryItemCheckEdit7
            // 
            this.repositoryItemCheckEdit7.AutoHeight = false;
            this.repositoryItemCheckEdit7.Name = "repositoryItemCheckEdit7";
            // 
            // customGridControl6
            // 
            this.customGridControl6.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customGridControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl6.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControl6.Location = new System.Drawing.Point(0, 0);
            this.customGridControl6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.customGridControl6.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl6.MainView = this.gridView12;
            this.customGridControl6.Name = "customGridControl6";
            this.customGridControl6.ObjectName = null;
            this.customGridControl6.Size = new System.Drawing.Size(557, 258);
            this.customGridControl6.TabIndex = 0;
            this.customGridControl6.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView12});
            // 
            // gridView12
            // 
            this.gridView12.GridControl = this.customGridControl6;
            this.gridView12.Name = "gridView12";
            // 
            // customGridControl1
            // 
            this.customGridControl1.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.tablePanel2.SetColumn(this.customGridControl1, 0);
            this.customGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            this.customGridControl1.Location = new System.Drawing.Point(13, 38);
            this.customGridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.customGridControl1.MainView = this.gridView7;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.ObjectName = null;
            this.tablePanel2.SetRow(this.customGridControl1, 1);
            this.customGridControl1.Size = new System.Drawing.Size(404, 731);
            this.customGridControl1.TabIndex = 4;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView7,
            this.gridView11});
            // 
            // gridView7
            // 
            this.gridView7.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
            this.gridView7.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView7.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.код,
            this.артикул,
            this.gridColumn2,
            this.gridColumn7,
            this.группа,
            this.модель,
            this.gridColumn4});
            this.gridView7.GridControl = this.customGridControl1;
            this.gridView7.Name = "gridView7";
            this.gridView7.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.False;
            this.gridView7.OptionsDetail.EnableMasterViewMode = false;
            this.gridView7.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView7.OptionsView.ShowGroupPanel = false;
            this.gridView7.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.gridView7.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView7_FocusedRowChanged);
            // 
            // код
            // 
            this.код.Caption = "код";
            this.код.FieldName = "Kod";
            this.код.Name = "код";
            this.код.Visible = true;
            this.код.VisibleIndex = 0;
            this.код.Width = 42;
            // 
            // артикул
            // 
            this.артикул.Caption = "артикул";
            this.артикул.FieldName = "Articul";
            this.артикул.Name = "артикул";
            this.артикул.Visible = true;
            this.артикул.VisibleIndex = 1;
            this.артикул.Width = 65;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = " ";
            this.gridColumn2.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn2.FieldName = "IsChecked";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.OptionsFilter.AllowInHeaderSearch = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.UnboundDataType = typeof(bool);
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 27;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ранее увязанные";
            this.gridColumn7.ColumnEdit = this.repositoryItemButtonEdit2;
            this.gridColumn7.FieldName = "BindedArt";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 111;
            // 
            // группа
            // 
            this.группа.Caption = "группа";
            this.группа.FieldName = "Group";
            this.группа.Name = "группа";
            this.группа.Visible = true;
            this.группа.VisibleIndex = 3;
            // 
            // модель
            // 
            this.модель.Caption = "модель";
            this.модель.FieldName = "Mod";
            this.модель.Name = "модель";
            this.модель.Visible = true;
            this.модель.VisibleIndex = 4;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.FieldName = "_isChecked";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridView11
            // 
            this.gridView11.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn19,
            this.gridColumn24,
            this.gridColumn25});
            this.gridView11.GridControl = this.customGridControl1;
            this.gridView11.Name = "gridView11";
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "gridColumn19";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 0;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "gridColumn24";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 1;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "gridColumn25";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 2;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Appearance.Header.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabPage3.Appearance.Header.ForeColor = System.Drawing.Color.Black;
            this.xtraTabPage3.Appearance.Header.Options.UseBackColor = true;
            this.xtraTabPage3.Appearance.Header.Options.UseForeColor = true;
            this.xtraTabPage3.Appearance.HeaderActive.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabPage3.Appearance.HeaderActive.Options.UseBackColor = true;
            this.xtraTabPage3.Appearance.HeaderHotTracked.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabPage3.Appearance.HeaderHotTracked.Options.UseBackColor = true;
            this.xtraTabPage3.Appearance.PageClient.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabPage3.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPage3.Controls.Add(this.splitContainerControl2);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1514, 786);
            this.xtraTabPage3.Text = "Предварительный архив";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.customButton2);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Controls.Add(this.flyoutPanel1);
            this.splitContainerControl2.Panel2.Controls.Add(this.gridControlPreArch);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1514, 786);
            this.splitContainerControl2.SplitterPosition = 122;
            this.splitContainerControl2.TabIndex = 2;
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.customButton2.FlatAppearance.BorderSize = 0;
            this.customButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton2.Font = new System.Drawing.Font("Arial", 12F);
            this.customButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(90)))), ((int)(((byte)(205)))));
            this.customButton2.Location = new System.Drawing.Point(10, 19);
            this.customButton2.Name = "customButton2";
            this.customButton2.ObjectName = null;
            this.customButton2.Size = new System.Drawing.Size(101, 30);
            this.customButton2.TabIndex = 1;
            this.customButton2.Text = "В архив";
            this.customButton2.UseVisualStyleBackColor = false;
            this.customButton2.Click += new System.EventHandler(this.customButton2_Click);
            // 
            // flyoutPanel1
            // 
            this.flyoutPanel1.Controls.Add(this.flyoutPanelControl1);
            this.flyoutPanel1.Location = new System.Drawing.Point(692, 218);
            this.flyoutPanel1.Name = "flyoutPanel1";
            this.flyoutPanel1.Size = new System.Drawing.Size(381, 254);
            this.flyoutPanel1.TabIndex = 1;
            // 
            // flyoutPanelControl1
            // 
            this.flyoutPanelControl1.Controls.Add(this.customCancelButton1);
            this.flyoutPanelControl1.Controls.Add(this.customComboBox1);
            this.flyoutPanelControl1.Controls.Add(this.customButton1);
            this.flyoutPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl1.FlyoutPanel = this.flyoutPanel1;
            this.flyoutPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.flyoutPanelControl1.Name = "flyoutPanelControl1";
            this.flyoutPanelControl1.Size = new System.Drawing.Size(381, 254);
            this.flyoutPanelControl1.TabIndex = 0;
            // 
            // customCancelButton1
            // 
            this.customCancelButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customCancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.customCancelButton1.Font = new System.Drawing.Font("Arial", 10F);
            this.customCancelButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customCancelButton1.Location = new System.Drawing.Point(276, 33);
            this.customCancelButton1.Name = "customCancelButton1";
            this.customCancelButton1.ObjectName = null;
            this.customCancelButton1.Size = new System.Drawing.Size(75, 25);
            this.customCancelButton1.TabIndex = 2;
            this.customCancelButton1.Text = "customCancelButton1";
            this.customCancelButton1.UseVisualStyleBackColor = false;
            // 
            // customComboBox1
            // 
            this.customComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.customComboBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.customComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.customComboBox1.FormattingEnabled = true;
            this.customComboBox1.Location = new System.Drawing.Point(218, 125);
            this.customComboBox1.Name = "customComboBox1";
            this.customComboBox1.ObjectName = null;
            this.customComboBox1.Size = new System.Drawing.Size(121, 24);
            this.customComboBox1.TabIndex = 1;
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.customButton1.Font = new System.Drawing.Font("Arial", 10F);
            this.customButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.customButton1.Location = new System.Drawing.Point(52, 34);
            this.customButton1.Name = "customButton1";
            this.customButton1.ObjectName = null;
            this.customButton1.Size = new System.Drawing.Size(75, 25);
            this.customButton1.TabIndex = 0;
            this.customButton1.Text = "customButton1";
            this.customButton1.UseVisualStyleBackColor = false;
            // 
            // gridControlPreArch
            // 
            this.gridControlPreArch.AlternateRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(196)))));
            this.gridControlPreArch.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControlPreArch.DataSource = this.artnormnBindingSource1;
            this.gridControlPreArch.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridControlPreArch.Font = new System.Drawing.Font("Arial", 10F);
            this.gridControlPreArch.Location = new System.Drawing.Point(0, 0);
            this.gridControlPreArch.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.gridControlPreArch.MainView = this.gridViewPreArch;
            this.gridControlPreArch.Name = "gridControlPreArch";
            this.gridControlPreArch.ObjectName = null;
            this.gridControlPreArch.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit5});
            this.gridControlPreArch.Size = new System.Drawing.Size(604, 786);
            this.gridControlPreArch.TabIndex = 0;
            this.gridControlPreArch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPreArch});
            // 
            // gridViewPreArch
            // 
            this.gridViewPreArch.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
            this.gridViewPreArch.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewPreArch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn16,
            this.gridColumn15,
            this.gridColumn17});
            this.gridViewPreArch.GridControl = this.gridControlPreArch;
            this.gridViewPreArch.Name = "gridViewPreArch";
            this.gridViewPreArch.OptionsView.RowAutoHeight = true;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "код";
            this.gridColumn14.FieldName = "Kod";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 0;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "артикул";
            this.gridColumn16.FieldName = "Articul";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 1;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = " ";
            this.gridColumn15.ColumnEdit = this.repositoryItemCheckEdit5;
            this.gridColumn15.FieldName = "IsChecked";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.UnboundDataType = typeof(bool);
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit5
            // 
            this.repositoryItemCheckEdit5.AutoHeight = false;
            this.repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "наличие НЗП шт.";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 3;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // TeamWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1518, 836);
            this.Controls.Add(this.xtraTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TeamWork";
            this.Text = "Нормативные расценки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeamWork_FormClosing);
            this.Load += new System.EventHandler(this.TeamWorkForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            this.panelControl7.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ANNgridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ANNgridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView14)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRaszTW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normraszBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRaskrTW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normraskBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlKontTW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normkontBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDopObrTW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normdopobrBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            this.xtraTabPageArticles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabPageWorkDivisions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3.Panel1)).EndInit();
            this.splitContainerControl3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3.Panel2)).EndInit();
            this.splitContainerControl3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            this.splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            this.splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).EndInit();
            this.flyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).EndInit();
            this.flyoutPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPreArch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.artnormnBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPreArch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normraszBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.artnormnBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sparticulBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.artnormnBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sparticulBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.constrBindingSource)).EndInit();
            this.ResumeLayout(false);

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
        private CustomGridControl customGridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn colannId1;
        private CustomGridControl customGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
        private DevExpress.XtraGrid.Columns.GridColumn код;
        private DevExpress.XtraGrid.Columns.GridColumn артикул;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private CustomGridControl customGridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView8;
        private DevExpress.XtraGrid.Columns.GridColumn colarticul1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn colannId7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit7;
        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private CustomCheckBox preliminaryCheckBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DevExpress.XtraGrid.Columns.GridColumn colstatus1;
        private BindingSource artnormnBindingSource2;
        RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
        private CustomCheckBox actualCheckBox1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraGrid.Columns.GridColumn coln3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private CustomLabel customLabel2;
        private CustomLabel customLabel1;
        private CustomLabel customLabel5;
        private CustomLabel customLabel4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private PanelControl panelControl2;
        private PanelControl panelControl6;
        private PanelControl panelControl5;
        private PanelControl panelControl4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private DevExpress.XtraGrid.Columns.GridColumn группа;
        private DevExpress.XtraGrid.Columns.GridColumn модель;
        private CustomGridControl customGridControl6;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private CustomCheckBox loadAllCheckBox;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView12;
        private BindingSource artnormnBindingSource;
        private BindingSource artnormnBindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
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
        private DevExpress.XtraGrid.GridControl ANNgridControl;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private CustomGridControl customGridControl5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView10;
        private DevExpress.XtraGrid.Columns.GridColumn kodd_rt;
        private DevExpress.XtraGrid.Columns.GridColumn colannId2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn kolNZP;
        private DevExpress.XtraGrid.Views.Grid.GridView ANNgridView;
        private DevExpress.XtraGrid.Columns.GridColumn colgroup;
        private DevExpress.XtraGrid.Columns.GridColumn colarticul;
        private DevExpress.XtraGrid.Columns.GridColumn colmod;
        private DevExpress.XtraGrid.Columns.GridColumn colsek;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz;
        private DevExpress.XtraGrid.Columns.GridColumn coldateCreate;
        private DevExpress.XtraGrid.Columns.GridColumn coldateUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_shv;
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
        private DevExpress.XtraGrid.Columns.GridColumn coldiz;
        private DevExpress.XtraGrid.Columns.GridColumn colconstr;
        private DevExpress.XtraGrid.Columns.GridColumn colannID;
        private RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private CustomButton customButton12;
        private CustomTextBox filterTextBox1;
        private RadioButton model;
        private RadioButton articul;
        private RadioButton kode;
        private RadioButton group;
        private TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraGrid.GridControl gridControlRaszTW;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn coln;
        private DevExpress.XtraGrid.Columns.GridColumn coln1;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd;
        private DevExpress.XtraGrid.Columns.GridColumn coltext;
        private DevExpress.XtraGrid.Columns.GridColumn colsek1;
        private DevExpress.XtraGrid.Columns.GridColumn colobor;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o;
        private DevExpress.XtraGrid.Columns.GridColumn colannId3;
        private DevExpress.XtraGrid.GridControl gridControlRaskrTW;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colid;
        private DevExpress.XtraGrid.Columns.GridColumn colkod2;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd1;
        private DevExpress.XtraGrid.Columns.GridColumn coltext1;
        private DevExpress.XtraGrid.Columns.GridColumn colsek2;
        private DevExpress.XtraGrid.Columns.GridColumn colannId4;
        private DevExpress.XtraGrid.GridControl gridControlKontTW;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd2;
        private DevExpress.XtraGrid.Columns.GridColumn coltext2;
        private DevExpress.XtraGrid.Columns.GridColumn colsek3;
        private DevExpress.XtraGrid.Columns.GridColumn colannId5;
        private DevExpress.XtraGrid.GridControl gridControlDopObrTW;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_p;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_p_tamp;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_v;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_stra;
        private DevExpress.XtraGrid.Columns.GridColumn colannId6;
        private CustomLabel label8;
        private RichTextBox commentRichTextBox;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView13;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView14;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView15;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.Utils.FlyoutPanel flyoutPanel1;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private CustomCancelButton customCancelButton1;
        private CustomComboBox customComboBox1;
        private CustomButton customButton1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private CustomSimpleButton ButtonUnboundWd;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private CustomLabel customLabel3;
        private ErrorProvider errorProvider1;
        private DevExpress.XtraGrid.Columns.GridColumn PztCount;
        private BindingSource desBindingSource;
        private BindingSource constrBindingSource;
        private CustomTextBox constructorTextBox;
        private CustomTextBox designerTextBox;
    }
}
