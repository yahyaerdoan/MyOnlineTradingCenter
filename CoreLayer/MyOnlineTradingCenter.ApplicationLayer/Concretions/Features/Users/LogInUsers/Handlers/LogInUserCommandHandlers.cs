using MediatR;
using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.CreateUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Handlers;

public class LogInUserCommandHandlers : IRequestHandler<LogInUserCommandRequest, LogInUserCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LogInUserCommandHandlers(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LogInUserCommandResponse> Handle(LogInUserCommandRequest request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);

        var response = new LogInUserCommandResponse();
        if (user == null)
        {
            response.Succeeded = false;
            response.Message = "Invalid credentials!";
            return response;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            Token token = _tokenHandler.CreateAccessToken(5);
            response.Token = token;
            response.Succeeded = true;
            response.Message = "User logged in successfully!";
        }
        else
            response.Message = "Invalid credentials!";

        return response;
    }
}
