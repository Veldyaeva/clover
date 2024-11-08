
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
using DevExpress.CodeParser;
using DevExpress.DataAccess.Native.Data;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraTab;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static DevExpress.Utils.Menu.DXMenuItemPainter;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace SewingProduction.form
{
    public partial class SpravOborud : Form
    {
        // Оснавная БД:
        //string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        public SpravOborud()
        {
            InitializeComponent();
        }

        private void SpravOborud_Load(object sender, EventArgs e)
        {

        }

        private void oborudGrid_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Открываем подключение
                connection.Open();
                //Текст запроса:
                string queryOborudList = $"SELECT kod_ob ,oborud_shv.text_ob,text_ob_s," +
                                        $"oborud_shv_ob.text_ob AS text_ob_tip,pokaz,noski,spec_ob,nastav," +
                                        $"vid_ob,spOborudMachine.name,pokaz_sp,socksserv,matrix_class.caption AS idClass " +
                                        $" FROM oborud_shv " +
                                        $" LEFT JOIN oborud_shv_ob ON oborud_shv_ob.ko_ob_all = oborud_shv.ko_ob_all " +
                                        $" LEFT JOIN spOborudMachine ON spOborudMachine.miniName = oborud_shv.no_spec" +
                                        $" LEFT JOIN matrix_class ON matrix_class.id_class = oborud_shv.id_class";
                //используя подключение отправляем запрос БД:
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryOborudList, connection);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableOborudList = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableOborudList);
                //Закгрузка в таблицу грида:
                oborudList.DataSource = tableOborudList;
                // Получаем доступ к GridView
                GridView gridView = oborudGrid.MainView as GridView;
                //Запрет на редактирование
                gridView.OptionsBehavior.Editable = false;
                // Если он открыт
                if (gridView != null)
                {
                    // Создаем экземпляр CheckEdit
                    RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit
                    {
                        ValueChecked = 1,   // Значение для отмеченной галочки
                        ValueUnchecked = 0   // Значение для неотмеченной галочки
                    };
                    // Назначаем его столбцам
                    gridView.Columns["pokaz"].ColumnEdit = checkEdit;
                    gridView.Columns["noski"].ColumnEdit = checkEdit;
                    gridView.Columns["spec_ob"].ColumnEdit = checkEdit;
                    gridView.Columns["vid_ob"].ColumnEdit = checkEdit;
                    gridView.Columns["pokaz_sp"].ColumnEdit = checkEdit;
                    gridView.Columns["socksserv"].ColumnEdit = checkEdit;
                    //gridView.Columns["pokaz"].OptionsColumn.AllowEdit = false; // Запрещаем редактирование

                    gridView.OptionsView.ShowGroupPanel = false; // Панель группировки отображается
                    gridView.GroupPanelText = ""; // Текст
                    gridView.OptionsFind.AlwaysVisible = true; // Всегда показывать панель поиска
                    
 
                }
                // Отображаем количество записей+1 в textBoxAddKod
                //textBoxAddKod.Text = (tableOborudList.Rows.Count + 1).ToString();

            }

        }

        //процедура загрузки таблицы oborud_shv_ob / spOborudMachine в комбобоксы Вид/Спец-ть (добавить/редактировать):
        public void comboTableItems(System.Windows.Forms.ComboBox comboBoxTableOborudShvOb,
                                    System.Windows.Forms.ComboBox comboBoxTableSpOborudMachine,
                                    System.Windows.Forms.ComboBox comboBoxTableMatrixClass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Открываем подключение
                connection.Open();

                //Текст запроса для oborud_shv_ob:
                string queryOborudAllList = $"SELECT * FROM oborud_shv_ob ORDER by ko_ob_all ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryOborudAllList, connection);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableOborudShvOb = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableOborudShvOb);
                comboBoxTableOborudShvOb.Items.Clear();
                //Загрузка в комбобокс:
                foreach (DataRow row in tableOborudShvOb.Rows)
                {
                    comboBoxTableOborudShvOb.Items.Add(row[1].ToString());
                }

                //Текст запроса spOborudMachine:
                string queryMachineAllList = $"SELECT * FROM spOborudMachine";
                dataAdapter = new SqlDataAdapter(queryMachineAllList, connection);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableMachineShvOb = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableMachineShvOb);
                comboBoxTableSpOborudMachine.Items.Clear();
                //Загрузка в комбобокс:
                foreach (DataRow row in tableMachineShvOb.Rows)
                {
                    comboBoxTableSpOborudMachine.Items.Add(row[1].ToString());
                }

                //Текст запроса MatrixClass:
                string queryMatrixClassAllList = $"SELECT id_class,caption FROM matrix_class";
                dataAdapter = new SqlDataAdapter(queryMatrixClassAllList, connection);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableMatrixClass = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableMatrixClass);
                comboBoxTableMatrixClass.Items.Clear();
                //Загрузка в комбобокс:
                foreach (DataRow row in tableMatrixClass.Rows)
                {
                    comboBoxTableMatrixClass.Items.Add(row[1].ToString());
                }
            }
        }

        private bool GetCheckBoxValue(string columnName, GridView gridViewGet)
        {
            // Получаем текущее выделенное значение в указанной ячейке столбца
            object value = gridViewGet.GetFocusedRowCellValue(columnName);
            // Проверяем, если значение равно 1 (отмеченная галочка)
            return value != null && value.Equals(1); // Вернуть true, если галочка отмечена, иначе false
        }
        private void simpleButtonRed_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = false;
            AddTab.TabPages[1].PageVisible = true;
            // Получаем доступ к GridView
            GridView gridView = oborudGrid.MainView as GridView;
            // Получаем текущую выделенную строку в текстбокси и др
            textBoxRedKod.Text = gridView.GetFocusedRowCellValue("kod_ob").ToString();
            textBoxRedName.Text = gridView.GetFocusedRowCellValue("text_ob").ToString();
            textBoxRedSokrName.Text = gridView.GetFocusedRowCellValue("text_ob_s").ToString();
            // Заполняем комбобоксы:
            comboTableItems(comboBoxRedVid, comboBoxRedSpec, comboBoxRedClass);
            comboBoxRedVid.Text = gridView.GetFocusedRowCellValue("text_ob_tip").ToString();
            comboBoxRedSpec.Text = gridView.GetFocusedRowCellValue("name").ToString();
            checkBoxRedPokaz.Checked = GetCheckBoxValue("pokaz", gridView);
            checkBoxRedNoski.Checked = GetCheckBoxValue("noski", gridView);
            checkBoxRedSpec.Checked = GetCheckBoxValue("spec_ob", gridView);
            checkBoxRedVid.Checked = GetCheckBoxValue("vid_ob", gridView);
            int selectedValue = Convert.ToInt32(gridView.GetFocusedRowCellValue("nastav").ToString());
            switch (selectedValue)
            {
                case 1:
                    radioButtonRed1.Checked = true;
                    break;
                case 2:
                    radioButtonRed2.Checked = true;
                    break;
                case 3:
                    radioButtonRed3.Checked = true;
                    break;
                default:
                    radioButtonRedNet.Checked = true;
                    break;
            }
            checkBoxRedPokaz_sp.Checked = GetCheckBoxValue("pokaz_sp", gridView);
            checkBoxRedSock.Checked = GetCheckBoxValue("socksserv", gridView);
            comboBoxRedClass.Text = gridView.GetFocusedRowCellValue("idClass").ToString();
        }
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = true;
            AddTab.TabPages[1].PageVisible = false;
            // Заполняем комбобоксы:
            comboTableItems(comboBoxAddVid, comboBoxAddSpec, comboBoxAddClass);
            comboBoxAddVid.Text = "прочее                             ";
            comboBoxAddSpec.Text = "Другое";
            // Получаем доступ к GridView
            GridView gridView = oborudGrid.MainView as GridView;
            // Код = последнему коду в таблице + 1
            textBoxAddKod.Text = (Convert.ToInt32(gridView.GetDataRow(gridView.RowCount-1)["kod_ob"])+1).ToString();
        }

        private void simpleButtonRedOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[1].PageVisible = false;
            textBoxRedKod.Text = "";
            textBoxRedName.Text = "";
            textBoxRedSokrName.Text = "";
            comboBoxRedVid.Text = "";
            comboBoxRedSpec.Text = "";
            checkBoxRedPokaz.Checked = false;
            checkBoxRedNoski.Checked = false;
            checkBoxRedSpec.Checked = false;
            checkBoxRedVid.Checked = false;
            radioButtonRedNet.Checked = true;
            radioButtonRed1.Checked = false;
            radioButtonRed2.Checked = false;
            radioButtonRed3.Checked = false;
            checkBoxRedPokaz_sp.Checked = false;
            checkBoxRedSock.Checked = false;
            comboBoxRedClass.Text = "";
        }

        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = false;
            //textBoxAddKod.Text = "";
            textBoxAddName.Text = "";
            textBoxAddSokrName.Text = "";
            //comboBoxAddVid.Text = "";
            //comboBoxAddSpec.Text = "";
            checkBoxAddPokaz.Checked = false;
            checkBoxAddNoski.Checked = false;
            checkBoxAddSpec.Checked = false;
            checkBoxAddVid.Checked = false;
            radioButtonAddNet.Checked = true;
            radioButtonAdd1.Checked = false;
            radioButtonAdd2.Checked = false;
            radioButtonAdd3.Checked = false;
            checkBoxAddPokaz_sp.Checked = false;
            checkBoxAddSock.Checked = false;
            comboBoxAddClass.Text = "";
        }



        private void simpleButtonRedSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int nastav = 0;
                if (radioButtonRedNet.Checked) nastav = 0;
                if (radioButtonRed1.Checked) nastav = 1;
                if (radioButtonRed2.Checked) nastav = 2;
                if (radioButtonRed3.Checked) nastav = 3;

                string queryOborudRed = $"UPDATE oborud_shv " +
                                             $"SET text_ob = '" + textBoxRedName.Text + "', " +
                                             $"text_ob_s = '" + textBoxRedSokrName.Text + "', " +
                                             $"oborud_shv.ko_ob_all = oborud_shv_ob.ko_ob_all, " +
                                             $"no_spec = spOborudMachine.miniName, " +
                                             $"pokaz = " + Convert.ToInt32(checkBoxRedPokaz.Checked) + ", " +
                                             $"noski = " + Convert.ToInt32(checkBoxRedNoski.Checked) + ", " +
                                             $"spec_ob = " + Convert.ToInt32(checkBoxRedSpec.Checked) + ", " +
                                             $"vid_ob = " + Convert.ToInt32(checkBoxRedVid.Checked) + ", " +
                                             $"nastav = " + nastav + ", " +
                                             $"pokaz_sp = " + Convert.ToInt32(checkBoxRedPokaz_sp.Checked) + ", " +
                                             $"socksserv = " + Convert.ToInt32(checkBoxRedSock.Checked) + ", " +
                                             $"oborud_shv.id_class = (SELECT id_class FROM matrix_class WHERE caption = '" + comboBoxRedClass.Text + "')" +
                                         $" FROM oborud_shv,spOborudMachine,oborud_shv_ob " +
                                         $" WHERE kod_ob = " + textBoxRedKod.Text +
                                         $" AND oborud_shv_ob.text_ob = '" + comboBoxRedVid.Text + "' " +
                                         $" AND spOborudMachine.name = '" + comboBoxRedSpec.Text + "' ";
                
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryOborudRed, connection);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableOborudRed = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableOborudRed);


            }
            GridView gridView = oborudGrid.MainView as GridView;
            // Сохраняем текущий индекс строки
            int currentRowIndex = gridView.FocusedRowHandle;
            int topRowIndex = gridView.TopRowIndex;
            //Обновление таблицы:
            oborudGrid_Load(sender, e);
            //Возвращаемся к курсору:
            gridView.FocusedRowHandle = currentRowIndex;
            gridView.TopRowIndex = topRowIndex;
            //Закрыть вкладку
            AddTab.TabPages[1].PageVisible = false;
        }

        private void simpleButtonAddSave_Click(object sender, EventArgs e)
        {
           using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int nastav = 0;
                if (radioButtonAddNet.Checked) nastav = 0;
                if (radioButtonAdd1.Checked) nastav = 1;
                if (radioButtonAdd2.Checked) nastav = 2;
                if (radioButtonAdd3.Checked) nastav = 3;

                string klass;
                if (comboBoxAddClass.Text == "")
                    klass = "NULL";
                else
                    klass = "(SELECT id_class FROM matrix_class WHERE caption = '" + comboBoxAddClass.Text + "')";

                string queryOborudAdd = $"INSERT INTO oborud_shv (text_ob, text_ob_s, ko_ob_all, no_spec, pokaz, noski, spec_ob, vid_ob, nastav, pokaz_sp, socksserv, id_class) " +
                                        $"VALUES (" +
                                        $"'{textBoxAddName.Text}', " +
                                        $"'{textBoxAddSokrName.Text}', " +
                                        $"(SELECT ko_ob_all FROM oborud_shv_ob WHERE text_ob = '{comboBoxAddVid.Text}'), " +
                                        $"(SELECT miniName FROM spOborudMachine WHERE name = '{comboBoxAddSpec.Text}'), " +
                                        $"{Convert.ToInt32(checkBoxAddPokaz.Checked)}, " +
                                        $"{Convert.ToInt32(checkBoxAddNoski.Checked)}, " +
                                        $"{Convert.ToInt32(checkBoxAddSpec.Checked)}, " +
                                        $"{Convert.ToInt32(checkBoxAddVid.Checked)}, " +
                                        $"{nastav}, " +
                                        $"{Convert.ToInt32(checkBoxAddPokaz_sp.Checked)}, " +
                                        $"{Convert.ToInt32(checkBoxAddSock.Checked)}, " +
                                        $"{klass} )";
                
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryOborudAdd, connection);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableOborudADD = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableOborudADD);
            }
            GridView gridView = oborudGrid.MainView as GridView;
            //Обновление таблицы:
            oborudGrid_Load(sender, e);
            //Идем в конец:
            gridView.FocusedRowHandle = gridView.RowCount-1;
            gridView.TopRowIndex = gridView.RowCount;
            //Закрыть вкладку
            AddTab.TabPages[0].PageVisible = false;
        }

        private void simpleButtonArhiv_Click(object sender, EventArgs e)
        {
            GridView gridView = oborudGrid.MainView as GridView;
            // Сохраняем текущий индекс строки
            int currentRowIndex = gridView.FocusedRowHandle;
            int topRowIndex = gridView.TopRowIndex;
            // Получаем данные из ячейки
            string textObArh = gridView.GetFocusedRowCellValue("text_ob").ToString();
            int kodObArh = Convert.ToInt32(gridView.GetFocusedRowCellValue("kod_ob"));
            string message = "Вы уверены что хотите занести '" + textObArh + "' в архив";
            var result = MessageBox.Show(message, "В архив?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //SQL запрос на занесение в архив
                //
                // Обновление таблицы:
                oborudGrid_Load(sender, e);
                // Возвращаемся к курсору:
                gridView.FocusedRowHandle = currentRowIndex - 1;
                gridView.TopRowIndex = topRowIndex - 1;
            }
        }
    }
}
