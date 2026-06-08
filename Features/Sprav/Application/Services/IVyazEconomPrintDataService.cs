using SewingProduction.Features.Sprav.Application.Services.DataRows;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.Application.Services
{
    public interface IVyazEconomPrintDataService
    {
        Task<VyazEconomPrintRows> GetPrintRowsAsync(
            int idPodr,
            string nomZadany,
            int year,
            CancellationToken ct = default);
        Task MarkDateEconomAsync(int nn, DateTime date, CancellationToken ct = default);
    }
}
