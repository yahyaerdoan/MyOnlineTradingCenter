using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Delete;

public class DeleteProductCommandRequest : IRequest<IResult>
{
    public DeleteProductDto DeleteProductDto { get; set; }
    public DeleteProductCommandRequest(DeleteProductDto deleteProductDto)
    {
        DeleteProductDto = deleteProductDto;
    }
}
