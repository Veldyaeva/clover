using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Threading.Tasks;

namespace SewingProduction.Features.CardByNom.Services
{
    /// <summary>
    /// Сервис работы со швейным производством
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class RaskrService
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
        public RaskrService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region
        /// <summary>
        /// Получает информацию по расчету (exec GetRasInfoView) по pach_kod 
        /// </summary>
        /// <param name="_pachKod">pach_kod</param>
        /// <returns></returns>
        public async Task<RasInfoByPachKod> GetRasInfoByPachKod(string pachKod)
        {
            try
            {
                string query = @"exec GetRasInfoView @pachKod";
                return await _dbService.GetEntityAsync<RasInfoByPachKod>(query, new { pachKod });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных RasInfo для PachKod {pachKod}");
                return null;
            }
        }
        #endregion

    }
}