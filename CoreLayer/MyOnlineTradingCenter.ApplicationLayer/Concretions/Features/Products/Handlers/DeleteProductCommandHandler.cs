using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Delete;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using ResultHandler.Implementations.ErrorResults;
using ResultHandler.Implementations.SuccessResults;
using ResultHandler.Interfaces.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, IResult>
{
    private readonly IProductService _productService;

    public DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IResult> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        DeleteProductDto? deleteProductDto = await _productService.DeleteProductAsync(request.DeleteProductDto);
        if (deleteProductDto is null)
        {
            return new ErrorResult("Product deletion failed.", HttpStatusCode.NotFound);
        }        
        return new SuccessResult("Product deletion completed successfully.", HttpStatusCode.OK);
    }
}
