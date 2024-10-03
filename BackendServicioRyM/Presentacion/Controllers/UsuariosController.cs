using Aplicacion.Servicios;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> CrearUsuario(AgregarUsuarioDto dto)
        {
            var resultado = await _usuariosService.AgregarUsuarioAsync(dto);

            return Ok(resultado);
        }
    }
}
