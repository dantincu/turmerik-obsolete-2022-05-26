using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Turmerik.Blazor.Server.Core.Services;
using Turmerik.OneDriveExplorer.Blazor.Server.App.AppSettings;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.Data
{
    public class AuthService : ServiceBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TrmrkAppSettings trmrkAppSettings;
        private readonly NavigationManager navigationManager;

        public AuthService(
            ILogger<ApplicationLog> logger,
            IHttpContextAccessor httpContextAccessor,
            TrmrkAppSettings trmrkAppSettings,
            NavigationManager navigationManager) : base(logger)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.trmrkAppSettings = trmrkAppSettings ?? throw new ArgumentNullException(nameof(trmrkAppSettings));
            this.navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public async Task LogIn()
        {
            Logger.LogTrace("Processing a LogIn request");

            var context = httpContextAccessor.HttpContext;
            await context.AuthenticateAsync();
        }

        public async Task LogOut()
        {
            Logger.LogTrace("Processing a LogOut request");
            var context = httpContextAccessor.HttpContext;

            if (!context.Response.HasStarted)
            {
                await context.SignOutAsync("Cookies");

                var prop = new AuthenticationProperties()
                {
                    RedirectUri = trmrkAppSettings.LoginUrl
                };

                await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, prop);
            }
            else
            {
                navigationManager.NavigateTo(navigationManager.Uri, true, true);
            }
        }
    }
}
