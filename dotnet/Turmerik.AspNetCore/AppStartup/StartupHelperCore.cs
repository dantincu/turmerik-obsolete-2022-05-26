using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Infrastucture;

namespace Turmerik.AspNetCore.AppStartup
{
    public class StartupHelperCore
    {
        public IAppCoreServiceCollection AppCoreServiceCollection { get; private set; }

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

            var rsaComponent = new RSAComponent();

            var appSvcsMtbl = new AppCoreServiceCollectionMtbl(coreSvcs)
            {
                TrmrkAppSettings = trmrkAppSettingsMtbl,
                RSAComponent = rsaComponent,
            };

            services.AddSingleton<ITrmrkAppSettings>(provider => trmrkAppSettings);
            services.AddSingleton<IRSAComponent>(provider => rsaComponent);

            AppCoreServiceCollection = new AppCoreServiceCollectionImmtbl(appSvcsMtbl);
            return AppCoreServiceCollection;
        }
    }
}
