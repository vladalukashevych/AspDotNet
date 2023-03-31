using AspDotNetLab1.customMiddleware;
using Microsoft.Extensions.FileProviders;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseFileServer(new FileServerOptions
{
    EnableDirectoryBrowsing = true,
    FileProvider = new PhysicalFileProvider(
Path.Combine(Directory.GetCurrentDirectory(), @"static"))
});


app.UseLoggerMiddleware();
app.UseSecretMiddleware();

app.Map("/home", async appBuilder =>
{
    appBuilder.Map("/index", Index);
    appBuilder.Map("/about", About);
    appBuilder.Run(async context =>
    {
        context.Response.StatusCode = 404;
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("static/files/404.html");
    }
    );
});

app.Run(async (context) =>
{
    var path = context.Request.Path;
    var fullPath = $"wwwroot/{path}";
    var response = context.Response;

    response.ContentType = "text/html; charset=utf-8";
    if (File.Exists(fullPath))
    {
        await response.SendFileAsync(fullPath);
    }
    else
    {
        response.StatusCode = 404;
        await response.SendFileAsync("static/files/404.html");
    }
});



app.Run();


void Index(IApplicationBuilder appBuilder)
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("wwwroot/home/index.html");
    });
    
}
void About(IApplicationBuilder appBuilder)
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("wwwroot/home/about.html");
    });
}