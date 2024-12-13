using Application.Services;
using Common.DTOs;
using Common.Exceptions;
using Domain.UnitOfWork;

namespace Application.UseCases.Auth
{
    public class RefreshTokenUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly TokenValidationService _tokenValidationService;

        public RefreshTokenUseCase(IUnitOfWork unitOfWork, TokenService tokenService, TokenValidationService tokenValidationService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _tokenValidationService = tokenValidationService;
        }

        public async Task<TokenResponse> HandleAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var principal = _tokenValidationService.GetPrincipalFromExpiredToken(refreshTokenRequest.AccessToken);
            var username = principal.Identity?.Name;

            if (username == null)
                throw new UnauthorizedException("Invalid refresh token.");

            var user = await _unitOfWork.Users.GetByUsernameAsync(username);
            if (user == null)
                throw new UnauthorizedException("User not found.");

            return await _tokenService.CreateTokensAsync(user);
        }
    }
}