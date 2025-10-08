using System.ComponentModel;
using System.Threading.Tasks;
using SewingProduction.Models;

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
