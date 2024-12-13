using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Domain.UnitOfWork;

namespace Application.UseCases.Events
{
    public class GetEventByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EventDTO> ExecuteAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid event id.");
            }

            var eventEntity = await _unitOfWork.Events.GetByIdAsync(id);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {id} not found.");
            }

            return _mapper.Map<EventDTO>(eventEntity);
        }
    }
}