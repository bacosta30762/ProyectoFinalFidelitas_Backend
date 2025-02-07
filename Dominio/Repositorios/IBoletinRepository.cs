using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IBoletinRepository
    {
        Task<Boletin> CrearAsync(Boletin boletin);
        Task<List<Boletin>> ObtenerTodosAsync();
        Task<Boletin> ModificarAsync(Boletin boletin);
        Task<bool> EliminarAsync(int id);
    }
}
