using System;

namespace SewingProduction.Core
{
    public static class AppServices
    {
        public static IServiceProvider Services { get; private set; }

        public static void Configure(IServiceProvider provider)
        {
            Services = provider ?? throw new ArgumentNullException(nameof(provider));
        }
    }
}


