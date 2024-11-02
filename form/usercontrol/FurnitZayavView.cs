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
    [DefaultEvent(nameof(TextChanged))]
    public partial class FurnitZayavView : UserControl
    {
        [Browsable(true)]
        public new event EventHandler TextChanged
        {
            add => tbKodF.TextChanged += value;
            remove => tbKodF.TextChanged -= value;
        }

        [Browsable(true)]
        public new string Text
        {
            get => tbKodF.Text;
            set => tbKodF.Text = value;
        }
        public FurnitZayavView()
        {
            InitializeComponent();
        }

        private string GetKodFD()
        {
            string kodFD = "";
            try
            {
                object data = gridView1.GetRow(gridView1.FocusedRowHandle);
                if (data != null)
                {
                    kodFD = ((DataRowView)data).Row["kod_f_d"].ToString().Trim();
                }
            }
            catch
            {
                kodFD = "";
            }
            return kodFD;
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _kodF = this.tbKodF.Text;
            if (_kodF.Length > 0)
            {
                string connectionString = Properties.Settings.Default.ACEConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapterFurnitArt = new SqlDataAdapter();
                    DataTable dtFurnitArt = new DataTable();
                    string queryFurnitArt = $"select * from furnit_art where kod_f = '{_kodF}' ";
                    queryFurnitArt += $" order by kod_f_d";
                    SqlCommand commandFurnitArt = new SqlCommand(queryFurnitArt, connection);
                    adapterFurnitArt.SelectCommand = commandFurnitArt;
                    adapterFurnitArt.Fill(dtFurnitArt);
                    bsFurnitArt.DataSource = dtFurnitArt;
                    bsFurnitArt.Sort = "kod_f_d asc";

                    gcFurnitArt.Refresh();

                    string _kodFD = GetKodFD();
                    MessageBox.Show(_kodFD);
                    this.cbIsChip.DataBindings.Clear();
                    this.cbIsChip.DataBindings.Add("Checked", dtIsChip, "isChip");

                    SqlDataAdapter adapterFurnitPach = new SqlDataAdapter();
                    DataTable dtFurnitPach = new DataTable();
                    string queryFurnitPach = $"select * from furnit_pach where kod_f_d = '{_kodFD}' ";
                    queryFurnitPach += $" order by pach_kod";
                    SqlCommand commandFurnitPach = new SqlCommand(queryFurnitPach, connection);
                    adapterFurnitPach.SelectCommand = commandFurnitPach;
                    adapterFurnitPach.Fill(dtFurnitPach);
                    bsFurnitPach.DataSource = dtFurnitPach;

                    SqlDataAdapter adapterFurnitF = new SqlDataAdapter();
                    DataTable dtFurnitF = new DataTable();
                    string queryFurnitF = $"select * from furnit_f where kod_f_d = '{_kodFD}' ";
                    queryFurnitF += $" order by n_pp";
                    SqlCommand commandFurnitF = new SqlCommand(queryFurnitF, connection);
                    adapterFurnitF.SelectCommand = commandFurnitF;
                    adapterFurnitF.Fill(dtFurnitF);
                    bsFurnitF.DataSource = dtFurnitF;
                    bsFurnitF.Sort = "n_pp asc";

                    SqlDataAdapter adapterFurnitFIt = new SqlDataAdapter();
                    DataTable dtFurnitFIt = new DataTable();
                    string queryFurnitFIt = $"select * from furnit_f_it where kod_f_d = '{_kodFD}' ";
                    queryFurnitF += $" order by n_pp, ffiID";
                    SqlCommand commandFurnitFIt = new SqlCommand(queryFurnitFIt, connection);
                    adapterFurnitFIt.SelectCommand = commandFurnitFIt;
                    adapterFurnitFIt.Fill(dtFurnitFIt);
                    bsFurnitFIt.DataSource = dtFurnitFIt;
                    bsFurnitFIt.Sort = "n_pp, ffiID asc";

                    //this.pbEskiz.DataBindings.Clear();
                    //this.pbEskiz.DataBindings.Add("ImageLocation", dtRasInfo, "pictPath");
                    //this.tbRzuNom.DataBindings.Clear();
                    //this.tbRzuNom.DataBindings.Add("Text", dtRasInfo, "rzuNom");
                }
            }
        }

        private void tbKodF_TextChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
