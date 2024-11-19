using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.UpdatePasswords.Commands.Update;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;
using System.Net;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.UpdatePasswords.Handlers;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, IResult>
{
    private readonly IAuthService _authService;

    public UpdatePasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<IResult> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (request == null || request.UpdatePasswordDto == null)
        {
            return new ErrorResult("Invalid request or data.", HttpStatusCode.BadRequest);
        }

        var isUpdated = await _authService.UpdatePasswordAsync(request.UpdatePasswordDto.UserId, request.UpdatePasswordDto.Email, request.UpdatePasswordDto.ResetToken, request.UpdatePasswordDto.Password);

        if (!isUpdated)
        {
            return new ErrorResult("Failed to update password.", HttpStatusCode.BadRequest);
        }

        return new SuccessResult("Password updated successfully.", HttpStatusCode.OK);
    }
}
