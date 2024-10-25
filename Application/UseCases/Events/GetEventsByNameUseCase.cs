using AutoMapper;
using Common.DTOs;
using Domain.Repositories;

public class GetEventsByNameUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventsByNameUseCase(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<List<EventDTO>> ExecuteAsync(string name)
    {
        var events = await _eventRepository.GetByNameAsync(name);
        return _mapper.Map<List<EventDTO>>(events);
    }
}
