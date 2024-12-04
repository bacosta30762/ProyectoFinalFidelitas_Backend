using Microsoft.AspNetCore.Mvc;
using Aplicacion.Servicios;
using Aplicacion.Dominio.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicacion.Interfaces;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentariosyValoracionesController : ControllerBase
    {
        private readonly ComentariosValoracionesService _comentariosyValoracionesService;

        public ComentariosyValoracionesController(ComentariosValoracionesService comentariosyValoracionesService)
        {
            _comentariosyValoracionesService = comentariosyValoracionesService;
        }

        // Endpoint para obtener todos los comentarios y valoraciones por servicio
        [HttpGet("{servicioId}")]
        public async Task<IActionResult> GetByServicio(int servicioId)
        {
            var comentariosyValoraciones = await _comentariosyValoracionesService.ObtenerComentariosyValoracionesPorServicioAsync(servicioId);
            return Ok(comentariosyValoraciones);
        }

        // Endpoint para agregar un nuevo comentario y valoración
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComentariosValoraciones comentariosValoraciones)
        {
            if (comentariosValoraciones == null)
            {
                return BadRequest("El objeto ComentariosyValoraciones no puede ser nulo.");
            }

            await _comentariosValoracionesService.AgregarComentarioValoracionAsync(comentariosValoraciones);
            return CreatedAtAction(nameof(GetByServicio), new { servicioId = comentariosValoraciones.ServicioId }, comentariosValoraciones);
        }
    }
}
