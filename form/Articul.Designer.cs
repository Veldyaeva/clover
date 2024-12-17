using System.Drawing;

namespace SewingProduction.form
{
    partial class Articul
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
            this.components = new System.ComponentModel.Container();
            this.gridArt = new DevExpress.XtraGrid.GridControl();
            this.bsArt = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gridArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridArt
            // 
            this.gridArt.DataSource = this.bsArt;
            this.gridArt.Location = new System.Drawing.Point(1, 1);
            this.gridArt.MainView = this.gridControl1;
            this.gridArt.Name = "gridArt";
            this.gridArt.Size = new System.Drawing.Size(464, 581);
            this.gridArt.TabIndex = 1;
            this.gridArt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridControl1});
            // 
            // gridControl1
            // 
            this.gridControl1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridControl1.GridControl = this.gridArt;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.OptionsBehavior.Editable = false;
            this.gridControl1.OptionsFind.AlwaysVisible = true;
            this.gridControl1.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Filter;
            this.gridControl1.OptionsView.ShowAutoFilterRow = true;
            this.gridControl1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Код";
            this.gridColumn1.FieldName = "kod";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 78;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Группа";
            this.gridColumn2.FieldName = "grup";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 94;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Артикул";
            this.gridColumn3.FieldName = "articul";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 84;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Размер";
            this.gridColumn4.FieldName = "razm";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 94;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Модель";
            this.gridColumn5.FieldName = "mod";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 102;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "ТМ";
            this.gridColumn6.FieldName = "kle";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 20;
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl1.Location = new System.Drawing.Point(471, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(714, 198);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "sfsfsd";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // stackPanel1
            // 
            this.stackPanel1.Appearance.Options.UseBackColor = true;
            this.stackPanel1.Appearance.Options.UseBorderColor = true;
            this.stackPanel1.Location = new System.Drawing.Point(471, 205);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(725, 260);
            this.stackPanel1.TabIndex = 3;
            this.stackPanel1.UseSkinIndents = true;
            // 
            // Articul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 700);
            this.Controls.Add(this.stackPanel1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gridArt);
            this.Name = "Articul";
            this.Text = "Справочник изделий";
            this.Load += new System.EventHandler(this.Articul_Load);
            this.Shown += new System.EventHandler(this.Articul_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gridArt;
        private DevExpress.XtraGrid.Views.Grid.GridView gridControl1;
        private System.Windows.Forms.BindingSource bsArt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
    }
}