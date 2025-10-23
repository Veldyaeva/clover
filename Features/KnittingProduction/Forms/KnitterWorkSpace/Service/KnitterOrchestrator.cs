using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Models;
using SewingProduction.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Оркестратор для KnitterWorkSpace: координация загрузки данных без UI/SQL.
    /// </summary>
    public class KnitterOrchestrator
    {
        private readonly KnitterRepository _repo;
        private readonly ILogger _logger;

        public KnitterOrchestrator(KnitterRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public Task<List<FioModel>> GetFioListAsync() => _repo.GetFioListAsync();
        public Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab) => _repo.GetPlanByTabAsync(tab);
        public Task<string> GetFioByTabAsync(int tab) => _repo.GetFioByTabAsync(tab);
    }
}


