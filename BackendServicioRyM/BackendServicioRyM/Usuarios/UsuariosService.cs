using Aplicacion.Servicios;
using Aplicacion.Usuarios.Dtos;
using AutoMapper;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Interfaces;
using FluentValidation;

namespace Aplicacion.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuarioRepository _usuariosRepository;
        private readonly IValidator<AgregarUsuarioDto> _validador;
        private readonly IMapper _mapper;

        public UsuariosService(IUsuarioRepository usuariosRepository, IValidator<AgregarUsuarioDto> validador, IMapper mapper)
        {
            _usuariosRepository = usuariosRepository;
            _validador = validador;
            _mapper = mapper;
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
    }


}
