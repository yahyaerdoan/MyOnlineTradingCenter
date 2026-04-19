using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;

public class LogInUserCommandRequest : IRequest<Response<LogInUserCommandResponse>>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
    //public int AccessTokenLifeTime { get; set; } = 30;
}
