using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenService _ordenService;
        private readonly IUsuariosService _usuariosService;

        public OrdenesController(IOrdenService ordenService, IUsuariosService usuariosService)
        {
            _ordenService = ordenService;
            _usuariosService = usuariosService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearOrdenAsync(CrearOrdenDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Los datos de la orden son inválidos.");
            }

            var resultado = await _ordenService.CrearOrdenAsync(dto);

            if (!resultado.FueExitoso)
            {
                return BadRequest(resultado.Errores);
            }

            return Ok("Orden creada con éxito.");
        }

        [HttpGet("horas-disponibles")]
        public async Task<IActionResult> ObtenerHorasDisponibles(int servicioId, DateOnly dia)
        {
            var horasDisponibles = await _ordenService.ObtenerHorasDisponiblesAsync(servicioId, dia);
            return Ok(horasDisponibles);
        }
    }
}
