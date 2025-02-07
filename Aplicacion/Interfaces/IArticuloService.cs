using Aplicacion.Usuarios.Dtos;
using Dominio.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IArticuloService
    {
        Task<Articulo> Agregar(CrearArticuloDto dto);
        Task<bool> Actualizar(ActualizarArticuloDto dto);
        Task<bool> Eliminar(int id);
        Task<List<ArticuloDto>> ObtenerTodos();
        Task<ArticuloDto> ObtenerPorId(int id);
        Task<List<ArticuloDto>> ObtenerPorCategoria(int categoriaId);
    }
}
