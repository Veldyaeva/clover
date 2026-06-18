//using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using SewingProduction.Core.Class.Settings;

namespace SewingProduction.Helpers
{
    #region DbHelper
    /// <summary>
    /// класс для работы с БД
    /// </summary>
    public class DatabaseHelperSQL
    {
        private readonly string _connectionString;
        private static string _globalConnectionString;

        // AsyncLocal изолирует транзакцию в пределах одной async-цепочки.
        // Экземпляр DatabaseHelperSQL может быть Singleton — гонок нет.
        private static readonly AsyncLocal<AmbientState?> _ambient = new AsyncLocal<AmbientState?>();

        private sealed class AmbientState
        {
            public SqlConnection Connection { get; }
            public SqlTransaction Transaction { get; }
            public AmbientState(SqlConnection conn, SqlTransaction tx) { Connection = conn; Transaction = tx; }
        }

        public DatabaseHelperSQL(string _serv)
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
                //case "cleverPG":
                //case "cleverPGconnectionstring":
                //    _connectionString = SewingProduction.Properties.Settings.Default.CleverPGConnectionString;
                //    break;
                //case "cleverPG":
                //case "cleverPGconnectionstring":
                //    _connectionString = SewingProduction.Properties.Settings.Default.CleverPGConnectionString;
                //    break;
                default:
                    _connectionString = SewingProduction.Properties.Settings.Default.ACEConnectionString;
                    break;
            }

            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new InvalidOperationException("Строка подключения не инициализирована");

            _globalConnectionString = _connectionString;
        }
        public DatabaseHelperSQL() : this(SettingsManager.GetSelectedDatabase())
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
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public async Task<SqlConnection> GetConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        private static bool HasActiveTransaction
        {
            get
            {
                var s = _ambient.Value;
                return s != null && s.Connection?.State == ConnectionState.Open;
            }
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
            var ambient = _ambient.Value;
            var useAmbientTransaction = ambient != null && ambient.Connection?.State == ConnectionState.Open;
            var connection = useAmbientTransaction ? ambient!.Connection : new SqlConnection(_connectionString);
            try
            {
                using (var command = new SqlCommand(query, connection))
                {
                    DataTable table = new DataTable();
                    command.CommandType = type;
                    if (useAmbientTransaction)
                        command.Transaction = ambient!.Transaction;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    if (!useAmbientTransaction)
                        await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                        return table;
                    }
                }
            }
            finally
            {
                if (!useAmbientTransaction)
                    connection.Dispose();
            }
        }

        /// <summary>
        /// Выполнение SQL запроса
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="parameters">параметры</param>
        public async Task ExecuteNonQueryAsync(string query, Dictionary<string, object> parameters = null)
        {
            var ambient = _ambient.Value;
            var useAmbientTransaction = ambient != null && ambient.Connection?.State == ConnectionState.Open;
            var connection = useAmbientTransaction ? ambient!.Connection : new SqlConnection(_connectionString);
            try
            {
                if (!useAmbientTransaction)
                    await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    if (useAmbientTransaction)
                        command.Transaction = ambient!.Transaction;
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
            finally
            {
                if (!useAmbientTransaction)
                    connection.Dispose();
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
            var ambient = _ambient.Value;
            var useAmbientTransaction = ambient != null && ambient.Connection?.State == ConnectionState.Open;
            var connection = useAmbientTransaction ? ambient!.Connection : new SqlConnection(_connectionString);
            try
            {
                if (!useAmbientTransaction)
                    await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    if (useAmbientTransaction)
                        command.Transaction = ambient!.Transaction;
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
            finally
            {
                if (!useAmbientTransaction)
                    connection.Dispose();
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
            var ambient = _ambient.Value;
            var useAmbientTransaction = ambient != null && ambient.Connection?.State == ConnectionState.Open;
            var connection = useAmbientTransaction ? ambient!.Connection : new SqlConnection(_connectionString);
            try
            {
                if (!useAmbientTransaction)
                    await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    if (useAmbientTransaction)
                        command.Transaction = ambient!.Transaction;
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
            finally
            {
                if (!useAmbientTransaction)
                    connection.Dispose();
            }
            return res;
        }
        public async Task<T> ExecuteScalarAsync<T>(string query, object parameters = null)
        {
            try
            {
                var ambient = _ambient.Value;
                var useAmbientTransaction = ambient != null && ambient.Connection?.State == ConnectionState.Open;
                var connection = useAmbientTransaction ? ambient!.Connection : new SqlConnection(_connectionString);
                try
                {
                    if (!useAmbientTransaction)
                        await connection.OpenAsync();
                    var result = await connection.ExecuteScalarAsync(query, parameters, transaction: useAmbientTransaction ? ambient!.Transaction : null);

                    if (result == null || result == DBNull.Value)
                        return default;
                    if (typeof(T).IsEnum)
                        return (T)Enum.Parse(typeof(T), result.ToString());

                    return (T)Convert.ChangeType(result, typeof(T));
                }
                finally
                {
                    if (!useAmbientTransaction)
                        connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                 // _logger.LogError(ex, $"Ошибка при ExecuteScalarAsync<{typeof(T).Name}>: {query}");
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
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var transaction = connection.BeginTransaction();
            _ambient.Value = new AmbientState(connection, transaction);
            return transaction;
        }

        public Task CommitTransactionAsync()
        {
            var state = _ambient.Value;
            if (state != null)
            {
                try { state.Transaction.Commit(); }
                finally
                {
                    state.Transaction.Dispose();
                    state.Connection.Dispose();
                    _ambient.Value = null;
                }
            }
            return Task.CompletedTask;
        }

        public Task RollbackTransactionAsync()
        {
            var state = _ambient.Value;
            if (state != null)
            {
                try { state.Transaction.Rollback(); }
                finally
                {
                    state.Transaction.Dispose();
                    state.Connection.Dispose();
                    _ambient.Value = null;
                }
            }
            return Task.CompletedTask;
        }

        public async Task ExecuteInTransactionAsync(Func<Task> operation)
        {
            await BeginTransactionAsync();
            try
            {
                await operation.Invoke();
                await CommitTransactionAsync();
            }
            catch (SqlException)
            {
                await RollbackTransactionAsync();
                throw;
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
        public T ExecuteScalar<T>(string query, Dictionary<string, object> parameters = null)
        {
            object result;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    result = command.ExecuteScalar();
                }
            }

            if (result == null || result == DBNull.Value)
                return default;

            if (result is T t)
                return t;

            try
            {
                return (T)System.Convert.ChangeType(result, typeof(T));
            }
            catch
            {
                return default;
            }
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

