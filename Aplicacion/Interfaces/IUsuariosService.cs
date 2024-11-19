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
        Task<Resultado> DesactivarUsuarioAsync(string cedula);
        Task<Resultado> AsignarRolAsync(AsignarRolDto dto);
        Task<Usuario?> ObtenerPorCedulaAsync(string cedula);
        Task<Resultado> ActivarUsuarioAsync(string cedula);
        Task<Resultado> GenerarTokenRecuperarPassword(RecuperarPasswordDto dto);
        Task<Resultado> RestablecerPasswordAsync(RestablecerPasswordDto dto);
        string? ObtenerUsuarioIdAutenticado();
        Task<Resultado<UsuarioDto>> ObtenerInformacionUsuario(string cedula);
        Task<List<ListaUsuarioDto>> ObtenerUsuariosAsync();
        Task<Resultado<RespuestaLoginDto>> LoginAdminAsync(LoginDto loginDto);
    }
}