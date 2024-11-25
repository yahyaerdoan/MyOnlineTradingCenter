using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.VerifyResetTokens.Commands.Create;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;
using System.Net;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.VerifyResetTokens.Handlers;

public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, IDataResult<VerifyResetTokenCommandResponse?>>
{
    private readonly IAuthService _authService;

    public VerifyResetTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<IDataResult<VerifyResetTokenCommandResponse?>> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
    {
        Boolean verifyResetTokenState = await _authService.VerifyResetTokenAsync(request.ResetToken, request.UserId);
        if (!verifyResetTokenState)
        {
            return new ErrorDataResult<VerifyResetTokenCommandResponse>("The reset token is invalid or has expired. Please request a new reset token.", HttpStatusCode.BadRequest);
        }
        var verifyResetTokenStateResponse = new VerifyResetTokenCommandResponse { VerifyResetTokenState = verifyResetTokenState };
        return new SuccessDataResult<VerifyResetTokenCommandResponse>(verifyResetTokenStateResponse, "The reset token has been successfully verified.", HttpStatusCode.OK);
    }
}
