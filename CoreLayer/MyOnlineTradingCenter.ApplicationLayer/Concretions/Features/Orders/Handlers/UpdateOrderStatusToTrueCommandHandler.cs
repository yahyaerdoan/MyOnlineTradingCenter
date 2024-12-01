using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Update;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Handlers;

public class UpdateOrderStatusToTrueCommandHandler : IRequestHandler<UpdateOrderStatusToTrueCommandRequest, UpdateOrderStatusToTrueCommandResponse>
{
    private readonly IOrderService _orderService;

    public UpdateOrderStatusToTrueCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<UpdateOrderStatusToTrueCommandResponse> Handle(UpdateOrderStatusToTrueCommandRequest request, CancellationToken cancellationToken)
    {
        bool orderStatus = await _orderService.UpdateOrderStatusToTrueAsync(Guid.Parse(request.UpdateOrderStatusDto.OrderId));
        if (orderStatus == false) { }
        return new UpdateOrderStatusToTrueCommandResponse { };

    }
}
