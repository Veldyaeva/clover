using System.ComponentModel;

namespace SewingProduction.report
{
    public partial class FurnUpakZayavReport : SewingProduction.Report.ConnectedXtraReport
    {
        public FurnUpakZayavReport()
        {
            InitializeComponent();
            UseCurrentConnection();
        }
        //public int sumKolSkl;

        private void xrTableCell65_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void PrintFurnUpakZayavReport_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
