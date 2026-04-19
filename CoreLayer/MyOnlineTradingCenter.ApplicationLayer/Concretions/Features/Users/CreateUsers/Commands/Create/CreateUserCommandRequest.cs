using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.CreateUsers.Commands.Create;

public class CreateUserCommandRequest : IRequest<Response<CreateUserCommandResponseDto>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
