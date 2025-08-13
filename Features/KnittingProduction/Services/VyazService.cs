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
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Services;

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
                    string query = $"select * from VyazPlanView where men not in (10, 30, 33) ";

                    var result = await connection.QueryAsync<VyazPlanView>(query, new Dictionary<string, object> {  });
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
        #endregion

    }
}