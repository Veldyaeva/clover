using Dapper;
using Newtonsoft.Json;
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
            return _dbService.GetListFromProcedureAsync<VyazEconomWoolRow>(
                "dbo.VyazEconomPrint_GetWoolLines",
                new { IdPodr = idPodr, NomZadany = nomZadany });
        }

        public async Task<VyazEconomRaskrRow?> GetRaskrHeaderAsync(string nomZadany, CancellationToken ct = default)
        {
            var rows = await _dbService.GetListFromProcedureAsync<VyazEconomRaskrRow>(
                "dbo.VyazEconomPrint_GetRaskrHeader",
                new { NomZadany = nomZadany });

            return rows.FirstOrDefault();
        }

        public async Task<VyazEconomDiapRow?> GetDiapAsync(string nomZadany, CancellationToken ct = default)
        {
            var rows = await _dbService.GetListFromProcedureAsync<VyazEconomDiapRow>(
                "dbo.VyazEconomPrint_GetDiap",
                new { NomZadany = nomZadany });

            return rows.FirstOrDefault();
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

            return await _dbService.GetListFromProcedureAsync<VyazEconomPrihodPryzRow>(
                "dbo.VyazEconomPrint_GetPrihodPryzByNakls",
                new { NaklsJson = JsonConvert.SerializeObject(distinct) });
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

            return await _dbService.GetListFromProcedureAsync<VyazEconomQuarterPriceRow>(
                "dbo.VyazEconomPrint_GetQuarterMaxPricesByZvet",
                new
                {
                    ZvetsJson = JsonConvert.SerializeObject(distinct),
                    Year = year
                });
        }

        public async Task MarkDateEconomAsync(int nn, DateTime date, CancellationToken ct = default)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Nn", nn);
            parameters.Add("@DateEconom", date.Date);

            await _dbService.ExecuteScalarProcedureAsync<int>(
                "dbo.VyazEconomPrint_MarkDateEconom",
                parameters);
        }
    }
}
