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
    public class KnitterOrchestrator : IKnitterOrchestrator
    {
        private readonly IKnitterRepository _repo;
        private readonly ILogger _logger;

        public KnitterOrchestrator(IKnitterRepository repo, ILogger logger)
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

        /// <summary>
        /// Устанавливает дату начала на стороне БД и возвращает фактически сохранённое значение.
        /// </summary>
        public Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId) => _repo.UpdatePzvDateStartAsync(pzvId);

        /// <summary>
        /// Устанавливает дату окончания на стороне БД и возвращает фактически сохранённое значение.
        /// </summary>
        public Task<KnitterPZVModel> UpdatePzvDateEndAsync(int pzvId) => _repo.UpdatePzvDateEndAsync(pzvId);

        /// <summary>
        /// Получает операции плана по списку партий через хранимую процедуру.
        /// </summary>
        /// <param name="nomListJson">JSON-массив номеров задания/номенклатуры.</param>
        /// <param name="vyazPodrKod">Код вязального подразделения.</param>
        /// <returns>Список операций плана.</returns>
        public Task<List<PlanZagrVyazOper>> GetPlanZagrVyazByPachListAsync(string nomListJson, int vyazPodrKod) => _repo.GetPlanZagrVyazByPachListAsync(nomListJson, vyazPodrKod);
    }
}


