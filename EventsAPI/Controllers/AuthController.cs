using Microsoft.AspNetCore.Mvc;
using Common.DTOs;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var tokenResponse = await _authService.AuthenticateAsync(loginDto);
            if (tokenResponse == null)
                return Unauthorized();
            return Ok(tokenResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenResponse tokenResponse)
        {
            var newTokenResponse = await _authService.RefreshTokenAsync(tokenResponse);
            if (newTokenResponse == null)
                return Unauthorized();
            return Ok(newTokenResponse);
        }
    }
}