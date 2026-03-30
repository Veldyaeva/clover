using System.Collections.Generic;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class ApproveBatchResult
    {
        public bool Success { get; set; }
        public List<int> UpdatedAnnIds { get; set; } = new List<int>();
        public List<ApproveBatchItem> Items { get; set; } = new List<ApproveBatchItem>();
        public List<string> Errors { get; set; } = new List<string>();
    }
}
