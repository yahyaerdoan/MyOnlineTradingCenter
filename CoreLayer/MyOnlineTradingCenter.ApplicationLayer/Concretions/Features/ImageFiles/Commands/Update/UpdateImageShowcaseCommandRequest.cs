using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;

public class UpdateImageShowcaseCommandRequest : IRequest<Response<UpdateImageShowcaseCommandResponse>>
{
    public string ProductId { get; set; }
    public string ImageId { get; set; }
}
