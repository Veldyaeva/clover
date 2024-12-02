using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.CodeParser;
using DevExpress.Data.Linq.Helpers;
using DevExpress.DataAccess.Native.Data;
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
    public partial class PlanZagrBrig : Form
    {
        public PlanZagrBrig()
        {
            InitializeComponent();
        }

        public int XIdBrig;
        public string XNameBrig;
        public void GetBrigName(int _xIdBrig)
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapterBrigName = new SqlDataAdapter();
                System.Data.DataTable dtBrigName = new System.Data.DataTable();
                string queryBrigName = $"select brig, id_brig from brig where id_brig = {_xIdBrig} ";
                SqlCommand commandBrigName = new SqlCommand(queryBrigName, connection);
                adapterBrigName.SelectCommand = commandBrigName;
                adapterBrigName.Fill(dtBrigName);
                //bsPzNomList.DataSource = dtPachList;
                lblBrigName.Text = dtBrigName.Rows[0]["brig"].ToString();
            }
        }
        public void GetPachListByNom(int _nlNom)
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapterPachList = new SqlDataAdapter();
                System.Data.DataTable dtPachList = new System.Data.DataTable();
                string queryPachList = $"select cast(string_agg(cast(n_pach as nvarchar) +' / ' + TRIM(razm) + ' / ' + cast(kol as nvarchar), char(13) + char(10)) as nvarchar(max)) as pList ";
                queryPachList += $"	from raskr_zeh_up ";
                queryPachList += $"	where nom = {_nlNom} ";
                SqlCommand commandPachList = new SqlCommand(queryPachList, connection);
                adapterPachList.SelectCommand = commandPachList;
                adapterPachList.Fill(dtPachList);
                //bsPzNomList.DataSource = dtPachList;
                tbPList.Text = dtPachList.Rows[0]["pList"].ToString();
            }
        }
        public static System.Data.DataTable GroupAndSum(System.Data.DataTable _sourceTable, string _groupByColumn, string _sumColumn)
        {
            // Проверка на существование необходимых столбцов
            if (!_sourceTable.Columns.Contains(_groupByColumn) || !_sourceTable.Columns.Contains(_sumColumn))
            {
                throw new ArgumentException("Source table must contain specified groupBy and sum columns.");
            }
            // Проверка типа данных суммируемого столбца
            if (_sourceTable.Columns[_sumColumn].DataType != typeof(int) && _sourceTable.Columns[_sumColumn].DataType != typeof(double) && _sourceTable.Columns[_sumColumn].DataType != typeof(decimal) && _sourceTable.Columns[_sumColumn].DataType != typeof(float))
            {
                throw new ArgumentException($"Column '{_sumColumn}' must be a numeric type.");
            }
            try
            {
                // Группировка данных с помощью LINQ
                var groupedData = from row in _sourceTable.AsEnumerable()
                                  group row by row.Field<object>(_groupByColumn) into grp
                                  select new
                                  {
                                      GroupKey = grp.Key,
                                      SumValue = grp.Sum(r => Convert.ToDouble(r.Field<object>(_sumColumn))) //Обработка различных числовых типов
                                  };
                // Создание нового DataTable для результатов группировки
                System.Data.DataTable resultDataTable = new System.Data.DataTable("GroupedTable");
                resultDataTable.Columns.Add(_groupByColumn, typeof(object)); // Используем object для большей гибкости
                resultDataTable.Columns.Add("Sum_" + _sumColumn, typeof(double)); // столбец суммы, тип double
                // Заполнение нового DataTable результатами группировки
                foreach (var group in groupedData)
                {
                    resultDataTable.Rows.Add(group.GroupKey, group.SumValue);
                }
                return resultDataTable;
            }
            catch (Exception ex)
            {
                // Обработка исключений (например, преобразование типов)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Или throw ex, если нужно передать исключение дальше
            }
        }

        private void PlanZagrBrigLoadData()
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                this.gridColumn15.Visible = false;
                this.gridColumn16.Visible = false;
                this.gridColumn17.Visible = false;
                connection.Open();
                SqlDataAdapter adapterPzNomList = new SqlDataAdapter();
                System.Data.DataTable dtPzNomList = new System.Data.DataTable();
                string queryPzNomList = $"exec rzu_nzp '{XIdBrig}' ";
                //queryPartNaklList += $" order by id";
                SqlCommand commandPzNomList = new SqlCommand(queryPzNomList, connection);
                adapterPzNomList.SelectCommand = commandPzNomList;
                adapterPzNomList.Fill(dtPzNomList);
                bsPzNomList.DataSource = dtPzNomList;
                //this.gcPzNomList.Refresh();
                //bsPzNomList.Sort = "id asc";
                this.gcPzNomList.Location = this.gcPzNomList.Location;
                this.gcPzNomList.Size = this.gcPzNomList.Size;

                //GetPachListByNom(Convert.ToInt32(dtPzNomList.Rows[0]["nlNom"]));
                try
                {
                    System.Data.DataTable groupedTable = GroupAndSum(dtPzNomList, "nlGrup, nlArticul, nlMod, nlGrupK, nlArticulK, nlModK, nlKoddRt, nlKodd", "nlKol");
                    bsPzArticulList.DataSource = dtPzNomList;
                    //if (groupedTable != null)
                    //{
                    //    foreach (DataRow row in groupedTable.Rows)
                    //    {
                    //        Console.WriteLine($"Category: {row["Category"]}, Sum_Value: {row["Sum_Value"]}");
                    //    }
                    //}

                    //DataTable groupedTable2 = GroupAndSum(dataTable, "Category", "AnotherValue");
                    //if (groupedTable2 != null)
                    //{
                    //    foreach (DataRow row in groupedTable2.Rows)
                    //    {
                    //        Console.WriteLine($"Category: {row["Category"]}, Sum_AnotherValue: {row["Sum_AnotherValue"]}");
                    //    }
                    //}
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

        }

        private void PlanZagrBrig_Load(object sender, EventArgs e)
        {
            
            XIdBrig = 1;
            GetBrigName(XIdBrig);
            this.radioButton3.Checked = true;
            this.radioButton6.Checked = true;
            PlanZagrBrigLoadData();
            //string connectionString = Properties.Settings.Default.ACEConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    this.gridColumn15.Visible = false;
            //    this.gridColumn16.Visible = false;
            //    this.gridColumn17.Visible = false;
            //    connection.Open();
            //    SqlDataAdapter adapterPzNomList = new SqlDataAdapter();
            //    DataTable dtPzNomList = new DataTable();
            //    string queryPzNomList = $"exec rzu_nzp '{XIdBrig}' ";
            //    //queryPartNaklList += $" order by id";
            //    SqlCommand commandPzNomList = new SqlCommand(queryPzNomList, connection);
            //    adapterPzNomList.SelectCommand = commandPzNomList;
            //    adapterPzNomList.Fill(dtPzNomList);
            //    bsPzNomList.DataSource = dtPzNomList;
            //    //this.gcPzNomList.Refresh();
            //    //bsPzNomList.Sort = "id asc";
            //    this.gcPzNomList.Location = this.gcPzNomList.Location;
            //    this.gcPzNomList.Size = this.gcPzNomList.Size;

            //    //GetPachListByNom(Convert.ToInt32(dtPzNomList.Rows[0]["nlNom"]));
            //}

        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //MessageBox.Show(gridView2.GetDataRow(gridView2.FocusedRowHandle)["nlNom"].ToString());
            GetPachListByNom(Convert.ToInt32(gridView2.GetDataRow(gridView2.FocusedRowHandle)["nlNom"]));
        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }

        private void labelControl13_Click(object sender, EventArgs e)
        {

        }

        private void labelControl14_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {

        }
    }
}
