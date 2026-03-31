using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases.PzvActions
{
    public sealed class AssignKnittingMachineUseCase
    {
        private readonly PzvActionValidator _validator;
        private readonly IPzvBulkUpdateService _bulkUpdateService;

        public AssignKnittingMachineUseCase(PzvActionValidator validator, IPzvBulkUpdateService bulkUpdateService)
        {
            _validator = validator;
            _bulkUpdateService = bulkUpdateService;
        }

        public async Task<OperationResult> ExecuteAsync(PzvSelectionContext context, CancellationToken ct = default)
        {
            var shift = context.CurrentShiftAssignment;
            if (shift?.kwsmlKmlID == null || shift.kwsmlKmlID == 0)
                return OperationResult.Fail("Не выбрана вязальная машина.");

            var validIds = new List<int>();

            foreach (var row in context.SelectedOperations)
            {
                if (_validator.CanAssignKnittingMachine(row))
                {
                    row.ErrorSelection = 0;
                    validIds.Add(row.olPzvID);
                }
                else
                {
                    row.ErrorSelection = 1;
                    row.SyncSelection = 0;
                }
            }

            if (validIds.Count == 0)
                return OperationResult.Fail("Нет строк для назначения машины.");

            await _bulkUpdateService.UpdateAsync(validIds, row =>
            {
                row.olPzvKmlID = shift.kwsmlKmlID;
                row.olPzvDateNaznKm = System.DateTime.Now;
            }, ct);

            return OperationResult.Ok();
        }

    }
}