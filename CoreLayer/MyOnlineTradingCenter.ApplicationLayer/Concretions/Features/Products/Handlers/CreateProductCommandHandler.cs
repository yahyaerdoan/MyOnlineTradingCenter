using MediatR;
using Microsoft.AspNetCore.Http;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IHubs;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.ViewModels.Products;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Response<CreateProductCommandResponse>>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductHubService _productHubService;

    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService, IProductService productService)
    {
        _productWriteRepository = productWriteRepository;
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
        await _productHubService.ProductAddedMessageAsync("added!");
        return Response<CreateProductCommandResponse>.Success(new CreateProductCommandResponse(), "Product created successfully.", StatusCodes.Status201Created);
    }

    #region CreateProductHandlerOldVersion
    //public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    //{
    //    await _productWriteRepository.AddAsync(new Product()
    //    {
    //        Name = request.Name,
    //        Description = request.Description,
    //        Stock = request.Stock,
    //        Price = request.Price,
    //    });
    //    await _productWriteRepository.SaveAsync();
    //    await _productHubService.ProductAddedMessageAsync($"{request.Name} added!");
    //    return Unit.Value;
    //}
    #endregion
}
