using Common.DTOs;
using Infrastructure.Repositories;
using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.UnitOfWork;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EventDTO>> GetEventsAsync(EventFilterDTO filter)
        {
            var events = await _eventRepository.GetAllAsync(filter);
            return _mapper.Map<List<EventDTO>>(events);
        }

        public async Task<EventDTO> GetEventByIdAsync(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            return _mapper.Map<EventDTO>(eventEntity);
        }

        public async Task<List<EventDTO>> GetEventsByNameAsync(string name)
        {
            var events = await _eventRepository.GetByNameAsync(name);
            return _mapper.Map<List<EventDTO>>(events);
        }

        public async Task<EventDTO> CreateEventAsync(CreateEventDTO createEventDto)
        {
            var eventEntity = _mapper.Map<Event>(createEventDto);
            await _eventRepository.AddAsync(eventEntity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<EventDTO>(eventEntity);
        }

        public async Task<bool> UpdateEventAsync(int id, UpdateEventDTO updateEventDto)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
                return false;

            _mapper.Map(updateEventDto, eventEntity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
                return false;

            _eventRepository.Remove(eventEntity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}