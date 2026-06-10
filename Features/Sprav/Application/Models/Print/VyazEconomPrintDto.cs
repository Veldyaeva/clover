using System.Collections.Generic;

namespace SewingProduction.Features.Sprav.Application.Models.Print
{
    public sealed class VyazEconomPrintDto
    {
        public int Nn { get; init; }
        public int IdPodr { get; init; }
        public string NomZadany { get; init; }
        public decimal? SebAll { get; init; }

        public int QuarterYear { get; init; }
        public string QuarterYearShortLabel { get; init; } = string.Empty;

        public VyazEconomRaskrHeaderDto Header { get; init; } = new();
        public VyazEconomDiapDto Diap { get; init; } = new();
        public IReadOnlyList<VyazEconomWoolLineDto> WoolLines { get; init; } = new List<VyazEconomWoolLineDto>();

        public decimal SumWoolConsumption { get; init; }
        public decimal? YarnTotalRub { get; init; }
        public decimal? GrandTotalRub { get; init; }
    }
}
