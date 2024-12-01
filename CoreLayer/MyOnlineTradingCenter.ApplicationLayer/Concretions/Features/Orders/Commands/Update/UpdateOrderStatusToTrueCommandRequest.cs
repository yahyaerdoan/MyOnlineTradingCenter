using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Update;

public class UpdateOrderStatusToTrueCommandRequest : IRequest<UpdateOrderStatusToTrueCommandResponse>
{
    public UpdateOrderStatusDto UpdateOrderStatusDto { get; set; }

    public UpdateOrderStatusToTrueCommandRequest(UpdateOrderStatusDto updateOrderStatusDto)
    {
        UpdateOrderStatusDto = updateOrderStatusDto;
    }
}
