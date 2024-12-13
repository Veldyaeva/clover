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
using DevExpress.XtraGrid.Views.Grid;

namespace SewingProduction.form
{
    public partial class SpravBrig : Form
    {
        // Оснавная БД:
        //string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        string queryList;
        int strForAdd;
        string tableString;
        List<string> fieldsQueryListSQL;
        // Отслеживание изменений в базе данных:
        private SqlDependency sqlDependency;
        // Соединение с бд:
        private SqlConnection connection;
        //чтобы перейти к нужной строке в таблице:
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        bool flagAddDown = false; //если добавили поле в таблицу
        bool flagStartListening = false; //вкл прослушки
        //словари для рус названий столбцов:
        Dictionary<string, string> eng_rus = new Dictionary<string, string>();
        Dictionary<string, string> rus_eng = new Dictionary<string, string>();
        //Таймер для уведомления о сохранении:
        private Timer timer;

        public SpravBrig(string tableSQL, string rusNameTableSQL)
        {
            InitializeComponent();
            //Таймер
            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            //Таблица Brig:
            tableString = tableSQL;
            //Имя формы:
            this.Text = rusNameTableSQL;
            //Текст запроса для Brig:
            queryList = $@" SELECT id_brig,n_brig,brig,nameZeh AS 'Цех'
                            FROM Brig
                            LEFT JOIN ZehList ON ZehList.idZeh = Brig.idZeh ";
            //Иницилизация листа столбцов:
            fieldsQueryListSQL = new List<string>();
        }
        private void SpravBrig_Load(object sender, EventArgs e)
        {
            //Задание имен
            allTableName(tableString);
            //Загрузка комбобокса:
            comboBoxZeh_Enter(sender, e);
        }
        //Рус нэйминг столбцов:
        void allTableName(string tableName)
        {
            try
            {
                string query = @"SELECT acn.name, acn.name_rus, data_type, readonly 
                                FROM dbo.all_column_name acn
                                INNER JOIN all_table_name atn
                                ON acn.id_atn = atn.id_atn
                                WHERE atn.name = @tableName
                                ORDER BY ORDINAL_POSITION";
                using (SqlConnection connectionName = new SqlConnection(connectionString))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connectionName);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@tableName", tableName);
                    System.Data.DataTable tableList = new System.Data.DataTable();
                    dataAdapter.Fill(tableList);
                    foreach (DataRow row in tableList.Rows)
                    {
                        eng_rus.Add(row["name"].ToString(), row["name_rus"].ToString());
                        rus_eng.Add(row["name_rus"].ToString(), row["name"].ToString());
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                MessageBox.Show($"{sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading rus name: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки русских имен {ex.Message}");
            }
        }
        //Загрузка грида:
        private void gridControlSprav_Load(object sender, EventArgs e)
        {
            using (SqlConnection connectionLoad = new SqlConnection(connectionString))
            {
                //используя подключение отправляем запрос БД:
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryList, connectionLoad);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableList = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableList);
                //Закгрузка в таблицу грида:
                spravList.DataSource = tableList;
                fieldsQueryListSQL.Clear();
                // Получаем имена столбцов и добавляем их в список:
                foreach (System.Data.DataColumn column in tableList.Columns)
                {
                    // Добавляем имя столбца в список
                    fieldsQueryListSQL.Add(column.ColumnName);
                }
                // Получаем доступ к GridView
                GridView gridView = gridControlSprav.MainView as GridView;
                //Запрет на редактирование 1й столбца
                //gridView.Columns[0].OptionsColumn.AllowEdit = false;
                gridView.Columns[0].Visible = false;
                // Изменяем заголовки столбцов
                for (int i = 0; i < tableList.Columns.Count; i++)
                {
                    // Получаем английское имя
                    string englishName = tableList.Columns[i].Caption;
                    // Проверяем, есть ли соответствующее русское имя в словаре
                    if (eng_rus.TryGetValue(englishName, out string russianName))
                    {
                        // Заменяем заголовок столбца на русское имя
                        gridView.Columns[i].Caption = russianName;
                    }
                }
                // Выравнивание столбцов
                //gridView.BestFitColumns();
                gridView1.Columns["n_brig"].Width = 100;
                gridView.OptionsView.ColumnAutoWidth = true;
            }
            if (!flagStartListening)
            {
                // Запуск отслеживания изменений для соединения с базой данных
                SqlDependency.Start(connectionString);
                // Начинаем прослушивание
                StartListening();
            }
        }
        //Загрузка комбобокса список цехов:
        private void comboBoxZeh_Enter(object sender, EventArgs e)
        {
            using (SqlConnection connectionCombo = new SqlConnection(connectionString))
            {
                //Текст запроса:
                string queryZehAllList = $"SELECT nameZeh FROM ZehList";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryZehAllList, connectionCombo);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableVidProizv = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableVidProizv);
                comboBoxZeh.Items.Clear();
                //Загрузка в комбобокс:
                foreach (DataRow row in tableVidProizv.Rows)
                {
                    comboBoxZeh.Items.Add(row[0].ToString());
                }
            }
        }
        public void StartListening()
        {
            try
            {
                flagStartListening = true;
                // Остановка предыдущего прослушивания, если оно было активно:
                StopListening();
                // SQL-запрос
                string queryOborudList = $"SELECT id_brig,idZeh,n_brig,brig FROM dbo.{tableString}";
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
                Debug.WriteLine("Data was changed");

                // Обновление UI через Invoke
                if (this.IsHandleCreated)
                this.Invoke((MethodInvoker)delegate
                {
                    GridView gridView = gridControlSprav.MainView as GridView;
                    // Запомнили положение в таблице:
                    topRowIndex = gridView.TopRowIndex;
                    // Обновили таблицу
                    gridControlSprav_Load(sender, e);
                    // Возвращаемся к курсору:
                    gridView.FocusedRowHandle = currentRowIndex;
                    gridView.TopRowIndex = topRowIndex;
                    // Если добавлена новая запись, переходим к ней:
                    if (flagAddDown)
                    {
                        gridView.FocusedRowHandle = gridView.RowCount - 1;
                        gridView.TopRowIndex = gridView.RowCount - 1;
                        flagAddDown = false;
                        gridControlSprav_Click(sender, e);
                    }
                });
            }
            // Возобновляем прослушивание
            StartListening();
        }
        //Кнопка добавить:
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                xtraTabPageAdd.Text = "Добавить";
                GridView gridView = (GridView)gridControlSprav.MainView;
                AddTab.TabPages[0].PageVisible = true;
                simpleButtonDel.Visible = false;
                simpleButtonAddOtm.Visible = true;
                simpleButtonAddSave.Visible = true;
                // Код = последнему коду в таблице + 1
                textBoxKod.Text = (Convert.ToInt32(gridView.GetDataRow(gridView.RowCount - 1)[0]) + 1).ToString();
                textBoxBrig.Text = "";
                textBoxBrig.ReadOnly = false;
                textBoxNBrig.Text = "";
                textBoxNBrig.ReadOnly = false;
                comboBoxZeh.Text = "";
                comboBoxZeh.Enabled = true;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        //Кнопка редактировать:
        private void simpleButtonRed_Click(object sender, EventArgs e)
        {
            try
            {
                xtraTabPageAdd.Text = "Редактировать";
                // Получаем доступ к GridView
                GridView gridView = gridControlSprav.MainView as GridView;
                AddTab.TabPages[0].PageVisible = true;
                simpleButtonDel.Visible = true;
                simpleButtonAddOtm.Visible = true;
                simpleButtonAddSave.Visible = true;
                // Получаем текущую выделенную строку в текстбокси и др
                textBoxKod.Text = gridView.GetFocusedRowCellValue(gridView.Columns[0]).ToString();
                textBoxBrig.Text = gridView.GetFocusedRowCellValue("brig").ToString();
                textBoxBrig.ReadOnly = false;
                textBoxNBrig.Text = gridView.GetFocusedRowCellValue("n_brig").ToString();
                textBoxNBrig.ReadOnly = false;
                comboBoxZeh.Text = gridView.GetFocusedRowCellValue("Цех").ToString();
                comboBoxZeh.Enabled = true;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        //Клик на грид:
        private void gridControlSprav_Click(object sender, EventArgs e)
        {
            try
            {
                xtraTabPageAdd.Text = "Просмотр";
                // Сохраняем индекс строки
                GridView gridView = gridControlSprav.MainView as GridView;
                currentRowIndex = gridView.FocusedRowHandle;
                AddTab.TabPages[0].PageVisible = true;
                simpleButtonDel.Visible = false;
                simpleButtonAddOtm.Visible = false;
                simpleButtonAddSave.Visible = false;
                // Получаем текущую выделенную строку в текстбокси и др
                textBoxKod.Text = gridView.GetFocusedRowCellValue(gridView.Columns[0]).ToString();
                textBoxBrig.Text = gridView.GetFocusedRowCellValue("brig").ToString();
                textBoxBrig.ReadOnly = true;
                textBoxNBrig.Text = gridView.GetFocusedRowCellValue("n_brig").ToString();
                textBoxNBrig.ReadOnly = true;
                //comboBoxZeh.Text = gridView.GetFocusedRowCellValue("Цех") != DBNull.Value ? gridView.GetFocusedRowCellValue("Цех").ToString() : "";
                if (gridView.GetFocusedRowCellValue("Цех") != DBNull.Value)
                    comboBoxZeh.Text = gridView.GetFocusedRowCellValue("Цех").ToString();
                else comboBoxZeh.SelectedIndex = -1;
                comboBoxZeh.Enabled = false;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        //Кнопки вверх/вниз:
        private void gridControlSprav_KeyUp(object sender, KeyEventArgs e)
        {
            gridControlSprav_Click(sender, e);
        }
        //Кнопка Отмена:
        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            AddTab.TabPages[0].PageVisible = false;
        }
        //Редактирование в таблице:
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                using (SqlConnection connectionUPDATE = new SqlConnection(connectionString))
                {
                    connectionUPDATE.Open();
                    string sql = $"UPDATE {tableString} SET {e.Column.FieldName} = '{e.Value}' WHERE {fieldsQueryListSQL[0]}  = {gridView1.GetDataRow(e.RowHandle)[0]}";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    connectionUPDATE.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"{tableString} SQL Error: {sqlEx.Message}");
                MessageBox.Show($"{sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex.Message}");
                MessageBox.Show($"Ошибка {ex.Message}");
            }
        }
        //Удалить запись:
        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gridView = gridControlSprav.MainView as GridView;
                currentRowIndex = gridView.FocusedRowHandle;
                string textCol = textBoxBrig.Text;
                string kodCol = textBoxKod.Text;
                string message = "Вы уверены что хотите удалить '" + textCol + "' ?";
                var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connectionDELETE = new SqlConnection(connectionString))
                    {
                        string queryOborudArh = $"DELETE FROM {tableString} WHERE id_brig = {kodCol}";
                        using (SqlCommand command = new SqlCommand(queryOborudArh, connectionDELETE))
                        {
                            connectionDELETE.Open();
                            command.ExecuteNonQuery();
                            connectionDELETE.Close();
                        }
                    }
                    AddTab.TabPages[0].PageVisible = false;
                }
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"{tableString} SQL Error: {sqlEx.Message}");
                MessageBox.Show($"{sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex.Message}");
                MessageBox.Show($"Ошибка {ex.Message}");
            }
        }
        //Кнопка сохранить:
        private void simpleButtonAddSave_Click(object sender, EventArgs e)
        {
            try
            {
                string queryAdd = $"";
                using (SqlConnection connectionINSERT = new SqlConnection(connectionString))
                {
                    GridView gridView = gridControlSprav.MainView as GridView;
                    currentRowIndex = gridView.FocusedRowHandle;
                    switch (xtraTabPageAdd.Text)
                    {
                        case "Добавить":
                            {
                                queryAdd = $@"INSERT INTO {tableString} (brig,n_brig,idZeh)
                                              VALUES ('{textBoxBrig.Text}',{textBoxNBrig.Text},
                                               (SELECT idZeh FROM ZehList WHERE nameZeh = '{comboBoxZeh.Text}'))";
                                flagAddDown = true;
                                break;
                            }
                        case "Редактировать":
                            {
                                queryAdd = $@"UPDATE {tableString} 
                                                SET brig = '{textBoxBrig.Text}',
                                                n_brig = {textBoxNBrig.Text},
                                                idZeh = (SELECT idZeh FROM ZehList WHERE nameZeh = '{comboBoxZeh.Text}')
                                              WHERE id_brig =  {textBoxKod.Text}";
                                break;
                            }
                        default:
                            break;
                    }
                    using (SqlCommand command = new SqlCommand(queryAdd, connectionINSERT))
                    {
                        connectionINSERT.Open();
                        command.ExecuteNonQuery();
                        connectionINSERT.Close();
                    }
                    labelSave.Text = "Сохранено!";
                    //AddTab.TabPages[0].PageVisible = false;
                }
            }
            catch (SqlException sqlEx)
            {
                labelSave.Text = "Ошибка!";
                Debug.WriteLine($"{tableString} SQL Error: {sqlEx.Message}");
                MessageBox.Show($"{sqlEx.Message}");
            }
            labelSave.Visible = true;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            labelSave.Visible = false; // Скрываем лейбл
            timer.Stop(); // Останавливаем таймер
        }
        //ЛейблЛинк Справочник Цехов:
        private void linkLabelVid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (Form child in this.MdiParent.MdiChildren)
            {
                if (child is SpravZeh)
                {
                    // Если форма уже открыта, переключаем на нее
                    child.BringToFront();
                    return;
                }
            }
            // Если форма не открыта, создаем новую
            SpravZeh f = new SpravZeh("ZehList", "Справочник Цехов");
            f.MdiParent = this.MdiParent;
            f.Show();
        }
        //Закрытие формы:
        private void SpravForAll_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

    }
}
