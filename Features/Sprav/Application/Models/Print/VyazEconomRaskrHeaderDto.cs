namespace SewingProduction.Features.Sprav.Application.Models.Print
{
    public sealed class VyazEconomRaskrHeaderDto
    {
        public string DisplayArticul { get; init; } = string.Empty;
        public string DisplayMod { get; init; } = string.Empty;
        public int ZadPl { get; init; }

        public string VysivkaMark { get; init; } = string.Empty;
        public string PrintMark { get; init; } = string.Empty;
        public string StirkaMark { get; init; } = string.Empty;
        public string PrinterMark { get; init; } = string.Empty;
        public string StrazyMark { get; init; } = string.Empty;
        public string PaetkiMark { get; init; } = string.Empty;
        public string TampMark { get; init; } = string.Empty;
        public string BusinyMark { get; init; } = string.Empty;
        public string GofprinterMark { get; init; } = string.Empty;
        public string NabivkaMark { get; init; } = string.Empty;

        public decimal? VysivkaSeb { get; init; }
        public decimal? PrintSeb { get; init; }
        public decimal? StirkaSeb { get; init; }
        public decimal? PrinterSeb { get; init; }

        public decimal FinishingTotal { get; init; }
    }
}
