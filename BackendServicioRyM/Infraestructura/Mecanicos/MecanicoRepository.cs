using Aplicacion.DataBase;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Repositorios;

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
    }
}
