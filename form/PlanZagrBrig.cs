using DevExpress.ChartRangeControlClient.Core;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.CodeParser;
using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using DevExpress.Data.Linq.Helpers;
using DevExpress.DataAccess.DataFederation;
using DevExpress.DataAccess.Native.Data;
using DevExpress.DataAccess.Native.Json;
using DevExpress.DataAccess.Sql;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.PivotGrid.QueryMode;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraCharts.Design;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace SewingProduction.form
{
    public partial class PlanZagrBrig : CustomForm
    {
        public int XIdBrig;
        public string XNameBrig;
        public System.Data.DataTable dtNomList;
        public System.Data.DataTable dtPzArticulList;
        public System.Data.DataTable dtPzOperList;
        public PlanZagrBrig()
        {
            InitializeComponent();
        }

        
        public void GetBrigName(int _xIdBrig)
        {
            //string connectionString = Properties.Settings.Default.ACEConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlDataAdapter adapterBrigName = new SqlDataAdapter();
            //    System.Data.DataTable dtBrigName = new System.Data.DataTable();
            //    string queryBrigName = $"select brig, id_brig from brig where id_brig = {_xIdBrig} ";
            //    SqlCommand commandBrigName = new SqlCommand(queryBrigName, connection);
            //    adapterBrigName.SelectCommand = commandBrigName;
            //    adapterBrigName.Fill(dtBrigName);
            //    //bsPzNomList.DataSource = dtPachList;
            //    lblBrigName.Text = dtBrigName.Rows[0]["brig"].ToString();
            //}
            string queryBrigName = $"select brig, id_brig from brig where id_brig = {_xIdBrig} ";
            var dtBrigName = CommonFunctions.ShowRelatedData("ace", queryBrigName);
            lblBrigName.Text = dtBrigName.Rows[0]["brig"].ToString();
        }
        public void GetMonthList()
        {
            try
            {
                //string connectionString = Properties.Settings.Default.ACEConnectionString;
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    SqlDataAdapter adapterMonthList = new SqlDataAdapter();
                //    System.Data.DataTable dtMonthList = new System.Data.DataTable();
                //    string queryMonthList = $"SELECT * FROM spr_month ";
                //    SqlCommand commandMonthList = new SqlCommand(queryMonthList, connection);
                //    adapterMonthList.SelectCommand = commandMonthList;
                //    adapterMonthList.Fill(dtMonthList);
                    
                //}
                string queryMonthList = $"SELECT * FROM spr_month ";
                var dtMonthList = CommonFunctions.ShowRelatedData("ace", queryMonthList);
                bsMonthList.DataSource = dtMonthList;
                cbMonthList.DataSource = bsMonthList;
                cbMonthList.DisplayMember = "name_month";
                cbMonthList.ValueMember = "kod";
                cbMonthList.SelectedIndex = -1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void GetPachListByNom(int _nlNom)
        {
            if (_nlNom == 0)
            {
                tbPList.Text = "";
            }
            else
            {
                //string connectionString = Properties.Settings.Default.ACEConnectionString;
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    SqlDataAdapter adapterPachList = new SqlDataAdapter();
                //    System.Data.DataTable dtPachList = new System.Data.DataTable();
                //    string queryPachList = $"select cast(string_agg(cast(n_pach as nvarchar) +' / ' + TRIM(razm) + ' / ' + cast(kol as nvarchar), char(13) + char(10)) as nvarchar(max)) as pList ";
                //    queryPachList += $"	from raskr_zeh_up ";
                //    queryPachList += $"	where nom = {_nlNom} ";
                //    SqlCommand commandPachList = new SqlCommand(queryPachList, connection);
                //    adapterPachList.SelectCommand = commandPachList;
                //    adapterPachList.Fill(dtPachList);
                //    //bsPzNomList.DataSource = dtPachList;
                //    tbPList.Text = dtPachList.Rows[0]["pList"].ToString();
                //}
                string queryPachList = $"select cast(string_agg(cast(n_pach as nvarchar) +' / ' + TRIM(razm) + ' / ' + cast(kol as nvarchar), char(13) + char(10)) as nvarchar(max)) as pList ";
                queryPachList += $"	from raskr_zeh_up ";
                queryPachList += $"	where nom = {_nlNom} ";
                var dtPachList = CommonFunctions.ShowRelatedData("ace", queryPachList);
                tbPList.Text = dtPachList.Rows[0]["pList"].ToString();
            }
        }
        //public static System.Data.DataTable GroupAndSum(System.Data.DataTable _sourceTable, string _groupByColumn, string _sumColumn)
        //{
        //    // Проверка на существование необходимых столбцов
        //    if (!_sourceTable.Columns.Contains(_groupByColumn) || !_sourceTable.Columns.Contains(_sumColumn))
        //    {
        //        throw new ArgumentException("Source table must contain specified groupBy and sum columns.");
        //    }
        //    // Проверка типа данных суммируемого столбца
        //    if (_sourceTable.Columns[_sumColumn].DataType != typeof(int) && _sourceTable.Columns[_sumColumn].DataType != typeof(double) && _sourceTable.Columns[_sumColumn].DataType != typeof(decimal) && _sourceTable.Columns[_sumColumn].DataType != typeof(float))
        //    {
        //        throw new ArgumentException($"Column '{_sumColumn}' must be a numeric type.");
        //    }
        //    try
        //    {
        //        // Группировка данных с помощью LINQ
        //        var groupedData = from row in _sourceTable.AsEnumerable()
        //                          group row by row.Field<object>(_groupByColumn) into grp
        //                          select new
        //                          {
        //                              GroupKey = grp.Key,
        //                              SumValue = grp.Sum(r => Convert.ToDouble(r.Field<object>(_sumColumn))) //Обработка различных числовых типов
        //                          };
        //        // Создание нового DataTable для результатов группировки
        //        System.Data.DataTable resultDataTable = new System.Data.DataTable("GroupedTable");
        //        resultDataTable.Columns.Add(_groupByColumn, typeof(object)); // Используем object для большей гибкости
        //        resultDataTable.Columns.Add("Sum_" + _sumColumn, typeof(double)); // столбец суммы, тип double
        //        // Заполнение нового DataTable результатами группировки
        //        foreach (var group in groupedData)
        //        {
        //            resultDataTable.Rows.Add(group.GroupKey, group.SumValue);
        //        }
        //        return resultDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка исключений (например, преобразование типов)
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //        return null; // Или throw ex, если нужно передать исключение дальше
        //    }
        //}

        public System.Data.DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            System.Data.DataTable dtReturn = new System.Data.DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new System.Data.DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public int GetUslFilter()
        {
            int _xUsl = 0;
            if (this.radioButton1.Checked == true)  // услуги
            {
                _xUsl = 1;
            }
            else if (this.radioButton2.Checked == true) // собственное
            {
                _xUsl = 2;
            }
            else if(this.radioButton3.Checked == true)  // ВСЕ
            {
                _xUsl = 3;
            }
             return _xUsl;

        }
        public int GetNZPFilter()
        {
            int _xNZP = 0;

            if (this.radioButton7.Checked == true)  // отгруженные с НЗОп
            {
                _xNZP = 3;
            }
            else if (this.radioButton6.Checked == true) // не отгруженные
            {
                _xNZP = 1;
            }
            else if (this.radioButton8.Checked == true) //ВСЕ с НЗОп
            {
                _xNZP = 2;
            }
            else if (this.radioButton9.Checked == true) // отгруженные без НЗОп
            {
                _xNZP = 4;
            }
            else if (this.radioButton5.Checked == true) // ВСЕ
            {
                _xNZP = 5;
            }
            return _xNZP;
        }
        public int GetMonthPlan()
        {
            int _xMonthPlan = 0;
            if (this.cbMonthList.SelectedIndex != -1)
            {
                _xMonthPlan = Convert.ToInt32(cbMonthList.SelectedValue);
            }
            return _xMonthPlan;
        }
        public int GetYearPlan()
        {
            int _xYearPlan = 0;
            int.TryParse(this.tbYearPlan.Text, out _xYearPlan);
            return _xYearPlan;
        }
        private void PlanZagrBrigLoadData()
        {
            //string connectionString = Properties.Settings.Default.ACEConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    this.gridColumn15.Visible = false;
            //    this.gridColumn16.Visible = false;
            //    this.gridColumn17.Visible = false;
            //    connection.Open();
            //    SqlDataAdapter adapterNomList = new SqlDataAdapter();
            //    dtNomList = new System.Data.DataTable();
            //    string queryNomList = $"exec rzu_nzp {XIdBrig}, {GetUslFilter()}, {GetNZPFilter()}, {GetYearPlan()}, {GetMonthPlan()} ";
            //    SqlCommand commandNomList = new SqlCommand(queryNomList, connection);
            //    adapterNomList.SelectCommand = commandNomList;
            //    adapterNomList.Fill(dtNomList);
                
            //    this.gcPzNomList.Location = this.gcPzNomList.Location;
            //    this.gcPzNomList.Size = this.gcPzNomList.Size;

            //    var queryPzArticulList = from row in dtNomList.AsEnumerable()
            //                             //where (row.IsNull("nlDateCd") || row.Field<DateTime>("nlDateCd") == DateTime.MinValue)
            //                             group row by new {
            //                          nlGrup = row.Field<string>("nlGrup"),
            //                          nlArticul = row.Field<string>("nlArticul"),
            //                          nlMod = row.Field<string>("nlMod"),
            //                          nlGrupK = row.Field<string>("nlGrupK"),
            //                          nlArticulK = row.Field<string>("nlArticulK"),
            //                          nlModK = row.Field<string>("nlModK"),
            //                          nlKoddRt = row.Field<string>("nlKoddRt"),
            //                          nlKodd = row.Field<string>("nlKodd"),
            //                          nlKodd7 = row.Field<string>("nlKodd7"),
            //                             } into g
            //                      select new
            //                      {
            //                          alGrup = g.Key.nlGrup,
            //                          alArticul = g.Key.nlArticul,
            //                          alMod = g.Key.nlMod,
            //                          alGrupK = g.Key.nlGrupK,
            //                          alArticulK = g.Key.nlArticulK,
            //                          alModK = g.Key.nlModK,
            //                          alKoddRt = g.Key.nlKoddRt,
            //                          alKodd = g.Key.nlKodd,
            //                          alKodd7 = g.Key.nlKodd7,
            //                          alV = 0,
            //                          alArtSort = (g.Key.nlArticulK.Length != 0 ? g.Key.nlArticulK : g.Key.nlArticul),
            //                          alModSort = (g.Key.nlModK.Length != 0 ? g.Key.nlModK : g.Key.nlMod),
            //                          alDataCdPl = g.Min(row => row.Field<DateTime>("nlDataCdPl")),
            //                          alKol = g.Sum(row => row.Field<decimal>("nlKol")),
            //                          alRowNumber = 0
            //                      };
                
            //    dtPzArticulList = ConvertToDataTable(queryPzArticulList);
            //    string _alKodd7 = "";
            //    string _alMod = "";
            //    if (dtPzArticulList.Rows.Count != 0)
            //    {
            //        dtPzArticulList.DefaultView.Sort = "alDataCdPl, alArtSort, alModSort, alArticul, alMod ASC";
            //        _alKodd7 = dtPzArticulList.Rows[0]["alKodd7"].ToString() == null ? "" : dtPzArticulList.Rows[0]["alKodd7"].ToString();
            //        _alMod = dtPzArticulList.Rows[0]["alMod"].ToString() == null ? "" : dtPzArticulList.Rows[0]["alMod"].ToString();
            //        for (int i = 0; i < dtPzArticulList.Rows.Count; i++)
            //        {
            //            dtPzArticulList.Rows[i]["alRowNumber"] = i + 1;
            //        }
            //    }
            //    bsPzArticulList.DataSource = dtPzArticulList;

            //    GetArticulNomList(_alKodd7, _alMod);
            //}

            this.gridColumn15.Visible = false;
            this.gridColumn16.Visible = false;
            this.gridColumn17.Visible = false;
            string queryNomList = $"exec rzu_nzp {XIdBrig}, {GetUslFilter()}, {GetNZPFilter()}, {GetYearPlan()}, {GetMonthPlan()} ";
            dtNomList = CommonFunctions.ShowRelatedData("ace", queryNomList);
            this.gcPzNomList.Location = this.gcPzNomList.Location;
            this.gcPzNomList.Size = this.gcPzNomList.Size;
            var queryPzArticulList = from row in dtNomList.AsEnumerable()
                                         //where (row.IsNull("nlDateCd") || row.Field<DateTime>("nlDateCd") == DateTime.MinValue)
                                     group row by new
                                     {
                                         nlGrup = row.Field<string>("nlGrup"),
                                         nlArticul = row.Field<string>("nlArticul"),
                                         nlMod = row.Field<string>("nlMod"),
                                         nlGrupK = row.Field<string>("nlGrupK"),
                                         nlArticulK = row.Field<string>("nlArticulK"),
                                         nlModK = row.Field<string>("nlModK"),
                                         nlKoddRt = row.Field<string>("nlKoddRt"),
                                         nlKodd = row.Field<string>("nlKodd"),
                                         nlKodd7 = row.Field<string>("nlKodd7"),
                                     } into g
                                     select new
                                     {
                                         alGrup = g.Key.nlGrup,
                                         alArticul = g.Key.nlArticul,
                                         alMod = g.Key.nlMod,
                                         alGrupK = g.Key.nlGrupK,
                                         alArticulK = g.Key.nlArticulK,
                                         alModK = g.Key.nlModK,
                                         alKoddRt = g.Key.nlKoddRt,
                                         alKodd = g.Key.nlKodd,
                                         alKodd7 = g.Key.nlKodd7,
                                         alV = 0,
                                         alArtSort = (g.Key.nlArticulK.Length != 0 ? g.Key.nlArticulK : g.Key.nlArticul),
                                         alModSort = (g.Key.nlModK.Length != 0 ? g.Key.nlModK : g.Key.nlMod),
                                         alDataCdPl = g.Min(row => row.Field<DateTime>("nlDataCdPl")),
                                         alKol = g.Sum(row => row.Field<decimal>("nlKol")),
                                         alRowNumber = 0
                                     };

            dtPzArticulList = ConvertToDataTable(queryPzArticulList);
            string _alKodd7 = "";
            string _alMod = "";
            if (dtPzArticulList.Rows.Count != 0)
            {
                dtPzArticulList.DefaultView.Sort = "alDataCdPl, alArtSort, alModSort, alArticul, alMod ASC";
                _alKodd7 = dtPzArticulList.Rows[0]["alKodd7"].ToString() == null ? "" : dtPzArticulList.Rows[0]["alKodd7"].ToString();
                _alMod = dtPzArticulList.Rows[0]["alMod"].ToString() == null ? "" : dtPzArticulList.Rows[0]["alMod"].ToString();
                for (int i = 0; i < dtPzArticulList.Rows.Count; i++)
                {
                    dtPzArticulList.Rows[i]["alRowNumber"] = i + 1;
                }
            }
            bsPzArticulList.DataSource = dtPzArticulList;

            GetArticulNomList(_alKodd7, _alMod);
        }
        private void GetArticulNomList(string _alKodd7, string _alMod)
        {
            var queryPzNomlList = from nl in dtNomList.AsEnumerable()
                                  where (nl.Field<string>("nlKodd7") == _alKodd7 && nl.Field<string>("nlMod") == _alMod)
                                  select new
                                  {
                                      nlNom = nl.Field<decimal>("nlNom"),
                                      nlNomN = nl.Field<decimal>("nlNomN"),
                                      nlBrig = nl.Field<string>("nlBrig"),
                                      nlNomZad = nl.Field<string>("nlNomZad"),
                                      nlKodd = nl.Field<string>("nlKodd"),
                                      nlKoddRt = nl.Field<string>("nlKoddRt"),
                                      nlGrup = nl.Field<string>("nlGrup"),
                                      nlArticul = nl.Field<string>("nlArticul"),
                                      nlMod = nl.Field<string>("nlMod"),
                                      nlGrupK = nl.Field<string>("nlGrupK"),
                                      nlArticulK = nl.Field<string>("nlArticulK"),
                                      nlModK = nl.Field<string>("nlModK"),
                                      nlKol = nl.Field<decimal>("nlKol"),
                                      nlIdBrig = nl.Field<int>("nlIdBrig"),
                                      nlNZzeh = nl.Field<decimal>("nlNZzeh"),
                                      nlDateZeh = (nl.IsNull("nlDateZeh") ? DateTime.MinValue : nl.Field<DateTime>("nlDateZeh")),
                                      nlDateZehP = (nl.IsNull("nlDateZehP") ? DateTime.MinValue : nl.Field<DateTime>("nlDateZehP")),
                                      nlDateRab = (nl.IsNull("nlDateRab") ? DateTime.MinValue : nl.Field<DateTime>("nlDateRab")),
                                      nlDataCdPl = (nl.IsNull("nlDataCdPl") ? DateTime.MinValue : nl.Field<DateTime>("nlDataCdPl")),
                                      nlDataCd = (nl.IsNull("nlDateCd") ? DateTime.MinValue : nl.Field<DateTime>("nlDateCd")),
                                      nlPachMin = nl.Field<decimal>("nlPachMin"),
                                      nlPachMax = nl.Field<decimal>("nlPachMax"),
                                      nlV = nl.Field<int>("nlV"),
                                      nlKodd7 = nl.Field<string>("nlKodd7"),
                                      nlNzOp = nl.Field<int>("nlNzOp"),
                                      nlTbID = nl.Field<string>("nlTbID"),
                                      brUsl = nl.Field<int>("brUsl"),
                                      idBrigFrom = (nl.IsNull("idBrigFrom") ? 0 : nl.Field<int>("idBrigFrom")),
                                      idBrigTo = (nl.IsNull("idBrigTo") ? 0 : nl.Field<int>("idBrigTo")),
                                      nlIzNakl = nl.Field<decimal>("nlIzNakl"),
                                      nn = nl.Field<string>("nn"),
                                      vidPr = nl.Field<string>("vidPr"),
                                      nlRowNumber = nl.Field<int>("nlRowNumber")
                                  };
            System.Data.DataTable dtPzNomList = ConvertToDataTable(queryPzNomlList);
            bsPzNomList.DataSource = dtPzNomList;
            if (dtPzNomList.Rows.Count != 0)
            {
                dtPzNomList.DefaultView.Sort = "nlDataCdPl, nlNom ASC";
                GetPachListByNom(Convert.ToInt32(dtPzNomList.Rows[0]["nlNom"]));
            }
            bsPzNomList.DataSource = dtPzNomList;
        }

        private void GetPztOperList(string _xmlString)
        {
            //string connectionString = Properties.Settings.Default.ACEConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlDataAdapter adapterPzOperList = new SqlDataAdapter();
            //    dtPzOperList = new System.Data.DataTable();
            //    string queryPzOperList = $"exec planZagrTwo_view ''";
            //    //queryPartNaklList += $" order by id";
            //    SqlCommand commandPzOperList = new SqlCommand(queryPzOperList, connection);
            //    adapterPzOperList.SelectCommand = commandPzOperList;
            //    adapterPzOperList.Fill(dtPzOperList);
                
            //}
            string queryPzOperList = $"exec planZagrTwo_view ''";
            var dtPzOperList = CommonFunctions.ShowRelatedData("ace", queryPzOperList);
            bsPzOperList.DataSource = dtPzOperList;
        }
        //private int LocateByNomInArticulList(string _cnAlArticul, string _vAlArticul, string _cnAlMod, string _vAlMod, string _cnAlKoddRT, string _vAlKoddRT, string _cnAlArticulK, string _vAlArticulK, string _cnAlModK, string _vAlModK)
        //{
        //    for (int i = 0; i < gridView1.RowCount - 1; i++) // -1 чтобы избежать ошибки с последней пустой строкой
        //    {
        //        DataGridViewRow row = gridView1.GetDataRow(i);

        //        // Проверка на null, чтобы избежать исключений, если ячейки пустые
        //        if (row.Cells[column1Name].Value != null && row.Cells[column2Name].Value != null)
        //        {
        //            if (row.Cells[column1Name].Value.ToString().Equals(value1, StringComparison.OrdinalIgnoreCase) &&
        //                row.Cells[column2Name].Value.ToString().Equals(value2, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return i; // Строка найдена
        //            }
        //        }
        //    }
        //    return -1; // Строка не найдена
        //}

        public static string DataRowToXml3(DataRow dataRow, string _row)
        {
            XElement root = new XElement(_row,
                from System.Data.DataColumn column in dataRow.Table.Columns
                select new XElement(column.ColumnName.ToLower(), dataRow[column])
            );
            return root.ToString();
        }
        private void PlanZagrBrig_Load(object sender, EventArgs e)
        {
            XIdBrig = 1;
            GetBrigName(XIdBrig);
            this.cbMonthList.SelectedIndex = -1;
            this.radioButton3.Checked = true;
            this.radioButton6.Checked = true;
            GetMonthList();
            GetPztOperList("");
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

        private void gcPzNomList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //MessageBox.Show(gridView2.GetDataRow(gridView2.FocusedRowHandle)["nlNom"].ToString());
            if (gridView2.RowCount > 0)
            {
                GetPachListByNom(Convert.ToInt32(gridView2.GetDataRow(gridView2.FocusedRowHandle)["nlNom"]));
            }
            else
            {
                GetPachListByNom(0);
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(cbMonthList.SelectedValue.ToString());
            cbMonthList.SelectedIndex = -1;
            PlanZagrBrigLoadData();
        }

        private void cbMonthList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMonthList.SelectedIndex > -1 && tbYearPlan.Text.ToString().Length > 0)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.tbYearPlan.Text = "";
            this.simpleButton4_Click(sender,e);
            PlanZagrBrigLoadData();
        }

        private void tbYearPlan_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbMonthList.SelectedIndex > -1 && tbYearPlan.Text.ToString().Length > 0)
                {
                    PlanZagrBrigLoadData();
                }
            }
        }

        private void gcPzArticulList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                GetArticulNomList(gridView1.GetDataRow(gridView1.FocusedRowHandle)["alKodd7"].ToString(), gridView1.GetDataRow(gridView1.FocusedRowHandle)["alMod"].ToString());
            }
            else
            {
                GetPachListByNom(0);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void tbNlNom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtNomList.PrimaryKey = new System.Data.DataColumn[] { dtNomList.Columns["nlNom"] };
                DataRow drNomListfoundRow = dtNomList.Rows.Find($"{this.tbNlNom.Text}");
                if (drNomListfoundRow != null)
                { 
                    string _nlArticul = drNomListfoundRow["nlArticul"].ToString();
                    string _nlMod = drNomListfoundRow["nlMod"].ToString();
                    string _nlKoddRT = drNomListfoundRow["nlKoddRT"].ToString();
                    string _nlArticulK = drNomListfoundRow["nlArticulK"].ToString();
                    string _nlModK = drNomListfoundRow["nlModK"].ToString();
                    dtPzArticulList.PrimaryKey = new System.Data.DataColumn[] { dtPzArticulList.Columns["alArticul"],
                                                                                dtPzArticulList.Columns["alMod"],
                                                                                dtPzArticulList.Columns["alKoddRT"],
                                                                                dtPzArticulList.Columns["alArticulK"],
                                                                                dtPzArticulList.Columns["alModK"] };
                    object[] compositeKeyValue = { _nlArticul, _nlMod, _nlKoddRT, _nlArticulK, _nlModK };
                    DataRow drPzArticulList = dtPzArticulList.Rows.Find(compositeKeyValue);

                    gridView1.FocusedRowHandle = gridView1.LocateByValue("alRowNumber", drPzArticulList["alRowNumber"].ToString());
                    //gridView2.FocusedColumn.FieldName = "nlV";

                    //gridView2.FocusedColumn = gridView1.Columns["GridColumn8"];

                    if (gridView2 == null) return;
                    //MessageBox.Show(drNomListfoundRow["nlRowNumber"].ToString());
                    gridView2.FocusedRowHandle = gridView2.LocateByValue("nlRowNumber", drNomListfoundRow["nlRowNumber"].ToString());
                    gridView2.FocusedColumn = gridView2.Columns["nlV"];
                    gridView2.ShowEditor();
                }
                else
                {
                    MessageBox.Show($"Расчет {this.tbNlNom.Text} не найден. Измените параметры фильтра и повторите попытку");
                }

            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.FieldName.ToString() == "nlV")
            //{
            //    if (Convert.ToInt32(gridView2.GetDataRow(e.RowHandle)["nlV"]) == 1)
            //    {
            //        MessageBox.Show("111");
            //    }
            //    else
            //    {
            //        MessageBox.Show("222");
            //    }
            //}
        }

        private void repositoryItemCheckEdit3_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(repositoryItemCheckEdit3 ) == 1)
            //{
            //    MessageBox.Show("111");
            //}
            //else
            //{
            //    MessageBox.Show("222");
            //}
        }

        private void repositoryItemCheckEdit3_CheckedChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(repositoryItemCheckEdit3.value) == 1)
            //{
            //    MessageBox.Show("111");
            //}
            //else
            //{
            //    MessageBox.Show("222");
            //}
            gridView2.FocusedColumn = gridView2.Columns["nlIzNakl"];
            gridView2.FocusedColumn = gridView2.Columns["nlV"];
            MessageBox.Show(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, gridView2.FocusedColumn).ToString());
            
            if (gridView2.GetRowCellValue(gridView2.FocusedRowHandle, gridView2.FocusedColumn).ToString() == "1")
            {
                
                //string xml3 = DataRowToXml3(gridView1.GetDataRow(gridView2.FocusedRowHandle));
                //Console.WriteLine(xml3);
                //Console.WriteLine(DataRowToXml3(gridView1.GetDataRow(gridView2.FocusedRowHandle)));

                string connectionString = Properties.Settings.Default.ACEConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    MessageBox.Show(DataRowToXml3(gridView2.GetDataRow(gridView2.FocusedRowHandle), "nom_kod_list"));
                    string sqlQuery = $"exec planZagrTwo_view '{DataRowToXml3(gridView2.GetDataRow(gridView2.FocusedRowHandle), "nom_kod_list")}'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        connection.Open();
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                dtPzOperList.Load(reader);
                            }
                        }
                        catch (Exception ex)
                        {
                            //Handle exceptions
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }

        }
    }
}
