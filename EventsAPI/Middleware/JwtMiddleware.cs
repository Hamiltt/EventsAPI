using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Application.UseCases.Auth;
using System.Text;
using Application.Providers;

namespace EventsAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserProvider userProvider)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                await AttachUserToContext(context, token, userProvider);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token, IUserProvider userProvider)
        {
            var user = await userProvider.GetUserFromTokenAsync(token);
            if (user != null)
            {
                context.Items["User"] = user;
            }
        }
    }
}
