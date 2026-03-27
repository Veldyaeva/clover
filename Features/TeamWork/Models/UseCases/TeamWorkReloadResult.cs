using System.Collections.Generic;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class TeamWorkReloadResult
    {
        public List<ArtNormN> Data { get; set; }
        public int? FocusAnnId { get; set; }
        public int? FocusRowHandle { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
