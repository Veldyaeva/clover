//using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using SewingProduction.Core.Class.Settings;

namespace SewingProduction.Helpers
{
    #region DbHelper
    /// <summary>
    /// класс для работы с БД
    /// </summary>
    public class DatabaseHelper
    {
        private readonly string _connectionString;
        private static string _globalConnectionString;
        private SqlTransaction _currentTransaction;
        private SqlConnection _currentConnection;

        public DatabaseHelper(string _serv)
        {
            switch (_serv.ToLower())
            {
                case "ace":
                case "aceconnectionstring":
                    _connectionString = SewingProduction.Properties.Settings.Default.ACEConnectionString;
                    break;
                case "ace_test":
                case "acetestconnectionstring":
                    _connectionString = SewingProduction.Properties.Settings.Default.ACEtestConnectionString;
                    break;
                case "ace_backup":
                case "acebackupconnectionstring":
                    _connectionString = SewingProduction.Properties.Settings.Default.ACEbackupConnectionString;
                    break;
                case "ace_backup_new":
                case "acebackupnewconnectionstring":
                    _connectionString = SewingProduction.Properties.Settings.Default.ACEbackupnewConnectionString;
                    break;
                case "global":
                case "globalconnectionstring":
                    _connectionString = SewingProduction.Properties.Settings.Default.GlobalConnectionString;
                    break;
                case "oms":
                case "omsconnectionstring":
                    _connectionString = SewingProduction.Properties.Settings.Default.OMSConnectionString;
                    break;
                default:
                    _connectionString = SewingProduction.Properties.Settings.Default.ACEConnectionString;
                    break;
            }

            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new InvalidOperationException("Строка подключения не инициализирована");

            _globalConnectionString = _connectionString;
        }
        public DatabaseHelper() : this(SettingsManager.GetSelectedDatabase())
        {
        }
        public static string GetGlobalConnectionString()
        {
            if (string.IsNullOrEmpty(_globalConnectionString))
            {
                throw new InvalidOperationException("Строка подключения ещё не установлена.");
            }
            return _globalConnectionString;
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);

            // Открываем соединение сразу, чтобы вызывающий код мог выполнять bulk-операции без задержек
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        #region async
        /// <summary>
        /// Выполнение SQL запроса, возвращает dataTable
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="parameters">параметры</param>
        /// <returns>DataTable</returns>
        public async Task<DataTable> ExecuteQueryAsync(string query, Dictionary<string, object> parameters = null, CommandType type = CommandType.Text)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    DataTable table = new DataTable();
                    command.CommandType = type;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    await connection.OpenAsync(); // Асинхронное подключение к БД
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        /// <summary>
        /// Выполнение SQL запроса
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="parameters">параметры</param>
        public async Task ExecuteNonQueryAsync(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Выполнение SQL запроса с возвратом количества затронутых строк
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="parameters">параметры</param>
        /// <returns>Количество затронутых строк</returns>
        public async Task<int> ExecuteNonQueryWithRowCountAsync(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        /// <summary>
        /// Выполняет запрос и возвращает число в результате 
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="parameters">Список параметров</param>
        /// <returns></returns>
        public async Task<int> ExecuteScalarAsync(string query, Dictionary<string, object> parameters = null)
        {
            int res = -1;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    object result = await command.ExecuteScalarAsync();
                    if (result != null) { res = Convert.ToInt32(result); }
                }
            }
            return res;
        }
        public async Task<T> ExecuteScalarAsync<T>(string query, object parameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.ExecuteScalarAsync(query, parameters);

                    if (result == null || result == DBNull.Value)
                        return default;
                    if (typeof(T).IsEnum)
                        return (T)Enum.Parse(typeof(T), result.ToString());

                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, $"Ошибка при ExecuteScalarAsync<{typeof(T).Name}>: {query}");
                throw;
            }
        }
        /// <summary>
        /// Транзакцию нельзя использовать через using, потому что using закрывает и уничтожает соединение сразу после выхода из блока,
        /// и возвращает транзакцию, которая привязана к уже закрытому соединению
        /// </summary>
        /// <returns></returns>
        public async Task<SqlTransaction> BeginTransactionAsync()
        {
            _currentConnection = new SqlConnection(_connectionString);

            if (_currentConnection.State != ConnectionState.Open)
            {
                await _currentConnection.OpenAsync();
            }

            _currentTransaction = _currentConnection.BeginTransaction();
            return _currentTransaction;
        }
        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Commit();
                _currentConnection.Close();
                _currentTransaction = null;
                _currentConnection = null;
            }
        }
        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Rollback();
                _currentConnection.Close();
                _currentTransaction = null;
                _currentConnection = null;
            }
        }

        public async Task ExecuteInTransactionAsync(Func<Task> operation)
        {
            await BeginTransactionAsync();
            try
            {
                await operation.Invoke();
                await CommitTransactionAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        #endregion

        #region sync
        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            var dt = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// Выполнение SQL запроса
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="parameters">параметры</param>
        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Выполнение SQL запроса с возвратом количества затронутых строк
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="parameters">параметры</param>
        /// <returns>Количество затронутых строк</returns>
        public int ExecuteNonQueryWithRowCount(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteScalar(string query, Dictionary<string, object> parameters = null)
        {
            int res = -1;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    object result = command.ExecuteScalar();
                    if (result != null) { res = Convert.ToInt32(result); }
                }
            }
            return res;
        }
        /// <summary>
        /// Проверяет, возвращает ли запрос хотя бы одну строку.
        /// </summary>
        /// <param name="query">SQL-запрос</param>
        /// <param name="parameters">Параметры</param>
        /// <returns>true, если есть хотя бы одна строка; иначе false</returns>
        public bool Exists(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
        #endregion
    }
}
#endregion

