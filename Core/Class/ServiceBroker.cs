using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
//using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SewingProduction.form;
using DataTable = System.Data.DataTable;
using System.Threading.Tasks;
using SewingProduction.Core.interfaces;

namespace SewingProduction
{
    /// <summary>
    /// Класс для получения обновлений
    /// </summary>
    public class ServiceBroker
    {
        private readonly object _form;
        // Для теста:
        // private readonly string _connectionString = Properties.Settings.Default.ACEtestConnectionString;
        private readonly string _connectionString = Properties.Settings.Default.ACEConnectionString;

        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDependency _dependency;

        private bool _flagStartListening = false;
        private bool _brokerStopped = false;

        private string _fields;
        private string _table;
        public ServiceBroker(object form)
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

        public bool GetFlagStartListening() => _flagStartListening;

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
                string query= $"SELECT {_fields} FROM dbo.{table}";
                // Создание соединения с базой данных
                _connection = new SqlConnection(_connectionString);
                // Открытие соединения
                _connection.Open();
                // Создание команды для выполнения SQL-запроса
                _command = new SqlCommand(query, _connection);
                // Dependency
                _dependency = new SqlDependency(_command);
                _dependency.OnChange += OnDependencyChange;

                // Выполнение команды
                _command.ExecuteReader(CommandBehavior.CloseConnection);
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
            try 
            { 
                //_flagStartListening = false;
                // Закрываем подключение
                if (_dependency != null)
                {
                    _dependency.OnChange -= OnDependencyChange;
                    _dependency = null;
                    Debug.WriteLine("SqlDependency unsubscribed.");
                }
                if (_command != null)
                {
                    _command.Dispose();
                    _command = null;
                    Debug.WriteLine("SqlCommand disposed.");
                }
                if (_connection != null)
                {
                    _connection.Close();
                    _connection.Dispose();
                    _connection = null;
                    Debug.WriteLine("SqlConnection closed.");
                }
                Debug.WriteLine($"Broker job");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during StopListening: {ex.Message}");
                return false;
            }
        }
        private async void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            Debug.WriteLine($"Notification received: Table= {_table}, Type={e.Type}, Info={e.Info}, Source={e.Source}");

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
                            if (_form is Form winForm)
                            {
                                await winForm.InvokeAsync(async () =>
                                {
                                    try
                                    {
                                        Debug.WriteLine("Calling UpdateDataInForm...");

                                        if (_form is IDataUpdatableFormAsync asyncForm)
                                        {
                                            await asyncForm.UpdateDataInFormAsync(_table);
                                            Debug.WriteLine("Async form updated." + _table);
                                        }
                                        if (_form is IDataUpdatableForm syncForm)
                                        {
                                            syncForm.UpdateDataInForm(_table);
                                            Debug.WriteLine("Sync form updated." + _table);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine($"Ошибка при обновлении формы: {ex.Message}");
                                    }
                                    finally
                                    {
                                        // ⬅️ Перезапускаем внутри UI потока, после обновления данных
                                        if (_flagStartListening && !_brokerStopped)
                                        {
                                            Debug.WriteLine("Restarting listener after update.");
                                            StartListening(_fields, _table);
                                        }
                                        else
                                        {
                                            Debug.WriteLine("Listener not restarted (stopped or disabled).");
                                        }
                                    }
                                });
                            }
                        }
                        catch (Exception invokeEx)
                        {
                            Debug.WriteLine($"Error invoking update on form: {invokeEx.Message}");
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
        }
    }
}
