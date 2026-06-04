using SewingProduction.Features.Sprav.Application.Models.Print;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.Application.Export
{
    /// <summary>
    /// Заглушка до выбора движка Excel (Interop / шаблон xlsx).
    /// </summary>
    internal sealed class NotImplementedVyazEconomExcelExporter : IVyazEconomExcelExporter
    {
        public Task ExportAsync(VyazEconomPrintDto data, CancellationToken ct = default)
        {
            throw new NotImplementedException(
                "Экспорт калькуляции в Excel ещё не реализован. Данные для отчёта загружаются через LoadVyazEconomPrintDataUseCase.");
        }
    }
}
