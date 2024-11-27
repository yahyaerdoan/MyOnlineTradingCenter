using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.CompletedOrders;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.CompletedOrders.Commands.Create;

public class CompleteOrderCommandRequest : IRequest<IResult>
{
    public CompleteOrderDto CompleteOrderDto { get; set; }

    public CompleteOrderCommandRequest(CompleteOrderDto completeOrderDto)
    {
        CompleteOrderDto = completeOrderDto;
    }
}
