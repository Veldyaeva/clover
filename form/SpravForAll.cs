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
using DevExpress.XtraGrid.Localization;

namespace SewingProduction.form
{
    public partial class SpravForAll : Form
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
        public SpravForAll(string tableSQL, string rusNameTableSQL)
        {
            GridLocalizer.Active = new RussianGridLocalizer();
            InitializeComponent();
            //Таблица:
            tableString = tableSQL;
            //Имя формы:
            this.Text = rusNameTableSQL;
            //Текст запроса:
            queryList = $"SELECT * FROM " + tableString;
            //Иницилизация листа столбцов:
            fieldsQueryListSQL = new List<string>();
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
                // Получаем имена столбцов и добавляем их в список:
                foreach (System.Data.DataColumn column in tableList.Columns)
                {
                    fieldsQueryListSQL.Add(column.ColumnName); // Добавляем имя столбца
                }
                // Получаем доступ к GridView
                GridView gridView = gridControlSprav.MainView as GridView;
                //Запрет на редактирование
                gridView.OptionsBehavior.Editable = false;
                // Выравнивание столбцов
                gridView.BestFitColumns();
            }
            if (!flagStartListening) 
            {
                // Запуск отслеживания изменений для соединения с базой данных
                SqlDependency.Start(connectionString);
                // Начинаем прослушивание
                StartListening();
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
            GridView gridView = (GridView)gridControlSprav.MainView;
            gridView.AddNewRow();
            //strForAdd = gridView.FocusedRowHandle;
            //gridView.UpdateCurrentRow();
            flagAddDown = true;
        }

        private void gridControlSprav_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControlSprav.MainView as GridView;
            // Сохраняем индекс строки
            currentRowIndex = gridView.FocusedRowHandle;
        }

        private void SpravForAll_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

        private void SpravForAll_Load(object sender, EventArgs e)
        {

        }
    }
}
