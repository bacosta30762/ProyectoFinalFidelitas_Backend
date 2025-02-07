using Aplicacion.Usuarios.Dtos;

namespace Aplicacion.Interfaces
{
    public interface IIngresoService
    {
        Task<bool> Actualizar(IngresoDto dto);
        Task<IngresoDto> Agregar(IngresoDto dto);
        Task<bool> Eliminar(int id);
        Task<IngresoDto> ObtenerPorId(int id);
        Task<List<IngresoDto>> ObtenerTodos();
    }
}
