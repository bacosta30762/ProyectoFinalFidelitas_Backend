using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _articuloService.ObtenerTodos());
        }

        [HttpGet("ListarPorCategoria/{categoriaId}")]
        public async Task<IActionResult> ObtenerPorCategoria(int categoriaId)
        {
            var articulos = await _articuloService.ObtenerPorCategoria(categoriaId);
            return Ok(articulos);
        }

        [HttpGet("Obtener{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var articulo = await _articuloService.ObtenerPorId(id);
            if (articulo == null) return NotFound();
            return Ok(articulo);
        }

        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] CrearArticuloDto dto)
        {
            var articulo = await _articuloService.Agregar(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = articulo.Id }, articulo);
        }

        [HttpPut("Actualizar{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarArticuloDto dto)
        {
            if (id != dto.Id) return BadRequest("El ID no coincide.");
            var resultado = await _articuloService.Actualizar(dto);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("Eliminar{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _articuloService.Eliminar(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}

