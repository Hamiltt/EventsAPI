using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.Participants
{
    public class GetParticipantByIdUseCase
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public GetParticipantByIdUseCase(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<ParticipantDTO> ExecuteAsync(int eventId, int participantId)
        {
            if (eventId <= 0 || participantId <= 0)
            {
                throw new BadRequestException("Invalid event or participant id.");
            }

            var participant = await _participantRepository.GetByIdAsync(participantId);
            if (participant == null || participant.EventId != eventId)
            {
                throw new NotFoundException($"Participant with id {participantId} not found for event id {eventId}.");
            }

            return _mapper.Map<ParticipantDTO>(participant);
        }
    }
}