using System.Text;

namespace AspDotNetLab2.Classes
{
    public static class ServicesInfo
    {
        public static string ServicesListHtml(IServiceCollection services)
        {
            var str = new StringBuilder();

            str.Append("<table>" +
                "<tr>" +
                "<th>Type</th>" +
                "<th>Lifetime</th>" +
                "<th>Implementation</th>" +
                "</tr>");

            foreach (var item in services)
            {
                str.Append("<tr>");

                str.Append($"<td>{item.ServiceType.Name}</td>");
                str.Append($"<td>{item.Lifetime}</td>");
                str.Append($"<td>{item.ImplementationType?.Name}</td>");
                str.Append("</tr>");

            }
            str.Append("</table>");

            str.Append("<style>" +
                "table { border-collapse:collapse;}" +
                "th, td { padding:10px; padding-right:25px; border-bottom:1px solid lightgray; }" +
                "</style>");

            return str.ToString();
        }
    }
}
