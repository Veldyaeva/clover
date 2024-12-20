using DevExpress.ChartRangeControlClient.Core;
using DevExpress.DataAccess.Native.Data;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraGrid;
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
using static DevExpress.Utils.Menu.DXMenuItemPainter;
using DataTable = System.Data.DataTable;
//using DataTable = DevExpress.DataAccess.Native.Data.DataTable;

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
            try
            {
                string query = $"select kod, grup, articul, razm, mod, kle from dbo.view_art";
                ShowRelatedDataAce(query, bsArt);
                
                get_ArticulFromSQl("0");

                query = $"SELECT kodsp,M_Naimen_Sokr FROM view_tovar_marka where tmOwn = 1 ";
                ShowRelatedDataAce(query, bsTM);
                cbTM.DisplayMember = "M_Naimen_Sokr";
                cbTM.ValueMember = "kodsp";
                cbTM.DataBindings.Add("SelectedValue", bsArticul, "kle", true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //groupControl1.AppearanceCaption.BackColor = Theme.ButtonBackground;

        }
        private void ShowRelatedDataAce(string query, System.Windows.Forms.BindingSource bsource)
        {
            try
            {
                string connectionString = Properties.Settings.Default.ACEConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    connection.Open();
                    //using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
                    //{
                        DataTable dT = new DataTable();

                    //using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                     //   SqlCommand command = new SqlCommand(query, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(dT);
                    bsource.DataSource = dT;
                    }

                    // transaction.Commit(); // Подтверждаем транзакцию

                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void get_ArticulFromSQl(string kod)
        {
            //string connectionString = Properties.Settings.Default.ACEConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlDataAdapter adapterArt = new SqlDataAdapter();
            //    DataTable dtArticul = new DataTable();
            //    string query = $"select * from dbo.sp_articul where kod = @kod ";
            //    SqlCommand command = new SqlCommand();
            //    command.Connection = connection;
            //    command.CommandText = query;
            //    command.Parameters.AddWithValue("kod",kod);
            //    adapterArt.SelectCommand = command;
            //    adapterArt.Fill(dtArticul);
            //    bsArticul.DataSource = dtArticul;
            //}
            //txbKod.Text = dtArticul.Rows[0]["kod"].ToString();
            //txbArticul.DataBindings.Clear();
            //txbArticul.DataBindings.Add(new Binding("Text", bsArticul, "Articul", true, DataSourceUpdateMode.OnPropertyChanged));
            try
            {   
                
                    string queryArticul = $"select * from dbo.sp_articul where kod = {kod}";
                    ShowRelatedDataAce(queryArticul, bsArticul);
                    if (bsArticul.Count > 0)
                        {
                            txbKod.Text = ((DataTable)bsArticul.DataSource).Rows[0]["kod"].ToString();
                            txbArticul.Text = ((DataTable)bsArticul.DataSource).Rows[0]["articul"].ToString();
                        }
                    
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void gridControl1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //var kod = Convert.ToInt32(gridControl1.GetDataRow(gridControl1.FocusedRowHandle)["kod"]);
            string kod = "";
            try
            {
                object data = gridControl1.GetRow(gridControl1.FocusedRowHandle);
                if (data != null)
                {
                    kod = ((DataRowView)data).Row["kod"].ToString();
                }
            }
            catch
            {
                kod = "";
            }

            get_ArticulFromSQl(kod);
            

        }

        private void txbKod_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
