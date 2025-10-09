using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SewingProduction.Core.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Articul
{
    public class ArtNewDataService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        public ArtNewDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            _dbService = new DbService(_dbHelper);
        }
        public DataTable GetGostUst()
        {
            string query = "SELECT id_gost AS 'Ид' ,trim(name_gost) AS 'Гост' ,TRIM(opi_gost) AS 'Описание' FROM gost"; // ust=1 убран
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetViewTovarMarka()
        {
            string query = "SELECT TRIM(kodsp) AS kle, TRIM(m_naimen) AS 'Наименование' FROM dbo.view_tovar_marka WHERE tmOwn = 1 ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetViewGrupMen()
        {
            string query = "SELECT men_id, TRIM(name) AS 'Наименование' FROM view_grup_men WHERE men_id >0 order by men_id ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetTovarCatDynsign()
        {
            string query = "SELECT tcds_name AS 'Признак', tcds_id FROM global.planeta.dbo.TOVAR_CAT_DYNSIGN WHERE tcds_tcat_id in (886,895) ORDER BY TCDS_NAME ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvPictAndArticulGrup(string idGost = null)
        {
            string condition = string.IsNullOrWhiteSpace(idGost) ? "" : $" WHERE id_gost = '{idGost}'";
            string query = $"SELECT DISTINCT TRIM(ag_naimen) AS 'Наименование', Id_art, Ag_id, Ag_tnved FROM gost_sv_pict LEFT JOIN articul_grup ON articul_grup.ag_id=gost_sv_pict.id_art " + condition;
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvRazmerAndGostRazmer(string idGost = null)
        {
            string condition = string.IsNullOrWhiteSpace(idGost) ? "" : $" WHERE id_gost = '{idGost}'";
            string query = $"SELECT DISTINCT TRIM(razm) AS 'Размер' FROM gost_sv_razmer LEFT JOIN gost_razmer ON gost_sv_razmer.id_razmer = gost_razmer.id_rost " + condition;
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetSpArticulByKod(string kodSQL)
        {
            string query = $"SELECT kod, gost, id_gost, articul, trim(razm) AS 'Размер', kle, mod, grup, ag_id, kod_tnved, CAST(grupp AS INT) AS men_id FROM sp_articul WHERE kod = @kodSQL";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@kodSQL", kodSQL } });
        }
        public int GetArticulByKod(string kod)
        {
            string query = "SELECT kod FROM sp_articul WHERE kod = @kod";
            return _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { { "@kod", kod } });
        }
        public int GetFreeKod(int kod)
        {
            string query = @" SELECT TOP 1 candidate AS FreeHundred
                FROM (
                    SELECT number * 100 + 10000000 AS candidate 
                    FROM master..spt_values   
                    WHERE type = 'P'
                    ) c
                    WHERE NOT EXISTS 
                        (    
                        SELECT 1 FROM sp_articul a
                        WHERE TRY_CAST(a.kod AS INT) BETWEEN c.candidate AND c.candidate + 99
                        )
                ORDER BY candidate";
            return _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { { "@kod", kod } });
        }
        public async Task SaveAsync(ArticulModel model)
        {
            await _dbService.SaveEntityAsync("sp_articul", "Kod", model);
        }
        public async Task<dynamic?> GetCordNameAsync(string kod)
        {
            string sql = @"SELECT RTRIM(gr)+' '+NAME AS name_k
                   FROM dop_ras_gr 
                   WHERE kod=@kod AND k1=LEFT(@kod,2)";
            return await _dbService.GetEntityAsync<dynamic>(sql, new { kod });
        }

        public async Task<dynamic?> GetCordLengthAsync(string kod)
        {
            string sql = @"
                SELECT pv.idvalue, pv.cfp_name, pv.cfl_name,
                       valpar.nameunit, valpar.cnt_id
                FROM cfn.confection_view_parameteresWithValue pv
                LEFT JOIN cfn.confection_view_parameteres valpar 
                       ON pv.cfp_id = valpar.cfp_id 
                      AND LEFT(pv.kod,4) = LEFT(valpar.kod_confection,4)
                WHERE pv.cfp_id=37 AND LEFT(pv.kod,7)=@kod";

            return await _dbService.GetEntityAsync<dynamic>(sql, new { kod });
        }


    }
}
