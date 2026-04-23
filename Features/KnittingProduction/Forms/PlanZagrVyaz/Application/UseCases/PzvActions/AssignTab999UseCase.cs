using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases.PzvActions
{
    public sealed class AssignTab999UseCase
    {
        private readonly PzvActionValidator _validator;
        private readonly IPzvBulkUpdateService _bulkUpdateService;

        public AssignTab999UseCase(PzvActionValidator validator, IPzvBulkUpdateService bulkUpdateService)
        {
            _validator = validator;
            _bulkUpdateService = bulkUpdateService;
        }

        public async Task<OperationResult> ExecuteAsync(PzvSelectionContext context, CancellationToken ct = default)
        {
            var shift = context.CurrentShiftAssignment;
            //if (shift?.kwsmlTab == null || shift.kwsmlTab == 0)
            //    return OperationResult.Fail("Не выбран табельный номер.");

            var validIds = new List<int>();

            foreach (var row in context.SelectedOperations)
            {
                if (_validator.CanAssignTab(row))
                {
                    row.ErrorSelection = 0;
                    validIds.Add(row.olPzvID);
                }
                else
                {
                    _validator.ShowValidationMessage(
                        row,
                        _validator.ValidateAssignTab,
                        "Таб 999");
                    row.ErrorSelection = 1;
                    row.SyncSelection = 0;
                }
            }

            if (validIds.Count == 0)
                return OperationResult.Fail("Нет строк для назначения табельного номера.");

            await _bulkUpdateService.UpdateAsync(validIds, row =>
            {
                row.olPzvTab = 999;
                row.olPzvDateNaznTab = System.DateTime.Now;
                row.olPzvKwsID = 0;
                row.olPzvKolNazn = row.olKol;
                row.olPzvSekNazn = row.olSekEd * row.olPzvKolNazn;
                row.olPzvChasNazn = row.olPzvNChasi;
                row.olPzvDateStart = System.DateTime.Now;
                row.olPzvDateEnd = System.DateTime.Now;
                row.olPzvDateML = System.DateTime.Now;
                row.olPzvDateMast = System.DateTime.Now;
            }, ct);

            return OperationResult.Ok();
        }
    }
}
