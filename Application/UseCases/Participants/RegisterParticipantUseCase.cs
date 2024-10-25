using AutoMapper;
using Common.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UseCases.Participants
{
    public class RegisterParticipantUseCase
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterParticipantUseCase(IParticipantRepository participantRepository, IEventRepository eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(int eventId, RegisterParticipantDTO registerDto)
        {
            if (eventId <= 0)
            {
                throw new BadRequestException("Invalid event id.");
            }

            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {eventId} not found.");
            }

            if (eventEntity.Participants.Count >= eventEntity.MaxParticipants)
            {
                throw new BadRequestException($"Event with id {eventId} has reached its maximum number of participants.");
            }

            var participant = _mapper.Map<Participant>(registerDto);
            participant.EventId = eventId;

            await _participantRepository.AddAsync(participant);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}