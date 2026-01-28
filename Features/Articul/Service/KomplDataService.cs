using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using SewingProduction.Core.Models;
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
                    NULLIF(LTRIM(RTRIM(kod1)),  '') AS Kod1,
                    NULLIF(LTRIM(RTRIM(kod2)),  '') AS Kod2,
                    NULLIF(LTRIM(RTRIM(kod3)),  '') AS Kod3,
                    NULLIF(LTRIM(RTRIM(kod4)),  '') AS Kod4,
                    NULLIF(LTRIM(RTRIM(kod5)),  '') AS Kod5,
                    NULLIF(LTRIM(RTRIM(kod6)),  '') AS Kod6,
                    NULLIF(LTRIM(RTRIM(kod7)),  '') AS Kod7,
                    NULLIF(LTRIM(RTRIM(kod8)),  '') AS Kod8,
                    NULLIF(LTRIM(RTRIM(kod9)),  '') AS Kod9,
                    NULLIF(LTRIM(RTRIM(kod10)), '') AS Kod10,
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
            var codes = new List<string>
            {
                model.Kod1, model.Kod2, model.Kod3, model.Kod4, model.Kod5,
                model.Kod6, model.Kod7, model.Kod8, model.Kod9, model.Kod10
            };
            // убираем null/0, сортируем, превращаем в строку
            return string.Join(";", codes.Where(c => c != "")
                                         .OrderBy(c => c));
        }
        public KomplCheckInfo GetKomplRazmCheckInfo(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            var result = new KomplCheckInfo();

            var data = view?.DataSource as List<SpArticulGrupMenViewModel>;
            if (data == null || data.Count == 0)
                return result;

            var kods = data
                .Where(x => x?.Kod != null)
                .Select(x => x.Kod.ToString())
                .Distinct()
                .ToList();

            result.InProizv = CheckInProizv(kods);
            result.InNakl = CheckNaklRas(kods);

            Debug.WriteLine(result.InProizv.Count);
            Debug.WriteLine(result.InNakl.Count);
            Debug.WriteLine(kods.Count);

            if (result.InProizv.Count == kods.Count || result.InNakl.Count == kods.Count)
                result.IsReadOnly = true;

            return result;
        }
        public List<string> CheckInProizv(List<string> kods)
        {
            if (kods == null || kods.Count == 0)
                return new List<string>();

            string query = @"
                SELECT DISTINCT CAST(kod_k_pach AS varchar(50)) 
                FROM View_rzu_rzv_nom_zad
                WHERE kod_k_pach IN @kods";

            return _dbService.GetListSync<string>(query, new { kods });
        }
        public bool CheckInProizvChast(string kod)
        {
            string query = @"
                SELECT vsa.kod,psa.nn
                from View_sp_articul vsa
                inner join plan_sezon_all psa on vsa.kodd = psa.kodd
                inner join plan_sezon_zad psz on psa.nn = psz.nn
                inner join View_rzu_rzv_nom_zad nzp on nzp.nom_zad = psz.nom
                where isnull(psa.psa_id_osn,0)>0
                and vsa.kod = @kod
                group by vsa.kod,psa.nn";

            return _dbHelper.Exists(query, new Dictionary<string, object> { { "@kod", kod } });
        }
        public List<string> CheckNaklRas(List<string> kods)
        {
            if (kods == null || kods.Count == 0)
                return new List<string>();

            string query = @"
                SELECT DISTINCT CAST(kod_k AS varchar(50))
                FROM nakl_ras
                WHERE kod_k IN @kods";

            return _dbService.GetListSync<string>(query, new { kods });
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
    public class KomplCheckInfo
    {
        public List<string> InProizv { get; set; } = new List<string>();
        public List<string> InNakl { get; set; } = new List<string>();

        public bool IsReadOnly { get; set; }
        public string ReadOnlyMessage { get;} = "Только чтение. Нельзя редактировать ни один из размеров комплекта.";
        public bool HasAny => InProizv.Count > 0 || InNakl.Count > 0;
    }
}
