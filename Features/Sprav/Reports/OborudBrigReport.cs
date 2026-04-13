using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraReports.UI;

namespace SewingProduction.Features.Sprav.Reports
{
    public partial class OborudBrigReport : SewingProduction.Report.ConnectedXtraReport
    {
        public OborudBrigReport()
        {
            InitializeComponent();
            UseCurrentConnection();
        }
    }
}
