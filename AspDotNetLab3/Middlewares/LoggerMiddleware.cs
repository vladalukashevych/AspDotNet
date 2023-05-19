namespace AspDotNetLab3.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string filePath;

        public LoggerMiddleware(RequestDelegate next, IConfiguration config)
        {
            this.next = next;
            this.filePath = config["LogFile"];
        }

        public async Task Invoke(HttpContext context)
        {

            File.AppendAllText(filePath, $"{DateTime.Now.ToString()} " +
                $"{context.Connection.RemoteIpAddress?.ToString()} {context.Request.Path}\n");
            await next.Invoke(context);
        }
    }
}
