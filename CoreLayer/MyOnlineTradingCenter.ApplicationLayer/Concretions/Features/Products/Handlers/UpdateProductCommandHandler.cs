using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Update;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;
using System.Net;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, IDataResult<UpdateProductCommandResponse?>>
{
    private readonly IProductService _productService;
    private readonly IProductReadRepository _productReadRepository;

    public UpdateProductCommandHandler(IProductService productService, IProductReadRepository productReadRepository)
    {
        _productService = productService;
        _productReadRepository = productReadRepository;
    }

    public async Task<IDataResult<UpdateProductCommandResponse?>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        UpdateProductDto? updateProductDto = await _productService.UpdateProductAsync(request.UpdateProductDto);
        var response = new UpdateProductCommandResponse { Id = request.UpdateProductDto.Id };

        if (updateProductDto == null)
        {
            if (await _productReadRepository.GetByIdAsync(request.UpdateProductDto.Id) == null)
            {
                return new ErrorDataResult<UpdateProductCommandResponse>("Product update failed. The product does not exist.", HttpStatusCode.NotFound);
            }
            return new SuccessDataResult<UpdateProductCommandResponse>(response, "No changes were made as the provided data was identical to the current data.", HttpStatusCode.NotModified);
        }
        return new SuccessDataResult<UpdateProductCommandResponse>(response, "Product update completed successfully.", HttpStatusCode.OK);
    }
}
