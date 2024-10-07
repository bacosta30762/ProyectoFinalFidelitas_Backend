using Dominio.Comun;
using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Resultado<Usuario>> AutenticarUsuarioAsync(string correo, string password);
        Task<IEnumerable<string>> ObtenerRolesAsync(Usuario usuario);
        Task<Resultado> RegistrarUsuarioAsync(Usuario usuario, string password);
    }
}
