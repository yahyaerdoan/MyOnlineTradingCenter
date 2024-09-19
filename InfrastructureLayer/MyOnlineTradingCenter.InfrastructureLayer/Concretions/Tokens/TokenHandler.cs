using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Tokens;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateAccessToken(int minute)
    {
        Token token = new();

        SymmetricSecurityKey symmetricSecurity = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials signingCredentials = new(symmetricSecurity, SecurityAlgorithms.HmacSha256);
        
        DateTime now = DateTime.Now;
        token.Expiration = now.AddSeconds(minute);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            expires: now.AddSeconds(minute),
            notBefore: now,
            signingCredentials: signingCredentials
            );

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        return token;
    }
}
