using System.Collections.Generic;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class BatchStatusUpdateResult
    {
        public bool Success { get; set; }
        public List<int> UpdatedAnnIds { get; set; } = new List<int>();
        public List<string> Errors { get; set; } = new List<string>();
    }
}
