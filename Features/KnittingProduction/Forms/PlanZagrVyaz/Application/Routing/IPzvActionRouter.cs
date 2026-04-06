using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Routing
{
    public interface IPzvActionRouter
    {
        Task<Results.OperationResult> ExecuteAsync(PzvActionType action, Contexts.PzvSelectionContext context, CancellationToken ct = default);
    }
}