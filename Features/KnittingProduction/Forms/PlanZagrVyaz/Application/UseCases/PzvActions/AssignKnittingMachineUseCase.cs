using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases.PzvActions
{
    public sealed class AssignKnittingMachineUseCase
    {
        private readonly PzvActionValidator _validator;
        private readonly IPzvBulkUpdateService _bulkUpdateService;
        private readonly DatabaseHelperSQL _dbHelper;

        public AssignKnittingMachineUseCase(
            PzvActionValidator validator,
            IPzvBulkUpdateService bulkUpdateService,
            DatabaseHelperSQL dbHelper)
        {
            _validator = validator;
            _bulkUpdateService = bulkUpdateService;
            _dbHelper = dbHelper;
        }

        public async Task<OperationResult> ExecuteAsync(PzvSelectionContext context, CancellationToken ct = default)
        {
            var shift = context.CurrentShiftAssignment;
            if (shift?.kwsmlKmlID == null || shift.kwsmlKmlID == 0)
                return OperationResult.Fail("Не выбрана вязальная машина.");

            var checkList = context.SelectedOperations?.ToList() ?? new List<PZVOperList>();
            var validIds = new List<int>();

            foreach (var row in checkList)
            {
                if (!_validator.CanAssignKnittingMachine(row))
                {
                    row.ErrorSelection = 1;
                    row.SyncSelection = 0;
                    continue;
                }

                if (row.olKodPodr != 1)
                {
                    MessageBox.Show("Операция не относится к вязальному подразделению, нельзя изменять В/М!");
                    return OperationResult.Fail("Операция не относится к вязальному подразделению.");
                }

                string query =
                    @"SELECT pszm.pszkmKmlID, mlv.kmlNumber, mlv.name_class
                      FROM plan_sezon_zad_knitMachine pszm
                      LEFT JOIN knitMachineList_view mlv ON pszm.pszkmKmlID = mlv.kmlID
                      WHERE pszm.pszkmPszNom = @PszNom and pszkmKnitClass = @KnitClass";

                DataTable machineInfo = _dbHelper.ExecuteQuery(query, new Dictionary<string, object>
                {
                    { "@PszNom", row.olNomZad },
                    { "@KnitClass", row.olIdVyazClass }
                });

                int planKmlID = 0;
                string planKmlNumber = string.Empty;
                string planVyazClass = string.Empty;

                if (machineInfo.Rows.Count > 0)
                {
                    planKmlID = Convert.ToInt32(machineInfo.Rows[0]["pszkmKmlID"]);
                    planKmlNumber = Convert.ToString(machineInfo.Rows[0]["kmlNumber"]);
                    planVyazClass = Convert.ToString(machineInfo.Rows[0]["name_class"]);
                }

                if (planKmlID != shift.kwsmlKmlID && row.olPzvKmlID == 0)
                {
                    string button1Text = $"Продолжить: назначить В/М \n оп. {row.olNomOper} {row.olOperName}";
                    string button2Text = $"Пропустить: НЕ назначать В/М \n оп. {row.olNomOper} {row.olOperName}";
                    string button3Text = checkList.Count == 1 ? "" : "Прервать назначение В/М на выбранные операции";

                    Dictionary<string, DialogResult> buttonsList;
                    if (checkList.Count == 1)
                    {
                        buttonsList = new Dictionary<string, DialogResult>
                        {
                            { button1Text, DialogResult.Yes },
                            { button2Text, DialogResult.No }
                        };
                    }
                    else
                    {
                        buttonsList = new Dictionary<string, DialogResult>
                        {
                            { button1Text, DialogResult.Yes },
                            { button2Text, DialogResult.No },
                            { button3Text, DialogResult.Abort }
                        };
                    }

                    var options = new MessageBoxOptions
                    {
                        Text = $"Внимание!\n" +
                               $"Операция: {row.olNomOper}\n" +
                               $"Описание: {row.olOperName}\n\n" +
                               $"Назначаемая машина не совпадает с плановой:" +
                               $"\n плановая В/М№ {planKmlNumber} - класс {planVyazClass}" +
                               $"\n назначаемая В/М№ {shift.kmlNumber} - класс {shift.nameVyazClass}",
                        Caption = "Назначение В/М на операцию",
                        Buttons = buttonsList,
                        Icon = MessageBoxIcon.Warning,
                        ButtonLayout = ButtonLayout.Vertical,
                        StretchVerticalButtons = true,
                        VerticalButtonSpacing = 15,
                        ButtonHeight = 45,
                        ButtonPadding = 20
                    };

                    var result = AdvancedMessageBox.Show(options);

                    switch (result)
                    {
                        case DialogResult.No:
                            row.ErrorSelection = 1;
                            row.SyncSelection = 0;
                            continue;

                        case DialogResult.Abort:
                        case DialogResult.Cancel:
                            return OperationResult.Fail("Назначение В/М прервано пользователем.");
                    }
                }

                row.ErrorSelection = 0;
                validIds.Add(row.olPzvID);
            }

            if (validIds.Count == 0)
                return OperationResult.Fail("Нет строк для назначения машины.");

            await _bulkUpdateService.UpdateAsync(validIds, row =>
            {
                row.olPzvKmlID = shift.kwsmlKmlID;
                row.olPzvDateNaznKm = DateTime.Now;
            }, ct);

            return OperationResult.Ok();
        }
    }
    //public sealed class AssignKnittingMachineUseCase
    //{
    //    private readonly PzvActionValidator _validator;
    //    private readonly IPzvBulkUpdateService _bulkUpdateService;
    //    private readonly DatabaseHelper _dbHelper;

    //    public AssignKnittingMachineUseCase(
    //        PzvActionValidator validator,
    //        IPzvBulkUpdateService bulkUpdateService,
    //        DatabaseHelper dbHelper)
    //    {
    //        _validator = validator;
    //        _bulkUpdateService = bulkUpdateService;
    //        _dbHelper = dbHelper;
    //    }

    //    public async Task<OperationResult> ExecuteAsync(PzvSelectionContext context, CancellationToken ct = default)
    //    {
    //        var shift = context.CurrentShiftAssignment;
    //        if (shift?.kwsmlKmlID == null || shift.kwsmlKmlID == 0)
    //            return OperationResult.Fail("Не выбрана вязальная машина.");

    //        var validIds = new List<int>();

    //        foreach (var row in context.SelectedOperations)
    //        {
    //            if (_validator.CanAssignKnittingMachine(row))
    //            {
    //                row.ErrorSelection = 0;
    //                validIds.Add(row.olPzvID);
    //            }
    //            else
    //            {
    //                row.ErrorSelection = 1;
    //                row.SyncSelection = 0;
    //            }
    //        }

    //        if (validIds.Count == 0)
    //            return OperationResult.Fail("Нет строк для назначения машины.");

    //        await _bulkUpdateService.UpdateAsync(validIds, row =>
    //        {
    //            row.olPzvKmlID = shift.kwsmlKmlID;
    //            row.olPzvDateNaznKm = System.DateTime.Now;
    //        }, ct);

    //        return OperationResult.Ok();
    //    }

    //}
}