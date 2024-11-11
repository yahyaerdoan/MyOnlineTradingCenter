using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System.Security.Claims;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response<CreateUserCommandResponseDto>> CreateUserAsync(CreateUserCommandRequestDto requestDto)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            UserName = requestDto.UserName,
            Email = requestDto.Email,
        }, requestDto.Password);
        if (identityResult.Succeeded)
        {
            var responseDto = new CreateUserCommandResponseDto
            {
                UserName = requestDto.UserName,
                Password = requestDto.Password
            };
            return Response<CreateUserCommandResponseDto>.Success(responseDto, "User created successfully!", StatusCodes.Status200OK);
        }
        else
        {
            var errors = identityResult.Errors.Select(e => $"{e.Code}: {e.Description}\n").ToList();
            var errorMessage = string.Join("\n", errors);
            return Response<CreateUserCommandResponseDto>.Failure(errorMessage, "User creation failed", StatusCodes.Status400BadRequest);
        }
    }


    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<UserDto> GetCurrentUserAsync()
    {
        var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userName))
        {
            return null!;
        }
        User? user = await _userManager.Users
            .Include(u => u.Baskets)
            .FirstOrDefaultAsync(u => u.UserName == userName);
        if (user == null) 
        {
            return null!;
        }

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }

    public async Task<Response<string>> UpdateRefreshTokenAsync(RefreshTokenCommandRequestDto requestDto)
    {
       User user = await _userManager.FindByIdAsync(requestDto.UserId);
        if (user is not null)
        {
            user.RefreshToken = requestDto.RefreshToken;
            user.RefreshTokenExpirationDate = requestDto.AccessTokenExpirationTime.ToUniversalTime().AddSeconds(requestDto.RefreshTokenLifeTime);
            await _userManager.UpdateAsync(user);          
            return Response<string>.Success(requestDto.RefreshToken, $"Refresh token created! Expires on: {user.RefreshTokenExpirationDate}", StatusCodes.Status200OK);
        }      
        return Response<string>.Failure("Error!", "Refresh token not created!", StatusCodes.Status400BadRequest);       
    }
}
