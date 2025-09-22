using System;
using System.Threading.Tasks;

namespace SewingProduction
{
    public interface ILogger
    {
        Task LogErrorAsync(Exception ex, string context = "");
        Task LogErrorAsync(string v1, string v2);
        Task LogEventAsync(string eventMessage, string context = "");
        Task LogWarningAsync(string v1, string v2);
    }
}
