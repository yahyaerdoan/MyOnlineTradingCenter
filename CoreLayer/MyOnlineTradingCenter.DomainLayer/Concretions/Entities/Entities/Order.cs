﻿using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public CompletedOrder CompletedOrder { get; set; } = default!;
}
