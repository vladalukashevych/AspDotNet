using AspDotNetLab2.Classes;
using AspDotNetLab2.Interfaces;
using AspDotNetLab2.Middlewares;
using AspDotNetLab2.Services;
using Microsoft.AspNetCore.Builder;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITimerService, TimerService>();
builder.Services.AddScoped<IRandomService, RandomService>();
builder.Services.AddSingleton<IGeneralCounterService, GeneralCounterService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => "Welcome aboard!");

app.MapGet("/services/list", async (context) =>
{
    var services = builder.Services;
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync(ServicesInfo.ServicesListHtml(services));
});

app.UseMiddleware<GeneralCounterMiddleware>();
app.UseMiddleware<RandomMiddleware>();
app.UseMiddleware<TimerMiddleware>();

app.Run();
