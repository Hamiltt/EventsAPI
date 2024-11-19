using Microsoft.AspNetCore.Mvc;
using Common.DTOs;
using Application.UseCases.Events;
using Common.Exceptions;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly GetAllEventsUseCase _getAllEventsUseCase;
        private readonly GetEventByIdUseCase _getEventByIdUseCase;
        private readonly GetEventsByNameUseCase _getEventsByNameUseCase;
        private readonly CreateEventUseCase _createEventUseCase;
        private readonly UpdateEventUseCase _updateEventUseCase;
        private readonly DeleteEventUseCase _deleteEventUseCase;

        public EventsController(
            GetAllEventsUseCase getAllEventsUseCase,
            GetEventByIdUseCase getEventByIdUseCase,
            GetEventsByNameUseCase getEventsByNameUseCase,
            CreateEventUseCase createEventUseCase,
            UpdateEventUseCase updateEventUseCase,
            DeleteEventUseCase deleteEventUseCase)
        {
            _getAllEventsUseCase = getAllEventsUseCase;
            _getEventByIdUseCase = getEventByIdUseCase;
            _getEventsByNameUseCase = getEventsByNameUseCase;
            _createEventUseCase = createEventUseCase;
            _updateEventUseCase = updateEventUseCase;
            _deleteEventUseCase = deleteEventUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] EventFilterDTO filter)
        {
            var events = await _getAllEventsUseCase.ExecuteAsync(filter);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var eventDto = await _getEventByIdUseCase.ExecuteAsync(id);
            return Ok(eventDto);

        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var events = await _getEventsByNameUseCase.ExecuteAsync(name);
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDTO createEventDto)
        {
            var eventDto = await _createEventUseCase.ExecuteAsync(createEventDto);
            return CreatedAtAction(nameof(GetById), new { id = eventDto.Id }, eventDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventDTO updateEventDto)
        {
            await _updateEventUseCase.ExecuteAsync(id, updateEventDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteEventUseCase.ExecuteAsync(id);
            return NoContent();

        }
    }
}
