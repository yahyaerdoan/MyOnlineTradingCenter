using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.CompletedOrders.Commands.Create;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;
using System.Net;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.CompletedOrders.Handlers;

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, IResult>
{
    private readonly ICompletedOrderService _completedOrderService;

    public CompleteOrderCommandHandler(ICompletedOrderService completedOrderService)
    {
        _completedOrderService = completedOrderService;
    }

    public async Task<IResult> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.OrderId))
            return new ErrorResult("Order ID is required.", HttpStatusCode.BadRequest);

        bool completedOrder = await _completedOrderService.CompleteOrderAsync(request.OrderId);

        if (!completedOrder)
            return new ErrorResult("Failed to complete the order. Please check the order ID.", HttpStatusCode.BadRequest);

        return new SuccessResult("Order marked as completed successfully.", HttpStatusCode.Created);
    }

}
