using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class UpdateImageShowcaseCommandHandler : IRequestHandler<UpdateImageShowcaseCommandRequest, Response<UpdateImageShowcaseCommandResponse>>
{
    private readonly IImageFileService _imageFileService;

    public UpdateImageShowcaseCommandHandler(IImageFileService imageFileService)
    {
        _imageFileService = imageFileService;
    }

    public async Task<Response<UpdateImageShowcaseCommandResponse>> Handle(UpdateImageShowcaseCommandRequest request, CancellationToken cancellationToken)
    {
        Response<UpdateImageShowcaseCommandResponse> response = await _imageFileService.ChangeImageShowcaseStatusAsync(request);
        return response;
    }
}
