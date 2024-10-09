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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SewingProduction
{
    public partial class CardByNom : Form
    {
        public CardByNom()
        {
            InitializeComponent();
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
                    this.tbPsaMenName.DataBindings.Clear();
                    this.tbPsaMenName.DataBindings.Add("Text", dtRasInfo, "psaMenName");
                    this.tbPsaTbID.DataBindings.Clear();
                    this.tbPsaTbID.DataBindings.Add("Text", dtRasInfo, "psaTbID");
                    this.tbPsaYear.DataBindings.Clear();
                    this.tbPsaYear.DataBindings.Add("Text", dtRasInfo, "psaYear");
                    this.tbPsaSez.DataBindings.Clear();
                    this.tbPsaSez.DataBindings.Add("Text", dtRasInfo, "psaSez");
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
                    this.tbPsaDataZap.DataBindings.Clear();
                    this.tbPsaDataZap.DataBindings.Add("Text", dtRasInfo, "psaDataZap");
                    this.tbPsaDataCdPlan.DataBindings.Clear();
                    this.tbPsaDataCdPlan.DataBindings.Add("Text", dtRasInfo, "psaDataCdPlan");
                    this.tbRzuDataCdUt.DataBindings.Clear();
                    this.tbRzuDataCdUt.DataBindings.Add("Text", dtRasInfo, "rzuDataCdUt");
                    this.tbRzuDataZeh.DataBindings.Clear();
                    this.tbRzuDataZeh.DataBindings.Add("Text", dtRasInfo, "rzuDataZeh");
                    this.tbRzuDataRab.DataBindings.Clear();
                    this.tbRzuDataRab.DataBindings.Add("Text", dtRasInfo, "rzuDataRab");
                    this.tbRzuDataUp.DataBindings.Clear();
                    this.tbRzuDataUp.DataBindings.Add("Text", dtRasInfo, "rzuDataUp");
                    this.tbRzuDataCd.DataBindings.Clear();
                    this.tbRzuDataCd.DataBindings.Add("Text", dtRasInfo, "rzuDataCd");
                    //MessageBox.Show("2");

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
    }
}
