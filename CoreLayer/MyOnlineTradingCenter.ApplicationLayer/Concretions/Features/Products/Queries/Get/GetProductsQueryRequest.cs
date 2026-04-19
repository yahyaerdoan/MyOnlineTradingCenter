using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.Get;

public class GetProductsQueryRequest : IRequest<GetProductsQueryResponse>
{
    public Pagination Pagination { get; set; }

    public GetProductsQueryRequest()
    {
        Pagination = new Pagination();
    }
    public GetProductsQueryRequest(Pagination pagination)
    {
        Pagination = pagination ?? new Pagination();
    }
}
