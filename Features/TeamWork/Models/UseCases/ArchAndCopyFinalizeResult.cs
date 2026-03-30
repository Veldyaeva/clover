namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class ArchAndCopyFinalizeResult
    {
        public bool Success { get; set; }
        public int NewStatus { get; set; }
        public string NewStatusText { get; set; }
        public string Error { get; set; }
    }
}
