using Aplicacion.Usuarios.Dtos;

namespace Aplicacion.Interfaces
{
    public interface IEgresoService
    {
        Task<bool> Actualizar(EgresoDto dto);
        Task<EgresoDto> Agregar(EgresoDto dto);
        Task<bool> Eliminar(int id);
        Task<EgresoDto> ObtenerPorId(int id);
        Task<List<EgresoDto>> ObtenerTodos();
    }
}
