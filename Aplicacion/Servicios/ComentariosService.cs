using Aplicacion.Interfaces;
using Aplicacion.Ordenes;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Repositorios;

namespace Aplicacion.Servicios
{
    public class ComentariosService : IComentariosService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ComentariosService(IComentarioRepository comentarioRepository, IUsuarioRepository usuarioRepository)
        {
            _comentarioRepository = comentarioRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<int> CrearComentarioAsync(CrearComentarioDto comentarioDto, string usuarioId)
        {
            // Aquí el usuarioId debería ser el que provenga de los claims del usuario autenticado
            // Si no lo recibimos como parámetro, lo podemos obtener en este punto
            if (string.IsNullOrEmpty(usuarioId))
            {
                throw new UnauthorizedAccessException("Usuario no autenticado");
            }

            var comentario = new Comentario
            {
                Texto = comentarioDto.Texto,
                Puntuacion = comentarioDto.Puntuacion,
                UsuarioId = usuarioId,
                FechaCreacion = DateTime.UtcNow
            };

            await _comentarioRepository.AgregarAsync(comentario);
            return comentario.Id;
        }

        public async Task EditarComentarioAsync(int id, CrearComentarioDto comentarioDto)
        {
            var comentario = await _comentarioRepository.ObtenerPorIdAsync(id);
            if (comentario == null) throw new Exception("Comentario no encontrado");

            comentario.Texto = comentarioDto.Texto;
            comentario.Puntuacion = comentarioDto.Puntuacion;

            await _comentarioRepository.ActualizarAsync(comentario);
        }

        public async Task EliminarComentarioAsync(int id)
        {
            var comentario = await _comentarioRepository.ObtenerPorIdAsync(id);
            if (comentario == null) throw new Exception("Comentario no encontrado");

            await _comentarioRepository.EliminarAsync(comentario);
        }

        public async Task<List<ComentarioDto>> ObtenerComentariosPorUsuarioAsync(string usuarioId)
        {
            var comentarios = await _comentarioRepository.ObtenerPorUsuarioAsync(usuarioId);

            return comentarios.Select(c => new ComentarioDto
            {
                Id = c.Id,
                Texto = c.Texto,
                Puntuacion = c.Puntuacion,
                UsuarioId = c.UsuarioId,
                NombreUsuario = c.Usuario?.Nombre,  // Asegúrate de que la entidad Usuario tenga el campo Nombre
                FechaCreacion = c.FechaCreacion
            }).ToList();
        }
    }
}
