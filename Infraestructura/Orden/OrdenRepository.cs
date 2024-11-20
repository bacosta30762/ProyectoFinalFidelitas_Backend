using Aplicacion.DataBase;
using Dominio.Entidades;
using Dominio.Repositorios;
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

        public async Task EliminarAsync(Orden orden)
        {
            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();
        }

        public async Task<Orden> ObtenerOrdenPorIdAsync(int id)
        {
            return await _context.Ordenes.FindAsync(id);
        }

        /*public async Task<Mecanico?> ObtenerMecanicoDisponibleAsync(int idServicio, DateOnly dia, int hora)
        {
            // Buscar el mecánico que puede atender el servicio y tiene menos órdenes en la fecha y hora especificadas
            var mecanicoDisponible = await _context.Mecanicos
                .Where(m => m.Servicios.Any(s => s.Id == idServicio)) // Mecánicos que pueden realizar el servicio
                .Where(m => !m.Ordenes.Any(o => o.Dia == dia && o.Hora == hora)) // Que no tengan una orden en ese día y hora
                .OrderBy(m => m.Ordenes.Count(o => o.Dia == dia)) // Ordenar por la cantidad de órdenes asignadas en el día
                .FirstOrDefaultAsync(); // Obtener el primer mecánico disponible

            return mecanicoDisponible;
        }*/

        public async Task<Mecanico?> ObtenerMecanicoDisponibleAsync(int idServicio, DateOnly dia, int hora)
        {
            // Buscar el mecánico que puede atender el servicio y no tiene órdenes asignadas para la fecha y hora especificadas
            var mecanicoDisponible = await _context.Mecanicos
                .Where(m => m.Servicios.Any(s => s.Id == idServicio)) // Mecánicos que pueden realizar el servicio
                .Where(m => !m.Ordenes
                    .Any(o => o.Dia == dia && o.Hora == hora && o.MecanicoAsignadoId == m.UsuarioId)) // Verificar que no tenga órdenes asignadas para esa hora y día
                .OrderBy(m => m.Ordenes.Count(o => o.Dia == dia)) // Ordenar por la cantidad de órdenes asignadas en el día
                .FirstOrDefaultAsync(); // Obtener el primer mecánico disponible

            return mecanicoDisponible;
        }

        public async Task<Orden?> ObtenerOrdenPorNumeroAsync(int numeroOrden)
        {
            return await _context.Ordenes.FindAsync(numeroOrden);
        }

        public async Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia)
        {
            int horaInicio = 8;
            int horaFin = 16;

            // Lista de todas las horas posibles
            var horas = Enumerable.Range(horaInicio, horaFin - horaInicio + 1).ToList();

            // Obtener los IDs de los mecánicos que pueden atender el servicio solicitado
            var mecanicoIds = await _context.Mecanicos
                .Where(m => m.Servicios.Any(s => s.Id == servicioId))
                .Select(m => m.UsuarioId)
                .ToListAsync();

            var horasDisponibles = new List<int>();

            foreach (var hora in horas)
            {
                // Verificar si hay algún mecánico disponible en esta hora
                bool hayDisponibilidad = await _context.Ordenes
                    .Where(o => o.ServicioId == servicioId && o.Dia == dia && o.Hora == hora)
                    .AnyAsync(o => !mecanicoIds.Contains(o.MecanicoAsignadoId));

                if (!hayDisponibilidad)
                {
                    horasDisponibles.Add(hora);
                }
            }

            return horasDisponibles;
        }

        public async Task<List<Orden>> ObtenerOrdenesPorClienteId(string clienteId)
        {
            return await _context.Ordenes
                .Include(o => o.MecanicoAsignado)
                    .ThenInclude(m => m.Usuario)
                .Include(o => o.Servicio)
                .Where(o => o.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<List<Orden>> ObtenerTodasLasOrdenes()
        {
            return await _context.Ordenes
                .Include(o => o.MecanicoAsignado)
                    .ThenInclude(m => m.Usuario)
                .Include(o => o.Servicio)
                .Include(o => o.Cliente)
                .ToListAsync();
        }

        public async Task<List<Orden>> ObtenerOrdenesPorMecanicoId(string mecanicoId)
        {
            return await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.Servicio)
                .Where(o => o.MecanicoAsignadoId == mecanicoId)
                .ToListAsync();
        }

    }
}

