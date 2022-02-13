using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Turmerik.OneDriveExplorer.Blazor.Server.App.Data;
using Microsoft.Graph;
using Turmerik.OneDriveExplorer.Blazor.Server.App.Graph;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Blazored.LocalStorage;
using Turmerik.OneDriveExplorer.Blazor.Server.App.AppSettings;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // var initialScopes = GraphConstants.Scopes; // Configuration.GetValue<string>("DownstreamApi:Scopes")?.Split(' ');

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(options =>
                {
                    Configuration.Bind("AzureAd", options);

                    options.Prompt = "select_account";

                    options.Events.OnTokenValidated = async context => {
                        var tokenAcquisition = context.HttpContext.RequestServices
                            .GetRequiredService<ITokenAcquisition>();

                        var graphClient = new GraphServiceClient(
                            new DelegateAuthenticationProvider(async (request) => {
                                var token = await tokenAcquisition
                                    .GetAccessTokenForUserAsync(GraphConstants.Scopes, user: context.Principal);
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

                        // Get the user's photo
                        // If the user doesn't have a photo, this throws
                        try
                        {
                            var photo = await graphClient.Me
                                .Photos["48x48"]
                                .Content
                                .Request()
                                .GetAsync();

                            context.Principal.AddUserGraphPhoto(photo);
                        }
                        catch (ServiceException ex)
                        {
                            if (ex.IsMatch("ErrorItemNotFound") ||
                                ex.IsMatch("ConsumerPhotoIsNotSupported"))
                            {
                                context.Principal.AddUserGraphPhoto(null);
                            }
                            else
                            {
                                throw;
                            }
                        }
                    };

                    options.Events.OnAuthenticationFailed = context => {
                        var error = WebUtility.UrlEncode(context.Exception.Message);
                        RedirectToAppError(context, "Authentication error", error);

                        return Task.FromResult(0);
                    };

                    options.Events.OnRemoteFailure = context =>
                    {
                        if (context.Failure is OpenIdConnectProtocolException)
                        {
                            var error = WebUtility.UrlEncode(context.Failure.Message);
                            RedirectToAppError(context, "Sign in error", error);
                        }

                        return Task.FromResult(0);
                    };
                })
                //.EnableTokenAcquisitionToCallDownstreamApi(GraphConstants.Scopes)

                // </AddSignInSnippet>
                // Add ability to call web API (Graph)
                // and get access tokens
                .EnableTokenAcquisitionToCallDownstreamApi(options => {
                    Configuration.Bind("AzureAd", options);
                }, GraphConstants.Scopes)
                // <AddGraphClientSnippet>
                // Add a GraphServiceClient via dependency injection
                .AddMicrosoftGraph(options => {
                    options.Scopes = string.Join(' ', GraphConstants.Scopes);
                })
                // </AddGraphClientSnippet>
                // Use in-memory token cache
                // See https://github.com/AzureAD/microsoft-identity-web/wiki/token-cache-serialization
                .AddInMemoryTokenCaches();
                //.AddMicrosoftGraph(Configuration.GetSection("DownstreamApi"))
                // .AddMicrosoftGraph()
                // .AddInMemoryTokenCaches();
            // services.AddControllersWithViews()
                // .AddMicrosoftIdentityUI();

            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();
            services.AddBlazoredLocalStorage();

            services.AddSingleton<WeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static string GetAppErrorUrl(string errName, string errMsg)
        {
            errName = Uri.EscapeDataString(errName);

            string url = string.Format(
                "{0}?{1}={2}&{3}={4}",
                PageRoutes.APP_ERROR,
                QueryStringKeys.ERR_NAME,
                errName,
                QueryStringKeys.ERR_MSG,
                errMsg);

            return url;
        }

        private static void RedirectToAppError<T>(HandleRequestContext<T> context, string errName, string errMsg)
            where T : AuthenticationSchemeOptions
        {
            string url = GetAppErrorUrl(errName, errMsg);
            context.Response.Redirect(url);

            context.HandleResponse();
        }
    }
}
