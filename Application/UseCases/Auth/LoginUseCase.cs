using Common.DTOs;
using Domain.Repositories;
using Domain.Exceptions;

namespace Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly IAuthRepository _authRepository;

        public LoginUseCase(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<TokenResponse> HandleAsync(LoginDTO loginDto)
        {
            var tokenResponse = await _authRepository.AuthenticateAsync(loginDto);
            if (tokenResponse == null)
                throw new UnauthorizedException("Invalid credentials provided.");

            return tokenResponse;
        }
    }
}