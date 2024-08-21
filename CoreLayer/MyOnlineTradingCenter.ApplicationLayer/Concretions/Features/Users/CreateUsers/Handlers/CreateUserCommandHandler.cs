using MediatR;
using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.CreateUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.CreateUsers.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, Response<CreateUserCommandResponseDto>>
{
    private readonly IUserService _userService;
    public CreateUserCommandHandler(IUserService userService) => _userService = userService;

    public async Task<Response<CreateUserCommandResponseDto>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        Response<CreateUserCommandResponseDto> responseDto = await _userService.CreateUserAsync(new CreateUserCommandRequestDto
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
        });
        return responseDto;
    }
}

#region CreateUserCommandResponse with return
//public async Task<Response<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
//{
//    var responseDto = await _userService.CreateUserAsync(new CreateUserCommandRequestDto
//    {
//        FirstName = request.FirstName,
//        LastName = request.LastName,
//        UserName = request.UserName,
//        Email = request.Email,
//        Password = request.Password,
//    });

//    var responseData = new CreateUserCommandResponse()
//    {
//        UserName = responseDto.Data.UserName,
//        Password = responseDto.Data.Password,
//    };

//    return new Response<CreateUserCommandResponse>(isSuccessful: true, message: "success", statusCode: 200, errors: null, data: responseData);
//}
#endregion