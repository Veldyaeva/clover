using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SewingProduction.form;
using DataTable = System.Data.DataTable;
using static SewingProduction.form.SettingsForm;
using System.Threading.Tasks;


namespace SewingProduction
{
    /// <summary>
    /// Класс для получения обновлений
    /// </summary>
    public class ServiceBroker
    {
        private readonly IDataUpdatableForm _form;
        // ИЗМЕНЕНИЯ СОМТРТСЯ НА ТЕСТОВОЙ БАЗЕ:(удалить коммент после изменения)
        private readonly string _connectionString = Properties.Settings.Default.ACEtestConnectionString;
        private SqlConnection _connection;
        private SqlDependency sqlDependency;
        private bool _flagStartListening = false;
        private string _fields;
        private string _table;
        private bool _brokerStopped = false;
        public ServiceBroker(IDataUpdatableForm form)
        {
            _form = form;
        }
        public void StartBroker()
        {
            try
            {
                // Запуск отслеживания изменений для соединения с базой данных
                SqlDependency.Start(_connectionString);
                Debug.WriteLine($"Broker is Started");
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
        public void StopBroker()
        {
            _brokerStopped = true; // Устанавливаем флаг остановки
            StopListening();
            try
            {
                SqlDependency.Stop(_connectionString);
                Debug.WriteLine("Broker is Stopped");
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error stopping broker: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error stopping broker: {ex.Message}");
            }
        }

        public bool GetFlagStartListening()
        {
            return _flagStartListening;
        }

        public void StartListening(string fields, string table)
        {
            try
            {
                Debug.WriteLine($"Starting SQL Dependency for Table: {table}, Fields: {fields}");
                _fields = fields;
                _table = table;
                _flagStartListening = true;
                // Остановка предыдущего прослушивания, если оно было активно:
                StopListening();
                // SQL-запрос
                string query= $"SELECT {fields} FROM dbo.{table}";
                // Создание соединения с базой данных
                _connection = new SqlConnection(_connectionString);
                // Открытие соединения
                _connection.Open();
                // Создание команды для выполнения SQL-запроса
                SqlCommand command = new SqlCommand(query, _connection);
                // Создание зависимости, чтобы отслеживать изменения
                sqlDependency = new SqlDependency(command);
                // Подписка на событие изменения
                sqlDependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
                // Выполнение команды
                command.ExecuteReader(CommandBehavior.CloseConnection);
                Debug.WriteLine($"Listening from broker is Started");
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

        public bool StopListening()
        {
            _flagStartListening = false;
            // Закрываем подключение
            if (_connection != null)
            {
                try
                {
                    _connection.Close();
                    _connection.Dispose();
                    Debug.WriteLine("Broker is stopped");
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error closing connection: {ex.Message}");
                    return true; // Сообщаем о проблеме
                }
            }
            Debug.WriteLine($"Broker job");
            return true;
        }
        private async void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            Debug.WriteLine($"Notification received: Type={e.Type}, Info={e.Info}, Source={e.Source}");

            if (e.Type == SqlNotificationType.Change)
            {
                switch (e.Info)
                {
                    case SqlNotificationInfo.Insert:
                    case SqlNotificationInfo.Update:
                    case SqlNotificationInfo.Delete:
                        Debug.WriteLine("Data was changed");
                        try
                        {
                            ((Form)_form).Invoke((MethodInvoker)delegate
                            {
                                try
                                {
                                    _form.UpdateDataInForm();
                                }
                                catch (Exception formEx)
                                {
                                    Debug.WriteLine($"Error updating form: {formEx.Message}");
                                    // Обработка ошибок при обновлении формы
                                }
                            });
                        }
                        catch (Exception invokeEx)
                        {
                            Debug.WriteLine($"Error invoking update on form: {invokeEx.Message}");
                            // Обработка ошибок при вызове Invoke
                        }
                        break;

                    case SqlNotificationInfo.Invalid:
                        Debug.WriteLine("Notification is invalid, restarting listener.");
                        break;

                    case SqlNotificationInfo.Error:
                        Debug.WriteLine("Notification error, check SQL Server configuration.");
                        break;

                    default:
                        Debug.WriteLine($"Unknown notification info: {e.Info}");
                        break;
                }
            }
            else if (e.Type == SqlNotificationType.Subscribe)
            {
                switch (e.Source)
                {
                    case SqlNotificationSource.Timeout:
                        Debug.WriteLine("Subscription timed out, restarting listener.");
                        break;
                    case SqlNotificationSource.Statement:
                        Debug.WriteLine("Statement executed successfully.");
                        break;
                    case SqlNotificationSource.Client:
                        Debug.WriteLine("Client initiated notification.");
                        break;
                    default:
                        Debug.WriteLine($"Unknown notification source: {e.Source}");
                        break;
                }
            }
            else
            {
                Debug.WriteLine($"Unexpected notification type: {e.Type}");
            }
            // Проверяем флаг и brokerStopped перед перезапуском прослушивания
            if (_flagStartListening && !_brokerStopped)
            {
                // Задержка перед перезапуском (можно сделать экспоненциальную)
                await Task.Delay(TimeSpan.FromSeconds(5));
                StartListening(_fields, _table);
            }
            else
            {
                Debug.WriteLine("Not restarting listener because it was stopped.");
            }
        }
    }
}
