using Dominio.Comun;
using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Dominio.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Resultado<Usuario>> AutenticarUsuarioAsync(string correo, string password);
        Task<IEnumerable<string>> ObtenerRolesAsync(Usuario usuario);
        Task<Resultado> RegistrarUsuarioAsync(Usuario usuario, string password);
        Task<Usuario?> ObtenerPorCedulaAsync(string cedula);
        Task<Resultado> ActualizarAsync(Usuario usuario);
        Task<Resultado> EliminarAsync(Usuario usuario);
        Task<Resultado> AsignarRolAsync(string cedula, string roleName);
        Task<Usuario?> ObtenerPorCorreoAsync(string correo);
        Task<string?> GenerarTokenRecuperacionPasswordAsync(Usuario usuario);
        Task<IdentityResult> RestablecerPasswordAsync(Usuario usuario, string token, string password);
        Task<IEnumerable<Usuario>> ObtenerUsuariosPorRolAsync(string roleName);
    }
}
