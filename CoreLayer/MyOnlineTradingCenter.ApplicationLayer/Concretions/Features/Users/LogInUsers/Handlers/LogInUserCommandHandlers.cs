using MediatR;
using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.CreateUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;
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

    public LogInUserCommandHandlers(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LogInUserCommandResponse> Handle(LogInUserCommandRequest request, CancellationToken cancellationToken)
    {
       User user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
        if (user == null)
           user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);

        LogInUserCommandResponse response = new() { Succeeded = false};
        if (user == null)
            response.Message = "User name or email or password wrong!.";            

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded) 
        {
            response.Succeeded = true;
            response.Message = "User logged in successfully!";
        }
        else
            response.Message = "Invalid credentials.";

        return response;
    }
}
