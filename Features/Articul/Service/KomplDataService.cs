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

        public async Task<List<KomplModel>> GetByArticulAsync(string articul_k)
        {
            string query = @"
                SELECT
                    kod_k,
                    grup_k,
                    articul_k,
                    mod_k,
                    razm_k,
                    sost_k,
                    TRY_CONVERT(int, NULLIF(TRIM(kod1),  ''))  AS kod1,
                    TRY_CONVERT(int, NULLIF(TRIM(kod2),  ''))  AS kod2,
                    TRY_CONVERT(int, NULLIF(TRIM(kod3),  ''))  AS kod3,
                    TRY_CONVERT(int, NULLIF(TRIM(kod4),  ''))  AS kod4,
                    TRY_CONVERT(int, NULLIF(TRIM(kod5),  ''))  AS kod5,
                    TRY_CONVERT(int, NULLIF(TRIM(kod6),  ''))  AS kod6,
                    TRY_CONVERT(int, NULLIF(TRIM(kod7),  ''))  AS kod7,
                    TRY_CONVERT(int, NULLIF(TRIM(kod8),  ''))  AS kod8,
                    TRY_CONVERT(int, NULLIF(TRIM(kod9),  ''))  AS kod9,
                    TRY_CONVERT(int, NULLIF(TRIM(kod10), ''))  AS kod10,
                    compName
                FROM kompl
                WHERE articul_k = @articul_k";
            return await _dbService.GetListAsync<KomplModel>(query, new { articul_k });
        }

        public async Task SaveAsync(KomplModel model)
        {
            await _dbService.SaveEntityAsync("kompl", "kod_k", model);
        }

        public async Task DeleteAsync(KomplModel model)
        {
            await _dbService.DeleteEntityAsync("kompl", "kod_k", model);
        }
        public async Task<bool> ExistsByKodKAsync(int kod_k)
        {
            string q = "SELECT COUNT(1) FROM kompl WHERE kod_k = @kod_k";
            int n = await _dbHelper.ExecuteScalarAsync<int>(q, new { kod_k });
            return n > 0;
        }

        /// Проверка «точно такой же комплект» по канонизированным kod1..kod10 + sost_k
        public async Task<bool> ExistsExactAsync(KomplModel model)
        {
            string query = @"
                SELECT COUNT(1)
                FROM kompl
                WHERE ISNULL(kod1,0)  = ISNULL(@kod1,0)
                  AND ISNULL(kod2,0)  = ISNULL(@kod2,0)
                  AND ISNULL(kod3,0)  = ISNULL(@kod3,0)
                  AND ISNULL(kod4,0)  = ISNULL(@kod4,0)
                  AND ISNULL(kod5,0)  = ISNULL(@kod5,0)
                  AND ISNULL(kod6,0)  = ISNULL(@kod6,0)
                  AND ISNULL(kod7,0)  = ISNULL(@kod7,0)
                  AND ISNULL(kod8,0)  = ISNULL(@kod8,0)
                  AND ISNULL(kod9,0)  = ISNULL(@kod9,0)
                  AND ISNULL(kod10,0) = ISNULL(@kod10,0)
                  AND ISNULL(sost_k,'') = ISNULL(@sost_k,'')";
            int cnt = await _dbHelper.ExecuteScalarAsync<int>(query, new
            {
                model.kod1,
                model.kod2,
                model.kod3,
                model.kod4,
                model.kod5,
                model.kod6,
                model.kod7,
                model.kod8,
                model.kod9,
                model.kod10,
                model.sost_k
            });
            return cnt > 0;
        }

    }

}
