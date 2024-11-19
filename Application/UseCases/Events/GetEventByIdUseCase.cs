using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.Events
{
    public class GetEventByIdUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetEventByIdUseCase(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDTO> ExecuteAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid event id.");
            }

            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {id} not found.");
            }

            return _mapper.Map<EventDTO>(eventEntity);
        }
    }
}