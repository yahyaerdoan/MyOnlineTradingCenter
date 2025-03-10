using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IProductService
{
    Task<bool> CreateProductAsync(CreateProductDto productDto);
    Task<DeleteProductDto?> DeleteProductAsync(DeleteProductDto productDto);
    Task<UpdateProductDto?> UpdateProductAsync(UpdateProductDto productDto);
}
