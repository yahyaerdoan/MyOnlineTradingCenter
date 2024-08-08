using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Update;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.ViewModels.Products;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Product product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Name = request.Name;
        product.Description = request.Description;
        product.Stock = request.Stock;
        product.Price = request.Price;
        await _productWriteRepository.SaveAsync();
        return new()
        {
            Id = request.Id,
        };
    }
}
