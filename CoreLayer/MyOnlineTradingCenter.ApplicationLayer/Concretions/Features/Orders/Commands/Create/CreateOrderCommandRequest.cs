using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Create;

public class CreateOrderCommandRequest : IRequest<IResult>
{
    public CreateOrderDto CreateOrderDto { get; set; }

    public CreateOrderCommandRequest(CreateOrderDto createOrderDto)
    {
        CreateOrderDto = createOrderDto;
    }
}
