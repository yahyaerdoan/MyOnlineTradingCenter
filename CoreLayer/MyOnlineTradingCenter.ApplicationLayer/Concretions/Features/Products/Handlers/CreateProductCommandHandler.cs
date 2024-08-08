using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = request.Name,
            Description = request.Description,
            Stock = request.Stock,
            Price = request.Price,
        });
        await _productWriteRepository.SaveAsync();
        return Unit.Value;
    }
}
