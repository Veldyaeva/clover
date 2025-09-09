using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Z.Dapper;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Dapper;
using DevExpress.Mvvm.Native;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Services;

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
        public async Task<RasInfo> GetRasInfoByPachKod(string pachKod)
        {
            try
            {
                string query = @"exec GetRasInfoView @xPachKod = @pachKod";
                return await _dbService.GetEntityAsync<RasInfo>(query, new { pachKod });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных RasInfo для PachKod {pachKod}");
                return null;
            }
        }
        public async Task<RasInfo> GetRasInfoByNomZad(string nomZad)
        {
            try
            {
                string query = $"exec GetRasInfoView @xNomZadany = '{nomZad}'";
                return await _dbService.GetEntityAsync<RasInfo>(query, new {  });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetRasInfoView для NomZad {nomZad}");
                return null;
            }
        }

        #endregion

    }
}