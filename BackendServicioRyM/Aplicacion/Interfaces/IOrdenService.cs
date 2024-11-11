using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IOrdenService
    {
        Task<Resultado> AsignarMecanicoAsync(int numeroOrden, string mecanicoId);
        Task<Resultado> CrearOrdenAsync(CrearOrdenDto dto);
        Task EliminarOrdenPorIdAsync(int id);
        Task<Resultado<List<ListarOrdenDto>>> listarOrdenesUsuarioAsync(string usuarioId);
        Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia);
    }
}
