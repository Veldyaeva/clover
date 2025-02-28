using DevExpress.XtraCharts.Native;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DatabaseHelper(string _serv)
        {
            switch (_serv.ToLower())
            {
                case "ace": _connectionString = SewingProduction.Properties.Settings.Default.ACEConnectionString; break;
                case "oms": _connectionString = SewingProduction.Properties.Settings.Default.OMSConnectionString; break;
                case "global": _connectionString = SewingProduction.Properties.Settings.Default.GlobalConnectionString; break;
            }
            _globalConnectionString = _connectionString;
        }

        public static string GetGlobalConnectionString()
        {
            if (string.IsNullOrEmpty(_globalConnectionString))
            {
                throw new InvalidOperationException("Строка подключения ещё не установлена.");
            }
            return _globalConnectionString;
        }

        /// <summary>
        /// Выполнение SQL запроса, возвращает dataTable
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="parameters">параметры</param>
        /// <returns>DataTable</returns>
        public async Task<DataTable> ExecuteQueryAsync(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                        DataTable table = new DataTable();
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

        public async Task<int> ExecuteScalar(string query, Dictionary<string, object> parameters = null)
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
    }
}
#endregion

