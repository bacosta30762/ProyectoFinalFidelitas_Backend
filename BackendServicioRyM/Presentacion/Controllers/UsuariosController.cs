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
    }
}
