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

namespace SewingProduction.form
{
    public partial class FurnUpakDeliveryInfo : Form
    {
        public FurnUpakDeliveryInfo(string _kodF)
        {
            InitializeComponent();
            tbKodF.Text = _kodF;
        }
        private int GetID(string xGrid)
        {
            int _ID = 0;
            switch (xGrid)
            {
                case "gcReestrFurn":
                    try
                    {
                        object data = gridView1.GetRow(gridView1.FocusedRowHandle);
                        if (data != null)
                        {
                            _ID = Convert.ToInt32(((DataRowView)data).Row["rfID"]);
                        }
                    }
                    catch
                    {
                        _ID = 0;
                    }
                    break;
                default:
                    _ID = 0;
                    break;
            }
            return _ID;
        }
        private void getReestrFurn(string _KodF)
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapterReestrFurn = new SqlDataAdapter();
                DataTable dtReestrFurn = new DataTable();
                string queryReestrFurn = $"select * from ReestrFurn where rfKodF = '{_KodF}' and rfTipZ = 1 ";
                queryReestrFurn += $" order by rfNOtgrPp";
                SqlCommand commandReestrFurn = new SqlCommand(queryReestrFurn, connection);
                adapterReestrFurn.SelectCommand = commandReestrFurn;
                adapterReestrFurn.Fill(dtReestrFurn);
                bsReestrFurn.DataSource = dtReestrFurn;
                bsReestrFurn.Sort = "rfNOtgrPp asc";
                getReestrFurnSost(Convert.ToInt32(dtReestrFurn.Rows[0]["rfID"]));
                getReestrFurnShtr(Convert.ToInt32(dtReestrFurn.Rows[0]["rfID"]));
            }
        }
        private void getReestrFurnSost(int _rfID)
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapterReestrFurnSost = new SqlDataAdapter();
                DataTable dtReestrFurnSost = new DataTable();
                string queryReestrFurnSost = $"select * from ReestrFurnSost where rfsRfID = {_rfID} ";
                queryReestrFurnSost += $" order by rfsRfID ";
                SqlCommand commandReestrFurnSost = new SqlCommand(queryReestrFurnSost, connection);
                adapterReestrFurnSost.SelectCommand = commandReestrFurnSost;
                adapterReestrFurnSost.Fill(dtReestrFurnSost);
                bsReestrFurnSost.DataSource = dtReestrFurnSost;
                bsReestrFurnSost.Sort = "rfsRfID asc";
            }
        }
        private void getReestrFurnShtr(int _rfID)
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapterReestrFurnShtr = new SqlDataAdapter();
                DataTable dtReestrFurnShtr = new DataTable();
                string queryReestrFurnShtr = $"select * from ReestrFurnShtr where rfshRfID = {_rfID} ";
                queryReestrFurnShtr += $" order by rfshRfID ";
                SqlCommand commandReestrFurnShtr = new SqlCommand(queryReestrFurnShtr, connection);
                adapterReestrFurnShtr.SelectCommand = commandReestrFurnShtr;
                adapterReestrFurnShtr.Fill(dtReestrFurnShtr);
                bsReestrFurnShtr.DataSource = dtReestrFurnShtr;
                bsReestrFurnShtr.Sort = "rfshRfID asc";
            }
        }
        private void FurnUpakDeliveryInfo_Load(object sender, EventArgs e)
        {
            if (tbKodF.Text.Length > 0)
            {
                btnKodFDelivInfo_Click(sender, e);
            }
        }

        private void btnKodFDelivInfo_Click(object sender, EventArgs e)
        {
            
            getReestrFurn(tbKodF.Text);
            //MessageBox.Show(GetID("gcReestrFurnSost").ToString());
            //getReestrFurnSost(GetID("gcReestrFurnSost"));
//            dtFurnZayavInfo.Rows[0]["FurnKKStat"]
        }

        private void gcReestrFurn_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            getReestrFurnSost(GetID("gcReestrFurn"));
            getReestrFurnShtr(GetID("gcReestrFurn"));

        }

        private void tbKodF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnKodFDelivInfo_Click(sender, e);
            }
        }
    }
}
