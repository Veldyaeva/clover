using System;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Use-case открытия смены вязальщика.
    /// Сериализует открытие по зоне, чтобы два рабочих места не создали две открытые смены.
    /// </summary>
    public sealed class StartKnitterShiftUseCase
    {
        private readonly IKnitterShiftGateway _shiftGateway;
        private readonly ILogger _logger;
        private const string LoggerContext = "StartKnitterShiftUseCase";

        public StartKnitterShiftUseCase(IKnitterShiftGateway shiftGateway, ILogger? logger = null)
        {
            _shiftGateway = shiftGateway ?? throw new ArgumentNullException(nameof(shiftGateway));
            _logger = logger ?? new FileLogger();
        }

        public async Task<StartShiftResult> ExecuteAsync(StartShiftCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.Tab <= 0)
            {
                return Failure(StartShiftStatus.NoEmployee, "Не выбран сотрудник.");
            }

            if (command.PzvIds == null || command.PzvIds.Count == 0)
            {
                return Failure(StartShiftStatus.NoRows, "Нет строк для назначения.");
            }

            try
            {
                int shiftId;
                await using (var tx = await _shiftGateway.BeginShiftTransactionAsync().ConfigureAwait(false))
                {
                    var lockResult = await tx.TryAcquireZoneOpenShiftLockAsync(command.KmaId).ConfigureAwait(false);
                    if (!lockResult.CanOpen)
                    {
                        return Failure(lockResult.Status, BuildOpenLockMessage(command, lockResult), lockResult);
                    }

                    await tx.UpdatePzvTabAsync(command.PzvIds, command.Tab).ConfigureAwait(false);
                    shiftId = await tx.StartWorkingShiftAsync(command.Tab, command.KmaId, command.KmaNum, kmsId: 0).ConfigureAwait(false);
                    await tx.UpdatePzvKwsIdAsync(command.PzvIds, shiftId).ConfigureAwait(false);
                    await tx.CommitAsync().ConfigureAwait(false);
                }

                if (shiftId <= 0)
                {
                    return Failure(StartShiftStatus.Failed, "Не удалось открыть смену.");
                }

                return new StartShiftResult
                {
                    Success = true,
                    Status = StartShiftStatus.Success,
                    ErrorMessage = string.Empty,
                    ShiftId = shiftId,
                    ShiftStartTime = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                await SafeLogAsync(() => _logger.LogErrorAsync(
                    ex,
                    $"{LoggerContext}.{nameof(ExecuteAsync)}(tab={command.Tab}, kmaId={command.KmaId})"))
                    .ConfigureAwait(false);

                return Failure(StartShiftStatus.Failed, $"Не удалось открыть смену: {ex.Message}");
            }
        }

        private static string BuildOpenLockMessage(StartShiftCommand command, ShiftOpenLockResult lockResult)
        {
            if (!string.IsNullOrWhiteSpace(lockResult.ErrorMessage))
                return lockResult.ErrorMessage;

            return lockResult.Status switch
            {
                StartShiftStatus.ConcurrentOpenInProgress =>
                    "Смена в этой зоне уже открывается на другом рабочем месте. Обновите данные через несколько секунд.",
                StartShiftStatus.AlreadyOpen =>
                    $"В зоне {command.KmaNum} уже открыта смена.",
                _ => "Не удалось открыть смену."
            };
        }

        private static StartShiftResult Failure(StartShiftStatus status, string message, ShiftOpenLockResult? lockResult = null)
        {
            return new StartShiftResult
            {
                Success = false,
                Status = status,
                ErrorMessage = message ?? string.Empty,
                ExistingShiftId = lockResult?.ExistingShiftId,
                ExistingTabStart = lockResult?.ExistingTabStart,
                ExistingDateStart = lockResult?.ExistingDateStart
            };
        }

        private static async Task SafeLogAsync(Func<Task> writeLog)
        {
            try
            {
                await writeLog().ConfigureAwait(false);
            }
            catch
            {
            }
        }
    }
}
