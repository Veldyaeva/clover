using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SewingProduction.Report
{
    public partial class TimeSheetParsecOrionPrint : ConnectedXtraReport
    {
        public TimeSheetParsecOrionPrint()
        {
            InitializeComponent();
            UseCurrentConnection();
        }
    }
}
