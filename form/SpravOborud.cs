
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization; // локализация для грида
//using DevExpress.XtraPrinting.Localization;// локализация для печати


namespace SewingProduction.form
{
    public partial class SpravOborud : Form
    {
        // Оснавная БД:
        //string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        private string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        // Отслеживание изменений в базе данных:
        private SqlDependency sqlDependency;
        // Соединение с бд:
        private SqlConnection connection;
        //чтобы перейти к нужной строке в таблице:
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        //если добавили поле в таблицу:
        bool flagAddDown = false;
        public SpravOborud()
        {
            InitializeComponent();
            // Запуск отслеживания изменений для соединения с базой данных
            SqlDependency.Start(connectionString);
            // Начинаем прослушивание
            StartListening();

        }

        private void SpravOborud_Load(object sender, EventArgs e)
        {
            GridLocalizer.Active = new RussianGridLocalizer();
            //PreviewLocalizer.Active = new RussianPrintLocalizer();
            label4.Text = "Группа оборуд-я (для учета \n в цехе, компетенций)";
            label7.Text = "Группа оборуд-я (для учета \n в цехе, компетенций)";
            label21.Text = "Спец. оборудование \n для оказания услуг";
            label22.Text = "Спец. оборудование \n для оказания услуг";
        }
        
        public void StartListening()
        {
            try
            {
                // Остановка предыдущего прослушивания, если оно было активно:
                StopListening();
                // SQL-запрос
                string queryOborudList = $"SELECT kod_ob ,text_ob,text_ob_s," +
                                            $"ko_ob_all,spec_ob,nastav, arhiv," +
                                            $"no_spec,pokaz_sp,id_class,show_for_plan,vid_shp, vid_vzp, vid_np, vid_rz" +
                                            $" FROM dbo.oborud_shv";
                // Создание соединения с базой данных
                connection = new SqlConnection(connectionString);
                // Открытие соединения
                connection.Open();
                // Создание команды для выполнения SQL-запроса
                SqlCommand command = new SqlCommand(queryOborudList, connection);
                // Создание зависимости, чтобы отслеживать изменения
                sqlDependency = new SqlDependency(command);
                // Подписка на событие изменения
                sqlDependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
                // Выполнение команды
                command.ExecuteReader();

            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error starting listener: {ex.Message}");
            }

        }

        public void StopListening()
        {
            // Закрываем подключение
            if (connection != null)
            {
                connection.Close();
            }
        }
        private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            // Строка состояния:
            Debug.WriteLine($"Notification received: Type={e.Type}, Info={e.Info}, Source={e.Source}");
            // Проверка есть ли уведомления
            if (e.Type == SqlNotificationType.Change)
            {
                Debug.WriteLine("Обновление таблицы");

                // Обновление UI через Invoke
                this.Invoke((MethodInvoker)delegate
                {
                    GridView gridView = oborudGrid.MainView as GridView;
                    // Запомнили положение в таблице:
                    topRowIndex = gridView.TopRowIndex;
                    // Обновили таблицу
                    LoadData();
                    // Возвращаемся к курсору:
                    gridView.FocusedRowHandle = currentRowIndex;
                    gridView.TopRowIndex = topRowIndex;
                    // Если добавлена новая запись, переходим к ней:
                    if (flagAddDown)
                    {
                        gridView.FocusedRowHandle = gridView.RowCount - 1;
                        gridView.TopRowIndex = gridView.RowCount - 1;
                        flagAddDown = false;
                    }
                });

            }
            // Возобновляем прослушивание
            StartListening();
        }

        // Загрузка / обновление данных:
        private void LoadData()
        {
            using (var connectionSELECT = new SqlConnection(connectionString))
            {
                string queryOborudList = $@"SELECT kod_ob ,oborud_shv.text_ob,text_ob_s,
                                            oborud_shv_ob.text_ob AS text_ob_tip,spec_ob,
                                            spOborudMachine.name AS vidm,pokaz_sp,matrix_class.caption AS idClass,show_for_plan,
                                            (CASE arhiv WHEN 1 THEN 1 ELSE 0 END) AS arhiv,
                                            (CASE nastav WHEN 1 THEN 'оверлок' WHEN 2 THEN 'плоскошовка' WHEN 3 THEN 'универсалка' ELSE NULL END) AS nastav,
                                            (CASE vid_shp WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_shp,
                                            (CASE vid_vzp WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_vzp,
                                            (CASE vid_np WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_np,
                                            (CASE vid_rz WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_rz
                                         FROM oborud_shv 
                                             LEFT JOIN oborud_shv_ob ON oborud_shv_ob.ko_ob_all = oborud_shv.ko_ob_all 
                                             LEFT JOIN spOborudMachine ON spOborudMachine.miniName = oborud_shv.no_spec 
                                             LEFT JOIN matrix_class ON matrix_class.id_class = oborud_shv.id_class
                                         {(checkEditArhiv.Checked ? "" : "WHERE arhiv IS NULL OR arhiv = 0")} ";

                //используя подключение отправляем запрос БД:
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryOborudList, connectionSELECT);
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
                    gridView.Columns["arhiv"].ColumnEdit = checkEdit;
                    //gridView.Columns["pokaz"].OptionsColumn.AllowEdit = false; // Запрещаем редактирование

                    gridView.OptionsView.ShowGroupPanel = false; // Панель группировки отображается
                    gridView.GroupPanelText = ""; // Текст
                    gridView.OptionsFind.AlwaysVisible = true; // Всегда показывать панель поиска

                }
                // Отображаем количество записей+1 в textBoxAddKod
                //textBoxAddKod.Text = (tableOborudList.Rows.Count + 1).ToString();

            }
        }

        // Загрузка таблицы:
        private void oborudGrid_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        //процедура загрузки таблицы oborud_shv_ob / spOborudMachine / MatrixClass в комбобоксы (добавить/редактировать):
        public void comboTableItems(System.Windows.Forms.ComboBox comboBoxTableOborudShvOb,
                                    System.Windows.Forms.ComboBox comboBoxTableSpOborudMachine,
                                    System.Windows.Forms.ComboBox comboBoxTableMatrixClass)
        {
            using (SqlConnection connectionCombo = new SqlConnection(connectionString))
            {
                //Текст запроса для oborud_shv_ob:
                string queryOborudAllList = $"SELECT * FROM oborud_shv_ob ORDER by ko_ob_all ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryOborudAllList, connectionCombo);
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
                dataAdapter = new SqlDataAdapter(queryMachineAllList, connectionCombo);
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
                dataAdapter = new SqlDataAdapter(queryMatrixClassAllList, connectionCombo);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableMatrixClass = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableMatrixClass);
                comboBoxTableMatrixClass.Items.Clear();
                comboBoxTableMatrixClass.Items.Add("нет");
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

        // Кнопка Редактировать
        private void simpleButtonRed_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = false;
            AddTab.TabPages[1].PageVisible = true;
            // Получаем доступ к GridView
            GridView gridView = oborudGrid.MainView as GridView;
            // Получаем текущую выделенную строку в текстбокси и др
            textBoxRedKod.Text = gridView.GetFocusedRowCellValue("kod_ob").ToString();
            textBoxRedName.Text = gridView.GetFocusedRowCellValue("text_ob").ToString().Trim();
            textBoxRedSokrName.Text = gridView.GetFocusedRowCellValue("text_ob_s").ToString().Trim();
            // Заполняем комбобоксы:
            comboTableItems(comboBoxRedGrup, comboBoxRedVidm, comboBoxRedClass);
            comboBoxRedGrup.Text = gridView.GetFocusedRowCellValue("text_ob_tip").ToString();
            comboBoxRedVidm.Text = gridView.GetFocusedRowCellValue("vidm").ToString();
            comboBoxRedClass.Text = gridView.GetFocusedRowCellValue("idClass").ToString();
            comboBoxRedNastav.Text = gridView.GetFocusedRowCellValue("nastav").ToString();
            // comboBox group for proizv:
            comboBoxRedShp.Text = gridView.GetFocusedRowCellValue("vid_shp") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_shp").ToString() : "нет";
            comboBoxRedVzp.Text = gridView.GetFocusedRowCellValue("vid_vzp") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_vzp").ToString() : "нет";
            comboBoxRedNp.Text = gridView.GetFocusedRowCellValue("vid_np") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_np").ToString() : "нет";
            comboBoxRedRz.Text = gridView.GetFocusedRowCellValue("vid_rz") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_rz").ToString() : "нет";
            //checkBox:
            checkBoxRedShow.Checked = GetCheckBoxValue("show_for_plan", gridView);
            checkBoxRedSpec.Checked = GetCheckBoxValue("spec_ob", gridView);
            checkBoxRedArhiv.Checked = GetCheckBoxValue("arhiv", gridView);
        }

        // Кнопка Добавить
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = true;
            AddTab.TabPages[1].PageVisible = false;
            // Получаем доступ к GridView
            GridView gridView = oborudGrid.MainView as GridView;
            // Код = последнему коду в таблице + 1
            textBoxAddKod.Text = (Convert.ToInt32(gridView.GetDataRow(gridView.RowCount - 1)["kod_ob"]) + 1).ToString();
            textBoxAddName.Text = "";
            textBoxAddSokrName.Text = "";
            // Заполняем комбобоксы:
            comboTableItems(comboBoxAddGrup, comboBoxAddVidm, comboBoxAddClass);
            comboBoxAddGrup.Text = "прочее                             ";
            comboBoxAddVidm.Text = "Другое";
            comboBoxAddClass.Text = "";
            comboBoxAddNastav.Text = "";
            //Вид произв:
            comboBoxAddShp.Text = "нет";
            comboBoxAddVzp.Text = "нет";
            comboBoxAddNp.Text = "нет";
            comboBoxAddRz.Text = "нет";
            //скрыть и тд:
            checkBoxAddShow.Checked = false;
            checkBoxAddSpec.Checked = false;
            checkBoxAddArhiv.Checked = false;
        }

        // Кнопка Отменить на вкладке Редактировать
        private void simpleButtonRedOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[1].PageVisible = false;
            textBoxRedKod.Text = "";
            textBoxRedName.Text = "";
            textBoxRedSokrName.Text = "";
            comboBoxRedGrup.Text = "";
            comboBoxRedVidm.Text = "";
            comboBoxRedClass.Text = "";
            comboBoxRedNastav.Text = "";
            checkBoxRedShow.Checked = false;
            checkBoxRedSpec.Checked = false;
            checkBoxRedArhiv.Checked = false;
        }

        // Кнопка Отменить на вкладке Добавить
        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = false;
        }
        // Проверка заполения полей
        string proverkaZap(TextBox proverkaKod, TextBox proverkaName, TextBox proverkaSokrName, ComboBox proverkaGrup,
                        ComboBox proverkaShp, ComboBox proverkaVzp, ComboBox proverkaNp, ComboBox proverkaRz)
        {
            if (proverkaName.Text == "")
                return "Заполните поле 'Вид оборудования'!";
            if (proverkaSokrName.Text == "")
                return "Заполните поле 'Cокращенное наименование'!";
            if (proverkaGrup.Text == "")
                return "Заполните поле 'Группа оборудования'!";
            if (proverkaShp.Text == "нет" && proverkaVzp.Text == "нет" && proverkaNp.Text == "нет" && proverkaRz.Text == "нет")
                return "Выберите вид производства!";
            if (proverkaKod.Text == "0" || proverkaKod.Text == "")
                return "Ошибка, связанная с кодом записи, перезапустите программу и попробуйте снова.";
            return "OK";
        }
        // Кнопка Сохранить на вкладке Редактировать
        private void simpleButtonRedSave_Click(object sender, EventArgs e)
        {
            string proverka = proverkaZap(textBoxRedKod, textBoxRedName, textBoxRedSokrName, comboBoxRedGrup,
                comboBoxRedShp, comboBoxRedVzp, comboBoxRedNp, comboBoxRedRz);
            if (proverka == "OK")
            { 
                // Сохраняем текущий индекс строки:
                GridView gridView = oborudGrid.MainView as GridView;
                currentRowIndex = gridView.FocusedRowHandle;
                //topRowIndex = gridView.TopRowIndex;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
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
                                                 $"nastav = " + comboBoxRedNastav.SelectedIndex + ", " +
                                                 $"show_for_plan = " + Convert.ToInt32(checkBoxRedShow.Checked) + ", " +
                                                 $"spec_ob = " + Convert.ToInt32(checkBoxRedSpec.Checked) + ", " +
                                                 $"arhiv = " + Convert.ToInt32(checkBoxRedArhiv.Checked) + " " +
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
                //Закрыть вкладку
                AddTab.TabPages[1].PageVisible = false;
            }
            else MessageBox.Show(proverka, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Кнопка Сохранить на вкладке Добавить
        private void simpleButtonAddSave_Click(object sender, EventArgs e)
        {
            string proverka = proverkaZap(textBoxAddName, textBoxAddName, textBoxAddSokrName, comboBoxAddGrup,
                comboBoxAddShp, comboBoxAddVzp, comboBoxAddNp, comboBoxAddRz);
            if (proverka == "OK")
            {
                using (SqlConnection connectionINSERT = new SqlConnection(connectionString))
                {
                    string klass;
                    if (comboBoxAddClass.Text == "")
                        klass = "NULL";
                    else
                        klass = "(SELECT id_class FROM matrix_class WHERE caption = '" + comboBoxAddClass.Text + "')";

                    string queryOborudAdd = $"INSERT INTO oborud_shv (text_ob, text_ob_s, ko_ob_all, no_spec, id_class, vid_shp," +
                                                $" vid_vzp, vid_np, vid_rz, nastav, show_for_plan, spec_ob,arhiv) " +
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
                                                $"{comboBoxAddNastav.SelectedIndex}, " +
                                                $"{Convert.ToInt32(checkBoxAddShow.Checked)}, " +
                                                $"{Convert.ToInt32(checkBoxAddSpec.Checked)}, " +
                                                $"{Convert.ToInt32(checkBoxAddArhiv.Checked)} ) ";

                    using (SqlCommand command = new SqlCommand(queryOborudAdd, connectionINSERT))
                    {
                        connectionINSERT.Open();
                        command.ExecuteNonQuery();
                        connectionINSERT.Close();
                    }
                }
                // Флаг для перехода вниз
                flagAddDown = true;
                // Закрыть вкладку
                AddTab.TabPages[0].PageVisible = false;
            }
            else MessageBox.Show(proverka, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Кнопка Архив
        private void simpleButtonArhiv_Click(object sender, EventArgs e)
        {
            GridView gridView = oborudGrid.MainView as GridView;
            // Сохраняем индекс строки
            currentRowIndex = gridView.FocusedRowHandle;
            // Получаем данные из ячейки
            string textObArh = gridView.GetFocusedRowCellValue("text_ob").ToString();
            int kodObArh = Convert.ToInt32(gridView.GetFocusedRowCellValue("kod_ob"));
            string message = "Вы уверены что хотите занести '" + textObArh + "' в архив?";
            var result = MessageBox.Show(message, "В архив?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection connectionUPDATE = new SqlConnection(connectionString))
                {
                    string queryOborudArh = $"UPDATE oborud_shv SET arhiv = 1 WHERE kod_ob = " + kodObArh;
                    // Используем SqlCommand для выполнения UPDATE
                    using (SqlCommand command = new SqlCommand(queryOborudArh, connectionUPDATE))
                    {
                        connectionUPDATE.Open();
                        command.ExecuteNonQuery(); // Выполняем запрос UPDATE 
                        connectionUPDATE.Close();
                    }
                }
            }
        }
        private void oborudGrid_Click(object sender, EventArgs e)
        {
            GridView gridView = oborudGrid.MainView as GridView;
            // Сохраняем индекс строки
            currentRowIndex = gridView.FocusedRowHandle;
            // Отменяем если редакитруем:
            simpleButtonRedOtm_Click(sender, e);
        }

        private void SpravOborud_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

        private void checkEditArhiv_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
            GridView gridView = oborudGrid.MainView as GridView;
            //gridView.Columns["arhiv"].Visible = !gridView.Columns["arhiv"].Visible;
            //перенос столбца архив в конец:
            gridView.Columns["arhiv"].VisibleIndex = -(gridView.Columns["arhiv"].VisibleIndex - (gridView.Columns.Count-2));
        }
    }
}
