using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Оркестратор для KnitterWorkSpace: слой координации между UI и репозиторием.
    /// Не содержит UI-логики и SQL, только делегирует доступ к данным.
    /// </summary>
    public class KnitterOrchestrator : IKnitterOrchestrator
    {
        private readonly IKnitterWorkSpaceUiGateway _uiGateway;

        public KnitterOrchestrator(IKnitterWorkSpaceUiGateway uiGateway)
        {
            _uiGateway = uiGateway ?? throw new ArgumentNullException(nameof(uiGateway));
        }

        public Task<List<FioModel>> GetFioListAsync() => _uiGateway.GetFioListAsync();

        public Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab, int? kwsId, int? kmaId, bool expandAssignedByNrId, decimal maxHours, bool includeFinished = false) =>
            _uiGateway.GetPlanByTabAsync(tab, kwsId, kmaId, expandAssignedByNrId, maxHours, includeFinished);

        public Task<string> GetFioByTabAsync(int tab) => _uiGateway.GetFioByTabAsync(tab);

        public Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId) => _uiGateway.UpdatePzvDateStartAsync(pzvId);

        public Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty) => _uiGateway.SplitPzvByFactAsync(pzvId, factQty);

        public Task<(int? kmaId, string kmaNum)> GetZoneByTabAsync(int tab) => _uiGateway.GetZoneByTabAsync(tab);

        public Task<(int? shiftId, DateTime? dateStart)> GetOpenShiftByTabAsync(int tab) => _uiGateway.GetOpenShiftByTabAsync(tab);

        public Task<(int? shiftId, int? tabStart, DateTime? dateStart)> GetOpenShiftByZoneAsync(int kmaId) => _uiGateway.GetOpenShiftByZoneAsync(kmaId);
    }
}
