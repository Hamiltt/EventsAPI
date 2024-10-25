using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Application.UseCases.Auth;
using System.Text;

namespace EventsAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, GetUserFromTokenUseCase getUserFromTokenUseCase)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                await AttachUserToContext(context, token, getUserFromTokenUseCase);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token, GetUserFromTokenUseCase getUserFromTokenUseCase)
        {
            try
            {
                var principal = ValidateToken(token);

                if (principal?.Identity?.IsAuthenticated ?? false)
                {
                    var username = principal.FindFirstValue(ClaimTypes.Name);
                    var user = await getUserFromTokenUseCase.ExecuteAsync(username);

                    if (user != null)
                    {
                        context.Items["User"] = user;
                    }
                }
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine($"Invalid token: {ex.Message}");
            }
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
