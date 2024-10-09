using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class BasketItemService : IBasketService
{
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

    public Task<Basket> GetBasketAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Basket> UpdateBasketAsync(Basket basket)
    {
        throw new NotImplementedException();
    }
}
