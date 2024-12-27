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
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.Diagnostics;
using DevExpress.ChartRangeControlClient.Core;
//using DevExpress.XtraGrid.Localization;
using DevExpress.DataAccess.Native.Data;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraExport.Helpers;
using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using System.Reflection;
using DevExpress.Mvvm.Native;

namespace SewingProduction.form
{
    public partial class SpravForAll : Form
    {
        // Оснавная БД:
        //string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
       string connectionString = Properties.Settings.Default.ACEConnectionString;

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
        private System.Windows.Forms.Label[] labels;
        private TextBox[] textBoxs;
        //словари:
        Dictionary<string, string> eng_rus = new Dictionary<string, string>();
        Dictionary<string, string> rus_eng = new Dictionary<string, string>();
        Dictionary<string, string> eng_type = new Dictionary<string, string>();
        Dictionary<string, int> rus_read = new Dictionary<string, int>();
        public SpravForAll(string tableSQL, string rusNameTableSQL)
        {
            //GridLocalizer.Active = new RussianGridLocalizer();
            InitializeComponent();
            //Таблица:
            tableString = tableSQL;
            //Имя формы:
            this.Text = rusNameTableSQL;
            //Текст запроса:
            queryList = $"SELECT * FROM " + tableString;
            //Иницилизация листа столбцов:
            fieldsQueryListSQL = new List<string>();
            labels = new System.Windows.Forms.Label[] { labelKod, label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };
            textBoxs = new TextBox[] { textBoxKod, textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10 };
            //Задание имен
            allTableName(tableString);
        }

        void allTableName(string tableName)
        {
            string query = @"SELECT acn.name, acn.name_rus, data_type, readonly 
                                FROM ACE_test.dbo.all_column_name acn
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
                    eng_type.Add(row["name"].ToString(), row["data_type"].ToString());
                    rus_read.Add(row["name_rus"].ToString(), (Int32)row["readonly"]);
                }
            }
        }
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
                gridView.Columns[0].OptionsColumn.AllowEdit = false;
                gridView.Columns[0].Visible = false;
                //gridView.OptionsBehavior.Editable = false;
                //gridView.GroupPanelText = ""; // Текст

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
                //перенос столбца архив в конец:
                //gridView.Columns["arhiv"].VisibleIndex = -(gridView.Columns["arhiv"].VisibleIndex - (gridView.Columns.Count - 2));
            }
            if (!flagStartListening) 
            {
                // Запуск отслеживания изменений для соединения с базой данных
                SqlDependency.Start(connectionString);
                // Начинаем прослушивание
                StartListening();
            }
        }
        // Отображение лейблов и текстбоксов в нужном кол-е
        void labelAndTextBox()
        {
            GridView gridView = gridControlSprav.MainView as GridView;

            if (gridView != null)
            {
                int columnCount = gridView.Columns.Count;

                for (int i = 0; i < labels.Length; i++)
                {
                    if (i < columnCount) // Если индекс меньше количества колонок
                    {
                        // текст = колонке
                        labels[i].Text = gridView.Columns[i].Caption;
                        // Делаем метку видимой
                        labels[i].Visible = true;
                        textBoxs[i].Visible = true; 
                    }
                    else
                    {
                        // Скрываем метки, если нет данных
                        labels[i].Visible = false;
                        textBoxs[i].Visible = false; 
                    }
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
                // SQL-запрос// Преобразование списка имен столбцов в строку
                string columns = string.Join(", ", fieldsQueryListSQL);
                // SQL-запрос
                string queryOborudList = $"SELECT {columns} FROM dbo.{tableString}";
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
                    }
                });

            }
            // Возобновляем прослушивание
            StartListening();
        }
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            xtraTabPageAdd.Text = "Добавить";
            AddTab.TabPages[0].PageVisible = true;
            simpleButtonDel.Visible = false;
            labelAndTextBox();
            GridView gridView = (GridView)gridControlSprav.MainView;
            // Код = последнему коду в таблице + 1
            textBoxKod.Text = (Convert.ToInt32(gridView.GetDataRow(gridView.RowCount - 1)[0]) + 1).ToString();
            for (int i = 1; i < fieldsQueryListSQL.Count; i++)
            {
                textBoxs[i].Text = "";
            }
            //flagAddDown = true;
            //gridView.AddNewRow();
            //strForAdd = gridView.FocusedRowHandle;
            //gridView.UpdateCurrentRow();
        }
        private void simpleButtonRed_Click(object sender, EventArgs e)
        {
            xtraTabPageAdd.Text = "Редактировать";
            AddTab.TabPages[0].PageVisible = true;
            simpleButtonDel.Visible = true;
            labelAndTextBox();
            // Получаем доступ к GridView
            GridView gridView = gridControlSprav.MainView as GridView;
            // Получаем текущую выделенную строку в текстбокси и др
            textBoxKod.Text = gridView.GetFocusedRowCellValue(gridView.Columns[0]).ToString();
            for (int i = 1; i < fieldsQueryListSQL.Count; i++)
            {
                textBoxs[i].Text = gridView.GetFocusedRowCellValue(gridView.Columns[i]).ToString();
            }
        }

        private void gridControlSprav_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControlSprav.MainView as GridView;
            // Сохраняем индекс строки
            currentRowIndex = gridView.FocusedRowHandle;
            if (xtraTabPageAdd.Text == "Редактировать")
                simpleButtonRed_Click(sender, e);
        }

        private void SpravForAll_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

        private void SpravForAll_Load(object sender, EventArgs e)
        {

        }

        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            AddTab.TabPages[0].PageVisible = false;
        }

        private void simpleButtonAddSave_Click(object sender, EventArgs e)
        {
            string queryOborudAdd = $"";
            using (SqlConnection connectionINSERT = new SqlConnection(connectionString))
            {
                GridView gridView = gridControlSprav.MainView as GridView;
                switch (xtraTabPageAdd.Text)
                {
                    case "Добавить":
                        {
                            queryOborudAdd += "INSERT INTO " + tableString + "(";
                            for (int i = 1; i < fieldsQueryListSQL.Count; i++)
                            {
                                queryOborudAdd += fieldsQueryListSQL[i];
                                queryOborudAdd += fieldsQueryListSQL.Count - 1 == i ? ")" : ",";
                            }
                            queryOborudAdd += " VALUES (";
                            for (int i = 1; i < fieldsQueryListSQL.Count; i++)
                            {
                                if (eng_type.TryGetValue(fieldsQueryListSQL[i], out string getType))
                                switch (getType)
                                {
                                    case "int": queryOborudAdd += textBoxs[i].Text; break;
                                    case "string": case "varchar": case "nvarchar": case "nchar": case "char": queryOborudAdd += "'" + textBoxs[i].Text + "'"; break;
                                    case "float": case "decimal": queryOborudAdd += textBoxs[i].Text.Replace(',', '.'); break;
                                }
                                queryOborudAdd += fieldsQueryListSQL.Count - 1 == i ? ")" : ",";
                            }
                            flagAddDown = true;
                            break;
                        }
                    case "Редактировать":
                        {
                            queryOborudAdd += "UPDATE " + tableString + " SET ";
                            for (int i = 1; i < fieldsQueryListSQL.Count; i++)
                            {
                                queryOborudAdd += fieldsQueryListSQL[i] + " = ";
                                if (eng_type.TryGetValue(fieldsQueryListSQL[i], out string getType))
                                switch (getType)
                                {
                                    case "int":     queryOborudAdd += textBoxs[i].Text; break;
                                    case "string": case "varchar": case "nvarchar": case "nchar": case "char": queryOborudAdd += "'" + textBoxs[i].Text + "'"; break;
                                    case "float": case "decimal":   queryOborudAdd += textBoxs[i].Text.Replace(',', '.'); break;
                                }
                                queryOborudAdd += fieldsQueryListSQL.Count - 1 == i ? "" : ",";
                            }
                            queryOborudAdd += " FROM " + tableString + " WHERE " + fieldsQueryListSQL[0] + " = " + textBoxs[0].Text;
                            break;
                        }
                    default:
                        break;
                }
                using (SqlCommand command = new SqlCommand(queryOborudAdd, connectionINSERT))
                {
                    connectionINSERT.Open();
                    command.ExecuteNonQuery();
                    connectionINSERT.Close();
                }
                AddTab.TabPages[0].PageVisible = false;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            using (SqlConnection connectionUPDATE = new SqlConnection(connectionString))
            {
                string englishName;
                connectionUPDATE.Open();
                if (rus_eng.TryGetValue(e.Column.FieldName, out string engName))
                    englishName = engName;
                else
                    englishName = e.Column.FieldName;
                string sql = $"UPDATE {tableString} SET {englishName} = '{e.Value}' WHERE {fieldsQueryListSQL[0]}  = {gridView1.GetDataRow(e.RowHandle)[0]}";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                connectionUPDATE.Close();
            }
        }

        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControlSprav.MainView as GridView;
            currentRowIndex = gridView.FocusedRowHandle;
            string textCol = gridView.GetFocusedRowCellValue(gridView.Columns[1]).ToString();
            int kodCol = Convert.ToInt32(gridView.GetFocusedRowCellValue(gridView.Columns[0]));
            string message = "Вы уверены что хотите удалить '" + textCol + "' ?";
            var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection connectionDELETE = new SqlConnection(connectionString))
                {
                    string queryOborudArh = $"DELETE FROM {tableString} WHERE {fieldsQueryListSQL[0]} = {kodCol}";
                    using (SqlCommand command = new SqlCommand(queryOborudArh, connectionDELETE))
                    {
                        connectionDELETE.Open();
                        command.ExecuteNonQuery();
                        connectionDELETE.Close();
                    }
                }
            }
        }
    }

}
