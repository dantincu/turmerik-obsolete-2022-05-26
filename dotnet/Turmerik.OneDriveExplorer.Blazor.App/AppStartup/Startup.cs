using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Turmerik.AspNetCore.Services;
using Turmerik.Blazor.Core.Hubs;

namespace Turmerik.OneDriveExplorer.Blazor.App.AppStartup
{
    public class Startup
    {
        private readonly StartupHelper helper;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            helper = new StartupHelper();
        }

        public IConfiguration Configuration { get; }

        private IAppCoreServiceCollection AppSvcs { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AppSvcs = helper.RegisterCoreServices(services, Configuration);
            helper.ConfigureServices(services, Configuration, AppSvcs);

            helper.RegisterServices(services, AppSvcs.TrmrkAppSettings.UseMockData);

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
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
                endpoints.MapHub<TrmrkAppHub>(AppSvcs.TrmrkAppSettings.TrmrkAppHubRelUrl);
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
