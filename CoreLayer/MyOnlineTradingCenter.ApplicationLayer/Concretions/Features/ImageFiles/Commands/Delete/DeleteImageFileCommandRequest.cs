using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Delete;

public class DeleteImageFileCommandRequest : IRequest<DeleteImageFileCommandResponse>
{
    public string Id { get; set; }
    public string? ImageId { get; set; }
}
