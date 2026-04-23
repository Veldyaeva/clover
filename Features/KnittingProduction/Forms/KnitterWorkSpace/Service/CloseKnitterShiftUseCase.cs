using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Use-case завершения смены вязальщика.
    /// Владеет конкурентной защитой: сначала захватывает смену в БД,
    /// затем проверяет актуальные операции и только после этого закрывает смену.
    /// </summary>
    public sealed class CloseKnitterShiftUseCase
    {
        private readonly IKnitterShiftGateway _shiftGateway;
        private readonly ILogger _logger;
        private const string LoggerContext = "CloseKnitterShiftUseCase";

        public CloseKnitterShiftUseCase(IKnitterShiftGateway shiftGateway, ILogger? logger = null)
        {
            _shiftGateway = shiftGateway ?? throw new ArgumentNullException(nameof(shiftGateway));
            _logger = logger ?? new FileLogger();
        }

        public async Task<CloseShiftResult> ExecuteAsync(CloseShiftCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.ShiftId <= 0)
            {
                return Failure(
                    CloseShiftStatus.NotFound,
                    "Не удалось определить открытую смену.");
            }

            try
            {
                await using var tx = await _shiftGateway.BeginShiftTransactionAsync().ConfigureAwait(false);

                var lockResult = await tx.TryAcquireShiftCloseLockAsync(command.ShiftId).ConfigureAwait(false);
                if (!lockResult.CanClose)
                {
                    return Failure(lockResult.Status, lockResult.ErrorMessage);
                }

                var unfinished = (await tx.GetUnfinishedOperationIdsForShiftAsync(command.ShiftId, command.TabEnd).ConfigureAwait(false))
                    .Where(id => id > 0)
                    .Distinct()
                    .ToList();

                if (unfinished.Count > 0)
                {
                    return new CloseShiftResult
                    {
                        Success = false,
                        Status = CloseShiftStatus.HasUnfinishedOperations,
                        ErrorMessage = "Есть начатые и не завершённые операции. Смену закрывать нельзя.",
                        HasUnfinishedOperations = true,
                        UnfinishedPzvIds = unfinished
                    };
                }

                await tx.AdjustNotStartedBeforeShiftEndAsync(
                        command.ShiftId,
                        command.TabEnd,
                        command.MinHours,
                        command.UserName ?? string.Empty)
                    .ConfigureAwait(false);

                var closeResult = await tx.TryEndWorkingShiftAsync(command.ShiftId, command.TabEnd).ConfigureAwait(false);
                if (!closeResult.Closed)
                {
                    return Failure(
                        closeResult.Status,
                        closeResult.ErrorMessage);
                }

                await tx.CommitAsync().ConfigureAwait(false);

                return new CloseShiftResult
                {
                    Success = true,
                    Status = CloseShiftStatus.Success,
                    ErrorMessage = string.Empty,
                    HasUnfinishedOperations = false,
                    UnfinishedPzvIds = new List<int>()
                };
            }
            catch (Exception ex)
            {
                await SafeLogAsync(() => _logger.LogErrorAsync(
                    ex,
                    $"{LoggerContext}.{nameof(ExecuteAsync)}(shiftId={command.ShiftId}, tabEnd={command.TabEnd})"))
                    .ConfigureAwait(false);

                return Failure(
                    CloseShiftStatus.Failed,
                    $"Не удалось завершить смену: {ex.Message}");
            }
        }

        private static CloseShiftResult Failure(CloseShiftStatus status, string message)
        {
            return new CloseShiftResult
            {
                Success = false,
                Status = status,
                ErrorMessage = message ?? string.Empty,
                HasUnfinishedOperations = false,
                UnfinishedPzvIds = new List<int>()
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
