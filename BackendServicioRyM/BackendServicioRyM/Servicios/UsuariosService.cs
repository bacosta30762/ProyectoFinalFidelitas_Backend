using Aplicacion.Interfaces;
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

        public UsuariosService(IUsuarioRepository usuariosRepository, IValidator<AgregarUsuarioDto> validador, IMapper mapper, IJwtService jwtService)
        {
            _usuariosRepository = usuariosRepository;
            _validador = validador;
            _mapper = mapper;
            _jwtService = jwtService;
        }

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

    }


}
