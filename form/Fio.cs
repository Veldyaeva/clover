using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace SewingProduction.form
{
    public partial class Fio : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        // Оснавная БД:
        string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        //string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        string tableString;
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        // если редактировали поле:
        bool flagRed = false;
        string filter = "";
        public Fio(string tableSQL, string rusNameTableSQL)
        {
            _dbHelper = new DatabaseHelper("ace");//Properties.Settings.Default.ACEConnectionString);
            
            InitializeComponent(); 
            //Таблица fio:
            tableString = tableSQL;
            //Имя формы:
            this.Text = rusNameTableSQL;
        }

        private void Fio_Load(object sender, EventArgs e)
        {
        }
        private async Task<DataTable> LoadDataAsync(string connectionString, string query)
        {
            return await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            });
        }
        private async void fioGrid_Load(object sender, EventArgs e)
        {

            string query = $@"SELECT fio.tab,       fio.fio,       fio.rab,    fio.ved,       fio.ftabn,
                                     fio.ftabnsort, fio.fgrd,      fio.data_p, fio.datau,     fio.bday,
                                     fio.tel_s,     fio.f_fvr_kod, fio.tab1c,  fio.tab_sovm,  fio.tel_r,
                                     fio.tel_d,     fio.mast,      fio.okl,    fio.tab_new,   
                                     fio.sovm,      fio.sdel,      fio.itr,    fio.dekret,
                                     sp_firms.name AS firms_name,
                                     sp_firms.frm_1c_inn,
                                     brig_object.name AS BRIG_object_name,
                                     spbrig.podrname1c AS podr1cname,
                                     spisok1c.id AS spisok1c_id,
                                     spisok1c.inn AS spisok1c_inn,
                                     spisok1c.orgName AS spisok1c_orgName,
                                     spisok1c.podrName AS spisok1c_podrName
                                FROM fio
                                LEFT JOIN sp_firms ON sp_firms.kod = fio.mast
                                LEFT JOIN brig_object ON brig_object.gr = fio.gr
                                LEFT JOIN spbrig ON spbrig.podrid1c = fio.podr_1c_id AND spbrig.podrid1c LIKE '%ЭЙС%'
                                LEFT JOIN spisok1c ON TRY_CAST(REPLACE(spisok1c.tab1c, ' ', '') AS INT) = CAST(fio.tab1c AS INT) AND spisok1c.orgcode = sp_firms.frm_1c_inn
                                ORDER BY fio.tab ASC";
            //fioList.DataSource = ShowRelatedData("ace_test", query);
            var dt = _dbHelper.ExecuteQuery(query);
            fioList.DataSource = dt;
            //DataTable dataTable = await LoadDataAsync(connectionString, query);
            //fioList.DataSource = dataTable;
            // Фильтр для уволенных:
            customCheckBoxDei.Checked = true;
            // Чекбоксы в гриде:
            if (gridViewFio != null)
            {
                // Создаем экземпляр CheckEdit
                RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit
                {
                    ValueChecked = 1, 
                    ValueUnchecked = 0
                };
                // Назначаем его столбцам
                gridViewFio.Columns["sovm"].ColumnEdit = checkEdit;
                gridViewFio.Columns["sdel"].ColumnEdit = checkEdit;
                gridViewFio.Columns["itr"].ColumnEdit = checkEdit;
                gridViewFio.Columns["dekret"].ColumnEdit = checkEdit;
            }
            if (flagRed)
            {
                gridViewFio.TopRowIndex = topRowIndex;
                gridViewFio.FocusedRowHandle = currentRowIndex;
                flagRed = false; 
            }
            else
            {
            // Переходим к последней строке
            gridViewFio.TopRowIndex = gridViewFio.RowCount - 1;
            gridViewFio.FocusedRowHandle = gridViewFio.RowCount - 1; 
            }
        }
        // Справочники
        private void customButtonSpDol_Click(object sender, EventArgs e)
        {
            openSprav("rab", "r_id,rab", "Справочник Должностей");
        }

        private void customButtonSpOrg_Click(object sender, EventArgs e)
        {
            openSprav("sp_firms", "kod,name,frm_1c_inn", "Справочник Организаций");
        }
        private void customButtonPdrSP_Click(object sender, EventArgs e)
        {
            openSprav("brig", "n_brig,nn,brig,ip_proizv,podrid1c,podrname1c", "Справочник Организаций");
        }
        private void customButtonSpDol1C_Click(object sender, EventArgs e)
        {
            openSprav("dolg1c", "id,name,inn", "Должности 1С");
        }
        private void customButtonSpOrg1C_Click(object sender, EventArgs e)
        {
            openSprav("podr1c", "id,name,inn", "Подразделения 1С");
        }
        private void customButtonSpVed_Click(object sender, EventArgs e)
        {
            openSprav("brig_ved", "vdID,brig,object,name,ip_proizv", "Ведомости");
        }
        // Функция для открытия справочников:
        private void openSprav(string nameSprav, string columns, string nameSpravRus)
        {
            foreach (Form child in this.MdiParent.MdiChildren)
            {
                if (child is SpravForAll && child.Text.ToString() == nameSpravRus)
                {
                    // Если форма уже открыта, переключаем на нее
                    child.BringToFront();
                    return;
                }
            }
            // Если форма не открыта, создаем новую
            SpravForAll f = new SpravForAll(nameSprav, columns, nameSpravRus);
            f.MdiParent = this.MdiParent;
            f.Show();
        }
        // Фильтры:
        private void customCheckBoxDel_CheckedChanged(object sender, EventArgs e)
        {
            if (customCheckBoxDel.Checked == true)
            {
                customCheckBoxDei.Checked = false;
            }
            gridViewFiltered();
        }
        private void customCheckBoxDei_CheckedChanged(object sender, EventArgs e)
        {
            if (customCheckBoxDei.Checked == true)
            {
                customCheckBoxDel.Checked = false;
            }
            gridViewFiltered();
        }
        private void customCheckBoxDekret_CheckedChanged(object sender, EventArgs e)
        {
            gridViewFiltered();
        }
        private void gridViewFiltered()
        {
            filter = "";
            if (customCheckBoxDel.Checked == true)
            {
                filter += "[datau] Is not Null";
            }
            if (customCheckBoxDei.Checked == true)
            {
                filter += "[datau] Is Null";
            }
            if (customCheckBoxDekret.Checked == true)
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += "([dekret] > 0)";
            }
            gridViewFio.ActiveFilterString = filter;

        }
        // Получение ИНН из грида в текстбокс:
        private void gridViewFio_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            innGetFocusedRowCellValue();
        }
        private void fioGrid_KeyUp(object sender, KeyEventArgs e)
        {
            innGetFocusedRowCellValue();
        }
        private void innGetFocusedRowCellValue()
        {
            string INN = customTextBoxInn.Text;
            GridColumn innColumn = gridViewFio.Columns["spisok1c_inn"];
            //Проверяем что колонка существует
            if (innColumn != null && (INN.Length == 12 || INN == ""))
            {
                // Проверяем, что есть строки в GridView
                if (gridViewFio.RowCount > 0)
                {
                    // Получаем значение ячейки
                    object innValue = gridViewFio.GetFocusedRowCellValue(gridViewFio.Columns["spisok1c_inn"]);
                    // Проверяем значение на null и  приводим к строке
                    customTextBoxInn.Text = innValue?.ToString() ?? "";
                }
                else
                {
                    customTextBoxInn.Text = "";
                }

            }
        }
        // КНОПКА "Найти по ИНН"
        private void customButtonINN_Click(object sender, EventArgs e)
        {
            string INN = customTextBoxInn.Text;
            if (!string.IsNullOrEmpty(INN))
            {
                filter = $"[spisok1c_inn] Like '%{INN.Replace("'", "''")}%'";
            }
            else gridViewFio.ActiveFilter.Clear();
            //string osnTab = gridViewFio.GetFocusedRowCellValue(gridViewFio.Columns["tab_sovm"]).ToString();
            //if (osnTab != "0")
            //{
            //    if (!string.IsNullOrEmpty(filter)) filter += " OR ";
            //    filter += $"([tab_sovm] = {osnTab})";
            //}
            //
            if (!string.IsNullOrEmpty(filter)) gridViewFio.ActiveFilterString = filter;
            
        }
        // КНОПКА "Добавить"
        private void customButtonAdd_Click(object sender, EventArgs e)
        {
            editFio f = new editFio("АВТО","Добавление сотрудника");
            if (f.ShowDialog() == DialogResult.OK)
            {
                // Обновляем таблицу
                fioGrid_Load(sender, e);
            }
        }
        // КНОПКА "Редактировать"
        private void customButtonRed_Click(object sender, EventArgs e)
        {
            string idFIO = gridViewFio.GetFocusedRowCellValue(gridViewFio.Columns["tab"]).ToString();
            if (Convert.ToInt32(idFIO) < 1)
            {
                MessageBox.Show("Табельный не найден или не выбран");
                return;
            }
            string computerName = Environment.MachineName;

            editFio f = new editFio(idFIO, "Редактирование сотрудника");
            if (f.ShowDialog() == DialogResult.OK)
            {
                flagRed = true;
                topRowIndex = gridViewFio.TopRowIndex;
                currentRowIndex = gridViewFio.FocusedRowHandle;
                fioGrid_Load(sender, e);
            }

        }
        // КНОПКИ "Печать ШК" И "Отчет по должности"
        private void customButtonPchShk_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPagePechSHk;
            xtraTabControl1.Visible = true;
            xtraTabPageOtchDolg.PageVisible = false;
            xtraTabPagePechSHk.PageVisible = true;
            xtraTabControl1.SelectedTabPageIndex = 0;


            string query = $@"select * from brig_ved";
            ShowRelatedComboBox(customComboBoxPechVed, "ace", query, "vdid", "brig");
        }
        private void customButtonOtcDol_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPagePechSHk;
            xtraTabControl1.Visible = true;
            xtraTabPagePechSHk.PageVisible = false;
            xtraTabPageOtchDolg.PageVisible = true;
            xtraTabControl1.SelectedTabPageIndex = 1;
            radioGroup1_SelectedIndexChanged(sender,e);
        }
        private void xtraTabPageX_Paint(object sender, PaintEventArgs e)
        {
            xtraTabControl1.Visible = false;
            xtraTabPagePechSHk.PageVisible = false;
            xtraTabPageOtchDolg.PageVisible = false;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            switch (radioGroup1.SelectedIndex)
            {
                default:
                    customLabelOtch.Text = " Организация ";
                    query = "select name as value from fio_dolgn_podr1c where ISNULL(name,'0') <> '0' group by name order by name";
                    break;
                case 1:
                    customLabelOtch.Text = " Подразделение 1С ";
                    query = "select podrname1c as value from fio_dolgn_podr1c  where ISNULL(podrname1c,'0') <> '0' group by podrname1c order by podrname1c";
                    break;
                case 2:
                    customLabelOtch.Text = " Табель ШП ";
                    query = "select naimen as value from fio_dolgn_podr1c where ISNULL(naimen,'0') <> '0' group by naimen order by naimen";
                    break;
            }
            if (!string.IsNullOrEmpty(query))
            {
                ShowRelatedComboBox(customComboBoxOtch, "ace", query, "value", "value");
            }
        }

        protected void ShowRelatedComboBox(CustomComboBox comboBox, string _serv, string query, string displayMember, string valueMember)
        {
            try
            {
                //DataTable dataTable = ShowRelatedData(_serv, query);
                DataTable dataTable = _dbHelper.ExecuteQuery(query);
                if (dataTable != null)
                {
                    comboBox.DataSource = dataTable;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                }
                else
                {
                    comboBox.DataSource = null;
                    comboBox.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании ComboBox: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // КНОПКА "Совместители"
        private void customButtonSov_Click(object sender, EventArgs e)
        {
            string query = $@"SELECT tss.tab_sovm,
                            CASE 
                                WHEN (minDataU IS NOT NULL AND maxDataU IS NOT NULL) THEN 'oaieai' 
                                ELSE SPACE(6)
                            END AS status,
                            f.tab, f.fio, f.rab, f.sovm,
                            f.datau, f.itr, tn.naimen, frm.name
                        FROM (
                            SELECT tab_sovm, MIN(datau) as minDataU, MAX(datau) as maxDataU
                            FROM fio
                            WHERE tab_sovm <> 0
                            GROUP BY tab_sovm
                            HAVING COUNT(*) > 1
                        ) AS tss
                    LEFT JOIN fio f ON tss.tab_sovm = f.tab_sovm
                    LEFT JOIN sp_firms frm ON f.mast = frm.kod
                    LEFT JOIN tab_n tn ON f.ftabn = tn.tnID
                    ORDER BY tss.tab_sovm, f.sovm, f.tab";
            //for_reports_excel.DataSource = ShowRelatedData("ace", query);
            for_reports_excel.DataSource = _dbHelper.ExecuteQuery(query);
            if (for_reports_excel.DataSource == null) { return; }

            // Генерируем отчёт
            ExcelReportGenerator generator = new ExcelReportGenerator();
            try
            {
                generator.GenerateExcel("sovm_list", for_reports_excel, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при экспорте: {ex.Message}");
                MessageBox.Show($"Ошибка при создании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // КНОПКА "Сформировать"
        private void customButtonSforming_Click(object sender, EventArgs e)
        {
            string query = $@"SELECT 
                                    fio AS 'Фио',
                                    tab AS 'Таб. №',
                                    rab AS 'Должность',
                                    CASE 
                                        WHEN fio_dolgn_podr1c.sovm = 0 THEN 'Основной'
                                        ELSE ''
                                    END AS 'Основное место работы',
                                    CASE 
                                        WHEN fio_dolgn_podr1c.sdel = 1 THEN 'Сделка'
                                        ELSE 'ИТР'
                                    END AS 'ИТР/Сделка' 
                                FROM  fio_dolgn_podr1c where";
            string value = customComboBoxOtch.Text;
            if (customCheckBoxOsnTab.Checked)
                query += $" sovm = 0 and ";
            switch (radioGroup1.SelectedIndex)
            {
                default:
                    query += $" name = '{value}' ";
                    break;
                case 1:
                    query += $" podrname1c = '{value}' ";
                    break;
                case 2:
                    query += $" naimen = '{value}' ";
                    break;
            }
            query += $" order by name,podrname1c,naimen,fio,tab";
            //for_reports_excel.DataSource = ShowRelatedData("ace", query);
            for_reports_excel.DataSource = _dbHelper.ExecuteQuery(query);
            if (for_reports_excel.DataSource == null) { return; }
            // Генерируем отчёт
            ExcelReportGenerator generator = new ExcelReportGenerator();
            try
            {
                generator.GenerateExcel("Отчет по должности", for_reports_excel, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при экспорте: {ex.Message}");
                MessageBox.Show($"Ошибка при создании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void customButtonAddSov_Click(object sender, EventArgs e)
        {
            // 1. Получение данных и проверка на совместительство
            string sovm = gridViewFio.GetFocusedRowCellValue(gridViewFio.Columns["sovm"]).ToString();
            if (Convert.ToInt32(sovm) == 1)
            {
                MessageBox.Show("Совместительство может быть оформлено только для основного табельного номера!");
                return;
            }
            // 2. SQL Update для текущего пользователя
            string tab = gridViewFio.GetFocusedRowCellValue(gridViewFio.Columns["tab"]).ToString();
            string query = $"UPDATE fio SET tab_sovm = {tab} WHERE fio.tab={tab}";
            //ExecuteNonQuery("ace", query, new SqlParameter("@tab_sovm", tab), new SqlParameter("@tab", tab));
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@tab_sovm", tab }, { "@tab", tab } });

            // 3.  Получение нового табельного номера
            query = "SELECT ISNULL(MAX(tab), 0) + 1 AS newTab FROM fio";
            //DataTable sqlFio = ShowRelatedData("ace", query);
            DataTable sqlFio = _dbHelper.ExecuteQuery(query);
            int newTabSovm = Convert.ToInt32(sqlFio.Rows[0]["newTab"]);

            // 4. Получение данных для XML
            query = $"SELECT * FROM fio WHERE fio.tab={tab}";
            //sqlFio = ShowRelatedData("ace", query);
            sqlFio = _dbHelper.ExecuteQuery(query);

            // 5.  Обновление записи
            DataRow newRow = sqlFio.Rows[0];
            newRow["tab"] = newTabSovm;
            newRow["komp_name"] = System.Environment.MachineName;
            newRow["tab_sovm"] = tab;
            newRow["sovm"] = 1;
            newRow["po"] = 0;
            newRow["kont"] = 0;
            newRow["grup"] = "";

            // 6. Добавление в XML и вызов хранимой процедуры
            string xmlFio = DataTableToXml3(sqlFio, "VFPData", "row");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedureQuery = "exec Insert_table_xml @xmlp = @xmlFio, @nameTable = 'fio'";
                using (SqlCommand commandProc = new SqlCommand(storedProcedureQuery, connection))
                {
                    commandProc.Parameters.AddWithValue("@xmlFio", xmlFio);
                    connection.Open();
                    commandProc.ExecuteNonQuery();
                    connection.Close();
                }
            }

            // 7. Обновляем таблицу
            fioGrid_Load(sender, e);
        }
        public static string DataTableToXml3(DataTable _dataTable, string _head, string _row)
        {
            XDocument doc = new XDocument(
                new XElement(_head,
                    from row in _dataTable.AsEnumerable()
                    select new XElement(_row,
                        from column in _dataTable.Columns.Cast<DataColumn>()
                        where column.ColumnName != "f_id" // Исключаем столбец "f_id"
                        let columnName = column.ColumnName
                        let columnValue = row[column]
                        where columnValue != null && columnValue != DBNull.Value
                        let formattedValue = columnValue is DateTime dt
                            ? dt.ToString("yyyy-MM-ddTHH:mm:ss")//формат даты
                            : columnValue.ToString()
                        where !string.IsNullOrEmpty(formattedValue)
                        select new XAttribute(columnName, formattedValue)
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

    }
}
