using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Class;

namespace SewingProduction.Report
{
	public partial class VshivkiReportHol : CustomConnectedXtraReport
	{
		public VshivkiReportHol()
		{
			InitializeComponent();
			UseCurrentConnection();
		}
	}
}
