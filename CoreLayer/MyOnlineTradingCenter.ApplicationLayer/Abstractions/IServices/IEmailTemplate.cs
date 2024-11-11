namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IEmailTemplate
{
    string GenerateBody(Dictionary<string, string> templateData);
    string GetSubject();
}
