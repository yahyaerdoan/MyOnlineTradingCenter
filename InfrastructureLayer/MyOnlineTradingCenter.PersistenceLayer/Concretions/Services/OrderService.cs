using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Baskets;
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

    public async Task<bool> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var currentUser = await _userService.GetCurrentUserAsync();
        BasketDto basketDto = await _basketService.GetBasketByUserIdAsync(currentUser.Id);
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.Id,
            OrderNumber = GenerateSecureOrderNumber(),
            Address = createOrderDto.Address,
            Description = createOrderDto.Description,
            OrderItems = new List<OrderItem>()
        };      
        foreach (var basketItemDto in basketDto.Items)
        {
            var product = await _productReadRepository.GetByIdAsync(basketItemDto.ProductId.ToString());
            if (product == null) continue;

            var orderItem = new OrderItem
            {
                OrderId = order.Id, //TODO: work to get the id, from db
                ProductId = basketItemDto.ProductId,
                Quantity = basketItemDto.Quantity,
                Price = basketItemDto.PriceAtTimeOfAddition,
                Status = true
            };
            order.OrderItems.Add(orderItem);
        }
        if (!await _orderWriteRepository.AddAsync(order)) return false;

        return await _orderWriteRepository.SaveAsync() > 0;
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
        //return "ORD-" + random.Next(100000, 999999).ToString();
        return random.Next(1, 10).ToString();
        //return $"ORD-{DateTime.Now.Ticks}";
        //return $"ORD-{Guid.NewGuid().ToString()}";
    }
}
