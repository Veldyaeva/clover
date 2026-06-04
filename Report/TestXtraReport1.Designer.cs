namespace SewingProduction.Report
{
	partial class TestXtraReport1
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

		#region Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			DevExpress.DataAccess.Sql.SelectQuery selectQuery2 = new DevExpress.DataAccess.Sql.SelectQuery();
			DevExpress.DataAccess.Sql.Column column4 = new DevExpress.DataAccess.Sql.Column();
			DevExpress.DataAccess.Sql.ColumnExpression columnExpression4 = new DevExpress.DataAccess.Sql.ColumnExpression();
			DevExpress.DataAccess.Sql.Table table2 = new DevExpress.DataAccess.Sql.Table();
			DevExpress.DataAccess.Sql.Column column5 = new DevExpress.DataAccess.Sql.Column();
			DevExpress.DataAccess.Sql.ColumnExpression columnExpression5 = new DevExpress.DataAccess.Sql.ColumnExpression();
			DevExpress.DataAccess.Sql.Column column6 = new DevExpress.DataAccess.Sql.Column();
			DevExpress.DataAccess.Sql.ColumnExpression columnExpression6 = new DevExpress.DataAccess.Sql.ColumnExpression();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestXtraReport1));
			this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
			this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
			this.label1 = new DevExpress.XtraReports.UI.XRLabel();
			this.Detail = new DevExpress.XtraReports.UI.DetailBand();
			this.pageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
			this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
			this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
			this.GroupCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.GroupData1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailData1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.GroupFooterBackground3 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
			this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// TopMargin
			// 
			this.TopMargin.Dpi = 25.4F;
			this.TopMargin.HeightF = 0F;
			this.TopMargin.Name = "TopMargin";
			// 
			// BottomMargin
			// 
			this.BottomMargin.Dpi = 25.4F;
			this.BottomMargin.HeightF = 5.842F;
			this.BottomMargin.Name = "BottomMargin";
			// 
			// ReportHeader
			// 
			this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.label1});
			this.ReportHeader.Dpi = 25.4F;
			this.ReportHeader.HeightF = 15.24F;
			this.ReportHeader.Name = "ReportHeader";
			// 
			// label1
			// 
			this.label1.Dpi = 25.4F;
			this.label1.Font = new DevExpress.Drawing.DXFont("Arial", 22F, DevExpress.Drawing.DXFontStyle.Italic);
			this.label1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
			this.label1.Name = "label1";
			this.label1.SizeF = new System.Drawing.SizeF(60F, 15.24F);
			this.label1.StyleName = "Title";
			this.label1.StylePriority.UseFont = false;
			this.label1.Text = "tested";
			// 
			// Detail
			// 
			this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pageInfo1});
			this.Detail.Dpi = 25.4F;
			this.Detail.HeightF = 8.382002F;
			this.Detail.HierarchyPrintOptions.Indent = 5.08F;
			this.Detail.Name = "Detail";
			// 
			// pageInfo1
			// 
			this.pageInfo1.Dpi = 25.4F;
			this.pageInfo1.Font = new DevExpress.Drawing.DXFont("Arial", 12F, DevExpress.Drawing.DXFontStyle.Bold);
			this.pageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.540002F);
			this.pageInfo1.Name = "pageInfo1";
			this.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
			this.pageInfo1.SizeF = new System.Drawing.SizeF(60F, 5.842F);
			this.pageInfo1.StyleName = "PageInfo";
			this.pageInfo1.StylePriority.UseFont = false;
			// 
			// sqlDataSource1
			// 
			this.sqlDataSource1.ConnectionName = "SewingProduction.Properties.Settings.ACEConnectionString";
			this.sqlDataSource1.Name = "sqlDataSource1";
			columnExpression4.ColumnName = "id_atn";
			table2.Name = "all_table_name";
			columnExpression4.Table = table2;
			column4.Expression = columnExpression4;
			columnExpression5.ColumnName = "name";
			columnExpression5.Table = table2;
			column5.Expression = columnExpression5;
			columnExpression6.ColumnName = "name_rus";
			columnExpression6.Table = table2;
			column6.Expression = columnExpression6;
			selectQuery2.Columns.Add(column4);
			selectQuery2.Columns.Add(column5);
			selectQuery2.Columns.Add(column6);
			selectQuery2.Name = "all_table_name";
			selectQuery2.Tables.Add(table2);
			this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            selectQuery2});
			this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
			// 
			// Title
			// 
			this.Title.BackColor = System.Drawing.Color.Transparent;
			this.Title.BorderColor = System.Drawing.Color.Black;
			this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.Title.BorderWidth = 1F;
			this.Title.Font = new DevExpress.Drawing.DXFont("Arial", 14.25F);
			this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.Title.Name = "Title";
			this.Title.Padding = new DevExpress.XtraPrinting.PaddingInfo(1.524F, 1.524F, 0F, 0F, 25.4F);
			// 
			// GroupCaption1
			// 
			this.GroupCaption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.GroupCaption1.BorderColor = System.Drawing.Color.White;
			this.GroupCaption1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
			this.GroupCaption1.BorderWidth = 2F;
			this.GroupCaption1.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Bold);
			this.GroupCaption1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
			this.GroupCaption1.Name = "GroupCaption1";
			this.GroupCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(1.524F, 0.508F, 0F, 0F, 25.4F);
			this.GroupCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// GroupData1
			// 
			this.GroupData1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.GroupData1.BorderColor = System.Drawing.Color.White;
			this.GroupData1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
			this.GroupData1.BorderWidth = 2F;
			this.GroupData1.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Bold);
			this.GroupData1.ForeColor = System.Drawing.Color.White;
			this.GroupData1.Name = "GroupData1";
			this.GroupData1.Padding = new DevExpress.XtraPrinting.PaddingInfo(1.524F, 0.508F, 0F, 0F, 25.4F);
			this.GroupData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// DetailCaption1
			// 
			this.DetailCaption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.DetailCaption1.BorderColor = System.Drawing.Color.White;
			this.DetailCaption1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
			this.DetailCaption1.BorderWidth = 2F;
			this.DetailCaption1.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Bold);
			this.DetailCaption1.ForeColor = System.Drawing.Color.White;
			this.DetailCaption1.Name = "DetailCaption1";
			this.DetailCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(1.524F, 1.524F, 0F, 0F, 25.4F);
			this.DetailCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// DetailData1
			// 
			this.DetailData1.BorderColor = System.Drawing.Color.Transparent;
			this.DetailData1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
			this.DetailData1.BorderWidth = 2F;
			this.DetailData1.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F);
			this.DetailData1.ForeColor = System.Drawing.Color.Black;
			this.DetailData1.Name = "DetailData1";
			this.DetailData1.Padding = new DevExpress.XtraPrinting.PaddingInfo(1.524F, 1.524F, 0F, 0F, 25.4F);
			this.DetailData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// GroupFooterBackground3
			// 
			this.GroupFooterBackground3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(131)))), ((int)(((byte)(131)))));
			this.GroupFooterBackground3.BorderColor = System.Drawing.Color.White;
			this.GroupFooterBackground3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
			this.GroupFooterBackground3.BorderWidth = 2F;
			this.GroupFooterBackground3.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Bold);
			this.GroupFooterBackground3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
			this.GroupFooterBackground3.Name = "GroupFooterBackground3";
			this.GroupFooterBackground3.Padding = new DevExpress.XtraPrinting.PaddingInfo(1.524F, 0.508F, 0F, 0F, 25.4F);
			this.GroupFooterBackground3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// DetailData3_Odd
			// 
			this.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
			this.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent;
			this.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.DetailData3_Odd.BorderWidth = 1F;
			this.DetailData3_Odd.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F);
			this.DetailData3_Odd.ForeColor = System.Drawing.Color.Black;
			this.DetailData3_Odd.Name = "DetailData3_Odd";
			this.DetailData3_Odd.Padding = new DevExpress.XtraPrinting.PaddingInfo(1.524F, 1.524F, 0F, 0F, 25.4F);
			this.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// PageInfo
			// 
			this.PageInfo.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Bold);
			this.PageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.PageInfo.Name = "PageInfo";
			this.PageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(1.524F, 1.524F, 0F, 0F, 25.4F);
			// 
			// TestXtraReport1
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.Detail});
			this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
			this.DataMember = "all_table_name";
			this.DataSource = this.sqlDataSource1;
			this.Dpi = 25.4F;
			this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
			this.Margins = new DevExpress.Drawing.DXMargins(0F, 0F, 0F, 5.842F);
			this.PageHeightF = 17.5F;
			this.PageWidthF = 60F;
			this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
			this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Millimeters;
			this.SnapGridSize = 2.5F;
			this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.GroupCaption1,
            this.GroupData1,
            this.DetailCaption1,
            this.DetailData1,
            this.GroupFooterBackground3,
            this.DetailData3_Odd,
            this.PageInfo});
			this.Version = "25.2";
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
		private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
		private DevExpress.XtraReports.UI.XRPageInfo pageInfo1;
		private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
		private DevExpress.XtraReports.UI.XRLabel label1;
		private DevExpress.XtraReports.UI.DetailBand Detail;
		private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
		private DevExpress.XtraReports.UI.XRControlStyle Title;
		private DevExpress.XtraReports.UI.XRControlStyle GroupCaption1;
		private DevExpress.XtraReports.UI.XRControlStyle GroupData1;
		private DevExpress.XtraReports.UI.XRControlStyle DetailCaption1;
		private DevExpress.XtraReports.UI.XRControlStyle DetailData1;
		private DevExpress.XtraReports.UI.XRControlStyle GroupFooterBackground3;
		private DevExpress.XtraReports.UI.XRControlStyle DetailData3_Odd;
		private DevExpress.XtraReports.UI.XRControlStyle PageInfo;
	}
}
