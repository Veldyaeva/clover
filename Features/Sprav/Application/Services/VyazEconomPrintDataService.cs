using Dapper;
using SewingProduction.Features.Sprav.Application.Services.DataRows;
using SewingProduction.Services;
using System;
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

        public Task<VyazEconomPrintRows> GetPrintRowsAsync(
            int idPodr,
            string nomZadany,
            int year,
            CancellationToken ct = default)
        {
            return _dbService.QueryMultipleFromProcedureAsync(
                "dbo.VyazEconomPrint_LoadData",
                new { IdPodr = idPodr, NomZadany = nomZadany, Year = year },
                async result =>
                {
                    var woolRows = (await result.ReadAsync<VyazEconomWoolRow>()).AsList();
                    var raskrRow = (await result.ReadAsync<VyazEconomRaskrRow>()).FirstOrDefault();
                    var diapRow = (await result.ReadAsync<VyazEconomDiapRow>()).FirstOrDefault();
                    var prihodRows = (await result.ReadAsync<VyazEconomPrihodPryzRow>()).AsList();
                    var quarterRows = (await result.ReadAsync<VyazEconomQuarterPriceRow>()).AsList();

                    return new VyazEconomPrintRows
                    {
                        WoolRows = woolRows,
                        RaskrRow = raskrRow,
                        DiapRow = diapRow,
                        PrihodRows = prihodRows,
                        QuarterRows = quarterRows
                    };
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
