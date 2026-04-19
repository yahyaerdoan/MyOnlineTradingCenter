using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Queries.Get;

public class GetImageFileQueryRequest : IRequest<List<GetImageFileQueryResponse>>
{
    public string Id { get; set; }
}
