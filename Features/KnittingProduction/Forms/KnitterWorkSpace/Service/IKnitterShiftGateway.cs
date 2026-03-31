using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using static SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service.KnitterRepository;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public interface IKnitterShiftGateway
    {
        Task<(int? shiftId, int? tabStart, DateTime? dateStart)> GetOpenShiftByZoneAsync(int kmaId);
        Task<IKnitterShiftTransaction> BeginShiftTransactionAsync();
    }

    public interface IKnitterShiftTransaction : IAsyncDisposable
    {
        Task UpdatePzvTabAsync(IEnumerable<int> pzvIds, int tab);
        Task<int> StartWorkingShiftAsync(int tabStart, int? kmaId, string kmaNum, int? kmsId = 0);
        Task UpdatePzvKwsIdAsync(IEnumerable<int> pzvIds, int kwsId);
        Task<IReadOnlyList<PzvSplitResult>> SplitPzvByModeAsync(int pzvId, int mode, int qtyFact);
        Task<IEnumerable<MachineHoursStat>> AdjustNotStartedBeforeShiftEndAsync(int shiftId, decimal minHours);
        Task EndWorkingShiftAsync(int shiftId, int tabEnd);
        Task CommitAsync();
    }
}
