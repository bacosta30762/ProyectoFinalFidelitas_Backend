using Aplicacion.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MarketingController : Controller
    {
        private readonly IMarketingService _marketingService;

        public MarketingController(IMarketingService marketingService)
        {
            _marketingService = marketingService;
        }



        [HttpPost("CrearBoletin")]
       
        public async Task<IActionResult> CrearBoletin([FromBody] Boletin boletin)
        {
            var result = await _marketingService.CrearBoletinAsync(boletin);
            return Ok(result);
        }

        [HttpGet("ObtenerBoletines")]

        public async Task<IActionResult> ObtenerBoletines()
        {
            var result = await _marketingService.ObtenerBoletinesAsync();
            return Ok(result);
        }

        [HttpPut("ModificarBoletin")]
       
        public async Task<IActionResult> ModificarBoletin([FromBody] Boletin boletin)
        {
            var result = await _marketingService.ModificarBoletinAsync(boletin);
            return Ok(result);
        }

        [HttpDelete("EliminarBoletin/{id}")]
       
        public async Task<IActionResult> EliminarBoletin(int id)
        {
            var success = await _marketingService.EliminarBoletinAsync(id);
            return success ? Ok() : NotFound();
        }
        [HttpPost("CrearResena")]
        public async Task<IActionResult> CrearResena([FromBody] Resena resena)
        {
            var result = await _marketingService.CrearResenaAsync(resena);
            return Ok(result);
        }

        [HttpGet("ObtenerResenas")]
        public async Task<IActionResult> ObtenerResenas()
        {
            var result = await _marketingService.ObtenerResenasAsync();
            return Ok(result);
        }
        [HttpPut("ModificarResena/{id}")]
        public async Task<IActionResult> ModificarResena(int id, [FromBody] Resena resena)
        {
            if (id != resena.Id)
                return BadRequest("El ID de la resena no coincide con el ID en la URL.");

            var resultado = await _marketingService.ModificarResenaAsync(resena);
            return Ok(resultado);
        }

        [HttpDelete("EliminarResena/{id}")]
        public async Task<IActionResult> EliminarResena(int id)
        {
            var resultado = await _marketingService.EliminarResenaAsync(id);
            if (!resultado)
                return NotFound($"No se encontró la resena con ID {id}.");

            return Ok($"Resena con ID {id} eliminada exitosamente.");
        }

        [HttpPost("CrearSuscripcion")]
        public async Task<IActionResult> CrearSuscripcion([FromBody] Suscripcion suscripcion)
        {
            var result = await _marketingService.CrearSuscripcionAsync(suscripcion);
            return Ok(result);
        }

        [HttpGet("ObtenerSuscripciones")]
        public async Task<IActionResult> ObtenerSuscripciones()
        {
            var result = await _marketingService.ObtenerSuscripcionesAsync();
            return Ok(result);
        }

        [HttpPut("ModificarSuscripcion/{id}")]
        public async Task<IActionResult> ModificarSuscripcion(int id, [FromBody] Suscripcion suscripcion)
        {
            if (id != suscripcion.Id)
                return BadRequest("El ID de la suscripcion no coincide con el ID en la URL.");

            var resultado = await _marketingService.ModificarSuscripcionAsync(suscripcion);
            return Ok(resultado);
        }

        [HttpDelete("EliminarSuscripcion/{id}")]
        public async Task<IActionResult> EliminarSuscripcion(int id)
        {
            var resultado = await _marketingService.EliminarSuscripcionAsync(id);
            if (!resultado)
                return NotFound($"No se encontró la suscripcion con ID {id}.");

            return Ok($"Suscripcion con ID {id} eliminada exitosamente.");
        }
    }
}
