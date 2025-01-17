using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Customization;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace SewingProduction.form
{
    public partial class Fio : CustomForm
    {

        // Оснавная БД:
        string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        //string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        string tableString;
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        // если редактировали поле:
        bool flagRed = false;
        public Fio(string tableSQL, string rusNameTableSQL)
        {
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
            /*string query = $@"SELECT tab,fio,rab,ved,ftabn,ftabnsort,fgrd,data_p,datau,bday,tel_s,f_fvr_kod,tab1c,tab_sovm,
                                tel_r,tel_d,mast,okl,tab_new,po,
                                (SELECT sp_firms.name FROM sp_firms WHERE sp_firms.kod = fio.mast) AS firms_name,
                                (SELECT brig_object.name FROM brig_object WHERE brig_object.gr = fio.gr) AS BRIG_object_name,
                                (SELECT DISTINCT spbrig.podrname1c FROM spbrig WHERE spbrig.podrid1c = fio.podr_1c_id AND podrid1c LIKE '%ЭЙС%') AS podr1cname,
                                (SELECT DISTINCT spisok1c.inn FROM spisok1c WHERE TRY_CAST(spisok1c.tab1c AS INT) = TRY_CAST(fio.tab1c AS INT) AND orgName like '%ЭЙС%') AS spisok1c_inn,
                                (SELECT DISTINCT spisok1c.id FROM spisok1c WHERE TRY_CAST(spisok1c.tab1c AS INT) = TRY_CAST(fio.tab1c AS INT) AND orgName like '%ЭЙС%') AS spisok1c_id,
                                (SELECT DISTINCT spisok1c.orgName FROM spisok1c WHERE TRY_CAST(spisok1c.tab1c AS INT) = TRY_CAST(fio.tab1c AS INT) AND orgName like '%ЭЙС%') AS spisok1c_orgName,
                                (SELECT DISTINCT spisok1c.podrName FROM spisok1c WHERE TRY_CAST(spisok1c.tab1c AS INT) = TRY_CAST(fio.tab1c AS INT) AND orgName like '%ЭЙС%') AS spisok1c_podrName,
                                sovm,sdel,itr,dekret
                              FROM fio 
                                ORDER BY tab ASC";*/
            string query = $@"SELECT fio.tab,       fio.fio,       fio.rab,    fio.ved,       fio.ftabn,
                                     fio.ftabnsort, fio.fgrd,      fio.data_p, fio.datau,     fio.bday,
                                     fio.tel_s,     fio.f_fvr_kod, fio.tab1c,  fio.tab_sovm,  fio.tel_r,
                                     fio.tel_d,     fio.mast,      fio.okl,    fio.tab_new,   fio.po,
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

            DataTable dataTable = await LoadDataAsync(connectionString, query);
            fioList.DataSource = dataTable;
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

        private void customButtonSpDol_Click(object sender, EventArgs e)
        {
            openSprav("rab", "r_id,rab", "Справочник Должностей");
        }

        private void customButtonSpOrg_Click(object sender, EventArgs e)
        {
            openSprav("sp_firms", "kod,name,frm_1c_inn", "Справочник Организаций");
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

        private void customButtonShowDel_Click(object sender, EventArgs e)
        {
            if (gridViewFio != null)
            {
                string filter = "";
                if (customCheckBoxDel.Checked == true)
                    filter += "[datau] Is not Null";
                if (customCheckBoxDei.Checked == true)
                {
                    if (!string.IsNullOrEmpty(filter)) filter += " OR ";
                    filter += "[datau] Is Null";
                }
                if (!string.IsNullOrEmpty(filter)) gridViewFio.ActiveFilterString = filter;
            }
        }
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
        private void customButtonINN_Click(object sender, EventArgs e)
        {
            string INN = customTextBoxInn.Text;
            string filter = "";
            if (!string.IsNullOrEmpty(INN))
            {
                filter += $"[spisok1c_inn] Like '%{INN.Replace("'", "''")}%'";
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

        private void customButtonAdd_Click(object sender, EventArgs e)
        {
            editFio f = new editFio("АВТО","Добавление сотрудника");
            if (f.ShowDialog() == DialogResult.OK)
            {
                // Обновляем таблицу
                fioGrid_Load(sender, e);
            }
        }

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

    }
}
