using Application.Services;
using Common.DTOs;
using Common.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly IAuthRepository _authRepository;
        private readonly TokenService _tokenService;

        public LoginUseCase(IAuthRepository authRepository, TokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<TokenResponse> HandleAsync(LoginDTO loginDto)
        {
            var user = await _authRepository.GetUserByUsernameAsync(loginDto.Username);
            if (user == null || !user.VerifyPassword(_tokenService.HashPassword(loginDto.Password)))
            {
                throw new UnauthorizedException("Invalid credentials provided.");
            }

            var accessToken = _tokenService.GenerateJwtToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            return new TokenResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
