using Dominio.Comun;
using Dominio.Entidades;


namespace Dominio.Repositorios
{
    public interface IMecanicoRepository
    {
        Task<Resultado> AgregarAsync(Mecanico mecanico);
        Task<Resultado> AsignarServiciosAMecanicoAsync(string usuarioId, List<int> servicioIds);
        Task<Resultado> EliminarAsync(string usuarioId);
        Task<Mecanico?> ObtenerMecanicoPorIdAsync(string usuarioId);
        Task<List<Mecanico>> ObtenerMecanicosAsync();
        Task<List<Mecanico?>> ObtenerMecanicosDisponiblesAsync();
    }
}
