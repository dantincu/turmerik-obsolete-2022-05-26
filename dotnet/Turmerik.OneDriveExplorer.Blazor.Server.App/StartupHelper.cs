using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Turmerik.Blazor.Server.Core;
using Turmerik.Blazor.Server.Core.Services;
using Turmerik.Core.Helpers;
using Turmerik.Core.Infrastucture;
using Turmerik.OneDriveExplorer.Blazor.Server.App.AppSettings;
using Turmerik.OneDriveExplorer.Blazor.Server.App.Data;
using Turmerik.OneDriveExplorer.Blazor.Server.App.Graph;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App
{
    public class StartupHelper : StartupHelperBase
    {
        private readonly Func<ILogger<MainApplicationLog>> loggerFactory;
        private readonly Func<ITrmrkUserSessionsManager> userSessionManagerFactory;

        private ILogger<MainApplicationLog> logger;
        private ITrmrkUserSessionsManager userSessionManager;

        public StartupHelper(
            Func<ILogger<MainApplicationLog>> loggerFactory,
            Func<ITrmrkUserSessionsManager> userSessionManagerFactory)
        {
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            this.userSessionManagerFactory = userSessionManagerFactory ?? throw new ArgumentNullException(nameof(userSessionManagerFactory));
        }

        private ILogger<MainApplicationLog> Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = loggerFactory();
                }

                return logger;
            }
        }

        private ITrmrkUserSessionsManager UserSessionManager
        {
            get
            {
                if (userSessionManager == null)
                {
                    userSessionManager = userSessionManagerFactory();
                }

                return userSessionManager;
            }
        }

        public string GetAppErrorUrl(string errName, string errMsg)
        {
            errName = Uri.EscapeDataString(errName);

            string url = string.Format(
                "{0}?{1}={2}&{3}={4}",
                PageRoutes.APP_ERROR,
                QsKeys.ERR_NAME,
                errName,
                QsKeys.ERR_MSG,
                errMsg);

            return url;
        }

        public void RedirectToAppError<T>(HandleRequestContext<T> context, string errName, string errMsg)
            where T : AuthenticationSchemeOptions
        {
            string url = GetAppErrorUrl(errName, errMsg);
            context.Response.Redirect(url);

            context.HandleResponse();
        }

        public async Task OnTokenValidated(TokenValidatedContext context)
        {
            var tokenAcquisition = context.HttpContext.RequestServices
                .GetRequiredService<ITokenAcquisition>();

            var token = await tokenAcquisition.GetAccessTokenForUserAsync(
                GraphConstants.Scopes, user: context.Principal);

            var graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (request) =>
                    {
                        request.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);
                    })
            );

            // Get user information from Graph
            var user = await graphClient.Me.Request()
                .Select(u => new {
                    u.DisplayName,
                    u.Mail,
                    u.UserPrincipalName,
                    u.MailboxSettings
                })
                .GetAsync();

            context.Principal.AddUserGraphInfo(user);
            byte[] photoBytes = null;

            // Get the user's photo
            // If the user doesn't have a photo, this throws
            try
            {
                var photo = await graphClient.Me
                    .Photos["48x48"]
                    .Content
                    .Request()
                    .GetAsync();

                photoBytes = context.Principal.AddUserGraphPhoto(photo);
            }
            catch (ServiceException ex)
            {
                if (ex.IsMatch("ErrorItemNotFound") ||
                    ex.IsMatch("ConsumerPhotoIsNotSupported"))
                {
                    context.Principal.AddUserGraphPhoto((byte[]?)null);
                }
                else
                {
                    throw;
                }
            }

            RegisterUserLogin(user, token).Wait();
        }

        public async Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            var error = WebUtility.UrlEncode(context.Exception.Message);
            RedirectToAppError(context, "Authentication error", error);
        }

        public async Task OnRemoteFailure(RemoteFailureContext context)
        {
            if (context.Failure is OpenIdConnectProtocolException)
            {
                var error = WebUtility.UrlEncode(context.Failure.Message);
                RedirectToAppError(context, "Sign in error", error);
            }
        }

        public async Task OnRemoteSignOut(RemoteSignOutContext context)
        {
        }

        public async Task OnSignedOutCallbackRedirect(RemoteSignOutContext context)
        {
        }

        public async Task OnRedirectToIdentityProviderForSignOut(RedirectContext context)
        {
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<AuthService>();
            services.AddSingleton<TrmrkUserSessionsManager>();
        }

        private Task RegisterUserLogin(User user, string authToken)
        {
            var userIdentifier = UserSessionManager?.RegisterLogin(user.UserPrincipalName, authToken).Result;

            if (userIdentifier.HasValue)
            {
                var usrIdnf = userIdentifier.Value;
                Logger?.LogInformation($"USER LOGGED IN: [{usrIdnf.UsernameHash}]-[{usrIdnf.AuthTokenHash}]");
            }
            
            return Task.CompletedTask;
        }
    }
}
