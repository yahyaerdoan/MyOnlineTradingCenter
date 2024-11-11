using Microsoft.Extensions.Configuration;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using System.Text;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.EmailTemplates;

public class PasswordResetTemplate : IEmailTemplate
{
    private readonly IConfiguration _configuration;

    public PasswordResetTemplate(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateBody(Dictionary<string, string> templateData)
    {
        var clientOrigin = _configuration.GetValue<string>("ClientOrigins:AngularClientOrigin");
        var resetPasswordPath = "update-password";
        var userId = templateData.GetValueOrDefault("UserId", "");
        var resetToken = templateData.GetValueOrDefault("ResetToken", "");

        var resetPasswordUrl = $"{clientOrigin}/{resetPasswordPath}/{userId}/{resetToken}";

        var mail = new StringBuilder();
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'>Hello,</p>");
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'>We received a request to reset your password. " +
            "If you made this request, please click the link below to reset your password:</p>");
        mail.AppendLine($"<p style='font-family: Arial, sans-serif;'><a href='{resetPasswordUrl}' style='color: #4CAF50; font-size: 16px;'>Reset Your Password</a></p>");
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px; color: #555;'>If you did not request a password reset, please ignore this message. " +
            "Your password will remain the same, and no changes will be made.</p>");
        mail.AppendLine("<br>");
        mail.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 14px;'>Best regards,<br>The My Online Trading Center Team</p>");

        return mail.ToString();
    }

    public string GetSubject()
    {
        return "Password Reset Request";
    }
}
