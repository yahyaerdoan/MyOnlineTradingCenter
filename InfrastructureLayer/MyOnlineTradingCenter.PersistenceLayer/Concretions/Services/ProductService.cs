using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class ProductService : IProductService
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;

    public ProductService(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task<bool> CreateProductAsync(CreateProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Stock = productDto.Stock,
            Price = productDto.Price
        };
        var addResult = await _productWriteRepository.AddAsync(product);
        var saveResult = await _productWriteRepository.SaveAsync();
        return saveResult > 0;
    }

    public async Task<DeleteProductDto?> DeleteProductAsync(DeleteProductDto productDto)
    {

        Product product = await _productReadRepository.GetSingleAsync(x => x.Id == Guid.Parse(productDto.Id));
        if (product == null)
        {
            return null;
        }
        var removedResult = await _productWriteRepository.RemoveByIdAsync(productDto.Id!);
        var savedResult = await _productWriteRepository.SaveAsync();
        if (removedResult)
        {
            return new DeleteProductDto
            {
                Id = productDto.Id!
            };
        }
        return null;
    }
}
