using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;

public class User : IdentityUser<string>
{
    public string FirtName { get; set; }
    public string LastName { get; set; }
    public string? RefreshToken { get; set; }
    public string? RefreshTokenExpirationDate { get; set; }
}
