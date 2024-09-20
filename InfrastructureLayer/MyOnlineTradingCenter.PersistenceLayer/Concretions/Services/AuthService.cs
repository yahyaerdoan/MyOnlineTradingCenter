using Google.Apis.Auth;
using Google.Apis.Util;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.LogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.RefreshTokenLogIns.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;
    private readonly ITokenHandler _tokenHandler;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, ITokenHandler tokenHandler, IConfiguration configuration, SignInManager<User> signInManager, IUserService userService)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _configuration = configuration;
        _signInManager = signInManager;
        _userService = userService;
    }

    public Task FacebookLogInAsync()
    {
        throw new NotImplementedException();
    }

    public Task GithubLogInAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Response<GoogleLogInUserCommandResponse>> GoogleLogInAsync(GoogleLogInUserCommandRequest requestDto)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLogInSettings:GoogleLogIn:ClientId"] }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(requestDto.IdToken, settings);

        var userInfo = new UserLoginInfo(requestDto.Provider, payload.Subject, requestDto.Provider);
        User user = await _userManager.FindByLoginAsync(userInfo.LoginProvider, userInfo.ProviderKey);

        bool result = await GetOrCreateExternalUserAsync(payload, user);
        var responseDto = new GoogleLogInUserCommandResponse();

        if (result)
        {
            await _userManager.AddLoginAsync(user, userInfo);
            Token token = _tokenHandler.CreateAccessToken(requestDto.AccessTokenLifeTime);
            await _userService.UpdateRefreshToken(token.RefreshToken, user.Id, token.Expiration, 5);
            responseDto.Token = token;
            return Response<GoogleLogInUserCommandResponse>.Success(responseDto, "User logged in successfully with Google!", StatusCodes.Status200OK);
        }
        else
            return Response<GoogleLogInUserCommandResponse>.Failure("Invalid credentials!");
    }

    public Task InstagramLogInAsync()
    {
        throw new NotImplementedException();
    }

    public Task LinkedInLogInAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Response<RefreshTokenLogInCommandResponse>> RefreshTokenLogInAsync(RefreshTokenLogInCommandRequest request)
    {
        User? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
        if (user != null && user?.RefreshTokenExpirationDate > DateTime.UtcNow)
        {
            Token token = _tokenHandler.CreateAccessToken(30);
           var updatedRefreshToken = await _userService.UpdateRefreshToken(token.RefreshToken, user.Id, token.Expiration, 30);
            var refreshTokenResponse = new RefreshTokenLogInCommandResponse { Token = token  };
            return Response<RefreshTokenLogInCommandResponse>.Success(refreshTokenResponse, "Refresh token created successfully!", StatusCodes.Status200OK);
        }
        return Response<RefreshTokenLogInCommandResponse>.Failure("Error!", "Refresh token creation failed!", StatusCodes.Status400BadRequest);
    }

    public async Task<Response<LogInUserCommandResponse>> SystemLogInAsync(LogInUserCommandRequest request)
    {
        User? user = await _userManager.FindByNameAsync(request.UserNameOrEmail)
               ?? await _userManager.Users.Where(u => u.Email == request.UserNameOrEmail).FirstOrDefaultAsync();

        if (user == null)
        {
            return Response<LogInUserCommandResponse>.Failure("Invalid credentials", "User not found!", StatusCodes.Status404NotFound);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (result.Succeeded)
        {
            Token token = _tokenHandler.CreateAccessToken(request.AccessTokenLifeTime);
            await _userService.UpdateRefreshToken(token.RefreshToken, user.Id, token.Expiration, 30);
            var loginResponse = new LogInUserCommandResponse { Token = token };
            return Response<LogInUserCommandResponse>.Success(loginResponse, "User logged in successfully!", StatusCodes.Status200OK);
        }
        else
            return Response<LogInUserCommandResponse>.Failure("Invalid credentials", "Login failed!", StatusCodes.Status401Unauthorized);
    }

    private async Task<bool> GetOrCreateExternalUserAsync(GoogleJsonWebSignature.Payload payload, User user)
    {
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    UserName = payload.GivenName + payload.FamilyName,
                    FirtName = payload.GivenName,
                    LastName = payload.FamilyName,
                };
                var identityResult = await _userManager.CreateAsync(user);
                return identityResult.Succeeded;
            }
        }
        return true;
    }
}
