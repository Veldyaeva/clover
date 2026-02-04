using Dapper;
using DevExpress.XtraGantt.Scheduling;
using Org.BouncyCastle.Asn1.Ocsp;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Services
{
    /// <summary>
    /// Сервис работы с вяз производством.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class VyazService
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
        public VyazService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region
        public async Task<List<VyazPlanView>> GetVyazPlanView()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select * from VyazPlanView where men not in (10, 30, 33)";

                    var result = await connection.QueryAsync<VyazPlanView>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных VyazPlanView");
                return null;
            }
        }

        public async Task<List<KnitMachineList>> GetKnitMachineListByClassID(int xIDClass)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    //string query = $"SELECT mlv.kmlID, mlv.kmlNumber, mlv.kmlKmaID, mlv.kmaNumber, mlv.machNazn" +
                    //    $"  , mc.name_class, mc.mc_id " +
                    //    $"FROM knitMachineList_view mlv " +
                    //    $"  LEFT JOIN matrix_class mc ON mlv.kmlIdVyazClass = mc.id_class" +
                    //    $"WHERE mlv.machNazn = 'вяз.подразделение' " +
                    //    $"  AND mc.id_class = {xIDClass} " +
                    //    $"ORDER BY mlv.kmlNumber";
                    string query = $"SELECT mlv.kmlID, mlv.kmlNumber, mlv.kmlKmaID, mlv.kmaNumber, mlv.machNazn, mc.name_class, mc.mc_id " +
                        $"FROM knitMachineList_view mlv " +
                        $"  LEFT JOIN matrix_class mc ON mlv.kmlIdVyazClass = mc.id_class " +
                        $"WHERE mlv.machNazn = 'вяз.подразделение' " +
                        $"  AND mc.id_class = {xIDClass} " +
                        $"ORDER BY mlv.kmlNumber";
                    var result = await connection.QueryAsync<KnitMachineList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetKnitMachineListByClassID");
                return null;
            }
        }
        public async Task<List<KnitMachineClassList>> GetKnitMachineClassList()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"SELECT * " +
                        $"  FROM matrix_class mc " +
                        $"  ORDER BY mc.name_class";

                    var result = await connection.QueryAsync<KnitMachineClassList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetKnitMachineClassList");
                return null;
            }
        }
        public async Task<List<KnitMachineAreaListView>> GetKnitMachineAreaList()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"SELECT * " +
                        $"FROM knitMachineArea_view " +
                        $"where kmaIDNazn = 6" +
                        $"ORDER BY kmaNumber";

                    var result = await connection.QueryAsync<KnitMachineAreaListView>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetKnitMachineAreaList");
                return null;
            }
        }
        public async Task<List<KnitMachineList>> GetKnitMachineListByAreaID(int kmaID)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"SELECT * " +
                        $"FROM knitMachineList_view " +
                        $"where kmlKmaID = {kmaID}" +
                        $"ORDER BY kmlNumber";

                    var result = await connection.QueryAsync<KnitMachineList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetKnitMachineListByAreaID");
                return null;
            }
        }
        public async Task<List<KnitMachineLoadAllInfo>> GetKnitMachineLoadInfoByClassID(int xIDClass)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"EXEC getKnitMachineLoadInfo {xIDClass} ";

                    var result = await connection.QueryAsync<KnitMachineLoadAllInfo>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetKnitMachineLoadInfoByClassID");
                return null;
            }
        }

        public async Task<List<ArtPrKnitMachineView>> GetArtPrKnitMachineByKodMatr(string nn, int typeVyazKM, string vidVyazKM)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    //string query = $"SELECT * " +
                    //    $"FROM ArtPrKnitMachineView " +
                    //    $"WHERE kod_matr = '{nn}' " +
                    //    $"  AND typevyazkm = {typeVyazKM} " +
                    //    $"  AND vidVyazKM = '{vidVyazKM}'";
                    string query = $"EXEC ArtPrKnitMachineView '{nn}', {typeVyazKM}, '{vidVyazKM}'";
                    var result = await connection.QueryAsync<ArtPrKnitMachineView>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArtPrKnitMachineByKodMatr");
                return null;
            }
        }

        public async Task<List<PlanSezonZadKnitMachine>> GetPlanSezonZadKnitMachineDivisionByDay(int kmlID)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    //string query = $"SELECT * " +
                    //    $"FROM ArtPrKnitMachineView " +
                    //    $"WHERE kod_matr = '{nn}' " +
                    //    $"  AND typevyazkm = {typeVyazKM} " +
                    //    $"  AND vidVyazKM = '{vidVyazKM}'";
                    string query = $"SELECT * " +
                        $"  FROM planSezonZadKnitMachineDivisionByDay " +
                        $"  WHERE pszkmKmlID = {kmlID} " +
                        $"  ORDER BY pszkmPlanDateFrom";
                    var result = await connection.QueryAsync<PlanSezonZadKnitMachine>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetplanSezonZadKnitMachineDivisionByDay");
                return null;
            }
        }

        public async Task<List<PlanSezonZadKnitMachine>> GetPlanSezonZadKnitMachine(int kmlID)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    //string query = $"SELECT * " +
                    //    $"FROM ArtPrKnitMachineView " +
                    //    $"WHERE kod_matr = '{nn}' " +
                    //    $"  AND typevyazkm = {typeVyazKM} " +
                    //    $"  AND vidVyazKM = '{vidVyazKM}'";
                    string query = $"SELECT * " +
                        $"  FROM planSezonZadKnitMachine " +
                        $"  WHERE pszkmKmlID = {kmlID} " +
                        $"  ORDER BY pszkmYearMonthInt, pszkmPlanDateFrom";
                    var result = await connection.QueryAsync<PlanSezonZadKnitMachine>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetplanSezonZadKnitMachine");
                return null;
            }
        }

        public async Task<List<PlanSezonZadKnitMachineLoadingSummary>> GetPlanSezonZadKnitMachineLoadingSummary(int kmlID)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    //string query = $"SELECT * " +
                    //    $"FROM ArtPrKnitMachineView " +
                    //    $"WHERE kod_matr = '{nn}' " +
                    //    $"  AND typevyazkm = {typeVyazKM} " +
                    //    $"  AND vidVyazKM = '{vidVyazKM}'";
                    string query = $"exec getPlanSezonZadKnitMachineLoadingSummary {kmlID} ";
                    var result = await connection.QueryAsync<PlanSezonZadKnitMachineLoadingSummary>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных getPlanSezonZadKnitMachineLoadingSummary");
                return null;
            }
        }

        public async Task<List<PlanTotalHoursByKnitMachine>> GetPlanTotalHoursByKnitMachine()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"exec GetPlanTotalHoursByKnitMachine ";

                    var result = await connection.QueryAsync<PlanTotalHoursByKnitMachine>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetPlanTotalHoursByKnitMachine");
                return null;
            }
        }

        public async Task<List<ZadanyListByMachine>> GetZadanyListByMachine(int kmlID)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"exec GetZadanyListByMachine {kmlID}";

                    var result = await connection.QueryAsync<ZadanyListByMachine>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetZadanyListByMachine");
                return null;
            }
        }
        public async Task<List<RzvPachListByNom>> GetRzvPachListByNom(int nom, string nomZad)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"SELECT rzv.zad_pl as nomZad, rzv.nom, rzv.nom_n, rzv.n_pach, rzv.pach_kod, rzv.kod, rzv.razm, rzv.kol, gradacia " +
                        $"  , dbo.getAnnIDByKod(rzv.kod) as annID " +
                        $"FROM raskr_zeh_vyaz rzv" +
                        $" WHERE rzv.nom = {nom} and zad_pl = {nomZad}";

                    var result = await connection.QueryAsync<RzvPachListByNom>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetRzvPachListByNom");
                return null;
            }
        }

        public async Task<List<PZVOperList>> GetPZVOperListByPachList(string pachList, int podrID)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"EXEC GetPlanZagrVyazByPachList @xNomZadNomListJson = '{pachList}', @xVyazPodrKod = {podrID}, @xResulType = 0";

                    var result = await connection.QueryAsync<PZVOperList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка SQL при получении данных GetPlanZagrVyazByPachList");
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetPlanZagrVyazByPachList");
                return null;
            }
        }

        public async Task<List<KnitPlanReportParametersList>> GetKnitPlanReportParametersList(int parameterType)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"EXEC getKnitPlanReportParametersList {parameterType}";

                    var result = await connection.QueryAsync<KnitPlanReportParametersList>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetKnitPlanReportParametersList");
                return null;
            }
        }
        public async Task<List<SmenZadanyVyazMachine>> GetSmenZadanyVyazMachine()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"EXEC getSmenZadanyVyazMachine ";

                    var result = await connection.QueryAsync<SmenZadanyVyazMachine>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetSmenZadanyVyazMachine");
                return null;
            }
        }

        public async Task<List<SmenZadanyVyazEmp>> GetSmenZadanyVyazEmp()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"EXEC getSmenZadanyVyazEmp ";

                    var result = await connection.QueryAsync<SmenZadanyVyazEmp>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetSmenZadanyVyazEmp");
                return null;
            }
        }
        //public async Task<List<SmenZadanyVyaz>> GetSmenZadanyVyaz(int _idNazn, int _kodProizv, int _kodPodr, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        using (var connection = _dbHelper.GetConnection())
        //        {
        //            string query = $"EXEC getSmenZadanyVyaz @xKmaIDNazn = {_idNazn}, @xKodProizv = {_kodProizv}, @xKodPodr = {_kodPodr}";

        //            var result = await connection.QueryAsync<c>(query, new Dictionary<string, object> { });
        //            return result.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetSmenZadanyVyaz");
        //        return null;
        //    }
        //}
        public async Task<BindingSource> GetSmenZadanyVyaz(int _idNazn, int _kodProizv, int _kodPodr, CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = _dbHelper.GetConnection();
                string query = $"EXEC GetSmenZadanyVyaz @xKmaIDNazn = {_idNazn}, @xKodProizv = {_kodProizv}, @xKodPodr = {_kodPodr}";

                var list = (await connection.QueryAsync<SmenZadanyVyaz>(query, new Dictionary<string, object> { })).ToList();
                //return result.ToList();

                return new BindingSource
                {
                    DataSource = new BindingList<SmenZadanyVyaz>(list)
                };
            }
            catch (OperationCanceledException)
            {
                // отмена — НЕ ошибка
                return new BindingSource
                {
                    DataSource = new BindingList<SmenZadanyVyaz>()
                };
            }
            catch (SqlException ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка SQL при получении данных GetSmenZadanyVyaz");
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при получении данных GetSmenZadanyVyaz");
                return new BindingSource
                {
                    DataSource = new BindingList<SmenZadanyVyaz>()
                };
            }
        }

        public async Task<BindingSource> GetNaryadZadanyVyaz(int tab, int kmlID, CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = _dbHelper.GetConnection();
                const string query = @"EXEC planZagrVyazTabKmlID_view @xPzvTab = @Tab, @xPzvKmlID = @KmlID";
                var command = new CommandDefinition(query, new { Tab = tab, KmlID = kmlID }, cancellationToken: cancellationToken);

                var list = (await connection
                    .QueryAsync<NaryadZadanyVyaz>(command))
                    .AsList();

                return new BindingSource
                {
                    DataSource = new BindingList<NaryadZadanyVyaz>(list)
                };
            }
            catch (OperationCanceledException)
            {
                // отмена — НЕ ошибка
                return new BindingSource
                {
                    DataSource = new BindingList<NaryadZadanyVyaz>()
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при получении данных planZagrVyazTabKmlID_view");
                return new BindingSource
                {
                    DataSource = new BindingList<NaryadZadanyVyaz>()
                };
            }
        }

        public async Task<BindingSource> GetPodrVyaz(CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = _dbHelper.GetConnection();
                const string query = @"SELECT kod_vyaz, text_vyaz FROM podr_vyaz WHERE kod_vyaz IN (1,2,3)";
                var command = new CommandDefinition(query, new { }, cancellationToken: cancellationToken);

                var list = (await connection
                    .QueryAsync<PodrVyaz>(command))
                    .AsList();

                return new BindingSource
                {
                    DataSource = new BindingList<PodrVyaz>(list)
                };
            }
            catch (OperationCanceledException)
            {
                // отмена — НЕ ошибка
                return new BindingSource
                {
                    DataSource = new BindingList<PodrVyaz>()
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при получении данных podr_vyaz");
                return new BindingSource
                {
                    DataSource = new BindingList<PodrVyaz>()
                };
            }
        }

        public async Task<BindingSource> GetPzvCheck(int _podrID, int _month, int _year, CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = _dbHelper.GetConnection();
                //const string query = @"EXEC dbo.pzv_pivotDayTab @xPodrID = podrID
                //    , @xMes = mes
                //    , @xGod = god";
                //var command = new CommandDefinition(query, new { podrID = _podrID, mes = _month, god = _year }, cancellationToken: cancellationToken);
                
                //string query = $"EXEC dbo.pzv_pivotDayTab @xPodrID = {_podrID}, @xMes = {_month}, @xGod = {_year}";
                //var command = new CommandDefinition(query, new {  }, cancellationToken: cancellationToken);
                //var list = (await connection
                //    .QueryAsync<PzvCheck>(command))
                //    .AsList();

                string query = $"EXEC dbo.pzv_pivotDayTab @xPodrID = {_podrID}, @xMes = {_month}, @xGod = {_year}";

                var list = (await connection.QueryAsync<PzvCheck>(query, new Dictionary<string, object> { })).ToList();
                //return result.ToList();

                return new BindingSource
                {
                    DataSource = new BindingList<PzvCheck>(list)
                };
            }
            catch (OperationCanceledException)
            {
                // отмена — НЕ ошибка
                return new BindingSource
                {
                    DataSource = new BindingList<PzvCheck>()
                };
            }
            catch (SqlException ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка SQL при получении данных GetPzvCheck");
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при получении данных GetPzvCheck");
                return new BindingSource
                {
                    DataSource = new BindingList<PzvCheck>()
                };
            }
        }
        //public async Task<string> GetAnyMachineByNomZad(string? _nomZadany = null)
        //{
        //    string sql;
        //    var p = new DynamicParameters();

        //    if (_nomZadany != null)        // поиск по № задания
        //    {
        //        sql = @"SELECT TOP (1) 
        //               dbo.getFileEskizForKodd_rt(vsa.annId)
        //        FROM   dbo.View_sp_articul vsa
        //        WHERE  vsa.annId = @annId";
        //        p.Add("@annId", annId);
        //    }
        //    else                      // прямой поиск по kodd
        //    {
        //        sql = "SELECT dbo.getFileEskizForKodd(@kod)";
        //        p.Add("@kod", kod);
        //    }

        //    return await _dbHelper.ExecuteScalarAsync<string>(sql, p);
        //}
        #endregion

    }
}