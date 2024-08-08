using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class DeleteImageFileCommandHandler : IRequestHandler<CreateImageFileCommandRequest, DeleteImageFileCommandResponse>
{
    public Task<DeleteImageFileCommandResponse> Handle(CreateImageFileCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
