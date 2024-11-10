using Microsoft.Extensions.Configuration;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(EmailMessage message)
    {
        var email = _configuration.GetValue<string>("EmailConfiguration:Email");
        var password = _configuration.GetValue<string>("EmailConfiguration:Password");
        var host = _configuration.GetValue<string>("EmailConfiguration:Host");
        var port = _configuration.GetValue<int>("EmailConfiguration:Port");
        var displayName = _configuration.GetValue<string>("EmailConfiguration:DisplayName");

        if (!string.IsNullOrEmpty(message.ToSingle))
        {
            await SendIndividualEmailAsync(message.ToSingle, message.Subject, message.Body, message.IsBodyHtml, email, password, host, port, displayName);
        }
        else
        {
            foreach (var recipient in message.ToMultiple)
            {
                await SendIndividualEmailAsync(recipient, message.Subject, message.Body, message.IsBodyHtml, email, password, host, port, displayName);
            }
        }
    }

    private static async Task SendIndividualEmailAsync(string recipient, string subject, string body, bool isBodyHtml, 
        string email, string password, string host, int port, string displayName)
    {
        using var mailMessage = new MailMessage
        {
            IsBodyHtml = isBodyHtml,
            Subject = subject,
            Body = body,
            From = new MailAddress(email, displayName, Encoding.UTF8)
        };

        mailMessage.To.Add(recipient);

        using var smtpClient = new SmtpClient(host, port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(email, password)
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}