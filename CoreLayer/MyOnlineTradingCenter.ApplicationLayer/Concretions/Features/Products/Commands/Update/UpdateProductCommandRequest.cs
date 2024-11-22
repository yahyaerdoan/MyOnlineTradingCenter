using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using ResultHandler.Interfaces.Contracts;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Update;

public class UpdateProductCommandRequest : IRequest<IDataResult<UpdateProductCommandResponse?>>
{
    public UpdateProductDto UpdateProductDto { get; set; }

    public UpdateProductCommandRequest(UpdateProductDto updateProductDto)
    {
        UpdateProductDto = updateProductDto;
    }
}
