using Microsoft.AspNetCore.Mvc;
using Common.DTOs;
using Application.UseCases.Participants;
using Common.Exceptions;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly GetParticipantsByEventUseCase _getParticipantsByEventUseCase;
        private readonly GetParticipantByIdUseCase _getParticipantByIdUseCase;
        private readonly RegisterParticipantUseCase _registerParticipantUseCase;
        private readonly UnregisterParticipantUseCase _unregisterParticipantUseCase;

        public ParticipantsController(
            GetParticipantsByEventUseCase getParticipantsByEventUseCase,
            GetParticipantByIdUseCase getParticipantByIdUseCase,
            RegisterParticipantUseCase registerParticipantUseCase,
            UnregisterParticipantUseCase unregisterParticipantUseCase)
        {
            _getParticipantsByEventUseCase = getParticipantsByEventUseCase;
            _getParticipantByIdUseCase = getParticipantByIdUseCase;
            _registerParticipantUseCase = registerParticipantUseCase;
            _unregisterParticipantUseCase = unregisterParticipantUseCase;
        }

        [HttpGet("{eventId}/participants")]
        public async Task<IActionResult> GetParticipants(int eventId)
        {
            var participants = await _getParticipantsByEventUseCase.ExecuteAsync(eventId);
            return Ok(participants);
        }

        [HttpGet("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> GetParticipantById(int eventId, int participantId)
        {
            var participant = await _getParticipantByIdUseCase.ExecuteAsync(eventId, participantId);
            return Ok(participant);
        }

        [HttpPost("{eventId}/register")]
        public async Task<IActionResult> Register(int eventId, [FromBody] RegisterParticipantDTO registerDto)
        {
            await _registerParticipantUseCase.ExecuteAsync(eventId, registerDto);
            return NoContent();
        }

        [HttpDelete("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> Unregister(int eventId, int participantId)
        {
            await _unregisterParticipantUseCase.ExecuteAsync(eventId, participantId);
            return NoContent();
        }
    }
}