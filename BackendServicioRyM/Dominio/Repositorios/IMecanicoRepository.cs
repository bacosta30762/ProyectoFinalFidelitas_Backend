using Dominio.Comun;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IMecanicoRepository
    {
        Task<Resultado> AgregarAsync(Mecanico mecanico);
        Task<Resultado> AsignarServiciosAMecanicoAsync(string usuarioId, List<int> servicioIds);
        Task<Mecanico?> ObtenerMecanicoPorIdAsync(string usuarioId);
    }
}
