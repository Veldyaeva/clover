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
    public partial class Articul : CustomForm
    {
        public Articul()
        {
            InitializeComponent();
        }

        private void Articul_Load(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapterArt = new SqlDataAdapter();
                DataTable dtArticul = new DataTable();
                string queryArt = $"select kod, grup, articul, razm, mod, kle_naimen from dbo.view_art";
                queryArt += $" order by ikod";
                //string queryArt = $"exec GetNaklView '{PachKod}' ";
                SqlCommand commandNaklList = new SqlCommand(queryArt, connection);
                adapterArt.SelectCommand = commandNaklList;
                adapterArt.Fill(dtArticul);
                bsArt.DataSource = dtArticul;


            }

        }
    }
}
