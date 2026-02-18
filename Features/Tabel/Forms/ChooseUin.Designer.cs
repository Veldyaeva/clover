namespace SewingProduction.Features.Tabel.Forms
{
    partial class ChooseUin
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
            customLayoutControl1 = new SewingProduction.Core.Class.CustomLayoutControl();
            customSimpleButton1 = new SewingProduction.Core.Class.CustomSimpleButton();
            customGridControl1 = new SewingProduction.Core.Class.CustomGridControl();
            gridViewSpisok = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridSpisokUin = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokTab1c = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokMiddleName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokBDay = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokOrgName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokPodrName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokDateU = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokDateP = new DevExpress.XtraGrid.Columns.GridColumn();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).BeginInit();
            customLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSpisok).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            SuspendLayout();
            // 
            // customLayoutControl1
            // 
            // customLayoutControl1.BackColor = System.Drawing.Color.White;
            customLayoutControl1.Controls.Add(customSimpleButton1);
            customLayoutControl1.Controls.Add(customGridControl1);
            customLayoutControl1.Font = new System.Drawing.Font("Arial", 10F);
            // customLayoutControl1.ForeColor = System.Drawing.Color.Black;
            customLayoutControl1.Location = new System.Drawing.Point(12, 12);
            customLayoutControl1.Name = "customLayoutControl1";
            customLayoutControl1.Root = Root;
            customLayoutControl1.Size = new System.Drawing.Size(1151, 428);
            customLayoutControl1.TabIndex = 0;
            customLayoutControl1.Text = "customLayoutControl1";
            // 
            // customSimpleButton1
            // 
            // customSimpleButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            customSimpleButton1.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            // customSimpleButton1.Appearance.ForeColor = System.Drawing.Color.Black;
            customSimpleButton1.Appearance.Options.UseBackColor = true;
            customSimpleButton1.Appearance.Options.UseFont = true;
            customSimpleButton1.Appearance.Options.UseForeColor = true;
            customSimpleButton1.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            customSimpleButton1.AppearanceDisabled.ForeColor = System.Drawing.Color.GreenYellow;
            customSimpleButton1.AppearanceDisabled.Options.UseBackColor = true;
            customSimpleButton1.AppearanceDisabled.Options.UseForeColor = true;
            customSimpleButton1.Location = new System.Drawing.Point(12, 394);
            customSimpleButton1.Name = "customSimpleButton1";
            customSimpleButton1.Size = new System.Drawing.Size(1127, 22);
            customSimpleButton1.StyleController = customLayoutControl1;
            customSimpleButton1.TabIndex = 5;
            customSimpleButton1.Text = "Подтвердить";
            customSimpleButton1.Click += customSimpleButton1_Click;
            // 
            // customGridControl1
            // 
            customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl1.Location = new System.Drawing.Point(12, 12);
            customGridControl1.MainView = gridViewSpisok;
            customGridControl1.Name = "customGridControl1";
            customGridControl1.Size = new System.Drawing.Size(1127, 378);
            customGridControl1.TabIndex = 4;
            customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSpisok });
            // 
            // gridViewSpisok
            // 
            gridViewSpisok.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.ActiveBorder;
            gridViewSpisok.Appearance.EvenRow.Options.UseBackColor = true;
            // gridViewSpisok.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            gridViewSpisok.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridViewSpisok.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewSpisok.Appearance.FocusedRow.Options.UseFont = true;
            gridViewSpisok.Appearance.Row.Options.UseBackColor = true;
            gridViewSpisok.Appearance.Row.Options.UseForeColor = true;
            gridViewSpisok.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridSpisokUin, gridSpisokTab1c, gridSpisokLastName, gridSpisokFirstName, gridSpisokMiddleName, gridSpisokBDay, gridSpisokOrgName, gridSpisokPodrName, gridSpisokDateU, gridSpisokDateP });
            gridViewSpisok.GridControl = customGridControl1;
            gridViewSpisok.Name = "gridViewSpisok";
            gridViewSpisok.RowCellClick += gridViewSpisok_RowCellClick;
            // 
            // gridSpisokUin
            // 
            gridSpisokUin.Caption = "Уин";
            gridSpisokUin.Name = "gridSpisokUin";
            gridSpisokUin.Visible = true;
            gridSpisokUin.VisibleIndex = 0;
            // 
            // gridSpisokTab1c
            // 
            gridSpisokTab1c.Caption = "Табельный 1с";
            gridSpisokTab1c.Name = "gridSpisokTab1c";
            gridSpisokTab1c.Visible = true;
            gridSpisokTab1c.VisibleIndex = 1;
            // 
            // gridSpisokLastName
            // 
            gridSpisokLastName.Caption = "Фамилия";
            gridSpisokLastName.Name = "gridSpisokLastName";
            gridSpisokLastName.Visible = true;
            gridSpisokLastName.VisibleIndex = 2;
            // 
            // gridSpisokFirstName
            // 
            gridSpisokFirstName.Caption = "Имя";
            gridSpisokFirstName.Name = "gridSpisokFirstName";
            gridSpisokFirstName.Visible = true;
            gridSpisokFirstName.VisibleIndex = 3;
            // 
            // gridSpisokMiddleName
            // 
            gridSpisokMiddleName.Caption = "Отчество";
            gridSpisokMiddleName.Name = "gridSpisokMiddleName";
            gridSpisokMiddleName.Visible = true;
            gridSpisokMiddleName.VisibleIndex = 4;
            // 
            // gridSpisokBDay
            // 
            gridSpisokBDay.Caption = "День рождения";
            gridSpisokBDay.Name = "gridSpisokBDay";
            gridSpisokBDay.Visible = true;
            gridSpisokBDay.VisibleIndex = 5;
            // 
            // gridSpisokOrgName
            // 
            gridSpisokOrgName.Caption = "Наименование организации";
            gridSpisokOrgName.Name = "gridSpisokOrgName";
            gridSpisokOrgName.Visible = true;
            gridSpisokOrgName.VisibleIndex = 6;
            // 
            // gridSpisokPodrName
            // 
            gridSpisokPodrName.Caption = "Наименование подразделения";
            gridSpisokPodrName.Name = "gridSpisokPodrName";
            gridSpisokPodrName.Visible = true;
            gridSpisokPodrName.VisibleIndex = 7;
            // 
            // gridSpisokDateU
            // 
            gridSpisokDateU.Caption = "Дата увольнения";
            gridSpisokDateU.Name = "gridSpisokDateU";
            gridSpisokDateU.Visible = true;
            gridSpisokDateU.VisibleIndex = 8;
            // 
            // gridSpisokDateP
            // 
            gridSpisokDateP.Caption = "Дата принятия";
            gridSpisokDateP.Name = "gridSpisokDateP";
            gridSpisokDateP.Visible = true;
            gridSpisokDateP.VisibleIndex = 9;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1151, 428);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customGridControl1;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(1131, 382);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = customSimpleButton1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 382);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(1131, 26);
            layoutControlItem2.TextVisible = false;
            // 
            // ChooseUin
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1177, 450);
            Controls.Add(customLayoutControl1);
            Name = "ChooseUin";
            Text = "ChooseUin";
            Load += ChooseUin_Load;
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).EndInit();
            customLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customGridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSpisok).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Core.Class.CustomLayoutControl customLayoutControl1;
        private Core.Class.CustomGridControl customGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSpisok;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokUin;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokTab1c;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokLastName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokFirstName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokMiddleName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokBDay;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokOrgName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokPodrName;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokDateU;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokDateP;
        private Core.Class.CustomSimpleButton customSimpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}