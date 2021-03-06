using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.MusicalKeyboard.Blazor.App.AppStartup
{
    public class StartupHelper : StartupHelperCoreBase
    {
        public override void RegisterServices(
            IServiceCollection services,
            bool useMockData)
        {
            base.RegisterServices(services, useMockData);
        }
    }
}
