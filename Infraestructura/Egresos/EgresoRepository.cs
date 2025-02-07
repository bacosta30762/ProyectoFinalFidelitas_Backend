using Aplicacion.DataBase;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Egresos
{
    public class EgresoRepository : IEgresoRepository
    {
        private readonly DatabaseContext _context;

        public EgresoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Egreso>> ObtenerTodos() =>
            await _context.Egresos.ToListAsync();

        public async Task<Egreso> ObtenerPorId(int id) =>
            await _context.Egresos.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Egreso> Agregar(Egreso egreso)
        {
            _context.Egresos.Add(egreso);
            await _context.SaveChangesAsync();
            return egreso;
        }

        public async Task<bool> Actualizar(Egreso egreso)
        {
            _context.Egresos.Update(egreso);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var egreso = await _context.Egresos.FindAsync(id);
            if (egreso == null) return false;

            _context.Egresos.Remove(egreso);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
