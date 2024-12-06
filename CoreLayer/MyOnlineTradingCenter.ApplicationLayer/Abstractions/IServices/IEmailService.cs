namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IEmailService 
{
    Task SendEmailAsync(EmailMessage message);
    Task RequestPasswordResetAsync(string to, string userId, string resetToken);
    Task SendCompletedOrderAsync(string to, string orderNumber, DateTime orderDate, string userFullName);
}

public class EmailMessage
{
    public string[] ToMultiple { get; set; } = Array.Empty<string>();
    public string ToSingle { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsBodyHtml { get; set; } = true;
}
