using AutoMapper;
using Common.DTOs;
using Domain.UnitOfWork;

namespace Application.UseCases.Events
{
    public class GetAllEventsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEventsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EventDTO>> ExecuteAsync(EventFilterDTO filter)
        {
            var events = await _unitOfWork.Events.GetAllAsync(filter);
            return _mapper.Map<List<EventDTO>>(events);
        }
    }
}