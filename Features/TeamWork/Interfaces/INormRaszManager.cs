using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SewingProduction.Models;

namespace SewingProduction.Interfaces
{
    public interface INormRaszManager
    {
        Task<List<NormRasz>> LoadNormRaszAsync(int annId);
        Task<int> SaveNormRaszAsync(NormRasz norm);
        Task<BindingList<NormRasz>> LoadByAnnIdAsync(int annId);
    }
}
