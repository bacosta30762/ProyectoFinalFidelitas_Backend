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

        public async Task<Mecanico?> ObtenerMecanicoDisponibleAsync(int idServicio, DateOnly dia, int hora)
        {
            // Buscar el mecánico que puede atender el servicio y tiene menos órdenes en la fecha y hora especificadas
            var mecanicoDisponible = await _context.Mecanicos
                .Where(m => m.Servicios.Any(s => s.Id == idServicio)) // Mecánicos que pueden realizar el servicio
                .Where(m => !m.Ordenes.Any(o => o.Dia == dia && o.Hora == hora)) // Que no tengan una orden en ese día y hora
                .OrderBy(m => m.Ordenes.Count(o => o.Dia == dia)) // Ordenar por la cantidad de órdenes asignadas en el día
                .FirstOrDefaultAsync(); // Obtener el primer mecánico disponible

            return mecanicoDisponible;
        }

        public async Task<List<int>> ObtenerHorasDisponibles(int servicioId, DateOnly dia)
        {
            // Definimos las horas del día (ejemplo: 8 a 17, es decir, de 8 AM a 5 PM)
            int horaInicio = 8;
            int horaFin = 16;

            // Lista de todas las horas posibles
            var horas = Enumerable.Range(horaInicio, horaFin - horaInicio).ToList();

            // Obtener los mecánicos que pueden atender el servicio solicitado
            var mecanicos = await _context.Mecanicos
                .Where(m => m.Servicios.Any(s => s.Id == servicioId))
                .ToListAsync();

            // Filtrar las horas donde hay disponibilidad
            var horasDisponibles = new List<int>();

            foreach (var hora in horas)
            {
                // Verificar si no hay órdenes asignadas a todos los mecánicos en esta hora
                bool todosOcupados = await _context.Ordenes
                    .AnyAsync(o => o.ServicioId == servicioId &&
                                   o.Dia == dia &&
                                   o.Hora == hora &&
                                   mecanicos.All(m => m.UsuarioId == o.MecanicoAsignadoId));

                if (!todosOcupados)
                {
                    horasDisponibles.Add(hora);
                }
            }

            return horasDisponibles;
        }
    }
}
