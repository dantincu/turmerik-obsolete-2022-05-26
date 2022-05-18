using Microsoft.AspNetCore.ResponseCompression;
using Turmerik.MusicalKeyboard.Blazor.App.AppStartup;

var builder = WebApplication.CreateBuilder(args);

var helper = new StartupHelper();
var appSvcs = helper.RegisterCoreServices(builder.Services, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

helper.RegisterServices(builder.Services, appSvcs.TrmrkAppSettings.UseMockData);

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
