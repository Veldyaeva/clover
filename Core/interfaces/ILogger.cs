using System;
using System.Threading.Tasks;

namespace SewingProduction
{
    public interface ILogger
    {
        Task LogErrorAsync(Exception ex, string context = "");
        Task LogEventAsync(string eventMessage, string context = "");
        Task LogWarningAsync(string v1, string v2);
    }
}
