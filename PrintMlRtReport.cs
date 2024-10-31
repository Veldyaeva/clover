using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SewingProduction
{
    public partial class PrintMlRtReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PrintMlRtReport()
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

        private void xrBarCode1_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}
