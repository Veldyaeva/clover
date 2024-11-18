using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace SewingProduction.report
{
    public partial class PrintKKReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PrintKKReport()
        {
            InitializeComponent();
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
