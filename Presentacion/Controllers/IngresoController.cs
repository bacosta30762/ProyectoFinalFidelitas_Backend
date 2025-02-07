using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoController : ControllerBase
    {
        private readonly IIngresoService _ingresoService;

        public IngresoController(IIngresoService ingresoService)
        {
            _ingresoService = ingresoService;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<List<IngresoDto>>> Get()
        {
            return Ok(await _ingresoService.ObtenerTodos());
        }

        [HttpGet("Obtener/{id}")]
        public async Task<ActionResult<IngresoDto>> GetById(int id)
        {
            var ingreso = await _ingresoService.ObtenerPorId(id);
            if (ingreso == null)
                return NotFound();

            return Ok(ingreso);
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<IngresoDto>> Create(IngresoDto dto)
        {
            var ingreso = await _ingresoService.Agregar(dto);
            return CreatedAtAction(nameof(GetById), new { id = ingreso.Id }, ingreso);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<ActionResult> Update(int id, IngresoDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var updated = await _ingresoService.Actualizar(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _ingresoService.Eliminar(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
