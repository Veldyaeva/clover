
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
                //connection.Open();
                //Текст запроса:
                string queryOborudList = $"SELECT kod_ob ,oborud_shv.text_ob,text_ob_s," +
                                        $"oborud_shv_ob.text_ob AS text_ob_tip,spec_ob,nastav," +
                                        $"spOborudMachine.name AS vidm,pokaz_sp,matrix_class.caption AS idClass,show_for_plan," +
                                        $"(CASE vid_shp WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_shp," +
                                        $"(CASE vid_vzp WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_vzp," +
                                        $"(CASE vid_np WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_np," +
                                        $"(CASE vid_rz WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_rz" +
                                        $" FROM oborud_shv " +
                                        $" LEFT JOIN oborud_shv_ob ON oborud_shv_ob.ko_ob_all = oborud_shv.ko_ob_all " +
                                        $" LEFT JOIN spOborudMachine ON spOborudMachine.miniName = oborud_shv.no_spec" +
                                        $" LEFT JOIN matrix_class ON matrix_class.id_class = oborud_shv.id_class" +
                                        $" WHERE arhiv IS NULL";
                //используя подключение отправляем запрос БД:
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryOborudList, connection);
                //Закрываем подключение
                //connection.Close();
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
                    gridView.Columns["show_for_plan"].ColumnEdit = checkEdit;
                    gridView.Columns["spec_ob"].ColumnEdit = checkEdit;
                    //gridView.Columns["pokaz"].OptionsColumn.AllowEdit = false; // Запрещаем редактирование

                    gridView.OptionsView.ShowGroupPanel = false; // Панель группировки отображается
                    gridView.GroupPanelText = ""; // Текст
                    gridView.OptionsFind.AlwaysVisible = true; // Всегда показывать панель поиска
                    
 
                }
                // Отображаем количество записей+1 в textBoxAddKod
                //textBoxAddKod.Text = (tableOborudList.Rows.Count + 1).ToString();

            }

        }

        //процедура загрузки таблицы oborud_shv_ob / spOborudMachine / MatrixClass в комбобоксы (добавить/редактировать):
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
            comboTableItems(comboBoxRedGrup, comboBoxRedVidm, comboBoxRedClass);
            comboBoxRedGrup.Text = gridView.GetFocusedRowCellValue("text_ob_tip").ToString();
            comboBoxRedVidm.Text = gridView.GetFocusedRowCellValue("vidm").ToString();
            comboBoxRedClass.Text = gridView.GetFocusedRowCellValue("idClass").ToString();
            // comboBox group for proizv:
            comboBoxRedShp.Text = gridView.GetFocusedRowCellValue("vid_shp") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_shp").ToString() : "нет";
            comboBoxRedVzp.Text = gridView.GetFocusedRowCellValue("vid_vzp") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_vzp").ToString() : "нет";
            comboBoxRedNp.Text = gridView.GetFocusedRowCellValue("vid_np") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_np").ToString() : "нет";
            comboBoxRedRz.Text = gridView.GetFocusedRowCellValue("vid_rz") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_rz").ToString() : "нет";
            // radiobutton group for nastav:
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
            //checkBox:
            checkBoxRedShow.Checked = GetCheckBoxValue("show_for_plan", gridView);
            checkBoxRedSpec.Checked = GetCheckBoxValue("spec_ob", gridView);
        }
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = true;
            AddTab.TabPages[1].PageVisible = false;
            // Получаем доступ к GridView
            GridView gridView = oborudGrid.MainView as GridView;
            // Код = последнему коду в таблице + 1
            textBoxAddKod.Text = (Convert.ToInt32(gridView.GetDataRow(gridView.RowCount-1)["kod_ob"])+1).ToString();
            textBoxAddName.Text = "";
            textBoxAddSokrName.Text = "";
            // Заполняем комбобоксы:
            comboTableItems(comboBoxAddGrup, comboBoxAddVidm, comboBoxAddClass);
            comboBoxAddGrup.Text = "прочее                             ";
            comboBoxAddVidm.Text = "Другое";
            comboBoxAddClass.Text = "";
            //Вид произв:
            comboBoxAddShp.Text = "нет";
            comboBoxAddVzp.Text = "нет";
            comboBoxAddNp.Text = "нет";
            comboBoxAddRz.Text = "нет";
            //наставничество:
            radioButtonAddNet.Checked = true;
            //скрыть и тд:
            checkBoxAddShow.Checked = false;
            checkBoxAddSpec.Checked = false;
        }

        private void simpleButtonRedOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[1].PageVisible = false;
            textBoxRedKod.Text = "";
            textBoxRedName.Text = "";
            textBoxRedSokrName.Text = "";
            comboBoxRedGrup.Text = "";
            comboBoxRedVidm.Text = "";
            checkBoxRedShow.Checked = false;
            checkBoxRedSpec.Checked = false;
            radioButtonRedNet.Checked = true;
            radioButtonRed1.Checked = false;
            radioButtonRed2.Checked = false;
            radioButtonRed3.Checked = false;
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
            checkBoxAddShow.Checked = false;
            checkBoxAddSpec.Checked = false;
            radioButtonAddNet.Checked = true;
            radioButtonAdd1.Checked = false;
            radioButtonAdd2.Checked = false;
            radioButtonAdd3.Checked = false;
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
                                             $"oborud_shv.id_class = (SELECT id_class FROM matrix_class WHERE caption = '" + comboBoxRedClass.Text + "')," +
                                             $"vid_shp = " + comboBoxRedShp.SelectedIndex + ", " +
                                             $"vid_vzp = " + comboBoxRedVzp.SelectedIndex + ", " +
                                             $"vid_np = " + comboBoxRedNp.SelectedIndex + ", " +
                                             $"vid_rz = " + comboBoxRedRz.SelectedIndex + ", " +
                                             $"nastav = " + nastav + ", " +
                                             $"show_for_plan = " + Convert.ToInt32(checkBoxRedShow.Checked) + ", " +
                                             $"spec_ob = " + Convert.ToInt32(checkBoxRedSpec.Checked) + " " +
                                         $" FROM oborud_shv,spOborudMachine,oborud_shv_ob " +
                                         $" WHERE kod_ob = " + textBoxRedKod.Text +
                                         $" AND oborud_shv_ob.text_ob = '" + comboBoxRedGrup.Text + "' " +
                                         $" AND spOborudMachine.name = '" + comboBoxRedVidm.Text + "' ";

                using (SqlCommand command = new SqlCommand(queryOborudRed, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
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

                string queryOborudAdd = $"INSERT INTO oborud_shv (text_ob, text_ob_s, ko_ob_all, no_spec, id_class, vid_shp, vid_vzp, vid_np, vid_rz, nastav, show_for_plan, spec_ob) " +
                                        $"VALUES (" +
                                        $"'{textBoxAddName.Text}', " +
                                        $"'{textBoxAddSokrName.Text}', " +
                                        $"(SELECT ko_ob_all FROM oborud_shv_ob WHERE text_ob = '{comboBoxAddGrup.Text}'), " +
                                        $"(SELECT miniName FROM spOborudMachine WHERE name = '{comboBoxAddVidm.Text}'), " +
                                        $"{klass} ," +
                                        $"{comboBoxAddShp.SelectedIndex}," +
                                        $"{comboBoxAddVzp.SelectedIndex}," +
                                        $"{comboBoxAddNp.SelectedIndex}," +
                                        $"{comboBoxAddRz.SelectedIndex}," +
                                        $"{nastav}, " +
                                        $"{Convert.ToInt32(checkBoxAddShow.Checked)}, " +
                                        $"{Convert.ToInt32(checkBoxAddSpec.Checked)} ) ";

                using (SqlCommand command = new SqlCommand(queryOborudAdd, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
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
            string message = "Вы уверены что хотите занести '" + textObArh + "' в архив?";
            var result = MessageBox.Show(message, "В архив?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryOborudArh = $"UPDATE oborud_shv SET arhiv = 1 WHERE kod_ob = " + kodObArh;
                    // Используем SqlCommand для выполнения UPDATE
                    using (SqlCommand command = new SqlCommand(queryOborudArh, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery(); // Выполняем запрос UPDATE 
                        connection.Close();
                    }
                }

                // Обновление таблицы:
                oborudGrid_Load(sender, e);
                // Возвращаемся к курсору:
                gridView.FocusedRowHandle = currentRowIndex - 1;
                gridView.TopRowIndex = topRowIndex - 1;
            }
        }

        private void SpravOborud_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
