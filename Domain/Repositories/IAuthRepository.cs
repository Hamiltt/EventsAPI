using Common.DTOs;
using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<TokenResponse> AuthenticateAsync(LoginDTO loginDto);
        Task<TokenResponse> RefreshTokenAsync(TokenResponse tokenResponse);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
