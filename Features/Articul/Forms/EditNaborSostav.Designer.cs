using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    partial class EditNaborSostav
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditNaborSostav));
            dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            customTextBoxKodGost_Old = new SewingProduction.Core.Class.CustomTextBox();
            customTextBoxOpiGost_Old = new SewingProduction.Core.Class.CustomTextBox();
            customLabel4 = new SewingProduction.Core.Class.CustomLabel();
            cLabelInfo = new SewingProduction.Core.Class.CustomLabel();
            customLabel3 = new SewingProduction.Core.Class.CustomLabel();
            customLabel2 = new SewingProduction.Core.Class.CustomLabel();
            customLabel1 = new SewingProduction.Core.Class.CustomLabel();
            customCheckBoxVerified = new CustomCheckBox();
            customSearchLookUpEditGrupN = new SewingProduction.Core.Class.CustomSearchLookUpEdit();
            _bsGrupForNabor = new BindingSource(components);
            customSearchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            customSearchLookUpEditGostN = new SewingProduction.Core.Class.CustomSearchLookUpEdit();
            _bsGostForNabor = new BindingSource(components);
            customSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            customButtonSaveNabor = new SewingProduction.Core.Class.CustomButton();
            customGridControlNabor = new SewingProduction.Core.Class.CustomGridControl();
            spArticulNaborSostavBindingSource = new BindingSource(components);
            GridViewNabor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            BandRazmAll1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            colAns_id1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colKod1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTa_id1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTk_id1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTxt_v1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTk_name1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colId_gost1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            repositoryItemSearchLookUpEditGost = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            _bsGostForSostav = new BindingSource(components);
            repositoryItemSearchLookUpEditGostView = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            colName_gost = new DevExpress.XtraGrid.Columns.GridColumn();
            colOpi_gost = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            colAg_id1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colN_i1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            repositoryItemSearchLookUpEditGrup = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            _bsGrupForSostav = new BindingSource(components);
            repositoryItemSearchLookUpEditGrupView = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            colAg_name_sokr = new DevExpress.XtraGrid.Columns.GridColumn();
            colAg_tnved = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            colN_g = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            colSostav1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colId_razm_nab1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colRazm1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colrazm_all1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            customTextBoxArtN = new SewingProduction.Core.Class.CustomTextBox();
            customTextBoxGrupN_Old = new SewingProduction.Core.Class.CustomTextBox();
            customTextBoxGostN_Old = new SewingProduction.Core.Class.CustomTextBox();
            customTextBoxArtN_Old = new SewingProduction.Core.Class.CustomTextBox();
            customPictureBoxNabor = new SewingProduction.Core.Class.CustomPictureBox();
            customGridControlNabor_Old = new SewingProduction.Core.Class.CustomGridControl();
            GridViewNabor_Old = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            BandRazmAll = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            colAns_id = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colKod = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTa_id = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTk_id = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTxt_v = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colTk_name = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colId_gost = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colAg_id = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colN_i = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colSostav = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colId_razm_nab = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colRazm = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            colRazm_all = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            customTextBoxId_razm_nab = new SewingProduction.Core.Class.CustomTextBox();
            customTextBoxKod = new SewingProduction.Core.Class.CustomTextBox();
            customTextBoxAns_id = new SewingProduction.Core.Class.CustomTextBox();
            layoutControlItemKod = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItemId_razm_nab = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItemAns_id = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroupDataNabor = new DevExpress.XtraLayout.LayoutControlGroup();
            Артикул = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ГОСТ = new DevExpress.XtraLayout.LayoutControlItem();
            Группа = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroupDataNaborOld = new DevExpress.XtraLayout.LayoutControlGroup();
            Артикул_Old = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItemPicture = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            tvnModelBindingSource = new BindingSource(components);
            assortModelBindingSource = new BindingSource(components);
            tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            popupTrigger = new PopupContainerEdit();
            customListBoxRazm = new SewingProduction.Core.Class.CustomListBox();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).BeginInit();
            dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEditGrupN.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_bsGrupForNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEdit2View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEditGostN.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_bsGostForNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEdit1View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spArticulNaborSostavBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridViewNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSearchLookUpEditGost).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_bsGostForSostav).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSearchLookUpEditGostView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSearchLookUpEditGrup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_bsGrupForSostav).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSearchLookUpEditGrupView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customPictureBoxNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlNabor_Old).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridViewNabor_Old).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemKod).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemId_razm_nab).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemAns_id).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupDataNabor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Артикул).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ГОСТ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Группа).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupDataNaborOld).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Артикул_Old).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tvnModelBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)assortModelBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabbedControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)popupTrigger.Properties).BeginInit();
            SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            dataLayoutControl1.Controls.Add(customTextBoxKodGost_Old);
            dataLayoutControl1.Controls.Add(customTextBoxOpiGost_Old);
            dataLayoutControl1.Controls.Add(customLabel4);
            dataLayoutControl1.Controls.Add(cLabelInfo);
            dataLayoutControl1.Controls.Add(customLabel3);
            dataLayoutControl1.Controls.Add(customLabel2);
            dataLayoutControl1.Controls.Add(customLabel1);
            dataLayoutControl1.Controls.Add(customCheckBoxVerified);
            dataLayoutControl1.Controls.Add(customSearchLookUpEditGrupN);
            dataLayoutControl1.Controls.Add(customSearchLookUpEditGostN);
            dataLayoutControl1.Controls.Add(customButtonSaveNabor);
            dataLayoutControl1.Controls.Add(customGridControlNabor);
            dataLayoutControl1.Controls.Add(customTextBoxArtN);
            dataLayoutControl1.Controls.Add(customTextBoxGrupN_Old);
            dataLayoutControl1.Controls.Add(customTextBoxGostN_Old);
            dataLayoutControl1.Controls.Add(customTextBoxArtN_Old);
            dataLayoutControl1.Controls.Add(customPictureBoxNabor);
            dataLayoutControl1.Controls.Add(customGridControlNabor_Old);
            dataLayoutControl1.Controls.Add(customTextBoxId_razm_nab);
            dataLayoutControl1.Controls.Add(customTextBoxKod);
            dataLayoutControl1.Controls.Add(customTextBoxAns_id);
            dataLayoutControl1.Dock = DockStyle.Fill;
            dataLayoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItemKod, layoutControlItemId_razm_nab, layoutControlItemAns_id, layoutControlGroup2 });
            dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            dataLayoutControl1.Name = "dataLayoutControl1";
            dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-674, 100, 650, 816);
            dataLayoutControl1.Root = Root;
            dataLayoutControl1.Size = new System.Drawing.Size(1195, 586);
            dataLayoutControl1.TabIndex = 0;
            dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // customTextBoxKodGost_Old
            // 
            customTextBoxKodGost_Old.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxKodGost_Old.ErrorColor = System.Drawing.Color.Red;
            customTextBoxKodGost_Old.ErrorMessage = null;
            customTextBoxKodGost_Old.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxKodGost_Old.Location = new System.Drawing.Point(121, 70);
            customTextBoxKodGost_Old.Name = "customTextBoxKodGost_Old";
            customTextBoxKodGost_Old.ReadOnly = true;
            customTextBoxKodGost_Old.Size = new System.Drawing.Size(276, 20);
            customTextBoxKodGost_Old.TabIndex = 2;
            // 
            // customTextBoxOpiGost_Old
            // 
            customTextBoxOpiGost_Old.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxOpiGost_Old.ErrorColor = System.Drawing.Color.Red;
            customTextBoxOpiGost_Old.ErrorMessage = null;
            customTextBoxOpiGost_Old.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxOpiGost_Old.Location = new System.Drawing.Point(121, 118);
            customTextBoxOpiGost_Old.Name = "customTextBoxOpiGost_Old";
            customTextBoxOpiGost_Old.ReadOnly = true;
            customTextBoxOpiGost_Old.Size = new System.Drawing.Size(276, 20);
            customTextBoxOpiGost_Old.TabIndex = 4;
            // 
            // customLabel4
            // 
            customLabel4.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customLabel4.Appearance.Options.UseFont = true;
            customLabel4.Appearance.Options.UseTextOptions = true;
            customLabel4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel4.AutoSizeMode = LabelAutoSizeMode.None;
            customLabel4.Location = new System.Drawing.Point(614, 264);
            customLabel4.Name = "customLabel4";
            customLabel4.Size = new System.Drawing.Size(67, 34);
            customLabel4.StyleController = dataLayoutControl1;
            customLabel4.TabIndex = 1;
            customLabel4.Text = "Состояние:";
            customLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cLabelInfo
            // 
            cLabelInfo.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            cLabelInfo.Appearance.Options.UseFont = true;
            cLabelInfo.Appearance.Options.UseTextOptions = true;
            cLabelInfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            cLabelInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            cLabelInfo.Location = new System.Drawing.Point(685, 264);
            cLabelInfo.Name = "cLabelInfo";
            cLabelInfo.Size = new System.Drawing.Size(486, 34);
            cLabelInfo.StyleController = dataLayoutControl1;
            cLabelInfo.TabIndex = 1;
            cLabelInfo.Text = "Нет изменений в составе";
            cLabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabel3
            // 
            customLabel3.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customLabel3.Appearance.Options.UseFont = true;
            customLabel3.Appearance.Options.UseTextOptions = true;
            customLabel3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel3.AutoSizeMode = LabelAutoSizeMode.None;
            customLabel3.Location = new System.Drawing.Point(614, 162);
            customLabel3.Name = "customLabel3";
            customLabel3.Size = new System.Drawing.Size(557, 16);
            customLabel3.StyleController = dataLayoutControl1;
            customLabel3.TabIndex = 1;
            customLabel3.Text = "Для изменения размера набора кликните на строку группировки";
            customLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabel2
            // 
            customLabel2.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customLabel2.Appearance.Options.UseFont = true;
            customLabel2.Appearance.Options.UseTextOptions = true;
            customLabel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel2.AutoSizeMode = LabelAutoSizeMode.None;
            customLabel2.Location = new System.Drawing.Point(614, 142);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new System.Drawing.Size(557, 16);
            customLabel2.StyleController = dataLayoutControl1;
            customLabel2.TabIndex = 1;
            customLabel2.Text = "При изменении ГОСТа набора, данные состава могут измениться, необходимо проверить корректность данных в таблице ниже.";
            customLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabel1
            // 
            customLabel1.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customLabel1.Appearance.Options.UseFont = true;
            customLabel1.Appearance.Options.UseTextOptions = true;
            customLabel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            customLabel1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            customLabel1.Location = new System.Drawing.Point(614, 122);
            customLabel1.Name = "customLabel1";
            customLabel1.Size = new System.Drawing.Size(557, 16);
            customLabel1.StyleController = dataLayoutControl1;
            customLabel1.TabIndex = 1;
            customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customCheckBoxVerified
            // 
            customCheckBoxVerified.Font = new System.Drawing.Font("Arial", 10F);
            customCheckBoxVerified.Location = new System.Drawing.Point(614, 532);
            customCheckBoxVerified.Name = "customCheckBoxVerified";
            customCheckBoxVerified.RightToLeft = RightToLeft.No;
            customCheckBoxVerified.Size = new System.Drawing.Size(266, 30);
            customCheckBoxVerified.TabIndex = 11;
            customCheckBoxVerified.Text = "Все проверено, набор корректен";
            customCheckBoxVerified.UseVisualStyleBackColor = true;
            customCheckBoxVerified.CheckedChanged += customCheckBoxVerified_CheckedChanged;
            // 
            // customSearchLookUpEditGrupN
            // 
            customSearchLookUpEditGrupN.Location = new System.Drawing.Point(711, 96);
            customSearchLookUpEditGrupN.Name = "customSearchLookUpEditGrupN";
            customSearchLookUpEditGrupN.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSearchLookUpEditGrupN.Properties.Appearance.Options.UseFont = true;
            customSearchLookUpEditGrupN.Properties.Appearance.Options.UseForeColor = true;
            customSearchLookUpEditGrupN.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            customSearchLookUpEditGrupN.Properties.DataSource = _bsGrupForNabor;
            customSearchLookUpEditGrupN.Properties.DisplayMember = "Ag_name_sokr";
            customSearchLookUpEditGrupN.Properties.NullText = "";
            customSearchLookUpEditGrupN.Properties.PopupView = customSearchLookUpEdit2View;
            customSearchLookUpEditGrupN.Properties.ValueMember = "Ag_id";
            customSearchLookUpEditGrupN.Size = new System.Drawing.Size(460, 22);
            customSearchLookUpEditGrupN.StyleController = dataLayoutControl1;
            customSearchLookUpEditGrupN.TabIndex = 9;
            // 
            // _bsGrupForNabor
            // 
            _bsGrupForNabor.DataSource = typeof(Core.Models.GostGrupIzdViewModel);
            // 
            // customSearchLookUpEdit2View
            // 
            customSearchLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn5, gridColumn6, gridColumn7, gridColumn8, gridColumn9, gridColumn10 });
            customSearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            customSearchLookUpEdit2View.Name = "customSearchLookUpEdit2View";
            customSearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            customSearchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gcCertMod1
            // 
            gridColumn5.Caption = "ГОСТ";
            gridColumn5.FieldName = "Id_gost";
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 0;
            gridColumn5.Width = 111;
            // 
            // gcCertTm_name1
            // 
            gridColumn6.Caption = "№ группы";
            gridColumn6.FieldName = "Ag_id";
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Visible = true;
            gridColumn6.VisibleIndex = 1;
            gridColumn6.Width = 120;
            // 
            // gcCertGrup1
            // 
            gridColumn7.FieldName = "Ag_name_sokr";
            gridColumn7.Name = "gridColumn7";
            gridColumn7.Width = 193;
            // 
            // gcCertText_mo1
            // 
            gridColumn8.Caption = "ТНВД";
            gridColumn8.FieldName = "Ag_tnved";
            gridColumn8.Name = "gridColumn8";
            gridColumn8.Visible = true;
            gridColumn8.VisibleIndex = 3;
            gridColumn8.Width = 363;
            // 
            // gridColumn9
            // 
            gridColumn9.Caption = "Название";
            gridColumn9.FieldName = "N_i";
            gridColumn9.Name = "gridColumn9";
            gridColumn9.Visible = true;
            gridColumn9.VisibleIndex = 2;
            gridColumn9.Width = 357;
            // 
            // gridColumn10
            // 
            gridColumn10.FieldName = "N_g";
            gridColumn10.Name = "gridColumn10";
            gridColumn10.Width = 200;
            // 
            // customSearchLookUpEditGostN
            // 
            customSearchLookUpEditGostN.Location = new System.Drawing.Point(711, 70);
            customSearchLookUpEditGostN.Name = "customSearchLookUpEditGostN";
            customSearchLookUpEditGostN.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSearchLookUpEditGostN.Properties.Appearance.Options.UseFont = true;
            customSearchLookUpEditGostN.Properties.Appearance.Options.UseForeColor = true;
            customSearchLookUpEditGostN.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            customSearchLookUpEditGostN.Properties.DataSource = _bsGostForNabor;
            customSearchLookUpEditGostN.Properties.DisplayMember = "Name_gost";
            customSearchLookUpEditGostN.Properties.NullText = "";
            customSearchLookUpEditGostN.Properties.PopupView = customSearchLookUpEdit1View;
            customSearchLookUpEditGostN.Properties.ValueMember = "Id_gost";
            customSearchLookUpEditGostN.Size = new System.Drawing.Size(460, 22);
            customSearchLookUpEditGostN.StyleController = dataLayoutControl1;
            customSearchLookUpEditGostN.TabIndex = 8;
            customSearchLookUpEditGostN.EditValueChanged += customSearchLookUpEditGostN_EditValueChanged;
            // 
            // _bsGostForNabor
            // 
            _bsGostForNabor.DataSource = typeof(Core.Models.GostGrupIzdViewModel);
            // 
            // customSearchLookUpEdit1View
            // 
            customSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn11, gridColumn12, gridColumn13 });
            customSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            customSearchLookUpEdit1View.Name = "customSearchLookUpEdit1View";
            customSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            customSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn11
            // 
            gridColumn11.Caption = "ГОСТ";
            gridColumn11.FieldName = "Id_gost";
            gridColumn11.Name = "gridColumn11";
            gridColumn11.Visible = true;
            gridColumn11.VisibleIndex = 0;
            gridColumn11.Width = 160;
            // 
            // gridColumn12
            // 
            gridColumn12.Caption = "Имя ГОСТа";
            gridColumn12.FieldName = "Name_gost";
            gridColumn12.Name = "gridColumn12";
            gridColumn12.Visible = true;
            gridColumn12.VisibleIndex = 1;
            gridColumn12.Width = 395;
            // 
            // gridColumn13
            // 
            gridColumn13.Caption = "Описание";
            gridColumn13.FieldName = "Opi_gost";
            gridColumn13.Name = "gridColumn13";
            gridColumn13.Visible = true;
            gridColumn13.VisibleIndex = 2;
            gridColumn13.Width = 396;
            // 
            // customButtonSaveNabor
            // 
            customButtonSaveNabor.Font = new System.Drawing.Font("Arial", 10F);
            customButtonSaveNabor.Location = new System.Drawing.Point(884, 532);
            customButtonSaveNabor.MinimumSize = new System.Drawing.Size(250, 30);
            customButtonSaveNabor.Name = "customButtonSaveNabor";
            customButtonSaveNabor.Size = new System.Drawing.Size(287, 30);
            customButtonSaveNabor.TabIndex = 12;
            customButtonSaveNabor.Text = "Сохранить";
            customButtonSaveNabor.UseVisualStyleBackColor = false;
            customButtonSaveNabor.Click += customButtonSaveNabor_Click;
            // 
            // customGridControlNabor
            // 
            customGridControlNabor.DataSource = spArticulNaborSostavBindingSource;
            customGridControlNabor.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlNabor.Location = new System.Drawing.Point(614, 302);
            customGridControlNabor.MainView = GridViewNabor;
            customGridControlNabor.Name = "customGridControlNabor";
            customGridControlNabor.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemSearchLookUpEditGost, repositoryItemSearchLookUpEditGrup });
            customGridControlNabor.Size = new System.Drawing.Size(557, 226);
            customGridControlNabor.TabIndex = 10;
            customGridControlNabor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { GridViewNabor });
            // 
            // spArticulNaborSostavBindingSource
            // 
            spArticulNaborSostavBindingSource.DataSource = typeof(Models.SpArticulNaborSostav);
            // 
            // GridViewNabor
            // 
            GridViewNabor.ActiveFilterEnabled = false;
            GridViewNabor.Appearance.EvenRow.Options.UseBackColor = true;
            GridViewNabor.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            GridViewNabor.Appearance.FocusedRow.Options.UseBackColor = true;
            GridViewNabor.Appearance.FocusedRow.Options.UseFont = true;
            GridViewNabor.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { BandRazmAll1 });
            GridViewNabor.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { colAns_id1, colKod1, colTa_id1, colTk_id1, colId_gost1, colAg_id1, colSostav1, colId_razm_nab1, colRazm1, colTxt_v1, colTk_name1, colN_i1, colrazm_all1 });
            GridViewNabor.GridControl = customGridControlNabor;
            GridViewNabor.Name = "GridViewNabor";
            GridViewNabor.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            GridViewNabor.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            GridViewNabor.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            GridViewNabor.OptionsFind.AllowFindPanel = false;
            GridViewNabor.OptionsPrint.PrintBandHeader = false;
            GridViewNabor.OptionsPrint.PrintFooter = false;
            GridViewNabor.OptionsView.EnableAppearanceEvenRow = true;
            GridViewNabor.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            GridViewNabor.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(colrazm_all1, DevExpress.Data.ColumnSortOrder.Ascending) });
            GridViewNabor.FocusedRowChanged += gridViewNabor_FocusedRowChanged;
            GridViewNabor.CellValueChanged += GridViewNabor_CellValueChanged;
            GridViewNabor.MouseDown += GridViewNabor_MouseDown;
            // 
            // BandRazmAll1
            // 
            BandRazmAll1.Caption = "Размер набора";
            BandRazmAll1.Columns.Add(colAns_id1);
            BandRazmAll1.Columns.Add(colKod1);
            BandRazmAll1.Columns.Add(colTa_id1);
            BandRazmAll1.Columns.Add(colTk_id1);
            BandRazmAll1.Columns.Add(colTxt_v1);
            BandRazmAll1.Columns.Add(colTk_name1);
            BandRazmAll1.Columns.Add(colId_gost1);
            BandRazmAll1.Columns.Add(colAg_id1);
            BandRazmAll1.Columns.Add(colN_i1);
            BandRazmAll1.Columns.Add(colSostav1);
            BandRazmAll1.Columns.Add(colId_razm_nab1);
            BandRazmAll1.Columns.Add(colRazm1);
            BandRazmAll1.Columns.Add(colrazm_all1);
            BandRazmAll1.Name = "BandRazmAll1";
            BandRazmAll1.VisibleIndex = 0;
            BandRazmAll1.Width = 612;
            // 
            // colAns_id1
            // 
            colAns_id1.Caption = "Ид";
            colAns_id1.FieldName = "Ans_id";
            colAns_id1.Name = "colAns_id1";
            colAns_id1.OptionsColumn.ReadOnly = true;
            // 
            // colKod1
            // 
            colKod1.Caption = "Код";
            colKod1.FieldName = "Kod";
            colKod1.Name = "colKod1";
            colKod1.OptionsColumn.ReadOnly = true;
            // 
            // colTa_id1
            // 
            colTa_id1.FieldName = "Ta_id";
            colTa_id1.Name = "colTa_id1";
            colTa_id1.OptionsColumn.ReadOnly = true;
            // 
            // colTk_id1
            // 
            colTk_id1.FieldName = "Tk_id";
            colTk_id1.Name = "colTk_id1";
            colTk_id1.OptionsColumn.ReadOnly = true;
            // 
            // colTxt_v1
            // 
            colTxt_v1.Caption = "Ассортимент";
            colTxt_v1.FieldName = "Txt_v";
            colTxt_v1.MinWidth = 100;
            colTxt_v1.Name = "colTxt_v1";
            colTxt_v1.OptionsColumn.ReadOnly = true;
            colTxt_v1.Visible = true;
            colTxt_v1.Width = 100;
            // 
            // colTk_name1
            // 
            colTk_name1.Caption = "Часть в наборе";
            colTk_name1.FieldName = "Tk_name";
            colTk_name1.MinWidth = 120;
            colTk_name1.Name = "colTk_name1";
            colTk_name1.OptionsColumn.ReadOnly = true;
            colTk_name1.Visible = true;
            colTk_name1.Width = 120;
            // 
            // colId_gost1
            // 
            colId_gost1.Caption = "ГОСТ";
            colId_gost1.ColumnEdit = repositoryItemSearchLookUpEditGost;
            colId_gost1.FieldName = "Id_gost";
            colId_gost1.MinWidth = 50;
            colId_gost1.Name = "colId_gost1";
            colId_gost1.Visible = true;
            // 
            // repositoryItemSearchLookUpEditGost
            // 
            repositoryItemSearchLookUpEditGost.AutoHeight = false;
            repositoryItemSearchLookUpEditGost.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemSearchLookUpEditGost.DataSource = _bsGostForSostav;
            repositoryItemSearchLookUpEditGost.DisplayMember = "Id_gost";
            repositoryItemSearchLookUpEditGost.Name = "repositoryItemSearchLookUpEditGost";
            repositoryItemSearchLookUpEditGost.NullText = "";
            repositoryItemSearchLookUpEditGost.PopupView = repositoryItemSearchLookUpEditGostView;
            repositoryItemSearchLookUpEditGost.ValueMember = "Id_gost";
            repositoryItemSearchLookUpEditGost.CloseUp += repositoryItemSearchLookUpEditGost_CloseUp;
            repositoryItemSearchLookUpEditGost.Popup += repositoryItemSearchLookUpEditGost_Popup;
            // 
            // _bsGostForSostav
            // 
            _bsGostForSostav.DataSource = typeof(Core.Models.GostModel);
            // 
            // repositoryItemSearchLookUpEditGostView
            // 
            repositoryItemSearchLookUpEditGostView.ActiveFilterEnabled = false;
            repositoryItemSearchLookUpEditGostView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn4, colName_gost, colOpi_gost, gridColumn14 });
            repositoryItemSearchLookUpEditGostView.DetailHeight = 100;
            repositoryItemSearchLookUpEditGostView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            repositoryItemSearchLookUpEditGostView.Name = "repositoryItemSearchLookUpEditGostView";
            repositoryItemSearchLookUpEditGostView.OptionsCustomization.AllowFilter = false;
            repositoryItemSearchLookUpEditGostView.OptionsCustomization.AllowGroup = false;
            repositoryItemSearchLookUpEditGostView.OptionsCustomization.AllowSort = false;
            repositoryItemSearchLookUpEditGostView.OptionsDetail.EnableMasterViewMode = false;
            repositoryItemSearchLookUpEditGostView.OptionsFilter.AllowFilterEditor = false;
            repositoryItemSearchLookUpEditGostView.OptionsFind.AllowFindInExpandedDetails = DevExpress.Utils.DefaultBoolean.False;
            repositoryItemSearchLookUpEditGostView.OptionsFind.AllowFindPanel = false;
            repositoryItemSearchLookUpEditGostView.OptionsFind.ClearFindOnClose = false;
            repositoryItemSearchLookUpEditGostView.OptionsFind.FindDelay = 100;
            repositoryItemSearchLookUpEditGostView.OptionsFind.FindFilterColumns = "";
            repositoryItemSearchLookUpEditGostView.OptionsFind.FindNullPrompt = "";
            repositoryItemSearchLookUpEditGostView.OptionsFind.ShowClearButton = false;
            repositoryItemSearchLookUpEditGostView.OptionsFind.ShowCloseButton = false;
            repositoryItemSearchLookUpEditGostView.OptionsFind.ShowFindButton = false;
            repositoryItemSearchLookUpEditGostView.OptionsFind.ShowSearchNavButtons = false;
            repositoryItemSearchLookUpEditGostView.OptionsMenu.EnableColumnMenu = false;
            repositoryItemSearchLookUpEditGostView.OptionsMenu.EnableFooterMenu = false;
            repositoryItemSearchLookUpEditGostView.OptionsMenu.EnableGroupPanelMenu = false;
            repositoryItemSearchLookUpEditGostView.OptionsSelection.EnableAppearanceFocusedCell = false;
            repositoryItemSearchLookUpEditGostView.OptionsView.ShowDetailButtons = false;
            repositoryItemSearchLookUpEditGostView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            repositoryItemSearchLookUpEditGostView.OptionsView.ShowGroupPanel = false;
            // 
            // gcCertArticul1
            // 
            gridColumn4.Caption = "ГОСТ";
            gridColumn4.FieldName = "Id_gost";
            gridColumn4.Name = "gridColumn4";
            gridColumn4.OptionsFilter.AllowAutoFilter = false;
            gridColumn4.OptionsFilter.AllowFilter = false;
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 0;
            gridColumn4.Width = 124;
            // 
            // colName_gost
            // 
            colName_gost.Caption = "Название";
            colName_gost.FieldName = "Name_gost";
            colName_gost.Name = "colName_gost";
            colName_gost.OptionsFilter.AllowAutoFilter = false;
            colName_gost.OptionsFilter.AllowFilter = false;
            colName_gost.Visible = true;
            colName_gost.VisibleIndex = 1;
            colName_gost.Width = 277;
            // 
            // colOpi_gost
            // 
            colOpi_gost.Caption = "Описание";
            colOpi_gost.FieldName = "Opi_gost";
            colOpi_gost.Name = "colOpi_gost";
            colOpi_gost.OptionsFilter.AllowAutoFilter = false;
            colOpi_gost.OptionsFilter.AllowFilter = false;
            colOpi_gost.Visible = true;
            colOpi_gost.VisibleIndex = 2;
            colOpi_gost.Width = 550;
            // 
            // gridColumn14
            // 
            gridColumn14.Caption = "Tk_id";
            gridColumn14.FieldName = "Tk_id";
            gridColumn14.Name = "gridColumn14";
            gridColumn14.OptionsFilter.AllowAutoFilter = false;
            gridColumn14.OptionsFilter.AllowFilter = false;
            // 
            // colAg_id1
            // 
            colAg_id1.Caption = "№ группы";
            colAg_id1.FieldName = "Ag_id";
            colAg_id1.MinWidth = 75;
            colAg_id1.Name = "colAg_id1";
            colAg_id1.OptionsColumn.ReadOnly = true;
            colAg_id1.Visible = true;
            // 
            // colN_i1
            // 
            colN_i1.Caption = "Группа по ГОСТ";
            colN_i1.ColumnEdit = repositoryItemSearchLookUpEditGrup;
            colN_i1.FieldName = "Ag_id";
            colN_i1.MinWidth = 90;
            colN_i1.Name = "colN_i1";
            colN_i1.Visible = true;
            colN_i1.Width = 102;
            // 
            // repositoryItemSearchLookUpEditGrup
            // 
            repositoryItemSearchLookUpEditGrup.AutoHeight = false;
            repositoryItemSearchLookUpEditGrup.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            repositoryItemSearchLookUpEditGrup.DataSource = _bsGrupForSostav;
            repositoryItemSearchLookUpEditGrup.DisplayMember = "N_i";
            repositoryItemSearchLookUpEditGrup.Name = "repositoryItemSearchLookUpEditGrup";
            repositoryItemSearchLookUpEditGrup.NullText = "";
            repositoryItemSearchLookUpEditGrup.PopupView = repositoryItemSearchLookUpEditGrupView;
            repositoryItemSearchLookUpEditGrup.ValueMember = "Ag_id";
            repositoryItemSearchLookUpEditGrup.CloseUp += repositoryItemSearchLookUpEditGrup_CloseUp;
            repositoryItemSearchLookUpEditGrup.Popup += repositoryItemSearchLookUpEditGrup_Popup;
            // 
            // _bsGrupForSostav
            // 
            _bsGrupForSostav.DataSource = typeof(Core.Models.GostGrupIzdViewModel);
            // 
            // repositoryItemSearchLookUpEditGrupView
            // 
            repositoryItemSearchLookUpEditGrupView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1, gridColumn2, colAg_name_sokr, colAg_tnved, gridColumn3, colN_g, gridColumn15 });
            repositoryItemSearchLookUpEditGrupView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            repositoryItemSearchLookUpEditGrupView.Name = "repositoryItemSearchLookUpEditGrupView";
            repositoryItemSearchLookUpEditGrupView.OptionsCustomization.AllowFilter = false;
            repositoryItemSearchLookUpEditGrupView.OptionsCustomization.AllowGroup = false;
            repositoryItemSearchLookUpEditGrupView.OptionsCustomization.AllowSort = false;
            repositoryItemSearchLookUpEditGrupView.OptionsMenu.EnableColumnMenu = false;
            repositoryItemSearchLookUpEditGrupView.OptionsMenu.EnableFooterMenu = false;
            repositoryItemSearchLookUpEditGrupView.OptionsMenu.EnableGroupPanelMenu = false;
            repositoryItemSearchLookUpEditGrupView.OptionsSelection.EnableAppearanceFocusedCell = false;
            repositoryItemSearchLookUpEditGrupView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            repositoryItemSearchLookUpEditGrupView.OptionsView.ShowGroupPanel = false;
            repositoryItemSearchLookUpEditGrupView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(colN_g, DevExpress.Data.ColumnSortOrder.Descending) });
            // 
            // gcCertGrupmen_name1
            // 
            gridColumn1.Caption = "ГОСТ";
            gridColumn1.FieldName = "Id_gost";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Width = 107;
            // 
            // gcCertTsn_name1
            // 
            gridColumn2.Caption = "Группа";
            gridColumn2.FieldName = "Ag_id";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.Width = 118;
            // 
            // colAg_name_sokr
            // 
            colAg_name_sokr.FieldName = "Ag_name_sokr";
            colAg_name_sokr.Name = "colAg_name_sokr";
            // 
            // colAg_tnved
            // 
            colAg_tnved.Caption = "ТНВД";
            colAg_tnved.FieldName = "Ag_tnved";
            colAg_tnved.Name = "colAg_tnved";
            colAg_tnved.Visible = true;
            colAg_tnved.VisibleIndex = 3;
            colAg_tnved.Width = 237;
            // 
            // gcCertTb_id1
            // 
            gridColumn3.Caption = "Название";
            gridColumn3.FieldName = "N_i";
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 2;
            gridColumn3.Width = 489;
            // 
            // colN_g
            // 
            colN_g.FieldName = "N_g";
            colN_g.Name = "colN_g";
            // 
            // gridColumn15
            // 
            gridColumn15.Caption = "tk_id";
            gridColumn15.FieldName = "Tk_id";
            gridColumn15.Name = "gridColumn15";
            // 
            // colSostav1
            // 
            colSostav1.Caption = "Состав";
            colSostav1.FieldName = "Sostav";
            colSostav1.MinWidth = 70;
            colSostav1.Name = "colSostav1";
            colSostav1.Visible = true;
            colSostav1.Width = 70;
            // 
            // colId_razm_nab1
            // 
            colId_razm_nab1.Caption = "Ид размер";
            colId_razm_nab1.FieldName = "Id_razm_nab";
            colId_razm_nab1.Name = "colId_razm_nab1";
            colId_razm_nab1.OptionsColumn.ReadOnly = true;
            // 
            // colRazm1
            // 
            colRazm1.Caption = "Размер";
            colRazm1.FieldName = "Razm";
            colRazm1.MinWidth = 70;
            colRazm1.Name = "colRazm1";
            colRazm1.OptionsColumn.ReadOnly = true;
            colRazm1.Visible = true;
            colRazm1.Width = 70;
            // 
            // colrazm_all1
            // 
            colrazm_all1.Caption = "Размер набора";
            colrazm_all1.FieldName = "Razm_all";
            colrazm_all1.Name = "colrazm_all1";
            colrazm_all1.OptionsColumn.ReadOnly = true;
            colrazm_all1.Width = 90;
            // 
            // customTextBoxArtN
            // 
            customTextBoxArtN.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxArtN.ErrorColor = System.Drawing.Color.Red;
            customTextBoxArtN.ErrorMessage = null;
            customTextBoxArtN.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxArtN.Location = new System.Drawing.Point(711, 46);
            customTextBoxArtN.Name = "customTextBoxArtN";
            customTextBoxArtN.Size = new System.Drawing.Size(460, 20);
            customTextBoxArtN.TabIndex = 7;
            // 
            // customTextBoxGrupN_Old
            // 
            customTextBoxGrupN_Old.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxGrupN_Old.ErrorColor = System.Drawing.Color.Red;
            customTextBoxGrupN_Old.ErrorMessage = null;
            customTextBoxGrupN_Old.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxGrupN_Old.Location = new System.Drawing.Point(121, 142);
            customTextBoxGrupN_Old.Name = "customTextBoxGrupN_Old";
            customTextBoxGrupN_Old.ReadOnly = true;
            customTextBoxGrupN_Old.Size = new System.Drawing.Size(276, 20);
            customTextBoxGrupN_Old.TabIndex = 5;
            // 
            // customTextBoxGostN_Old
            // 
            customTextBoxGostN_Old.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxGostN_Old.ErrorColor = System.Drawing.Color.Red;
            customTextBoxGostN_Old.ErrorMessage = null;
            customTextBoxGostN_Old.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxGostN_Old.Location = new System.Drawing.Point(121, 94);
            customTextBoxGostN_Old.Name = "customTextBoxGostN_Old";
            customTextBoxGostN_Old.ReadOnly = true;
            customTextBoxGostN_Old.Size = new System.Drawing.Size(276, 20);
            customTextBoxGostN_Old.TabIndex = 3;
            // 
            // customTextBoxArtN_Old
            // 
            customTextBoxArtN_Old.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxArtN_Old.ErrorColor = System.Drawing.Color.Red;
            customTextBoxArtN_Old.ErrorMessage = null;
            customTextBoxArtN_Old.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxArtN_Old.Location = new System.Drawing.Point(121, 46);
            customTextBoxArtN_Old.Name = "customTextBoxArtN_Old";
            customTextBoxArtN_Old.ReadOnly = true;
            customTextBoxArtN_Old.Size = new System.Drawing.Size(276, 20);
            customTextBoxArtN_Old.TabIndex = 0;
            // 
            // customPictureBoxNabor
            // 
            customPictureBoxNabor.BackColor = System.Drawing.SystemColors.Control;
            customPictureBoxNabor.Font = new System.Drawing.Font("Arial", 10F);
            customPictureBoxNabor.ForeColor = System.Drawing.SystemColors.ControlText;
            customPictureBoxNabor.ImagePath = null;
            customPictureBoxNabor.Location = new System.Drawing.Point(401, 46);
            customPictureBoxNabor.Name = "customPictureBoxNabor";
            customPictureBoxNabor.Size = new System.Drawing.Size(175, 179);
            customPictureBoxNabor.SizeMode = PictureBoxSizeMode.Zoom;
            customPictureBoxNabor.TabIndex = 1;
            customPictureBoxNabor.TabStop = false;
            // 
            // customGridControlNabor_Old
            // 
            customGridControlNabor_Old.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlNabor_Old.Location = new System.Drawing.Point(24, 261);
            customGridControlNabor_Old.MainView = GridViewNabor_Old;
            customGridControlNabor_Old.Name = "customGridControlNabor_Old";
            customGridControlNabor_Old.Size = new System.Drawing.Size(552, 260);
            customGridControlNabor_Old.TabIndex = 6;
            customGridControlNabor_Old.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { GridViewNabor_Old });
            customGridControlNabor_Old.Load += customGridControl1_Load;
            // 
            // GridViewNabor_Old
            // 
            GridViewNabor_Old.Appearance.EvenRow.Options.UseBackColor = true;
            GridViewNabor_Old.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            GridViewNabor_Old.Appearance.FocusedRow.Options.UseBackColor = true;
            GridViewNabor_Old.Appearance.FocusedRow.Options.UseFont = true;
            GridViewNabor_Old.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { BandRazmAll });
            GridViewNabor_Old.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { colAns_id, colKod, colTa_id, colTk_id, colId_gost, colAg_id, colSostav, colId_razm_nab, colRazm, colTxt_v, colTk_name, colN_i, colRazm_all });
            GridViewNabor_Old.GridControl = customGridControlNabor_Old;
            GridViewNabor_Old.Name = "GridViewNabor_Old";
            GridViewNabor_Old.OptionsBehavior.Editable = false;
            GridViewNabor_Old.OptionsBehavior.ReadOnly = true;
            GridViewNabor_Old.OptionsCustomization.AllowRowSizing = true;
            GridViewNabor_Old.OptionsView.EnableAppearanceEvenRow = true;
            GridViewNabor_Old.OptionsView.RowAutoHeight = true;
            GridViewNabor_Old.FocusedRowChanged += gridViewNabor_FocusedRowChanged;
            // 
            // BandRazmAll
            // 
            BandRazmAll.Caption = "Размер набора";
            BandRazmAll.Columns.Add(colAns_id);
            BandRazmAll.Columns.Add(colKod);
            BandRazmAll.Columns.Add(colTa_id);
            BandRazmAll.Columns.Add(colTk_id);
            BandRazmAll.Columns.Add(colTxt_v);
            BandRazmAll.Columns.Add(colTk_name);
            BandRazmAll.Columns.Add(colId_gost);
            BandRazmAll.Columns.Add(colAg_id);
            BandRazmAll.Columns.Add(colN_i);
            BandRazmAll.Columns.Add(colSostav);
            BandRazmAll.Columns.Add(colId_razm_nab);
            BandRazmAll.Columns.Add(colRazm);
            BandRazmAll.Name = "BandRazmAll";
            BandRazmAll.VisibleIndex = 0;
            BandRazmAll.Width = 616;
            // 
            // colAns_id
            // 
            colAns_id.Caption = "Ид";
            colAns_id.FieldName = "Ans_id";
            colAns_id.Name = "colAns_id";
            colAns_id.OptionsColumn.ReadOnly = true;
            colAns_id.Width = 28;
            // 
            // colKod
            // 
            colKod.Caption = "Код";
            colKod.FieldName = "Kod";
            colKod.Name = "colKod";
            colKod.OptionsColumn.ReadOnly = true;
            colKod.Width = 33;
            // 
            // colTa_id
            // 
            colTa_id.FieldName = "Ta_id";
            colTa_id.Name = "colTa_id";
            colTa_id.OptionsColumn.ReadOnly = true;
            // 
            // colTk_id
            // 
            colTk_id.FieldName = "Tk_id";
            colTk_id.Name = "colTk_id";
            colTk_id.OptionsColumn.ReadOnly = true;
            // 
            // colTxt_v
            // 
            colTxt_v.Caption = "Ассортимент";
            colTxt_v.FieldName = "Txt_v";
            colTxt_v.MinWidth = 100;
            colTxt_v.Name = "colTxt_v";
            colTxt_v.OptionsColumn.ReadOnly = true;
            colTxt_v.Visible = true;
            colTxt_v.Width = 100;
            // 
            // colTk_name
            // 
            colTk_name.Caption = "Часть в наборе";
            colTk_name.FieldName = "Tk_name";
            colTk_name.MinWidth = 110;
            colTk_name.Name = "colTk_name";
            colTk_name.OptionsColumn.ReadOnly = true;
            colTk_name.Visible = true;
            colTk_name.Width = 110;
            // 
            // colId_gost
            // 
            colId_gost.Caption = "ГОСТ";
            colId_gost.FieldName = "Id_gost";
            colId_gost.MinWidth = 50;
            colId_gost.Name = "colId_gost";
            colId_gost.OptionsColumn.ReadOnly = true;
            colId_gost.Visible = true;
            colId_gost.Width = 74;
            // 
            // colAg_id
            // 
            colAg_id.Caption = "№ группы";
            colAg_id.FieldName = "Ag_id";
            colAg_id.MinWidth = 75;
            colAg_id.Name = "colAg_id";
            colAg_id.OptionsColumn.ReadOnly = true;
            colAg_id.Visible = true;
            // 
            // colN_i
            // 
            colN_i.Caption = "Группа по ГОСТ";
            colN_i.FieldName = "N_i";
            colN_i.MinWidth = 90;
            colN_i.Name = "colN_i";
            colN_i.OptionsColumn.ReadOnly = true;
            colN_i.Visible = true;
            colN_i.Width = 117;
            // 
            // colSostav
            // 
            colSostav.Caption = "Состав";
            colSostav.FieldName = "Sostav";
            colSostav.MinWidth = 70;
            colSostav.Name = "colSostav";
            colSostav.OptionsColumn.ReadOnly = true;
            colSostav.Visible = true;
            colSostav.Width = 70;
            // 
            // colId_razm_nab
            // 
            colId_razm_nab.Caption = "Ид размера";
            colId_razm_nab.FieldName = "Id_razm_nab";
            colId_razm_nab.Name = "colId_razm_nab";
            colId_razm_nab.OptionsColumn.ReadOnly = true;
            colId_razm_nab.Width = 33;
            // 
            // colRazm
            // 
            colRazm.Caption = "Размер";
            colRazm.FieldName = "Razm";
            colRazm.MinWidth = 70;
            colRazm.Name = "colRazm";
            colRazm.OptionsColumn.ReadOnly = true;
            colRazm.Visible = true;
            colRazm.Width = 70;
            // 
            // colRazm_all
            // 
            colRazm_all.Caption = "Размер набора";
            colRazm_all.FieldName = "Razm_all";
            colRazm_all.MinWidth = 75;
            colRazm_all.Name = "colRazm_all";
            colRazm_all.OptionsColumn.ReadOnly = true;
            colRazm_all.Visible = true;
            // 
            // customTextBoxId_razm_nab
            // 
            customTextBoxId_razm_nab.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxId_razm_nab.DataBindings.Add(new Binding("Text", spArticulNaborSostavBindingSource, "Id_razm_nab", true));
            customTextBoxId_razm_nab.ErrorColor = System.Drawing.Color.Red;
            customTextBoxId_razm_nab.ErrorMessage = null;
            customTextBoxId_razm_nab.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxId_razm_nab.Location = new System.Drawing.Point(747, 173);
            customTextBoxId_razm_nab.Name = "customTextBoxId_razm_nab";
            customTextBoxId_razm_nab.Size = new System.Drawing.Size(394, 20);
            customTextBoxId_razm_nab.TabIndex = 1;
            // 
            // customTextBoxKod
            // 
            customTextBoxKod.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxKod.DataBindings.Add(new Binding("Text", spArticulNaborSostavBindingSource, "Kod", true));
            customTextBoxKod.ErrorColor = System.Drawing.Color.Red;
            customTextBoxKod.ErrorMessage = null;
            customTextBoxKod.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxKod.Location = new System.Drawing.Point(747, 45);
            customTextBoxKod.Name = "customTextBoxKod";
            customTextBoxKod.Size = new System.Drawing.Size(394, 20);
            customTextBoxKod.TabIndex = 1;
            // 
            // customTextBoxAns_id
            // 
            customTextBoxAns_id.BorderStyle = BorderStyle.FixedSingle;
            customTextBoxAns_id.DataBindings.Add(new Binding("Text", spArticulNaborSostavBindingSource, "Ans_id", true));
            customTextBoxAns_id.ErrorColor = System.Drawing.Color.Red;
            customTextBoxAns_id.ErrorMessage = null;
            customTextBoxAns_id.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxAns_id.Location = new System.Drawing.Point(787, 46);
            customTextBoxAns_id.Name = "customTextBoxAns_id";
            customTextBoxAns_id.Size = new System.Drawing.Size(82, 20);
            customTextBoxAns_id.TabIndex = 1;
            // 
            // layoutControlItemKod
            // 
            layoutControlItemKod.Control = customTextBoxKod;
            layoutControlItemKod.Location = new System.Drawing.Point(0, 0);
            layoutControlItemKod.Name = "layoutControlItemKod";
            layoutControlItemKod.Size = new System.Drawing.Size(536, 24);
            layoutControlItemKod.Text = "Код артикула";
            layoutControlItemKod.TextSize = new System.Drawing.Size(126, 13);
            // 
            // layoutControlItemId_razm_nab
            // 
            layoutControlItemId_razm_nab.Control = customTextBoxId_razm_nab;
            layoutControlItemId_razm_nab.Location = new System.Drawing.Point(0, 128);
            layoutControlItemId_razm_nab.Name = "layoutControlItemId_razm_nab";
            layoutControlItemId_razm_nab.Size = new System.Drawing.Size(536, 24);
            layoutControlItemId_razm_nab.Text = "Идентификатор размера";
            layoutControlItemId_razm_nab.TextSize = new System.Drawing.Size(126, 13);
            // 
            // layoutControlItemAns_id
            // 
            layoutControlItemAns_id.Control = customTextBoxAns_id;
            layoutControlItemAns_id.DataBindings.Add(new Binding("CustomizationFormText", spArticulNaborSostavBindingSource, "Ans_id", true, DataSourceUpdateMode.OnValidation, "0"));
            layoutControlItemAns_id.Location = new System.Drawing.Point(205, 0);
            layoutControlItemAns_id.Name = "layoutControlItemAns_id";
            layoutControlItemAns_id.Size = new System.Drawing.Size(205, 520);
            layoutControlItemAns_id.Text = "Идентификатор";
            layoutControlItemAns_id.TextSize = new System.Drawing.Size(126, 13);
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { simpleLabelItem1 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Size = new System.Drawing.Size(276, 520);
            // 
            // simpleLabelItem1
            // 
            simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(252, 475);
            simpleLabelItem1.TextSize = new System.Drawing.Size(107, 13);
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroupDataNabor, layoutControlGroupDataNaborOld, simpleSeparator1, splitterItem1 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1195, 586);
            Root.TextVisible = false;
            // 
            // layoutControlGroupDataNabor
            // 
            layoutControlGroupDataNabor.CustomizationFormText = "Данные набора";
            layoutControlGroupDataNabor.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { Артикул, layoutControlItem8, layoutControlItem9, ГОСТ, Группа, layoutControlItem3, layoutControlItem4, layoutControlItem7, layoutControlItem15, layoutControlItem16, emptySpaceItem4, layoutControlItem12 });
            layoutControlGroupDataNabor.Location = new System.Drawing.Point(590, 1);
            layoutControlGroupDataNabor.Name = "layoutControlGroupDataNabor";
            layoutControlGroupDataNabor.Size = new System.Drawing.Size(585, 565);
            layoutControlGroupDataNabor.Text = "Данные набора для редактирования";
            // 
            // Артикул
            // 
            Артикул.Control = customTextBoxArtN;
            Артикул.CustomizationFormText = "Артикул";
            Артикул.Location = new System.Drawing.Point(0, 0);
            Артикул.Name = "Артикул";
            Артикул.Size = new System.Drawing.Size(561, 24);
            Артикул.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = customGridControlNabor;
            layoutControlItem8.Location = new System.Drawing.Point(0, 256);
            layoutControlItem8.MinSize = new System.Drawing.Size(104, 24);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.Size = new System.Drawing.Size(561, 230);
            layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = customButtonSaveNabor;
            layoutControlItem9.Location = new System.Drawing.Point(270, 486);
            layoutControlItem9.MinSize = new System.Drawing.Size(254, 34);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new System.Drawing.Size(291, 34);
            layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem9.TextVisible = false;
            // 
            // ГОСТ
            // 
            ГОСТ.Control = customSearchLookUpEditGostN;
            ГОСТ.Location = new System.Drawing.Point(0, 24);
            ГОСТ.Name = "ГОСТ";
            ГОСТ.Size = new System.Drawing.Size(561, 26);
            ГОСТ.TextSize = new System.Drawing.Size(85, 13);
            // 
            // Группа
            // 
            Группа.Control = customSearchLookUpEditGrupN;
            Группа.Location = new System.Drawing.Point(0, 50);
            Группа.Name = "Группа";
            Группа.Size = new System.Drawing.Size(561, 26);
            Группа.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = customCheckBoxVerified;
            layoutControlItem3.Location = new System.Drawing.Point(0, 486);
            layoutControlItem3.MinSize = new System.Drawing.Size(24, 24);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(270, 34);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = customLabel1;
            layoutControlItem4.Location = new System.Drawing.Point(0, 76);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(561, 20);
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = customLabel2;
            layoutControlItem7.Location = new System.Drawing.Point(0, 96);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new System.Drawing.Size(561, 20);
            layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            layoutControlItem15.Control = customLabel4;
            layoutControlItem15.Location = new System.Drawing.Point(0, 218);
            layoutControlItem15.MinSize = new System.Drawing.Size(17, 20);
            layoutControlItem15.Name = "layoutControlItem15";
            layoutControlItem15.Size = new System.Drawing.Size(71, 38);
            layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            layoutControlItem16.Control = customLabel3;
            layoutControlItem16.Location = new System.Drawing.Point(0, 116);
            layoutControlItem16.Name = "layoutControlItem16";
            layoutControlItem16.Size = new System.Drawing.Size(561, 20);
            layoutControlItem16.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.Location = new System.Drawing.Point(0, 136);
            emptySpaceItem4.MinSize = new System.Drawing.Size(104, 24);
            emptySpaceItem4.Name = "emptySpaceItem4";
            emptySpaceItem4.Size = new System.Drawing.Size(561, 82);
            emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            // 
            // layoutControlItem12
            // 
            layoutControlItem12.Control = cLabelInfo;
            layoutControlItem12.Location = new System.Drawing.Point(71, 218);
            layoutControlItem12.MinSize = new System.Drawing.Size(155, 20);
            layoutControlItem12.Name = "layoutControlItem12";
            layoutControlItem12.Size = new System.Drawing.Size(490, 38);
            layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem12.TextVisible = false;
            // 
            // layoutControlGroupDataNaborOld
            // 
            layoutControlGroupDataNaborOld.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { Артикул_Old, layoutControlItem5, layoutControlItem6, layoutControlItemPicture, simpleSeparator3, layoutControlItem1, emptySpaceItem6, simpleSeparator2, emptySpaceItem3, emptySpaceItem7, layoutControlItem10, layoutControlItem14 });
            layoutControlGroupDataNaborOld.Location = new System.Drawing.Point(0, 1);
            layoutControlGroupDataNaborOld.Name = "layoutControlGroupDataNaborOld";
            layoutControlGroupDataNaborOld.Size = new System.Drawing.Size(580, 565);
            layoutControlGroupDataNaborOld.Text = "Данные набора (до изменений) не для редактирования";
            // 
            // Артикул_Old
            // 
            Артикул_Old.AccessibleName = "";
            Артикул_Old.Control = customTextBoxArtN_Old;
            Артикул_Old.CustomizationFormText = "Артикул";
            Артикул_Old.Location = new System.Drawing.Point(0, 0);
            Артикул_Old.Name = "Артикул_Old";
            Артикул_Old.Size = new System.Drawing.Size(377, 24);
            Артикул_Old.Text = "Артикул";
            Артикул_Old.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = customTextBoxGostN_Old;
            layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(377, 24);
            layoutControlItem5.Text = "ГОСТ";
            layoutControlItem5.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = customTextBoxGrupN_Old;
            layoutControlItem6.Location = new System.Drawing.Point(0, 96);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(377, 24);
            layoutControlItem6.Text = "Группа";
            layoutControlItem6.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItemPicture
            // 
            layoutControlItemPicture.Control = customPictureBoxNabor;
            layoutControlItemPicture.Location = new System.Drawing.Point(377, 0);
            layoutControlItemPicture.Name = "layoutControlItemPicture";
            layoutControlItemPicture.Size = new System.Drawing.Size(179, 199);
            layoutControlItemPicture.Text = " ";
            layoutControlItemPicture.TextLocation = DevExpress.Utils.Locations.Bottom;
            layoutControlItemPicture.TextSize = new System.Drawing.Size(85, 13);
            // 
            // simpleSeparator3
            // 
            simpleSeparator3.Location = new System.Drawing.Point(0, 519);
            simpleSeparator3.Name = "simpleSeparator3";
            simpleSeparator3.Size = new System.Drawing.Size(556, 1);
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customGridControlNabor_Old;
            layoutControlItem1.Location = new System.Drawing.Point(0, 215);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(556, 264);
            layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            emptySpaceItem6.Location = new System.Drawing.Point(0, 200);
            emptySpaceItem6.Name = "emptySpaceItem6";
            emptySpaceItem6.Size = new System.Drawing.Size(556, 15);
            // 
            // simpleSeparator2
            // 
            simpleSeparator2.Location = new System.Drawing.Point(0, 199);
            simpleSeparator2.Name = "simpleSeparator2";
            simpleSeparator2.Size = new System.Drawing.Size(556, 1);
            // 
            // emptySpaceItem3
            // 
            emptySpaceItem3.Location = new System.Drawing.Point(0, 120);
            emptySpaceItem3.Name = "emptySpaceItem3";
            emptySpaceItem3.Size = new System.Drawing.Size(377, 79);
            // 
            // emptySpaceItem7
            // 
            emptySpaceItem7.Location = new System.Drawing.Point(0, 479);
            emptySpaceItem7.Name = "emptySpaceItem7";
            emptySpaceItem7.Size = new System.Drawing.Size(556, 40);
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.Control = customTextBoxOpiGost_Old;
            layoutControlItem10.Location = new System.Drawing.Point(0, 72);
            layoutControlItem10.Name = "layoutControlItem10";
            layoutControlItem10.Size = new System.Drawing.Size(377, 24);
            layoutControlItem10.Text = "Описание ГОСТа";
            layoutControlItem10.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem14
            // 
            layoutControlItem14.Control = customTextBoxKodGost_Old;
            layoutControlItem14.Location = new System.Drawing.Point(0, 24);
            layoutControlItem14.Name = "layoutControlItem14";
            layoutControlItem14.Size = new System.Drawing.Size(377, 24);
            layoutControlItem14.Text = "Код ГОСТа";
            layoutControlItem14.TextSize = new System.Drawing.Size(85, 13);
            // 
            // simpleSeparator1
            // 
            simpleSeparator1.Location = new System.Drawing.Point(0, 0);
            simpleSeparator1.Name = "simpleSeparator1";
            simpleSeparator1.Size = new System.Drawing.Size(1175, 1);
            // 
            // splitterItem1
            // 
            splitterItem1.Location = new System.Drawing.Point(580, 1);
            splitterItem1.Name = "splitterItem1";
            splitterItem1.Size = new System.Drawing.Size(10, 565);
            // 
            // tvnModelBindingSource
            // 
            tvnModelBindingSource.DataSource = typeof(Core.Models.TvnModel);
            // 
            // assortModelBindingSource
            // 
            assortModelBindingSource.DataSource = typeof(Core.Models.AssortModel);
            // 
            // tabbedControlGroup1
            // 
            tabbedControlGroup1.Location = new System.Drawing.Point(0, 336);
            tabbedControlGroup1.Name = "tabbedControlGroup1";
            tabbedControlGroup1.SelectedTabPage = layoutControlGroup1;
            tabbedControlGroup1.Size = new System.Drawing.Size(585, 57);
            tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1 });
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new System.Drawing.Size(561, 10);
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.Visible = false;
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.Location = new System.Drawing.Point(0, 152);
            emptySpaceItem5.Name = "emptySpaceItem2";
            emptySpaceItem5.Size = new System.Drawing.Size(18, 37);
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = cLabelInfo;
            layoutControlItem13.Location = new System.Drawing.Point(0, 299);
            layoutControlItem13.Name = "layoutControlItem12";
            layoutControlItem13.Size = new System.Drawing.Size(431, 24);
            layoutControlItem13.TextVisible = false;
            // 
            // popupTrigger
            // 
            popupTrigger.Location = new System.Drawing.Point(0, 0);
            popupTrigger.Name = "popupTrigger";
            popupTrigger.Size = new System.Drawing.Size(1, 20);
            popupTrigger.TabIndex = 0;
            popupTrigger.Visible = false;
            // 
            // customListBoxRazm
            // 
            customListBoxRazm.BackColor = System.Drawing.SystemColors.Window;
            customListBoxRazm.Font = new System.Drawing.Font("Arial", 10F);
            customListBoxRazm.ForeColor = System.Drawing.SystemColors.WindowText;
            customListBoxRazm.FormattingEnabled = true;
            customListBoxRazm.Location = new System.Drawing.Point(0, 0);
            customListBoxRazm.Name = "customListBoxRazm";
            customListBoxRazm.ObjectName = null;
            customListBoxRazm.Size = new System.Drawing.Size(120, 84);
            customListBoxRazm.TabIndex = 1;
            customListBoxRazm.Visible = false;
            customListBoxRazm.SelectedIndexChanged += listRazm_SelectedIndexChanged;
            // 
            // EditNaborSostav
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1195, 586);
            Controls.Add(customListBoxRazm);
            Controls.Add(popupTrigger);
            Controls.Add(dataLayoutControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "EditNaborSostav";
            Text = "Редактирование набора";
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).EndInit();
            dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEditGrupN.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)_bsGrupForNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEdit2View).EndInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEditGostN.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)_bsGostForNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEdit1View).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)spArticulNaborSostavBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridViewNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSearchLookUpEditGost).EndInit();
            ((System.ComponentModel.ISupportInitialize)_bsGostForSostav).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSearchLookUpEditGostView).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSearchLookUpEditGrup).EndInit();
            ((System.ComponentModel.ISupportInitialize)_bsGrupForSostav).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSearchLookUpEditGrupView).EndInit();
            ((System.ComponentModel.ISupportInitialize)customPictureBoxNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlNabor_Old).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridViewNabor_Old).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemKod).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemId_razm_nab).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemAns_id).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupDataNabor).EndInit();
            ((System.ComponentModel.ISupportInitialize)Артикул).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)ГОСТ).EndInit();
            ((System.ComponentModel.ISupportInitialize)Группа).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupDataNaborOld).EndInit();
            ((System.ComponentModel.ISupportInitialize)Артикул_Old).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tvnModelBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)assortModelBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabbedControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)popupTrigger.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Core.Class.CustomTextBox customTextBoxAns_id;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAns_id;
        private System.Windows.Forms.BindingSource spArticulNaborSostavBindingSource;
        private Core.Class.CustomTextBox customTextBoxKod;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKod;
        private Core.Class.CustomTextBox customTextBoxId_razm_nab;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemId_razm_nab;
        private Core.Class.CustomGridControl customGridControlNabor_Old;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource assortModelBindingSource;
        private System.Windows.Forms.BindingSource _bsGrupForSostav;
        private System.Windows.Forms.BindingSource _bsGostForSostav;
        private System.Windows.Forms.BindingSource tvnModelBindingSource;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private Core.Class.CustomPictureBox customPictureBox1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private Core.Class.CustomPictureBox customPictureBoxNabor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPicture;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private Core.Class.CustomTextBox customTextBoxGrupN_Old;
        private Core.Class.CustomTextBox customTextBoxGostN_Old;
        private Core.Class.CustomTextBox customTextBoxArtN_Old;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupDataNaborOld;
        private DevExpress.XtraLayout.LayoutControlItem Артикул_Old;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GridViewNabor_Old;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAns_id;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKod;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTa_id;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTk_id;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colId_gost;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAg_id;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSostav;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colId_razm_nab;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRazm;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTxt_v;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTk_name;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colN_i;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRazm_all;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupDataNabor;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private Core.Class.CustomTextBox customTextBoxArtN;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem Артикул;
        private Core.Class.CustomButton customButtonSaveNabor;
        private Core.Class.CustomGridControl customGridControlNabor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView GridViewNabor;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAns_id1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKod1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTa_id1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTk_id1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colId_gost1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAg_id1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSostav1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colId_razm_nab1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRazm1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTxt_v1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTk_name1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colN_i1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colrazm_all1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BandRazmAll1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditGost;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEditGostView;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditGrup;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEditGrupView;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private Core.Class.CustomSearchLookUpEdit customSearchLookUpEditGrupN;
        private DevExpress.XtraGrid.Views.Grid.GridView customSearchLookUpEdit2View;
        private Core.Class.CustomSearchLookUpEdit customSearchLookUpEditGostN;
        private DevExpress.XtraGrid.Views.Grid.GridView customSearchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem ГОСТ;
        private DevExpress.XtraLayout.LayoutControlItem Группа;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand BandRazmAll;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn colName_gost;
        private DevExpress.XtraGrid.Columns.GridColumn colOpi_gost;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn colAg_name_sokr;
        private DevExpress.XtraGrid.Columns.GridColumn colAg_tnved;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn colN_g;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private System.Windows.Forms.BindingSource _bsGostForNabor;
        private System.Windows.Forms.BindingSource _bsGrupForNabor;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private CustomCheckBox customCheckBoxVerified;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private Core.Class.CustomLabel customLabel1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Core.Class.CustomLabel customLabel2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private Core.Class.CustomLabel customLabel4;
        private Core.Class.CustomLabel cLabelInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private Core.Class.CustomTextBox customTextBoxKodGost_Old;
        private Core.Class.CustomTextBox customTextBoxOpiGost_Old;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraEditors.PopupContainerEdit popupTrigger;
        private Core.Class.CustomListBox customListBoxRazm;
        private Core.Class.CustomLabel customLabel3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
    }
}