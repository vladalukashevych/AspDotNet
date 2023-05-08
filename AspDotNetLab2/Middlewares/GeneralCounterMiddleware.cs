using AspDotNetLab2.Interfaces;
using AspDotNetLab2.Services;
using System.Diagnostics.Metrics;
using System.Text;

namespace AspDotNetLab2.Middlewares
{
    public class GeneralCounterMiddleware
    {
        RequestDelegate next;

        public GeneralCounterMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IGeneralCounterService counter)
        {
            counter.IncreaseValue(context.Request.Path);

            if (context.Request.Path == "/services/general-counter")
            {                
                context.Response.ContentType = "text/html;charset=utf-8";
                string html = GetGeneralCountUrlHtml(counter);
                await context.Response.WriteAsync(html);
            }
            else
            {
                await next.Invoke(context);
            }
        }

        public string GetGeneralCountUrlHtml(IGeneralCounterService counter)
        {
            var generalCountUrl = counter.GetGeneralCountUrl();
            StringBuilder str = new StringBuilder();
            str.Append("<table>" +
                "<tr>" +
                "<th>URL</th>" +
                "<th>Request count</th>" +
                "</tr>");
            foreach (var row in generalCountUrl)
            {
                str.Append($"<tr><td>{row.Key}</td><td>{row.Value}</td></tr>");
            }
            str.Append("</table>");
            str.Append("<style>" +
                "table { border-collapse:collapse; }" +
                "th, td { padding:10px; padding-right:25px; border-bottom:1px solid lightgray; }" +
                "</style>");
            return str.ToString();
        }

    }
}

