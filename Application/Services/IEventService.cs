using Common.DTOs;

namespace Application.Services
{
    public interface IEventService
    {
        Task<List<EventDTO>> GetEventsAsync(EventFilterDTO filter);
        Task<EventDTO> GetEventByIdAsync(int id);
        Task<List<EventDTO>> GetEventsByNameAsync(string name);
        Task<EventDTO> CreateEventAsync(CreateEventDTO createEventDto);
        Task<bool> UpdateEventAsync(int id, UpdateEventDTO updateEventDto);
        Task<bool> DeleteEventAsync(int id);
    }
}