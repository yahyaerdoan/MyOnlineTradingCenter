using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IBasketItemRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IBasketRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.ViewModels.BasketItems;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class BasketItemService : IBasketItemService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    private readonly IProductReadRepository _productReadRepository;

    public BasketItemService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IOrderReadRepository orderReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository, IProductReadRepository productReadRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _orderReadRepository = orderReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
        _basketReadRepository = basketReadRepository;
        _productReadRepository = productReadRepository;
    }

    private async Task<Basket?> GetCurrentUserAsync()
    {
        var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            User? user = await _userManager.Users.Include(u => u.Baskets).FirstOrDefaultAsync(u => u.UserName == username);

            var currentBasket = from basket in user?.Baskets
                                join order in _orderReadRepository.Table
                                on basket.Id equals order.Id into BasketOrders
                                from order in BasketOrders.DefaultIfEmpty()
                                select new
                                {
                                    Basket = basket,
                                    Order = order,
                                };

            Basket? targetBasket = null;
            if (currentBasket.Any(b => b.Order is null))
            {
                targetBasket = currentBasket.FirstOrDefault(b => b.Order is null)?.Basket;
            }
            else
            {
                targetBasket = new();
                user?.Baskets.Add(targetBasket);
            }
            await _basketWriteRepository.SaveAsync();
            return targetBasket;
        }
        throw new Exception("Some error");
    }

    public async Task<BasketItem> AddBasketItemAsync(CreateBasketItemViewModel model)
    {
        Basket? basket = await GetCurrentUserAsync()
            ?? throw new InvalidOperationException("User basket not found.");

        var productId = Guid.Parse(model.ProductId);
        Product product = await _productReadRepository.GetByIdAsync(productId.ToString());
        BasketItem currentBasketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == productId);

        if (currentBasketItem != null)
        {
            currentBasketItem.Quantity++;
        }
        else
        {
            currentBasketItem = new BasketItem
            {
                BasketId = basket.Id,
                ProductId = productId,
                Quantity = model.Quantity,
                PriceAtTimeOfAddition = product.Price
            };
            await _basketItemWriteRepository.AddAsync(currentBasketItem);
        }

        await _basketItemWriteRepository.SaveAsync();
        return currentBasketItem;
    }

    public async Task<List<BasketItem>> GetBasketItemsAsync()
    {
        var currentUserBasket = await GetCurrentUserAsync();
        var basketId = currentUserBasket?.Id;

        var basket = await _basketReadRepository.Table
            .Include(b => b.BasketItems)
            .ThenInclude(bi => bi.Product)
            .FirstOrDefaultAsync(b => b.Id == basketId);

        return basket?.BasketItems?.ToList() ?? new List<BasketItem>();
    }

    public async Task<bool> RemoveBasketItemAsync(string basketItemId)
    {
        BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
        if (basketItem is null)
        {
            return false;
        }

        await _basketItemWriteRepository.RemoveAsync(basketItem);
        await _basketItemWriteRepository.SaveAsync();
        return true;
    }

    public async Task<BasketItem> UpdateBasketItemQuantityAsync(UpdateBasketItemViewModel model)
    {
        BasketItem? basketItem = (await _basketItemReadRepository.GetByIdAsync(model.BasketItemId))
            ?? throw new InvalidOperationException("Basket item not found.");

        basketItem.Quantity = model.Quantity;
        await _basketItemWriteRepository.SaveAsync();

        return basketItem;
    }
}
