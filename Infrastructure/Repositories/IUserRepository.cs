using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAndPasswordAsync(string username, string password);
        Task<User> GetByUsernameAsync(string username);
    }
}