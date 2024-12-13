using Application.Services;
using Common.DTOs;
using Common.Exceptions;
using Domain.UnitOfWork;

namespace Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;

        public LoginUseCase(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<TokenResponse> HandleAsync(LoginRequest loginRequest)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(loginRequest.Username);
            if (user == null || !user.VerifyPassword(_tokenService.HashPassword(loginRequest.Password)))
            {
                throw new UnauthorizedException("Invalid credentials provided.");
            }

            return await _tokenService.CreateTokensAsync(user);
        }
    }
}
