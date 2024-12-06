using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using System.Text;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.EmailTemplates;

public class CompletedOrderTemplate : IEmailTemplate
{
    public string GenerateBody(Dictionary<string, string> templateData)
    {
        var orderNumber = templateData.GetValueOrDefault("OrderNumber", "");
        var orderDate = templateData.GetValueOrDefault("OrderDate", "");
        var userFullName = templateData.GetValueOrDefault("UserFullName", "");

        var mail = new StringBuilder();
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'>Hello " + userFullName + ",</p>");
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'>Thank you for your order! Your order has been successfully completed. Below are the details:</p>");
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'><strong>Order Number:</strong> " + orderNumber + "</p>");
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'><strong>Order Date:</strong> " + orderDate + "</p>");
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'>You can view more details by logging into your account on our platform.</p>");
        mail.AppendLine("<br>");
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'>Best regards,<br>The My Online Trading Center Team</p>");

        return mail.ToString();
    }

    public string GetSubject()
    {
        return "Your Order is Complete!";
    }
}
