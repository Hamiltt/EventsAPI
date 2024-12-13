using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Domain.Entities;
using Domain.UnitOfWork;

namespace Application.UseCases.Events
{
    public class CreateEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EventDTO> ExecuteAsync(CreateEventRequest createEventRequest)
        {
            var existingEvent = await _unitOfWork.Events.GetByNameAsync(createEventRequest.Name);
            if (existingEvent != null)
            {
                throw new AlreadyExistsException($"Event with name {createEventRequest.Name} already exists.");
            }

            var eventEntity = _mapper.Map<Event>(createEventRequest);
            await _unitOfWork.Events.AddAsync(eventEntity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EventDTO>(eventEntity);
        }
    }
}