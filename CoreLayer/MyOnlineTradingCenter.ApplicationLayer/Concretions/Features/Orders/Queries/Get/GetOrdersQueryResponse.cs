using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.Get;

public class GetOrdersQueryResponse
{
    public int TotalOrderCount { get; set; }
    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}