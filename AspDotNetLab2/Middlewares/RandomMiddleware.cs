using AspDotNetLab2.Interfaces;
using AspDotNetLab2.Services;

namespace AspDotNetLab2.Middlewares
{
    public class RandomMiddleware
    {
        RequestDelegate next;

        public RandomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRandomService random)
        {
            if (context.Request.Path == "/services/random")
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($"Numbers from random: {random?.GetNumber()}, {random?.GetNumber()}");
            }
            else
            {
                await next.Invoke(context);
            }

        }
    }
}
