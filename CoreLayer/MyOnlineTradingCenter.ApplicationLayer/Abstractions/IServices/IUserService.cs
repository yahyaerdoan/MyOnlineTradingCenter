using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IUserService
{
    Task<Response<CreateUserCommandResponseDto>> CreateUserAsync(CreateUserCommandRequestDto requestDto);
    Task<Response<string>> UpdateRefreshTokenAsync(RefreshTokenCommandRequestDto requestDto);
}
