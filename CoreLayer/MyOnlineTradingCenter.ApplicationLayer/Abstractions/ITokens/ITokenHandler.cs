using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;

public interface ITokenHandler
{
    Token CreateAccessToken(int minute, User user);
    string CreateRefreshToken();
}
