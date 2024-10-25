using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UseCases.Participants
{
    public class UnregisterParticipantUseCase
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnregisterParticipantUseCase(IParticipantRepository participantRepository, IUnitOfWork unitOfWork)
        {
            _participantRepository = participantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int eventId, int participantId)
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

            _participantRepository.Remove(participant);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}