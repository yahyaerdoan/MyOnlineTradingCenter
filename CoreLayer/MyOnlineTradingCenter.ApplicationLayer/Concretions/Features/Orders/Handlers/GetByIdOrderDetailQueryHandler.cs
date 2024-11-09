using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.GetByIdDetail;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Handlers;

public class GetByIdOrderDetailQueryHandler : IRequestHandler<GetByIdOrderDetailQueryRequest, IDataResult<GetByIdOrderDetailQueryResponse?>>
{
    private readonly IOrderService _orderService;

    public GetByIdOrderDetailQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IDataResult<GetByIdOrderDetailQueryResponse?>> Handle(GetByIdOrderDetailQueryRequest request, CancellationToken cancellationToken)
    {
        OrderDetailDto orderDetailDto = await _orderService.GetByIdOrderDetailAsync(request.Id);
        if (orderDetailDto == null)
        {
            return new ErrorDataResult<GetByIdOrderDetailQueryResponse?>();
        }
        var response = new GetByIdOrderDetailQueryResponse { OrderDetailDto = orderDetailDto };
        return new SuccessDataResult<GetByIdOrderDetailQueryResponse?>(response);
    }
}
