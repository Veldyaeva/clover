using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class FinalizePreArchiveResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public ArtNormN UpdatedSourceAnn { get; set; }
    }
}
