using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.UpdatePasswords.Commands.Update;

public class UpdatePasswordCommandRequest : IRequest<IResult>
{
    public UpdatePasswordDto UpdatePasswordDto { get; set; }

    public UpdatePasswordCommandRequest(UpdatePasswordDto updatePasswordDto)
    {
        UpdatePasswordDto = updatePasswordDto;
    }
}
