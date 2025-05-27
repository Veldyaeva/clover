using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Interfaces
{
    public interface INormRaszManager
    {
        Task<List<NormRasz>> LoadNormRaszAsync(int annId);
        Task<int> SaveNormRaszAsync(NormRasz norm);
        Task<BindingList<NormRasz>> LoadByAnnIdAsync(int annId);
    }
}
