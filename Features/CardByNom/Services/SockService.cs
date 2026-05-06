using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.CardByNom.Services
{
    /// <summary>
    /// Сервис работы с вяз производством.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class SockService
    {
        private static DatabaseHelperSQL _dbHelper;
        private readonly DbService _dbService;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        #region
        /// <summary>
        /// Инициализирует новый экземпляр dbService.
        /// </summary>
        /// <param name="dbHelper">Помощник для работы с базой данных.</param>
        public SockService(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region

        public async Task<List<SockZadanyInfo>> GetSockKnitZadanyInfo(string nomZad)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"exec dbo.knitZadany_info '{nomZad}'";

                    var result = await connection.QueryAsync<SockZadanyInfo>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных SockZadanySmenList");
                return null;
            }
        }


        public async Task<List<SockZadanySmenList>> GetSockZadanySmenListByNomZad(string nomZad)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"exec GetKnitZadanyBySmen_view @xNomZad = '{nomZad}'";
                    var result = await connection.QueryAsync<SockZadanySmenList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных SockZadanySmenList");
                return null;
            }
        }
        public async Task<List<SockServiceList>> GetSockServiceListByNomZad(string nomZad)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select * from KnitMashineServiceByNomZad where kzPszNom = '{nomZad}'";
                    var result = await connection.QueryAsync<SockServiceList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных SockServiceList");
                return null;
            }
        }

        public async Task<List<SockDownTimeList>> GetSockDownTimeListByNomZad(string nomZad)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select * from KnitMachineDownTimeByNomZad where kzPszNom = '{nomZad}'";
                    var result = await connection.QueryAsync<SockDownTimeList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных SockDownTimeList");
                return null;
            }
        }
        public async Task<List<SockDefectList>> GetSockDefectListByNomZad(string nomZad)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"SELECT * FROM vSpisPryzDefect_view WHERE nom_zadany = '{nomZad}'";
                    var result = await connection.QueryAsync<SockDefectList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных SockDefectList");
                return null;
            }
        }
        #endregion

    }
}