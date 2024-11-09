using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.Get;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Handlers;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest, IDataResult<GetOrdersQueryResponse?>>
{
    private readonly IOrderService _orderService;

    public GetOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IDataResult<GetOrdersQueryResponse?>> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        var (totalOrderCount, orders) = await _orderService.GetOrdersAsync(request.Pagination);
        if (orders == null || !orders.Any())
        {
            return new ErrorDataResult<GetOrdersQueryResponse?>();
        }

        var response = new GetOrdersQueryResponse
        {
            TotalOrderCount = totalOrderCount,
            Orders = orders.ToList()
        };

        return new SuccessDataResult<GetOrdersQueryResponse?>(response);
    }
}
