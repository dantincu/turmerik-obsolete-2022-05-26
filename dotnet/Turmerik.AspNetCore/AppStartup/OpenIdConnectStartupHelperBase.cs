using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.UserSession;
using Turmerik.Core.Infrastucture;

namespace Turmerik.AspNetCore.AppStartup
{
    public abstract class OpenIdConnectStartupHelperBase : StartupHelperBase
    {
        private readonly Func<ITrmrkUserSessionsManager> userSessionManagerFactory;
        private ITrmrkUserSessionsManager UserSessionManagerInstn;

        protected OpenIdConnectStartupHelperBase(
            Func<ILogger<MainApplicationLog>> loggerFactory,
            Func<ITrmrkUserSessionsManager> userSessionManagerFactory) : base(loggerFactory)
        {
            this.userSessionManagerFactory = userSessionManagerFactory ?? throw new ArgumentNullException(nameof(userSessionManagerFactory));
        }

        protected ITrmrkUserSessionsManager UserSessionManager
        {
            get
            {
                if (UserSessionManagerInstn == null)
                {
                    UserSessionManagerInstn = userSessionManagerFactory();
                }

                return UserSessionManagerInstn;
            }
        }
    }
}
