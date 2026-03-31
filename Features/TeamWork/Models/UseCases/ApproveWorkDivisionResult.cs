using System;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class ApproveWorkDivisionResult
    {
        public bool Success { get; set; }
        public DateTime ApprovedAt { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
