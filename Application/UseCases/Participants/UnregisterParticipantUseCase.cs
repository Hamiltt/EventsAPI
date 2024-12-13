using Common.Exceptions;
using Domain.UnitOfWork;

namespace Application.UseCases.Participants
{
    public class UnregisterParticipantUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnregisterParticipantUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int eventId, int participantId)
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

            _unitOfWork.Participants.Remove(participant);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}