using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Core.interfaces
{
    public interface IAppServiceBrokerHub
    {
        Task SubscribeAsync(
            string ownerId,
            string ownerName,
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> tableFields,
            Func<string, string?, Task> onTableChangedAsync,
            CancellationToken ct);

        Task UnsubscribeAsync(string ownerId);

        string GetDiagnosticsSnapshot();
    }
}
