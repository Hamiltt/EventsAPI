using Common.DTOs;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync(EventFilterDTO filter);
        Task<Event> GetByIdAsync(int id);
        Task<List<Event>> GetByNameAsync(string name);
        Task AddAsync(Event eventEntity);
        void Remove(Event eventEntity);
    }
}