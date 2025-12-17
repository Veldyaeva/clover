using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public interface IKnitterRepository
    {
        Task<List<FioModel>> GetFioListAsync();
        Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab);
        Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab, int? kwsId, bool onlyUnassigned, bool includeFinished, decimal maxHours);
        Task<string> GetFioByTabAsync(int tab);
        Task<List<PlanZagrVyaz>> GetPlanTreeByTabAsync(int tab);
        Task UpdatePzvTabAsync(IEnumerable<int> pzvIds, int tab);
        //Task<List<PlanZagrVyazOper>> GetPlanZagrVyazByPachListAsync(string nomListJson, int vyazPodrKod);
        Task<KnitterPZVModel> UpdatePzvDateStartAsync(int pzvId);
        Task<KnitterPZVModel> UpdatePzvDateEndAsync(int pzvId);
        Task<IReadOnlyList<PzvSplitResult>> SplitPzvByFactAsync(int pzvId, int factQty);
		Task<IReadOnlyList<PzvSplitResult>> SplitPzvByModeAsync(int pzvId, int mode, int qtyFact);
        Task<int> StartWorkingShiftAsync(int tabStart, int? kmaId, string kmaNum, int? kmsId);
        Task EndWorkingShiftAsync(int shiftId, int tabEnd);
        Task<(int? kmaId, string kmaNum)> GetZoneByTabAsync(int tab);
		Task<(int? shiftId, DateTime? dateStart)> GetOpenShiftByTabAsync(int tab);
		Task UpdatePzvKwsIdAsync(IEnumerable<int> pzvIds, int kwsId);
    }
}


