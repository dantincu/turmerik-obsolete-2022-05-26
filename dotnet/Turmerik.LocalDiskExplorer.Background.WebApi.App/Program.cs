using Turmerik.AspNetCore.AppStartup;

string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
// string myAllowAllOrigins = "_myAllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);
var helper = new StartupHelperCore();

var appSvcs = helper.RegisterCoreServices(builder.Services, builder.Configuration);

builder.Services.AddCors(options =>
{
    /* options.AddPolicy(myAllowAllOrigins, builder =>
          builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()); */

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
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();
app.MapControllers().RequireCors(myAllowSpecificOrigins);

app.Run();
