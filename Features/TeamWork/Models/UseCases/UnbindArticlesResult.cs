namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class UnbindArticlesResult
    {
        public bool Success { get; set; }
        public int AffectedRows { get; set; }
        public string Error { get; set; }
    }
}
