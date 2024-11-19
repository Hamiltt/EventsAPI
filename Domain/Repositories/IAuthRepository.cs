using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
