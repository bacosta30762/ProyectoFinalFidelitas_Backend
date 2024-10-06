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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto) 
        {
            var token = await _usuariosService.LoginAsync(loginDto); 

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); 
            }

            return Ok(new { Token = token }); 
        }
    }
}
