using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service.KnitterRepository;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public interface IKnitterOrchestrator
    {
        Task<List<FioModel>> GetFioListAsync();
        Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab, int? kwsId, int? kmaId, bool expandAssignedByNrId, decimal maxHours = 25, bool includeFinished = false);
        Task<string> GetFioByTabAsync(int tab);
        Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId);
        Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty);
        Task<(int? kmaId, string kmaNum)> GetZoneByTabAsync(int tab);
		Task<(int? shiftId, DateTime? dateStart)> GetOpenShiftByTabAsync(int tab);
        Task<(int? shiftId, int? tabStart, DateTime? dateStart)> GetOpenShiftByZoneAsync(int kmaId);
        Task<List<ShiftHistoryModel>> GetShiftsByTabAsync(int tab);
        Task<List<ShiftHistoryModel>> GetShiftsByKmaAsync(int kmaId);
    }
}


