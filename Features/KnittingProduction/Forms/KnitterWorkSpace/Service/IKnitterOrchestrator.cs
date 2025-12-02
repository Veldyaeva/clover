using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public interface IKnitterOrchestrator
    {
        Task<List<FioModel>> GetFioListAsync();
        Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab);
        Task<string> GetFioByTabAsync(int tab);
        Task<List<PlanZagrVyaz>> GetPlanTreeByTabAsync(int tab);
        Task SetPzvTabAsync(IEnumerable<int> pzvIds, int tab);
        Task<List<PlanZagrVyazOper>> GetPlanZagrVyazByPachListAsync(string nomListJson, int vyazPodrKod);
        Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId);
        Task<KnitterPZVModel> UpdatePzvDateEndAsync(int pzvId);
        Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty);
		Task<IReadOnlyList<PzvSplitResult>> SplitPzvAsync(int pzvId, int mode, int qtyFact);
        Task<int> StartWorkingShiftAsync(int tabStart, int? kmaId, string kmaNum);
        Task EndWorkingShiftAsync(int shiftId, int tabEnd);
        Task<(int? kmaId, string kmaNum)> GetZoneByTabAsync(int tab);
		Task<(int? shiftId, DateTime? dateStart)> GetOpenShiftByTabAsync(int tab);
    }
}


