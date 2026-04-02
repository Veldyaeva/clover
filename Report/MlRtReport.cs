using System.ComponentModel;

namespace SewingProduction
{
    public partial class MlRtReport : SewingProduction.Report.ConnectedXtraReport
    {
        public MlRtReport()
        {
            InitializeComponent();
            UseCurrentConnection();
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
