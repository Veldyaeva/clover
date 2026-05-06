using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.KnittingProduction.Services
{
    /// <summary>
    /// Сервис работы с вяз производством.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class ProrabotkiService
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
        public ProrabotkiService(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xNn">код матрицы, для которого получаем список проработок</param>
        /// <param name="xNazn">назначение проработки</param>
        /// <returns></returns>
        public async Task<List<ArtPrFioProgr>> GetArtPrFioProgr(string xNn, int xNazn)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select * from ArtPrFioProgrView " +
                                    $"  where kod_matr = '{xNn.Trim()}' " +
                                    $"      and nazn = {xNazn} " +
                                    $"  order by nom_pr";

                    var result = await connection.QueryAsync<ArtPrFioProgr>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных ArtPrFioProgr");
                return null;
            }
        }

        //public async Task<List<ArtPrFioProgr>> GetRecomMachineByKodMatr(string xNn)
        //{
        //    try
        //    {
        //        using (var connection = _dbHelper.GetConnection())
        //        {
        //            string query = $"select * from ArtPrFioProgrView " +
        //                            $"  where kod_matr = '{xNn.Trim()}' " +
        //                            $"      and nazn = {xNazn} " +
        //                            $"  order by nom_pr";

        //            var result = await connection.QueryAsync<ArtPrFioProgr>(query, new Dictionary<string, object> {  });
        //            return result.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при получении данных ArtPrFioProgr");
        //        return null;
        //    }
        //}
        #endregion

    }
}