using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class UpdateImageFileCommandHandler : IRequestHandler<UpdateImageFileCommandRequest, UpdateImageFileCommandResponse>
{
    public Task<UpdateImageFileCommandResponse> Handle(UpdateImageFileCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
