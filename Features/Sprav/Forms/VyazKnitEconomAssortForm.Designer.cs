using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Class;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SewingProduction.Features.Sprav.Forms
{
    internal partial class VyazKnitEconomAssortForm
    {
        private IContainer components = null;
        private CustomGridControl gridControl;
        private GridView gridView;
        private GridColumn colNn;
        private GridColumn colNomZadany;
        private GridColumn colRazmRyad;
        private GridColumn colPachMin;
        private GridColumn colPachMax;
        private GridColumn colNom;
        private GridColumn colArticul;
        private GridColumn colMod;
        private GridColumn colDateEconom;
        private GridColumn colLastOtmIzm;
        private GridColumn colGrup;
        private GridColumn colDataCdMin;
        private GridColumn colKoefZatrat;
        private GridColumn colIdPodr;
        private GridColumn colSebAll;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions3 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions4 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions5 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions6 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions7 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VyazKnitEconomAssortForm));
            gridControl = new CustomGridControl();
            bindingSource = new BindingSource(components);
            gridView = new GridView();
            colNn = new GridColumn();
            colNomZadany = new GridColumn();
            colRazmRyad = new GridColumn();
            colPachMin = new GridColumn();
            colPachMax = new GridColumn();
            colNom = new GridColumn();
            colArticul = new GridColumn();
            colMod = new GridColumn();
            colDateEconom = new GridColumn();
            colLastOtmIzm = new GridColumn();
            colGrup = new GridColumn();
            colDataCdMin = new GridColumn();
            colKoefZatrat = new GridColumn();
            colIdPodr = new GridColumn();
            colSebAll = new GridColumn();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            gridControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)gridControl).BeginInit();
            ((ISupportInitialize)bindingSource).BeginInit();
            ((ISupportInitialize)gridView).BeginInit();
            ((ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((ISupportInitialize)layoutControlGroup1).BeginInit();
            ((ISupportInitialize)layoutControlGroup2).BeginInit();
            ((ISupportInitialize)gridControlItem).BeginInit();
            SuspendLayout();
            // 
            // gridControl
            // 
            gridControl.DataSource = bindingSource;
            gridControl.Font = new Font("Arial", 10F);
            gridControl.Location = new Point(5, 26);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl";
            gridControl.Size = new Size(1254, 704);
            gridControl.TabIndex = 0;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            // 
            // gridView
            // 
            gridView.Columns.AddRange(new GridColumn[]
            {
                colNn, colNomZadany, colRazmRyad, colPachMin, colPachMax, colNom, colArticul, colMod,
                colDateEconom, colLastOtmIzm, colGrup, colDataCdMin, colKoefZatrat, colIdPodr, colSebAll
            });
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.ReadOnly = true;
            gridView.OptionsFind.AlwaysVisible = true;
            gridView.OptionsView.ShowAutoFilterRow = true;
            gridView.OptionsView.ShowGroupPanel = false;
            // 
            // colNn
            // 
            colNn.Caption = "№";
            colNn.FieldName = "nn";
            colNn.Name = "colNn";
            colNn.Visible = true;
            colNn.VisibleIndex = 0;
            colNn.Width = 50;
            // 
            // colNomZadany
            // 
            colNomZadany.Caption = "Ном. задания";
            colNomZadany.FieldName = "nom_zadany";
            colNomZadany.Name = "colNomZadany";
            colNomZadany.Visible = true;
            colNomZadany.VisibleIndex = 1;
            colNomZadany.Width = 90;
            // 
            // colRazmRyad
            // 
            colRazmRyad.Caption = "Разм. ряд";
            colRazmRyad.FieldName = "razm_ryad";
            colRazmRyad.Name = "colRazmRyad";
            colRazmRyad.Visible = true;
            colRazmRyad.VisibleIndex = 2;
            colRazmRyad.Width = 70;
            // 
            // colPachMin
            // 
            colPachMin.Caption = "Пач. мин";
            colPachMin.FieldName = "pach_min";
            colPachMin.Name = "colPachMin";
            colPachMin.Visible = true;
            colPachMin.VisibleIndex = 3;
            colPachMin.Width = 65;
            // 
            // colPachMax
            // 
            colPachMax.Caption = "Пач. макс";
            colPachMax.FieldName = "pach_max";
            colPachMax.Name = "colPachMax";
            colPachMax.Visible = true;
            colPachMax.VisibleIndex = 4;
            colPachMax.Width = 65;
            // 
            // colNom
            // 
            colNom.Caption = "Ном";
            colNom.FieldName = "nom";
            colNom.Name = "colNom";
            colNom.Visible = true;
            colNom.VisibleIndex = 5;
            colNom.Width = 60;
            // 
            // colArticul
            // 
            colArticul.Caption = "Артикул";
            colArticul.FieldName = "articul";
            colArticul.Name = "colArticul";
            colArticul.Visible = true;
            colArticul.VisibleIndex = 6;
            colArticul.Width = 120;
            // 
            // colMod
            // 
            colMod.Caption = "Мод";
            colMod.FieldName = "mod";
            colMod.Name = "colMod";
            colMod.Visible = true;
            colMod.VisibleIndex = 7;
            colMod.Width = 80;
            // 
            // colDateEconom
            // 
            colDateEconom.Caption = "Дата кальк.";
            colDateEconom.DisplayFormat.FormatString = "dd.MM.yyyy";
            colDateEconom.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colDateEconom.FieldName = "date_econom";
            colDateEconom.Name = "colDateEconom";
            colDateEconom.Visible = true;
            colDateEconom.VisibleIndex = 8;
            colDateEconom.Width = 90;
            // 
            // colLastOtmIzm
            // 
            colLastOtmIzm.Caption = "Посл. отм. изм.";
            colLastOtmIzm.FieldName = "last_otm_izm";
            colLastOtmIzm.Name = "colLastOtmIzm";
            colLastOtmIzm.Visible = true;
            colLastOtmIzm.VisibleIndex = 9;
            colLastOtmIzm.Width = 80;
            // 
            // colGrup
            // 
            colGrup.Caption = "Группа";
            colGrup.FieldName = "grup";
            colGrup.Name = "colGrup";
            colGrup.Visible = true;
            colGrup.VisibleIndex = 10;
            colGrup.Width = 70;
            // 
            // colDataCdMin
            // 
            colDataCdMin.Caption = "Дата CD мин";
            colDataCdMin.DisplayFormat.FormatString = "dd.MM.yyyy";
            colDataCdMin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colDataCdMin.FieldName = "data_cd_min";
            colDataCdMin.Name = "colDataCdMin";
            colDataCdMin.Visible = true;
            colDataCdMin.VisibleIndex = 11;
            colDataCdMin.Width = 90;
            // 
            // colKoefZatrat
            // 
            colKoefZatrat.Caption = "Коэф. затрат";
            colKoefZatrat.DisplayFormat.FormatString = "0.####";
            colKoefZatrat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colKoefZatrat.FieldName = "koef_zatrat";
            colKoefZatrat.Name = "colKoefZatrat";
            colKoefZatrat.Visible = true;
            colKoefZatrat.VisibleIndex = 12;
            colKoefZatrat.Width = 80;
            // 
            // colIdPodr
            // 
            colIdPodr.Caption = "Подразд.";
            colIdPodr.FieldName = "id_podr";
            colIdPodr.Name = "colIdPodr";
            colIdPodr.Visible = true;
            colIdPodr.VisibleIndex = 13;
            colIdPodr.Width = 65;
            // 
            // colSebAll
            // 
            colSebAll.Caption = "Себ. всего";
            colSebAll.DisplayFormat.FormatString = "0.####";
            colSebAll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSebAll.FieldName = "seb_all";
            colSebAll.Name = "colSebAll";
            colSebAll.Visible = true;
            colSebAll.VisibleIndex = 14;
            colSebAll.Width = 90;
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(gridControl);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = layoutControlGroup1;
            layoutControl1.Size = new Size(1264, 761);
            layoutControl1.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup2 });
            layoutControlGroup1.Name = "Root";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup1.Size = new Size(1264, 761);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            buttonImageOptions6.Image = (Image)resources.GetObject("buttonImageOptions5.Image");
            layoutControlGroup2.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[]
            {
                new GroupBoxButton("Носочный", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, true, true, null, -1),
                new GroupBoxButton("   |   ", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1),
                new GroupBoxButton("Вязальный", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, true, true, null, -1),
                new GroupBoxButton("   |   ", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1),
                new GroupBoxButton("Шнуры", true, buttonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, true, true, null, -1),
                new GroupBoxButton("   |   ", true, buttonImageOptions7, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, false, null, true, false, true, null, -1),
                new GroupBoxButton("Обновить", true, buttonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1)
            });
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { gridControlItem });
            layoutControlGroup2.Location = new Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup2.Size = new Size(1264, 761);
            layoutControlGroup2.Text = " ";
            layoutControlGroup2.CustomButtonClick += LayoutControlGroup2_CustomButtonClick;
            layoutControlGroup2.CustomButtonUnchecked += layoutControlGroup2_CustomButtonUnchecked;
            layoutControlGroup2.CustomButtonChecked += layoutControlGroup2_CustomButtonChecked;
            // 
            // gridControlItem
            // 
            gridControlItem.Control = gridControl;
            gridControlItem.Location = new Point(0, 0);
            gridControlItem.Name = "gridControlItem";
            gridControlItem.Size = new Size(1258, 734);
            gridControlItem.TextVisible = false;
            // 
            // VyazKnitEconomAssortForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 761);
            Controls.Add(layoutControl1);
            MinimumSize = new Size(1100, 700);
            Name = "VyazKnitEconomAssortForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Калькуляция вязального ассортимента";
            FormClosing += VyazKnitEconomAssortForm_FormClosing;
            Load += VyazKnitEconomAssortForm_Load;
            ((ISupportInitialize)gridControl).EndInit();
            ((ISupportInitialize)bindingSource).EndInit();
            ((ISupportInitialize)gridView).EndInit();
            ((ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((ISupportInitialize)layoutControlGroup1).EndInit();
            ((ISupportInitialize)layoutControlGroup2).EndInit();
            ((ISupportInitialize)gridControlItem).EndInit();
            ResumeLayout(false);
        }

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem gridControlItem;
        private BindingSource bindingSource;
    }
}
