using MediatR;
using Microsoft.AspNetCore.Http;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Create;

public class CreateImageFileCommandRequest : IRequest<CreateImageFileCommandResponse>
{
    public string Id { get; set; }
    public IFormFileCollection? Files { get; set; }
}
