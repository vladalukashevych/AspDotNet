using AspDotNetLab3.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("config.json");
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddMvc(options =>
    options.EnableEndpointRouting = false);

builder.Services.AddSession()
    .AddHttpContextAccessor()
    .AddLogging();

var app = builder.Build();

app.UseSession();

app.MapGet("/", () => "Hello World!");

app.MapGet("/{lang}/{controller}/{action}/{id:int?}",
    (string lang, string controller, string action, string? id) =>
    {
        if (id == null)
            id = "null";
        return $"language: {lang} | controller: {controller} | action: {action} | id: {id}";
    });
app.MapGet("/{controller}/{action}/{id:int?}",
    (string controller, string action, string? id) =>
    {
        if (id == null)
            id = "null";
        return $"controller: {controller} | action: {action} | id: {id}";
    });

app.MapGet("/session/add/{param}/{value}",
    async (HttpContext context, string param, string value) =>
    {
        context.Session.SetString(param, value);
        await context.Response.WriteAsync($"Added");
    });
app.MapGet("/cookie/add/{param}/{value}",
    async (HttpContext context, string param, string value) =>
    {
        context.Response.Cookies.Append(param, value);
        await context.Response.WriteAsync($"Added");
    });
app.MapGet("/session/view/{param}",
    async (HttpContext context, string param) =>
    {
        var value = context.Session.GetString(param);
        if (value == null)
            value = "failed to get";
        await context.Response.WriteAsync($"{param} : {value}");
    });
app.MapGet("/cookie/view/{param}",
    async (HttpContext context, string param) =>
    {
        var value = context.Request.Cookies[param];
        if (value == null)
            value = "failed to get";
        await context.Response.WriteAsync($"{param} : {value}");
    });

app.UseMiddleware<LoggerMiddleware>();
app.Run();
