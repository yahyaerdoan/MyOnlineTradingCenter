namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IEmailService 
{
    Task SendEmailAsync(EmailMessage message);
}

public class EmailMessage
{
    public string[] ToMultiple { get; set; } = Array.Empty<string>();
    public string ToSingle { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsBodyHtml { get; set; } = true;
}
