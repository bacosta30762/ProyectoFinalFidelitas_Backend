using Aplicacion.DataBase;
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

            // Obtener servicios por ID
            var servicios = await _context.Servicios
                .Where(s => servicioIds.Contains(s.Id))
                .ToListAsync();

            // Asignar servicios al mecánico
            foreach (var servicio in servicios)
            {
                if (!mecanico.Servicios.Contains(servicio))
                {
                    mecanico.Servicios.Add(servicio);
                }
            }

            await _context.SaveChangesAsync();
            return Resultado.Exitoso();
        }

        public async Task<List<Mecanico>> ObtenerMecanicosDisponiblesAsync()
        {
            return await _context.Mecanicos.ToListAsync();
        }
    }
}
