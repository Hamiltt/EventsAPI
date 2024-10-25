using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.Auth
{
    public class GetUserFromTokenUseCase
    {
        private readonly IAuthRepository _authRepository;

        public GetUserFromTokenUseCase(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<User> ExecuteAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty", nameof(username));
            }

            var user = await _authRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found or token is invalid.");
            }

            return user;
        }
    }
}
