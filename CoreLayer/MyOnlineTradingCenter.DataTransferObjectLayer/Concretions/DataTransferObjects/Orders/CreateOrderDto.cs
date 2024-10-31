using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.OrderItems;

namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

public class CreateOrderDto
{
    public string? UserId { get; set; }
    public string? OrderNumber { get; set; }
    public string Address { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<OrderItemDto> OrderItemDtos { get; set; } = new List<OrderItemDto>();
}
