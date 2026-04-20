using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases.PzvActions
{
    public sealed class CancelTab999UseCase
    {
        private readonly PzvActionValidator _validator;
        private readonly IPzvBulkUpdateService _bulkUpdateService;

        public CancelTab999UseCase(PzvActionValidator validator, IPzvBulkUpdateService bulkUpdateService)
        {
            _validator = validator;
            _bulkUpdateService = bulkUpdateService;
        }

        public async Task<OperationResult> ExecuteAsync(PzvSelectionContext context, CancellationToken ct = default)
        {
            var validIds = new List<int>();

            foreach (var row in context.SelectedOperations)
            {
                if (_validator.CanCancelTab(row))
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
                return OperationResult.Fail("Нет строк для снятия табельного номера.");

            await _bulkUpdateService.UpdateAsync(validIds, row =>
            {
                row.olPzvTab = 0;
                row.olPzvDateNaznTab = null;
                row.olPzvKwsID = 0;
                row.olPzvSekNazn = 0;
                row.olPzvKolNazn = 0;
                row.olPzvChasNazn = 0;
                row.olPzvDateStart = null;
                row.olPzvDateEnd = null;
                row.olPzvDateML = null;
                row.olPzvDateMast = null;
            }, ct);

            return OperationResult.Ok();
        }
    }
}