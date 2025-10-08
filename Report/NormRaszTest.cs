using System.ComponentModel;
using DevExpress.XtraReports.UI;

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
            //this.xrSubreport1.CanShrink = true;
        }

        private void xrTableCell38_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell3_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}
