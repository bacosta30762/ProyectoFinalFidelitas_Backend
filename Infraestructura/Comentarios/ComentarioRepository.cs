using Aplicacion.DataBase;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Comentarios
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly DatabaseContext _context;

        public ComentarioRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AgregarAsync(Comentario comentario)
        {
            await _context.Comentarios.AddAsync(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task<Comentario> ObtenerPorIdAsync(int id)
        {
            return await _context.Comentarios.FindAsync(id);
        }

        public async Task<List<Comentario>> ObtenerPorUsuarioAsync(string usuarioId)
        {
            return await _context.Comentarios
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task ActualizarAsync(Comentario comentario)
        {
            _context.Comentarios.Update(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Comentario comentario)
        {
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
        }
    }
}

