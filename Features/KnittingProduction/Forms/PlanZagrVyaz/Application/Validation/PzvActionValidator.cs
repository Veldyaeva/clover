using SewingProduction.Features.KnittingProduction.Models;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation
{
    public sealed class PzvActionValidator
    {
        public bool CanAssignKnittingMachine(PZVOperList row) =>
            row.olPzvDateNaznKm == null;

        public bool CanCancelKnittingMachine(PZVOperList row) =>
            row.olPzvDateNaznKm != null &&
            row.olPzvDateStart == null &&
            row.olPzvDateEnd == null;

        public bool CanAssignTab(PZVOperList row) =>
            row.olPzvDateNaznTab == null;

        public bool CanCancelTab(PZVOperList row) =>
            (row.olPzvDateNaznTab != null &&
                row.olPzvDateStart == null &&
                row.olPzvDateEnd == null) 
            || row.olPzvTab == 999;

        public bool CanStartWork(PZVOperList row) =>
            ((row.olKodPodr == 2 || row.olKodPodr == 3) || row.olPzvDateNaznKm != null) &&
            (row.olPzvDateNaznTab != null &&
            row.olPzvDateStart == null);

        public bool CanCancelStartWork(PZVOperList row) =>
            row.olPzvDateStart != null &&
            row.olPzvDateEnd == null;

        public bool CanStopWork(PZVOperList row) =>
            row.olPzvDateStart != null &&
            row.olPzvDateEnd == null;

        public bool CanCancelStopWork(PZVOperList row) =>
            row.olPzvDateEnd != null;

        public bool CanConfirmMaster(PZVOperList row) =>
            row.olPzvDateEnd != null &&
            row.olPzvDateMast == null;

        public bool CanCancelMasterConfirmation(PZVOperList row) =>
            row.olPzvDateMast != null;
    }
}