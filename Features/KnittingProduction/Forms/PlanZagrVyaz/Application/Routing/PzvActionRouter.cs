using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases.PzvActions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Routing
{
    public sealed class PzvActionRouter : IPzvActionRouter
    {
        private readonly AssignKnittingMachineUseCase _assignKnittingMachine;
        private readonly CancelKnittingMachineUseCase _cancelKnittingMachine;
        private readonly AssignTabUseCase _assignTab;
        private readonly CancelTabUseCase _cancelTab;
        private readonly StartWorkUseCase _startWork;
        private readonly CancelWorkStartUseCase _cancelStartWork;
        private readonly StopWorkUseCase _stopWork;
        private readonly CancelWorkStopUseCase _cancelStopWork;
        private readonly ConfirmMasterUseCase _confirmMaster;
        private readonly CancelMasterConfirmationUseCase _cancelMaster;
        private readonly AssignTab999UseCase _assignTab999;
        private readonly CancelTab999UseCase _cancelTab999;

        public PzvActionRouter(
            AssignKnittingMachineUseCase assignKnittingMachine,
            CancelKnittingMachineUseCase cancelKnittingMachine,
            AssignTabUseCase assignTab,
            CancelTabUseCase cancelTab,
            StartWorkUseCase startWork,
            CancelWorkStartUseCase cancelStartWork,
            StopWorkUseCase stopWork,
            CancelWorkStopUseCase cancelStopWork,
            ConfirmMasterUseCase confirmMaster,
            CancelMasterConfirmationUseCase cancelMaster,
            AssignTab999UseCase assignTab999,
            CancelTab999UseCase cancelTab999)
        {
            _assignKnittingMachine = assignKnittingMachine;
            _cancelKnittingMachine = cancelKnittingMachine;
            _assignTab = assignTab;
            _cancelTab = cancelTab;
            _startWork = startWork;
            _cancelStartWork = cancelStartWork;
            _stopWork = stopWork;
            _cancelStopWork = cancelStopWork;
            _confirmMaster = confirmMaster;
            _cancelMaster = cancelMaster;
            _assignTab999 = assignTab999;
            _cancelTab999 = cancelTab999;
        }

        public Task<OperationResult> ExecuteAsync(PzvActionType action, PzvSelectionContext context, CancellationToken ct = default)
        {
            Debug.WriteLine($"ROUTER ACTION = {action}");
            Debug.WriteLine($"router context shift null = {context.CurrentShiftAssignment == null}");

            return action switch
            {
                PzvActionType.AssignKnittingMachine => _assignKnittingMachine.ExecuteAsync(context, ct),
                PzvActionType.CancelKnittingMachine => _cancelKnittingMachine.ExecuteAsync(context, ct),
                PzvActionType.AssignTab => _assignTab.ExecuteAsync(context, ct),
                PzvActionType.CancelTab => _cancelTab.ExecuteAsync(context, ct),
                PzvActionType.StartWork => _startWork.ExecuteAsync(context, ct),
                PzvActionType.CancelStartWork => _cancelStartWork.ExecuteAsync(context, ct),
                PzvActionType.StopWork => _stopWork.ExecuteAsync(context, ct),
                PzvActionType.CancelStopWork => _cancelStopWork.ExecuteAsync(context, ct),
                PzvActionType.ConfirmMaster => _confirmMaster.ExecuteAsync(context, ct),
                PzvActionType.CancelMasterConfirmation => _cancelMaster.ExecuteAsync(context, ct),
                PzvActionType.AssignTab999 => _assignTab999.ExecuteAsync(context, ct),
                PzvActionType.CancelTab999 => _cancelTab999.ExecuteAsync(context, ct),
                _ => Task.FromResult(OperationResult.Fail("Неизвестное действие"))
            };
        }
    }
}