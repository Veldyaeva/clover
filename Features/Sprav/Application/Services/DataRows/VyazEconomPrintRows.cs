using System.Collections.Generic;

namespace SewingProduction.Features.Sprav.Application.Services.DataRows
{
    public sealed class VyazEconomPrintRows
    {
        public List<VyazEconomWoolRow> WoolRows { get; init; } = new();
        public VyazEconomRaskrRow? RaskrRow { get; init; }
        public VyazEconomDiapRow? DiapRow { get; init; }
        public List<VyazEconomPrihodPryzRow> PrihodRows { get; init; } = new();
        public List<VyazEconomQuarterPriceRow> QuarterRows { get; init; } = new();
    }
}
