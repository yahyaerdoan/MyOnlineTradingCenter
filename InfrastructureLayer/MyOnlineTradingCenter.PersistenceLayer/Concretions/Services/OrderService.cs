using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Baskets;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.OrderItems;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IBasketService _basketService;
    private readonly IUserService _userService;

    public OrderService(IOrderWriteRepository orderWriteRepository, IUserService userService, IProductReadRepository productReadRepository, IBasketService basketService)
    {
        _orderWriteRepository = orderWriteRepository;
        _userService = userService;
        _productReadRepository = productReadRepository;
        _basketService = basketService;
    }

    public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var currentUser = await _userService.GetCurrentUserAsync();
        if (currentUser == null)
        {
            return;
        }

        BasketDto basketDto = await _basketService.GetBasketByUserIdAsync(currentUser.Id);
        if (basketDto == null || !basketDto.Items.Any())
        {
            return;
        }
        Random random = new Random();
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.Id,
            OrderNumber = GenerateSecureOrderNumber(), //createOrderDto.OrderNumber,
            Address = "45876 W Granville Ave", //createOrderDto.Address,
            Description = "Order Description" + " " + random.Next(1, 10), //createOrderDto.Description
            OrderItems = new List<OrderItem>()
        };
        foreach (var basketItemDto in basketDto.Items)
        {
            var product = await _productReadRepository.GetByIdAsync(basketItemDto.ProductId.ToString());
            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = basketItemDto.ProductId,
                Quantity = basketItemDto.Quantity,
                Price = basketItemDto.PriceAtTimeOfAddition,
                Status = true
            };
            order.OrderItems.Add(orderItem);
        }
        await _orderWriteRepository.AddAsync(order);
        await _orderWriteRepository.SaveAsync();
    }

    private static string GenerateSecureOrderNumber()
    {
        using var radomNumberGenarate = System.Security.Cryptography.RandomNumberGenerator.Create();
        byte[] randomNumber = new byte[32];
        radomNumberGenarate.GetBytes(randomNumber);
        int value = BitConverter.ToInt32(randomNumber, 0);
        return "ORD-" + Math.Abs(value).ToString();
    }

    private static string GenerateOrderNumber()
    {
        Random random = new();
        return "ORD-" + random.Next(100000, 999999).ToString();
        //return $"ORD-{DateTime.Now.Ticks}";
    }
}
