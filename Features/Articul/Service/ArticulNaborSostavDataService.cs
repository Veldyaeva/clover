using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Articul.Service
{
    public class ArticulNaborSostavDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public ArticulNaborSostavDataService()
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
        }
        /// <summary>
        /// Получить список записей по коду артикула
        /// </summary>
        public async Task<List<SpArticulNaborSostav>> GetByKodAsync(string kod)
        {
            string query = @"SELECT  ans.ans_id,   ans.kod,          ans.ta_id,    a.Txt_v,
                                     ans.tk_id,    t.TK_NAME,        ans.id_gost,  ans.ag_id,
                                     ggi.N_i,      ggi.Ag_name_sokr, ans.sostav,   ans.id_razm_nab,    
                                     ans.razm,     vsa.razm AS razm_all
                            FROM dbo.articulNaborSostav AS ans
                            LEFT JOIN gtin.assort AS a ON a.Kod_v = ans.ta_id
                            LEFT JOIN t_v_n AS t ON t.TK_ID = ans.tk_id
                            LEFT JOIN View_GostGrupIzd AS ggi 
                                ON ggi.Ag_id = ans.ag_id 
                               AND ggi.Id_gost = ans.id_gost
                               AND ggi.ag_tk_id = ans.tk_id
                            LEFT JOIN view_sp_articul vsa ON vsa.kod = ans.kod
                            WHERE ans.kod LIKE @kod
                            ORDER BY ans.tk_id;";
            return await _dbService.GetListAsync<SpArticulNaborSostav>(query, new { kod = $"{kod.Substring(0, 7)}%" });
        }
        public async Task<List<AssortModel>> GetAssortAsync()
        {
            string query = @"SELECT Kod_v, Txt_v FROM gtin.assort";
            return await _dbService.GetListAsync<AssortModel>(query, new { });
        }
        public async Task<List<TvnModel>> GetTvnAsync()
        {
            string query = @"SELECT TK_ID, TK_NAME, Men FROM t_v_n";
            return await _dbService.GetListAsync<TvnModel>(query, new { });
        }
        public async Task<List<GostModel>> GetGostAsync()
        {
            string query = @"SELECT Id_gost, Name_gost, Opi_gost FROM gost";
            return await _dbService.GetListAsync<GostModel>(query, new { });
        }
        public async Task<List<GostGrupIzdViewModel>> GetGostGrupIzdAsync()
        {
            string query = @"SELECT Id_gost, Ag_id,Ag_name_sokr,Ag_tnved,N_i,N_g FROM dbo.View_GostGrupIzd WHERE arh = 0 ";
            return await _dbService.GetListAsync<GostGrupIzdViewModel>(query, new {});
        }
        public bool CheckOpis(string kod)
        {
            string query = @"SELECT sad.t_id FROM sp_articul_dateopis_gl_1c sad
                            INNER JOIN  plan_sezon_all psa ON left(sad.nn,10) = psa.nn
                            INNER JOIN view_sp_articul vsa ON vsa.kodd = psa.kodd 
                            WHERE vsa.kodd = @kod";
            return _dbHelper.Exists(query, new Dictionary<string, object> { { "@kod", $"{kod.Substring(0, 7)}" } });
        }
        public bool CheckPovt(string kod)
        {
            string query = @"
                SELECT sad.t_id
                FROM sp_articul_dateopis_gl_1c sad
                INNER JOIN plan_sezon_all psa ON LEFT(sad.nn,10) = psa.nn
                WHERE psa.kodd = 2024370
                GROUP BY sad.t_id
                HAVING COUNT(DISTINCT sad.t_art_poln) > 1";
            return _dbHelper.Exists(query, new Dictionary<string, object> { { "@kod", $"{kod.Substring(0, 7)}" } });
        }
        public async Task UpdateArticulNaborSostavAsync(SpArticulNaborSostav model, int oldAgId)
        {
            string query = @"EXEC UpdateArticulNaborSostavTransaction @kod, @TkId, @IdGost, @oldAgId, @newAgId, @sostav";
            await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> {
                { "@Kod", $"{model.Kod.Substring(0, 7)}%" }, //kodd
                { "@TkId", model.Tk_id },
                { "@IdGost", model.Id_gost },
                { "@oldAgId", oldAgId },
                { "@newAgId", model.Ag_id },
                { "@sostav", model.Sostav}
            });
        }
    }
}
