using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.RefreshTokenLogIns.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices.IAuthentications;

public interface IInternalAuthentication
{
    Task<Response<RefreshTokenLogInCommandResponse>> RefreshTokenLogInAsync(RefreshTokenLogInCommandRequest request);
    Task<Response<LogInUserCommandResponse>> SystemLogInAsync(LogInUserCommandRequest request);
}
