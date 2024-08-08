using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class CreateImageFileCommandHandler : IRequestHandler<CreateImageFileCommandRequest, CreateImageFileCommandResponse>
{
    public Task<CreateImageFileCommandResponse> Handle(CreateImageFileCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
