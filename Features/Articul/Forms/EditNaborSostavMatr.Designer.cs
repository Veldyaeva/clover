namespace SewingProduction.Features.Articul.Forms
{
    partial class EditNaborSostavMatr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditNaborSostavMatr));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            customButtonSave = new Core.Class.CustomButton();
            customGridControlArtKomplekt = new Core.Class.CustomGridControl();
            gridViewArtKomplekt = new DevExpress.XtraGrid.Views.Grid.GridView();
            customGridControlPlanSezonAll = new Core.Class.CustomGridControl();
            gridViewPlanSezonAll = new DevExpress.XtraGrid.Views.Grid.GridView();
            psaGridColumnCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            psaPsaid = new DevExpress.XtraGrid.Columns.GridColumn();
            psaNn = new DevExpress.XtraGrid.Columns.GridColumn();
            psaYear = new DevExpress.XtraGrid.Columns.GridColumn();
            psaSezon = new DevExpress.XtraGrid.Columns.GridColumn();
            psaTbid = new DevExpress.XtraGrid.Columns.GridColumn();
            psaArticul = new DevExpress.XtraGrid.Columns.GridColumn();
            psaMod = new DevExpress.XtraGrid.Columns.GridColumn();
            psaGridColumnButton = new DevExpress.XtraGrid.Columns.GridColumn();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            akAkId = new DevExpress.XtraGrid.Columns.GridColumn();
            akParentNN = new DevExpress.XtraGrid.Columns.GridColumn();
            akParentPsaid = new DevExpress.XtraGrid.Columns.GridColumn();
            akTkId = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControlArtKomplekt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArtKomplekt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlPlanSezonAll).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPlanSezonAll).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(customButtonSave);
            layoutControl1.Controls.Add(customGridControlArtKomplekt);
            layoutControl1.Controls.Add(customGridControlPlanSezonAll);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(379, 411, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1060, 547);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // customButtonSave
            // 
            customButtonSave.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            customButtonSave.Font = new System.Drawing.Font("Arial", 10F);
            customButtonSave.ForeColor = System.Drawing.Color.FromArgb(72, 61, 139);
            customButtonSave.Location = new System.Drawing.Point(788, 478);
            customButtonSave.Name = "customButtonSave";
            customButtonSave.Size = new System.Drawing.Size(248, 45);
            customButtonSave.TabIndex = 3;
            customButtonSave.Text = "Сохранить";
            customButtonSave.UseVisualStyleBackColor = false;
            // 
            // customGridControlArtKomplekt
            // 
            customGridControlArtKomplekt.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlArtKomplekt.Location = new System.Drawing.Point(490, 45);
            customGridControlArtKomplekt.MainView = gridViewArtKomplekt;
            customGridControlArtKomplekt.Name = "customGridControlArtKomplekt";
            customGridControlArtKomplekt.Size = new System.Drawing.Size(546, 178);
            customGridControlArtKomplekt.TabIndex = 2;
            customGridControlArtKomplekt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewArtKomplekt });
            // 
            // gridViewArtKomplekt
            // 
            gridViewArtKomplekt.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            gridViewArtKomplekt.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewArtKomplekt.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            gridViewArtKomplekt.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridViewArtKomplekt.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewArtKomplekt.Appearance.FocusedRow.Options.UseFont = true;
            gridViewArtKomplekt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { akAkId, akParentNN, akParentPsaid, akTkId, gridColumn1 });
            gridViewArtKomplekt.GridControl = customGridControlArtKomplekt;
            gridViewArtKomplekt.Name = "gridViewArtKomplekt";
            gridViewArtKomplekt.OptionsView.EnableAppearanceEvenRow = true;
            gridViewArtKomplekt.OptionsView.ShowGroupPanel = false;
            // 
            // customGridControlPlanSezonAll
            // 
            customGridControlPlanSezonAll.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlPlanSezonAll.Location = new System.Drawing.Point(24, 45);
            customGridControlPlanSezonAll.MainView = gridViewPlanSezonAll;
            customGridControlPlanSezonAll.Name = "customGridControlPlanSezonAll";
            customGridControlPlanSezonAll.Size = new System.Drawing.Size(452, 478);
            customGridControlPlanSezonAll.TabIndex = 0;
            customGridControlPlanSezonAll.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewPlanSezonAll });
            // 
            // gridViewPlanSezonAll
            // 
            gridViewPlanSezonAll.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            gridViewPlanSezonAll.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewPlanSezonAll.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(230, 230, 250);
            gridViewPlanSezonAll.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridViewPlanSezonAll.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewPlanSezonAll.Appearance.FocusedRow.Options.UseFont = true;
            gridViewPlanSezonAll.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { psaGridColumnCheck, psaPsaid, psaNn, psaYear, psaSezon, psaTbid, psaArticul, psaMod, psaGridColumnButton });
            gridViewPlanSezonAll.GridControl = customGridControlPlanSezonAll;
            gridViewPlanSezonAll.Name = "gridViewPlanSezonAll";
            gridViewPlanSezonAll.OptionsView.EnableAppearanceEvenRow = true;
            gridViewPlanSezonAll.OptionsView.ShowGroupPanel = false;
            // 
            // psaGridColumnCheck
            // 
            psaGridColumnCheck.Caption = "psaGridColumnCheck";
            psaGridColumnCheck.Name = "psaGridColumnCheck";
            psaGridColumnCheck.Visible = true;
            psaGridColumnCheck.VisibleIndex = 0;
            // 
            // psaPsaid
            // 
            psaPsaid.Caption = "Psa_id";
            psaPsaid.FieldName = "Psa_id";
            psaPsaid.Name = "psaPsaid";
            psaPsaid.Visible = true;
            psaPsaid.VisibleIndex = 1;
            // 
            // psaNn
            // 
            psaNn.Caption = "№";
            psaNn.FieldName = "nn";
            psaNn.Name = "psaNn";
            psaNn.Visible = true;
            psaNn.VisibleIndex = 2;
            // 
            // psaYear
            // 
            psaYear.Caption = "Год";
            psaYear.FieldName = "nn.Substring(0, 4)";
            psaYear.Name = "psaYear";
            psaYear.Visible = true;
            psaYear.VisibleIndex = 3;
            // 
            // psaSezon
            // 
            psaSezon.Caption = "Сезон";
            psaSezon.Name = "psaSezon";
            psaSezon.Visible = true;
            psaSezon.VisibleIndex = 4;
            // 
            // psaTbid
            // 
            psaTbid.Caption = "Блок";
            psaTbid.FieldName = "tb_id";
            psaTbid.Name = "psaTbid";
            psaTbid.Visible = true;
            psaTbid.VisibleIndex = 5;
            // 
            // psaArticul
            // 
            psaArticul.Caption = "Артикул";
            psaArticul.FieldName = "Articul";
            psaArticul.Name = "psaArticul";
            psaArticul.Visible = true;
            psaArticul.VisibleIndex = 6;
            // 
            // psaMod
            // 
            psaMod.Caption = "Модель";
            psaMod.FieldName = "mod";
            psaMod.Name = "psaMod";
            psaMod.Visible = true;
            psaMod.VisibleIndex = 7;
            // 
            // psaGridColumnButton
            // 
            psaGridColumnButton.Caption = "psaGridColumnButton";
            psaGridColumnButton.Name = "psaGridColumnButton";
            psaGridColumnButton.Visible = true;
            psaGridColumnButton.VisibleIndex = 8;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1060, 547);
            Root.TextVisible = false;
            // 
            // splitterItem3
            // 
            splitterItem3.Location = new System.Drawing.Point(456, 0);
            splitterItem3.Name = "splitterItem3";
            splitterItem3.Size = new System.Drawing.Size(10, 482);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(466, 182);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(550, 251);
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customGridControlPlanSezonAll;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(456, 482);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = customGridControlArtKomplekt;
            layoutControlItem2.Location = new System.Drawing.Point(466, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(550, 182);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = customButtonSave;
            layoutControlItem3.Location = new System.Drawing.Point(764, 433);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(252, 49);
            layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.Location = new System.Drawing.Point(466, 433);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(298, 49);
            // 
            // akAkId
            // 
            akAkId.Caption = "akAkId";
            akAkId.FieldName = "ak_id";
            akAkId.Name = "akAkId";
            akAkId.Visible = true;
            akAkId.VisibleIndex = 0;
            // 
            // akParentNN
            // 
            akParentNN.Caption = "akParentNN";
            akParentNN.FieldName = "parent_nn";
            akParentNN.Name = "akParentNN";
            akParentNN.Visible = true;
            akParentNN.VisibleIndex = 1;
            // 
            // akParentPsaid
            // 
            akParentPsaid.Caption = "akParentPsaid";
            akParentPsaid.FieldName = "parent_psaid";
            akParentPsaid.Name = "akParentPsaid";
            akParentPsaid.Visible = true;
            akParentPsaid.VisibleIndex = 2;
            // 
            // akTkId
            // 
            akTkId.Caption = "akTkId";
            akTkId.FieldName = "tk_id";
            akTkId.Name = "akTkId";
            akTkId.Visible = true;
            akTkId.VisibleIndex = 3;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "gridColumn1";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 4;
            // 
            // EditNaborSostavMatr
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1060, 547);
            Controls.Add(layoutControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "EditNaborSostavMatr";
            Text = "Матрица";
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customGridControlArtKomplekt).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArtKomplekt).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlPlanSezonAll).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPlanSezonAll).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Core.Class.CustomGridControl customGridControlPlanSezonAll;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPlanSezonAll;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Core.Class.CustomGridControl customGridControlArtKomplekt;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewArtKomplekt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Core.Class.CustomButton customButtonSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn psaGridColumnCheck;
        private DevExpress.XtraGrid.Columns.GridColumn psaPsaid;
        private DevExpress.XtraGrid.Columns.GridColumn psaNn;
        private DevExpress.XtraGrid.Columns.GridColumn psaYear;
        private DevExpress.XtraGrid.Columns.GridColumn psaSezon;
        private DevExpress.XtraGrid.Columns.GridColumn psaTbid;
        private DevExpress.XtraGrid.Columns.GridColumn psaArticul;
        private DevExpress.XtraGrid.Columns.GridColumn psaGridColumnButton;
        private DevExpress.XtraGrid.Columns.GridColumn psaMod;
        private DevExpress.XtraGrid.Columns.GridColumn akAkId;
        private DevExpress.XtraGrid.Columns.GridColumn akParentNN;
        private DevExpress.XtraGrid.Columns.GridColumn akParentPsaid;
        private DevExpress.XtraGrid.Columns.GridColumn akTkId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}