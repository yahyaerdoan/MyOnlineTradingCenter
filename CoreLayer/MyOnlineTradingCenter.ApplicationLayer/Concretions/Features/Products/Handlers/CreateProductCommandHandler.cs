using MediatR;
using Microsoft.AspNetCore.Http;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Response<CreateProductCommandResponse>>
{
    private readonly IProductHubService _productHubService;
    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductHubService productHubService, IProductService productService)
    {
        _productHubService = productHubService;
        _productService = productService;
    }

    public async Task<Response<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var isSuccessful = await _productService.CreateProductAsync(request.CreateProductDto);
        if (!isSuccessful) 
        { 
            return Response<CreateProductCommandResponse>.Failure("Failed to create product", "An unexpected error occurred while saving the product to the database.", StatusCodes.Status400BadRequest);
        }
        await _productHubService.ProductAddedMessageAsync("New product created.");
        return Response<CreateProductCommandResponse>.Success(new CreateProductCommandResponse(), "Product created successfully.", StatusCodes.Status201Created);
    }
}
