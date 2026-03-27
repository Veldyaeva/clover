using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public interface IKnitterWorkSpaceService
    {
        Task<StartShiftResult> StartShiftAsync(StartShiftCommand command);
        Task<CloseShiftResult> CloseShiftAsync(CloseShiftCommand command);
    }

    public sealed class StartShiftCommand
    {
        public int Tab { get; init; }
        public int? KmaId { get; init; }
        public string KmaNum { get; init; } = "";
        public List<int> PzvIds { get; init; } = new();
    }

    public sealed class StartShiftResult
    {
        public bool Success { get; init; }
        public string ErrorMessage { get; init; }
        public int? ShiftId { get; init; }
        public DateTime? ShiftStartTime { get; init; }
    }

    public sealed class CloseShiftCommand
    {
        public int ShiftId { get; init; }
        public int TabEnd { get; init; }
        public decimal MinHours { get; init; } = 12m;
        public List<KnitterPZVModel> CurrentRows { get; init; } = new();
    }

    public sealed class CloseShiftResult
    {
        public bool Success { get; init; }
        public string ErrorMessage { get; init; }
        public bool HasUnfinishedOperations { get; init; }
        public List<int> UnfinishedPzvIds { get; init; } = new();
    }
}
