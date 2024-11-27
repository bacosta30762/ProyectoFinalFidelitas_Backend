using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenService _ordenService;
        private readonly IUsuariosService _usuariosService;
        private readonly IMecanicoService _mecanicoService;

        public OrdenesController(IOrdenService ordenService, IUsuariosService usuariosService, IMecanicoService mecanicoService)
        {
            _ordenService = ordenService;
            _usuariosService = usuariosService;
            _mecanicoService = mecanicoService;
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

        [HttpDelete("Eliminarorden{id}")]
        public async Task<IActionResult> EliminarOrden(int id)
        {
            try
            {
                await _ordenService.EliminarOrdenPorIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("lista-mecanicos")]
        public async Task<IActionResult> ObtenerMecanicosDisponibles()
        {
            var mecanicos = await _mecanicoService.ObtenerMecanicosDisponiblesAsync();

            if (mecanicos == null || !mecanicos.Any())
            {
                return NotFound("No se encontraron mecánicos disponibles.");
            }

            return Ok(mecanicos);
        }

        [HttpPut("asignar-mecanico")]
        public async Task<IActionResult> AsignarMecanicoAsync(int numeroOrden, string mecanicoId)
        {
            var resultado = await _ordenService.AsignarMecanicoAsync(numeroOrden, mecanicoId);

            if (!resultado.FueExitoso)
            {
                return BadRequest(resultado.Errores);
            }

            return Ok("Mecánico asignado con éxito a la orden.");
        }

        [HttpGet("listar-ordenes-usuario")]
        public async Task<IActionResult> ObtenerOrdenesUsuario()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized("Usuario no autenticado.");

            var resultado = await _ordenService.listarOrdenesUsuarioAsync(usuarioId);

            if (!resultado.FueExitoso)
                return NotFound(resultado.Errores.FirstOrDefault());

            return Ok(resultado.Valor);
        }

        [HttpGet("listar-todas-ordenes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ObtenerTodasLasOrdenes()
        {
            var resultado = await _ordenService.ListarTodasLasOrdenesAsync();

            if (!resultado.FueExitoso)
                return NotFound(resultado.Errores.FirstOrDefault());

            return Ok(resultado.Valor);
        }

        [HttpGet("listar-ordenes-asignadas")]
        [Authorize(Roles = "Mecanico")]
        public async Task<IActionResult> ObtenerOrdenesAsignadas()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized("Usuario no autenticado.");

            var resultado = await _ordenService.ListarOrdenesPorMecanicoAsync(usuarioId);

            if (!resultado.FueExitoso)
                return NotFound(resultado.Errores.FirstOrDefault());

            return Ok(resultado.Valor);
        }

        [HttpPost("bloquear-dia")]
        public async Task<IActionResult> BloquearDia([FromBody] BloquearDiaDto request)
        {
            try
            {
                await _ordenService.BloquearDiaAsync(request);
                return Ok(new { mensaje = "Día bloqueado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("desbloquear-dia")]
        public async Task<IActionResult> DesbloquearDia([FromBody] BloquearDiaDto request)
        {
            try
            {
                await _ordenService.DesbloquearDiaAsync(request);
                return Ok(new { mensaje = "Día desbloqueado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
