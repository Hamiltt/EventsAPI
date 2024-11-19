using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.Participants
{
    public class GetParticipantsByEventUseCase
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public GetParticipantsByEventUseCase(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<List<ParticipantDTO>> ExecuteAsync(int eventId)
        {
            if (eventId <= 0)
            {
                throw new BadRequestException("Invalid event id.");
            }

            var participants = await _participantRepository.GetByEventIdAsync(eventId);
            if (participants == null || !participants.Any())
            {
                throw new NotFoundException($"No participants found for event id {eventId}.");
            }

            return _mapper.Map<List<ParticipantDTO>>(participants);
        }
    }
}