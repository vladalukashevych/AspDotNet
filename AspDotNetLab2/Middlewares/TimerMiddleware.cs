using AspDotNetLab2.Classes;
using AspDotNetLab2.Interfaces;
using System.Diagnostics.Metrics;
using System.Text;

namespace AspDotNetLab2.Middlewares
{
    public class TimerMiddleware
    {

        RequestDelegate next;

        public TimerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITimerService timer)
        {
            if (context.Request.Path == "/services/timer")
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($"Current date and time: {timer?.GetDateAndTime()}");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
