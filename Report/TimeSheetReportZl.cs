using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SewingProduction.Report
{
    public partial class TimeSheetReportZl : ConnectedXtraReport
    {
        public TimeSheetReportZl()
        {
            InitializeComponent();
            UseCurrentConnection();
        }
    }
}
