using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Domain.Entities;
using Domain.UnitOfWork;

namespace Application.UseCases.Participants
{
    public class RegisterParticipantUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterParticipantUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(int eventId, RegisterParticipantRequest registerRequest)
        {
            if (eventId <= 0)
            {
                throw new BadRequestException("Invalid event id.");
            }

            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {eventId} not found.");
            }

            if (eventEntity.Participants.Count >= eventEntity.MaxParticipants)
            {
                throw new BadRequestException($"Event with id {eventId} has reached its maximum number of participants.");
            }

            var user = await _unitOfWork.Users.GetByUsernameAsync(registerRequest.Username);
            if (user == null)
            {
                throw new NotFoundException($"User with username {registerRequest.Username} not found.");
            }

            var participant = _mapper.Map<Participant>(registerRequest);
            participant.EventId = eventId;

            await _unitOfWork.Participants.AddAsync(participant);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}