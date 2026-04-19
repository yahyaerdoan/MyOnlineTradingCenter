using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.RefreshTokenLogIns.Commands.Create;

public class RefreshTokenLogInCommandRequest : IRequest<Response<RefreshTokenLogInCommandResponse>>
{
    public string RefreshToken { get; set; }
}
