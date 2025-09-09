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
                    string query = $"select * from VyazPlanView ";

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
                    string query = $"SELECT mlv.kmlID, mlv.kmlNumber, mlv.kmlKmaID, mlv.kmaNumber, mlv.machNazn" +
                        $"  , mc.name_class, mc.mc_id " +
                        $"FROM matrix_class mc " +
                        $"  LEFT JOIN oborud_shv os ON mc.id_class = os.id_class " +
                        $"  LEFT JOIN knitMachineList_view mlv ON os.kod_ob = mlv.kmlKodOb " +
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
        #endregion

    }
}