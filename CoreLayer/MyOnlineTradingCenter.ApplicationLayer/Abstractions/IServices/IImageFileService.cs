using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IImageFileService
{
    Task<Response<UpdateImageShowcaseCommandResponse>> ChangeImageShowcaseStatusAsync(UpdateImageShowcaseCommandRequest request);
}
