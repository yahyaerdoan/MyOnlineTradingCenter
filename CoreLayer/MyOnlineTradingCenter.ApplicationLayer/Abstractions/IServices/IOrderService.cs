using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(CreateOrderDto createOrderDto);
    Task<(int TotalOrderCount, List<OrderDto> Orders)> GetOrdersAsync(Pagination pagination);
    Task<OrderDetailDto> GetByIdOrderDetailAsync(Guid orderId);
    Task<bool> UpdateOrderStatusToTrueAsync(Guid orderId);
}
