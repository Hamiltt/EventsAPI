using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EventsDbContext context) : base(context) { }

        public async Task<User> GetByUsernameAndPasswordAsync(string username, string passwordHash)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == passwordHash);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
