using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;

public class User : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpirationDate { get; set; }

    public ICollection<Basket> Baskets { get; set; }
    public ICollection<Order> Orders { get; set; }
}
