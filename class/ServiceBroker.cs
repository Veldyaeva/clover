using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SewingProduction.form;
using DataTable = System.Data.DataTable;


namespace SewingProduction
{
    public class ServiceBroker
    {
        private readonly DatabaseHelper _dbHelper;
        string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        private SqlConnection connection;
        private SqlDependency sqlDependency;
        bool flagStartListening = false;
        string _fields;
        string _table;
        public ServiceBroker(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void StartListening(string fields, string table)
        {
            try
            {
                _fields = fields;
                _table = table;
                flagStartListening = true;
                // Остановка предыдущего прослушивания, если оно было активно:
                StopListening();
                // SQL-запрос
                string query= $"SELECT {fields} FROM {table}";
                // Создание соединения с базой данных
                connection = new SqlConnection(connectionString);
                // Открытие соединения
                connection.Open();
                // Создание команды для выполнения SQL-запроса
                SqlCommand command = new SqlCommand(query, connection);
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
                //if (this.IsHandleCreated)
                //    this.Invoke((MethodInvoker)delegate
                //    {
                //        // Обновили таблицу
                //        //gridOborud_Load(sender, e);
                //    });

            }
            // Возобновляем прослушивание
            StartListening(_fields, _table);
        }
    }
}
