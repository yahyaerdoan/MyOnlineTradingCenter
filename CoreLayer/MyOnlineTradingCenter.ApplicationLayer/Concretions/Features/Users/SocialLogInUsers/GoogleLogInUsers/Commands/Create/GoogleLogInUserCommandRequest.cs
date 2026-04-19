using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;

public class GoogleLogInUserCommandRequest : IRequest<Response<GoogleLogInUserCommandResponse>>
{
    public string Id { get; set; }
    //public int AccessTokenLifeTime { get; set; } = 15;
    public string IdToken { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public string Provider { get; set; }
}
