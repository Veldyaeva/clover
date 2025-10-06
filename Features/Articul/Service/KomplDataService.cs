using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Core.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

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
            await _dbService.SaveEntityAsync("kompl", "Kod_k", model);
        }

        public async Task DeleteAsync(KomplModel model)
        {
            await _dbService.DeleteEntityAsync("kompl", "Kod_k", model);
        }
        public async Task<bool> ExistsByKodKAsync(int kod_k)
        {
            string q = "SELECT COUNT(1) FROM kompl WHERE kod_k = @kod_k";
            int n = await _dbHelper.ExecuteScalarAsync<int>(q, new { kod_k });
            return n > 0;
        }

        /// Проверка «точно такой же комплект» по канонизированным kod1..kod10 + sost_k
        //public async Task<bool> ExistsExactAsync(KomplModel model)
        //{
        //    string query = @"
        //        SELECT COUNT(1)
        //        FROM kompl
        //        WHERE ISNULL(kod1,0)  = ISNULL(@kod1,0)
        //          AND ISNULL(kod2,0)  = ISNULL(@kod2,0)
        //          AND ISNULL(kod3,0)  = ISNULL(@kod3,0)
        //          AND ISNULL(kod4,0)  = ISNULL(@kod4,0)
        //          AND ISNULL(kod5,0)  = ISNULL(@kod5,0)
        //          AND ISNULL(kod6,0)  = ISNULL(@kod6,0)
        //          AND ISNULL(kod7,0)  = ISNULL(@kod7,0)
        //          AND ISNULL(kod8,0)  = ISNULL(@kod8,0)
        //          AND ISNULL(kod9,0)  = ISNULL(@kod9,0)
        //          AND ISNULL(kod10,0) = ISNULL(@kod10,0)
        //          AND ISNULL(sost_k,'') = ISNULL(@sost_k,'')";
        //    int cnt = await _dbHelper.ExecuteScalarAsync<int>(query, new
        //    {
        //        model.Kod1,
        //        model.Kod2,
        //        model.Kod3,
        //        model.Kod4,
        //        model.Kod5,
        //        model.Kod6,
        //        model.Kod7,
        //        model.Kod8,
        //        model.Kod9,
        //        model.Kod10,
        //        model.Sost_k
        //    });
        //    return cnt > 0;
        //}
        public async Task<bool> ExistsExactAsync(KomplModel model)
        {
            var all = await GetByArticulAsync(model.Articul_k); // или GetAllAsync()
            string keyNew = BuildCompositionKey(model);

            return all.Any(k =>
                BuildCompositionKey(k) == keyNew &&
                string.Equals(k.Sost_k?.Trim() ?? "", model.Sost_k?.Trim() ?? "", StringComparison.OrdinalIgnoreCase));
        }
        private string BuildCompositionKey(KomplModel model)
        {
            var codes = new List<int?>
            {
                model.Kod1, model.Kod2, model.Kod3, model.Kod4, model.Kod5,
                model.Kod6, model.Kod7, model.Kod8, model.Kod9, model.Kod10
            };
            // убираем null/0, сортируем, превращаем в строку
            return string.Join(";", codes.Where(c => c.HasValue && c.Value != 0)
                                         .OrderBy(c => c.Value));
        }
        public bool CheckInProizv(string kod)
        {
            string query = "SELECT top 1 1 FROM View_rzu_rzv_nom_zad WHERE kod_k_pach  = @kod";
            return _dbHelper.Exists(query, new Dictionary<string, object> { { "@kod", kod } });
        }
        public bool CheckNaklRas(string kod)
        {
            string query = "SELECT top 1 1 FROM nakl_ras WHERE kod_k  = @kod";
            return _dbHelper.Exists(query, new Dictionary<string, object> { { "@kod", kod } });
        }
        public bool CheckNabor(string kod)
        {
            string query = "SELECT top 1 1 FROM articulNaborSostav WHERE kod  = @kod";
            return _dbHelper.Exists(query, new Dictionary<string, object> { { "@kod", kod } });
        }
        public bool CheckKompl(string kod)
        {
            string query = "SELECT top 1 1 FROM Kompl WHERE kod_k  = @kod";
            return _dbHelper.Exists(query, new Dictionary<string, object> { { "@kod", kod } });
        }
        public int GetCountByRazmAll(string razmAll)
        {
            string query = "SELECT COUNT(*) FROM Razm WHERE Razm_all = @razmAll";
            var result = _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { { "@razmAll", razmAll } });
            return Convert.ToInt32(result);
        }
    }

}
