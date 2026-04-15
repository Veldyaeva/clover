namespace ExchangeApp.Forms
{
    partial class FrmExchangeManager
    {
        private System.ComponentModel.IContainer components = null;

        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;

        private DevExpress.XtraEditors.LookUpEdit lueCompany;
        private DevExpress.XtraEditors.DateEdit deFrom;
        private DevExpress.XtraEditors.DateEdit deTo;
        private DevExpress.XtraEditors.RadioGroup rgMode;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccbeExportTypes;

        private DevExpress.XtraEditors.LabelControl lblCompany;
        private DevExpress.XtraEditors.LabelControl lblFrom;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.LabelControl lblMode;
        private DevExpress.XtraEditors.LabelControl lblExportTypes;
        private DevExpress.XtraEditors.LabelControl lblHint;

        private DevExpress.XtraEditors.SimpleButton btnLoadDocuments;
        private DevExpress.XtraEditors.SimpleButton btnRun;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnSelectAllDocuments;
        private DevExpress.XtraEditors.SimpleButton btnUnselectAllDocuments;
        private DevExpress.XtraEditors.SimpleButton btnClose;

        private DevExpress.XtraGrid.GridControl gcDocuments;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDocuments;

        private DevExpress.XtraGrid.GridControl gcBatches;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBatches;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();

            this.lueCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.deFrom = new DevExpress.XtraEditors.DateEdit();
            this.deTo = new DevExpress.XtraEditors.DateEdit();
            this.rgMode = new DevExpress.XtraEditors.RadioGroup();
            this.ccbeExportTypes = new DevExpress.XtraEditors.CheckedComboBoxEdit();

            this.lblCompany = new DevExpress.XtraEditors.LabelControl();
            this.lblFrom = new DevExpress.XtraEditors.LabelControl();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.lblMode = new DevExpress.XtraEditors.LabelControl();
            this.lblExportTypes = new DevExpress.XtraEditors.LabelControl();
            this.lblHint = new DevExpress.XtraEditors.LabelControl();

            this.btnLoadDocuments = new DevExpress.XtraEditors.SimpleButton();
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAllDocuments = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnselectAllDocuments = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();

            this.gcDocuments = new DevExpress.XtraGrid.GridControl();
            this.gvDocuments = new DevExpress.XtraGrid.Views.Grid.GridView();

            this.gcBatches = new DevExpress.XtraGrid.GridControl();
            this.gvBatches = new DevExpress.XtraGrid.Views.Grid.GridView();

            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.lueCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccbeExportTypes.Properties)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.gcDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocuments)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.gcBatches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBatches)).BeginInit();

            this.SuspendLayout();

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1600, 95);
            this.panelTop.TabIndex = 0;

            // panelBottom
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 810);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1600, 60);
            this.panelBottom.TabIndex = 1;

            // splitMain
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 95);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1.Controls.Add(this.gcDocuments);
            this.splitMain.Panel2.Controls.Add(this.gcBatches);
            this.splitMain.Size = new System.Drawing.Size(1600, 715);
            this.splitMain.SplitterPosition = 900;
            this.splitMain.TabIndex = 2;

            // lblCompany
            this.lblCompany.Location = new System.Drawing.Point(12, 12);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(72, 16);
            this.lblCompany.Text = "Организация";

            // lueCompany
            this.lueCompany.Location = new System.Drawing.Point(12, 34);
            this.lueCompany.Name = "lueCompany";
            this.lueCompany.Size = new System.Drawing.Size(240, 22);
            this.lueCompany.TabIndex = 0;

            // lblFrom
            this.lblFrom.Location = new System.Drawing.Point(272, 12);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(8, 16);
            this.lblFrom.Text = "С";

            // deFrom
            this.deFrom.EditValue = null;
            this.deFrom.Location = new System.Drawing.Point(272, 34);
            this.deFrom.Name = "deFrom";
            this.deFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[]
            {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
            });
            this.deFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[]
            {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
            });
            this.deFrom.Size = new System.Drawing.Size(110, 22);
            this.deFrom.TabIndex = 1;

            // lblTo
            this.lblTo.Location = new System.Drawing.Point(398, 12);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(16, 16);
            this.lblTo.Text = "По";

            // deTo
            this.deTo.EditValue = null;
            this.deTo.Location = new System.Drawing.Point(398, 34);
            this.deTo.Name = "deTo";
            this.deTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[]
            {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
            });
            this.deTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[]
            {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
            });
            this.deTo.Size = new System.Drawing.Size(110, 22);
            this.deTo.TabIndex = 2;

            // lblMode
            this.lblMode.Location = new System.Drawing.Point(526, 12);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(40, 16);
            this.lblMode.Text = "Режим";

            // rgMode
            this.rgMode.Location = new System.Drawing.Point(526, 29);
            this.rgMode.Name = "rgMode";
            this.rgMode.Size = new System.Drawing.Size(360, 30);
            this.rgMode.TabIndex = 3;
            this.rgMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ExchangeApp.Models.ExportRunMode.Primary, "Первичная"));
            this.rgMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ExchangeApp.Models.ExportRunMode.Delta, "Догрузка"));
            this.rgMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ExchangeApp.Models.ExportRunMode.Reexport, "Перевыгрузка"));
            this.rgMode.SelectedIndexChanged += new System.EventHandler(this.rgMode_SelectedIndexChanged);

            // lblExportTypes
            this.lblExportTypes.Location = new System.Drawing.Point(904, 12);
            this.lblExportTypes.Name = "lblExportTypes";
            this.lblExportTypes.Size = new System.Drawing.Size(86, 16);
            this.lblExportTypes.Text = "Виды выгрузки";

            // ccbeExportTypes
            this.ccbeExportTypes.Location = new System.Drawing.Point(904, 34);
            this.ccbeExportTypes.Name = "ccbeExportTypes";
            this.ccbeExportTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[]
            {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
            });
            this.ccbeExportTypes.Size = new System.Drawing.Size(260, 22);
            this.ccbeExportTypes.TabIndex = 4;

            // lblHint
            this.lblHint.Location = new System.Drawing.Point(12, 67);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(0, 16);

            // gcDocuments
            this.gcDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDocuments.Location = new System.Drawing.Point(0, 0);
            this.gcDocuments.MainView = this.gvDocuments;
            this.gcDocuments.Name = "gcDocuments";
            this.gcDocuments.Size = new System.Drawing.Size(900, 715);
            this.gcDocuments.TabIndex = 0;
            this.gcDocuments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gvDocuments
            });

            // gvDocuments
            this.gvDocuments.GridControl = this.gcDocuments;
            this.gvDocuments.Name = "gvDocuments";
            this.gvDocuments.OptionsBehavior.Editable = true;
            this.gvDocuments.OptionsView.ShowAutoFilterRow = true;
            this.gvDocuments.OptionsView.ShowGroupPanel = false;

            // gcBatches
            this.gcBatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBatches.Location = new System.Drawing.Point(0, 0);
            this.gcBatches.MainView = this.gvBatches;
            this.gcBatches.Name = "gcBatches";
            this.gcBatches.Size = new System.Drawing.Size(688, 715);
            this.gcBatches.TabIndex = 0;
            this.gcBatches.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gvBatches
            });

            // gvBatches
            this.gvBatches.GridControl = this.gcBatches;
            this.gvBatches.Name = "gvBatches";
            this.gvBatches.OptionsBehavior.Editable = false;
            this.gvBatches.OptionsView.ShowAutoFilterRow = true;
            this.gvBatches.OptionsView.ShowGroupPanel = false;

            // btnLoadDocuments
            this.btnLoadDocuments.Location = new System.Drawing.Point(12, 14);
            this.btnLoadDocuments.Name = "btnLoadDocuments";
            this.btnLoadDocuments.Size = new System.Drawing.Size(160, 30);
            this.btnLoadDocuments.TabIndex = 0;
            this.btnLoadDocuments.Text = "Загрузить документы";
            this.btnLoadDocuments.Click += new System.EventHandler(this.btnLoadDocuments_Click);

            // btnRun
            this.btnRun.Location = new System.Drawing.Point(178, 14);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(130, 30);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Выполнить";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(314, 14);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Обновить пакеты";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // btnSelectAllDocuments
            this.btnSelectAllDocuments.Location = new System.Drawing.Point(470, 14);
            this.btnSelectAllDocuments.Name = "btnSelectAllDocuments";
            this.btnSelectAllDocuments.Size = new System.Drawing.Size(160, 30);
            this.btnSelectAllDocuments.TabIndex = 3;
            this.btnSelectAllDocuments.Text = "Отметить все";
            this.btnSelectAllDocuments.Click += new System.EventHandler(this.btnSelectAllDocuments_Click);

            // btnUnselectAllDocuments
            this.btnUnselectAllDocuments.Location = new System.Drawing.Point(636, 14);
            this.btnUnselectAllDocuments.Name = "btnUnselectAllDocuments";
            this.btnUnselectAllDocuments.Size = new System.Drawing.Size(160, 30);
            this.btnUnselectAllDocuments.TabIndex = 4;
            this.btnUnselectAllDocuments.Text = "Снять все";
            this.btnUnselectAllDocuments.Click += new System.EventHandler(this.btnUnselectAllDocuments_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(802, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Закрыть";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // top panel controls
            this.panelTop.Controls.Add(this.lblCompany);
            this.panelTop.Controls.Add(this.lueCompany);
            this.panelTop.Controls.Add(this.lblFrom);
            this.panelTop.Controls.Add(this.deFrom);
            this.panelTop.Controls.Add(this.lblTo);
            this.panelTop.Controls.Add(this.deTo);
            this.panelTop.Controls.Add(this.lblMode);
            this.panelTop.Controls.Add(this.rgMode);
            this.panelTop.Controls.Add(this.lblExportTypes);
            this.panelTop.Controls.Add(this.ccbeExportTypes);
            this.panelTop.Controls.Add(this.lblHint);

            // bottom panel controls
            this.panelBottom.Controls.Add(this.btnLoadDocuments);
            this.panelBottom.Controls.Add(this.btnRun);
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Controls.Add(this.btnSelectAllDocuments);
            this.panelBottom.Controls.Add(this.btnUnselectAllDocuments);
            this.panelBottom.Controls.Add(this.btnClose);

            // form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 870);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "FrmExchangeManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление выгрузкой / догрузкой / перевыгрузкой";
            this.Load += new System.EventHandler(this.FrmExchangeManager_Load);

            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.lueCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccbeExportTypes.Properties)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.gcDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBatches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBatches)).EndInit();

            this.ResumeLayout(false);
        }
    }
}