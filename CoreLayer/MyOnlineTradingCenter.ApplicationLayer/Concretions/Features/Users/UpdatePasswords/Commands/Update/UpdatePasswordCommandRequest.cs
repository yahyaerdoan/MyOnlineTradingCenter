using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.UpdatePasswords.Commands.Update;

public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
{
    public UpdatePasswordDto UpdatePasswordDto { get; set; } = new();
}
