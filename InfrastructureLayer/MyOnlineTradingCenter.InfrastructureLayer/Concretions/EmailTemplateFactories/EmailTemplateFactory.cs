using Microsoft.Extensions.Configuration;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.InfrastructureLayer.Concretions.EmailTemplates;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.EmailTemplateFactories;

public class EmailTemplateFactory
{
    private readonly IConfiguration _configuration;

    public EmailTemplateFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEmailTemplate CreateTemaplate(EmailTemplateType templateType)
    {
        return templateType switch
        {
            EmailTemplateType.PasswordReset => new PasswordResetTemplate(_configuration),
            EmailTemplateType.ComplatedOrder => new CompletedOrderTemplate(),

            _ => throw new ArgumentException("Invalid template type")
        };
    }
}
public enum EmailTemplateType
{
    PasswordReset,
    ComplatedOrder
}
