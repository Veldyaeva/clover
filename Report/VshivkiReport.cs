using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Class;

namespace SewingProduction.Report
{
    public partial class VshivkiReport : CustomConnectedXtraReport
	{
        public VshivkiReport()
        {
            InitializeComponent();
            UseCurrentConnection();
        }
    }
}
