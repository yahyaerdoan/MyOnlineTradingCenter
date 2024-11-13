using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.VerifyResetTokens.Commands.Create;

public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
{
    public string ResetToken { get; set; }
    public string UserId { get; set; }
}
