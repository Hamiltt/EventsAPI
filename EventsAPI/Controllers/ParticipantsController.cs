using Microsoft.AspNetCore.Mvc;
using Common.DTOs;
using Application.Services;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantsController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet("{eventId}/participants")]
        public async Task<IActionResult> GetParticipants(int eventId)
        {
            var participants = await _participantService.GetParticipantsByEventAsync(eventId);
            return Ok(participants);
        }

        [HttpGet("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> GetParticipantById(int eventId, int participantId)
        {
            var participant = await _participantService.GetParticipantByIdAsync(eventId, participantId);
            if (participant == null)
                return NotFound();
            return Ok(participant);
        }

        [HttpPost("{eventId}/register")]
        public async Task<IActionResult> Register(int eventId, [FromBody] RegisterParticipantDTO registerDto)
        {
            var result = await _participantService.RegisterParticipantAsync(eventId, registerDto);
            if (!result)
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> Unregister(int eventId, int participantId)
        {
            var result = await _participantService.UnregisterParticipantAsync(eventId, participantId);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}