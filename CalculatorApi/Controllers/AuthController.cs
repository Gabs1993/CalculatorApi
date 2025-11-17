using Application.DTOs.UserDto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.AuthenticateAsync(dto);
            if (result == null)
                return Unauthorized(new { message = "Email ou senha inválidos." });

            return Ok(result);
        }
    }
}
