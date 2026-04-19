using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;

public class UpdateImageShowcaseCommandRequest : IRequest<Response<UpdateImageShowcaseCommandResponse>>
{
    public string? ProductId { get; set; }
    public string? ImageId { get; set; }
}
