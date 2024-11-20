using Aplicacion.DataBase;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Inventario
{
    internal class InventarioRepository : IInventarioRepository
    {
        private readonly DatabaseContext _context;

        public InventarioRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventario>> GetAllAsync()
        {
            return await _context.Inventarios.ToListAsync();
        }

        public async Task<Inventario> GetByIdAsync(int id)
        {
            return await _context.Inventarios.FindAsync(id);
        }

        public async Task AddAsync(Inventario inventario)
        {
            await _context.Inventarios.AddAsync(inventario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Inventario inventario)
        {
            _context.Inventarios.Update(inventario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Inventarios.FindAsync(id);
            if (entity != null)
            {
                _context.Inventarios.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
