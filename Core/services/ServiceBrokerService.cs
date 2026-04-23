using Dapper;
using Org.BouncyCastle.Asn1.Ocsp;
using SewingProduction.Core.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
        private sealed class ListenInfoCacheEntry
        {
            public required List<ServiceBrokerModel.TableListenInfo> Value { get; init; }
            public required DateTime CachedAtUtc { get; init; }
        }

        private static readonly ConcurrentDictionary<string, ListenInfoCacheEntry> _listenInfoCache =
            new(StringComparer.OrdinalIgnoreCase);
        private static readonly TimeSpan ListenInfoCacheLifetime = TimeSpan.FromMinutes(15);
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly DbService _dbService;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        private bool _disposed;
        private SqlConnection? _connection;
        private SqlCommand? _command;

        public ServiceBrokerService(DatabaseHelperSQL dbHelper)
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
                var cacheKey = $"{DatabaseHelperSQL.GetGlobalConnectionString()}|{_objectName}";
                if (_listenInfoCache.TryGetValue(cacheKey, out var cached) &&
                    (DateTime.UtcNow - cached.CachedAtUtc) <= ListenInfoCacheLifetime)
                {
                    return cached.Value.Select(CloneListenInfo).ToList();
                }

                await using var connection = _dbHelper.GetConnection();
                const string query = @"exec dbo.getSQLObjectSource @xObjectName = @objName";
                var command = new CommandDefinition(
                    query, 
                    new { objName = _objectName }, 
                    cancellationToken: cancellationToken);

                var list = (await connection.QueryAsync<ServiceBrokerModel.TableListenInfo>(command)).AsList();
                _listenInfoCache[cacheKey] = new ListenInfoCacheEntry
                {
                    Value = list.Select(CloneListenInfo).ToList(),
                    CachedAtUtc = DateTime.UtcNow
                };

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

        private static ServiceBrokerModel.TableListenInfo CloneListenInfo(ServiceBrokerModel.TableListenInfo item)
        {
            return new ServiceBrokerModel.TableListenInfo
            {
                ObjectName = item.ObjectName,
                TableSchema = item.TableSchema,
                TableName = item.TableName,
                TableFieldList = item.TableFieldList
            };
        }
    }
}
