using Common.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using AutoMapper;
using Common.Exceptions;

namespace Application.UseCases.Events
{
    public class CreateEventUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEventUseCase(IEventRepository eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EventDTO> ExecuteAsync(CreateEventDTO createEventDto)
        {
            var existingEvent = await _eventRepository.GetByNameAsync(createEventDto.Name);
            if (existingEvent != null)
            {
                throw new AlreadyExistsException($"Event with name {createEventDto.Name} already exists.");
            }

            var eventEntity = _mapper.Map<Event>(createEventDto);
            await _eventRepository.AddAsync(eventEntity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EventDTO>(eventEntity);
        }
    }
}
