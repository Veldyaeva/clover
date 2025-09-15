using SewingProduction.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SewingProduction.Interfaces
{
    public interface IAnnManager
    {
        Task<BindingList<MyDataANN>> LoadAnnByArtAsync(int kod, string articul);
        void RefreshAnnGrid();
        Task SaveAnnToDatabaseAsync(MyDataANN ann);
        Task<BindingList<MyDataANN>> LoadByArtAsync(int kod, string articul, bool loadAll);
    }
}
