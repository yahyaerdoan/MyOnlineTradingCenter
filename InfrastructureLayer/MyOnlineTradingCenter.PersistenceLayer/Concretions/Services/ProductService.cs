﻿using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.PersistenceLayer.Concretions.Repositories.ProductRepository;
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
        var removedResult = await _productWriteRepository.RemoveByIdAsync(productDto.Id);
        var savedResult = await _productWriteRepository.SaveAsync();
        if (removedResult)
        {
            return new DeleteProductDto
            {
                Id = productDto.Id
            };
        }
        return null;
    }

    public async Task<UpdateProductDto?> UpdateProductAsync(UpdateProductDto productDto)
    {
        Product? product = await _productReadRepository.GetByIdAsync(productDto.Id);

        if (product == null)
        {
            return null;
        }

        if (IsProductDataUnchanged(product, productDto))
        {
            return null;
        }

        ApplyProductUpdates(product, productDto);

        var savedResult = await _productWriteRepository.SaveAsync();

        if (savedResult == 0)
        {
            return null;
        }

        return UpdatedProductDto(productDto, product);
    }

    private static void ApplyProductUpdates(Product product, UpdateProductDto productDto)
    {
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Stock = productDto.Stock;
        product.Price = productDto.Price;
    }

    private static UpdateProductDto? UpdatedProductDto(UpdateProductDto productDto, Product product)
    {
        return new UpdateProductDto
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Description = productDto.Description,
            Stock = productDto.Stock,
            Price = productDto.Price
        };
    }

    private static bool IsProductDataUnchanged(Product product, UpdateProductDto productDto)
    {
        return product.Name == productDto.Name &&
                   product.Description == productDto.Description &&
                   product.Stock == productDto.Stock &&
                   product.Price == productDto.Price;
    }
}
#region MyRegion
//Product? product = await _productReadRepository.GetByIdAsync(productDto.Id);
//if (product == null)
//{
//    return null;
//}
//product.Name = productDto.Name;
//product.Description = productDto.Description;
//product.Stock = productDto.Stock;
//product.Price = productDto.Price;

//var savedResult = await _productWriteRepository.SaveAsync();
//if(savedResult == 0)
//{
//    return null;
//}

//if (savedResult > 0)
//{
//    return new UpdateProductDto
//    {
//        Id = productDto.Id,
//        Name = product.Name,
//        Description = product.Description,
//        Stock = product.Stock,
//        Price = product.Price
//    };
//}
//return null;

#endregion