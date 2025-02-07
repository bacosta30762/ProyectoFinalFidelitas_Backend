using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteFinancieroController : ControllerBase
    {
        private readonly IReporteFinancieroService _reporteFinancieroService;

        public ReporteFinancieroController(IReporteFinancieroService reporteFinancieroService)
        {
            _reporteFinancieroService = reporteFinancieroService;
        }

        [HttpGet]
        public async Task<ActionResult<ReporteFinancieroDto>> ObtenerReporte([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                return BadRequest("La fecha de inicio no puede ser mayor a la fecha de fin.");
            }

            var reporte = await _reporteFinancieroService.ObtenerReporte(fechaInicio, fechaFin);

            return Ok(reporte);
        }
    }
}
