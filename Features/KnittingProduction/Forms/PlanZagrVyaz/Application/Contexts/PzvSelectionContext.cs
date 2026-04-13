using SewingProduction.Features.KnittingProduction.Models;
using System;
using System.Collections.Generic;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts
{
    public sealed class PzvSelectionContext
    {
        public int VyazPodrKod { get; init; }
        public int CurrentPzvId { get; init; }
        public string CurrentColumn { get; init; } = string.Empty;

        public IReadOnlyList<PZVOperList> SelectedOperations { get; init; } = Array.Empty<PZVOperList>();
        public IReadOnlyList<RzvPachListByNom> SelectedPachList { get; init; } = Array.Empty<RzvPachListByNom>();
        public SmenZadanyVyaz? CurrentShiftAssignment { get; init; }
    }
}