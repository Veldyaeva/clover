namespace SewingProduction.Features.Yarn.Models
{
    public sealed class YarnRecalcResult
    {
        public int Error { get; set; }
        public string MessageError { get; set; }
        public bool IsOk => Error == 0;
        public decimal? SebUpr { get; set; }
    }
}
