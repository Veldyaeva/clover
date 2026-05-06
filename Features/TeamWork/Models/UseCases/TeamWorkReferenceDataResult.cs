using System.Collections.Generic;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class TeamWorkReferenceDataResult
    {
        public List<KodProizvModel> KodProizv { get; set; } = new();
        public List<PodrVyazModel> PodrVyaz { get; set; } = new();
        public List<OborudShvModel> OborudShv { get; set; } = new();
    }
}
