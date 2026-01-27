using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Core.interfaces
{
    public interface IDataUpdatableFormAsyncV2
    {
        Task UpdateDataInFormAsync(string table, string? changedFields);
        Task UpdateDataInFormAsync(string table);
    }
}