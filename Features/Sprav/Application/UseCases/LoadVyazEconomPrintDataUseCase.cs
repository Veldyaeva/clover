using SewingProduction.Features.Sprav.Application.Contexts;
using SewingProduction.Features.Sprav.Application.Models.Print;
using SewingProduction.Features.Sprav.Application.Results;
using SewingProduction.Features.Sprav.Application.Services;
using SewingProduction.Features.Sprav.Application.Services.DataRows;
using SewingProduction.Features.Sprav.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.Application.UseCases
{
    public sealed class LoadVyazEconomPrintDataUseCase
    {
        private readonly VyazEconomPrintValidator _validator;
        private readonly IVyazEconomPrintDataService _dataService;

        public LoadVyazEconomPrintDataUseCase(
            VyazEconomPrintValidator validator,
            IVyazEconomPrintDataService dataService)
        {
            _validator = validator;
            _dataService = dataService;
        }

        public async Task<VyazEconomPrintDataResult> ExecuteAsync(
            VyazEconomPrintContext context,
            CancellationToken ct = default)
        {
            var validation = _validator.Validate(context);
            if (!validation.Success)
            {
                return VyazEconomPrintDataResult.Fail(validation.ErrorMessage ?? "Ошибка валидации.");
            }

            var quarterYear = DateTime.Today.Month == 1
                ? DateTime.Today.Year - 1
                : DateTime.Today.Year;

            var printRows = await _dataService.GetPrintRowsAsync(
                context.IdPodr,
                context.NomZadany,
                quarterYear,
                ct);

            var woolRows = printRows.WoolRows;
            var raskrRow = printRows.RaskrRow;
            var diapRow = printRows.DiapRow;

            if (raskrRow == null)
            {
                return VyazEconomPrintDataResult.Fail("Не найдены данные раскроя для задания.");
            }

            if (diapRow?.kol is not > 0)
            {
                return VyazEconomPrintDataResult.Fail("Нет данных раскроя для задания (количество).");
            }

            var diapKol = diapRow.kol!.Value;
            var prihodByNakl = printRows.PrihodRows
                .GroupBy(p => p.nakl.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

            var quarterByZvet = printRows.QuarterRows
                .GroupBy(q => q.zvet.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

            var woolLines = new List<VyazEconomWoolLineDto>();
            decimal sumWoolConsumption = 0;

            foreach (var wool in woolRows)
            {
                var naklKey = wool.nakl?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(naklKey) || !prihodByNakl.TryGetValue(naklKey, out var prihod))
                {
                    continue;
                }

                var consumption = Math.Round(wool.kol / diapKol, 8, MidpointRounding.AwayFromZero);
                sumWoolConsumption += consumption;

                var zvetKey = wool.zvet?.Trim() ?? string.Empty;
                quarterByZvet.TryGetValue(zvetKey, out var quarter);

                woolLines.Add(new VyazEconomWoolLineDto
                {
                    YarnArticul = prihod.t_articul,
                    Nakl = naklKey,
                    Zvet = prihod.zvet,
                    ConsumptionPerUnit = consumption,
                    CostPerUnit = prihod.seb_t_m,
                    AdditionalMaterialMark = wool.type_pryz == 1 ? "V" : string.Empty,
                    Quarter1MaxPrice = quarter?.Q1,
                    Quarter2MaxPrice = quarter?.Q2,
                    Quarter3MaxPrice = quarter?.Q3,
                    Quarter4MaxPrice = quarter?.Q4
                });
            }

            var header = MapHeader(raskrRow);
            var finishingTotal = header.FinishingTotal;
            var sebAll = context.SebAll ?? 0m;
            var grandTotal = sebAll + finishingTotal;

            var dto = new VyazEconomPrintDto
            {
                Nn = context.Nn,
                IdPodr = context.IdPodr,
                NomZadany = context.NomZadany,
                SebAll = context.SebAll,
                QuarterYear = quarterYear,
                QuarterYearShortLabel = (quarterYear % 100).ToString("00"),
                Header = header,
                Diap = new VyazEconomDiapDto
                {
                    Kol = diapKol,
                    DiapPach = diapRow.diapPach ?? string.Empty,
                    DiapSize = diapRow.diapSize ?? string.Empty
                },
                WoolLines = woolLines,
                SumWoolConsumption = sumWoolConsumption,
                YarnTotalRub = context.SebAll,
                GrandTotalRub = grandTotal
            };

            return VyazEconomPrintDataResult.Ok(dto);
        }

        private static VyazEconomRaskrHeaderDto MapHeader(VyazEconomRaskrRow row)
        {
            var isKit = !string.IsNullOrWhiteSpace(row.kod_k);
            var vSeb = row.v_seb ?? 0m;
            var pSeb = row.p_seb ?? 0m;
            var printerVal = row.pr_printer ?? 0m;
            var finishingTotal = vSeb + pSeb + printerVal;

            return new VyazEconomRaskrHeaderDto
            {
                DisplayArticul = isKit ? "Комплект " + row.articul_k : row.articul,
                DisplayMod = isKit ? "Комплект " + row.mod_k : row.mod,
                ZadPl = row.zad_pl,
                VysivkaMark = FlagMark(row.v == 1),
                PrintMark = FlagMark(row.p == 1),
                StirkaMark = FlagMark(row.stir == 1),
                PrinterMark = FlagMark(printerVal > 0),
                StrazyMark = FlagMark(row.stra == 1),
                PaetkiMark = FlagMark(row.poet == 1),
                TampMark = FlagMark(row.p_tamp == 1),
                BusinyMark = FlagMark(row.bus == 1),
                GofprinterMark = FlagMark(row.gofp == 1),
                NabivkaMark = FlagMark(row.nabiv_all == 1),
                VysivkaSeb = row.v_seb,
                PrintSeb = row.p_seb,
                StirkaSeb = null,
                PrinterSeb = row.pr_printer,
                FinishingTotal = finishingTotal
            };
        }

        private static string FlagMark(bool value) => value ? "V" : string.Empty;
    }
}
