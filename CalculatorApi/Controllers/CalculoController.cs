using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CalculoController : ControllerBase
    {
        private readonly CalculoApplicationService _service;

        public CalculoController(CalculoApplicationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Calcular([FromBody] CalculoRequestDto dto)
        {
            var result = await _service.CalcularAsync(dto);
            return Ok(result);
        }
    }
}
