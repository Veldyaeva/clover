using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SewingProduction
{
    public partial class FurnitFITByKodFD : DevExpress.XtraReports.UI.XtraReport
    {
        public FurnitFITByKodFD()
        {
            InitializeComponent();
        }

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            //this.xrSubreport1.FillParameterBindings();
            //this.xrSubreport1.ApplyParameterBindings();
        }
    }
}
