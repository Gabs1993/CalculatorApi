using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CotacaoController : ControllerBase
    {
        private readonly CotacaoService _service;

        public CotacaoController(CotacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cotacao cotacao)
        {
            await _service.AddAsync(cotacao);
            return CreatedAtAction(nameof(GetById), new { id = cotacao.id }, cotacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Cotacao cotacao)
        {
            if (id != cotacao.id) return BadRequest();
            await _service.UpdateAsync(cotacao);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
