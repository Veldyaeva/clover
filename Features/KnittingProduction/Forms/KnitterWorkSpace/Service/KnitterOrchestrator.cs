using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service.KnitterRepository;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Оркестратор для KnitterWorkSpace: слой координации между UI и репозиторием.
    /// Не содержит UI-логики и SQL — только вызовы репозитория, агрегация и возврат дельт.
    /// </summary>
    public class KnitterOrchestrator : IKnitterOrchestrator
    {
        private readonly IKnitterRepository _repo;
        private readonly ILogger _logger;

        /// <summary>
        /// Создаёт оркестратор.
        /// </summary>
        /// <param name="repo">Репозиторий доступа к данным.</param>
        /// <param name="logger">Логгер для записи ошибок/событий.</param>
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

        public Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab, int? kwsId, int? kmaId, bool onlyUnassigned, bool expandAssignedByNrId, decimal maxHours, bool includeFinished = false) =>
            _repo.GetPlanByTabAsync(tab, kwsId, kmaId, onlyUnassigned, expandAssignedByNrId, maxHours, includeFinished);

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

        /// <summary>
        /// Массово назначает табельный номер для указанных записей плана.
        /// </summary>
        public Task SetPzvTabAsync(IEnumerable<int> pzvIds, int tab) => _repo.UpdatePzvTabAsync(pzvIds, tab);

        /// <summary>
        /// Устанавливает дату начала на стороне БД и возвращает фактически сохранённое значение (серверное время).
        /// </summary>
        public Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId) => _repo.UpdatePzvDateStartAsync(pzvId);

        /// <summary>
        /// Устанавливает дату окончания на стороне БД и возвращает фактически сохранённое значение (серверное время).
        /// </summary>
        public Task<KnitterPZVModel> UpdatePzvDateEndAsync(int pzvId) => _repo.UpdatePzvDateEndAsync(pzvId);
        public Task UpdatePzvFactAsync(int pzvId, int factQty) => _repo.UpdatePzvFactAsync(pzvId, factQty);

        /// <summary>
        /// Получает операции плана по списку партий через хранимую процедуру.
        /// </summary>
        /// <param name="nomListJson">JSON-массив номеров задания/номенклатуры.</param>
        /// <param name="vyazPodrKod">Код вязального подразделения.</param>
        /// <returns>Список операций плана.</returns>
        //public Task<List<PlanZagrVyazOper>> GetPlanZagrVyazByPachListAsync(string nomListJson, int vyazPodrKod) => _repo.GetPlanZagrVyazByPachListAsync(nomListJson, vyazPodrKod);

        /// <summary>
        /// При неполном выполнении: разделяет запись на “факт” и “остаток”.
        /// Должно создать дополнительную запись в плановой таблице с оставшимся количеством.
        /// </summary>
        public Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty) => _repo.SplitPzvByFactAsync(pzvId, factQty);

		/// <summary>
		/// Универсальный вызов PZV_Split с указанием режима и количества.
		/// </summary>
		public Task<IReadOnlyList<PzvSplitResult>> SplitPzvAsync(int pzvId, int mode, int qtyFact) => _repo.SplitPzvByModeAsync(pzvId, mode, qtyFact);

        /// <summary>
        /// Фиксирует начало смены в таблице ACE.dbo.knitWorkingShift и возвращает kwsID.
        /// </summary>
        public Task<int> StartWorkingShiftAsync(int tabStart, int? kmaId, string kmaNum) => _repo.StartWorkingShiftAsync(tabStart, kmaId, kmaNum, kmsId: 0);

        /// <summary>
        /// Фиксирует завершение смены (kwsDateEnd/kwsTabEnd) по kwsID.
        /// </summary>
        public Task EndWorkingShiftAsync(int shiftId, int tabEnd) => _repo.EndWorkingShiftAsync(shiftId, tabEnd);

        /// <summary>
        /// Возвращает id зоны и номер зоны для табельного номера.
        /// </summary>
        public Task<(int? kmaId, string kmaNum)> GetZoneByTabAsync(int tab) => _repo.GetZoneByTabAsync(tab);

		/// <summary>
		/// Возвращает открытую смену по табелю, если есть (ID и дата начала).
		/// </summary>
		public Task<(int? shiftId, DateTime? dateStart)> GetOpenShiftByTabAsync(int tab) => _repo.GetOpenShiftByTabAsync(tab);

        /// <summary>
        /// Возвращает открытую смену по зоне, если есть (ID, табель, дата начала).
        /// </summary>
        public Task<(int? shiftId, int? tabStart, DateTime? dateStart)> GetOpenShiftByZoneAsync(int kmaId) => _repo.GetOpenShiftByZoneAsync(kmaId);

		/// <summary>
		/// Проставляет pzvKwsID для списка операций плана.
		/// </summary>
		public Task UpdatePzvKwsIdAsync(IEnumerable<int> pzvIds, int kwsId) => _repo.UpdatePzvKwsIdAsync(pzvIds, kwsId);

        /// <summary>
        /// Сбрасываем таб при закрытии смены у неначатых операций
        /// </summary>
        /// <param name="currentShiftId"></param>
        /// <returns></returns>
        public Task<IEnumerable<MachineHoursStat>> AdjustNotStartedBeforeShiftEndAsync(int? currentShiftId, decimal v) => _repo.AdjustNotStartedBeforeShiftEndAsync(currentShiftId, 12m);


    }
}


