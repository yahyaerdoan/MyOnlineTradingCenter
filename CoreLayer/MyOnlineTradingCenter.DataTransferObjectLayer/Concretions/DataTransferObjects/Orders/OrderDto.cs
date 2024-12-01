namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

public class OrderDto
{
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public DateTime CreatedDate { get; set; } 
    public decimal TotalAmount { get; set; }
}