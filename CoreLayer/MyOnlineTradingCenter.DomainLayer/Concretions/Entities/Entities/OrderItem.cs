using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; } = default!;
    public Order Order { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public Product Product { get; set; } = default!; 
    public int Quantity { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}
