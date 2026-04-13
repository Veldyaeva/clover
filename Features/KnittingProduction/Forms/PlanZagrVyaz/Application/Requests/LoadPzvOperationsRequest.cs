using SewingProduction.Features.KnittingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Requests
{
    public sealed class LoadPzvOperationsRequest
    {
        public int VyazPodrKod { get; init; }
        public IReadOnlyList<RzvPachListByNom> SelectedPachList { get; init; } = Array.Empty<RzvPachListByNom>();
    }
}
