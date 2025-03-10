using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IImageFileService
{
    Task<Response<UpdateImageShowcaseCommandResponse>> ChangeImageShowcaseStatusAsync(UpdateImageShowcaseCommandRequest request);
}
