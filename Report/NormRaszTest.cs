using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SewingProduction.Report
{
    public partial class NormRaszTest : XtraReport
    {
        public NormRaszTest()
        {
            InitializeComponent();
        }
        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void PrintMlRtReport_BeforePrint(object sender, CancelEventArgs e)
        {
            this.xrSubreport1.CanShrink = true;
        }

        private void xrTableCell38_BeforePrint(object sender, CancelEventArgs e)
        {

        }


    }
}
