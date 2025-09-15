using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
//using System.Windows.Media;
using System.Xml.Linq;
using DataColumn = System.Data.DataColumn;
using DataTable = System.Data.DataTable;
//using XmlElement = System.Xls.XmlElement;

namespace SewingProduction.form
{
    public partial class PlanZagrBrig : CustomForm, IThemeable
    {
        public int XIdBrig;
        public string XNameBrig;
        public DataTable dtNomList;
        public DataTable dtPzArticulList;
        public DataTable dtPzNomList;
        public DataTable dtPzOperList;
        private readonly DatabaseHelper _dbHelperACE;
        private readonly DatabaseHelper _dbHelperGLOBAL;

        public PlanZagrBrig()
        {
            InitializeComponent();
            _dbHelperACE = new DatabaseHelper("ace");//Properties.Settings.Default.ACEConnectionString);
            _dbHelperGLOBAL = new DatabaseHelper("global");
            ThemeManager.UpdateTheme(this);
        }

        private void PlanZagrBrigLoadData()
        {
            string queryNomList = $"exec rzu_nzp {XIdBrig}, {GetUslFilter()}, {GetNZPFilter()}, {GetYearPlan()}, {GetMonthPlan()} ";
            dtNomList = _dbHelperACE.ExecuteQuery(queryNomList);
            this.gridControlPzNomList.Location = this.gridControlPzNomList.Location;
            this.gridControlPzNomList.Size = this.gridControlPzNomList.Size;
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
                                         nlAnnID = row.Field<int>("nlAnnID"),
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
                                         alAnnID = g.Key.nlAnnID,
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
                                      nlAnnID = nl.Field<int>("nlAnnID"),
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
            dtPzNomList = ConvertToDataTable(queryPzNomlList);
            bsPzNomList.DataSource = dtPzNomList;
            if (dtPzNomList.Rows.Count != 0)
            {
                dtPzNomList.DefaultView.Sort = "nlDataCdPl, nlNom ASC";
                GetPachListByNom(Convert.ToInt32(dtPzNomList.Rows[0]["nlNom"]));
            }
            bsPzNomList.DataSource = dtPzNomList;
        }

        public void GetMlDateBlock(DateTime _olDateEnd, int _olTab)
        {
            if (_olDateEnd != null && _olDateEnd != DateTime.MinValue)
            {
                string sqlQuery = $"SELECT dbo.getMLDateBlock(datefromparts(YEAR({_olDateEnd}), MONTH({_olDateEnd}), DAY({_olDateEnd})), {_olTab}) as dateBlock";
                var dt = _dbHelperACE.ExecuteQuery(sqlQuery);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.ctbMlDateBlock.Text = dt.Rows[0]["mlDateBlock"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка определения даты блокировки изменений в МЛ");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка определения даты блокировки изменений в МЛ");
                }
            }
            else
            {
                this.ctbMlDateBlock.Text = "";
            }
        }
        public static void DeleteRowsByCriteria(DataTable _dataTable, string[,] _delCriteria)
        {
            int _criteriaCount = _delCriteria.GetLength(0);
            for (int _row = _dataTable.Rows.Count - 1; _row >= 0; _row--)
            {
                DataRow row = _dataTable.Rows[_row];
                for (int i = 0; i < _criteriaCount; i++)
                {
                    string _dtColumn = _delCriteria[i, 0];
                    string _dtValue = _delCriteria[i, 1];
                    if (i == _criteriaCount - 1 && (string.Equals(row[_dtColumn].ToString(), _dtValue, StringComparison.OrdinalIgnoreCase)))
                    {
                        row.Delete();
                    }
                    else if ((i < _criteriaCount - 1 && !(string.Equals(row[_dtColumn].ToString(), _dtValue, StringComparison.OrdinalIgnoreCase))))
                    {
                        break;
                    }
                }
            }
            _dataTable.AcceptChanges();
        }
        public int[] GetNom(int _tbPachYear, int _tbPachNumber)
        {
            int[] result = { 0, 0 };
            string queryPachNom = $"select nom, nom_n from raskr_zeh_up where year(data_r) = {_tbPachYear} and n_pach = {_tbPachNumber} ";
            var dtPachNom = _dbHelperACE.ExecuteQuery(queryPachNom);
            if (dtPachNom != null && dtPachNom.Rows.Count != 0)
            {
                result[0] = Convert.ToInt32(dtPachNom.Rows[0]["nom"]);
                result[1] = Convert.ToInt32(dtPachNom.Rows[0]["nom_n"]);
            }
            return result;
        }
        private void FindNomRas(int _nlNom, int _nlNomN)
        {
            string _selectText = "";
            if (_nlNomN == 0)
            {
                _selectText = $"nlNom = {_nlNom}";
            }
            else
            {
                _selectText = $"nlNom = {_nlNom} and nlNomN = {_nlNomN}";
            }
            DataRow[] drNomListFoundRow = dtNomList.Select(_selectText);
            if (drNomListFoundRow.Length > 0)
            {
                DataRow _CurrRow = drNomListFoundRow[0]; // Take the first matching row (if there are multiple matches).
                string _nlArticul = _CurrRow["nlArticul"].ToString();
                string _nlMod = _CurrRow["nlMod"].ToString();
                int _nlAnnID = (int)_CurrRow["nlAnnID"];
                string _nlArticulK = _CurrRow["nlArticulK"].ToString();
                string _nlModK = _CurrRow["nlModK"].ToString();
                dtPzArticulList.PrimaryKey = new DataColumn[] { dtPzArticulList.Columns["alArticul"],
                                                                                        dtPzArticulList.Columns["alMod"],
                                                                                        dtPzArticulList.Columns["alAnnID"],
                                                                                        dtPzArticulList.Columns["alArticulK"],
                                                                                        dtPzArticulList.Columns["alModK"] };
                object[] compositeKeyValue = { _nlArticul, _nlMod, _nlAnnID, _nlArticulK, _nlModK };
                DataRow drPzArticulList = dtPzArticulList.Rows.Find(compositeKeyValue);

                gridViewPzArticulList.FocusedRowHandle = gridViewPzArticulList.LocateByValue("alRowNumber", drPzArticulList["alRowNumber"].ToString());

                if (gridViewPzNomList == null) return;
                gridViewPzNomList.FocusedRowHandle = gridViewPzNomList.LocateByValue("nlRowNumber", _CurrRow["nlRowNumber"].ToString());
                gridViewPzNomList.FocusedColumn = gridViewPzNomList.Columns["nlV"];
                gridViewPzNomList.ShowEditor();
            }
            else
            {
                if (_nlNomN == 0)
                {
                    MessageBox.Show($"Расчет {_nlNom} не найден. Измените параметры фильтра и повторите попытку");
                }
            }
        }

        public void GetBrigName(int _xIdBrig)
        {
            string queryBrigName = $"select brig, id_brig from brig where id_brig = {_xIdBrig} ";
            var dtBrigName = _dbHelperACE.ExecuteQuery(queryBrigName);
            lblBrigName.Text = dtBrigName.Rows[0]["brig"].ToString();
        }

        public void GetMonthList()
        {
            try
            {
                string queryMonthList = $"SELECT * FROM spr_month ";
                var dtMonthList = _dbHelperACE.ExecuteQuery(queryMonthList);
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

        public void GetPlanZagrTwoStatusList()
        {
            try
            {
                string queryPlanZagrTwoStatusList = $"SELECT pztsIDMast, pztsNameMast FROM planZagrTwoStatus GROUP BY pztsIDMast, pztsNameMast ORDER BY pztsIDMast, pztsNameMast\r\n ";
                var dtPlanZagrTwoStatusList = _dbHelperACE.ExecuteQuery(queryPlanZagrTwoStatusList);
                bsPlanZagrTwoStatusList.DataSource = dtPlanZagrTwoStatusList;
                cbPlanZagrTwoStatusList.DataSource = bsPlanZagrTwoStatusList;
                cbPlanZagrTwoStatusList.DisplayMember = "pztsNameMast";
                cbPlanZagrTwoStatusList.ValueMember = "pztsIDMast";
                cbPlanZagrTwoStatusList.SelectedIndex = -1;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void GetWorkersList()
        {
            try
            {
                var queryWorkersList = from row in dtPzOperList.AsEnumerable()
                                       where (row.Field<int>("olTab") != 0)
                                       group row by new
                                       {
                                           wlTab = row.Field<int>("olTab"),
                                           wlFio = row.Field<string>("olFio"),
                                       } into g
                                       select new
                                       {
                                           wlTab = g.Key.wlTab,
                                           wlFio = g.Key.wlFio
                                       };

                var dtWorkersList = ConvertToDataTable(queryWorkersList);
                if (dtWorkersList.Rows.Count > 0)
                {
                    bsWorkersList.DataSource = dtWorkersList;
                    cbWorkersList.DataSource = bsWorkersList;
                    cbWorkersList.DisplayMember = "wlFio";
                    cbWorkersList.ValueMember = "wlTab";
                    cbWorkersList.SelectedIndex = -1;

                }
                else
                {
                    bsWorkersList.DataSource = null;
                    cbWorkersList.Refresh();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void GetGroupOborudList()
        {
            try
            {
                var queryGroupOborudList = from row in dtPzOperList.AsEnumerable()
                                           where ((row.IsNull("olKoObAll") ? 0 : row.Field<int>("olKoObAll")) != 0)
                                           group row by new
                                           {
                                               golKoObAll = row.Field<int>("olKoObAll"),
                                               golOborudOb = row.Field<string>("olOborudOb"),
                                           } into g
                                           select new
                                           {
                                               golKoObAll = g.Key.golKoObAll,
                                               golOborudOb = g.Key.golOborudOb
                                           };

                var dtGroupOborudList = ConvertToDataTable(queryGroupOborudList);
                bsGroupOborudList.DataSource = dtGroupOborudList;
                cbGroupOborudList.DataSource = bsGroupOborudList;

                cbGroupOborudList.ValueMember = "golKoObAll";
                cbGroupOborudList.DisplayMember = "golOborudOb";
                cbGroupOborudList.SelectedIndex = -1;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void GetOborudList()
        {
            try
            {
                DataTable dtOborudList;
                if (cbGroupOborudList.SelectedIndex == -1)
                {
                    var queryOborudList = from row in dtPzOperList.AsEnumerable()
                                              //where (row.IsNull("nlDateCd") || row.Field<DateTime>("nlDateCd") == DateTime.MinValue)
                                              //where (Convert.ToInt32(row.Field<string>("olKodOb")) != 0)
                                          where (Convert.ToInt32((row.IsNull("olKodOb") ? "0" : row.Field<string>("olKodOb"))) != 0)
                                          group row by new
                                          {
                                              olKodOb = row.Field<string>("olKodOb"),
                                              olTextObS = row.Field<string>("olTextObS"),
                                              olKoObAll = row.Field<int>("olKoObAll"),
                                              olOborudOb = row.Field<string>("olOborudOb"),
                                              olOborud = row.Field<string>("olOborud"),
                                          } into g
                                          select new
                                          {
                                              olKodOb = g.Key.olKodOb,
                                              olTextObS = g.Key.olTextObS,
                                              olKoObAll = g.Key.olKoObAll,
                                              olOborudOb = g.Key.olOborudOb,
                                              olOborud = g.Key.olOborud
                                          };
                    dtOborudList = ConvertToDataTable(queryOborudList);
                }
                else
                {
                    //MessageBox.Show(cbGroupOborudList.SelectedValue.ToString());
                    //MessageBox.Show(cbGroupOborudList.SelectedIndex.ToString());
                    var queryOborudList = from row in dtPzOperList.AsEnumerable()
                                              //where (row.IsNull("olKoObAll") || row.Field<int>("olKoObAll") == Convert.ToInt32(cbGroupOborudList.SelectedValue))
                                          where ((row.IsNull("olKoObAll") ? 0 : row.Field<int>("olKoObAll")) == Convert.ToInt32(cbGroupOborudList.SelectedValue))
                                          group row by new
                                          {
                                              olKodOb = row.Field<string>("olKodOb"),
                                              olTextObS = row.Field<string>("olTextObS"),
                                              olKoObAll = row.Field<int>("olKoObAll"),
                                              olOborudOb = row.Field<string>("olOborudOb"),
                                              olOborud = row.Field<string>("olOborud"),
                                          } into g
                                          select new
                                          {
                                              olKodOb = g.Key.olKodOb,
                                              olTextObS = g.Key.olTextObS,
                                              olKoObAll = g.Key.olKoObAll,
                                              olOborudOb = g.Key.olOborudOb,
                                              olOborud = g.Key.olOborud
                                          };
                    dtOborudList = ConvertToDataTable(queryOborudList);
                }

                bsOborudList.DataSource = dtOborudList;
                cbOborudList.DataSource = bsOborudList;
                cbOborudList.DisplayMember = "olOborud";
                cbOborudList.ValueMember = "olKodOb";
                cbOborudList.SelectedIndex = -1;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public int GetPztStatus()
        {
            int _xPztStatus = 0;
            if (this.cbPlanZagrTwoStatusList.SelectedIndex != -1)
            {
                _xPztStatus = Convert.ToInt32(cbPlanZagrTwoStatusList.SelectedValue);
            }
            return _xPztStatus;
        }

        public void GetPachListByNom(int _nlNom)
        {
            if (_nlNom == 0)
            {
                tbPList.Text = "";
            }
            else
            {
                string queryPachList = $"select cast(string_agg(cast(n_pach as nvarchar) +' / ' + TRIM(razm) + ' / ' + cast(kol as nvarchar), char(13) + char(10)) as nvarchar(max)) as pList ";
                queryPachList += $"	from raskr_zeh_up ";
                queryPachList += $"	where nom = {_nlNom} ";
                var dtPachList = _dbHelperACE.ExecuteQuery(queryPachList);
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

        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
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
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
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
            else if (this.radioButton3.Checked == true)  // ВСЕ
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
            else if (this.radioButton6.Checked == true) // ВСЕ не отгруженные
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
        private void GetPztOperList(string _xmlString)
        {
            string queryPzOperList = $"exec planZagrTwo_view ''";
            var dtPzOperList = _dbHelperACE.ExecuteQuery(queryPzOperList);
            bsPzOperList.DataSource = dtPzOperList;
        }

        private string GetFilterOperList()
        {
            string _selectText = "";
            if (this.cbPlanZagrTwoStatusList.SelectedIndex != -1)
            {
                _selectText += _selectText.Trim().Length > 0 ? " and " : "";
                _selectText += $" olStatIDMast == {cbPlanZagrTwoStatusList.SelectedValue} ";
            }
            if (this.cbWorkersList.SelectedIndex != -1)
            {
                _selectText += _selectText.Trim().Length > 0 ? " and " : "";
                _selectText += $" olTab == {cbWorkersList.SelectedValue} ";
            }
            if (this.cbGroupOborudList.SelectedIndex != -1)
            {
                _selectText += _selectText.Trim().Length > 0 ? " and " : "";
                _selectText += $" olKoObAll == {cbGroupOborudList.SelectedValue} ";
            }
            if (this.cbOborudList.SelectedIndex != -1)
            {
                _selectText += _selectText.Trim().Length > 0 ? " and " : "";
                _selectText += $" olKodOb == {cbOborudList.SelectedValue} ";
            }
            return _selectText;
        }

        private void SetFilterOperList()
        {
            string _selectText = GetFilterOperList();
            ApplyParameterizedFilter(gridViewPzOperList, GetFilterOperList());
        }

        private void ApplyParameterizedFilter(GridView _gridView, string _filter)
        {
            _gridView.ActiveFilterString = _filter;
            //Console.WriteLine($"Applied filter: {_filter}");
        }

        public static string DataRowToXml3(DataRow _dataRow, string _row)
        {
            XElement root = new XElement(_row,
                from DataColumn column in _dataRow.Table.Columns
                select new XElement(column.ColumnName.ToLower(), _dataRow[column].ToString().Trim())
            );
            return root.ToString();
        }

        public static string DataTableToXml3(DataTable _dataTable, string _head, string _row, int _toLowerCase)
        {
            XDocument doc = new XDocument(
            new XElement(_head,
                from row in _dataTable.AsEnumerable()
                select new XElement(_row,
                   from column in _dataTable.Columns.Cast<DataColumn>()
                   select new XElement(_toLowerCase == 1 ? column.ColumnName.ToLower() : column.ColumnName, row[column])
                )
             )
            );

            try
            {
                return doc.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting to string: {ex.Message}");
                return null;
            }
        }
        private void PlanZagrBrig_Load(object sender, EventArgs e)
        {
            XIdBrig = 1;
            GetBrigName(XIdBrig);
            this.cbMonthList.SelectedIndex = -1;
            this.radioButton3.Checked = true;
            this.radioButton6.Checked = true;
            //this.gridColumn15.Visible = false;
            //this.gridColumn16.Visible = false;
            //this.gridColumn17.Visible = false;
            //this.gridColumn52.Visible = false;
            //this.gridColumn53.Visible = false;
            //this.gridColumn54.Visible = false;
            //this.gridColumn55.Visible = false;
            GetPlanZagrTwoStatusList();
            GetMonthList();
            GetPztOperList("");
            PlanZagrBrigLoadData();

        }

        private void gcPzNomList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewPzNomList.RowCount > 0)
            {
                GetPachListByNom(Convert.ToInt32(gridViewPzNomList.GetDataRow(gridViewPzNomList.FocusedRowHandle)["nlNom"]));
            }
            else
            {
                GetPachListByNom(0);
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
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
            this.simpleButton4_Click(sender, e);
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
            if (gridViewPzArticulList.RowCount > 0)
            {
                GetArticulNomList(gridViewPzArticulList.GetDataRow(gridViewPzArticulList.FocusedRowHandle)["alKodd7"].ToString(), gridViewPzArticulList.GetDataRow(gridViewPzArticulList.FocusedRowHandle)["alMod"].ToString());
            }
            else
            {
                GetPachListByNom(0);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton2.Checked)
            if (GetUslFilter() > 0 && GetNZPFilter() > 0)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton3.Checked)
            if (GetUslFilter() > 0 && GetNZPFilter() > 0)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton6.Checked)
            if (GetUslFilter() > 0 && GetNZPFilter() > 0)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton7.Checked)
            if (GetUslFilter() > 0 && GetNZPFilter() > 0)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton5.Checked)
            if (GetUslFilter() > 0 && GetNZPFilter() > 0)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton8.Checked)
            if (GetUslFilter() > 0 && GetNZPFilter() > 0)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton9.Checked)
            if (GetUslFilter() > 0 && GetNZPFilter() > 0)
            {
                PlanZagrBrigLoadData();
            }
        }

        private void tbNlNom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindNomRas(Convert.ToInt32(tbNlNom.Text), 0);
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }

        private void repositoryItemCheckEdit3_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void repositoryItemCheckEdit3_CheckedChanged(object sender, EventArgs e)
        {
            gridViewPzNomList.FocusedColumn = gridViewPzNomList.Columns["nlIzNakl"];
            gridViewPzNomList.FocusedColumn = gridViewPzNomList.Columns["nlV"];
            DataTable dtPzOperListToLoad = dtNomList.Clone();
            if (gridViewPzNomList.GetRowCellValue(gridViewPzNomList.FocusedRowHandle, gridViewPzNomList.FocusedColumn).ToString() == "1")
            {
                int _nlRowNumber = Convert.ToInt32(gridViewPzNomList.GetDataRow(gridViewPzNomList.FocusedRowHandle)["nlRowNumber"]);
                string filterExpression = $" nlRowNumber = {_nlRowNumber}";
                DataRow[] rowsToUpdate = dtNomList.Select(filterExpression);
                try
                {
                    foreach (DataRow row in rowsToUpdate)
                    {
                        row["nlV"] = 1;
                        dtPzOperListToLoad.ImportRow(row);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating row: {ex.Message}");
                }
                if (dtPzOperListToLoad.Rows.Count > 0)
                {
                    string _xmlParamString = DataTableToXml3(dtPzOperListToLoad, "CData", "nom_kod_list", 1);
                    string sqlQuery = $"exec planZagrTwo_view '{_xmlParamString}'";

                    if (dtPzOperList is null)
                    {
                        dtPzOperList = _dbHelperACE.ExecuteQuery(sqlQuery);
                    }
                    else
                    {
                        var dtPzOperListAdd = _dbHelperACE.ExecuteQuery(sqlQuery);
                        dtPzOperList.Merge(dtPzOperListAdd);
                    }
                    bsPzOperList.DataSource = dtPzOperList;
                }

            }
            else
            {
                string[,] DelCriteria = {
                    { "olNom", gridViewPzNomList.GetDataRow(gridViewPzNomList.FocusedRowHandle)["nlNom"].ToString()},
                    { "olNomN", gridViewPzNomList.GetDataRow(gridViewPzNomList.FocusedRowHandle)["nlNomN"].ToString()},
                    { "olAnnID", gridViewPzNomList.GetDataRow(gridViewPzNomList.FocusedRowHandle)["nlAnnID"].ToString()}
                };
                DeleteRowsByCriteria(dtPzOperList, DelCriteria);
                bsPzOperList.DataSource = dtPzOperList;
                //-----
                int _nlRowNumber = Convert.ToInt32(gridViewPzNomList.GetDataRow(gridViewPzNomList.FocusedRowHandle)["nlRowNumber"]);
                string filterExpression = $" nlRowNumber = {_nlRowNumber}";
                DataRow[] rowsToUpdate = dtNomList.Select(filterExpression);
                try
                {
                    foreach (DataRow row in rowsToUpdate)
                    {
                        row["nlV"] = 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating row: {ex.Message}");
                }
                //-----
            }
            GetWorkersList();
            GetGroupOborudList();
            GetOborudList();
            ApplyParameterizedFilter(gridViewPzOperList, "");
        }

        private void tbPachNumber_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbPachYear.Text.Length > 0 && tbPachNumber.Text.Length > 0)
                {
                    int[] _nom = GetNom(Convert.ToInt32(tbPachYear.Text), Convert.ToInt32(tbPachNumber.Text));
                    if (_nom[0] != 0)
                    {
                        FindNomRas(_nom[0], _nom[1]);
                    }
                    else
                    {
                        //Console.WriteLine("Совпадений не найдено");
                        MessageBox.Show($"Пачка {tbPachNumber.Text} / {tbPachYear.Text} не найдена. Проверьте параметры фильтра и поиска и повторите попытку");
                    }
                }
            }

        }

        private void tbPachYear_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.tbPachNumber_KeyDown(sender, e);
            }
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            gridViewPzArticulList.FocusedColumn = gridViewPzArticulList.Columns["alDataCdPl"];
            gridViewPzArticulList.FocusedColumn = gridViewPzArticulList.Columns["alV"];
            DataTable dtPzOperListToLoad = dtNomList.Clone();
            if (gridViewPzArticulList.GetRowCellValue(gridViewPzArticulList.FocusedRowHandle, "alV").ToString() == "1")
            {
                for (int rowHandle = 0; rowHandle < gridViewPzNomList.RowCount; rowHandle++)
                {
                    gridViewPzNomList.FocusedRowHandle = rowHandle;
                    if (gridViewPzNomList.GetRowCellValue(rowHandle, "nlV").ToString() != "1")
                    {
                        gridViewPzNomList.SetRowCellValue(rowHandle, "nlV", 1);
                        //repositoryItemCheckEdit3_CheckedChanged(sender, e);
                        int _nlRowNumber = Convert.ToInt32(gridViewPzNomList.GetDataRow(gridViewPzNomList.FocusedRowHandle)["nlRowNumber"]);
                        string filterExpression = $" nlRowNumber = {_nlRowNumber}";
                        DataRow[] rowsToUpdate = dtNomList.Select(filterExpression);
                        try
                        {
                            foreach (DataRow row in rowsToUpdate)
                            {
                                row["nlV"] = 1;
                                dtPzOperListToLoad.ImportRow(row);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error updating row: {ex.Message}");
                        }
                    }
                }
                if (dtPzOperListToLoad.Rows.Count > 0)
                {
                    string _xmlParamString = DataTableToXml3(dtPzOperListToLoad, "CData", "nom_kod_list", 1);
                    string sqlQuery = $"exec planZagrTwo_view '{_xmlParamString}'";

                    if (dtPzOperList is null)
                    {
                        dtPzOperList = _dbHelperACE.ExecuteQuery(sqlQuery);
                    }
                    else
                    {
                        var dtPzOperListAdd = _dbHelperACE.ExecuteQuery(sqlQuery);
                        dtPzOperList.Merge(dtPzOperListAdd);
                    }
                    bsPzOperList.DataSource = dtPzOperList;
                }
            }
            else
            {
                for (int rowHandle = 0; rowHandle < gridViewPzNomList.RowCount; rowHandle++)
                {
                    gridViewPzNomList.FocusedRowHandle = rowHandle;
                    if (gridViewPzNomList.GetRowCellValue(rowHandle, "nlV").ToString() != "0")
                    {
                        gridViewPzNomList.SetRowCellValue(rowHandle, "nlV", 0);
                        repositoryItemCheckEdit3_CheckedChanged(sender, e);
                    }
                }
            }
            GetWorkersList();
            GetGroupOborudList();
            GetOborudList();
            ApplyParameterizedFilter(gridViewPzOperList, "");

        }

        private void cbPlanZagrTwoStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPlanZagrTwoStatusList.SelectedIndex > -1 && cbPlanZagrTwoStatusList.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                SetFilterOperList();
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            cbPlanZagrTwoStatusList.SelectedIndex = -1;
            SetFilterOperList();
        }

        private void cbWorkersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbWorkersList.SelectedIndex > -1 && cbWorkersList.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                SetFilterOperList();
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            cbWorkersList.SelectedIndex = -1;
            SetFilterOperList();
        }

        private void cbGroupOborudList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGroupOborudList.SelectedIndex > -1 && cbGroupOborudList.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                GetOborudList();
                SetFilterOperList();
            }
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            cbGroupOborudList.SelectedIndex = -1;
            SetFilterOperList();
        }

        private void cbOborudList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOborudList.SelectedIndex > -1 && cbOborudList.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                SetFilterOperList();
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            cbOborudList.SelectedIndex = -1;
            SetFilterOperList();
        }

        private void gcPzOperList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewPzOperList.RowCount > 0)
            {
                //this.ctbMlDateBlock.Text = gwPzOperList.GetDataRow(gwPzOperList.FocusedRowHandle)["mlDateBlock"].ToString();
                int _olTab = Convert.ToInt32(gridViewPzOperList.GetDataRow(gridViewPzOperList.FocusedRowHandle)["olTab"] ?? "0");
                MessageBox.Show(gridViewPzOperList.GetDataRow(gridViewPzOperList.FocusedRowHandle)["olDateEnd"].ToString());
                //DateTime _olDateEnd = gwPzOperList.GetDataRow(gwPzOperList.FocusedRowHandle)["olDateEnd"] == null ? DateTime.MinValue : Convert.ToDateTime(gwPzOperList.GetDataRow(gwPzOperList.FocusedRowHandle)["olDateEnd"]);
                //DateTime? xDateTime = Convert.ToDateTime(gwPzOperList.GetDataRow(gwPzOperList.FocusedRowHandle)["olDateEnd"]);


                DateTime _olDateEnd = DateTime.MinValue;
                //if (gwPzOperList.GetDataRow(gwPzOperList.FocusedRowHandle)["olDateEnd"] != null)
                if (gridViewPzOperList.GetDataRow(gridViewPzOperList.FocusedRowHandle)["olDateEnd"].ToString().Trim().Length != 0)
                //if (xDateTime != null)
                {
                    _olDateEnd = Convert.ToDateTime(gridViewPzOperList.GetDataRow(gridViewPzOperList.FocusedRowHandle)["olDateEnd"]);
                }
                GetMlDateBlock(_olDateEnd, _olTab);
                this.ctbOlNameFull.Text = gridViewPzOperList.GetDataRow(gridViewPzOperList.FocusedRowHandle)["olNameFull"].ToString();
                //MessageBox.Show(gwPzOperList.GetDataRow(gwPzOperList.FocusedRowHandle)["mlDateBlock"].ToString());
                MessageBox.Show(gridViewPzOperList.GetDataRow(gridViewPzOperList.FocusedRowHandle)["olNameFull"].ToString());
            }
        }

        private void gcPzArticulList_Click(object sender, EventArgs e)
        {

        }

        private void customLabel11_Click(object sender, EventArgs e)
        {

        }

        private void customLabel10_Click(object sender, EventArgs e)
        {

        }

        private void customLabel9_Click(object sender, EventArgs e)
        {

        }
    }
}
