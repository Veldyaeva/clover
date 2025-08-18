using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Articul.Service
{
    public class GrupMenDataService
    {
        private readonly DbService _dbService;

        public GrupMenDataService()
        {
            _dbService = new DbService(new DatabaseHelper());
        }

        public async Task<List<GrupMenModel>> GetAllAsync()
        {
            string query = "SELECT * FROM grup_men";
            return await _dbService.GetListAsync<GrupMenModel>(query, new { });
        }

        public async Task<GrupMenModel> GetByMenAsync(string men)
        {
            string query = "SELECT * FROM grup_men WHERE men = @men";
            return await _dbService.GetEntityAsync<GrupMenModel>(query, new { men });
        }

        public async Task SaveAsync(GrupMenModel model)
        {
            await _dbService.SaveEntityAsync("grup_men", "Men", model);
        }

        public async Task DeleteAsync(GrupMenModel model)
        {
            await _dbService.DeleteEntityAsync("grup_men", "Men", model);
        }
    }
}
