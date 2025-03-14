﻿using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class BasketItem : BaseEntity
{
    public Guid ProductId { get; set; } = default!;
    public Guid BasketId { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public decimal PriceAtTimeOfAddition { get; set; } = default!;
    public Basket Basket { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
