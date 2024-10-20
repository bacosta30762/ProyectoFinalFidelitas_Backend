using Dominio.Comun;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IMecanicoRepository
    {
        Task<Resultado> AgregarAsync(Mecanico mecanico);
    }
}
