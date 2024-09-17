using Google.Apis.Auth;
using Google.Apis.Util;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.ITokens;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.SocialLogInUsers.GoogleLogInUsers.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Tokens;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users.GoogleLogInUsers;
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
    private readonly ITokenHandler _tokenHandler;

    public AuthService(UserManager<User> userManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
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
            Audience = new List<string> { "554001534100-n61ikte483p3maaeo3drs07p1eph7inu.apps.googleusercontent.com" }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(requestDto.IdToken, settings);

        var userInfo = new UserLoginInfo(requestDto.Provider, payload.Subject, requestDto.Provider);
        User user = await _userManager.FindByLoginAsync(userInfo.LoginProvider, userInfo.ProviderKey);

        bool result = user != null;
        var responseDto = new GoogleLogInUserCommandResponse();

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
                result = identityResult.Succeeded;
            }
        }
        if (result)
        {
            await _userManager.AddLoginAsync(user, userInfo);
            Token token = _tokenHandler.CreateAccessToken(requestDto.TokenLifeTime);
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

    public Task SystemLogInAsync()
    {
        throw new NotImplementedException();
    }
}
