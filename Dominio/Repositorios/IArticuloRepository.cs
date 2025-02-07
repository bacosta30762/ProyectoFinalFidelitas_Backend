using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IArticuloRepository
    {
        Task<List<Articulo>> ObtenerTodos();
        Task<Articulo> ObtenerPorId(int id);
        Task<Articulo> Agregar(Articulo articulo);
        Task<bool> Actualizar(Articulo articulo);
        Task<bool> Eliminar(int id);
        Task<List<Articulo>> ObtenerPorCategoria(int categoriaId);
    }
}
