using AutoMapper;
using Common.DTOs;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UseCases.Events
{
    public class UpdateEventUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEventUseCase(IEventRepository eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(int id, UpdateEventDTO updateEventDto)
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

            _mapper.Map(updateEventDto, eventEntity);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}