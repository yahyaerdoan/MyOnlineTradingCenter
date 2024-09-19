using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.CreateUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Response<CreateUserCommandResponseDto>> CreateUserAsync(CreateUserCommandRequestDto requestDto)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            FirtName = requestDto.FirstName,
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

   public async Task UpdateRefreshToken(string refreshToken, string userId, DateTime accessTokenDuration, int refreshTokenDuration)
    {
        User user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationDate = accessTokenDuration.AddSeconds(refreshTokenDuration).ToString("o");
            await _userManager.UpdateAsync(user);
        }
    }
}
//IdentityResult result = await _userManager.CreateAsync(new()
//{
//    Id = Guid.NewGuid().ToString(),
//    FirtName = request.FirstName,
//    LastName = request.LastName,
//    UserName = request.UserName,
//    Email = request.Email,
//}, request.Password);

//CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };
//        if (result.Succeeded)
//            response.Message = "User created!";
//        else
//            foreach (var error in result.Errors)
//                response.Message += $"{error.Code} - {error.Description}\n";
//        return response;