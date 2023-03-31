using System.Text.RegularExpressions;

namespace AspDotNetLab1.customMiddleware
{
    public class SecretMiddleware
    {
        readonly RequestDelegate next;
        public SecretMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var match = Regex.Match(context.Request.Path.Value, @"secret-\d+$").Value;
            if (match.Any())
            {
                var number = Regex.Match(match, @"\d+$").Value;
                await context.Response.WriteAsync($"Here you go, our secret message #{number}.\n\n God loves you, honey! Keep going!");
            }
            else
            {
                await next(context);
            }            
            
        }
    }

    public static class SecretMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecretMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecretMiddleware>();
        }
    }
}
