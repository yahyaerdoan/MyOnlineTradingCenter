﻿using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IBasketService
{
    Task<Basket> CreateBasketAsync(string userId);  // Creates a new basket for a specified user
    Task<Basket> GetBasketAsync(string userId);     // Retrieves the current basket for a user
    Task<Basket> UpdateBasketAsync(Basket basket);  // Updates a basket
    Task<bool> DeleteBasketAsync(string basketId);  // Deletes a basket
    Task<bool> ClearBasketAsync(string basketId);   // Clears all items from a basket without deleting it
}