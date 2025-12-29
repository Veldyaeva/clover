using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWorkSpace.Models
{
    public sealed record TaskCandidate(
        int PzvId,
        int PriorityGroup,   // 1, 2 или 3
        double Hours         // длительность операции в часах
    );
}

