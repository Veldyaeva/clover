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
        /// Получить все записи из articulNaborSostav
        /// </summary>
        public async Task<List<SpArticulNaborSostav>> GetAllAsync()
        {
            string query = @"SELECT ans_id, kod, ta_id, tk_id, id_gost, ag_id, sostav, id_razm_nab, razm 
                             FROM dbo.articulNaborSostav";
            return await _dbService.GetListAsync<SpArticulNaborSostav>(query, new { });
        }

        /// <summary>
        /// Получить список записей по коду артикула
        /// </summary>
        public async Task<List<SpArticulNaborSostav>> GetByKodAsync(string kod)
        {
            string query = @"SELECT  ans.ans_id, ans.kod,    ans.ta_id,    a.Txt_v,
                                     ans.tk_id,    t.TK_NAME,    ans.id_gost,    ans.ag_id,
                                      ggi.N_i,    ans.sostav,    ans.id_razm_nab,    ans.razm
                            FROM dbo.articulNaborSostav AS ans
                            LEFT JOIN gtin.assort AS a ON a.Kod_v = ans.ta_id
                            LEFT JOIN t_v_n AS t ON t.TK_ID = ans.tk_id
                            LEFT JOIN View_GostGrupIzd AS ggi 
                                ON ggi.Ag_id = ans.ag_id 
                               AND ggi.Id_gost = ans.id_gost
                               AND ggi.ag_tk_id = ans.tk_id
                            WHERE ans.kod = @kod";
            return await _dbService.GetListAsync<SpArticulNaborSostav>(query, new { kod });
        }

        /// <summary>
        /// Получить запись по ID
        /// </summary>
        public async Task<SpArticulNaborSostav> GetByIdAsync(int ansId)
        {
            string query = @"SELECT ans_id, kod, ta_id, tk_id, id_gost, ag_id, sostav, id_razm_nab, razm 
                             FROM dbo.articulNaborSostav 
                             WHERE ans_id = @ansId";
            return await _dbService.GetEntityAsync<SpArticulNaborSostav>(query, new { ansId });
        }

        /// <summary>
        /// Сохранить (вставка/обновление)
        /// </summary>
        public async Task SaveAsync(SpArticulNaborSostav model)
        {
            await _dbService.SaveEntityAsync("dbo.articulNaborSostav", "Ans_id", model);
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        public async Task DeleteAsync(SpArticulNaborSostav model)
        {
            await _dbService.DeleteEntityAsync("dbo.articulNaborSostav", "Ans_id", model);
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
        public async Task<List<GostGrupIzdViewModel>> GetGostGrupIzdAsync(int idGost, int idTK)
        {
            string query = @"SELECT Id_gost, Ag_id,Ag_tnved,N_i,N_g FROM dbo.View_GostGrupIzd WHERE arh = 0 
                                AND id_gost = @idGost AND ag_tk_id = @idTK";
            return await _dbService.GetListAsync<GostGrupIzdViewModel>(query, new { idGost , idTK });
        }
        public bool CheckOpis(string kod)
        {
            string query = @"SELECT sad.t_id FROM sp_articul_dateopis_gl_1c sad
                            INNER JOIN  plan_sezon_all psa ON sad.nn = psa.nn
                            INNER JOIN view_sp_articul vsa ON vsa.kodd = psa.kodd 
                            WHERE sad.t_id IS NOT NULL AND vsa.kod = @kod";
            return _dbHelper.Exists(query, new Dictionary<string, object> { { "@kod", kod } });
        }
        public async Task UpdateArtKoplektAsync(string kod)
        {
            string query = @"UPDATE ak
                            SET ak.ag_id_grupgost = ans.ag_id
                            FROM art_komplekt ak
                            INNER JOIN plan_sezon_all psa ON ak.parent_nn = psa.nn AND ak.tk_id = psa.tk_id
                            INNER JOIN view_sp_articul vsa ON vsa.kodd = psa.kodd
                            INNER JOIN articulNaborSostav ans ON ans.kod = vsa.kod AND ans.tk_id = psa.tk_id
                            WHERE vsa.kod = @kod";
            await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object>{ { "@kod", kod }, } );
        }
    }
}
