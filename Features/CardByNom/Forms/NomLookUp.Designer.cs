namespace SewingProduction.Features.KnittingProduction.Forms
{
    partial class NomLookUp
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
            layoutControl1 = new SewingProduction.Core.Class.CustomLayoutControl();
            gridControlRasNomList = new SewingProduction.Core.Class.CustomGridControl();
            gridViewRasNomList = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridRasNomListColumnNomZad = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnNomPach = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnMinPach = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnMaxPach = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnMgZakr = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnKod7 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnGrupPach = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnArticulPach = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnModPach = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnYearPach = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnProizvType = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnDostZeh = new DevExpress.XtraGrid.Columns.GridColumn();
            gridRasNomListColumnIDBrig = new DevExpress.XtraGrid.Columns.GridColumn();
            customTextBoxEx1 = new CustomTextBoxEx();
            customLabel1 = new SewingProduction.Core.Class.CustomLabel();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            gridRasNomListColumnDataR = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlRasNomList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRasNomList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customTextBoxEx1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(gridControlRasNomList);
            layoutControl1.Controls.Add(customTextBoxEx1);
            layoutControl1.Controls.Add(customLabel1);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2975, 213, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(936, 645);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // gridControlRasNomList
            // 
            gridControlRasNomList.Font = new System.Drawing.Font("Arial", 10F);
            gridControlRasNomList.Location = new System.Drawing.Point(12, 38);
            gridControlRasNomList.MainView = gridViewRasNomList;
            gridControlRasNomList.Name = "gridControlRasNomList";
            gridControlRasNomList.Size = new System.Drawing.Size(912, 595);
            gridControlRasNomList.TabIndex = 6;
            gridControlRasNomList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRasNomList });
            gridControlRasNomList.DoubleClick += gridControlRasNomList_DoubleClick;
            // 
            // gridViewRasNomList
            // 
            gridViewRasNomList.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(200, 225, 255);
            gridViewRasNomList.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewRasNomList.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(200, 225, 255);
            gridViewRasNomList.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridViewRasNomList.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewRasNomList.Appearance.FocusedRow.Options.UseFont = true;
            gridViewRasNomList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridRasNomListColumnNomZad, gridRasNomListColumnNomPach, gridRasNomListColumnDataR, gridRasNomListColumnMinPach, gridRasNomListColumnMaxPach, gridRasNomListColumnMgZakr, gridRasNomListColumnKod7, gridRasNomListColumnGrupPach, gridRasNomListColumnArticulPach, gridRasNomListColumnModPach, gridRasNomListColumnYearPach, gridRasNomListColumnProizvType, gridRasNomListColumnDostZeh, gridRasNomListColumnIDBrig });
            gridViewRasNomList.GridControl = gridControlRasNomList;
            gridViewRasNomList.Name = "gridViewRasNomList";
            gridViewRasNomList.OptionsView.EnableAppearanceEvenRow = true;
            gridViewRasNomList.DoubleClick += gridViewRasNomList_DoubleClick;
            // 
            // gridRasNomListColumnNomZad
            // 
            gridRasNomListColumnNomZad.Caption = "№ задания";
            gridRasNomListColumnNomZad.Name = "gridRasNomListColumnNomZad";
            gridRasNomListColumnNomZad.Visible = true;
            gridRasNomListColumnNomZad.VisibleIndex = 0;
            // 
            // gridRasNomListColumnNomPach
            // 
            gridRasNomListColumnNomPach.Caption = "№ расчета";
            gridRasNomListColumnNomPach.Name = "gridRasNomListColumnNomPach";
            gridRasNomListColumnNomPach.Visible = true;
            gridRasNomListColumnNomPach.VisibleIndex = 2;
            // 
            // gridRasNomListColumnMinPach
            // 
            gridRasNomListColumnMinPach.Caption = "Пачка С";
            gridRasNomListColumnMinPach.Name = "gridRasNomListColumnMinPach";
            gridRasNomListColumnMinPach.Visible = true;
            gridRasNomListColumnMinPach.VisibleIndex = 4;
            // 
            // gridRasNomListColumnMaxPach
            // 
            gridRasNomListColumnMaxPach.Caption = "Пачка ПО";
            gridRasNomListColumnMaxPach.Name = "gridRasNomListColumnMaxPach";
            gridRasNomListColumnMaxPach.Visible = true;
            gridRasNomListColumnMaxPach.VisibleIndex = 5;
            // 
            // gridRasNomListColumnMgZakr
            // 
            gridRasNomListColumnMgZakr.Caption = "Карта кроя";
            gridRasNomListColumnMgZakr.Name = "gridRasNomListColumnMgZakr";
            gridRasNomListColumnMgZakr.Visible = true;
            gridRasNomListColumnMgZakr.VisibleIndex = 6;
            // 
            // gridRasNomListColumnKod7
            // 
            gridRasNomListColumnKod7.Caption = "Код изделия";
            gridRasNomListColumnKod7.Name = "gridRasNomListColumnKod7";
            gridRasNomListColumnKod7.Visible = true;
            gridRasNomListColumnKod7.VisibleIndex = 7;
            // 
            // gridRasNomListColumnGrupPach
            // 
            gridRasNomListColumnGrupPach.Caption = "Группа";
            gridRasNomListColumnGrupPach.Name = "gridRasNomListColumnGrupPach";
            gridRasNomListColumnGrupPach.Visible = true;
            gridRasNomListColumnGrupPach.VisibleIndex = 8;
            // 
            // gridRasNomListColumnArticulPach
            // 
            gridRasNomListColumnArticulPach.Caption = "Артикул";
            gridRasNomListColumnArticulPach.Name = "gridRasNomListColumnArticulPach";
            gridRasNomListColumnArticulPach.Visible = true;
            gridRasNomListColumnArticulPach.VisibleIndex = 9;
            // 
            // gridRasNomListColumnModPach
            // 
            gridRasNomListColumnModPach.Caption = "Модель";
            gridRasNomListColumnModPach.Name = "gridRasNomListColumnModPach";
            gridRasNomListColumnModPach.Visible = true;
            gridRasNomListColumnModPach.VisibleIndex = 10;
            // 
            // gridRasNomListColumnYearPach
            // 
            gridRasNomListColumnYearPach.Caption = "YearPach";
            gridRasNomListColumnYearPach.Name = "gridRasNomListColumnYearPach";
            // 
            // gridRasNomListColumnProizvType
            // 
            gridRasNomListColumnProizvType.Caption = "ProizvType";
            gridRasNomListColumnProizvType.Name = "gridRasNomListColumnProizvType";
            // 
            // gridRasNomListColumnDostZeh
            // 
            gridRasNomListColumnDostZeh.Caption = "Бригада";
            gridRasNomListColumnDostZeh.Name = "gridRasNomListColumnDostZeh";
            gridRasNomListColumnDostZeh.Visible = true;
            gridRasNomListColumnDostZeh.VisibleIndex = 1;
            // 
            // gridRasNomListColumnIDBrig
            // 
            gridRasNomListColumnIDBrig.Caption = "ID_brig";
            gridRasNomListColumnIDBrig.Name = "gridRasNomListColumnIDBrig";
            // 
            // customTextBoxEx1
            // 
            customTextBoxEx1.Location = new System.Drawing.Point(166, 12);
            customTextBoxEx1.Name = "customTextBoxEx1";
            customTextBoxEx1.ObjectName = null;
            customTextBoxEx1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(230, 245, 255);
            customTextBoxEx1.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customTextBoxEx1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(50, 90, 160);
            customTextBoxEx1.Properties.Appearance.Options.UseBackColor = true;
            customTextBoxEx1.Properties.Appearance.Options.UseFont = true;
            customTextBoxEx1.Properties.Appearance.Options.UseForeColor = true;
            customTextBoxEx1.Size = new System.Drawing.Size(165, 22);
            customTextBoxEx1.StyleController = layoutControl1;
            customTextBoxEx1.TabIndex = 5;
            customTextBoxEx1.KeyDown += customTextBoxEx1_KeyDown;
            // 
            // customLabel1
            // 
            customLabel1.Font = new System.Drawing.Font("Arial", 10F);
            customLabel1.ForeColor = System.Drawing.Color.FromArgb(30, 70, 140);
            customLabel1.Location = new System.Drawing.Point(12, 12);
            customLabel1.Name = "customLabel1";
            customLabel1.Size = new System.Drawing.Size(150, 22);
            customLabel1.TabIndex = 4;
            customLabel1.Text = "Параметр поиска";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, emptySpaceItem1, layoutControlItem3 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(936, 645);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customLabel1;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(154, 26);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = customTextBoxEx1;
            layoutControlItem2.Location = new System.Drawing.Point(154, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(169, 26);
            layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(323, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(593, 26);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = gridControlRasNomList;
            layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(916, 599);
            layoutControlItem3.TextVisible = false;
            // 
            // gridRasNomListColumnDataR
            // 
            gridRasNomListColumnDataR.Caption = "Дата расчета";
            gridRasNomListColumnDataR.Name = "gridRasNomListColumnDataR";
            gridRasNomListColumnDataR.Visible = true;
            gridRasNomListColumnDataR.VisibleIndex = 3;
            // 
            // NomLookUp
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(936, 645);
            Controls.Add(layoutControl1);
            Name = "NomLookUp";
            Text = "Form1";
            Load += NomLookUp_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlRasNomList).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRasNomList).EndInit();
            ((System.ComponentModel.ISupportInitialize)customTextBoxEx1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Core.Class.CustomGridControl gridControlRasNomList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRasNomList;
        private CustomTextBoxEx customTextBoxEx1;
        private Core.Class.CustomLabel customLabel1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnNomZad;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnNomPach;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnMinPach;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnMaxPach;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnMgZakr;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnKod7;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnGrupPach;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnArticulPach;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnModPach;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnYearPach;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnProizvType;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnDostZeh;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnIDBrig;
        private DevExpress.XtraGrid.Columns.GridColumn gridRasNomListColumnDataR;
    }
}