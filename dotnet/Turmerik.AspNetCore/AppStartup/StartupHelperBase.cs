using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.LocalSession;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Infrastucture;

namespace Turmerik.AspNetCore.AppStartup
{
    public abstract class StartupHelperBase
    {
        public virtual IAppCoreServiceCollection RegisterCoreServices(
            IServiceCollection services, IConfiguration config)
        {
            var coreSvcs = TrmrkCoreServiceCollectionBuilder.RegisterAll(services);
            var typesCache = coreSvcs.TypesStaticDataCache;

            var trmrkAppSettingsMtbl = config.GetObject<TrmrkAppSettingsMtbl>(
                typesCache,
                ConfigKeys.TRMRK,
                typeof(TrmrkAppSettingsCoreMtbl),
                s =>
                {
                    s.LoginUrl = $"{s.AppBaseUrl}/{s.LoginRelUrl}";
                    s.LogoutUrl = $"{s.AppBaseUrl}/{s.LogoutRelUrl}";
                });

            var trmrkAppSettings = new TrmrkAppSettingsImmtbl(
                trmrkAppSettingsMtbl);

            var appSvcsMtbl = new AppCoreServiceCollectionMtbl(coreSvcs)
            {
                TrmrkAppSettings = trmrkAppSettingsMtbl
            };

            var appSvcsImmtbl = new AppCoreServiceCollectionImmtbl(appSvcsMtbl);
            services.AddSingleton<ITrmrkAppSettings>(provider => trmrkAppSettings);

            return appSvcsImmtbl;
        }

        public virtual void RegisterServices(IServiceCollection services, bool useMockData)
        {
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();

            services.AddHttpContextAccessor();

            services.AddSingleton<ILocalSessionsDictnr, LocalSessionsDictnr>();
            services.AddScoped<ILocalSessionsManager, LocalSessionsManager>();
        }
    }
}
