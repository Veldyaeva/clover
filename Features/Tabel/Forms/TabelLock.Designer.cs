namespace SewingProduction.Features.Tabel.Forms
{
    partial class TabelLock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabelLock));
            customLayoutControl1 = new SewingProduction.Core.Class.CustomLayoutControl();
            label2 = new System.Windows.Forms.Label();
            customLookUpEditTB = new SewingProduction.Core.Class.CustomLookUpEdit();
            customGridControlTL = new SewingProduction.Core.Class.CustomGridControl();
            gridViewTL = new DevExpress.XtraGrid.Views.Grid.GridView();
            TslID = new DevExpress.XtraGrid.Columns.GridColumn();
            TslMes = new DevExpress.XtraGrid.Columns.GridColumn();
            TslGod = new DevExpress.XtraGrid.Columns.GridColumn();
            TslMg = new DevExpress.XtraGrid.Columns.GridColumn();
            TslMgDate = new DevExpress.XtraGrid.Columns.GridColumn();
            TslDateTo = new DevExpress.XtraGrid.Columns.GridColumn();
            TslTsltID = new DevExpress.XtraGrid.Columns.GridColumn();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItemTB = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).BeginInit();
            customLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customLookUpEditTB.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlTL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            SuspendLayout();
            // 
            // customLayoutControl1
            // 
            customLayoutControl1.Controls.Add(label2);
            customLayoutControl1.Controls.Add(customLookUpEditTB);
            customLayoutControl1.Controls.Add(customGridControlTL);
            customLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            customLayoutControl1.Font = new System.Drawing.Font("Arial", 10F);
            customLayoutControl1.Location = new System.Drawing.Point(0, 0);
            customLayoutControl1.Name = "customLayoutControl1";
            customLayoutControl1.Root = Root;
            customLayoutControl1.Size = new System.Drawing.Size(812, 603);
            customLayoutControl1.TabIndex = 0;
            customLayoutControl1.Text = "customLayoutControl1";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label2.Location = new System.Drawing.Point(12, 571);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(788, 20);
            label2.TabIndex = 37;
            label2.Text = "🖱 Двойной щелчок для редактирования даты";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLookUpEditTB
            // 
            customLookUpEditTB.Location = new System.Drawing.Point(150, 12);
            customLookUpEditTB.Name = "customLookUpEditTB";
            customLookUpEditTB.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            customLookUpEditTB.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            customLookUpEditTB.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
            customLookUpEditTB.Properties.Appearance.Options.UseBackColor = true;
            customLookUpEditTB.Properties.Appearance.Options.UseFont = true;
            customLookUpEditTB.Properties.Appearance.Options.UseForeColor = true;
            customLookUpEditTB.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            customLookUpEditTB.Properties.AppearanceDisabled.Options.UseFont = true;
            customLookUpEditTB.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            customLookUpEditTB.Properties.AppearanceDropDown.Options.UseFont = true;
            customLookUpEditTB.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            customLookUpEditTB.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            customLookUpEditTB.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            customLookUpEditTB.Properties.AppearanceFocused.Options.UseFont = true;
            customLookUpEditTB.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            customLookUpEditTB.Properties.AppearanceReadOnly.Options.UseFont = true;
            customLookUpEditTB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            customLookUpEditTB.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] { new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TsltID", "№"), new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Tslt_Name", "тип") });
            customLookUpEditTB.Size = new System.Drawing.Size(650, 30);
            customLookUpEditTB.StyleController = customLayoutControl1;
            customLookUpEditTB.TabIndex = 6;
            customLookUpEditTB.EditValueChanged += customLookUpEditTB_EditValueChanged;
            // 
            // customGridControlTL
            // 
            customGridControlTL.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlTL.Location = new System.Drawing.Point(12, 46);
            customGridControlTL.MainView = gridViewTL;
            customGridControlTL.Name = "customGridControlTL";
            customGridControlTL.Size = new System.Drawing.Size(788, 521);
            customGridControlTL.TabIndex = 4;
            customGridControlTL.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTL });
            // 
            // gridViewTL
            // 
            gridViewTL.Appearance.FocusedRow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            gridViewTL.Appearance.FocusedRow.Options.UseFont = true;
            gridViewTL.Appearance.GroupFooter.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            gridViewTL.Appearance.GroupFooter.Options.UseFont = true;
            gridViewTL.Appearance.GroupRow.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            gridViewTL.Appearance.GroupRow.Options.UseFont = true;
            gridViewTL.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            gridViewTL.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewTL.Appearance.Preview.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            gridViewTL.Appearance.Preview.Options.UseFont = true;
            gridViewTL.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            gridViewTL.Appearance.Row.Options.UseFont = true;
            gridViewTL.Appearance.TopNewRow.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            gridViewTL.Appearance.TopNewRow.Options.UseFont = true;
            gridViewTL.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { TslID, TslMes, TslGod, TslMg, TslMgDate, TslDateTo, TslTsltID });
            gridViewTL.GridControl = customGridControlTL;
            gridViewTL.Name = "gridViewTL";
            gridViewTL.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridViewTL.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridViewTL.OptionsCustomization.AllowFilter = false;
            gridViewTL.OptionsEditForm.EditFormColumnCount = 1;
            gridViewTL.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            gridViewTL.OptionsFilter.AllowFilterEditor = false;
            gridViewTL.OptionsMenu.EnableColumnMenu = false;
            gridViewTL.OptionsMenu.EnableFooterMenu = false;
            gridViewTL.OptionsMenu.EnableGroupPanelMenu = false;
            gridViewTL.OptionsView.EnableAppearanceEvenRow = true;
            gridViewTL.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridViewTL.OptionsView.ShowFooter = true;
            gridViewTL.OptionsView.ShowGroupPanel = false;
            gridViewTL.RowUpdated += gridViewTL_RowUpdated;
            // 
            // TslID
            // 
            TslID.Caption = "TslID";
            TslID.FieldName = "TslID";
            TslID.Name = "TslID";
            TslID.OptionsColumn.AllowEdit = false;
            TslID.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            TslID.Width = 64;
            // 
            // TslMes
            // 
            TslMes.Caption = "TslMes";
            TslMes.FieldName = "Tsl_Mes";
            TslMes.Name = "TslMes";
            TslMes.OptionsColumn.AllowEdit = false;
            TslMes.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            TslMes.Width = 62;
            // 
            // TslGod
            // 
            TslGod.Caption = "Год";
            TslGod.FieldName = "Tsl_God";
            TslGod.Name = "TslGod";
            TslGod.OptionsColumn.AllowEdit = false;
            TslGod.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            TslGod.Visible = true;
            TslGod.VisibleIndex = 0;
            TslGod.Width = 85;
            // 
            // TslMg
            // 
            TslMg.Caption = "TslMg";
            TslMg.FieldName = "Tsl_Mg";
            TslMg.Name = "TslMg";
            TslMg.OptionsColumn.AllowEdit = false;
            TslMg.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            TslMg.Width = 115;
            // 
            // TslMgDate
            // 
            TslMgDate.Caption = "Месяц";
            TslMgDate.DisplayFormat.FormatString = "MMMM";
            TslMgDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            TslMgDate.FieldName = "Tsl_Mg_Date";
            TslMgDate.Name = "TslMgDate";
            TslMgDate.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            TslMgDate.Visible = true;
            TslMgDate.VisibleIndex = 1;
            // 
            // TslDateTo
            // 
            TslDateTo.Caption = "Дата закрытия редактирования";
            TslDateTo.FieldName = "Tsl_DateTo";
            TslDateTo.Name = "TslDateTo";
            TslDateTo.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            TslDateTo.Visible = true;
            TslDateTo.VisibleIndex = 2;
            TslDateTo.Width = 321;
            // 
            // TslTsltID
            // 
            TslTsltID.Caption = "TslTsltID";
            TslTsltID.FieldName = "Tsl_TsltID";
            TslTsltID.Name = "TslTsltID";
            TslTsltID.OptionsColumn.AllowEdit = false;
            TslTsltID.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            TslTsltID.Width = 116;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItemTB, layoutControlItem2 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(812, 603);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customGridControlTL;
            layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(792, 525);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItemTB
            // 
            layoutControlItemTB.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            layoutControlItemTB.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItemTB.AppearanceItemCaptionDisabled.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            layoutControlItemTB.AppearanceItemCaptionDisabled.Options.UseFont = true;
            layoutControlItemTB.Control = customLookUpEditTB;
            layoutControlItemTB.Location = new System.Drawing.Point(0, 0);
            layoutControlItemTB.Name = "layoutControlItemTB";
            layoutControlItemTB.Size = new System.Drawing.Size(792, 34);
            layoutControlItemTB.Text = "Табель / План";
            layoutControlItemTB.TextSize = new System.Drawing.Size(126, 23);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = label2;
            layoutControlItem2.Location = new System.Drawing.Point(0, 559);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(792, 24);
            layoutControlItem2.TextVisible = false;
            // 
            // TabelLock
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(812, 603);
            Controls.Add(customLayoutControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "TabelLock";
            Text = "Блокировка изменений в табеле";
            Load += TabelLock_Load;
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).EndInit();
            customLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customLookUpEditTB.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlTL).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTL).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Core.Class.CustomLayoutControl customLayoutControl1;
        private Core.Class.CustomGridControl customGridControlTL;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTL;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Core.Class.CustomLookUpEdit customLookUpEditTB;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTB;
        private DevExpress.XtraGrid.Columns.GridColumn TslID;
        private DevExpress.XtraGrid.Columns.GridColumn TslMes;
        private DevExpress.XtraGrid.Columns.GridColumn TslGod;
        private DevExpress.XtraGrid.Columns.GridColumn TslMg;
        private DevExpress.XtraGrid.Columns.GridColumn TslDateTo;
        private DevExpress.XtraGrid.Columns.GridColumn TslTsltID;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn TslMgDate;
    }
}