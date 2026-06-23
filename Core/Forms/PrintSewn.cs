using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Class;
using SewingProduction.Core.Models;
using SewingProduction.Core.services;
using SewingProduction.Features.Articul.Service;
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
			//NomZad.Visible = false;
			//убираем столбцы номеров заданий и пачек (чтобы передался только код)
		}
		public PrintSewn(string nom, string nomZad, int proizvType)
		{
			_nom = nom;
			_nomZad = nomZad;
			_proizvType = proizvType;
			InitializeComponent();
			//NomZad.Visible = true;
		}
		private async void PrintSewn_Load(object sender, EventArgs e)
		{
			gridViewR.ShowLoadingPanel();
			try
			{
				var list = await _printSewnDataService.GetViewRzuRzv(
					_nomZad ?? null,
					_nom ?? null,
					_kod ?? null
				);

				if (IsOpenByKod())
				{
					list = LeaveFirstRowForEachModel(list);
				}

				customGridControlR.DataSource = list;
				ConfigureGridView();
			}
			finally
			{
				gridViewR.HideLoadingPanel();
			}
		}
		private bool IsOpenByKod()
		{
			return !string.IsNullOrWhiteSpace(_kod);
		}
		//группировка по модели
		private List<ViewRzuRzv> LeaveFirstRowForEachModel(List<ViewRzuRzv> source)
		{
			if (source == null || source.Count == 0)
				return new List<ViewRzuRzv>();

			return source
				.OrderBy(x => x.mod ?? string.Empty)
				.ThenBy(x => x.nom_zad ?? string.Empty)
				.ThenBy(x => x.nom_pach ?? 0)
				.GroupBy(x => x.mod ?? string.Empty)
				.Select(g => g.First())
				.ToList();
		}
		private void ConfigureGridView()
		{
			var colNomZad = gridViewR.Columns["nom_zad"];
			var colNomPach = gridViewR.Columns["nom_pach"];

			if (IsOpenByKod())
			{
				if (colNomZad != null)
					colNomZad.Visible = false; 
				if (colNomPach != null)
					colNomPach.Visible = false;
			}

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
		private async void createReport(int izdType = 0)
		{
			var selectedRows = GetSelectedRows();

			if (selectedRows.Count == 0)
			{
				MessageBox.Show("Не выбраны номера для печати.");
				return;
			}

			foreach (var row in selectedRows)
			{
				string nomZad = row.nom_zad?.ToString() ?? string.Empty;
				string nom = row.nom_pach?.ToString() ?? string.Empty;
				string kod = row.kod_izd?.ToString() ?? string.Empty;
				int proizvType = row.proizvType ?? 0;

				VshivkiReport report1 = new VshivkiReport();
				if (string.IsNullOrWhiteSpace(_kod))
					requestParameters(report1, proizvType, izdType, nomZad, nom, "0");
				else
					requestParameters(report1, proizvType, izdType, nomZad, "0", kod);
				//ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
				//reportPrintTool1.ShowPreviewDialog();

				await report1.ShowOrPrintBySettingsAsync(this);

				if (customCheckBoxNomPach.Checked)
				{
					VshivkiReportHol report2 = new VshivkiReportHol();
					if (string.IsNullOrWhiteSpace(kod))
						requestParameters(report2, proizvType, izdType, nomZad, nom);
					else
						requestParameters(report2, proizvType, izdType, kod: kod);
					//ReportPrintTool reportPrintTool2 = new ReportPrintTool(report2);
					//reportPrintTool2.ShowPreviewDialog();					
					await report2.ShowOrPrintBySettingsAsync(this);
				}
			}
		}
		private void requestParameters(XtraReport report, int proizvType, int izdType, string nomZad = null, string nom = null, string kod = null)
		{
			report.RequestParameters = false;

			SetParameter(report, "_nomZad", null);
			SetParameter(report, "_nom", null);
			SetParameter(report, "_proizvType", null);
			SetParameter(report, "_izdType", null);
			SetParameter(report, "_kod", null);

			if (!string.IsNullOrWhiteSpace(nomZad) && !string.IsNullOrWhiteSpace(nom))
			{
				SetParameter(report, "_nomZad", nomZad);
				SetParameter(report, "_nom", Convert.ToInt32(nom));
				SetParameter(report, "_proizvType", proizvType);
				SetParameter(report, "_izdType", izdType);
				SetParameter(report, "_kod", kod);
			}
		}
		private void SetParameter(XtraReport report, string name, object value)
		{
			var p = report.Parameters[name];
			if (p == null)
				return;

			p.Value = value;
			p.Visible = false;
		}

		private void gridViewR_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
		{
			if (e.Column != IsSelected || e.RowHandle < 0)
				return;

			bool currentValue = Convert.ToBoolean(gridViewR.GetRowCellValue(e.RowHandle, IsSelected));
			gridViewR.SetRowCellValue(e.RowHandle, IsSelected, !currentValue);

			gridViewR.PostEditor();
			gridViewR.UpdateCurrentRow();
		}
		private List<ViewRzuRzv> GetSelectedRows()
		{
			var result = new List<ViewRzuRzv>();

			for (int i = 0; i < gridViewR.RowCount; i++)
			{
				var row = gridViewR.GetRow(i) as ViewRzuRzv;
				if (row == null)
					continue;

				if (row.IsSelected)
					result.Add(row);
			}

			return result;
		}
	}
}
