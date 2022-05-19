using Turmerik.AspNetCore.AppStartup;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var helper = new StartupHelperCore();

var appSvcs = helper.RegisterCoreServices(builder.Services, builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(appSvcs.TrmrkAppSettings.LocalDiskExplorerAppBaseUrl.TrimEnd('/'));
        });
});

// Add services to the container.

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.MapControllers();

app.Run();
