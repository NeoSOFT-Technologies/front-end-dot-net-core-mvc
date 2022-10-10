using Microsoft.AspNetCore.Diagnostics;
using MVC.Boilerplate.Middleware;
using Serilog;
using System.Net;

using MVC.Boilerplate.Application.Helper.ApiHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

//logger setup
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration)));


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped(typeof(IApiClient<>), typeof(ApiClient<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseCustomExceptionHandler();
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();