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
        public async Task<List<ArticulModel>> GetArtPreviewAsync()
        {
            string query = "select * from dbo.view_art";

            return await _dbService.GetListAsync<ArticulModel>(query, new { });
            //string query = $"select * from dbo.view_art";
            //kodd,kod, grup, articul, razm, mod, kle
        }
        public async Task<ArticulModel> GetByKodAsync(string kod)
        {
            string query = "SELECT * FROM dbo.viewArticul_preview WHERE kod = @kod";
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
