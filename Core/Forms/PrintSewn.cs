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
		private string _proizvType;
		public PrintSewn(UserClass user) : base(user)
        {
            InitializeComponent();
		}
		public PrintSewn(string kod, string model, List<PrintSewnRazmKolRow> printSewnRazmKolRow)
		{
			_kod = kod;
			_model = model;
			_printSewnRazmKolRow = printSewnRazmKolRow;
			InitializeComponent();
		}
		public PrintSewn(string nom, string nomZad, string proizvType)
		{
			_nom = nom;
			_nomZad = nomZad;
			_proizvType = proizvType;
			InitializeComponent();
		}
		private void PrintSewn_Load(object sender, EventArgs e)
        {
        }

		#region широкие ЭЙС, Клевер (новый)
		private void customSimpleButtonACE_Click(object sender, EventArgs e)
		{
            VshivkiReport report1 = new VshivkiReport();
            requestParameters(report1);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();

            VshivkiReportHol report2 = new VshivkiReportHol();
			requestParameters(report2);
			ReportPrintTool reportPrintTool2 = new ReportPrintTool(report2);
			reportPrintTool2.ShowPreviewDialog();
		}
		private void requestParameters(XtraReport report)
		{
			report.RequestParameters = false;
			report.Parameters["_nomZad"].Value = _nomZad;
			report.Parameters["_nomZad"].Visible = false;
			report.Parameters["_nom"].Value = _nom;
			report.Parameters["_nom"].Visible = false;
			report.Parameters["_proizvType"].Value = _proizvType;
			report.Parameters["_proizvType"].Visible = false;
		}
		#endregion
		#region Комплекты  
		private void customSimpleButtonKompl_Click(object sender, EventArgs e)
		{
		}

        #endregion
        #region Набор одежды 

        private void customSimpleButtonNabor_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
