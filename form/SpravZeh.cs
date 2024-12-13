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
using DevExpress.XtraReports.UI;
using static DevExpress.Mvvm.Native.Either;

namespace SewingProduction.form
{
    public partial class SpravZeh : Form
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

        public SpravZeh(string tableSQL, string rusNameTableSQL)
        {
            InitializeComponent();
            //Таймер
            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            //Таблица ZehList:
            tableString = tableSQL;
            //Имя формы:
            this.Text = rusNameTableSQL;
            //Текст запроса для ZehList:
            queryList = $@" SELECT idZeh,nameZeh,nameProizv,address 
                            FROM ZehList
                            LEFT JOIN spVidProizv ON spVidProizv.idProizv = ZehList.idProizv ";
            //Иницилизация листа столбцов:
            fieldsQueryListSQL = new List<string>();
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
        private void SpravZeh_Load(object sender, EventArgs e)
        {
            //Задание имен
            allTableName(tableString);
            //Загрузка комбобокса:
            comboBoxVidProizv_Enter(sender, e);
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
                gridView.BestFitColumns();
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
        //Загрузка комбобокса виды производства:
        private void comboBoxVidProizv_Enter(object sender, EventArgs e)
        {
            using (SqlConnection connectionCombo = new SqlConnection(connectionString))
            {
                //Текст запроса:
                string queryOborudAllList = $"SELECT nameProizv FROM spVidProizv";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryOborudAllList, connectionCombo);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableVidProizv = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableVidProizv);
                comboBoxVidProizv.Items.Clear();
                //Загрузка в комбобокс:
                foreach (DataRow row in tableVidProizv.Rows)
                {
                    comboBoxVidProizv.Items.Add(row[0].ToString());
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
                string queryOborudList = $"SELECT idZeh,nameZeh,address,idProizv FROM dbo.{tableString}";
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
                textBoxName.Text = "";
                textBoxName.ReadOnly = false;
                textBoxAdres.Text = "";
                textBoxAdres.ReadOnly = false;
                comboBoxVidProizv.Text = "";
                comboBoxVidProizv.Enabled = true;
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
                textBoxName.Text = gridView.GetFocusedRowCellValue("nameZeh").ToString();
                textBoxName.ReadOnly = false;
                textBoxAdres.Text = gridView.GetFocusedRowCellValue("address").ToString();
                textBoxAdres.ReadOnly = false;
                comboBoxVidProizv.Text = gridView.GetFocusedRowCellValue("nameProizv").ToString();
                comboBoxVidProizv.Enabled = true;
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
                textBoxName.Text = gridView.GetFocusedRowCellValue("nameZeh").ToString();
                textBoxName.ReadOnly = true;
                textBoxAdres.Text = gridView.GetFocusedRowCellValue("address").ToString();
                textBoxAdres.ReadOnly = true;
                //comboBoxVidProizv.Text = gridView.GetFocusedRowCellValue("nameProizv") != DBNull.Value ? gridView.GetFocusedRowCellValue("nameProizv").ToString() : "";
                if (gridView.GetFocusedRowCellValue("nameProizv") != DBNull.Value)
                    comboBoxVidProizv.Text = gridView.GetFocusedRowCellValue("nameProizv").ToString();
                else comboBoxVidProizv.SelectedIndex = -1;
                comboBoxVidProizv.Enabled = false;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Error: {Ex.Message}");
            }
        }
        private void gridControlSprav_KeyUp(object sender, KeyEventArgs e)
        {
            gridControlSprav_Click(sender, e);
        }
        //Кнопка Отмена:
        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            AddTab.TabPages[0].PageVisible = false;
        }
        //Редактирование таблице:
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }
        //Удалить запись:
        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gridView = gridControlSprav.MainView as GridView;
                currentRowIndex = gridView.FocusedRowHandle;
                string textCol = textBoxName.Text;
                string kodCol = textBoxKod.Text;
                string message = "Удаление ЦЕХА приведет к удалению бригад и оборудования в этом цеху, Вы уверены что хотите удалить '" + textCol + "' ?";
                var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connectionDELETE = new SqlConnection(connectionString))
                    {
                        string queryOborudArh = $@" DELETE FROM OborudBrig WHERE idZeh = {kodCol}
                                                    DELETE FROM Brig WHERE idZeh = {kodCol}
                                                    DELETE FROM ZehList WHERE idZeh = {kodCol}";
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
                                queryAdd = $@"INSERT INTO {tableString} (nameZeh,address,idProizv)
                                              VALUES ('{textBoxName.Text}','{textBoxAdres.Text}',
                                               (SELECT idProizv FROM spVidProizv WHERE nameProizv = '{comboBoxVidProizv.Text}'))";
                                flagAddDown = true;
                                break;
                            }
                        case "Редактировать":
                            {
                                queryAdd = $@"UPDATE {tableString} 
                                                SET nameZeh = '{textBoxName.Text}',
                                                address = '{textBoxAdres.Text}',
                                                idProizv = (SELECT idProizv FROM spVidProizv WHERE nameProizv = '{comboBoxVidProizv.Text}')
                                              WHERE idZeh =  {textBoxKod.Text}";
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
                    //gridView.FocusedRowHandle = gridView.RowCount - 1;
                    //gridView.TopRowIndex = gridView.RowCount - 1;
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
        // ЛейблЛинк виды производств:
        private void linkLabelVid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (Form child in this.MdiParent.MdiChildren)
            {
                if (child is SpravForAll)
                {
                    // Если форма уже открыта, переключаем на нее
                    child.BringToFront();
                    return;
                }
            }
            // Если форма не открыта, создаем новую
            SpravForAll f = new SpravForAll("spVidProizv", "Справочник Вид произв");
            f.MdiParent = this.MdiParent;
            f.Show();
        }
        // Закрытие формы:
        private void SpravForAll_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

    }
}
