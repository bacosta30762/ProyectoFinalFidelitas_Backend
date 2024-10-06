using Aplicacion.Servicios;
using Aplicacion.Usuarios.Dtos;
using AutoMapper;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Repositorios;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;

namespace Aplicacion.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuarioRepository _usuariosRepository;
        private readonly IValidator<AgregarUsuarioDto> _validador;
        private readonly IMapper _mapper;
        private readonly IAutenticacionRepository _autenticacionRepository;

        public UsuariosService(IUsuarioRepository usuariosRepository, IValidator<AgregarUsuarioDto> validador, IMapper mapper, IAutenticacionRepository autenticacionRepository)
        {
            _usuariosRepository = usuariosRepository;
            _validador = validador;
            _mapper = mapper;
            _autenticacionRepository = autenticacionRepository;
        }

        public async Task<Resultado> AgregarUsuarioAsync(AgregarUsuarioDto dto)
        {
            var validacion = await _validador.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return Resultado.Fallido(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var usuario = _mapper.Map<Usuario>(dto);
            var resultado = await _usuariosRepository.RegistrarUsuarioAsync(usuario, dto.Contraseña);

            return resultado;

        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            return await _autenticacionRepository.LoginAsync(loginDto.Email, loginDto.Password);
        }
    }


}
