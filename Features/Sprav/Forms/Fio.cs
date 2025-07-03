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
        private readonly Features.Sprav.FioDataService _fioDataService;
        string tableString;
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        // если редактировали поле:
        bool flagRed = false;
        string filter = "";
        public Fio(UserClass user, string tableSQL, string rusNameTableSQL) : base(user)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper("ace");
            _fioDataService = new Features.Sprav.FioDataService(dbHelper);
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

        private void customButtonPech1_Click(object sender, EventArgs e)
        {
            var pbForm = new PrintBarcode();
            pbForm.Pech1(customComboBoxPechVed.Text, customTextBoxPechSHK1.Text);
            //pbForm.Show();
        }

        private void customButtonPech2_Click(object sender, EventArgs e)
        {
            var pbForm = new PrintBarcode();
            pbForm.Pech2(customTextBoxPechTab.Text.Trim(), customTextBoxPechSHK2.Text.Trim());
            //pbForm.Show();

        }
    }

}