using System.ComponentModel;

namespace SewingProduction
{
    public partial class FurnitFITByKodFD : SewingProduction.Report.ConnectedXtraReport
    {
        public FurnitFITByKodFD()
        {
            InitializeComponent();
            UseCurrentConnection();
        }

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            //this.xrSubreport1.FillParameterBindings();
            //this.xrSubreport1.ApplyParameterBindings();
        }
    }
}
