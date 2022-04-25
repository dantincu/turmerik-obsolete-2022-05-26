using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Blazor.Server.Core.Services;
using Turmerik.Core.Infrastucture;

namespace Turmerik.Blazor.Server.Core
{
    public abstract class StartupHelperBase
    {
        public IAppCoreServiceCollection RegisterCoreServices(
            IServiceCollection services, IConfiguration config)
        {
            var coreSvcs = TrmrkCoreServiceCollectionBuilder.RegisterAll(services);
            var typesCache = coreSvcs.TypesStaticDataCache;

            var trmrkAppSettings = config.GetObject<TrmrkAppSettings>(
                typesCache,
                ConfigKeys.TRMRK,
                typeof(TrmrkAppSettingsCore),
                s =>
                {
                    s.LoginUrl = $"{s.AppBaseUrl}/{s.LoginRelUrl}";
                });

            var userSessionManager = new TrmrkUserSessionsManager();

            var appSvcsMtbl = new AppCoreServiceCollectionMtbl(coreSvcs)
            {
                TrmrkAppSettings = trmrkAppSettings,
                UserSessionsManager = userSessionManager
            };

            var appSvcsImmtbl = new AppCoreServiceCollectionImmtbl(appSvcsMtbl);
            services.AddSingleton(provider => trmrkAppSettings);

            services.AddSingleton<ITrmrkUserSessionsManager>(provider => userSessionManager);
            services.AddSingleton<IAppUserSessionsManager, AppUserSessionsManager>();

            return appSvcsImmtbl;
        }
    }
}
