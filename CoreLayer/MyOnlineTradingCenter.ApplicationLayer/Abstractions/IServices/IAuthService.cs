using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices.IAuthentications;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IAuthService : IInternalAuthentication, IExternalAuthentication
{
    Task ResetPasswordAsync(string email);
}
