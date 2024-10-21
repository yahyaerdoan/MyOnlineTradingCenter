using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using ResultHandler.Interfaces.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Delete;

public class DeleteProductCommandRequest : IRequest<IDataResult<DeleteProductCommandResponse>>
{
    public DeleteProductDto DeleteProductDto { get; set; }
    public DeleteProductCommandRequest(DeleteProductDto deleteProductDto)
    {
        DeleteProductDto = deleteProductDto;
    }
}
