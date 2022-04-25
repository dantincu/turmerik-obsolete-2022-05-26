using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Settings;
using Turmerik.AspNetCore.UserSession;
using Turmerik.Core.Infrastucture;

namespace Turmerik.AspNetCore.AppStartup
{
    public abstract class StartupHelperBase
    {
        private readonly Func<ILogger<MainApplicationLog>> loggerFactory;
        private ILogger<MainApplicationLog> LoggerInstn;

        protected StartupHelperBase(
            Func<ILogger<MainApplicationLog>> loggerFactory)
        {
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        protected ILogger<MainApplicationLog> Logger
        {
            get
            {
                if (LoggerInstn == null)
                {
                    LoggerInstn = loggerFactory();
                }

                return LoggerInstn;
            }
        }

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
