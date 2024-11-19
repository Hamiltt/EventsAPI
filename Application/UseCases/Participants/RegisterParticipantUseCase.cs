using AutoMapper;
using Common.DTOs;
using Domain.Entities;
using Common.Exceptions;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UseCases.Participants
{
    public class RegisterParticipantUseCase
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterParticipantUseCase(
            IParticipantRepository participantRepository,
            IEventRepository eventRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _participantRepository = participantRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
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

            // Проверка существования пользователя
            var user = await _userRepository.GetByUsernameAsync(registerDto.Username);
            if (user == null)
            {
                throw new NotFoundException($"User with username {registerDto.Username} not found.");
            }

            var participant = _mapper.Map<Participant>(registerDto);
            participant.EventId = eventId;

            await _participantRepository.AddAsync(participant);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
