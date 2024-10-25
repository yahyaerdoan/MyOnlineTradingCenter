using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IBasketRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.BasketItems;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Baskets;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class BasketService : IBasketService
{
    private readonly IBasketReadRepository _basketReadRepository;

    public BasketService(IBasketReadRepository basketReadRepository)
    {
        _basketReadRepository = basketReadRepository;
    }

    public Task<bool> ClearBasketAsync(string basketId)
    {
        throw new NotImplementedException();
    }

    public Task<Basket> CreateBasketAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBasketAsync(string basketId)
    {
        throw new NotImplementedException();
    }

    public async Task<BasketDto> GetBasketByUserIdAsync(string userId)
    {
        Basket? basket = await _basketReadRepository.Table.Include(b => b.BasketItems)
                              .ThenInclude(i => i.Product)
                              .FirstOrDefaultAsync(b => b.UserId == userId);
        if (basket == null)
        {
            return null!;
        }

        var basketDto = new BasketDto
        {
            UserId = basket.UserId,
            Items = basket.BasketItems.Select(item => new BasketItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                PriceAtTimeOfAddition = item.Product.Price,
            }).ToList()
        };
        return basketDto;
    }

    public Task<Basket> UpdateBasketAsync(Basket basket)
    {
        throw new NotImplementedException();
    }
}
