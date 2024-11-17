using Aplicacion.Usuarios.Dtos;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacion.Usuarios.Mapeadores
{
    public class MapeadorUsuarios : Profile
    {
        public MapeadorUsuarios()
        {
            CreateMap<AgregarUsuarioDto, Usuario>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Correo));

            CreateMap<ActualizarUsuarioDto, Usuario>()
                .ForMember(dest => dest.Cedula, opt => opt.MapFrom(src => src.Cedula))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Correo));
        }

    }
}
