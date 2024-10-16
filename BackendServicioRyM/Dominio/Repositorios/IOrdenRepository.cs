using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IOrdenRepository
    {
        Task ActualizarAsync(Orden orden);
        Task CrearAsync(Orden orden);
        Task<Orden> ObtenerPorNumeroAsync(int numeroOrden);
    }
}
