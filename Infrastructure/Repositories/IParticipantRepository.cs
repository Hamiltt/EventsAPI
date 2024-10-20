using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IParticipantRepository
    {
        Task<List<Participant>> GetByEventIdAsync(int eventId);
        Task<Participant> GetByIdAsync(int participantId);
        Task AddAsync(Participant participant);
        void Remove(Participant participant);
    }
}