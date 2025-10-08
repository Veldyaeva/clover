using Dapper;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Services
{
    /// <summary>
    /// Сервис работы с вяз производством.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class MlService
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
        public MlService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region
        //public async Task<List<Ml>> GetMlByMg(string _mg)
        //{
        //    try
        //    {
        //        using (var connection = _dbHelper.GetConnection())
        //        {
        //            string query = $"select * from ml where mg ={ _mg } ";

        //            var result = await connection.QueryAsync<Ml>(query, new Dictionary<string, object> { });
        //            return result.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMlByMg");
        //        return null;
        //    }
        //}

        //public async Task<List<MlOp>> GetMlOpByNomAndNrID(int _nom, int _nrID)
        //{
        //    try
        //    {
        //        using (var connection = _dbHelper.GetConnection())
        //        {
        //            string query = $"select * from ml_op where nomr = {_nom} and nrID = {_nrID} ";

        //            var result = await connection.QueryAsync<MlOp>(query, new Dictionary<string, object> { });
        //            return result.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetSmenZadanyVyazMachine");
        //        return null;
        //    }
        //}

        #endregion

    }
}