using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IAsignacionMecanicoService _asignacionMecanicoService;
        private readonly IOrdenService _ordenService;

        public OrdenesController(IAsignacionMecanicoService asignacionMecanicoService, IOrdenService ordenService)
        {
            _asignacionMecanicoService = asignacionMecanicoService;
            _ordenService = ordenService;
        }

        [HttpPost("Crear Orden")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CrearOrden(CrearOrdenDto dto)
        {
            var resultadoCreacion = await _ordenService.CrearOrdenAsync(dto);

            if (!resultadoCreacion.FueExitoso)
            {
                return BadRequest(resultadoCreacion.Errores);
            }

            // Asignación automática del mecánico
            var resultadoAsignacion = await _asignacionMecanicoService.AsignarMecanicoAsync(resultadoCreacion.Valor.NumeroOrden, dto.EspecialidadRequerida);

            if (!resultadoAsignacion.FueExitoso)
            {
                return BadRequest(resultadoAsignacion.Errores);
            }

            return Ok("Orden creada y mecánico asignado exitosamente.");
        }
    }
}
