using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IJwtRepository
    {
        string GenerarToken(Usuario usuario);
    }
}
