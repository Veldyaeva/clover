using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation
{
    public sealed class PzvActionValidator
    {
        private readonly DatabaseHelperSQL _dbHelper;

        public PzvActionValidator(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public bool CanAssignKnittingMachine(PZVOperList row) => ValidateAssignKnittingMachine(row).IsValid;
        public bool CanCancelKnittingMachine(PZVOperList row) => ValidateCancelKnittingMachine(row).IsValid;
        public bool CanAssignTab(PZVOperList row) => ValidateAssignTab(row).IsValid;
        public bool CanCancelTab(PZVOperList row) => ValidateCancelTab(row).IsValid;
        public bool CanStartWork(PZVOperList row) => ValidateStartWork(row).IsValid;
        public bool CanCancelStartWork(PZVOperList row) => ValidateCancelStartWork(row).IsValid;
        public bool CanStopWork(PZVOperList row) => ValidateStopWork(row).IsValid;
        public bool CanCancelStopWork(PZVOperList row) => ValidateCancelStopWork(row).IsValid;
        public bool CanConfirmMaster(PZVOperList row) => ValidateConfirmMaster(row).IsValid;
        public bool CanCancelMasterConfirmation(PZVOperList row) => ValidateCancelMasterConfirmation(row).IsValid;

        public PzvValidationResult ValidateAssignKnittingMachine(PZVOperList row) =>
            Validate(row,
                RowExists,
                DateNaznKmMustBeEmpty,
                DateNaznTabMustBeEmpty,
                DateStartMustBeEmpty,
                DateEndMustBeEmpty,
                DateMastMustBeEmpty);

        public PzvValidationResult ValidateCancelKnittingMachine(PZVOperList row) =>
            Validate(row,
                RowExists,
                DateNaznKmMustBeFilled,
                DateNaznTabMustBeEmpty,
                DateStartMustBeEmpty,
                DateEndMustBeEmpty,
                DateMastMustBeEmpty);

        public PzvValidationResult ValidateAssignTab(PZVOperList row) =>
            Validate(row,
                RowExists,
                MustHaveMachineOrAllowedDepartment,
                DateNaznTabMustBeEmpty,
                DateStartMustBeEmpty,
                DateEndMustBeEmpty,
                DateMastMustBeEmpty);

        public PzvValidationResult ValidateCancelTab(PZVOperList row) =>
            Validate(row,
                RowExists,
                MustHaveMachineOrAllowedDepartment,
                DateNaznTabMustBeFilled,
                DateStartMustBeEmpty,
                DateEndMustBeEmpty,
                DateMastMustBeEmpty,
                TabMustNotBe999);

        public PzvValidationResult ValidateStartWork(PZVOperList row) =>
            Validate(row,
                RowExists,
                ShiftMustBeOpen,
                MustHaveMachineOrAllowedDepartment,
                DateNaznTabMustBeFilled,
                DateStartMustBeEmpty,
                DateEndMustBeEmpty,
                DateMastMustBeEmpty,
                KwsIdMustBeFilled);

        public PzvValidationResult ValidateCancelStartWork(PZVOperList row) =>
            Validate(row,
                RowExists,
                ShiftMustBeOpen,
                MustHaveMachineOrAllowedDepartment,
                DateNaznTabMustBeFilled,
                DateStartMustBeFilled,
                DateEndMustBeEmpty,
                DateMastMustBeEmpty,
                KwsIdMustBeFilled);

        public PzvValidationResult ValidateStopWork(PZVOperList row) =>
            Validate(row,
                RowExists,
                ShiftMustBeOpen,
                MustHaveMachineOrAllowedDepartment,
                DateNaznTabMustBeFilled,
                DateStartMustBeFilled,
                DateEndMustBeEmpty,
                DateMastMustBeEmpty,
                KwsIdMustBeFilled);

        public PzvValidationResult ValidateCancelStopWork(PZVOperList row) =>
            Validate(row,
                RowExists,
                ShiftMustBeOpen,
                MustHaveMachineOrAllowedDepartment,
                DateNaznTabMustBeFilled,
                DateStartMustBeFilled,
                DateEndMustBeFilled,
                DateMastMustBeEmpty,
                KwsIdMustBeFilled);

        public PzvValidationResult ValidateConfirmMaster(PZVOperList row) =>
            Validate(row,
                RowExists,
                ShiftMustBeOpen,
                MustHaveMachineOrAllowedDepartment,
                DateNaznTabMustBeFilled,
                DateStartMustBeFilled,
                DateEndMustBeFilled,
                DateMastMustBeEmpty,
                KwsIdMustBeFilled);

        public PzvValidationResult ValidateCancelMasterConfirmation(PZVOperList row) =>
            Validate(row,
                RowExists,
                ShiftMustBeOpen,
                MustHaveMachineOrAllowedDepartment,
                DateNaznTabMustBeFilled,
                DateStartMustBeFilled,
                DateEndMustBeFilled,
                DateMastMustBeFilled,
                KwsIdMustBeFilled);

        private delegate PzvValidationResult Rule(PZVOperList row);

        private static PzvValidationResult Validate(PZVOperList row, params Rule[] rules)
        {
            foreach (var rule in rules)
            {
                var result = rule(row);
                if (!result.IsValid)
                    return result;
            }

            return PzvValidationResult.Ok();
        }
        private int GetPzvIbpvtybkf (PZVOperList row)
        {
            if (row == null || row.olPzvID == 0)
                return 0;

            const string query = @"
                select pzvKwsID
                from planZagrVyaz
                where pzvID = @pzvID";

            object result = _dbHelper.ExecuteScalar<int>(query, new Dictionary<string, object>
            {
                ["@pzvID"] = row.olPzvID
            });

            return result != null ? Convert.ToInt32(result) : 0;
        }
        private bool IsShiftClosed(PZVOperList row)
        {

            //if (row == null || row.olPzvKwsID == 0)
            if (row == null || GetPzvID(row) == 0)
                return false;

            const string query = @"
                select kwsDateEnd
                from knitWorkingShiftNew_view
                where kwsID = @kwsID";

            object result = _dbHelper.ExecuteScalar<DateTime?>(query, new Dictionary<string, object>
            {
                ["@kwsID"] = GetPzvID(row)
            });

            return result != null && result != DBNull.Value;
        }
        private static PzvValidationResult Ok() => PzvValidationResult.Ok();
        private static PzvValidationResult Fail(string message) => PzvValidationResult.Fail(message);

        private static PzvValidationResult RowExists(PZVOperList row) =>
            row == null ? Fail("Строка операции не определена.") : Ok();

        private PzvValidationResult ShiftMustBeOpen(PZVOperList row) =>
            IsShiftClosed(row) ? Fail("Смена уже закрыта.") : Ok();

        private static PzvValidationResult MustHaveMachineOrAllowedDepartment(PZVOperList row) =>
            row.olKodPodr == 2 || row.olKodPodr == 3 || row.olPzvDateNaznKm != null
                ? Ok()
                : Fail("Операция не назначена на В/М и не относится к подразделениям 2 или 3.");

        private static PzvValidationResult KwsIdMustBeFilled(PZVOperList row) =>
            row.olPzvKwsID != 0
                ? Ok()
                : Fail("Не заполнен kwsID смены.");

        private static PzvValidationResult TabMustNotBe999(PZVOperList row) =>
            row.olPzvTab != 999
                ? Ok()
                : Fail("Для табельного номера 999 снятие назначения недопустимо.");

        private static PzvValidationResult DateNaznKmMustBeEmpty(PZVOperList row) =>
            row.olPzvDateNaznKm == null
                ? Ok()
                : Fail("Вязальная машина уже назначена.");

        private static PzvValidationResult DateNaznKmMustBeFilled(PZVOperList row) =>
            row.olPzvDateNaznKm != null
                ? Ok()
                : Fail("Вязальная машина не назначена.");

        private static PzvValidationResult DateNaznTabMustBeEmpty(PZVOperList row) =>
            row.olPzvDateNaznTab == null
                ? Ok()
                : Fail("Табельный номер уже назначен.");

        private static PzvValidationResult DateNaznTabMustBeFilled(PZVOperList row) =>
            row.olPzvDateNaznTab != null
                ? Ok()
                : Fail("Операция не назначена на табельный номер.");

        private static PzvValidationResult DateStartMustBeEmpty(PZVOperList row) =>
            row.olPzvDateStart == null
                ? Ok()
                : Fail("Операция уже запущена.");

        private static PzvValidationResult DateStartMustBeFilled(PZVOperList row) =>
            row.olPzvDateStart != null
                ? Ok()
                : Fail("Дата начала не заполнена.");

        private static PzvValidationResult DateEndMustBeEmpty(PZVOperList row) =>
            row.olPzvDateEnd == null
                ? Ok()
                : Fail("Дата окончания уже заполнена.");

        private static PzvValidationResult DateEndMustBeFilled(PZVOperList row) =>
            row.olPzvDateEnd != null
                ? Ok()
                : Fail("Дата окончания не заполнена.");

        private static PzvValidationResult DateMastMustBeEmpty(PZVOperList row) =>
            row.olPzvDateMast == null
                ? Ok()
                : Fail("Операция уже подтверждена мастером.");

        private static PzvValidationResult DateMastMustBeFilled(PZVOperList row) =>
            row.olPzvDateMast != null
                ? Ok()
                : Fail("Подтверждение мастером ещё не выполнено.");

        public bool ShowValidationMessage(PZVOperList row, Func<PZVOperList, PzvValidationResult> validator, string caption = "Проверка операции")
        {
            var result = validator(row);

            if (result.IsValid)
                return true;

            MessageBox.Show(result.Message, caption);
            return false;
        }
        //public bool CanAssignKnittingMachine(PZVOperList row) =>
        //    row.olPzvDateNaznKm == null &&
        //        row.olPzvDateNaznTab == null &&
        //        row.olPzvDateStart == null &&
        //        row.olPzvDateEnd == null &&
        //        row.olPzvDateMast == null;

        //public bool CanCancelKnittingMachine(PZVOperList row) =>
        //    row.olPzvDateNaznKm != null &&
        //    row.olPzvDateNaznTab == null &&
        //        row.olPzvDateStart == null &&
        //        row.olPzvDateEnd == null &&
        //        row.olPzvDateMast == null;

        //public bool CanAssignTab(PZVOperList row) =>
        //    (row.olPzvDateNaznKm != null || row.olKodPodr == 2 || row.olKodPodr == 3) &&
        //    row.olPzvDateNaznTab == null &&
        //        row.olPzvDateStart == null &&
        //        row.olPzvDateEnd == null &&
        //        row.olPzvDateMast == null;

        //public bool CanCancelTab(PZVOperList row) =>
        //    ((row.olPzvDateNaznKm != null || row.olKodPodr == 2 || row.olKodPodr == 3) &&
        //    row.olPzvDateNaznTab != null &&
        //        row.olPzvDateStart == null &&
        //        row.olPzvDateEnd == null &&
        //        row.olPzvDateMast == null)
        //    && row.olPzvTab != 999;
        //    //|| row.olPzvTab == 999;

        //public bool CanStartWork(PZVOperList row) =>
        //    !IsShiftClosed(row) &&
        //    ((row.olKodPodr == 2 || row.olKodPodr == 3) || row.olPzvDateNaznKm != null) &&
        //    (row.olPzvDateNaznTab != null &&
        //    row.olPzvDateStart == null &&
        //    row.olPzvDateEnd == null &&
        //    row.olPzvDateMast == null &&
        //    row.olPzvKwsID != 0
        //    );

        //public bool CanCancelStartWork(PZVOperList row) =>
        //   !IsShiftClosed(row) &&
        //    ((row.olKodPodr == 2 || row.olKodPodr == 3) || row.olPzvDateNaznKm != null) &&
        //    row.olPzvDateNaznTab != null &&
        //    row.olPzvDateStart != null &&
        //    row.olPzvDateEnd == null &&
        //    row.olPzvDateMast == null &&
        //    row.olPzvKwsID != 0;

        //public bool CanStopWork(PZVOperList row) =>
        //    !IsShiftClosed(row) &&
        //    ((row.olKodPodr == 2 || row.olKodPodr == 3) || row.olPzvDateNaznKm != null) &&
        //    row.olPzvDateNaznTab != null &&
        //    row.olPzvDateStart != null &&
        //    row.olPzvDateEnd == null &&
        //    row.olPzvDateMast == null &&
        //    row.olPzvKwsID != 0;

        //public bool CanCancelStopWork(PZVOperList row) =>
        //    !IsShiftClosed(row) &&
        //    ((row.olKodPodr == 2 || row.olKodPodr == 3) || row.olPzvDateNaznKm != null) &&
        //    row.olPzvDateNaznTab != null &&
        //    row.olPzvDateStart != null &&
        //    row.olPzvDateEnd != null &&
        //    row.olPzvDateMast == null &&
        //    row.olPzvKwsID != 0;

        //public bool CanConfirmMaster(PZVOperList row) =>
        //    !IsShiftClosed(row) &&
        //    ((row.olKodPodr == 2 || row.olKodPodr == 3) || row.olPzvDateNaznKm != null) &&
        //    row.olPzvDateNaznTab != null &&
        //    row.olPzvDateStart != null &&
        //    row.olPzvDateEnd != null &&
        //    row.olPzvDateMast == null &&
        //    row.olPzvKwsID != 0;

        //public bool CanCancelMasterConfirmation(PZVOperList row) =>
        //    !IsShiftClosed(row) &&
        //    ((row.olKodPodr == 2 || row.olKodPodr == 3) || row.olPzvDateNaznKm != null) &&
        //    row.olPzvDateNaznTab != null &&
        //    row.olPzvDateStart != null &&
        //    row.olPzvDateEnd != null &&
        //    row.olPzvDateMast != null &&
        //    row.olPzvKwsID != 0;
    }
}