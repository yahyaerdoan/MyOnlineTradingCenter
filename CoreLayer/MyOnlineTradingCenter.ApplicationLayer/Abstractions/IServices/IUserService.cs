using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IUserService
{
    Task<Response<CreateUserCommandResponseDto>> CreateUserAsync(CreateUserCommandRequestDto requestDto);
    Task<Response<string>> UpdateRefreshTokenAsync(RefreshTokenCommandRequestDto requestDto);
    Task<UserDto> GetCurrentUserAsync();
}
