using DevExpress.ChartRangeControlClient.Core;
using DevExpress.DataAccess.Native.Data;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraGrid;
using Microsoft.ReportingServices.DataProcessing;
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
            groupControl1.BackColor = Theme.ButtonBackground;
            panelControl1.BackColor = Theme.ButtonBackground;
        }

        private void Articul_Load(object sender, EventArgs e)
        {
            try
            {
                //загрузка перечня кодов из справочника, часть полей
                string query = $"select * from dbo.view_art";
                //kodd,kod, grup, articul, razm, mod, kle
                var dt = ShowRelatedDataAce(query);
                bsArt.DataSource = dt;
                // загрузка одиночного кода из справочника, все поля  
                getArticulFromSQl("0");

                query = $"SELECT kodsp,M_Naimen_Sokr FROM view_tovar_marka where tmOwn = 1 ";
                dt = ShowRelatedDataAce(query);
                bsTM.DataSource = dt;
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
        private DataTable ShowRelatedDataAce(string query )
            //System.Windows.Forms.BindingSource bsource
        {
            DataTable dT = new DataTable();
            try
            {
                string connectionString = Properties.Settings.Default.ACEConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    connection.Open();
                    //using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
                    //{
                    //using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    using (SqlCommand command = new SqlCommand(query, connection ))
                    {
                     //   SqlCommand command = new SqlCommand(query, connection);
                     //CommandType commandType = command.CommandType;

                        adapter.SelectCommand = command;
                        adapter.Fill(dT);
                    //bsource.DataSource = dT;
                    }

                    // transaction.Commit(); // Подтверждаем транзакцию

                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return dT;
        }

        private void getArticulFromSQl(string kod)
        {
            
            try
            {   
                
                string queryArticul = $"select * from dbo.sp_articul where kod = {kod}";
                var dt = ShowRelatedDataAce(queryArticul);
                bsArticul.DataSource = dt;
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
            string kodd = "";
            
            try
            {
                object data = gridControl1.GetRow(gridControl1.FocusedRowHandle);
                if (data != null)
                {
                    kod = ((DataRowView)data).Row["kod"].ToString();
                    kodd = ((DataRowView)data).Row["kodd"].ToString();
                }

                getArticulFromSQl(kod);
            
                string query = $"select dbo.getFileEskizForKodd('{kodd}') as pathpict ";
                var dt = ShowRelatedDataAce(query);
                if (dt != null)
                {
                    pictureBoxArticul.Image = Image.FromFile(((DataTable)dt).Rows[0]["pathpict"].ToString());

                }

            }
            catch
            {
                kod = "";
            }

            


        }

        private void txbKod_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
