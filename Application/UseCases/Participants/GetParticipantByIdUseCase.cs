using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Domain.UnitOfWork;

namespace Application.UseCases.Participants
{
    public class GetParticipantByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetParticipantByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ParticipantDTO> ExecuteAsync(int eventId, int participantId)
        {
            if (eventId <= 0 || participantId <= 0)
            {
                throw new BadRequestException("Invalid event or participant id.");
            }

            var participant = await _unitOfWork.Participants.GetByIdAsync(participantId);
            if (participant == null || participant.EventId != eventId)
            {
                throw new NotFoundException($"Participant with id {participantId} not found for event id {eventId}.");
            }

            return _mapper.Map<ParticipantDTO>(participant);
        }
    }
}