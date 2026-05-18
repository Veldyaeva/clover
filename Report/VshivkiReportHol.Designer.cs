using DevExpress.DataAccess.Sql;

namespace SewingProduction.Report
{
	partial class VshivkiReportHol
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
			DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VshivkiReportHol));
			DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter8 = new DevExpress.DataAccess.Sql.QueryParameter();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter9 = new DevExpress.DataAccess.Sql.QueryParameter();
			DevExpress.DataAccess.Sql.QueryParameter queryParameter10 = new DevExpress.DataAccess.Sql.QueryParameter();
			this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
			this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			this.Detail = new DevExpress.XtraReports.UI.DetailBand();
			this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
			this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
			this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
			this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailData1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
			this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
			this._nomZad = new DevExpress.XtraReports.Parameters.Parameter();
			this._nom = new DevExpress.XtraReports.Parameters.Parameter();
			this._proizvType = new DevExpress.XtraReports.Parameters.Parameter();
			this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// TopMargin
			// 
			this.TopMargin.Dpi = 25.4F;
			this.TopMargin.HeightF = 0.03536991F;
			this.TopMargin.Name = "TopMargin";
			// 
			// BottomMargin
			// 
			this.BottomMargin.Dpi = 25.4F;
			this.BottomMargin.HeightF = 0.3278108F;
			this.BottomMargin.Name = "BottomMargin";
			// 
			// Detail
			// 
			this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
			this.Detail.Dpi = 25.4F;
			this.Detail.HeightF = 39.9701F;
			this.Detail.HierarchyPrintOptions.Indent = 5.08F;
			this.Detail.Name = "Detail";
			// 
			// xrLine1
			// 
			this.xrLine1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
			this.xrLine1.Dpi = 25.4F;
			this.xrLine1.LineStyle = DevExpress.Drawing.DXDashStyle.Dash;
			this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 37.4301F);
			this.xrLine1.Name = "xrLine1";
			this.xrLine1.SizeF = new System.Drawing.SizeF(24.92925F, 0.5291672F);
			// 
			// xrLabel4
			// 
			this.xrLabel4.AutoWidth = true;
			this.xrLabel4.CanShrink = true;
			this.xrLabel4.Dpi = 25.4F;
			this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[minNPach] + NewLine() + \' - \' + NewLine() + [maxNPach]")});
			this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 17.89263F);
			this.xrLabel4.Multiline = true;
			this.xrLabel4.Name = "xrLabel4";
			this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0.5291666F, 0.5291666F, 0F, 0F, 25.4F);
			this.xrLabel4.SizeF = new System.Drawing.SizeF(24.92926F, 19.53747F);
			this.xrLabel4.StylePriority.UseTextAlignment = false;
			this.xrLabel4.Text = "xrLabel1";
			this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// xrLabel3
			// 
			this.xrLabel3.Dpi = 25.4F;
			this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'Пачки:\'")});
			this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 14.224F);
			this.xrLabel3.Multiline = true;
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0.5291666F, 0.5291666F, 0F, 0F, 25.4F);
			this.xrLabel3.SizeF = new System.Drawing.SizeF(24.92926F, 3.668633F);
			this.xrLabel3.StylePriority.UseTextAlignment = false;
			this.xrLabel3.Text = "xrLabel1";
			this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// xrLabel2
			// 
			this.xrLabel2.Dpi = 25.4F;
			this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[mod]")});
			this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8.381998F);
			this.xrLabel2.Multiline = true;
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0.5291666F, 0.5291666F, 0F, 0F, 25.4F);
			this.xrLabel2.SizeF = new System.Drawing.SizeF(24.92926F, 5.841999F);
			this.xrLabel2.StylePriority.UseTextAlignment = false;
			this.xrLabel2.Text = "xrLabel1";
			this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// xrLabel1
			// 
			this.xrLabel1.Dpi = 25.4F;
			this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[articul_izd]")});
			this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.539998F);
			this.xrLabel1.Multiline = true;
			this.xrLabel1.Name = "xrLabel1";
			this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0.5291666F, 0.5291666F, 0F, 0F, 25.4F);
			this.xrLabel1.SizeF = new System.Drawing.SizeF(24.92926F, 5.842F);
			this.xrLabel1.StylePriority.UseTextAlignment = false;
			this.xrLabel1.Text = "xrLabel1";
			this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			// 
			// sqlDataSource1
			// 
			this.sqlDataSource1.ConnectionName = "SewingProduction.Properties.Settings.ACEConnectionString";
			this.sqlDataSource1.Name = "sqlDataSource1";
			storedProcQuery1.Name = "getVshivkiInfo";
			queryParameter1.Name = "@xNomZad";
			queryParameter1.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter1.Value = new DevExpress.DataAccess.Expression("?_nomZad", typeof(string));
			queryParameter2.Name = "@xNom";
			queryParameter2.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter2.Value = new DevExpress.DataAccess.Expression("?_nom", typeof(int));
			queryParameter3.Name = "@xProizvType";
			queryParameter3.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter3.Value = new DevExpress.DataAccess.Expression("?_proizvType", typeof(int));
			queryParameter4.Name = "@xIzdType";
			queryParameter4.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter4.Value = new DevExpress.DataAccess.Expression("?_izdType", typeof(int));
			storedProcQuery1.Parameters.AddRange(new DevExpress.DataAccess.Sql.QueryParameter[] {
            queryParameter1,
            queryParameter2,
			queryParameter3,
			queryParameter4});
			storedProcQuery1.StoredProcName = "getVshivkiInfo";
			this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
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
			// _nomZad
			// 
			this._nomZad.AllowNull = true;
			this._nomZad.Description = "nom_Zad";
			this._nomZad.Name = "_nomZad";
			// 
			// _nom
			// 
			this._nom.AllowNull = true;
			this._nom.Description = "nom";
			this._nom.Name = "_nom";
			this._nom.Type = typeof(int);
			this._nom.ValueInfo = "0";
			// 
			// _proizvType
			// 
			this._proizvType.AllowNull = true;
			this._proizvType.Description = "proizvType";
			this._proizvType.Name = "_proizvType";
			this._proizvType.Type = typeof(int);
			this._proizvType.ValueInfo = "0";
			// 
			// sqlDataSource2
			// 
			this.sqlDataSource2.ConnectionName = "SewingProduction.Properties.Settings.ACEConnectionString";
			this.sqlDataSource2.Name = "sqlDataSource2";
			storedProcQuery2.Name = "getVshivkiInfo";
			queryParameter6.Name = "@xNomZad";
			queryParameter6.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter6.Value = new DevExpress.DataAccess.Expression("", typeof(string));
			queryParameter7.Name = "@xNom";
			queryParameter7.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter7.Value = new DevExpress.DataAccess.Expression("0", typeof(int));
			queryParameter8.Name = "@xProizvType";
			queryParameter8.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter8.Value = new DevExpress.DataAccess.Expression("0", typeof(int));
			queryParameter9.Name = "@xIzdType";
			queryParameter9.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter9.Value = new DevExpress.DataAccess.Expression("0", typeof(int));
			queryParameter10.Name = "@xKod";
			queryParameter10.Type = typeof(global::DevExpress.DataAccess.Expression);
			queryParameter10.Value = new DevExpress.DataAccess.Expression("", typeof(string));
			storedProcQuery2.Parameters.AddRange(new DevExpress.DataAccess.Sql.QueryParameter[] {
            queryParameter6,
            queryParameter7,
            queryParameter8,
            queryParameter9,
            queryParameter10});
			storedProcQuery2.StoredProcName = "getVshivkiInfo";
			this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery2});
			this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
			// 
			// VshivkiReportHol
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
			this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1,
            this.sqlDataSource2});
			this.DataMember = "getVshivkiInfo";
			this.DataSource = this.sqlDataSource1;
			this.Dpi = 25.4F;
			this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
			this.Margins = new DevExpress.Drawing.DXMargins(0.03536991F, 0.03536991F, 0.03536991F, 0.3278108F);
			this.PageHeightF = 42F;
			this.PageWidthF = 25F;
			this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
			this.ParameterPanelLayoutItems.AddRange(new DevExpress.XtraReports.Parameters.ParameterPanelLayoutItem[] {
            new DevExpress.XtraReports.Parameters.ParameterLayoutItem(this._nomZad, DevExpress.XtraReports.Parameters.Orientation.Horizontal),
            new DevExpress.XtraReports.Parameters.ParameterLayoutItem(this._nom, DevExpress.XtraReports.Parameters.Orientation.Horizontal),
            new DevExpress.XtraReports.Parameters.ParameterLayoutItem(this._proizvType, DevExpress.XtraReports.Parameters.Orientation.Horizontal)});
			this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this._nomZad,
            this._nom,
            this._proizvType});
			this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Millimeters;
			this.SnapGridSize = 2.5F;
			this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.DetailCaption1,
            this.DetailData1,
            this.DetailData3_Odd,
            this.PageInfo});
			this.Version = "25.2";
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
		private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
		private DevExpress.XtraReports.UI.DetailBand Detail;
		private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
		private DevExpress.XtraReports.UI.XRControlStyle Title;
		private DevExpress.XtraReports.UI.XRControlStyle DetailCaption1;
		private DevExpress.XtraReports.UI.XRControlStyle DetailData1;
		private DevExpress.XtraReports.UI.XRControlStyle DetailData3_Odd;
		private DevExpress.XtraReports.UI.XRControlStyle PageInfo;
		private DevExpress.XtraReports.UI.XRLabel xrLabel4;
		private DevExpress.XtraReports.UI.XRLabel xrLabel3;
		private DevExpress.XtraReports.UI.XRLabel xrLabel2;
		private DevExpress.XtraReports.UI.XRLabel xrLabel1;
		private DevExpress.XtraReports.UI.XRLine xrLine1;
		private DevExpress.XtraReports.Parameters.Parameter _nomZad;
		private DevExpress.XtraReports.Parameters.Parameter _nom;
		private DevExpress.XtraReports.Parameters.Parameter _proizvType;
		private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
	}
}
