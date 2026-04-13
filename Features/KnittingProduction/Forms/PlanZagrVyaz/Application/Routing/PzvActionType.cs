using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Routing
{
    public enum PzvActionType
    {
        AssignKnittingMachine,
        CancelKnittingMachine,
        AssignTab,
        CancelTab,
        StartWork,
        CancelStartWork,
        StopWork,
        CancelStopWork,
        ConfirmMaster,
        CancelMaster,
        CancelMasterConfirmation,
        AssignTab999,
        CancelTab999
    }
}
