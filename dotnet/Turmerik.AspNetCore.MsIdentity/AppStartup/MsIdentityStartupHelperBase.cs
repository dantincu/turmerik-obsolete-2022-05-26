using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.AppStartup;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.MsIdentity.Graph;
using Turmerik.AspNetCore.MsIdentity.Services;
using Turmerik.AspNetCore.MsIdentity.Services.DriveItems;
using Turmerik.AspNetCore.OpenId.AppStartup;
using Turmerik.AspNetCore.OpenId.Services;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.Services.DriveItems;
using Turmerik.Core.Helpers;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Turmerik.AspNetCore.MsIdentity.AppStartup
{
    public abstract class MsIdentityStartupHelperBase : OpenIdConnectStartupHelperBase
    {
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
                .Select(u => new
                {
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

            var userSessionGuid = Guid.NewGuid();

            var usernameHashBytes = EncodeH.EncodeSha1(
                user.UserPrincipalName);

            var userNameHash = Convert.ToBase64String(usernameHashBytes);

            context.Response.Cookies.Append(
                SessionKeys.UserName,
                userNameHash);

            context.Response.Cookies.AddValue(
                SessionKeys.UserSessionGuid,
                userSessionGuid);
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

        public virtual void ConfigureServices(
            IServiceCollection services,
            IConfiguration config,
            IAppCoreServiceCollection appSvcs)
        {
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(options =>
                {
                    config.Bind("AzureAd", options);
                    options.AccessDeniedPath = $"/{appSvcs.TrmrkAppSettings.LoginRelUrl}";

                    options.Prompt = "select_account";
                    options.Events.OnTokenValidated = OnTokenValidated;

                    options.Events.OnAuthenticationFailed = OnAuthenticationFailed;
                    options.Events.OnRemoteFailure = OnRemoteFailure;
                })

                // Add ability to call web API (Graph)
                // and get access tokens
                .EnableTokenAcquisitionToCallDownstreamApi(options =>
                {
                    config.Bind("AzureAd", options);
                }, GraphConstants.Scopes)

                // Add a GraphServiceClient via dependency injection
                .AddMicrosoftGraph(options =>
                {
                    options.Scopes = string.Join(' ', GraphConstants.Scopes);
                })

                // Use in-memory token cache
                // See https://github.com/AzureAD/microsoft-identity-web/wiki/token-cache-serialization
                .AddInMemoryTokenCaches();

            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddRazorPages();

            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();
        }

        public override void RegisterServices(IServiceCollection services, bool useMockData)
        {
            base.RegisterServices(services, useMockData);
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
