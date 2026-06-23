namespace SewingProduction.Features.Sprav.Application.Services.DataRows
{
    public sealed class VyazEconomRaskrRow
    {
        public string? kod_k { get; set; }
        public string articul { get; set; } = string.Empty;
        public string mod { get; set; } = string.Empty;
        public string articul_k { get; set; } = string.Empty;
        public string mod_k { get; set; } = string.Empty;
        public int zad_pl { get; set; }
        public int? v { get; set; }
        public int? p { get; set; }
        public int? stir { get; set; }
        public decimal? pr_printer { get; set; }
        public int? stra { get; set; }
        public int? poet { get; set; }
        public int? p_tamp { get; set; }
        public int? bus { get; set; }
        public int? gofp { get; set; }
        public int? nabiv_all { get; set; }
        public decimal? v_seb { get; set; }
        public decimal? p_seb { get; set; }
    }
}
