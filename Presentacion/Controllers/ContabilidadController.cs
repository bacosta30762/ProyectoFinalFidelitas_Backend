using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Entidades.Contabilidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContabilidadController : Controller
    {
        private readonly IContabilidadService _contabilidadService;

        public ContabilidadController(IContabilidadService contabilidadService)
        {
            _contabilidadService = contabilidadService;
        }

        // Métodos para **Egresos**

        [HttpPost("CrearEgreso")]
        public async Task<IActionResult> AgregarEgresoAsync(Egreso egreso)
        {
            var result = await _contabilidadService.AgregarEgresoAsync(egreso);
            return Ok(result);
        }

        [HttpGet("ObtenerEgresos")]
        public async Task<IActionResult> ObtenerEgresos()
        {
            var result = await _contabilidadService.ObtenerEgresosAsync();
            return Ok(result);
        }

        [HttpGet("ObtenerEgresoPorIdAsync")]
        public async Task<IActionResult> ObtenerEgresoPorIdAsync(int id)
        {
            var result = await _contabilidadService.ObtenerEgresoPorIdAsync(id);
            return Ok(result);
        }
        [HttpDelete("EliminarEgreso/{id}")]
        public async Task<IActionResult> EliminarEgreso(int id)
        {
            var success = await _contabilidadService.EliminarEgresoAsync(id);
            return success ? Ok() : NotFound();
        }

        // Métodos para **Ingresos**

        [HttpPost("CrearIngreso")]
        public async Task<IActionResult> AgregarIngresoAsync(Ingreso ingreso)
        {
            var result = await _contabilidadService.AgregarIngresoAsync(ingreso);
            return Ok(result);
        }

        [HttpGet("ObtenerIngresos")]
        public async Task<IActionResult> ObtenerIngresos()
        {
            var result = await _contabilidadService.ObtenerIngresosAsync();
            return Ok(result);
        }

        [HttpGet("ObtenerIngresoPorIdAsync")]
        public async Task<IActionResult> ObtenerIngresoPorIdAsync(int id)
        {
            var result = await _contabilidadService.ObtenerIngresoPorIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("EliminarIngreso/{id}")]
        public async Task<IActionResult> EliminarIngreso(int id)
        {
            var success = await _contabilidadService.EliminarIngresoAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
