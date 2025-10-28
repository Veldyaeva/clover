using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Репозиторий данных для KnitterWorkSpace (минимальный слой доступа к БД).
    /// </summary>
    public class KnitterRepository
    {
        private readonly DbService _dbService;

        public KnitterRepository(DatabaseHelper dbHelper)
        {
            _dbService = new DbService(dbHelper);
        }

        /// <summary>
        /// Возвращает список записей плана загрузки вязальщика по табельному номеру.
        /// </summary>
        /// <param name="tab">Табельный номер сотрудника.</param>
        /// <returns>Список укороченной модели <see cref="KnitterPZVModel"/> для отображения.</returns>
        public async Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab)
        {
            // Выбираем из основной таблицы, чтобы получить pzvAnnID
            const string query = "SELECT * FROM dbo.vwPlanZagrVyazNorm_ByTab WHERE pzvTab = @tab";
            return await _dbService.GetListAsync<KnitterPZVModel>(query, new { tab });
        }

        /// <summary>
        /// Вызывает хранимую процедуру <c>GetPlanZagrVyazByPachList</c> для получения плана по списку партий.
        /// </summary>
        /// <param name="nomListJson">JSON массив с элементами номеров задания/номенклатуры (например: [{"nomZad":"123","nom":456}]).</param>
        /// <param name="vyazPodrKod">Код вязального подразделения.</param>
        /// <returns>Список операций плана <see cref="PlanZagrVyazOper"/>.</returns>
        public async Task<List<PlanZagrVyazOper>> GetPlanZagrVyazByPachListAsync(string nomListJson, int vyazPodrKod)
        {
            const string query = "EXEC GetPlanZagrVyazByPachList @xNomZadNomListJson = @nomListJson, @xVyazPodrKod = @vyazPodrKod";
            return await _dbService.GetListAsync<PlanZagrVyazOper>(query, new { nomListJson, vyazPodrKod });
        }

        /// <summary>
        /// Возвращает ФИО сотрудника по табельному номеру.
        /// </summary>
        /// <param name="tab">Табельный номер.</param>
        /// <returns>Строка ФИО или пустая строка, если не найдено.</returns>
        public async Task<string> GetFioByTabAsync(int tab)
        {
            const string query = "SELECT fio FROM dbo.fio WHERE tab = @tab";
            var fio = await _dbService.GetFirstOrDefaultAsync<string>(query, new { tab });
            return fio ?? string.Empty;
        }

        /// <summary>
        /// Возвращает справочник сотрудников (таб и ФИО) для выпадающего списка.
        /// </summary>
        /// <returns>Список сотрудников.</returns>
        public async Task<List<FioModel>> GetFioListAsync()
        {
            const string query = @"SELECT tab AS Tab, fio AS Fio FROM dbo.fio ORDER BY fio";
            return await _dbService.GetListAsync<FioModel>(query, new { });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public async Task<List<PlanZagrVyaz>> GetPlanTreeByTabAsync(int tab)
        {
            const string query = "SELECT * FROM dbo.planZagrVyaz WHERE pzvTab = @tab";
            return await _dbService.GetListAsync<PlanZagrVyaz>(query, new { tab });
        }

    }
}


