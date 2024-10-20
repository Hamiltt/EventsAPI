using Infrastructure.Repositories;
using AutoMapper;
using Common.DTOs;
using Infrastructure.Entities;
using Infrastructure.UnitOfWork;

namespace Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParticipantService(IParticipantRepository participantRepository, IEventRepository eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ParticipantDTO>> GetParticipantsByEventAsync(int eventId)
        {
            var participants = await _participantRepository.GetByEventIdAsync(eventId);
            return _mapper.Map<List<ParticipantDTO>>(participants);
        }

        public async Task<ParticipantDTO> GetParticipantByIdAsync(int eventId, int participantId)
        {
            var participant = await _participantRepository.GetByIdAsync(participantId);
            if (participant == null || participant.EventId != eventId)
                return null;

            return _mapper.Map<ParticipantDTO>(participant);
        }

        public async Task<bool> RegisterParticipantAsync(int eventId, RegisterParticipantDTO registerDto)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null || eventEntity.Participants.Count >= eventEntity.MaxParticipants)
                return false;

            var participant = _mapper.Map<Participant>(registerDto);
            participant.EventId = eventId;
            await _participantRepository.AddAsync(participant);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UnregisterParticipantAsync(int eventId, int participantId)
        {
            var participant = await _participantRepository.GetByIdAsync(participantId);
            if (participant == null || participant.EventId != eventId)
                return false;

            _participantRepository.Remove(participant);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}