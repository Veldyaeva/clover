using SewingProduction.Features.Sprav.Application.Contexts;
using SewingProduction.Features.Sprav.Application.Export;
using SewingProduction.Features.Sprav.Application.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.Application.UseCases
{
    /// <summary>
    /// Полный цикл печати: загрузка данных → Excel → обновление date_econom.
    /// </summary>
    public sealed class PrintVyazEconomCalculationUseCase
    {
        private readonly LoadVyazEconomPrintDataUseCase _loadData;
        private readonly IVyazEconomExcelExporter _excelExporter;
        private readonly MarkVyazEconomPrintedUseCase _markPrinted;

        public PrintVyazEconomCalculationUseCase(
            LoadVyazEconomPrintDataUseCase loadData,
            IVyazEconomExcelExporter excelExporter,
            MarkVyazEconomPrintedUseCase markPrinted)
        {
            _loadData = loadData;
            _excelExporter = excelExporter;
            _markPrinted = markPrinted;
        }

        public async Task<OperationResult> ExecuteAsync(
            VyazEconomPrintContext context,
            CancellationToken ct = default)
        {
            var loadResult = await _loadData.ExecuteAsync(context, ct);
            if (!loadResult.Success || loadResult.Data == null)
            {
                return OperationResult.Fail(loadResult.ErrorMessage ?? "Не удалось загрузить данные для печати.");
            }

            try
            {
                await _excelExporter.ExportAsync(loadResult.Data, ct);
            }
            catch (VyazEconomExportCancelledException)
            {
                return OperationResult.Cancelled();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Ошибка формирования Excel: {ex.Message}");
            }

            return await _markPrinted.ExecuteAsync(context, ct);
        }
    }
}
