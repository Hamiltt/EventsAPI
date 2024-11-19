using Common.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(EventsDbContext context) : base(context) { }

        public async Task<List<Event>> GetAllAsync(EventFilterDTO filter)
        {
            var query = _dbSet.Include(e => e.Participants).AsQueryable();

            if (filter.Date.HasValue)
                query = query.Where(e => e.Date.Date == filter.Date.Value.Date);
            if (!string.IsNullOrEmpty(filter.Location))
                query = query.Where(e => e.Location == filter.Location);
            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(e => e.Category == filter.Category);

            // Применение пагинации
            query = query.Skip((filter.PageNumber - 1) * filter.PageSize)
                         .Take(filter.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<Event>> GetByNameAsync(string name)
        {
            return await _dbSet.Include(e => e.Participants)
                                .Where(e => e.Name.Contains(name))
                                .ToListAsync();
        }
    }
}
