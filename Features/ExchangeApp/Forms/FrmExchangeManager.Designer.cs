using SewingProduction.Core.Class;

namespace ExchangeApp.Forms
{
    partial class FrmExchangeManager
    {
        private System.ComponentModel.IContainer components = null;

        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private DevExpress.XtraEditors.DateEdit deFrom;
        private DevExpress.XtraEditors.DateEdit deTo;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccbeExportTypes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDocuments;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBatches;
        //private DevExpress.XtraEditors.LookUpEdit lueCompany;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExchangeManager));
            panelTop = new DevExpress.XtraEditors.PanelControl();
            lblCompany = new CustomLabel();
            lueCompany = new CustomLookUpEdit();
            lblFrom = new CustomLabel();
            deFrom = new DevExpress.XtraEditors.DateEdit();
            lblTo = new CustomLabel();
            deTo = new DevExpress.XtraEditors.DateEdit();
            lblMode = new CustomLabel();
            rgMode = new CustomRadioGroup();
            lblExportTypes = new CustomLabel();
            ccbeExportTypes = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            lblHint = new CustomLabel();
            panelBottom = new DevExpress.XtraEditors.PanelControl();
            btnLoadDocuments = new CustomSimpleButton();
            btnRun = new CustomSimpleButton();
            btnRefresh = new CustomSimpleButton();
            btnSelectAllDocuments = new CustomSimpleButton();
            btnUnselectAllDocuments = new CustomSimpleButton();
            btnClose = new CustomSimpleButton();
            splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            gcDocuments = new CustomGridControl();
            gvDocuments = new DevExpress.XtraGrid.Views.Grid.GridView();
            gcBatches = new CustomGridControl();
            gvBatches = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)panelTop).BeginInit();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lueCompany.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)deFrom.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)deFrom.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)deTo.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)deTo.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rgMode.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ccbeExportTypes.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelBottom).BeginInit();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitMain.Panel1).BeginInit();
            splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain.Panel2).BeginInit();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gcDocuments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gvDocuments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gcBatches).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gvBatches).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(lblCompany);
            panelTop.Controls.Add(lueCompany);
            panelTop.Controls.Add(lblFrom);
            panelTop.Controls.Add(deFrom);
            panelTop.Controls.Add(lblTo);
            panelTop.Controls.Add(deTo);
            panelTop.Controls.Add(lblMode);
            panelTop.Controls.Add(rgMode);
            panelTop.Controls.Add(lblExportTypes);
            panelTop.Controls.Add(ccbeExportTypes);
            panelTop.Controls.Add(lblHint);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(1575, 89);
            panelTop.TabIndex = 0;
            // 
            // lblCompany
            // 
            lblCompany.Appearance.Options.UseTextOptions = true;
            lblCompany.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            lblCompany.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblCompany.Location = new System.Drawing.Point(10, 11);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new System.Drawing.Size(66, 13);
            lblCompany.TabIndex = 0;
            lblCompany.Text = "Организация";
            lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lueCompany
            // 
            lueCompany.Location = new System.Drawing.Point(10, 32);
            lueCompany.Name = "lueCompany";
            lueCompany.Size = new System.Drawing.Size(210, 20);
            lueCompany.TabIndex = 0;
            // 
            // lblFrom
            // 
            lblFrom.Appearance.Options.UseTextOptions = true;
            lblFrom.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            lblFrom.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblFrom.Location = new System.Drawing.Point(238, 11);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new System.Drawing.Size(7, 13);
            lblFrom.TabIndex = 1;
            lblFrom.Text = "С";
            lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // deFrom
            // 
            deFrom.EditValue = null;
            deFrom.Location = new System.Drawing.Point(238, 32);
            deFrom.Name = "deFrom";
            deFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            deFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            deFrom.Size = new System.Drawing.Size(96, 20);
            deFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            lblTo.Appearance.Options.UseTextOptions = true;
            lblTo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            lblTo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblTo.Location = new System.Drawing.Point(348, 11);
            lblTo.Name = "lblTo";
            lblTo.Size = new System.Drawing.Size(13, 13);
            lblTo.TabIndex = 2;
            lblTo.Text = "По";
            lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // deTo
            // 
            deTo.EditValue = null;
            deTo.Location = new System.Drawing.Point(348, 32);
            deTo.Name = "deTo";
            deTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            deTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            deTo.Size = new System.Drawing.Size(96, 20);
            deTo.TabIndex = 2;
            // 
            // lblMode
            // 
            lblMode.Appearance.Options.UseTextOptions = true;
            lblMode.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            lblMode.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblMode.Location = new System.Drawing.Point(460, 11);
            lblMode.Name = "lblMode";
            lblMode.Size = new System.Drawing.Size(32, 13);
            lblMode.TabIndex = 3;
            lblMode.Text = "Режим";
            lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rgMode
            // 
            rgMode.Location = new System.Drawing.Point(460, 27);
            rgMode.Name = "rgMode";
            rgMode.ObjectName = null;
            rgMode.Size = new System.Drawing.Size(315, 28);
            rgMode.TabIndex = 3;
            rgMode.SelectedIndexChanged += rgMode_SelectedIndexChanged;
            // 
            // lblExportTypes
            // 
            lblExportTypes.Appearance.Options.UseTextOptions = true;
            lblExportTypes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            lblExportTypes.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblExportTypes.Location = new System.Drawing.Point(791, 11);
            lblExportTypes.Name = "lblExportTypes";
            lblExportTypes.Size = new System.Drawing.Size(78, 13);
            lblExportTypes.TabIndex = 4;
            lblExportTypes.Text = "Виды выгрузки";
            lblExportTypes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ccbeExportTypes
            // 
            ccbeExportTypes.Location = new System.Drawing.Point(791, 32);
            ccbeExportTypes.Name = "ccbeExportTypes";
            ccbeExportTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            ccbeExportTypes.Size = new System.Drawing.Size(228, 20);
            ccbeExportTypes.TabIndex = 4;
            // 
            // lblHint
            // 
            lblHint.Appearance.Options.UseTextOptions = true;
            lblHint.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            lblHint.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblHint.Location = new System.Drawing.Point(10, 63);
            lblHint.Name = "lblHint";
            lblHint.Size = new System.Drawing.Size(0, 13);
            lblHint.TabIndex = 5;
            lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnLoadDocuments);
            panelBottom.Controls.Add(btnRun);
            panelBottom.Controls.Add(btnRefresh);
            panelBottom.Controls.Add(btnSelectAllDocuments);
            panelBottom.Controls.Add(btnUnselectAllDocuments);
            panelBottom.Controls.Add(btnClose);
            panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelBottom.Location = new System.Drawing.Point(0, 760);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new System.Drawing.Size(1575, 56);
            panelBottom.TabIndex = 1;
            // 
            // btnLoadDocuments
            // 
            btnLoadDocuments.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnLoadDocuments.ImageOptions.Image");
            btnLoadDocuments.Location = new System.Drawing.Point(10, 13);
            btnLoadDocuments.Name = "btnLoadDocuments";
            btnLoadDocuments.Size = new System.Drawing.Size(171, 28);
            btnLoadDocuments.TabIndex = 0;
            btnLoadDocuments.Text = "Загрузить документы";
            btnLoadDocuments.Click += btnLoadDocuments_Click;
            // 
            // btnRun
            // 
            btnRun.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnRun.ImageOptions.Image");
            btnRun.Location = new System.Drawing.Point(209, 13);
            btnRun.Name = "btnRun";
            btnRun.Size = new System.Drawing.Size(171, 28);
            btnRun.TabIndex = 1;
            btnRun.Text = "Выполнить";
            btnRun.Click += btnRun_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnRefresh.ImageOptions.Image");
            btnRefresh.Location = new System.Drawing.Point(405, 13);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(171, 28);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Обновить пакеты";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnSelectAllDocuments
            // 
            btnSelectAllDocuments.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnSelectAllDocuments.ImageOptions.Image");
            btnSelectAllDocuments.Location = new System.Drawing.Point(604, 13);
            btnSelectAllDocuments.Name = "btnSelectAllDocuments";
            btnSelectAllDocuments.Size = new System.Drawing.Size(171, 28);
            btnSelectAllDocuments.TabIndex = 3;
            btnSelectAllDocuments.Text = "Отметить все";
            btnSelectAllDocuments.Click += btnSelectAllDocuments_Click;
            // 
            // btnUnselectAllDocuments
            // 
            btnUnselectAllDocuments.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnUnselectAllDocuments.ImageOptions.Image");
            btnUnselectAllDocuments.Location = new System.Drawing.Point(797, 13);
            btnUnselectAllDocuments.Name = "btnUnselectAllDocuments";
            btnUnselectAllDocuments.Size = new System.Drawing.Size(171, 28);
            btnUnselectAllDocuments.TabIndex = 4;
            btnUnselectAllDocuments.Text = "Снять все";
            btnUnselectAllDocuments.Click += btnUnselectAllDocuments_Click;
            // 
            // btnClose
            // 
            btnClose.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnClose.ImageOptions.Image");
            btnClose.Location = new System.Drawing.Point(994, 13);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(171, 28);
            btnClose.TabIndex = 5;
            btnClose.Text = "Закрыть";
            btnClose.Click += btnClose_Click;
            // 
            // splitMain
            // 
            splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitMain.Location = new System.Drawing.Point(0, 89);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(gcDocuments);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(gcBatches);
            splitMain.Size = new System.Drawing.Size(1575, 671);
            splitMain.SplitterPosition = 787;
            splitMain.TabIndex = 2;
            // 
            // gcDocuments
            // 
            gcDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            gcDocuments.Font = new System.Drawing.Font("Arial", 10F);
            gcDocuments.Location = new System.Drawing.Point(0, 0);
            gcDocuments.MainView = gvDocuments;
            gcDocuments.Name = "gcDocuments";
            gcDocuments.Size = new System.Drawing.Size(787, 671);
            gcDocuments.TabIndex = 0;
            gcDocuments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvDocuments });
            // 
            // gvDocuments
            // 
            gvDocuments.DetailHeight = 328;
            gvDocuments.GridControl = gcDocuments;
            gvDocuments.Name = "gvDocuments";
            gvDocuments.OptionsEditForm.PopupEditFormWidth = 700;
            gvDocuments.OptionsView.ShowAutoFilterRow = true;
            gvDocuments.OptionsView.ShowGroupPanel = false;
            gvDocuments.RowCellStyle += gvDocuments_RowCellStyle;
            gvDocuments.CustomColumnDisplayText += gvDocuments_CustomColumnDisplayText;
            // 
            // gcBatches
            // 
            gcBatches.Dock = System.Windows.Forms.DockStyle.Fill;
            gcBatches.Font = new System.Drawing.Font("Arial", 10F);
            gcBatches.Location = new System.Drawing.Point(0, 0);
            gcBatches.MainView = gvBatches;
            gcBatches.Name = "gcBatches";
            gcBatches.Size = new System.Drawing.Size(778, 671);
            gcBatches.TabIndex = 0;
            gcBatches.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvBatches });
            // 
            // gvBatches
            // 
            gvBatches.DetailHeight = 328;
            gvBatches.GridControl = gcBatches;
            gvBatches.Name = "gvBatches";
            gvBatches.OptionsBehavior.Editable = false;
            gvBatches.OptionsEditForm.PopupEditFormWidth = 700;
            gvBatches.OptionsView.ShowAutoFilterRow = true;
            gvBatches.OptionsView.ShowGroupPanel = false;
            gvBatches.RowCellStyle += gvBatches_RowCellStyle;
            gvBatches.CustomColumnDisplayText += gvBatches_CustomColumnDisplayText;
            // 
            // FrmExchangeManager
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1575, 816);
            Controls.Add(splitMain);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Name = "FrmExchangeManager";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Управление выгрузкой / догрузкой / перевыгрузкой";
            Load += FrmExchangeManager_Load;
            ((System.ComponentModel.ISupportInitialize)panelTop).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)lueCompany.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)deFrom.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)deFrom.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)deTo.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)deTo.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)rgMode.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ccbeExportTypes.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelBottom).EndInit();
            panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain.Panel1).EndInit();
            splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain.Panel2).EndInit();
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gcDocuments).EndInit();
            ((System.ComponentModel.ISupportInitialize)gvDocuments).EndInit();
            ((System.ComponentModel.ISupportInitialize)gcBatches).EndInit();
            ((System.ComponentModel.ISupportInitialize)gvBatches).EndInit();
            ResumeLayout(false);
        }
        private CustomLookUpEdit lueCompany;
        private CustomRadioGroup rgMode;
        private CustomLabel lblCompany;
        private CustomLabel lblFrom;
        private CustomLabel lblTo;
        private CustomLabel lblMode;
        private CustomLabel lblExportTypes;
        private CustomLabel lblHint;
        private CustomSimpleButton btnLoadDocuments;
        private CustomSimpleButton btnRun;
        private CustomSimpleButton btnRefresh;
        private CustomSimpleButton btnSelectAllDocuments;
        private CustomSimpleButton btnUnselectAllDocuments;
        private CustomSimpleButton btnClose;
        private CustomGridControl gcDocuments;
        private CustomGridControl gcBatches;
    }
}