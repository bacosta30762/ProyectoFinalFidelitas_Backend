using Dominio.Comun;
using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Resultado> RegistrarUsuarioAsync(Usuario usuario, string password);
    }
}
