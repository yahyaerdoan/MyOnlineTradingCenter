using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.ResetPasswords.Commands.Create;

public class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResponse>
{
    public string Email { get; set; } = default!;
}
