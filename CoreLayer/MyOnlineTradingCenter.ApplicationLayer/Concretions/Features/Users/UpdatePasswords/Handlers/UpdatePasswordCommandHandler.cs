using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.UpdatePasswords.Commands.Update;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.UpdatePasswords.Handlers;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
{
    private readonly IAuthService _authService;

    public UpdatePasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (request == null || request.UpdatePasswordDto == null)
        {
            throw new ArgumentNullException(nameof(request), "Request or UpdatePasswordDto cannot be null.");

        }

        bool a = await  _authService.UpdatePasswordAsync(request.UpdatePasswordDto.UserId, request.UpdatePasswordDto.Email, request.UpdatePasswordDto.ResetToken, request.UpdatePasswordDto.Password);
        var response = new UpdatePasswordCommandResponse();
        return response;

    }
}
