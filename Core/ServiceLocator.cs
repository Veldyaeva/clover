using Microsoft.Extensions.DependencyInjection;
using SewingProduction.Core.Services.DataCleanup;
using SewingProduction.Features.TeamWork.Services;
using System;

namespace SewingProduction.Core
{
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;

        static ServiceLocator()
        {
            Configure();
        }

        private static void Configure()
        {
            var services = new ServiceCollection();

            // Регистрируем необходимые сервисы
            services.AddScoped<ITeamWorkDataService, TeamWorkDataService>();
            services.AddSingleton<HybridLogger>();
            services.AddSingleton<IDataCleanupService, DataCleanupService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        public static T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
