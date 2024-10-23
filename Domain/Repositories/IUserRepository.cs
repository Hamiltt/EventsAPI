using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAndPasswordAsync(string username, string password);
        Task<User> GetByUsernameAsync(string username);
    }
}