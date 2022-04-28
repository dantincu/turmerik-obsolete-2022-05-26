using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Settings;

namespace Turmerik.AspNetCore.Services
{
    public interface IAuthService
    {
        Task LogIn();
        Task LogOut();
    }

    public class AuthService : ServiceBase, IAuthService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITrmrkAppSettings trmrkAppSettings;
        private readonly NavigationManager navigationManager;

        public AuthService(
            ILogger<ApplicationLog> logger,
            IHttpContextAccessor httpContextAccessor,
            ITrmrkAppSettings trmrkAppSettings,
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
