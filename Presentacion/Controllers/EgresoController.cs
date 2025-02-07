using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EgresoController : ControllerBase
    {
        private readonly IEgresoService _egresoService;

        public EgresoController(IEgresoService egresoService)
        {
            _egresoService = egresoService;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<List<EgresoDto>>> Get()
        {
            return Ok(await _egresoService.ObtenerTodos());
        }

        [HttpGet("Obtener/{id}")]
        public async Task<ActionResult<EgresoDto>> GetById(int id)
        {
            var egreso = await _egresoService.ObtenerPorId(id);
            if (egreso == null)
                return NotFound();

            return Ok(egreso);
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<EgresoDto>> Create(EgresoDto dto)
        {
            var egreso = await _egresoService.Agregar(dto);
            return CreatedAtAction(nameof(GetById), new { id = egreso.Id }, egreso);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<ActionResult> Update(int id, EgresoDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var updated = await _egresoService.Actualizar(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _egresoService.Eliminar(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
