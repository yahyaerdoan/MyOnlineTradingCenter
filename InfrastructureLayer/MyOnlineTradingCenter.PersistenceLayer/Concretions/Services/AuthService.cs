using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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
using System.Text;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;
    private readonly ITokenHandler _tokenHandler;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public AuthService(UserManager<User> userManager, ITokenHandler tokenHandler, IConfiguration configuration, SignInManager<User> signInManager, IUserService userService, IEmailService emailService)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _configuration = configuration;
        _signInManager = signInManager;
        _userService = userService;
        _emailService = emailService;
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
        var payload = await ValidateGoogleTokenAsync(requestDto.IdToken);
        if (payload == null)
        {
            return Response<GoogleLogInUserCommandResponse>.Failure("Invalid Google ID token.");
        }

        var userLogInfo = new UserLoginInfo(requestDto.Provider, payload.Subject, requestDto.Provider);
        var user = await FindOrCreateExternalUserAsync(payload, userLogInfo);

        if (user == null)
        {
            return Response<GoogleLogInUserCommandResponse>.Failure("User could not be created or found!");
        }

        await AddLoginToUserAsync(user, userLogInfo);

        var token = await GenerateAndUpdateTokensAsync(user);

        var responseDto = new GoogleLogInUserCommandResponse { Token = token };
        return Response<GoogleLogInUserCommandResponse>.Success(responseDto, "User logged in successfully with Google!", StatusCodes.Status200OK);
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
        var user = await GetUserByRefreshTokenAsync(request.RefreshToken);

        if (user == null || !IsRefreshTokenValid(user))
        {
            return Response<RefreshTokenLogInCommandResponse>.Failure("InvalidRefreshToken!", "The provided refresh token is either expired or invalid.", StatusCodes.Status400BadRequest);
        }

        var token = await GenerateAndUpdateTokensAsync(user);

        var refreshTokenResponse = new RefreshTokenLogInCommandResponse { Token = token };
        return Response<RefreshTokenLogInCommandResponse>.Success(refreshTokenResponse, "Refresh token created successfully!", StatusCodes.Status200OK);
    }

    public async Task<Response<LogInUserCommandResponse>> SystemLogInAsync(LogInUserCommandRequest request)
    {
        var user = await FindUserAsync(request.UserNameOrEmail);
        if (user == null)
        {
            return Response<LogInUserCommandResponse>.Failure("Invalid credentials", "User not found!", StatusCodes.Status404NotFound);
        }

        var signInResult = await ValidateUserCredentialsAsync(user, request.Password);
        if (!signInResult.Succeeded)
        {
            return Response<LogInUserCommandResponse>.Failure("Invalid credentials", "Login failed!", StatusCodes.Status401Unauthorized);
        }

        var token = await GenerateAndUpdateTokensAsync(user);

        var loginResponse = new LogInUserCommandResponse { Token = token };
        return Response<LogInUserCommandResponse>.Success(loginResponse, "User logged in successfully!", StatusCodes.Status200OK);
    }

    public async Task ResetPasswordAsync(string email)
    {
        var user = await FindUserAsync(email);
        if (string.IsNullOrEmpty(user?.Email)) return;

        var resetToken = await GenerateEncodedResetTokenAsync(user);
        await _emailService.RequestPasswordResetAsync(email, resetToken, user.Id);
    }

    public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var decodedToken = DecodeResetToken(resetToken);
        return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, PasswordResetTokenPurpose, decodedToken);
    }
    public async Task<bool> UpdatePasswordAsync(string userId, string email, string resetToken, string newPassword)
    {
        var user = await FindUserByEmailAndIdAsync(userId, email);
        if (user == null) return false;

        var decodedToken = DecodeResetToken(resetToken);
        var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);

        if (result.Succeeded)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            return true;
        }

        return false;
    }
    private async Task<User?> FindUserByEmailAndIdAsync(string? userId, string? email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null) return user;
        }

        if (!string.IsNullOrEmpty(userId))
        {
            return await _userManager.FindByIdAsync(userId);
        }

        return null;
    }

    private const string PasswordResetTokenPurpose = "ResetPassword";
    private async Task<string> GenerateEncodedResetTokenAsync(User user)
    {
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        return EncodeResetToken(resetToken);
    }
    private static string DecodeResetToken(string encodedToken)
    {
        byte[] tokenBytes = WebEncoders.Base64UrlDecode(encodedToken);
        return Encoding.UTF8.GetString(tokenBytes);
    }
    private static string EncodeResetToken(string token)
    {
        byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
        return WebEncoders.Base64UrlEncode(tokenBytes);
    }

    #region Helper methods
    private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string> { _configuration["ExternalLogInSettings:GoogleLogIn:ClientId"] }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        return payload;
    }

    private async Task<User?> FindOrCreateExternalUserAsync(GoogleJsonWebSignature.Payload payload, UserLoginInfo userInfo)
    {
        var user = await _userManager.FindByLoginAsync(userInfo.LoginProvider, userInfo.ProviderKey)
                  ?? await _userManager.FindByEmailAsync(payload.Email);

        if (user == null)
        {
            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = payload.Email,
                UserName = $"{payload.GivenName}{payload.FamilyName}",
                FirstName = payload.GivenName,
                LastName = payload.FamilyName
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return null;
            }
        }

        return user;
    }

    private async Task AddLoginToUserAsync(User user, UserLoginInfo userInfo)
    {
        if ((await _userManager.FindByLoginAsync(userInfo.LoginProvider, userInfo.ProviderKey)) == null)
        {
            await _userManager.AddLoginAsync(user, userInfo);
        }
    }

    private async Task<Token> GenerateAndUpdateTokensAsync(User user)
    {
        int accessTokenLifeTime = _configuration.GetValue<int>("TokenSettings:AccessTokenLifeTime");
        int refreshTokenLifeTime = _configuration.GetValue<int>("TokenSettings:RefreshTokenLifeTime");

        Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);

        await _userService.UpdateRefreshTokenAsync(new RefreshTokenCommandRequestDto
        {
            AccessTokenExpirationTime = token.Expiration,
            RefreshToken = token.RefreshToken,
            RefreshTokenLifeTime = refreshTokenLifeTime,
            UserId = user.Id
        });

        return token;
    }

    private async Task<User?> FindUserAsync(string userNameOrEmail)
    {
        return await _userManager.FindByNameAsync(userNameOrEmail)
               ?? await _userManager.Users.Where(u => u.Email == userNameOrEmail).FirstOrDefaultAsync();
    }

    private async Task<SignInResult> ValidateUserCredentialsAsync(User user, string password)
    {
        return await _signInManager.CheckPasswordSignInAsync(user, password, true);
    }

    private async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    private static bool IsRefreshTokenValid(User user)
    {
        return user.RefreshTokenExpirationDate > DateTime.Now;
    }

    #endregion
}
