using Aplicacion.DataBase;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Ingresos
{
    public class IngresoRepository : IIngresoRepository
    {
        private readonly DatabaseContext _context;

        public IngresoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Ingreso>> ObtenerTodos() =>
            await _context.Ingresos.ToListAsync();

        public async Task<Ingreso> ObtenerPorId(int id) =>
            await _context.Ingresos.FirstOrDefaultAsync(i => i.Id == id);

        public async Task<Ingreso> Agregar(Ingreso ingreso)
        {
            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();
            return ingreso;
        }

        public async Task<bool> Actualizar(Ingreso ingreso)
        {
            _context.Ingresos.Update(ingreso);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso == null) return false;

            _context.Ingresos.Remove(ingreso);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
