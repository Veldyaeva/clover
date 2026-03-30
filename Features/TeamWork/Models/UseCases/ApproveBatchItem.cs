using System;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class ApproveBatchItem
    {
        public int AnnId { get; set; }
        public DateTime ApprovedAt { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
    }
}
