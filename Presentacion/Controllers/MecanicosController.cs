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

        [HttpPost("asignar-servicios")]
        public async Task<IActionResult> AsignarServiciosAMecanico(string usuarioId, [FromBody] List<int> servicioIds)
        {
            var resultado = await _mecanicoService.AsignarServiciosAsync(usuarioId, servicioIds);

            if (!resultado.FueExitoso)
            {
                return BadRequest(resultado.Errores);
            }

            return Ok("Servicios asignados con éxito.");
        }
    }
}
