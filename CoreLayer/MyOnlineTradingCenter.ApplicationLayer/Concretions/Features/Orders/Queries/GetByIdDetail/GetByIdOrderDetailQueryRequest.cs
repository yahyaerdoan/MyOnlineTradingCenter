using MediatR;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.GetByIdDetail;

public class GetByIdOrderDetailQueryRequest : IRequest<IDataResult<GetByIdOrderDetailQueryResponse?>>
{
    public Guid Id { get; set; }
    public GetByIdOrderDetailQueryRequest() { }
    public GetByIdOrderDetailQueryRequest(Guid id)
    {
        Id = id;
    }   
}
