﻿using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using AutoMapper;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Interfaces;
using FluentValidation;

namespace Aplicacion.Servicios
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuarioRepository _usuariosRepository;
        private readonly IValidator<AgregarUsuarioDto> _validador;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IRoleRepository _roleRepository;
        private readonly IValidator<ActualizarUsuarioDto> _validadorActualizar;
        private readonly IValidator<RecuperarPasswordDto> _validadorRecuperarPassword;
        private readonly IEnviadorCorreos _enviadorCorreos;
        private readonly IValidator<RestablecerPasswordDto> _validadorRestablecerPassword;


        public UsuariosService(IUsuarioRepository usuariosRepository, IValidator<AgregarUsuarioDto> validador, IMapper mapper, IJwtService jwtService, IRoleRepository roleRepository, IValidator<ActualizarUsuarioDto> validadorActualizar, IEnviadorCorreos enviadorCorreos, IValidator<RecuperarPasswordDto> validadorRecuperarPassword, IValidator<RestablecerPasswordDto> validadorRestablecerPassword)
        {
            _usuariosRepository = usuariosRepository;
            _validador = validador;
            _mapper = mapper;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
            _validadorActualizar = validadorActualizar;
            _enviadorCorreos = enviadorCorreos;
            _validadorRecuperarPassword = validadorRecuperarPassword;
            _validadorRestablecerPassword = validadorRestablecerPassword;
        }

        //Agregar Usuario
        public async Task<Resultado> AgregarUsuarioAsync(AgregarUsuarioDto dto)
        {
            var validacion = await _validador.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return Resultado.Fallido(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var usuario = _mapper.Map<Usuario>(dto);
            usuario.Activo = true;
            var resultado = await _usuariosRepository.RegistrarUsuarioAsync(usuario, dto.Contraseña);

            return resultado;

        }

        //Iniciar Sesión
        public async Task<Resultado<RespuestaLoginDto>> LoginAsync(LoginDto loginDto)
        {
            var respuestausuario = await _usuariosRepository.AutenticarUsuarioAsync(loginDto.Email, loginDto.Password);
            if (!respuestausuario.FueExitoso)
            {
                return Resultado<RespuestaLoginDto>.Fallido(respuestausuario.Errores);
            }
            var roles = await _usuariosRepository.ObtenerRolesAsync(respuestausuario.Valor);
            var token = _jwtService.GenerarToken(respuestausuario.Valor, roles);

            return Resultado<RespuestaLoginDto>.Exitoso(new RespuestaLoginDto(token));
        }

        public async Task<Usuario?> ObtenerPorCedulaAsync(string cedula)
        {
            return await _usuariosRepository.ObtenerPorCedulaAsync(cedula);
        }

        // Actualizar un usuario
        public async Task<Resultado> ActualizarUsuarioAsync(ActualizarUsuarioDto dto)
        {
            
            var validacion = await _validadorActualizar.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return Resultado.Fallido(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var usuarioExistente = await _usuariosRepository.ObtenerPorCedulaAsync(dto.Cedula);
            if (usuarioExistente == null)
            {
                return Resultado.Fallido(new[] { "Usuario no encontrado." });
            }

            _mapper.Map(dto, usuarioExistente); 

            var result = await _usuariosRepository.ActualizarAsync(usuarioExistente);
            return result.FueExitoso ? Resultado.Exitoso() : Resultado.Fallido(new[] { "No se pudo actualizar el usuario." });
        }

        // Desactivar un usuario por cédula
        public async Task<Resultado> DesactivarUsuarioAsync(string cedula)
        {
            var user = await _usuariosRepository.ObtenerPorCedulaAsync(cedula);
            if (user != null)
            {
                user.Activo = false;
                var result = await _usuariosRepository.ActualizarAsync(user);
                if (result.FueExitoso)
                {
                    return Resultado.Exitoso();
                }
                return Resultado.Fallido(new[] { "No se pudo desactivar el usuario." });
            }
            return Resultado.Fallido(new[] { "Usuario no encontrado." });
        }

        // Activar un usuario por cédula
        public async Task<Resultado> ActivarUsuarioAsync(string cedula)
        {
            var user = await _usuariosRepository.ObtenerPorCedulaAsync(cedula);
            if (user != null)
            {
                user.Activo = true;
                var result = await _usuariosRepository.ActualizarAsync(user);
                if (result.FueExitoso)
                {
                    return Resultado.Exitoso();
                }
                return Resultado.Fallido(new[] { "No se pudo activar el usuario." });
            }
            return Resultado.Fallido(new[] { "Usuario no encontrado." });
        }

        // Asignar uno o varios roles a un usuario por cédula
        public async Task<Resultado> AsignarRolAsync(AsignarRolDto dto)
        {
            var user = await _usuariosRepository.ObtenerPorCedulaAsync(dto.Cedula);

            if (user == null)
            {
                return Resultado.Fallido(new[] { "Usuario no encontrado" });
            }

            var errores = new List<string>();

            foreach (var roleName in dto.RoleNames)
            {
                var resultadoRol = await _usuariosRepository.AsignarRolAsync(user.Cedula, roleName);
                if (!resultadoRol.FueExitoso)
                {
                    errores.Add($"Error al asignar el rol {roleName}: {string.Join(", ", resultadoRol.Errores)}");
                }
            }

            if (errores.Any())
            {
                return Resultado.Fallido(errores);
            }

            return Resultado.Exitoso();
        }

        // Recuperar contraseña (Generar Token)
        public async Task<Resultado> GenerarTokenRecuperarPassword(RecuperarPasswordDto dto)
        {
            var validacion = await _validadorRecuperarPassword.ValidateAsync(dto);

            if (!validacion.IsValid)
            {
                return Resultado.Fallido(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var usuario = await _usuariosRepository.ObtenerPorCorreoAsync(dto.Correo);
            if (usuario == null)
            {
                return Resultado.Fallido(new[] { "El correo no se encuentra asignado a algún usuario" }); ;
            }

            var token = await _usuariosRepository.GenerarTokenRecuperacionPasswordAsync(usuario);
            string mensajeCorreo = $"Su token de recuperación es: {token}";

            await _enviadorCorreos.SendEmailAsync(usuario.Email, "Recuperar Contraseña", mensajeCorreo);

            return Resultado.Exitoso();
        }

        //Restablecer contraseña
        public async Task<Resultado> RestablecerPasswordAsync(RestablecerPasswordDto dto)
        {
            var validacion = await _validadorRestablecerPassword.ValidateAsync(dto);

            if (!validacion.IsValid)
            {
                return Resultado.Fallido(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var usuario = await _usuariosRepository.ObtenerPorCorreoAsync(dto.Correo);
            if (usuario == null)
            {
                return Resultado.Fallido(new[] { "El correo no se encuentra asignado a algún usuario" });
            }

            var resultado = await _usuariosRepository.RestablecerPasswordAsync(usuario, dto.Token, dto.NuevaPassword);

            if (!resultado.Succeeded)
            {
                return Resultado.Fallido(resultado.Errors.Select(e => e.Description));
            }

            return Resultado.Exitoso();
        }
    }
}




