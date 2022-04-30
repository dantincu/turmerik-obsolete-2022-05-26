using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.Core.Services;

namespace Turmerik.LocalDiskExplorer.Blazor.App.AppStartup
{
    public class StartupHelper : StartupHelperBase
    {
        public override void RegisterServices(
            IServiceCollection services,
            bool useMockData)
        {
            base.RegisterServices(services, useMockData);
            services.AddScoped<IDriveFolderService, FileSystemFolderService>();
        }
    }
}
