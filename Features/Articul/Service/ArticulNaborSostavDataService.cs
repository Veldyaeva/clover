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
            return await _dbService.GetListAsync<SpArticulNaborSostav>(
                "EXEC dbo.sp_GetArticulNaborSostavByKod @kod",
                new Dictionary<string, object>
                {
                    { "@kod", $"{kod.Substring(0, 7)}%" }
                }
            );
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
        public async Task<List<GostModel>> GetGostNaborAsync(int? idGost, string kod)
        {
            return await _dbService.GetListAsync<GostModel>(
                "EXEC dbo.GetCompatibleGostByArticul @IdGost, @Kod",
                new Dictionary<string, object>
                {
                    { "@IdGost", idGost },
                    { "@Kod", kod.Substring(0, 7) }
                }
            );
        }

        public async Task<List<GostModel>> GetGostSostavAsync(int? idGost = null)
        {
            string query = @"SELECT Id_gost_nab AS Id_gost,
                                Name_gost_nab AS Name_gost,
                                Opi_gost_nab AS Opi_gost,
                                Tk_id_nab AS Tk_id,
                                Id_gost AS Id_glav_gost
                            FROM View_gost_nab"
                            + (idGost.HasValue ? " WHERE Id_gost = @id_gost" :"");

            return await _dbService.GetListAsync<GostModel>(query, new { id_gost = idGost});
        }
        public async Task<List<GostGrupIzdViewModel>> GetGostGrupIzdNaborAsync(int[] Ids = null, int? tkId = null)
        {
            string query = @"SELECT Id_gost, Ag_id, Ag_name_sokr, Ag_tnved, N_i, N_g , ag_tk_id AS tk_id
                     FROM dbo.View_GostGrupIzd
                     WHERE arh = 0 "
                             + ((Ids != null && Ids.Length > 0) ? " AND Id_gost IN @Ids" : "")
                             + (tkId.HasValue ? " AND ag_tk_id = @tkId" : "");
            return await _dbService.GetListAsync<GostGrupIzdViewModel>(query, new { Ids, tkId });
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
                WHERE psa.kodd = @kod
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
        public async Task<List<GostRazmerNabViewModel>> GetGostRazmerSostAsync(int? idGost = null)
        {
            string query = @"SELECT id_razm_nab as Id_razmer, Razm, id_gost_nab AS Id_gost, id_gost as Id_gost_parent
                            FROM View_gost_razmer_nab"
                            + (idGost.HasValue ? " WHERE Id_gost = @id_gost" : "");

            return await _dbService.GetListAsync<GostRazmerNabViewModel>(query, new { id_gost = idGost });
        }
    }
}
