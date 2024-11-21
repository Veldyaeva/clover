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
using SewingProduction.report;
using DevExpress.XtraGauges.Core.Styles;
using DevExpress.XtraPrinting;

namespace SewingProduction
{
    public partial class CardByNom : Form
    {
        public int fspecrez, uspecrez;
        public string fkodfd, ukodfd;
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
            tbNomPach.Select();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void textBox1_Leave(object sender, EventArgs e)
        {
            
            
        }

        private void tabPane1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
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
                //textBox1_Leave(sender, e);
                //int NomPach = Convert.ToInt32(tbNomPach.Text);
                //int YearPach = Convert.ToInt32(tbYearPach.Text);
                // label59.text +  в цех / на упак. / передачи в ш.ц. - доработать с учетом вида производства


                string PachKod = string.Concat(tbYearPach.Text, tbNomPach.Text.PadLeft(6));
                //string _dateFormat = "dd/MM/yyyy";
                //if (NomPach > 0 && YearPach > 0)
                if (PachKod.Length > 0)
                {
                    
                    string connectionString = Properties.Settings.Default.ACEConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        //Console.WriteLine("Подключение открыто");
                        SqlDataAdapter adapterNaklList = new SqlDataAdapter();
                        DataTable dtNaklList = new DataTable();
                        //string query = $"select * from raskr_zeh_up where pach_kod like {YearPach}{NomPach} + '%' ";
                        //query += $" order by n_pach";
                        //string queryNaklList = $"select * from NaklView where nom = (select nom from raskr_zeh_up where pach_kod like '{YearPach}{NomPach}%') ";
                        string queryNaklList = $"select * from NaklView where nom = (select nom from raskr_zeh_up where pach_kod like '{PachKod}%') ";
                        queryNaklList += $" order by iz";
                        SqlCommand commandNaklList = new SqlCommand(queryNaklList, connection);
                        adapterNaklList.SelectCommand = commandNaklList;
                        adapterNaklList.Fill(dtNaklList);
                        bsNaklList.DataSource = dtNaklList;
                        bsNaklList.Sort = "iz asc";
                        //MessageBox.Show("Запрос выполнен", "Запрос списка накладных", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        SqlDataAdapter adapterRasInfo = new SqlDataAdapter();
                        DataTable dtRasInfo = new DataTable();
                        //string queryRasInfo = $"select * from RasInfoView where rzuNom = (select nom from raskr_zeh_up where pach_kod like '{YearPach}{NomPach}%') ";
                        string queryRasInfo = $"select * from RasInfoView where rzuNom = (select nom from raskr_zeh_up where pach_kod like '{PachKod}%') ";
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
                        //this.tbPsaNameSbit1.DataBindings.Clear();
                        //this.tbPsaNameSbit1.DataBindings.Add("Text", dtRasInfo, "psaNameSbit");
                        this.tbPsaNN.DataBindings.Clear();
                        this.tbPsaNN.DataBindings.Add("Text", dtRasInfo, "psaNN");
                        this.tbPsaPsaID.DataBindings.Clear();
                        this.tbPsaPsaID.DataBindings.Add("Text", dtRasInfo, "psaPsaID");
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
                        this.tbSostPoln.DataBindings.Clear();
                        this.tbSostPoln.DataBindings.Add("Text", dtRasInfo, "sostPoln");
                        //this.tbSostOtdelka.DataBindings.Clear();
                        //this.tbSostOtdelka.DataBindings.Add("Text", dtRasInfo, "sostOtdelka");
                        //this.tbSostPodklad.DataBindings.Clear();
                        //this.tbSostPodklad.DataBindings.Add("Text", dtRasInfo, "sostPodklad");
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
                        this.mtbRzuData1С.DataBindings.Clear();
                        this.mtbRzuData1С.DataBindings.Add("Text", dtRasInfo, "rzuData1C");
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
                        this.cbPszPrintPlan.DataBindings.Clear();
                        this.cbPszPrintPlan.DataBindings.Add("Checked", dtRasInfo, "PszPrintPlan");
                        this.cbRzuPrintFact.DataBindings.Clear();
                        this.cbRzuPrintFact.DataBindings.Add("Checked", dtRasInfo, "RzuPrintFact");
                        this.cbPszVishPlan.DataBindings.Clear();
                        this.cbPszVishPlan.DataBindings.Add("Checked", dtRasInfo, "PszVishPlan");
                        this.cbRzuVishFact.DataBindings.Clear();
                        this.cbRzuVishFact.DataBindings.Add("Checked", dtRasInfo, "RzuVishFact");
                        this.cbPszStirPlan.DataBindings.Clear();
                        this.cbPszStirPlan.DataBindings.Add("Checked", dtRasInfo, "PszStirPlan");
                        this.cbRzuStirFact.DataBindings.Clear();
                        this.cbRzuStirFact.DataBindings.Add("Checked", dtRasInfo, "RzuStirFact");

                        string NomZad = this.tbPsaNomZad.Text;
                        SqlDataAdapter adapterIsChip = new SqlDataAdapter();
                        DataTable dtIsChip = new DataTable();
                        string queryIsChip = $"SELECT dbo.checkChipNakl('', '{NomZad}') AS isChip ";
                        SqlCommand commandIsChip = new SqlCommand(queryIsChip, connection);
                        adapterIsChip.SelectCommand = commandIsChip;
                        adapterIsChip.Fill(dtIsChip);
                        bsIsChip.DataSource = dtIsChip;
                        this.cbIsChip.DataBindings.Clear();
                        this.cbIsChip.DataBindings.Add("Checked", dtIsChip, "isChip");

                        //DataRow[] currentRows = dtIsChip.Select(null, null, DataViewRowState.CurrentRows);
                        //if (currentRows.Length < 1)
                        //    Console.WriteLine("No Current Rows in IsChip Found");
                        //else
                        //{
                        //    //foreach (DataColumn column in dtPartNaklList.Columns)
                        //    //    Console.Write("\t{0}", column.ColumnName);
                        //    //Console.WriteLine("\tRowState");
                        //    foreach (DataRow row in currentRows)
                        //    {
                        //        foreach (DataColumn column in dtIsChip.Columns)
                        //            MessageBox.Show(Convert.ToString(row[column]));
                        //        //Console.WriteLine("\t" + row.RowState);
                        //    }
                        //}

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

                        if (xtraTabControl1.SelectedTabPageIndex == 1)
                        {
                            button11_Click(sender, e);
                        }
                        
                    }
                }
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
            //FioListReport report = new FioListReport();
            //report.RequestParameters = false;
            //report.Parameters["_tab"].Value = 3;

            //ReportPrintTool reportPrintTool = new ReportPrintTool(report);
            //reportPrintTool.ShowPreviewDialog();

        }

        private void progressPanel1_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void btnNaklPart_Click(object sender, EventArgs e)
        {
            if (btnNaklPart.Text == "Показать информацию по делению накладной")
            {
                btnNaklPart.Text = "Скрыть информацию по делению накладной";
                string iz = GetIzNakl();
                
                string connectionString = Properties.Settings.Default.ACEConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapterPartNaklList = new SqlDataAdapter();
                    DataTable dtPartNaklList = new DataTable();
                    string queryPartNaklList = $"SELECT * FROM [ACE].[dbo].[View_History_razdel_nakl] where iz_b = '{iz}' ";
                    queryPartNaklList += $" order by id";
                    SqlCommand commandPartNaklList = new SqlCommand(queryPartNaklList, connection);
                    adapterPartNaklList.SelectCommand = commandPartNaklList;
                    adapterPartNaklList.Fill(dtPartNaklList);
                    bsPartNaklList.DataSource = dtPartNaklList;
                    bsPartNaklList.Sort = "id asc";
                    this.gcPartNaklList.Location = this.gcNaklList.Location;
                    this.gcPartNaklList.Size = this.gcNaklList.Size;
                    this.gcPartNaklList.BringToFront();
                    this.gcPartNaklList.Visible = true;

                    //DataRow[] currentRows = dtPartNaklList.Select(null, null, DataViewRowState.CurrentRows);
                    //if (currentRows.Length < 1)
                    //    Console.WriteLine("No Current Rows Found");
                    //else
                    //{
                    //    foreach (DataColumn column in dtPartNaklList.Columns)
                    //        Console.Write("\t{0}", column.ColumnName);
                    //    Console.WriteLine("\tRowState");
                    //    foreach (DataRow row in currentRows)
                    //    {
                    //        foreach (DataColumn column in dtPartNaklList.Columns)
                    //            Console.Write("\t{0}", row[column]);
                    //        Console.WriteLine("\t" + row.RowState);
                    //    }
                    //}
                }
            }
            else
            {
                btnNaklPart.Text = "Показать информацию по делению накладной";
                this.gcNaklList.BringToFront();
                this.gcPartNaklList.Visible = false;
            }
            
        }

        private void gcPartNaklList_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int RzuNom = Convert.ToInt32(this.tbRzuNom.Text);
            int IsChip = Convert.ToInt32(this.cbIsChip.Checked);
            PrintMlRtReport report1 = new PrintMlRtReport();
            report1.RequestParameters = false;
            report1.Parameters["_rzuNom"].Value = RzuNom;
            report1.Parameters["_isChip"].Value = IsChip;
            report1.Parameters["_isUpak"].Value = 0;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int RzuNom = Convert.ToInt32(this.tbRzuNom.Text);
            int IsChip = Convert.ToInt32(this.cbIsChip.Checked);
            PrintMlRtReport report1 = new PrintMlRtReport();
            report1.RequestParameters = false;
            report1.Parameters["_rzuNom"].Value = RzuNom;
            report1.Parameters["_isChip"].Value = IsChip;
            report1.Parameters["_isUpak"].Value = 1;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int RzuNom = Convert.ToInt32(this.tbRzuNom.Text);
            string RzuPachList = this.tbRzuPach.Text;
            PrintReestrListReport report1 = new PrintReestrListReport();
            report1.RequestParameters = false;
            report1.Parameters["_rzuNom"].Value = RzuNom;
            report1.Parameters["_rzuPachList"].Value = RzuPachList;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void mtbRzuDataUp_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtbRzuDataRab_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtbRzuDataZeh_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtbRzuDataCdUt_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtbPsaDataCdPlan_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtbPsaDataZap_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void mtbRzuDataCd_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void furnitZayavView1_Load(object sender, EventArgs e)
        {
            
        }

        private void furnitZayavViewFurnit_TextChanged(object sender, EventArgs e)
        {

        }

        private void furnitZayavViewUpak_TextChanged(object sender, EventArgs e)
        {

        }

        private void xtraTabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void xtraTabControl1_Selecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            button11_Click(sender, e);
            //furnitZayavViewFurnit.Text = "2024   37605";
            ////furnitZayavViewFurnit.Refresh();
            //furnitZayavViewUpak.Text = "2024   40367";
            
        }

        private void btnZayavFurnPrint_Click(object sender, EventArgs e)
        {
            PrintFurnUpakZayavReport report1 = new PrintFurnUpakZayavReport();
            report1.RequestParameters = false;
            report1.Parameters["_kodF"].Value = fkodfd.Substring(0, 12);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PrintFurnUpakZayavReport report1 = new PrintFurnUpakZayavReport();
            report1.RequestParameters = false;
            report1.Parameters["_kodF"].Value = ukodfd.Substring(0, 12);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void btnFullKKPrint_Click(object sender, EventArgs e)
        {
            PrintFullKKReport report1 = new PrintFullKKReport();
            report1.RequestParameters = false;
            report1.Parameters["_psaid"].Value = Convert.ToInt32(tbPsaPsaID.Text);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void btnUpakKKPrint_Click(object sender, EventArgs e)
        {
            PrintKKReport report1 = new PrintKKReport();
            report1.RequestParameters = false;
            report1.Parameters["_kodFD"].Value = fkodfd;
            report1.Parameters["_nomZad"].Value = tbPsaNomZad.Text;
            report1.Parameters["_vidF"].Value = 2;
            report1.Parameters["_specRez"].Value = Convert.ToBoolean(uspecrez);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string PachKod = string.Concat(tbYearPach.Text, tbNomPach.Text.PadLeft(6));
            string connectionString = Properties.Settings.Default.ACEConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Console.WriteLine("Подключение открыто");
                SqlDataAdapter adapterFurnZayavInfo = new SqlDataAdapter();
                DataTable dtFurnZayavInfo = new DataTable();
                string queryFurnZayavInfo = $"exec furnitZayavCheck '{PachKod}', 1 ";  // 1 - ШП. на будущее нужно будут доработать с учетом выбора вида производства
                SqlCommand commandFurnZayavInfo = new SqlCommand(queryFurnZayavInfo, connection);
                adapterFurnZayavInfo.SelectCommand = commandFurnZayavInfo;
                adapterFurnZayavInfo.Fill(dtFurnZayavInfo);
                bsFurnZayavInfo.DataSource = dtFurnZayavInfo;
                //MessageBox.Show("Запрос выполнен", "Запрос списка накладных", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                this.tbFurnKKStat.DataBindings.Clear();
                this.tbFurnKKStat.DataBindings.Add("text", bsFurnZayavInfo, "FurnKKStat");
                if (dtFurnZayavInfo.Rows[0]["FurnKKStat"].ToString() != 'V'.ToString() && Convert.ToInt32(dtFurnZayavInfo.Rows[0]["is_furnit"]) == 1)
                {
                    tbFurnKKStat.ForeColor = Color.Red; 
                    btnFurnKKPrint.ForeColor = Color.Red;
                }
                else
                {
                    tbFurnKKStat.ForeColor = Color.Black;
                    btnFurnKKPrint.ForeColor = Color.Black;
                }
                this.tbUpakKKStat.DataBindings.Clear();
                this.tbUpakKKStat.DataBindings.Add("text", bsFurnZayavInfo, "UpakKKStat");
                if (dtFurnZayavInfo.Rows[0]["UpakKKStat"].ToString() != 'V'.ToString() && Convert.ToInt32(dtFurnZayavInfo.Rows[0]["is_upak"]) == 1)
                {
                    tbUpakKKStat.ForeColor = Color.Red;
                    btnUpakKKPrint.ForeColor = Color.Red;
                }
                else
                {
                    tbUpakKKStat.ForeColor = Color.Black;
                    btnUpakKKPrint.ForeColor = Color.Black;
                }
                this.tbFurnZayav.DataBindings.Clear();
                this.tbFurnZayav.DataBindings.Add("text", bsFurnZayavInfo, "FurnZayav");
                this.tbData_f_o.DataBindings.Clear();
                this.tbData_f_o.DataBindings.Add("text", bsFurnZayavInfo, "Data_f_o");
                this.tbFZSozdStat.DataBindings.Clear();
                this.tbFZSozdStat.DataBindings.Add("text", bsFurnZayavInfo, "FZSozdStat");
                this.tbData_f_z.DataBindings.Clear();
                this.tbData_f_z.DataBindings.Add("text", bsFurnZayavInfo, "Data_f_z");
                this.tbFZSobrStat.DataBindings.Clear();
                this.tbFZSobrStat.DataBindings.Add("text", bsFurnZayavInfo, "FZSobrStat");

                this.tbUpakZayav.DataBindings.Clear();
                this.tbUpakZayav.DataBindings.Add("text", bsFurnZayavInfo, "UpakZayav");
                this.tbData_f_o_u.DataBindings.Clear();
                this.tbData_f_o_u.DataBindings.Add("text", bsFurnZayavInfo, "Data_f_o_u");
                this.tbUZSozdStat.DataBindings.Clear();
                this.tbUZSozdStat.DataBindings.Add("text", bsFurnZayavInfo, "UZSozdStat");
                this.tbData_f_z_u.DataBindings.Clear();
                this.tbData_f_z_u.DataBindings.Add("text", bsFurnZayavInfo, "Data_f_z_u");
                this.tbUZSobrStat.DataBindings.Clear();
                this.tbUZSobrStat.DataBindings.Add("text", bsFurnZayavInfo, "UZSobrStat");

                this.mtbData_zeh.DataBindings.Clear();
                this.mtbData_zeh.DataBindings.Add("text", bsFurnZayavInfo, "Data_zeh");
                this.tbIs_got.DataBindings.Clear();
                this.tbIs_got.DataBindings.Add("text", bsFurnZayavInfo, "Is_got");
                if (dtFurnZayavInfo.Rows[0]["Is_got"].ToString() == 'V'.ToString())
                {
                    tbIs_got.ForeColor = Color.Red;
                    mtbData_zeh.ForeColor = Color.Red;
                }
                else
                {
                    tbIs_got.ForeColor = Color.Black;
                    mtbData_zeh.ForeColor = Color.Black;
                }

                this.mtbData_cd.DataBindings.Clear();
                this.mtbData_cd.DataBindings.Add("text", bsFurnZayavInfo, "Data_cd");
                this.tbOtgrStat.DataBindings.Clear();
                this.tbOtgrStat.DataBindings.Add("text", bsFurnZayavInfo, "OtgrStat");
                if (dtFurnZayavInfo.Rows[0]["OtgrStat"].ToString() == 'V'.ToString())
                {
                    tbOtgrStat.ForeColor = Color.Red;
                    mtbData_cd.ForeColor = Color.Red;
                }
                else
                {
                    tbOtgrStat.ForeColor = Color.Black;
                    mtbData_cd.ForeColor = Color.Black;
                }

                this.tbDatZayav.DataBindings.Clear();
                this.tbDatZayav.DataBindings.Add("text", bsFurnZayavInfo, "DatZayav");

                //furnitZayavViewFurnit.Text = "2024   37605";
                ////furnitZayavViewFurnit.Refresh();
                //furnitZayavViewUpak.Text = "2024   40367";
                //MessageBox.Show(dtFurnZayavInfo.Rows[0]["FKoDFD"].ToString());
                //MessageBox.Show(dtFurnZayavInfo.Rows[0]["FKoDFD"].ToString().Substring(0,12));
                furnitZayavViewFurnit.Text = dtFurnZayavInfo.Rows[0]["FKoDFD"].ToString().Substring(0,12);
                furnitZayavViewFurnit.Refresh();
                furnitZayavViewUpak.Text = dtFurnZayavInfo.Rows[0]["UKoDFD"].ToString().Substring(0,12);
                furnitZayavViewUpak.Refresh();
                fspecrez = Convert.ToInt32(dtFurnZayavInfo.Rows[0]["FSpecRez"]);
                uspecrez = Convert.ToInt32(dtFurnZayavInfo.Rows[0]["USpecRez"]);
                fkodfd = dtFurnZayavInfo.Rows[0]["FKoDFD"].ToString();
                ukodfd = dtFurnZayavInfo.Rows[0]["UKoDFD"].ToString();


                
            }
        }

        private void btnFurnKKPrint_Click(object sender, EventArgs e)
        {
            //string iz = GetIzNakl();
            //PrintNaklReport report1 = new PrintNaklReport();
            //report1.RequestParameters = false;
            //report1.Parameters["_naklIz"].Value = iz;
            //ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            //reportPrintTool1.ShowPreviewDialog();

            //string nomzad = tbPsaNomZad.Text;
            //int vidf = 1;
            PrintKKReport report1 = new PrintKKReport();
            report1.RequestParameters = false;
            report1.Parameters["_kodFD"].Value = fkodfd;
            report1.Parameters["_nomZad"].Value = tbPsaNomZad.Text;
            report1.Parameters["_vidF"].Value = 1;
            report1.Parameters["_specRez"].Value = Convert.ToBoolean(fspecrez);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        //private void gcNaklList1_RowCellStyle(object sender, MaskInputRejectedEventArgs e)
        //{
        //    if (e.Column.FieldName == "Field2")
        //    {
        //        var data = gridView1.GetRow(e.RowHandle) as Sample;
        //        if (data == null)
        //            return;

        //        if (data.Field2 < 0)
        //            e.Appearance.ForeColor = Color.Red;
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    XtraReport1 xtpt = new XtraReport1();
        //    xtpt.LoadLayout(Application.StartupPath + "..\\XtraReport1.repx");
        //    ReportDesignTool tool = new ReportDesignTool(xtpt);
        //    tool.ShowDesignerDialog();
        //}
    }
}
