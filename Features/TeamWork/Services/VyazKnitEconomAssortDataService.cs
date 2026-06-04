using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{
    internal sealed class VyazKnitEconomAssortDataService
    {
        private readonly DbService _dbService;

        public VyazKnitEconomAssortDataService(DbService dbService)
        {
            _dbService = dbService;
        }

        public Task<List<VyazKnitEconomAssortRow>> LoadRowsAsync(VyazEconomAssortPodrMode mode)
        {
            var (whereSql, parameters) = BuildPodrFilter(mode);
            var query = $@"
SELECT
  nn,
  nom_zadany,
  razm_ryad,
  pach_min,
  pach_max,
  nom,
  articul,
  mod,
  date_econom,
  last_otm_izm,
  grup,
  data_cd_min,
  koef_zatrat,
  id_podr,
  seb_all
FROM dbo.view_seb_vyaz_econom_assort WITH (NOLOCK)
WHERE {whereSql}
ORDER BY data_cd_min";

            return _dbService.GetListAsync<VyazKnitEconomAssortRow>(query, parameters);
        }

        private static (string whereSql, object parameters) BuildPodrFilter(VyazEconomAssortPodrMode mode)
        {
            return mode switch
            {
                VyazEconomAssortPodrMode.Sock => ("id_podr IN (10, 16)", new { }),
                VyazEconomAssortPodrMode.Knit => ("id_podr = 6", new { }),
                VyazEconomAssortPodrMode.Cord => ("id_podr = 19", new { }),
                _ => ("1 = 1", new { })
            };
        }
    }
}
