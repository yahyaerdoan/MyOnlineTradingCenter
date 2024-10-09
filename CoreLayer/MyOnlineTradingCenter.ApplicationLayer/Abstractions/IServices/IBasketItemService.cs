using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IBasketItemService
{
    Task<BasketItem> AddItemAsync(string basketId, int productId, int quantity);  // Adds a new product to the basket //in
    Task<BasketItem> UpdateItemQuantityAsync(string basketItemId, int quantity);  // Updates the quantity of a specific basket item //in
    Task<bool> RemoveItemAsync(string basketItemId);                             // Removes an item from the basket //in
    Task<List<BasketItem>> GetItemsAsync(string basketId);                // Retrieves all items in a specific basket //in
}
