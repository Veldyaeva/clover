using System.ComponentModel;

namespace SewingProduction
{
    public partial class MlRtReport : SewingProduction.Report.ConnectedXtraReport
        //public partial class MlRtReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MlRtReport()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            UseCurrentConnection();
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void PrintMlRtReport_BeforePrint(object sender, CancelEventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
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
