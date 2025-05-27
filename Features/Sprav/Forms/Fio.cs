using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.DataAccess.Native.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using SewingProduction.Helpers;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.form
{
    public partial class Fio : CustomForm
    {
        private readonly FioDataService _fioDataService;
        string tableString;
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        // если редактировали поле:
        bool flagRed = false;
        string filter = "";
        public Fio(UserClass user,string tableSQL, string rusNameTableSQL) : base(user)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper("ace");
            _fioDataService = new FioDataService(dbHelper);
            ThemeManager.UpdateTheme(this);
            //Таблица fio:
            tableString = tableSQL;
            //Имя формы:
            this.Text = rusNameTableSQL;
        }
        public Fio()
        {
            InitializeComponent();
        }
        private void Fio_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Загрузка таблицы fio (сотрудников)
        /// </summary>
        private void fioGrid_Load(object sender, EventArgs e)
        {
            fioList.DataSource = _fioDataService.GetFioTable();
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
            editFio f = new editFio(_user, "АВТО", "Добавление сотрудника");
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

            editFio f = new editFio(_user, idFIO, "Редактирование сотрудника");
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

            customComboBoxPechVed.DataSource = _fioDataService.GetBrigVed();
            customComboBoxPechVed.DisplayMember = "vdid";
            customComboBoxPechVed.ValueMember = "brig";
        }
        private void customButtonOtcDol_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPagePechSHk;
            xtraTabControl1.Visible = true;
            xtraTabPagePechSHk.PageVisible = false;
            xtraTabPageOtchDolg.PageVisible = true;
            xtraTabControl1.SelectedTabPageIndex = 1;
            radioGroup1_SelectedIndexChanged(sender, e);
        }
        private void xtraTabPageX_Paint(object sender, PaintEventArgs e)
        {
            xtraTabControl1.Visible = false;
            xtraTabPagePechSHk.PageVisible = false;
            xtraTabPageOtchDolg.PageVisible = false;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroup1.SelectedIndex)
            {
                default:
                    customLabelOtch.Text = "Организация";
                    break;
                case 1:
                    customLabelOtch.Text = "Подразделение 1С";
                    break;
                case 2:
                    customLabelOtch.Text = "Табель ШП";
                    break;
            }
            if (!string.IsNullOrEmpty(customLabelOtch.Text))
            {
                customComboBoxOtch.DataSource = _fioDataService.GetFioDolgnPodr1c(customLabelOtch.Text);
                customComboBoxOtch.DisplayMember = "value";
                customComboBoxOtch.ValueMember = "value";
            }
        }
        /// <summary>
        /// КНОПКА отчет "Совместители"
        /// </summary>
        private void customButtonSov_Click(object sender, EventArgs e)
        {
            for_reports_excel.DataSource = _fioDataService.GetFioAndSpFirmsAndTabN();
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

        /// <summary>
        /// КНОПКА отчет "Сформировать"
        /// </summary>
        private void customButtonSforming_Click(object sender, EventArgs e)
        {
            for_reports_excel.DataSource = _fioDataService.GetFioDolgnPogr1cWhere(customLabelOtch.Text, customComboBoxOtch.Text, customCheckBoxOsnTab.Checked);
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

        #region Совместители XML
        /// <summary>
        /// Добавление записи сотрудника-совместителя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButtonAddSov_Click(object sender, EventArgs e)
        {
            try
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
                _fioDataService.UpdateFioSovm(tab);

                // 3.  Получение нового табельного номера
                int newTabSovm = _fioDataService.GetFioNewTab();

                // 4. Получение данных для XML
                System.Data.DataTable sqlFio = _fioDataService.GetFioForXML(tab);

                // 5.  Обновление записи
                DataRow newRow = sqlFio.Rows[0];
                newRow["tab"] = newTabSovm;
                newRow["komp_name"] = System.Environment.MachineName;
                newRow["tab_sovm"] = tab;
                newRow["sovm"] = 1;
                newRow["po"] = 0;
                newRow["kont"] = 0;
                newRow["grup"] = "";

                // 6. Добавление в XML
                string xmlFio = DataTableToXml3(sqlFio, "VFPData", "row");

                // 7. Вызов хранимой процедуры
                _fioDataService.SetFioInsertTableXml(xmlFio);

                // 8. Обновляем таблицу
                fioGrid_Load(sender, e);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error XML: {ex.Message}");
            }
        }
        public static string DataTableToXml3(System.Data.DataTable _dataTable, string _head, string _row)
        {
            XDocument doc = new XDocument(
                new XElement(_head,
                    from row in _dataTable.AsEnumerable()
                    select new XElement(_row,
                        from column in _dataTable.Columns.Cast<System.Data.DataColumn>()
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
        #endregion
        #region Справочники
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
        /// <summary>
        /// Функция для открытия справочников:
        /// </summary>
        /// <param name="nameSprav">Имя справочника</param>
        /// <param name="columns">Колонки</param>
        /// <param name="nameSpravRus">Имя справочника на русском</param>
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
        #endregion
    }
    public class FioDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public FioDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        #region fio
        public System.Data.DataTable GetFioTable()
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
            return _dbHelper.ExecuteQuery(query);
        }
        public System.Data.DataTable GetBrigVed()
        {
            string query = "SELECT * FROM brig_ved";
            return _dbHelper.ExecuteQuery(query);
        }
        public System.Data.DataTable GetFioDolgnPodr1c(string selectValue)
        {
            string query;
            switch (selectValue)
            {
                case "Организация":
                    query = "SELECT name AS 'value' FROM fio_dolgn_podr1c  WHERE ISNULL(name,'0') <> '0' group by name order by name";
                    break;
                case "Подразделение 1С":
                    query = "SELECT podrname1c AS 'value' FROM fio_dolgn_podr1c  WHERE ISNULL(podrname1c,'0') <> '0' group by podrname1c order by podrname1c";
                    break;
                case "Табель ШП":
                    query = "SELECT naimen AS 'value' FROM fio_dolgn_podr1c  WHERE ISNULL(naimen,'0') <> '0' group by naimen order by naimen";
                    break;
                default:
                    query = "SELECT * FROM fio_dolgn_podr1c";
                    break;
            }
            return _dbHelper.ExecuteQuery(query);
        }
        public System.Data.DataTable GetFioAndSpFirmsAndTabN()
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
            return _dbHelper.ExecuteQuery(query);
        }
        public System.Data.DataTable GetFioDolgnPogr1cWhere(string selectValue, string selectOtch, bool checkBoxOsnTab)
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
            if (checkBoxOsnTab)
                query += $" sovm = 0 and ";
            switch (selectValue)
            {
                case "Организация":
                    query += $" name = '{selectOtch}' ";
                    break;
                case "Подразделение 1С":
                    query += $" podrname1c = '{selectOtch}' ";
                    break;
                case "Табель ШП":
                    query += $" naimen = '{selectOtch}' ";
                    break;
                default:
                    query = " 1=1 ";
                    break;
            }
            query += $" order by name,podrname1c,naimen,fio,tab";
            return _dbHelper.ExecuteQuery(query);
        }
        public void UpdateFioSovm(string tab)
        {
            string query = "UPDATE fio SET tab_sovm = @tab_sovm WHERE fio.tab=@tab";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@tab_sovm", tab }, { "@tab", tab } });
        }
        /// <summary>
        ///  Получение нового табельного номера
        /// </summary>
        /// <param name="sqlFio">Максимальный табельный</param>
        /// <returns></returns>
        public int GetFioNewTab()
        {
            string query = "SELECT ISNULL(MAX(tab), 0) + 1 AS newTab FROM fio";
            System.Data.DataTable sqlFio = _dbHelper.ExecuteQuery(query);
            return Convert.ToInt32(sqlFio.Rows[0]["newTab"]);
        }
        public System.Data.DataTable GetFioForXML(string tab)
        {
            string query = "SELECT * FROM fio WHERE fio.tab=@tab";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        public void SetFioInsertTableXml(string xmlFio)
        {
            string query = "exec Insert_table_xml @xmlp = @xmlFio, @nameTable = 'fio'";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@xmlFio", xmlFio } });
        }
        #endregion
    }
}