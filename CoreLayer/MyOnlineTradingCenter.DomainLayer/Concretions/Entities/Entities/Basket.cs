using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class Basket : BaseEntity
{
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public ICollection<BasketItem> BasketItems { get; set; } = default!;
}
