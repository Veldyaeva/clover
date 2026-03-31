using SewingProduction.Features.KnittingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results
{
    public sealed class LoadPzvOperationsResult
    {
        public IReadOnlyList<PZVOperList> Rows { get; init; } = Array.Empty<PZVOperList>();
        public string? JsonPayload { get; init; }
    }
}
