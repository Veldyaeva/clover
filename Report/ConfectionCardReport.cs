using System.ComponentModel;

using SewingProduction.Report;

namespace SewingProduction.report
{
    public partial class ConfectionCardReport : ConnectedXtraReport
    {
        public ConfectionCardReport()
        {
            InitializeComponent();
            UseCurrentConnection();
        }

        private void PrintKKReport_BeforePrint(object sender, CancelEventArgs e)
        {
            //(sender as PrintKKReport).Parameters["_nomZad"].Value = this.GetCurrentColumnValue("nom_zad");
            //this.Parameters["_specRez"].Value = this.GetCurrentColumnValue("faSpecRez");
            //this.Parameters["_vidF"].Value = this.GetCurrentColumnValue("vidF");
            //sqlDataSource1.Fill();
            //sqlDataSource1.Fill("FurnitArtView");
            //MessageBox.Show(this.xrLabel8.Text);
            //this.Parameters["_nomZad"].Value = this.GetCurrentColumnValue("nom_zad").ToString();
            // MessageBox.Show(this.GetCurrentColumnValue("nom_zad").ToString());
            // MessageBox.Show(this.Parameters["_nomZad"].Value.ToString());
        }
    }
}
