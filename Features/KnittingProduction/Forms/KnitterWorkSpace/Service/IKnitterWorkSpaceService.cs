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

    public enum CloseShiftStatus
    {
        Success,
        AlreadyClosed,
        HasUnfinishedOperations,
        NotFound,
        ConcurrentCloseInProgress,
        Failed
    }

    public enum StartShiftStatus
    {
        Success,
        AlreadyOpen,
        NoEmployee,
        NoRows,
        ConcurrentOpenInProgress,
        Failed
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
        public StartShiftStatus Status { get; init; } = StartShiftStatus.Failed;
        public string ErrorMessage { get; init; }
        public bool AlreadyOpen => Status == StartShiftStatus.AlreadyOpen;
        public bool ConcurrentOpenInProgress => Status == StartShiftStatus.ConcurrentOpenInProgress;
        public int? ShiftId { get; init; }
        public DateTime? ShiftStartTime { get; init; }
    }

    public sealed class CloseShiftCommand
    {
        public int ShiftId { get; init; }
        public int TabEnd { get; init; }
        public decimal MinHours { get; init; } = 12m;
        public string UserName { get; init; } = "";
    }

    public sealed class CloseShiftResult
    {
        public bool Success { get; init; }
        public CloseShiftStatus Status { get; init; } = CloseShiftStatus.Failed;
        public string ErrorMessage { get; init; }
        public bool HasUnfinishedOperations { get; init; }
        public bool AlreadyClosed => Status == CloseShiftStatus.AlreadyClosed;
        public bool ConcurrentCloseInProgress => Status == CloseShiftStatus.ConcurrentCloseInProgress;
        public List<int> UnfinishedPzvIds { get; init; } = new();
    }
}
