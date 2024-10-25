﻿namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.OrderItems;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}