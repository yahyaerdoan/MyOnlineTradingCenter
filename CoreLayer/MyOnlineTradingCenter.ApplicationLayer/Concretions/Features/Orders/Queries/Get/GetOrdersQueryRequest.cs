using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.Get;

public class GetOrdersQueryRequest : IRequest<IDataResult<GetOrdersQueryResponse?>>
{
    public Pagination Pagination { get; set; } = default!;

    public GetOrdersQueryRequest(Pagination pagination)
    {
        Pagination = pagination;
    }
}