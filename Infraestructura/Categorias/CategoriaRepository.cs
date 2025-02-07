using Aplicacion.DataBase;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Categorias
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DatabaseContext _context;

        public CategoriaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ObtenerTodas() =>
            await _context.Categorias.ToListAsync();

        public async Task<Categoria> ObtenerPorId(int id) =>
            await _context.Categorias.Include(c => c.Articulos)
                                      .FirstOrDefaultAsync(c => c.Id == id);
    }
}
