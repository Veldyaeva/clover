using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SewingProduction.Report
{
    public partial class VshivkiReport : ConnectedXtraReport
    {
        public VshivkiReport()
        {
            InitializeComponent();
            UseCurrentConnection();
        }
    }
}
