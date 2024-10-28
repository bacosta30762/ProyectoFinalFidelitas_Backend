using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IOrdenService
    {
        Task<Resultado> CrearOrdenAsync(CrearOrdenDto dto);
        Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia);
    }
}
