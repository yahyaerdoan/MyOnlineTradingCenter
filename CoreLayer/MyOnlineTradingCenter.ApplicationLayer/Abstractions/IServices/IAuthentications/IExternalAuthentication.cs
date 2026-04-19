using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices.IAuthentications;

public interface IExternalAuthentication
{
    Task<Response<GoogleLogInUserCommandResponse>> GoogleLogInAsync(GoogleLogInUserCommandRequest request);
    Task FacebookLogInAsync();
    Task InstagramLogInAsync();
    Task GithubLogInAsync();
    Task LinkedInLogInAsync();
}
