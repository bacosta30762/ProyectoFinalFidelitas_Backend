using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IComentarioRepository
    {
        Task AgregarAsync(Comentario comentario);
        Task<Comentario> ObtenerPorIdAsync(int id);
        Task<List<Comentario>> ObtenerPorUsuarioAsync(string usuarioId);
        Task ActualizarAsync(Comentario comentario);
        Task EliminarAsync(Comentario comentario);
        Task<List<Comentario>> ObtenerTodosAsync();
        Task<Comentario> ObtenerComentarioPorIdAsync(int id);
        Task ActualizarComentarioAsync(Comentario comentario);
    }
}
