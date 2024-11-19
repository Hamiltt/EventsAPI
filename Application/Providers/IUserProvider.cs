using Domain.Entities;

namespace Application.Providers
{
    public interface IUserProvider
    {
        Task<User> GetUserFromTokenAsync(string token);
    }
}
