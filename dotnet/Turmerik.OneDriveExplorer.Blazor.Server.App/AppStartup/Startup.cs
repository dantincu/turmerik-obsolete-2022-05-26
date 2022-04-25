using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using Turmerik.AspNetCore.Graph;
using Turmerik.AspNetCore.Services;
using Turmerik.AspNetCore.UserSession;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.AppStartup
{
    public class Startup
    {
        private readonly StartupHelper helper;
        private ILogger<MainApplicationLog> logger;
        private ITrmrkUserSessionsManager userSessionManager;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            helper = new StartupHelper(
                () => logger,
                () => userSessionManager);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var appSvcs = helper.RegisterCoreServices(services, Configuration);
            userSessionManager = appSvcs.UserSessionsManager;

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(options =>
                {
                    Configuration.Bind("AzureAd", options);
                    options.AccessDeniedPath = $"/{appSvcs.TrmrkAppSettings.LoginRelUrl}";

                    options.Prompt = "select_account";
                    options.Events.OnTokenValidated = helper.OnTokenValidated;

                    options.Events.OnAuthenticationFailed = helper.OnAuthenticationFailed;
                    options.Events.OnRemoteFailure = helper.OnRemoteFailure;
                })

                // Add ability to call web API (Graph)
                // and get access tokens
                .EnableTokenAcquisitionToCallDownstreamApi(options =>
                {
                    Configuration.Bind("AzureAd", options);
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
            services.AddBlazoredLocalStorage();

            helper.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<MainApplicationLog>();

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
    }
}
