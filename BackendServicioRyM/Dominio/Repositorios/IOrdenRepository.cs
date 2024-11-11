using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IOrdenRepository
    {
        Task ActualizarAsync(Orden orden);
        Task CrearAsync(Orden orden);
        Task EliminarAsync(Orden orden);
        Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia);
        Task<Mecanico?> ObtenerMecanicoDisponibleAsync(int idServicio, DateOnly dia, int hora);
        Task<List<Orden>> ObtenerOrdenesPorClienteId(string clienteId);
        Task<Orden> ObtenerOrdenPorIdAsync(int id);
        Task<Orden> ObtenerPorNumeroAsync(int numeroOrden);
    }
}
