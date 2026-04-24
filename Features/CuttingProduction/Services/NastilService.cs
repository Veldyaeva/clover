using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.CardByNom.Services
{
    /// <summary>
    /// Сервис работы с вяз производством.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class NastilService
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
        public NastilService(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region

        public async Task<List<NastilList>> GetNastilView(string _mgKart)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select * from NastilView where mg_kart = '{_mgKart}'";

                    var result = await connection.QueryAsync<NastilList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetNastilView");
                return null;
            }
        }
        
        public async Task<List<NastilGroupView>> GetNastilGroupView(string _mgKart)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select * from NastilVyb_view where mg_kart = '{_mgKart}'";

                    var result = await connection.QueryAsync<NastilGroupView>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных NastilVyb_view");
                return null;
            }
        }
        #endregion

    }
}