using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Queries.GetById;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class GetByIdImageFileQueryHandler : IRequestHandler<GetByIdImageFileQueryRequest, GetByIdImageFileQueryResponse>
{
    public Task<GetByIdImageFileQueryResponse> Handle(GetByIdImageFileQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
