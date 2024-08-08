using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class GetByIdImageFileQueryHandler : IRequestHandler<GetByIdImageFileQueryRequest, GetByIdImageFileQueryResponse>
{
    public Task<GetByIdImageFileQueryResponse> Handle(GetByIdImageFileQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
