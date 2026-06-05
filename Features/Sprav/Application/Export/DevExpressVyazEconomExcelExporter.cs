using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using SewingProduction.Features.Sprav.Application.Models.Print;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Sprav.Application.Export
{
    public sealed class DevExpressVyazEconomExcelExporter : IVyazEconomExcelExporter
    {
        private const string MoneyFormat = "#,##0.00";
        private const string ConsumptionFormat = "#,##0.00000";
        private const double ColumnWidthChars = 18d;
        private static readonly Color GrandTotalFill = Color.FromArgb(217, 217, 217);

        public Task ExportAsync(VyazEconomPrintDto data, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            var filePath = GetFilePathFromDialog(data);
            if (string.IsNullOrEmpty(filePath))
            {
                throw new VyazEconomExportCancelledException();
            }

            using var spreadsheet = new SpreadsheetControl { Visible = false };
            spreadsheet.CreateNewDocument();
            var workbook = spreadsheet.Document;
            var worksheet = workbook.Worksheets[0];
            worksheet.Name = "Калькуляция";
            worksheet.ActiveView.ShowGridlines = true;

            var layout = FillReport(worksheet, data);
            ApplyFormatting(worksheet, layout.YarnTotalRow, layout.GrandTotalRow, layout.LastWoolRow);

            workbook.SaveDocument(filePath, DocumentFormat.Xlsx);
            OpenFile(filePath);

            return Task.CompletedTask;
        }

        private readonly struct ReportLayout
        {
            public int YarnTotalRow { get; init; }
            public int GrandTotalRow { get; init; }
            public int LastWoolRow { get; init; }
        }

        private static ReportLayout FillReport(Worksheet worksheet, VyazEconomPrintDto data)
        {
            var header = data.Header;
            var diap = data.Diap;
            var yearLabel = data.QuarterYearShortLabel;

            SetValue(worksheet, 0, 1, "Калькуляция себестоимости изделия по фактическим расходам");

            SetValue(worksheet, 2, 0, "Артикул");
            SetValue(worksheet, 2, 1, header.DisplayArticul);
            SetValue(worksheet, 3, 0, "Модель");
            SetValue(worksheet, 3, 1, header.DisplayMod);
            SetValue(worksheet, 4, 0, "Задание");
            SetValue(worksheet, 4, 1, header.ZadPl);
            SetValue(worksheet, 5, 0, "Пачки");
            SetValue(worksheet, 5, 1, diap.DiapPach);
            SetValue(worksheet, 6, 0, "Количество");
            SetValue(worksheet, 6, 1, diap.Kol);
            SetValue(worksheet, 7, 0, "Размерный ряд");
            SetValue(worksheet, 7, 1, diap.DiapSize);

            SetValue(worksheet, 9, 1, "Затраты");
            SetValue(worksheet, 10, 0, "Отделка");

            SetValue(worksheet, 11, 0, "Вид отделки");
            SetValue(worksheet, 11, 1, "Наличие отделки");
            SetValue(worksheet, 11, 2, "Себ-ть на 1 изд.");

            WriteFinishingRow(worksheet, 12, "Вышивка", header.VysivkaMark, header.VysivkaSeb);
            WriteFinishingRow(worksheet, 13, "Принт", header.PrintMark, header.PrintSeb);
            WriteFinishingRow(worksheet, 14, "Стирка", header.StirkaMark, header.StirkaSeb);
            WriteFinishingRow(worksheet, 15, "Принтер", header.PrinterMark, header.PrinterSeb);
            WriteFinishingRow(worksheet, 16, "Стразы", header.StrazyMark, null);
            WriteFinishingRow(worksheet, 17, "Паетки", header.PaetkiMark, null);
            WriteFinishingRow(worksheet, 18, "Тамп.печать", header.TampMark, null);
            WriteFinishingRow(worksheet, 19, "Бусины", header.BusinyMark, null);
            WriteFinishingRow(worksheet, 20, "Гофропринтер", header.GofprinterMark, null);
            WriteFinishingRow(worksheet, 21, "Набивка полностью", header.NabivkaMark, null);

            SetValue(worksheet, 22, 1, "Итого по отделке,руб.:");
            SetMoney(worksheet, 22, 2, header.FinishingTotal);

            SetValue(worksheet, 24, 0, "Расход пряжи");

            SetValue(worksheet, 25, 0, "Пряжа");
            SetValue(worksheet, 25, 1, "№ карты");
            SetValue(worksheet, 25, 2, "№ цвета");
            SetValue(worksheet, 25, 3, "Расход на 1 изд.");
            SetValue(worksheet, 25, 4, "Себ-ть в руб.");
            SetValue(worksheet, 25, 5, "Доп.мат.");
            SetValue(worksheet, 25, 6, $"макс цена 1 кв {yearLabel} г");
            SetValue(worksheet, 25, 7, $"макс цена 2 кв {yearLabel} г");
            SetValue(worksheet, 25, 8, $"макс цена 3 кв {yearLabel} г");
            SetValue(worksheet, 25, 9, $"макс цена 4 кв {yearLabel} г");

            var woolStartRow = 26;
            var row = woolStartRow;
            foreach (var wool in data.WoolLines)
            {
                SetValue(worksheet, row, 0, wool.YarnArticul);
                SetValue(worksheet, row, 1, wool.Nakl);
                SetValue(worksheet, row, 2, wool.Zvet);
                SetConsumption(worksheet, row, 3, wool.ConsumptionPerUnit);
                SetMoney(worksheet, row, 4, wool.CostPerUnit);
                SetValue(worksheet, row, 5, wool.AdditionalMaterialMark);
                SetMoney(worksheet, row, 6, wool.Quarter1MaxPrice);
                SetMoney(worksheet, row, 7, wool.Quarter2MaxPrice);
                SetMoney(worksheet, row, 8, wool.Quarter3MaxPrice);
                SetMoney(worksheet, row, 9, wool.Quarter4MaxPrice);
                row++;
            }

            var yarnTotalRow = row;
            SetValue(worksheet, yarnTotalRow, 1, "Итого по пряже,руб.:");
            SetMoney(worksheet, yarnTotalRow, 2, data.YarnTotalRub);

            var grandTotalRow = yarnTotalRow + 2;
            SetValue(worksheet, grandTotalRow, 1, "Итого себест.,руб.:");
            SetMoney(worksheet, grandTotalRow, 2, data.GrandTotalRub);

            var lastWoolRow = data.WoolLines.Count > 0 ? row - 1 : 25;
            return new ReportLayout
            {
                YarnTotalRow = yarnTotalRow,
                GrandTotalRow = grandTotalRow,
                LastWoolRow = lastWoolRow
            };
        }

        private static void ApplyFormatting(Worksheet worksheet, int yarnTotalRow, int grandTotalRow, int lastWoolRow)
        {

            for (var col = 0; col <= 9; col++)
            {
                worksheet.Columns[col].WidthInCharacters = ColumnWidthChars;
            }

            SetFont(worksheet.Range.FromLTRB(0, 0, 1, 0), bold: true, size: 11);
            SetFont(worksheet.Range.FromLTRB(0, 2, 0, 7), bold: true, size: 11);
            SetFont(worksheet.Range.FromLTRB(1, 2, 1, 7), bold: false, size: 11);
            SetFont(worksheet.Range.FromLTRB(1, 9, 1, 9), bold: true, size: 12);
            SetFont(worksheet.Range.FromLTRB(0, 11, 2, 11), bold: true, size: 12);
            SetFont(worksheet.Range.FromLTRB(0, 24, 0, 24), bold: true, size: 11);
            SetFont(worksheet.Range.FromLTRB(0, 25, 5, 25), bold: true, size: 11);
            SetFont(worksheet.Range.FromLTRB(6, 25, 9, 25), bold: true, size: 11);

            ApplyBorder(worksheet.Range.FromLTRB(0, 2, 1, 7));
            ApplyBorder(worksheet.Range.FromLTRB(0, 11, 2, 21));

            if (lastWoolRow >= 26)
            {
                ApplyBorder(worksheet.Range.FromLTRB(0, 25, 9, lastWoolRow));
            }

            var grandRange = worksheet.Range.FromLTRB(0, grandTotalRow, 9, grandTotalRow);
            SetFont(grandRange, bold: true, size: 11);
            grandRange.Fill.BackgroundColor = GrandTotalFill;
            SetAlignment(grandRange, horizontalCenter: false);

            SetAlignment(worksheet.Range.FromLTRB(0, 0, 9, grandTotalRow), horizontalCenter: true);
            SetAlignment(worksheet.Range.FromLTRB(0, 2, 0, 7), horizontalCenter: false);
            SetAlignment(worksheet.Range.FromLTRB(0, 11, 0, 21), horizontalCenter: false);
            SetAlignment(worksheet.Range.FromLTRB(0, 24, 0, 24), horizontalCenter: false);
        }

        private static void WriteFinishingRow(Worksheet ws, int row, string label, string mark, decimal? cost)
        {
            SetValue(ws, row, 0, label);
            SetValue(ws, row, 1, mark);
            if (cost.HasValue)
            {
                SetMoney(ws, row, 2, cost);
            }
        }

        private static void SetValue(Worksheet ws, int row, int col, object? value)
        {
            var cell = ws.Cells[row, col];
            switch (value)
            {
                case null:
                    cell.Value = CellValue.Empty;
                    break;
                case string s:
                    cell.Value = s;
                    break;
                case int i:
                    cell.Value = i;
                    break;
                case long l:
                    cell.Value = l;
                    break;
                case decimal d:
                    cell.Value = (double)d;
                    break;
                case double dbl:
                    cell.Value = dbl;
                    break;
                default:
                    cell.Value = value.ToString() ?? string.Empty;
                    break;
            }
        }

        private static void SetMoney(Worksheet ws, int row, int col, decimal? value)
        {
            var cell = ws.Cells[row, col];
            if (value.HasValue)
            {
                cell.Value = value.Value;
                cell.NumberFormat = MoneyFormat;
            }
            else
            {
                cell.Value = string.Empty;
            }
        }

        private static void SetConsumption(Worksheet ws, int row, int col, decimal value)
        {
            var cell = ws.Cells[row, col];
            cell.Value = value;
            cell.NumberFormat = ConsumptionFormat;
        }

        private static void SetFont(CellRange range, bool bold, int size)
        {
            var font = range.Font;
            font.Bold = bold;
            font.Size = size;
        }

        private static void SetAlignment(CellRange range, bool horizontalCenter)
        {
            range.Alignment.Horizontal = horizontalCenter
                ? SpreadsheetHorizontalAlignment.Center
                : SpreadsheetHorizontalAlignment.Left;
            range.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
        }

        private static void ApplyBorder(CellRange range)
        {
            range.Borders.SetAllBorders(Color.Black, BorderLineStyle.Thin);
        }

        private static string? GetFilePathFromDialog(VyazEconomPrintDto data)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Сохранить калькуляцию",
                FileName = $"Калькуляция_{data.NomZadany}.xlsx"
            };

            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }

        private static void OpenFile(string filePath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
    }
}
