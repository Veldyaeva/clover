using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class DuplicateDraftResult
    {
        public bool Success { get; set; }
        public int NewAnnId { get; set; }
        public ArtNormN DraftAnn { get; set; }
        public string Error { get; set; }
    }
}
