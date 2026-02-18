namespace SewingProduction.Features.Articul.Forms
{
    partial class CreateArticulMatr
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
            gridArtMatr = new SewingProduction.Core.Class.CustomGridControl();
            gridViewArtMatr = new DevExpress.XtraGrid.Views.Grid.GridView();
            gcGrupmen_name = new DevExpress.XtraGrid.Columns.GridColumn();
            gcTsn_name = new DevExpress.XtraGrid.Columns.GridColumn();
            gcTb_id = new DevExpress.XtraGrid.Columns.GridColumn();
            gcArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            gcMod = new DevExpress.XtraGrid.Columns.GridColumn();
            gcTm_name = new DevExpress.XtraGrid.Columns.GridColumn();
            gcGrup = new DevExpress.XtraGrid.Columns.GridColumn();
            gcText_mo = new DevExpress.XtraGrid.Columns.GridColumn();
            gcP = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            gcPrinter = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            gcStra = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            gcBus = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            gcPpres = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            gcV = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            gcKruj = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            gcTkan = new DevExpress.XtraGrid.Columns.GridColumn();
            gcSost = new DevExpress.XtraGrid.Columns.GridColumn();
            gcRazmNames = new DevExpress.XtraGrid.Columns.GridColumn();
            gcDatePublic = new DevExpress.XtraGrid.Columns.GridColumn();
            gcSost2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gcSost3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)gridArtMatr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArtMatr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit7).BeginInit();
            SuspendLayout();
            // 
            // gridArtMatr
            // 
            gridArtMatr.Dock = System.Windows.Forms.DockStyle.Top;
            gridArtMatr.Font = new System.Drawing.Font("Arial", 10F);
            gridArtMatr.Location = new System.Drawing.Point(0, 0);
            gridArtMatr.MainView = gridViewArtMatr;
            gridArtMatr.Name = "gridArtMatr";
            gridArtMatr.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEdit1, repositoryItemCheckEdit2, repositoryItemCheckEdit3, repositoryItemCheckEdit4, repositoryItemCheckEdit5, repositoryItemCheckEdit6, repositoryItemCheckEdit7 });
            gridArtMatr.Size = new System.Drawing.Size(1683, 453);
            gridArtMatr.TabIndex = 0;
            gridArtMatr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewArtMatr });
            // 
            // gridViewArtMatr
            // 
            gridViewArtMatr.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridViewArtMatr.Appearance.FocusedRow.Options.UseFont = true;
            gridViewArtMatr.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewArtMatr.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridViewArtMatr.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gcGrupmen_name, gcTsn_name, gcTb_id, gcArticul, gcMod, gcTm_name, gcGrup, gcText_mo, gcP, gcPrinter, gcStra, gcBus, gcPpres, gcV, gcKruj, gcTkan, gcSost, gcRazmNames, gcDatePublic, gcSost2, gcSost3 });
            gridViewArtMatr.GridControl = gridArtMatr;
            gridViewArtMatr.Name = "gridViewArtMatr";
            gridViewArtMatr.OptionsBehavior.ReadOnly = true;
            gridViewArtMatr.OptionsSelection.MultiSelect = true;
            gridViewArtMatr.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewArtMatr.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            gridViewArtMatr.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            // 
            // gcGrupmen_name
            // 
            gcGrupmen_name.Caption = "Менеджер";
            gcGrupmen_name.Name = "gcGrupmen_name";
            gcGrupmen_name.Visible = true;
            gcGrupmen_name.VisibleIndex = 1;
            gcGrupmen_name.Width = 118;
            // 
            // gcTsn_name
            // 
            gcTsn_name.Caption = "Сезон";
            gcTsn_name.Name = "gcTsn_name";
            gcTsn_name.Visible = true;
            gcTsn_name.VisibleIndex = 2;
            gcTsn_name.Width = 62;
            // 
            // gcTb_id
            // 
            gcTb_id.Caption = "Блок";
            gcTb_id.Name = "gcTb_id";
            gcTb_id.Visible = true;
            gcTb_id.VisibleIndex = 3;
            gcTb_id.Width = 71;
            // 
            // gcArticul
            // 
            gcArticul.Caption = "Артику";
            gcArticul.Name = "gcArticul";
            gcArticul.Visible = true;
            gcArticul.VisibleIndex = 4;
            gcArticul.Width = 71;
            // 
            // gcMod
            // 
            gcMod.Caption = "Модель";
            gcMod.Name = "gcMod";
            gcMod.Visible = true;
            gcMod.VisibleIndex = 5;
            gcMod.Width = 71;
            // 
            // gcTm_name
            // 
            gcTm_name.Caption = "Торг Марка";
            gcTm_name.Name = "gcTm_name";
            gcTm_name.Visible = true;
            gcTm_name.VisibleIndex = 6;
            gcTm_name.Width = 54;
            // 
            // gcGrup
            // 
            gcGrup.Caption = "Группа";
            gcGrup.Name = "gcGrup";
            gcGrup.Visible = true;
            gcGrup.VisibleIndex = 7;
            gcGrup.Width = 105;
            // 
            // gcText_mo
            // 
            gcText_mo.Caption = "Модельный признак";
            gcText_mo.Name = "gcText_mo";
            gcText_mo.Visible = true;
            gcText_mo.VisibleIndex = 8;
            gcText_mo.Width = 110;
            // 
            // gcP
            // 
            gcP.Caption = "П";
            gcP.ColumnEdit = repositoryItemCheckEdit1;
            gcP.Name = "gcP";
            gcP.Visible = true;
            gcP.VisibleIndex = 9;
            gcP.Width = 23;
            // 
            // repositoryItemCheckEdit1
            // 
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit1.ValueChecked = (short)1;
            repositoryItemCheckEdit1.ValueUnchecked = (short)0;
            // 
            // gcPrinter
            // 
            gcPrinter.Caption = "Прин тер";
            gcPrinter.ColumnEdit = repositoryItemCheckEdit3;
            gcPrinter.Name = "gcPrinter";
            gcPrinter.Visible = true;
            gcPrinter.VisibleIndex = 10;
            gcPrinter.Width = 36;
            // 
            // repositoryItemCheckEdit3
            // 
            repositoryItemCheckEdit3.AutoHeight = false;
            repositoryItemCheckEdit3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            repositoryItemCheckEdit3.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit3.ValueChecked = (short)1;
            repositoryItemCheckEdit3.ValueUnchecked = (short)0;
            // 
            // gcStra
            // 
            gcStra.Caption = "С";
            gcStra.ColumnEdit = repositoryItemCheckEdit2;
            gcStra.Name = "gcStra";
            gcStra.Visible = true;
            gcStra.VisibleIndex = 11;
            gcStra.Width = 21;
            // 
            // repositoryItemCheckEdit2
            // 
            repositoryItemCheckEdit2.AutoHeight = false;
            repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit2.ValueChecked = (short)1;
            repositoryItemCheckEdit2.ValueUnchecked = (short)0;
            // 
            // gcBus
            // 
            gcBus.Caption = "Б";
            gcBus.ColumnEdit = repositoryItemCheckEdit4;
            gcBus.Name = "gcBus";
            gcBus.Visible = true;
            gcBus.VisibleIndex = 12;
            gcBus.Width = 22;
            // 
            // repositoryItemCheckEdit4
            // 
            repositoryItemCheckEdit4.AutoHeight = false;
            repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            repositoryItemCheckEdit4.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit4.ValueChecked = (short)1;
            repositoryItemCheckEdit4.ValueUnchecked = (short)0;
            // 
            // gcPpres
            // 
            gcPpres.Caption = "Пресс";
            gcPpres.ColumnEdit = repositoryItemCheckEdit5;
            gcPpres.Name = "gcPpres";
            gcPpres.Visible = true;
            gcPpres.VisibleIndex = 13;
            gcPpres.Width = 36;
            // 
            // repositoryItemCheckEdit5
            // 
            repositoryItemCheckEdit5.AutoHeight = false;
            repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            repositoryItemCheckEdit5.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit5.ValueChecked = (short)1;
            repositoryItemCheckEdit5.ValueUnchecked = (short)0;
            // 
            // gcV
            // 
            gcV.Caption = "В";
            gcV.ColumnEdit = repositoryItemCheckEdit6;
            gcV.Name = "gcV";
            gcV.Visible = true;
            gcV.VisibleIndex = 14;
            gcV.Width = 20;
            // 
            // repositoryItemCheckEdit6
            // 
            repositoryItemCheckEdit6.AutoHeight = false;
            repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            repositoryItemCheckEdit6.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit6.ValueChecked = (short)1;
            repositoryItemCheckEdit6.ValueUnchecked = (short)0;
            // 
            // gcKruj
            // 
            gcKruj.Caption = "Круж";
            gcKruj.ColumnEdit = repositoryItemCheckEdit7;
            gcKruj.Name = "gcKruj";
            gcKruj.Visible = true;
            gcKruj.VisibleIndex = 15;
            gcKruj.Width = 33;
            // 
            // repositoryItemCheckEdit7
            // 
            repositoryItemCheckEdit7.AutoHeight = false;
            repositoryItemCheckEdit7.Name = "repositoryItemCheckEdit7";
            repositoryItemCheckEdit7.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit7.ValueChecked = (short)1;
            repositoryItemCheckEdit7.ValueUnchecked = (short)0;
            // 
            // gcTkan
            // 
            gcTkan.Caption = "Полотно";
            gcTkan.Name = "gcTkan";
            gcTkan.Visible = true;
            gcTkan.VisibleIndex = 16;
            gcTkan.Width = 194;
            // 
            // gcSost
            // 
            gcSost.Caption = "Состав: Основа";
            gcSost.Name = "gcSost";
            gcSost.Visible = true;
            gcSost.VisibleIndex = 17;
            gcSost.Width = 115;
            // 
            // gcRazmNames
            // 
            gcRazmNames.Caption = "Размеры";
            gcRazmNames.Name = "gcRazmNames";
            gcRazmNames.Visible = true;
            gcRazmNames.VisibleIndex = 18;
            gcRazmNames.Width = 73;
            // 
            // gcDatePublic
            // 
            gcDatePublic.Caption = "Публикация";
            gcDatePublic.Name = "gcDatePublic";
            gcDatePublic.Visible = true;
            gcDatePublic.VisibleIndex = 19;
            gcDatePublic.Width = 70;
            // 
            // gcSost2
            // 
            gcSost2.Caption = "Состав: Отделка";
            gcSost2.Name = "gcSost2";
            gcSost2.Visible = true;
            gcSost2.VisibleIndex = 20;
            gcSost2.Width = 80;
            // 
            // gcSost3
            // 
            gcSost3.Caption = "Состав: Подклад";
            gcSost3.Name = "gcSost3";
            gcSost3.Visible = true;
            gcSost3.VisibleIndex = 21;
            gcSost3.Width = 176;
            // 
            // CreateArticulMatr
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1683, 676);
            Controls.Add(gridArtMatr);
            Name = "CreateArticulMatr";
            Text = "CreateArticulMatr";
            Load += CreateArticulMatr_Load;
            ((System.ComponentModel.ISupportInitialize)gridArtMatr).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArtMatr).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit3).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit2).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit4).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit5).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit6).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit7).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Core.Class.CustomGridControl gridArtMatr;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewArtMatr;
        private DevExpress.XtraGrid.Columns.GridColumn gcGrupmen_name;
        private DevExpress.XtraGrid.Columns.GridColumn gcTsn_name;
        private DevExpress.XtraGrid.Columns.GridColumn gcTb_id;
        private DevExpress.XtraGrid.Columns.GridColumn gcArticul;
        private DevExpress.XtraGrid.Columns.GridColumn gcMod;
        private DevExpress.XtraGrid.Columns.GridColumn gcTm_name;
        private DevExpress.XtraGrid.Columns.GridColumn gcGrup;
        private DevExpress.XtraGrid.Columns.GridColumn gcText_mo;
        private DevExpress.XtraGrid.Columns.GridColumn gcP;
        private DevExpress.XtraGrid.Columns.GridColumn gcPrinter;
        private DevExpress.XtraGrid.Columns.GridColumn gcStra;
        private DevExpress.XtraGrid.Columns.GridColumn gcBus;
        private DevExpress.XtraGrid.Columns.GridColumn gcPpres;
        private DevExpress.XtraGrid.Columns.GridColumn gcV;
        private DevExpress.XtraGrid.Columns.GridColumn gcKruj;
        private DevExpress.XtraGrid.Columns.GridColumn gcTkan;
        private DevExpress.XtraGrid.Columns.GridColumn gcSost;
        private DevExpress.XtraGrid.Columns.GridColumn gcRazmNames;
        private DevExpress.XtraGrid.Columns.GridColumn gcDatePublic;
        private DevExpress.XtraGrid.Columns.GridColumn gcSost2;
        private DevExpress.XtraGrid.Columns.GridColumn gcSost3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit7;
    }
}