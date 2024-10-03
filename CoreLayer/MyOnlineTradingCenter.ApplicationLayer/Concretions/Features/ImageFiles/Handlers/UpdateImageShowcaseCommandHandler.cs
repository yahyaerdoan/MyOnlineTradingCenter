using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class UpdateImageShowcaseCommandHandler : IRequestHandler<UpdateImageShowcaseCommandRequest, Response<UpdateImageShowcaseCommandResponse>>
{
    public Task<Response<UpdateImageShowcaseCommandResponse>> Handle(UpdateImageShowcaseCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
