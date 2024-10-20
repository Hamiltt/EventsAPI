using Common.DTOs;

namespace Application.Services
{
    public interface IParticipantService
    {
        Task<List<ParticipantDTO>> GetParticipantsByEventAsync(int eventId);
        Task<ParticipantDTO> GetParticipantByIdAsync(int eventId, int participantId);
        Task<bool> RegisterParticipantAsync(int eventId, RegisterParticipantDTO registerDto);
        Task<bool> UnregisterParticipantAsync(int eventId, int participantId);
    }
}