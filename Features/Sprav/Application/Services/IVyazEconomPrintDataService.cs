using SewingProduction.Features.Sprav.Application.Services.DataRows;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.Application.Services
{
    public interface IVyazEconomPrintDataService
    {
        Task<List<VyazEconomWoolRow>> GetWoolLinesAsync(int idPodr, string nomZadany, CancellationToken ct = default);
        Task<VyazEconomRaskrRow?> GetRaskrHeaderAsync(string nomZadany, CancellationToken ct = default);
        Task<VyazEconomDiapRow?> GetDiapAsync(string nomZadany, CancellationToken ct = default);
        Task<List<VyazEconomPrihodPryzRow>> GetPrihodPryzByNaklsAsync(IReadOnlyList<string> nakls, CancellationToken ct = default);
        Task<List<VyazEconomQuarterPriceRow>> GetQuarterMaxPricesByZvetAsync(
            IReadOnlyList<string> zvets,
            int year,
            CancellationToken ct = default);
        Task MarkDateEconomAsync(int nn, DateTime date, CancellationToken ct = default);
    }
}
