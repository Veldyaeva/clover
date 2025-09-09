using Dapper;
using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Mvvm.Native;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper;
using DataTable = System.Data.DataTable;

namespace SewingProduction.Features.CardByNom.Services
{
    /// <summary>
    /// Сервис работы с вяз производством.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class SockService
    {
        private static DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        #region
        /// <summary>
        /// Инициализирует новый экземпляр dbService.
        /// </summary>
        /// <param name="dbHelper">Помощник для работы с базой данных.</param>
        public SockService(DatabaseHelper dbHelper)
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
                    string query = $"WITH OrderedData AS (" +
                        $"SELECT * " +
                        $"      ,LEAD(kzDateAdd) OVER(ORDER BY kzDateAdd) AS NextKzDateAdd " +
                        $"  FROM knitZadany_view " +
                        $"  WHERE kzPszNom = '{nomZad}'" +
                        $")" +
                        $"SELECT *," +
                        $"  CASE " +        //Количество дней(целое число)
                        $"      WHEN kzDateEnd IS NOT NULL AND NextKzDateAdd IS NOT NULL" +
                        $"      THEN DATEDIFF(SECOND, kzDateEnd, NextKzDateAdd) / 86400 " +
                        $"      ELSE 0" +
                        $"  END AS DaysDiff," +
                        $"  CASE " +        //Оставшееся время(тип TIME)
                        $"      WHEN kzDateEnd IS NOT NULL AND NextKzDateAdd IS NOT NULL " +
                        $"      THEN CAST(DATEADD(SECOND, DATEDIFF(SECOND, kzDateEnd, NextKzDateAdd) % 86400, '19000101') AS TIME) " +
                        $"      ELSE CAST('00:00:00' AS TIME) " +
                        $"  END AS TimeDiff " +
                        $"FROM OrderedData " +
                        $"ORDER BY kzDateAdd";
                    var result = await connection.QueryAsync<SockZadanySmenList>(query, new Dictionary<string, object> {  });
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
                    var result = await connection.QueryAsync<SockServiceList>(query, new Dictionary<string, object> {  });
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