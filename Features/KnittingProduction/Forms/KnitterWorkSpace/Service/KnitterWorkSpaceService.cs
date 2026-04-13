using System;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public class KnitterWorkSpaceService : IKnitterWorkSpaceService
    {
        private readonly IKnitterShiftGateway _shiftGateway;
        private readonly ILogger _logger;
        private readonly CloseKnitterShiftUseCase _closeShiftUseCase;

        public KnitterWorkSpaceService(IKnitterShiftGateway shiftGateway, ILogger? logger = null)
        {
            _shiftGateway = shiftGateway ?? throw new ArgumentNullException(nameof(shiftGateway));
            _logger = logger ?? new FileLogger();
            _closeShiftUseCase = new CloseKnitterShiftUseCase(_shiftGateway, _logger);
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
            return await _closeShiftUseCase.ExecuteAsync(command).ConfigureAwait(false);
        }
    }
}
