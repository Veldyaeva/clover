using SewingProduction.Features.Sprav.Application.Models.Print;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.Application.Export
{
    public interface IVyazEconomExcelExporter
    {
        Task ExportAsync(VyazEconomPrintDto data, CancellationToken ct = default);
    }
}
