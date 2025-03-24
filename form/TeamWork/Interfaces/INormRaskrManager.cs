using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Interfaces
{
    public interface INormRaskManager
    {
        //Task<DataTable> LoadRaskroyNormByGroupAsync(int groupId);
        //Task SaveRaskroySelectionAsync(RaskroyNormSelection selection);
        Task LoadNormRask(int annId);
        Task<BindingList<NormRask>> LoadByAnnIdAsync(int annId);
    }

}
