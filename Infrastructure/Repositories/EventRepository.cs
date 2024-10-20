using Common.DTOs;
using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventsDbContext _context;

        public EventRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllAsync(EventFilterDTO filter)
        {
            var query = _context.Events.Include(e => e.Participants).AsQueryable();

            if (filter.Date.HasValue)
                query = query.Where(e => e.Date.Date == filter.Date.Value.Date);
            if (!string.IsNullOrEmpty(filter.Location))
                query = query.Where(e => e.Location == filter.Location);
            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(e => e.Category == filter.Category);

            return await query.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.Participants)
                                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Event>> GetByNameAsync(string name)
        {
            return await _context.Events.Include(e => e.Participants)
                                        .Where(e => e.Name.Contains(name))
                                        .ToListAsync();
        }

        public async Task AddAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
        }

        public void Remove(Event eventEntity)
        {
            _context.Events.Remove(eventEntity);
        }
    }
}