using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Queries.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class GetImageFileQueryHandler : IRequestHandler<GetImageFileQueryRequest, GetImageFileQueryResponse>
{
    public Task<GetImageFileQueryResponse> Handle(GetImageFileQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
