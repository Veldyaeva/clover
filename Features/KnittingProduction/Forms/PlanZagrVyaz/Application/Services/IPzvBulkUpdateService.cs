using SewingProduction.Features.KnittingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services
{
    public interface IPzvBulkUpdateService
    {
        Task UpdateAsync(
            IReadOnlyCollection<int> pzvIds,
            Action<PZVOperList> mutate,
            CancellationToken ct = default);
    }
}
