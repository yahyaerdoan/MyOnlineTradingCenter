using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Extensions;

public static class AuthenticationServiceRegistration
{
    public static void AddAuthenticationServiceRegistrations(this IServiceCollection services, IConfiguration configurations)
    {
        _ = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
           .AddJwtBearer("Admin", options =>
           {
               string securityKey = configurations["Token:SecurityKey"] ??
                throw new InvalidOperationException("Security key must be configured.");
               options.TokenValidationParameters = new()
               {
                   ValidateAudience = true,
                   ValidateIssuer = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidAudience = configurations["Token:Audience"] ?? "defaultAudience",
                   ValidIssuer = configurations["Token:Issuer"] ?? "defaultIssuer",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                   LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                   NameClaimType = ClaimTypes.Name
               };
           });

    }
}
