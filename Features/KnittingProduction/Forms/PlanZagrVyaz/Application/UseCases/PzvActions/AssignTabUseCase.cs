using DevExpress.XtraSpreadsheet.Import.Xlsb;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases.PzvActions
{
    public sealed class AssignTabUseCase
    {
        private readonly PzvActionValidator _validator;
        private readonly IPzvBulkUpdateService _bulkUpdateService;

        public AssignTabUseCase(PzvActionValidator validator, IPzvBulkUpdateService bulkUpdateService)
        {
            _validator = validator;
            _bulkUpdateService = bulkUpdateService;
        }

        public async Task<OperationResult> ExecuteAsync(PzvSelectionContext context, CancellationToken ct = default)
        {
            Debug.WriteLine($"USECASE shift null = {context.CurrentShiftAssignment == null}");

            var shift = context.CurrentShiftAssignment;
            Debug.WriteLine($"shift null = {shift == null}");
            Debug.WriteLine($"shift.kwsTabStart = {shift?.kwsTabStart}");
            Debug.WriteLine($"shift.szTab = {shift?.szTab}");
            Debug.WriteLine($"shift.kwsmlKmlID = {shift?.kwsmlKmlID}");
            Debug.WriteLine($"shift.fio = {shift?.fio}");
            Debug.WriteLine($"shift.szFio = {shift?.szFio}");
            //if (shift?.kwsmlTab == null || shift.kwsmlTab == 0)
            if ((shift?.kwsTabStart == null || shift.kwsTabStart == 0) && (shift?.szTab == null || shift.szTab == 0))
                    return OperationResult.Fail("Не выбран табельный номер.");

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
                    row.ErrorSelection = 1;
                    row.SyncSelection = 0;
                }
            }

            if (validIds.Count == 0)
                return OperationResult.Fail("Нет строк для назначения табельного номера.");

            await _bulkUpdateService.UpdateAsync(validIds, row =>
            {
                //row.olPzvTab = shift.kwsmlTab.Value;
                row.olPzvTab = shift.kwsTabStart == 0 ? shift.szTab : shift.kwsTabStart;
                row.olPzvDateNaznTab = System.DateTime.Now;
                row.olPzvKwsID = shift.kwsID;
            }, ct);

            return OperationResult.Ok();
        }
    }
}