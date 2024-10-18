using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Create;

public class CreateProductCommandRequest : IRequest<Response<CreateProductCommandResponse>>
{
    public CreateProductDto CreateProductDto { get; set; }

    public CreateProductCommandRequest(CreateProductDto createProductDto)
    {
        CreateProductDto = createProductDto;
    }
}
