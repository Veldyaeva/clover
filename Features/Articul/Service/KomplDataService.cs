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
    public class KomplDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public KomplDataService()
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
        }

        public async Task<List<KomplModel>> GetAllAsync()
        {
            string query = "SELECT * FROM kompl";
            return await _dbService.GetListAsync<KomplModel>(query, new { });
        }

        public async Task<KomplModel> GetByKodAsync(int kod_k)
        {
            string query = "SELECT * FROM kompl WHERE kod_k = @kod_k";
            return await _dbService.GetEntityAsync<KomplModel>(query, new { kod_k });
        }

        public async Task SaveAsync(KomplModel model)
        {
            await _dbService.SaveEntityAsync("kompl", "kod_k", model);
        }

        public async Task DeleteAsync(KomplModel model)
        {
            await _dbService.DeleteEntityAsync("kompl", "kod_k", model);
        }
        public async Task<bool> ExistsAsync(KomplModel model)
        {
            string query = @"
            SELECT COUNT(1)
            FROM kompl
            WHERE kod1 = @kod1 AND kod2 = @kod2 AND kod3 = @kod3 AND kod4 = @kod4 AND kod5 = @kod5
              AND kod6 = @kod6 AND kod7 = @kod7 AND kod8 = @kod8 AND kod9 = @kod9 AND kod10 = @kod10
              AND sost_k = @sost_k";

            int count = await _dbHelper.ExecuteScalarAsync<int>(query, new
            {
                model.kod1, model.kod2, model.kod3, model.kod4, model.kod5, 
                model.kod6, model.kod7, model.kod8, model.kod9, model.kod10,
                model.sost_k
            });

            return count > 0;
        }
    }

}
