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
using SewingProduction.form;
using DevExpress.DataAccess.Native.Sql;
using DevExpress.XtraTab;
using DevExpress.Utils.Gesture;
using SewingProduction.Helpers;

namespace SewingProduction
{
    public partial class CardByNom : CustomForm
    {
        public int fspecrez, uspecrez;
        public string fkodfd, ukodfd;
        private readonly DatabaseHelper _dbHelper;
        public CardByNom()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            ApplyTheme();

        }

        //private void gridView4_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        //{
        //    PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;
        //    pb.PageSettings.Landscape = true;
        //}
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
        private void UpdateFurnitUpak()
        {
            string PachKod = string.Concat(tbYearPach.Text, tbNomPach.Text.PadLeft(6));
    //        string connectionString = Properties.Settings.Default.ACEConnectionString;
    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            SqlDataAdapter adapterFurnZayavInfo = new SqlDataAdapter();
    //            DataTable dtFurnZayavInfo = new DataTable();
    //            string queryFurnZayavInfo = $"exec furnitZayavCheck '{PachKod}', 1 ";  // 1 - ШП. на будущее нужно будут доработать с учетом выбора вида производства
    //            SqlCommand commandFurnZayavInfo = new SqlCommand(queryFurnZayavInfo, connection);
    //            adapterFurnZayavInfo.SelectCommand = commandFurnZayavInfo;
    //            adapterFurnZayavInfo.Fill(dtFurnZayavInfo);
    //            bsFurnZayavInfo.DataSource = dtFurnZayavInfo;
    //            this.tbFurnKKStat.DataBindings.Clear();
    //            this.tbFurnKKStat.DataBindings.Add("text", bsFurnZayavInfo, "FurnKKStat");
    //            if (dtFurnZayavInfo.Rows[0]
    //["FurnKKStat"].ToString() != 'V'.ToString() && Convert.ToInt32(dtFurnZayavInfo.Rows[0]
    //["is_furnit"]) == 1)
    //            {
    //                tbFurnKKStat.ForeColor = Color.Red;
    //                btnFurnKKPrint.ForeColor = Color.Red;
    //            }
    //            else
    //            {
    //                tbFurnKKStat.ForeColor = Color.Black;
    //                btnFurnKKPrint.ForeColor = Color.Black;
    //            }
    //            this.tbUpakKKStat.DataBindings.Clear();
    //            this.tbUpakKKStat.DataBindings.Add("text", bsFurnZayavInfo, "UpakKKStat");
    //            if (dtFurnZayavInfo.Rows[0]
    //["UpakKKStat"].ToString() != 'V'.ToString() && Convert.ToInt32(dtFurnZayavInfo.Rows[0]
    //["is_upak"]) == 1)
    //            {
    //                tbUpakKKStat.ForeColor = Color.Red;
    //                btnUpakKKPrint.ForeColor = Color.Red;
    //            }
    //            else
    //            {
    //                tbUpakKKStat.ForeColor = Color.Black;
    //                btnUpakKKPrint.ForeColor = Color.Black;
    //            }
    //            this.tbFurnZayav.DataBindings.Clear();
    //            this.tbFurnZayav.DataBindings.Add("text", bsFurnZayavInfo, "FurnZayav");
    //            this.tbData_f_o.DataBindings.Clear();
    //            this.tbData_f_o.DataBindings.Add("text", bsFurnZayavInfo, "Data_f_o");
    //            this.tbFZSozdStat.DataBindings.Clear();
    //            this.tbFZSozdStat.DataBindings.Add("text", bsFurnZayavInfo, "FZSozdStat");
    //            this.tbData_f_z.DataBindings.Clear();
    //            this.tbData_f_z.DataBindings.Add("text", bsFurnZayavInfo, "Data_f_z");
    //            this.tbFZSobrStat.DataBindings.Clear();
    //            this.tbFZSobrStat.DataBindings.Add("text", bsFurnZayavInfo, "FZSobrStat");

    //            this.tbUpakZayav.DataBindings.Clear();
    //            this.tbUpakZayav.DataBindings.Add("text", bsFurnZayavInfo, "UpakZayav");
    //            this.tbData_f_o_u.DataBindings.Clear();
    //            this.tbData_f_o_u.DataBindings.Add("text", bsFurnZayavInfo, "Data_f_o_u");
    //            this.tbUZSozdStat.DataBindings.Clear();
    //            this.tbUZSozdStat.DataBindings.Add("text", bsFurnZayavInfo, "UZSozdStat");
    //            this.tbData_f_z_u.DataBindings.Clear();
    //            this.tbData_f_z_u.DataBindings.Add("text", bsFurnZayavInfo, "Data_f_z_u");
    //            this.tbUZSobrStat.DataBindings.Clear();
    //            this.tbUZSobrStat.DataBindings.Add("text", bsFurnZayavInfo, "UZSobrStat");

    //            this.mtbData_zeh.DataBindings.Clear();
    //            this.mtbData_zeh.DataBindings.Add("text", bsFurnZayavInfo, "Data_zeh");
    //            this.tbIs_got.DataBindings.Clear();
    //            this.tbIs_got.DataBindings.Add("text", bsFurnZayavInfo, "Is_got");
    //            if (dtFurnZayavInfo.Rows[0]["Is_got"].ToString() == "--".ToString())
    //            {
    //                tbIs_got.ForeColor = Color.Red;
    //                mtbData_zeh.ForeColor = Color.Red;
    //            }
    //            else
    //            {
    //                tbIs_got.ForeColor = Color.Black;
    //                mtbData_zeh.ForeColor = Color.Black;
    //            }

    //            this.mtbData_cd.DataBindings.Clear();
    //            this.mtbData_cd.DataBindings.Add("text", bsFurnZayavInfo, "Data_cd");
    //            this.tbOtgrStat.DataBindings.Clear();
    //            this.tbOtgrStat.DataBindings.Add("text", bsFurnZayavInfo, "OtgrStat");
    //            if (dtFurnZayavInfo.Rows[0]["OtgrStat"].ToString() == 'V'.ToString())
    //            {
    //                tbOtgrStat.ForeColor = Color.Red;
    //                mtbData_cd.ForeColor = Color.Red;
    //            }
    //            else
    //            {
    //                tbOtgrStat.ForeColor = Color.Black;
    //                mtbData_cd.ForeColor = Color.Black;
    //            }

    //            this.tbDatZayav.DataBindings.Clear();
    //            this.tbDatZayav.DataBindings.Add("text", bsFurnZayavInfo, "DatZayav");

    //            furnitZayavViewFurnit.Text = dtFurnZayavInfo.Rows[0]
    //            ["FKoDFD"].ToString().Substring(0, 12);
    //            furnitZayavViewFurnit.ViewType = "r";
    //            furnitZayavViewFurnit.Refresh();
    //            furnitZayavViewUpak.Text = dtFurnZayavInfo.Rows[0]
    //            ["UKoDFD"].ToString().Substring(0, 12);
    //            furnitZayavViewUpak.ViewType = "r";
    //            furnitZayavViewUpak.Refresh();
    //            fspecrez = Convert.ToInt32(dtFurnZayavInfo.Rows[0]
    //            ["FSpecRez"]);
    //            uspecrez = Convert.ToInt32(dtFurnZayavInfo.Rows[0]
    //            ["USpecRez"]);
    //            fkodfd = dtFurnZayavInfo.Rows[0]
    //            ["FKoDFD"].ToString();
    //            ukodfd = dtFurnZayavInfo.Rows[0]
    //            ["UKoDFD"].ToString();
    //        }

            string queryFurnZayavInfo = $"exec furnitZayavCheck '{PachKod}', 1 ";  // 1 - ШП. на будущее нужно будут доработать с учетом выбора вида производства
            var dtFurnZayavInfo = _dbHelper.ExecuteQuery(queryFurnZayavInfo);//CommonFunctions.ShowRelatedData("ace", queryFurnZayavInfo);
            bsFurnZayavInfo.DataSource = dtFurnZayavInfo;
            this.tbFurnKKStat.DataBindings.Clear();
            this.tbFurnKKStat.DataBindings.Add("text", bsFurnZayavInfo, "FurnKKStat");
            if (dtFurnZayavInfo.Rows[0]
                ["FurnKKStat"].ToString() != 'V'.ToString() && Convert.ToInt32(dtFurnZayavInfo.Rows[0]
                ["is_furnit"]) == 1)
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
            if (dtFurnZayavInfo.Rows[0]
                ["UpakKKStat"].ToString() != 'V'.ToString() && Convert.ToInt32(dtFurnZayavInfo.Rows[0]
                ["is_upak"]) == 1)
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
            if (dtFurnZayavInfo.Rows[0]["Is_got"].ToString() == "--".ToString())
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

            furnitZayavViewFurnit.Text = dtFurnZayavInfo.Rows[0]
            ["FKoDFD"].ToString().Substring(0, 12);
            furnitZayavViewFurnit.ViewType = "r";
            furnitZayavViewFurnit.Refresh();
            furnitZayavViewUpak.Text = dtFurnZayavInfo.Rows[0]
            ["UKoDFD"].ToString().Substring(0, 12);
            furnitZayavViewUpak.ViewType = "r";
            furnitZayavViewUpak.Refresh();
            fspecrez = Convert.ToInt32(dtFurnZayavInfo.Rows[0]
            ["FSpecRez"]);
            uspecrez = Convert.ToInt32(dtFurnZayavInfo.Rows[0]
            ["USpecRez"]);
            fkodfd = dtFurnZayavInfo.Rows[0]
            ["FKoDFD"].ToString();
            ukodfd = dtFurnZayavInfo.Rows[0]
            ["UKoDFD"].ToString();
        }
        private void UpdateProizvCombIzd()
        {
            //string connectionString = Properties.Settings.Default.ACEConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlDataAdapter adapterProizvCombIzdSP = new SqlDataAdapter();
            //    DataTable dtProizvCombIzdSP = new DataTable();
            //    string queryProizvCombIzdSP = $"exec getProizvCombIzd {this.tbPsaPsaIDOsn.Text}, 1 ";  // 1 - расчеты ШП
            //    SqlCommand commandProizvCombIzdSP = new SqlCommand(queryProizvCombIzdSP, connection);
            //    adapterProizvCombIzdSP.SelectCommand = commandProizvCombIzdSP;
            //    adapterProizvCombIzdSP.Fill(dtProizvCombIzdSP);
            //    bsProizvCombIzdSP.DataSource = dtProizvCombIzdSP;

            //    SqlDataAdapter adapterProizvCombIzdVZP = new SqlDataAdapter();
            //    DataTable dtProizvCombIzdVZP = new DataTable();
            //    string queryProizvCombIzdVZP = $"exec getProizvCombIzd {this.tbPsaPsaIDOsn.Text}, 2 ";  // 2 - расчеты ВЗП
            //    SqlCommand commandProizvCombIzdVZP = new SqlCommand(queryProizvCombIzdVZP, connection);
            //    adapterProizvCombIzdVZP.SelectCommand = commandProizvCombIzdVZP;
            //    adapterProizvCombIzdVZP.Fill(dtProizvCombIzdVZP);
            //    bsProizvCombIzdVZP.DataSource = dtProizvCombIzdVZP;
            //}
            string queryProizvCombIzdSP = $"exec getProizvCombIzd {this.tbPsaPsaIDOsn.Text}, 1 ";  // 1 - расчеты ШП
            var dtProizvCombIzdSP = _dbHelper.ExecuteQuery(queryProizvCombIzdSP);
            bsProizvCombIzdSP.DataSource = dtProizvCombIzdSP;

            string queryProizvCombIzdVZP = $"exec getProizvCombIzd {this.tbPsaPsaIDOsn.Text}, 2 ";  // 2 - расчеты ВЗП
            var dtProizvCombIzdVZP = _dbHelper.ExecuteQuery(queryProizvCombIzdVZP);
            bsProizvCombIzdVZP.DataSource = dtProizvCombIzdVZP;
        }
        private void CardByNom_Load(object sender, EventArgs e)
        {
            this.gridColumn40.Visible = false;
            this.gridColumn44.Visible = false;
            this.gridColumn45.Visible = false;
            this.gridColumn46.Visible = false;
            this.tbYearPach.Text = Convert.ToString(DateTime.Now.Year);
            tbNomPach.Select();
        }
        private void tbNomPach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string PachKod = string.Concat(tbYearPach.Text, tbNomPach.Text.PadLeft(6));
                if (PachKod.Length > 0)
                {
                    string queryNaklList = $"select * from NaklView where nom = (select nom from raskr_zeh_up where pach_kod like '{PachKod}%') ";
                    queryNaklList += $" order by iz";
                    var dtNaklList = _dbHelper.ExecuteQuery(queryNaklList);
                    bsNaklList.DataSource = dtNaklList;
                    bsNaklList.Sort = "iz asc";
                    if (dtNaklList.Rows.Count != 0) 
                        {this.xtraTabControl1.Enabled = true;}
                    else 
                        {this.xtraTabControl1.Enabled = false;}

                    string queryRasInfo = $"exec GetRasInfoView '{PachKod}' ";
                    var dtRasInfo = _dbHelper.ExecuteQuery(queryRasInfo);
                    bsRasInfo.DataSource = dtRasInfo;
                    tbPszRpcNom.Text = "РЦ" + dtRasInfo.Rows[0]["PszRpcNom"].ToString();
                    tbArtTradeMark.Text = dtRasInfo.Rows[0]["ArtTradeMark"].ToString();
                    this.xtraTabControl1.Refresh();
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
                    this.tbPsaNN.DataBindings.Clear();
                    this.tbPsaNN.DataBindings.Add("Text", dtRasInfo, "psaNN");
                    this.tbPsaPsaID.DataBindings.Clear();
                    this.tbPsaPsaID.DataBindings.Add("Text", dtRasInfo, "psaPsaID");
                    tbPsaPsaIDOsn.Text = dtRasInfo.Rows[0]["PsaPsaIDOsn"].ToString();

                    if (Convert.ToInt32(dtRasInfo.Rows[0]["PsaPsaIDOsn"]) != 0
                        && (("V;F").IndexOf(dtRasInfo.Rows[0]["PsaKombIzd"].ToString()) >= 0 || Convert.ToInt32(dtRasInfo.Rows[0]["PsaKombIzd"]) == 1)
                        && (dtRasInfo.Rows[0]["PsaTkIdSet"].ToString().Length == 0))
                    {
                        this.xtraTabPage4.PageVisible = true;
                    }
                    else
                    {
                        this.xtraTabPage4.PageVisible = false;
                    }
                    tbPsaKombIzd.Text = dtRasInfo.Rows[0]["PsaKombIzd"].ToString();
                    tbPsaKombOsn.Text = dtRasInfo.Rows[0]["PsaKombOsn"].ToString();
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
                    this.mtbRzuDataR.DataBindings.Clear();
                    this.mtbRzuDataR.DataBindings.Add("Text", dtRasInfo, "rzuDataR");
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
                    
                    string queryIsChip = $"SELECT dbo.checkChipNakl('', '{NomZad}') AS isChip ";
                    var dtIsChip = _dbHelper.ExecuteQuery(queryIsChip);
                    bsIsChip.DataSource = dtIsChip;
                    this.cbIsChip.DataBindings.Clear();
                    this.cbIsChip.DataBindings.Add("Checked", dtIsChip, "isChip");

                    string queryOtdelkaList = $"SELECT vpso.psa_field_name, vpso.kol_sl_zv, vpso.frt_naimen, DetIzdName, VidIzdName ";
                    queryOtdelkaList += $" FROM View_plan_sezon_otdelka vpso ";
                    queryOtdelkaList += $" WHERE vpso.nn = '{this.tbPsaNN.Text}' ";
                    var dtOtdelkaList = _dbHelper.ExecuteQuery(queryOtdelkaList);
                    bsOtdelkaList.DataSource = dtOtdelkaList;

                    if (xtraTabControl1.SelectedTabPageIndex == 1)
                    {
                        UpdateFurnitUpak();
                    }
                    if (xtraTabControl1.SelectedTabPageIndex == 3)
                    {
                        UpdateProizvCombIzd();
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
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            string iz = GetIzNakl();
             
            PrintNaklReport report1 = new PrintNaklReport();
            report1.RequestParameters = false;
            report1.Parameters["_naklIz"].Value = iz;

            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        private void btnNaklPart_Click(object sender, EventArgs e)
        {
            if (btnNaklPart.Text == "Показать информацию по делению накладной")
            {
                btnNaklPart.Text = "Скрыть информацию по делению накладной";
                string iz = GetIzNakl();

                string queryPartNaklList = $"SELECT * FROM View_History_razdel_nakl where iz_b = '{iz}' ";
                queryPartNaklList += $" order by id";
                var dtPartNaklList = _dbHelper.ExecuteQuery(queryPartNaklList);
            }
            else
            {
                btnNaklPart.Text = "Показать информацию по делению накладной";
                this.gcNaklList.BringToFront();
                this.gcPartNaklList.Visible = false;
            }
            
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
        private void xtraTabControl1_Selecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            //button11_Click(sender, e);
            //if (xtraTabControl1.SelectedTabPageIndex == 1)
            //{
                UpdateFurnitUpak();
            //}
            //if (xtraTabControl1.SelectedTabPageIndex == 3)
            //{
                UpdateProizvCombIzd();
            //}
        }
        private void btnZayavFurnPrint_Click(object sender, EventArgs e)
        {
            PrintFurnUpakZayavReport report1 = new PrintFurnUpakZayavReport();
            report1.RequestParameters = false;
            report1.Parameters["_kodF"].Value = fkodfd.Substring(0, 12);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        private void btnZayavUpakPrint_Click(object sender, EventArgs e)
        {
            PrintFurnUpakZayavReport report1 = new PrintFurnUpakZayavReport();
            report1.RequestParameters = false;
            report1.Parameters["_kodF"].Value = ukodfd.Substring(0, 12);
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            FurnUpakDeliveryInfo FDI = new FurnUpakDeliveryInfo(fkodfd.Substring(0,12));
            DialogResult result = FDI.ShowDialog() ;
            // Обработка результата, возвращенного модальной формой
            if (result == DialogResult.OK)
            {
                // Действия при успешном завершении работы модальной формы
                //MessageBox.Show("OK");
            }
            else
            {
                // Действия при отмене или другом результате
                //MessageBox.Show("Cancel");
            }
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

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
        private void sbProizvCombIzdSP_Click(object sender, EventArgs e)
        {
            gcProizvCombIzdSP.ShowPrintPreview();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            
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


    }
}
