using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Delete;

public class DeleteImageFileCommandRequest : IRequest<DeleteImageFileCommandResponse>
{
    public string Id { get; set; }
    public string? ImageId { get; set; }
}
