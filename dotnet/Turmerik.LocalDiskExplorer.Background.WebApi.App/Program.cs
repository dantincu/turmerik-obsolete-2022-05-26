using Turmerik.AspNetCore.AppStartup;
using Turmerik.LocalDiskExplorer.Background.WebApi.App.Hubs;
using Turmerik.NetCore.Services;

string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var helper = new StartupHelperCore();

var appSvcs = helper.RegisterCoreServices(builder.Services, builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(
                appSvcs.TrmrkAppSettings.LocalDiskExplorerAppBaseUrl.TrimEnd(
                    '/')).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();
app.MapControllers().RequireCors(myAllowSpecificOrigins);

app.MapHub<MainHub>($"/{LocalDiskExplorerBackgroundApiH.MAIN_HUB_NAME}");
app.Run();
