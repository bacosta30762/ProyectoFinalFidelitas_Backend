using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MecanicosController : ControllerBase
    {
        private readonly IMecanicoService _mecanicoService;

        public MecanicosController(IMecanicoService mecanicoService)
        {
            _mecanicoService = mecanicoService;
        }

        [HttpPost("asignar-servicios/{usuarioId}")]
        public async Task<IActionResult> AsignarServiciosAMecanico(string usuarioId, [FromBody] List<int> servicioIds)
        {
            if (string.IsNullOrEmpty(usuarioId))
            {
                return BadRequest(new { Error = "El usuarioId es requerido." });
            }

            if (servicioIds == null || servicioIds.Count == 0)
            {
                return BadRequest(new { Error = "Debe proporcionar al menos un servicioId." });
            }

            var resultado = await _mecanicoService.AsignarServiciosAsync(usuarioId, servicioIds);

            if (!resultado.FueExitoso)
            {
                return BadRequest(resultado.Errores);
            }

            return Ok("Servicios asignados con éxito.");
        }

        /*[HttpPost("asignar-servicios")]
        public async Task<IActionResult> AsignarServiciosAMecanico(string usuarioId, [FromBody] List<int> servicioIds)
        {
            var resultado = await _mecanicoService.AsignarServiciosAsync(usuarioId, servicioIds);

            if (!resultado.FueExitoso)
            {
                return BadRequest(resultado.Errores);
            }

            return Ok("Servicios asignados con éxito.");
        }*/

        [HttpGet("obtener-mecanicos")]
        public async Task<IActionResult> ObtenerMecanicos()
        {
            var mecanicos = await _mecanicoService.ObtenerMecanicosAsync();
            return Ok(mecanicos);
        }
    }
}
