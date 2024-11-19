using Aplicacion.DataBase;
using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Mecanicos
{
    public class MecanicoRepository : IMecanicoRepository
    {
        private readonly DatabaseContext _context;

        public MecanicoRepository(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Resultado> AgregarAsync(Mecanico mecanico)
        {
            try
            {
                await _context.Mecanicos.AddAsync(mecanico);
                await _context.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (Exception ex)
            {
                return Resultado.Fallido(new[] {"Error al ingresar mecánico: " + ex.Message});
            }
        }

        public async Task<Mecanico?> ObtenerMecanicoPorIdAsync(string usuarioId)
        {
            return await _context.Mecanicos
                .Include(m => m.Servicios) 
                .FirstOrDefaultAsync(m => m.UsuarioId == usuarioId);
        }

        public async Task<Resultado> AsignarServiciosAMecanicoAsync(string usuarioId, List<int> servicioIds)
        {
            var mecanico = await ObtenerMecanicoPorIdAsync(usuarioId);
            if (mecanico == null)
            {
                return Resultado.Fallido(new[] { "Mecánico no encontrado." });
            }

            // Obtener los servicios actuales del mecánico
            var serviciosActuales = mecanico.Servicios.ToList();

            // Obtener los servicios por ID de la nueva lista
            var nuevosServicios = await _context.Servicios
                .Where(s => servicioIds.Contains(s.Id))
                .ToListAsync();

            // Determinar los servicios que deben ser eliminados
            var serviciosAEliminar = serviciosActuales
                .Where(s => !nuevosServicios.Contains(s))
                .ToList();

            // Determinar los servicios que deben ser agregados
            var serviciosAAgregar = nuevosServicios
                .Where(s => !serviciosActuales.Contains(s))
                .ToList();

            // Eliminar los servicios que no están en la nueva lista
            foreach (var servicio in serviciosAEliminar)
            {
                mecanico.Servicios.Remove(servicio);
            }

            // Agregar los nuevos servicios
            foreach (var servicio in serviciosAAgregar)
            {
                mecanico.Servicios.Add(servicio);
            }

            await _context.SaveChangesAsync();
            return Resultado.Exitoso();


        }

        public async Task<List<Mecanico>> ObtenerMecanicosDisponiblesAsync()
        {
            //return await _context.Mecanicos.ToListAsync();
            return await _context.Mecanicos
               .Include(m => m.Usuario) // Incluimos la relación con la tabla de usuarios
               .ToListAsync();
        }

        public async Task<Resultado> EliminarAsync(string usuarioId)
        {
            try
            {
                var mecanico = await ObtenerMecanicoPorIdAsync(usuarioId);
                if (mecanico == null)
                {
                    return Resultado.Fallido(new[] { "Mecánico no encontrado." });
                }

                // Eliminar servicios relacionados
                mecanico.Servicios.Clear();

                // Eliminar mecánico
                _context.Mecanicos.Remove(mecanico);
                await _context.SaveChangesAsync();

                return Resultado.Exitoso();
            }
            catch (Exception ex)
            {
                return Resultado.Fallido(new[] { "Error al eliminar mecánico: " + ex.Message });
            }
        }
    }
}
