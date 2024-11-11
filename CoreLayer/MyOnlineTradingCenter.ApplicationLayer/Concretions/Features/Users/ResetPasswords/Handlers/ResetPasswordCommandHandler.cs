using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.ResetPasswords.Commands.Create;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.ResetPasswords.Handlers;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
{
    private readonly IAuthService _authService;

    public ResetPasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ResetPasswordCommandResponse();
        await _authService.ResetPasswordAsync(request.Email);
        return response;
    }
}
