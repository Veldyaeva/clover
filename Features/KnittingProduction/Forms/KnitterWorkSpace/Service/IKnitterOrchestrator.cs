using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
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
    }
}


