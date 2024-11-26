using MediatR;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.CompletedOrders.Commands.Create;

public class CompleteOrderCommandRequest : IRequest<IResult>
{
    public string OrderId { get; set; } = default!;

    public CompleteOrderCommandRequest(string orderId)
    {
        OrderId = orderId;
    }
}
