using Microsoft.AspNetCore.Http;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IProductService
{
    Task<bool> CreateProductAsync(CreateProductDto productDto);
    Task<DeleteProductDto?> DeleteProductAsync(DeleteProductDto productDto);
    Task<UpdateProductDto?> UpdateProductAsync(UpdateProductDto productDto);
}
