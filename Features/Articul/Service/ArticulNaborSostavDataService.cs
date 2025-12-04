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
        public async Task<List<GostModel>> GetGostNaborAsync(int? idGost, string kod)
        {
            return await _dbService.GetListAsync<GostModel>(
                @"SELECT DISTINCT Id_gost, Name_gost, Opi_gost
                FROM gost 
                WHERE pr_nabor = 1
                ORDER BY Id_gost",
                new {}
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
                            Ans_id = oldSize.Ans_id,
                            razm_old = oldSize.Razm,
                            razm_new = x.Razm
                        };
                    })
                    .Where(r => r != null)
                    .ToList();

                if (!changedMain && razmChanges.Count == 0)
                    return null;

                return new
                {
                    kod = kod,
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

            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //string filePath = Path.Combine(desktopPath, $"NaborUpdate_{DateTime.Now:yyyyMMdd_HHmmss}.json");

            //File.WriteAllText(filePath, json, Encoding.UTF8);
            //Process.Start("notepad.exe", filePath);
            //await Task.CompletedTask;

            MessageBox.Show("Изменения успешно сохранены!", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async Task<List<GostRazmerNabViewModel>> GetGostRazmerSostAsync(int? idGost = null)
        {
            string query = @"SELECT id_razm_nab as Id_razmer, RTRIM (razm) AS Razm, id_gost_nab AS Id_gost, id_gost as Id_gost_parent
                            FROM View_gost_razmer_nab"
                            + (idGost.HasValue ? " WHERE Id_gost = @id_gost" : "");

            return await _dbService.GetListAsync<GostRazmerNabViewModel>(query, new { id_gost = idGost });
        }
        public async Task<List<GostRazmerNabViewModel>> GetGostRazmerNaborAsync(int? idGost = null)
        {
            string query = @"SELECT Id_razmer, id_razm AS Razm, Id_gost
                            FROM gost_sv_razmer"
                            + (idGost.HasValue ? " WHERE Id_gost = @id_gost" : "");

            return await _dbService.GetListAsync<GostRazmerNabViewModel>(query, new { id_gost = idGost });
        }
    }
}
