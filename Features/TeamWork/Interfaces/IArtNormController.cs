using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Interfaces
{
    public interface IArtNormController
    {
        Task LoadCurrentDataAsync(int kod);
        Task LoadAnnByArticulAsync(int kod, string articul);
        Task<bool> InsertNormRaszAsync(NormRasz normRasz);
        Task<bool> InsertNormRaskAsync(NormRask normRask);
    }


}
