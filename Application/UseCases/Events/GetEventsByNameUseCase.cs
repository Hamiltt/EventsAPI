using AutoMapper;
using Common.DTOs;
using Domain.UnitOfWork;

namespace Application.UseCases.Events
{
    public class GetEventsByNameUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventsByNameUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EventDTO>> ExecuteAsync(string name)
        {
            var events = await _unitOfWork.Events.GetByNameAsync(name);
            return _mapper.Map<List<EventDTO>>(events);
        }
    }
}