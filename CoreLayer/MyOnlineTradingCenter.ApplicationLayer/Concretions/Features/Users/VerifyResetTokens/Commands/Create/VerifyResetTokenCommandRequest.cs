using MediatR;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.VerifyResetTokens.Commands.Create;

public class VerifyResetTokenCommandRequest : IRequest<IDataResult<VerifyResetTokenCommandResponse?>>
{
    public string ResetToken { get; set; } = default!;
    public string UserId { get; set; } = default!;
}
