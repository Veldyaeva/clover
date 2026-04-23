using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases.PzvActions
{
    public sealed class CancelWorkStartUseCase
    {
        private readonly PzvActionValidator _validator;
        private readonly IPzvBulkUpdateService _bulkUpdateService;

        public CancelWorkStartUseCase(PzvActionValidator validator, IPzvBulkUpdateService bulkUpdateService)
        {
            _validator = validator;
            _bulkUpdateService = bulkUpdateService;
        }

        public async Task<OperationResult> ExecuteAsync(PzvSelectionContext context, CancellationToken ct = default)
        {
            var validIds = new List<int>();

            foreach (var row in context.SelectedOperations)
            {
                if (_validator.CanCancelStartWork(row))
                {
                    row.ErrorSelection = 0;
                    validIds.Add(row.olPzvID);
                }
                else
                {
                    _validator.ShowValidationMessage(
                        row,
                        _validator.ValidateCancelStartWork,
                        "Отмена начала выполнения операции");
                    row.ErrorSelection = 1;
                    row.SyncSelection = 0;
                }
            }

            if (validIds.Count == 0)
                return OperationResult.Fail("Нет строк для отмены запуска.");

            await _bulkUpdateService.UpdateAsync(validIds, row =>
            {
                row.olPzvDateStart = null;
            }, ct);

            return OperationResult.Ok();
        }
    }
}