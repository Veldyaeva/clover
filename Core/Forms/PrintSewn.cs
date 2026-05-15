using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Class;
using SewingProduction.Core.Models;
using SewingProduction.Core.services;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Report;

namespace SewingProduction.Core.Forms
{
	public partial class PrintSewn : CustomForm
	{
		private string _kod;
		private string _model;
		private List<PrintSewnRazmKolRow> _printSewnRazmKolRow;
		private readonly PrintSewnDataService _printSewnDataService = new PrintSewnDataService();
		private List<PrintSewnBlVshRow> _blVshRows = new List<PrintSewnBlVshRow>();
		private string _nom;
		private string _nomZad;
		private int _proizvType = 0;

		//-- @xProizvType:
		//-- 0 - raskr_zeh_up
		//-- 1 - raskr_zeh_vyaz WHEN vsa.grupp NOT IN(10,30,33)
		//-- 2 - raskr_zeh_vyaz WHEN vsa.grupp IN(10)
		//-- 3 - raskr_zeh_vyaz WHEN vsa.grupp IN(30, 33)
		public PrintSewn(UserClass user) : base(user)
		{
			InitializeComponent();
		}
		public PrintSewn(string kod)
		{
			_kod = kod;
			//_model = model; 
			//_proizvType = proizvType;
			InitializeComponent();
		}
		public PrintSewn(string nom, string nomZad, int proizvType)
		{
			_nom = nom;
			_nomZad = nomZad;
			_proizvType = proizvType;
			InitializeComponent();
		}
		private async void PrintSewn_Load(object sender, EventArgs e)
		{
			customGridControlRzuRzv.DataSource = await _printSewnDataService.GetViewRzuRzv(
				_nomZad ?? null,
				_nom ?? null,
				_kod ?? null
				);
		}

		#region широкие ЭЙС, Клевер (новый)
		private void customSimpleButtonACE_Click(object sender, EventArgs e)
		{
			createReport(0);
		}
		#endregion
		#region Комплекты  
		private void customSimpleButtonKompl_Click(object sender, EventArgs e)
		{
			createReport(1);
		}
		#endregion
		#region Набор одежды 
		private void customSimpleButtonNabor_Click(object sender, EventArgs e)
		{
			createReport(2);
		}
		#endregion
		private void createReport(int _izdType = 0)
		{
			VshivkiReport report1 = new VshivkiReport();
			requestParameters(report1, _izdType);
			ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
			reportPrintTool1.ShowPreviewDialog();

			VshivkiReportHol report2 = new VshivkiReportHol();
			requestParameters(report2, _izdType);
			ReportPrintTool reportPrintTool2 = new ReportPrintTool(report2);
			reportPrintTool2.ShowPreviewDialog();
		}
		private void requestParameters(XtraReport report, int _izdType)
		{
			report.RequestParameters = false;

			SetParameter(report, "_nomZad", null);
			SetParameter(report, "_nom", null);
			SetParameter(report, "_proizvType", null);
			SetParameter(report, "_izdType", null);
			SetParameter(report, "_kod", null);

			if (!string.IsNullOrWhiteSpace(_kod))
			{
				SetParameter(report, "_kod", _kod);
				//SetParameter(report, "_proizvType", _proizvType);	//по коду сам решит, какой тип производства
				SetParameter(report, "_izdType", _izdType);
			}
			else if (!string.IsNullOrWhiteSpace(_nomZad) && !string.IsNullOrWhiteSpace(_nom))
			{
				SetParameter(report, "_nomZad", _nomZad);
				SetParameter(report, "_nom", int.Parse(_nom));
				SetParameter(report, "_proizvType", _proizvType);
				SetParameter(report, "_izdType", _izdType);
			}
			//MessageBox.Show($"_kod={_kod}\n_nom={_nom}\n_nomZad={_nomZad}\n_proizvType={_proizvType}\n_izdType={_izdType}");
		}

		private void SetParameter(XtraReport report, string name, object value)
		{
			var p = report.Parameters[name];
			if (p == null)
				return;

			p.Value = value;
			p.Visible = false;
		}

		private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
		{
			gridViewRzuRzv.PostEditor();
			gridViewRzuRzv.UpdateCurrentRow();

		}

		private void gridViewRzuRzv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (e.Column != IsSelected)
				return;

			var row = gridViewRzuRzv.GetRow(e.RowHandle) as ViewRzuRzv;
			if (row == null)
				return;

			row.IsSelected = Convert.ToBoolean(e.Value);

		}
	}
}
