using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ObtenerCategorias()
        {
            return Ok(await _categoriaService.ObtenerCategorias());
        }

        [HttpGet("Obtener/{id}")]
        public async Task<ActionResult<CategoriaDto>> ObtenerCategoriaPorId(int id)
        {
            var categoriaDto = await _categoriaService.ObtenerCategoriaPorId(id);
            if (categoriaDto == null) return NotFound();
            return Ok(categoriaDto);
        }
    }
}
