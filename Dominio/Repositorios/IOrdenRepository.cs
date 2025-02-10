using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IOrdenRepository
    {
        Task ActualizarAsync(Orden orden);
        Task BloquearDiaAsync(DateOnly dia);
        Task CrearAsync(Orden orden);
        Task DesbloquearDiaAsync(DateOnly dia);
        Task EliminarAsync(Orden orden);
        Task<List<DiaBloqueado>> ObtenerDiasBloqueadosAsync();
        Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia);
        Task<string?> ObtenerMecanicoDisponibleAsync(int idServicio, DateOnly dia, int hora);
        Task<List<Orden>> ObtenerOrdenesPorClienteId(string clienteId);
        Task<List<Orden>> ObtenerOrdenesPorMecanicoId(string mecanicoId);
        Task<Orden> ObtenerOrdenPorIdAsync(int id);
        Task<Orden> ObtenerPorNumeroAsync(int numeroOrden);
        Task<List<Orden>> ObtenerTodasLasOrdenes();
    }
}
