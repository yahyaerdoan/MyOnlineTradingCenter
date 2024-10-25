using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.OrderItems;

namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}
