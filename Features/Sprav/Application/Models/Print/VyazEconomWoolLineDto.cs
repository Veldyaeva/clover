namespace SewingProduction.Features.Sprav.Application.Models.Print
{
    public sealed class VyazEconomWoolLineDto
    {
        public string YarnArticul { get; init; } = string.Empty;
        public string Nakl { get; init; } = string.Empty;
        public string Zvet { get; init; } = string.Empty;
        public decimal ConsumptionPerUnit { get; init; }
        public decimal? CostPerUnit { get; init; }
        public string AdditionalMaterialMark { get; init; } = string.Empty;
        public decimal? Quarter1MaxPrice { get; init; }
        public decimal? Quarter2MaxPrice { get; init; }
        public decimal? Quarter3MaxPrice { get; init; }
        public decimal? Quarter4MaxPrice { get; init; }
    }
}
