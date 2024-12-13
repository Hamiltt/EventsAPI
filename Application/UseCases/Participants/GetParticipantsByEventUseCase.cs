using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Domain.UnitOfWork;

namespace Application.UseCases.Participants
{
    public class GetParticipantsByEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetParticipantsByEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ParticipantDTO>> ExecuteAsync(int eventId)
        {
            if (eventId <= 0)
            {
                throw new BadRequestException("Invalid event id.");
            }

            var participants = await _unitOfWork.Participants.GetByEventIdAsync(eventId);
            if (participants == null || !participants.Any())
            {
                throw new NotFoundException($"No participants found for event id {eventId}.");
            }

            return _mapper.Map<List<ParticipantDTO>>(participants);
        }
    }
}