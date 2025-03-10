using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Stock { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public ICollection<OrderItem> OrderItems { get; set; } = default!;
    public ICollection<ImageFile> ImageFiles { get; set; } = default!;
    public ICollection<BasketItem> BasketItems { get; set; } = default!;
}
