using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class Basket : BaseEntity
{
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public ICollection<BasketItem> BasketItems { get; set; } = default!;
}
