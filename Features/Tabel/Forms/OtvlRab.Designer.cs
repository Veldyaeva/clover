using SewingProduction.CustomControls;

namespace SewingProduction.Features.Tabel.Forms
{
    partial class OtvlRab
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
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            customSearchLookUpEditFio = new Core.Class.CustomSearchLookUpEdit();
            customSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            customGridControlTabel = new Core.Class.CustomGridControl();
            gridViewTabel = new DevExpress.XtraGrid.Views.Grid.GridView();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            layoutControlItemFio = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            customCalendarControlTabel = new CustomCalendarControl();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            customButtonAll = new Core.Class.CustomButton();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            customButtonAdd = new Core.Class.CustomButton();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            customButtonDel = new Core.Class.CustomButton();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEditFio.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEdit1View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlTabel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTabel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemFio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customCalendarControlTabel.BottomPanel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customCalendarControlTabel.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(customButtonDel);
            layoutControl1.Controls.Add(customButtonAdd);
            layoutControl1.Controls.Add(customButtonAll);
            layoutControl1.Controls.Add(customCalendarControlTabel);
            layoutControl1.Controls.Add(customSearchLookUpEditFio);
            layoutControl1.Controls.Add(customGridControlTabel);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1210, 605);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // customSearchLookUpEditFio
            // 
            customSearchLookUpEditFio.Location = new System.Drawing.Point(583, 12);
            customSearchLookUpEditFio.Name = "customSearchLookUpEditFio";
            customSearchLookUpEditFio.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            customSearchLookUpEditFio.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customSearchLookUpEditFio.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(85, 45, 115);
            customSearchLookUpEditFio.Properties.Appearance.Options.UseBackColor = true;
            customSearchLookUpEditFio.Properties.Appearance.Options.UseFont = true;
            customSearchLookUpEditFio.Properties.Appearance.Options.UseForeColor = true;
            customSearchLookUpEditFio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            customSearchLookUpEditFio.Properties.PopupView = customSearchLookUpEdit1View;
            customSearchLookUpEditFio.Size = new System.Drawing.Size(316, 22);
            customSearchLookUpEditFio.StyleController = layoutControl1;
            customSearchLookUpEditFio.TabIndex = 0;
            // 
            // customSearchLookUpEdit1View
            // 
            customSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            customSearchLookUpEdit1View.Name = "customSearchLookUpEdit1View";
            customSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            customSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // customGridControlTabel
            // 
            customGridControlTabel.Font = new System.Drawing.Font("Arial", 10F);
            customGridControlTabel.Location = new System.Drawing.Point(12, 127);
            customGridControlTabel.MainView = gridViewTabel;
            customGridControlTabel.Name = "customGridControlTabel";
            customGridControlTabel.Size = new System.Drawing.Size(1186, 466);
            customGridControlTabel.TabIndex = 4;
            customGridControlTabel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTabel });
            // 
            // gridViewTabel
            // 
            gridViewTabel.Appearance.EvenRow.BackColor = System.Drawing.Color.Lavender;
            gridViewTabel.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewTabel.Appearance.FocusedRow.BackColor = System.Drawing.Color.Lavender;
            gridViewTabel.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridViewTabel.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewTabel.Appearance.FocusedRow.Options.UseFont = true;
            gridViewTabel.Appearance.Row.Options.UseBackColor = true;
            gridViewTabel.Appearance.Row.Options.UseForeColor = true;
            gridViewTabel.GridControl = customGridControlTabel;
            gridViewTabel.Name = "gridViewTabel";
            gridViewTabel.OptionsView.EnableAppearanceEvenRow = true;
            gridViewTabel.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, splitterItem3, layoutControlItem1, emptySpaceItem1, simpleSeparator2, splitterItem1, splitterItem2, layoutControlItem4, layoutControlItemFio, layoutControlItem5, layoutControlItem6 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1210, 605);
            Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = customGridControlTabel;
            layoutControlItem2.Location = new System.Drawing.Point(0, 115);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(1190, 470);
            layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            splitterItem1.Location = new System.Drawing.Point(0, 105);
            splitterItem1.Name = "splitterItem1";
            splitterItem1.Size = new System.Drawing.Size(1190, 10);
            // 
            // layoutControlItemFio
            // 
            layoutControlItemFio.Control = customSearchLookUpEditFio;
            layoutControlItemFio.Location = new System.Drawing.Point(536, 0);
            layoutControlItemFio.Name = "layoutControlItemFio";
            layoutControlItemFio.Size = new System.Drawing.Size(355, 26);
            layoutControlItemFio.Text = "ФИО";
            layoutControlItemFio.TextSize = new System.Drawing.Size(23, 13);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(891, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(299, 26);
            // 
            // simpleSeparator2
            // 
            simpleSeparator2.Location = new System.Drawing.Point(0, 26);
            simpleSeparator2.Name = "simpleSeparator2";
            simpleSeparator2.Size = new System.Drawing.Size(1190, 1);
            // 
            // splitterItem2
            // 
            splitterItem2.Location = new System.Drawing.Point(526, 0);
            splitterItem2.Name = "splitterItem2";
            splitterItem2.Size = new System.Drawing.Size(10, 26);
            // 
            // splitterItem3
            // 
            splitterItem3.Location = new System.Drawing.Point(405, 0);
            splitterItem3.Name = "splitterItem3";
            splitterItem3.Size = new System.Drawing.Size(121, 26);
            // 
            // customCalendarControlTabel
            // 
            customCalendarControlTabel.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            customCalendarControlTabel.Appearance.Options.UseFont = true;
            customCalendarControlTabel.AutoSize = false;
            // 
            // 
            // 
            customCalendarControlTabel.BottomPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            customCalendarControlTabel.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            customCalendarControlTabel.BottomPanel.Location = new System.Drawing.Point(0, 78);
            customCalendarControlTabel.BottomPanel.Name = "";
            customCalendarControlTabel.BottomPanel.Padding = new System.Windows.Forms.Padding(6);
            customCalendarControlTabel.BottomPanel.Size = new System.Drawing.Size(1186, 42);
            customCalendarControlTabel.BottomPanel.TabIndex = 0;
            customCalendarControlTabel.BottomPanel.Visible = false;
            customCalendarControlTabel.BottomPanelHeight = 42;
            customCalendarControlTabel.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            customCalendarControlTabel.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            customCalendarControlTabel.CustomSettings.BottomPanelHeight = 42;
            customCalendarControlTabel.CustomSettings.BottomPanelVisible = false;
            customCalendarControlTabel.CustomSettings.CycleStartDate = new System.DateTime(2026, 1, 19, 0, 0, 0, 0);
            customCalendarControlTabel.CustomSettings.HorizontalRowHeight = 42;
            customCalendarControlTabel.CustomSettings.LayoutMode = CalendarLayoutMode.HorizontalDays;
            customCalendarControlTabel.CustomSettings.NormalDayBackColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.CustomSettings.OffDayBackColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.CustomSettings.OffDayForeColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.CustomSettings.SpecialBackColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.CustomSettings.SpecialForeColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.CycleStartDate = new System.DateTime(2026, 1, 19, 0, 0, 0, 0);
            customCalendarControlTabel.HorizontalRowHeight = 42;
            customCalendarControlTabel.LayoutMode = CalendarLayoutMode.HorizontalDays;
            customCalendarControlTabel.Location = new System.Drawing.Point(12, 39);
            customCalendarControlTabel.Name = "customCalendarControlTabel";
            customCalendarControlTabel.NormalDayBackColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.OffDayBackColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.OffDayForeColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.Size = new System.Drawing.Size(1140, 74);
            customCalendarControlTabel.SpecialBackColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.SpecialForeColor = System.Drawing.Color.Empty;
            customCalendarControlTabel.StyleController = layoutControl1;
            customCalendarControlTabel.TabIndex = 2;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customCalendarControlTabel;
            layoutControlItem1.Location = new System.Drawing.Point(0, 27);
            layoutControlItem1.MaxSize = new System.Drawing.Size(0, 78);
            layoutControlItem1.MinSize = new System.Drawing.Size(1, 78);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(1144, 78);
            layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem1.TextVisible = false;
            // 
            // customButtonAll
            // 
            customButtonAll.BackColor = System.Drawing.Color.Lavender;
            customButtonAll.Font = new System.Drawing.Font("Arial", 10F);
            customButtonAll.ForeColor = System.Drawing.Color.DarkSlateBlue;
            customButtonAll.Location = new System.Drawing.Point(1156, 39);
            customButtonAll.Name = "customButtonAll";
            customButtonAll.Size = new System.Drawing.Size(42, 74);
            customButtonAll.TabIndex = 3;
            customButtonAll.Text = "Все";
            customButtonAll.UseVisualStyleBackColor = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = customButtonAll;
            layoutControlItem4.Location = new System.Drawing.Point(1144, 27);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(46, 78);
            layoutControlItem4.TextVisible = false;
            // 
            // customButtonAdd
            // 
            customButtonAdd.BackColor = System.Drawing.Color.Lavender;
            customButtonAdd.Font = new System.Drawing.Font("Arial", 10F);
            customButtonAdd.ForeColor = System.Drawing.Color.DarkSlateBlue;
            customButtonAdd.Location = new System.Drawing.Point(12, 12);
            customButtonAdd.Name = "customButtonAdd";
            customButtonAdd.Size = new System.Drawing.Size(198, 22);
            customButtonAdd.TabIndex = 5;
            customButtonAdd.Text = "Добавить запись";
            customButtonAdd.UseVisualStyleBackColor = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = customButtonAdd;
            layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(202, 26);
            layoutControlItem5.TextVisible = false;
            // 
            // customButtonDel
            // 
            customButtonDel.BackColor = System.Drawing.Color.Lavender;
            customButtonDel.Font = new System.Drawing.Font("Arial", 10F);
            customButtonDel.ForeColor = System.Drawing.Color.DarkSlateBlue;
            customButtonDel.Location = new System.Drawing.Point(214, 12);
            customButtonDel.Name = "customButtonDel";
            customButtonDel.Size = new System.Drawing.Size(199, 22);
            customButtonDel.TabIndex = 6;
            customButtonDel.Text = "Удалить запись";
            customButtonDel.UseVisualStyleBackColor = false;
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = customButtonDel;
            layoutControlItem6.Location = new System.Drawing.Point(202, 0);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(203, 26);
            layoutControlItem6.TextVisible = false;
            // 
            // OtvlRab
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(1210, 605);
            Controls.Add(layoutControl1);
            Name = "OtvlRab";
            Text = "Почасовая занятость работника склада";
            TransparencyKey = System.Drawing.Color.White;
            Load += OtvlRab_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEditFio.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)customSearchLookUpEdit1View).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControlTabel).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTabel).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemFio).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator2).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitterItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)customCalendarControlTabel.BottomPanel).EndInit();
            ((System.ComponentModel.ISupportInitialize)customCalendarControlTabel.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private Core.Class.CustomGridControl customGridControlTabel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTabel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Core.Class.CustomSearchLookUpEdit customSearchLookUpEditFio;
        private DevExpress.XtraGrid.Views.Grid.GridView customSearchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFio;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private CustomCalendarControl customCalendarControlTabel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Core.Class.CustomButton customButtonDel;
        private Core.Class.CustomButton customButtonAdd;
        private Core.Class.CustomButton customButtonAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}