using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Turmerik.AspNetCore.MsIdentity.AppStartup;
using Turmerik.AspNetCore.MsIdentity.Services.DriveItems;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.OneDriveExplorer.Blazor.App.AppStartup
{
    public class StartupHelper : MsIdentityStartupHelperBase
    {
        public override void RegisterServices(
            IServiceCollection services,
            bool useMockData)
        {
            base.RegisterServices(services, useMockData);
            // services.AddScoped<IDriveFolderService, OneDriveFolderService>();
            services.AddScoped<IDriveExplorerService, LocalDiskExplorerService>();
        }
    }
}
