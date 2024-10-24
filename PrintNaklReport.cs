using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SewingProduction
{
    public partial class PrintNaklReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PrintNaklReport()
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
