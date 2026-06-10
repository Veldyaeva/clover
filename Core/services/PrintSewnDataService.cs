using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.CodeParser;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Core.services
{
    public class PrintSewnDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;
        public PrintSewnDataService()
        {
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
        }
        # region CuttingForm
        public async Task<bool> GetNotPrintVshAsync(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                return false;

            var result = await _dbService.SelectOneFieldAsync<int?>(
                "dbo.view_infoZadany",
                "notPrintVsh",
                new Dictionary<string, object>
                {
                    { "nom", nom },
                    { "notPrintVsh", 1 }
                });

            return result.HasValue;
        }
        public async Task<bool> HasKombProvAsync(string kod)
        {
            if (string.IsNullOrWhiteSpace(kod))
                return false;

            string query = @"
            SELECT TOP 1 1
            FROM [gtin].[kompl_normalized] kk
            LEFT JOIN dbo.view_sp_articul sp ON kk.kod_k = sp.kod
            WHERE kk.kod_n = @kod
              AND sp.grup = '10'";

            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@kod", kod }
            });

            return result != null && result != DBNull.Value;
        }
        public async Task<List<ViewPlanSezonAll>> GetCheckMatrix(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                return new List<ViewPlanSezonAll>();

            string query = @"
            select top 1 Tk_id,T_typeUp 
            from view_plan_sezon_all psa 
            inner join plan_sezon_zad psz on psa.nn=psz.nn 
            where psz.nom=@nom";

            return await _dbService.GetListAsync<ViewPlanSezonAll>(query, new { nom });
        }
        public async Task<List<raskrZehUpView>> GetPachKodRZU(string nom, string nom_n, string kod, string kod_k = null)
        {
            string kodValue = string.IsNullOrWhiteSpace(kod_k) ? kod : kod_k;
            string query = @"
                SELECT
                    pach_kod,
                    n_pach,
                    mod,
                    mod_k,
                    kod,
                    kod_k,
                    articul,
                    articul_k,
                    razm,
                    kol
                FROM dbo.raskr_zeh_up
                WHERE nom = @nom
                  AND nom_n = @nom_n ";

            query += string.IsNullOrWhiteSpace(kod_k) 
                ? "AND LEFT(kod, 5) = LEFT(@kodValue, 5)"
                : "AND LEFT(kod_k, 5) = LEFT(@kodValue, 5)";

            return await _dbService.GetListAsync<raskrZehUpView>(query, new { nom, nom_n, kodValue });
        }
        public async Task UpdRZUvsh(string pachKod)
        {
            await _dbService.UpdateFieldAsync("raskr_zeh_up", "vsh", 1, "pach_kod", pachKod);
        }

        #endregion
        #region PrintSewn
        public async Task<List<ArticulModel>> GetRas2ByKod(string kod)
        {
            if (string.IsNullOrWhiteSpace(kod))
                return new List<ArticulModel>();

            string query = @"
            select 
                kod,
                grup,
                articul,
                mod,
                kle,
                razm,
                razm as razm1,
                razm as razm2,
                dbo.fn_AddSpaceToComposition(sost) as sost,
                dbo.fn_AddSpaceToComposition(sost2) as sost2,
                dbo.fn_AddSpaceToComposition(sost3) as sost3,
                id_gost,
                gost,
                id_svyaz,
                kruj
            from view_sp_articul
            where kod = @kod";

            return await _dbService.GetListAsync<ArticulModel>(query, new { kod });
        }

        public async Task<List<GostModel>> GetGostById(int? idGost)
        {
            if (!idGost.HasValue)
                return new List<GostModel>();

            string query = @"
            SELECT *
            FROM gost
            WHERE id_gost = @id_gost";

            return await _dbService.GetListAsync<GostModel>(query, new { id_gost = idGost.Value });
        }
        public async Task<List<GostSvPictModel>> GetGostSvPictByIdGost(int? idGost)
        {
            if (!idGost.HasValue)
                return new List<GostSvPictModel>();

            string query = @"
            SELECT *
            FROM gost_sv_pict
            WHERE id_gost = @id_gost";

            return await _dbService.GetListAsync<GostSvPictModel>(query, new { id_gost = idGost.Value });
        }
        public async Task<List<string>> GetGostPred(int go, int gr)
        {
            string query = @"
            SELECT pred
            FROM gost_sv_pred
            WHERE go = @go AND gr = @gr";

            return await _dbService.GetListAsync<string>(query, new { go, gr });
        }
        public async Task<List<KomplModel>> GetExistKompl(string kod)
        {
            if (string.IsNullOrWhiteSpace(kod))
                return null;

            string query = @"
            SELECT * FROM kompl WHERE kod_k=@kod";

            return await _dbService.GetListAsync<KomplModel>(query, new { kod = kod });
        }
        public async Task<List<KomplNormalized>> GetKompProv(string kod)
        {
            if (string.IsNullOrWhiteSpace(kod))
                return null;
            
            string query = @"
            SELECT distinct dbo.fn_AddSpaceToComposition(sost) as sost, 
            sost as sost1, 
            dbo.fn_AddSpaceToComposition(sost2) as sost2, 
            dbo.fn_AddSpaceToComposition(sost3) as sost3 
            FROM [gtin].[kompl_normalized] kk 
			left join view_sp_articul sp on kod_k = sp.kod 
            WHERE kod_n = @kod";

            return await _dbService.GetListAsync<KomplNormalized>(query, new { kod = kod });
        }
        #region широкие ЭЙС, Клевер (новый)
        public async Task<int?> GetPsaIdOsnByNomAsync(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                return null;

            string query = @"
            SELECT TOP 1 psa_id_osn
            FROM [ACE].[dbo].[View_plan_sezon_zad_svyaz_zadany]
            WHERE nom = @nom";

            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@nom", nom }
            });

            if (result == null || result == DBNull.Value)
                return null;

            return Convert.ToInt32(result);
        }
        public async Task<bool> IsNaborAsync(string kod)
        {
            if (string.IsNullOrWhiteSpace(kod))
                return false;

            string query = @"
            SELECT TOP 1 1
            FROM view_articulNaborSostav
            WHERE SUBSTRING(kod, 1, 7) = SUBSTRING(@kod, 1, 7)";

            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@kod", kod }
            });

            return result != null && result != DBNull.Value;
        }
        public async Task<int> GetOsUseTmAsync(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                return 1;

            string query = @"
            SELECT TOP 1 osUseTm
            FROM [ACE].[dbo].[View_StorZakVygr1C]
            WHERE nom = @nom";

            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@nom", nom }
            });

            if (result == null || result == DBNull.Value)
                return 1;

            return Convert.ToInt32(result);
        }
        public async Task<int?> GetPlanSezonNnByNomAsync(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                return null;

            string query = @"
            SELECT TOP 1 nn
            FROM plan_sezon_zad
            WHERE nom = @nom";

            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@nom", nom }
            });

            if (result == null || result == DBNull.Value)
                return null;

            return Convert.ToInt32(result);
        }
        public async Task<bool> HasKomplJoinVshivkiAsync(int nn)
        {
            string query = @"
            exec proc_tc_psa_kompl_join_vshivki @nn";

            var rows = await _dbService.GetListAsync<int>(query, new { nn });
            return rows != null && rows.Count > 0;
        }
        #endregion
        #region Комплекты

        #endregion
        #region Набор одежды

        #endregion
        #endregion
        public async Task SaveAsync(ArticulModel model)
        {
            await _dbService.SaveEntityAsync("sp_articul", "Kod", model);
		}
		public async Task<List<ViewRzuRzv>> GetViewRzuRzv(string xNomZad = null, string xNom = null, string xKod = null)
		{   
            string query = @"SELECT * FROM View_rzu_rzv_nom_zad vrrnz ";
            if (xNomZad != null || xNom != null || xKod != null)
            {
                query += " WHERE 1=1 ";
                if (xNomZad != null)
                    query += " AND vrrnz.nom_zad = @xNomZad";
                if (xNom != null)
                    query += " AND vrrnz.nom_pach = @xNom";
                if (xKod != null)
                    query += " AND vrrnz.kod_izd = @xKod";
            }
			return await _dbService.GetListAsync<ViewRzuRzv>(query, new { xNomZad, xNom, xKod });
		}
	}
}
