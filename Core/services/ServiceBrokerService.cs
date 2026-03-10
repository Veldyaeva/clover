using Dapper;
using Org.BouncyCastle.Asn1.Ocsp;
using SewingProduction.Core.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SewingProduction.Core.Models.ServiceBrokerModel;

namespace SewingProduction.Core.services
{
    public class ServiceBrokerService: IDisposable, IAsyncDisposable
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        private bool _disposed;
        private SqlConnection? _connection;
        private SqlCommand? _command;

        public ServiceBrokerService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // освобождаем managed ресурсы
                _command?.Dispose();
                _connection?.Dispose();
            }
            _connection = null;
            _disposed = true;
        }

        ServiceBrokerService()
        { Dispose(false); }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;

            try
            {
                if (_connection != null)
                    await _connection.DisposeAsync();
            }
            finally
            {
                _connection = null;
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }
        //        public async Task<List<TableListenInfo>> GetObjectListForServiceBroker(string _objectName, CancellationToken cancellationToken)
        public async Task<List<ServiceBrokerModel.TableListenInfo>> GetObjectListForServiceBroker(string _objectName, CancellationToken cancellationToken)

        {
            try
            {
                await using var connection = _dbHelper.GetConnection();
                const string query = @"exec dbo.getSQLObjectSource @xObjectName = @objName";
                var command = new CommandDefinition(
                    query, 
                    new { objName = _objectName }, 
                    cancellationToken: cancellationToken);

                var list = (await connection.QueryAsync<ServiceBrokerModel.TableListenInfo>(command)).AsList();

                return list;
            }
            catch (OperationCanceledException ex)
            {
                // отмена — НЕ ошибка
                return new List<TableListenInfo>();
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[ServiceBrokerService] SqlException in getSQLObjectSource: obj={_objectName}, " +
                    $"Number={ex.Number}, State={ex.State}, Class={ex.Class}, Procedure={ex.Procedure}, Line={ex.LineNumber}, " +
                    $"Message={ex.Message}");
                await _logger.LogErrorAsync(
                    ex,
                    $"Ошибка getSQLObjectSource: obj={_objectName}, Number={ex.Number}, State={ex.State}, Procedure={ex.Procedure}, Line={ex.LineNumber}");
                return new List<TableListenInfo>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[ServiceBrokerService] Exception in getSQLObjectSource: obj={_objectName}, {ex}");
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных getSQLObjectSource: obj={_objectName}");
                return new List<TableListenInfo>();
            }
        }
    }
}
