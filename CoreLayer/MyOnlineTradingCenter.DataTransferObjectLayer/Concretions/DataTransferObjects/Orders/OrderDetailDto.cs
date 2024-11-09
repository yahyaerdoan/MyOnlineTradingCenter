namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

public class OrderDetailDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    public DateTime CreatedDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }    
    public decimal WithTax { get; set; }
    public decimal TotalAmount { get; set; }
    public bool Status { get; set; }
}
public class OrderItemDto
{
    public Guid OrderId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => Price * Quantity;
}