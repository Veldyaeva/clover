using DevExpress.Xpf.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.BandedGrid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    public class KnitterWorkSpaceService : IKnitterWorkSpaceService
    {
        private readonly IKnitterOrchestrator _orchestrator;
        public KnitterWorkSpaceService(IKnitterOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public Task<StartShiftResult> StartShiftAsync(StartShiftCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.Tab <= 0)
                return new StartShiftResult { Success = false, ErrorMessage = "Не выбран сотрудник." };

            if (command.PzvIds == null || command.PzvIds.Count == 0)
                return new StartShiftResult { Success = false, ErrorMessage = "Нет строк для назначения." };

            if (command.KmaId.HasValue)
            {
                var openShift = await _orchestrator.GetOpenShiftByZoneAsync(command.KmaId.Value);
                if (openShift.shiftId.HasValue)
                {
                    return new StartShiftResult
                    {
                        Success = false,
                        ErrorMessage = $"В зоне {command.KmaNum} уже открыта смена."
                    };
                }
            }

            await _orchestrator.SetPzvTabAsync(command.PzvIds, command.Tab);

            var shiftId = await _orchestrator.StartWorkingShiftAsync(command.Tab, command.KmaId, command.KmaNum);
            if (shiftId <= 0)
            {
                return new StartShiftResult
                {
                    Success = false,
                    ErrorMessage = "Не удалось открыть смену."
                };
            }

            await _orchestrator.UpdatePzvKwsIdAsync(command.PzvIds, shiftId);

            return new StartShiftResult
            {
                Success = true,
                ShiftId = shiftId,
                ShiftStartTime = DateTime.Now
            };
            //simpleButton2.Enabled = false;
            //try
            //{
            //    // Если смена уже запущена — завершаем смену: запись в БД, остановка таймера и смена текста
            //    if (_isShiftRunning)
            //    {
            //        if (!ShowShiftEndConfirmationDialog())
            //            return;
            //        // Перед завершением смены: обработать все операции; если есть незавершённые — не закрываем.
            //        var canClose = await ProcessOperationsOnShiftEndAsync();
            //        if (!canClose)
            //            return;

            //        // Очищаем список незавершённых операций при успешном закрытии смены
            //        _unfinishedOperationIds.Clear();
            //        // Обновляем отображение, чтобы убрать подсветку
            //        this.BeginInvoke(new Action(() =>
            //        {
            //            bandedGridView3?.RefreshData();
            //            advBandedGridView1?.RefreshData();
            //        }));
            //        //// снимаем назначение у всех НЕ начатых в текущей смене
            //        var stat = (await _orchestrator.AdjustNotStartedBeforeShiftEndAsync(_currentShiftId, 12m)).ToList();

            //        if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tabEnd) || tabEnd <= 0)
            //        {
            //            XtraMessageBox.Show(this, "Не удалось определить табель при завершении смены.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            LogWarning("Не удалось определить табель при завершении смены.", "Shift.End");
            //        }
            //        else if (_currentShiftId.HasValue && _currentShiftId.Value > 0)
            //        {
            //            int shiftID = _currentShiftId.Value;
            //            await _orchestrator.EndWorkingShiftAsync(_currentShiftId.Value, tabEnd);
            //            // Перезагрузим план, чтобы обновить статусы/проценты
            //            await LoadPlanForTabAsync(tabEnd, forceReload: true);
            //            LogSuccess($"Смена успешно завершена. ShiftId={shiftID}, Tab={tabEnd}", "Shift.End");
            //        }

            //        await RefreshFioListAsync();
            //        ApplyShiftUi(false, null, null);
            //        return;
            //    }

            //    if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int selectedTab) || selectedTab <= 0)
            //    {
            //        XtraMessageBox.Show(this, "Выберите сотрудника для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        LogWarning("Попытка старта смены без выбранного сотрудника.", "Shift.Start");
            //        return;
            //    }

            //    // Проверка: нельзя открыть вторую смену в зоне
            //    if (_currentKmaId.HasValue)
            //    {
            //        var openByZone = await _orchestrator.GetOpenShiftByZoneAsync(_currentKmaId.Value);
            //        if (openByZone.shiftId.HasValue)
            //        {
            //            XtraMessageBox.Show(this, $"В зоне {_currentKmaNum} уже открыта смена (таб. {openByZone.tabStart}), сначала завершите её.", "Смена уже открыта", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            LogWarning($"Попытка открыть смену в зоне {_currentKmaNum} при уже открытой смене (tab={openByZone.tabStart}).", "Shift.Start");
            //            return;
            //        }
            //    }

            //    // Назначаем таб ВСЕМ загруженным строкам 
            //    var rowsForUpdate = _planPresenter.AllRows?.ToList() ?? new List<KnitterPZVModel>();
            //    if (!rowsForUpdate.Any())
            //    {
            //        XtraMessageBox.Show(this, "Нет строк для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        LogWarning("Нет строк для назначения табельного номера при старте смены.", "Shift.Start");
            //        return;
            //    }

            //    var pzvIds = rowsForUpdate
            //        .Select(r => r.pzvID)
            //        .Where(id => id > 0)
            //        .Distinct()
            //        .ToList();

            //    if (pzvIds.Count == 0)
            //    {
            //        XtraMessageBox.Show(this, "Не удалось определить записи для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        LogWarning("Список pzvID пуст при старте смены.", "Shift.Start");
            //        return;
            //    }


            //    // Успешный старт смены: фиксируем в БД, проставляем pzvKwsID для всех! операций, меняем текст кнопки и запускаем таймер
            //    try
            //    {
            //        await _orchestrator.SetPzvTabAsync(pzvIds, selectedTab);
            //        _currentShiftId = await _orchestrator.StartWorkingShiftAsync(selectedTab, _currentKmaId, _currentKmaNum);
            //        if (_currentShiftId.HasValue && _currentShiftId.Value > 0)
            //        {
            //            await _orchestrator.UpdatePzvKwsIdAsync(pzvIds, _currentShiftId.Value);


            //            // Обновим план после проставления pzvKwsID
            //            await LoadPlanForTabAsync(selectedTab, forceReload: true);
            //            await RefreshFioListAsync();

            //            ApplyShiftUi(true, _currentShiftId, DateTime.Now);
            //            LogSuccess($"Смена успешно начата. ShiftId={_currentShiftId.Value}, Tab={selectedTab}, Rows={pzvIds.Count}", "Shift.Start");
            //        }
            //        else
            //        {
            //            XtraMessageBox.Show(this, "Не удалось получить ID смены для обновления операций.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            LogWarning("StartWorkingShiftAsync вернул некорректный ID смены: " + _currentShiftId, "Shift.Start");
            //        }
            //    }
            //    catch (Exception exStart)
            //    {
            //        XtraMessageBox.Show(this, $"Не удалось записать начало смены: {exStart.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        LogError(exStart, "Shift.Start");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(this, $"Ошибка при назначении табельного номера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    LogError(ex, "simpleButton2_Click");
            //}
            //simpleButton2.Enabled = true;
        }

        public Task<CloseShiftResult> CloseShiftAsync(CloseShiftCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.ShiftId <= 0)
                return new CloseShiftResult { Success = false };

            var unfinished = (command.CurrentRows ?? new List<KnitterPZVModel>())
                .Where(r => r.pzvDateStart != null && r.pzvDateEnd == null)
                .Select(r => r.pzvID)
                .Distinct()
                .ToList();

            if (unfinished.Count > 0)
            {
                return new CloseShiftResult
                {
                    Success = false,
                    HasUnfinishedOperations = true,
                    UnfinishedPzvIds = unfinished
                };
            }

            await _orchestrator.AdjustNotStartedBeforeShiftEndAsync(command.ShiftId, command.MinHours);
            await _orchestrator.EndWorkingShiftAsync(command.ShiftId, command.TabEnd);

            return new CloseShiftResult
            {
                Success = true,
                HasUnfinishedOperations = false,
                UnfinishedPzvIds = new List<int>()
            };
        }
    }
}


