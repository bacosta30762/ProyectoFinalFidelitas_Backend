using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto) 
        {
            var respuestalogin = await _usuariosService.LoginAsync(loginDto);
            if (!respuestalogin.FueExitoso)
            {
                return Unauthorized(respuestalogin.Errores);
            }

            return Ok(respuestalogin.Valor); 
        }

        [HttpGet("Privado")]
        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> Privado()
        {
           return Ok();
        }

        [HttpPut("Actualizar/{cedula}")]
        public async Task<IActionResult> ActualizarUsuario([FromRoute]string cedula, [FromBody]ActualizarUsuarioDto dto)
        {
            var resultado = await _usuariosService.ActualizarUsuarioAsync(dto);
            return Ok(resultado);
        }

        [HttpDelete("Eliminar")]
        public async Task<IActionResult> EliminarUsuario(string cedula)
        {
            var usuario = await _usuariosService.ObtenerPorCedulaAsync(cedula); 
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            var resultado = await _usuariosService.EliminarUsuarioAsync(usuario.Cedula);
            return Ok(resultado);
        }

        [HttpPost("AsignarRol")]
        public async Task<IActionResult> AsignarRol(AsignarRolDto dto)
        {
            var resultado = await _usuariosService.AsignarRolAsync(dto);
            return Ok(resultado);
        }

    }
}
