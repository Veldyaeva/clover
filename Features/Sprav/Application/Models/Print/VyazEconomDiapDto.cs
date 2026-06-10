namespace SewingProduction.Features.Sprav.Application.Models.Print
{
    public sealed class VyazEconomDiapDto
    {
        public decimal Kol { get; init; }
        public string DiapPach { get; init; } = string.Empty;
        public string DiapSize { get; init; } = string.Empty;
    }
}
