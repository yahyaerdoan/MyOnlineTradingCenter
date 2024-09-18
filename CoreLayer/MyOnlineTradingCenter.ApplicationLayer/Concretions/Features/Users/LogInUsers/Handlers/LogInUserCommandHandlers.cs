using MediatR;
using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.CreateUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Handlers;

public class LogInUserCommandHandlers : IRequestHandler<LogInUserCommandRequest, Response<LogInUserCommandResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    private readonly IAuthService _authService;

    public LogInUserCommandHandlers(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler, IAuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
        _authService = authService;
    }

    public async Task<Response<LogInUserCommandResponse>> Handle(LogInUserCommandRequest request, CancellationToken cancellationToken)
    {
        Response<LogInUserCommandResponse> response = await _authService.SystemLogInAsync(request);
        return response;
    }
    #region Code refactored.
    //User user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
    //if (user == null)
    //    user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);

    //var response = new LogInUserCommandResponse();
    //if (user == null)
    //{
    //    response.Succeeded = false;
    //    response.Message = "Invalid credentials!";
    //    return response;
    //}

    //var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
    //if (result.Succeeded)
    //{
    //    Token token = _tokenHandler.CreateAccessToken(5);
    //    response.Token = token;
    //    response.Succeeded = true;
    //    response.Message = "User logged in successfully!";
    //}
    //else
    //    response.Message = "Invalid credentials!";

    //return response; 
    #endregion
}