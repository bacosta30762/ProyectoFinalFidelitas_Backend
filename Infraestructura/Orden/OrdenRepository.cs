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

        public async Task<string?> ObtenerMecanicoDisponibleAsync(int idServicio, DateOnly dia, int hora)
        {
            // Obtener los mecánicos que pueden realizar el servicio solicitado
            var mecanicosDelServicio = await _context.Mecanicos
                .Where(m => m.Servicios.Any(s => s.Id == idServicio))
                .Select(m => m.UsuarioId)
                .ToListAsync();

            // Obtener los mecánicos ocupados en la fecha y hora especificadas
            var mecanicosOcupados = await _context.Ordenes
                .Where(o => o.Dia == dia && o.Hora == hora)
                .Select(o => o.MecanicoAsignadoId)
                .ToListAsync();

            // Excluir los mecánicos ocupados (ya sea con el mismo servicio o con otros)
            var mecanicosDisponibles = mecanicosDelServicio.Except(mecanicosOcupados);

            // Retornar el primer mecánico disponible, o null si no hay ninguno
            return mecanicosDisponibles.FirstOrDefault();
        }

        public async Task<Orden?> ObtenerOrdenPorNumeroAsync(int numeroOrden)
        {
            return await _context.Ordenes.FindAsync(numeroOrden);
        }

        public async Task<List<int>> ObtenerHorasDisponiblesAsync(int servicioId, DateOnly dia)
        {
            // Verificar si el día está bloqueado
            var diaBloqueado = await _context.DiasBloqueados.AnyAsync(db => db.Dia == dia);
            if (diaBloqueado)
            {
                // Si el día está bloqueado, no hay horas disponibles
                return new List<int>();
            }

            int horaInicio = 8;
            int horaFin = 16;

            // Lista de todas las horas posibles
            var horas = Enumerable.Range(horaInicio, horaFin - horaInicio + 1).ToList();

            // Obtener los IDs de los mecánicos que pueden atender el servicio solicitado
            var mecanicosDelServicio = await _context.Mecanicos
                .Where(m => m.Servicios.Any(s => s.Id == servicioId))
                .Select(m => m.UsuarioId)
                .ToListAsync();

            var horasDisponibles = new List<int>();

            foreach (var hora in horas)
            {
                // Obtener los mecánicos ocupados en esta hora con cualquier servicio
                var mecanicosOcupados = await _context.Ordenes
                    .Where(o => o.Dia == dia && o.Hora == hora)
                    .Select(o => o.MecanicoAsignadoId)
                    .ToListAsync();

                // Determinar los mecánicos disponibles que pueden atender este servicio
                var mecanicosDisponibles = mecanicosDelServicio.Except(mecanicosOcupados);

                // Si hay al menos un mecánico disponible, agregar la hora como disponible
                if (mecanicosDisponibles.Any())
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

        public async Task BloquearDiaAsync(DateOnly dia)
        {
            // Verificar si ya hay órdenes para el día
            var ordenesDelDia = await _context.Ordenes.Where(o => o.Dia == dia).ToListAsync();

            if (ordenesDelDia.Any())
            {
                // Eliminar órdenes si existen
                _context.Ordenes.RemoveRange(ordenesDelDia);
                await _context.SaveChangesAsync();
            }

            // Bloquear el día
            var diaBloqueado = new DiaBloqueado { Dia = dia };
            await _context.DiasBloqueados.AddAsync(diaBloqueado);
            await _context.SaveChangesAsync();
        }

        public async Task DesbloquearDiaAsync(DateOnly dia)
        {
            // Eliminar el día bloqueado si existe
            var diaBloqueado = await _context.DiasBloqueados.FirstOrDefaultAsync(db => db.Dia == dia);
            if (diaBloqueado != null)
            {
                _context.DiasBloqueados.Remove(diaBloqueado);
                await _context.SaveChangesAsync();
            }
        }

    }
}

