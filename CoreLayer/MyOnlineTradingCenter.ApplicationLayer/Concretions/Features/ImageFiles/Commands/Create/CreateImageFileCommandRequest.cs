using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Create;

public class CreateImageFileCommandRequest : IRequest<CreateImageFileCommandResponse>
{
    public string Id { get; set; }
    public IFormFileCollection? Files { get; set; }
}
