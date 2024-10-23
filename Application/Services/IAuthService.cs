using Common.DTOs;
using Domain.Entities;

public interface IAuthService
{
    Task<TokenResponse> AuthenticateAsync(LoginDTO loginDto);
    Task<TokenResponse> RefreshTokenAsync(TokenResponse tokenResponse);
    Task<User> GetUserFromTokenAsync(string token);
}
