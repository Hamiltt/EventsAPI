using Microsoft.AspNetCore.Mvc;
using Common.DTOs;
using Application.UseCases.Auth;
using Domain.Exceptions;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RefreshTokenUseCase _refreshTokenUseCase;

        public AuthController(
            LoginUseCase loginUseCase,
            RefreshTokenUseCase refreshTokenUseCase)
        {
            _loginUseCase = loginUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var tokenResponse = await _loginUseCase.HandleAsync(loginDto);
                return Ok(tokenResponse);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenResponse tokenResponse)
        {
            try
            {
                var newTokenResponse = await _refreshTokenUseCase.HandleAsync(tokenResponse);
                return Ok(newTokenResponse);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
        }
    }
}
