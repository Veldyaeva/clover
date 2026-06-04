using SewingProduction.Features.Sprav.Application.Services.DataRows;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.Application.Services
{
    public sealed class VyazEconomPrintDataService : IVyazEconomPrintDataService
    {
        private readonly DbService _dbService;

        public VyazEconomPrintDataService(DbService dbService)
        {
            _dbService = dbService;
        }

        public Task<List<VyazEconomWoolRow>> GetWoolLinesAsync(int idPodr, string nomZadany, CancellationToken ct = default)
        {
            const string query = @"
SELECT
  RTRIM(nakl) AS nakl,
  kol,
  type_pryz,
  RTRIM(zvet) AS zvet
FROM dbo.v_spis_pryz WITH (NOLOCK)
WHERE id_podr = @idPodr AND nom_zadany = @nomZadany";

            return _dbService.GetListAsync<VyazEconomWoolRow>(query, new { idPodr, nomZadany });
        }

        public Task<VyazEconomRaskrRow?> GetRaskrHeaderAsync(string nomZadany, CancellationToken ct = default)
        {
            const string query = @"
SELECT TOP 1
  kod_k,
  RTRIM(articul) AS articul,
  RTRIM(mod) AS mod,
  RTRIM(articul_k) AS articul_k,
  RTRIM(mod_k) AS mod_k,
  zad_pl,
  v, p, stir, pr_printer, stra, poet, p_tamp, bus, gofp, nabiv_all,
  v_seb, p_seb
FROM dbo.raskr_zeh_vyaz WITH (NOLOCK)
WHERE zad_pl = @nomZadany";

            return _dbService.GetEntityAsync<VyazEconomRaskrRow>(query, new { nomZadany });
        }

        public Task<VyazEconomDiapRow?> GetDiapAsync(string nomZadany, CancellationToken ct = default)
        {
            const string query = @"
SELECT
  SUM(kol) AS kol,
  TRIM(STR(MIN(n_pach))) + ' - ' + TRIM(STR(MAX(n_pach))) AS diapPach,
  TRIM(MIN(razm)) + ' - ' + TRIM(MAX(razm)) AS diapSize
FROM dbo.raskr_zeh_vyaz WITH (NOLOCK)
WHERE zad_pl = @nomZadany";

            return _dbService.GetEntityAsync<VyazEconomDiapRow>(query, new { nomZadany });
        }

        public async Task<List<VyazEconomPrihodPryzRow>> GetPrihodPryzByNaklsAsync(
            IReadOnlyList<string> nakls,
            CancellationToken ct = default)
        {
            var distinct = nakls
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (distinct.Count == 0)
            {
                return new List<VyazEconomPrihodPryzRow>();
            }

            const string query = @"
SELECT
  RTRIM(nakl) AS nakl,
  RTRIM(t_articul) AS t_articul,
  RTRIM(zvet) AS zvet,
  seb_t_m
FROM dbo.prihod_pryz WITH (NOLOCK)
WHERE nakl IN @nakls";

            return await _dbService.GetListAsync<VyazEconomPrihodPryzRow>(query, new { nakls = distinct });
        }

        public async Task<List<VyazEconomQuarterPriceRow>> GetQuarterMaxPricesByZvetAsync(
            IReadOnlyList<string> zvets,
            int year,
            CancellationToken ct = default)
        {
            var distinct = zvets
                .Where(z => !string.IsNullOrWhiteSpace(z))
                .Select(z => z.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (distinct.Count == 0)
            {
                return new List<VyazEconomQuarterPriceRow>();
            }

            var q1Start = new DateTime(year, 1, 1);
            var q1End = new DateTime(year, 3, 31);
            var q2Start = new DateTime(year, 4, 1);
            var q2End = new DateTime(year, 6, 30);
            var q3Start = new DateTime(year, 7, 1);
            var q3End = new DateTime(year, 9, 30);
            var q4Start = new DateTime(year, 10, 1);
            var q4End = new DateTime(year, 12, 31);

            const string query = @"
SELECT
  RTRIM(pp.zvet) AS zvet,
  MAX(CASE WHEN pv.data_sozd >= @q1Start AND pv.data_sozd <= @q1End THEN pp.seb_t_m END) AS Q1,
  MAX(CASE WHEN pv.data_sozd >= @q2Start AND pv.data_sozd <= @q2End THEN pp.seb_t_m END) AS Q2,
  MAX(CASE WHEN pv.data_sozd >= @q3Start AND pv.data_sozd <= @q3End THEN pp.seb_t_m END) AS Q3,
  MAX(CASE WHEN pv.data_sozd >= @q4Start AND pv.data_sozd <= @q4End THEN pp.seb_t_m END) AS Q4
FROM dbo.prih_v pv WITH (NOLOCK)
INNER JOIN dbo.prihod_v prv WITH (NOLOCK) ON pv.np_id = prv.np_id
INNER JOIN dbo.prihod_pryz pp WITH (NOLOCK) ON prv.kod_pr = pp.kod_pr
WHERE pp.zvet IN @zvets
GROUP BY pp.zvet";

            return await _dbService.GetListAsync<VyazEconomQuarterPriceRow>(query, new
            {
                zvets = distinct,
                q1Start,
                q1End,
                q2Start,
                q2End,
                q3Start,
                q3End,
                q4Start,
                q4End
            });
        }

        public Task MarkDateEconomAsync(int nn, DateTime date, CancellationToken ct = default)
        {
            return _dbService.UpdateFieldAsync(
                "dbo.seb_vyaz_econom",
                "date_econom",
                date.Date,
                "nn",
                nn);
        }
    }
}
