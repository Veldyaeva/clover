using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public class KnitterWorkSpaceService : IKnitterWorkSpaceService
    {
        private readonly IKnitterShiftGateway _shiftGateway;

        public KnitterWorkSpaceService(IKnitterShiftGateway shiftGateway)
        {
            _shiftGateway = shiftGateway ?? throw new ArgumentNullException(nameof(shiftGateway));
        }

        public async Task<StartShiftResult> StartShiftAsync(StartShiftCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.Tab <= 0)
                return new StartShiftResult { Success = false, ErrorMessage = "Не выбран сотрудник." };

            if (command.PzvIds == null || command.PzvIds.Count == 0)
                return new StartShiftResult { Success = false, ErrorMessage = "Нет строк для назначения." };

            if (command.KmaId.HasValue)
            {
                var openShift = await _shiftGateway.GetOpenShiftByZoneAsync(command.KmaId.Value);
                if (openShift.shiftId.HasValue)
                {
                    return new StartShiftResult
                    {
                        Success = false,
                        ErrorMessage = $"В зоне {command.KmaNum} уже открыта смена."
                    };
                }
            }

            int shiftId;
            await using (var tx = await _shiftGateway.BeginShiftTransactionAsync())
            {
                await tx.UpdatePzvTabAsync(command.PzvIds, command.Tab);
                shiftId = await tx.StartWorkingShiftAsync(command.Tab, command.KmaId, command.KmaNum, kmsId: 0);
                await tx.UpdatePzvKwsIdAsync(command.PzvIds, shiftId);
                await tx.CommitAsync();
            }

            if (shiftId <= 0)
            {
                return new StartShiftResult
                {
                    Success = false,
                    ErrorMessage = "Не удалось открыть смену."
                };
            }

            return new StartShiftResult
            {
                Success = true,
                ShiftId = shiftId,
                ShiftStartTime = DateTime.Now
            };
        }

        public async Task<CloseShiftResult> CloseShiftAsync(CloseShiftCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.ShiftId <= 0)
            {
                return new CloseShiftResult
                {
                    Success = false,
                    ErrorMessage = "Не удалось определить открытую смену."
                };
            }

            var unfinished = (command.CurrentRows ?? new List<KnitterPZVModel>())
                .Where(r => r.pzvDateStart != null && r.pzvDateEnd == null)
                .Select(r => r.pzvID)
                .Distinct()
                .ToList();

            if (unfinished.Count > 0)
            {
                return new CloseShiftResult
                {
                    Success = false,
                    ErrorMessage = "Есть начатые и не завершённые операции. Смену закрывать нельзя.",
                    HasUnfinishedOperations = true,
                    UnfinishedPzvIds = unfinished
                };
            }

            var notStarted = (command.CurrentRows ?? new List<KnitterPZVModel>())
                .Where(r => r.pzvID > 0 && r.pzvDateStart == null && r.pzvDateEnd == null)
                .Select(r => r.pzvID)
                .Distinct()
                .ToList();

            await using (var tx = await _shiftGateway.BeginShiftTransactionAsync())
            {
                foreach (var pzvId in notStarted)
                {
                    try
                    {
                        await tx.SplitPzvByModeAsync(pzvId, mode: 2, qtyFact: 0);
                    }
                    catch
                    {
                        // Сохраняем старое поведение: сбой split одной строки не должен блокировать закрытие смены.
                    }
                }

                await tx.AdjustNotStartedBeforeShiftEndAsync(command.ShiftId, command.MinHours);
                await tx.EndWorkingShiftAsync(command.ShiftId, command.TabEnd);
                await tx.CommitAsync();
            }

            return new CloseShiftResult
            {
                Success = true,
                ErrorMessage = string.Empty,
                HasUnfinishedOperations = false,
                UnfinishedPzvIds = new List<int>()
            };
        }
    }
}
