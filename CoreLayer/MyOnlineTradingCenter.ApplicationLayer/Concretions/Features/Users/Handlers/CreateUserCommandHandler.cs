using MediatR;
using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.Commands.Create;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
       IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            FirtName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
        },  request.Password);

        CreateUserCommandResponse response = new() { Succeeded = result.Succeeded};
        if (result.Succeeded)
            response.Message = "User created!";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}\n";
        return response;           
    }
}
