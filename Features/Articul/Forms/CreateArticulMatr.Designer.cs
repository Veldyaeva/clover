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
            dcTsn_name = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)gridArtMatr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArtMatr).BeginInit();
            SuspendLayout();
            // 
            // gridArtMatr
            // 
            gridArtMatr.Font = new System.Drawing.Font("Arial", 10F);
            gridArtMatr.Location = new System.Drawing.Point(13, 4);
            gridArtMatr.MainView = gridViewArtMatr;
            gridArtMatr.Name = "gridArtMatr";
            gridArtMatr.Size = new System.Drawing.Size(1341, 376);
            gridArtMatr.TabIndex = 0;
            gridArtMatr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewArtMatr });
            // 
            // gridViewArtMatr
            // 
            gridViewArtMatr.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridViewArtMatr.Appearance.FocusedRow.Options.UseFont = true;
            gridViewArtMatr.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gcGrupmen_name, dcTsn_name, gridColumn2 });
            gridViewArtMatr.GridControl = gridArtMatr;
            gridViewArtMatr.Name = "gridViewArtMatr";
            // 
            // gcGrupmen_name
            // 
            gcGrupmen_name.Caption = "gridColumn1";
            gcGrupmen_name.Name = "gcGrupmen_name";
            gcGrupmen_name.Visible = true;
            gcGrupmen_name.VisibleIndex = 0;
            // 
            // dcTsn_name
            // 
            dcTsn_name.Caption = "gridColumn1";
            dcTsn_name.Name = "dcTsn_name";
            dcTsn_name.Visible = true;
            dcTsn_name.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "gridColumn2";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2;
            // 
            // CreateArticulMatr
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1426, 557);
            Controls.Add(gridArtMatr);
            Name = "CreateArticulMatr";
            Text = "CreateArticulMatr";
            Load += CreateArticulMatr_Load;
            ((System.ComponentModel.ISupportInitialize)gridArtMatr).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewArtMatr).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Core.Class.CustomGridControl gridArtMatr;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewArtMatr;
        private DevExpress.XtraGrid.Columns.GridColumn gcGrupmen_name;
        private DevExpress.XtraGrid.Columns.GridColumn dcTsn_name;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}