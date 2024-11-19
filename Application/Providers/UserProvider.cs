using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public UserProvider(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<User> GetUserFromTokenAsync(string token)
        {
            var principal = ValidateToken(token);
            if (principal?.Identity?.IsAuthenticated ?? false)
            {
                var username = principal.FindFirstValue(ClaimTypes.Name);
                return await _authRepository.GetUserByUsernameAsync(username);
            }
            return null;
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
    }
}
