using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class UpdateImageFileCommandHandler : IRequestHandler<UpdateImageFileCommandRequest, UpdateImageFileCommandResponse>
{
    public Task<UpdateImageFileCommandResponse> Handle(UpdateImageFileCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
