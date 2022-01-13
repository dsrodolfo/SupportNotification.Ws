using SupportNotification.Ws.Interfaces;
using System.Text;

namespace SupportNotification.Ws.Services
{
    public class TemplateService : ITemplateService
    {
        public string GetDefaultTemplate()
        {
            StringBuilder template = new();
            template.AppendLine("Dear Employee,");
            template.AppendLine("<p><b>Customer Name:</b> @Model.CustomerName</p>");
            template.AppendLine("<p><b>Product:</b> @Model.ProductName</p>");
            template.AppendLine("<p><b>Description:</b> @Model.Description </p>");
            template.AppendLine("<br>");
            template.AppendLine("<br>");
            template.AppendLine(">>>>>>> Sent from Support Notification Service <<<<<<<");

            return template.ToString();
        }
    }
}