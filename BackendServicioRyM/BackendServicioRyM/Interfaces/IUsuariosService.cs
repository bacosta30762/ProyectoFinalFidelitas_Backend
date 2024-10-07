using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IUsuariosService
    {
        Task<Resultado> AgregarUsuarioAsync(AgregarUsuarioDto dto);
        Task<Resultado<RespuestaLoginDto>> LoginAsync(LoginDto loginDto);
        Task<Resultado> ActualizarUsuarioAsync(ActualizarUsuarioDto dto);
        Task<Resultado> EliminarUsuarioAsync(string cedula);
        Task<Resultado> AsignarRolAsync(AsignarRolDto dto);
        Task<Usuario?> ObtenerPorCedulaAsync(string cedula);
    }
}