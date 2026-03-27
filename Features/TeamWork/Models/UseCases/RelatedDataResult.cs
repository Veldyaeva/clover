using System.Collections.Generic;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class RelatedDataResult
    {
        public List<NormRask> NormRask { get; set; }
        public List<NormKont> NormKont { get; set; }
        public List<NormRasz> NormRasz { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
