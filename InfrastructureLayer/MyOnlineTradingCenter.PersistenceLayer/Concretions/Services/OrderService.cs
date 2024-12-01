using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Baskets;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IBasketService _basketService;
    private readonly IUserService _userService;

    public OrderService(IOrderWriteRepository orderWriteRepository, IUserService userService, IProductReadRepository productReadRepository, IBasketService basketService, IOrderReadRepository orderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _userService = userService;
        _productReadRepository = productReadRepository;
        _basketService = basketService;
        _orderReadRepository = orderReadRepository;
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

    public async Task<(int TotalOrderCount, List<OrderDto> Orders)> GetOrdersAsync(Pagination pagination)
    {
        var query = _orderReadRepository.Table.
            Skip((pagination.Page) * pagination.Size).Take(pagination.Size).Where(x => x.Status == false)
            .Include(x => x.User)
                .ThenInclude(x => x.Orders)
                .ThenInclude(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Select(o => new OrderDto
                {
                    OrderId = o.Id,
                    OrderNumber = o.OrderNumber,
                    UserName = o.User.FirstName + ' ' + o.User.LastName,
                    CreatedDate = o.CreatedDate,
                    TotalAmount = o.OrderItems.Sum(x => x.Product.Price * x.Quantity)
                });

        int totalOrderCount = await _orderReadRepository.Table.CountAsync();
        List<OrderDto> orders = await query.OrderByDescending(x => x.CreatedDate).ToListAsync();
        return (totalOrderCount, orders);
    }

    public async Task<OrderDetailDto> GetByIdOrderDetailAsync(Guid orderId)
    {
        const decimal taxRate = 0.18m;

        var query = await _orderReadRepository.Table
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.User)
            .Where(o => o.Id == orderId)
            .Select(o => new OrderDetailDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                UserName = $"{o.User.FirstName} {o.User.LastName}",
                Address = o.Address,
                Description = o.Description,
                OrderItems = o.OrderItems.OrderBy(oi => oi.Price)
                .Select(oi => new OrderItemDto
                {
                    OrderId = oi.OrderId,
                    ProductName = oi.Product.Name,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList(),
                CreatedDate = o.CreatedDate,
                Subtotal = o.OrderItems.Sum(oi => oi.Price * oi.Quantity),
                Tax = o.OrderItems.Sum(oi => oi.Price * oi.Quantity) * taxRate,
                WithTax = o.OrderItems.Sum(oi => oi.Price * oi.Quantity) * (1 + taxRate),
                TotalAmount = o.OrderItems.Sum(oi => oi.Price * oi.Quantity) * (1 + taxRate),
                Status = o.Status
            }).FirstOrDefaultAsync();

        return query ?? new OrderDetailDto();
    }

    public async Task<bool> UpdateOrderStatusToTrueAsync(Guid orderId)
    {
        var order = await _orderReadRepository.GetByIdAsync(orderId.ToString());
        if (order == null) { return false; }

        order.Status = true;

        await _orderWriteRepository.UpdateAsync(order);
        await _orderWriteRepository.SaveAsync();
        return true;
    }

    #region GenerateSecureOrderNumber Helper Method
    private static string GenerateSecureOrderNumber()
    {
        long ticks = DateTime.Now.Ticks;
        using var randomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator.Create();
        byte[] randomBytes = new byte[4];
        randomNumberGenerator.GetBytes(randomBytes);
        int randomPart = BitConverter.ToInt32(randomBytes, 0);
        return "ORD-" + ticks + "-" + Math.Abs(randomPart).ToString();
    }

    private static string GenerateOrderNumber()
    {
        Random random = new();
        //return "ORD-" + random.Next(100000, 999999).ToString();
        return random.Next(1, 10).ToString();
        //return $"ORD-{DateTime.Now.Ticks}";
        //return $"ORD-{Guid.NewGuid().ToString()}";
    }

    #endregion
}