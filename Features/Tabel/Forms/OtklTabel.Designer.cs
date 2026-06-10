namespace SewingProduction.Features.Tabel.Forms
{
    partial class OtklTabel
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

        private Core.Class.CustomLayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        private Core.Class.CustomLookUpEdit lookUpPodrazdelenie;
        private Core.Class.CustomLookUpEdit lookUpMonth;
        private Core.Class.CustomSimpleButton btnPrint;
        private Core.Class.CustomSimpleButton btnApply;
        private Core.Class.CustomSimpleButton btnClose;

        private Core.Class.CustomGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;

        private DevExpress.XtraGrid.Columns.GridColumn colPodrazdelenie;
        private DevExpress.XtraGrid.Columns.GridColumn colFio;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraGrid.Columns.GridColumn colDayInTab;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeIn;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeOut;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;

        private DevExpress.XtraLayout.LayoutControlItem lciPodrazdelenie;
        private DevExpress.XtraLayout.LayoutControlItem lciMonth;
        private DevExpress.XtraLayout.LayoutControlItem lciPrint;
        private DevExpress.XtraLayout.LayoutControlItem lciApply;
        private DevExpress.XtraLayout.LayoutControlItem lciGrid;
        private DevExpress.XtraLayout.LayoutControlItem lciClose;

        private DevExpress.XtraLayout.SimpleLabelItem lblTitle;
        private DevExpress.XtraLayout.SimpleLabelItem lblMonthYear;
        private DevExpress.XtraLayout.EmptySpaceItem emptyRightBottom;


        private void InitializeComponent()
        {
            layoutControl1 = new SewingProduction.Core.Class.CustomLayoutControl();
            lookUpPodrazdelenie = new SewingProduction.Core.Class.CustomLookUpEdit();
            lookUpMonth = new SewingProduction.Core.Class.CustomLookUpEdit();
            btnPrint = new SewingProduction.Core.Class.CustomSimpleButton();
            btnApply = new SewingProduction.Core.Class.CustomSimpleButton();
            gridControl1 = new SewingProduction.Core.Class.CustomGridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colPodrazdelenie = new DevExpress.XtraGrid.Columns.GridColumn();
            colFio = new DevExpress.XtraGrid.Columns.GridColumn();
            colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            colDayInTab = new DevExpress.XtraGrid.Columns.GridColumn();
            colTimeIn = new DevExpress.XtraGrid.Columns.GridColumn();
            colTimeOut = new DevExpress.XtraGrid.Columns.GridColumn();
            colMinute = new DevExpress.XtraGrid.Columns.GridColumn();
            colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            btnClose = new SewingProduction.Core.Class.CustomSimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            lblTitle = new DevExpress.XtraLayout.SimpleLabelItem();
            lciPodrazdelenie = new DevExpress.XtraLayout.LayoutControlItem();
            lblMonthYear = new DevExpress.XtraLayout.SimpleLabelItem();
            lciMonth = new DevExpress.XtraLayout.LayoutControlItem();
            lciPrint = new DevExpress.XtraLayout.LayoutControlItem();
            lciApply = new DevExpress.XtraLayout.LayoutControlItem();
            lciGrid = new DevExpress.XtraLayout.LayoutControlItem();
            emptyRightBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            lciClose = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lookUpPodrazdelenie.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lookUpMonth.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lblTitle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciPodrazdelenie).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lblMonthYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciPrint).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciApply).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptyRightBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lciClose).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(lookUpPodrazdelenie);
            layoutControl1.Controls.Add(lookUpMonth);
            layoutControl1.Controls.Add(btnPrint);
            layoutControl1.Controls.Add(btnApply);
            layoutControl1.Controls.Add(gridControl1);
            layoutControl1.Controls.Add(btnClose);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Font = new System.Drawing.Font("Arial", 10F);
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1305, 644);
            layoutControl1.TabIndex = 0;
            // 
            // lookUpPodrazdelenie
            // 
            lookUpPodrazdelenie.Location = new System.Drawing.Point(12, 46);
            lookUpPodrazdelenie.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lookUpPodrazdelenie.Name = "lookUpPodrazdelenie";
            lookUpPodrazdelenie.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            lookUpPodrazdelenie.Properties.NullText = "";
            lookUpPodrazdelenie.Size = new System.Drawing.Size(227, 20);
            lookUpPodrazdelenie.StyleController = layoutControl1;
            lookUpPodrazdelenie.TabIndex = 0;
            // 
            // lookUpMonth
            // 
            lookUpMonth.Location = new System.Drawing.Point(243, 47);
            lookUpMonth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lookUpMonth.Name = "lookUpMonth";
            lookUpMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            lookUpMonth.Properties.NullText = "";
            lookUpMonth.Size = new System.Drawing.Size(991, 20);
            lookUpMonth.StyleController = layoutControl1;
            lookUpMonth.TabIndex = 4;
            // 
            // btnPrint
            // 
            btnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            btnPrint.Appearance.ForeColor = System.Drawing.Color.Purple;
            btnPrint.Appearance.Options.UseFont = true;
            btnPrint.Appearance.Options.UseForeColor = true;
            btnPrint.Location = new System.Drawing.Point(1238, 30);
            btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new System.Drawing.Size(55, 22);
            btnPrint.StyleController = layoutControl1;
            btnPrint.TabIndex = 6;
            btnPrint.Text = "Печать";
            btnPrint.Click += btnPrint_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new System.Drawing.Point(243, 71);
            btnApply.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnApply.Name = "btnApply";
            btnApply.Size = new System.Drawing.Size(1050, 22);
            btnApply.StyleController = layoutControl1;
            btnApply.TabIndex = 8;
            btnApply.Text = "Применить";
            btnApply.Click += btnApply_Click;
            // 
            // gridControl1
            // 
            gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridControl1.Font = new System.Drawing.Font("Arial", 10F);
            gridControl1.Location = new System.Drawing.Point(12, 97);
            gridControl1.MainView = gridView1;
            gridControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(1281, 509);
            gridControl1.TabIndex = 10;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colPodrazdelenie, colFio, colDate, colReason, colDayInTab, colTimeIn, colTimeOut, colMinute, colNote });
            gridView1.DetailHeight = 404;
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsEditForm.PopupEditFormWidth = 933;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            // 
            // colPodrazdelenie
            // 
            colPodrazdelenie.Caption = "Подразделение";
            colPodrazdelenie.FieldName = "podrazdelenie";
            colPodrazdelenie.MinWidth = 23;
            colPodrazdelenie.Name = "colPodrazdelenie";
            colPodrazdelenie.Visible = true;
            colPodrazdelenie.VisibleIndex = 0;
            colPodrazdelenie.Width = 163;
            // 
            // colFio
            // 
            colFio.Caption = "Ф.И.О.";
            colFio.FieldName = "fio";
            colFio.MinWidth = 23;
            colFio.Name = "colFio";
            colFio.Visible = true;
            colFio.VisibleIndex = 1;
            colFio.Width = 122;
            // 
            // colDate
            // 
            colDate.Caption = "Дата";
            colDate.FieldName = "data";
            colDate.MinWidth = 23;
            colDate.Name = "colDate";
            colDate.Visible = true;
            colDate.VisibleIndex = 2;
            colDate.Width = 64;
            // 
            // colReason
            // 
            colReason.Caption = "Причина";
            colReason.FieldName = "prichina";
            colReason.MinWidth = 23;
            colReason.Name = "colReason";
            colReason.Visible = true;
            colReason.VisibleIndex = 3;
            colReason.Width = 181;
            // 
            // colDayInTab
            // 
            colDayInTab.Caption = "День в таб.";
            colDayInTab.FieldName = "den_v_tab";
            colDayInTab.MinWidth = 23;
            colDayInTab.Name = "colDayInTab";
            colDayInTab.Visible = true;
            colDayInTab.VisibleIndex = 4;
            colDayInTab.Width = 93;
            // 
            // colTimeIn
            // 
            colTimeIn.Caption = "Время вх.";
            colTimeIn.FieldName = "vremya_vh";
            colTimeIn.MinWidth = 23;
            colTimeIn.Name = "colTimeIn";
            colTimeIn.Visible = true;
            colTimeIn.VisibleIndex = 5;
            colTimeIn.Width = 138;
            // 
            // colTimeOut
            // 
            colTimeOut.Caption = "Время вых.";
            colTimeOut.FieldName = "vremya_vyh";
            colTimeOut.MinWidth = 23;
            colTimeOut.Name = "colTimeOut";
            colTimeOut.Visible = true;
            colTimeOut.VisibleIndex = 6;
            colTimeOut.Width = 353;
            // 
            // colMinute
            // 
            colMinute.Caption = "Минуты";
            colMinute.Name = "colMinute";
            colMinute.Visible = true;
            colMinute.VisibleIndex = 7;
            // 
            // colNote
            // 
            colNote.Caption = "Примечание";
            colNote.FieldName = "primechanie";
            colNote.MinWidth = 23;
            colNote.Name = "colNote";
            colNote.Visible = true;
            colNote.VisibleIndex = 8;
            colNote.Width = 92;
            // 
            // btnClose
            // 
            btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            btnClose.Appearance.ForeColor = System.Drawing.Color.Purple;
            btnClose.Appearance.Options.UseFont = true;
            btnClose.Appearance.Options.UseForeColor = true;
            btnClose.Location = new System.Drawing.Point(1226, 610);
            btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(67, 22);
            btnClose.StyleController = layoutControl1;
            btnClose.TabIndex = 11;
            btnClose.Text = "Закрыть";
            btnClose.Click += btnClose_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lblTitle, lciPodrazdelenie, lblMonthYear, lciMonth, lciPrint, lciApply, lciGrid, emptyRightBottom, lciClose });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1305, 644);
            Root.TextVisible = false;
            // 
            // lblTitle
            // 
            lblTitle.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            lblTitle.AppearanceItemCaption.ForeColor = System.Drawing.Color.Purple;
            lblTitle.AppearanceItemCaption.Options.UseFont = true;
            lblTitle.AppearanceItemCaption.Options.UseForeColor = true;
            lblTitle.AppearanceItemCaption.Options.UseTextOptions = true;
            lblTitle.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblTitle.Location = new System.Drawing.Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(1285, 18);
            lblTitle.Text = "Подразделение/предприятие";
            lblTitle.TextSize = new System.Drawing.Size(190, 14);
            // 
            // lciPodrazdelenie
            // 
            lciPodrazdelenie.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            lciPodrazdelenie.AppearanceItemCaption.ForeColor = System.Drawing.Color.Purple;
            lciPodrazdelenie.AppearanceItemCaption.Options.UseFont = true;
            lciPodrazdelenie.AppearanceItemCaption.Options.UseForeColor = true;
            lciPodrazdelenie.Control = lookUpPodrazdelenie;
            lciPodrazdelenie.Location = new System.Drawing.Point(0, 18);
            lciPodrazdelenie.Name = "lciPodrazdelenie";
            lciPodrazdelenie.Size = new System.Drawing.Size(231, 67);
            lciPodrazdelenie.Text = "Выберите подразделение";
            lciPodrazdelenie.TextLocation = DevExpress.Utils.Locations.Top;
            lciPodrazdelenie.TextSize = new System.Drawing.Size(190, 13);
            // 
            // lblMonthYear
            // 
            lblMonthYear.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            lblMonthYear.AppearanceItemCaption.ForeColor = System.Drawing.Color.Purple;
            lblMonthYear.AppearanceItemCaption.Options.UseFont = true;
            lblMonthYear.AppearanceItemCaption.Options.UseForeColor = true;
            lblMonthYear.CustomizationFormText = "Определите месяц";
            lblMonthYear.Location = new System.Drawing.Point(231, 18);
            lblMonthYear.Name = "lblMonthYear";
            lblMonthYear.Size = new System.Drawing.Size(995, 17);
            lblMonthYear.Text = "Определите месяц/год";
            lblMonthYear.TextSize = new System.Drawing.Size(190, 13);
            // 
            // lciMonth
            // 
            lciMonth.Control = lookUpMonth;
            lciMonth.Location = new System.Drawing.Point(231, 35);
            lciMonth.Name = "lciMonth";
            lciMonth.Size = new System.Drawing.Size(995, 24);
            lciMonth.TextVisible = false;
            // 
            // lciPrint
            // 
            lciPrint.Control = btnPrint;
            lciPrint.Location = new System.Drawing.Point(1226, 18);
            lciPrint.Name = "lciPrint";
            lciPrint.Size = new System.Drawing.Size(59, 41);
            lciPrint.TextVisible = false;
            // 
            // lciApply
            // 
            lciApply.Control = btnApply;
            lciApply.Location = new System.Drawing.Point(231, 59);
            lciApply.Name = "lciApply";
            lciApply.Size = new System.Drawing.Size(1054, 26);
            lciApply.TextVisible = false;
            // 
            // lciGrid
            // 
            lciGrid.Control = gridControl1;
            lciGrid.Location = new System.Drawing.Point(0, 85);
            lciGrid.Name = "lciGrid";
            lciGrid.Size = new System.Drawing.Size(1285, 513);
            lciGrid.TextVisible = false;
            // 
            // emptyRightBottom
            // 
            emptyRightBottom.Location = new System.Drawing.Point(0, 598);
            emptyRightBottom.Name = "emptyRightBottom";
            emptyRightBottom.Size = new System.Drawing.Size(1214, 26);
            // 
            // lciClose
            // 
            lciClose.Control = btnClose;
            lciClose.Location = new System.Drawing.Point(1214, 598);
            lciClose.Name = "lciClose";
            lciClose.Size = new System.Drawing.Size(71, 26);
            lciClose.TextVisible = false;
            // 
            // OtklTabel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1305, 644);
            Controls.Add(layoutControl1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(1037, 683);
            Name = "OtklTabel";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Отклонения от рабочего времени";
            Load += OtklTabel_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lookUpPodrazdelenie.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)lookUpMonth.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)lblTitle).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciPodrazdelenie).EndInit();
            ((System.ComponentModel.ISupportInitialize)lblMonthYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciPrint).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciApply).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptyRightBottom).EndInit();
            ((System.ComponentModel.ISupportInitialize)lciClose).EndInit();
            ResumeLayout(false);
            #endregion
        }
        private DevExpress.XtraGrid.Columns.GridColumn colMinute;
    }
}