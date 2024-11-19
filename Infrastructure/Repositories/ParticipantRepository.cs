using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(EventsDbContext context) : base(context) { }

        public async Task<List<Participant>> GetByEventIdAsync(int eventId)
        {
            return await _dbSet.Where(p => p.EventId == eventId).ToListAsync();
        }

        public async Task<Participant> GetByIdAsync(int participantId)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Id == participantId);
        }

        public async Task<List<Participant>> GetPagedParticipantsAsync(int eventId, int pageNumber, int pageSize)
        {
            return await _dbSet.Where(p => p.EventId == eventId)
                               .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();
        }
    }
}
