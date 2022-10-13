using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Diagnostics;
using MVC.Boilerplate.Middleware;
using Serilog;
using System.Net;

using MVC.Boilerplate.Application.Helper.ApiHelper;
using System.Text.Json.Serialization;
using ServiceStack.Text;
using Rotativa.AspNetCore;

using MVC.Boilerplate.Application;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration Configuration = builder.Configuration;
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

//logger setup
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration)));


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped(typeof(IApiClient<>), typeof(ApiClient<>));

builder.Services.AddInfrastructureServices(Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// RotativaConfiguration.Setup(app.Environment);
RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)app.Environment);
app.UseHttpsRedirection();
app.UseNotyf();
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.UseCustomExceptionHandler();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
