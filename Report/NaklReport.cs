using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SewingProduction
{
    public partial class NaklReport : DevExpress.XtraReports.UI.XtraReport
    {
        public NaklReport()
        {
            InitializeComponent();
        }

        private void PrintNaklReport_BeforePrint(object sender, CancelEventArgs e)
        {
            this.xrSubreport1.FillParameterBindings();
            this.xrSubreport1.ApplyParameterBindings();
        }

        private void PrintNaklReport_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
        {
            //this.xrSubreport1.FillParameterBindings();
            //this.xrSubreport1.ApplyParameterBindings();
        }

        private void xrSubreport1_ParentChanged(object sender, ChangeEventArgs e)
        {


        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            this.xrSubreport1.FillParameterBindings();
            this.xrSubreport1.ApplyParameterBindings();
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrSubreport1_BeforePrint_1(object sender, CancelEventArgs e)
        {
            this.xrSubreport1.FillParameterBindings();
            this.xrSubreport1.ApplyParameterBindings();
        }
    }
}
