using Dominio.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IJwtService
    {
        string GenerarToken(Usuario usuario, IEnumerable<string> roles);
    }
}
