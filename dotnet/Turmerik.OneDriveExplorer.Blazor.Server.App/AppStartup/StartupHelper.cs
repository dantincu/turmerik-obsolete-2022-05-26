using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.UserSession;
using Turmerik.OneDriveExplorer.Blazor.Server.App.Data;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.AppStartup
{
    public class StartupHelper : MsIdentityStartupHelperBase
    {
        public override void RegisterServices(
            IServiceCollection services,
            bool useMockData)
        {
            base.RegisterServices(services, useMockData);

            if (useMockData)
            {

            }
            else
            {
                services.AddScoped<IDriveFolderService, DriveFolderService>();
            }
            
        }
    }
}
