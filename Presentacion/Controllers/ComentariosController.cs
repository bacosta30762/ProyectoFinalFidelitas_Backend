using Aplicacion.Interfaces;
using Aplicacion.Ordenes;
using Aplicacion.Usuarios.Dtos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly IComentariosService _comentariosService;

        public ComentariosController(IComentariosService comentariosService)
        {
            _comentariosService = comentariosService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearComentario([FromServices] UserManager<Usuario> userManager, CrearComentarioDto comentarioDto)
        {
            var usuario = await userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return Unauthorized("Usuario no autenticado");
            }

            var usuarioId = usuario.Id;
            var comentarioId = await _comentariosService.CrearComentarioAsync(comentarioDto, usuarioId);

            return Ok(new { Id = comentarioId });
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> EditarComentario(int id, CrearComentarioDto comentarioDto)
        {
            await _comentariosService.EditarComentarioAsync(id, comentarioDto);
            return Ok();
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarComentario(int id)
        {
            await _comentariosService.EliminarComentarioAsync(id);
            return Ok();
        }

        [HttpGet("obtenercomentariosusuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerComentariosPorUsuario(string usuarioId)
        {
            var comentarios = await _comentariosService.ObtenerComentariosPorUsuarioAsync(usuarioId);
            return Ok(comentarios);
        }

        [HttpGet("obtenercomentarios")]
        public async Task<IActionResult> ObtenerTodosLosComentarios()
        {
            var comentarios = await _comentariosService.ObtenerTodosLosComentariosAsync();
            return Ok(comentarios);
        }

        [HttpPost("responder/{id}")]
        public async Task<IActionResult> ResponderComentario(int id, [FromBody] ResponderComentarioDto respuestaDto)
        {
            await _comentariosService.ResponderComentarioAsync(id, respuestaDto);
            return Ok();
        }
    }
}
