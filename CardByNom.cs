using DevExpress.Office.Utils;
using DevExpress.XtraGrid.Views.Card.ViewInfo;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UserDesigner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace SewingProduction
{
    public partial class CardByNom : Form
    {
        public CardByNom()
        {
            InitializeComponent();
        }

        private string GetIzNakl()
        {
            string iz = "";
            try
            {
                object data = gridView1.GetRow(gridView1.FocusedRowHandle);
                if (data != null)
                {
                    iz = ((DataRowView)data).Row["iz"].ToString().Trim();
                }
            }
            catch
            {
                iz = "";
            }
            return iz;
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void CardByNom_Load(object sender, EventArgs e)
        {
            this.tbYearPach.Text = Convert.ToString(DateTime.Now.Year);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void textBox1_Leave(object sender, EventArgs e)
        {
            int NomPach = Convert.ToInt32(this.tbNomPach.Text);
            int YearPach = Convert.ToInt32(this.tbYearPach.Text);
            string _dateFormat = "dd/MM/yyyy";
            if (NomPach > 0 && YearPach > 0)
            {
                this.progressPanel1.Visible = true;
                string connectionString = Properties.Settings.Default.ACEConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Console.WriteLine("Подключение открыто");
                    SqlDataAdapter adapterNaklList = new SqlDataAdapter();
                    DataTable dtNaklList = new DataTable();
                    //string query = $"select * from raskr_zeh_up where pach_kod like {YearPach}{NomPach} + '%' ";
                    //query += $" order by n_pach";
                    string queryNaklList = $"select * from NaklView where nom = (select nom from raskr_zeh_up where pach_kod like '{YearPach}{NomPach}%') ";
                    queryNaklList += $" order by iz";
                    SqlCommand commandNaklList = new SqlCommand(queryNaklList, connection);
                    adapterNaklList.SelectCommand = commandNaklList;
                    adapterNaklList.Fill(dtNaklList);
                    bsNaklList.DataSource = dtNaklList;
                    bsNaklList.Sort = "iz asc";
                    //MessageBox.Show("Запрос выполнен", "Запрос списка накладных", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    SqlDataAdapter adapterRasInfo = new SqlDataAdapter();
                    DataTable dtRasInfo = new DataTable();
                    string queryRasInfo = $"select * from RasInfoView where rzuNom = (select nom from raskr_zeh_up where pach_kod like '{YearPach}{NomPach}%') ";
                    //queryRasInfo += $" order by iz";
                    SqlCommand commandRasInfo = new SqlCommand(queryRasInfo, connection);
                    adapterRasInfo.SelectCommand = commandRasInfo;
                    adapterRasInfo.Fill(dtRasInfo);
                    bsRasInfo.DataSource = dtRasInfo;
                    //bsRasInfo.Sort = "iz asc";
                    //textBox1.DataBindings.Add("Text", model, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
                    //MessageBox.Show("1");
                    this.pbEskiz.DataBindings.Clear();
                    this.pbEskiz.DataBindings.Add("ImageLocation", dtRasInfo, "pictPath");
                    this.tbRzuNom.DataBindings.Clear();
                    this.tbRzuNom.DataBindings.Add("Text", dtRasInfo, "rzuNom");
                    this.tbRzuPach.DataBindings.Clear();
                    this.tbRzuPach.DataBindings.Add("Text", dtRasInfo, "rzuPach");
                    this.tbRzuKol.DataBindings.Clear();
                    this.tbRzuKol.DataBindings.Add("Text", dtRasInfo, "rzuKol");
                    this.tbPsaPrn.DataBindings.Clear();
                    this.tbPsaPrn.DataBindings.Add("Text", dtRasInfo, "psaPrn");
                    this.tbRzuArticul.DataBindings.Clear();
                    this.tbRzuArticul.DataBindings.Add("Text", dtRasInfo, "rzuArticul");
                    this.tbRzuMod.DataBindings.Clear();
                    this.tbRzuMod.DataBindings.Add("Text", dtRasInfo, "rzuMod");
                    this.tbRzuDostZeh.DataBindings.Clear();
                    this.tbRzuDostZeh.DataBindings.Add("Text", dtRasInfo, "rzuDostZeh");
                    this.tbPsaNameSbit.DataBindings.Clear();
                    this.tbPsaNameSbit.DataBindings.Add("Text", dtRasInfo, "psaNameSbit");
                    this.tbPsaNameSbit1.DataBindings.Clear();
                    this.tbPsaNameSbit1.DataBindings.Add("Text", dtRasInfo, "psaNameSbit");
                    this.tbPsaNN.DataBindings.Clear();
                    this.tbPsaNN.DataBindings.Add("Text", dtRasInfo, "psaNN");
                    this.tbPsaNomZad.DataBindings.Clear();
                    this.tbPsaNomZad.DataBindings.Add("Text", dtRasInfo, "psaNomZad");
                    this.tbPsaMenName.DataBindings.Clear();
                    this.tbPsaMenName.DataBindings.Add("Text", dtRasInfo, "psaMenName");
                    this.tbPsaTbID.DataBindings.Clear();
                    this.tbPsaTbID.DataBindings.Add("Text", dtRasInfo, "psaTbID");
                    this.tbPsaYear.DataBindings.Clear();
                    this.tbPsaYear.DataBindings.Add("Text", dtRasInfo, "psaYear");
                    this.psaSezName.DataBindings.Clear();
                    this.psaSezName.DataBindings.Add("Text", dtRasInfo, "psaSezName");
                    this.tbArtGrup.DataBindings.Clear();
                    this.tbArtGrup.DataBindings.Add("Text", dtRasInfo, "artGrup");
                    this.tbArtSost1.DataBindings.Clear();
                    this.tbArtSost1.DataBindings.Add("Text", dtRasInfo, "artSost1");
                    this.tbArtSost2.DataBindings.Clear();
                    this.tbArtSost2.DataBindings.Add("Text", dtRasInfo, "artSost2");
                    this.tbArtSost3.DataBindings.Clear();
                    this.tbArtSost3.DataBindings.Add("Text", dtRasInfo, "artSost3");
                    this.tbPsaKodZv1.DataBindings.Clear();
                    this.tbPsaKodZv1.DataBindings.Add("Text", dtRasInfo, "psaKodZv1");
                    this.tbPsaKodZv2.DataBindings.Clear();
                    this.tbPsaKodZv2.DataBindings.Add("Text", dtRasInfo, "psaKodZv2");
                    this.mtbPsaDataZap.DataBindings.Clear();
                    this.mtbPsaDataZap.DataBindings.Add("Text", dtRasInfo, "psaDataZap");
                    this.mtbPsaDataCdPlan.DataBindings.Clear();
                    this.mtbPsaDataCdPlan.DataBindings.Add("Text", dtRasInfo, "psaDataCdPlan");
                    this.mtbRzuDataCdUt.DataBindings.Clear();
                    this.mtbRzuDataCdUt.DataBindings.Add("Text", dtRasInfo, "rzuDataCdUt");
                    this.mtbRzuDataZeh.DataBindings.Clear();
                    this.mtbRzuDataZeh.DataBindings.Add("Text", dtRasInfo, "rzuDataZeh");
                    this.mtbRzuDataRab.DataBindings.Clear();
                    this.mtbRzuDataRab.DataBindings.Add("Text", dtRasInfo, "rzuDataRab");
                    this.mtbRzuDataUp.DataBindings.Clear();
                    this.mtbRzuDataUp.DataBindings.Add("Text", dtRasInfo, "rzuDataUp");
                    this.mtbRzuDataCd.DataBindings.Clear();
                    this.mtbRzuDataCd.DataBindings.Add("Text", dtRasInfo, "rzuDataCd");
                    this.mtbRzuDataRasp.DataBindings.Clear();
                    this.mtbRzuDataRasp.DataBindings.Add("Text", dtRasInfo, "RzuDataRasp");
                    this.mtbRzuDataPrP.DataBindings.Clear();
                    this.mtbRzuDataPrP.DataBindings.Add("Text", dtRasInfo, "RzuDataPrP");
                    this.mtbRzuDataPrR.DataBindings.Clear();
                    this.mtbRzuDataPrR.DataBindings.Add("Text", dtRasInfo, "RzuDataPrR");
                    this.mtbRzuDataPrPe.DataBindings.Clear();
                    this.mtbRzuDataPrPe.DataBindings.Add("Text", dtRasInfo, "RzuDataPrPe");
                    this.mtbRzuDataPrKm.DataBindings.Clear();
                    this.mtbRzuDataPrKm.DataBindings.Add("Text", dtRasInfo, "RzuDataPrKm");
                    this.mtbRzuDataPrCd.DataBindings.Clear();
                    this.mtbRzuDataPrCd.DataBindings.Add("Text", dtRasInfo, "RzuDataPrCd");
                    this.mtbRzuDataRasv.DataBindings.Clear();
                    this.mtbRzuDataRasv.DataBindings.Add("Text", dtRasInfo, "RzuDataRasv");
                    this.mtbRzuDataVP.DataBindings.Clear();
                    this.mtbRzuDataVP.DataBindings.Add("Text", dtRasInfo, "RzuDataVP");
                    this.mtbRzuDataVR.DataBindings.Clear();
                    this.mtbRzuDataVR.DataBindings.Add("Text", dtRasInfo, "RzuDataVR");
                    this.mtbRzuDataVChi.DataBindings.Clear();
                    this.mtbRzuDataVChi.DataBindings.Add("Text", dtRasInfo, "RzuDataVChi");
                    this.mtbRzuDataVCd.DataBindings.Clear();
                    this.mtbRzuDataVCd.DataBindings.Add("Text", dtRasInfo, "RzuDataVCd");
                    this.mtbRzuDataStP.DataBindings.Clear();
                    this.mtbRzuDataStP.DataBindings.Add("Text", dtRasInfo, "RzuDataStP");
                    this.mtbRzuDataStR.DataBindings.Clear();
                    this.mtbRzuDataStR.DataBindings.Add("Text", dtRasInfo, "RzuDataStR");
                    this.mtbRzuDataStCd.DataBindings.Clear();
                    this.mtbRzuDataStCd.DataBindings.Add("Text", dtRasInfo, "RzuDataStCd");
                    this.mtbRzuVidStir.DataBindings.Clear();
                    this.mtbRzuVidStir.DataBindings.Add("Text", dtRasInfo, "RzuVidStir");

                    //MessageBox.Show("2");

                    SqlDataAdapter adapterOtdelkaList = new SqlDataAdapter();
                    DataTable dtOtdelkaList = new DataTable();
                    //string query = $"select * from raskr_zeh_up where pach_kod like {YearPach}{NomPach} + '%' ";
                    //query += $" order by n_pach";
                    string queryOtdelkaList = $"SELECT vpso.psa_field_name, vpso.kol_sl_zv, vpso.frt_naimen, DetIzdName, VidIzdName ";
                    queryOtdelkaList += $" FROM View_plan_sezon_otdelka vpso ";
                    queryOtdelkaList += $" WHERE vpso.nn = '{this.tbPsaNN.Text}' ";
                    SqlCommand commandOtdelkaList = new SqlCommand(queryOtdelkaList, connection);
                    adapterOtdelkaList.SelectCommand = commandOtdelkaList;
                    adapterOtdelkaList.Fill(dtOtdelkaList);
                    bsOtdelkaList.DataSource = dtOtdelkaList;
                    //bsOtdelkaList.Sort = "iz asc";
                }
                this.progressPanel1.Visible = false;
            }
            
        }

        private void tabPane1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            textBox1_Leave(sender, e);
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbNomPach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1_Leave(sender, e);
            }
        }

        private void btnNaklPrint_Click(object sender, EventArgs e)
        {
            string iz = GetIzNakl();
            PrintNaklReport report1 = new PrintNaklReport();
            report1.RequestParameters = false;
            report1.Parameters["_naklIz"].Value = iz;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();

            //DataRowView SelectedRow = (DataRowView)bsNaklList.Current;
            //DataRow row = SelectedRow.Row;
            //string iz = row["iz"].ToString();
            //PrintReport report = new PrintReport();
            //report.query = $"select * from NaklView where iz = '{iz}' ";
            //report.Show();
            ////string iz = GetIzNakl();

            ////string connectionString = Properties.Settings.Default.ACEConnectionString;
            ////using (SqlConnection connection = new SqlConnection(connectionString))
            ////{
            ////    connection.Open();
            ////    SqlDataAdapter adapterNaklListReport = new SqlDataAdapter();
            ////    DataTable dtNaklListReport = new DataTable();
            ////    //string query = $"select * from raskr_zeh_up where pach_kod like {YearPach}{NomPach} + '%' ";
            ////    //query += $" order by n_pach";
            ////    string queryNaklListReport = $"select * from NaklView where iz = '{iz}' ";
            ////    //queryNaklListReport += $" order by iz";
            ////    SqlCommand commandNaklListReport = new SqlCommand(queryNaklListReport, connection);
            ////    adapterNaklListReport.SelectCommand = commandNaklListReport;
            ////    adapterNaklListReport.Fill(dtNaklListReport);
            ////    //bsNaklList.DataSource = dtNaklListReport;
            ////    //bsNaklList.Sort = "kod asc";

            ////    //dtNaklListReport.WriteXml("C:\\1\\dtNaklListReport.xml", System.Data.XmlWriteMode.WriteSchema);

            ////    //Создаем отчет
            ////    PrintNakl report = new PrintNakl();
            ////    //Открываем шаблон отчета в формате *.repx
            ////    //report.LoadLayout(Application.StartupPath + ".. \\ XtraReport1.repx"); // Report to the root directory
            ////    //Передаем класс с данными
            ////    report.DataSource = dtNaklListReport;

            ////    //Открываем отчет для предпросмотра и дальнейшей работы с ним (печать/экспорт и т.д.)
            ////    ////ReportPrintTool tool = new ReportPrintTool(report);
            ////    ////tool.ShowPreview();
            ////    report.ShowPreview();
            ////}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //MessageBox.Show(GetIzNakl());
            string iz = GetIzNakl();
            //Создаем отчет
            //XtraReport2 report = new XtraReport2();
            //report.RequestParameters = false;
            //report.Parameters["naklIz"].Value = iz;

            //ReportPrintTool reportPrintTool = new ReportPrintTool(report);
            //reportPrintTool.ShowPreviewDialog();
            
            PrintNaklReport report1 = new PrintNaklReport();
            report1.RequestParameters = false;
            report1.Parameters["_naklIz"].Value = iz;

            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();

            //PrintNaklReport report = new PrintNaklReport
            //{
            //   FilterString = "[nakl.iz] = ?NaklIz",
            //    RequestParameters = false
            //};

            //report.Parameters["NaklIz"].Value = iz;
            //ReportPrintTool reportPrintTool = new ReportPrintTool(report);
            //reportPrintTool.ShowPreviewDialog();
            //string iz = GetIzNakl();
            ////Создаем отчет
            //rptPrintNakl report = new rptPrintNakl
            //{
            //    FilterString = "[nakl.iz] = ?NaklIz",
            //    RequestParameters = false
            //};
            //report.Parameters["NaklIz"].Value = iz;
            //ReportPrintTool reportPrintTool = new ReportPrintTool(report);
            //reportPrintTool.ShowPreviewDialog();

            //string connectionString = Properties.Settings.Default.ACEConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlDataAdapter adapterNaklListReport = new SqlDataAdapter();
            //    DataTable dtNaklListReport = new DataTable();
            //    //string query = $"select * from raskr_zeh_up where pach_kod like {YearPach}{NomPach} + '%' ";
            //    //query += $" order by n_pach";
            //    string queryNaklListReport = $"select * from NaklView where iz = '{iz}' ";
            //    //queryNaklListReport += $" order by iz";
            //    SqlCommand commandNaklListReport = new SqlCommand(queryNaklListReport, connection);
            //    adapterNaklListReport.SelectCommand = commandNaklListReport;
            //    adapterNaklListReport.Fill(dtNaklListReport);
            //    //bsNaklList.DataSource = dtNaklListReport;
            //    //bsNaklList.Sort = "kod asc";

            //    //dtNaklListReport.WriteXml("C:\\1\\dtNaklListReport.xml", System.Data.XmlWriteMode.WriteSchema);

            //    //Создаем отчет
            //    rptPrintNakl report = new rptPrintNakl
            //    {
            //        FilterString = "[nakl.iz] = ?NaklIz",
            //        RequestParameters = false
            //    };
            //    report.Parameters["NaklIz"].Value = iz;
            //    ReportPrintTool reportPrintTool = new ReportPrintTool(report);
            //    reportPrintTool.ShowPreviewDialog();

            //    //Открываем шаблон отчета в формате *.repx
            //    //report.LoadLayout(Application.StartupPath + ".. \\ XtraReport1.repx"); // Report to the root directory
            //    //Передаем класс с данными
            //    //report.DataSource = "dtNaklListReport";

            //    //Открываем отчет для предпросмотра и дальнейшей работы с ним (печать/экспорт и т.д.)
            //    ////ReportPrintTool tool = new ReportPrintTool(report);
            //    ////tool.ShowPreview();
            //    ////report.ShowPreview();
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FioListReport report = new FioListReport();
            report.RequestParameters = false;
            report.Parameters["_tab"].Value = 3;

            ReportPrintTool reportPrintTool = new ReportPrintTool(report);
            reportPrintTool.ShowPreviewDialog();

        }

        private void progressPanel1_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    XtraReport1 xtpt = new XtraReport1();
        //    xtpt.LoadLayout(Application.StartupPath + "..\\XtraReport1.repx");
        //    ReportDesignTool tool = new ReportDesignTool(xtpt);
        //    tool.ShowDesignerDialog();
        //}
    }
}
