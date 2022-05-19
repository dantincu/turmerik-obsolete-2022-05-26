using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.LocalSession;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Services.LocalSessionStorage;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Infrastucture;

namespace Turmerik.AspNetCore.AppStartup
{
    public abstract class StartupHelperCoreBase : StartupHelperCore
    {
        public virtual void RegisterServices(IServiceCollection services, bool useMockData)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<INavManager, NavManager>();

            services.AddSingleton<ILocalSessionsDictnr, LocalSessionsDictnr>();
            services.AddScoped<ILocalSessionsManager, LocalSessionsManager>();

            services.AddScoped<ISessionStorageSvc, SessionStorageSvc>();
            services.AddScoped<ILocalStorageSvc, LocalStorageSvc>();

            services.AddScoped<ILocalStorageWrapper, LocalStorageWrapper>();
            services.AddScoped<ISessionStorageWrapper, SessionStorageWrapper>();

            services.AddScoped<IMainLayoutService, MainLayoutService>();
        }
    }
}
