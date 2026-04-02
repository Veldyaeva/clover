using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace SewingProduction.Report
{
    public partial class TimeSheetReport : ConnectedXtraReport
    {
        public TimeSheetReport()
        {
            InitializeComponent();
            UseCurrentConnection();
        }
        
    }
}
