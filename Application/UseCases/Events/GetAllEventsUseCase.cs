using AutoMapper;
using Common.DTOs;
using Domain.Repositories;

public class GetAllEventsUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetAllEventsUseCase(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<List<EventDTO>> ExecuteAsync(EventFilterDTO filter)
    {
        var events = await _eventRepository.GetAllAsync(filter);
        return _mapper.Map<List<EventDTO>>(events);
    }
}
