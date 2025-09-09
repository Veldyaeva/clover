using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Service
{
    public class ArticulDataService
    {
        private readonly DbService _dbService;

        public ArticulDataService()
        {
            _dbService = new DbService(new DatabaseHelper());
        }

        public async Task<List<ArticulModel>> GetAllAsync()
        {
            string query = "SELECT * FROM sp_articul";
            return await _dbService.GetListAsync<ArticulModel>(query, new { });
        }

        public async Task<ArticulModel> GetByKodAsync(int kod)
        {
            string query = "SELECT * FROM sp_articul WHERE kod = @kod";
            return await _dbService.GetEntityAsync<ArticulModel>(query, new { kod });
        }

        public async Task SaveAsync(ArticulModel model)
        {
            await _dbService.SaveEntityAsync("sp_articul", "Kod", model);
        }

        public async Task DeleteAsync(ArticulModel model)
        {
            await _dbService.DeleteEntityAsync("sp_articul", "Kod", model);
        }
    }
}
