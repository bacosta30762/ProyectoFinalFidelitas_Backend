using Aplicacion.DataBase;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Articulos
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly DatabaseContext _context;

        public ArticuloRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Articulo>> ObtenerTodos() =>
        await _context.Articulos.Include(a => a.Categoria).ToListAsync();

        public async Task<Articulo> ObtenerPorId(int id) =>
            await _context.Articulos.Include(a => a.Categoria)
                                    .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<List<Articulo>> ObtenerPorCategoria(int categoriaId)
        {
            return await _context.Articulos
                .Include(a => a.Categoria)
                .Where(a => a.CategoriaId == categoriaId) // Filtrar por categoría
                .ToListAsync();
        }

        public async Task<Articulo> Agregar(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task<bool> Actualizar(Articulo articulo)
        {
            _context.Articulos.Update(articulo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null) return false;

            _context.Articulos.Remove(articulo);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
