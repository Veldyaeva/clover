using SewingProduction.Helpers;
using SewingProduction.Models;
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
using DevExpress.Pdf.Native;
using SewingProduction.Services;

namespace SewingProduction.Features.Furnit.Services
{
    /// <summary>
    /// Сервис работы с фурнитурой/упаковкой
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class FurnitService
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
        public FurnitService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region
        /// <summary>
        /// Получает заголовок заявки из furnit_n по kod_f 
        /// </summary>
        /// <param name="kodF">pach_kod</param>
        /// <returns></returns>
        public async Task<FurnitNView> GetFurnitNViewByKodF(string kodF)
        {
            try
            {
                string query = "";
                if (kodF != "")
                {
                    query = $"select * " +
                                $"  from furnitNView " +
                                $"  where kod_f = @kodF ";
                }
                else
                {
                    query = $"select top 0 * " +
                                $"  from furnitNView " +
                                $"  where kod_f = @kodF ";
                }
                
                return await _dbService.GetEntityAsync<FurnitNView>(query, new { kodF });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных furnitNView для kod_f {kodF}");
                return null;
            }
        }


        // Получение связанных данных
        /// <summary>
        /// Получает данные из FurnitArtView по kod_f
        /// </summary>
        /// <param name="kodF">pach_kod</param>
        /// <returns></returns>
        public async Task<List<FurnitArtView>> GetFurnitArtViewByKodF(string kodF)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "";
                    if (kodF != "")
                    {
                        query = $"select * " +
                                    $"  from FurnitArtView " +
                                    $"  where kod_f = @kodF " +
                                    $"  order by kod_f_d ";
                    }
                    else
                    {
                        query = $"select top 0 * " +
                                    $"  from FurnitArtView " +
                                    $"  where kod_f = @kodF " +
                                    $"  order by kod_f_d ";
                    }

                    var result = await connection.QueryAsync<FurnitArtView>(query, new Dictionary<string, object> { { "@kodF", kodF } });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных FurnitArtView для kod_f {kodF}");
                return null;
            }
        }

        /// <summary>
        /// Получает данные из furnit_pach по kod_f_d
        /// </summary>
        /// <param name="kodFD">pach_kod</param>
        /// <returns></returns>
        public async Task<List<FurnitPach>> GetFurnitPachByKodFD(string kodFD)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query;
                if (kodFD != "")
                {
                    query = $"select * " +
                                $"  from furnit_pach " +
                                $"  where kod_f_d = @kodFD " +
                                $"  order by pach_kod ";
                }
                else
                {
                    query = $"select top 0 * " +
                                $"  from furnit_pach " +
                                $"  where kod_f_d = @kodFD " +
                                $"  order by pach_kod ";
                }

                var result = await connection.QueryAsync<FurnitPach>(query, new Dictionary<string, object> { { "@kodFD", kodFD } });
                return result.ToList();
            }
        }
        
        /// <summary>
        /// Получает данные из furnit_f по kod_f_d
        /// </summary>
        /// <param name="kodFD">pach_kod</param>
        /// <returns></returns>
        public async Task<List<FurnitF>> GetFurnitFByKodFD(string kodFD)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query;
                if (kodFD != "")
                {
                    query = $"select * " +
                            $"  from furnit_f " +
                            $"  where kod_f_d = @kodFD " +
                            $"  order by n_pp ";
                }
                else
                {
                    query = $"select top 0 * " +
                            $"  from furnit_f " +
                            $"  where kod_f_d = @kodFD " +
                            $"  order by n_pp ";
                }
                

                var result = await connection.QueryAsync<FurnitF>(query, new Dictionary<string, object> { { "@kodFD", kodFD } });
                return result.ToList();
            }
        }

        /// <summary>
        /// Получает данные из furnit_f_it по kod_f_d
        /// </summary>
        /// <param name="kodFD">pach_kod</param>
        /// <returns></returns>
        public async Task<List<FurnitFIt>> GetFurnitFItByKodFD(string kodFD)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query;
                if (kodFD != "")
                {
                    query = $"select * " +
                                $"  from furnit_f_it " +
                                $"  where kod_f_d = @kodFD " +
                                $"  order by n_pp, ffiid ";
                }
                else
                {
                    query = $"select top 0 * " +
                                $"  from furnit_f_it " +
                                $"  where kod_f_d = @kodFD " +
                                $"  order by n_pp, ffiid ";
                }

                var result = await connection.QueryAsync<FurnitFIt>(query, new Dictionary<string, object> { { "@kodFD", kodFD } });
                return result.ToList();
            }
        }
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

        /// <summary>
        /// Получает информацию по заявкам на фурнитуру/упаковку по pach_kod 
        /// </summary>
        /// <param name="_pachKod">pach_kod</param>
        /// <returns></returns>
        public async Task<FurnitZayavCheckByPachKod> GetFurnitZayavCheckByPachKod(string pachKod)
        {
            try
            {
                string query = @"exec furnitZayavCheck @pachKod, 1";
                return await _dbService.GetEntityAsync<FurnitZayavCheckByPachKod>(query, new { pachKod });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных FurnitZayavCheck для PachKod {pachKod}");
                return null;
            }
        }

        /// <summary>
        ////Полчает список заборных карт по номеру заявки
        /// </summary>
        /// <param name="kodF"></param>
        /// <returns></returns>
        public async Task<List<ReestrFurn>> GetReestrFurnByKodF(string kodF)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query;
                if (kodF != "")
                {
                    query = $"select * from ReestrFurn where rfKodF = @kodF and rfTipZ = 1 " +
                            $" order by rfNOtgrPp";
                }
                else
                {
                    query = $"select top 0 * from ReestrFurn ";
                }

                var result = await connection.QueryAsync<ReestrFurn>(query, new Dictionary<string, object> { { "@kodF", kodF } });
                return result.ToList();
            }
        }

        /// <summary>
        /// Получает состав заборной карты по ее коду
        /// </summary>
        /// <param name="rfID"></param>
        /// <returns></returns>
        public async Task<List<ReestrFurnSostView>> GetReestrFurnSostByRfID(int rfID)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query;
                if (rfID != 0)
                {
                    query = $"select rfs.*, cast(ltrim(rtrim(gr)) as nvarchar) + ' / ' + cast(ltrim(rtrim(name)) as nvarchar) as fn from ReestrFurnSost rfs " +
                            $" left join dop_ras_mat drm on drm.kod = rfs.rfsKodO and drm.kod_art = rfs.rfsKodArt " +
                            $" where rfs.rfsRfID = @rfID " +
                            $" order by rfs.rfsID";
                }
                else
                {
                    query = $"select top 0 rfs.*, cast(ltrim(rtrim(gr)) as nvarchar) + ' / ' + cast(ltrim(rtrim(name)) as nvarchar) as fn " +
                            $" from ReestrFurnSost rfs "+
                            $"  left join dop_ras_mat drm on drm.kod = rfs.rfsKodO and drm.kod_art = rfs.rfsKodArt ";
                }

                var result = await connection.QueryAsync<ReestrFurnSostView>(query, new Dictionary<string, object> { { "@rfID", rfID} });
                return result.ToList();
            }
        }

        /// <summary>
        /// Получает ШК, входящие в состав заборной карты по ее коду
        /// </summary>
        /// <param name="rfID"></param>
        /// <returns></returns>
        public async Task<List<ReestrFurnShtr>> GetReestrFurnShtrByRfID(int rfID)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query;
                if (rfID != 0)
                {
                    query = $"select * from ReestrFurnShtr where rfshRfID = @rfID " +
                            " order by rfshRfID";
                }
                else
                {
                    query = $"select top 0 * from ReestrFurnShtr ";
                }

                var result = await connection.QueryAsync<ReestrFurnShtr>(query, new Dictionary<string, object> { { "@rfID", rfID } });
                return result.ToList();
            }
        }

        /// <summary>
        /// Получает места для доставки по коду
        /// </summary>
        /// <param name="rfDbID"></param>
        /// <returns></returns>
        public async Task<List<ReestrFurnDeliveryBagView>> GetReestrFurnDeliveryBagByRfDbID(int rfDbID)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query;
                if (rfDbID != 0)
                {
                    query = $"select rfdb.*, dd.ddName, dt.dtDateTime " +
                            $" from ReestrFurnDeliveryBag rfdb " +
                            $" left join deliveryDirection dd on rfdb.rfdbDdID = dd.ddID " +
                            $" left join deliveryTimeTable dt on rfdb.rfdbDtID = dt.dtID " +
                            $" where rfdbID = @rfDbID " +
                            $" order by rfdbID";
                }
                else
                {
                    query = $"select top 0 rfdb.*, dd.ddName, dt.dtDateTime " +
                            $" from ReestrFurnDeliveryBag rfdb " +
                            $" left join deliveryDirection dd on rfdb.rfdbDdID = dd.ddID " +
                            $" left join deliveryTimeTable dt on rfdb.rfdbDtID = dt.dtID ";
                }

                var result = await connection.QueryAsync<ReestrFurnDeliveryBagView>(query, new Dictionary<string, object> { { "@rfDbID", rfDbID } });
                return result.ToList();
            }
        }

        /// <summary>
        ////Получает ШК транспортировочных мест по коду 
        /// </summary>
        /// <param name="rfDbID"></param>
        /// <returns></returns>
        public async Task<List<ReestrFurnDeliveryBagSost>> GetReestrFurnDeliveryBagSostByRfDbID(int rfDbID)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query;
                if (rfDbID != 0)
                {
                    query = $"select * from reestrFurnDeliveryBagSost where rfdbsrfdbid = @rfDbID " +
                            $" order by rfdbsrfdbid";
                }
                else
                {
                    query = $"select top 0 * from reestrFurnDeliveryBagSost ";
                }

                var result = await connection.QueryAsync<ReestrFurnDeliveryBagSost>(query, new Dictionary<string, object> { { "@rfDbID", rfDbID } });
                return result.ToList();
            }
        }

        #endregion

    }
}