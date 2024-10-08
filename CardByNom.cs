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
            this.textBox2.Text = Convert.ToString(DateTime.Now.Year);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void textBox1_Leave(object sender, EventArgs e)
        {
            int NomPach = Convert.ToInt32(this.textBox1.Text);
            int YearPach = Convert.ToInt32(this.textBox2.Text);
            if (NomPach > 0 && YearPach > 0)
            {
                string connectionString = Properties.Settings.Default.ACEConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Console.WriteLine("Подключение открыто");
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    //string query = $"select * from raskr_zeh_up where pach_kod like {YearPach}{NomPach} + '%' ";
                    //query += $" order by n_pach";
                    string query = $"select * from NaklView where nom = (select nom from raskr_zeh_up where pach_kod like '{YearPach}{NomPach}%') ";
                    query += $" order by iz";
                    SqlCommand command = new SqlCommand(query, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                    bsNaklList.DataSource = dt;
                    bsNaklList.Sort = "iz asc";
                    //MessageBox.Show("Запрос выполнен", "Запрос списка накладных", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            }
            
        }
    }
}
