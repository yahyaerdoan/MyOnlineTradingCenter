using MediatR;
using Microsoft.Extensions.Logging;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.RefreshTokenLogIns.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.RefreshTokenLogIns.Handlers;

public class RefreshTokenLogInCommandHandler : IRequestHandler<RefreshTokenLogInCommandRequest, Response<RefreshTokenLogInCommandResponse>>
{
    private readonly IAuthService _authService;
    private readonly ILogger<RefreshTokenLogInCommandHandler> _logger;

    public RefreshTokenLogInCommandHandler(IAuthService authService, ILogger<RefreshTokenLogInCommandHandler> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<Response<RefreshTokenLogInCommandResponse>> Handle(RefreshTokenLogInCommandRequest request, CancellationToken cancellationToken)
    {
        Response<RefreshTokenLogInCommandResponse> response = await _authService.RefreshTokenLogInAsync(request);
        _logger.LogInformation("Refresh token has been created!");
        return response;
    }
}
