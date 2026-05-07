using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Core.Services
{
    /// <summary>
    /// Сервис работы с матрицей
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class MatrixService
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
        public MatrixService(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region
        // Получение связанных данных
        /// <summary>
        /// Получает данные из View_plan_sezon_otdelka по pach_kod
        /// </summary>
        /// <param name="pachKod">pach_kod</param>
        /// <returns></returns>
        public async Task<List<PlanSezonOtdelkaView>> GetPlanSezonOtdelkaViewByPachKod(string pachKod)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var xPachKod = pachKod + "%";
                    string query = $"select * " +
                                    $"  from View_plan_sezon_otdelka " +
                                    $"  where nn = (SELECT nn from plan_sezon_zad WHERE nom = (SELECT nom_zad FROM raskr_zeh_up WHERE pach_kod LIKE @pachKod)) ";

                    var result = await connection.QueryAsync<PlanSezonOtdelkaView>(query, new Dictionary<string, object> { { "@pachKod", xPachKod } });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных View_plan_sezon_otdelka для pach_kod {pachKod}");
                return null;
            }
        }

        public async Task<List<PlanSezonZadanyView>> GetPlanSezonZadanyRazmKolByNomZad(string nomZad)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"SELECT * FROM view_plan_sezon_zadany vpsz WHERE nom = '{nomZad}' ";

                    var result = await connection.QueryAsync<PlanSezonZadanyView>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных view_plan_sezon_zadany для nom {nomZad}");
                return null;
            }
        }

        ///// <summary>
        ///// Получает данные из furnit_pach по kod_f_d
        ///// </summary>
        ///// <param name="kodFD">pach_kod</param>
        ///// <returns></returns>
        //public async Task<List<FurnitPach>> GetFurnitPachByKodFD(string kodFD)
        //{
        //    using (var connection = _dbHelper.GetConnection())
        //    {
        //        string query = $"select * " +
        //                        $"  from furnit_pach " +
        //                        $"  where kod_f_d = @kodFD' " +
        //                        $"  order by pach_kod";

        //        var result = await connection.QueryAsync<FurnitPach>(query, new Dictionary<string, object> { { "@kodFD", kodFD } });
        //        return result.ToList();
        //    }
        //}

        ///// <summary>
        ///// Получает данные из furnit_f по kod_f_d
        ///// </summary>
        ///// <param name="kodFD">pach_kod</param>
        ///// <returns></returns>
        //public async Task<List<FurnitF>> GetFurnitFByKodFD(string kodFD)
        //{
        //    using (var connection = _dbHelper.GetConnection())
        //    {
        //        string query = $"select * " +
        //                        $"  from furnit_f " +
        //                        $"  where kod_f_d = @kodFD' " +
        //                        $"  order by n_pp";

        //        var result = await connection.QueryAsync<FurnitF>(query, new Dictionary<string, object> { { "@kodFD", kodFD } });
        //        return result.ToList();
        //    }
        //}

        ///// <summary>
        ///// Получает данные из furnit_f_it по kod_f_d
        ///// </summary>
        ///// <param name="kodFD">pach_kod</param>
        ///// <returns></returns>
        //public async Task<List<FurnitFIt>> GetFurnitFItByKodFD(string kodFD)
        //{
        //    using (var connection = _dbHelper.GetConnection())
        //    {
        //        string query = $"select * " +
        //                        $"  from furnit_f_it " +
        //                        $"  where kod_f_d = @kodFD' " +
        //                        $"  order by n_pp, ffiid";

        //        var result = await connection.QueryAsync<FurnitFIt>(query, new Dictionary<string, object> { { "@kodFD", kodFD } });
        //        return result.ToList();
        //    }
        //}

        ///// <summary>
        ///// Получает данные из таблицы NaklView по pach_kod 
        ///// </summary>
        ///// <param pachKod="_pachKod">pach_kod</param>
        ///// <returns></returns>
        //public async Task<List<HistoryRazdelNaklViewByIz>> GetHistoryRazdelNaklViewByIz(string iz)
        //{
        //    using (var connection = _dbHelper.GetConnection())
        //    {
        //        //var xPachKod = pachKod + "%";
        //        //string query = $"select * from NaklView where nom = (select nom from raskr_zeh_up where left(pach_kod,10) = '{pachKod}' )";
        //        string query = "";
        //        if (iz.Length == 0 || iz == null)
        //        {
        //            query = $"SELECT top 0 * FROM View_History_razdel_nakl order by id";
        //        }
        //        else
        //        {
        //            query = $"SELECT * FROM View_History_razdel_nakl where iz_b = '{iz}' order by id";
        //        }
        //        //return await _dbHelper.GetConnection().QueryAsync<NaklViewByPachKod>(query).ContinueWith(t => t.Result.ToList());
        //        //string query = "UPDATE sp_articul SET annId = @annId WHERE kod like @kod";
        //        //return await _dbService.GetEntityAsync<NaklViewByPachKod>(query, new { pachKod });
        //        var result = await connection.QueryAsync<HistoryRazdelNaklViewByIz>(query, new Dictionary<string, object> { { "@pachKod", iz } });
        //        return result.ToList();
        //    }
        //}
        ///// <summary>
        ///// Получает информацию по расчету (exec GetRasInfoView) по pach_kod 
        ///// </summary>
        ///// <param name="_pachKod">pach_kod</param>
        ///// <returns></returns>

        //public async Task<RasInfoByPachKod> GetRasInfoByPachKod(string pachKod)
        //{
        //    try
        //    {
        //        string query = @"exec GetRasInfoView @pachKod";
        //        return await _dbService.GetEntityAsync<RasInfoByPachKod>(query, new { pachKod });
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при получении данных RasInfo для PachKod {pachKod}");
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Получает информацию по заявкам на фурнитуру/упаковку по pach_kod 
        ///// </summary>
        ///// <param name="_pachKod">pach_kod</param>
        ///// <returns></returns>
        //public async Task<FurnitZayavCheckByPachKod> GetFurnitZayavCheckByPachKod(string pachKod)
        //{
        //    try
        //    {
        //        string query = @"exec furnitZayavCheck @pachKod, 1";
        //        return await _dbService.GetEntityAsync<FurnitZayavCheckByPachKod>(query, new { pachKod });
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при получении данных FurnitZayavCheck для PachKod {pachKod}");
        //        return null;
        //    }
        //}
        #endregion

        public async Task<string> GetFileEskizNN(string nn)
        {
            try
            {
                return @"h:\proizv\eskiz\"+nn+".jpg";
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetFileEskizNN");
                return null;
            }

        }


    }
}