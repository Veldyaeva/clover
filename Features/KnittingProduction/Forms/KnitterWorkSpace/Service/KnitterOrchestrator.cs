using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Оркестратор для KnitterWorkSpace: координация загрузки данных без UI/SQL.
    /// </summary>
    public class KnitterOrchestrator
    {
        private readonly KnitterRepository _repo;
        private readonly ILogger _logger;

        public KnitterOrchestrator(KnitterRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает список сотрудников для выбора в UI.
        /// </summary>
        public Task<List<FioModel>> GetFioListAsync() => _repo.GetFioListAsync();

        /// <summary>
        /// Возвращает укороченный план по табельному номеру.
        /// </summary>
        public Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab) => _repo.GetPlanByTabAsync(tab);

        /// <summary>
        /// Возвращает ФИО по табельному номеру (для авто-подмешивания в список).
        /// </summary>
        public Task<string> GetFioByTabAsync(int tab) => _repo.GetFioByTabAsync(tab);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public Task<List<PlanZagrVyaz>> GetPlanTreeByTabAsync(int tab) => _repo.GetPlanTreeByTabAsync(tab);

        public Task SetPzvTabAsync(IEnumerable<int> pzvIds, int tab) => _repo.UpdatePzvTabAsync(pzvIds, tab);
        public Task UpdatePzvDateStartAsync(int pzvId, DateTime dateStart) => _repo.UpdatePzvDateStartAsync(pzvId, dateStart);

        /// <summary>
        /// Получает операции плана по списку партий через хранимую процедуру.
        /// </summary>
        /// <param name="nomListJson">JSON-массив номеров задания/номенклатуры.</param>
        /// <param name="vyazPodrKod">Код вязального подразделения.</param>
        /// <returns>Список операций плана.</returns>
        public Task<List<PlanZagrVyazOper>> GetPlanZagrVyazByPachListAsync(string nomListJson, int vyazPodrKod) => _repo.GetPlanZagrVyazByPachListAsync(nomListJson, vyazPodrKod);
    }
}


