using MediatR;
using Microsoft.AspNetCore.Authentication;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.VerifyResetTokens.Commands.Create;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.VerifyResetTokens.Handlers;

public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse>
{
    private readonly IAuthService _authService;

    public VerifyResetTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
    {
        Boolean tokenState = await _authService.VerifyResetTokenAsync(request.ResetToken, request.UserId);
        if (!tokenState) { return null; }
        return new()
        {
            State = tokenState,
        }; 
    }
}
