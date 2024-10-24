using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction
{
    public partial class PrintReport : Form
    {
        public string query;
        public PrintReport()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ACEConnectionString))
            //{
            //    connection.Open();
            //    using (SqlDataAdapter adapter = new SqlDataAdapter())
            //    {
            //        //DataSet dt = new DataSet();
            //        //SqlCommand command = new SqlCommand(query, connection);
            //        //adapter.SelectCommand = command;
            //        //adapter.Fill(dt);
            //        //reportViewer1.LocalReport.DataSources.Clear();
            //        //ReportDataSource source = new ReportDataSource("dsPrintNakl", dt.Tables[0]);
            //        ////MessageBox.Show(Application.StartupPath);
            //        ////MessageBox.Show(Application.ExecutablePath);
            //        ////reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\..\PrintNakl.rdlc";
            //        ////reportViewer1.LocalReport.ReportPath = @"d:\SharpProj\sewingproduction\PrintNakl.rdlc";
            //        //reportViewer1.LocalReport.ReportPath = @"PrintNakl.rdlc";
            //        //reportViewer1.LocalReport.DataSources.Add(source);
            //        //reportViewer1.LocalReport.DataSources[0].Name = "dsPrintNakl";
            //        ////

            //        //reportViewer1.ProcessingMode = ProcessingMode.Local;
            //        reportViewer1.LocalReport.DataSources.Clear();
            //        reportViewer1.LocalReport.ReportPath = @"PrintNakl.rdlc";
            //        DataSet ds = new DataSet();
            //        ReportDataSource reportDataSource = new ReportDataSource();
            //        reportDataSource.Name = "dsPrintNakl";
            //        reportDataSource.Value = ds.Tables[0];
            //        reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            //        //reportViewer1.DataBind();


            //    }
            //}

            //this.reportViewer1.RefreshReport();

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ACEConnectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand(query, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];

                reportViewer1.Reset();
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"PrintNakl.rdlc";
                //ReportDataSource reportDataSource = new ReportDataSource();
                ReportDataSource reportDataSource = new ReportDataSource("dsPrintNakl", ds.Tables[0]);
                // Must match the DataSet in the RDLC
                //reportDataSource.Name = "dsPrintNakl";
                //reportDataSource.Value = ds.Tables[0];
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.RefreshReport();
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
