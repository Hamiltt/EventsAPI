using Common.DTOs;
using Domain.Repositories;
using Domain.Exceptions;

namespace Application.UseCases.Auth
{
    public class RefreshTokenUseCase
    {
        private readonly IAuthRepository _authRepository;

        public RefreshTokenUseCase(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<TokenResponse> HandleAsync(TokenResponse tokenResponse)
        {
            var newTokenResponse = await _authRepository.RefreshTokenAsync(tokenResponse);
            if (newTokenResponse == null)
                throw new UnauthorizedException("Invalid refresh token.");

            return newTokenResponse;
        }
    }
}