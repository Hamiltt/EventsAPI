using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly EventsDbContext _context;

        public ParticipantRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Participant>> GetByEventIdAsync(int eventId)
        {
            return await _context.Participants.Where(p => p.EventId == eventId).ToListAsync();
        }

        public async Task<Participant> GetByIdAsync(int participantId)
        {
            return await _context.Participants.FirstOrDefaultAsync(p => p.Id == participantId);
        }

        public async Task AddAsync(Participant participant)
        {
            await _context.Participants.AddAsync(participant);
        }

        public void Remove(Participant participant)
        {
            _context.Participants.Remove(participant);
        }
    }
}