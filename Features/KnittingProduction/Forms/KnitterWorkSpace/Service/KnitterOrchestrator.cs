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
    /// Не содержит UI-логики и SQL, только делегирует доступ к данным.
    /// </summary>
    public class KnitterOrchestrator : IKnitterOrchestrator
    {
        private readonly IKnitterRepository _repo;
        private readonly ILogger _logger;

        public KnitterOrchestrator(IKnitterRepository repo, ILogger logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<List<FioModel>> GetFioListAsync() => _repo.GetFioListAsync();

        public Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab) => _repo.GetPlanByTabAsync(tab);

        public Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab, int? kwsId, int? kmaId, bool onlyUnassigned, bool expandAssignedByNrId, decimal maxHours, bool includeFinished = false) =>
            _repo.GetPlanByTabAsync(tab, kwsId, kmaId, onlyUnassigned, expandAssignedByNrId, maxHours, includeFinished);

        public Task<string> GetFioByTabAsync(int tab) => _repo.GetFioByTabAsync(tab);

        public Task<List<PlanZagrVyaz>> GetPlanTreeByTabAsync(int tab) => _repo.GetPlanTreeByTabAsync(tab);

        public Task SetPzvTabAsync(IEnumerable<int> pzvIds, int tab) => _repo.UpdatePzvTabAsync(pzvIds, tab);

        public Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId) => _repo.UpdatePzvDateStartAsync(pzvId);

        public Task<KnitterPZVModel> UpdatePzvDateEndAsync(int pzvId) => _repo.UpdatePzvDateEndAsync(pzvId);

        public Task UpdatePzvFactAsync(int pzvId, int factQty) => _repo.UpdatePzvFactAsync(pzvId, factQty);

        public Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty) => _repo.SplitPzvByFactAsync(pzvId, factQty);

        public Task<IReadOnlyList<PzvSplitResult>> SplitPzvAsync(int pzvId, int mode, int qtyFact) => _repo.SplitPzvByModeAsync(pzvId, mode, qtyFact);

        public Task<int> StartWorkingShiftAsync(int tabStart, int? kmaId, string kmaNum) => _repo.StartWorkingShiftAsync(tabStart, kmaId, kmaNum, kmsId: 0);

        public Task EndWorkingShiftAsync(int shiftId, int tabEnd) => _repo.EndWorkingShiftAsync(shiftId, tabEnd);

        public Task<(int? kmaId, string kmaNum)> GetZoneByTabAsync(int tab) => _repo.GetZoneByTabAsync(tab);

        public Task<(int? shiftId, DateTime? dateStart)> GetOpenShiftByTabAsync(int tab) => _repo.GetOpenShiftByTabAsync(tab);

        public Task<(int? shiftId, int? tabStart, DateTime? dateStart)> GetOpenShiftByZoneAsync(int kmaId) => _repo.GetOpenShiftByZoneAsync(kmaId);

        public Task UpdatePzvKwsIdAsync(IEnumerable<int> pzvIds, int kwsId) => _repo.UpdatePzvKwsIdAsync(pzvIds, kwsId);

        public Task<IEnumerable<MachineHoursStat>> AdjustNotStartedBeforeShiftEndAsync(int? currentShiftId, decimal v) =>
            _repo.AdjustNotStartedBeforeShiftEndAsync(currentShiftId, v);

        public Task<int> StartShiftWorkflowAsync(int tabStart, int? kmaId, string kmaNum, IEnumerable<int> pzvIds) =>
            _repo.StartShiftWorkflowAsync(tabStart, kmaId, kmaNum, pzvIds);

        public Task CloseShiftWorkflowAsync(int shiftId, int tabEnd, decimal minHours) =>
            _repo.CloseShiftWorkflowAsync(shiftId, tabEnd, minHours);
    }
}
