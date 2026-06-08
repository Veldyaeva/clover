using SewingProduction.Features.Sprav.Models;
using SewingProduction.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.DataService
{
    internal sealed class VyazKnitEconomAssortDataService
    {
        private readonly DbService _dbService;

        public VyazKnitEconomAssortDataService(DbService dbService)
        {
            _dbService = dbService;
        }

        public Task<List<VyazKnitEconomAssortRow>> LoadRowsAsync(VyazEconomAssortPodrMode mode, bool showAll)
        {
            return _dbService.GetListFromProcedureAsync<VyazKnitEconomAssortRow>(
                "dbo.VyazKnitEconomAssort_LoadRows",
                new { PodrMode = mode.ToString(), ShowAll = showAll });
        }
    }
}
