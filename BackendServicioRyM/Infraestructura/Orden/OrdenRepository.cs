using Aplicacion.DataBase;
using Dominio.Repositorios;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Ordenes
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly DatabaseContext _context;

        public OrdenRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Orden> ObtenerPorNumeroAsync(int numeroOrden)
        {
            return await _context.Ordenes
                .Include(o => o.MecanicoAsignado)
                .FirstOrDefaultAsync(o => o.NumeroOrden == numeroOrden);
        }

        public async Task ActualizarAsync(Orden orden)
        {
            _context.Ordenes.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task CrearAsync(Orden orden)
        {
            await _context.Ordenes.AddAsync(orden);
            await _context.SaveChangesAsync();
        }
    }
}
