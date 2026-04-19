using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.GetById;

public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
{
    public string Id { get; set; }
}
