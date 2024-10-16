using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public decimal TotalAmount => OrderItems.Sum(item => item.TotalPrice);

    public void AddOrderItem(OrderItem item)
    {
        OrderItems.Add(item);
    }
}
