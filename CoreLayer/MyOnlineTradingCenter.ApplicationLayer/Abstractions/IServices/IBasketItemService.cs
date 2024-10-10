using MyOnlineTradingCenter.ApplicationLayer.Concretions.ViewModels.BasketItems;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IBasketItemService
{
    Task<BasketItem> AddBasketItemAsync(CreateBasketItemViewModel model);  // Adds a new product to the basket //in
    Task<BasketItem> UpdateBasketItemQuantityAsync(UpdateBasketItemViewModel model);  // Updates the quantity of a specific basket item //in
    Task<bool> RemoveBasketItemAsync(string basketItemId);                             // Removes an item from the basket //in
    Task<List<BasketItem>> GetBasketItemsAsync();  //string basketId              // Retrieves all items in a specific basket //in
}
