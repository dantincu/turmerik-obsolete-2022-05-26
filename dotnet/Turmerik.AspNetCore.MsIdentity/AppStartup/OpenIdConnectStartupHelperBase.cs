using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.UserSession;
using Turmerik.Core.Infrastucture;

namespace Turmerik.AspNetCore.MsIdentity.AppStartup
{
    public abstract class OpenIdConnectStartupHelperBase : StartupHelperBase
    {
    }
}
