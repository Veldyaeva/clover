using Dapper;
using Org.BouncyCastle.Asn1.Ocsp;
using SewingProduction.Core.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SewingProduction.Core.Models.ServiceBrokerModel;

namespace SewingProduction.Core.services
{
    public class ServiceBrokerService
    {
        private static DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        public ServiceBrokerService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
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
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetPlanZagrVyazByPachList");
                return new List<TableListenInfo>();
            }
        }
    }
}
