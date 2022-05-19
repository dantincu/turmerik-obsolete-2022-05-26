using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.OpenId.UserSession;
using Turmerik.AspNetCore.Services;
using Turmerik.Core.Infrastucture;

namespace Turmerik.AspNetCore.OpenId.AppStartup
{
    public abstract class OpenIdConnectStartupHelperBase : StartupHelperCoreBase
    {
        public override void RegisterServices(IServiceCollection services, bool useMockData)
        {
            base.RegisterServices(services, useMockData);

            services.AddSingleton<IUserSessionsDictnr, UserSessionsDictnr>();
            services.AddScoped<IUserSessionsManager, UserSessionsManager>();
        }
    }
}
