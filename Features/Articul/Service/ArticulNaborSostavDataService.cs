using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.DataAccess.Native.Excel;
using Newtonsoft.Json;
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
        public async Task<List<GostModel>> GetGostNaborAsync(int? idGost = null)
        {
            return await _dbService.GetListAsync<GostModel>(
                @"SELECT DISTINCT Id_gost, Name_gost, Opi_gost
                FROM gost 
                WHERE pr_nabor = 1 "
                + (idGost.HasValue ? " AND Id_gost = @IdGost" : " ORDER BY Id_gost"),
                new Dictionary<string, object>
                {
                    { "@IdGost", idGost }
                }
            );
        }
        public async Task<List<GostModel>> GetGostNaborRazmAsync(int? idGost, string kod)
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

        public async Task<List<GostModel>> GetGostSostavAsync(int? idGost = null, int? idGostChast = null)
        {
            string query = @"SELECT Id_gost_nab AS Id_gost,
                                Name_gost_nab AS Name_gost,
                                Opi_gost_nab AS Opi_gost,
                                Tk_id_nab AS Tk_id,
                                Id_gost AS Id_glav_gost
                            FROM View_gost_nab"
                            + (idGost.HasValue ? " WHERE Id_gost = @idGost" : "")
                            + (idGostChast.HasValue ? " WHERE Id_gost_nab = @idGostChast" : "");

            return await _dbService.GetListAsync<GostModel>(query, new { idGost, idGostChast });
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
        public async Task UpdateNaborJsonAsync(
            string kod,
            int oldGostMain,
            int newGostMain,
            int? oldGroupMain,
            int? newGroupMain,
            List<SpArticulNaborSostav> newList,
            List<SpArticulNaborSostav> oldList)
        {
            var head = new
            {
                kod = kod,
                id_gost_main_old = oldGostMain,
                id_gost_main_new = newGostMain,
                ag_id_main_old = oldGroupMain,
                ag_id_main_new = newGroupMain
            };
            var items = newList
            .GroupBy(x => x.Tk_id)
            .Select(g =>
            {
                var newRow = g.First();
                var oldRow = oldList.FirstOrDefault(o => o.Tk_id == g.Key);
                if (oldRow == null)
                    return null;

                bool changedMain =
                    oldRow.Id_gost != newRow.Id_gost ||
                    oldRow.Ag_id != newRow.Ag_id ||
                    !string.Equals(oldRow.Sostav, newRow.Sostav, StringComparison.OrdinalIgnoreCase);

                // --- размеры ---
                var razmChanges = g
                    .Select(x =>
                    {
                        var oldSize = oldList.FirstOrDefault(o => o.Ans_id == x.Ans_id);
                        if (oldSize == null) return null;

                        if (oldSize.Razm == x.Razm)
                            return null;

                        return new
                        {
                            Kod = oldSize.Kod,
                            Ans_id = oldSize.Ans_id,
                            razm_old = oldSize.Razm,
                            razm_new = x.Razm,
                            razm_all_old = oldSize.Razm_all,
                            razm_all_new = x.Razm_all
                        };
                    })
                    .Where(r => r != null)
                    .ToList();

                if (!changedMain && razmChanges.Count == 0)
                    return null;

                return new
                {
                    //kod = kod,
                    tk_id = g.Key,

                    id_gost_old = oldRow.Id_gost,
                    id_gost_new = newRow.Id_gost,

                    ag_id_old = oldRow.Ag_id,
                    ag_id_new = newRow.Ag_id,

                    sostav_old = oldRow.Sostav,
                    sostav_new = newRow.Sostav,

                    razm = razmChanges
                };
            })
            .Where(x => x != null)
            .ToList();


            if (items.Count == 0
                && oldGostMain == newGostMain
                && oldGroupMain == newGroupMain)
            { 
                MessageBox.Show("Изменений не обнаруженно!", "Сохранение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var finalJson = new
            {
                head = head,
                items = items
            };

            //string json = JsonConvert.SerializeObject(payload);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(finalJson, Newtonsoft.Json.Formatting.Indented);

            var param = new Dictionary<string, object>
                {
                    { "@ListJson", json }
                };

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filePath = Path.Combine(desktopPath, $"NaborUpdate_{DateTime.Now:yyyyMMdd_HHmmss}.json");

            File.WriteAllText(filePath, json, Encoding.UTF8);
            Process.Start("notepad.exe", filePath);
            await Task.CompletedTask;

            var result = await _dbService.GetListAsync<dynamic>(
                "EXEC dbo.UpdateNaborFromJson @ListJson",
                new { ListJson = json }
            );

            if (result != null && result.Count > 0)
            {
                var row = result.First();

                int errorCode = row.error;
                string errorMessage = row.messageerror;

                if (errorCode != 0)
                    throw new Exception("Ошибка SQL: " + errorMessage);
            }


            MessageBox.Show("Изменения успешно сохранены!", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
       
        public async Task<string> UpdateNaborByKodJsonAsync(
            string kod,
            int oldGostMain,
            int newGostMain,
            int? oldGroupMain,
            int? newGroupMain,
            List<SpArticulNaborSostav> newList,
            List<SpArticulNaborSostav> oldList)
        {
            var head = new
            {
                kod = kod,
                id_gost_main_old = oldGostMain,
                id_gost_main_new = newGostMain,
                ag_id_main_old = oldGroupMain,
                ag_id_main_new = newGroupMain
            };

            var items = newList
                .GroupBy(x => x.Tk_id)
                .Select(g =>
                {
                    var newRow = g.First();
                    var oldRow = oldList.FirstOrDefault(o => o.Tk_id == g.Key);
                    if (oldRow == null) return null;

                    bool changedMain =
                        oldRow.Id_gost != newRow.Id_gost ||
                        oldRow.Ag_id != newRow.Ag_id ||
                        !string.Equals(oldRow.Sostav, newRow.Sostav, StringComparison.OrdinalIgnoreCase);

                    // размеры
                    var razmChanges = g
                        .Select(x =>
                        {
                            var oldSize = oldList.FirstOrDefault(o => o.Ans_id == x.Ans_id);
                            if (oldSize == null) return null;

                            if (oldSize.Razm == x.Razm && oldSize.Razm_all == x.Razm_all)
                                return null;

                            return new
                            {
                                Kod = oldSize.Kod,
                                Ans_id = oldSize.Ans_id,
                                razm_old = oldSize.Razm,
                                razm_new = x.Razm,
                                razm_all_old = oldSize.Razm_all,
                                razm_all_new = x.Razm_all
                            };
                        })
                        .Where(r => r != null)
                        .ToList();

                    if (!changedMain && razmChanges.Count == 0)
                        return null;

                    return new
                    {
                        tk_id = g.Key,
                        id_gost_old = oldRow.Id_gost,
                        id_gost_new = newRow.Id_gost,
                        ag_id_old = oldRow.Ag_id,
                        ag_id_new = newRow.Ag_id,
                        sostav_old = oldRow.Sostav,
                        sostav_new = newRow.Sostav,
                        razm = razmChanges
                    };
                })
                .Where(x => x != null)
                .ToList();

            if (items.Count == 0
                && oldGostMain == newGostMain
                && oldGroupMain == newGroupMain)
            {
                MessageBox.Show("Изменений не обнаружено!", "Сохранение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            var finalJson = new
            {
                head = head,
                items = items
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(finalJson, Newtonsoft.Json.Formatting.Indented);

            var result = await _dbService.GetListAsync<dynamic>(
                "EXEC dbo.UpdateNaborByKodFromJson @ListJson",
                new { ListJson = json }
            );

            if (result != null && result.Count > 0)
            {
                var row = result.First();
                int errorCode = row.error;
                string errorMessage = row.messageerror;

                if (errorCode != 0)
                    throw new Exception("Ошибка SQL: " + errorMessage);
            }
            return json;
        }
        public async Task UpdateNaborByNnJsonAsync(string applyBaseJson, List<string> selectedNns)
        {
            if (string.IsNullOrWhiteSpace(applyBaseJson))
                throw new ArgumentException("applyBaseJson пустой", nameof(applyBaseJson));

            if (selectedNns == null || selectedNns.Count == 0)
            {
                MessageBox.Show("Не выбраны nn для применения изменений.", "Сохранение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // чистим nn
            var nns = selectedNns
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (nns.Count == 0)
                return;

            // ВАЖНО: в SQL ты читаешь $.selected_nn
            var jObj = Newtonsoft.Json.Linq.JObject.Parse(applyBaseJson);
            jObj["selected_nn"] = new Newtonsoft.Json.Linq.JArray(nns);

            string finalJson = jObj.ToString(Newtonsoft.Json.Formatting.Indented);

            var result = await _dbService.GetListAsync<dynamic>(
                "EXEC dbo.UpdateNaborByNnFromJson @ListJson",
                new { ListJson = finalJson }
            );

            // Если процедура вернула ошибку через SELECT messageerror/error (как в твоём старом варианте)
            if (result != null && result.Count > 0)
            {
                var row = result.First();
                int errorCode = row.error;
                string errorMessage = row.messageerror;

                if (errorCode != 0)
                    throw new Exception("Ошибка SQL: " + errorMessage);
            }

            MessageBox.Show("Изменения применены к выбранным nn!", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async Task<List<GostRazmerNabViewModel>> GetGostRazmerNaborAsync(int? idGost = null)
        {
            string query = @"SELECT id_razm as Id_razmer, RTRIM (gr.razm) AS Razm, Id_gost, id as Id_gost_parent
                            FROM gost_sv_razmer gsr
                            LEFT JOIN gost_razmer gr ON gr.id_rost = gsr.id_razmer"
                            + (idGost.HasValue ? " WHERE Id_gost = @id_gost" : "");

            return await _dbService.GetListAsync<GostRazmerNabViewModel>(query, new { id_gost = idGost });
        }
        public async Task<List<GostRazmerNabViewModel>> GetGostRazmerSostAsync(int? idGost = null)
        {
            string query = @"SELECT id_razm_nab as Id_razmer, RTRIM (razm) AS Razm, id_gost_nab AS Id_gost, id_gost as Id_gost_parent
                            FROM View_gost_razmer_nab"
                            + (idGost.HasValue ? " WHERE Id_gost = @id_gost" : "");

            return await _dbService.GetListAsync<GostRazmerNabViewModel>(query, new { id_gost = idGost });
        }
        public async Task<List<PlanSezonAllModel>> GetPlanSezonAllByKod(int? kodd = null)
        {
            string query = @"SELECT Psa_id, Nn, Tb_id, Articul, Mod, Tg_id_n, Tgm_id_n, Men, Text_mo
                            FROM plan_sezon_all"
                            + (kodd.HasValue ? " WHERE kodd = @kodd" : "");

            return await _dbService.GetListAsync<PlanSezonAllModel>(query, new { kodd });
        }
        public async Task<List<ArtKomplektModel>> GetArtKomplektByNn(string? nn = null)
        {
            string query = @"SELECT ak.Npp, ak.Ak_id, ak.Tk_id, ak.Parent_nn, ak.Id_gost, ak.Ag_id_grupgost, t.Tk_name, ak.Tg_id_n, ak.Tgm_id_n
                            FROM art_komplekt ak
                            LEFT JOIN t_v_n AS t ON t.TK_ID = ak.tk_id"
                            + (nn != null ? " WHERE Parent_nn = @nn" : "");

            return await _dbService.GetListAsync<ArtKomplektModel>(query, new { nn });
        }
        public async Task<ArticulModel> GetArtByKodAsync(string kod)
        {
            string query = "SELECT * FROM sp_articul WHERE kod = @kod";
            return await _dbService.GetEntityAsync<ArticulModel>(query, new { kod });
        }
        #region EditNaborSostavPart
        public async Task UpdatePlanSezonAll(PlanSezonAllModel PSA)
        {
            await _dbService.UpdateEntityAsync("plan_sezon_all", "Psa_id", PSA);
        }
        public async Task UpdateArtKomplekt(ArtKomplektModel AK)
        {
            await _dbService.UpdateEntityAsync("art_komplekt", "Ak_id", AK);
        }
        public async Task UpdateSpravNoskiDetal(int? id_spr, int? tcds_id)
        {
            await _dbService.UpdateFieldAsync("GLOBAL.PLANETA.dbo.TOVAR_CAT_DYNSIGN", "tcds_id_spr", id_spr, "tcds_id ", tcds_id);
        }
        public async Task<List<TovarClassModel>> GetTovarClass()
        {
            string query = @"SELECT * FROM GLOBAL.PLANETA.dbo.TOVAR_class ORDER BY TC_CLASSNAME";

            return await _dbService.GetListAsync<TovarClassModel>(query, new { });
        }
        public async Task<List<TovarGroupModel>> GetTovarGroup()
        {
            string query = @"SELECT * FROM GLOBAL.PLANETA.dbo.tovar_group ORDER BY TG_GROUPNAME";

            return await _dbService.GetListAsync<TovarGroupModel>(query, new { });
        }
        public async Task<List<TovarCategoryModel>> GetTovarCategory()
        {
            string query = @"SELECT * from GLOBAL.PLANETA.dbo.tOVAR_CATEGORY ORDER BY TCAT_CATEGORYNAME";

            return await _dbService.GetListAsync<TovarCategoryModel>(query, new { });
        }
        public async Task<List<TovarCatDynsignModel>> GetTovarCatDynsign()
        {
            string query = @"SELECT * FROM GLOBAL.PLANETA.dbo.TOVAR_CAT_DYNSIGN where tcds_name<>' ' ORDER BY TCDS_NAME";

            return await _dbService.GetListAsync<TovarCatDynsignModel>(query, new { });
        }
        public async Task<List<SpravNoskiDetalModel>> GetSpravNoskiDetal()
        {
            string query = @"SELECT * from [dbo].[SpravNoskiDetal]";

            return await _dbService.GetListAsync<SpravNoskiDetalModel>(query, new { });
        }
        #endregion 
    }
}
