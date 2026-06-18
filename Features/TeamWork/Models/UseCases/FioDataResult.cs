using System.Collections.Generic;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Models.UseCases
{
    public class FioDataResult
    {
        public List<FioModel> Designers { get; set; } = new List<FioModel>();
        public List<FioModel> KnitConstructors { get; set; } = new List<FioModel>();
    }
}
