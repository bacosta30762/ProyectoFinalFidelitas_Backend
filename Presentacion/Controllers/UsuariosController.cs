﻿using Aplicacion.Interfaces;
using Aplicacion.Servicios;
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

            //Enviar lista de los errores obtenidos
            if (!resultado.FueExitoso)
            {
                return BadRequest(resultado.Errores);
            }

            return Ok(new { Mensaje = "Usuario registrado exitosamente." });
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

        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin(LoginDto loginDto)
        {
            var respuestalogin = await _usuariosService.LoginAdminAsync(loginDto);
            if (!respuestalogin.FueExitoso)
            {
                return Unauthorized(respuestalogin.Errores);
            }

            return Ok(respuestalogin.Valor);
        }

        [HttpPut("Actualizar/{cedula}")]
        public async Task<IActionResult> ActualizarUsuario([FromRoute]string cedula, [FromBody]ActualizarUsuarioDto dto)
        {
            var resultado = await _usuariosService.ActualizarUsuarioAsync(dto);
            return Ok(resultado);
        }

        [HttpPut("Deseactivar")]
        public async Task<IActionResult> DesactivarUsuario(string cedula)
        {
            var usuario = await _usuariosService.ObtenerPorCedulaAsync(cedula); 
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            var resultado = await _usuariosService.DesactivarUsuarioAsync(usuario.Cedula);
            return Ok(resultado);
        }

        [HttpPut("Activar")]
        public async Task<IActionResult> ActivarUsuario(string cedula)
        {
            var usuario = await _usuariosService.ObtenerPorCedulaAsync(cedula);
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            var resultado = await _usuariosService.ActivarUsuarioAsync(usuario.Cedula);
            return Ok(resultado);
        }

        [HttpPost("AsignarRol")]
        public async Task<IActionResult> AsignarRol(AsignarRolDto dto)
        {
            var resultado = await _usuariosService.AsignarRolAsync(dto);
            return Ok(resultado);
        }

        [HttpPost("RecuperarContraseña")]
        public async Task<IActionResult> RecuperarContraseñaToken(RecuperarPasswordDto dto)
        {
            var resultado = await _usuariosService.GenerarTokenRecuperarPassword(dto);

            if (!resultado.FueExitoso)
            {
                return BadRequest(resultado.Errores);
            }

            return Ok(new { mensaje = "Token de recuperación enviado al correo" });
        }

        [HttpPost("restablecer-password")]
        public async Task<IActionResult> RestablecerPassword(RestablecerPasswordDto dto)
        {
            var resultado = await _usuariosService.RestablecerPasswordAsync(dto);

            if (!resultado.FueExitoso)
            {
                return BadRequest(resultado.Errores);
            }

            return Ok("La contraseña ha sido restablecida con éxito");
        }

        [HttpGet("ObtenerUsuario/{cedula}")]
        public async Task<IActionResult> ObtenerUsuarioPorCedula(string cedula)
        {
            try
            {
                var usuario = await _usuariosService.ObtenerInformacionUsuario(cedula);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("lista-usuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuariosService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        }
    }
}
